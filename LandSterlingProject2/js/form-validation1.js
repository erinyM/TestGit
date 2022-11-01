

/************************************************/


$(function () {
    $('#registeration').validate({
        rules: {
            name: {
                required: true
            }//,
            //email: {
            //    required: true
            //},
            //PhoneNumber: {
            //    required: true
            //},
            //Password: {
            //    required: true
            //}
        },
        messages: {
            name: {
                required: 'Please Name is required'
            } //,
            //email: {
            //    required: 'Please Email is required'
            //},
            //PhoneNumber: {
            //    required: 'Please Phone Number is required'
            //},
            //password: {
            //    required: 'Please Password is required'
            //}
        },
        //errorPlacement: function (label, element) {
        //    label.addClass('text-danger');
        //    label.insertAfter(element);
        //},
    });
    $("#registeration").removeData("validator");
    $("#registeration").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("#registeration");
});

$(function () {
    $('#SellYourProperty').validate({
        rules: {
            FullName: {
                required: true
            },
             EmailId: {
                 required: true
            },
            MobileNumber: {
                required: true
            }
        },
        messages: {
            FullName: {
                required: 'Please Name is required'
            },
            EmailId: {
                required: 'Please Email is required'
            },
            MobileNumber: {
                required: 'Please Mobile Number is required'
            }
        },
        errorPlacement: function (label, element) {
            label.addClass('text-danger');
            label.insertAfter(element);
        },
    });
   

    $("#SellYourProperty").removeData("validator");
    $("#SellYourProperty").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("#SellYourProperty");
});
$(function () {
    $('#ContactUs').validate({
        rules: {
            FullName: {
                required: true
            },
            EmailId: {
                required: true
            },
            MobileNumber: {
                required: true
            }
        },
        messages: {
            FullName: {
                required: 'Please Name is required'
            },
            EmailId: {
                required: 'Please Email is required'
            },
            MobileNumber: {
                required: 'Please Mobile Number is required'
            }
        },
        errorPlacement: function (label, element) {
            label.addClass('text-danger');
            label.insertAfter(element);
        },
    });


    $("#SellYourProperty").removeData("validator");
    $("#SellYourProperty").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("#SellYourProperty");
});

$(function () {
    $('#Subscribe').validate({
        rules: {
            name: {
                required: true
            },
            email: {
                required: true
            }
        },
        messages: {
            name: {
                required: 'Please Name is required'
            },
            Email: {
                required: 'Please Name is required'
            }
        },
        errorPlacement: function (label, element) {
            label.addClass('text-danger');
            label.insertAfter(element);
        },
    });


    $("#SellYourProperty").removeData("validator");
    $("#SellYourProperty").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("#SellYourProperty");
});
function ShowsuccessSellYProperty(result) {
   
    swal("Thank you! ", "Submitted successfully", "success");
    $("#EmailId").val("");
    $("#FullName").val("");
    $("#MobileNumber").val("");
    $.validator.unobtrusive.parse($(result));
};
function ShowfailSellYProperty(result)
{
    swal("Fail to save!", "Please fix errors", "error");
}
function ShowsuccessRegister(result)
{
    swal("Thank you! ", "Submitted successfully", "success");
    $("#email").val("");
    $("#PhoneNumber").val("");
    $("#con_password").val("");
    $("#password").val("");
    $("#name").val("");
    $.validator.unobtrusive.parse($(result));
}
function ShowsuccessContactUS(result)
{
    swal("Thank you! ", "Submitted successfully", "success");
     $("#name").val("");
    $("#EmailId").val("");
    $("#subject").val("");
    $("#PhoneNumber").val("");
    $("#Message").val("");
    $.validator.unobtrusive.parse($(result));
}
function ShowsuccessSubscribe(result)
{
    swal("Thank you! ", "Submitted successfully", "success");
    $("#name").val("");
    $("#Email").val("");
    $.validator.unobtrusive.parse($(result));
}

function CheckAvailability() {
    
    var email = $("#email").val();
    $.ajax({
        type: "POST",
        url: "/User/IsUserExist",
        data: '{Email: "' + email + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var message = $("#message");
            if (!response) {
                //Username available.
                message.css("color", "green");
                message.html("Email is available");
                $('#submit_registeration').enabled;
                document.getElementById("submit_registeration").disabled = false;
            }
            else {
                //Username not available.
                message.css("color", "red");
                message.html("Email is Exist");
                document.getElementById("submit_registeration").disabled = true;
            }
        }
    });
};

function insert_Success() {
    $.ajax(
        {
            type: "POST",
            url: "/Home/SellYourProperty",
            data: null, // optional data
            success: function (result) {
                swal("Good job!","Successfully inserted", "success");
},
                error: function (req, status, error) {
                    // do something with error   
                }
            });
}
