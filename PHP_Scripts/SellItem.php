<?php

require 'ConnectionSettings.php';

// variables submitted by user
$ID = $_POST["ID"];
$itemID = $_POST["itemID"];
$userID = $_POST["userID"];

// check connection
if ($conn->connect_error)
{
    die("Connection failed: " . $conn->connect_error);
}

// first Sql
$sql = "SELECT price FROM items WHERE ID = '" . $itemID . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0)
{

    // store item price
    $itemPrice = $result->fetch_assoc()["price"];

    // second sql (delete item)
    $sql2 = "DELETE FROM usersitems WHERE ID = '" . $ID . "'";

    $result2 = $conn->query($sql2);

    if ($result2)
    {
        // if deleted successfully
        $sql3 = "UPDATE `user` SET `gold`= gold + '" . $itemPrice . "' WHERE `id` = '" . $userID . "'";
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