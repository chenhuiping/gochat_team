<?php
error_reporting(E_ALL);
error_reporting(E_ALL^E_NOTICE^E_WARNING);
class Server extends CI_Controller {
    public function __construct()
    {
        parent::__construct();
        $this->load->helper('url');
        $this->load->model('admin_model');

    }

    public function test()
    {
//        set_time_limit(0);
        echo "<h2>TCP/IP Connection</h2>\n";

        /* Get the port for the WWW service. */
//        $service_port = getservbyname('www', 'tcp');
        $service_port=9000;

        /* Get the IP address for the target host. */
        $address = gethostbyname('127.0.0.1');

        /* Create a TCP/IP socket. */
        $socket = socket_create(AF_INET, SOCK_STREAM, SOL_TCP);
        if ($socket === false) {
            echo "socket_create() failed: reason: " . socket_strerror(socket_last_error()) . "\n";
        } else {
            echo "OK.\n";
        }

        echo "Attempting to connect to '$address' on port '$service_port'...";
        $result = socket_connect($socket, $address, $service_port);
        if ($result === false) {
            echo "socket_connect() failed.\nReason: ($result) " . socket_strerror(socket_last_error($socket)) . "\n";
        } else {
            echo "OK.\n";
        }
        $in = "p";
        $in .= "peggy";
        $in .= "$";
        $out ="";

        echo "Sending HTTP HEAD request...";
        socket_write($socket, $in, strlen($in));
//        socket_write($socket, $in);
        echo "OK.\n";


        echo "Reading response:\n\n";
        while ($out = socket_read($socket, 2048)) {
            echo $out;
        }

        echo "Closing socket...";
        socket_close($socket);
        echo "OK.\n\n";
    }

    public function serverMessage($message)
    {
        echo $message;
    }

    public function get()
    {

//        set_time_limit(0);
    //ob_implicit_flush();

        $address = '127.0.0.1';
        $port = 9005;
    //创建端口
        if( ($sock = socket_create(AF_INET, SOCK_STREAM, SOL_TCP)) === false) {
            echo "socket_create() failed :reason:" . socket_strerror(socket_last_error()) . "\n";
        }

    //绑定
        if (socket_bind($sock, $address, $port) === false) {
            echo "socket_bind() failed :reason:" . socket_strerror(socket_last_error($sock)) . "\n";
        }

    //监听
        if (socket_listen($sock, 5) === false) {
            echo "socket_bind() failed :reason:" . socket_strerror(socket_last_error($sock)) . "\n";
        }

        do {
            //得到一个链接
            if (($msgsock = socket_accept($sock)) === false) {
                echo "socket_accepty() failed :reason:".socket_strerror(socket_last_error($sock)) . "\n";
                break;
            }
            //welcome  发送到客户端
            $msg = "server send:welcome";
            socket_write($msgsock, $msg, strlen($msg));
            echo 'read client message\n';
            $buf = socket_read($msgsock, 8192);
            $talkback = "received message:$buf\n";
            echo $talkback;
            if (false === socket_write($msgsock, $talkback, strlen($talkback))) {
                echo "socket_write() failed reason:" . socket_strerror(socket_last_error($sock)) ."\n";
            } else {
                echo 'send success';
            }
            socket_close($msgsock);
        } while(true);
    //关闭socket
        socket_close($sock);
    }


    public function udp()
    {
        $sock = socket_create(AF_INET, SOCK_DGRAM, SOL_UDP);
        $msg = 'hello$';
        $len = strlen($msg);
        socket_sendto($sock, $msg, $len, 0, '127.0.0.1', 9000);
        socket_close($sock);
    }

//    public function webSocket()
//    {
//
//    }
//    public function

//     function send_message($ipserver,$portserver,$message)
//    {
//        $fp=stream_socket_client("tcp://$ipserver:$portserver", $errno, $errstr);
//        if(!$fp)
//        {
//            echo "erreur : $errno - $errstr<br />n";
//        }
//        else
//        {
//            fwrite($fp,"$message");
//            $response =  fread($fp, 4);
//            if($response != "okn")
//            {
//                echo "the command couldn't be executed...ncause :".$response;
//            }
//             else
//            {
//              echo 'execution successfull...';
//            }
//            fclose($fp);
//        }
//    }

}
?>
















