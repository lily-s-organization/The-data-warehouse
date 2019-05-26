using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mooc.Common.Utils
{
    public class VerifyHelper
    {
        public static MemoryStream CreateVerify(string vnum)
        {
            System.Drawing.Bitmap img = null;
            System.Drawing.Graphics g = null;
            MemoryStream ms = null;
            int gheight = vnum.Length * 15;
            img = new System.Drawing.Bitmap(gheight, 30);
            g = System.Drawing.Graphics.FromImage(img);

            ////背景颜色             
            System.Web.UI.WebControls.WebColorConverter wcc = new System.Web.UI.WebControls.WebColorConverter();
            g.Clear((System.Drawing.Color)wcc.ConvertFromString("#f1efef")); //

            ////文字字体 
            System.Drawing.Font f = new System.Drawing.Font("Elephant", 14);//arial black

            ////文字颜色 
            System.Drawing.SolidBrush s = new System.Drawing.SolidBrush((System.Drawing.Color)wcc.ConvertFromString("#5e9c2d"));
            g.DrawString(vnum, f, s, 3, 3);

            ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            g.Dispose();
            img.Dispose();
            return ms;
        }

        public static string Randnum(int vcodenum)
        {
            //string vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string vchar = "0,1,2,3,4,5,6,7,8,9";
            string[] vcarray = vchar.Split(new Char[] { ',' });
            string vnum = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 1; i < vcodenum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(9);//35

                if (temp != -1 && temp == t)
                {
                    return Randnum(vcodenum);
                }
                temp = t;
                vnum += vcarray[t];
            }
            return vnum;
        }

        public static string CreateVerifyString(int vcodenum)
        {
            string vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] vcarray = vchar.Split(new Char[] { ',' });
            string vnum = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 1; i < vcodenum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(9);//35

                if (temp != -1 && temp == t)
                {
                    return Randnum(vcodenum);
                }
                temp = t;
                vnum += vcarray[t];
            }
            return vnum;
        } 
    }
}
