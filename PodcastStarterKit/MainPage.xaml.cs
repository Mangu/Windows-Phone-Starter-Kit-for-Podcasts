﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

#define DEBUG

using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using PodcastStarterKit.ViewModels;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PodcastStarterKit
{
    public partial class MainPage : PhoneApplicationPage
    {
        PeriodicTask tileTask = null;        

        public MainPage()
        {
            InitializeComponent();
            StartTracking();
        }

        private void StartTracking()
        {
            string tileTaskId = "PodcastStarterKitPlaybackAudioAgent";

            tileTask = ScheduledActionService.Find(tileTaskId) as PeriodicTask;
            
            if (tileTask != null && !tileTask.IsEnabled)
            {
                MessageBox.Show("Background agent for this application has been disabled by the user.");
                return;
            }

            if (tileTask != null && tileTask.IsEnabled)
            {
                RemoveAgent(tileTaskId);
            }

            tileTask = new PeriodicTask(tileTaskId);

            tileTask.Description = "New Poscast Finder";
            ScheduledActionService.Add(tileTask);

         
            // If debugging is enabled, use LaunchForTest to launch the agent in one minute.
            #if(DEBUG)
            ScheduledActionService.LaunchForTest(tileTaskId, TimeSpan.FromSeconds(60));
            #endif
        }

        private void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
            }
            catch (Exception)
            {
            }
        }

        private void Recent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PodcastItemModel podcast = (PodcastItemModel)(sender as ListBox).SelectedItem;
            if (podcast != null)
            {
                App.CurrentItem = podcast;

                InsertIntoCollection(podcast, App.History);
                NavigationService.Navigate(new Uri("/Play.xaml", UriKind.Relative));
            }
        }

        private void InsertIntoCollection(PodcastItemModel Item, ObservableCollection<PodcastItemModel> Collection)
        {           
            foreach (PodcastItemModel i in Collection)
            {
                if (i.EnclosureUrl == Item.EnclosureUrl)
                {
                    Collection.Remove(i);                    
                    break;
                }    
            }
            Collection.Insert(0, Item);            
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Reset the Selected Index
            RecentList.SelectedIndex = -1;
            HistoryListBox.SelectedIndex = -1;
        }

        private void rate_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void info_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            EmailComposeTask emailTask = new EmailComposeTask();
            emailTask.To = Settings.EmailAddress;
            emailTask.Subject = Settings.Subject;
            emailTask.Show();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}