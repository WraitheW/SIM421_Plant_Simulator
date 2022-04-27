<?php

require 'ConnectionSettings.php';

// check connection
if ($conn->connect_error) 
{
  die("Connection failed: " . $conn->connect_error);
}

// variables submitted by user
$itemID = $_POST["itemID"];
$urlPath = $_POST["urlPath"];

//$sql = "SELECT imageURL FROM items WHERE itemID = '" . $itemID . "'";

$path = "https://sim421-group.000webhostapp.com/ItemsIcons/" . $itemID . ".png";
//$path = "sim421-group.000webhostapp.com/ItemsIcons/1.png";

$urlPath = $path;
//echo $urlPath;

$image = file_get_contents($path);

echo $image;

$conn->close();

?>