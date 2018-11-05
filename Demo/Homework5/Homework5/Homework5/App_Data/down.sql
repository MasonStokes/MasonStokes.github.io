--Take the User table down
IF EXISTS(
	SELECT *
	FROM [dbo].[Users]
	)
	DROP TABLE [dbo].[User]
Go