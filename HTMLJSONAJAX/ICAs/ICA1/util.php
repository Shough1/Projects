<?php

//global $status;

function GenerateNumbers() {
    global $status,$count;
    $array = array();

    for ($i = 0; $i < 10; $i++) {
        $array[] = $i;
    }

    shuffle($array);
    $count+=1;
    $status .= ", $count";
    return $array;
}

function MakeList($array) {

    global $status, $count; //this is global so values can be carried accross the form
    $output = "<ol>";
    foreach ($array as $key => $value) {
        $output .= "<li>$value</li>";
    }
    $output .= "</ol>";
    $count++;
    $status .= ", $count";
    return $output;
}
