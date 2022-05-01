function submitContact() {
    $("#formContact").submit(function (e) {
        e.preventDefault();
        var name = $('#name').val();
        var email = $('#email').val();
        var message = $('#message').val();

        if (name == "" || email == "" || message == "") {
            $("#error_message").show().html("All Fields are Required");
        } else {
            $("#error_message").html("").hide();
            $.ajax({
                type: "POST",
                url: "Contact_Us",
                data: "name=" + name + "&comment=" + comment,
                success: function (data) {
                    $('#success_message').fadeIn().html(data);
                    setTimeout(function () {
                        $('#success_message').fadeOut("slow");
                    }, 2000);

                }
            });
        }
    })
}


function submitForm() {

    var formValidation = validateEmail() && validatePassword();

    if (formValidation) {

        $.ajax({
            url: 'Register',
            data: { username: $('#username').val(), email: $('#email').val(), password: $('#password').val() },
            type: "POST",
            async: false,
            success: function (data) {

                document.getElementById("registerForm").reset();
                if (data.success) {
                    showAlertandFade('#successMessage', 'Account saved successfully.');
                    //window.location.href = '/Account/Login';
                }
                else {
                    showAlertandFade('#errorMessage', data.responseMessage);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //some errror, some show err msg to user and log the error  
                alert(xhr.responseText);
            }
        });
    }
}




function submitEditForm() {

    var formValidation = validateEmail();

    if (formValidation) {

        $.ajax({
            url: 'Edit',
            data: { username: $('#username').val(), email: $('#email').val() },
            type: "POST",
            async: false,
            success: function (data) {

                document.getElementById("registerForm").reset();
                if (data.success) {
                    showAlertandFade('#successMessage', 'Details Updated Successfully.');
                    //window.location.href = '/Account/Login';
                }
                else {
                    showAlertandFade('#errorMessage', data.responseMessage);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //some errror, some show err msg to user and log the error  
                alert(xhr.responseText);
            }
        });
    }
}

function validatePassword() {

    var regex = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;

    if (!regex.test($('#password').val())) {
        showAlertandFade('#errorMessage', 'Password must be a minimum of 8 characters and must include a number, upper, lower and a special character');
        return false;
    }

    if ($('#password').val() != $('#confirm-password').val()) {
        showAlertandFade('#errorMessage', 'Passwords do not match. Please enter again');
        return false;
    }

    return true;
}

function validateEmail() {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    if (!regex.test($('#email').val())) {
        showAlertandFade('#errorMessage', 'Please enter a valid email.');
        return false;
    }

    return true;
}

function showAlertandFade(alertBoxId, responseMessage) {
    $(alertBoxId).removeClass('d-none');
    $(alertBoxId).text(responseMessage);

    // Fade 
    $(alertBoxId).fadeTo(2000, 500).slideUp(500, function () {
        $(alertBoxId).slideUp(500);
    });
}