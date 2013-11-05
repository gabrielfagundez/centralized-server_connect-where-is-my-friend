
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/04/2013 20:37:26
-- Generated from EDMX file: C:\Users\Gabriel\Documents\GitHub\PISServer\PisDataAccess\DevelopmentEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [serverdevelopmentpis];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserSession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SessionSet] DROP CONSTRAINT [FK_UserSession];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserPosition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserPositionSet] DROP CONSTRAINT [FK_UserUserPosition];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet] DROP CONSTRAINT [FK_UserEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserUser] DROP CONSTRAINT [FK_UserUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser_User1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserUser] DROP CONSTRAINT [FK_UserUser_User1];
GO
IF OBJECT_ID(N'[dbo].[FK_WhereSolicitationWhereAcceptationEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_WhereAcceptationEvent] DROP CONSTRAINT [FK_WhereSolicitationWhereAcceptationEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_WhereSolicitationWhereNegationEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_WhereNegationEvent] DROP CONSTRAINT [FK_WhereSolicitationWhereNegationEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_WhereSolicitationWhereSolicitationEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_WhereSolicitationEvent] DROP CONSTRAINT [FK_WhereSolicitationWhereSolicitationEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_ShareUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShareSet] DROP CONSTRAINT [FK_ShareUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserShare]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShareSet] DROP CONSTRAINT [FK_UserShare];
GO
IF OBJECT_ID(N'[dbo].[FK_WhereAcceptationEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_WhereAcceptationEvent] DROP CONSTRAINT [FK_WhereAcceptationEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_WhereNegationEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_WhereNegationEvent] DROP CONSTRAINT [FK_WhereNegationEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_WhereSolicitationEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_WhereSolicitationEvent] DROP CONSTRAINT [FK_WhereSolicitationEvent_inherits_Event];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[EventSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[SessionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SessionSet];
GO
IF OBJECT_ID(N'[dbo].[UserPositionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserPositionSet];
GO
IF OBJECT_ID(N'[dbo].[WhereSolicitationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WhereSolicitationSet];
GO
IF OBJECT_ID(N'[dbo].[ShareSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShareSet];
GO
IF OBJECT_ID(N'[dbo].[PermissionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PermissionSet];
GO
IF OBJECT_ID(N'[dbo].[EventSet_WhereAcceptationEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet_WhereAcceptationEvent];
GO
IF OBJECT_ID(N'[dbo].[EventSet_WhereNegationEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet_WhereNegationEvent];
GO
IF OBJECT_ID(N'[dbo].[EventSet_WhereSolicitationEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet_WhereSolicitationEvent];
GO
IF OBJECT_ID(N'[dbo].[UserUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserUser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EventSet'
CREATE TABLE [dbo].[EventSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FacebookId] nvarchar(max)  NOT NULL,
    [LinkedInId] nvarchar(max)  NOT NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SessionSet'
CREATE TABLE [dbo].[SessionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DeviceId] nvarchar(max)  NOT NULL,
    [Platform] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [Active] bit  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'UserPositionSet'
CREATE TABLE [dbo].[UserPositionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Latitude] nvarchar(max)  NOT NULL,
    [Longitude] nvarchar(max)  NOT NULL,
    [User_Id] int  NULL
);
GO

-- Creating table 'WhereSolicitationSet'
CREATE TABLE [dbo].[WhereSolicitationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [From] int  NOT NULL,
    [For] int  NOT NULL
);
GO

-- Creating table 'ShareSet'
CREATE TABLE [dbo].[ShareSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [UserId1] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'PermissionSet'
CREATE TABLE [dbo].[PermissionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Platform] nvarchar(max)  NOT NULL,
    [Content] varbinary(max)  NOT NULL
);
GO

-- Creating table 'EventSet_WhereAcceptationEvent'
CREATE TABLE [dbo].[EventSet_WhereAcceptationEvent] (
    [Sent] bit  NOT NULL,
    [Id] int  NOT NULL,
    [WhereSolicitation_Id] int  NULL
);
GO

-- Creating table 'EventSet_WhereNegationEvent'
CREATE TABLE [dbo].[EventSet_WhereNegationEvent] (
    [Sent] bit  NOT NULL,
    [Id] int  NOT NULL,
    [WhereSolicitation_Id] int  NULL
);
GO

-- Creating table 'EventSet_WhereSolicitationEvent'
CREATE TABLE [dbo].[EventSet_WhereSolicitationEvent] (
    [Sent] bit  NOT NULL,
    [Id] int  NOT NULL,
    [WhereSolicitation_Id] int  NULL
);
GO

-- Creating table 'UserUser'
CREATE TABLE [dbo].[UserUser] (
    [FriendsFrom_Id] int  NOT NULL,
    [FriendsOf_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'EventSet'
ALTER TABLE [dbo].[EventSet]
ADD CONSTRAINT [PK_EventSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SessionSet'
ALTER TABLE [dbo].[SessionSet]
ADD CONSTRAINT [PK_SessionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserPositionSet'
ALTER TABLE [dbo].[UserPositionSet]
ADD CONSTRAINT [PK_UserPositionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WhereSolicitationSet'
ALTER TABLE [dbo].[WhereSolicitationSet]
ADD CONSTRAINT [PK_WhereSolicitationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ShareSet'
ALTER TABLE [dbo].[ShareSet]
ADD CONSTRAINT [PK_ShareSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PermissionSet'
ALTER TABLE [dbo].[PermissionSet]
ADD CONSTRAINT [PK_PermissionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventSet_WhereAcceptationEvent'
ALTER TABLE [dbo].[EventSet_WhereAcceptationEvent]
ADD CONSTRAINT [PK_EventSet_WhereAcceptationEvent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventSet_WhereNegationEvent'
ALTER TABLE [dbo].[EventSet_WhereNegationEvent]
ADD CONSTRAINT [PK_EventSet_WhereNegationEvent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventSet_WhereSolicitationEvent'
ALTER TABLE [dbo].[EventSet_WhereSolicitationEvent]
ADD CONSTRAINT [PK_EventSet_WhereSolicitationEvent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [FriendsFrom_Id], [FriendsOf_Id] in table 'UserUser'
ALTER TABLE [dbo].[UserUser]
ADD CONSTRAINT [PK_UserUser]
    PRIMARY KEY NONCLUSTERED ([FriendsFrom_Id], [FriendsOf_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'SessionSet'
ALTER TABLE [dbo].[SessionSet]
ADD CONSTRAINT [FK_UserSession]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserSession'
CREATE INDEX [IX_FK_UserSession]
ON [dbo].[SessionSet]
    ([UserId]);
GO

-- Creating foreign key on [User_Id] in table 'UserPositionSet'
ALTER TABLE [dbo].[UserPositionSet]
ADD CONSTRAINT [FK_UserUserPosition]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserPosition'
CREATE INDEX [IX_FK_UserUserPosition]
ON [dbo].[UserPositionSet]
    ([User_Id]);
GO

-- Creating foreign key on [UserId] in table 'EventSet'
ALTER TABLE [dbo].[EventSet]
ADD CONSTRAINT [FK_UserEvent]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEvent'
CREATE INDEX [IX_FK_UserEvent]
ON [dbo].[EventSet]
    ([UserId]);
GO

-- Creating foreign key on [FriendsFrom_Id] in table 'UserUser'
ALTER TABLE [dbo].[UserUser]
ADD CONSTRAINT [FK_UserUser_User]
    FOREIGN KEY ([FriendsFrom_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FriendsOf_Id] in table 'UserUser'
ALTER TABLE [dbo].[UserUser]
ADD CONSTRAINT [FK_UserUser_User1]
    FOREIGN KEY ([FriendsOf_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser_User1'
CREATE INDEX [IX_FK_UserUser_User1]
ON [dbo].[UserUser]
    ([FriendsOf_Id]);
GO

-- Creating foreign key on [WhereSolicitation_Id] in table 'EventSet_WhereAcceptationEvent'
ALTER TABLE [dbo].[EventSet_WhereAcceptationEvent]
ADD CONSTRAINT [FK_WhereSolicitationWhereAcceptationEvent]
    FOREIGN KEY ([WhereSolicitation_Id])
    REFERENCES [dbo].[WhereSolicitationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WhereSolicitationWhereAcceptationEvent'
CREATE INDEX [IX_FK_WhereSolicitationWhereAcceptationEvent]
ON [dbo].[EventSet_WhereAcceptationEvent]
    ([WhereSolicitation_Id]);
GO

-- Creating foreign key on [WhereSolicitation_Id] in table 'EventSet_WhereNegationEvent'
ALTER TABLE [dbo].[EventSet_WhereNegationEvent]
ADD CONSTRAINT [FK_WhereSolicitationWhereNegationEvent]
    FOREIGN KEY ([WhereSolicitation_Id])
    REFERENCES [dbo].[WhereSolicitationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WhereSolicitationWhereNegationEvent'
CREATE INDEX [IX_FK_WhereSolicitationWhereNegationEvent]
ON [dbo].[EventSet_WhereNegationEvent]
    ([WhereSolicitation_Id]);
GO

-- Creating foreign key on [WhereSolicitation_Id] in table 'EventSet_WhereSolicitationEvent'
ALTER TABLE [dbo].[EventSet_WhereSolicitationEvent]
ADD CONSTRAINT [FK_WhereSolicitationWhereSolicitationEvent]
    FOREIGN KEY ([WhereSolicitation_Id])
    REFERENCES [dbo].[WhereSolicitationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WhereSolicitationWhereSolicitationEvent'
CREATE INDEX [IX_FK_WhereSolicitationWhereSolicitationEvent]
ON [dbo].[EventSet_WhereSolicitationEvent]
    ([WhereSolicitation_Id]);
GO

-- Creating foreign key on [UserId] in table 'ShareSet'
ALTER TABLE [dbo].[ShareSet]
ADD CONSTRAINT [FK_ShareUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ShareUser'
CREATE INDEX [IX_FK_ShareUser]
ON [dbo].[ShareSet]
    ([UserId]);
GO

-- Creating foreign key on [UserId1] in table 'ShareSet'
ALTER TABLE [dbo].[ShareSet]
ADD CONSTRAINT [FK_UserShare]
    FOREIGN KEY ([UserId1])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserShare'
CREATE INDEX [IX_FK_UserShare]
ON [dbo].[ShareSet]
    ([UserId1]);
GO

-- Creating foreign key on [Id] in table 'EventSet_WhereAcceptationEvent'
ALTER TABLE [dbo].[EventSet_WhereAcceptationEvent]
ADD CONSTRAINT [FK_WhereAcceptationEvent_inherits_Event]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EventSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'EventSet_WhereNegationEvent'
ALTER TABLE [dbo].[EventSet_WhereNegationEvent]
ADD CONSTRAINT [FK_WhereNegationEvent_inherits_Event]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EventSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'EventSet_WhereSolicitationEvent'
ALTER TABLE [dbo].[EventSet_WhereSolicitationEvent]
ADD CONSTRAINT [FK_WhereSolicitationEvent_inherits_Event]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EventSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------