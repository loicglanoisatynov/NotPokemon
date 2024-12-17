-- Création de la base de données
CREATE DATABASE ExerciceMonster;
GO
-- Utilisation de la base de données
USE ExerciceMonster;
GO
-- Table Login
CREATE TABLE Login (
 ID INT PRIMARY KEY IDENTITY(1,1),
 Username NVARCHAR(50) NOT NULL,
 PasswordHash NVARCHAR(255) NOT NULL
);
-- Table Player
CREATE TABLE Player (
 ID INT PRIMARY KEY IDENTITY(1,1),
 Name NVARCHAR(50) NOT NULL,
 LoginID INT,
 FOREIGN KEY (LoginID) REFERENCES Login(ID)
);
-- Table Monster
CREATE TABLE Monster (
 ID INT PRIMARY KEY IDENTITY(1,1),
 Name NVARCHAR(50) NOT NULL,
 Health INT NOT NULL,
 ImageURL NVARCHAR(255) NULL
);
-- Table Spell
CREATE TABLE Spell (
 ID INT PRIMARY KEY IDENTITY(1,1),
 Name NVARCHAR(50) NOT NULL,
 Damage INT NOT NULL,
 Description NVARCHAR(MAX)
);
-- Table PlayerMonster (relation Player <-> Monster)
CREATE TABLE PlayerMonster (
 PlayerID INT NOT NULL,
 MonsterID INT NOT NULL,
 PRIMARY KEY (PlayerID, MonsterID),
 FOREIGN KEY (PlayerID) REFERENCES Player(ID),
 FOREIGN KEY (MonsterID) REFERENCES Monster(ID)
);
-- Table MonsterSpell (relation Monster <-> Spell)
CREATE TABLE MonsterSpell (
 MonsterID INT NOT NULL,
 SpellID INT NOT NULL,
 PRIMARY KEY (MonsterID, SpellID),
 FOREIGN KEY (MonsterID) REFERENCES Monster(ID),
 FOREIGN KEY (SpellID) REFERENCES Spell(ID)
);