<?php
require_once 'functions.php';
require_once 'inc/db.php';
global $mysqli, $mysqli_response, $mysqli_status;

if (!isset($_SESSION['username'])) {
    header("Location: login.php");
    die();
}

function Refresh() {
    
    $q = "SELECT prj_messages.msg, prj_users.username, prj_messages.stamp"
                . " FROM  prj_messages"
                . " INNER JOIN prj_users"
                . " ON prj_messages.userID = prj_users.userID"
                . " ORDER BY prj_messages.stamp DESC"
                . " LIMIT 10";


        $tableoutput = array();
        if ($result = mysqliQuery($q)) {

            while ($row = $result->fetch_assoc()) {
                $tableoutput[] = $row;
            }
        }
    else {
        return "Query Error: $q";
    }
    return json_encode($tableoutput);
    //die();
    
}

if(isset($_POST['action']) && $_POST['action']=='refresh')
{
    echo Refresh();
    die();
}

if (isset($_POST['action']) && $_POST['action'] == 'send' && strlen($_POST['msg']) > 0) {
    $uID = $_SESSION['userID'];
    $message = $mysqli->real_escape_string($_POST['msg']);
    $insert = "INSERT INTO prj_messages (userID , msg) VALUES ($uID, '$message')";

    if (mysqliNonQuery($insert) == -1) {
        return "Insertion Failed!";
    } else
        echo Refresh();
    die();
   
}

if (isset($_POST['action']) && $_POST['action'] == 'search' && strlen($_POST['filter']) > 0) {

    $filter = $_POST['filter'];

    $query = "SELECT prj_messages.msg, prj_users.username, prj_messages.stamp"
            . " FROM  prj_messages"
            . " INNER JOIN prj_users"
            . " ON prj_messages.userID = prj_users.userID"
            . " AND (prj_messages.msg like '%$filter%'"
            . " OR prj_users.username like '%$filter%')"
            . " ORDER BY prj_messages.stamp DESC"
            . " LIMIT 10";

    $array = array();
    if ($result = mysqliQuery($query)) {
        while ($row = $result->fetch_assoc()) {
            $array[] = $row;
        }
    }
    echo json_encode($array);
    die();
}
?>


<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Messages</title>
        <link href="Style.css" rel="stylesheet" type="text/css"/>
        <link href='https://fonts.googleapis.com/css?family=Ubuntu+Condensed|Ranchers&effect=3d' rel='stylesheet' type='text/css'>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
        <script src="js/ajax.js" type="text/javascript"></script>
        <script>
            $(document).ready(function() {
                $("#refresh").click(function() {
                    var output = {};
                    output['action'] = 'refresh';
                    AjaxPostJson('messages.php', output, TableOut);
                });
                $("#btnSend").click(function() {
                    var input = {};
                    input['action'] = 'send';
                    input['msg'] = $("#txtMsg").val();
                    AjaxPostJson('messages.php', input, TableOut);
                });
                $("#btnFilter").click(function() {
                    var search = {};
                    search['action'] = 'search';
                    search['filter'] = $("#txtFilter").val();
                    AjaxPostJson('messages.php', search, TableOut);
                });

            });
            function TableOut(retData)
            {

                var tblItems = "";
                for (var i = 0; i < retData.length; i++)
                {
                    tblItems += "<tr>";

                    username = retData[i]['username'];
                    message = retData[i]['msg'];
                    timeStamp = retData[i]['stamp'];

                    tblItems += "<td>" + username + "</td><td>" + timeStamp + "</td><td>" + message + "</td>";

                    tblItems += "</tr>";
                }
                $("#mTblBody").html(tblItems);
            }


        </script>

    </head>
    <body>
        <div class="header">
            <h1><?php echo "ICA05 - Messages {$_SESSION['username']}" ?></h1>
        </div>
        <div class="main">
            <a href="index.php"><button>Back</button></a>
            <table id="msgtbl">
                <tbody>
                    <tr>
                        <td>Message: </td><td colspan="2"><input type="text" placeholder="Message to be sent" name="Message" id="txtMsg"></td>
                    </tr>
                    <tr>
                        <td colspan="3"><button id="btnSend">Send</buton></td>
                    </tr>
                    <tr><td>Filter:</td><td><input type="text" placeholder="username OR message" name="filtertxt" id="txtFilter"></td><td><button id="btnFilter">&#x1f50e;</button></td></tr>
                    <tr><td colspan="3"><button id="refresh">Refresh/Load Table</button></td></tr>
                </tbody>
            </table>

        </div>
        <div>
            <table id="messageTbl">
                <thead>
                    <tr><th>Username</th><th>Time Stamp</th><th>Message</th></tr>
                </thead>
                <tbody id="mTblBody">

                </tbody>

            </table>
        </div>

    </body>
</html>


