var async = require('async');
var mysql = require('mysql');
var conn = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: '',
    database: 'gochat',
    port: 3306
});
conn.connect();
var send_data = function(req,res){
    sql = 'SELECT * from chat where member like "%1%"';
    conn.query(sql, [0,0,6], function(err, rows1, fields) {
        if (err) throw err;
        console.log(rows1[0].ChatId);
        async.map(rows, function(item, callback) {
            sql = "SELECT * FROM message WHERE ChatId="+rows1[0].ChatId;
            connection.query(sql, item.ChatId, function(err, tags, fields){
                item.tags = tags;
                callback(null, item);
            });
        }, function(err,results) {
            res.render('index', {supplies:results, login:req.session.login});
        });
    });
};

