﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 
 
using System;
using System.Collections.ObjectModel;
using PodcastStarterKit.ViewModels;

namespace PodcastStarterKit.Services
{
    public class PodcastServiceMock : IPodcastService
    {
        public ObservableCollection<TwitterStatusModel> GetTwitterFeed()
        {
            return null;
        }
        
        public event LoadEventHandler InformationLoaded;
        public event LoadEventHandler TwitterLoaded;       

        public ObservableCollection<PodcastItemModel> GetPodcasts(string SearchBy)
        {
            var mockPodcast = new ObservableCollection<PodcastItemModel>();

            mockPodcast.Add(new PodcastItemModel()
            {
                Link = "http://www.dotnetrocks.com/default.aspx?ShowNum=709", 
                Date = DateTime.Now,
                Description = "<p><img src='http://thisdeveloperslife.com/images/typeface.png' /></p><p>Who cares about typefaces and why should you? Well, these guys do and you should start caring. Rob and Scott explore the world of reading online with one of the godfathers in the world of typeface and fonts.</p><p><a href='http://traffic.libsyn.com/devlife/205a-Typo.mp3'>Download Here</a></p>",
                Title = "1.01 Type",
                SourceUrl = "http://www.dotnetrocks.com/default.aspx?ShowNum=709",
                EnclosureUrl = "http://s3.amazonaws.com/dnr/dotnetrocks_0709_alexander_gross.mp3"
            });


            return mockPodcast;
        }

        public PodcastInformationModel GetPodcastInformation()
        {
            return new PodcastInformationModel
            {
                Title = "Test"
            };

        }
    }
}
