using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPF_Main.Models;

namespace WPF_Main.Utils
{
    /// <summary>
    /// Класс для работы с байтами. В основном с байтами изображений
    /// </summary>
    public static class ByteOperation
    {

        public static Sql_Image[] GetImages(byte[] data)
        {

            if (data == null) return null;

            List<Sql_Image> _images = new List<Sql_Image>();

            int countImages = BitConverter.ToInt32(data, 0);

            int indexInByteArr = 4;

            for (int i = 0; i < countImages; i++)
            {
                int countBytes = BitConverter.ToInt32(data, indexInByteArr);

                indexInByteArr += 4;

                byte[] dataImage = new byte[countBytes];

                System.Buffer.BlockCopy(data, indexInByteArr, dataImage, 0, countBytes);

                indexInByteArr += countBytes;

                using (MemoryStream ms = new MemoryStream(dataImage))
                {
                    ms.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                }

                _images.Add(new Sql_Image() { Data = dataImage });
            }

            return _images.ToArray();
        }

        public static Sql_Image GetImage(byte[] data)
        {

            if (data == null) return null;

            Sql_Image _image = null;

            int countImages = BitConverter.ToInt32(data, 0);

            int indexInByteArr = 4;

            for (int i = 0; i < countImages; i++)
            {
                int countBytes = BitConverter.ToInt32(data, indexInByteArr);

                indexInByteArr += 4;

                byte[] dataImage = new byte[countBytes];

                System.Buffer.BlockCopy(data, indexInByteArr, dataImage, 0, countBytes);

                indexInByteArr += countBytes;

                using (MemoryStream ms = new MemoryStream(dataImage))
                {
                    ms.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                }

                _image = new Sql_Image() { Data = dataImage };
            }

            return _image;
        }


        public static byte[] GetByteFromBuffer()
        {
            List<byte> vs = new List<byte>();

            var image = Clipboard.GetImage();

            if(image == null) return null;

            byte[] data = ImageToByte(image);

            vs.AddRange(BitConverter.GetBytes(1));
            vs.AddRange(BitConverter.GetBytes(data.Length));
            vs.AddRange(data);

            return vs.ToArray();
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static byte[] GetByteImages(string[] paths)
        {

            List<byte> vs = new List<byte>();

            byte[] dataCountImages = BitConverter.GetBytes(paths.Length);

            vs.AddRange(dataCountImages);

            foreach (var path in paths)
            {
                byte[] dataImage = File.ReadAllBytes(path);

                vs.AddRange(BitConverter.GetBytes(dataImage.Length));
                vs.AddRange(dataImage);
            }

            return vs.ToArray();
        }

        public static byte[] GetByteImage(string path)
        {
            List<byte> vs = new List<byte>();

            byte[] dataCountImages = BitConverter.GetBytes(1);

            vs.AddRange(dataCountImages);

            byte[] dataImage = File.ReadAllBytes(path);

            vs.AddRange(BitConverter.GetBytes(dataImage.Length));
            vs.AddRange(dataImage);

            return vs.ToArray();
        }


    }
}
