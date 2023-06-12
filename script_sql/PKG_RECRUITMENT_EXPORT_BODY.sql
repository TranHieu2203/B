create or replace package body PKG_RECRUITMENT_EXPORT AS

  PROCEDURE GET_TEMPLATE_MAIL(P_CODE IN NVARCHAR2,
                              P_TYPE IN NVARCHAR2,
                              P_OUT  OUT CURSOR_TYPE) IS
    BEGIN
  
    OPEN P_OUT FOR
      SELECT ST.TITLE, ST.NAME, ST.CONTENT, ST.MAIL_CC
        FROM SE_MAIL_TEMPLATE ST
       WHERE ST.CODE = P_CODE
         AND ST.GROUP_MAIL = P_TYPE;
  END;
  
  PROCEDURE GET_MAILCC_DIRECT_HR(P_EMP_ID IN NUMBER,
                                 P_USER_ID IN NUMBER,
                                 P_OUT OUT NVARCHAR2) IS
     PV_MAIL_DIRECT NVARCHAR2(255) := '';
     PV_MAIL_HR NVARCHAR2(255) := '';
  BEGIN
    SELECT CV.WORK_EMAIL
    INTO PV_MAIL_DIRECT
    FROM HU_EMPLOYEE E
    LEFT JOIN HU_EMPLOYEE MNG
    ON MNG.ID = E.DIRECT_MANAGER
    LEFT JOIN HU_EMPLOYEE_CV CV
    ON CV.EMPLOYEE_ID = MNG.ID
    WHERE E.ID = P_EMP_ID;
    
    SELECT CV.WORK_EMAIL
    INTO PV_MAIL_HR   
    FROM HU_EMPLOYEE_CV CV
    WHERE CV.EMPLOYEE_ID = P_USER_ID;
    
    IF PV_MAIL_DIRECT IS NOT NULL THEN
       P_OUT := PV_MAIL_DIRECT || ',' || PV_MAIL_HR;
    ELSE
       P_OUT := PV_MAIL_HR;
    END IF;
  END;

  PROCEDURE GET_RECRUITMENT_IMPORT(P_USERNAME  IN NVARCHAR2,
                                 P_ORG_ID      IN NUMBER,
                                    P_IS_DISSOLVE     IN NUMBER,
                                 P_CUR1 OUT CURSOR_TYPE,
                                 P_CUR2 OUT CURSOR_TYPE,
                                 P_CUR3 OUT CURSOR_TYPE,
                                 P_CUR4 OUT CURSOR_TYPE,
                                 P_CUR5 OUT CURSOR_TYPE,
                                 P_CUR6 OUT CURSOR_TYPE,
                                 P_CUR7 OUT CURSOR_TYPE,
                                 P_CUR8 OUT CURSOR_TYPE,
                                 P_CUR9 OUT CURSOR_TYPE,
                                 P_CUR10 OUT CURSOR_TYPE,
                                 P_CUR11 OUT CURSOR_TYPE,
                                  P_CUR12 OUT CURSOR_TYPE) IS
  BEGIN  
    OPEN P_CUR1 FOR
         SELECT T.DESCRIPTION_PATH, T.ID
         FROM HU_ORGANIZATION T
         INNER JOIN TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID, P_IS_DISSOLVE)) ORG
            ON ORG.ORG_ID =T.ID
         WHERE T.ACTFLG = 'A';
    
    OPEN P_CUR2 FOR     
         SELECT T.ORG_ID, T.TITLE_ID, E.NAME_VN TITLE_NAME 
         FROM HU_ORG_TITLE T
         INNER JOIN HU_TITLE E 
         ON T.TITLE_ID = E.ID 
         WHERE T.ACTFLG = 'A'
         ORDER BY T.ORG_ID ASC;
    
    OPEN P_CUR3 FOR         
         /*SELECT T.NAME_VN CONTRACT_GROUP_NAME, T.ID CONTRACT_GROUP_ID
         FROM OT_OTHER_LIST T 
         WHERE T.TYPE_ID = 2243 AND T.ACTFLG = 'A';*/
          SELECT T.ID LABOR_TYPE_ID,T.NAME_VN LABOR_TYPE_NAME FROM OT_OTHER_LIST T 
          INNER JOIN OT_OTHER_LIST_TYPE O ON  O.ID=T.TYPE_ID
          WHERE O.CODE='LABOR_TYPE';

    OPEN P_CUR4 FOR
         SELECT T.NAME_VN RECRUIT_PROPERTY_NAME, T.ID RECRUIT_PROPERTY_ID 
         FROM OT_OTHER_LIST T
         WHERE T.TYPE_ID = 2281 AND T.ACTFLG = 'A';

    OPEN P_CUR5 FOR
         SELECT T.NAME_VN RECRUIT_REASON_NAME, T.ID RECRUIT_REASON_ID
         FROM OT_OTHER_LIST T 
         WHERE T.TYPE_CODE = 'RC_RECRUIT_REASON' AND T.ACTFLG = 'A';

    OPEN P_CUR6 FOR
         SELECT T.NAME_VN LEARNING_LEVEL_NAME, T.ID LEARNING_LEVEL_ID
         FROM OT_OTHER_LIST T
         WHERE T.TYPE_CODE = 'LEARNING_LEVEL' AND T.ACTFLG = 'A';

    OPEN P_CUR7 FOR
         SELECT T.NAME_VN MAJOR_NAME, T.ID MAJOR_ID
         FROM OT_OTHER_LIST T
         WHERE T.TYPE_ID = 40 AND T.ACTFLG = 'A';
    OPEN P_CUR8 FOR
         SELECT T.NAME_VN SPECIALSKILLS_NAME, T.ID SPECIALSKILLS_ID
         FROM OT_OTHER_LIST T
         WHERE T.TYPE_ID = 2241 AND T.ACTFLG = 'A';
    
    OPEN P_CUR9 FOR
         SELECT T.NAME_VN LANGUAGE_NAME, T.ID LANGUAGE_ID
         FROM OT_OTHER_LIST T
         WHERE T.TYPE_ID = 1020 AND T.ACTFLG = 'A';

    OPEN P_CUR10 FOR
         SELECT T.NAME_VN LANGUAGE_LEVEL_NAME, T.ID LANGUAGE_LEVEL_ID
         FROM OT_OTHER_LIST T
         WHERE T.TYPE_ID = 38 AND T.ACTFLG = 'A';
         
    OPEN P_CUR11 FOR
         SELECT T.NAME_VN COMPUTER_LEVEL_NAME, T.ID COMPUTER_LEVEL_ID
         FROM OT_OTHER_LIST T
         WHERE T.TYPE_ID = 1022 AND T.ACTFLG = 'A';
    OPEN P_CUR12 FOR
    SELECT T.ID Work_Location_ID,T.NAME_VN Work_Location_name FROM HU_PROVINCE T;
  END;
  
  PROCEDURE GET_INS_HEATH_IMPORT(P_CUR1 OUT CURSOR_TYPE,
                                 P_CUR2 OUT CURSOR_TYPE,
                                 P_CUR3 OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR1 FOR
      SELECT I.ID,
             I.CONTRACT_INS_NO,
             I.YEAR,
             I.ORG_INSURANCE,
             I.START_DATE,
             I.EXPIRE_DATE,
             I.VAL_CO,
             I.BUY_DATE,
             I.NOTE,
             I.PROGRAM_ID
        FROM INS_LIST_CONTRACT I
       WHERE I.IS_DELETED = 0
       ORDER BY YEAR;
  
    OPEN P_CUR2 FOR
      SELECT CD.ID,
             C.CONTRACT_INS_NO,
             CD.CONTRACT_INS_ID,
             CD.INS_PROGRAM_ID,
             CD.MONEY_INS,
             INS.NAME INS_PROGRAM_NAME,
             INS.NAME,
             C.CONTRACT_INS_NO||'_'||INS.NAME  DETAIL_NAME
        FROM INS_LIST_CONTRACT C
        LEFT JOIN INS_LIST_CONTRACT_DETAIL CD
          ON C.ID=CD.CONTRACT_INS_ID
        LEFT JOIN INS_LIST_PROGRAM INS
          ON INS.ID = CD.INS_PROGRAM_ID
          WHERE C.IS_DELETED=0
       ORDER BY CD.CONTRACT_INS_ID;
    OPEN P_CUR3 FOR
      SELECT F.ID,
             F.EMPLOYEE_ID,
             E.EMPLOYEE_CODE,
             E.FULLNAME_VN EMP_NAME,
             F.FULLNAME,
             F.ID_NO,
             TO_CHAR(F.BIRTH_DATE, 'dd/MM/yyyy') as BIRTH_DATE,
             O.NAME_VN,
             O.CODE CODE_QUANHE
        FROM HU_FAMILY F
        LEFT JOIN HU_EMPLOYEE E
          ON E.ID=F.EMPLOYEE_ID
        LEFT JOIN OT_OTHER_LIST O
          ON F.RELATION_ID = O.ID
         AND O.TYPE_CODE = 'RELATION'
         ORDER BY F.EMPLOYEE_ID;
  END;
  
  PROCEDURE GET_OFFERLETTER_IMPORT(P_USERNAME IN NVARCHAR2,
                                   P_CUR      OUT CURSOR_TYPE,
                                   P_CUR1     OUT CURSOR_TYPE,
                                   P_CUR2     OUT CURSOR_TYPE,
                                   P_CUR3     OUT CURSOR_TYPE,
                                   P_CUR4     OUT CURSOR_TYPE,
                                   P_CUR5     OUT CURSOR_TYPE,
                                   P_CUR6     OUT CURSOR_TYPE,
                                   P_CUR7     OUT CURSOR_TYPE) IS
  BEGIN
  
    OPEN P_CUR FOR --get YCTD
      SELECT DISTINCT P.ID, PR.CODE_RC
        FROM RC_PROGRAM P
       INNER JOIN RC_REQUEST PR
          ON PR.ID = P.RC_REQUEST_ID
         AND PR.STATUS_ID = 4101
       INNER JOIN RC_PROGRAM_CANDIDATE CAN
          ON CAN.RC_PROGRAM_ID = P.ID;
  
    OPEN P_CUR1 FOR --get loai hop dong
    
      SELECT CT.ID, CT.NAME, CT.CODE
        FROM HU_CONTRACT_TYPE CT
       WHERE CT.ACTFLG = 'A'
         AND CT.IS_REQUIREMENT = -1;
  
    OPEN P_CUR2 FOR --get nhom luong
    
      SELECT ST.ID, ST.CODE, ST.NAME
        FROM PA_SALARY_TYPE ST
       WHERE ST.ACTFLG = 'A';
  
    OPEN P_CUR3 FOR --GET BIEU THUE
    
      SELECT OL.ID, OL.CODE, OL.NAME_VN
        FROM OT_OTHER_LIST OL
       WHERE OL.TYPE_CODE = 'TAX_TABLE'
         AND OL.ACTFLG = 'A';
  
    OPEN P_CUR4 FOR --GET LOAINV
    
      SELECT OLNV.ID, OLNV.CODE, OLNV.NAME_VN
        FROM OT_OTHER_LIST OLNV
       WHERE OLNV.TYPE_ID = 11
         AND OLNV.ACTFLG = 'A';
  
    OPEN P_CUR5 FOR -- GET LOAI PHU CAP
    
      SELECT AL.ID, AL.NAME, AL.CODE
        FROM HU_ALLOWANCE_LIST AL
       WHERE AL.ACTFLG = 'A'
         AND AL.IS_CONTRACT = -1;
  
    OPEN P_CUR6 FOR
      SELECT OLDVT.ID, OLDVT.CODE, OLDVT.NAME_VN
        FROM OT_OTHER_LIST OLDVT
       WHERE OLDVT.TYPE_CODE = 'DVT_PHUCAP'
         AND OLDVT.ACTFLG = 'A';
    OPEN P_CUR7 FOR
      SELECT C.ID, C.CANDIDATE_CODE, C.FULLNAME_VN FROM RC_CANDIDATE C;
  
  END;
end PKG_RECRUITMENT_EXPORT;
