
<?php
require_once 'util.php';




if (isset($_GET['Name']) && strlen($_GET['Name']) > 0 &&
        isset($_GET['Hobby']) && strlen($_GET['Hobby']) > 0) {
    $name = strip_tags($_GET['Name']);
    $hobbs = strip_tags($_GET['Hobby']);
    $likes = $_GET['HowMuch'];


    $str .= $name;
    for ($i = 0; $i < $likes; $i++) {
        $str .= " really,";
    }
    $str .= " likes $hobbs!";
} elseif (isset($_GET['Name']) && isset($_GET['Hobby'])) {
    $str .= "Sam";
    for ($i = 0; $i < $_GET['HowMuch']; $i++) {
        $str .= " really,";
    }
    $str .= " likes gaming!";
}
?>
<!DOCTYPE html>
<html>
    <head>
        <meta charset="UTF-8">
        <link href="Style.css" rel="stylesheet" type="text/css"/>
        <link href='https://fonts.googleapis.com/css?family=La+Belle+Aurore' rel='stylesheet' type='text/css'>
        <title>ICA01-PHP Intro</title>
    </head>
    <body>
        <h1 class='font-effect-3d'>ica01_php - Shawn Hough</h1>
        <div id='body'>
            <?php
            echo "<label id=pt1>Your IP Address is: {$_SERVER['REMOTE_ADDR']}</label><br>";
            echo "<label id=pt1>Found: " . count($_GET) . " entry in the \$_GET</label><br>";
            echo "<label id=pt1>Found: " . count($_POST) . " entry in the \$_POST</label><br>";
            $count += 1;
            $status .= "$count";
            ?>  

        </div>

        <div id='body'>
            <?php
            $oList = "<ul>";
            foreach ($_GET as $key => $value) {
                $oList .= "<li>[$key] : $value</li>";
            }
            $oList .= "</ul>";
            echo $oList;
            $count += 1;
            $status .= ", $count";
            ?>         


        </div>
        <div id='body'>
            <?php
            echo MakeList(GenerateNumbers());
            ?>
        </div>
        <div id='body'>
            <form method="Get" action="index.php">

                <table id="ICA1Tbl">
                    <tr>
                        <td>Name:</td><td><input type='text' name="Name" id='Name'></td>
                    </tr>
                    <tr>
                        <td>Hobby:</td><td><input type='text' name="Hobby" id='Hobby'></td>
                    </tr>
                    <tr>
                        <td>How Much I Like It:</td><td><input type='range' name='HowMuch' id='HowMuch' max="10" min="1"></td>
                    </tr>
                    <tr>
                        <td colspan="2"><input name="submit" type="submit" value="Go Now"></td>
                    </tr>
                    <tr>
                        <td colspan="2"><label id="lblTarget">
                                <?php
                                echo $str;
                                ?>

                            </label></td>
                    </tr>
                </table> 

            </form>

        </div>
        <a href="../">&COPY;</a> 2015 Coypright by ShawnH<?php echo" Status: $status" ?>
        
    </body>
</html>
