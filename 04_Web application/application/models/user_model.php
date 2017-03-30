<?php


class User_model extends CI_Model {
    public function __construct()
    {
        $this->load->database();
    }


    //CREAT TABLE USER
    public function createUser($tablename)
    {
        $sql="CREATE TABLE ".$tablename."(UserId INT NOT NULL,UserName VARCHAR (255) NOT NULL,PIN  VARCHAR(255) NOT NULL,profile  CHAR (255) ,Ip CHAR(255),PRIMARY KEY (UserId));";

        return $this->db->query($sql);
    }

    //CREAT TABLE FRIEND
    public function createFriend($tablename)
    {
        $sql="CREATE TABLE ".$tablename."(UserId INT NOT NULL,FriendId VARCHAR (255) NOT NULL,PRIMARY KEY (UserId));";

        return $this->db->query($sql);
}

    //CREAT TABLE CHAT
    public function createChat($tablename)
    {
        $sql="CREATE TABLE ".$tablename."(ChatId INT NOT NULL,member VARCHAR (255) NOT NULL,type INT NOT NULL,PRIMARY KEY (ChatId));";

        return $this->db->query($sql);
    }

    //CREAT TABLE MESSAGE
    public function createMessage($tablename)
    {
        $sql="CREATE TABLE ".$tablename."(Id INT NOT NULL,ChatId INT NOT NULL,time VARCHAR(255) NOT NULL,content VARCHAR(255),".$tablename.".from INT NOT NULL,type INT NOT NULL, PRIMARY KEY (Id));";

        return $this->db->query($sql);
    }


    //    login
    public function login_do($loginInfo)
    {
        $query = $this->db->get_where('user',$loginInfo);
        return $query->row_array();
    }

    //insert  register

    public function register($user,$passwd)
    {
        $sql = "INSERT into user (PIN,UserName) values('".$passwd."','".$user."')";
        $query = $this->db->query($sql);
        return $query->row_array();
    }

    public function insert($loginInfo)
    {
        $query = $this->db->insert('user_copy', $loginInfo);
        return $query->row_array();
    }




    //GET USERID FROMTABLE
    public function getUserId()
    {
        $query = $this->db->get('user');
        return $query->row_array();

    }

    //INSERT INTO USER TALBE
    public function insertUser($UserName,$loginInfo)
    {
        return $this->db->insert("".$UserName."", $loginInfo);
    }


    //INSERT INTO FRIEND TABLE
    public function insertFriend($UserName,$loginInfo)
    {
        return $this->db->insert("".$UserName."", $loginInfo);

    }

    //INSERT INTO CHAT TABLE
    public function insertChat($UserName,$loginInfo)
    {
//        var_dump($UserName);
        return $this->db->insert("".$UserName."", $loginInfo);
    }

    //INSERT INTO CHAT TABLE
    public function insertMessage($UserName,$loginInfo)
    {
//        var_dump($UserName);
        return $this->db->insert("".$UserName."", $loginInfo);
    }

    //DELETE USER TABLE
    public function deleteUser($UserName)
    {
        $query = $this->db->query('delete from user_'.$UserName);
        return $query;
    }

    //DELETE FRIEND TABLE
    public function deleteFriend($UserName)
    {
        $query = $this->db->query('delete from friend_'.$UserName);
        return $query;
    }

    //DELETE CHAT TABLE
    public function deleteChat($UserName)
    {
        $query = $this->db->query('delete from chat_'.$UserName);
        return $query;
    }

    //DELETE FRIEND TABLE
    public function deleteMessage($UserName)
    {
        $query = $this->db->query('delete from message_'.$UserName);
        return $query;
    }


    //INSERT  BRODCAST FRIEND TALBE
    public function insertUFriend($UserName,$UserId,$FriendId)
    {
        $sql = "UPDATE ".$UserName." set FriendId = concat(FriendId,',".$FriendId."') where UserId=".$UserId;
        $query = $this->db->query($sql);
        return $query;
    }


    //INSERT  BRODCAST user TALBE
    public function insertUUser($UserName,$info)
    {
        return $this->db->insert("".$UserName."", $info);
    }

    //INSERT BRODCAST CHAT TABLE

    public function insertUChat($UserName,$info)
    {
        return $this->db->insert("".$UserName."", $info);
    }

    //INSERT INTO MESSAGE TABLE NEW MESSAGE
    public function insertUMessage($UserName,$info)
    {
        return $this->db->insert("".$UserName."", $info);
    }

    //FIND CHATID
    public function getChatType($UserName,$ChatId)
    {
        $sql = "SELECT * FROM ".$UserName." WHERE ChatId=".$ChatId;
//        var_dump($sql);
        $query = $this->db->query($sql);
        return $query->row_array();

    }


}

?>
