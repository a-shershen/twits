function deleteMessage(messageId)
{
    //
}

function makeRepost(username, messageId)
{
    var action = "/User/MakeRepost?username=" + username + "&id=" + messageId;
    
    $.ajax({
        type: "GET",
        url: action,
        fail: alert("Возникла ошибка! \n\n Возможно, вы пытаетесь ретвитнуть: \n - сами себя; \n - уже ретвитнутое сообщение.")
    });
}