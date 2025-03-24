using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;


namespace SnapSortApp.Models
{
    public class ImageModel
    {
        public string FilePath { get; set; }
        public string FileName => Path.GetFileName(FilePath);
        public long FileSize => new FileInfo(FilePath).Length / 1024; 
        public string Resolution { get; set; }

        public ImageModel(string filePath)
        {
            FilePath = filePath;
            using (var img = Image.FromFile(filePath))
            {
                Resolution = $"{img.Width}x{img.Height}";
            }
        }

        public BitmapImage Thumbnail
        {
            get
            {
                BitmapImage bitmap = new();
                using (FileStream stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.DecodePixelWidth = 100;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    bitmap.Freeze();
                }
                return bitmap;
            }
        }
    }
}
