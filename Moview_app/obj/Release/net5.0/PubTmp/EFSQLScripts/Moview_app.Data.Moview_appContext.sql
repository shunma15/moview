IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210614150905_Initial')
BEGIN
    CREATE TABLE [Account] (
        [ID] int NOT NULL IDENTITY,
        [Username] nvarchar(max) NOT NULL,
        [Age] nvarchar(max) NULL,
        [Gender] nvarchar(max) NULL,
        [Pass] nvarchar(max) NOT NULL,
        [RememberMe] bit NULL,
        CONSTRAINT [PK_Account] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210614150905_Initial')
BEGIN
    CREATE TABLE [Assessment] (
        [ID] int NOT NULL IDENTITY,
        [MovieID] int NOT NULL,
        [AccountID] int NOT NULL,
        [Username] nvarchar(max) NULL,
        [Posting] nvarchar(max) NULL,
        [Value] int NOT NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_Assessment] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210614150905_Initial')
BEGIN
    CREATE TABLE [Movie] (
        [ID] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Genre] nvarchar(max) NOT NULL,
        [Actor] nvarchar(max) NULL,
        [Director] nvarchar(max) NULL,
        CONSTRAINT [PK_Movie] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210614150905_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210614150905_Initial', N'5.0.6');
END;
GO

COMMIT;
GO

