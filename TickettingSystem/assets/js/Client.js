
$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

$("#btnSearchClients").on("click", function () {
    searchClients($("#txtSearchClients").val());
})



$("#btnSearchTrades").on("click", function () {

    var getUserId = $("#srchUserId").val();

    var getToDate=!!$("#tradeToDate").val()? new Date($("#tradeToDate").val()):null;
    var getFromDate=!!$("#tradeFromDate").val()? new Date($("#tradeFromDate").val()):null;
    var getExchange=$("#tradeSrExch").val();
    if (getToDate != null && getFromDate != null) {
        if (getToDate < getFromDate) {
            alert("to date must come after from date");
            return;
        }
    }
    var searchObject = {};
    if (getUserId) {
        searchObject["userId"] = getUserId;
    }
    if (getExchange) {
        searchObject["exchange"] = getExchange;
    }
    if (getToDate ) {
         searchObject["toDate"] =getToDate.toLocaleDateString();
    }
     if (getFromDate) {
         searchObject["fromDate"] =getFromDate.toLocaleDateString();
    }
    var url = "/home/trades/search?"+$.param(searchObject,true);
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                var searchResult = response.msg.result;
                var table = document.getElementById("tbTradeSearchResult");
                for (var i = 2; i < table.rows.length; i++) {
                    table.deleteRow(i - 1);
                }

                for (var i = 0; i < searchResult.length; i++) {
                    var tr = table.insertRow(-1);
                    var tabCell1 = tr.insertCell(-1);
                    var date = new Date(searchResult[i].createdOn);

                    tabCell1.innerHTML = date.getFullYear() + '/' + (date.getMonth() + 1) + '/' + date.getDate();

                    var tabCell2 = tr.insertCell(-1);
                    tabCell2.innerHTML = searchResult[i].exchange;
                    var tabCell3 = tr.insertCell(-1);
                    tabCell3.innerHTML = searchResult[i].operation;
                    var tabCell4 = tr.insertCell(-1);
                    tabCell4.innerHTML = searchResult[i].price;
                    var tabCell5 = tr.insertCell(-1);
                    tabCell5.innerHTML = "Yes";
                    var tabCell6 = tr.insertCell(-1);
                    tabCell6.innerHTML = "No";
                }
               
            }
            else {
                alert("searching error");
            }
        },
        error: function () {
            alert("An unknown error has ;occured");
        },
    })
})
$("#btnCancelClientSearch").on("click", function () {
    $("#txtSearchClients").val("");
    const anyUnsavedChanges = true;
    if (anyUnsavedChanges) {
        if (confirm("Are You Sure That You Want to Abandon Changes?")) {
            removeSelectedClientRecords();
        }
    }
})
$("#btnResetClientPassword").on("click", function () {
    let selectedClientId = $("#hiddenClientID").val();
    if (!selectedClientId) {
        alert("No client selected!");
        return;
    }
    var url = "/home/resetpassword/" + selectedClientId;
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                alert(response.msg);
            }
            else {
                alert(response.msg);
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })

})
$("#btnUpdateClient").on("click", function () {
    let selectedClientId = $("#hiddenClientID").val();
    if (!selectedClientId) {
        alert("No client selected!");
        return;
    }
    if (confirm("Are You Sure You Want to Update This Record?")) {
        let clientData = {
            ID: selectedClientId,
            Address: $("#txtClientAddressLine1").val(),
            Nationality: $("#txtClientNationailty").val(),
            Language: $("#txtClientLanguage").val(),
            DateOfBirth: $("#txtClientDOB").val()
        };
        // validate inputs
        if (!clientData.Address || !clientData.Nationality || !clientData.Language || !clientData.DateOfBirth) {
            alert("Fill in all required fields");
            return;
        }
        // call the update api
        let url = "/home/UpdateClient"; 
        $.ajax({
            url: url,
            type: "PUT",
            url: url,
            data: JSON.stringify(clientData),
            contentType: "application/json",
            processData: false,
            success: function (response) {
                if (response.success) {
                    alert(response.msg);
                    // after successful update, 
                    // Todo: refresh table
                    searchClients(""); 
                }
                else {
                    alert(response.msg);
                }
            },
            error: function () {
                alert("An unknown error has occured");
            },
        })
    }
})
$(".clientDataRow").on("click", function () {  // https://stackoverflow.com/a/26602984/7162741 this function got fired many times cuz the js file was loaded in some partial views
    
    var id = $(this).find('td:first').html();
    $("#hiddenClientID").val(id);
    var url = "/Home/clients/" + id;

    // populate client's details
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                var client = response.msg.result;
                $("#txtClientName").val(client.name);

                $("#txtClientSurname").val(client.name);
                $("#txtClientAddressLine1").val(client.address);
                $("#txtClientNationailty").val(client.nationality);
                $("#txtClientLanguage").val(client.language);
                $("#txtClientSurname").val(client.surname);
                $("#txtClientReferedBy").val(client.referredBy);
                $("#txtClientRefUrl").val(client.refUrl);
                $("#txtClientUserId").val("temp user Id");
                $("#txtClientEmail").val(client.email);
                $("#txtClientKycLevel").val(client.kycLevel);
                document.getElementById("txtClientDOB").valueAsDate = new Date(client.dateOfBirth);
                document.getElementById("txtClientJoinedOn").valueAsDate = new Date(client.joinedDate);
                document.getElementById("txtClientDate").valueAsDate = new Date();
            }
            else {
                alert(response.msg);
            }
        },
        error: function () {
            alert("An error occured");
        },
    })

    // populate client's trades
    $.ajax({
        type: "GET",
        url: "/home/trades/search?UserId=" + id,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                var searchResult = response.msg.result;
                var table = document.getElementById("tbTradeSearchResult");
                for (var i = 2; i < table.rows.length; i++) {
                    table.deleteRow(i - 1);
                }
                $("#srchUserId").val(id);
                for (var i = 0; i < searchResult.length; i++) {
                    var tr = table.insertRow(-1);
                    var tabCell1 = tr.insertCell(-1);
                    var date = new Date(searchResult[i].createdOn);

                    tabCell1.innerHTML = date.getFullYear() + '/' + (date.getMonth() + 1) + '/' + date.getDate();

                    var tabCell2 = tr.insertCell(-1);
                    tabCell2.innerHTML = searchResult[i].exchange;
                    var tabCell3 = tr.insertCell(-1);
                    tabCell3.innerHTML = searchResult[i].operation;
                    var tabCell4 = tr.insertCell(-1);
                    tabCell4.innerHTML = "$"+ searchResult[i].price;
                    var tabCell5 = tr.insertCell(-1);
                    tabCell5.innerHTML = "Yes";
                    var tabCell6 = tr.insertCell(-1);
                    tabCell6.innerHTML = "No";
                }


            }
            else {
                alert(response.msg);
            }
        },
        error: function () {
            alert("An error occured");
        },
    })

    // populate client's exchanges
    searchExchanges(id);

    // populate Membership tab
    searchMemberships(id);

    // populate clien't tickets
    searchTickets("userid=" + id);
})
$("#btnAddNote").on("click", function (e) {
    const note = $("#txtNewNote").val();
    if (!note) {
        alert("Note can not be empty!");
        return;
    }
    e.preventDefault();

    var url = "/home/createNote/" + note;
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                alert(response.msg);
                $("#txtNewNote").val("");
                // Todo: re-render notes partial
                loadNotes();
            }
            else {
                alert(response.msg);
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })
})

$("#btnSearchExchanges").on("click", function () {
    var userId = $("#txtUserIdForExchangeSearch").val();
    if (!userId) {
        alert("Enter a user ID to fetch exchanges");
        return;
    }
    searchExchanges(userId);
})
$("#btnCancelExchangeSearch").on("click", function () {
    // clear search textbox
    // refresh table
    $("#txtUserIdForExchangeSearch").val("");
    searchExchanges("");
})
$("#btnSearchStaff").on("click", function () {
    var keyword = $("#txtStaffSearchKeyword").val();
    if (!keyword) { 
        return;
    }
    searchStaff(keyword); 
})
$("#btnCancelStaffSearch").on("click", function () { 
    $("#txtStaffSearchKeyword").val("*");
    searchStaff("*");
})
$("#btnCancelStaffSearch").on("click", function () {
    $("#txtStaffSearchKeyword").val("*");
    searchStaff("*");
})

$("#btnCancelTicketSearch").on("click", function () {
    $("#txtTicketSearchKeyword").val("*");
    searchTickets("*");
})

//$(".staffDataRow").live("click", function () {  
$(document).on("click", ".staffDataRow", function () {  // https://stackoverflow.com/a/1207393/7162741
    var id = $(this).find('td:first').html(); 
    $("#hiddenSelectedStaffID").val(id);
    var url = "/Home/staff/" + id;
    // populate staff's details
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                var staff = response.msg.result;
                $("#txtStaffName").val(staff.name); 
                $("#txtStaffSurname").val(staff.surname);
                $("#txtStaffStreetNumber").val(staff.streetNumber);
                $("#txtStaffStreetName1").val(staff.streetName1);
                $("#txtStaffStreetName2").val(staff.streetName2);
                $("#txtStaffStreetName3").val(staff.streetName3);
                $("#txtStaffNationality").val(staff.nationality); 
                $("#txtStaffManager").val(staff.manager);
                $("#txtStaffDepartment").val(staff.department);
                $("#txtStaffHiredBy").val(staff.hiredBy);
                document.getElementById("txtStaffHiredOnDate").valueAsDate = new Date(staff.hiredOn);
                document.getElementById("txtStaffFiredOnDate").valueAsDate = new Date(staff.firedOn);
                document.getElementById("txtStaffResignedOnDate").valueAsDate = new Date(staff.firedOn);
                document.getElementById("txtStaffDOB").valueAsDate = new Date(staff.dateOfBirth);

                loadStaffNotes(id);
            }
            else {
                alert(response.msg);
            }
        },
        error: function () {
            alert("An error occured");
        },
    })

})

$("#formUpdateStaff :input").change(function () {
    if ($("#hiddenSelectedStaffID").val()) {
        $("#btnUpdateStaff").attr("disabled", false);
    }
    
});
$("#btnAddStaffNote").on("click", function (e) {
    var staffId = $("#hiddenSelectedStaffID").val();
    if (!staffId) {
        alert("No staff selected!");
        return;
    }
    const note = $("#txtNewStaffNote").val();
    if (!note) {
        alert("Note can not be empty!");
        return;
    }
    e.preventDefault();
    
    var url = "/home/staff/" + staffId + "/createNote/" + note;
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                alert(response.msg);
                $("#txtNewStaffNote").val("");
                // Todo: re-render notes partial
                loadStaffNotes(staffId);
            }
            else {
                alert(response.msg);
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })
})
$("#btnCreateNewStaff").on("click", function (e) {
    resetStaffForm();
})
$("#btnUpdateStaff").on("click", function (e) {
    // save or update opn
    // check input fields
    var id = $("#hiddenSelectedStaffID").val();
    if (!id) id = "0";

    let staff = {
        Id: id,
        Name: $("#txtStaffName").val(),
        Surname: $("#txtStaffSurname").val(),
        Department: $("#txtStaffDepartment").val(),
        Manager: $("#txtStaffManager").val(),
        StreetNumber: $("#txtStaffStreetNumber").val(),
        HiredBy: $("#txtStaffHiredBy").val(),
        Nationality: $("#txtStaffNationality").val()
    };

    if (!validateStaffInputFields(staff)) {
        alert("Fill in all required fields!");
        return; 
    }
    // if selected id is null, Save
    // else update
    saveOrUpdateStaff(staff);

    // reset form
    // disable update button
})

$(document).on("click", ".ticketDataRow", function () {
    var id = $(this).find('td:first').html();
    $("#SelectedTicketId").val(id);
    loadTicketDetails(id);
})

$("#btnAddTicketNote").on("click", function (e) {
    var ticketId = $("#SelectedTicketId").val();
    if (!ticketId) {
        alert("No ticket selected!");
        return;
    }
    const note = $("#txtNewTicketNote").val();
    if (!note) {
        alert("Note can not be empty!");
        return;
    }
    e.preventDefault();

    var url = "/home/ticket/" + ticketId + "/createNote/" + note;
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                alert(response.msg);
                $("#txtNewTicketNote").val("");
                // Todo: re-render notes partial
                loadTicketDetails(ticketId);
            }
            else {
                alert(response.msg);
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })
})

function removeSelectedClientRecords() {
    $("#hiddenClientID").val("");
    $("#txtClientName").val("");
    $("#txtClientSurname").val("");
    $("#txtClientAddressLine1").val("");
    $("#txtClientNationailty").val("");
    $("#txtClientLanguage").val("");
    $("#txtClientSurname").val("");
    $("#txtClientReferedBy").val("");
    $("#txtClientRefUrl").val("");
    $("#txtClientUserId").val("");
    $("#txtClientEmail").val("");
    $("#txtClientKycLevel").val("");
    document.getElementById("txtClientDOB").valueAsDate = new Date();
    document.getElementById("txtClientJoinedOn").valueAsDate = new Date();
    document.getElementById("txtClientDate").valueAsDate = new Date();
}


var searchClients = function (searchStr) {
    var url = "/home/SearchClients/" + searchStr;
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response) {
                $("#clientTableDiv").html(response);
            }
            else {
                alert("searching error");
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })
}

var searchExchanges = function (userId) {
    var url = "/home/exchanges/" + userId;
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response) {
                $("#divClientExchanges").html(response);
            }
            else {
                alert("loading error");
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })
}

var searchMemberships = function (userId) {
    var url = "/home/memberships/" + userId;
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response) {
                $("#custom-tab-5").html(response);
            }
            else {
                alert("loading error");
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })
}
var searchTickets = function (s) {
    var url = "/home/tickets/search?s=" + s;
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response) {
                $("#divClientTickets").html(response);
            }
            else {
                alert("loading error");
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })
}

var loadNotes = function () {
    var url = "/home/Notes";;
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response) {
                $("#clientNotes").html(response); 
            }
            else {
                alert("loading error");
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })
} 

var connectExchange = function (el) {
    var connected = $(el).attr('data-connected');
    if (String("true") == connected) {
        // it's connected so disconnect it
        $(el).attr('data-connected', "false");
        el.style.backgroundColor = "red";
        $(el).val("Connect");
    }
    else if (String("false") == connected) {
        // it's disconnected, so connect it
        $(el).attr('data-connected', "true");
        el.style.backgroundColor = "green";
        $(el).val("Disconnect");
    } 
}
var searchStaff = function (keyword) {
    var url = "/home/staff/search/" + keyword;
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response) {
                $("#divStaffList").html(response);
            }
            else {
                alert("loading error");
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })
}
var loadStaffNotes = function (staffId) {
    var url = "/home/staff/" + staffId+"/Notes";
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response) {
                $("#staffNotes").html(response);
                $('[data-toggle="popover"]').popover();
            }
            else {
                alert("loading error");
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })
} 
function resetStaffForm() {
    $("#formUpdateStaff")[0].reset();
    $("#hiddenSelectedStaffID").val("");
    $("#staffNotes").html("");
    document.getElementById("txtStaffHiredOnDate").valueAsDate = new Date();
}

function validateStaffInputFields(staff) {
    if (staff.Nationality && staff.HiredBy) {  // check for other things
        return true;
    }
    return false;
}
function saveOrUpdateStaff(staff) {
    // after successful addition, reload staff list
   
    // call the save or update api
    let url = "/home/staff/saveorupdate/" + staff.Id;
    $.ajax({
        url: url,
        type: "POST",
        url: url,
        data: JSON.stringify(staff),
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                alert(response.msg);  
                resetStaffForm();
                // disable update button
                $("#btnUpdateStaff").attr("disabled", true);
                // reload staff list
                $("#txtStaffSearchKeyword").val("*");
                searchStaff("*");
            }
            else {
                alert(response.msg);
            }
        },
        error: function () {
            alert("An unknown error has occured");
        },
    })
}

function loadTicketDetails(id) {
    var url = "/Home/tickets/" + id;
    // populate ticket's details
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                var notes = response.notes;
                $("#clientTicketNotes").html("");
                for (ind in notes) {
                    $('#clientTicketNotes').append('<p data-toggle="popover" data-placement="top" data-content="' + notes[ind].content + '" >' + notes[ind].shortNote + '</p >')
                }
                $('[data-toggle="popover"]').popover();

                var convos = response.convos;
                $("#divTicketConversation").html("");
                for (i in convos) {
                    var d = new Date(convos[i].dateCreated)
                    var t = d.getHours() + ":" + d.getMinutes();
                    if (convos[i].createdByClient) {
                        $("#divTicketConversation").append('<div class="single-comment">' +
                            '<p class="text-danger">' + convos[i].content + '</p>' +
                            '<p class="somoy">' + d.toDateString() + '   <span>' + t + '</span></p>' +
                            ' </div>'
                        )
                    }
                    else {
                        $("#divTicketConversation").append('<div class="single-comment">' +
                            '<p class="text-primary">' + convos[i].content + '</p>' +
                            '<p class="somoy">' + d.toDateString() + '   <span>' + t + '</span></p>' +
                            ' </div>'
                        )
                    }

                }
            }
            else {
                alert(response.msg);
            }
        },
        error: function () {
            alert("An error occured");
        },
    })
}