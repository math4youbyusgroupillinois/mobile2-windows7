﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using ECollegeAPI.Model;
using eCollegeWP7.Util;
using System.Linq;
using ECollegeAPI.Services.Activities;

namespace eCollegeWP7.ViewModels
{
    public class ActivitiesViewModel : ViewModelBase
    {

        private bool _CanLoadMore = false;
        public bool CanLoadMore
        {
            get { return _CanLoadMore; }
            set { _CanLoadMore = value; this.OnPropertyChanged(() => this.CanLoadMore); }
        }

        public bool LoadStarted { get; set; }

        private long? _CourseID;
        public long? CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value;
                this.OnPropertyChanged(() => this.CourseID); this.OnPropertyChanged(() => this.Course); }
        }

        private Course _Course;
        public Course Course
        {
            get { return CourseID.HasValue ? AppViewModel.Courses.CourseIdMap[CourseID.Value] : null; }
        }

        protected BackgroundWorker _loadingWorker;
        protected string _typeFilter;

        private ObservableCollection<ActivityViewModel> _Activities;
        public ObservableCollection<ActivityViewModel> Activities
        {
            get { return _Activities; }
            set { _Activities = value; this.OnPropertyChanged(() => this.Activities); }
        }

        public ActivitiesViewModel() : this(-1, null) {}

        public ActivitiesViewModel(long courseId) : this(courseId,null) {}

        public ActivitiesViewModel(long courseId, string typeFilter)
        {
            if (courseId > 0)
            {
                this.CourseID = courseId;
            }
            this._typeFilter = typeFilter;
        }

        public void Load(bool all)
        {
            Load(all,null);
        }

        public void Load(bool all, Action<bool> callback)
        {
            LoadStarted = true;
            DateTime? since = null;

            if (all)
            {
                CanLoadMore = false;
            }
            else
            {
                since = DateTime.Today.AddDays(-14.0);
            }

            App.BuildService(new FetchMyWhatsHappeningFeedService(since,CourseID,_typeFilter)).Execute((service) =>
            {
                _loadingWorker = new BackgroundWorker();
                _loadingWorker.DoWork += (s, e) =>
                {
                    ObservableCollection<ActivityViewModel> data = new ObservableCollection<ActivityViewModel>();
                    foreach (var item in service.Result)
                    {
                        data.Add(new ActivityViewModel(item));
                    }
                    e.Result = data;
                };
                _loadingWorker.RunWorkerCompleted += (s, e) =>
                {
                    this.Activities = e.Result as ObservableCollection<ActivityViewModel>;
                    this.CanLoadMore = all ? false : true;
                    _loadingWorker = null;
                    if (callback != null) callback(true);
                };
                _loadingWorker.RunWorkerAsync();
            });
        }
    }
}