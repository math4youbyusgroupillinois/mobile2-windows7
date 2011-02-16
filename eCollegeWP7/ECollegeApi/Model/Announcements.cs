﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Net;

namespace ECollegeAPI.Model
{
    public class AnnouncementList
    {
        public AnnouncementList()
        {
            Announcements = new List<Announcement>();
        }

        public List<Announcement> Announcements { get; set; }
    }

    public class Announcement
    {
        public string ID { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string Submitter { get; set; }
        public DateTime StartDisplayDate { get; set; }
        public DateTime EndDisplayDate { get; set; }
    }
}
