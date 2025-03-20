using System;
using System.Reflection;

namespace SnapSortApp.Services
{
    public static class AppInfoHelper
    {
        public static string AppName => "SnapSortApp";

        public static string AppVersion
            => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown Version";

        public static string Copyright => $"© {DateTime.Now.Year} SnapSortApp. All rights reserved.";

        public static string HelpLink => "https://github.com/your-repo/SnapSortApp/wiki";
    }
}
