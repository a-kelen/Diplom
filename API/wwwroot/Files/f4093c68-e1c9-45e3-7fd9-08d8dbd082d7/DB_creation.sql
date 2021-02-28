
DROP DATABASE IF EXISTS db;
CREATE DATABASE db;
 
USE db;

 
CREATE TABLE AspNetUsers(
	Id Char(36) NOT NULL,
	Avatar Longtext NULL,
	Lastname Longtext NULL,
	Firstname Longtext NULL,
	Created Datetime(6) NOT NULL,
	UserName nvarchar(256) NULL,
	NormalizedUserName nvarchar(256) NULL,
	Email nvarchar(256) NULL,
	NormalizedEmail nvarchar(256) NULL,
	EmailConfirmed Tinyint NOT NULL,
	PasswordHash Longtext NULL,
	SecurityStamp Longtext NULL,
	ConcurrencyStamp Longtext NULL,
	PhoneNumber Longtext NULL,
	PhoneNumberConfirmed Tinyint NOT NULL,
	TwoFactorEnabled Tinyint NOT NULL,
	LockoutEnd Datetime(6) NULL,
	LockoutEnabled Tinyint NOT NULL,
	AccessFailedCount int NOT NULL,
 CONSTRAINT PK_AspNetUsers PRIMARY KEY 
(
	Id ASC
) 
); 

 
CREATE TABLE ComponentReports(
	Id Char(36) NOT NULL,
	UserId Char(36) NOT NULL,
	Content Longtext NULL,
	ComponentId Char(36) NOT NULL,
	Created Datetime(6) NOT NULL,
 CONSTRAINT PK_ComponentReports PRIMARY KEY 
(
	Id ASC
) 
);

CREATE TABLE Components(
	Id Char(36) NOT NULL,
	UserId Char(36) NULL,
	Name Longtext NULL,
	Status Tinyint NOT NULL,
	File Longtext NULL,
	Deleted Tinyint NOT NULL,
	LibraryId Char(36) NULL,
	Description Longtext NULL,
	Created Datetime(6) NOT NULL,
 CONSTRAINT PK_Components PRIMARY KEY 
(
	Id ASC
) 
);
 
CREATE TABLE Events(
	Id Char(36) NOT NULL,
	Name Longtext NULL,
	ComponentId Char(36) NOT NULL,
	Description Longtext NULL,
 CONSTRAINT PK_Events PRIMARY KEY 
(
	Id ASC
) 
); 
 
CREATE TABLE Files(
	Id Char(36) NOT NULL,
	Path Longtext NULL,
	ComponentId Char(36) NOT NULL,
 CONSTRAINT PK_Files PRIMARY KEY 
(
	Id ASC
) 
); 
 
CREATE TABLE Followers(
	Id Char(36) NOT NULL,
	PersonId Char(36) NOT NULL,
	UserId Char(36) NOT NULL,
	Created Datetime(6) NOT NULL,
 CONSTRAINT PK_Followers PRIMARY KEY 
(
	Id ASC
) 
);

 
CREATE TABLE Libraries(
	Id Char(36) NOT NULL,
	UserId Char(36) NOT NULL,
	Name Longtext NULL,
	Status Tinyint NOT NULL,
	File Longtext NULL,
	Deleted Tinyint NOT NULL,
	Description Longtext NULL,
	Created Datetime(6) NOT NULL,
 CONSTRAINT PK_Libraries PRIMARY KEY 
(
	Id ASC
) 
); 
 
CREATE TABLE LibraryReports(
	Id Char(36) NOT NULL,
	LibraryId Char(36) NOT NULL,
	Content Longtext NULL,
	UserId Char(36) NOT NULL,
	Created Datetime(6) NOT NULL,
 CONSTRAINT PK_LibraryReports PRIMARY KEY 
(
	Id ASC
) 
); 
 
CREATE TABLE OwnedComponent(
	Id Char(36) NOT NULL,
	ComponentId Char(36) NOT NULL,
	UserId Char(36) NOT NULL,
	Created Datetime(6) NOT NULL,
 CONSTRAINT PK_OwnedComponent PRIMARY KEY 
(
	Id ASC
) 
);

 
CREATE TABLE OwnedLibrary(
	Id Char(36) NOT NULL,
	LibraryId Char(36) NOT NULL,
	UserId Char(36) NOT NULL,
	Created Datetime(6) NOT NULL,
 CONSTRAINT PK_OwnedLibrary PRIMARY KEY 
(
	Id ASC
) 
);

 
CREATE TABLE Props(
	Id Char(36) NOT NULL,
	Name Longtext NULL,
	ComponentId Char(36) NOT NULL,
	Description Longtext NULL,
 CONSTRAINT PK_Props PRIMARY KEY 
(
	Id ASC
) 
); 
 
CREATE TABLE UserReports(
	Id Char(36) NOT NULL,
	Content Longtext NULL,
	PersonId Char(36) NOT NULL,
	UserId Char(36) NOT NULL,
	Created Datetime(6) NOT NULL,
 CONSTRAINT PK_UserReports PRIMARY KEY 
(
	Id ASC
) 
);

ALTER TABLE AspNetUsers CHANGE Created Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE Components CHANGE Created Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE Libraries CHANGE Created Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE ComponentReports 
ADD CONSTRAINT FK_ComponentReports_AspNetUsers_UserId
FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;



ALTER TABLE ComponentReports
ADD CONSTRAINT FK_ComponentReports_Components_ComponentId
FOREIGN KEY (ComponentId) REFERENCES Components (Id) ON DELETE CASCADE;


ALTER TABLE Components
ADD CONSTRAINT FK_Components_AspNetUsers_UserId
FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ;


ALTER TABLE Components
ADD CONSTRAINT FK_Components_Libraries_LibraryId
FOREIGN KEY (LibraryId) REFERENCES Libraries (Id);


ALTER TABLE Events
ADD CONSTRAINT FK_Events_Components_ComponentId
FOREIGN KEY (ComponentId) REFERENCES Components (Id) ON DELETE CASCADE;

ALTER TABLE Files
ADD CONSTRAINT FK_Files_Components_ComponentId
FOREIGN KEY (ComponentId) REFERENCES Components (Id);

ALTER TABLE Followers
ADD CONSTRAINT FK_Followers_AspNetUsers_PersonId
FOREIGN KEY (PersonId) REFERENCES AspNetUsers (Id);

ALTER TABLE Followers
ADD CONSTRAINT FK_Followers_AspNetUsers_UserId
FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;

ALTER TABLE Libraries
ADD CONSTRAINT FK_Libraries_AspNetUsers_UserId
FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;

ALTER TABLE LibraryReports
ADD CONSTRAINT FK_LibraryReports_AspNetUsers_UserId
FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;

ALTER TABLE LibraryReports
ADD CONSTRAINT FK_LibraryReports_Libraries_LibraryId
FOREIGN KEY (LibraryId) REFERENCES Libraries (Id) ;

ALTER TABLE OwnedComponent
ADD CONSTRAINT FK_OwnedComponent_AspNetUsers_UserId
FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;

ALTER TABLE OwnedComponent
ADD CONSTRAINT FK_OwnedComponent_Components_ComponentId
FOREIGN KEY (ComponentId) REFERENCES Components (Id) ;

ALTER TABLE OwnedLibrary
ADD CONSTRAINT FK_OwnedLibrary_AspNetUsers_UserId
FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;

ALTER TABLE OwnedLibrary
ADD CONSTRAINT FK_OwnedLibrary_Libraries_LibraryId
FOREIGN KEY (LibraryId) REFERENCES Libraries (Id) ;

ALTER TABLE Props
ADD CONSTRAINT FK_Props_Components_ComponentId
FOREIGN KEY (ComponentId) REFERENCES Components (Id) ON DELETE CASCADE;

ALTER TABLE UserReports
ADD CONSTRAINT FK_UserReports_AspNetUsers_PersonId
FOREIGN KEY (PersonId) REFERENCES AspNetUsers (Id) ;

ALTER TABLE UserReports
ADD CONSTRAINT FK_UserReports_AspNetUsers_UserId
FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE;


