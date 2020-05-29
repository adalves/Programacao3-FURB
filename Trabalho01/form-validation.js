jQuery.validator.setDefaults({
    errorElement: 'span',
    errorPlacement: function (error, element) {
        error.addClass('invalid-feedback');
        element.closest('.form-group').append(error);
    },
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    }
});

$(function() {
    var contactForm = $("form[name='contact']");
    contactForm.validate({      
        rules: {
            nameInput: "required",
            emailInput: {
                required: true,
                email: true
            },
            messageInput: "required"
        },
        messages: {
            nameInput: "Please enter your name",
            emailInput: "Please enter a valid email address",
            messageInput: "Please enter your message"
        }
    });

    $('#message-btn').on('click', function() {
        if (contactForm.valid()) {
            alert("Message sent");
        }
    });
});

$(function() {
    var signinForm = $("form[name='signin']");
    signinForm.validate({
        rules: {
            emailInput: {
                required: true,
                email: true
            },
            passwordInput: "required"
        },
        messages: {
            emailInput: "Please enter a valid email address",
            passwordInput: "Please enter your password"
        }
    });

    $('#signin-btn').on('click', function() {
        if (signinForm.valid()) {
            alert("Signed in");
        }
    });
})