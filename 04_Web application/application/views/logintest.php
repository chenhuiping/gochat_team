
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><head>
    <title>gochat</title>
    <base href="<?= base_url()?>" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!--    <script src="assets/js/jquery-2.1.1.min.js" type="text/javascript"></script>-->
    <link href="assets/css/sendImg.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="assets/js/jquery-1.9.0.min.js"></script>
    <script type="text/javascript" src="assets/images/login.js"></script>
    <link href="assets/css/login2.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="assets/css/style.css" />
<link rel="stylesheet" type="text/css" href="assets/css/chosen.css" />
<link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
<link rel="stylesheet" type="text/css" href="assets/css/bootstrap-fileupload.css" />
    <script src="assets/js/form-samples.js"></script>
    <script src="assets/js/jquery.form.js"></script>
    <script src="assets/js/client.js"></script>
<style>
    .file {
        position: relative;
        display: inline-block;
        background: #2795dc;
        border: 1px solid ;
        border-radius: 4px;
        padding: 1px 6px;
        overflow: hidden;
        color: #fff;
        text-decoration: none;
        text-indent: 0;
        line-height: 20px;
        margin-left: 0px;
        margin-top: 5px;
    }
    .file input {
        position: absolute;
        font-size: 100px;
        right: 0;
        top: 0;
        opacity: 0;
    }
    .file:hover {
        background: #0081c1;
        border-color: #0081c1;
        color: #fff;
        text-decoration: none;
    }

    #aim{
        border: 1px solid;
        background-color:#fff;
        height: 24px;
        width: 150px;
        padding-top: 0px;
        font-family: Arial, Helvetica, sans-serif;
        font-size: 8px;
        padding-left: 10px;
        position: relative;
        margin-top: 0px;
        border: hidden;
    }

    .upload input {
        position: absolute;
        font-size: 100px;
        right: 0;
        top: 0;
        opacity: 0;
    }
    .upload:hover {
        background: #0081c1;
        border-color: #0081c1;
        color: #fff;
        text-decoration: none;
    }
    .upload {
        position: relative;
        display: inline-block;
        background: #2795dc;
        border: 1px solid ;
        border-radius: 4px;
        padding: 1px 6px;
        overflow: hidden;
        color: #fff;
        text-decoration: none;
        text-indent: 0;
        line-height: 20px;
        margin-left:40px;
    }

</style>
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

                        <div style="padding-left:50px;margin-top:20px;"><input type="button"  value="Sign in" style="width:150px;" class="button_blue" id="login_button"/></div>
                    </form>
                </div>

            </div>

        </div>
        <!--sign in-->
    </div>

    <!--sign up-->
    <div class="qlogin" id="qlogin" style="display: none; ">

        <div class="web_login">

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
<!---->
<!--                        <label for="user"  class="input-tips2">portrait：</label>-->
<!--                        <div class="inputOuter2">-->
<!--                            <form method="post" enctype="multipart/form-data" action="/gochat/upload/uploadProfile" id="uploadForm" name="uploadForm" >-->
<!--                                <div class="fileupload fileupload-new" data-provides="fileupload">-->
<!--                        <span class="btn btn-file">-->
<!--                            <span class="fileupload-new">Select file</span>-->
<!--                            <span class="fileupload-exists">Change</span>-->
<!--                            <input type="file" class="default" name="userfile" size="20" id="userfile" />-->
<!--                        </span>-->
<!--                                    <span class="fileupload-preview"></span>-->
<!--                                    <a href="#" class="close fileupload-exists" data-dismiss="fileupload" style="float: none" ></a>-->
<!--                                    <input type="submit" value="upload" name="upload-btn" id="upload-btn"/>-->
<!--                                    <input value="" name="imgPath" id="imgPath"  hidden="hidden"/>-->
<!--                                    <input value="" name="imagePath" id="imagePath" hidden="hidden"/>-->
<!--                                </div>-->
<!--                            </form>-->
<!--                        </div>-->
<!---->
<!--                    </li>-->
                    <li>
                        <div class="inputArea">
                            <input type="button"  style="margin-top:10px;margin-left:85px;" class="button_blue" value="Sign up" id="sign_up"/>
                            <a href="#" class="zcxy" target="_blank"></a>
                        </div>
                    </li>
                    <div class="cl"></div>
                </ul></form>


        </div>


    </div>
    <!--注册end-->
</div>
<script type="text/javascript" src="assets/js/jquery.min.js"></script>
<script type="text/javascript" src="assets/js/jquery.js"></script>
<script src="assets/js/jquery-2.1.1.min.js" type="text/javascript"></script>
<script src="assets/js/jquery.min.js"></script>





<script type="text/javascript">

    //SignUp

    function SignUp()
    {
        var checkPwdURL = "/gochat/user/register";
        var user = document.getElementById('user').value;
        var passwd = document.getElementById('passwd').value;
        var passwd2 = document.getElementById('passwd2').value;
//        alert(user);
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

//显示上传的文件名
//$(document).ready(function(){
//    var file = $('#choosefile'),
//        aim = $('#aim');
//    file.on('change', function( e ){
//        e.currentTarget.files 是一个数组，如果支持多个文件，则需要遍历
//        var name = e.currentTarget.files[0].name;
//        aim.val( name );
//    });
//});
</script>
<!--<div class="jianyi">*推荐使用ie8或以上版本ie浏览器或Chrome内核浏览器访问本站</div>-->
</body></html>