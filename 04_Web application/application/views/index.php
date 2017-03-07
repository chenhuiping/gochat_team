<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <base href="<?= base_url()?>" />
    <link rel="stylesheet" type="text/css" href="assets/css/style.css" />
    <title>gochat</title>
    <style>
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
        .dispalyNone{
            display: none;
        }

        .imageSize{
            width:20px;
            height: 20px;
        }

    </style>
</head>
<body>

<div id="chat">
    <div class="sidebar">
        <div class="m-card">
            <header>
                <img class="avatar" width="40" height="40" alt="Coffce" src="<?=$userInfo['profile']?>">
                <p class="name"><?=$userInfo['UserName']?></p>
            </header>
            <footer>
                <input class="search" placeholder="search user...">
            </footer>
        </div>
        <div class="tab">
            <div class="tab_chat" >
                <a class="chat" href="#chatList" >
                    <img class="avatar imageSize" id="chatNow" onclick="changeChat()" src="assets/dist/images/chat1.png">
                </a>
            </div>
            <div class="tab_groupchat">
                <a class="chat" href="#groupList">
                    <img class="avatar imageSize" id="groupChat" onclick="changeGroup()" src="assets/dist/images/groupchat1.png">
                </a>
            </div>
            <div class="tab_friend">
                <a class="chat" href="#friendList">
                    <img class="avatar imageSize" id="friend" onclick="changeFriend()"  src="assets/dist/images/businesscard1.png">
                </a>
            </div>
        </div>
        <div class="m-list" id="chatList">
            <ul id="recentList" >
                <?php foreach($recent as $rl){?>
                    <li onclick="getMessage(<?=$rl['UserId']?>)">
                        <img class="avatar" width="30" height="30" alt="示例介绍" src="<?=$rl['profile']?>">
                        <p class="name"><?=$rl['UserName']?></p>
                    </li>
                <?php }?>

            </ul>
        </div>
        <div class="m-list dispalyNone" id="groupList">
            <ul>
                <li>
                    <img class="avatar" width="30" height="30" alt="示例介绍" src="assets/dist/images/2.png">
                    <p class="name">3</p>
                </li>
                <li>
                    <img class="avatar" width="30" height="30" alt="示例介绍" src="assets/dist/images/3.jpg">
                    <p class="name">4</p>
                </li>
            </ul>
        </div>

        <div class="m-list dispalyNone" id="friendList">
            <ul>
                <?php foreach ($friendInfo as $fri)
                {?>
                <li onclick="getMessage(<?=$fri['UserId']?>)">
                    <img class="avatar" width="30" height="30" alt="示例介绍" src="<?=$fri['profile']?>">
                    <p class="name"><?=$fri['UserName']?></p>
                </li>
               <?php }?>

            </ul>
        </div>
    </div>
    <div class="main">
        <div class="m-message">
            <ul  id="leftContent">
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
        <div class="m-text">
            <div style="height: 33px;padding: 5px 17px;">
                <?php echo form_open_multipart('upload/do_upload/'.$UserName);?>

<!--                <img src="assets/dist/images/file.png" width="20px"/>-->
                    <input type="file"  name="userfile" size="10"/>
<!--                <input type="file" id="file" multiple="multiple" size="1" />-->
<!--                     <img width="20px" src="assets/dist/images/file.png"  style="cursor:hand"/>-->
<!--                    <button onclick="do_upload()">upload</button>-->
                <input type="submit" value="upload">
            </div>
            <textarea placeholder="" id="leftText"></textarea>
        </div>
        <button id="leftSendBtn">send</button>
    </div>
</div>
<!--<script src="assets/dist/vue.js"></script>-->
<!--<script src="assets/dist/main.js"></script>-->
<script type="text/javascript" src="assets/js/jquery.min.js"></script>
<script type="text/javascript" src="assets/js/jquery.js"></script>
<!--<script type="text/javascript" src="js/my.js"></script>-->
<script type ="text/javascript">
    $(document).ready(function(){

        $('li').click(function(){
            $(this).siblings().removeClass('active');
            $(this).addClass('active');

        });


    });
    function changeChat()
    {
        document.getElementById("chatNow").src="assets/dist/images/chat2.png";
        document.getElementById("groupChat").src="assets/dist/images/groupchat1.png";
        document.getElementById("friend").src="assets/dist/images/businesscard1.png";
        $('#chatList').removeClass('dispalyNone');
        $('#groupList').addClass('dispalyNone');
        $('#friendList').addClass('dispalyNone');
    }
    function changeGroup()
    {
        document.getElementById("chatNow").src="assets/dist/images/chat1.png";
        document.getElementById("groupChat").src="assets/dist/images/groupchat2.png";
        document.getElementById("friend").src="assets/dist/images/businesscard1.png";
        $('#chatList').addClass('dispalyNone');
        $('#groupList').removeClass('dispalyNone');
        $('#friendList').addClass('dispalyNone');
    }
    function changeFriend()
    {
        document.getElementById("chatNow").src="assets/dist/images/chat1.png";
        document.getElementById("groupChat").src="assets/dist/images/groupchat1.png";
        document.getElementById("friend").src="assets/dist/images/businesscard2.png";
        $('#chatList').addClass('dispalyNone');
        $('#groupList').addClass('dispalyNone');
        $('#friendList').removeClass('dispalyNone');
    }

    function getMessage(friendId)
    {
        var getMessageURL = "/gochat/admin/getMessage";
        var userId = 1;
        var friendId = friendId;
//            alert(friendId);

        $.ajax({
            type: "post",
            url: getMessageURL,
            dataType: "json",
            data: {
                userId : userId,
                friendId : friendId
            },
            success: function (data) {
                if(data.message){
//                        alert(data.message);
                    var a=data.message;
                    var userId=data.userId;
//                        b=15:00-16:00;
//                        alert(b);
                    str="";
                    for(var i=0;i < a.length;i++)
                    {
                        var time,content,from,type;
//                            alert(a[i]['time']);

                        time = a[i]['time'];
                        content = a[i]['content'];
                        from = a[i]['from'];
                        type = a[i]['type'];



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
                            str = str+"<img class='avatar' width='30' height='30' src='assets/dist/images/1.jpg'>";
                        }
                        else
                        {
                            str = str+"<div class='main'>";
                            str = str+"<img class='avatar' width='30' height='30' src='assets/dist/images/2.png'>";
                        }

                        if(type==0)
                            str = str+"<div class='text'>"+content+"</div></div></li>";
                        if(type==1)
                            str = str+"<img class='avatar' width='100' height='100' src='"+content+"'></div></li>";
                        if(type==2 )
                        {

                            content=content.split("/");
                            content=content[content.length-1];
                            str = str+"<div class='sendFile'><div class='displayInB'>";
                            str = str+"<img src='assets/dist/images/ufile1.png' height='60px'/></div>";
                            str = str+"<div class='cont'><p class='ptitle'>"+content+"</p>";
                            str = str+"<a href='uploads/test.docx' class='dherf'>Download</a></div></div></div></li>";

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

    //上传文件
//    function do_upload()
//    {
//        var getMessageURL = "/gochat/admin/getMessage";
//        var userId = 1;
//        var friendId = friendId;
////            alert(friendId);
//
//        $.ajax({
//            type: "post",
//            url: getMessageURL,
//            dataType: "json",
//            data: {
//                userId : userId,
//                friendId : friendId
//            },
//            success: function (data) {
//                if(){
//                    }
//                }
//        })
//    }

</script>


</body>
</html>
