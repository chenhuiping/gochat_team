$(function () {
    "use strict";

    //alert(friendInfo);
    var leftSendBtn = $('#leftSendBtn');
    var SFButton = $('#SFButton');
    var logout = $('#logout');
    var createGroupChat = $('#createGroupChat');
    var upload_btn=$('#upload-btn');
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
        if (json.type === 'umessage') { // entire message history
            //insert every single message to the chat window
            var message=json.data;
            var checkPwdURL = "/gochat/user/insertUMessage";
            //alert(message);
            var UserName = document.getElementById('getUserName').value;
            $.ajax({
                type: "post",
                url: checkPwdURL,
                dataType: "json",
                data:
                {
                    message:message,
                    UserName:UserName
                },
                success: function (data) {
                    if(data){
                        var id=data.message['ChatId'];
                        var type=data.type['type'];
                        var from =data.message['from'];
                        var userId=$('#getUserId').val();
                        document.getElementById("leftText").value ="";
                        if(type==0)
                        {
                            getMessage(id);
                            if(from!=userId)
                            {
                                alert(from);

                                $('#'+id+' .RedPoint').removeClass('displayNone');
                                //alert($('#'+id+' .RedPoint').attr('class'));
                            }



                        }
                        else{
                            getGroupMessage(id);
                            if(from!=userId)
                            {
                                $('#'+id+' .RedPoint').removeClass('displayNone');
                            }


                        }
                    }
                    else{
                        alert("failed");
                    }
                }
            })
        }

        if (json.type === 'searchFriend') { // entire message history
            //insert every single message to the chat window
            var friend=json.data;

            if(friend.UserId)
            {
                var username,profile,UserId;
                    username = friend.UserName;
                    profile = friend.profile;
                    UserId = friend.UserId;
                    var str="";
                    str +="<div class='searchResult'>";
                    str +=" <img class='avatar friend searchResultImg'  src='"+profile+"'>";
                    str +=" <p class='friend_Name' >"+username+"</p >";
                    str +="</div>";
                    str +="<button type='button' class='addResult' >Add</button>";
                    $("#addNow").html(str);
            }
            else{
                    alert(friend);
            }
            var addResult = $('.addResult');
            addResult.click(function() {
                //var UserName = document.getElementById('getUserName').value;
                var userId = document.getElementById('getUserId').value;
                 //SEND THIS USER'S FRIENDLIST
                var userIdC=userId;
                var str1="friend$"+UserId+"$"+userIdC;
                var FriendId=UserId;
                var str="friend$"+userId+"$"+FriendId;

                //SEND THIS FRIEND'S FRIENDLIST ADD CHATID
                var member=userId+","+FriendId;
                var chatlist="chat$"+member+"$0";
                connection.send(chatlist);
                connection.send(str);
                connection.send(str1);
                //alert(str);
            });
            var checkPwdURL = "/gochat/user/insertUUser";
            var UserName = document.getElementById('getUserName').value;
            $.ajax({
                type: "post",
                url: checkPwdURL,
                dataType: "json",
                data:
                {
                    friend:friend,
                    UserName:UserName
                }
            })

        }

        if (json.type === 'uchat') { // entire message history
            //insert every single message to the chat window
            var uchat=json.data;
            if(uchat['ChatId'])
            {
                var UserName = document.getElementById('getUserName').value;
                //var userId = document.getElementById('getUserId').value;
                //alert(userId);
                var checkPwdURL = "/gochat/user/insertUChat";
                $.ajax({
                    type: "post",
                    url: checkPwdURL,
                    dataType: "json",
                    data:
                    {
                        uchat:uchat,
                        UserName:UserName
                    },
                    success: function (data) {
                        if(data.status){
                            if(data.uchat['type']==1)
                            {
                                alert("succeed");
                                $('.ngdialog').addClass('ngdialog_close');
                            }
                        }
                        else{
                            alert("failed");

                        }
                    }

                })
            }
            else{
                alert(uchat);
            }

            }


        if (json.type === 'ufriend') { // entire message history
            //insert every single message to the chat window
            var ufriend=json.data;
            var UserName = document.getElementById('getUserName').value;
            var userId = document.getElementById('getUserId').value;
            //alert(userId);
            if(userId==ufriend.UserId)
            {
                //alert(userId);
                var checkPwdURL = "/gochat/user/insertUFriend";
                $.ajax({
                    type: "post",
                    url: checkPwdURL,
                    dataType: "json",
                    data:
                    {
                        ufriend:ufriend,
                        UserName:UserName
                    },
                    success: function (data) {
                        if(data.status){
                            //alert()
                            alert("succeed");
                            $('.searchBar').addClass('closeAddFr');
                        }
                        else{
                            alert("failed");

                        }
                    }

                })
            }
        }
    };

    //SEND MESSAGE TO SERVER
    leftSendBtn.click(function() {
        var ChatId = $('.active').attr('id');
        var time=gettime();


        //alert(time);
        var leftText=document.getElementById('leftText').value;

        var str="message$"+ChatId+"$"+time+"$"+leftText+"$"+userId+"$0";
        //alert(str);
        connection.send(str);
    });

    //SEND IMAGE
    upload_btn.click(function(){
//            var file = $("#userfile").val();
//            var FileExt=file.replace(/.+\./,"");   //正则表达式获取后缀
        $('#uploadForm').ajaxForm({
            dataType: 'json',
            success: function (data) {
                alert("succeed");
                var ChatId = $('.active').attr('id');
                var time=gettime();
                var path=data.path;
                var type=data.type;
                //alert(userId);
                var str="message$"+ChatId+"$"+time+"$"+path+"$"+userId+"$"+type;

                connection.send(str);
            }
        });
    });

    //CREATE GROUPCHAT
    createGroupChat.click(function() {
        var userId = document.getElementById('getUserId').value;
        var group = document.getElementsByName("contact");
        var objArray = group.length;
        var apiContentStr="";

        for(var i=0;i<objArray;i++){
            if(group[i].checked == true){
                apiContentStr += group[i].value+",";
            }
        }
        var member = apiContentStr.substring(0, apiContentStr.length - 1);
        //alert(member);
        member=userId+","+member;
        var len=member.split(",");
        //alert(len.length);
        if(len.length<=2)
        {
            alert("please select more than two people");
            return false;
        }

        var str="chat$"+member+"$1";
        //alert(str);
        connection.send(str);
    });

    //ADD FRIEND SEND USERNAME TO SERVER

    SFButton.click(function() {
        //alert(1111);
        var userId = document.getElementById('getUserId').value;
        var FriendName = document.getElementById('searchusername').value;
        //alert(FriendName);

        //var leftText=document.getElementById('leftText').value;
        var str="search$"+userId+"$"+FriendName;
        //alert(leftText);
        connection.send(str);
    });

    //LOUT OUT FUNCTION DELETE LOCAL DATABASE
    logout.click(function() {
        var UserName = document.getElementById('getUserName').value;
        var str="logout$"+UserName;
        var checkPwdURL = "/gochat/user/deleteTable";

        connection.send(str);

        $.ajax({
            type: "post",
            url: checkPwdURL,
            dataType: "json",
            data:
            {
                UserName:UserName
            },
            success: function (data) {
                if(data){
                    window.location.href='/gochat/admin/logintest';
                }
                else{
                    alert('failed');
                }
            }

        });

    });

    function gettime(){
        var mydate = new Date();
        var time,hour,min,year,month,date,second;

        year=mydate.getFullYear();
        //alert(year);
        //GET MONTH
        if(mydate.getMonth()<10)
        {
            month="0"+mydate.getMonth();
        }
        else{
            month=mydate.getMonth();
        }

        //GET DATE
        if(mydate.getDate()<10)
        {
            date="0"+mydate.getDate();
        }
        else{
            date=mydate.getDate();
        }

        //GET HOUR
        if(mydate.getHours()<10)
        {
            hour="0"+mydate.getHours();
        }
        else{
            hour=mydate.getHours();
        }

        //GET MIN
        if(mydate.getMinutes()<10)
        {
            min="0"+mydate.getMinutes();
        }
        else{
            min=mydate.getMinutes();
        }

        //GET SECOND
        if(mydate.getSeconds()<10)
        {
            second="0"+mydate.getSeconds();
        }
        else{
            second=mydate.getSeconds();
        }
        time=date+"/"+month+"/"+year+" "+hour+":"+min+":"+second;
        return time;
    }

    //CREATE CHATID




});