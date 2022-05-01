function login() {

        $.ajax({
            url: 'Account/Login',
            data: { email: $('#email').val(), password: $('#password').val() },
            type: "POST",
            async: false,
            success: function (data) {
                //call is successfully completed and we got result in data
                document.getElementById("loginForm").reset();
                if (data.success) {
                    window.location.href = data.redirectUrl;
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

function showAlertandFade(alertBoxId, responseMessage) {
    $(alertBoxId).removeClass('d-none');
    $(alertBoxId).text(responseMessage ?? 'Error while saving the data.');

    // Fade 
    $(alertBoxId).fadeTo(2000, 500).slideUp(500, function () {
        $(alertBoxId).slideUp(500);
    });
}
