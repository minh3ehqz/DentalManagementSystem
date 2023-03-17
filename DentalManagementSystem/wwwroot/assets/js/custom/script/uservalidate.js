"use strict";

// Class definition
var KTSigninGeneral = function () {
    // Elements
    var form;
    var submitButton;
    var validator;

    // Handle form
    var handleForm = function (e) {
        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'username': {
                        validators: {
                            regexp: {
                                regexp: /^[a-zA-Z0-9]+$/,
                                message: 'Tên đăng nhập chứa kí tự chữ hoặc số'
                            },
                            notEmpty: {
                                message: 'Chưa nhập tên đăng nhập'
                            }
                        }
                    },
                    'fullname': {
                        validators: {
                            regexp: {
                                regexp: /[A-Z][a-z]/,
                                message: 'Fullname chưa ít nhất một chữ hoa và chữ thường'
                            },
                            notEmpty: {
                                message: 'Chưa nhập họ và tên'
                            }
                        }
                    },
                    'password': {
                        validators: {
                            regexp: {
                                regexp: /^(?=.*[a-z])(?=.*\d)[a-zA-Z\d]{8,}$/,
                                message: 'Mật khẩu chứa ít nhát 8 kí tự, 1 chữ thường, 1 chữ sô'
                            },
                            notEmpty: {
                                message: 'Chưa nhập mật khẩu'
                            }
                        }
                    },
                    'email': {
                        validators: {
                            regexp: {
                                regexp: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                                message: 'Email nhập vào chưa hợp lệ'
                            },
                            notEmpty: {
                                message: 'Chưa nhập email'
                            }
                        }
                    },
                    'phone': {
                        validators: {
                            regexp: {
                                regexp: /^(0|\+84)(?!(?:0{10}|\d{1,9}0{1,9}))(?:\d){9}$/,
                                message: 'Số điện thoại nhập chưa hợp lệ '
                            },
                            notEmpty: {
                                message: 'Chưa nhập số điên thoại'
                            }
                        }
                    },

                    'birthday': {
                        validators: {
                            notEmpty: {
                                message: 'Ngày tháng năm sinh là bắt buộc'
                            },
                            callback: {
                                message: 'Ngày tháng năm sinh không hợp lệ',
                                callback: function (input) {
                                    var currentDate = new Date();
                                    var birthday = new Date(input.value);
                                    return (currentDate >= birthday);
                                }
                            }
                        }
                    },
                    'salary': {
                        validators: {
                            regexp: {
                                regexp: /^[1-9]\d*$/,
                                message: 'Chưa hợp lệ'
                            },
                            notEmpty: {
                                message: 'Chưa nhập dữ liệu'
                            }
                        }
                    }

                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',  // comment to enable invalid state icons
                        eleValidClass: '' // comment to enable valid state icons
                    })
                }
            }
        );

        // Handle form submit
        submitButton.addEventListener('click', function (e) {
            // Prevent button default action
            e.preventDefault();

            // Validate form
            validator.validate().then(async function (status) {
                if (status == 'Valid') {
                    // Show loading indication
                    submitButton.setAttribute('data-kt-indicator', 'on');

                    // Disable button to avoid multiple click 
                    submitButton.disabled = true;
                    var email = document.querySelector("[name=email]").value;
                    var phone = document.querySelector("[name=phone]").value;

                    let url = window.location.origin;
                    let valid = '';
                    await fetch(url + '/User/checkEmailPhone?email=' + email + '&phone=' + phone + '').then((response) => response.text())
                        .then((text) => {
                            valid = text;
                        });
                    console.log(valid);

                    // Hide loading indication
                    submitButton.removeAttribute('data-kt-indicator');

                    // Enable button
                    submitButton.disabled = false;

                    if (valid === 'Valid') {
                        // Show message popup. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                        Swal.fire({
                            text: "Tạo mới thành công!",
                            icon: "Thành công",
                            buttonsStyling: false,
                            confirmButtonText: "Đồng ý",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then(function (result) {
                            if (result.isConfirmed) {
                                form.submit(); // submit form
                            }
                        });
                    } else {
                        // Show error message popup
                        Swal.fire({
                            text: valid,
                            icon: "Lỗi",
                            buttonsStyling: false,
                            confirmButtonText: "Ok",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                    }
                } else {
                    // Show error popup. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                    Swal.fire({
                        text: "Có lỗi đã xảy ra, vui lòng nhập lại",
                        icon: "Lỗi",
                        buttonsStyling: false,
                        confirmButtonText: "Ok",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    });
                }
            });
        });
    }


    // Public functions
    return {
        // Initialization
        init: function () {
            form = document.querySelector('#add_employee_form');
            submitButton = document.querySelector('#create_submit');

            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTSigninGeneral.init();
});
