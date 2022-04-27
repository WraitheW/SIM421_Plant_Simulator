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

//$sql = "SELECT password, id FROM user WHERE username = '" . $loginUser . "'";
//$result = $conn->query($sql);

$sql = "SELECT password, ID, level, $ FROM user WHERE username = ?";
$statement = $conn->prepare($sql);
$statement->bind_param("s", $loginUser);

$statement->execute();
$result = $statement->get_result();

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) 
  {
    if ($row["password"] == $loginPass)
    {
        echo $row["ID"];
        // get user's data here

        // get player info

        // get inventory

        // modify player data
    }
    else
    {
        echo "Wrong credentials.";
    }
  }
} else {
  echo "Username does not exist.";
}
$conn->close();

?>