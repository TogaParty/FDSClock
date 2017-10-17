using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Windows.UI.Composition;
using Windows.UI.ViewManagement;
using Windows.Storage;

namespace FDSClock
{
    class FDSSettings
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public enum DisplayMode {
            Clock800x480,
            Clock1920x1080,
            Clock3840x2160,
            ScreenSaver1920x1080,
            ScreenSaver3840x2160
        };

        public Guid ClientID { get; set; }
        public int ZipCode { get; set; } = -1;
        public int ScreenWidth { get; set; } = -1;
        public int ScreenHeight { get; set; } = -1;

        public DisplayMode Mode { get; set; } = DisplayMode.Clock800x480;

        public FDSSettings(DisplayMode mode)
        {
            try
            {
                ClientID = (Guid)localSettings.Values["ClientID"];
                ZipCode = Convert.ToInt32(localSettings.Values["ZipCode"]);
            }
            catch (Exception ex)
            {
                ClientID = Guid.NewGuid();
                localSettings.Values["ClientID"] = ClientID;
                ZipCode = 98028;
                localSettings.Values["ZipCode"] = ZipCode;
            }
                                    
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            ScreenWidth = Convert.ToInt32(bounds.Width);
            ScreenHeight = Convert.ToInt32(bounds.Height);
        }
    }
}
