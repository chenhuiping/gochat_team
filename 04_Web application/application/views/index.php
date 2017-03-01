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
            <ul >
                <li>
                    <img class="avatar" width="30" height="30" alt="示例介绍" src="assets/dist/images/2.png">
                    <p class="name">1</p>
                </li>
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
                <li>
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
                <li>
                    <p class="time">
                        <span>22:05</span>
                    </p>
                    <div class="main">
                        <img class="avatar" width="30" height="30" src="assets/dist/images/2.png">
                        <div class="text">
                            "Hello"
                        </div>
                    </div>
                </li>
                <li>
                    <p class="time">
                        <span>22:20</span>
                    </p>
                    <div class="main">
                        <img class="avatar" width="30" height="30" src="assets/dist/images/2.png">
                        <div class="text">
                            my name is peggy.
                        </div>
                    </div>
                </li>
                <li>
                    <p class="time">
                        <span>22:40</span>
                    </p>
                    <div class="main self">
                        <img class="avatar" width="30" height="30" src="assets/dist/images/1.jpg">
                        <div class="text">
                            hi peggy
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        <div class="m-text">
            <div style="height: 25px;padding: 5px 17px;">
                <?php echo form_open_multipart('admin/do_upload');?>
<!--                <input type="file" id="file" multiple="multiple" size="1" />-->
                <img src="assets/dist/images/file.png" width="20px"/>
                    <input type="file"  name="userfile" size="10" />
                    <input type="submit" value="upload" />
            </div>
            <textarea placeholder="" id="leftText"></textarea>
        </div>
        <button id="leftSendBtn">send</button>
    </div>
</div>
<!--<script src="assets/dist/vue.js"></script>-->
<!--<script src="assets/dist/main.js"></script>-->
<script type="text/javascript" src="assets/js/jquery.min.js"></script>
<!--<script type="text/javascript" src="js/my.js"></script>-->
<script type ="text/javascript">
    $(document).ready(function(){

        $('li').click(function(){
            $(this).siblings().removeClass('active');
            $(this).addClass('active');
            var getMessageURL = "/gochat/admin/getMessage";
            var userId = 1;
            var friendId = 2;

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
                        alert("11");
                    }
                    else{
                        alert("2222");
                    }
                }
            })
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

</script>


</body>
</html>
