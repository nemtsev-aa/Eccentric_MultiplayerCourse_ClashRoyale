<?php
	require 'ReadbeanPHP/rb-mysql.php';

    R::setup( 'mysql:host=localhost;dbname=test', 'root', '' );

    if(R::testConnection() == false){
        echo 'Connect error!';
        exit;
    }
?>