using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Single_Page_Automatic_Assignment.AddOns;
using Single_Page_Automatic_Assignment.Data;
using Single_Page_Automatic_Assignment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Single_Page_Automatic_Assignment.Controllers
{
    public class MasterController : Controller
    {
        private AppDBContext _dbContext;

        public MasterController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get Master Data BU
        [HttpGet]
        public ActionResult GetBU()
        {
            try
            {
                var BUs = _dbContext.Mst_BU.ToList();
                if (BUs.Count == 0)
                {
                    return StatusCode(404, "No data was found");
                }
                return Ok(BUs);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error has occured");
            }
        }


        //Get Master Data Brand
        [HttpGet]
        public ActionResult GetBrand()
        {
            try
            {
                var brandList = _dbContext.Mst_Brand.ToList();

                if (brandList.Count == 0)
                {
                    return StatusCode(404, "No data was found");
                }
                return Ok(brandList);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error has occured");
            }
        }

        //Get Campaign Header Data
        [HttpGet]
        public ActionResult GetHeader()
        {
            try
            {
                var headerList = _dbContext.vCampaignHeader.ToList();

                if (headerList.Count == 0)
                {
                    return StatusCode(404, "No data was found");
                }
                return Ok(headerList);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error has occured");
            }
        }

        [HttpPost]
        public ActionResult SetCampaignID(VMCampaignID CampaignID)
        {
            HttpContext.Session.SetString("tes", CampaignID.ID);
            //var test = HttpContext.Session.GetString("tes");
            return Ok();
        }

        [HttpPost]
        public ActionResult UploadFile(IList<IFormFile> files)
        {
            string ID = HttpContext.Session.GetString("tes");

            foreach (IFormFile source in files)
            {
                var newfile = new FileUpload()
                {
                    CampaignID = ID,
                    files = source
                };
                Upload(newfile);
            }

            HttpContext.Session.Clear();

            return Json(new { success= true });
        }


        public void Upload(FileUpload file)
        {
            try
            {
                if (file != null)
                {
                    var rootPath = FilePathRead.ReadFile();
                    var newfilename = file.files.FileName;
                    var newPath = Path.Combine("", rootPath + newfilename);

                    using (var stream = new FileStream(newPath, FileMode.Create))
                    {
                        file.files.CopyTo(stream);
                    }

                    Campaign_FileUpload newFile = new Campaign_FileUpload();
                    newFile.CampaignID = file.CampaignID;
                    newFile.FileName = file.files.FileName;
                    newFile.FilePath = newPath;
                    newFile.UploadDate = DateTime.Now;
                    newFile.UserUpload = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    //newFile.Id = files.Id;

                    _dbContext.Campaign_FileUpload.AddAsync(newFile);
                    _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
