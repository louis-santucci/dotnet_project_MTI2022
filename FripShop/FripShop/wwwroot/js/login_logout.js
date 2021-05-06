
function dynamicLogButton() {
    var cookie = document.cookie;
    if (cookie != null && cookie != "") {
        var loginButton = document.getElementById("loginButton");
        var registerButton = document.getElementById("registerButton");
        loginButton.style.display = "none";
        registerButton.style.display = "none";

    } else {
        var logoutButton = document.getElementById("logout_submit");
        logoutButton.style.display = "none";
    }
}

dynamicLogButton();