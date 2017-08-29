
function UpdateMessage(model) {
    /// <summary>
    /// Updates the message.
    /// </summary>
    /// <param name="model">The model of message.</param>

    $.post("/api/TopicAPI/EditMessage", {
        ID: model.MessageID(),
        Message: model.MessageText()
    });
}

function DeleteMessage(id) {
    /// <summary>
    /// Deletes the message.
    /// </summary>
    /// <param name="id">The message identifier.</param>

    $.post('/api/TopicAPI/DeleteMessage/' + id, {

    }).success(function () {
        location.reload();
    });
}
