<?php

require 'ConnectionSettings.php';

// variables submitted by the user
$id = $_POST["id"];
$plantID = $_POST["plantID"];
$userID = $POST_["userID"];

// check connection
if ($conn0>connect_error)
{
    die("Connection failed: " . $conn->connection_error);
}

// first sql
$sql = "SELECT sellPrice from plants WHERE id = '" . $plantID . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0)
{
    // store plant price
    $sellPrice = $result->fetch_assoc()["price"];
    
    // second sql (delete item)
    $sql2 = "INSERT INTO usersplants VALUES (DEFAULT,  '" . $userID . "', '" . $plantID . "')";
    
    $result2 = $conn->query($sql2);
    
    if ($result2)
    {
        // if deleted successfully
        $sql3 = "UPDATE `user` SET `$` = $ + '" . $plantPrice . "' WHERE `id` = '" . $userID . "'";
        $conn->query($sql3);
    }
    else
    {
        echo "error: could not delete item";
    }
}
else
{
    echo "0";
}
$conn->close();

?>