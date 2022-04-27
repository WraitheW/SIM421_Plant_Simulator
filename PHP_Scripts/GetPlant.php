<?php

require 'ConnectionSettings.php';

// variables submitted by user
//$loginUser = $_POST["loginUser"];
//$loginPass= $_POST["loginPass"];
$plantID = $_POST["plantID"];

// check connection
if ($conn->connect_error) 
{
  die("Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully, now we will show the users.<br><br>";

$sql = "SELECT name, description, soil, water, sunlight, shade, maturity, pruning, seedAmount, price, sellPrice, imgVer, needSun, position FROM plants WHERE id = '" . $plantID . "'";

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