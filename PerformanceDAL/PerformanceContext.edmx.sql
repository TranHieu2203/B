-- --------------------------------------------------
-- Entity Designer DDL Script for Oracle database
-- --------------------------------------------------
-- Date Created: 14/02/2022 14:14:37
-- Generated from EDMX file: D:\TVC\HSV_1\PerformanceDAL\PerformanceContext.edmx
-- --------------------------------------------------

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

-- DROP TABLE "PerformanceModelStoreContainer"."HUV_PERFORMANCE_KPI";

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PE_CRITERIA'
CREATE TABLE "dbo"."PE_CRITERIA" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(50) NULL,
   "NAME" NVARCHAR2(255) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "UNIT_ID" NUMBER(38,0) NULL,
   "FREQUENCY_ID" NUMBER(38,0) NULL,
   "SOURCE_ID" NUMBER(38,0) NULL
);

-- Creating table 'PE_OBJECT_GROUP'
CREATE TABLE "dbo"."PE_OBJECT_GROUP" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(50) NULL,
   "NAME" NVARCHAR2(255) NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL
);

-- Creating table 'PE_OBJECT_GROUP_PERIOD'
CREATE TABLE "dbo"."PE_OBJECT_GROUP_PERIOD" (
   "ID" NUMBER(38,0) NOT NULL,
   "OBJECT_GROUP_ID" NUMBER(38,0) NOT NULL,
   "PERIOD_ID" NUMBER(38,0) NOT NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL
);

-- Creating table 'HU_EMPLOYEE'
CREATE TABLE "dbo"."HU_EMPLOYEE" (
   "ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE_CODE" NVARCHAR2(15) NULL,
   "FIRST_NAME_VN" NVARCHAR2(255) NULL,
   "LAST_NAME_VN" NVARCHAR2(255) NULL,
   "FULLNAME_VN" NVARCHAR2(255) NULL,
   "FIRST_NAME_EN" VARCHAR2(255) NULL,
   "LAST_NAME_EN" NVARCHAR2(255) NULL,
   "FULLNAME_EN" NVARCHAR2(255) NULL,
   "ORG_ID" NUMBER(38,0) NULL,
   "WORK_STATUS" NUMBER(38,0) NULL,
   "CONTRACT_ID" NUMBER(38,0) NULL,
   "TITLE_ID" NUMBER(38,0) NULL,
   "JOIN_DATE" DATE NULL,
   "DIRECT_MANAGER" NUMBER(38,0) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "LAST_WORKING_ID" NUMBER(38,0) NULL,
   "TER_EFFECT_DATE" DATE NULL,
   "TER_LAST_DATE" DATE NULL,
   "ITIME_ID" NUMBER(38,0) NULL,
   "STAFF_RANK_ID" NUMBER(38,0) NULL,
   "PA_OBJECT_SALARY_ID" NUMBER(38,0) NULL,
   "EXPIREDATE_ENT" DATE NULL,
   "JOIN_DATE_STATE" DATE NULL,
   "LEVEL_MANAGER" NUMBER(38,0) NULL,
   "EMPLOYEE_3B_ID" NUMBER(38,0) NULL,
   "EMPLOYEE_CODE_OLD" NVARCHAR2(15) NULL,
   "IS_3B" NUMBER(5,0) NULL
);

-- Creating table 'HU_EMPLOYEE_CV'
CREATE TABLE "dbo"."HU_EMPLOYEE_CV" (
   "EMPLOYEE_ID" NUMBER(38,0) NOT NULL,
   "GENDER" NUMBER(38,0) NULL,
   "IMAGE" NVARCHAR2(255) NULL,
   "BIRTH_DATE" DATE NULL,
   "BIRTH_PLACE" NVARCHAR2(255) NULL,
   "MARITAL_STATUS" NUMBER(38,0) NULL,
   "RELIGION" NUMBER(38,0) NULL,
   "NATIVE" NUMBER(38,0) NULL,
   "NATIONALITY" NUMBER(38,0) NULL,
   "PER_ADDRESS" NVARCHAR2(255) NULL,
   "PER_PROVINCE" NUMBER(38,0) NULL,
   "HOME_PHONE" NVARCHAR2(255) NULL,
   "MOBILE_PHONE" NVARCHAR2(255) NULL,
   "ID_NO" NVARCHAR2(15) NULL,
   "ID_DATE" DATE NULL,
   "PASS_NO" NVARCHAR2(255) NULL,
   "PASS_DATE" DATE NULL,
   "PASS_EXPIRE" DATE NULL,
   "PASS_PLACE" NVARCHAR2(255) NULL,
   "VISA" NVARCHAR2(255) NULL,
   "VISA_DATE" DATE NULL,
   "VISA_EXPIRE" DATE NULL,
   "VISA_PLACE" NVARCHAR2(255) NULL,
   "WORK_PERMIT" NVARCHAR2(255) NULL,
   "WORK_PERMIT_DATE" DATE NULL,
   "WORK_PERMIT_EXPIRE" DATE NULL,
   "WORK_EMAIL" NVARCHAR2(255) NULL,
   "NAV_ADDRESS" NVARCHAR2(255) NULL,
   "NAV_PROVINCE" NUMBER(38,0) NULL,
   "PIT_CODE" VARCHAR2(255) NULL,
   "PER_EMAIL" NVARCHAR2(255) NULL,
   "CONTACT_PER" NVARCHAR2(255) NULL,
   "CONTACT_PER_PHONE" NVARCHAR2(15) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "ID_PLACE" NVARCHAR2(255) NULL,
   "WORK_PERMIT_PLACE" NVARCHAR2(255) NULL,
   "BANK_ID" NUMBER(38,0) NULL,
   "BANK_BRANCH_ID" NUMBER(38,0) NULL,
   "BANK_NO" NVARCHAR2(255) NULL,
   "NGAY_VAO_DOAN" DATE NULL,
   "NOI_VAO_DOAN" NVARCHAR2(255) NULL,
   "CHUC_VU_DOAN" NVARCHAR2(255) NULL,
   "DOAN_PHI" NUMBER(5,0) NULL,
   "NGAY_VAO_DANG" DATE NULL,
   "NOI_VAO_DANG" NVARCHAR2(255) NULL,
   "CHUC_VU_DANG" NVARCHAR2(255) NULL,
   "DANG_PHI" NUMBER(5,0) NULL,
   "CAREER" NVARCHAR2(255) NULL,
   "PER_DISTRICT" NUMBER(38,0) NULL,
   "NAV_DISTRICT" NUMBER(38,0) NULL,
   "PER_WARD" NUMBER(38,0) NULL,
   "NAV_WARD" NUMBER(38,0) NULL,
   "WORKPLACE_ID" NUMBER(38,0) NULL,
   "IS_PERMISSION" NUMBER(5,0) NULL,
   "INS_REGION_ID" NUMBER(38,0) NULL
);

-- Creating table 'HU_ORG_TITLE'
CREATE TABLE "dbo"."HU_ORG_TITLE" (
   "ID" NUMBER(38,0) NOT NULL,
   "ORG_ID" NUMBER(38,0) NULL,
   "TITLE_ID" NUMBER(38,0) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL
);

-- Creating table 'HU_ORGANIZATION'
CREATE TABLE "dbo"."HU_ORGANIZATION" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(50) NULL,
   "NAME_EN" NVARCHAR2(255) NULL,
   "NAME_VN" NVARCHAR2(255) NULL,
   "PARENT_ID" NUMBER(38,0) NULL,
   "LEVEL_ID" NUMBER(38,0) NULL,
   "DISSOLVE_DATE" DATE NULL,
   "FOUNDATION_DATE" DATE NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "HIERARCHICAL_PATH" VARCHAR2(1023) NULL,
   "DESCRIPTION_PATH" NVARCHAR2(1023) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "ADDRESS" NVARCHAR2(1023) NULL,
   "FAX" NVARCHAR2(255) NULL,
   "REPRESENTATIVE_ID" NUMBER(38,0) NULL,
   "MOBILE" NVARCHAR2(255) NULL,
   "PROVINCE_NAME" NVARCHAR2(50) NULL,
   "COST_CENTER_CODE" NVARCHAR2(50) NULL,
   "ORD_NO" NUMBER(38,0) NULL
);

-- Creating table 'HU_TITLE'
CREATE TABLE "dbo"."HU_TITLE" (
   "ID" NUMBER(38,0) NOT NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "CODE" NVARCHAR2(50) NULL,
   "NAME_EN" NVARCHAR2(255) NULL,
   "NAME_VN" NVARCHAR2(255) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "TITLE_GROUP_ID" NUMBER(38,0) NULL
);

-- Creating table 'SE_CHOSEN_ORG'
CREATE TABLE "dbo"."SE_CHOSEN_ORG" (
   "ORG_ID" NUMBER(38,0) NOT NULL,
   "USERNAME" NVARCHAR2(255) NOT NULL
);

-- Creating table 'HU_STAFF_RANK'
CREATE TABLE "dbo"."HU_STAFF_RANK" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(50) NULL,
   "NAME" NVARCHAR2(255) NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "LEVEL_STAFF" NUMBER(38,0) NULL
);

-- Creating table 'HUV_ORGANIZATION'
CREATE TABLE "dbo"."HUV_ORGANIZATION" (
   "ID" NUMBER(38,0) NOT NULL,
   "NAME_VN" NVARCHAR2(255) NULL,
   "ORG_NAME" NVARCHAR2(255) NULL,
   "PARENT_ID" NUMBER(38,0) NULL,
   "CODE" NVARCHAR2(50) NULL,
   "ADDRESS" NVARCHAR2(1023) NULL,
   "DESCRIPTION_PATH" NVARCHAR2(1023) NULL,
   "COST_CENTER_CODE" NVARCHAR2(50) NULL,
   "PROVINCE_NAME" NVARCHAR2(50) NULL,
   "ORG_NAME1" NVARCHAR2(1999) NULL,
   "ORG_NAME2" NVARCHAR2(1999) NULL,
   "ORG_NAME3" NVARCHAR2(1999) NULL,
   "ORG_NAME4" NVARCHAR2(1999) NULL,
   "ORG_NAME5" NVARCHAR2(1999) NULL,
   "ORG_NAME6" NVARCHAR2(1999) NULL,
   "ORG_NAME7" NVARCHAR2(1999) NULL,
   "ORG_NAME8" NVARCHAR2(1999) NULL,
   "ORG_NAME9" NVARCHAR2(1999) NULL,
   "ORG_ID1" NUMBER(38,0) NULL,
   "ORG_ID2" NUMBER(38,0) NULL,
   "ORG_ID3" NUMBER(38,0) NULL,
   "ORG_ID4" NUMBER(38,0) NULL,
   "ORG_ID5" NUMBER(38,0) NULL,
   "ORG_ID6" NUMBER(38,0) NULL,
   "ORG_ID7" NUMBER(38,0) NULL,
   "ORG_ID8" NUMBER(38,0) NULL,
   "ORG_ID9" NUMBER(38,0) NULL,
   "ORG_CODE1" NVARCHAR2(1999) NULL,
   "ORG_CODE2" NVARCHAR2(1999) NULL,
   "ORG_CODE3" NVARCHAR2(1999) NULL,
   "ORG_CODE4" NVARCHAR2(1999) NULL,
   "ORG_CODE5" NVARCHAR2(1999) NULL,
   "ORG_CODE6" NVARCHAR2(1999) NULL,
   "ORG_CODE7" NVARCHAR2(1999) NULL,
   "ORG_CODE8" NVARCHAR2(1999) NULL,
   "ORG_CODE9" NVARCHAR2(1999) NULL,
   "ORG_COST_CENTER1" NVARCHAR2(1999) NULL,
   "ORG_COST_CENTER2" NVARCHAR2(1999) NULL,
   "ORG_COST_CENTER3" NVARCHAR2(1999) NULL,
   "ORG_COST_CENTER4" NVARCHAR2(1999) NULL,
   "ORG_COST_CENTER5" NVARCHAR2(1999) NULL,
   "ORG_COST_CENTER6" NVARCHAR2(1999) NULL,
   "ORG_COST_CENTER7" NVARCHAR2(1999) NULL,
   "ORG_COST_CENTER8" NVARCHAR2(1999) NULL,
   "ORG_COST_CENTER9" NVARCHAR2(1999) NULL,
   "ORG_PATH" NVARCHAR2(2000) NULL,
   "ORD_NO" NUMBER(38,0) NULL,
   "DISSOLVE_DATE" DATE NULL,
   "ACTFLG" NVARCHAR2(1) NULL
);

-- Creating table 'PE_PERSONAL'
CREATE TABLE "dbo"."PE_PERSONAL" (
   "ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NOT NULL,
   "PERIOD_ID" NUMBER(38,0) NOT NULL,
   "CRITERIA_GROUP_ID" NUMBER(38,0) NOT NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL,
   "OBJECT_GROUP_ID" NUMBER(38,0) NULL,
   "MARK" NUMBER(38,0) NULL,
   "RATING" NVARCHAR2(255) NULL
);

-- Creating table 'PE_EMPLOYEE_ASSESSMENT_DTL'
CREATE TABLE "dbo"."PE_EMPLOYEE_ASSESSMENT_DTL" (
   "ID" NUMBER(38,0) NOT NULL,
   "PE_EMPLOYEE_ASSESSMENT_ID" NUMBER(38,0) NOT NULL,
   "CRITERIA_ID" NUMBER(38,0) NOT NULL,
   "POINT" NUMBER(38,0) NOT NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL,
   "PERIOD_ID" NUMBER(38,0) NULL,
   "GROUP_OBJ_ID" NUMBER(38,0) NULL,
   "CLASSIFICATION" NVARCHAR2(255) NULL
);

-- Creating table 'PE_ORGANIZATION'
CREATE TABLE "dbo"."PE_ORGANIZATION" (
   "ID" NUMBER(38,0) NOT NULL,
   "PERIOD_ID" NUMBER(38,0) NULL,
   "ORG_ID" NUMBER(38,0) NULL,
   "RESULT" NVARCHAR2(255) NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL
);

-- Creating table 'PE_CLASSIFICATION'
CREATE TABLE "dbo"."PE_CLASSIFICATION" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(10) NULL,
   "NAME" NVARCHAR2(100) NULL,
   "VALUE_FROM" NUMBER(5,0) NULL,
   "VALUE_TO" NUMBER(5,0) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "EFFECT_DATE" DATE NULL
);

-- Creating table 'OT_OTHER_LIST'
CREATE TABLE "dbo"."OT_OTHER_LIST" (
   "ID" NUMBER(38,0) NOT NULL,
   "TYPE_ID" NUMBER(38,0) NULL,
   "NAME_VN" NVARCHAR2(255) NULL,
   "NAME_EN" NVARCHAR2(255) NULL,
   "ATTRIBUTE1" NVARCHAR2(255) NULL,
   "ATTRIBUTE2" NVARCHAR2(255) NULL,
   "ATTRIBUTE3" NVARCHAR2(255) NULL,
   "ATTRIBUTE4" NVARCHAR2(255) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "CODE" NVARCHAR2(50) NULL,
   "TYPE_CODE" NVARCHAR2(50) NULL,
   "ORDERBYID" NUMBER(38,0) NULL,
   "REMARK" NVARCHAR2(1023) NULL
);

-- Creating table 'PE_ASSESSMENT'
CREATE TABLE "dbo"."PE_ASSESSMENT" (
   "ID" NUMBER(38,0) NOT NULL,
   "PE_EMPLOYEE_ASSESSMENT_ID" NUMBER(38,0) NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NULL,
   "PE_PERIO_ID" NUMBER(38,0) NULL,
   "PE_OBJECT_ID" NUMBER(38,0) NULL,
   "PE_CRITERIA_ID" NUMBER(38,0) NULL,
   "RESULT" NVARCHAR2(1023) NULL,
   "STATUS_EMP_ID" NUMBER(38,0) NULL,
   "DIRECT_ID" NUMBER(38,0) NULL,
   "UPDATE_DATE" DATE NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "RESULT_DIRECT" NVARCHAR2(1023) NULL,
   "ASS_DATE" DATE NULL,
   "REMARK_DIRECT" NVARCHAR2(1023) NULL,
   "RESULT_CONVERT" NUMBER(38,0) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "STATUS_DIRECT" NUMBER(38,0) NULL,
   "RESULT_CONVERT_QL" NUMBER(38,0) NULL
);

-- Creating table 'PE_EMPLOYEE_ASSESSMENT'
CREATE TABLE "dbo"."PE_EMPLOYEE_ASSESSMENT" (
   "ID" NUMBER(38,0) NOT NULL,
   "OBJECT_GROUP_ID" NUMBER(38,0) NOT NULL,
   "PERIOD_ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NOT NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL,
   "PE_STATUS" NUMBER(38,0) NULL
);

-- Creating table 'PE_CRITERIA_OBJECT_GROUP'
CREATE TABLE "dbo"."PE_CRITERIA_OBJECT_GROUP" (
   "ID" NUMBER(38,0) NOT NULL,
   "OBJECT_GROUP_ID" NUMBER(38,0) NOT NULL,
   "CRITERIA_ID" NUMBER(38,0) NOT NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL,
   "EXPENSE" NVARCHAR2(100) NULL,
   "AMONG" NUMBER(38,0) NULL,
   "FROM_DATE" DATE NULL,
   "TO_DATE" DATE NULL
);

-- Creating table 'PE_PERIOD'
CREATE TABLE "dbo"."PE_PERIOD" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(50) NULL,
   "NAME" NVARCHAR2(255) NULL,
   "START_DATE" DATE NULL,
   "END_DATE" DATE NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL,
   "TYPE_ASS" NUMBER(38,0) NULL,
   "YEAR" NUMBER(38,0) NULL,
   "EMPLOYEE_TYPE" NUMBER(38,0) NULL,
   "NUMBER_DAY" NUMBER(38,0) NULL,
   "NUMBER_MONTH" NUMBER(38,0) NULL
);

-- Creating table 'PE_KPI_ASSESSMENT'
CREATE TABLE "dbo"."PE_KPI_ASSESSMENT" (
   "ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE" NUMBER(38,0) NULL,
   "YEAR" NUMBER(38,0) NULL,
   "PE_PERIOD_ID" NUMBER(38,0) NULL,
   "START_DATE" DATE NULL,
   "END_DATE" DATE NULL,
   "NUMBER_MONTH" NUMBER(38,0) NULL,
   "GOAL" NCLOB NULL,
   "EVALUATION_POINTS" NCLOB NULL,
   "CLASSIFICATION" NCLOB NULL,
   "REMARK" NCLOB NULL,
   "REASON" NCLOB NULL,
   "STATUS_ID" NUMBER(38,0) NULL,
   "PORTAL_ID" NUMBER(38,0) NULL,
   "EFFECT_DATE" DATE NULL,
   "IS_CONFIRM" NUMBER(38,0) NULL,
   "REASON_CONFIRM" NCLOB NULL,
   "RATIO" NUMBER(38,0) NULL
);

-- Creating table 'PE_KPI_ASSESSMENT_DETAIL'
CREATE TABLE "dbo"."PE_KPI_ASSESSMENT_DETAIL" (
   "ID" NUMBER(38,0) NOT NULL,
   "GOAL_ID" NUMBER(38,0) NULL,
   "KPI_ASSESSMENT" NUMBER(38,0) NULL,
   "KPI_ASSESSMENT_TEXT" NCLOB NULL,
   "DVT" NUMBER(38,0) NULL,
   "DESCRIPTION" NCLOB NULL,
   "FREQUENCY" NUMBER(38,0) NULL,
   "SOURCE" NUMBER(38,0) NULL,
   "GOAL_TYPE" NUMBER(38,0) NULL,
   "RATIO" NUMBER(38,0) NULL,
   "TARGET" NCLOB NULL,
   "TARGET_MIN" NCLOB NULL,
   "BENCHMARK" NUMBER(38,0) NULL,
   "EMPLOYEE_ACTUAL" NCLOB NULL,
   "EMPLOYEE_POINTS" NUMBER(38,0) NULL,
   "DIRECT_ACTUAL" NCLOB NULL,
   "DIRECT_POINTS" NUMBER(38,0) NULL,
   "TARGET_TYPE" NUMBER(38,0) NULL,
   "NOTE" NCLOB NULL,
   "NOTE_QLTT" NCLOB NULL,
   "SOURCE_TEXT" NCLOB NULL
);

-- Creating table 'PE_ASSESSMENT_DETAIL_HISTORY'
CREATE TABLE "dbo"."PE_ASSESSMENT_DETAIL_HISTORY" (
   "ID" NUMBER(38,0) NOT NULL,
   "KPI_ASSESSMENT_ID" NUMBER(38,0) NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_DATE" DATE NULL,
   "EMPLOYEE_ACTUAL" NCLOB NULL,
   "EMPLOYEE_POINTS" NCLOB NULL,
   "NOTE_NV" NCLOB NULL,
   "DIRECT_ACTUAL" NCLOB NULL,
   "DIRECT_POINTS" NCLOB NULL,
   "NOTE_QLTT" NCLOB NULL
);

-- Creating table 'PA_PAYMENT_LIST'
CREATE TABLE "dbo"."PA_PAYMENT_LIST" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(50) NOT NULL,
   "OBJ_PAYMENT_ID" NUMBER(38,0) NOT NULL,
   "EFFECTIVE_DATE" DATE NOT NULL,
   "VALUE" NUMBER(38,0) NULL,
   "SDESC" NVARCHAR2(1000) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "NAME" NVARCHAR2(255) NULL,
   "ORDERS" NUMBER(38,0) NULL
);

-- Creating table 'TR_SETTING_CRI_COURSE'
CREATE TABLE "dbo"."TR_SETTING_CRI_COURSE" (
   "ID" NUMBER(38,0) NOT NULL,
   "TR_COURSE_ID" NUMBER(38,0) NULL,
   "TR_CRITERIA_GROUP_ID" NUMBER(38,0) NULL,
   "EFFECT_FROM" DATE NULL,
   "EFFECT_TO" DATE NULL,
   "REMARK" NVARCHAR2(1000) NULL
);

-- Creating table 'TR_CRITERIA_GROUP'
CREATE TABLE "dbo"."TR_CRITERIA_GROUP" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(50) NULL,
   "NAME" NVARCHAR2(255) NULL,
   "POINT_MAX" NUMBER(38,0) NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL,
   "ACTFLG" NUMBER(5,0) NULL
);

-- Creating table 'TR_COURSE'
CREATE TABLE "dbo"."TR_COURSE" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(50) NULL,
   "NAME" NVARCHAR2(255) NULL,
   "TR_CERTIFICATE_ID" NUMBER(38,0) NULL,
   "ACTFLG" NUMBER(5,0) NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL,
   "TR_CER_GROUP_ID" NUMBER(38,0) NULL,
   "TR_TRAIN_FIELD" NUMBER(38,0) NULL,
   "TR_PROGRAM_GROUP" NUMBER(38,0) NULL,
   "DRIVER" NUMBER(5,0) NULL,
   "REMARK" NVARCHAR2(255) NULL
);

-- Creating table 'PE_CRITERIA_HTCH'
CREATE TABLE "dbo"."PE_CRITERIA_HTCH" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(10) NULL,
   "NAME" NVARCHAR2(100) NULL,
   "DESCRIPTION" NVARCHAR2(100) NULL,
   "IS_CHECK" NUMBER(38,0) NULL,
   "CREATED_DATE" DATE NULL,
   "IS_KYLUAT" NUMBER(38,0) NULL
);

-- Creating table 'PE_PERIOD_HTCH'
CREATE TABLE "dbo"."PE_PERIOD_HTCH" (
   "ID" NUMBER(38,0) NOT NULL,
   "YEAR" NUMBER(38,0) NULL,
   "CODE" NVARCHAR2(100) NULL,
   "NAME" NVARCHAR2(100) NULL,
   "START_DATE" DATE NULL,
   "END_DATE" DATE NULL,
   "REMARK" NCLOB NULL,
   "CREATED_DATE" DATE NULL,
   "MONTH" NUMBER(38,0) NULL
);

-- Creating table 'PE_CLASSIFICATION_HTCH'
CREATE TABLE "dbo"."PE_CLASSIFICATION_HTCH" (
   "ID" NUMBER(38,0) NOT NULL,
   "CODE" NVARCHAR2(10) NULL,
   "NAME" NVARCHAR2(100) NULL,
   "VALUE_FROM" NUMBER(5,0) NULL,
   "VALUE_TO" NUMBER(5,0) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "EFFECT_DATE" DATE NULL
);

-- Creating table 'PE_CRITERIA_TITLEGROUP'
CREATE TABLE "dbo"."PE_CRITERIA_TITLEGROUP" (
   "ID" NUMBER(38,0) NOT NULL,
   "EFFECT_DATE" DATE NULL,
   "NOTE" NVARCHAR2(1024) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "GROUPTITLE_ID" NUMBER(38,0) NULL,
   "BRAND_ID" NUMBER(38,0) NULL
);

-- Creating table 'PE_CRITERIA_TITLEGROUP_DETAIL'
CREATE TABLE "dbo"."PE_CRITERIA_TITLEGROUP_DETAIL" (
   "ID" NUMBER(38,0) NOT NULL,
   "CRITERIA_TITLEGROUP_ID" NUMBER(38,0) NULL,
   "CRITERIA_ID" NUMBER(38,0) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "RATIO" NUMBER(38,5) NULL
);

-- Creating table 'PE_EMPLOYEE_PERIOD'
CREATE TABLE "dbo"."PE_EMPLOYEE_PERIOD" (
   "ID" NUMBER(38,0) NOT NULL,
   "PE_PERIOD_ID" NUMBER(38,0) NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NULL,
   "ORG_ID" NUMBER(38,0) NULL,
   "TITLE_ID" NUMBER(38,0) NULL,
   "JOIN_DATE" DATE NULL,
   "JOIN_DATE_STATE" DATE NULL,
   "MONTH_NUMBER" NUMBER(38,0) NULL,
   "SEND_MAIL" NUMBER(38,0) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL
);

-- Creating table 'PROCESS_APPROVED_STATUS'
CREATE TABLE "dbo"."PROCESS_APPROVED_STATUS" (
   "ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NULL,
   "EMPLOYEE_APPROVED" NUMBER(38,0) NULL,
   "APP_DATE" DATE NULL,
   "APP_LEVEL" NUMBER(38,0) NULL,
   "APP_NOTES" NVARCHAR2(1000) NULL,
   "CREATED_BY" NVARCHAR2(100) NULL,
   "CREATED_DATE" DATE NULL,
   "APP_STATUS" NUMBER(38,0) NULL,
   "PE_PERIOD_ID" NUMBER(38,0) NULL,
   "ID_REGGROUP" NUMBER(38,0) NULL,
   "TEMPLATE_ID" NUMBER(38,0) NULL,
   "PROCESS_TYPE" NVARCHAR2(100) NULL
);

-- Creating table 'PE_EMPLOYEEHTCH_PERIOD'
CREATE TABLE "dbo"."PE_EMPLOYEEHTCH_PERIOD" (
   "ID" NUMBER(38,0) NOT NULL,
   "PE_PERIOD_ID" NUMBER(38,0) NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NULL,
   "ORG_ID" NUMBER(38,0) NULL,
   "TITLE_ID" NUMBER(38,0) NULL,
   "JOIN_DATE" DATE NULL,
   "JOIN_DATE_STATE" DATE NULL,
   "CHANGESALARY_DATE" DATE NULL,
   "SALARY" NUMBER(38,0) NULL,
   "MONTH_NUMBER" NUMBER(38,0) NULL,
   "SEND_MAIL" NUMBER(38,0) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "DIRECT_MANAGER" NUMBER(38,0) NULL
);

-- Creating table 'PE_CRITERIA_TITLEGROUP_RANK'
CREATE TABLE "dbo"."PE_CRITERIA_TITLEGROUP_RANK" (
   "ID" NUMBER(38,0) NOT NULL,
   "CRITERIA_ID" NUMBER(38,0) NULL,
   "GROUPTITLE_ID" NUMBER(38,0) NULL,
   "EFFECT_DATE" DATE NULL,
   "NOTE" NVARCHAR2(1000) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL
);

-- Creating table 'PE_CRITERIA_TITLEGROUP_RANK_DTL'
CREATE TABLE "dbo"."PE_CRITERIA_TITLEGROUP_RANK_DTL" (
   "ID" NUMBER(38,0) NOT NULL,
   "CRITERIA_TITLEGROUP_ID" NUMBER(38,0) NULL,
   "RANK_FROM" NUMBER(38,0) NULL,
   "RANK_TO" NUMBER(38,0) NULL,
   "POINT" NUMBER(38,0) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NCLOB NULL,
   "CREATED_LOG" NCLOB NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NCLOB NULL,
   "MODIFIED_LOG" NCLOB NULL,
   "DESCRIPTION" NCLOB NULL
);

-- Creating table 'PE_HTCH_ASSESSMENT_DETAIL'
CREATE TABLE "dbo"."PE_HTCH_ASSESSMENT_DETAIL" (
   "ID" NUMBER(38,0) NOT NULL,
   "HTCH_ASSESSMENT_ID" NUMBER(38,0) NULL,
   "CRITERIA_ID" NUMBER(38,0) NULL,
   "RATIO" NUMBER(38,0) NULL,
   "POINTS_ACTUAL" NUMBER(38,0) NULL,
   "RESULT_ACTUAL" NCLOB NULL,
   "POINTS_FINAL" NUMBER(38,0) NULL,
   "NOTE" NCLOB NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL
);

-- Creating table 'PE_HTCH_ASSESSMENT'
CREATE TABLE "dbo"."PE_HTCH_ASSESSMENT" (
   "ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NULL,
   "YEAR" NUMBER(38,0) NULL,
   "PERIOD_ID" NUMBER(38,0) NULL,
   "START_DATE" DATE NULL,
   "END_DATE" DATE NULL,
   "EVALUATION_POINTS" NVARCHAR2(255) NULL,
   "CLASSIFICATION" NVARCHAR2(255) NULL,
   "STRENGTH_NOTE" NVARCHAR2(255) NULL,
   "IMPROVE_NOT" NVARCHAR2(255) NULL,
   "PROSPECT_NOTE" NVARCHAR2(255) NULL,
   "BRANCH_EVALUATE" NVARCHAR2(255) NULL,
   "REMARK" NVARCHAR2(255) NULL,
   "REASON" NVARCHAR2(255) NULL,
   "STATUS_ID" NUMBER(38,0) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "ORG_ID" NUMBER(38,0) NULL,
   "TITLE_ID" NUMBER(38,0) NULL,
   "TITLEGROUP_ID" NUMBER(38,0) NULL,
   "BRAND_ID" NUMBER(38,0) NULL
);

-- Creating table 'PE_ORG_MR_RR'
CREATE TABLE "dbo"."PE_ORG_MR_RR" (
   "ID" NUMBER(38,0) NOT NULL,
   "PERIOD_ID" NUMBER(38,0) NULL,
   "ORG_ID" NUMBER(38,0) NULL,
   "MR_THE" NUMBER(38,0) NULL,
   "MR_LE" NUMBER(38,0) NULL,
   "RR" NUMBER(38,0) NULL,
   "CREATED_BY" NVARCHAR2(50) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(1023) NULL,
   "MODIFIED_BY" NVARCHAR2(50) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(1023) NULL
);

-- Creating table 'AT_PERIOD'
CREATE TABLE "dbo"."AT_PERIOD" (
   "YEAR" NUMBER(38,0) NULL,
   "MONTH" NUMBER(38,0) NULL,
   "PERIOD_NAME" NVARCHAR2(255) NULL,
   "START_DATE" DATE NULL,
   "END_DATE" DATE NULL,
   "PERIOD_STANDARD" NUMBER(38,0) NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "ID" NUMBER(38,0) NOT NULL,
   "BONUS_DATE" DATE NULL,
   "REMARK" NVARCHAR2(1023) NULL,
   "ACTFLG" NVARCHAR2(1) NULL,
   "IS_DELETED" NUMBER(38,0) NULL,
   "STATUS" NUMBER(38,0) NULL,
   "PERIOD_T" NVARCHAR2(255) NULL
);

-- Creating table 'PE_KPI_ASSESSMENT_RESULT'
CREATE TABLE "dbo"."PE_KPI_ASSESSMENT_RESULT" (
   "ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NULL,
   "YEAR" NUMBER(38,0) NULL,
   "PE_PERIOD_ID" NUMBER(38,0) NULL,
   "START_DATE" DATE NULL,
   "END_DATE" DATE NULL,
   "EVALUATION_POINTS" NCLOB NULL,
   "CLASSIFICATION" NCLOB NULL,
   "IS_LOCK" NUMBER(38,0) NULL
);

-- Creating table 'HUV_PERFORMANCE_KPI'
CREATE TABLE "dbo"."HUV_PERFORMANCE_KPI" (
   "ID" NUMBER(38,0) NOT NULL,
   "EMPLOYEE_ID" NUMBER(38,0) NULL,
   "SUB_EMPLOYEE_ID" NUMBER(38,0) NULL,
   "PROCESS_ID" NUMBER(38,0) NULL,
   "FROM_DATE" DATE NULL,
   "TO_DATE" DATE NULL,
   "CREATED_DATE" DATE NULL,
   "CREATED_BY" NVARCHAR2(255) NULL,
   "CREATED_LOG" NVARCHAR2(255) NULL,
   "MODIFIED_DATE" DATE NULL,
   "MODIFIED_BY" NVARCHAR2(255) NULL,
   "MODIFIED_LOG" NVARCHAR2(255) NULL,
   "REPLACEALL" NUMBER(38,0) NULL,
   "PROCESS_CODE" VARCHAR2(50) NULL
);


-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on "ID"in table 'PE_CRITERIA'
ALTER TABLE "dbo"."PE_CRITERIA"
ADD CONSTRAINT "PK_PE_CRITERIA"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_OBJECT_GROUP'
ALTER TABLE "dbo"."PE_OBJECT_GROUP"
ADD CONSTRAINT "PK_PE_OBJECT_GROUP"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_OBJECT_GROUP_PERIOD'
ALTER TABLE "dbo"."PE_OBJECT_GROUP_PERIOD"
ADD CONSTRAINT "PK_PE_OBJECT_GROUP_PERIOD"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HU_EMPLOYEE'
ALTER TABLE "dbo"."HU_EMPLOYEE"
ADD CONSTRAINT "PK_HU_EMPLOYEE"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "EMPLOYEE_ID"in table 'HU_EMPLOYEE_CV'
ALTER TABLE "dbo"."HU_EMPLOYEE_CV"
ADD CONSTRAINT "PK_HU_EMPLOYEE_CV"
   PRIMARY KEY ("EMPLOYEE_ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HU_ORG_TITLE'
ALTER TABLE "dbo"."HU_ORG_TITLE"
ADD CONSTRAINT "PK_HU_ORG_TITLE"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HU_ORGANIZATION'
ALTER TABLE "dbo"."HU_ORGANIZATION"
ADD CONSTRAINT "PK_HU_ORGANIZATION"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HU_TITLE'
ALTER TABLE "dbo"."HU_TITLE"
ADD CONSTRAINT "PK_HU_TITLE"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ORG_ID", "USERNAME"in table 'SE_CHOSEN_ORG'
ALTER TABLE "dbo"."SE_CHOSEN_ORG"
ADD CONSTRAINT "PK_SE_CHOSEN_ORG"
   PRIMARY KEY ("ORG_ID", "USERNAME" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HU_STAFF_RANK'
ALTER TABLE "dbo"."HU_STAFF_RANK"
ADD CONSTRAINT "PK_HU_STAFF_RANK"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HUV_ORGANIZATION'
ALTER TABLE "dbo"."HUV_ORGANIZATION"
ADD CONSTRAINT "PK_HUV_ORGANIZATION"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_PERSONAL'
ALTER TABLE "dbo"."PE_PERSONAL"
ADD CONSTRAINT "PK_PE_PERSONAL"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_EMPLOYEE_ASSESSMENT_DTL'
ALTER TABLE "dbo"."PE_EMPLOYEE_ASSESSMENT_DTL"
ADD CONSTRAINT "PK_PE_EMPLOYEE_ASSESSMENT_DTL"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_ORGANIZATION'
ALTER TABLE "dbo"."PE_ORGANIZATION"
ADD CONSTRAINT "PK_PE_ORGANIZATION"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_CLASSIFICATION'
ALTER TABLE "dbo"."PE_CLASSIFICATION"
ADD CONSTRAINT "PK_PE_CLASSIFICATION"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'OT_OTHER_LIST'
ALTER TABLE "dbo"."OT_OTHER_LIST"
ADD CONSTRAINT "PK_OT_OTHER_LIST"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_ASSESSMENT'
ALTER TABLE "dbo"."PE_ASSESSMENT"
ADD CONSTRAINT "PK_PE_ASSESSMENT"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_EMPLOYEE_ASSESSMENT'
ALTER TABLE "dbo"."PE_EMPLOYEE_ASSESSMENT"
ADD CONSTRAINT "PK_PE_EMPLOYEE_ASSESSMENT"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_CRITERIA_OBJECT_GROUP'
ALTER TABLE "dbo"."PE_CRITERIA_OBJECT_GROUP"
ADD CONSTRAINT "PK_PE_CRITERIA_OBJECT_GROUP"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_PERIOD'
ALTER TABLE "dbo"."PE_PERIOD"
ADD CONSTRAINT "PK_PE_PERIOD"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_KPI_ASSESSMENT'
ALTER TABLE "dbo"."PE_KPI_ASSESSMENT"
ADD CONSTRAINT "PK_PE_KPI_ASSESSMENT"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_KPI_ASSESSMENT_DETAIL'
ALTER TABLE "dbo"."PE_KPI_ASSESSMENT_DETAIL"
ADD CONSTRAINT "PK_PE_KPI_ASSESSMENT_DETAIL"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_ASSESSMENT_DETAIL_HISTORY'
ALTER TABLE "dbo"."PE_ASSESSMENT_DETAIL_HISTORY"
ADD CONSTRAINT "PK_PE_ASSESSMENT_DETAIL_HISTORY"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PA_PAYMENT_LIST'
ALTER TABLE "dbo"."PA_PAYMENT_LIST"
ADD CONSTRAINT "PK_PA_PAYMENT_LIST"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'TR_SETTING_CRI_COURSE'
ALTER TABLE "dbo"."TR_SETTING_CRI_COURSE"
ADD CONSTRAINT "PK_TR_SETTING_CRI_COURSE"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'TR_CRITERIA_GROUP'
ALTER TABLE "dbo"."TR_CRITERIA_GROUP"
ADD CONSTRAINT "PK_TR_CRITERIA_GROUP"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'TR_COURSE'
ALTER TABLE "dbo"."TR_COURSE"
ADD CONSTRAINT "PK_TR_COURSE"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_CRITERIA_HTCH'
ALTER TABLE "dbo"."PE_CRITERIA_HTCH"
ADD CONSTRAINT "PK_PE_CRITERIA_HTCH"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_PERIOD_HTCH'
ALTER TABLE "dbo"."PE_PERIOD_HTCH"
ADD CONSTRAINT "PK_PE_PERIOD_HTCH"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_CLASSIFICATION_HTCH'
ALTER TABLE "dbo"."PE_CLASSIFICATION_HTCH"
ADD CONSTRAINT "PK_PE_CLASSIFICATION_HTCH"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_CRITERIA_TITLEGROUP'
ALTER TABLE "dbo"."PE_CRITERIA_TITLEGROUP"
ADD CONSTRAINT "PK_PE_CRITERIA_TITLEGROUP"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_CRITERIA_TITLEGROUP_DETAIL'
ALTER TABLE "dbo"."PE_CRITERIA_TITLEGROUP_DETAIL"
ADD CONSTRAINT "PK_PE_CRITERIA_TITLEGROUP_DETAIL"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_EMPLOYEE_PERIOD'
ALTER TABLE "dbo"."PE_EMPLOYEE_PERIOD"
ADD CONSTRAINT "PK_PE_EMPLOYEE_PERIOD"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PROCESS_APPROVED_STATUS'
ALTER TABLE "dbo"."PROCESS_APPROVED_STATUS"
ADD CONSTRAINT "PK_PROCESS_APPROVED_STATUS"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_EMPLOYEEHTCH_PERIOD'
ALTER TABLE "dbo"."PE_EMPLOYEEHTCH_PERIOD"
ADD CONSTRAINT "PK_PE_EMPLOYEEHTCH_PERIOD"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_CRITERIA_TITLEGROUP_RANK'
ALTER TABLE "dbo"."PE_CRITERIA_TITLEGROUP_RANK"
ADD CONSTRAINT "PK_PE_CRITERIA_TITLEGROUP_RANK"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_CRITERIA_TITLEGROUP_RANK_DTL'
ALTER TABLE "dbo"."PE_CRITERIA_TITLEGROUP_RANK_DTL"
ADD CONSTRAINT "PK_PE_CRITERIA_TITLEGROUP_RANK_DTL"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_HTCH_ASSESSMENT_DETAIL'
ALTER TABLE "dbo"."PE_HTCH_ASSESSMENT_DETAIL"
ADD CONSTRAINT "PK_PE_HTCH_ASSESSMENT_DETAIL"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_HTCH_ASSESSMENT'
ALTER TABLE "dbo"."PE_HTCH_ASSESSMENT"
ADD CONSTRAINT "PK_PE_HTCH_ASSESSMENT"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_ORG_MR_RR'
ALTER TABLE "dbo"."PE_ORG_MR_RR"
ADD CONSTRAINT "PK_PE_ORG_MR_RR"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'AT_PERIOD'
ALTER TABLE "dbo"."AT_PERIOD"
ADD CONSTRAINT "PK_AT_PERIOD"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'PE_KPI_ASSESSMENT_RESULT'
ALTER TABLE "dbo"."PE_KPI_ASSESSMENT_RESULT"
ADD CONSTRAINT "PK_PE_KPI_ASSESSMENT_RESULT"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- Creating primary key on "ID"in table 'HUV_PERFORMANCE_KPI'
ALTER TABLE "dbo"."HUV_PERFORMANCE_KPI"
ADD CONSTRAINT "PK_HUV_PERFORMANCE_KPI"
   PRIMARY KEY ("ID" )
   ENABLE
   VALIDATE;


-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on "EMPLOYEE_ID" in table 'HU_EMPLOYEE_CV'
ALTER TABLE "dbo"."HU_EMPLOYEE_CV"
ADD CONSTRAINT "FK_HE_HEC"
   FOREIGN KEY ("EMPLOYEE_ID")
   REFERENCES "dbo"."HU_EMPLOYEE"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating foreign key on "TITLE_ID" in table 'HU_EMPLOYEE'
ALTER TABLE "dbo"."HU_EMPLOYEE"
ADD CONSTRAINT "FK_HE_HT"
   FOREIGN KEY ("TITLE_ID")
   REFERENCES "dbo"."HU_TITLE"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_HE_HT'
CREATE INDEX "IX_FK_HE_HT"
ON "dbo"."HU_EMPLOYEE"
   ("TITLE_ID");

-- Creating foreign key on "ORG_ID" in table 'HU_EMPLOYEE'
ALTER TABLE "dbo"."HU_EMPLOYEE"
ADD CONSTRAINT "FK_HU_ORG_HU_EMP"
   FOREIGN KEY ("ORG_ID")
   REFERENCES "dbo"."HU_ORGANIZATION"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_HU_ORG_HU_EMP'
CREATE INDEX "IX_FK_HU_ORG_HU_EMP"
ON "dbo"."HU_EMPLOYEE"
   ("ORG_ID");

-- Creating foreign key on "DIRECT_MANAGER" in table 'HU_EMPLOYEE'
ALTER TABLE "dbo"."HU_EMPLOYEE"
ADD CONSTRAINT "FK_HUEMD_HUEM"
   FOREIGN KEY ("DIRECT_MANAGER")
   REFERENCES "dbo"."HU_EMPLOYEE"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_HUEMD_HUEM'
CREATE INDEX "IX_FK_HUEMD_HUEM"
ON "dbo"."HU_EMPLOYEE"
   ("DIRECT_MANAGER");

-- Creating foreign key on "ORG_ID" in table 'HU_ORG_TITLE'
ALTER TABLE "dbo"."HU_ORG_TITLE"
ADD CONSTRAINT "FK_HUOG_HUOT"
   FOREIGN KEY ("ORG_ID")
   REFERENCES "dbo"."HU_ORGANIZATION"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_HUOG_HUOT'
CREATE INDEX "IX_FK_HUOG_HUOT"
ON "dbo"."HU_ORG_TITLE"
   ("ORG_ID");

-- Creating foreign key on "TITLE_ID" in table 'HU_ORG_TITLE'
ALTER TABLE "dbo"."HU_ORG_TITLE"
ADD CONSTRAINT "FK_HUTL_HUOT"
   FOREIGN KEY ("TITLE_ID")
   REFERENCES "dbo"."HU_TITLE"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_HUTL_HUOT'
CREATE INDEX "IX_FK_HUTL_HUOT"
ON "dbo"."HU_ORG_TITLE"
   ("TITLE_ID");

-- Creating foreign key on "PARENT_ID" in table 'HU_ORGANIZATION'
ALTER TABLE "dbo"."HU_ORGANIZATION"
ADD CONSTRAINT "FK_HU_ORG_HU_ORG"
   FOREIGN KEY ("PARENT_ID")
   REFERENCES "dbo"."HU_ORGANIZATION"
       ("ID")
   ENABLE
   VALIDATE;

-- Creating index for FOREIGN KEY 'FK_HU_ORG_HU_ORG'
CREATE INDEX "IX_FK_HU_ORG_HU_ORG"
ON "dbo"."HU_ORGANIZATION"
   ("PARENT_ID");

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
