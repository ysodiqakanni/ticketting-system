


$("#btnSearchClients").on("click", function () {
    alert("search clicked!");
})
$("#btnCancelClientSearch").on("click", function () {
    $("#txtSearchClients").val("");
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
$(".clientDaraRow").on("click", function () {
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
    // refresg tables

})
    //$("#tblClients tr").click(function () {
    //    //$(this).addClass('selected').siblings().removeClass('selected');
    //    var value = $(this).find('td:first').html();
    //    alert(value);
    //});