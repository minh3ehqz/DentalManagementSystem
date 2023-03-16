"use strict";

// Class definition
var KTSigninGeneralSchedules = function () {
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
                    'Date': {
                        validators: {
                            callback: {
                                message: 'lịch hẹn không hợp lệ ,thời gian thích hợp là từ thứ 2 đến thứ 6 và từ 8 giờ sáng đến 5 giờ chiều',
                                callback: function (input) {
                                    var currentDate = new Date();
                                    var date = new Date(input.value);
                                    var dayOfWeek = date.getDay();
                                    var hours = date.getHours();

                                    var isValidDay = dayOfWeek >= 1 && dayOfWeek <= 5; 
                                    var isValidTime = hours >= 8 && hours < 17; 
                                    var isValidDate = currentDate <= date && date.getFullYear() - currentDate.getFullYear() <= 1; 
                                    return isValidDay && isValidTime  && isValidDate;
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

                    // Hide loading indication
                    submitButton.removeAttribute('data-kt-indicator');

                    // Enable button
                    submitButton.disabled = false;


                    // Show message popup. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                    Swal.fire({
                        text: "Đặt lịch hẹn thành công!",
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
                    // Show error popup. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                    Swal.fire({
                        text: "Đã có lỗi xảy ra vui lòng thử lại.",
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
            form = document.querySelector('#booking_form');
            submitButton = document.querySelector('#booking-btn');
            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTSigninGeneralSchedules.init();
});


