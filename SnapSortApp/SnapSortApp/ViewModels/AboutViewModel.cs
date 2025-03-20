using SnapSortApp.Services;
using System.Diagnostics;
using System.Windows.Input;

namespace SnapSortApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string AppName => AppInfoHelper.AppName;
        public string AppVersion => AppInfoHelper.AppVersion;
        public string Copyright => AppInfoHelper.Copyright;
        public string HelpLink => AppInfoHelper.HelpLink;

        public ICommand OpenHelpCommand { get; }

        public AboutViewModel()
        {
            OpenHelpCommand = new RelayCommand(OpenHelp);
        }

        private void OpenHelp()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = HelpLink,
                UseShellExecute = true
            });
        }
    }
}
