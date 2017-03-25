$(function () {
    "use strict";

    var login_button = $('#login_button');
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
            var checkPwdURL = "/gochat/user/createtable";

            $.ajax({
                type: "post",
                url: checkPwdURL,
                dataType: "json",
                data: {
                    UserName: UserName
                }

            });
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

            // NOTE: if you're not sure about the JSON structure
            // check the server source code above
            console.log(json);



                var user = "";
                var friend = "";
                var chat = "";
                var message = "";

                var UserName = document.getElementById('username').value;


                if (json.type === 'user') { // entire message history
                    //insert every single message to the chat window
                    user = json.data;
                    var checkPwdURL = "/gochat/user/insertUser";

                    $.ajax({
                        type: "post",
                        url: checkPwdURL,
                        dataType: "json",
                        data: {
                            user: user,
                            UserName : UserName
                        },
                        success: function (data) {
                            if (data) {
                                var UserId = data.UserId;
                                window.location.href = '/gochat/admin/index/'+UserId;

                            }
                            else {
                                alert("failed");
                                //window.location.href='/gochat/admin/logintest';
                            }
                        }
                    })
                }
                if (json.type === 'friend') { // entire message history
                    //insert every single message to the chat window
                    friend = json.data;
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
                if (json.type === 'chat') { // entire message history
                    //insert every single message to the chat window
                    chat = json.data;
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
                if (json.type === 'message') { // entire message history
                    //insert every single message to the chat window
                    message = json.data;
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
        var user = document.getElementById('user').value;
        var passwd = document.getElementById('passwd').value;
        var passwd2 =document.getElementById('passwd2').value;
        var profile;
        if(passwd!=passwd2)
        {
            alert("The password is different!");
            return false;
        }
        var str ="register$user$"+user+"$"+passwd+"$profile";
        //alert(str);
        //var msg = $(this).val();
        if (!str) {
            return;
        }
        //console.log(str);
        connection.send(str);

    });

    //function Post(URL, PARAMTERS) {
    //    //创建form表单
    //    var temp_form = document.createElement("form");
    //    temp_form.action = URL;
    //    //如需打开新窗口，form的target属性要设置为'_blank'
    //    temp_form.target = "_self";
    //    temp_form.method = "post";
    //    temp_form.style.display = "none";
    //    //添加参数
    //
    //    for (var item in PARAMTERS) {
    //        var opt = document.createElement("textarea");
    //        opt.name = PARAMTERS[item].name;
    //        opt.value = PARAMTERS[item].value;
    //        temp_form.appendChild(opt);
    //    }
    //
    //    document.body.appendChild(temp_form);
    //    //提交数据
    //    temp_form.submit();
    //}



});