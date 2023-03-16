"use strict";

// Class definition
var KTSigninGeneralPatients = function () {
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
                    'Email': {
                        validators: {
                            regexp: {
                                regexp: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                                message: 'Email không hợp lệ',
                            },
                            notEmpty: {
                                message: 'Email là bắt buộc',
                            }
                        }
                    },
                    'Phone': {
                        validators: {
                            regexp: {
                                regexp: /^(0|\+84)(\d{9})$/,
                                message: 'số điện thoại không hợp lệ',
                            },
                            notEmpty: {
                                message: 'Số điện thoại là bắt buộc',
                            },
                        }
                    },

                    'Name': {
                        validators: {
                            regexp: {
                                regexp: /^[^0-9]*$/,
                                message: "tên không được chứa số",
                            },
                            callback: {
                                message: 'Tên phải có ít nhất 2 từ',
                                callback: function (input) {
                                    var name = input.value.trim();
                                    var wordCount = name.split(' ').length;
                                    return wordCount >= 2;
                                }
                            }
                        }
                    },

                    'Birthday': {
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

                    'BodyPrehistory': {
                        validators: {
                            callback: {
                                message: 'Trường dữ liệu phải có ít nhất 2 từ',
                                callback: function (input) {
                                    var name = input.value.trim();
                                    var wordCount = name.split(' ').length;
                                    return wordCount >= 2;
                                }
                            },
                        }
                    },

                    'TeethPrehistory': {
                        validators: {
                            callback: {
                                message: 'Trường dữ liệu phải có ít nhất 2 từ',
                                callback: function (input) {
                                    var name = input.value.trim();
                                    var wordCount = name.split(' ').length;
                                    return wordCount >= 2;
                                }
                            },
                        }
                    },

                    'Address': {
                        validators: {
                            callback: {
                                message: 'Trường dữ liệu phải có ít nhất 2 từ',
                                callback: function (input) {
                                    var name = input.value.trim();
                                    var wordCount = name.split(' ').length;
                                    return wordCount >= 2;
                                }
                            },
                        }
                    },

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
       
                    let url = window.location.origin;
                    let valid = 'Valid';
                    
                    // Hide loading indication
                    submitButton.removeAttribute('data-kt-indicator');

                    // Enable button
                    submitButton.disabled = false;

                    if (valid === 'Valid') {
                        // Show message popup. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                        Swal.fire({
                            text: "Thao tác thành công!",
                            icon: "success",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
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
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                    }
                } else {
                    // Show error popup. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                    Swal.fire({
                        text: "Đã có lỗi xảy ra vui lòng thử lại",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
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
            form = document.querySelector('#add_patient_form');
            submitButton = document.querySelector('#create_patient');
            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTSigninGeneralPatients.init();
});

