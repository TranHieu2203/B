-- --------------------------------------------------
-- Entity Designer DDL Script for Oracle database
-- --------------------------------------------------
-- Date Created: 10/23/2013 8:51:04 AM
-- Generated from EDMX file: E:\TVC_F2\3.DEPLOYMENT\32.SourceCode\F2_2013\Web\PortalDAL\PortalContext.edmx
-- --------------------------------------------------

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

-- DROP TABLE "PortalModelStoreContainer"."PO_EVENT";

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PO_EVENT'
CREATE TABLE "dbo"."PO_EVENT" (
   "ID" NUMBER(38,0) NOT NULL,
   "TITLE" NVARCHAR2(255) NOT NULL,
   "DETAIL" NCLOB NOT NULL,
   "ADD_TIME" DATE NOT NULL,
   "IS_SHOW" NUMBER(5,0) NULL,
   "CREATED_DATE" DATE NOT NULL,
   "CREATED_BY" NVARCHAR2(255) NOT NULL,
   "CREATED_LOG" NVARCHAR2(255) NOT NULL,
   "MODIFIED_DATE" DATE NOT NULL,
   "MODIFIED_BY" NVARCHAR2(255) NOT NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NOT NULL
);


-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on "ID"in table 'PO_EVENT'
ALTER TABLE "dbo"."PO_EVENT"
ADD CONSTRAINT "PK_PO_EVENT"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
