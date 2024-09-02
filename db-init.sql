USE [master]
GO

-- Verificar si la base de datos 'Investment' existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Investment')
BEGIN
    CREATE DATABASE Investment
    PRINT 'Base de datos Investment creada.'
END
ELSE
BEGIN
    PRINT 'La base de datos Investment ya existe.'
END
GO

-- Verificar si el login 'srv_user_investment' existe
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'srv_user_investment')
BEGIN
    CREATE LOGIN srv_user_investment WITH PASSWORD=N'Pass1234', DEFAULT_DATABASE=[Investment], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
    PRINT 'Login srv_user_investment creado.'
END
ELSE
BEGIN
    PRINT 'El login srv_user_investment ya existe.'
END
GO

USE [Investment]
GO

-- Verificar si el usuario 'srv_user_investment' existe
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'srv_user_investment')
BEGIN
    CREATE USER srv_user_investment FOR LOGIN srv_user_investment
    ALTER USER srv_user_investment WITH DEFAULT_SCHEMA=[dbo]
    ALTER ROLE [db_owner] ADD MEMBER srv_user_investment
    PRINT 'Usuario srv_user_investment creado y asignado al rol db_owner.'
END
ELSE
BEGIN
    PRINT 'El usuario srv_user_investment ya existe.'
END
GO

-- Verificar si la tabla 'Portfolios' existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Portfolios' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON

    CREATE TABLE [dbo].[Portfolios](
        [Id] [bigint] IDENTITY(1,1) NOT NULL,
        [Name] [nvarchar](450) NOT NULL,
        [Enabled] [bit] NOT NULL,
     CONSTRAINT [PK_Portfolio_Id] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    PRINT 'Tabla Portfolios creada.'
END
ELSE
BEGIN
    PRINT 'La tabla Portfolios ya existe.'
END
GO
