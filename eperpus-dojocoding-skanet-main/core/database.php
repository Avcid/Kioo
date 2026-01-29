<?php

$hostname = "localhost";
$username = "root";
$password = "@CakAdi#123";
$database = "perpus_online";

$db = new mysqli($hostname, $username, $password, $database);

if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}