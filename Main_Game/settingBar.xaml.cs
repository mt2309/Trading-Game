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
    public partial class settingBar : UserControl, IScreen
    {
        public settingBar()
        {
            InitializeComponent();
        }

        public UIElement Element { get { return this; } }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            ScreenManager.SetScreen(new LoginScreen());           
            ScreenManager.SetSideBar(null);           
            ScreenManager.SetSettingBar(null);
            // Magic logout shizzle.
        }
    }
}
