using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

using System.Diagnostics;
using Foundation.Weather;
using System.Reflection;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FDSClock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class View_800x480Clock : Page
    {
        string currentWeatherIconPath = "ms-appx:///Assets/Images/Icons/loading.svg";
        string updatedCurrentWeatherIconPath = "ms-appx:///Assets/Images/Icons/loading.svg";
        string todayWeatherIconPath = "ms-appx:///Assets/Images/Icons/loading.svg";
        string updatedTodayWeatherIconPath = "ms-appx:///Assets/Images/Icons/loading.svg";
        string tomorrowWeatherIconPath = "ms-appx:///Assets/Images/Icons/loading.svg";
        string updatedTomorrowWeatherIconPath = "ms-appx:///Assets/Images/Icons/loading.svg";

        WeatherUnderground weatherUnderground = null;

        public View_800x480Clock()
        {
            this.InitializeComponent();

            var version = typeof(App).GetTypeInfo().Assembly.GetName().Version;
            //BuildLabel.Text = version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision + "." + version.MinorRevision;

            weatherUnderground = new WeatherUnderground(98028, WeatherUnderground.Measurement.Imperial);
            weatherUnderground.GetCurrentWeather();
            weatherUnderground.GetTenDayWeather();

            DispatcherTimer clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Tick += Clock_Tick;
            clockTimer.Start();

            DispatcherTimer updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(5);
            updateTimer.Tick += UpdateUI_Tick;
            updateTimer.Start();

            DispatcherTimer currentWeatherTimer = new DispatcherTimer();
            currentWeatherTimer.Interval = TimeSpan.FromMinutes(15);
            currentWeatherTimer.Tick += CurrentWeatherTimer_Tick;
            currentWeatherTimer.Start();

            DispatcherTimer tenDayWeatherTimer = new DispatcherTimer();
            tenDayWeatherTimer.Interval = TimeSpan.FromHours(6);
            tenDayWeatherTimer.Tick += TenDayWeatherTimer_Tick;
            tenDayWeatherTimer.Start();
        }

        private void Clock_Tick(object sender, object e)
        {
            FDSDateTime fdsDateTime = new FDSDateTime();
            fdsDateTime.Update();

            DateBlock1.Text = fdsDateTime.DisplayDate;
            this.DateBlock2.Text = fdsDateTime.DisplayMonth;
            this.Time.Text = fdsDateTime.DisplayHour + ":" + fdsDateTime.DisplayMinute;
            this.AMPM.Text = fdsDateTime.DisplayAMPM;
            this.Seconds.Text = fdsDateTime.DisplaySecond;
            this.Weekday.Text = fdsDateTime.DayOfWeek;
        }

        private void UpdateUI_Tick(object sender, object e)
        {
            FDSDateTime fdsDateTime = new FDSDateTime();
            fdsDateTime.Update();

            Debug.WriteLine("Hit UIUpdate Timer @ " + fdsDateTime.DisplayHour + ":" + fdsDateTime.DisplayMinute);

            if (weatherUnderground.CurrentWeather != null)
            {
                CurrentWeatherText.Text = weatherUnderground.CurrentWeather.current_observation.weather;
                updatedCurrentWeatherIconPath = GetWeatherIconPath(weatherUnderground.CurrentWeather.current_observation.weather);
                CurrentTemp.Text = Math.Round(weatherUnderground.CurrentWeather.current_observation.temp_f, 0).ToString() + "°";

                if (currentWeatherIconPath != updatedCurrentWeatherIconPath)
                {
                    SvgImageSource svg = new SvgImageSource(new Uri(updatedCurrentWeatherIconPath));
                    svg.RasterizePixelHeight = 792;
                    svg.RasterizePixelWidth = 612;

                    CurrentWeatherIcon.Source = svg;
                    currentWeatherIconPath = updatedCurrentWeatherIconPath;
                }
            }

            if (weatherUnderground.TenDayWeather != null)
            {
                TodayWeatherText.Text = weatherUnderground.TenDayWeather.forecast.simpleforecast.forecastday[0].conditions;
                updatedTodayWeatherIconPath = GetWeatherIconPath(weatherUnderground.TenDayWeather.forecast.simpleforecast.forecastday[0].conditions);
                TodayHighTemp.Text = weatherUnderground.TenDayWeather.forecast.simpleforecast.forecastday[0].high.fahrenheit + "°";
                TodayLowTemp.Text = weatherUnderground.TenDayWeather.forecast.simpleforecast.forecastday[0].low.fahrenheit + "°";

                TomorrowWeatherText.Text = weatherUnderground.TenDayWeather.forecast.simpleforecast.forecastday[1].conditions;
                updatedTomorrowWeatherIconPath = GetWeatherIconPath(weatherUnderground.TenDayWeather.forecast.simpleforecast.forecastday[1].conditions);
                TomorrowHighTemp.Text = weatherUnderground.TenDayWeather.forecast.simpleforecast.forecastday[1].high.fahrenheit + "°";
                TomorrowLowTemp.Text = weatherUnderground.TenDayWeather.forecast.simpleforecast.forecastday[1].low.fahrenheit + "°";

                if (todayWeatherIconPath != updatedTodayWeatherIconPath)
                {
                    SvgImageSource svg = new SvgImageSource(new Uri(updatedTodayWeatherIconPath));
                    svg.RasterizePixelHeight = 792;
                    svg.RasterizePixelWidth = 612;

                    TodayWeatherIcon.Source = svg;
                    todayWeatherIconPath = updatedCurrentWeatherIconPath;
                }

                if (tomorrowWeatherIconPath != updatedTomorrowWeatherIconPath)
                {
                    SvgImageSource svg = new SvgImageSource(new Uri(updatedTomorrowWeatherIconPath));
                    svg.RasterizePixelHeight = 792;
                    svg.RasterizePixelWidth = 612;

                    TomorrowWeatherIcon.Source = svg;
                    tomorrowWeatherIconPath = updatedCurrentWeatherIconPath;

                    tomorrowWeatherIconPath = updatedTomorrowWeatherIconPath;
                }

                //TodayWeatherPanel.Visibility = Visibility.Visible;
                //TomorrowWeatherPanel.Visibility = Visibility.Visible;
            }
        }

        private void CurrentWeatherTimer_Tick(object sender, object e)
        {
            FDSDateTime fdsDateTime = new FDSDateTime();
            fdsDateTime.Update();

            Debug.WriteLine("Hit Now Weather Timer @ " + fdsDateTime.DisplayHour + ":" + fdsDateTime.DisplayMinute);
            weatherUnderground.GetCurrentWeather();
        }

        private void TenDayWeatherTimer_Tick(object sender, object e)
        {
            FDSDateTime fdsDateTime = new FDSDateTime();
            fdsDateTime.Update();

            Debug.WriteLine("Hit Ten Day Weather Timer @ " + fdsDateTime.DisplayHour + ":" + fdsDateTime.DisplayMinute);
            weatherUnderground.GetTenDayWeather();
        }

        private string GetWeatherIconPath(string weather)
        {
            string weatherPath = null;

            switch (weather)
            {
                case "Chance of Flurries":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/chanceflurries.svg";
                        break;
                    }
                case "Chance of Rain":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/chancerain.svg";
                        break;
                    }
                case "Chance of Sleet":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/chancesleet.svg";
                        break;
                    }
                case "Chance of Snow":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/chancesnow.svg";
                        break;
                    }
                case "Chance of a Thunderstorm":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/chancetstorms.svg";
                        break;
                    }
                case "Clear":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/clear.svg";
                        break;
                    }
                case "Flurries":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/flurries.svg";
                        break;
                    }
                case "Fog":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/fog.svg";
                        break;
                    }
                case "Hazey":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/hazey.svg";
                        break;
                    }
                case "Mostly Cloudy":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/mostlycloudy.svg";
                        break;
                    }
                case "Mostly Sunny":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/mostlysunny.svg";
                        break;
                    }
                case "Overcast":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/overcast.svg";
                        break;
                    }
                case "Partly Cloudy":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/partlycloudy.svg";
                        break;
                    }
                case "Partly Sunny":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/partlysunny.svg";
                        break;
                    }
                case "Rain":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/rain.svg";
                        break;
                    }
                case "Sleet":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/sleet.svg";
                        break;
                    }
                case "Snow":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/snow.svg";
                        break;
                    }
                case "Sunny":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/sunny.svg";
                        break;
                    }
                case "Thunderstorm":
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/tstorms.svg";
                        break;
                    }
                default:
                    {
                        weatherPath = "ms-appx:///Assets/Images/Icons/unknown.svg";
                        break;
                    }
            }

            return weatherPath;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            //this.SettingsPanel.Visibility = Visibility.Visible;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //this.SettingsPanel.Visibility = Visibility.Collapsed;
        }
    }
}
