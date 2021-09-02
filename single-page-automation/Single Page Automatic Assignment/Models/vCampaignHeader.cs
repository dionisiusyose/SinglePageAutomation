using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Single_Page_Automatic_Assignment.Models
{
    public class vCampaignHeader
    {
        [Column(name: "Campaign ID")]
        public string CampaignID { get; set; }
        [Column(name: "Campaign Name")]
        public string CampaignName { get; set; }
        [Column(name: "Business Unit")]
        public string BusinessUnit { get; set; }
        [Column(name: "Brand")]
        public string Brand { get; set; }
        [Column(name: "Status")]
        public string Status { get; set; }
        [Column(name: "Start Date")]
        public string StartDate { get; set; }
        [Column(name: "End Date")]
        public string EndDate { get; set; }
        [Column(name: "Channel")]
        public string Channel { get; set; }
        [Column(name: "Target Audience")]
        public string TargetAudience { get; set; }
        [Column(name: "User Created")]
        public string UserCreated { get; set; }
    }
}
