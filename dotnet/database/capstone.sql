USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE posts (
	post_id int IDENTITY(1,1) NOT NULL,
	post_title varchar(40) NOT NULL,
	username varchar(50) NOT NULL,
	content varchar(3000) NOT NULL,
	upvote_counter int NOT NULL,
	downvote_counter int NOT NULL,
	posted_date datetime NOT NULL
	CONSTRAINT PK_post PRIMARY KEY (post_id)
)

CREATE TABLE replies (
	reply_id int IDENTITY (1,1) NOT NULL,
	post_id int NOT NULL,
	username varchar(50) NOT NULL,
	content varchar(3000) NOT NULL,
	posted_date datetime NOT NULL,
	CONSTRAINT PK_reply PRIMARY KEY (reply_id),
	CONSTRAINT FK_post FOREIGN KEY (post_id) REFERENCES posts (post_id)
)



--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO posts (post_title, username, content, upvote_counter, downvote_counter, posted_date) VALUES ( 'default post','user', 'this is a default post', 0, 0, GETDATE())

INSERT INTO replies (post_id, username, content, posted_date) VALUES (1, 'user', 'this is a default reply', GETDATE())

GO