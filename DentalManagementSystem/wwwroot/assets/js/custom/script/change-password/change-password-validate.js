"use strict";

// Class definition
var KTSigninGeneral = function () {
    // Elements
    var form;
    var submitButton;
    var validator;

    // Handle
    var handleForm = function (e) {
        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'OldPassword': {
                        validators: {
                            notEmpty: {
                                message: 'Bạn chưa nhập mật khẩu'
                            }
                        }
                    },
                    'NewPassword': {
                        validators: {
                            notEmpty: {
                                message: 'Bạn chưa nhập mật khẩu'
                            }
                        }
                    },
                    'ConfirmPassword': {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng nhập lại mật khẩu'
                            },
                            identical: {
                                compare: function () {
                                    return form.querySelector('[name="NewPassword"]').value;
                                },
                                message: 'Mật khẩu mới không trùng khớp'
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

                        form.submit(); // submit form
                    }, 2000);
                } else {

                }
            });
        });
    }

    // Public functions
    return {
        // Initialization
        init: function () {
            form = document.querySelector('#change-password-form');
            submitButton = document.querySelector('#submit-btn');

            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTSigninGeneral.init();
    let form = document.querySelector('#change-password-form');
    if (form.getAttribute('error-message') !== '' && form.getAttribute('error-message') != null) {
        let errorMessage = form.getAttribute('error-message');
        Swal.fire({
            text: errorMessage,
            icon: "error",
            buttonsStyling: false,
            confirmButtonText: "Đồng ý",
            customClass: {
                confirmButton: "btn btn-primary"
            }
        });
    }

    if (form.getAttribute('success-message') !== '' && form.getAttribute('success-message') != null) {
        let successMessage = form.getAttribute('success-message');
        Swal.fire({
            text: successMessage,
            icon: "success",
            buttonsStyling: false,
            confirmButtonText: "Đồng ý",
            customClass: {
                confirmButton: "btn btn-primary"
            }
        }).then(function (result) {
            if (result.isConfirmed) {
                setTimeout(function () {
                    window.location.href = "/Home";
                }, 500);
            }
        });
    }
});
