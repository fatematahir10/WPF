using SnapSortApp.Services;
using System.Windows.Controls;

namespace SnapSortApp.Views
{
    public partial class AboutView : UserControl
    {
        public string AppName => AppInfoHelper.AppName;
        public string AppVersion => AppInfoHelper.AppVersion;
        public string Copyright => AppInfoHelper.Copyright;
        public string HelpLink => AppInfoHelper.HelpLink;

        public AboutView()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
