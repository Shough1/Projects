<?php
session_start();  //must be the VERY FIRST THING DONE!!!!!
//save encrypted password of "germf"
$encrypted_password = password_hash("germf", PASSWORD_DEFAULT);

function Validate( $params ){
    global $encrypted_password;  //pulls into local scope
    if(password_verify($params['password'], $encrypted_password))
    {
       $params['response'] = "Hello {$params['username']} you are authenticated";
       $params['status'] = true;
    }
    else
    {
       $params['response'] = "Bugger Off ya HOZER";
       $params['status'] = false; 
    }
    
    return $params;
    
}






 //crypto - encrypt or hashing
 //many ways in php
//current best practice
// encrypted = password_hash($to_hashm PASSWORD_DEFAULT)
//                              php enviro default
//                              Blowfish w/ "Salt"
// (bool) = password_verify($text_password, $hashed_password)
// sessions - enable cookie with session id that will ten maintain data on server mapped against 
// session id for your connection (session)
// $_Session - supraglobal holds your data on server.  Can confuse server  may require cookie data be deleted
// so may require clearing local cookies( or new private browser)




