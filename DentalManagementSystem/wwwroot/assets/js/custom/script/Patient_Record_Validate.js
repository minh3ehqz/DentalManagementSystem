"use strict";

// Class definition
var KTSigninGeneral2 = function () {
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
                    'Reason': {
                        validators: {
                            regexp: {
                                regexp: /^[^0-9]*$/,
                                message: 'Vui lòng nhập ký tự chữ cái',
                            },
                        }
                    },
                    'Diagnostic': {
                        validators: {
                            regexp: {
                                regexp: /^[^0-9]*$/,
                                message: 'Vui lòng nhập ký tự chữ cái',
                            },
                        }
                    },

                    'Causal': {
                        validators: {
                            regexp: {
                                regexp: /^[0-9]*$/,
                                message: "Vui lòng nhập ký tự chữ cái",
                            },
                        }
                    },
                    'TreatmentName': {
                        validators: {
                            regexp: {
                                regexp: /^[0-9]*$/,
                                message: "Vui lòng nhập ký tự chữ cái",
                            },
                        }
                    },
                    'MarrowRecord': {
                        validators: {
                            regexp: {
                                regexp: /^[0-9]*$/,
                                message: "Vui lòng nhập ký tự chữ cái",
                            },
                        }
                    },
                    'Debit': {
                        validators: {
                            regexp: {
                                regexp: /^[0-9]*$/,
                                message: "Vui lòng nhập số",
                            },
                        }
                    },
                    'Note': {
                        validators: {
                            regexp: {
                                regexp: /^[0-9]*$/,
                                message: "Vui lòng khhong nhập các ký tự đặc biệt",
                            },
                        }
                    },
                    'Prescription': {
                        validators: {
                            regexp: {
                                regexp: /^[0-9]*$/,
                                message: "Vui lòng khhong nhập các ký tự đặc biệt",
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
            form = document.querySelector('#add_patient_record_form');
            submitButton = document.querySelector('#create_patient_record');
            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTSigninGeneral.init();
});
