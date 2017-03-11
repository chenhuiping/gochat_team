<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><head>
    <title>gochat</title>
    <base href="<?= base_url()?>" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <script type="text/javascript" src="assets/js/jquery-1.9.0.min.js"></script>
    <script type="text/javascript" src="assets/images/login.js"></script>
    <link href="assets/css/login2.css" rel="stylesheet" type="text/css" />
</head>
<body>
<h1>Gochat<sup></sup></h1>

<div class="login" style="margin-top:50px;">

    <div class="header">
        <div class="switch" id="switch"><a class="switch_btn_focus" id="switch_qlogin" href="javascript:void(0);" tabindex="7">Sign in</a>
            <a class="switch_btn" id="switch_login" href="javascript:void(0);" tabindex="8">Sign up</a><div class="switch_bottom" id="switch_bottom" style="position: absolute; width: 64px; left: 0px;"></div>
        </div>
    </div>


    <div class="web_qr_login" id="web_qr_login" style="display: block; height: 235px;">

        <!--sign in-->
        <div class="web_login" id="web_login">


            <div class="login-box">


                <div class="login_form">
                    <form action="" name="loginform" accept-charset="utf-8" id="login_form" class="loginForm" method="post"><input type="hidden" name="did" value="0"/>
                        <input type="hidden" name="to" value="log"/>
                        <div class="uinArea" id="uinArea">
                            <label class="input-tips" for="u">UserName：</label>
                            <div class="inputOuter" id="uArea">

                                <input type="text" id="username"  class="inputstyle"/>
                            </div>
                        </div>
                        <div class="pwdArea" id="pwdArea">
                            <label class="input-tips" for="p">Password：</label>
                            <div class="inputOuter" id="pArea">

                                <input type="password" id="password" name="password" class="inputstyle"/>
                            </div>
                        </div>

                        <div style="padding-left:50px;margin-top:20px;"><input type="button" onclick="checkPW()" value="Sign in" style="width:150px;" class="button_blue"/></div>
                    </form>
                </div>

            </div>

        </div>
        <!--sign in-->
    </div>

    <!--sign up-->
    <div class="qlogin" id="qlogin" style="display: none; ">

        <div class="web_login"><form name="form2" id="regUser" accept-charset="utf-8"  action="" method="post">
                <input type="hidden" name="to" value="reg"/>
                <input type="hidden" name="did" value="0"/>
                <ul class="reg_form" id="reg-ul">
<!--                    <div id="userCue" class="cue">快速注册请注意格式</div>-->
                    <li>

                        <label for="user"  class="input-tips2">UserName：</label>
                        <div class="inputOuter2">
                            <input type="text" id="user" name="user" maxlength="16" class="inputstyle2"/>
                        </div>

                    </li>

                    <li>
                        <label for="passwd" class="input-tips2">Password：</label>
                        <div class="inputOuter2">
                            <input type="password" id="passwd"  name="passwd" maxlength="16" class="inputstyle2"/>
                        </div>

                    </li>
                    <li>
                        <label for="passwd2" class="input-tips2">rePassword：</label>
                        <div class="inputOuter2">
                            <input type="password" id="passwd2" name="" maxlength="16" class="inputstyle2" />
                        </div>

                    </li>

<!--                    <li>-->
<!--                        <label for="qq" class="input-tips2">telephone：</label>-->
<!--                        <div class="inputOuter2">-->
<!---->
<!--                            <input type="text" id="telephone" name="telephone" maxlength="10" class="inputstyle2"/>-->
<!--                        </div>-->
<!---->
<!--                    </li>-->

                    <li>

                        <label for="user"  class="input-tips2">portrait：</label>
                        <div class="inputOuter2">
                            <input type="file">
                            <input type="submit" value="upload"/>
                        </div>

                    </li>

                    <li>
                        <div class="inputArea">
                            <input type="button" onclick="SignUp()" style="margin-top:10px;margin-left:85px;" class="button_blue" value="Sign up"/>
                            <a href="#" class="zcxy" target="_blank"></a>
                        </div>

                    </li><div class="cl"></div>
                </ul></form>


        </div>


    </div>
    <!--注册end-->
</div>
<script src="assets/js/jquery-2.1.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    function checkPW()
    {
        var checkPwdURL = "/gochat/user/login_do";
        var username = document.getElementById('username').value;
        var password = document.getElementById('password').value;
//        alert(username);
//        alert(password);

        $.ajax({
            type: "post",
            url: checkPwdURL,
            dataType: "json",
            data: {
                username : username,
                password : password
            },
            success: function (data) {
                if(data.status){
                    window.location.href='/gochat/admin/index';
//				alert("ok");
                }
                else{
                    alert("failed");
                    window.location.href='/gochat/admin/logintest';
                }
            }
        })
    }

    //SignUp

    function SignUp()
    {
        var checkPwdURL = "/gochat/user/register";
        var user = document.getElementById('user').value;
        var passwd = document.getElementById('passwd').value;
        var passwd2 = document.getElementById('passwd2').value;
        alert(user);
        if(passwd!=passwd2)
        {
            alert("The password is different!");
            return false;
        }

        $.ajax({
            type: "post",
            url: checkPwdURL,
            dataType: "json",
            data: {
                user : user,
                passwd : passwd,

            },
            success: function (data) {
                if(data.status){
                    window.location.href='/gochat/admin/index';
//				alert("ok");
                }
                else{
                    alert("failed");
                    window.location.href='/gochat/admin/logintest';
                }
            }
        })
    }
</script>
<!--<div class="jianyi">*推荐使用ie8或以上版本ie浏览器或Chrome内核浏览器访问本站</div>-->
</body></html>