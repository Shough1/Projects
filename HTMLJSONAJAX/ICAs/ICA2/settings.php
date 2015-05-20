<?php
require_once 'functions.php';


if (!isset($_SESSION['username'])) {
    header("Location: login.php");
    die();
}
?>

<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Settings</title>
        <link href="Style.css" rel="stylesheet" type="text/css"/>
        <link href='https://fonts.googleapis.com/css?family=Ubuntu+Condensed|Ranchers&effect=3d' rel='stylesheet' type='text/css'>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
        <style>
            legend{font-weight: bold;} fieldset{width:90%; margin-left: auto; margin-right: auto;} th{font-weight: bold; border: dashed 1px blue; } #userAdd{width:80%; margin-left: auto; margin-right: auto;}
        </style>
    </head>
    <body>
        <div class="header">
            <h1'>ica02- <?php echo "ICA02 - Welcome {$_SESSION['username']} to the settings page" ?></h1>
        </div>
        <div class="main">
            <fieldset>
                <legend><i>Add User:</i></legend>
            <form action='settings.php' method='get'>
                 <table id='ica2tbl'>
                    <tbody>
                        <tr>
                            <td>UserName:</td><td colspan="2"><input type='text' placeholder='Supply a username' Name='username'></td>
                        </tr>
                        <tr>
                            <td>Password:</td><td colspan="2"><input type='text' placeholder="Supply your password" Name='password'></td>
                        </tr>
                        <tr>
                            <td></td><td colspan="2"><input type='submit' value='Add User' Name='add' id='add'></td>
                        </tr>
                    </tbody>
                </table>
            </form>
            </fieldset>
            <table id="userAdd">
                <thead><tr><th style="background-color: lightgreen" >Op</th><th>userID</th><th style="background-color: lightgreen" >UserName</th><th>Encrypted Password</th></tr></thead>
            </table>
            <div class="status"></div> 
        </div>
        <div class="footer"></div>
        
    </body>
</html>

