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
using ECollegeAPI;
using ECollegeAPI.Model;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using ECollegeAPI.Services.Users;
using RestSharp;

namespace eCollegeWP7.ViewModels
{
    public class AppViewModel : ViewModelBase
    {

        private int _PendingServiceCalls = 0;
        public int PendingServiceCalls
        {
            get { return _PendingServiceCalls; }
            set { _PendingServiceCalls = value; this.OnPropertyChanged(() => this.PendingServiceCalls); }
        }


        private User _CurrentUser;
        public User CurrentUser
        {
            get { return _CurrentUser; }
            set { _CurrentUser = value; this.OnPropertyChanged(() => this.CurrentUser); }
        }
        
        public CoursesViewModel Courses { get; set; }

        public ECollegeClient Client { get; set; }

        public AppViewModel()
        {
            Client = new ECollegeClient(AppResources.ClientString, AppResources.ClientID);
        }

        public void Activate(IDictionary<string,object> state)
        {
            object storedCurrentUser;
            if (state.TryGetValue("CurrentUser", out storedCurrentUser))
            {
                CurrentUser = state["CurrentUser"] as User;
            }
            object storedCourses;
            if (state.TryGetValue("Courses", out storedCourses))
            {
                Courses = state["Courses"] as CoursesViewModel;
            } 
            object grantToken;
            if (state.TryGetValue("grantToken", out grantToken) && grantToken != null)
            {
                Client.SetupAuthentication(grantToken.ToString());
            }
        }

        public void Deactivate(IDictionary<string, object> state)
        {
            state["grantToken"] = Client.GrantToken;
            state["CurrentUser"] = CurrentUser;
            state["Courses"] = Courses;
        }

        protected void FetchInitialUserData(Action successCallback,Action<FetchMeService,RestResponse> failureCallback)
        {
            var call = App.BuildService(new FetchMeService());
            call.AddFailureHandler(failureCallback);
            call.Execute(service =>
            {
                CurrentUser = service.Result;
                Courses = new CoursesViewModel();
                Courses.Load(successCallback);
            });
        }

        public void Login(String grantToken, Action successCallback, Action<FetchMeService, RestResponse> failureCallback)
        {
            Client.SetupAuthentication(grantToken);
            FetchInitialUserData(successCallback, failureCallback);
        }

        public void Login(String username, String password, Action successCallback, Action<FetchMeService, RestResponse> failureCallback)
        {
            Client.SetupAuthentication(username, password);
            FetchInitialUserData(successCallback, failureCallback);
        }
        
    }
}
