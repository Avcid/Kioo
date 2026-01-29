<?php

require_once __DIR__ . '/core/database.php';

$uriParam = $_GET['page'];

if($uriParam === NULL) {
    header('Location: /index.php?page=login');
}

switch ($uriParam) {
    case 'login':
        require_once __DIR__ . '/pages/login.php';
        break;
    
    default:
        # code...
        break;
}