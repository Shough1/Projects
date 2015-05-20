<?php
session_start();
$userTable = array();
$userTable['admin'] = password_hash('god', PASSWORD_DEFAULT);
$userTable['germf'] = password_hash('new123', PASSWORD_DEFAULT);

function Validate($param) {
    global $userTable;

    if (isset($userTable[$param['username']])) {

        if (password_verify($param['password'], $userTable[$param['username']])) {
            $param['response'] = "<style>.status{background-color: green; font-weight: bold; font-size:x-large;}</style>Server Response: Hello, {$param['username']} you have been authenticated.";
            $param['status'] = true;
        } else {
            $param['response'] = "<style>.status{background-color: red; font-weight: bold; font-size:x-large;}</style>Server Response: Bugger off ya Hozer!!!";
            $param['status'] = false;
        }
    } else {
        $param['response'] = "<style>.status{background-color: red; font-weight: bold; font-size:x-large;}</style>Server Response: Bugger off ya Hozer!!!";
        $param['status'] = false;
    }


    return $param;
}
