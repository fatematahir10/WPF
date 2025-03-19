using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Drawing;
using SnapSortApp.Models;
using SnapSortApp.Services;

namespace SnapSortApp.ViewModels 
{

    public class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<ImageModel> Images { get; set; } = new();

        public ICommand SortImagesCommand { get; }

        public MainWindowViewModel()
        {
            SortImagesCommand = new RelayCommand(SortImages);
        }

        // Drag and Drop Event Handler
        public void ImageDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    if (IsImage(file))
                    {
                        Images.Add(new ImageModel(file));
                    }
                }
            }
        }

        private bool IsImage(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            return ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".bmp";
        }

        // Sorting Logic
        private void SortImages()
        {
            if (Images.Count == 0)
            {
                MessageBox.Show("No images to sort!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string baseDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SortedImages");
            Directory.CreateDirectory(baseDir);

            foreach (var image in Images)
            {
                string category = image.FileSize < 500 ? "Small" : image.FileSize < 2000 ? "Medium" : "Large";
                string targetFolder = Path.Combine(baseDir, category);
                Directory.CreateDirectory(targetFolder);

                string destPath = Path.Combine(targetFolder, image.FileName);
                File.Copy(image.FilePath, destPath, true);
            }

            MessageBox.Show("Images sorted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
