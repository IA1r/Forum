function CreateSection() {
    /// <summary>
    /// Creates the section.
    /// </summary>
    /// <param name="name">The section name.</param>  
    
    var section_name = $("#section_name").val();

    $.get("/api/TopicAPI/CreateSection", { name: section_name },
        function (response) {
            window.location.href = response.MainPage;
        }).error(function (response) {
            $("#error_section").text(jQuery.parseJSON(response.responseText).Error);
        });
}

function ShowForm() {

    $("#section_form").show();
}

function HideForm() {
    $("#section_form").hide();
}

function DeleteSection(name) {
    /// <summary>
    /// Deletes the section.
    /// </summary>
    /// <param name="name">The section name.</param>

    $.get('/api/TopicAPI/DeleteSection/', { name: name })
        .success(function () {
            location.reload();
        })
}