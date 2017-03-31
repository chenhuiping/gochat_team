<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><head>
    <title>gochat</title>
    <base href="<?= base_url()?>" />
    <meta http-equiv="Content-Type" content="text/html; charsetutf-8" />

    <link rel="stylesheet" type="text/css" href="assets/css/style.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/chosen.css" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
    <link rel="stylesheet" type="text/css" href="assets/css/bootstrap-fileupload.css" />


    <link href="assets/css/image-crop.css" rel="stylesheet"/>
    <script src="assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="assets/js/jquery.scrollTo.js"></script>

    <script src="assets/js/index.js"></script>
    <title>gochat</title>    <style>

        *, *:before, *:after {
            box-sizing: border-box;
        }
        body, html {
            height: 100%;
            overflow: hidden;
        }
        body, ul {
            margin: 0;
            padding: 0;
        }
        body {
            color: #4d4d4d;
            font: 14px/1.4em 'Helvetica Neue', Helvetica, 'Microsoft Yahei', Arial, sans-serif;
            background: #f5f5f5 url('assets/dist/images/bg.jpg') no-repeat center;
            background-size: cover;
            -webkit-font-smoothing: antialiased;
        }
        ul {
            list-style: none;
        }
        #chat {
            margin: 20px auto;
            width: 800px;
        	height: 600px;
        }

        .sidebar, .main {
            height: 100%;
        }

        /*milo*/
        .sidebar{
            position: relative;
        }

        #file{
            position:absolute;
            z-index:100;
            width:18px;
            opacity:0;
            filter:alpha(opacity=0);
            margin-top:-5px;
        }
        .tab{
            overflow:hidden;
            position:relative;
            border-bottom: 1px solid #24272c;
            padding-bottom: 8px;

        }
        .tab_chat,.tab_friend,.tab_groupchat{
            display:inline-block;
            border-right: 1px solid #24272c;
        }
        .tab_chat,.tab_friend,.tab_groupchat{
            width:30%;
            height:100%;
            text-align:center;
        }
        .displayNone{
            display: none;
        }

        .imageSize{
            width:20px;
            height: 20px;
        }


        /*功能列表显示按钮*/
        .functionOpt{
            display: inline-block;
            position: relative;
            top: 5px;
            left:35px;
        }
        /*添加好友界面*/
        .searchBar{
            display: block;
            position: absolute;
            z-index: 102!important;
            width: 240px;
            height: 320px;
            background-color: white;
            border-color: #c3c3c3;
            -webkit-border-radius: 6px;
        }
        .searchUser{
            position: relative;
            width: 140px;
            height: 24px;
            -webkit-border-radius: 6px;
            border: 0;
        }
        .searchBg{
            background-color: #f9f9f9;
            width:240px;
            height:40px;
            display:inline-block;
            /*-webkit-border-top-left-radius:6px;*/
            /*-webkit-border-top-right-radius: 6px;*/
            position: relative;
            padding-left: 15px;
            padding-top: 8px;
        }

        .closeAddFr{
            display: none;
        }

        .searchResult{
            /*padding: 15px 30px;*/
            border-color: #1f1d1d;
            border-radius: 4px;
            background-color: #ffffff;
            margin-top: 10px;
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 10px;
            width:150px;
        }

        .resultDisplay{
            margin-top: 20px;
        }

        .friend_Name{
            text-align: center;
            color: #1f1d1d;
            margin-top: 10px;
            margin-bottom: 0;
            font-family: sans-serif;
            font-size: 20px;
        }

        .addfriendSucess{
            display: none;
        }

        .searchBar_close{
            position: absolute;
            cursor: pointer;
            top: 5px;
            right: 5px;
        }
        /*功能列表界面*/
        .functionList{
            display: block;
            position: absolute;
            z-index: 102!important;
            width: 150px;
            height: 120px;
            background-color: white;
            border-color: #c3c3c3;
            -webkit-border-radius: 6px;
            left: 35px;
            top: 80px;
            padding-top: 1px;
        }
        .closeFunction{
            display: none;
        }

        .dropdown_menu li {
            text-align: left;
            padding-left: 3px;
        }

        a{
            text-decoration: none;
            color: #000;
        }
        .dropdown_menu li p{
            margin: 10px;
        }
        /*添加好友界面*/
        .ngdialog{
            position: fixed;
            left:0px;
            right: 0px;
            top: 0px;
            bottom: 0px;
            overflow: auto;
            z-index: 10000;
            box-sizing: border-box;
            /*display: none;*/
        }
        .ngdialog_close{
            display: none;
        }
        .ngdialog-overlay{
            background: rgba(0,0,0,.4);
            animation: .1s;
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            padding-top: 20px;


        }
        .ngdialog-content{
            padding: 0;
            overflow: hidden;
            width: 480px;
            height: 600px;
            background: #fff;
            color: #444;
            margin: auto;
            max-width: 100%;
            position: relative;
            min-height: 528px;
            animation: .1s;
            box-sizing: border-box;
            -webkit-border-radius: 4px;
        }

        .title{
            margin-bottom: 0;
            margin-top: 0;
            line-height: 50px;
            background-color: #f9f9f9;
            font-size: 16px;
            font-weight: 400;
            text-align: center;
        }

        .nav_tabs{
            text-align: center;
            font-size: 0;
            -webkit-border-radius: 3px;
            border-bottom: 1px solid #e5e5e5;
            background-color: #f9f9f9;
        }

        ul{
            padding-left: 0;
            list-style-type: none;
        }

        .choose_contact{
            display: inline-block;
            line-height: 37px;
            color: #288525;
            margin-left: 20px;
            margin-right: 20px;
            font-size: 14px;
            position: relative;
        }
        .contact-picker{
            visibility: visible;
            width: auto;
            position: static;
        }
        .selector{
            max-height: 110px;
            overflow-y: auto;
            position: absolute;
            min-height: 60px;
            left: 0;
            right: 0;
            z-index: 999;
            background-color: #fff;
            padding:10px 0 0 20px;
        }
        .input_box{
            margin-bottom: 10px;
            position: relative;
            float: left;
        }
        .input{
            border: 0;
            font-size: 16px;
            width: 5em;
            line-height: 40px;
            height: 40px;
        }
        .addchat_icon{
            display: inline-block;
            vertical-align: middle;
            width: 20px;
            height: 20px;
        }

        .chooser{
            margin: 0 20px;
            padding-top: 60px;
        }
        .contactList{
            position: relative;
            height: 370px;
            overflow-y: scroll;
        }
        .contact_item{
            overflow: hidden;
            padding: 7px 20px;
            cursor: pointer;
            border-bottom: 1px solid #f2f2f2;
            height: 55px;
        }
        .opt{
            float: left;
            margin-right: 10px;
            margin-top:10px;
            height: 40px;
            line-height: 40px;
        }
        .isCheck_user{
            display: inline-block;
            vertical-align: middle;
            width: 20px;
            height: 20px;
            margin: 0;
        }
        .contact_item .avatar{
            float: left;
            margin-right: 10px;
        }

        .img_lazy{
            display: block;
            width: 40px;
            height: 40px;
            -webkit-border-radius: 2px;
        }

        .nickname{
            font-weight: 400;
            font-size: 13px;
            width: 100%;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            word-wrap: normal;
            margin: 0;
        }

        .dialog_ft{
            padding-top: 20px;
            border-top: 1px solid #f1f1f1;
            text-align: center;
            overflow: hidden;
            font-size: 0;
        }
        .button_default{
            display: inline-block;
            border: 1px solid #c1c1c1;
            background-color: #c9c9c9;
            font-size: 14px;
            width: 190px;
            text-align: center;
            line-height: 40px;
            color: #fff;
            text-decoration: none;
            -webkit-border-radius: 4px;

        }
        .ngdialog-close{
            position: absolute;
            cursor: pointer;
            top: 14px;
            right: 14px;
        }

        .addResult{
            position: relative;
            background-color: #3caf36;
            color: white;
            border: 0;
            border-radius: 4px;
            width: 50px;
            height: 25px;
            margin-left: 95px;
            margin-right: 95px;
            font-size: 15px
        }

        .searchResultImg{
            position: relative;
            width: 150px;
            height: 150px;
        }
        .RedPoint{
            width: 15px;
            position: relative;
            right: 0px;
            top: 22%;
        }
        /*.m-list{*/
            /*height:495px;*/
            /*overflow-y: scroll;*/
        /*}*/
    </style>
</head>
<body>

<div id="chat">
    <div class="sidebar">
        <div class="m-card">
            <header>
                <input id="getUserId" value="<?=$userId?>" class="displayNone"/>
                <input id="getUserName" value="<?=$UserName?>" class="displayNone"/>
                <img class="avatar" width="45" height="45" alt="Coffce" src="<?=$userInfo['profile']?>" onclick="CreateChatId()">
                <p class="name"><?=$userInfo['UserName']?></p>
<!--                功能列表按钮-->
                <div class="functionOpt " id="functionOpt">
                    <a class="add" href="admin/index/<?=$UserName?>/<?=$userId?>/#">
                        <img class="functionDisplay" src="assets/dist/images/functions.png" width="20px" height="20px">
                    </a >
                </div>
<!--                功能列表按钮结束-->
            </header>
            <footer>
<!--                <input class="search" placeholder="">-->
            </footer>
        </div>
        <div class="tab">
            <div class="tab_chat" >
                <a class="chat" href="admin/index/<?=$UserName?>/<?=$userId?>/#chatList" >
                    <img class="avatar imageSize" id="chatNow" onclick="changeChat()" src="assets/dist/images/chat1.png">
                </a>
            </div>
            <div class="tab_groupchat">
                <a class="chat" href="admin/index/<?=$UserName?>/<?=$userId?>/#groupList">
                    <img class="avatar imageSize" id="groupChat" onclick="changeGroup()" src="assets/dist/images/groupchat1.png">
                </a>
            </div>
            <div class="tab_friend">
                <a class="chat" href="admin/index/<?=$UserName?>/<?=$userId?>/#friendList">
                    <img class="avatar imageSize" id="friend" onclick="changeFriend()"  src="assets/dist/images/businesscard1.png">
                </a>
            </div>
        </div>
        <div class="m-list" id="chatList">
            <ul id="recentList" >

                <?php if($recent!=NULL){
                foreach($recent as $rl){?>
                    <li onclick="getMessage(<?=$rl['ChatId']?>)" id="<?=$rl['ChatId']?>" name="recent">
                        <img class="avatar" width="30" height="30" alt="示例介绍" src="<?=$rl['profile']?>">
                        <p class="name"><?=$rl['UserName']?></p>
                        <img class="RedPoint displayNone"  src="assets/dist/images/red_dot.png">
                    </li>
                <?php }}?>

            </ul>
        </div>
        <div class="m-list displayNone"  id="groupList">
            <ul>
                <?php if($groupUser!=NULL){
                foreach($groupUser as $group)
                {?>
                    <li onclick="getGroupMessage(<?=$group['ChatId']?>)" id="<?=$group['ChatId']?>" name="group">
                        <img class="avatar" width="30" height="30" alt="示例介绍" src="assets/dist/images/group2.png">
                        <p style="word-wrap:break-word; word-break:break-all;width: 100px;overflow: hidden; " class="name">
                        <?php

                            echo $userInfo['UserName'].".....(".count($group['User']).")";
                        ?>
                        </p>
                        <img class="RedPoint displayNone"  src="assets/dist/images/red_dot.png">
                    </li>
               <?php }
                }?>

            </ul>
        </div>

        <div class="m-list displayNone" id="friendList">
            <ul>
                <?php if(!$friendInfo!="NULL"){

                foreach ($friendInfo as $fri)
                {

                    ?>
                <li onclick="getMessage(<?=$fri['ChatId']?>)" id="<?=$fri['ChatId']?>" name="friend">
                    <img class="avatar" width="30" height="30" alt="示例介绍" src="<?=$fri['profile']?>">
                    <p class="name"><?=$fri['UserName']?></p>
<!--                    <img class="RedPoint displayNone"  src="assets/dist/images/red_dot.png">-->
                </li>
               <?php }}?>

            </ul>
        </div>
        <!--        功能列表界面-->
        <div class="functionList closeFunction " id="functionList">
            <ul class="dropdown_menu">
                <li id="addfriends" style="border-bottom: 1px solid #f1f1f1 ;">
                    <a href="admin/index/<?=$UserName?>/<?=$userId?>/#" >
                        <p>Add Friend</p >
                    </a >
                </li>
                <li id="create_groupchat" style="border-bottom: 1px solid #f1f1f1 ;">
                    <a href="admin/index/<?=$UserName?>/<?=$userId?>/#">
                        <p>Create Group Chat</p >
                    </a >
                </li>
                <li>
                    <a href="admin/index/<?=$UserName?>/<?=$userId?>/#">
                        <p id="logout">Log Out</p >
                    </a >
                </li>
            </ul>
        </div>
        <!--功能列表界面结束-->

        <!--        添加好友界面-->
        <div class="searchBar closeAddFr" id="search_bar" style="top:75px;left:330px">
            <div style="background-color: #f9f9f9;color: black;height: 30px;padding-top: 10px;border-top-left-radius: 6px;border-top-right-radius: 6px;">
                <p style="text-align: center; margin: 0;">Add Friend</p >
            </div>
            <div class="searchBg">
                <input  class="searchUser" placeholder="search friends..." id="searchusername">
                <button type="button" id="SFButton" style="height: 24px;position: relative;background-color: #3caf36;border:0;border-radius:4px;margin-left:14px;color: white;">Search</button>
            </div>
            <div class="resultDisplay" id="addNow">

            </div>
            <div class="searchBar_close">
                <img src="assets/dist/images/close.png" width="20px" height="20px">
<!--                <img src="assets/dist/images/close.png"/>-->
            </div>
        </div>
        <!--        search friend function over-->
    </div>
    <div class="main">
        <div class="m-message" id="m_message">
            <ul  id="leftContent" >
<!--                -->
<!--                <li>-->
<!--                    <p class='time'>-->
<!--                        <span>11：11</span>-->
<!--                    </p>-->
<!--                    <div class='main'>-->
<!--                        <img class='avatar' width='30' height='30' src='assets/dist/images/1.jpg'>-->
<!--                        <div class='sendFile'>-->
<!--                            <div class="displayInB">-->
<!--                                <img src="assets/dist/images/ufile1.png" height="65px"/>-->
<!--                            </div>-->
<!--                            <div class="cont">-->
<!--                                <p class="ptitle">text.docx</p>-->
<!--                                <a href="uploads/test.docx" class="dherf">Download</a>-->
<!--                            </div>-->
<!--                        </div>-->
<!--                    </div>-->
<!--                </li>-->
            </ul>
        </div>
        <div class="m-text displayNone" id="m_text">
            <div style="height: 33px;padding: 5px 17px;">
                <form method="post" enctype="multipart/form-data" action="/gochat/upload/uploadCgPhoto/<?=$UserName?>" id="uploadForm" name="uploadForm" >
                    <div class="fileupload fileupload-new" data-provides="fileupload">
                        <span class="btn btn-file">
                            <span class="fileupload-new">Select file</span>
                            <span class="fileupload-exists">Change</span>
                            <input type="file" class="default" name="userfile" size="20" id="userfile" />
                        </span>
                        <span class="fileupload-preview"></span>
                        <a href="#" class="close fileupload-exists" data-dismiss="fileupload" style="float: none" ></a>
                        <input type="submit" value="upload" name="upload-btn" id="upload-btn"/>
                        <input value="" name="imgPath" id="imgPath"  hidden="hidden"/>
                        <input value="" name="imagePath" id="imagePath" hidden="hidden"/>
                    </div>
                </form>
            </div>
            <textarea placeholder="" id="leftText"></textarea>
        </div>
        <input  type="button" id="leftSendBtn" value="send"/>
    </div>
</div>
<!--发起聊天-->
<div class="ngdialog ngdialog_close">
    <div class="ngdialog-overlay">
        <div class="ngdialog-content">
            <div class="dialog_hd">
                <h3 class="title">Create Chatting room</h3>
            </div>
            <div class="dialog_bd">
                <ul class="nav_tabs">
                    <li class="choose_contact">
                        Choose Contacts
                    </li>
                </ul>
            </div>
            <div class="contact-picker">
                <div class="selector">
                    <div class="input_box">
                        <img class="addchat_icon" src="assets/dist/images/searchicon.png">
                        <input type="text" class="input" placeholder="search">
                    </div>
                </div>
                <div class="chooser">
                    <div class="contactList">
                        <?php if($friendInfo!=NULL){

                            foreach ($friendInfo as $fri)
                            {?>
                                <div class="contact_item">
                                    <div class="opt" >
                                        <input type="checkbox" name="contact" value="<?=$fri['UserId']?>" class="isCheck_user">
                                    </div>
                                    <div class="avatar">
                                        <img class="img_lazy" src="<?=$fri['profile']?>">
                                    </div>
                                    <div class="info" style="overflow: hidden; height: 20px">
                                        <h4 class="nickname"><?=$fri['UserName']?></h4>
                                    </div>
                                </div>
                            <?php }}?>

                    </div>
                </div>
            </div>
            <div class="dialog_ft">
                <a class="button_default" href="admin/index/<?=$UserName?>/<?=$userId?>/#" id="createGroupChat">Confirm</a >
            </div>
            <div class="ngdialog-close">
                <img src="assets/dist/images/close.png">
            </div>
        </div>
    </div>
</div>
<!--<script src="assets/dist/vue.js"></script>-->
<!--<script src="assets/dist/main.js"></script>-->
<script type="text/javascript" src="assets/js/jquery.min.js"></script>
<script type="text/javascript" src="assets/js/jquery.js"></script>
<script src="assets/js/form-samples.js"></script>
<script src="assets/js/jquery.form.js"></script>

<!--<script type="text/javascript" src="js/my.js"></script>-->
<script type ="text/javascript">
    $(document).ready(function(){



        //显示功能列表
        $("#functionOpt").click(function(){
            $("#functionList").toggleClass('closeFunction');

        });

        $('li').click(function(){
            $(this).siblings().removeClass('active');
            $(this).addClass('active');
            $('#m_text').removeClass('displayNone');

        });



        //显示加好友界面

            $("#addfriends").click(function(){
                $(".searchBar").removeClass('closeAddFr');
                $(".functionList").addClass('closeFunction');
            });

        //显示发起聊天界面

            $("#create_groupchat").click(function(){
                $(".ngdialog").removeClass('ngdialog_close');
                $(".functionList").addClass('closeFunction');
            });


        //关闭添加好友界面

            $(".searchBar_close").click(function(){
                $('.searchBar').addClass('closeAddFr');
            });

        //显示发起聊天界面

            $(".ngdialog-close").click(function(){
                $('.ngdialog').addClass('ngdialog_close');
            });

    });


    function changeChat()
    {
        document.getElementById("chatNow").src="assets/dist/images/chat2.png";
        document.getElementById("groupChat").src="assets/dist/images/groupchat1.png";
        document.getElementById("friend").src="assets/dist/images/businesscard1.png";

        $('#leftText').removeClass('displayNone');
        $('#chatList').removeClass('displayNone');
        $('#groupList').addClass('displayNone');
        $('#friendList').addClass('displayNone');
        $('#groupList ul li').removeClass('active');
        $('#friendList ul li').removeClass('active');
    }
    function changeGroup()
    {
        document.getElementById("chatNow").src="assets/dist/images/chat1.png";
        document.getElementById("groupChat").src="assets/dist/images/groupchat2.png";
        document.getElementById("friend").src="assets/dist/images/businesscard1.png";
        $('#chatList ul li').removeClass('active');
        $('#friendList ul li').removeClass('active');
        $('#chatList').addClass('displayNone');
        $('#groupList').removeClass('displayNone');
        $('#friendList').addClass('displayNone');
    }
    function changeFriend()
    {
        document.getElementById("chatNow").src="assets/dist/images/chat1.png";
        document.getElementById("groupChat").src="assets/dist/images/groupchat1.png";
        document.getElementById("friend").src="assets/dist/images/businesscard2.png";
        $('#groupList ul li').removeClass('active');
        $('#chatList ul li').removeClass('active');
        $('#chatList').addClass('displayNone');
        $('#groupList').addClass('displayNone');
        $('#friendList').removeClass('displayNone');
    }

    function searchFriend()
    {
        var searchFriendURL = "/gochat/admin/addfriend_do";
        var username = document.getElementById('searchusername').value;

        $.ajax({
            type: "post",
            url: searchFriendURL,
            dataType: "json",
            data: {
                searchusername : username,
            },
            success: function (data) {
                if(data.addFriend){
                    var username,profile,friendId;
                    username = data.addFriend['UserName'];
                    profile = data.addFriend['profile'];
                    friendId = data.addFriend['UserId'];

                    str="";
                    str +="<div class='searchResult'>";
                    str +=" <img class='avatar friend searchResultImg'  src='"+profile+"'>";
                    str +=" <p class='friend_Name' >"+username+"</p ></div>";
                    str +="<button type='button' onclick='addFriendNow("+friendId+")' class='addResult' >Add</button>";
                    $("#addNow").html(str);
                }
                else{
                    alert("no result");
                }
            }
        })
    }

    //ADDFRIEDN CHECK
    function addFriendNow(friendId)
    {
        var searchFriendURL = "/gochat/admin/addFriendNow";
        var UserId=<?=$userId?>;

        $.ajax({
            type: "post",
            url: searchFriendURL,
            dataType: "json",
            data: {
                UserId : UserId,
                friendId : friendId
            },
            success: function (data) {
                if(data.status){
                    alert('success');
                }
                else{
                    alert("fail");
                }
            }
        })
    }
    //GET CHATING MESSAGE
    function getMessage(chatId)
    {

        var div = document.getElementById('m_message');
        $('#m_message').scrollTop(div.scrollHeight);

        $('#'+chatId+' .RedPoint').addClass('displayNone');
        var getMessageURL = "/gochat/admin/getMessage";
        var userId = <?=$userId?>;
//        alert(userId);
        var UserName = "<?=$UserName?>";

//        var friendId = friendId;
//            alert(friendId);
        $.ajax({
            type: "post",
            url: getMessageURL,
            dataType: "json",
            data: {
                userId : userId,
//                friendId : friendId
                UserName :UserName,
                chatId :chatId

            },
            success: function (data) {
                if(data.message){
//                        alert(data.message);
                    var a=data.message;
                    var userId=data.userId;
                    str="";
                    for(var i=0;i<a.length;i++)
                    {
                        var time,content,from,type,profile;
//                            alert(a[i]['time']);
//                        var strtime,strtime1;
                        time = a[i]['time'].substring(11, 16);
                        content = a[i]['content'];
                        from = a[i]['from'];
                        type = a[i]['type'];
                        profile = a[i]['profile']['profile'];

                        str = str+"<li>";
//                        if(i==0)
//                        {
                            str = str+"<p class='time'><span>"+time+"</span></p>";
//                        }
//                        else
//                        {
//
//                                strtime=time.split(" ");
//                                strtime1 = a[i-1]['time'].split("");
//                                if(strtime[0]==strtiem1[0]) {
//                                    str = str + "<p class='time'><span>" + a[i]['time'].substring(11, 16) + "</span></p>";
//                                }
//                                else{
//                                    str = str+"<p class='time'><span>"+a[i]['time']+"</span></p>";
//                                }
//                        }


                        if(from==userId)
                        {
                            str = str+"<div class='main self'>";
                            str = str+"<img class='avatar' width='30' height='30' src='"+profile+"'>";
                        }
                        else
                        {
                            str = str+"<div class='main'>";
                            str = str+"<img class='avatar' width='30' height='30' src='"+profile+"'>";
                        }

                        if(type==0)
                            str = str+"<div class='text'>"+content+"</div></div></li>";
                        if(type==1)
                            str = str+"<img class='avatar' width='100' height='100' src='"+content+"'></div></li>";
                        if(type==2 )
                        {
                            var content1=content.split("/");
                            content1=content1[content1.length-1];
                            str = str+"<div class='sendFile'><div class='displayInB'>";
                            str = str+"<img src='assets/dist/images/ufile1.png' height='60px'/></div>";
                            str = str+"<div class='cont'><p class='ptitle'>"+content1+"</p>";
                            str = str+"<a href='"+content+"' class='dherf'>Download</a></div></div></div></li>";
                        }
                    }

                    $("#leftContent").html(str);
//                    $("#leftContent").scrollTo('100%');
                }
                else{
                    str="";
                    $("#leftContent").html(str);
                }
            }
        })
    }


    //GET GROUPCHAT MESSAGE
    function getGroupMessage(ChatId)
    {
        var div = document.getElementById('m_message');
        $('#m_message').scrollTop(div.scrollHeight);
        $('#'+ChatId+' .RedPoint').addClass('displayNone');
        var getMessageURL = "/gochat/admin/getGroupMessage";
        var userId = <?=$userId?>;
//        alert(userId);
        var UserName ="<?=$UserName?>";
//        alert(UserName);

        $.ajax({
            type: "post",
            url: getMessageURL,
            dataType: "json",
            data: {
                userId : userId,
                ChatId : ChatId,
                UserName: UserName
            },
            success: function (data) {
                if(data.groupMessage){
//                        alert(data.message);
                    var a=data.groupMessage;
                    var userId=data.userId;
//                        b=15:00-16:00;
//                        alert(b);
                    str="";
                    for(var i=0;i < a.length;i++)
                    {
                        var time,content,from,type,profile;
//
                        time = a[i]['time'];
                        content = a[i]['content'];
                        from = a[i]['from'];
                        type = a[i]['type'];
                        profile = a[i]['profile']['profile'];

                        str = str+"<li>";
                        if(i==0)
                        {
                            str = str+"<p class='time'><span>"+time+"</span></p>";
                        }
                        else if(i!=0 && time-5>a[i-1]['time'])
                        {
                            str = str+"<p class='time'><span>"+time+"</span></p>";
                        }

                        if(from==userId)
                        {
                            str = str+"<div class='main self'>";
                            str = str+"<img class='avatar' width='30' height='30' src='"+profile+"'>";
                        }
                        else
                        {
                            str = str+"<div class='main'>";
                            str = str+"<img class='avatar' width='30' height='30' src='"+profile+"'>";
                        }

                        if(type==0)
                            str = str+"<div class='text'>"+content+"</div></div></li>";
                        if(type==1)
                            str = str+"<img class='avatar' width='100' height='100' src='"+content+"'></div></li>";
                        if(type==2 )
                        {

                            var content1=content.split("/");
                            content1=content1[content1.length-1];
                            str = str+"<div class='sendFile'><div class='displayInB'>";
                            str = str+"<img src='assets/dist/images/ufile1.png' height='60px'/></div>";
                            str = str+"<div class='cont'><p class='ptitle'>"+content1+"</p>";
                            str = str+"<a href='"+content+"' class='dherf'>Download</a></div></div></div></li>";

                        }
                    }
                    $("#leftContent").html(str);
                }
                else{
                    str="";
                    $("#leftContent").html(str);
                }
            }
        })

    }

</script>


</body>
</html>
