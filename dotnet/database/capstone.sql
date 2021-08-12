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
CREATE TABLE forums_list (
	forum_id int IDENTITY(1,1) NOT NULL,
	forum_title varchar(100) NOT NULL,
	forum_picture varchar(MAX)
	CONSTRAINT PK_forums PRIMARY KEY (forum_id)
)

CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE user_favorite_forum(
user_id int NOT NULL,
forum_id int NOT NULL,
FOREIGN KEY (user_id) REFERENCES users (user_id),
FOREIGN KEY (forum_id) REFERENCES forums_list (forum_id),
UNIQUE (user_id, forum_id)
)

CREATE TABLE user_moderator_forum(
user_id int NOT NULL,
forum_id int NOT NULL,
FOREIGN KEY (user_id) REFERENCES users (user_id),
FOREIGN KEY (forum_id) REFERENCES forums_list (forum_id),
UNIQUE (user_id, forum_id)
)

CREATE TABLE posts (
	post_id int IDENTITY(1,1) NOT NULL,
	forum_id int NOT NULL,
	post_title varchar(100) NOT NULL,
	username varchar(50) NOT NULL,
	content varchar(3000) NOT NULL,
	upvote_counter int NOT NULL,
	downvote_counter int NOT NULL,
	posted_date datetime NOT NULL,
	image_url varchar(MAX),
	CONSTRAINT PK_post PRIMARY KEY (post_id),
	CONSTRAINT FK_forum FOREIGN KEY (forum_id) REFERENCES forums_list (forum_id)
)

CREATE TABLE user_vote_posts(
user_id int NOT NULL,
post_id int NOT NULL,
vote int NOT NULL,
FOREIGN KEY (user_id) REFERENCES users (user_id),
FOREIGN KEY (post_id) REFERENCES posts (post_id),
UNIQUE (user_id, post_id)
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

INSERT INTO forums_list (forum_title, forum_picture) VALUES ('default forum', 'http://static1.squarespace.com/static/55ef2da9e4b03f6e1ef0cd28/t/5cddb9fb5ed3ff0001d64d24/1558034940526/Mark.jpg?format=1500w');
INSERT INTO forums_list (forum_title, forum_picture) VALUES ('secondary forum', 'http://static1.squarespace.com/static/55ef2da9e4b03f6e1ef0cd28/t/5cddb9fb5ed3ff0001d64d24/1558034940526/Mark.jpg?format=1500w');
INSERT INTO forums_list (forum_title, forum_picture) VALUES ('thirdary forum', 'http://static1.squarespace.com/static/55ef2da9e4b03f6e1ef0cd28/t/5cddb9fb5ed3ff0001d64d24/1558034940526/Mark.jpg?format=1500w');

INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) VALUES (1, 'default post', 'Billy', 'this is a default post', 0, 0, GETDATE(), 'https://i.imgur.com/38avySe.jpeg');
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) VALUES (1, 'secondary post', 'Bob', 'this is a secondary post', 0, 0, GETDATE(), 'https://gifimage.net/wp-content/uploads/2017/10/cat-gif-imgur-3.gif');
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date) VALUES (1, 'thirdary post', 'Joe', 'this is a thirdary post', 0, 0, GETDATE());

INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date) VALUES (2, 'default post', 'Billy', 'this is a default post', 0, 0, GETDATE());
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date) VALUES (2, 'secondary post', 'Bob', 'this is a secondary post', 0, 0, GETDATE());
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date) VALUES (2, 'thirdary post', 'Joe', 'this is a thirdary post', 0, 0, GETDATE());

INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date) VALUES (3, 'default post', 'Billy', 'this is a default post', 0, 0, GETDATE());
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date) VALUES (3, 'secondary post', 'Bob', 'this is a secondary post', 0, 0, GETDATE());
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date) VALUES (3, 'thirdary post', 'Joe', 'this is a thirdary post', 0, 0, GETDATE());

INSERT INTO replies (post_id, username, content, posted_date) VALUES (1, 'user', 'this is a default reply', GETDATE());
INSERT INTO replies (post_id, username, content, posted_date) VALUES (1, 'user', 'Bilye wuz hear', GETDATE());
INSERT INTO replies (post_id, username, content, posted_date) VALUES (1, 'user', 'this is a thirdary reply', GETDATE());

INSERT INTO replies (post_id, username, content, posted_date) VALUES (2, 'user', 'this is a default reply', GETDATE());
INSERT INTO replies (post_id, username, content, posted_date) VALUES (2, 'user', 'BIG BOB wuz hear', GETDATE());
INSERT INTO replies (post_id, username, content, posted_date) VALUES (2, 'user', 'this is a thirdary reply', GETDATE());

INSERT INTO replies (post_id, username, content, posted_date) VALUES (3, 'user', 'this is a default reply', GETDATE());
INSERT INTO replies (post_id, username, content, posted_date) VALUES (3, 'user', 'Roo wuz hear', GETDATE());
INSERT INTO replies (post_id, username, content, posted_date) VALUES (3, 'user', 'this is a thirdary reply', GETDATE());

INSERT INTO user_favorite_forum (user_id, forum_id) VALUES (1, 1);
INSERT INTO user_favorite_forum (user_id, forum_id) VALUES (1, 2);
INSERT INTO user_favorite_forum (user_id, forum_id) VALUES (2, 3);

INSERT INTO user_moderator_forum (user_id, forum_id) VALUES (1, 3);

GO

