$(function () {
    // It has the name attribute "registration"
    $("form[name='registration']").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
            name: "required",
            email: {
                required: true,
                // Specify that email should be validated
                // by the built-in "email" rule
                email: true
            },
            password: {
                required: true,
                minlength: 5
            }
        },
        // Specify validation error messages
        messages: {
            firstname: "Please enter your firstname",
            lastname: "Please enter your lastname",
            password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long"
            },
            email: "Please enter a valid email address"
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });
});


$('#BTN').click(function () {
    var $formContainer = $('#formContainer');
    var url = $formContainer.attr('data-url');
    $formContainer.load(url, function () {
        var $form = $('#registration');
        $.validator.unobtrusive.parse($form);
        $form.submit(function () {
            var $form = $(this);
            if ($form.valid()) {
                $.ajax({
                    url: url,
                    async: true,
                    type: 'POST',
                    data: JSON.stringify("Your Object or parameter"),
                    beforeSend: function (xhr, opts) {
                    },
                    contentType: 'application/json; charset=utf-8',
                    complete: function () { },
                    success: function (data) {
                        $form.html(data);
                        $form.removeData('validator');
                        $form.removeData('unobtrusiveValidation');
                        $.validator.unobtrusive.parse($form);
                    }
                });
            }
            return false;
        });
    });
    return false;
});