document.addEventListener("DOMContentLoaded", async () => {
    let userId;
    let userRole;

    function parseJwt(token) {
        try {
            const base64Url = token.split(".")[1];
            const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
            const jsonPayload = decodeURIComponent(atob(base64).split("").map(c =>
                "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2)
            ).join(""));
            return JSON.parse(jsonPayload);
        } catch (e) {
            console.error("Invalid JWT:", e);
            return null;
        }
    }

    const token = localStorage.getItem("accessToken");

    if (!token) {
        console.error("JWT not found, user is not logged in.");
    } else {
        const decodedToken = parseJwt(token);
        console.log("Decoded JWT:", decodedToken);

        if (decodedToken) {
            userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            console.log("User ID:", userId);
            console.log("User Role:", userRole);
        }
    }

    if (typeof signalR === "undefined") {
        console.error("SignalR library is missing!");
        return;
    }

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/orderHub")
        .withAutomaticReconnect() // Tự động reconnect
        .build();

    async function startConnection() {
        try {
            await connection.start();
            console.log("Connected to OrderHub");

            await connection.invoke("RegisterUser", userId);
            console.log("User registered:", userId);

            await connection.invoke("JoinGroup", userRole);
            console.log(`User ${userId} joined group ${userRole}`);
        } catch (err) {
            console.error("Connection error:", err);
            setTimeout(startConnection, 5000); // Thử kết nối lại sau 5 giây
        }
    }

    connection.onclose(() => {
        console.warn("Disconnected from OrderHub");
        setTimeout(startConnection, 5000); // Tự động kết nối lại sau 5 giây
    });

    connection.onreconnected(async () => {
        console.log("Reconnected to OrderHub");

        if (userId) {
            await connection.invoke("RegisterUser", userId);
            console.log("Re-registered user:", userId);
        }
    });


    startConnection(); // 🚀 Kết nối ngay khi trang tải xong

    connection.on("NewOrder", (message) => {
        console.log("News Update:", message);
        Toastify({
            text: message,
            duration: 3000,
            gravity: "top",
            position: "right",
            backgroundColor: "#ff2a2a",
            stopOnFocus: true,
        }).showToast();
    });

    connection.on("ReceivePrivateMessage", (message) => {
        Toastify({
            text: message,
            duration: 3000,
            gravity: "top",
            position: "right",
            backgroundColor: "#ff2a2a",
            stopOnFocus: true,
        }).showToast();
    });

    connection.on("ReceiveConnectionId", (connectionId) => {
        console.log("Your Connection ID:", connectionId);
    });

    connection.on("MessageFailed", (error) => {
        console.error("Message Failed:", error);
    });

    window.acceptOrder = async function (element) {
        console.log("Accept Order:", element);

        if (!element.dataset.orderId || !element.dataset.userId) {
            console.error("Missing orderId or userId");
            return;
        }

        const orderId = element.dataset.orderId;
        const cusId = element.dataset.userId;

        element.textContent = "Cancel";
        element.classList.remove("btn-outline-success");
        element.classList.add("btn-outline-danger");

        const orderElement = document.getElementById(orderId);
        if (orderElement) {
            orderElement.textContent = "Processing";
        }

        if (connection) {
            try {
                let message = `Order ${orderId} is accepted`;
                await connection.invoke("SendPrivateMessageToUser", cusId, message);
                console.log("Message sent via SignalR");
            } catch (error) {
                console.error("Failed to send message via SignalR:", error);
            }
        } else {
            console.error("SignalR connection is not initialized.");
        }

        try {
            const token = localStorage.getItem("accessToken");

            const response = await fetch("/api/Orders/ChangeOrderStatus", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify({
                    orderId: orderId,
                    StatusOrder: "Processing",
                }),
            });

            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }

            const responseData = await response.json();
            console.log("Order updated successfully:", responseData);
        } catch (error) {
            console.error("Order update failed:", error);
        }
    };

});
