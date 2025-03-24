// Script File
var hamburgerBtn = document.querySelector('.hamburger-btn');
var sideBar = document.querySelector('.side-bar');

hamburgerBtn.addEventListener('click', sidebarToggle);
function sidebarToggle() {
    sideBar.classList.toggle('active');
}

// Code For Light/Dark Mode Toggle
var modeSwitcher = document.querySelector('.mode-switch i');
var body = document.querySelector('body');
modeSwitcher.addEventListener('click', modeSwitch);
function modeSwitch() {
    body.classList.toggle('active');
}

function previewImage(event) {
    var input = event.target;
    var reader = new FileReader();

    reader.onload = function () {
        var imgElement = document.getElementById("preview");
        imgElement.src = reader.result;
        imgElement.style.display = "block";
    };

    if (input.files.length > 0) {
        reader.readAsDataURL(input.files[0]);
    }
}

function submitFood() {
    var food = {
        FoodName: document.getElementById("foodName").value,
        Description: document.getElementById("foodDescription").value,
        Price: document.getElementById("foodPrice").value,
        ListPrice: document.getElementById("foodPrice").value,
        FoodCategoryID: document.getElementById("foodCategory").value
    };

    fetch("/api/foods/CreateFoodAsync", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(food)
    })
        .then(response => response.json())
        .then(data => {
            if (data && data.foodID) {
                let foodId = data.foodID;
                let fileInput = document.getElementById("foodImage");

                if (fileInput.files.length > 0) {
                    let file = new FormData();
                    file.append("file", fileInput.files[0]);
                    return fetch(`/api/foodimage/upload?foodId=${foodId}`, {
                        method: "POST",
                        body: file
                    });
                }
            }
        })
        .then(() => {
            alert("Tạo món ăn thành công!");
            location.reload(); // Reload trang để hiển thị món ăn mới
        })
        .catch(error => console.error("Lỗi:", error));
}

function confirmDelete(foodId) {
    if (confirm("Bạn có chắc chắn muốn xóa món ăn này không?")) {
        fetch(`/api/foods/DeleteFoodAsync?foodId=${foodId}`, {
            method: "DELETE"
        })
            .then(response => {
                if (response.ok) {
                    alert("Xóa món ăn thành công!");
                    location.reload(); // Reload để cập nhật danh sách
                } else {
                    alert("Không thể xóa món ăn. Hãy thử lại!");
                }
            })
            .catch(error => console.error("Lỗi:", error));
    }
}

function openEditModal(foodID, name, description, price, category, imageUrl) {
    document.getElementById("editFoodID").value = foodID;
    document.getElementById("editFoodName").value = name;
    document.getElementById("editFoodDescription").value = description;
    document.getElementById("editFoodPrice").value = parseFloat(price);;
    document.getElementById("editFoodCategory").value = category;

    let preview = document.getElementById("editPreview");
    if (imageUrl) {
        preview.src = imageUrl;
        preview.style.display = "block";
    } else {
        preview.style.display = "none";
    }

    let modal = new bootstrap.Modal(document.getElementById("editFoodModal"));
    modal.show();
}

function previewEditImage(event) {
    let preview = document.getElementById("editPreview");
    let file = event.target.files[0];

    if (file) {
        let reader = new FileReader();
        reader.onload = function (e) {
            preview.src = e.target.result;
            preview.style.display = "block";
        };
        reader.readAsDataURL(file);
    }
}

function updateFood() {
    let foodID = document.getElementById("editFoodID").value;
    let FoodName = document.getElementById("editFoodName").value;
    let Description = document.getElementById("editFoodDescription").value;
    let Price = document.getElementById("editFoodPrice").value;
    let ListPrice = document.getElementById("editFoodPrice").value;
    let FoodCategoryID = document.getElementById("editFoodCategory").value;

    let formData = {
        foodID: foodID,
        foodName: FoodName,
        description: Description,
        price: parseFloat(Price),
        listPrice: parseFloat(ListPrice),
        foodCategoryID: FoodCategoryID
    };

    let fileInput = document.getElementById("editFoodImage");
    if (fileInput.files.length > 0) {
        formData.append("Image", fileInput.files[0]);
    }

    fetch(`/api/foods/UpdateFoodAsync`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData)
    })
        .then(response => response.json())
        .then(data => {
            alert("Cập nhật thành công!");
            location.reload();
        })
        .catch(error => console.error("Lỗi:", error));
}
