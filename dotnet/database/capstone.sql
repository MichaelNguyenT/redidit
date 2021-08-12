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
	post_title varchar(MAX) NOT NULL,
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

INSERT INTO forums_list (forum_title, forum_picture) VALUES ('ReDo the Wheel', 'https://media.istockphoto.com/photos/broken-wheel-of-a-bike-in-the-junkyard-picture-id955271488');
INSERT INTO forums_list (forum_title, forum_picture) VALUES ('Movies that Need to be ReDone', 'https://d2gg9evh47fn9z.cloudfront.net/800px_COLOURBOX21949207.jpg');
INSERT INTO forums_list (forum_title, forum_picture) VALUES ('Animals that Could Use a Redo.', 'https://imgix.bustle.com/rehost/2016/9/13/f479670c-be2b-4cb1-b224-27605abd2a68.jpg?w=800&fit=crop&crop=faces&auto=format%2Ccompress');

INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) VALUES (1, 'Initial Post: The wheel is old trash technology.', 'FutureMan37', 'It is 2021. Why are we using technology from the literal stoneage?? SMH. What are some ideas to make the wheel better/more modern??', 0, 0, GETDATE(), 'https://i.imgur.com/ZM5PoJa.jpeg');
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) VALUES (1, 'Be the Wheel', 'DunkaroosRule', 'why attach a whole vehicle when you can be the vehicle?', 0, 0, GETDATE(), 'https://i.imgur.com/lWkLqRP.gif');
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) VALUES (1, 'Parm?', 'SwissCheeseStan', 'TALK ABOUT MEALS ON WHEELS JAJAJAJAJ', 0, 0, GETDATE(), 'https://static.turbosquid.com/Preview/001168/203/0Z/3D-parmesan-cheese-wheel_Z.jpg');

INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) VALUES (2, 'Saving Private Rhianna', 'FilmNerd4Ever', 'We need an all female cast. They brave the perils of Disturbia to find our queen safe underneath her Umbrella, ella, ella. Or something.', 0, 0, GETDATE(),'https://static.billboard.com/files/media/Rihanna-diamond-ball-2015-a-billboard-1548-compressed.jpg' );
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) VALUES (2, 'Vegan Dracula.', 'TreeHugger', 'The original nemesis of PETA, Transylvania''s own has a rude awakening when exposure to 5G waves gives them an allergy to blood! Will this bloodsucker turn into the world''s oldest soyboy?' , 0, 0, GETDATE(),'https://i.pinimg.com/originals/be/6a/95/be6a95a13e7d0229da6e777bdc82c1c3.jpg' );
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) VALUES (2, 'Spiderman', 'SpideySensesTingling', 'I may get hate for this but I personally believe that Spiderman has not gotten enough attention and deserves a fresh reskin. I think the perfect casting for a fresh take would be the dog that starred in Airbud.', 0, 0, GETDATE(), 'https://s3.amazonaws.com/cdn-origin-etr.akc.org/wp-content/uploads/2016/10/21102031/spider-costume-for-dog.jpg');

INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) VALUES (3, 'The Koala is the least efficient animal on the planet', '360NoScope', 'These things sleep 20 hours a day becuase all they have is eucalyptus leaf energy. They need a sandwich and a Monster Energy drink.', 0, 0, GETDATE(),'https://upload.wikimedia.org/wikipedia/commons/thumb/7/71/Dropbear.jpg/220px-Dropbear.jpg');
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) VALUES (3, 'Bats are wonderful!!!', 'AlysiaZebb', 'Usually small mammals have short lives and large mammals have long ones, but bats live about 11x longer than we would expect based on their size, and can live past 40! There''s only a single known virus that can kill bats (rabies, which is 100% fatal for every mammal) but most of the time bats don''t even get sick. Some bats can smell in three dimensions.', 0, 0, GETDATE(), 'https://upload.wikimedia.org/wikipedia/commons/thumb/f/f8/Florida_bonneted_bat_%28Eumops_floridanus%29.jpg/220px-Florida_bonneted_bat_%28Eumops_floridanus%29.jpg');
INSERT INTO posts (forum_id, post_title, username, content, upvote_counter, downvote_counter, posted_date, image_url) VALUES (3, 'IDK but it needs help', 'PostMaloneisGod', 'bro look at the pic...', 0, 0, GETDATE(),'https://dynaimage.cdn.cnn.com/cnn/q_auto,w_900,c_fill,g_auto,h_506,ar_16:9/http%3A%2F%2Fcdn.cnn.com%2Fcnnnext%2Fdam%2Fassets%2F130911185736-ugly-animals-aye-aye.jpg' );

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

