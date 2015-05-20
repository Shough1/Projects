<?php
require_once 'functions.php';
require_once 'inc/db.php';

$info = "";

function TableUpdate() {

    global $mysqli, $mysqli_response, $mysqli_status, $info;
        
    $filter = $mysqli->real_escape_string($filter);
    $q = "SELECT userID,username,password";
    $q .= " FROM prj_users";


    $tableOutput = "<tbody>";
    if ($result = mysqliQuery($q)) {

        while ($row = $result->fetch_assoc()) {
            $tableOutput .= "<tr>";
            $tableOutput .= "<td ><a href='settings.php?userID=$row[userID]&action=delete'><button>Delete</button></a></td>
                        <td>{$row['userID']}</td><td>{$row['username']}</td><td>{$row['password']}</td>";
            $tableOutput .= "</tr>";
        }

        $tableOutput .= "</tbody>";
    } else {
        return "Query Error: $q";
    }
    return $tableOutput;
}

if (isset($_GET['submit']) && $_GET['submit'] == 'add' &&
        isset($_GET['username']) && strlen($_GET['username']) > 0 &&
        isset($_GET['password']) && strlen($_GET['password']) > 0 &&
        $_GET['username'] != $_SESSION['username']) {

    $uName = $mysqli->real_escape_string($_GET['username']);

    $exists = "SELECT username";
    $exists .= " FROM prj_users";
    $exists .= " WHERE username ='$uName'";


    if ($result = mysqliNonQuery($exists)) {

        $info = "User name already exists!";
    } else {

        $userName = $mysqli->real_escape_string($_GET['username']);
        $password = $mysqli->real_escape_string($_GET['password']);

        $userNameSet = strip_tags($userName);
        $passwordSet = password_hash($password, PASSWORD_DEFAULT);
        $q = "INSERT INTO prj_users (username, password) VALUES ('$userNameSet','$passwordSet')";
        mysqliNonQuery($q);

        $info = "User Added!";
    }
}

function DeleteUser($user) {
    global $mysqli, $mysqli_response, $mysqli_status, $info;

    $user = $mysqli->real_escape_string($user);
    $query = "DELETE FROM prj_users WHERE userID = $user";

    if ($_SESSION['userID'] == $user) {

        return "Cannot delete yourself!";
    }
    if (($result = mysqliNonQuery($query)) == -1) {

        return "Query Failed";
    }
}

if ($_GET['action'] == 'delete') {
    $info = DeleteUser($_GET['userID']);
}




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
            <h1>ica04- <?php echo "ICA04 - Welcome {$_SESSION['username']} to the settings page" ?></h1>
        </div>
        <div class="main">
            <fieldset>
                <legend><i>Add User:</i></legend>
                <a href="index.php"><button>Back</button></a>
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
                                <td></td><td colspan="2"><input type='submit' value='add' Name='submit' id='add'></td>

                            </tr>
                        </tbody>
                    </table>
                </form>
            </fieldset>
            <table id="userAdd">
                <thead><tr><th style="background-color: lightgreen" >Op</th><th>userID</th><th style="background-color: lightgreen" >UserName</th><th>Encrypted Password</th></tr></thead>
<?php echo TableUpdate() ?>
            </table>
            <div class="status">
<?php echo $info; ?>
            </div> 
        </div>
        <div class="footer"></div>

    </body>
</html>

