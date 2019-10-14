var timeOnPageLoad = new Date();
let sessionTimeout = $("#txtSessionTimeout").val();
let timeToTimeout = sessionTimeout - 1;   // warning comes a minute to timeout
window.onload = function WindowLoad(event) {
    init();
}

function init() {
    timeOnPageLoad = new Date();
    sessionTimeout = $("#txtSessionTimeout").val();
    timeToTimeout = sessionTimeout - 1;
    var sessionWarningTimer = setTimeout('SessionWarning()',
        parseInt(timeToTimeout) * 60 * 1000);

    var redirectToLoginPageTimer = setTimeout('RedirectToLogin()',
        parseInt(sessionTimeout) * 60 * 1000);
}

function SessionWarning() {
    $('#myModal').modal({ show: true })
    startCountdounTimer();
}

//Session timeout
function RedirectToLogin() {
    $('#myModal').modal({ show: false })

    console.log("Session expired. You will be redirected to login page");
    // alert("Session expired. You will be redirected to login page");
    window.location = "/staff/login";
}

function KeepSessionAlive() {
    const url = "/staff/KeepSessionAlive";
    $.ajax({
        type: "POST",
        url: url,
        success: function () { }
    });
}

// modal interactions
$("#btnKeepAlive").on('click', function () {
    // extend session here
    KeepSessionAlive();

    //Clear the RedirectToLogin method
    if (redirectToLoginPageTimer != null) {
        clearTimeout(redirectToLoginPageTimer);
    }
    //reset the time on page load
    timeOnPageLoad = new Date();
    sessionWarningTimer = setTimeout('SessionWarning()',
        parseInt(timeToTimeout) * 60 * 1000);
    //To redirect to the welcome page
    redirectToLoginPageTimer = setTimeout
        ('RedirectToLogin()', parseInt(sessionTimeout) * 60 * 1000);
})

$("#btnLoggOff").on('click', function () {
    var currentTime = new Date();
    //time for expiry 
    var timeForExpiry = timeOnPageLoad.setMinutes(timeOnPageLoad.getMinutes() +
        parseInt(sessionTimeout));
    //Current time is greater than the expiry time
    if (Date.parse(currentTime) > timeForExpiry) {  // no response until page timed out
        alert("Session expired. You will be redirected to login page");
        window.location = "/staff/login";
    }
    window.location = "/staff/Logout";
})

// start countdown
function startCountdounTimer() {
    var stopTime = new Date();
    stopTime.setMinutes(stopTime.getMinutes() + timeToTimeout);
    var countDownDate = stopTime.getTime();

    // Update the count down every 1 second
    var x = setInterval(function () {

        // Get today's date and time
        var now = new Date().getTime();

        // Find the distance between now and the count down date
        var distance = countDownDate - now;

        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        // Output the result in an element with id="demo"
        document.getElementById("stopTimeLbl").innerHTML = minutes + "m " + seconds + "s ";

        // If the count down is over, write some text 
        if (distance <= 0) {
            clearInterval(x);

        }
    }, 1000);
} 