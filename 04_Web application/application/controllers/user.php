<?php
error_reporting(E_ALL^E_NOTICE^E_WARNING);
class User extends CI_Controller {
    public function __construct()
    {
        parent::__construct();
        $this->load->helper('url');
        $this->load->helper(array('form', 'url'));
        $this->load->model('user_model');
    }

    public function login_check()
    {
        $session_admin = $this->session->userdata('jm_admin');
        if(!$session_admin){
            redirect('admin/login');
        }

    }

    /*功能 登录*/
    public function login_do()
    {
        $username=  $this->input->post('username');
        $password = $this->input->post('password');

//        $username="admin";
//        $password ="admin";

        $loginInfo = array(

            'PIN'=> $password,
            'UserName'=>$username
        );
        $user = $this->user_model->login_do($loginInfo);
        if(!count($user)){
            //false
            $data['status']=false;
//            redirect('admin/login');
        }
        else{
            //success
            $data['status']=true;
            //var_dump($user['nickName']);die;
            $this->session->set_userdata(array('jm_admin'=>$user['UserName'],'jm_admin_id'=>$user['UserId']));
//            redirect('admin/index');
        }
        $json = json_encode($data);
        print_r($json);
    }

    public function logout()
    {
        $this->session->sess_destroy();
            if(!empty($_COOKIE['jm_admin_id']) || !empty($_COOKIE['jm_admin'])){
                setcookie("jm_admin_id", null, time()-3600*24*365);
                setcookie("jm_admin", null, time()-3600*24*365);
            }

        redirect('login');
        return;
    }


    //logintest
    public function logintest()
    {
        $this->load->view('logintest');
    }

    //register
    public function register()
    {
        $user=  $this->input->post('user');
        $passwd = $this->input->post('passwd');

        $loginInfo = array(

            'UserName'=> $user,
            'PIN'=>$passwd
        );
        $user = $this->user_model->register($user,$passwd);
        if(!count($user)){
            //false
            $data['status']=false;
//            redirect('admin/login');
        }
        else{
            //success
            $data['status']=true;
            //var_dump($user['nickName']);die;
            $this->session->set_userdata(array('jm_admin'=>$user['UserName'],'jm_admin_id'=>$user['UserId']));
//            redirect('admin/index');
        }
        $json = json_encode($data);
        print_r($json);

    }






}
?>
















