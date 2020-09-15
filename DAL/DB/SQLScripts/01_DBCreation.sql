USE [master]
GO

IF EXISTS(SELECT * FROM sys.databases WHERE NAME='ResultSession')
ALTER DATABASE ResultSession SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE ResultSession
GO

CREATE DATABASE ResultSession
GO