<?php


class User_model extends CI_Model {
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

    //insert  register

    public function register($user,$passwd)
    {
        $sql = "INSERT into user (PIN,UserName) values('".$passwd."','".$user."')";
        $query = $this->db->query($sql);
        return $query->row_array();

    }


}

?>
