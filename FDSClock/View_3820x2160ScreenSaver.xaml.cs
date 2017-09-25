using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FDSClock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class View_3820x2160ScreenSaver : Page
    {
        string nowWeatherPath = "Assets/Images/Sunny_Light.png";
        string nowWeatherShadowPath = "Assets/Images/Sunny_Dark.png";

        string todayWeatherPath = "Assets/Images/Sunny_Light.png";
        string todayWeatherShadowPath = "Assets/Images/Sunny_Dark.png";

        string tomorrowWeatherPath = "Assets/Images/Rainy_Light.png";
        string tomorrowWeatherShadowPath = "Assets/Images/Rainy_Dark.png";

        public View_3820x2160ScreenSaver()
        {
            this.InitializeComponent();

            DispatcherTimer clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Tick += Clock_Tick;
            clockTimer.Start();

            DispatcherTimer weatherTimer = new DispatcherTimer();
            weatherTimer.Interval = TimeSpan.FromMinutes(1);
            weatherTimer.Tick += Weather_Tick;
            weatherTimer.Start();
        }

        private void Clock_Tick(object sender, object e)
        {
            FDSDateTime fdsDateTime = new FDSDateTime();
            fdsDateTime.Update();

            DateBlock1.Text = fdsDateTime.DisplayDate;
            //this.DateBlock1Shadow.Text = fdsDateTime.DisplayDate;
            this.DateBlock2.Text = " of " + fdsDateTime.DisplayMonth;
            //this.DateBlock2Shadow.Text = " of " + fdsDateTime.DisplayMonth;

            this.Time.Text = fdsDateTime.DisplayHour + ":" + fdsDateTime.DisplayMinute;
            //this.TimeShadow.Text = fdsDateTime.DisplayHour + ":" + fdsDateTime.DisplayMinute;

            this.AMPM.Text = fdsDateTime.DisplayAMPM;
            //this.AMPMShadow.Text = fdsDateTime.DisplayAMPM;
            this.Seconds.Text = fdsDateTime.DisplaySecond;
            //this.SecondsShadow.Text = fdsDateTime.DisplaySecond;

            this.Weekday.Text = fdsDateTime.DayOfWeek;
            //this.WeekdayShadow.Text = fdsDateTime.DayOfWeek;
        }

        private void Weather_Tick(object sender, object e)
        {
            nowWeatherPath = "Assets/Images/Rainy_Light.png";
            nowWeatherShadowPath = "Assets/Images/Rainy_Dark.png";

            todayWeatherPath = "Assets/Images/Snowy_Light.png";
            todayWeatherShadowPath = "Assets/Images/Snowy_Dark.png";

            tomorrowWeatherPath = "Assets/Images/Snowy_Light.png";
            tomorrowWeatherShadowPath = "Assets/Images/Snowy_Dark.png";
        }
    }
}
