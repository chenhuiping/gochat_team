<?php


class Admin_model extends CI_Model {
    public function __construct()
    {
        $this->load->database();
    }

    //    登录
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

    public function getChatId($info1,$info2)
    {
        $sql = "SELECT ChatId FROM chat WHERE member=".$info1." OR member =".$info2;
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

//    public function insertMessage($content)
//    {
//        $sql="insert into message VALUES ('5', '2', '13:22','".$content."', '1')";
//        $query = $this->db->query($sql);
//        return $query->row_array();
//
//    }

}

?>
