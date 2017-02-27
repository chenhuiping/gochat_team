/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50611
Source Host           : localhost:3306
Source Database       : gochat

Target Server Type    : MYSQL
Target Server Version : 50611
File Encoding         : 65001

Date: 2017-02-16 20:18:42
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `chat`
-- ----------------------------
DROP TABLE IF EXISTS `chat`;
CREATE TABLE `chat` (
  `ChatId` int(11) NOT NULL AUTO_INCREMENT,
  `member` char(255) NOT NULL,
  PRIMARY KEY (`ChatId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of chat
-- ----------------------------

-- ----------------------------
-- Table structure for `friend`
-- ----------------------------
DROP TABLE IF EXISTS `friend`;
CREATE TABLE `friend` (
  `UserId` int(11) NOT NULL,
  `FriendId` char(255) NOT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of friend
-- ----------------------------
INSERT INTO `friend` VALUES ('1', '2,3');

-- ----------------------------
-- Table structure for `message`
-- ----------------------------
DROP TABLE IF EXISTS `message`;
CREATE TABLE `message` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ChatId` int(11) NOT NULL,
  `time` char(255) NOT NULL,
  `content` text NOT NULL,
  `from` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of message
-- ----------------------------

-- ----------------------------
-- Table structure for `user`
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `PIN` char(255) NOT NULL,
  `UserName` char(255) NOT NULL,
  `Telphone` char(11) DEFAULT NULL,
  `Authority` int(11) NOT NULL DEFAULT '0',
  `profile` char(255) NOT NULL,
  `Login` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('1', 'admin', '', '07599639588', '0', 'assets/dist/images/1.jpg', '0');
INSERT INTO `user` VALUES ('2', 'admin', 'viki', '11111111111', '0', 'assets/dist/images/2.png', '0');
INSERT INTO `user` VALUES ('3', 'admin', 'Peggy', '07599639588', '0', 'assets/dist/images/3.jpg', '0');
