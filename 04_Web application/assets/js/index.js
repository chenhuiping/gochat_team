$(function () {
    "use strict";

    var leftSendBtn = $('#leftSendBtn');
    var logout = $('#logout');
    //var leftText = $('#leftText');
    // for better performance - to avoid searching in DOM
    var connection;
    var userId=$('#getUserId').val();
//alert(userId);

    window.WebSocket = window.WebSocket || window.MozWebSocket;

    // if browser doesn't support WebSocket, just show some notification and exit
    if (!window.WebSocket) {
        content.html($('<p>', { text: 'Sorry, but your browser doesn\'t '
        + 'support WebSockets.'} ));
        input.hide();
        $('span').hide();
        return;
    }

    // open connection
    connection = new WebSocket('ws://127.0.0.1:1337');

    connection.onopen = function () {

    };

    connection.onerror = function (error) {
        // just in there were some problems with conenction...
        content.html($('<p>', { text: 'Sorry, but there\'s some problem with your '
        + 'connection or the server is down.' } ));
    };

    // most important part - incoming messages
    connection.onmessage = function (message) {
        // try to parse JSON message. Because we know that the server always returns
        // JSON this should work without any problem but we should make sure that
        // the massage is not chunked or otherwise damaged.
        try {
            var json = JSON.parse(message.data);
        } catch (e) {
            console.log('This doesn\'t look like a valid JSON: ', message.data);
            return;
        }
        console.log(json);
        if (json.type === 'message') { // entire message history
            //insert every single message to the chat window
            var message=json.data;
            var checkPwdURL = "/gochat/user/insertUMessage";

            $.ajax({
                type: "post",
                url: checkPwdURL,
                dataType: "json",
                data:
                {
                    message:message
                },
                success: function (data) {
                    if(data.status){
                        var str='';
                        document.getElementById("leftText").value = "";
				//alert("ok");
                    }
                    else{
                        alert("failed");

                    }
                }

            })
        }


    };

    //SEND MESSAGE TO SERVER
    leftSendBtn.click(function() {
        var ChatId = $('.active').attr('id');
        //alert(ChatId);
        var mydate = new Date();
        var time,hour,min;
        if(mydate.getHours()<10)
        {
            hour="0"+mydate.getHours();

        }
        else{
            hour=mydate.getHours();
        }
        if(mydate.getMinutes()<10)
        {
            min="0"+mydate.getMinutes();

        }
        else{
            min=mydate.getMinutes();
        }
        time=hour+":"+min;

        //alert(time);
        var leftText=document.getElementById('leftText').value;
        var str="message$"+ChatId+"$"+time+"$"+leftText+"$"+userId+"$0";
        //alert(leftText);
        connection.send(str);
    });

    //LOUT OUT FUNCTION DELETE LOCAL DATABASE
    logout.click(function() {
        var checkPwdURL = "/gochat/user/deleteTable";

        $.ajax({
            type: "post",
            url: checkPwdURL,
            dataType: "json",
            data:
            {
                userId:userId
            },
            success: function (data) {
                if(data.status1 && data.status2 && data.status3 && data.status4){
                    window.location.href='/gochat/admin/logintest';
                }
                else{
                    alert('failed');
                }
            }

        });
        var str="logout"
        connection.send(str);
    });

});