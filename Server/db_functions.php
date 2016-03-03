<?php

//This class contains function to perform database CRUD operations. But i wrote function for creating user only. 
class DB_Functions {
 
    private $db;
 
    //put your code here
    // constructor
    function __construct() {
        include_once './db_connect.php';
        // connecting to database
        $this->db = new DB_Connect();
        $this->db->connect();
    }
 
    // destructor
    function __destruct() {
         
    }
 
    /**
     * Storing new user
     * returns user details
     */
    public function storeUser($name, $email, $gcm_regid) {
        // insert user into database
        $result = mysql_query("INSERT INTO gcm_users(name, email, gcm_regid, created_at) VALUES('$name', '$email', '$gcm_regid', NOW()) ON DUPLICATE KEY UPDATE name = '$name', email = '$email', created_at = NOW();");
        // check for successful store
        if ($result) {
            return "";			
        } else {
            return mysql_error();
        }
    }
  
    /**
     * Remove user
     */
    public function removeUser($gcm_regid) {
        // delete user 
        $result = mysql_query("DELETE FROM gcm_users WHERE gcm_regid = '$gcm_regid';");
        // check for successful store
        if ($result) {
			return "";
        } else {
            return mysql_error();
        }
    }
    /**
     * Getting all users
     */
    public function getAllUsers() {
        $result = mysql_query("select * FROM gcm_users");
        return $result;
    }
 
}
 
?>