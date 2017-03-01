<!doctype html>
<html lang="zh">
<head>
<meta charset="UTF-8">
	<base href="<?= base_url()?>" />
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>login</title>
<link rel="stylesheet" type="text/css" href="assets/css/styles.css">
</head>
<body>
<div class="htmleaf-container">
	<div class="wrapper">
		<div class="container">
			<h1>GoChat</h1>
			
			<form class="form" >
				<input type="text" placeholder="Username" id="username">
				<input type="password" placeholder="Password" id="password">
				<button onclick="checkPW()" type="button" id="login-button">Login</button>
			</form>
		</div>
		
		<!--<ul class="bg-bubbles">-->

		<!--</ul>-->
	</div>
</div>

<script src="assets/js/jquery-2.1.1.min.js" type="text/javascript"></script>
<script>
$('#login-button').click(function (event) {
	event.preventDefault();
	$('form').fadeOut(500);
	$('.wrapper').addClass('form-success');
});

function checkPW()
{
	var checkPwdURL = "/gochat/admin/login_do";
	var username = document.getElementById('username').value;
	var password = document.getElementById('password').value;

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
				window.location.href='http://127.0.0.1/gochat/admin/index';
//				alert("ok");
			}
			else{
				alert("wrong password");
				window.location.href='/gochat/admin/login';
			}
		}
	})
}




</script>

<div style="text-align:center;margin:50px 0; font:normal 14px/24px 'MicroSoft YaHei';color:#000000">

</div>
</body>
</html>