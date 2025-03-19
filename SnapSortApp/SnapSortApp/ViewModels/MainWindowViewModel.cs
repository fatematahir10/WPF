using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Drawing;
using SnapSortApp.Models;
using SnapSortApp.Services;
using Microsoft.Win32;

namespace SnapSortApp.ViewModels 
{

    public class MainWindowViewModel : BaseViewModel
    {
        public string TargetFolder { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SortedImages");

        public ObservableCollection<ImageModel> Images { get; set; } = new();

        public ICommand SortImagesCommand { get; }
        public ICommand ChangeFolderCommand { get; }
        public ICommand BrowseImagesCommand { get; }

        public MainWindowViewModel()
        {
            SortImagesCommand = new RelayCommand(SortImages);
            ChangeFolderCommand = new RelayCommand(ChangeFolder);
            BrowseImagesCommand = new RelayCommand(BrowseImages);
        }

        private void BrowseImages()
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Select Images",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    if (IsImage(filePath))
                    {
                        Images.Add(new ImageModel(filePath));
                    }
                }
            }
        }

        private bool IsImage(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            return ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp";
        }
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

        private void SortImages()
        {
            if (Images.Count == 0)
            {
                MessageBox.Show("No images to sort!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Directory.CreateDirectory(TargetFolder);

            foreach (var image in Images)
            {
                string category = image.FileSize < 500 ? "Small" : image.FileSize < 2000 ? "Medium" : "Large";
                string targetFolder = Path.Combine(TargetFolder, category);
                Directory.CreateDirectory(targetFolder);

                string destPath = Path.Combine(targetFolder, image.FileName);
                File.Copy(image.FilePath, destPath, true);
            }

            MessageBox.Show($"Images sorted successfully!\n\nSaved in: {TargetFolder}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ChangeFolder()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Select Folder",
                ValidateNames = false
            };

            if (dialog.ShowDialog() == true)
            {
                TargetFolder = System.IO.Path.GetDirectoryName(dialog.FileName) ?? TargetFolder;
                OnPropertyChanged(nameof(TargetFolder));
            }
        }
    }
}
