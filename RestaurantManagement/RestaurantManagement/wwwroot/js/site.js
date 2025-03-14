document.addEventListener("DOMContentLoaded", function () {
    const token = localStorage.getItem("accessToken");
    const loginLink = document.getElementById("loginLink");
    const registerLink = document.getElementById("registerLink");
    const logoutLink = document.getElementById("logoutLink");

    if (token) {
        loginLink.style.display = "none";
        registerLink.style.display = "none";
        logoutLink.style.display = "block";
    }
});

function logout() {
    localStorage.removeItem("accessToken");
    window.location.href = "/Auth/Login"; // Chuyển hướng về trang đăng nhập
}
