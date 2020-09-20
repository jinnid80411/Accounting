using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;


namespace Accounting
{

    public partial class GenerateAuthCode : System.Web.UI.Page
    {
        int codenumber = 5;

        protected void Page_Load(object sender, EventArgs e)
        {
            createCodeImage(generateCode());
        }
        public string generateCode()
        {
            int number;
            char code;
            string checkcode = "";
            Random random = new Random();
            for (int i = 0; i < codenumber; i++)
            {
                number = random.Next();
                //if (number % 3 == 0)
                //{
                string s = "0";
                code = Convert.ToChar(Convert.ToInt32(s[0]) + (number % 10));
                //}          
                //else
                //{
                //    string s = "A";
                //    code = Convert.ToChar(Convert.ToInt32(s[0]) + (number % 26));
                //}
                checkcode += code.ToString();
            }
            Session["AuthCode"] = checkcode;
            return checkcode;
        }

        public void createCodeImage(string checkcode)
        {
            if (checkcode == null || checkcode == "")
            {
                return;
            }

            Bitmap image = new Bitmap(150, 45);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                g.Clear(Color.White);
                for (int i = 0; i < 24; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, x2, y1, y2);
                }
                int bold = random.Next();
                Font font = null;
                if (bold % 2 == 0)
                {
                    font = new Font("Arial", 28, System.Drawing.FontStyle.Bold);
                }
                else
                {
                    font = new Font("Arial", 28, System.Drawing.FontStyle.Italic);
                }
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2F, true);

                g.DrawString(checkcode, font, brush, 2, 2);
                for (int i = 0; i < 2000; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Gif);

                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());

            }
            catch (Exception ex)
            {


            }
            finally
            {
                g.Dispose();

                image.Dispose();

            }


        }
    }
}