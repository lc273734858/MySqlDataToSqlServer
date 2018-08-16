/*
Navicat SQLite Data Transfer

Source Server         : 本地SqlLite
Source Server Version : 30714
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30714
File Encoding         : 65001

Date: 2018-08-16 12:59:44
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for log
-- ----------------------------
DROP TABLE IF EXISTS "main"."log";
CREATE TABLE "log" (
"id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"taskname"  TEXT(200) NOT NULL COLLATE BINARY ,
"database"  TEXT(30) NOT NULL,
"tablename"  TEXT(100) NOT NULL,
"status"  INTEGER
);

-- ----------------------------
-- Indexes structure for table log
-- ----------------------------
CREATE INDEX "main"."task"
ON "log" ("id" ASC, "taskname" ASC);
