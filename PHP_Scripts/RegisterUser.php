<?php

require 'ConnectionSettings.php';

// variables submitted by user
$loginUser = $_POST["loginUser"];
$loginPass= $_POST["loginPass"];

// check connection
if ($conn->connect_error) 
{
  die("Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully, now we will show the users.<br><br>";

$sql = "SELECT username FROM user WHERE username = '" . $loginUser . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // tell user that that name is already taken
  echo "Username is already taken.";
} 
else 
{
  echo "Creating user...";
  // insert the user and password into the database
  $sql = "INSERT INTO user (username, password, level, $) VALUES ('" . $loginUser . "', '" . $loginPass . "', 1, 20)";
  
  if ($conn->query($sql) === TRUE)
  {
    echo "New user created successfully.";
  }
  else
  {
    echo "Error: " . $sql . "<br>" . $conn->error;
  }
}
$conn->close();

?>