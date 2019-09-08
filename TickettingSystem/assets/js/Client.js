
$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

$("#btnSearchClients").on("click", function () {
    alert("search clicked!");
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
        let url1 = "/home/ResetPassword";
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
    // get client by Id
    // populate details section
    // refresh tables

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

//$(".notes").dblclick(function () {
//    alert("The paragraph was double-clicked");
//    $(this).popover("show");
//});

//function popNote(el) {
//    $(el).popover("show");
//}

//$('body').on('click', function (e) {
//    //did not click a popover toggle or popover
//    if ($(e.target).data('toggle') !== 'popover'
//        && $(e.target).parents('.popover.in').length === 0) {
//        $('[data-toggle="popover"]').popover('hide');
//    }
//});
 