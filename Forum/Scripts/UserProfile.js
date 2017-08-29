
function UpdateProfile(model) {

    /// <summary>
    /// Updates the profile.
    /// </summary>
    /// <param name="model">The model of user profile.</param>

    $.post("/api/AccountAPI/UpdateProfile", {
        UserID: model.Id(),
        Name: model.Name(),
        Password: model.Password(),
        Email: model.Email(),
        Image: model.NewImage(),
        Country: model.Country(),
        ICQ: model.ICQ(),
        VK: model.VK(),
        Skype: model.Skype()
    },
    function () {
        location.reload();
    }).error(function (response) {
        if (response.responseText.indexOf("email") != -1) {
            $("#error_email").text(jQuery.parseJSON(response.responseText).Error)
        }
        if (response.responseText.indexOf("VK") != -1) {
            $("#error_vk").text(jQuery.parseJSON(response.responseText).Error)
        }
        $("#edit_button").click();
    });  
}

function SubmitBan(id, report) {
    /// <summary>
    /// Submits the ban.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <param name="report">The report.</param>

    $.post("/api/AccountAPI/SubmitBan", {
        UserID: id,
        Report: report
    }, function () {
        location.reload();
    }).error(function (response) {
        $("#error_report").text(jQuery.parseJSON(response.responseText).Error)
    })

}

function SubmitUnBan(id) {
    /// <summary>
    /// Submits the un ban.
    /// </summary>
    /// <param name="id">The user identifier.</param>

    $.post('/api/AccountAPI/SubmitUnBan/' + id, {
    }).done(function () {
        location.reload();
    });

}




