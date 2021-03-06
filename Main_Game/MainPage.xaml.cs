﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Main_Game
{
    public partial class MainPage : UserControl, IScreenHost
    {

        public MainPage()
        {
            InitializeComponent();
        }

        public void SetScreen(IScreen screen)
        {
            mainContent.Children.Clear();
            mainContent.Children.Add(screen.Element);
        }

        public void SetSettingBar(IScreen screen)
        {
            settingBar.Children.Clear();
            if (screen != null)
                settingBar.Children.Add(screen.Element);
        }

        public void SetSideBar(IScreen screen)
        {
            sideBar.Children.Clear();
            if (screen != null)
                sideBar.Children.Add(screen.Element);
        }

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            ScreenManager.SetHost(this);
            ScreenManager.SetScreen(new LoginScreen());
        }

    }
    public interface IScreen
    {
        UIElement Element { get; }
    }

    public interface IScreenHost
    {
        void SetScreen(IScreen screen);
        void SetSettingBar(IScreen screen);
        void SetSideBar(IScreen screen);
    }

    public static class ScreenManager
    {
        private static IScreenHost _host;

        public static void SetHost(IScreenHost host)
        {
            _host = host;
        }

        public static void SetScreen(IScreen screen)
        {
            _host.SetScreen(screen);
        }

        public static void SetSettingBar(IScreen screen)
        {
            _host.SetSettingBar(screen);
        }

        public static void SetSideBar(IScreen screen)
        {
            _host.SetSideBar(screen);
        }
    }


}
