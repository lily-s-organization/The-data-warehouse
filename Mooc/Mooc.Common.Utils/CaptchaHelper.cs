using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Common.Utils
{
    public class CaptchaHelper
    {
        static Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Brown, Color.DarkBlue };
        static string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="length">指定验证码的长度</param>
        /// <returns></returns>
        public static string CreateValidateCode(int length)
        {
            Random rnd = new Random();
            string chkCode = string.Empty;
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };

            //生成验证码字符串 
            for (int i = 0; i < length; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            return chkCode;
        }


        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="validateCode">验证码</param>
        public static byte[] CreateValidateGraphic(string validateCode, int w = 80, int h = 35)
        {
            Bitmap image = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();

                Color clr = color[random.Next(color.Length)];
                Font ft = new Font("Times New Roman", 16);
                //清空图片背景色
                g.Clear(Color.White);

                //画图片的干扰线
                for (int i = 0; i < 6; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    clr = color[random.Next(color.Length)];
                    g.DrawLine(new Pen(clr), x1, y1, x2, y2);
                }

                //画验证码字符串 
                for (int i = 0; i < validateCode.Length; i++)
                {
                    string fnt = font[random.Next(font.Length)];
                    ft = new Font(fnt, 16);
                    clr = color[random.Next(color.Length)];
                    g.DrawString(validateCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 15 + 8, (float)6);
                }
               
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    clr = color[random.Next(color.Length)];
                    image.SetPixel(x, y, clr);
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }


    }
}
