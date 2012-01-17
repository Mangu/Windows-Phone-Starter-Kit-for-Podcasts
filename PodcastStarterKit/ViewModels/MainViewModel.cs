﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

 using System.Collections.ObjectModel;
using PodcastStarterKit.Services;

namespace PodcastStarterKit.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<PodcastItemModel> Recent { get; private set; }
        public ObservableCollection<PodcastItemModel> History { get; private set; }
        public ObservableCollection<TwitterStatusModel> Twitter { get; private set; }
        public PodcastInformationModel PodcastInformation { get; private set; }

        public MainViewModel()
        {           
            Service.InformationLoaded += new LoadEventHandler(Service_InformationLoaded);
            Service.TwitterLoaded += new LoadEventHandler(Service_TwitterLoaded);
            LoadData();           
        }

        void Service_TwitterLoaded(object sender, LoadEventArgs e)
        {
            TwitterDataStatus = e.Message;
        }
        void Service_InformationLoaded(object sender, LoadEventArgs e)
        {
            PodcastInfoDataStatus = e.Message;
        }       

        private string _TwitterDataStatus;
        public string TwitterDataStatus
        {
            get { return _TwitterDataStatus; }
            set
            {
                if (_TwitterDataStatus == value)
                    return;
                _TwitterDataStatus = value;
                NotifyPropertyChanged("TwitterDataStatus");
            }
        }

        private string _PodcastInfoDataStatus;
        public string PodcastInfoDataStatus
        {
            get { return _PodcastInfoDataStatus; }
            set
            {
                if (_PodcastInfoDataStatus == value)
                    return;
                _PodcastInfoDataStatus = value;
                NotifyPropertyChanged("PodcastInfoDataStatus");
            }
        }

        private IPodcastService _Service;
        public IPodcastService Service
        {
            get
            {
                if (_Service == null)
                {
                    if (IsInDesignMode())
                        _Service = new PodcastServiceMock();
                    else
                        _Service = new PodcastService();
                }

                return _Service;
            }

            set
            {
                _Service = value;
            }
        }

        public void LoadData()
        {
           PodcastInformation = Service.GetPodcastInformation();
           Recent = PodcastInformation.Items;
           History = App.History;           
           Twitter = Service.GetTwitterFeed();
        }       
    }
}