<?php
require_once 'inc/db.php'; // sqliconnect has completed, and $mysqli is our connection object

if (isset($_POST['action']) && $_POST['action'] == 'show' &&
        isset($_POST['search']) && strlen($_POST['search']) > 0) {
    global $mysqli, $mysqli_response, $mysqli_status; // register the globals

    $filter = $mysqli->real_escape_string($filter);

    $query = "SELECT title_id, title, price ";
    $query.= " FROM titles";
    //$query.= " WHERE title like '%$filter%'"; // wildcard for strings = %

    $jsonData = array();
    if ($result = mysqliQuery($query)) {// Made it here, query was good, we have data
        while ($row = $result->fetch_assoc()) { // get ['title_id'] => title_id_value// for each row, add it to the response json data array
            $jsonData[] = $row;
        }
    }
//  else
//  {
//    $jsonData[] = "Query Error : $query";
//  }
//  header('Content-Type: html/text'); // default
//  header('Content-Type: application/json'); // for json
//  header('Content-Type: application/ajax'); // for json
    echo json_encode($jsonData);
    die(); // DON'T GO ON
}
?>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>ica05DemoSH</title>
        <link href='https://fonts.googleapis.com/css?family=Ubuntu+Condensed|Ranchers&effect=3d' rel='stylesheet' type='text/css'>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>
        <script src="js/ajax.js" type="text/javascript"></script>
        <link href="Style.css" rel="stylesheet" type="text/css"/>
        <script>
            $(document).ready(function() {
                $("#btnShow").click(function(event) {
                    var outData = {};
                    outData['action'] = 'show';
                    outData['search'] = 'x';
                    AjaxPostJson('index.php', outData, HandleResponse);
                });
            });
            function HandleResponse(retData)
            {
                console.log(retData);
                
            }
        </script>
        <style>
            body {  background-color: #bad; font-size:x-large; font-family:"Ubuntu Condensed", Verdana, sans-serif; }
            h1,h1 { font-family:"Ranchers", cursive;}
        </style>
    </head>
    <body>
        <div>
            <!-- Use session -->
            <h1 class='font-effect-3d'>ica05 json php Demo</h1>
        </div>
        <!-- Form -->
        <div><button id="btnShow">Show</button>
            <br/>
            <div id="outDiv">
            </div>
        </div>
        <a href="../">&copy;</a>ShawnH
    </body>
</html>

