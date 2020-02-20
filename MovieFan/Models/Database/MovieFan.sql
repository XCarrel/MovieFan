USE master
GO

-- First delete the database if it exists
IF (DB_ID('MovieFan') is not null)
BEGIN
	USE master
	ALTER DATABASE MovieFan SET SINGLE_USER WITH ROLLBACK IMMEDIATE; -- Disconnect users the hard way (we cannot drop the db if someone's connected)
	DROP DATABASE MovieFan -- Destroy it
END
GO

-- Second ensure we have the proper directory structure
CREATE TABLE #ResultSet (Directory varchar(200)) -- Temporary table (name starts with #) -> will be automatically destroyed at the end of the session

INSERT INTO #ResultSet EXEC master.sys.xp_subdirs 'c:\' -- Stored procedure that lists subdirectories

IF NOT EXISTS (Select * FROM #ResultSet where Directory = 'DATA')
	EXEC master.sys.xp_create_subdir 'C:\DATA\' -- create DATA

DELETE FROM #ResultSet -- start over for MSSQL subdir
INSERT INTO #ResultSet EXEC master.sys.xp_subdirs 'c:\DATA'

IF NOT EXISTS (Select * FROM #ResultSet where Directory = 'MSSQL')
	EXEC master.sys.xp_create_subdir 'C:\DATA\MSSQL'

DROP TABLE #ResultSet -- Explicitely delete it because the script may be executed multiple times during the same session
GO

-- Everything is ready, we can create the db
CREATE DATABASE MovieFan ON  PRIMARY 
( NAME = 'MovieFan_data', FILENAME = 'C:\DATA\MSSQL\MovieFan.mdf' , SIZE = 20480KB , MAXSIZE = 51200KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = 'MovieFan_log', FILENAME = 'C:\DATA\MSSQL\MovieFan.ldf' , SIZE = 10240KB , MAXSIZE = 20480KB , FILEGROWTH = 1024KB )

GO

USE MovieFan
GO

CREATE TABLE categories(
	id int Identity primary key,
	name varchar(50) NOT NULL UNIQUE
	)
GO

CREATE TABLE ratings(
	id int Identity primary key,
	name varchar(50) NOT NULL UNIQUE
	)
GO

CREATE TABLE movies(
	id int Identity primary key,
	title varchar(50) NOT NULL,
	release date,
	picture varchar(200),
	synopsis varchar (5000),
	category_id int not null,
	rating_id int not null
	)
GO

Alter Table movies
Add Constraint  UQ_movie unique (title, release);
go

CREATE TABLE users(
	id int Identity primary key,
	firstname varchar(50) NOT NULL,
	lastname varchar(50) NOT NULL,
	isAdmin tinyint
	)
GO

Alter Table users
Add Constraint  UQ_user unique (firstname, lastname);
go

CREATE TABLE user_like_movie(
	id int Identity primary key,
	user_id int not null,
	movie_id int not null,
	comment varchar(5000),
	stars int check (stars between 1 and 5),
	hasSeen tinyint not null default 0
	)
GO

Alter Table user_like_movie
Add Constraint  UQ_like unique (user_id, movie_id);
go


ALTER TABLE movies WITH CHECK ADD CONSTRAINT FK_category FOREIGN KEY(category_id)
REFERENCES categories (id)
GO
ALTER TABLE movies WITH CHECK ADD  CONSTRAINT FK_rating FOREIGN KEY(rating_id)
REFERENCES ratings (id)
GO
ALTER TABLE user_like_movie  WITH CHECK ADD  CONSTRAINT FK_user_like FOREIGN KEY(user_id)
REFERENCES users (id)
GO
ALTER TABLE user_like_movie WITH CHECK ADD  CONSTRAINT FK_movie_like FOREIGN KEY(movie_id)
REFERENCES movies (id)
GO

INSERT INTO categories (name) VALUES 
				('Action'),
				('Comedy'),
				('Thriller'),
				('Drama'),
				('SF'),
				('Horror');

INSERT INTO ratings (name) VALUES 
				('Tout public'),
				('14+'),
				('16+'),
				('18+');

GO

INSERT INTO movies (title, release, picture, synopsis, category_id, rating_id) VALUES 
    				('Superman vs Batman', '2016-03-25', 'G01', 'D01',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('Deadpool', '2016-02-12', 'G02', 'D02',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('Furious 7', '2015-04-03', 'G03', 'D03',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('PK', '2014-12-19', 'G04', 'D04',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('Gladiator', '2000-05-05', 'G01', 'D05',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('The Hangover', '2009-06-05', 'G02', 'D06',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('3 Idiots', '2009-12-25', 'G04', 'D04',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('Spectre', '2015-11-06', 'G03', 'D07',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('Batman Begins', '2005-06-15', 'G01', 'D08',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('The Dark Knight', '2008-07-18', 'G05', 'D08',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID()));

INSERT INTO users(firstname,lastname) VALUES('Allistair','Cunningham'),('Sarah','Weaver'),('Lucas','Knowles'),('Rinah','Jefferson'),('Timon','Hardy'),('Nell','Marks'),('Elmo','Middleton'),('Jane','Vance'),('Lee','Conway'),('Bevis','Blake');
INSERT INTO users(firstname,lastname) VALUES('Cecilia','Hardin'),('Taylor','Solis'),('Lael','Spence'),('Tyrone','Faulkner'),('Jelani','Jones'),('Jane','Bradshaw'),('Xena','Waters'),('Basil','Roman'),('Bruce','Hammond'),('Ulysses','Stein');
INSERT INTO users(firstname,lastname) VALUES('Uta','Valenzuela'),('Beatrice','Mendoza'),('Chastity','Miller'),('Zenia','Miles'),('Jermaine','Shepherd'),('Adele','Mason'),('Cullen','Bender'),('Macaulay','Tran'),('Timon','Thornton'),('Noble','Goff');
INSERT INTO users(firstname,lastname) VALUES('Aquila','Huffman'),('Dorian','Carlson'),('Iola','Monroe'),('Aiko','Schultz'),('Myles','Osborne'),('Sacha','Ewing'),('Cameran','Ross'),('Beck','Prince'),('Megan','Bass'),('Hyacinth','Sears');
INSERT INTO users(firstname,lastname) VALUES('Aquila','Brady'),('Lee','Erickson'),('Maia','Alexander'),('Elizabeth','Clark'),('Kibo','Camacho'),('Levi','Phillips'),('Kendall','Benson'),('Vladimir','Harris'),('Alika','Waters'),('Hiroko','Sparks');
INSERT INTO users(firstname,lastname) VALUES('Nissim','Chavez'),('Josiah','Leonard'),('Daphne','Holcomb'),('Alma','Aguilar'),('Rae','Rowe'),('Clementine','Nieves'),('Jade','Perez'),('Kasper','Mccarthy'),('Odessa','Salas'),('Zane','Craig');
INSERT INTO users(firstname,lastname) VALUES('Tucker','Bowman'),('Cain','Mcfarland'),('Keefe','Suarez'),('Teagan','Blackwell'),('Melyssa','Morin'),('Taylor','Solo'),('Perry','Tillman'),('Barrett','Monroe'),('Evangeline','Mccormick'),('Kenneth','Hester');
INSERT INTO users(firstname,lastname) VALUES('Gray','Dixon'),('Dane','Solis'),('Quentin','Workman'),('Peter','Cobb'),('Price','Sanford'),('Jana','Adkins'),('Zenia','Baldwin'),('Sara','Fernandez'),('Reagan','Ruiz'),('Kylee','Mcdaniel');
INSERT INTO users(firstname,lastname) VALUES('Urielle','Crawford'),('Shaine','Parker'),('Dean','Jimenez'),('Cullen','Mckay'),('Vladimir','Hale'),('Carter','Estrada'),('Pascale','Morin'),('Sonia','Edwards'),('Xerxes','Hart'),('Jonah','Finch');
INSERT INTO users(firstname,lastname) VALUES('Clare','Barry'),('Harriet','King'),('Garrison','Herrera'),('Justine','Mendoza'),('Harding','Kelley'),('Benjamin','Kirkland'),('Tana','Woods'),('Hector','Marks'),('Garrett','Stephens'),('Rahim','Hobbs');

update users set isAdmin = 1 where id = (SELECT TOP 1 id FROM users ORDER BY NEWID());
update users set isAdmin = 1 where id = (SELECT TOP 1 id FROM users ORDER BY NEWID());
update users set isAdmin = 1 where id = (SELECT TOP 1 id FROM users ORDER BY NEWID());


-- This loop will cause insert errors on duplicates. Just ignore them
WHILE (SELECT count(*) FROM user_like_movie) < 300  
BEGIN  
	insert into user_like_movie (user_id, movie_id, stars, hasSeen) values ((SELECT TOP 1 id FROM users ORDER BY NEWID()),(SELECT TOP 1 id FROM movies ORDER BY NEWID()),round(1+5*rand(),0,1),(SELECT CAST(ROUND(RAND(),0) AS BIT)))
END 