using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;

namespace Mooc.Common.Utils
{
    public class QRCodeHelper
    {
        public static byte[] GetQRCode(string url, string imgurl)
        {

     
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 8;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            System.Drawing.Image image = qrCodeEncoder.Encode(url);
            System.IO.MemoryStream MStream = new System.IO.MemoryStream();
            image.Save(MStream, System.Drawing.Imaging.ImageFormat.Png);

            System.IO.MemoryStream MStream1 = new System.IO.MemoryStream();

            Image destImg = get_image(imgurl);

            CombinImage(image, destImg).Save(MStream1, System.Drawing.Imaging.ImageFormat.Png);
        
            return  MStream1.ToArray();


        }

        /// <summary>    
        /// 调用此函数后使此两种图片合并，类似相册，有个    
        /// 背景图，中间贴自己的目标图片    
        /// </summary>    
        /// <param name="imgBack">粘贴的源图片</param>    
        /// <param name="destImg">粘贴的目标图片</param>    
        public static Image CombinImage(Image imgBack, Image destImg)
        {
            Image img = destImg; //Image.FromFile(destImg);        //照片图片    
  

            if (img.Height != 48 || img.Width != 48)
            {
                img = KiResizeImage(img, 48, 48, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
          
            GC.Collect();
         
            return imgBack;
        }


        /// <summary>    
        /// Resize图片    
        /// </summary>    
        /// <param name="bmp">原始Bitmap</param>    
        /// <param name="newW">新的宽度</param>    
        /// <param name="newH">新的高度</param>    
        /// <param name="Mode">保留着，暂时未用</param>    
        /// <returns>处理以后的图片</returns>    
        public static Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }


        public static Image get_image(string url)
        {
            Image image = null;

            System.Net.WebRequest webreq = System.Net.WebRequest.Create(url);
            System.Net.WebResponse webres = webreq.GetResponse();

            using (System.IO.Stream stream = webres.GetResponseStream())
            {
                image = Image.FromStream(stream);
            }

            return image;  
         
        }

    }
}
