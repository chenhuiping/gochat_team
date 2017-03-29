
<?php include "head.php" ?>
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>login</title>
<link rel="stylesheet" type="text/css" href="assets/css/styles.css">
</head>
<body>
<div id="content" style="background-color: #1f1d1d"></div>
<div class="htmleaf-container">
	<div class="wrapper">
		<div class="container">
			<h1>GoChat</h1>

			<form class="form" id="" >
				<input type="text" placeholder="UserName" id="UserName">
				<input type="password" placeholder="password" id="password">
				<button  type="button"  id="login_button">Login</button>
			</form>
		</div>
		
		<!--<ul class="bg-bubbles">-->

		<!--</ul>-->
	</div>
</div>
<script>
    function checkPW()
    {
        var checkPwdURL = "/gochat/user/webSocket";
        var user = document.getElementById('UserName').value;
        var passwd = document.getElementById('password').value;
    alert(user);
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
//                    window.location.href='/gochat/admin/index';
				alert("ok");
                }
                else{
                    alert("failed");
                    window.location.href='/gochat/admin/logintest';
                }
            }
        })
    }

</script>

</body>
</html>