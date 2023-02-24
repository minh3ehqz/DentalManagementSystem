//chặn người dùng nhập 1 chuỗi dấu cách liên tục
$(document).ready(function () {
    $('input[type="text"]').on('input', function () {
        if (/\s{2,}/.test(this.value)) {
            this.value = this.value.replace(/\s{2,}/g, ' '); // thay thế các chuỗi dấu cách bằng một dấu cách
        }
    });
});

