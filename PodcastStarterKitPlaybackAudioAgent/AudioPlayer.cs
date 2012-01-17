﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

using System;
using Microsoft.Phone.BackgroundAudio;

namespace PodcastStarterKitPlaybackAudioAgent
{
    public class AudioPlayer : AudioPlayerAgent
    {        
        protected override void OnPlayStateChanged(BackgroundAudioPlayer player, AudioTrack track, PlayState playState)
        {
            switch (playState)
            {
                case PlayState.TrackEnded:
                    player.Close();
                    break;

                case PlayState.TrackReady:
                    player.Volume = 1.0;
                    player.Play();
                    break;

                case PlayState.Shutdown:
                    // TODO: Handle the shutdown state here (e.g. save state)
                    break;

                case PlayState.Unknown:
                    break;

                case PlayState.Stopped:
                    break;

                case PlayState.Paused:
                    break;

                case PlayState.Playing:
                    break;

                case PlayState.BufferingStarted:
                    break;

                case PlayState.BufferingStopped:
                    break;

                case PlayState.Rewinding:
                    break;

                case PlayState.FastForwarding:
                    break;
            }

            NotifyComplete();
        }
    
        protected override void OnUserAction(BackgroundAudioPlayer player, AudioTrack track, UserAction action, object param)
        {
            switch (action)
            {
                case UserAction.Play:
                    if (PlayState.Playing != player.PlayerState)
                    {
                        player.Play();
                    }
                    break;

                case UserAction.Stop:
                    player.Stop();
                    break;

                case UserAction.Pause:
                    if (PlayState.Playing == player.PlayerState)
                    {
                        player.Pause();
                    }
                    break;
                case UserAction.Rewind:
                  player.Position = player.Position.Subtract(new TimeSpan(0,0,10));
                    break;

                case UserAction.FastForward:
                  player.Position = player.Position.Add(new TimeSpan(0,0,10));
                    break;
            }

            NotifyComplete();
        }


        
        protected override void OnError(BackgroundAudioPlayer player, AudioTrack track, Exception error, bool isFatal)
        {
            //TODO: Add code to handle error conditions

            NotifyComplete();
        }

        /// <summary>
        /// Called when the agent request is getting cancelled
        /// </summary>
        protected override void OnCancel()
        {
        }
    }
}
