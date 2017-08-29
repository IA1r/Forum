$(document).ready(function () {
    GetUsers(null);
});

function GetUsers(isBanned) {
    /// <summary>
    /// Gets the users.
    /// </summary>
    /// <param name="isBanned"> user banned property.</param>

    $.get("/api/AccountAPI/GetUsers", { isBanned: isBanned }, {
    }).success(function (users) {
        $('#user_list_table').append('<tr>' +
                                           '<th>Login</th>' +
                                           '<th>Banned</th>' +
                                        '</tr>');
        $.each(users, function (index, user) {
            $('#user_list_table').append('<tr>' +
                                            '<td>' + '<a href="/Account/UserProfile?login=' + user.Login + '">' + user.Login + '</a></td>' +
                                            '<td>' + user.IsBanned  + '</td>' +
                                         '</tr>');
        });
    });
}

function Check(IsBanned) {
    /// <summary>
    /// Checks the specified banned property.
    /// </summary>
    /// <param name="IsBanned">The banned property.</param>

    if (IsBanned == null) {
        ClearTable();
        GetUsers(IsBanned);
        $("#checkbox_banned").prop("checked", false);
        $("#checkbox_unbanned").prop("checked", false);
    }

    if (IsBanned == true) {
        ClearTable();
        GetUsers(IsBanned);
        $("#checkbox_all").prop("checked", false);
        $("#checkbox_unbanned").prop("checked", false);
    }


    if (IsBanned == false) {
        ClearTable();
        GetUsers(IsBanned);
        $("#checkbox_banned").prop("checked", false);
        $("#checkbox_all").prop("checked", false);
    }

}

function ClearTable() {
    $('#user_list_table tr').remove();
};