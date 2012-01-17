﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

 using System.Collections.Generic;
using PodcastStarterKit.ViewModels;

namespace PodcastStarterKit.Helpers
{
    public class FeedChannel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public string ManagingEditor { get; set; }
        public string WebMaster { get; set; }
        public string Rating { get; set; }
        public IEnumerable<PodcastItemModel> Items { get; set; }
    }
}
