﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

using System;
using Microsoft.Phone.Shell;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;

namespace PodcastStarterKit
{
    public partial class Play : PhoneApplicationPage
    {
        DispatcherTimer _timer;

        public Play()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize a timer to update the UI every half-second.
           
        }

        void Instance_PlayStateChanged(object sender, EventArgs e)
        {
            UpdateState(null, null);
        }

        bool _isCurrentItemPlaying;
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(0.5);
            _timer.Tick += new EventHandler(UpdateState);

            BackgroundAudioPlayer.Instance.PlayStateChanged += new EventHandler(Instance_PlayStateChanged);

            if (BackgroundAudioPlayer.Instance.PlayerState == PlayState.Playing)
            {
                // If audio was already playing when the app was launched, update the UI.
                positionIndicator.IsIndeterminate = false;
                positionIndicator.Maximum = BackgroundAudioPlayer.Instance.Track.Duration.TotalSeconds;
                UpdateState(null, null);

                if (BackgroundAudioPlayer.Instance.Track.Artist == Settings.ApplicationName && BackgroundAudioPlayer.Instance.Track.Title == App.CurrentItem.Title)
                {
                    _isCurrentItemPlaying = true;
                    UpdateState(null, null);                   
                }
                else                
                 _isCurrentItemPlaying = false;
            }          
        }

        private void UpdateState(object sender, EventArgs e)
        {
            if (_isCurrentItemPlaying)
            {
                switch (BackgroundAudioPlayer.Instance.PlayerState)
                {
                    case PlayState.Playing:
                        // Update the UI.
                        ((ApplicationBarIconButton)(ApplicationBar.Buttons[1])).Text = "pause";
                        ((ApplicationBarIconButton)(ApplicationBar.Buttons[1])).IconUri = new Uri("/Images/appbar.transport.pause.rest.png", UriKind.Relative);
                        positionIndicator.IsIndeterminate = false;
                        positionIndicator.Maximum = BackgroundAudioPlayer.Instance.Track.Duration.TotalSeconds;

                        // Start the timer for updating the UI.
                        _timer.Start();
                        break;

                    case PlayState.Paused:
                        // Update the UI. 
                        ((ApplicationBarIconButton)(ApplicationBar.Buttons[1])).Text = "play";
                        ((ApplicationBarIconButton)(ApplicationBar.Buttons[1])).IconUri = new Uri("/Images/appbar.transport.play.rest.png", UriKind.Relative);

                        // Stop the timer for updating the UI.
                        _timer.Stop();
                        break;
                }

                if (BackgroundAudioPlayer.Instance.Track != null)
                {
                    // Set the current position on the ProgressBar.

                    positionIndicator.Value = BackgroundAudioPlayer.Instance.Position.TotalSeconds;

                    // Update the current playback position.
                    TimeSpan position = new TimeSpan();
                    position = BackgroundAudioPlayer.Instance.Position;
                    textPosition.Text = String.Format("{0:d2}:{1:d2}:{2:d2}", position.Hours, position.Minutes, position.Seconds);

                    // Update the time remaining digits.
                    TimeSpan timeRemaining = new TimeSpan();
                    timeRemaining = BackgroundAudioPlayer.Instance.Track.Duration - position;
                    textRemaining.Text = String.Format("-{0:d2}:{1:d2}:{2:d2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds);
                }
            }
        }

        private void Back_Click(object sender, System.EventArgs e)
        {
            BackgroundAudioPlayer.Instance.Rewind();
            UpdateState(null, null);
        }

        private void Play_Click(object sender, System.EventArgs e)
        {
            if (BackgroundAudioPlayer.Instance.PlayerState == PlayState.Playing && _isCurrentItemPlaying)
            {
                BackgroundAudioPlayer.Instance.Pause();
            }
            else
            {
                if (BackgroundAudioPlayer.Instance.Track == null || !_isCurrentItemPlaying)
                {                
                    AudioTrack track = new AudioTrack(new Uri(App.CurrentItem.EnclosureUrl), App.CurrentItem.Title, Settings.ApplicationName, "Podcast", null, "", EnabledPlayerControls.All);
                    BackgroundAudioPlayer.Instance.Track = track;
                }
                _isCurrentItemPlaying = true;
                BackgroundAudioPlayer.Instance.Play();               
            }
        }

        private void Forward_Click(object sender, System.EventArgs e)
        {
            BackgroundAudioPlayer.Instance.FastForward();
            UpdateState(null, null);
        }
    }
}