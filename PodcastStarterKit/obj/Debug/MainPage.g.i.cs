﻿#pragma checksum "C:\Code\github\Windows-Phone-Starter-Kit-for-Podcasts\PodcastStarterKit\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "72ACB0152F39D4CD09895117B7632D49"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
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


namespace PodcastStarterKit {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Panorama MainPano;
        
        internal System.Windows.Controls.ListBox RecentList;
        
        internal System.Windows.Controls.ListBox HistoryListBox;
        
        internal System.Windows.Controls.ListBox TwitterListBox;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/PodcastStarterKit;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.MainPano = ((Microsoft.Phone.Controls.Panorama)(this.FindName("MainPano")));
            this.RecentList = ((System.Windows.Controls.ListBox)(this.FindName("RecentList")));
            this.HistoryListBox = ((System.Windows.Controls.ListBox)(this.FindName("HistoryListBox")));
            this.TwitterListBox = ((System.Windows.Controls.ListBox)(this.FindName("TwitterListBox")));
        }
    }
}

