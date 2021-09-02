$(document).ready(function () {
    //debugger;
    $.ajax({
        url: "/Master/GetBU/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            //debugger;
            //console.log(data);
            localStorage.setItem("budata", JSON.stringify(data));
            //console.log(data);
            var dropDown = document.querySelector(".business-unit-dropdown");
            for (var i = 0; i < data.length; i++) {
                dropDown.innerHTML = dropDown.innerHTML +
                    '<option value="' + data[i].buid + '">' + data[i].bu + '</option > ';
            }
        },
        error: function (errormessage) {
            alert("Something went wrong!!!");
        }
    });

    $.ajax({
        url: "/Master/GetBrand/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            //debugger;
            localStorage.setItem("branddata", JSON.stringify(data));
            //console.log(data);
            var dropDown = document.querySelector(".brand-unit-dropdown");
            for (var i = 0; i < data.length; i++) {
                //let optionValue = `<option value="${data[i].brandid}">${data[i].brandname}</option>`;
                //console.log(optionValue);
                dropDown.innerHTML = dropDown.innerHTML +
                    '<option value="' + data[i].brandID + '">' + data[i].brandName + '</option > ';
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    $.ajax({
        url: "/Master/GetHeader/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            //debugger;
            //console.log(data);
            //console.log(data);
            localStorage.setItem("campaigndata", JSON.stringify(data));
            var dropDown = document.querySelector(".campaign-list-dropdown");
            for (var i = 0; i < data.length; i++) {
                //let optionValue = `<option value="${data[i].brandid}">${data[i].brandname}</option>`;
                //console.log(optionValue);
                if (data[i].status > 0) {
                    dropDown.innerHTML = dropDown.innerHTML +
                        `<option value="${data[i].campaignID}">${data[i].campaignID} - ${data[i].campaignName}</option>`;
                }
                
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

});