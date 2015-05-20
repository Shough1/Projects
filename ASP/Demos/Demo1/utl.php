<?php

function MakeArr(){
    
    $num = rand(1,10);
    $newArr = array();
    //$newArr = [];
    
    for($i = 0; $i < $num; $i++)
    {        $newArr[] = $i;}
    
    shuffle($newArr);
    
    return $newArr;
    
}
