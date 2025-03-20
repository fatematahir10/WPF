using SnapSortApp.Services;
using SnapSortApp.ViewModels;
using System.Windows.Controls;

namespace SnapSortApp.Views
{
    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            InitializeComponent();
            DataContext = new AboutViewModel();
        }
    }
}
