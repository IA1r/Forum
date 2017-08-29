function ChangeSection(id, section) {
    /// <summary>
    /// Changes the section of topic.
    /// </summary>
    /// <param name="id">The topic identifier.</param>
    /// <param name="section">The section of topic.</param>

    $.post("/api/TopicAPI/ChangeSection", {
        TopicID: id,
        Section: section
    }).done(function () {
        location.reload();
    });
}

var TopicID

function ShowForm(id) {
    TopicID = id
    $("#password_form").show();
}

function HideForm() {
    $("#password_form").hide();
    $("#wrong_password").text("");
    $('#password').val("");
}

function Access() {
    /// <summary>
    /// Accesses to topic if password valid.
    /// </summary>

    var password = $('#password').val();
    $.get('/api/TopicAPI/GetTopic/' + TopicID, { password: password }, function (response) {
        if (response.Success) {
            window.location.href = response.RedirectUrl;
        }
        else {
            $("#wrong_password").text("Wrong Password!")
        }
    });
}

function DeleteTopic(id) {
    /// <summary>
    /// Deletes the topic.
    /// </summary>
    /// <param name="id">The topic identifier.</param>

    $.post('/api/TopicAPI/DeleteTopic/' + id, {

    }).success(function () {
        location.reload();
    });
}




function SearchTopic() {
    /// <summary>
    /// Searches the topic by keyword.
    /// </summary>

    var key_word = $("#keyword").val();
    if (key_word != "") {
        $("#required_keyword").text("");
        $('#topic_list').html('');

        $.get('/api/TopicAPI/SearchTopics/', { keyword: key_word },
            function (response) {
                    $.each(response.Topics, function (index, topic) {
                        if (topic.Password != null)
                            $('#topic_list').append('<div> <a href="#" class="decoration_2" onclick="ShowForm(' + topic.ID + ')"> ' + (index + 1) + '. ' + topic.Name + '</a> </div>');
                        else
                            $('#topic_list').append('<div> <a class="decoration_2" href="/Topic/Topic/' + topic.ID + '">' + (index + 1) + '. ' + topic.Name + '</a> </div>');
                    });
            }).error(function (response) {
                $('#topic_list').append(jQuery.parseJSON(response.responseText).Error);
            });
    }
    else {
        $("#required_keyword").text("Required keyword.");
        $('#topic_list').html('');
    }
}

$(document).on("focus", "#keyword", function (e) {
    $("#keyword").val("");
});

$(document).on("focusout", "#keyword", function (e) {
    $("#required_keyword").text("");
    $("#keyword").val("search..");
    $('#topic_list').html('');
});





