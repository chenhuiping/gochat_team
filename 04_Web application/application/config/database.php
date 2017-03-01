<?php
defined('BASEPATH') OR exit('No direct script access allowed');

/*
| -------------------------------------------------------------------
| DATABASE CONNECTIVITY SETTINGS
| -------------------------------------------------------------------
| This file will contain the settings needed to access your database.
|
| For complete instructions please consult the 'Database Connection'
| page of the User Guide.
|
| -------------------------------------------------------------------
| EXPLANATION OF VARIABLES
| -------------------------------------------------------------------
|
|	['dsn']      The full DSN string describe a connection to the database.
|	['hostname'] The hostname of your database server.
|	['username'] The username used to connect to the database
|	['password'] The password used to connect to the database
|	['database'] The name of the database you want to connect to
|	['dbdriver'] The database driver. e.g.: mysqli.
|			Currently supported:
|				 cubrid, ibase, mssql, mysql, mysqli, oci8,
|				 odbc, pdo, postgre, sqlite, sqlite3, sqlsrv
|	['dbprefix'] You can add an optional prefix, which will be added
|				 to the table name when using the  Query Builder class
|	['pconnect'] TRUE/FALSE - Whether to use a persistent connection
|	['db_debug'] TRUE/FALSE - Whether database errors should be displayed.
|	['cache_on'] TRUE/FALSE - Enables/disables query caching
|	['cachedir'] The path to the folder where cache files should be stored
|	['char_set'] The character set used in communicating with the database
|	['dbcollat'] The character collation used in communicating with the database
|				 NOTE: For MySQL and MySQLi databases, this setting is only used
| 				 as a backup if your server is running PHP < 5.2.3 or MySQL < 5.0.7
|				 (and in table creation queries made with DB Forge).
| 				 There is an incompatibility in PHP with mysql_real_escape_string() which
| 				 can make your site vulnerable to SQL injection if you are using a
| 				 multi-byte character set and are running versions lower than these.
| 				 Sites using Latin-1 or UTF-8 database character set and collation are unaffected.
|	['swap_pre'] A default table prefix that should be swapped with the dbprefix
|	['encrypt']  Whether or not to use an encrypted connection.
|	['compress'] Whether or not to use client compression (MySQL only)
|	['stricton'] TRUE/FALSE - forces 'Strict Mode' connections
|							- good for ensuring strict SQL while developing
|	['failover'] array - A array with 0 or more data for connections if the main should fail.
|	['save_queries'] TRUE/FALSE - Whether to "save" all executed queries.
| 				NOTE: Disabling this will also effectively disable both
| 				$this->db->last_query() and profiling of DB queries.
| 				When you run a query, with this setting set to TRUE (default),
| 				CodeIgniter will store the SQL statement for debugging purposes.
| 				However, this may cause high memory usage, especially if you run
| 				a lot of SQL queries ... disable this to avoid that problem.
|
| The $active_group variable lets you choose which connection group to
| make active.  By default there is only one group (the 'default' group).
|
| The $query_builder variables lets you determine whether or not to load
| the query builder class.
*/

//内网ip：rds89l2vqze5r465076j.mysql.rds.aliyuncs.com
//用户名：lcht
//密码：lcht2015_yyj



$active_group = 'default';
$query_builder = TRUE;

//$db['default']['hostname'] = 'rds5ty3k88i163pqs7y7.mysql.rds.aliyuncs.com';
//$db['default']['username'] = 'lcht';
//$db['default']['password'] = 'lcht2015_yyj';
//$db['default']['database'] = 'jinm';
//$db['default']['dbdriver'] = 'mysqli';
//$db['default']['dbprefix'] = 'jm_';
//$db['default']['pconnect'] = TRUE;
//$db['default']['db_debug'] = TRUE;
//$db['default']['cache_on'] = FALSE;
//$db['default']['cachedir'] = '';
//$db['default']['char_set'] = 'utf8';
//$db['default']['dbcollat'] = 'utf8_general_ci';
//$db['default']['swap_pre'] = 'jm_';
//$db['default']['autoinit'] = TRUE;
//$db['default']['stricton'] = FALSE;
//$db['default']['pconnect'] = FALSE;

$db['default']['hostname'] = '127.0.0.1';
$db['default']['username'] = 'root';
$db['default']['password'] = '';
$db['default']['database'] = 'gochat';
$db['default']['dbdriver'] = 'mysqli';
$db['default']['dbprefix'] = '';
$db['default']['pconnect'] = TRUE;
$db['default']['db_debug'] = TRUE;
$db['default']['cache_on'] = FALSE;
$db['default']['cachedir'] = '';
$db['default']['char_set'] = 'utf8';
$db['default']['dbcollat'] = 'utf8_general_ci';
$db['default']['swap_pre'] = '';
$db['default']['autoinit'] = TRUE;
$db['default']['stricton'] = FALSE;
$db['default']['pconnect'] = FALSE;



//$db['vote']['hostname'] = '115.159.109.46';
//$db['vote']['username'] = 'root';
//$db['vote']['password'] = 'lcht2015';
//$db['vote']['database'] = 'vote';
//$db['vote']['dbdriver'] = 'mysqli';
//$db['vote']['dbprefix'] = '';
//$db['vote']['pconnect'] = TRUE;
//$db['vote']['db_debug'] = TRUE;
//$db['vote']['cache_on'] = FALSE;
//$db['vote']['cachedir'] = '';
//$db['vote']['char_set'] = 'utf8';
//$db['vote']['dbcollat'] = 'utf8_general_ci';
//$db['vote']['swap_pre'] = '';
//$db['vote']['autoinit'] = TRUE;
//$db['vote']['stricton'] = FALSE;
//$db['vote']['pconnect'] = FALSE;

