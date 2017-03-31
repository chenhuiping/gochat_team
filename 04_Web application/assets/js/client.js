$(function () {
    "use strict";

    var login_button = $('#login_button');
    var uploadProfile= $('#uploadProfile');
    //var content =$('#content');
    var sign_up=$('#sign_up');

    // for better performance - to avoid searching in DOM
    var connection;


    login_button.click(function() {

        var UserName = document.getElementById('username').value;
        var password = document.getElementById('password').value;
        //alert(UserName);
        //if user is running mozilla then use it's built-in WebSocket
        window.WebSocket = window.WebSocket || window.MozWebSocket;
        connection = new WebSocket('ws://127.0.0.1:1337');
        connection.onopen = function () {
            var str =UserName+","+password;
            //alert(str);
            //var msg = $(this).val();
            if (!str) {
                return;
            }
            //console.log(str);

            connection.send(str);

        };

        connection.onerror = function (error) {
            // just in there were some problems with conenction...
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

            // NOTE: if you're not sure about the JSON structure
            // check the server source code above

                var user = "";
                var friend = "";
                var chat = "";
                var message = "";

                var UserName = document.getElementById('username').value;

                if(json.type ==='logincheck')
                {
                    var logincheck = json.data;
                    if(logincheck !="wrong password")
                    {
                        var checkPwdURL = "/gochat/user/createtable";
                        $.ajax({
                            type: "post",
                            url: checkPwdURL,
                            dataType: "json",
                            data: {
                                UserName: UserName
                            }
                        })
                    }
                    else
                    {
                        alert(logincheck);
                    }

                }

                if(json.type === 'user') {
                    // entire message history
                    //insert every single message to the chat window
                    user = json.data;
                    //alert(user.length);
                    if (user[0]['UserId']){
                        var checkPwdURL = "/gochat/user/insertUser";

                    $.ajax({
                        type: "post",
                        url: checkPwdURL,
                        dataType: "json",
                        data: {
                            user: user,
                            UserName: UserName
                        },
                        success: function (data) {
                            if (data.status) {
                                //alert(11);
                                var UserId = data.UserId;
                                var UserName = data.UserName;
                                window.location.href = '/gochat/admin/index/' + UserName + '/' + UserId;
                            }
                            else {
                                alert("failed");
                                //window.location.href='/gochat/admin/logintest';
                            }
                        }
                    });
                }
                }
                if (json.type === 'friend') {
                    // entire message history
                    //insert every single message to the chat window
                    friend = json.data;
                    //alert(friend.length);
                    if(friend['UserId'])
                    {
                        var checkPwdURL = "/gochat/user/insertFriend";

                        $.ajax({
                            type: "post",
                            url: checkPwdURL,
                            dataType: "json",
                            data: {
                                friend: friend,
                                UserName : UserName
                            }
                        });
                    }

                }
                if (json.type === 'chat') {
                    // entire message history
                    //insert every single message to the chat window
                    chat = json.data;
                    //alert(chat.length);
                    if(chat[0]['ChatId']>0)
                    {

                    var checkPwdURL = "/gochat/user/insertChat";

                    $.ajax({
                        type: "post",
                        url: checkPwdURL,
                        dataType: "json",
                        data: {
                            chat: chat,
                            UserName : UserName
                        }
                    });

                    }
                }
                if (json.type === 'message') {
                    // entire message history
                    //insert every single message to the chat window
                    //alert(message.length);
                    message = json.data;
                    if(message[0]['Id'])
                    {

                    var checkPwdURL = "/gochat/user/insertMessage";

                    $.ajax({
                        type: "post",
                        url: checkPwdURL,
                        dataType: "json",
                        data: {
                            message: message,
                            UserName : UserName
                        }
                    });
                    }
                }
                if (json.type === 'register') { // entire message history
                    //insert every single message to the chat window
                    if (json.data == "right") {
                        window.location.href = '/gochat/admin/logintest';
                        alert("success");
                    }
                }

        };
    });

    sign_up.click(function() {
        window.WebSocket = window.WebSocket || window.MozWebSocket;
        connection = new WebSocket('ws://127.0.0.1:1337');
        connection.onopen = function () {
            var user = document.getElementById('user').value;
            var passwd = document.getElementById('passwd').value;
            var passwd2 =document.getElementById('passwd2').value;

            if(user=="")
            {
                alert("please fill in username.");
                return false;
            }
            if(passwd=="")
            {
                alert("please fill in password.");
                return false;
            }
            if(passwd!=passwd2)
            {
                alert("The password is different!");
                return false;
            }
            var profile="uploads/profile/profile.png";
            var str1="register$"+user+"$"+passwd+"$"+profile;
            alert(str1);
            connection.send(str1);

        };

        connection.onerror = function (error) {
            // just in there were some problems with conenction...
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

            // NOTE: if you're not sure about the JSON structure
            // check the server source code above
            if (json.type === 'register') { // entire message history
                //insert every single message to the chat window
                if (json.data == "succeed") {
                    window.location.href = '/gochat/admin/logintest';
                    alert("succeed");
                }
            }

        };

    });
    //uploadProfile.click(function(){

        //alert(11);
        //var user = document.getElementById('user').value;
        //var passwd = document.getElementById('passwd').value;
        //var passwd2 =document.getElementById('passwd2').value;
        //
        //if(user=="")
        //{
        //    alert("please fill in username.");
        //    return false;
        //}
        //if(passwd=="")
        //{
        //    alert("please fill in password.");
        //    return false;
        //}
        //if(passwd!=passwd2)
        //{
        //    alert("The password is different!");
        //    return false;
        //}
        //$('#uploadPForm').ajaxForm({
        //    dataType: 'json',
        //    success: function (data) {
        //        alert("succeed");
        //        var profile=data.path;
        //        alert(profile);
        //        var str ="register$user$"+user+"$"+passwd+"$"+profile;
        //        alert(profile);
                //connection.send(str);
            //}
        //});
    //});





});