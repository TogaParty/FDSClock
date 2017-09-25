using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Diagnostics;
using Windows.UI.Composition;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FDSClock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            FDSSettings settings = new FDSSettings(FDSSettings.DisplayMode.Clock800x480);

            if (settings.ScreenHeight >= 1080 && settings.ScreenWidth >= 1920)
                settings.Mode = FDSSettings.DisplayMode.Clock1920x1080;

            if (settings.ScreenHeight >= 2160 && settings.ScreenWidth >= 3840)
                settings.Mode = FDSSettings.DisplayMode.ScreenSaver3840x2160;

            Debug.WriteLine("DisplayMode = " + settings.Mode);
            Debug.WriteLine("ClientID = " + settings.ClientID);
            Debug.WriteLine("ZipCode = " + settings.ZipCode);
            Debug.WriteLine("ScreenWidth = " + settings.ScreenWidth);
            Debug.WriteLine("ScreenHeight = " + settings.ScreenHeight);

            Frame frame = new Frame();

            if (settings.Mode == FDSSettings.DisplayMode.Clock800x480)
            {
                frame.Navigate(typeof(View_800x480Clock), null);
                Window.Current.Content = frame;
                Window.Current.Activate();
            }

            if(settings.Mode == FDSSettings.DisplayMode.Clock1920x1080)
            {
                frame.Navigate(typeof(View_1920x1080Clock), null);
                Window.Current.Content = frame;
                Window.Current.Activate();
            }

            if (settings.Mode == FDSSettings.DisplayMode.ScreenSaver3840x2160)
            {
                frame.Navigate(typeof(View_3820x2160ScreenSaver), null);
                Window.Current.Content = frame;
                Window.Current.Activate();
            }

            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = Windows.Media.Core.MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Audio/startup.wav"));
            mediaPlayer.Play();
        }
    }
}
