var mysql = require('mysql');
var conn = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: '',
    database: 'gochat',
    port: 3306
});
conn.connect();
//INSERT IP IN USER TABLE
function InsertIp(Ip,UserName)
{
    conn.query('UPDATE user set Ip="'+Ip+'" where UserName="'+UserName+'"',
        function(err, rows, fields) {
            if (err) throw err;
            return true;

            //console.log('The solution is: ', json.data.time);
        });
    //return 1;
}

//REMOVE IP IN USER TABLE
function RemoveIp(UserName)
{
    conn.query('UPDATE user set Ip="" where UserName="'+UserName+'"',
        function(err, rows, fields) {
            if (err) throw err;
            return true;

            //console.log('The solution is: ', json.data.time);
        });
}

//CHECK USER BEFORE REGISTER
function checkUser(username)
{


}
//REGISTER
function InsertUser(username,pin,profile)
{
    conn.query('INSERT INTO user (UserName,PIN,profile) VALUES("'+username+'","'+pin+'","'+profile+'")',
        function(err, rows, fields) {
            if (err) throw err;
            return true;
        });
}

//UPDATA FRIEND TABLE
function updateFriend(UserId,FriendId)
{
    conn.query('UPDATE friend set FriendId = concat(FriendId,",'+FriendId+'") where UserId='+UserId,
        function(err, rows, fields) {
            if (err) throw err;
            return true;
        });
}

//INSERT CHAT TABLE
function InsertChat(member,type)
{
    conn.query('INSERT INTO chat (member,type) VALUES("'+member+'",'+type+')',
        function(err, rows, fields) {
            if (err) throw err;
            return true;
        });
}

//INSERT MESSAGE TABLE
function InsertMessage(ChatId,time,content,from,type)
{
    conn.query('INSERT INTO  message (ChatId,time,content,message.`from`,type) VALUES('+ChatId+',"'+time+'","'+content+'",'+from+','+type+')',
        function(err, rows, fields) {
            if (err) throw err;
            return true;
        });
}


// http://ejohn.org/blog/ecmascript-5-strict-mode-json-and-more/
"use strict";

// Optional. You will see this name in eg. 'ps' or 'top' command
process.title = 'node-chat';

// Port where we'll run the websocket server
var webSocketsServerPort = 1337;

// websocket and http servers
var webSocketServer = require('websocket').server;
var http = require('http');

/**
 * Global variables
 */
// latest 100 messages
var history = [ ];
// list of currently connected clients (users)
var clients = [ ];
var clientsIp = [];
/**
 * Helper function for escaping input strings
 */
function htmlEntities(str) {
    return String(str).replace(/&/g, '&amp;').replace(/</g, '&lt;')
        .replace(/>/g, '&gt;').replace(/"/g, '&quot;');
}

// Array with some colors
var colors = [ 'red', 'green', 'blue', 'magenta', 'purple', 'plum', 'orange' ];
// ... in random order
colors.sort(function(a,b) { return Math.random() > 0.5; } );

/**
 * HTTP server
 */
var server = http.createServer(function(request, response) {
    // Not important for us. We're writing WebSocket server, not HTTP server
});
server.listen(webSocketsServerPort, function() {
    console.log((new Date()) + " Server is listening on port " + webSocketsServerPort);
});

/**
 * WebSocket server
 */
var wsServer = new webSocketServer({
    // WebSocket server is tied to a HTTP server. WebSocket request is just
    // an enhanced HTTP request. For more info http://tools.ietf.org/html/rfc6455#page-6
    httpServer: server
});

// This callback function is called every time someone
// tries to connect to the WebSocket server
wsServer.on('request', function(request) {
    console.log((new Date()) + ' Connection from origin ' + request.origin + '.');

    // accept connection - you should check 'request.origin' to make sure that
    // client is connecting from your website
    // (http://en.wikipedia.org/wiki/Same_origin_policy)
    var connection = request.accept(null, request.origin);
    //console.log('ip:'+connection.remoteAddresses);

    // we need to know client index to remove them on 'close' event
    var index = clients.push(connection) - 1;
    //var Ip = clientsIp.push(connection.remoteAddress);
    //console.log(Ip);
    var UserName = false;
    var password = false;
    var UserId = false;

    console.log((new Date()) + ' Connection accepted.');

    for (var i = 0; i < clients.length; i++) {
        //console.log(clients[i].remoteAddress);

        console.log(i+"!!!!!"+clients[i].remoteAddress);
    }
    // send back chat history
    if (history.length > 0) {
        connection.sendUTF(JSON.stringify( { type: 'history', data: history} ));
    }

    // user sent some message
    connection.on('message', function(message) {

        if (message.type === 'utf8') { // accept only text

            message = htmlEntities(message.utf8Data);

            var strs = message.split(','); //字符分割

            //console.log(strs);
            UserName = strs[0];
            password = strs[1];
            //console.log(strs);
            //anis
            var checkD = message.split('$');

            if (strs.length===2 && checkD.length < 2) {
                // first message sent by user is their name
                // remember user name and password

                // verify client here -> create hash value from password and compare with stored hash value

                // if verification result is true,store client's ip and send database to client


                conn.query('SELECT * from user where UserName="' + strs[0] + '" and PIN="' + strs[1] + '"',
                    function (err, logincheck, fields) {
                        if (err) throw err;

                        if (logincheck.length == 0) {
                            var logininfo ="wrong";
                            connection.sendUTF(JSON.stringify({type: 'logincheck', data:logininfo}));

                        }else{
                            InsertIp(clients[index].remoteAddress,UserName);
                            //var logininfo ="true";
                            //connection.sendUTF(JSON.stringify({type: 'logincheck', data:logininfo}));
                            UserName = strs[0];
                            password = strs[1];
                            //GET USER TABLE
                            conn.query('SELECT * from user where UserName="' + UserName + '" and PIN="' + password + '"',
                                function (err, rows, fields) {
                                    if (err) throw err;
                                    //anis
                                    if(rows.length==0){
                                        // throw exception here
                                    }else{
                                        UserId = rows[0].UserId;
                                        var jsonstr = "[{UserId: " + rows[0].UserId + ", PIN:'" + rows[0].PIN + "', UserName: '" + rows[0].UserName + "', profile:'"+rows[0].profile+"' , Ip:'"+rows[0].Ip+"' }]";
                                        var UserList = eval('(' + jsonstr + ')');

                                    }


                                    //GET INFO FROM FRIEND TABLE
                                    conn.query('SELECT * from friend where UserId=' + UserId,
                                        function (err, fri, fields) {
                                            if (err) throw err;
                                            //anis
                                            if (fri.length==0){
                                                connection.sendUTF(JSON.stringify({type: 'user', data: UserList}));


                                                var friendList = "No friend list in database";
                                                connection.sendUTF(JSON.stringify({type: 'friend', data: friendList}));


                                            }else{
                                                var FriendId = fri[0].FriendId;
                                                var friendList = {
                                                    UserId: fri[0].UserId,
                                                    FriendId: fri[0].FriendId
                                                };

                                                connection.sendUTF(JSON.stringify({type: 'friend', data: friendList}));
                                                conn.query('SELECT * from user where UserId in('+FriendId+')',
                                                    function (err, friends, fields) {
                                                        if (err) throw err;

                                                        var long = friends.length;

                                                        for (var i = 0; i < long; i++) {
                                                            var array =
                                                            {
                                                                UserId: friends[i].UserId,
                                                                PIN: friends[i].PIN,
                                                                UserName: friends[i].UserName,
                                                                profile: friends[i].profile,
                                                                Ip:friends[i].Ip
                                                            };
                                                            UserList.push(array);
                                                        }
                                                        connection.sendUTF(JSON.stringify({type: 'user', data: UserList}));
                                                        //connection.sendUTF(JSON.stringify({type: 'friend', data: friendList}));

                                                    });
                                                //connection.sendUTF(JSON.stringify({ type:'firend', data: friendList}));

                                            }
                                        });

                                    //GET CHAT TABLE
                                    conn.query('SELECT * from chat where member like "%' + UserId + '%"',
                                        function (err, rows, fields) {
                                            if (err) throw err;
                                            //anis
                                            if (rows.length == 0){
                                                var chatList = "No chat list in the database";
                                                connection.sendUTF(JSON.stringify({type: 'chat', data: chatList}));


                                                var  messageList = "No message list in the database";
                                                connection.sendUTF(JSON.stringify({type: 'message', data: messageList}));
                                            }else{
                                                var jsonstr = "[{ChatId: " + rows[0].ChatId + ", member:'" + rows[0].member + "', type: " + rows[0].type + " }]";
                                                var chatList = eval('(' + jsonstr + ')');
                                                var long = rows.length;

                                                for (var i = 1; i < long; i++) {
                                                    var arr =
                                                    {
                                                        "ChatId": rows[i].ChatId,
                                                        "member": rows[i].member,
                                                        "type": rows[i].type,

                                                    };
                                                    chatList.push(arr);
                                                }

                                                connection.sendUTF(JSON.stringify({type: 'chat', data: chatList}));
                                                var str="";
                                                for(var j=0;j<rows.length;j++)
                                                {
                                                    if(j<rows.length-1)
                                                    {
                                                        str +=rows[j].ChatId+',';
                                                    }
                                                    else{
                                                        str+=rows[j].ChatId;
                                                    }

                                                }

                                                conn.query('SELECT * from message where ChatId in ('+str+')',
                                                    function (err, mess, fields) {
                                                        if (err) throw err;


                                                        var  messInfo = "[{Id:"+mess[0].Id+",ChatId: " + mess[0].ChatId + ", time:'" + mess[0].time + "', content: '" + mess[0].content + "',from:" + mess[0].from + ",type:" + mess[0].type + "}]";
                                                        var  messageList = eval('(' + messInfo + ')');
                                                        for (var j = 1; j < mess.length; j++) {
                                                            var array =
                                                            {
                                                                "Id" : mess[j].Id,
                                                                "ChatId": mess[j].ChatId,
                                                                "time": mess[j].time,
                                                                "content": mess[j].content,
                                                                "from": mess[j].from,
                                                                "type": mess[j].type
                                                            };
                                                            messageList.push(array);
                                                        }
                                                        //messList.push(messageList);
                                                        connection.sendUTF(JSON.stringify({type: 'message', data: messageList}));
                                                    });
                                            }
                                        });
                                });
                        }
                    });

            } else { // log and broadcast the message

                var strs = message.split('$'); //字符分割

                //ADD FRIEND
                if(strs[0] == "search")
                {
                    conn.query('select * from user where UserName="'+strs[2]+'"',
                        function(err, rows, fields) {
                            if (err) throw err;

                            if(rows.length==0)
                            {
                                obj="wrong : no user";
                                var json = JSON.stringify({type: 'searchFriend', data: obj});
                                clients[index].sendUTF(json);
                            }
                            else
                            {
                                conn.query('SELECT * from friend where FriendId like "%'+rows[0].UserId+'%" and UserId='+strs[1],
                                    function(err, checkfriend, fields) {
                                        if (err) throw err;
                                        if(checkfriend.length>0)
                                        {
                                            obj="This user is already your friend";
                                            var json = JSON.stringify({type: 'searchFriend', data: obj});
                                            clients[index].sendUTF(json);
                                        }
                                        else {
                                            var obj =
                                            {
                                                "UserId" : rows[0].UserId,
                                                "PIN": rows[0].PIN,
                                                "UserName": rows[0].UserName,
                                                "profile": rows[0].profile,
                                                "Ip": rows[0].Ip
                                            };
                                            var json = JSON.stringify({type: 'searchFriend', data: obj});
                                            clients[index].sendUTF(json);
                                        }
                                    });

                                //InsertUser(strs[2],strs[3],strs[4]);

                            }
                        });
                }

                //REGISTER
                if(strs[0] == "register")
                {
                    conn.query('select * from user where UserName="'+strs[1]+'"',
                        function(err, rows, fields) {
                            if (err) throw err;
                            if(rows.length>0)
                            {
                                obj="please change another username";
                                var json = JSON.stringify({type: 'register', data: obj});
                                clients[index].sendUTF(json);
                            }
                            else
                            {
                                InsertUser(strs[1],strs[2],strs[3]);
                                obj="succeed";
                                var json = JSON.stringify({type: 'register', data: obj});
                                clients[index].sendUTF(json);
                            }
                        });
                }

                //CHANGE FRIEND TABLE
                if (strs[0] == "friend") {
                    updateFriend(strs[1],strs[2]);

                    // we want to keep history of all sent messages
                    var obj = {
                        UserId: strs[1],
                        FriendId: strs[2]
                    };
                    var json = JSON.stringify({type: 'ufriend', data: obj});

                    conn.query('SELECT user.Ip from user where UserId in ('+strs[2]+','+strs[1]+')',
                        function (err, rows, fields) {
                            if (err) throw err;

                            for(var j=0;j<rows.length;j++)
                            {
                                for (var i = 0; i < clients.length; i++) {

                                    if (rows[j].Ip == clients[i].remoteAddress) {
                                        clients[i].sendUTF(json);
                                    }
                                }
                            }
                        });
                }

                //CHANGE CHAT TABLE
                if (strs[0] == "chat") {
                    InsertChat(strs[1], strs[2]);
                    // we want to keep history of all sent messages
                    conn.query('SELECT * from chat where member ="'+strs[1]+'" and type ="'+strs[2]+'"',
                        function (err, rows, fields) {
                            if (err) throw err;
                            //console.log(rows[0].Ip);
                            if (rows.length == 0){
                                var obj = "Failed to create new chat";
                                connection.sendUTF(JSON.stringify({type: 'uchat', data: obj}));
                            }else{
                                var obj = {
                                    ChatId:rows[0].ChatId,
                                    member: rows[0].member,
                                    type: rows[0].type
                                };

                                var json = JSON.stringify({type: 'uchat', data: obj});


                                //console.log(obj.member);
                                conn.query('SELECT user.Ip from user where UserId in ('+strs[1]+')',
                                    function (err, rows, fields) {
                                        if (err) throw err;
                                        //console.log(rows[0].Ip);
                                        for(var j=0;j<rows.length;j++)
                                        {
                                            for (var i = 0; i < clients.length; i++) {

                                                if (rows[j].Ip == clients[i].remoteAddress) {
                                                    clients[i].sendUTF(json);
                                                }
                                            }
                                        }
                                    });
                            }

                        });
                }

                //CHANGE MESSAGE TABLE
                if (strs[0] == "message") {

                    InsertMessage(strs[1], strs[2], strs[3], strs[4], strs[5]);
                    // we want to keep history of all sent messages
                    conn.query('SELECT * from message where ChatId ='+strs[1]+' and time="'+strs[2]+'" and content="'+strs[3]+'" and message.from='+strs[4],
                        function (err, messageList, fields) {
                            if (err) throw err;
                            //console.log(rows[0].Ip);
                            var obj = {
                                Id :messageList[0].Id,
                                ChatId:messageList[0].ChatId,
                                time: messageList[0].time,
                                content: messageList[0].content,
                                from: messageList[0].from,
                                type: messageList[0].type
                            };
                            var json = JSON.stringify({type: 'umessage', data: obj});


                            conn.query('SELECT member from chat where ChatId='+strs[1],
                                function (err, ChatId, fields) {
                                    if (err) throw err;

                                    conn.query('SELECT user.Ip from user where UserId in ('+ChatId[0].member+')',
                                        function (err, rows, fields) {
                                            if (err) throw err;
                                            //console.log(rows[0].Ip);

                                            for(var j=0;j<rows.length;j++)
                                            {
                                                for (var i = 0; i < clients.length; i++) {
                                                    if (rows[j].Ip == clients[i].remoteAddress) {
                                                        clients[i].sendUTF(json);
                                                    }
                                                }
                                            }
                                        });
                                });
                        });
                }
            }
        }
    });

    //user disconnected
    connection.on('close', function(connection) {
        if (UserName !== false && password !== false) {
            console.log((new Date()) + " Peer "
                + UserName + " disconnected.");
            // remove user from the list of connected clients
            //RemoveIp(UserName);
            clients.splice(index, 1);
        }
    });

});
