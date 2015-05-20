<?php
require_once 'functions.php';
require_once 'inc/db.php';

if (!isset($_SESSION['username'])) {
    header("Location: login.php");
    die();
}

if (isset($_POST['action']) && $_POST['action'] == 'search') {
    $filter = $_POST['filter'];

    $q = "SELECT tagID"
            . " FROM prj_tags"
            . " WHERE tagID like '%$filter%'";

    $search = array();
    if ($result = mysqliQuery($q)) {
        while ($row = $result->fetch_assoc()) {
            $search[] = $row;
        }
    } else {
        return $info = $q;
    }
    echo json_encode($search);
    die();
}
if (isset($_POST['action']) && $_POST['action'] == 'refresh') {

    $value = $_POST['values'];
    $valueFilter = "";

    foreach ($value as &$res) {
        $valueFilter .= " OR tagID = '$res'";
    }
    $query = "SELECT tagID,tagMin,tagMax"
            . " FROM prj_tags"
            . " WHERE tagID = '$value[0]'$valueFilter";
    $ret = array();
    if ($result = mysqliQuery($query)) {
        while ($row = $result->fetch_assoc()) {

            $oLimit = rand(0, 10);

            if ($oLimit == 5) {
                $row['value'] = $row['tagMax'] + rand(5,100);
                $ret[] = $row;
            } elseif ($oLimit == 3) {
                $row['value'] = $row['tagMin'] - rand(5,100);
                $ret[] = $row;
            } else {
                $row['value'] = rand($row['tagMin'], $row['tagMax']);
                $ret[] = $row;
            }
        }
    }
    $info = $query;
    echo json_encode($ret);
    die();
}
?>
<html>
    <head>
        <meta charset="UTF-8">
        <link href='https://fonts.googleapis.com/css?family=Ubuntu+Condensed|Ranchers&effect=3d' rel='stylesheet' type='text/css'>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
        <script src="js/ajax.js" type="text/javascript"></script>
       
        <link href="Style.css" rel="stylesheet" type="text/css"/>
        <style>
            #FilteredTbl{margin-left: auto; margin-right: auto; border:green dashed 1px;}
            #RTTable{margin-left: auto; margin-right: auto; margin-bottom: auto; margin-top: auto; border: black dotted 1px;}
            #tags{width:100%;}#slctFilter{width:100%; background-color: yellow; height: 500px; vertical-align: top;}
            tr,td{vertical-align: top;}
        </style>
        <title>Real-Time Data</title>
    </head>
    <body>
        <div class="header">
            <h1>ICA 06 - RealTime Monitor - Welcome <?php echo $_SESSION['username'] ?></h1>
        </div>
        <div class="main">
            <a href="index.php"><button>Back</button></a>
            <table id="RTTable"><a href="index.php">
                <tr><td>Tag Filter: </td><td><input type="text" placeholder="Filter Tag" id="txtFilter"><br><button id="tags">Get Tags</button>
                        <select class="slctItems" id="slctFilter" multiple="multiple"></select><br></td>
                    <td><table id="FilteredTbl">
                            <thead><tr><th colspan="3"> <button id="Refresh">Refresh</button></th><th colspan="2"><button id="live">Live Update</button></th></tr>
                                <tr><th>ID</th><th>Min</th><th>Max</th><th>Value</th><th>Bar</th></tr>
                            </thead><tbody id="slctBdy"></tbody></table></td>
                </tr>
            </table>
        </div>
    </body>
</html>
