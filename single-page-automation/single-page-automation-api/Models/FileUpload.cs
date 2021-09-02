using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace single_page_automation_api.Models
{
    public class FileUpload
    {
        //public int Id { get; set; }
        public string CampaignID { get; set; }
        public IFormFile files { get; set; }
    }
}
