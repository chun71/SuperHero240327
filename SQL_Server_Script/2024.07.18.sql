
USE [SuperHero];

CREATE TABLE [Character] (
  [ID] BIGINT PRIMARY KEY IDENTITY,
  [Name] NVARCHAR(50) NOT NULL,
  [FirstName] NVARCHAR(50) NOT NULL,
  [LastName] NVARCHAR(50) NOT NULL, 
  [Place] NVARCHAR(50),
  [CreateTime] DATETIME NOT NULL,
  [UpdateTime] DATETIME
);

INSERT INTO [Character] ([Name], [FirstName] , [LastName], [Place], [CreateTime]) VALUES ('Andy', 'Andy', 'Liao', 'S', SYSDATETIME());
INSERT INTO [Character] ([Name], [FirstName] , [LastName], [Place], [CreateTime]) VALUES ('Kevin', 'Kevin', 'Lee', 'C', SYSDATETIME());
INSERT INTO [Character] ([Name], [FirstName] , [LastName], [CreateTime]) VALUES ('Sam', 'Sam', 'Wu', SYSDATETIME());
INSERT INTO [Character] ([Name], [FirstName] , [LastName], [Place], [CreateTime]) VALUES ('Benson', 'Benson', 'Liu', 'A',SYSDATETIME());

CREATE TABLE [CharacterLog] (
  [CharacterID] BIGINT NOT NULL,
  [Name] NVARCHAR(50) NOT NULL,
  [FirstName] NVARCHAR(50) NOT NULL,
  [LastName] NVARCHAR(50) NOT NULL, 
  [Place] NVARCHAR(50),
  [Action] NVARCHAR(5) NOT NULL,
  [CreateTime] DATETIME PRIMARY KEY
);
