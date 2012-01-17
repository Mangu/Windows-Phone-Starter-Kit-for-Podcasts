﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

using System.Collections.ObjectModel;

namespace PodcastStarterKit.ViewModels
{
    public class PodcastInformationModel : ModelBase
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

        private string _language;
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                if (value != _language)
                {
                    _language = value;
                    NotifyPropertyChanged("Language");
                }
            }
        }

        private string _copyright;
        public string Copyright
        {
            get
            {
                return _copyright;
            }
            set
            {
                if (value != _copyright)
                {
                    _copyright = value;
                    NotifyPropertyChanged("Copyright");
                }
            }
        }

        private string _managingEditor;
        public string ManagingDirector
        {
            get
            {
                return _managingEditor;
            }
            set
            {
                if (value != _managingEditor)
                {
                    _managingEditor = value;
                    NotifyPropertyChanged("ManagingDirector");
                }
            }
        }

        private string _webMaster;
        public string WebMaster
        {
            get
            {
                return _webMaster;
            }
            set
            {
                if (value != _webMaster)
                {
                    _webMaster = value;
                    NotifyPropertyChanged("WebMaster");
                }
            }
        }

        private string _rating;
        public string Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                if (value != _rating)
                {
                    _rating = value;
                    NotifyPropertyChanged("Rating");
                }
            }
        }

        private string _ImageUrl;
        public string ImageUrl
        {
            get { return _ImageUrl; }
            set
            {
                if (_ImageUrl == value)
                    return;
                _ImageUrl = value;
                NotifyPropertyChanged("ImageUrl");
            }
        }

        private ObservableCollection<PodcastItemModel> _items = new ObservableCollection<PodcastItemModel>();
        public ObservableCollection<PodcastItemModel> Items
        {
            get { return _items; }
            set
            {
                if (value != _items)
                {
                    _items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }
    }
}
