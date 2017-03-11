
$(function()
{
	
	$('#switch_qlogin').click(function() {
		$('#switch_login').removeClass("switch_btn_focus").addClass('switch_btn');
		$('#switch_qlogin').removeClass("switch_btn").addClass('switch_btn_focus');
		$('#switch_bottom').animate({left:'0px',width:'50px'});
		$('#qlogin').css('display','none');
		$('#web_qr_login').css('display','block');
		
		});
	$('#switch_login').click(function(){
		
		$('#switch_login').removeClass("switch_btn").addClass('switch_btn_focus');
		$('#switch_qlogin').removeClass("switch_btn_focus").addClass('switch_btn');
		$('#switch_bottom').animate({left:'135px',width:'65px'});
		
		$('#qlogin').css('display','block');
		$('#web_qr_login').css('display','none');
		});
if(getParam("a")=='0')
{
	$('#switch_login').trigger('click');
}

});

function upload()
{
	$("div#mydropzone").dropzone({ url: "/js" });
}


function logintab()
{
	scrollTo(0)
	$('#switch_qlogin').removeClass("switch_btn_focus").addClass('switch_btn');
	$('#switch_login').removeClass("switch_btn").addClass('switch_btn_focus');
	$('#switch_bottom').animate({left:'154px',width:'150px'});
	$('#qlogin').css('display','none');
	$('#web_qr_login').css('display','block');
	
}


//根据参数名获得该参数 pname等于想要的参数名 
function getParam(pname)
{
    var params = location.search.substr(1); // 获取参数 平且去掉？ 
    var ArrParam = params.split('&'); 
    if (ArrParam.length == 1) { 
        //只有一个参数的情况 
        return params.split('=')[1]; 
    }
    else {
         //多个参数参数的情况
        for (var i = 0; i < ArrParam.length; i++) {
            if (ArrParam[i].split('=')[0] == pname) {
                return ArrParam[i].split('=')[1]; 
            } 
        } 
    } 
}  


var reMethod = "GET"
		pwdmin = 6 //password min number

//******
$(document).ready(function()
{
// 当登陆页面加载完

	$('#reg').click(function()
	{

		if ($('#user').val() == "")
		{
			//用户名为空，报错时 输入框的改变
			$('#user').focus().css(
					{
				border: "1px solid red",
				boxShadow: "0 0 2px red"
			});
			$('#userCue').html("<font color='red'><b>×Username is empty.</b></font>");
			return false;
		}



		if ($('#user').val().length < 4 || $('#user').val().length > 16)
		{

			$('#user').focus().css({
				border: "1px solid red",
				boxShadow: "0 0 2px red"
			});
			$('#userCue').html("<font color='red'><b>×用户名位4-16字符</b></font>");
			return false;

		}
		//* check the username is allowed or not
		$.ajax(
				{
			type: reMethod,
			url: "/member/ajaxyz.php",
			data: "uid=" + $("#user").val() + '&temp=' + new Date(),
			dataType: 'html',

			success: function(result)
			{

				if (result.length > 2)
				{
					$('#user').focus().css({
						border: "1px solid red",
						boxShadow: "0 0 2px red"
					});$("#userCue").html(result);
					return false;
				}
				else
				{
					$('#user').css({
						border: "1px solid #D7D7D7",
						boxShadow: "none"
					});
				}

			}
		});


		if ($('#passwd').val().length < pwdmin)
		{
			$('#passwd').focus();
			$('#userCue').html("<font color='red'><b>×Passwords should more than " + pwdmin + "</b></font>");
			return false;
		}
		if ($('#passwd2').val() != $('#passwd').val())
		{
			$('#passwd2').focus();
			$('#userCue').html("<font color='red'><b>× the two password are different！</b></font>");
			return false;
		}



			$('#regUser').submit();
	});
	

});