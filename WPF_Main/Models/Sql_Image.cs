using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_Main.Models
{
    public class Sql_Image
    {

        public byte[] Data { get; set; }

        public ImageSource Image
        {
            get
            {
                if(Data == null) return null;

                BitmapImage image = new BitmapImage();

                //int height = 0;
                //int width = 0;

                //using (MemoryStream ms = new MemoryStream(Data))
                //{
                //    ms.Position = 0;

                //    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                //    height = img.Height;
                //    width = img.Width;

                //    ms.Position = 0;

                //    var bit = Bitmap.FromStream(ms);
                //}

                using (MemoryStream ms = new MemoryStream(Data))
                {
                    ms.Position = 0;

                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.None;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = ms;

                    image.EndInit();

                }

                

                return image;
            }
        }

        public float Width 
        { 
            get
            {
                return (Image as BitmapSource).PixelWidth;
            }
        }

        public float Height

        {
            get
            {
                return (Image as BitmapSource).PixelHeight;
            }
        }

    }
}
