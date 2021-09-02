using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace single_page_automation_api.Data
{
    public class Campaign_FileUpload
    {
        [Key]
        public int Id { get; set; }
        public string CampaignID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public string UserUpload { get; set; }
    }
}
