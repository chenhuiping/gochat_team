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

    public function getUserInfo($UserId)
    {
        $sql = "SELECT * FROM user WHERE UserId=".$UserId;
        $query = $this->db->query($sql);
        return $query->row_array();
    }

    public function getFriend($UserId)
    {
        $sql = "SELECT * FROM friend WHERE UserId=".$UserId;
        $query = $this->db->query($sql);
        return $query->row_array();
    }

    // CHATTING FRIEND ID NON_GROUP
    public function getUList($ChatId,$UserId)
    {
        $sql="select DISTINCT message.from from message where message.from!=".$UserId." and message.ChatId=".$ChatId." ORDER BY message.time desc";
        $query = $this->db->query($sql);
        return $query->row_array();
    }

    // CHATTING FRIEND ID GROUP
    public function getGroupId($ChatId,$UserId)
    {
        $sql="select DISTINCT message.from from message where message.from!=".$UserId." and message.ChatId=".$ChatId." ORDER BY message.time desc";
        $query = $this->db->query($sql);
        return $query->result_array();
    }


    public function getChatId($info1,$info2)
    {
        $sql = "SELECT ChatId FROM chat WHERE member='".$info1."' OR member ='".$info2."'";
        $query = $this->db->query($sql);
//        var_dump($query);die;
        return $query->row_array();
    }


    public function getMessage($ChatId)
    {
        $sql = "SELECT * FROM message WHERE ChatId=".$ChatId;
        $query = $this->db->query($sql);
        return $query->result_array();
    }

    //get chatid non-group
    public function getChatId_nogroup()
    {
        $sql = "SELECT ChatId FROM chat WHERE type=0";
        $query = $this->db->query($sql);
        return $query->result_array();
    }

    //GET CHATID GROUP
    public function getChatId_group()
    {
        $sql = "SELECT ChatId FROM chat WHERE type=1";
        $query = $this->db->query($sql);
        return $query->result_array();
    }


    //GET GROUPCHAT USERINFO
    public function getGroupUser($UserId)
    {
        $sql = "SELECT UserName FROM user WHERE UserId=".$UserId;
        $query = $this->db->query($sql);
        return $query->row_array();
    }

    //GET PROFILE IN CHATING BY USERID
    public function getProfile($UserId)
    {
        $sql = "SELECT profile FROM user WHERE UserId=".$UserId;
        $query = $this->db->query($sql);
        return $query->row_array();
    }

}

?>
