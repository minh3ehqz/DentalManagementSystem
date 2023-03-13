"use strict";

// Class definition
var KTSigninGeneral = function() {
    // Elements
    var form;
    var submitButton;
    var validator;

    // Handle
    var handleForm = function(e) {
        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validator = FormValidation.formValidation(
			form,
			{
				fields: {					
                    'email': {
                        validators: {
                            regexp: {
                                regexp: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                                message: 'The value is not a valid email address',
                            },
                            notEmpty: {
                                message: 'Email address is required'
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
                    setTimeout(function() {
                        // Hide loading indication
                        submitButton.removeAttribute('data-kt-indicator');

                        // Enable button
                        submitButton.disabled = false;

                        // Show message popup. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                        Swal.fire({
                            text: "Đã gửi yêu cầu lấy lại mật khẩu, vui lòng chờ trong giây lát",
                            icon: "success",
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
                    }, 2000);   						
                } else {
                    // Show error popup. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                    Swal.fire({
                        text: "Bạn đã nhập thiếu Email",
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
    }

    // Public functions
    return {
        // Initialization
        init: function() {
            form = document.querySelector('#kt_sign_in_form');
            submitButton = document.querySelector('#kt_sign_in_submit');
            
            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTSigninGeneral.init();
    let form = document.querySelector('#kt_sign_in_form');
    if (form.getAttribute('error-message') !== '' && form.getAttribute('error-message') != null) {
        console.log(form.getAttribute('error-message'));
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
        console.log(form.getAttribute('success-message'));
        let successMessage = form.getAttribute('success-message');
        Swal.fire({
            text: successMessage,
            icon: "success",
            buttonsStyling: false,
            confirmButtonText: "Đồng ý",
            customClass: {
                confirmButton: "btn btn-primary"
            }
        });
    }
});
