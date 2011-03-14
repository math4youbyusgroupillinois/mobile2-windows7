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
using System.Diagnostics;
using System.Collections.ObjectModel;
using ECollegeAPI.Model;
using eCollegeWP7.Util;
using eCollegeWP7.Views.Dialogs;
using eCollegeWP7.Exceptions;
using eCollegeWP7.ViewModels;

namespace eCollegeWP7.Views
{
    public partial class MainPage : BasePage
    {
        public MainViewModel Model { get { return this.DataContext as MainViewModel; } }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = new MainViewModel();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
             */
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            string defaultPanoramaItem;
            {
                defaultPanoramaItem = "PanHome";
            }

            var defaultItem = PanMain.FindName(defaultPanoramaItem);
            PanMain.DefaultItem = defaultItem;
        }

        private void BtnShowDialog_Click(object sender, RoutedEventArgs e)
        {
            ErrorDialog ed = new ErrorDialog();
            ed.Show();
        }

        private void BasePage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            throw new AppExitException();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as HyperlinkButton;
            var link = btn.DataContext as HomeLink;

            if (link.LinkPath != null)
            {
                this.NavigationService.Navigate(new Uri(link.LinkPath, UriKind.Relative));
            }
            else
            {
                var defaultItem = PanMain.FindName(link.PanoramaItemName);
                PanMain.DefaultItem = defaultItem;
            }
        }
    }
}