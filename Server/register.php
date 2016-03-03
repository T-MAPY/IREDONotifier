<?php
 
// response json
$json = array();
 
/**
 * Registering a user device
 * Store reg id in users table
 */
if (isset($_POST["regId"])) {
    $name = $_POST["name"];
    $email = $_POST["email"];
    $gcm_regid = $_POST["regId"]; // GCM Registration ID
    // Store user details in db
    include_once './db_functions.php';
    include_once './GCM.php';
 
    $db = new DB_Functions();
    $gcm = new GCM();
 
    $res = $db->storeUser($name, $email, $gcm_regid);
 
    //$registatoin_ids = array($gcm_regid);
	//$message = array("message" => "Device registered");	
    //$result = $gcm->send_notification($gcm_regid , $message);
	if (strlen($res) == 0)
		echo '{"success" : 1,"failure" : 0}';
    else 
		echo '{"success" : 0,"failure" : 1, "results":[{"error":"' . $res . '"}]}';
} else {
    // user details missing
}
?>