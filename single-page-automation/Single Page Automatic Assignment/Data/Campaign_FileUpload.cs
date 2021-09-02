using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Single_Page_Automatic_Assignment.Data
{
    public class Campaign_FileUpload
    {
        public int Id { get; set; }
        public string CampaignID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public string UserUpload { get; set; }
    }
}
