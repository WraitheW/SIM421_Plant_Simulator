<?php

require 'ConnectionSettings.php';

// variables submitted by user
//$loginUser = $_POST["loginUser"];
//$loginPass= $_POST["loginPass"];
$itemID = $_POST["itemID"];

// check connection
if ($conn->connect_error) 
{
  die("Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully, now we will show the users.<br><br>";

$sql = "SELECT name, description, price, imgVer FROM items WHERE ID = '" . $itemID . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    $rows = array();
    while($row = $result->fetch_assoc()) {
      $rows[] = $row;
    }
    echo json_encode($rows);
  } else {
    echo "0";
  }

$conn->close();

?>