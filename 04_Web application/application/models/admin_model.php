<?php


class Admin_model extends CI_Model {
    public function __construct()
    {
        $this->load->database();
    }

    //    login
    public function login_do($loginInfo)
    {
        $query = $this->db->get_where('user',$loginInfo);
        return $query->row_array();
    }

    public function getUserInfo($UserName,$UserId)
    {
        $sql = "SELECT * FROM user_".$UserName." WHERE UserId=".$UserId;
        $query = $this->db->query($sql);
        return $query->row_array();
    }

    public function getFriend($UserName,$UserId)
    {
        $sql = "SELECT * FROM friend_".$UserName." WHERE UserId=".$UserId;
        $query = $this->db->query($sql);
        return $query->row_array();
    }

    // CHATTING FRIEND ID NON_GROUP
    public function getUList($UserName,$ChatId,$UserId)
    {
        $sql="select DISTINCT message_".$UserName.".ChatId,message_".$UserName.".from from message_".$UserName." where message_".$UserName.".from!=".$UserId." and message_".$UserName.".ChatId=".$ChatId." ORDER BY message_".$UserName.".time desc";
        $query = $this->db->query($sql);
        return $query->row_array();
    }

    // CHATTING FRIEND ID GROUP
    public function getGroupId($UserName,$ChatId,$UserId)
    {
        $sql="select DISTINCT message_".$UserName.".from,message_".$UserName.".ChatId from message_".$UserName." where message_".$UserName.".from!=".$UserId." and message_".$UserName.".ChatId=".$ChatId." ORDER BY message_".$UserName.".time desc";
        $query = $this->db->query($sql);
        return $query->result_array();
    }


    public function getChatId($UserName,$info1,$info2)
    {
        $sql = "SELECT ChatId FROM chat_".$UserName." WHERE member='".$info1."' OR member ='".$info2."'";
        $query = $this->db->query($sql);
//        var_dump($query);die;
        return $query->row_array();
    }


    public function getMessage($UserName,$ChatId)
    {
        $sql = "SELECT * FROM message_".$UserName." WHERE ChatId=".$ChatId;
        $query = $this->db->query($sql);
        return $query->result_array();
    }

    //get chatid non-group
    public function getChatId_nogroup($UserName)
    {
        $sql = "SELECT ChatId,member FROM chat_".$UserName." WHERE type=0";
        $query = $this->db->query($sql);
        return $query->result_array();
    }

    //GET CHATID GROUP
    public function getChatId_group($UserName)
    {
        $sql = "SELECT ChatId,member FROM chat_".$UserName." WHERE type=1";
        $query = $this->db->query($sql);
        return $query->result_array();
    }

    //GET GROUPCHAT USERINFO
    public function getGroupUser($UserName,$UserId)
    {
        $sql = "SELECT UserName FROM user_".$UserName." WHERE UserId=".$UserId;
        $query = $this->db->query($sql);
        return $query->row_array();
    }

    //GET PROFILE IN CHATING BY USERID
    public function getProfile($UserName,$UserId)
    {
        $sql = "SELECT profile FROM user_".$UserName." WHERE UserId=".$UserId;
        $query = $this->db->query($sql);
        return $query->row_array();
    }

    //添加好友
    public function addfriend_do($UserName)
    {
        $sql = "SELECT * FROM user WHERE UserName='".$UserName."'";
        $query = $this->db->query($sql);
        return $query->row_array();
    }

    //INSERT FRIEDN
    public function insertFriend($UserId,$str)
    {
        $sql = "UPDATE friend set FriendId='".$str."' where UserId=".$UserId;
        $query = $this->db->query($sql);
//        var_dump($query);die;
        return $this->db->affected_rows();
    }

}

?>
