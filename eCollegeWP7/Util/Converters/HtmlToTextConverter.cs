﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using HtmlAgilityPack;

namespace eCollegeWP7.Util.Converters
{
    public class HtmlToTextConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(value.ToString());

            var output = "";

            foreach (var node in doc.DocumentNode.ChildNodes)
            {
                output += node.InnerText;
            }

            return output.Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}