﻿#pragma checksum "C:\Users\Chris\Documents\Visual Studio 2010\Projects\Game_Project\Main_Game\Main_Game\City.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "629ACEB11A34D59D2D43EA35706A962D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Main_Game {
    
    
    public partial class City : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Canvas cnvCity;
        
        internal System.Windows.Controls.Image CityBackground;
        
        internal System.Windows.Controls.Image mainChar;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Main_Game;component/City.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.cnvCity = ((System.Windows.Controls.Canvas)(this.FindName("cnvCity")));
            this.CityBackground = ((System.Windows.Controls.Image)(this.FindName("CityBackground")));
            this.mainChar = ((System.Windows.Controls.Image)(this.FindName("mainChar")));
        }
    }
}
