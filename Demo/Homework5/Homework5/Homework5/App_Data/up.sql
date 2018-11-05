--Users table
CREATE TABLE [dbo].[Users]
(
	[ID]				INT IDENTITY (1,1)		NOT NULL,
	[FirstName]			NVARCHAR(64)			NOT NULL,
	[LastName]			NVARCHAR(128)			NOT NULL,
	[PhoneNumber]		NVARCHAR(64)			NOT NULL,
	[ApartmentName]		NVARCHAR(128)			NOT NULL,
	[UnitNumber]		INT						NOT NULL,
	[Explanation]		VARCHAR(512)			NOT NULL,
	[Permission]		BIT,
	[SubmitTime]		DATETIME				NOT NULL,
	CONTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO

INSERT INTO [dbo].[Users] (FirstName, LastName, PhoneNumber, ApartmentName, UnitNumber, Explanation, Permission, SubmitTime) VALUES
	('Bil', 'Bo', '111-111-1111', 'The Prancing Pony', 1, 'Blah', 0, '2018-10-10 01:00:00'),
	('Fro', 'Do', '222-222-2222', 'The Prancing Pony', 3, 'Blah', 0, '2018-10-11 02:00:00'),
	('Rick', 'Astley', '333-333-3333', 'The Prancing Pony', 4, 'Blah', 1, '2018-10-12 03:00:00'),
	('Some', 'Guy', '444-444-4444', 'The Prancing Pony', 5, 'Blah', 0, '2018-10-14 04:00:00'),
	('Ron', 'Burgundy', '555-555-5555', 'The Prancing Pony', 6, 'Blah', 0, '2018-10-15 05:00:00')


--Select * from dbo.Users