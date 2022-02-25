using System;
using System.Collections.Generic;
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

                using (MemoryStream ms = new MemoryStream(Data))
                {
                    ms.Position = 0;

                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = ms;
                    image.EndInit();
                }

                return image;
            }
        }



    }
}
