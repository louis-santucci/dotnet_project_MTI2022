
function dynamicLogButton() {
    cookie = document.cookie;
    if (cookie != null && cookie != "") {
        var loginButton = document.getElementById("loginButton");
        var registerButton = document.getElementById("registerButton");
        loginButton.style.display = "none";
        registerButton.style.display = "none";

    } else {
        var logoutButton = document.getElementById("Submit");
        logoutButton.style.display = "none";
    }
}

dynamicLogButton();