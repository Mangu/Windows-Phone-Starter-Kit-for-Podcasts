﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 
 
using Microsoft.Phone.Scheduler;
using System.Net;
using System.Xml.Linq;
using System;
using System.IO.IsolatedStorage;
using PodcastStarterKit.Helpers;
using System.Linq;
using Microsoft.Phone.Shell;
using PodcastStarterKit;

namespace PodcastStarterKitTileTaskAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        
        public static DateTime LastShow { get; set; }

        protected override void OnInvoke(ScheduledTask task)
        {            
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(PodcastListDownloadStringCompleted);
            wc.DownloadStringAsync(new Uri(Settings.PodcastRssUri)); 
        }

        void PodcastListDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            int j = 0;
           
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LastShowPostedOn"))
                LastShow = (DateTime)IsolatedStorageSettings.ApplicationSettings["LastShowPostedOn"];  

            try
            {
                var doc = XDocument.Parse(e.Result);
                var xChannel = doc.Descendants("channel").First();

                foreach (var item in xChannel.Descendants("item"))
                {                
                    if (RssHelper.ParseRssDate(RssHelper.getElementValue(item, "pubDate")) > LastShow)
                    {
                        j++;
                    }
                }
               
            }
            catch (Exception)
            {
                // do nothing just let the MinValue be returned
            }
            
            var tile = ShellTile.ActiveTiles.First();

            StandardTileData update = new StandardTileData();
            update.Count = j;
            update.BackBackgroundImage = new Uri("/Images/Background.png", UriKind.Relative);
            tile.Update(update);

            NotifyComplete();
        }
    }
}
