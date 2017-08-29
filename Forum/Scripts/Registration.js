function Register() {
    /// <summary>
    /// User registration at the website.
    /// </summary>


    $("#form0").submit(function (event) {
        event.preventDefault();
        event.stopImmediatePropagation();

        var user_data = $(this).serialize();
        user_data = user_data.replace(new RegExp(/g-recaptcha-response/), "ReCaptchaResponse");

        if ($(this).valid()) {
            $.ajax({
                type: "POST",
                url: "/api/AccountAPI/Register",
                data: user_data,
                success: function (response) {
                    if (response.Success) {
                        window.location.href = response.MainPage;
                    }
                },
                error: function (response) {
                    if (response.responseText.indexOf("user") != -1) {
                        $("#error_login").text(jQuery.parseJSON(response.responseText).Error)
                        ClrearError("#error_login");
                    };
                    if (response.responseText.indexOf("email") != -1) {
                        $("#error_email").text(jQuery.parseJSON(response.responseText).Error)
                        ClrearError("#error_email");
                    };
                    if (response.responseText.indexOf("Captcha") != -1) {
                        $("#error_captcha").text(jQuery.parseJSON(response.responseText).Error)
                        ClrearError("#error_captcha");
                    };
                }
            })
        }
    });
}

function ClrearError(label_id) {
    /// <summary>
    /// Clrears the error.
    /// </summary>
    /// <param name="label_id">The field of error.</param>


    setTimeout(function () {
        $(label_id).text("");
    }, 3000);
}
