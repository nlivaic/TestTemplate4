USE master
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TestTemplate4Db')
BEGIN
  CREATE DATABASE TestTemplate4Db;
END;
GO

USE TestTemplate4Db;
GO

IF NOT EXISTS (SELECT 1
                 FROM sys.server_principals
                WHERE [name] = N'TestTemplate4Db_Login' 
                  AND [type] IN ('C','E', 'G', 'K', 'S', 'U'))
BEGIN
    CREATE LOGIN TestTemplate4Db_Login
        WITH PASSWORD = '<DB_PASSWORD>';
END;
GO  

IF NOT EXISTS (select * from sys.database_principals where name = 'TestTemplate4Db_User')
BEGIN
    CREATE USER TestTemplate4Db_User FOR LOGIN TestTemplate4Db_Login;
END;
GO  


EXEC sp_addrolemember N'db_datareader', N'TestTemplate4Db_User';
GO

EXEC sp_addrolemember N'db_datawriter', N'TestTemplate4Db_User';
GO

EXEC sp_addrolemember N'db_ddladmin', N'TestTemplate4Db_User';
GO
