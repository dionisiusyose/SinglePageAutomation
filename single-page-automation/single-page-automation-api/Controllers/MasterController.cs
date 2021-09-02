using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using single_page_automation_api.AddOns;
using single_page_automation_api.Data;
using single_page_automation_api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace single_page_automation_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private AppDBContext _dbContext;

        public MasterController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        // Get All Bu's Data
        [HttpGet("GetBU")]
        public IActionResult GetBU()
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

        // Get All Brand Data
        [HttpGet("GetBrand")]
        public IActionResult GetBrand()
        {
            try
            {
                var brand = _dbContext.Mst_Brand.ToList();

                if (brand.Count == 0)
                {
                    return StatusCode(404, "No data was found");
                }
                return Ok(brand);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error has occured");
            }
        }

        // Get All Header
        [HttpGet("GetHeader")]
        public IActionResult GetCampaignHeader()
        {
            try
            {
                var brand = _dbContext.vCampaignHeader.ToList();

                if (brand.Count == 0)
                {
                    return StatusCode(404, "No data was found");
                }
                return Ok(brand);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error has occured");
            }
        }


        // Upload Data
        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile([FromForm]FileUpload files)
        {
            //return Ok(files);
            try
            {
                if (files != null)
                {
                    var rootPath = FilePathRead.ReadFile();
                    var newfilename = files.files.FileName;
                    var newPath = Path.Combine("", rootPath + newfilename);

                    using (var stream = new FileStream(newPath, FileMode.Create))
                    {
                        files.files.CopyTo(stream);
                    }

                    Campaign_FileUpload newFile = new Campaign_FileUpload();
                    newFile.CampaignID = files.CampaignID;
                    newFile.FileName = files.files.FileName;
                    newFile.FilePath = newPath;
                    newFile.UploadDate = DateTime.Now;
                    newFile.UserUpload = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    //newFile.Id = files.Id;

                    await _dbContext.Campaign_FileUpload.AddAsync(newFile);
                    await _dbContext.SaveChangesAsync();

                    return Ok("Upload Successful");
                }


                return StatusCode(500, "Something went wrong");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }



        }
    }
}
