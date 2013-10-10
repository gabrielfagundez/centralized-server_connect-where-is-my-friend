
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/09/2013 21:57:25
-- Generated from EDMX file: C:\Users\ENVY 14 SPECTRE\Documents\Fing\PIS\PISServer\PisDataAccess\DevelopmentEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [testingpis];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_WhereEventWhereSolicitation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_WhereEvent] DROP CONSTRAINT [FK_WhereEventWhereSolicitation];
GO
IF OBJECT_ID(N'[dbo].[FK_ConnectEventConnectSolicitation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_ConnectEvent] DROP CONSTRAINT [FK_ConnectEventConnectSolicitation];
GO
IF OBJECT_ID(N'[dbo].[FK_WhereEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_WhereEvent] DROP CONSTRAINT [FK_WhereEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_WhereSolicitation_inherits_Solicitation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SolicitationSet_WhereSolicitation] DROP CONSTRAINT [FK_WhereSolicitation_inherits_Solicitation];
GO
IF OBJECT_ID(N'[dbo].[FK_ConnectEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_ConnectEvent] DROP CONSTRAINT [FK_ConnectEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_ConnectSolicitation_inherits_Solicitation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SolicitationSet_ConnectSolicitation] DROP CONSTRAINT [FK_ConnectSolicitation_inherits_Solicitation];
GO
IF OBJECT_ID(N'[dbo].[FK_WhereSolicitationEvent_inherits_WhereEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_WhereSolicitationEvent] DROP CONSTRAINT [FK_WhereSolicitationEvent_inherits_WhereEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_WhereAcceptationEvent_inherits_WhereEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_WhereAcceptationEvent] DROP CONSTRAINT [FK_WhereAcceptationEvent_inherits_WhereEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_WhereNegationEvent_inherits_WhereEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_WhereNegationEvent] DROP CONSTRAINT [FK_WhereNegationEvent_inherits_WhereEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_ConnectSolicitationEvent_inherits_ConnectEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_ConnectSolicitationEvent] DROP CONSTRAINT [FK_ConnectSolicitationEvent_inherits_ConnectEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_ConnectAcceptationEvent_inherits_ConnectEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_ConnectAcceptationEvent] DROP CONSTRAINT [FK_ConnectAcceptationEvent_inherits_ConnectEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_ConnectNegationEvent_inherits_ConnectEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventSet_ConnectNegationEvent] DROP CONSTRAINT [FK_ConnectNegationEvent_inherits_ConnectEvent];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[EventSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet];
GO
IF OBJECT_ID(N'[dbo].[SolicitationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SolicitationSet];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[EventSet_WhereEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet_WhereEvent];
GO
IF OBJECT_ID(N'[dbo].[SolicitationSet_WhereSolicitation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SolicitationSet_WhereSolicitation];
GO
IF OBJECT_ID(N'[dbo].[EventSet_ConnectEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet_ConnectEvent];
GO
IF OBJECT_ID(N'[dbo].[SolicitationSet_ConnectSolicitation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SolicitationSet_ConnectSolicitation];
GO
IF OBJECT_ID(N'[dbo].[EventSet_WhereSolicitationEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet_WhereSolicitationEvent];
GO
IF OBJECT_ID(N'[dbo].[EventSet_WhereAcceptationEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet_WhereAcceptationEvent];
GO
IF OBJECT_ID(N'[dbo].[EventSet_WhereNegationEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet_WhereNegationEvent];
GO
IF OBJECT_ID(N'[dbo].[EventSet_ConnectSolicitationEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet_ConnectSolicitationEvent];
GO
IF OBJECT_ID(N'[dbo].[EventSet_ConnectAcceptationEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet_ConnectAcceptationEvent];
GO
IF OBJECT_ID(N'[dbo].[EventSet_ConnectNegationEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventSet_ConnectNegationEvent];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EventSet'
CREATE TABLE [dbo].[EventSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'SolicitationSet'
CREATE TABLE [dbo].[SolicitationSet] (
    [Id] int IDENTITY(1,1) NOT NULL
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

-- Creating table 'EventSet_WhereEvent'
CREATE TABLE [dbo].[EventSet_WhereEvent] (
    [Id] int  NOT NULL,
    [WhereSolicitation_Id] int  NOT NULL
);
GO

-- Creating table 'SolicitationSet_WhereSolicitation'
CREATE TABLE [dbo].[SolicitationSet_WhereSolicitation] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'EventSet_ConnectEvent'
CREATE TABLE [dbo].[EventSet_ConnectEvent] (
    [Id] int  NOT NULL,
    [ConnectSolicitation_Id] int  NOT NULL
);
GO

-- Creating table 'SolicitationSet_ConnectSolicitation'
CREATE TABLE [dbo].[SolicitationSet_ConnectSolicitation] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'EventSet_WhereSolicitationEvent'
CREATE TABLE [dbo].[EventSet_WhereSolicitationEvent] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'EventSet_WhereAcceptationEvent'
CREATE TABLE [dbo].[EventSet_WhereAcceptationEvent] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'EventSet_WhereNegationEvent'
CREATE TABLE [dbo].[EventSet_WhereNegationEvent] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'EventSet_ConnectSolicitationEvent'
CREATE TABLE [dbo].[EventSet_ConnectSolicitationEvent] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'EventSet_ConnectAcceptationEvent'
CREATE TABLE [dbo].[EventSet_ConnectAcceptationEvent] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'EventSet_ConnectNegationEvent'
CREATE TABLE [dbo].[EventSet_ConnectNegationEvent] (
    [Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'SolicitationSet'
ALTER TABLE [dbo].[SolicitationSet]
ADD CONSTRAINT [PK_SolicitationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventSet_WhereEvent'
ALTER TABLE [dbo].[EventSet_WhereEvent]
ADD CONSTRAINT [PK_EventSet_WhereEvent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SolicitationSet_WhereSolicitation'
ALTER TABLE [dbo].[SolicitationSet_WhereSolicitation]
ADD CONSTRAINT [PK_SolicitationSet_WhereSolicitation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventSet_ConnectEvent'
ALTER TABLE [dbo].[EventSet_ConnectEvent]
ADD CONSTRAINT [PK_EventSet_ConnectEvent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SolicitationSet_ConnectSolicitation'
ALTER TABLE [dbo].[SolicitationSet_ConnectSolicitation]
ADD CONSTRAINT [PK_SolicitationSet_ConnectSolicitation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventSet_WhereSolicitationEvent'
ALTER TABLE [dbo].[EventSet_WhereSolicitationEvent]
ADD CONSTRAINT [PK_EventSet_WhereSolicitationEvent]
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

-- Creating primary key on [Id] in table 'EventSet_ConnectSolicitationEvent'
ALTER TABLE [dbo].[EventSet_ConnectSolicitationEvent]
ADD CONSTRAINT [PK_EventSet_ConnectSolicitationEvent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventSet_ConnectAcceptationEvent'
ALTER TABLE [dbo].[EventSet_ConnectAcceptationEvent]
ADD CONSTRAINT [PK_EventSet_ConnectAcceptationEvent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventSet_ConnectNegationEvent'
ALTER TABLE [dbo].[EventSet_ConnectNegationEvent]
ADD CONSTRAINT [PK_EventSet_ConnectNegationEvent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [WhereSolicitation_Id] in table 'EventSet_WhereEvent'
ALTER TABLE [dbo].[EventSet_WhereEvent]
ADD CONSTRAINT [FK_WhereEventWhereSolicitation]
    FOREIGN KEY ([WhereSolicitation_Id])
    REFERENCES [dbo].[SolicitationSet_WhereSolicitation]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WhereEventWhereSolicitation'
CREATE INDEX [IX_FK_WhereEventWhereSolicitation]
ON [dbo].[EventSet_WhereEvent]
    ([WhereSolicitation_Id]);
GO

-- Creating foreign key on [ConnectSolicitation_Id] in table 'EventSet_ConnectEvent'
ALTER TABLE [dbo].[EventSet_ConnectEvent]
ADD CONSTRAINT [FK_ConnectEventConnectSolicitation]
    FOREIGN KEY ([ConnectSolicitation_Id])
    REFERENCES [dbo].[SolicitationSet_ConnectSolicitation]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ConnectEventConnectSolicitation'
CREATE INDEX [IX_FK_ConnectEventConnectSolicitation]
ON [dbo].[EventSet_ConnectEvent]
    ([ConnectSolicitation_Id]);
GO

-- Creating foreign key on [Id] in table 'EventSet_WhereEvent'
ALTER TABLE [dbo].[EventSet_WhereEvent]
ADD CONSTRAINT [FK_WhereEvent_inherits_Event]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EventSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'SolicitationSet_WhereSolicitation'
ALTER TABLE [dbo].[SolicitationSet_WhereSolicitation]
ADD CONSTRAINT [FK_WhereSolicitation_inherits_Solicitation]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[SolicitationSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'EventSet_ConnectEvent'
ALTER TABLE [dbo].[EventSet_ConnectEvent]
ADD CONSTRAINT [FK_ConnectEvent_inherits_Event]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EventSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'SolicitationSet_ConnectSolicitation'
ALTER TABLE [dbo].[SolicitationSet_ConnectSolicitation]
ADD CONSTRAINT [FK_ConnectSolicitation_inherits_Solicitation]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[SolicitationSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'EventSet_WhereSolicitationEvent'
ALTER TABLE [dbo].[EventSet_WhereSolicitationEvent]
ADD CONSTRAINT [FK_WhereSolicitationEvent_inherits_WhereEvent]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EventSet_WhereEvent]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'EventSet_WhereAcceptationEvent'
ALTER TABLE [dbo].[EventSet_WhereAcceptationEvent]
ADD CONSTRAINT [FK_WhereAcceptationEvent_inherits_WhereEvent]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EventSet_WhereEvent]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'EventSet_WhereNegationEvent'
ALTER TABLE [dbo].[EventSet_WhereNegationEvent]
ADD CONSTRAINT [FK_WhereNegationEvent_inherits_WhereEvent]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EventSet_WhereEvent]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'EventSet_ConnectSolicitationEvent'
ALTER TABLE [dbo].[EventSet_ConnectSolicitationEvent]
ADD CONSTRAINT [FK_ConnectSolicitationEvent_inherits_ConnectEvent]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EventSet_ConnectEvent]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'EventSet_ConnectAcceptationEvent'
ALTER TABLE [dbo].[EventSet_ConnectAcceptationEvent]
ADD CONSTRAINT [FK_ConnectAcceptationEvent_inherits_ConnectEvent]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EventSet_ConnectEvent]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'EventSet_ConnectNegationEvent'
ALTER TABLE [dbo].[EventSet_ConnectNegationEvent]
ADD CONSTRAINT [FK_ConnectNegationEvent_inherits_ConnectEvent]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EventSet_ConnectEvent]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------