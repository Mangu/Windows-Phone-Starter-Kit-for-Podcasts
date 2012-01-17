﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

 using System;
using System.Globalization;
using System.Xml.Linq;

namespace PodcastStarterKit.Helpers
{
    public static class RssHelper
    {
        /// <summary>
        /// RSS has a specific DateTime format - this method pulls out the DateTime from that provided
        /// </summary>
        /// <param name="Date">Incoming RSS DateTime</param>
        /// <returns>Compatible .NET DateTime</returns>
        public static DateTime ParseRssDate(string Date)
        {
            // clean up in the input date
            var newDate = Date;
            newDate = newDate.Replace("GMT", "+0:00");
            newDate = newDate.Replace("PST", "-8:00");
            newDate = newDate.Replace("PDT", "-7:00");
            newDate = newDate.Replace("MST", "-7:00");
            newDate = newDate.Replace("CST", "-6:00");
            newDate = newDate.Replace("CDT", "-5:00");
            newDate = newDate.Replace("EST", "-5:00");
            newDate = newDate.Replace("EDT", "-4:00");
            newDate = newDate.Replace("AST", "-4:00");
            newDate = newDate.Replace("ADT", "-3:00");
            // add your own here if I missed one you need

            // run the conversion
            var provider = CultureInfo.InvariantCulture;
            DateTime result;
            const string format = "ddd, dd MMM yyyy HH:mm:ss zzz";

            try
            {
                result = DateTime.ParseExact(newDate, format, provider);
            }
            catch (Exception)
            {
                //return the min value for that item. no need to have the entire feed fail
                result = DateTime.MinValue;
            }           

            // all done!
            return result;
        }

        /// <summary>
        /// Look for an element and find it's value.
        /// </summary>
        /// <param name="element">the element in which to search</param>
        /// <param name="elementName">the child element name</param>
        /// <param name="attributeName">if supplied, find the attribute value instead of the element value</param>
        /// <returns>value from the element or attribute</returns>
        public static string getElementValue(XElement element, XName elementName, string attributeName = "")
        {
            var e = element.Element(elementName);
            if (e == null)
                return "";

            if (attributeName.Length > 0)
            {
                var a = e.Attribute(attributeName);
                return a == null ? "" : a.Value;
            }
            else
            {
                return e == null ? "" : e.Value;
            }
        }
    }
}