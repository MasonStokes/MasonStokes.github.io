--Users table
CREATE TABLE [dbo].[Users]
(
	[ID]				INT IDENTITY (1,1)		NOT NULL,
	[FirstName]			NVARCHAR(64)			NOT NULL,
	[LastName]			NVARCHAR(64)			NOT NULL,
	[Phone]				NVARCHAR(64)			NOT NULL,
	[ApartmentName]		NVARCHAR(64)			NOT NULL,
	[UnitNumber]		INT						NOT NULL,
	[Explanation]		VARCHAR(364)			NOT Null,
	[Permission]		BIT,
	CONTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[Users] (FirstName, LastName, PhoneNumber, ApartmentName, UnitNumber, Explanation, Permission) VALUES
	('Bil', 'Bo', '111-111-1111', 'The Prancing Pony', 1, 'Blah', 0),
	('Fro', 'Do', '222-222-2222', 'The Prancing Pony', 3, 'Blah', 0),
	('Rick', 'Astley', '333-333-3333', 'The Prancing Pony', 4, 'Blah', 1),
	('Some', 'Guy', '444-444-4444', 'The Prancing Pony', 5, 'Blah', 0),
	('Ron', 'Burgundy', '555-555-5555', 'The Prancing Pony', 6, 'Blah', 0)
GO

--Select * from dbo.Users