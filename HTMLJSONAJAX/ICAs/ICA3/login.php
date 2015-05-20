<?php
require_once 'functions.php';

if (isset($_POST['submit']) && $_POST['submit'] == 'login' &&
        isset($_POST['username']) && strlen($_POST['username']) > 0 &&
        isset($_POST['password']) && strlen($_POST['password']) > 0) {
    $param = array();
    $param['username'] = strip_tags($_POST['username']);
    $param['password'] = strip_tags($_POST['password']);
    $param['response'] = "";
    $param['status'] = false;
    

    $valid = Validate($param);

    if ($valid['status']) {
        $_SESSION['username'] = $valid['username'];
        $_SESSION['userID'] = $valid['userID'];
        header("Location: index.php");
        die();
    }
    $serverResponse = $valid['response'];
}

if ($_POST['submit'] =='logout') {
    session_unset();
    session_destroy();
    header("Location: login.php");
    die();
}
?>


<!DOCTYPE html>
<html>
    <head>
        <title>ICA 03 - Login Page</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <link href='https://fonts.googleapis.com/css?family=Frijole' rel='stylesheet' type='text/css'>
        <link href="Style.css" rel="stylesheet" type="text/css"/>

    </head>
    <body>
        <div class='header'><h2>ICA03/04/05 - Login with SQL - Shawn Hough</h2></div>
        <div class='main'>
            <form action='login.php' method='post'>
                <table id='ica2tbl'>
                    <tbody>
                        <tr>
                            <td>UserName:</td><td><input type='text' placeholder='Supply a username' Name='username'><td>(willy)</td></td>
                        </tr>
                        <tr>
                            <td>Password:</td><td><input type='password' placeholder="Supply your password" Name='password'><td>(wonka)</td></td>
                        </tr>
                        <tr>
                            <td></td><td><input type='submit' value='login' Name='submit' id='Login'></td><td></td>
                        </tr>
                        <tr><td></td><td><input type='submit' value='logout'  Name='submit'></td><td></td></tr>
                    </tbody>
                </table>
            </form>
            <div class='status'>
                <?PHP echo "$serverResponse"; ?>
            </div>
        </div>
        <div class='footer'>
            <a href='../'>&COPY;</a> 2015 ShawnH
        </div>
    </body>
</html>

