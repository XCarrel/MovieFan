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
	picture varchar(200) not null default 'default.jpg',
	synopsis varchar (5000),
	category_id int not null,
	rating_id int null
	)
GO

Alter Table movies
Add Constraint  UQ_movie unique (title, release);
go

CREATE TABLE users(
	id int Identity primary key,
	firstname varchar(50) NOT NULL,
	lastname varchar(50) NOT NULL,
	email varchar(200) NOT NULL UNIQUE,
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
    				('Superman vs Batman', '2016-03-25', 'default.jpg', 'Enfant, Bruce Wayne fuit l''enterrement de ses parents (un flash-back montre leur assassinat par un inconnu), puis tombe dans la caverne où une nuée de chauve-souris lui inspire son identité de héros. Des années plus tard, alors que Superman affronte le général Zod (événements vus dans le film Man of Steel) à Métropolis, Bruce tente de sauver des gens dans un bâtiment de Wayne Enterprises sans y parvenir complètement. Héros en activité depuis près de 20 ans à Gotham City sous l''identité de Batman, Wayne est persuadé que l''être surpuissant sera un jour la perte de l''Humanité et qu''il convient de se préparer à l''éliminer',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('Deadpool', '2016-02-12', 'default.jpg', 'Deadpool est un mercenaire défiguré doté d''une capacité surhumaine de guérison accélérée et capable de prouesses physiques. Le personnage est aussi connu sous le surnom de « Mercenaire à la grande bouche » (Merc with a Mouth) en raison de sa tendance à discuter et plaisanter constamment, notamment en cassant le quatrième mur (en parlant à ses lecteurs) pour causer des effets humoristiques et en faisant des gags récurrents',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('Furious 7', '2015-04-03', 'default.jpg', 'Après avoir vaincu Owen Shaw (Luke Evans) et sa bande, et avoir obtenu l’amnistie, Dominic Toretto (Vin Diesel), Brian O’Conner (Paul Walker) et leurs amis sont de retour aux États-Unis pour mener à nouveau une vie de famille tranquille. Brian commence à s’habituer à sa vie de père tandis que Dom tente d’aider Letty (Michelle Rodríguez) à retrouver la mémoire en la ramenant aux Race Wars (« Guerres de courses ») ; cependant, après une altercation avec Hector (Noel Gugliemi), un vieil ami à eux et organisateur du tournoi, elle s’en va',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('PK', '2014-12-19', 'default.jpg', 'Une mission extra-terrestre arrive dans le désert du Rajasthan et dépose l''un des siens en mission de reconnaissance. Celui-ci croise un voleur qui lui dérobe l''unique objet qui lui sert de communication avec les siens. Nu et désemparé, il cherche à le retrouver.',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('Gladiator', '2000-05-05', 'default.jpg', 'Maximus Decimus Meridius, général romain renommé, mène une nouvelle fois les légions de l''empereur Marc Aurèle à la victoire en ce jour de bataille en pays germanique. L''empereur, sentant sa fin proche, annonce le soir même en privé à Maximus qu''il souhaite lui laisser le pouvoir à sa mort, pour qu''il puisse le transmettre au Sénat et que Rome devienne à nouveau une République',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('The Hangover', '2009-06-05', 'default.jpg', 'Doug Billings va se marier avec Tracy Garner, la fille d''un riche habitant de Los Angeles. Pour son enterrement de vie de garçon, il souhaite passer une nuit à Las Vegas avec ses amis, Phil et Stu, et avec Alan, le frère de la future mariée. Sid, le père de Tracy, confie à Doug les clés de sa plus belle voiture, une ancienne Mercedes-Benz, tout en exigeant qu''il soit absolument le seul à la conduire',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('3 Idiots', '2009-12-25', 'default.jpg', 'Raju, Farhan et Rancho deviennent amis à l''Imperial College of Engineering, une école d''ingénieurs renommée en Inde. Rancho est souvent en confrontation avec Viru, le directeur de l''école qui accorde beaucoup d''importance à la compétition. Rancho s''oppose à Chatur, un autre étudiant qui apprécie les méthodes de travail du directeur. Le jour où Rancho fait une blague qui humilie Chatur, ce dernier lance le défi de se retrouver dans dix ans pour évaluer celui qui aura le plus de succès',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('Spectre', '2015-11-06', 'default.jpg', 'Lors d’une mission à Mexico, pendant la fête des morts, James Bond exécute les dernières volontés de l''ancienne M tuée à Skyfall en faisant exploser un appartement où se sont réunis plusieurs terroristes qui projettent de faire sauter un stade sportif. Bond prend en chasse le seul qui a survécu à l''explosion, Marco Sciarra, qui tente de s''échapper par hélicoptère. Bond s''agrippe à l''hélicoptère et, après un violent combat, jette par-dessus bord Sciarra après s''être emparé de l''anneau à motif de pieuvre qu''il portait au doigt',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('Batman Begins', '2005-06-15', 'default.jpg', 'Le jeune Bruce Wayne assiste impuissant au meurtre de ses parents. Profondément traumatisé, il grandit obnubilé par un désir de vengeance et voyage aux quatre coins du monde pour étudier la criminologie et les arts martiaux. La Ligue des ombres2, une secte de guerriers ninja dirigée par Ra''s al Ghul, se chargera de son entraînement physique. De retour chez lui à Gotham City, le jeune homme se charge de la gestion de Wayne Enterprises dont il est l''héritier. Opérant depuis le sous-sol du manoir familial avec l''aide de son majordome Alfred Pennyworth, Bruce Wayne se lance alors dans la lutte contre le crime sous le nom de Batman',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID())),
    				('The Dark Knight', '2008-07-18', 'default.jpg', 'Batman aborde une phase décisive de sa guerre au crime. Avec l''aide du lieutenant de police Jim Gordon et du nouveau procureur Harvey Dent, il entreprend de démanteler les dernières organisations criminelles qui infestent les rues de la ville. L''association s''avère efficace, mais le trio se heurte bientôt à un nouveau génie du crime qui répand la terreur et le chaos dans Gotham : le Joker. On ne sait pas d''où il vient ni qui il est. Ce criminel possède une intelligence redoutable doublé d''un humour sordide et n''hésite pas à s''attaquer à la pègre locale dans le seul but de semer le chaos',(SELECT TOP 1 id FROM categories ORDER BY NEWID()),(SELECT TOP 1 id FROM ratings ORDER BY NEWID()));

INSERT INTO users(firstname,lastname,email) VALUES('Macaulay','Christian','euismod@mollisPhasellus.org'),('Alice','Stevens','Donec@sed.org'),('Wilma','Lawson','tincidunt@euismodenim.net'),('Quail','Cervantes','dis.parturient.montes@lacusAliquam.org'),('Hasad','Landry','purus.gravida.sagittis@pellentesquemassa.com'),('Davis','Higgins','eros.Nam@Donecluctusaliquet.net'),('Cooper','Mcdonald','massa@elitEtiam.ca'),('Sage','Rodgers','cursus.vestibulum.Mauris@Vivamuseuismodurna.co.uk'),('Sylvester','Michael','ornare.Fusce.mollis@nunc.org'),('Kyla','Bolton','et@eliteratvitae.co.uk');
INSERT INTO users(firstname,lastname,email) VALUES('Karleigh','Fuentes','Proin.mi.Aliquam@eunibh.net'),('Veda','Charles','penatibus@mattisCraseget.com'),('Sage','Stout','ultrices.mauris@semPellentesque.com'),('Hadley','Koch','tincidunt.nibh@mauris.org'),('Audra','Dotson','lectus.convallis.est@neque.ca'),('Harlan','West','pede@acmetusvitae.net'),('Vielka','Odom','elit.pharetra.ut@vulputate.ca'),('Sheila','Sosa','enim@variusorci.com'),('Winifred','Rodgers','Nunc.mauris@nectellus.com'),('Rudyard','Fields','Vestibulum.accumsan@ligula.net');
INSERT INTO users(firstname,lastname,email) VALUES('Claudia','Estrada','urna.nec.luctus@pedeblanditcongue.net'),('Marny','Delacruz','auctor.vitae@sodales.com'),('Raphael','May','lorem.ac@nonluctus.org'),('Ruth','Ross','iaculis.enim.sit@adlitora.org'),('Alvin','Atkins','orci.quis.lectus@elitpretium.co.uk'),('Hall','Lindsay','Vestibulum.accumsan@etmagnisdis.ca'),('Aimee','Saunders','semper.egestas@perinceptos.org'),('Malik','Solis','urna@vestibulumnequesed.co.uk'),('Timon','Kane','mauris.Suspendisse.aliquet@atlacus.co.uk'),('Cooper','Rodriquez','taciti.sociosqu@sagittis.ca');
INSERT INTO users(firstname,lastname,email) VALUES('Guinevere','Quinn','egestas.Aliquam.nec@sit.com'),('Akeem','Cummings','augue@suscipitest.edu'),('Lionel','Roberts','non@convallisante.org'),('Lawrence','Buckley','sit.amet.dapibus@ipsumprimis.co.uk'),('Venus','Becker','nulla@tinciduntaliquam.net'),('Plato','Blankenship','ornare.placerat@placerat.ca'),('Jenette','Burnett','sed.libero.Proin@gravidamauris.com'),('Bradley','Bridges','Pellentesque.tincidunt.tempus@parturient.com'),('Kyra','Snider','Lorem.ipsum@idmagna.com'),('Sharon','Singleton','tempus@Nuncquis.edu');
INSERT INTO users(firstname,lastname,email) VALUES('Mary','Cook','cursus.vestibulum@atlacus.co.uk'),('Wade','Alston','odio@eu.edu'),('Deirdre','Landry','Nullam.vitae@non.edu'),('Virginia','Chambers','at.pretium@tincidunt.com'),('Griffin','Berry','in@luctus.edu'),('Gray','Bowen','Cum.sociis@aceleifend.org'),('Blythe','Pittman','leo.Cras@Curabitur.net'),('Wynter','Sharpe','sagittis.placerat.Cras@cubiliaCurae.org'),('Inez','Bradford','sem.magna@Maecenas.edu'),('Gemma','King','pharetra@Proin.net');
INSERT INTO users(firstname,lastname,email) VALUES('Reagan','Wright','tortor.Integer@Sedpharetrafelis.net'),('Camilla','Rodgers','eros.non@Quisquenonummyipsum.edu'),('Lynn','Guerrero','vulputate.eu@dolor.com'),('Alma','Tucker','faucibus@Pellentesque.edu'),('Rosalyn','Chambers','rutrum.non.hendrerit@tortorNunccommodo.ca'),('Keith','Oneil','ac.eleifend.vitae@mus.org'),('Caldwell','Valencia','eget@dapibus.edu'),('Kimberley','Foreman','dui.Suspendisse.ac@pedenonummy.edu'),('Xandra','Melton','nonummy.Fusce@Nullamnisl.edu'),('Wylie','Holloway','ornare.Fusce@hendrerita.net');
INSERT INTO users(firstname,lastname,email) VALUES('Leroy','Kaufman','fringilla.purus.mauris@Donec.net'),('Jada','Mack','euismod.mauris@Duiselementumdui.com'),('Briar','Burch','tortor.at@afelis.net'),('Jakeem','Day','eu.erat.semper@elitpellentesquea.com'),('Rosalyn','Gilmore','egestas.hendrerit.neque@Ut.co.uk'),('Aaron','Patterson','orci@veliteget.com'),('Byron','Tillman','lorem.lorem.luctus@aliquetlibero.co.uk'),('Dean','Valentine','Fusce.fermentum@ante.co.uk'),('Macaulay','Wiggins','mollis.vitae@mattissemperdui.co.uk'),('Basil','Watkins','orci.consectetuer@sollicitudincommodo.org');
INSERT INTO users(firstname,lastname,email) VALUES('Matthew','Hickman','mauris.sapien@Maurisnulla.net'),('Teagan','Holder','justo@nibhvulputate.co.uk'),('William','Bruce','Pellentesque@ornaresagittisfelis.edu'),('Kelsey','Gould','et@sedest.org'),('Nolan','Melendez','Cras@egestasadui.ca'),('Scarlett','Rich','Quisque@Inornare.net'),('Orla','Richmond','cursus.Nunc.mauris@odioNaminterdum.edu'),('Kimberly','Munoz','Duis@Nullatempor.net'),('Mohammad','Moreno','viverra.Donec@augueeu.edu'),('Clayton','Rosario','Integer@odiosempercursus.net');
INSERT INTO users(firstname,lastname,email) VALUES('Justine','Francis','tellus@malesuada.edu'),('Fay','Walls','euismod@feugiatnonlobortis.edu'),('Salvador','Sargent','volutpat.Nulla@utmi.edu'),('Eagan','Rojas','luctus.et.ultrices@rutrumFuscedolor.org'),('Ina','Pollard','ante@iaculisquispede.ca'),('Lenore','Simon','elit.pretium@sagittisaugue.org'),('Lars','Washington','egestas@egetvolutpat.org'),('Kevin','Austin','risus@Nullamut.org'),('Dean','Brady','egestas.lacinia.Sed@duiCum.co.uk'),('Shana','Long','dis.parturient@dis.ca');
INSERT INTO users(firstname,lastname,email) VALUES('Amela','Douglas','molestie.Sed.id@aliquameros.com'),('Blake','Suarez','rutrum.Fusce@cursus.net'),('Hector','Cortez','diam@libero.edu'),('Raja','Burns','sem.eget@Pellentesqueutipsum.ca'),('Kaden','Woodard','nunc.id.enim@diam.com'),('Carissa','Mccarty','dui@Curabitur.com'),('Uma','Chapman','lacus.varius@magna.edu'),('Isabella','Garner','sagittis@Sedid.ca'),('Athena','Hopper','metus@elementumdui.ca'),('Dean','Calhoun','vitae@egestasFusce.ca');

update users set isAdmin = 1 where id = (SELECT TOP 1 id FROM users ORDER BY NEWID());
update users set isAdmin = 1 where id = (SELECT TOP 1 id FROM users ORDER BY NEWID());
update users set isAdmin = 1 where id = (SELECT TOP 1 id FROM users ORDER BY NEWID());
update users set isAdmin = 1 where id = (SELECT TOP 1 id FROM users ORDER BY NEWID());
update movies set rating_id = null where id = (SELECT TOP 1 id FROM movies ORDER BY NEWID());
update movies set rating_id = null where id = (SELECT TOP 1 id FROM movies ORDER BY NEWID());
update movies set rating_id = null where id = (SELECT TOP 1 id FROM movies ORDER BY NEWID());
update movies set rating_id = null where id = (SELECT TOP 1 id FROM movies ORDER BY NEWID());


-- This loop will cause insert errors on duplicates. Just ignore them
WHILE (SELECT count(*) FROM user_like_movie) < 300  
BEGIN  
	insert into user_like_movie (user_id, movie_id, stars, hasSeen) values ((SELECT TOP 1 id FROM users ORDER BY NEWID()),(SELECT TOP 1 id FROM movies ORDER BY NEWID()),round(1+5*rand(),0,1),(SELECT CAST(ROUND(RAND(),0) AS BIT)))
END 