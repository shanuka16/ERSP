
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/06/2023 15:05:32
-- Generated from EDMX file: C:\Users\Administrator\Desktop\firefox downloads\QuizApp\QuizApp\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [QuizApp];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__tbl_categ__cat_f__239E4DCF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_category] DROP CONSTRAINT [FK__tbl_categ__cat_f__239E4DCF];
GO
IF OBJECT_ID(N'[dbo].[FK__tbl_quest__q_fk___276EDEB3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_questions] DROP CONSTRAINT [FK__tbl_quest__q_fk___276EDEB3];
GO
IF OBJECT_ID(N'[dbo].[FK__tbl_setEx__exam___1ED998B2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_setExam] DROP CONSTRAINT [FK__tbl_setEx__exam___1ED998B2];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[student]', 'U') IS NOT NULL
    DROP TABLE [dbo].[student];
GO
IF OBJECT_ID(N'[dbo].[tbl_admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_admin];
GO
IF OBJECT_ID(N'[dbo].[tbl_category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_category];
GO
IF OBJECT_ID(N'[dbo].[tbl_questions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_questions];
GO
IF OBJECT_ID(N'[dbo].[tbl_setExam]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_setExam];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'students'
CREATE TABLE [dbo].[students] (
    [std_id] int IDENTITY(1,1) NOT NULL,
    [std_name] nvarchar(20)  NOT NULL,
    [std_password] nvarchar(20)  NOT NULL,
    [std_image] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'tbl_admin'
CREATE TABLE [dbo].[tbl_admin] (
    [ad_id] int IDENTITY(1,1) NOT NULL,
    [ad_name] nvarchar(20)  NOT NULL,
    [ad_pass] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'tbl_questions'
CREATE TABLE [dbo].[tbl_questions] (
    [q_id] int IDENTITY(1,1) NOT NULL,
    [q_text] nvarchar(max)  NOT NULL,
    [QA] nvarchar(100)  NOT NULL,
    [QB] nvarchar(100)  NOT NULL,
    [QC] nvarchar(100)  NOT NULL,
    [QD] nvarchar(100)  NOT NULL,
    [QCorrectAns] nvarchar(100)  NOT NULL,
    [q_fk_catid] int  NULL
);
GO

-- Creating table 'tbl_setExam'
CREATE TABLE [dbo].[tbl_setExam] (
    [examId] int IDENTITY(1,1) NOT NULL,
    [exam_date] datetime  NULL,
    [exam_fk_stu] int  NULL,
    [exam_name] nvarchar(20)  NOT NULL,
    [std_score] int  NULL,
    [exam_fk_cat__id] int  NULL
);
GO

-- Creating table 'tbl_category'
CREATE TABLE [dbo].[tbl_category] (
    [cat_id] int IDENTITY(1,1) NOT NULL,
    [cat_name] nvarchar(50)  NOT NULL,
    [cat_fk_ad_id] int  NULL,
    [cat_encrytped_string] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [std_id] in table 'students'
ALTER TABLE [dbo].[students]
ADD CONSTRAINT [PK_students]
    PRIMARY KEY CLUSTERED ([std_id] ASC);
GO

-- Creating primary key on [ad_id] in table 'tbl_admin'
ALTER TABLE [dbo].[tbl_admin]
ADD CONSTRAINT [PK_tbl_admin]
    PRIMARY KEY CLUSTERED ([ad_id] ASC);
GO

-- Creating primary key on [q_id] in table 'tbl_questions'
ALTER TABLE [dbo].[tbl_questions]
ADD CONSTRAINT [PK_tbl_questions]
    PRIMARY KEY CLUSTERED ([q_id] ASC);
GO

-- Creating primary key on [examId] in table 'tbl_setExam'
ALTER TABLE [dbo].[tbl_setExam]
ADD CONSTRAINT [PK_tbl_setExam]
    PRIMARY KEY CLUSTERED ([examId] ASC);
GO

-- Creating primary key on [cat_id] in table 'tbl_category'
ALTER TABLE [dbo].[tbl_category]
ADD CONSTRAINT [PK_tbl_category]
    PRIMARY KEY CLUSTERED ([cat_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [exam_fk_stu] in table 'tbl_setExam'
ALTER TABLE [dbo].[tbl_setExam]
ADD CONSTRAINT [FK__tbl_setEx__exam___1ED998B2]
    FOREIGN KEY ([exam_fk_stu])
    REFERENCES [dbo].[students]
        ([std_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__tbl_setEx__exam___1ED998B2'
CREATE INDEX [IX_FK__tbl_setEx__exam___1ED998B2]
ON [dbo].[tbl_setExam]
    ([exam_fk_stu]);
GO

-- Creating foreign key on [cat_fk_ad_id] in table 'tbl_category'
ALTER TABLE [dbo].[tbl_category]
ADD CONSTRAINT [FK__tbl_categ__cat_f__239E4DCF]
    FOREIGN KEY ([cat_fk_ad_id])
    REFERENCES [dbo].[tbl_admin]
        ([ad_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__tbl_categ__cat_f__239E4DCF'
CREATE INDEX [IX_FK__tbl_categ__cat_f__239E4DCF]
ON [dbo].[tbl_category]
    ([cat_fk_ad_id]);
GO

-- Creating foreign key on [q_fk_catid] in table 'tbl_questions'
ALTER TABLE [dbo].[tbl_questions]
ADD CONSTRAINT [FK__tbl_quest__q_fk___276EDEB3]
    FOREIGN KEY ([q_fk_catid])
    REFERENCES [dbo].[tbl_category]
        ([cat_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__tbl_quest__q_fk___276EDEB3'
CREATE INDEX [IX_FK__tbl_quest__q_fk___276EDEB3]
ON [dbo].[tbl_questions]
    ([q_fk_catid]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------