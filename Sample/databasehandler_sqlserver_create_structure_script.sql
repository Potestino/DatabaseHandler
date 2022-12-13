USE [database]
GO
----------------------------------------------------------
--DATABASEHANDLER WITH IDENTITY
----------------------------------------------------------

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DBH_Tb_With_Identity'))
BEGIN
    DROP TABLE [dbo].[DBH_Tb_With_Identity]
END

CREATE TABLE [dbo].[DBH_Tb_With_Identity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[Value] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

----------------------------------------------------------
--DATABASEHANDLER WITHOUT IDENTITY (AND WITH GUID AS PK)
----------------------------------------------------------
IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DBH_Tb_Without_Identity'))
BEGIN
    DROP TABLE [dbo].[DBH_Tb_Without_Identity]
END

CREATE TABLE [dbo].[DBH_Tb_Without_Identity](
	[Guid] [varchar](100) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[Value] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


