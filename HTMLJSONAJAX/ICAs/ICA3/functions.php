<?php
session_start();
require_once 'inc/db.php';

function TestUsername($username) {
    global $mysqli, $mysqli_response, $mysqli_status;
    $userTab = array();

    $username = $mysqli->real_escape_string($username);

    $user = "SELECT userID,username, password";
    $user .= " FROM prj_users";
    $user .= " WHERE username = '$username'";

    if ($result = mysqliQuery($user)) {
        while ($row = $result->fetch_assoc()) {
            $userTab['userID'] = $row['userID'];
            $userTab[$row['username']] = $row['password'];
            
        }
    } else {
        return "Query Error: $user";
    }

    return $userTab;
}

function Validate($param) {

    $userTable = TestUsername($param['username']);


    if (isset($userTable[$param['username']])) {

        if (password_verify($param['password'], $userTable[$param['username']])) {
            $param['response'] = "<style>.status{background-color: green; font-weight: bold; font-size:x-large;}</style>Server Response: Hello, {$param['username']} you have been authenticated.";
            $param['userID'] = $userTable['userID'];
            $param['status'] = true;
        } else {
            $param['response'] = "<style>.status{background-color: red; font-weight: bold; font-size:x-large;}</style>Server Response: Username Correct / Password Incorrect!!!";
            $param['status'] = false;
        }
    } else {
        $param['response'] = "<style>.status{background-color: red; font-weight: bold; font-size:x-large;}</style>Server Response: Username not found!!!";
        $param['status'] = false;
    }


    return $param;
}

//username willy password wonka
//username great password gazoo