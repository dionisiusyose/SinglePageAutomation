// Campaign Description Input
var campaignid = document.getElementById("campaignid");
var campaignname = document.getElementById("campaignname");
var startdate = document.getElementById("startdate");
var enddate = document.getElementById("enddate");
var businessunit = document.getElementById("businessunit");
var brand = document.getElementById("brand");
var targetaudience = document.getElementById("targetaudience");

var searchCampaign = document.querySelector(".search-btn");



//local Storage Business Unit
var buData = JSON.parse(localStorage.getItem("budata"));

var buList = [];
buData.forEach((data) => {
    buList.push(data.bu);
})

////Local Storage Brand Data
var brandData = JSON.parse(localStorage.getItem("branddata"));
var brandList = [];
brandData.forEach((data) => {
    brandList.push(data.brandID)
});


// Local Storage Campaign Data
var campaignData = JSON.parse(localStorage.getItem("campaigndata"));
var filteredCampaignStatus = [];
campaignData.forEach((data) => {
    if (data.status > 0) {
        filteredCampaignStatus.push(data);
    }
});
//var qt = JSON.parse(campaignData);
var campaignList = [];
campaignData.forEach((data) => {
    campaignList.push(data.campaignID);
});

var campaigndetailbutton = document.querySelector(".campaign-select-go-container");

campaigndetailbutton.addEventListener("click", () => {
    //debugger;
    //$(".campaign-list-dropdown").select2();

    var selectedCampaignID = $(".campaign-list-dropdown :selected").val();
    
    selectedData = campaignData[campaignList.indexOf(selectedCampaignID)];

    //console.log(selectedData);
    campaignid.value = selectedData.campaignID;
    campaignname.value = selectedData.campaignName;
    startdate.value = selectedData.startDate;
    enddate.value = selectedData.endDate;
    businessunit.value = selectedData.businessUnit;
    brand.value = selectedData.brand;
    targetaudience.value = selectedData.targetAudience;
})

//Change on Business Unit
$(".business-unit-dropdown").on('change', () => {
    var dropDown = document.querySelector(".brand-unit-dropdown");
    var changeBU = $(".business-unit-dropdown :selected").val();
    dropDown.innerHTML = "";

    var filteredBrand = [];
    //console.log(brandData);
    brandData.forEach((data) => {
        if (data.buid == changeBU) {
            filteredBrand.push(data);
        }
    });

    //console.log(filteredBrand.length);
    
    dropDown.innerHTML = ' <option value="" selected hidden>Select Brand ...</option>';
    for (var i = 0; i < filteredBrand.length; i++) {
        
        dropDown.innerHTML = dropDown.innerHTML +
            '<option value="' + filteredBrand[i].brandID + '">' + filteredBrand[i].brandName + '</option > ';
    }
})


$(".campaign-list-dropdown").on('change', () => {
    let campaignValue = $(".campaign-list-dropdown :selected").val();
    var VMCampaignID = new Object();
    VMCampaignID.ID = campaignValue;

    $.ajax({
        type: "Post",
        url: "/Master/SetCampaignID",
        data: VMCampaignID,
        dataType: 'json',
    })
})

//Filter Campaign
searchCampaign.addEventListener("click", () => {
    //debugger;
    var changeBU = $(".business-unit-dropdown :selected").val();
    var changeBrand = $(".brand-unit-dropdown :selected").val();
    var dropDown = document.querySelector(".campaign-list-dropdown");
    dropDown.innerHTML = "";

    dropDown.innerHTML = '<option value="" selected hidden>Select Campaign ...</option>'


    //console.log(filteredCampaignStatus);
    if (changeBU == "" && changeBrand == "") {
        for (var i = 0; i < filteredCampaignStatus.length; i++) {
            //let optionValue = `<option value="${qt[i].brandid}">${qt[i].brandname}</option>`;
            console.log(filteredCampaignStatus[i]);

            dropDown.innerHTML = dropDown.innerHTML +
                `<option value="${filteredCampaignStatus[i].campaignID}">${filteredCampaignStatus[i].campaignID} - ${filteredCampaignStatus[i].campaignName}</option>`;
        }
    }
    else if (changeBU != "" && changeBrand == "") {
        var buText = $(".business-unit-dropdown :selected").text();
        filteredCampaignStatus.forEach((data) => {
            if (data.businessUnit == buText) {
                dropDown.innerHTML = dropDown.innerHTML +
                    `<option value="${data.campaignID}">${data.campaignID} - ${data.campaignName}</option>`;
            }
        })
    }
    else {
        //var buText = $(".business-unit-dropdown :selected").text();
        var brandText = $(".brand-unit-dropdown :selected").text();

        filteredCampaignStatus.forEach((data) => {
            if (data.brand.indexOf(brandText) > -1) {
                dropDown.innerHTML = dropDown.innerHTML +
                    `<option value="${data.campaignID}">${data.campaignID} - ${data.campaignName}</option>`;
            }
        })
    }
})


// Upload Data Functionality
const form = document.querySelector("form"),
    fileInput = form.querySelector(".file-input"),
    //progressArea = document.querySelector(".progress-area"),
    uploadedArea = document.querySelector(".uploaded-area");


form.addEventListener("click", () => {
    fileInput.click();
    
});

fileInput.onchange = ({ target }) => {
    //debugger;
    let file = target.files[0];
    if (file) {
        let fileName = file.name;
        if(fileName.length >= 12){
            let splitName = fileName.split('.');
            fileName = splitName[0].substring(0, 13) + "... ." + splitName[1];
        }

        let campaignValue = $(".campaign-list-dropdown :selected").val();
        if (campaignValue != "") {
            uploadFile(fileName, campaignValue);
        }
        else {alert("No Campaign Selected, Please Select Campaign First !!!");}
    }
}

function uploadFile(fileName, id) {
    //debugger;
    var FileUpload = new Object();
    var formData = new FormData();
    var inputData = fileInput.files;

    for (var i = 0; i != inputData.length; i++) {
        formData.append("files", inputData[i])
    }


    FileUpload.CampaignID = id;
    FileUpload.files = formData;
    //debugger;

    $.ajax({
        type: "Post",
        url: "/Master/UploadFile",
        data: FileUpload.files,
        processData: false,
        contentType: false,
        success: function (result) { 

            Swal.fire('File Uploaded!', '', 'success');

            let fileTotal = Math.floor(fileInput.files[0].size / 1000);
            let fileSize;
            if (fileTotal < 1024) {
                fileSize = fileTotal + " KB";
            } else {
                fileSize = (fileTotal / 1024).toFixed(2) + " MB"
            }

            if (result.success = "true") {

                uploadedArea.classList.add("onprogress");

                let uploadedHTML = `<li class="row">
                            <div class="content upload">
                              <i class="fas fa-file-alt"></i>
                              <div class="details">
                                <span class="name">${fileName} • Uploaded</span>
                                <span class="size">${fileSize}</span>
                              </div>
                            </div>
                            <i class="fas fa-check"></i>
                          </li>`;
                uploadedArea.classList.remove("onprogress");
                uploadedArea.insertAdjacentHTML("afterbegin", uploadedHTML);
            }
        },
        error: function (result) {
            Swal.fire('File Uploaded!', '', 'error');
        }
    }).then((data) => {
        
    })
}

$(".save-and-submit-container").on('click', () => {
    $(".campaign-list-dropdown").val(null).trigger('change');
    $(".business-unit-dropdown").val(null).trigger('change');
    $(".brand-unit-dropdown").val(null).trigger('change');
})