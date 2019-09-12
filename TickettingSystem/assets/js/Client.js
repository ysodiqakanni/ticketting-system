
$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

$("#btnSearchClients").on("click", function () {
    searchClients($("#txtSearchClients").val());
})



$("#btnSearchTrades").on("click", function () {
    /*var getTodate;
    var getFromDate;
    var getUserId;
    var getExchange;
    if (getTodate.getTime() < getFromdate.getTime()) {
        alert("to date must come after from date");
    }*/
    var url = "/home/trades/search";
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
$(".clientDataRow").on("click", function () {
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
    var userId = $("#txtUserIdForExchangeSearch").val();;
    if (!userId) {
        alert("Enter a user ID to fetch exchanges");
        return;
    }
    searchExchanges(userId);
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
                $("#notes").html(response); 
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