﻿using System;
 

namespace FSE_SBA.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public int? Priority { get; set; }
        public User User { get; set; }
        public int NoOfTasks { get; set; }
        public int NoOfCompletedTasks { get; set; }
    }
}