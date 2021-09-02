using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace single_page_automation_api.Data
{
    public class Mst_BU
    {
        [Key]
        public int? BUID { get; set; }
        public string? BU { get; set; }
        public string? Status { get; set; }
        public string? User_Updated { get; set; }
        public DateTime? Date_Updated { get; set; }
    }
}
