
$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});


// CLIENT FUNCTIONS
$("#btnSearchClients").on("click", function () {
    searchClients($("#txtSearchClients").val());
})
$("#btnCancelClientSearch").on("click", function () {
    $("#txtSearchClients").val("");
    $("#clientTableDiv").html("");
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
            HouseNumber: $("#txtClientAddressLine1").val(),
            StreetName1: $("#txtClientAddressLine2").val(),
            StreetName2: $("#txtClientAddressLine3").val(),
            StreetName3: $("#txtClientAddressLine4").val(),
            Nationality: $("#txtClientNationailty").val(),
            Language: $("#txtClientLanguage").val(),
            Dob: $("#txtClientDOB").val()
        };
        // validate inputs
        if (!clientData.HouseNumber || !clientData.Nationality || !clientData.Language || !clientData.Dob) {
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
//$(".clientDataRow").on("click", function () {  // https://stackoverflow.com/a/26602984/7162741 this function got fired many times cuz the js file was loaded in some partial views
$(document).on("click", ".clientDataRow", function () {
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
                var client = response.msg;
                $("#txtClientName").val(client.name);

                $("#txtClientSurname").val(client.name);
                $("#txtClientAddressLine1").val(client.houseNumber);
                $("#txtClientAddressLine2").val(client.streetName1);
                $("#txtClientAddressLine3").val(client.streetName2);
                $("#txtClientAddressLine4").val(client.streetName3);
                $("#txtClientNationailty").val(client.nationality);
                $("#txtClientLanguage").val(client.language);
                $("#txtClientSurname").val(client.surname);
                $("#txtClientReferedBy").val(client.referredBy);
                $("#txtClientRefUrl").val(client.refUrl);
                $("#txtClientUserId").val(client.id);
                $("#txtClientEmail").val(client.email);
                $("#txtClientKycLevel").val(client.kycLevel);
                document.getElementById("txtClientDOB").valueAsDate = new Date(client.dateOfBirth);
                document.getElementById("txtClientJoinedOn").valueAsDate = new Date(client.joinedDate);
                document.getElementById("txtClientDate").valueAsDate = new Date();

                loadClientNotes(id);
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
            if (response) {

                $("#tblTrades").html(response) 

            }
            else {
                alert(response.msg);
            }
        },
        error: function () {
            alert("An error occured");
        },
    })

    $(document).on("dblclick", ".tradeDataRow", function () { 
        var id = $($(this).find('td:last').html()).val();
        var table = document.getElementById("tbTradeSearchResult");


        $.ajax({
            type: "GET",
            url: "/home/trades/" + id,
            contentType: "application/json",
            processData: false,
            success: function (response) {
                if (response.success) {
                    trade = response.msg;
                    for (var i = 2; i <= table.rows.length; i++) {
                        table.deleteRow(i - 1);
                    }

                    var tr = table.insertRow(-1);
                    var tabCell1 = tr.insertCell(-1);
                    var date = new Date(trade.createdOn);
                    tabCell1.innerHTML = date.getFullYear() + '/' + (date.getMonth() + 1) + '/' + date.getDate();

                    var tabCell2 = tr.insertCell(-1);
                    tabCell2.innerHTML = trade.exchange;
                    var tabCell3 = tr.insertCell(-1);
                    tabCell3.innerHTML = trade.operation;

                    var tabCell4 = tr.insertCell(-1);
                    tabCell4.innerHTML = trade.currencyPair;

                    var tabCell5 = tr.insertCell(-1);
                    tabCell5.innerHTML = "$" + trade.price;

                    var tabCell6 = tr.insertCell(-1);
                    tabCell6.innerHTML = trade.arbitrage; 

                    var tabCell7 = tr.insertCell(-1);
                    tabCell7.innerHTML = trade.social;
                }
                else {
                    alert('error fetching trade data!');
                }
            },
            error: function () {
                alert("An unknown error has occured");
            }
        })

        //ar trade = getTradeById(id);
        // clear table

    });

    // populate client's exchanges
    searchExchanges(id);

    // populate Membership tab
    searchMemberships(id);

    // populate clien't tickets
    searchTickets("userid=" + id);

})
$("#btnAddNote").on("click", function (e) {
    var clientId = $("#hiddenClientID").val();
    if (!clientId) {
        alert("Not client selected!");
        return;
    }
    const note = $("#txtNewNote").val();
    if (!note) {
        alert("Note can not be empty!");
        return;
    }
    e.preventDefault();

    var url = "/home/createNote/" + note + "/" + clientId;  // assume it's an add operation
    let noteId = $("#txtClientNoteId").val();
    if (noteId) {
        url = "/home/updateClientNote/" + note + "/" + noteId;
    }

    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                if (!noteId) {
                    $("#txtNewNote").val("");
                }
               
                // Todo: re-render notes partial
                loadClientNotes(clientId);
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

//triggered when client note modal is about to be shown  addTicketNoteModal
$('#addNoteModal').on('show.bs.modal', function (e) {

    //get data-id attribute of the clicked element
    let noteId = $(e.relatedTarget).data('note-id');
    if (!!noteId) {
        // update operation
        // populate the textarea
        $(e.currentTarget).find('textarea[name="txtNewNote"]').val($(e.relatedTarget).text());
        $(e.currentTarget).find('input[id="txtClientNoteId"]').val(noteId);

        var title = $(e.currentTarget).find('h4')[0];
        title.innerText = "Update Note";
    }
    else {
        $(e.currentTarget).find('textarea[name="txtNewNote"]').val("");
        $(e.currentTarget).find('input[id="txtClientNoteId"]').val("");
        var title = $(e.currentTarget).find('h4')[0];
        title.innerText = "Create a new note";
    } 
});

//triggered when ticket note modal is about to be shown  
$('#addTicketNoteModal').on('show.bs.modal', function (e) {

    //get data-id attribute of the clicked element
    let noteId = $(e.relatedTarget).data('note-id');
    if (!!noteId) {
        // update operation
        // populate the textarea
        $(e.currentTarget).find('textarea[name="txtNewTicketNote"]').val($(e.relatedTarget).text());
        $(e.currentTarget).find('input[id="txtTicketNoteId"]').val(noteId);

        var title = $(e.currentTarget).find('h4')[0];
        title.innerText = "Update Note";
    }
    else {
        $(e.currentTarget).find('textarea[name="txtNewTicketNote"]').val("");
        $(e.currentTarget).find('input[id="txtTicketNoteId"]').val("");
        var title = $(e.currentTarget).find('h4')[0];
        title.innerText = "Create a new note";
    }
});

//triggered when staff note modal is about to be shown  
$('#addStaffNoteModal').on('show.bs.modal', function (e) {

    //get data-id attribute of the clicked element
    let noteId = $(e.relatedTarget).data('note-id');
    if (!!noteId) {
        // update operation
        // populate the textarea
        $(e.currentTarget).find('textarea[name="txtNewStaffNote"]').val($(e.relatedTarget).text());
        $(e.currentTarget).find('input[id="txtStaffNoteId"]').val(noteId);

        var title = $(e.currentTarget).find('h4')[0];
        title.innerText = "Update Note";
    }
    else {
        $(e.currentTarget).find('textarea[name="txtNewStaffNote"]').val("");
        $(e.currentTarget).find('input[id="txtStaffNoteId"]').val("");
        var title = $(e.currentTarget).find('h4')[0];
        title.innerText = "Create a new note";
    }
});


// TRADES FUNCTIONS
$("#btnSearchTrades").on("click", function () {
    var userId = $("#srchUserId").val();
    var dateFrom = $("#tradeFromDate").val();
    var dateTo = $("#tradeToDate").val();
    var exchange = $("#tradeSrExch").val();
    var currencyPair = $("#tradeSrCurr").val();
    if (userId == "" && dateFrom == "" && dateTo == "" && exchange == "" && currencyPair == "") {
        alert("Atleast one field must be entered");
        return;
    }

    var getUserId = $("#srchUserId").val();

    var getToDate = !!$("#tradeToDate").val() ? new Date($("#tradeToDate").val()) : null;
    var getFromDate = !!$("#tradeFromDate").val() ? new Date($("#tradeFromDate").val()) : null;
    var getExchange = $("#tradeSrExch").val();
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
    if (getToDate) {
        searchObject["ToDateTime"] = getToDate.toISOString();  // https://github.com/Automattic/mongoose/issues/756#issuecomment-65924767
    }
    if (getFromDate) {
        searchObject["FromDateTime"] = getFromDate.toLocaleDateString();
    }
    if (currencyPair) {
        searchObject["currencyCode"] = currencyPair;
    }

    var url = "/home/trades/search?" + $.param(searchObject, true);
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response) {

                $("#tbTradeSearchResult").html(response);

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


// EXCHANGE FUNCTIONS
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


// TICKET FUNCTIONS
$("#btnSearchTickets").on("click", function () {
    searchTickets($("#txtTicketSearchKeyword").val());
})
$("#btnCancelTicketSearch").on("click", function () {
    $("#txtTicketSearchKeyword").val("*");
    searchTickets("*");
})
$(document).on("click", ".ticketDataRow", function () {
    var id = $(this).find('td:first').html();
    $("#SelectedTicketId").val(id);
    let name = ($(this).find('td')[4]).innerText;
    $("#txtTicketAssignedTo").val(name);
    loadTicketDetails(id);

    // unable the buttons
    $("#btnSendResponse").removeClass("disabled");
    $("#btnCloseTicket").removeClass("disabled");
    $("#btnUpdateTicket").removeClass("disabled");
    $("#btnShowTicketModal").removeClass("disabled");
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
     
    var url = "/home/ticket/" + ticketId + "/createNote/" + note; // for creating a new note

    let noteId = $("#txtTicketNoteId").val();
    if (noteId) {
        url = "/home/updateTicketNote/" + note + "/" + noteId;  // update note
    }
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                if (!noteId) {
                    $("#txtNewTicketNote").val("");
                }
               
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
$("#btnCloseTicket").on("click", function () {
    if (confirm("Are You Sure You Want to Close this Ticket?")) {
        var id = $("#SelectedTicketId").val();
        if (!id) {
            alert('No ticket selected!');
            return;
        }
        closeTicket(id);
    }
})
$("#btnUpdateTicket").on("click", function () {
    if (confirm("Proceed to update?")) {
        var id = $("#SelectedTicketId").val();
        if (!id) {
            alert('No ticket selected!');
            return;
        }
        var staffId = $("#txtTicketReAssign").val();
        var note = "some dummy note";
        updateTicket(id, staffId, note);

        // successful update
        // reload ticket table
        searchTickets("*");
        // clear the selected ticket data
        $("#txtTicketAssignedTo").val("");
    }
})

function closeTicket(id) {
    var url = "/home/tickets/close/" + id;
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
}
function updateTicket(id, assignedStaffId, note) {
    var model = {
        Id: id,
        AssignedStaffId: assignedStaffId,
        Note: note
    };
    let url = "/home/tickets/update";
    $.ajax({
        url: url,
        type: "POST",
        url: url,
        data: JSON.stringify(model),
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
                   $('#clientTicketNotes').append('<p data-toggle="modal" data-target="#addTicketNoteModal" data-note-id="' + notes[ind].id + '" >' + notes[ind].shortNote + '</p >')
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



// STAFF FUNCTIONS
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
                var staff = response.msg;
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
                $("#txtStaffNationality").val(staff.nationality);
                document.getElementById("txtStaffHiredOnDate").valueAsDate = new Date(staff.hiredOn);
                document.getElementById("txtStaffFiredOnDate").valueAsDate = new Date(staff.firedOn);
                document.getElementById("txtStaffResignedOnDate").valueAsDate = new Date(staff.firedOn);
                document.getElementById("txtStaffDOB").valueAsDate = new Date(staff.dateOfBirth);
                $("#teritories").val(staff.teritories);
                $("#txtStaffLanguages").val(staff.languages); 

                loadStaffNotes(id);
            }
            else {
                alert(response.msg);
            }
        },
        error: function (e) {
            alert("An error occured");
        },
    })

})

$("#formUpdateStaff :input").change(function () {
    if ($("#hiddenSelectedStaffID").val()) {
        $("#btnUpdateStaff").removeClass("disabled");
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
    let noteId = $("#txtStaffNoteId").val();
    if (noteId) {
        url = "/home/updateStaffNote/" + note + "/" + noteId;
    }
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response.success) {
                if (!noteId) {
                    $("#txtNewStaffNote").val("");
                }
               
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

    $("#btnUpdateStaff").removeClass("disabled");
    resetStaffForm();
})
$("#btnUpdateStaff").on("click", function (e) {
    // save or update opn
    // check input fields
    var id = $("#hiddenSelectedStaffID").val();
    if (!id) id = '0';

    if (id === '0' && $("#txtStaffPassword").val() != $("#txtStaffConfirmPassword").val()) {
        alert("Password and confirm password do not match!");
        return;
    }

    let staff = {
        Id: id,
        Name: $("#txtStaffName").val(),
        Surname: $("#txtStaffSurname").val(),
        Department: $("#txtStaffDepartment").val(),
        Manager: $("#txtStaffManager").val(),
        StreetNumber: $("#txtStaffStreetNumber").val(),
        StreetName1: $("#txtStaffStreetName1").val(),
        StreetName2: $("#txtStaffStreetName2").val(),
        StreetName3: $("#txtStaffStreetName3").val(),
        HiredBy: $("#txtStaffHiredBy").val(),
        Nationality: $("#txtStaffNationality").val(),
        Teritories: $("#teritories").val(),
        Languages: $("#txtStaffLanguages").val(),
        DateOfBirth: $("#txtStaffDOB").val(),
        FiredOn: $("#txtStaffFiredOnDate").val() ? $("#txtStaffFiredOnDate").val() : undefined,
        HiredOn: $("#txtStaffHiredOnDate").val(),
        ResignedOn: $("#txtStaffResignedOnDate").val() ? $("#txtStaffResignedOnDate").val() : undefined,
        Password: $("#txtStaffPassword").val(),
        Email: $("#txtStaffEmailAddress").val()
    };

    if (!validateStaffInputFields(staff)) {
        return;
    }
    // if selected id is null, Save
    // else update
    saveOrUpdateStaff(staff);

    // reset form
    // disable update button
    if (!id) {  // "save" operation
        $("#btnUpdateStaff").addClass("disabled");
    }
   
})

$("#btnUpdateStaffPassword").on("click", function (e) {
    var staffId = $("#hiddenSelectedStaffID").val();
    if (!staffId) {
        alert("No staff selected!");
        return;
    }

    var oldPassword = $("#txtOldStaffPassword").val();
    var newpassword = $("#txtNewStaffPassword").val();
    if (oldPassword.length < 5 || newpassword.length < 5) {
        alert("A minimum of 5 character password is required!");
        return;
    }


    var url = "/home/staff/" + staffId + "/changepassword/" + oldPassword + "/" + newpassword;

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



$("#btnTradesCancel").on("click", function () {
    if (confirm("Are You Sure You Want to Clear inputs?")) {
        removeTrades();
    }
})




function ajaxGet(url, callBackFunction) {
    
}

function removeTrades() {
    $("#srchUserId").val("");
    $("#tradeSrExch").val("");
    $("#tradeSrCurr").val("");

    document.getElementById("tradeFromDate").valueAsDate = null;
    document.getElementById("tradeToDate").valueAsDate = null;

    $("#transactionId").val("");
    $("#walletFrom").val("");
    $("#walletTo").val("");
}


function removeSelectedClientRecords() {
    $("#hiddenClientID").val("");
    $("#txtClientName").val("");
    $("#txtClientSurname").val("");
    $("#txtClientAddressLine1").val("");
    $("#txtClientAddressLine2").val("");
    $("#txtClientAddressLine3").val("");
    $("#txtClientAddressLine4").val("");
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

    // clear notes
    $("#clientNotes").html("");
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
    var url = "/home/staff/" + staffId + "/Notes";
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

var loadClientNotes = function (clientId) {
    var url = "/home/clients/" + clientId + "/Notes";
    $.ajax({
        url: url,
        type: "GET",
        url: url,
        contentType: "application/json",
        processData: false,
        success: function (response) {
            if (response) {
                $("#clientNotes").html(response);
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
    document.getElementById("txtStaffDOB").valueAsDate = new Date();
    document.getElementById("txtStaffFiredOnDate").valueAsDate = new Date();
    document.getElementById("txtStaffResignedOnDate").valueAsDate = new Date();
}

function validateStaffInputFields(staff) {
    if (staff.Id === '0' && (!staff.Password || staff.Password.length < 5)) {
        alert("A minimum of 5 character password is required!");
        return;
    }
    if (!staff.HiredOn || staff.HiredOn <= staff.DateOfBirth) {
        alert("Hired on date must be greater than the date of birth");
        return false;
    }
    if (staff.ResignedOn < staff.HiredOn || staff.FiredOn < staff.HiredOn) {
        alert("Date hired must be greater than date fired or resigned");
        return false;
    } 

    if (staff.Nationality && staff.HiredBy && staff.Name && staff.Surname && staff.Nationality && staff.StreetNumber && staff.DateOfBirth && staff.Email) {  // check for other things
        return true;
    }

    alert("Fill in all required fields!");
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
