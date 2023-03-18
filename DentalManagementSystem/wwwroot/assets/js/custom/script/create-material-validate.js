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
                    'Name': {
                        validators: {
                            regexp: {
                                regexp: /[A-Z][a-z]/,
                                message: 'Tên phải có chữ hoa và chữ thường'
                            },
                            notEmpty: {
                                message: 'Chưa nhập tên'
                            }
                        }
                    },
                    'Unit': {
                        validators: {
                            regexp: {
                                regexp: /[A-Z][a-z]/,
                                message: 'Tên phải có chữ hoa và chữ thường'
                            },
                            notEmpty: {
                                message: 'Chưa nhập dữ liệu'
                            }
                        }
                    },
                    'Amount': {
                        validators: {
                            regexp: {
                                regexp: /^\s*-?[0-9]{1,10}\s*$/,
                                message: 'Chỉ được nhập số nguyên dương'
                            },
                            notEmpty: {
                                message: 'Không được để trống'
                            }

                        }
                    },
                    'Price': {
                        validators: {
                            regexp: {
                                regexp: /^\s*-?[0-9]{1,10}\s*$/,
                                message: 'Chỉ được nhập số nguyên dương'
                            },
                            notEmpty: {
                                message: 'Không được để trống'
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
                 
                    //Validate name of material
                    // Disable button to avoid multiple click 
                    submitButton.disabled = true;
                    var name = document.getElementById("Name").value;                   
                    let url = window.location.origin;
                    let valid = '';
                    await fetch(url + '/Material/checkName?name=' + name).then((response) => response.text())
                        .then((text) => {
                            valid = text;
                        });
                    // Hide loading indication
                    submitButton.removeAttribute('data-kt-indicator');

                    // Enable button
                    submitButton.disabled = false;


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
                            confirmButtonText: "Ok!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then(function (result) {
                            if (result.isConfirmed) {
                                console.log(form)
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
                        text: "Có lỗi, xem lại.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok!",
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
            form = document.querySelector('#kt_modal_add_material');
            submitButton = document.querySelector('#create_material');
            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTSigninGeneral.init();
});
