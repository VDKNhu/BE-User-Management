CREATE DATABASE USERMANAGEMENT;

CREATE TABLE title (
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(50)
);

CREATE TABLE web_user (
	id INT PRIMARY KEY IDENTITY(1,1),
	first_name NVARCHAR(100) NOT NULL,
	last_name NVARCHAR(100) NOT NULL,
	date_of_birth DATE,
	gender SMALLINT, 
	-- 1: female, 2: male, 3: other
	company NVARCHAR(10) DEFAULT('ROSEN'),
	title INT FOREIGN KEY REFERENCES title(id),
	email VARCHAR(100) NOT NULL,
	avatar VARCHAR(1000)
);

SET DATEFORMAT MDY;

INSERT INTO title VALUES ('Team lead');
INSERT INTO title VALUES ('Architecture');
INSERT INTO title VALUES ('Web Developer');
INSERT INTO title VALUES ('Tester');
INSERT INTO title VALUES ('UI/UX');
INSERT INTO title VALUES ('DBA');

INSERT INTO web_user VALUES (N'Kim', N'Như', '12-30-2001', 1, 'ROSEN', 3, 'kimnhu@gmail.com', '');
INSERT INTO web_user VALUES (N'Trúc', N'Linh', '12-30-2002', 1, 'ROSEN', 1, 'truclinh@gmail.com', '');
INSERT INTO web_user VALUES (N'Thiên', N'Ân', '09-10-2002', 2, 'ROSEN', 2, 'thienan@gmail.com', '');
INSERT INTO web_user VALUES (N'An', N'Nhiên', '12-30-2000', 1, 'ROSEN', 4, 'annhien@gmail.com', '');
INSERT INTO web_user VALUES (N'Minh', N'Thư', '01-10-2001', 1, 'ROSEN', 5, 'minhthu@gmail.com', '');


SELECT * FROM title;
SELECT * FROM web_user;

