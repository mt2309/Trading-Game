﻿#pragma checksum "C:\Users\David\documents\visual studio 2010\Projects\SilverlightApplication1\SilverlightApplication1\CharacterPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CE70CE23F77E4148D9380AE28605FE2D"
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


namespace SilverlightApplication1 {
    
    
    public partial class CharacterPage : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Page CharacterForm;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button createCharButton;
        
        internal System.Windows.Controls.TextBlock CharacterBox;
        
        internal System.Windows.Controls.Image charImage;
        
        internal System.Windows.Controls.Button playCharButton;
        
        internal System.Windows.Controls.Button deleteCharButton;
        
        internal System.Windows.Controls.ScrollViewer abilityViewer;
        
        internal System.Windows.Controls.Grid abilityGrid;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SilverlightApplication1;component/CharacterPage.xaml", System.UriKind.Relative));
            this.CharacterForm = ((System.Windows.Controls.Page)(this.FindName("CharacterForm")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.createCharButton = ((System.Windows.Controls.Button)(this.FindName("createCharButton")));
            this.CharacterBox = ((System.Windows.Controls.TextBlock)(this.FindName("CharacterBox")));
            this.charImage = ((System.Windows.Controls.Image)(this.FindName("charImage")));
            this.playCharButton = ((System.Windows.Controls.Button)(this.FindName("playCharButton")));
            this.deleteCharButton = ((System.Windows.Controls.Button)(this.FindName("deleteCharButton")));
            this.abilityViewer = ((System.Windows.Controls.ScrollViewer)(this.FindName("abilityViewer")));
            this.abilityGrid = ((System.Windows.Controls.Grid)(this.FindName("abilityGrid")));
        }
    }
}

