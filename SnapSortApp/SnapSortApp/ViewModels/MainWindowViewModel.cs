using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Drawing;
using SnapSortApp.Models;
using SnapSortApp.Services;
using Microsoft.Win32;
using SnapSortApp.Views;
using System.Windows.Controls;

namespace SnapSortApp.ViewModels 
{

    public class MainWindowViewModel : BaseViewModel
    {
        private int _selectedTabIndex;
        private UserControl _currentView;

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex));
                SwitchView();
            }
        }

        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainWindowViewModel()
        {
            CurrentView = new MainView(); // Default View
        }

        private void SwitchView()
        {
            CurrentView = SelectedTabIndex switch
            {
                0 => new MainView(),
                1 => new AboutView(),
                2 => new SettingsView(),
                _ => new MainView()
            };
        }
    }
}
