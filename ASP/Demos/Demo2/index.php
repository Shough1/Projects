<?php
require_once 'functions.php';

if(!isset($_SESSION['username']))
{
    $valid['status'] = false;
    header("Location: login.php");
    die();
}
?>

<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Index</title>
        <link href='https://fonts.googleapis.com/css?family=Ubuntu+Condensed|Ranchers&effect=3d' rel='stylesheet' type='text/css'>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
        <style>
            body { background-color: #D1CB07; font-size:x-large; font-family:"Ubuntu Condensed", Verdana, sans-serif; }
            h1,h1 { font-family:"Ranchers", cursive;}
        </style>
    </head>
    <body>
        <div>
            <h1 class='font-effect-3d'>ica02 Demo - Index</h1>
        </div>
        <div>
            <h2><?php echo "Hi {$_SESSION['username']}, welcome to ica02 demo index." ?></h2>
            <form action="login.php" method="post">
               <input type="submit" name="submit" value="logout">
            </form>
            
        </div>
        
    </body>
</html>

