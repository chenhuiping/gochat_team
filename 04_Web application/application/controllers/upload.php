<?php

class Upload extends CI_Controller {

    public function __construct()
    {
        parent::__construct();
        $this->load->helper(array('form', 'url'));

    }

    public function index()
    {
        $this->load->view('upload_form', array('error' => ' ' ));
    }

    //UPLOAD FILE

    public function do_upload($UserName)
    {
        $filePath='./uploads/'.$UserName."/";

        if (!file_exists($filePath))
        {
            mkdir ($filePath);
        }
//        $fileName=date('YmdHis').rand(1000,9999).".jpg";

        $config['upload_path']      = $filePath;
        $config['allowed_types']    = 'gif|jpg|png|docx';
        $config['max_size']     = 1000;
        $config['max_width']        = 1024;
        $config['max_height']       = 768;
        $config['overwrite']=true;
//        $config['file_name']=$fileName;

        $this->load->library('upload', $config);

        if ( ! $this->upload->do_upload('userfile'))
        {
            $error = array('error' => $this->upload->display_errors());

            $this->load->view('upload_form', $error);
        }
//        else
//        {
////            $data = array('upload_data' => $this->upload->data());
//
////            $this->load->view('index');
//            redirect('/admin/index');
//        }
        redirect('/admin/index/admin/1');


    }


    /*功能 上传图片*/
    public function uploadCgPhoto($Username)
    {
        $filePath='./uploads/'.$Username."/";

        if (!file_exists($filePath))
        {
            mkdir ($filePath);
        }
//        $fileName=date('YmdHis').rand(1000,9999).".jpg";
        $fileName = $_FILES['userfile']['name'];

        $len = strlen($fileName);
//        var_dump($len);
        $leng =$len-3;
        $sufix=substr($fileName, $leng, $len);
        if($sufix=="jpg" || $sufix=="png" || $sufix=="png")
        {
            $data['type']=1;
        }
        else{
            $data['type']=2;
        }
        $config['upload_path']      = $filePath;
        $config['allowed_types']    = 'gif|jpg|png|docx|';
        $config['max_size']     = 1000;
        $config['max_width']        = 1024;
        $config['max_height']       = 768;
        $config['overwrite']=true;
        $config['file_name']=$fileName;

        $this->load->library('upload', $config);

        if ( ! $this->upload->do_upload('userfile'))
        {
            $error = array('error' => $this->upload->display_errors());

            $this->load->view('upload_form', $error);
        }

            $data['path']=$filePath.$fileName;
            $json=json_encode($data);
            print_r($json);
//        }
    }
}
?>