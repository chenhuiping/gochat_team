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
//        $str="";
//        $str .="W%user%select%";
//        $str .=$password;
//        $str .="%";
//        $str .=$username;
//        $str .="%";
//        echo $str;
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

    public function addUserInfo()
    {
        $UserId=  $this->input->post('UserId');
        $PIN = $this->input->post('PIN');
        $UserName=  $this->input->post('UserName');
        $profile = $this->input->post('profile');

        $loginInfo = array(

            'UserId'=> $UserId,
            'PIN'=>$PIN,
            'UserName'=> $UserName,
            'profile'=>$profile
        );
        $user = $this->user_model->insert($loginInfo);
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

//    //CREAT TABLE FOR USER
//    public function createtable()
//    {
//        $user="user_admin";
//        $friend="user_admin";
//        $chat="user_admin";
//        $message="user_admin";
//        $data=$this->user_model->createUser($user);
//        $data=$this->user_model->createFriend($friend);
//        $data=$this->user_model->createChat($chat);
//        $data=$this->user_model->createMessage($message);
//        var_dump($data);
//    }

//    public function connection()
//    {
//        $user=  $this->input->post('user');
//        $friend=  $this->input->post('friend');
//        $chat=  $this->input->post('chat');
//        $message=  $this->input->post('message');
//        var_dump($user);
//
//        $data['status']=true;
//        $json = json_encode($data);
//        print_r($json);
//
//    }

    //CREATE TABLE
    public function createtable()
    {
        $userList=  $this->input->post('UserName');

//        $data['UserId']=$userList[0]['UserId'];
        $user="user_".$userList;
        $friend="friend_".$userList;
        $chat="chat_".$userList;
        $message="message_".$userList;

        $this->user_model->createUser($user);
        $this->user_model->createFriend($friend);
        $this->user_model->createChat($chat);
        $data['status']=$this->user_model->createMessage($message);

        $json = json_encode($data);
        print_r($json);
    }

    //INSERT INTO USER TABLE
    public function insertUser()
    {

        $userList=  $this->input->post('user');
        $UserName=  $this->input->post('UserName');
//        var_dump($UserName);
        $data['UserName']=$UserName;
        $UserName = "user_".$UserName;
//        var_dump($UserName);
//        $UserId="admin";

        for( $i=0;$i<count($userList);$i++)
        {
            $data['status']=$this->user_model->insertUser($UserName,$userList[$i]);
        }
        $data['UserId']=$userList[0]['UserId'];

//        var_dump($data['status']);
        $json = json_encode($data);
        print_r($json);
    }

    //INSERT INTO FRIEND TABLE
    public function insertFriend()
    {
        $UserName=  $this->input->post('UserName');
        $UserName = "friend_".$UserName;
//        var_dump($UserName);

        $friend=  $this->input->post('friend');

        $data['status2'] =$this->user_model->insertFriend($UserName,$friend);

        $json = json_encode($data);
        print_r($json);
    }



    //INSERT CHAT TABLE
    public function insertChat()
    {
        $UserName=  $this->input->post('UserName');
        $UserName = "chat_".$UserName;
//        var_dump($UserName);
//        var_dump(1);die;
        $chat =  $this->input->post('chat');
//       var_dump($chat);die;
        for( $i=0;$i<count($chat);$i++)
        {
            $data['status']=$this->user_model->insertChat($UserName,$chat[$i]);
        }

        $json = json_encode($data);
        print_r($json);

    }

    //INSERT MESSAGE TABLE
    public function insertMessage()
    {
        $UserName=  $this->input->post('UserName');
        $UserName = "message_".$UserName;
//        var_dump($UserName);
        $message =  $this->input->post('message');
            for( $i=0;$i<count($message);$i++)
            {
                $data['status']=$this->user_model->insertMessage($UserName,$message[$i]);
            }

        $json = json_encode($data);
        print_r($json);

    }

    //INSERT INTO UFRIEND TABLE
    public function insertUFriend()
    {
        $UserName=  $this->input->post('UserName');
        $UserName = "friend_".$UserName;
//        var_dump($UserName);

        $friend=  $this->input->post('ufriend');
//        var_dump($friend['UserId']);
        $data['status'] = $this->user_model->insertUFriend($UserName,$friend['UserId'],$friend['FriendId']);
//        var_dump($data['status']);

        $json = json_encode($data);
        print_r($json);
    }

    //INSERT INTO USER TABLE FOR NEW FRIEND
    public function insertUUser()
    {
        $UserName=  $this->input->post('UserName');
        $UserName = "user_".$UserName;
//        var_dump($UserName);

        $friend=  $this->input->post('friend');

        $data['status'] = $this->user_model->insertUUser($UserName,$friend);

        $json = json_encode($data);
        print_r($json);
    }

    //INSERT CHAT TABLE FOR NEW GROUPCHAT
    public function insertUChat()
    {
        $UserName=  $this->input->post('UserName');
        $UserName = "chat_".$UserName;
//        var_dump($UserName);

        $uchat=  $this->input->post('uchat');

        $data['status'] = $this->user_model->insertUChat($UserName,$uchat);

        $json = json_encode($data);
        print_r($json);

    }

    //INSERT MESSAGE TABLE ONE MESSAGE
    public function insertUMessage()
    {
        $UserName =  $this->input->post('UserName');
        $talbeName="chat_".$UserName;
        $message =  $this->input->post('message');
        $UserName ="message_".$UserName;

        $data['message']=$message;
        $data['status']=$this->user_model->insertUMessage($UserName,$message);
//        var_dump($message['ChatId']);die;

        $data['type']=$this->user_model->getChatType($talbeName,$message['ChatId']);
//        var_dump($data['type']);die;

        $json = json_encode($data);
        print_r($json);

    }

    //DELETE TABLE
    public function deleteTable()
    {
        $data['status2']=$this->user_model->deleteUser();
        $data['status3']=$this->user_model->deleteChat();
        $data['status4']=$this->user_model->deleteMessage();
        $data['status1']=$this->user_model->deleteUser();


        $json = json_encode($data);
        print_r($json);

    }







}
?>
















