﻿"use strict";

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
                    'PatientRecordId': {
                        validators: {
                            regexp: {
                                regexp: /^[1-9]\d*$/,
                                message: 'Chỉ được nhập số nguyên dương'
                            },
                            notEmpty: {
                                message: 'Không được để trống'
                            }
                            
                        }
                    },
                    'MaterialId': {
                        validators: {
                            regexp: {
                                regexp: /^[1-9]\d*$/,
                                message: 'Chỉ được nhập số nguyên dương'
                            },
                            notEmpty: {
                                message: 'Không được để trống'
                            }
                        }
                    },
                    'Amount': {
                        validators: {
                            regexp: {
                                regexp: /^[1-9]\d*$/,
                                message: 'Chỉ được nhập số nguyên dương'
                            },
                            notEmpty: {
                                message: 'Không được để trống'
                            }
                        }
                    },
                    'TotalPrice': {
                        validators: {
                            regexp: {
                                regexp: /^[1-9]\d*$/,
                                message: 'Chỉ được nhập số nguyên dương'
                            },
                            notEmpty: {
                                message: 'Không được để trống'
                            }
                        }
                    }
                    'Date': {
                        validators: {
                            notEmpty: {
                                message: 'Ngày tháng năm sinh là bắt buộc'
                            },
                            callback: {
                                message: 'Ngày tháng năm sinh không hợp lệ',
                                callback: function (input) {
                                    var currentDate = new Date();
                                    var date = new Date(input.value);
                                    return (date <= currentDate);
                                }
                            }
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
                            text: "Thành công!",
                            icon: "success",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then(function (result) {
                            if (result.isConfirmed) {
                                form.submit(); // submit form
                                var redirectUrl = form.getAttribute('data-kt-redirect-url');
                                if (redirectUrl) {
                                    location.href = redirectUrl;
                                }
                            }
                        });
                    }, 2000);
                } else {
                    // Show error popup. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                    Swal.fire({
                        text: "Có lỗi, hãy xem lại.",
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
        const closeButton = element.querySelector('[data-kt-users-modal-action="close"]');
        closeButton.addEventListener('click', e => {
            e.preventDefault();

            Swal.fire({
                text: "Are you sure you would like to cancel?",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Yes, cancel it!",
                cancelButtonText: "No, return",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-active-light"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form			
                    modal.hide();
                } else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "Your form has not been cancelled!.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",
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
            form = document.querySelector('#kt_modal_add_export');
            submitButton = document.querySelector('#create_submit_1');

            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTSigninGeneral.init();
});
