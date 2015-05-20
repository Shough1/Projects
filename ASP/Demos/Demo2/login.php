<?php
require_once 'functions.php';
//Now Session has started we have encrypted password

if (isset($_POST['submit']) && $_POST['submit'] == 'login' &&
        isset($_POST['user']) && strlen($_POST['user']) > 0 &&
        isset($_POST['password']) && strlen($_POST['password']) > 0) {
    $params = array();
    $params['username'] = strip_tags($_POST['user']);
    $params['password'] = strip_tags($_POST['password']);
    $params['response'] = ""; //text message 
    $params['status'] = false; //bool indicating success / failure

    $valid = Validate($params);

    if ($valid['status']) { //bool - login successful?
        $username = $params['username'];
        $_SESSION['username'] = $username;
        header("Location: index.php"); //insert header redirect, and quit
        die();
    }
    $PageStatus = $valid['response'];
}
if(isset($_POST['submit']))
{
    $valid['status'] = false;
    session_destroy();
    header("Location: login.php");
   
    die();
}

?>


<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Login</title>
        <link href='https://fonts.googleapis.com/css?family=Ubuntu+Condensed|Ranchers&effect=3d' rel='stylesheet' type='text/css'>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
        <style>
            body { background-color: #bad; font-size:x-large; font-family:"Ubuntu Condensed", Verdana, sans-serif; }
            h1,h1 { font-family:"Ranchers", cursive;}
        </style>
    </head>
    <body>
        <div>
            <h1 class='font-effect-3d'>ica02 Demo - Login</h1>
        </div>
        <div>
            <!-- Make form -->
            <form action="login.php" method="post">
                UserName : <input type="text" name="user" placeholder="Supply a username"><br/>
                Password : <input type="text" name="password" placeholder="Supply your password"><br/>
                <!-- 2 submits -->
                <input type="submit" name="submit" value="login">
                <input type="submit" name="submit" value="logout">
            </form>
        </div>
        <div>
<?php
echo "Encrypted password is: $encrypted_password <br>";
?>
<?php
echo "Response: $PageStatus";
?>
            <!-- info out : probably blank, except for failed login -->
        </div>
    </body>
</html>

