<?php

require_once 'utl.php';

$me = "Shawn";  // global scope --> global variable, accessible everywhere

function ShowArray($arr) {

    global $me;
    $outlist = "$me says : <br><ul>";
    foreach ($arr as $key => $value) {
        $outlist .= "<li>$key : $value</li>";
    }
    $outlist .="</ul>";
    return $outlist;
}

//check for form input vars from either GET or POST
if (isset($_GET['name']) && strlen($_GET['name'] > 0)) {
    $me = $_GET['name'];
}
?>
<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8" />
        <title>ica01Demo</title>
        <link href='https://fonts.googleapis.com/css?family=Ubuntu|Ubuntu+Mono|Ranchers&effect=3d' rel='stylesheet' type='text/css'>
        <link href="Style.css" rel="stylesheet" type="text/css"/>
        <style>
            body { font-size:x-large; font-family:"Ubuntu", Verdana, sans-serif; }
            h1,h1 { font-family:"Ranchers", cursive;}
            .code { font-family:"Ubuntu Mono", Consolas, monospace;}
        </style>
    </head>
<?php
$me .= "ie";
?>
    <body>
        <div>
            <header>
                <h1 class='font-effect-3d'>ica01Demo</h1>
            </header>

            <div class="content">
<?php
echo "Hi Man you are: $me<br>";
echo " Your IP address is : {$_SERVER['REMOTE_ADDR']} and has " . count($_SERVER) . " entries<br>";
echo "Found : " . count($_GET) . " entries for \$_GET<br>";
?>
                <hr>
                <?php
                $outlist = "<ul>";
                foreach ($_GET as $key => $value) {
                    $outlist .= "<li>$key : $value</li>";
                }
                $outlist .="</ul>";
                echo $outlist . "<br>";
                ?>
                <hr>
                <?php
                echo ShowArray($_ENV) . "<br>";
                echo ShowArray(MakeArr()) . "<br>";
                ?>
                <br/>
                <a href="form.php">Link to Part II, Form Processing</a>
                <br/>
            </div>
            <footer>
                <p>
                    <a href='../'>&copy;</a> Copyright  by Shawnh
                </p>
            </footer>
        </div>
    </body>
</html>
