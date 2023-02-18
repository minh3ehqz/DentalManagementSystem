// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Lấy thẻ popup
var popup = document.getElementById("myPopup");

// Khi người dùng nhấn nút, mở popup
function openPopup() {
    popup.style.display = "block";
}

// Khi người dùng nhấn vào nút đóng, đóng popup
function closePopup() {
    popup.style.display = "none";
}

// Khi người dùng nhấn phím ESC, đóng popup
document.onkeydown = function (evt) {
    evt = evt || window.event;
    if (evt.keyCode == 27) {
        closePopup();
    }
}

//chặn người dùng nhập 1 chuỗi dấu cách liên tục
$(document).ready(function () {
    $('input[type="text"]').on('input', function () {
        if (/\s{2,}/.test(this.value)) {
            this.value = this.value.replace(/\s{2,}/g, ' '); // thay thế các chuỗi dấu cách bằng một dấu cách
        }
    });
});


