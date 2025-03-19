using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace SnapSortApp.Models
{
    public class ImageModel
    {
        public string FilePath { get; set; }
        public string FileName => Path.GetFileName(FilePath);
        public long FileSize => new FileInfo(FilePath).Length / 1024; // KB
        public string Resolution { get; set; }

        public ImageModel(string filePath)
        {
            FilePath = filePath;
            using (var img = Image.FromFile(filePath))
            {
                Resolution = $"{img.Width}x{img.Height}";
            }
        }
    }
}
