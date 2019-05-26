using log4net;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Common.Utils
{
    public class RabbitMqHelper
    {
        private static ILog logger = LogManager.GetLogger(typeof(RabbitMqHelper));
        private static IConnection Iconection;
        private static object _lock = new object();
        public static IConnection GetIconection()
        {
            try
            {
                if (Iconection == null)
                {
                    lock (_lock)
                    {
                        if (Iconection == null)
                        {
                            Iconection = GetFactory().CreateConnection();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Iconection;
        }
        public static ConnectionFactory GetFactory()
        {
            string mqConnectStrings = ConfigurationManager.AppSettings["mqConnectStrings"];
            string[] arry = mqConnectStrings.Split(';');
            string host = arry[0];
            int port = Convert.ToInt32(arry[1]);
            string vhost = arry[2];
            string userName = arry[3];
            string pwd = arry[4];
            ConnectionFactory factory = new ConnectionFactory() { HostName = host, Port = port, VirtualHost = vhost, UserName = userName, Password = pwd };
            return factory;
        }
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="msg"></param>
        public static void PublishMsg(MQMessage msg)
        {

            using (IConnection conn = GetFactory().CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(msg.Msg);
                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;
                    channel.BasicPublish(msg.Exchange, msg.RouteKey, properties, buffer);
                }
            }
        }
        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="routekey"></param>
        /// <param name="myproc"></param>
        public static void ConsumeMsg(string routekey, Action<string> myproc)
        {

            using (var connection = GetIconection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicQos(0, 2, false);
                channel.BasicConsume(queue: routekey,
                                      noAck: true,//和tcp协议的ack一样，为false则服务端必须在收到客户端的回执（ack）后才能删除本条消息
                                      consumer: consumer);
                while (true)
                {
                    try
                    {
                        BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        string message = Encoding.UTF8.GetString(ea.Body);
                        myproc(message);
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                }

            }

        }
        
    }

    public class MQMessage
    {
        public string Msg { get; set; }
        public string Exchange { get; set; }
        public string RouteKey { get; set; }
    }
}
