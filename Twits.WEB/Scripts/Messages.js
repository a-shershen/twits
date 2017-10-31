function deleteMessage(messageId, user)
{
    if(confirm("Вы уыерены, что хотите удалить сообщение?"))
    {
        var action = "/User/DeleteMessage?id=" + messageId;

        $.ajax({
            type: "GET",
            url: action,
            error: function () {
                alert("Возникла ошибка при удалении сообщения!");
            },
            success: function (data) {
                $("#showContent").load("/User/UserMessages");
            }
        });
    }
}

function makeRepost(messageId)
{
    var action = "/User/MakeRepost?id=" + messageId;
    
    $.ajax({
        type: "GET",
        url: action,
        error:
        function () {
            alert("Возникла ошибка! \n\n Возможно, вы пытаетесь ретвитнуть: \n - сами себя; \n - уже ретвитнутое сообщение.");
        }
        ,
        success: function () {
            window.location.reload(true);
        }
    });
}