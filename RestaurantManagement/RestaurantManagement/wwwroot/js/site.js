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

function deleteAllCookies() {
    document.cookie.split(";").forEach(function (cookie) {
        document.cookie = cookie
            .replace(/^ +/, "") // Xóa khoảng trắng đầu dòng
            .replace(/=.*/, "=;expires=" + new Date(0).toUTCString() + ";path=/"); // Đặt ngày hết hạn về quá khứ
    });
}

function logout() {
    fetch('/Auth/Logout', {
        method: 'POST',
        credentials: 'same-origin' // Giữ cookie cùng domain
    })
        .then(response => {
            deleteAllCookies(); // Xóa cookie trình duyệt

            localStorage.removeItem("accessToken"); // Xóa token trong localStorage
            sessionStorage.clear(); // Xóa sessionStorage (nếu có)

            if (response.redirected) {
                window.location.href = response.url;
            } else {
                window.location.href = "/Auth/Login"; // Chuyển hướng về trang đăng nhập
            }
        })
        .catch(error => console.error('Logout error:', error));
}

