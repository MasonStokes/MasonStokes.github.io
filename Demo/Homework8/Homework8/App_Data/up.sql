
CREATE TABLE [dbo].[Buyers]
(
	[ID]			INT IDENTITY (1,1)							NOT NULL,
	[NAME]			NVARCHAR(100)								NOT NULL

	CONSTRAINT [PK_dbo.Buyers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Sellers]
(
	[ID]			INT IDENTITY (1,1)							NOT NULL,
	[NAME]			NVARCHAR(100)								NOT NULL

	CONSTRAINT [PK_dbo.Sellers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Items]
(
	[ID]			INT IDENTITY (1001,1)						NOT NULL,
	[NAME]			NVARCHAR(100)								NOT NULL,
	[DESCRIPTION]	NVARCHAR(MAX)								NOT NULL,
	[SellerID]		INT FOREIGN KEY REFERENCES Sellers(ID) ON DELETE CASCADE ON UPDATE CASCADE		NOT NULL

	CONSTRAINT [PK_dbo.Items] PRIMARY KEY CLUSTERED ([ID] ASC),
);

CREATE TABLE [dbo].[Bids]
(
	[ID]			INT IDENTITY (1, 1)							NOT NULL,
	[ITEMID]		INT	FOREIGN KEY REFERENCES Items(ID) ON DELETE CASCADE ON UPDATE CASCADE		NOT NULL,
	[BUYERID]		INT	FOREIGN KEY REFERENCES Buyers(ID) ON DELETE CASCADE ON UPDATE CASCADE		NOT NULL,
	[PRICE]			INT											NOT NULL,
	[TIMESTAMP]		DATETIME									NOT NULL

	CONSTRAINT [PK_dbo.Bids] PRIMARY KEY CLUSTERED ([ID] ASC)
	
);

INSERT INTO [dbo].[Buyers] (NAME) VALUES
	('Jane Stone'),
	('Tom McMasters'),
	('Otto Vanderwall')

INSERT INTO [dbo].[Sellers](NAME) VALUES
	('Gayle Hardy'),
	('Lyle Banks'),
	('Pearl Greene')

INSERT INTO [dbo].[Items](NAME, DESCRIPTION, SellerID) VALUES
	('Abraham Lincoln Hammer','A bench mallet fashioned from a broken rail-splitting maul in 1829 and owned by Abraham Lincoln', 3),
	('Albert Einsteins Telescope','A brass telescope owned by Albert Einstein in Germany, circa 1927', 1),
	('Bob Dylan Love Poems','Five versions of an original unpublished, handwritten, love poem by Bob Dylan', 2)

INSERT INTO [dbo].[Bids](ITEMID, BUYERID, PRICE, TIMESTAMP) VALUES
	(1001, 3, 250000,'12/04/2017 09:04:22'),
	(1003, 1, 95000 ,'12/04/2017 08:44:03')

GO