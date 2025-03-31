
function deleteAllCookies() {
    document.cookie.split(";").forEach(cookie => {
        document.cookie = cookie.replace(/^ +/, "").replace(/=.*/, "=;expires=" + new Date(0).toUTCString() + ";path=/");
    });
}

// 🔑 Đăng xuất và xóa session
function logout() {
    fetch('/Auth/Logout', {
        method: 'POST',
        credentials: 'same-origin'
    })
        .then(response => {
            deleteAllCookies();
            localStorage.removeItem("accessToken");
            sessionStorage.clear();
            window.location.href = response.redirected ? response.url : "/Auth/Login";
        })
        .catch(error => console.error('Logout error:', error));
}
