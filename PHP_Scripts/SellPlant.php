<?php

require 'ConnectionSettings.php';

// variables submitted by user
$ID = $_POST["ID"];
$plantID = $_POST["plantID"];
$userID = $_POST["userID"];

// check connection
if ($conn->connect_error)
{
    die("Connection failed: " . $conn->connect_error);
}

// first Sql
$sql = "SELECT sellPrice FROM plants WHERE ID = '" . $plantID . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0)
{

    // store item price
    $plantPrice = $result->fetch_assoc()["sellPrice"];

    // second sql (delete item)
    $sql2 = "DELETE FROM usersplants WHERE ID = '" . $ID . "'";

    $result2 = $conn->query($sql2);

    if ($result2)
    {
        // if deleted successfully
        $sql3 = "UPDATE `user` SET `$`= `$` + '" . $plantPrice . "' WHERE `id` = '" . $userID . "'";
        $conn->query($sql3);
    }
    else
    {
        echo "error: could not delete plant";
    }
}
else
{
    echo "0";
}
$conn->close();

?>