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
                    'name': {
                        validators: {
                            regexp: {
                                regexp: /[A-Z][a-z]/,
                                message: 'Tên phải có chữ hoa và chữ thường'
                            },
                            notEmpty: {
                                message: 'Chưa nhập tên Service'
                            }
                        }
                    },
                    'unit': {
                        validators:
                            notEmpty: {
                                message: 'Chưa nhập dữ liệu'
                            }
                        }
                    },
                    'marketprice': {
                        validators: {
                            regexp: {
                                regexp: /\d{1,1000}/,
                                message: 'Chưa hợp lệ'
                            },
                            notEmpty: {
                                message: 'Chưa nhập dữ liệu'
                            }
                        }
                    },
                    'price': {
                        validators: {
                            regexp: {
                                regexp: /\d{1,1000}/,
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
                            text: "Tạo mới thành công",
                            icon: "success",
                            buttonsStyling: false,
                            confirmButtonText: "Đồng ý",
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
                        text: "Đã có lỗi xảy ra, vui lòng nhập lại",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Đồng ý",
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
                text: "Bạn có chắc chắn muốn xóa",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Có",
                cancelButtonText: "Không",
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
            form = document.querySelector('#kt_modal_add_service_form');
            submitButton = document.querySelector('#create_submit');

            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTSigninGeneral.init();
});
