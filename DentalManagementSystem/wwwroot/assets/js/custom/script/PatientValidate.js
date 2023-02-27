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
                    'Email': {
                        validators: {
                            regexp: {
                                regexp: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                                message: 'Email không hợp lệ',
                            },
                            notEmpty: {
                                message: 'Email address is required',
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
                                message: 'Phone is required',
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
                                message: 'Birthday is required'
                            },
                            callback: {
                                message: 'Tuổi phải lớn hơn 0',
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
            validator.validate().then(function (status) {
                if (status == 'Valid') {
                    // Show loading indication
                    submitButton.setAttribute('data-kt-indicator', 'on');

                    // Disable button to avoid multiple click 
                    submitButton.disabled = true;


                    // Simulate ajax request
                    setTimeout(function () {
                        // Hide loading indication
                        submitButton.removeAttribute('data-kt-indicator');

                        // Enable button
                        submitButton.disabled = false;

                        // Show message popup. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                        Swal.fire({
                            text: "Tạo mới thành công!",
                            icon: "success",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then(async function (result) {
                            if (result.isConfirmed) {

                                let url = window.location.origin;
                                let valid = '';
                                await fetch(url + '/Patients/sdfjkhsjkd').then((response) => response.text())
                                    .then((text) => {
                                        valid = text;
                                    });
                                if (valid === 'Valid') {
                                    form.submit(); // submit form
                                }
                                else {
                                    Swal.fire({
                                        text: "Trung me no email roi.",
                                        icon: "error",
                                        buttonsStyling: false,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        }
                                    });
                                    return;
                                }

                            }
                        });
                    }, 2000);
                } else {
                    // Show error popup. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                    Swal.fire({
                        text: "Sorry, looks like there are some errors detected, please try again.",
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
    KTSigninGeneral.init();
});
