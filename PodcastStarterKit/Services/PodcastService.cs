﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

 using System;
using System.Net;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Globalization;
using PodcastStarterKit.ViewModels;
using PodcastStarterKit.Helpers;

namespace PodcastStarterKit.Services
{
    public class PodcastService : IPodcastService
    {        
        XNamespace ITUNES = "http://www.itunes.com/dtds/podcast-1.0.dtd";
              
        private ObservableCollection<TwitterStatusModel> TwitterFeed;
        private PodcastInformationModel PodcastInformation;
     
        public event LoadEventHandler InformationLoaded;
        public event LoadEventHandler TwitterLoaded;

        public ObservableCollection<TwitterStatusModel> GetTwitterFeed()
        {
            TwitterFeed = new ObservableCollection<TwitterStatusModel>();
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(TwitterStatusInformationCompleted);
            wc.DownloadStringAsync(new Uri(Settings.TwitterFeedUri));
            return TwitterFeed;   
        }

        void TwitterStatusInformationCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            LoadEventArgs loadedEventArgs = new LoadEventArgs();

            try
            {
                var doc = XDocument.Parse(e.Result);
                foreach (var item in doc.Descendants("status"))
                {
                    var model = new TwitterStatusModel()
                    {
                        Date = item.Element("created_at").Value.Substring(0, item.Element("created_at").Value.IndexOf('+')),
                        Text = item.Element("text").Value,
                    };
                    TwitterFeed.Add(model);
                }

                loadedEventArgs.IsLoaded = true;
                loadedEventArgs.Message = "";
            }
            catch (Exception)
            {
                loadedEventArgs.IsLoaded = false;
                loadedEventArgs.Message = "unable to load twitter data";
            }
            finally
            {
                OnTwitterLoaded(loadedEventArgs);
            }

            TwitterFeed = null;
        }
         
        public PodcastInformationModel GetPodcastInformation()
        {      
            PodcastInformation = new PodcastInformationModel();           

            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadPodcastInformationCompleted);
            wc.DownloadStringAsync(new Uri(Settings.PodcastRssUri));
            return PodcastInformation;   
        }

        void DownloadPodcastInformationCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            LoadEventArgs loadedEventArgs = new LoadEventArgs();

            try
            {
                var doc = XDocument.Parse(e.Result);
                var xChannel = doc.Descendants("channel").First();

                PodcastInformation.Title = getElementValue(xChannel, "title");
                PodcastInformation.Description = getElementValue(xChannel, "description");
                PodcastInformation.Copyright = getElementValue(xChannel, "copyright");
                PodcastInformation.Rating = getElementValue(xChannel, "rating");
                PodcastInformation.WebMaster = getElementValue(xChannel, "webMaster");
                PodcastInformation.Link = getElementValue(xChannel, "link");              
                PodcastInformation.Language = getElementValue(xChannel, "language");              
                PodcastInformation.ManagingDirector = getElementValue(xChannel, "managingEditor");              
                PodcastInformation.ImageUrl = Coalesce(Settings.DefaulImage, getElementValue(xChannel, ITUNES + "image", "href")).ToString();

                foreach (var item in xChannel.Descendants("item"))
                {
                    PodcastItemModel pim = new PodcastItemModel()
                    {
                        Title = getElementValue(item, "title").Trim(),
                        Link = getElementValue(item, "link"),
                        Date = RssHelper.ParseRssDate(getElementValue(item, "pubDate")),
                        Description = StripHtmlTags(getElementValue(item, "description")).Trim(),
                        EnclosureUrl = getElementValue(item, "enclosure", "url"),
                        SourceUrl = getElementValue(item, "link"),
                        ImageUrl = Coalesce(
                            ParseImageFromDescription(getElementValue(item, "description")),
                            getElementValue(item, ITUNES + "image", "href"),
                            PodcastInformation.ImageUrl,
                            Settings.DefaulImage                            
                        ).ToString(),
                    };

                    // ensure that there is an enclosure. No enclosure == no podcast media :(
                    if (pim.EnclosureUrl.Length == 0)
                    {
                        pim.EnclosureUrl = "no media available";
                    }

                    // finally, add the item to the collection
                    PodcastInformation.Items.Add(pim);
                }

                loadedEventArgs.IsLoaded = true;
                loadedEventArgs.Message = "";
            }
            catch
            {
                loadedEventArgs.IsLoaded = false;
                loadedEventArgs.Message = "unable to load data";
            }
            finally
            {
                OnPodcastInfoLoaded(loadedEventArgs);
            }
        }

        protected virtual void OnTwitterLoaded(LoadEventArgs e)
        {
            if (TwitterLoaded != null)
            { 
                TwitterLoaded(this, e);
            }
        }

        protected virtual void OnPodcastInfoLoaded(LoadEventArgs e)
        {
            if (InformationLoaded != null)
            {               
                InformationLoaded(this, e);
            }
        }


        /// <summary>
        /// Pull out the first img tag we find in the html description.
        /// If not, then use a default image.
        /// </summary>
        /// <param name="description">the description from the podcast rss feed</param>
        /// <returns>description without the HTML markup</returns>
        private string ParseImageFromDescription(string description)
        {
            // if there is no image, then kick out the default
            var imgStart = description.IndexOf("<img ");
            if (imgStart <= 0)
                return Settings.DefaulImage;

            // find where the src parameter starts
            var srcStart = description.IndexOf("src=", imgStart);

            // this is either a " or a '
            var wrapper = description.Substring(srcStart + 4, 1);

            // find where the other " or ' is located
            var srcEnd = description.IndexOf(wrapper, srcStart + 5);

            // pull out the url from between the "'s
            var src = description.Substring(srcStart + 5, srcEnd - srcStart - 5);
            return src;
        }

        /// <summary>
        /// Take the first non-null, non-empty value from the list
        /// </summary>
        /// <param name="default">supply a default value if no matches are made</param>
        /// <param name="parms">ParamArray for the items to coalesce</param>
        /// <returns>the first non-null, non-empty value, or the default</returns>
        private object Coalesce(params object[] parms)
        {
            foreach (var p in parms)
            {
                if (p != null && p.ToString().Length > 0)
                    return p;
            }
            return null;
        }
        /// <summary>
        /// Remove HTML tags from a given string
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private string StripHtmlTags(string value)
        {
            const string HTML_TAG_PATTERN = "<.*?>";
            var result = Regex.Replace(value, HTML_TAG_PATTERN, string.Empty);
            return result;
        }

        /// <summary>
        /// Look for an element and find it's value.
        /// </summary>
        /// <param name="element">the element in which to search</param>
        /// <param name="elementName">the child element name</param>
        /// <param name="attributeName">if supplied, find the attribute value instead of the element value</param>
        /// <returns>value from the element or attribute</returns>
        private static string getElementValue(XElement element, XName elementName, string attributeName = "")
        {
            return RssHelper.getElementValue(element, elementName, attributeName);
        }
    }
}
