using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace Assignment6CSE445


{
    public class CaptchaHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            string captchaText = "ABC12";


            using (Bitmap bitmap = new Bitmap(100, 30))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                Font font = new Font("Arial", 16, FontStyle.Bold);
                Brush brush = Brushes.Black;

                graphics.DrawString(captchaText, font, brush, new PointF(10, 5));

                // Return the static image
                context.Response.ContentType = "image/png";
                bitmap.Save(context.Response.OutputStream, ImageFormat.Png);
            }
        }

        public bool IsReusable => false;
    }
}