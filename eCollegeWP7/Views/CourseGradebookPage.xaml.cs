﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using ECollegeAPI.Model;
using eCollegeWP7.Util;
using eCollegeWP7.ViewModels;

namespace eCollegeWP7.Views
{
    public partial class CourseGradebookPage : BasePage
    {

        public GradesViewModel Model { get { return this.DataContext as GradesViewModel; } }

        public CourseGradebookPage()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            int courseId = Int32.Parse(parameters["courseId"]);
            this.DataContext = new GradesViewModel(courseId);
        }

        private void BtnGrade_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var ug = btn.DataContext as UserGradebookItem;

            if (ug.Grade != null)
            {
                this.NavigationService.Navigate(new Uri("/Views/GradePage.xaml?courseId=" + Model.CourseID + "&gradebookItemGuid=" + ug.GradebookItem.ID, UriKind.Relative));
            }
        }


    }
}