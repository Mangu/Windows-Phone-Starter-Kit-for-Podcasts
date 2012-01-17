﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

using System;

namespace PodcastStarterKit.ViewModels
{
    public class PodcastItemModel : ModelBase
    {       
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                    return;
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private string _link;
        public string Link
        {
            get { return _link; }
            set
            {
                if (_link == value)
                    return;
                _link = value;
                NotifyPropertyChanged("Link");
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    NotifyPropertyChanged("Date");
                }
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }
 
        private string _sourceUrl;
        public string SourceUrl
        {
            get
            {
               return _sourceUrl;
            }
            set
            {
                if (value != _sourceUrl)
                {
                    _sourceUrl = value;
                    NotifyPropertyChanged("SourceUrl");
                }
            }
        }

        private string _enclosureUrl;
        public string EnclosureUrl
        {
            get
            {
                return _enclosureUrl;
            }
            set
            {
                if (value != _enclosureUrl)
                {
                    _enclosureUrl = value;
                    NotifyPropertyChanged("EnclosureUrl");
                }
            }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    NotifyPropertyChanged("ImageUrl");
                }
            }
        }       
   }
}