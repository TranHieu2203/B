CREATE OR REPLACE PACKAGE BODY PKG_RECRUITMENT IS

  PROCEDURE CHECK_EXIST_REQUEST(P_ORG_ID             IN NUMBER,
                                P_TITLE_ID           IN NUMBER,
                                P_SEND_DATE          IN DATE,
                                P_EXPECTED_JOIN_DATE IN DATE,
                                P_OUT                OUT NUMBER) IS
  BEGIN
    P_OUT := 0;
    SELECT COUNT(1)
      INTO P_OUT
      FROM RC_REQUEST T
     WHERE T.ORG_ID = P_ORG_ID
       AND T.TITLE_ID = P_TITLE_ID
       AND T.SEND_DATE = P_SEND_DATE
       AND T.EXPECTED_JOIN_DATE = P_EXPECTED_JOIN_DATE;
  END;
  PROCEDURE update_cadidate_trainning(p_id                  NUMBER,
                                      p_year_gra            NUMBER,
                                      p_name_shools         NVARCHAR2,
                                      p_form_train_id       NUMBER,
                                      p_specialized_train   NVARCHAR2,
                                      p_result_train        NVARCHAR2,
                                      p_certificate         NUMBER,
                                      p_effective_date_from DATE,
                                      p_effective_date_to   DATE,
                                      p_candidate_id        NUMBER,
                                      p_from_date           DATE,
                                      p_to_date             DATE,
                                      p_upload_file         NVARCHAR2,
                                      p_file_name           NVARCHAR2,
                                      p_type_train_id       NUMBER,
                                      p_receive_degree_date DATE,
                                      p_is_renewed          NUMBER,
                                      p_level_id            NUMBER,
                                      p_point_level         NVARCHAR2,
                                      p_content_level       NVARCHAR2,
                                      p_note                NVARCHAR2,
                                      p_certificate_code    NVARCHAR2,
                                      p_type_train_name     NVARCHAR2,
                                      p_out                 OUT NUMBER) AS
  BEGIN
    UPDATE rc_candidate_trainning
       SET year_gra            = p_year_gra,
           name_shools         = p_name_shools,
           form_train_id       = p_form_train_id,
           specialized_train   = p_specialized_train,
           result_train        = p_result_train,
           certificate         = p_certificate,
           effective_date_from = p_effective_date_from,
           effective_date_to   = p_effective_date_to,
           modified_date       = sysdate,
           modified_by         = 'ADMIN',
           modified_log        = 'ADMIN',
           candidate_id        = p_candidate_id,
           from_date           = p_from_date,
           to_date             = p_to_date,
           upload_file         = p_upload_file,
           file_name           = p_file_name,
           type_train_id       = p_type_train_id,
           receive_degree_date = p_receive_degree_date,
           is_renewed          = p_is_renewed,
           level_id            = p_level_id,
           point_level         = p_point_level,
           content_level       = p_content_level,
           note                = p_note,
           certificate_code    = p_certificate_code,
           type_train_name     = p_type_train_name
     WHERE id = p_id;
  
    COMMIT;
    p_out := 1;
  EXCEPTION
    WHEN OTHERS THEN
      sys_write_exception_log(sqlcode,
                              'PKG_RECRUITMENT_NEW.update_cadidate_trainning',
                              sqlerrm,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
    
      p_out := -1;
  END;
  PROCEDURE delete_rc_candidate_trainning(p_id IN NUMBER, p_out OUT NUMBER) IS
  BEGIN
    DELETE FROM rc_candidate_trainning t WHERE id = p_id;
    commit;
    p_out := 1;
  EXCEPTION
    WHEN OTHERS THEN
      p_out := -1;
      sys_write_exception_log(sqlcode,
                              'PKG_RECRUITMENT.delete_rc_candidate_trainning',
                              sqlerrm,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
    
  END;
  PROCEDURE insert_cadidate_trainning(p_year_gra            NUMBER,
                                      p_name_shools         NVARCHAR2,
                                      p_form_train_id       NUMBER,
                                      p_specialized_train   NVARCHAR2,
                                      p_result_train        NVARCHAR2,
                                      p_certificate         NUMBER,
                                      p_effective_date_from DATE,
                                      p_effective_date_to   DATE,
                                      p_candidate_id        NUMBER,
                                      p_from_date           DATE,
                                      p_to_date             DATE,
                                      p_upload_file         NVARCHAR2,
                                      p_file_name           NVARCHAR2,
                                      p_type_train_id       NUMBER,
                                      p_receive_degree_date DATE,
                                      p_is_renewed          NUMBER,
                                      p_level_id            NUMBER,
                                      p_point_level         NVARCHAR2,
                                      p_content_level       NVARCHAR2,
                                      p_note                NVARCHAR2,
                                      p_certificate_code    NVARCHAR2,
                                      p_type_train_name     NVARCHAR2,
                                      P_OUT                 OUT NUMBER) AS
  BEGIN
    INSERT INTO rc_candidate_trainning
      ("ID",
       "YEAR_GRA",
       name_shools,
       "FORM_TRAIN_ID",
       "SPECIALIZED_TRAIN", --
       "RESULT_TRAIN", --
       "CERTIFICATE", --
       "EFFECTIVE_DATE_FROM", --
       "EFFECTIVE_DATE_TO", --
       "CREATED_DATE", --
       "CREATED_BY",
       "CREATED_LOG",
       "MODIFIED_DATE",
       "MODIFIED_BY",
       "MODIFIED_LOG",
       "CANDIDATE_ID",
       "FROM_DATE",
       "TO_DATE",
       "UPLOAD_FILE", --
       "FILE_NAME", --
       "TYPE_TRAIN_ID", --
       "RECEIVE_DEGREE_DATE", --
       "IS_RENEWED", --
       "LEVEL_ID", --
       "POINT_LEVEL", --
       "CONTENT_LEVEL",
       "NOTE",
       "CERTIFICATE_CODE",
       "TYPE_TRAIN_NAME")
    VALUES
      (seq_rc_candidate_trainning.NEXTVAL,
       p_year_gra,
       p_name_shools,
       p_form_train_id,
       p_specialized_train,
       p_result_train,
       p_certificate,
       p_effective_date_from,
       p_effective_date_to,
       sysdate,
       'ADMIN',
       'ADMIN',
       sysdate,
       'ADMIN',
       'ADMIN',
       p_candidate_id,
       p_from_date,
       p_to_date,
       p_upload_file,
       p_file_name,
       p_type_train_id,
       p_receive_degree_date,
       p_is_renewed,
       p_level_id,
       p_point_level,
       p_content_level,
       p_note,
       p_certificate_code,
       p_type_train_name)
    returning id into P_OUT;
  
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      sys_write_exception_log(sqlcode,
                              'PKG_RECRUITMENT_NEW.insert_cadidate_trainning',
                              sqlerrm,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
      P_OUT := -1;
  END;
  PROCEDURE get_combobox(p_combobox_code IN VARCHAR2,
                         p_cur           OUT cursor_type) IS
  P_ID NUMBER;
  BEGIN
    IF p_combobox_code = 'GET_LEVEL_TRAIN' THEN
      OPEN p_cur FOR
        SELECT n.id, n.name_vn, n.name_en
          FROM ot_other_list n
          LEFT OUTER JOIN ot_other_list_type n1
            ON n.type_id = n1.id
         WHERE n1.code = 'LEARNING_LEVEL'
           AND n.actflg = 'A';
    
      return;
    END IF;
  
    IF p_combobox_code = 'GET_TRAINING_FORM' THEN
      OPEN p_cur FOR
        SELECT n.id, n.name_vn, n.name_en
          FROM ot_other_list n
          LEFT OUTER JOIN ot_other_list_type n1
            ON n.type_id = n1.id
         WHERE n1.code = 'TRAINING_FORM'
           AND n.actflg = 'A';
    
    END IF;
  
    IF p_combobox_code = 'GET_CERTIFICATE_TYPE' THEN
      OPEN p_cur FOR
        SELECT n.id, n.name_vn, n.name_en
          FROM ot_other_list n
          LEFT OUTER JOIN ot_other_list_type n1
            ON n.type_id = n1.id
         WHERE n1.code = 'CERTIFICATE_TYPE'
           AND n.actflg = 'A';
    
    END IF;
    
    IF p_combobox_code = 'HU_TITLE_GROUP' THEN
      OPEN p_cur FOR
        SELECT n.id, n.name_vn, n.name_en
          FROM ot_other_list n
          LEFT OUTER JOIN ot_other_list_type n1
            ON n.type_id = n1.id
         WHERE n1.code = 'HU_TITLE_GROUP'
           AND n.actflg = 'A';
    
    END IF;
   IF p_combobox_code like '%EXAMINATOR%' THEN
      SELECT TO_NUMBER(NVL(SUBSTR( p_combobox_code, 11 , LENGTH(p_combobox_code)),0)) 
          INTO P_ID FROM DUAL;
      OPEN p_cur FOR
        SELECT n.id, n.fullname_vn name_vn, n.employee_code name_en
          FROM HU_EMPLOYEE n
          JOIN RC_PROGRAM_SCHEDULE A ON A.ID = P_ID
         WHERE INSTR(','||A.ID_EXAM||',', ','||TO_CHAR(N.ID)||',') > 0;
    
      return;
    END IF;
  END;
  PROCEDURE Get_candidate_trainning(p_candidate_code IN VARCHAR2,
                                    P_CUR            OUT CURSOR_TYPE) IS
    p_candidate_id number(38);
  BEGIN
    select n.id
      into p_candidate_id
      from rc_candidate n
     where n.candidate_code = p_candidate_code;
    OPEN P_CUR FOR
      SELECT n.id,
             n1.candidate_code,
             n.from_date,
             n.to_date,
             n.year_gra,
             n.name_shools,
             n.form_train_id,
             n2.name_vn form_train_name,
             n.upload_file,
             n.file_name,
             n.specialized_train,
             n.result_train,
             n3.name_vn certificate,
             n.certificate certificate_id,
             n.effective_date_from,
             n.effective_date_to,
             n.receive_degree_date,
             n.created_by,
             n.created_date,
             n.created_log,
             n.modified_by,
             n.modified_date,
             n.modified_log,
             n.is_renewed,
             CASE
               WHEN n.is_renewed = 0 THEN
                TO_CHAR(UNISTR('kh\00f4ng'))
               ELSE
                TO_CHAR(UNISTR('c\00f3'))
             END renewed_name,
             n.level_id,
             n2.name_vn level_name,
             n.point_level,
             n.content_level,
             n.note,
             n.certificate_code,
             n.type_train_name,
             n.type_train_id
        FROM rc_candidate_trainning n
        LEFT OUTER JOIN rc_candidate n1
          ON n.candidate_id = n1.id
        LEFT OUTER JOIN ot_other_list n2
          ON n.form_train_id = n2.id
        LEFT OUTER JOIN ot_other_list n3
          ON n.certificate = n3.id
        LEFT OUTER JOIN ot_other_list n4
          ON n.level_id = n4.id
       WHERE n.candidate_id = p_candidate_id;
  end;
  PROCEDURE GET_PLAN_SUMMARY(P_YEAR       IN VARCHAR,
                             P_USERNAME   IN VARCHAR,
                             P_ORGID      IN NUMBER,
                             P_ISDISSOLVE IN NUMBER,
                             P_CUR        OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT --REGEXP_SUBSTR(O.DESCRIPTION_PATH, '[^;]+', 1, 2) ORG_NAME2,
      --REGEXP_SUBSTR(O.DESCRIPTION_PATH, '[^;]+', 1, 3) ORG_NAME3,
      --REGEXP_SUBSTR(O.DESCRIPTION_PATH, '[^;]+', 1, 4) ORG_NAME4,
       O.NAME_VN ORG_NAME,
       O.DESCRIPTION_PATH ORG_DESC,
       T.NAME_VN TITLE_NAME,
       NVL(E.SL, 0) SL_HIENTAI,
       NVL(SL_T1, 0) SL_T1,
       NVL(SL_T2, 0) SL_T2,
       NVL(SL_T3, 0) SL_T3,
       NVL(SL_T4, 0) SL_T4,
       NVL(SL_T5, 0) SL_T5,
       NVL(SL_T6, 0) SL_T6,
       NVL(SL_T7, 0) SL_T7,
       NVL(SL_T8, 0) SL_T8,
       NVL(SL_T9, 0) SL_T9,
       NVL(SL_T10, 0) SL_T10,
       NVL(SL_T11, 0) SL_T11,
       NVL(SL_T12, 0) SL_T12,
       NVL(SL_T1, 0) + NVL(SL_T2, 0) + NVL(SL_T3, 0) + NVL(SL_T4, 0) +
       NVL(SL_T5, 0) + NVL(SL_T6, 0) + NVL(SL_T7, 0) + NVL(SL_T8, 0) +
       NVL(SL_T9, 0) + NVL(SL_T10, 0) + NVL(SL_T11, 0) + NVL(SL_T12, 0) SL_DUBAO,
       NVL(CAN.SL, 0) SL_DATUYEN,
       NVL((select O.Name_VN
             from RC_PLAN_REG R
            inner join OT_OTHER_LIST O
               on O.ID = R.RECRUIT_REASON_ID
            where r.id = REG.ID),
           '') LyDoTuyenDung,
       NVL((select R.REMARK from RC_PLAN_REG R where r.id = REG.ID), '') GhiChu
      
        FROM HU_ORGANIZATION O
       INNER JOIN HU_ORG_TITLE OT
          ON O.ID = OT.ORG_ID
       INNER JOIN HU_TITLE T
          ON OT.TITLE_ID = T.ID
        LEFT JOIN (SELECT E.ORG_ID, E.TITLE_ID, COUNT(*) SL
                     FROM (SELECT E.*,
                                  EMP.LAST_WORKING_ID,
                                  ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                             FROM HU_WORKING E
                            INNER JOIN HU_EMPLOYEE EMP
                               ON E.EMPLOYEE_ID = EMP.ID
                            WHERE TO_CHAR(E.EFFECT_DATE, 'YYYYMMDD') <=
                                  P_YEAR || '1231'
                              AND (EMP.TER_EFFECT_DATE IS NULL OR
                                   (EMP.TER_EFFECT_DATE IS NOT NULL AND
                                   TO_CHAR(EMP.TER_EFFECT_DATE, 'YYYYMMDD') <=
                                   P_YEAR || '1231'))
                              AND E.ORG_ID IS NOT NULL
                              AND E.TITLE_ID IS NOT NULL
                              AND (TO_CHAR(EMP.Join_Date, 'RRRR') <= P_YEAR OR
                                   EMP.Join_Date IS NULL)
                              AND (select CODE
                                     from Ot_Other_List
                                    where ID = EMP.Work_Status) = 'WORKING') E
                    WHERE E.ROW_NUMBER = 1
                    GROUP BY E.ORG_ID, E.TITLE_ID) E
          ON (E.ORG_ID = O.ID OR E.Org_Id = O.PARENT_ID)
         AND E.TITLE_ID = T.ID
       inner JOIN (SELECT REG.ORG_ID,
                          REG.TITLE_ID,
                          REG.ID,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '01' THEN
                                     REG.RECRUIT_NUMBER
                                    ELSE
                                     0
                                  END,
                                  0)) SL_T1,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '02' THEN
                                     REG.RECRUIT_NUMBER
                                  END,
                                  0)) SL_T2,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '03' THEN
                                     REG.RECRUIT_NUMBER
                                  END,
                                  0)) SL_T3,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '04' THEN
                                     REG.RECRUIT_NUMBER
                                  END,
                                  0)) SL_T4,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '05' THEN
                                     REG.RECRUIT_NUMBER
                                  END,
                                  0)) SL_T5,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '06' THEN
                                     REG.RECRUIT_NUMBER
                                  END,
                                  0)) SL_T6,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '07' THEN
                                     REG.RECRUIT_NUMBER
                                  END,
                                  0)) SL_T7,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '08' THEN
                                     REG.RECRUIT_NUMBER
                                  END,
                                  0)) SL_T8,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '09' THEN
                                     REG.RECRUIT_NUMBER
                                  END,
                                  0)) SL_T9,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '10' THEN
                                     REG.RECRUIT_NUMBER
                                  END,
                                  0)) SL_T10,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '11' THEN
                                     REG.RECRUIT_NUMBER
                                  END,
                                  0)) SL_T11,
                          SUM(NVL(CASE
                                    WHEN TO_CHAR(REG.Expected_Join_Date, 'mm') = '12' THEN
                                     REG.RECRUIT_NUMBER
                                  END,
                                  0)) SL_T12
                     FROM RC_PLAN_REG REG
                    WHERE TO_CHAR(REG.Expected_Join_Date, 'yyyy') = P_YEAR
                      AND REG.STATUS_ID = 4051
                    GROUP BY REG.ORG_ID, REG.TITLE_ID, REG.ID) REG
      
          ON (REG.ORG_ID = O.ID OR REG.ORG_ID = O.PARENT_ID)
         AND REG.TITLE_ID = T.ID
        left JOIN (SELECT CAN.ORG_ID, CAN.TITLE_ID, COUNT(*) SL
                     FROM RC_CANDIDATE CAN
                    INNER JOIN RC_PROGRAM PRO
                       ON CAN.RC_PROGRAM_ID = PRO.ID
                    INNER JOIN RC_REQUEST REQUEST
                       ON PRO.RC_REQUEST_ID = REQUEST.ID
                    WHERE CAN.STATUS_ID = 'RC_CANDIDATE_STATUS' --4110 KH?A L?I KO DUNG ID N?A
                      AND TO_CHAR(REQUEST.EXPECTED_JOIN_DATE, 'yyyy') =
                          P_YEAR
                    GROUP BY CAN.ORG_ID, CAN.TITLE_ID) CAN
          ON CAN.ORG_ID = O.ID
         AND CAN.TITLE_ID = T.ID
       WHERE O.ID IN
             (SELECT *
                FROM TABLE(TABLE_ORG_RIGHT(P_USERNAME,
                                           P_ORGID,
                                           NVL(P_ISDISSOLVE, 0))));
  
  END;

  PROCEDURE GET_CANDIDATE_IMPORT(PV_CUR   OUT CURSOR_TYPE,
                                 PV_CUR1  OUT CURSOR_TYPE,
                                 PV_CUR2  OUT CURSOR_TYPE,
                                 PV_CUR3  OUT CURSOR_TYPE,
                                 PV_CUR4  OUT CURSOR_TYPE,
                                 PV_CUR5  OUT CURSOR_TYPE,
                                 PV_CUR6  OUT CURSOR_TYPE,
                                 PV_CUR7  OUT CURSOR_TYPE,
                                 PV_CUR8  OUT CURSOR_TYPE,
                                 PV_CUR9  OUT CURSOR_TYPE,
                                 PV_CUR10 OUT CURSOR_TYPE,
                                 PV_CUR11 OUT CURSOR_TYPE,
                                 PV_CUR12 OUT CURSOR_TYPE,
                                 PV_CUR13 OUT CURSOR_TYPE,
                                 PV_CUR14 OUT CURSOR_TYPE,
                                 PV_CUR15 OUT CURSOR_TYPE,
                                 PV_CUR16 OUT CURSOR_TYPE,
                                 PV_CUR17 OUT CURSOR_TYPE,
                                 PV_CUR18 OUT CURSOR_TYPE) IS
  BEGIN
    OPEN PV_CUR FOR
      SELECT NULL FIRST_NAME_VN,
             NULL LAST_NAME_VN,
             NULL GENDER_NAME,
             NULL GENDER,
             NULL MARITAL_STATUS_NAME,
             NULL MARITAL_STATUS,
             NULL NATIVE_NAME,
             NULL NATIVE,
             NULL BIRTH_DATE,
             NULL BIRTH_NATION_NAME,
             NULL BIRTH_NATION_ID,
             NULL BIRTH_PROVINCE_NAME,
             NULL BIRTH_PROVINCE,
             NULL NAV_PROVINCE_NAME,
             NULL NAV_PROVINCE,
             NULL NATIONALITY_NAME,
             NULL NATIONALITY_ID,
             NULL RELIGION_NAME,
             NULL RELIGION,
             NULL ID_NO,
             NULL ID_DATE,
             NULL ID_DATE_EXPIRATION,
             NULL ID_PLACE_NAME,
             NULL ID_PLACE,
             NULL IS_RESIDENT,
             NULL NAV_NATION_NAME,
             NULL NAV_NATION_ID,
             NULL PER_ADDRESS,
             NULL PER_NATION_NAME,
             NULL PER_NATION_ID,
             NULL PER_PROVINCE_NAME,
             NULL PER_PROVINCE,
             NULL PER_DISTRICT_NAME,
             NULL PER_DISTRICT_ID,
             NULL CONTACT_ADDRESS,
             NULL CONTACT_NATION_NAME,
             NULL CONTACT_NATION_ID,
             NULL CONTACT_PROVINCE_NAME,
             NULL CONTACT_PROVINCE,
             NULL CONTACT_DISTRICT_NAME,
             NULL CONTACT_DISTRICT_ID,
             NULL CONTACT_MOBILE,
             NULL CONTACT_PHONE,
             NULL WORK_EMAIL,
             NULL PER_EMAIL,
             NULL PERTAXCODE,
             NULL PER_TAX_DATE,
             NULL PER_TAX_PLACE,
             NULL PASSPORT_ID,
             NULL PASSPORT_DATE,
             NULL PASSPORT_DATE_EXPIRATION,
             NULL PASSPORT_PLACE_NAME,
             NULL VISA_NUMBER,
             NULL VISA_DATE,
             NULL VISA_DATE_EXPIRATION,
             NULL VISA_PLACE,
             NULL VNAIRLINES_NUMBER,
             NULL VNAIRLINES_DATE,
             NULL VNAIRLINES_DATE_EXPIRATION,
             NULL VNAIRLINES_PLACE,
             NULL LABOUR_NUMBER,
             NULL LABOUR_DATE,
             NULL LABOUR_DATE_EXPIRATION,
             NULL LABOUR_PLACE,
             NULL WORK_PERMIT,
             NULL WORK_PERMIT_START,
             NULL WORK_PERMIT_END,
             NULL TEMP_RESIDENCE_CARD,
             NULL TEMP_RESIDENCE_CARD_START,
             NULL TEMP_RESIDENCE_CARD_END,
             NULL ACADEMY_NAME,
             NULL ACADEMY,
             NULL LEARNING_LEVEL_NAME,
             NULL LEARNING_LEVEL,
             NULL FIELD_NAME,
             NULL FIELD,
             NULL SCHOOL_NAME,
             NULL SCHOOL,
             NULL MAJOR_NAME,
             NULL MAJOR,
             NULL DEGREE_NAME,
             NULL DEGREE,
             NULL MARK_EDU_NAME,
             NULL MARK_EDU,
             NULL GPA,
             NULL IT_CERTIFICATE,
             NULL IT_LEVEL_NAME,
             NULL IT_LEVEL,
             NULL IT_MARK,
             NULL IT_CERTIFICATE1,
             NULL IT_LEVEL1_NAME,
             NULL IT_LEVEL1,
             NULL IT_MARK1,
             NULL IT_CERTIFICATE2,
             NULL IT_LEVEL2_NAME,
             NULL IT_LEVEL2,
             NULL IT_MARK2,
             NULL ENGLISH,
             NULL ENGLISH_LEVEL_NAME,
             NULL ENGLISH_LEVEL,
             NULL ENGLISH_MARK,
             NULL ENGLISH1,
             NULL ENGLISH_LEVEL1_NAME,
             NULL ENGLISH_LEVEL1,
             NULL ENGLISH_MARK1,
             NULL ENGLISH2,
             NULL ENGLISH_LEVEL2_NAME,
             NULL ENGLISH_LEVEL2,
             NULL ENGLISH_MARK2,
             NULL NGAYVAOCONGDOAN,
             NULL NOIVAOCONGDOAN,
             NULL CONGDOANPHI,
             NULL ACCOUNT_NAME,
             NULL ACCOUNT_NUMBER,
             NULL ACCOUNT_EFFECT_DATE,
             NULL BANK_BRANCH_NAME,
             NULL BANK_BRANCH,
             NULL BANK_NAME,
             NULL BANK,
             0    IS_PAYMENT_VIA_BANK
        FROM DUAL;
    -- gioi tinh
    OPEN PV_CUR1 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = type.id
       WHERE TYPE.CODE = 'GENDER'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- tinh trang hon nhan
    OPEN PV_CUR2 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'FAMILY_STATUS'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- dan toc
    OPEN PV_CUR3 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'NATIVE'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- quoc gia
    OPEN PV_CUR4 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM HU_NATION O
       WHERE O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- tinh thanh
    OPEN PV_CUR5 FOR
      SELECT O.NAME_VN NAME_PARENT, PRO.ID, PRO.NAME_VN NAME
        FROM HU_NATION O
       INNER JOIN HU_PROVINCE PRO
          ON O.ID = PRO.NATION_ID
       WHERE O.ACTFLG = 'A'
         AND PRO.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese'),
                NLSSORT(PRO.NAME_VN, 'NLS_SORT=vietnamese');
    -- quan huyen
    OPEN PV_CUR6 FOR
      SELECT O.NAME_VN NAME_PARENT, DIS.ID, DIS.NAME_VN NAME
        FROM HU_PROVINCE O
       INNER JOIN HU_DISTRICT DIS
          ON O.ID = DIS.PROVINCE_ID
       WHERE O.ACTFLG = 'A'
         AND DIS.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese'),
                NLSSORT(DIS.NAME_VN, 'NLS_SORT=vietnamese');
    -- trinh do van hoa
    OPEN PV_CUR7 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'ACADEMY'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- trinh do hoc van
    OPEN PV_CUR8 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'LEARNING_LEVEL'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- trinh do chuyen mon
    OPEN PV_CUR9 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'FIELD'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- truong hoc
    OPEN PV_CUR10 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'SCHOOL'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- chuyen nganh
    OPEN PV_CUR11 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'MAJOR'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- bang cap
    OPEN PV_CUR12 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'DEGREE'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- xep loai
    OPEN PV_CUR13 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'MARK_EDU'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- trinh do tin hoc
    OPEN PV_CUR14 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'COMPUTER_LEVEL'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- trinh do ngoai ngu
    OPEN PV_CUR15 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'LANGUAGE_LEVEL'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- ton giao
    OPEN PV_CUR16 FOR
      SELECT O.ID, O.NAME_VN NAME
        FROM OT_OTHER_LIST O
       INNER JOIN OT_OTHER_LIST_TYPE TYPE
          ON O.TYPE_ID = TYPE.ID
       WHERE TYPE.CODE = 'RELIGION'
         AND O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME_VN, 'NLS_SORT=vietnamese');
    -- ngan hang
    OPEN PV_CUR17 FOR
      SELECT O.ID, O.NAME NAME
        FROM HU_BANK O
       WHERE O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME, 'NLS_SORT=vietnamese');
    -- chi nhanh ngan hang
    OPEN PV_CUR18 FOR
      SELECT O.ID, O.NAME NAME
        FROM HU_BANK_BRANCH O
       WHERE O.ACTFLG = 'A'
       ORDER BY NLSSORT(O.NAME, 'NLS_SORT=vietnamese');
  
  END;

  PROCEDURE UPDATE_REMARK_REJECT(P_ID            IN NUMBER,
                                 P_REMARK_REJECT IN NVARCHAR2,
                                 P_OUT           OUT NUMBER) IS
  BEGIN
    UPDATE RC_PLAN_REG
       SET REMARK_REJECT = P_REMARK_REJECT, STATUS_id = '4052'
     WHERE ID = P_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_REMARK_REJECT',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;
  PROCEDURE UPDATE_REQUEST_REMARK_REJECT(P_ID            IN NUMBER,
                                         P_REMARK_REJECT IN NVARCHAR2,
                                         P_OUT           OUT NUMBER) IS
  BEGIN
    UPDATE RC_REQUEST
       SET REMARK_REJECT = P_REMARK_REJECT, STATUS_id = '4052'
     WHERE ID = P_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_REQUEST_REMARK_REJECT',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_TOTAL_EMPLOYEE_BY_TITLEID(P_ORGID   IN NUMBER,
                                          P_TITLEID IN NUMBER,
                                          P_USER    IN NVARCHAR2,
                                          P_OUT     OUT NUMBER) IS
  BEGIN
    P_OUT := 0;
    SELECT COUNT(E.ID)
      Into P_OUT
      FROM HU_EMPLOYEE E
     INNER JOIN HU_TITLE T
        ON E.TITLE_ID = T.ID
     INNER JOIN HU_ORGANIZATION O
        ON O.ID = E.ORG_ID
     INNER JOIN TABLE(TABLE_ORG_RIGHT(P_USER, P_ORGID)) ORG
        ON ORG.ORG_ID = O.ID
     WHERE E.TITLE_ID = P_TITLEID;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_TOTAL_EMPLOYEE_BY_TITLEID',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE STAGE_GET_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT S.*, OT.NAME_VN AS SOURCE_NAME, OT.CODE AS SOURCE_ID
        FROM RC_STAGE S
       INNER JOIN HU_ORGANIZATION O
          ON O.ID = S.ORG_ID
        LEFT JOIN OT_OTHER_LIST OT
          ON S.SOURCEOFREC_ID = OT.CODE
       WHERE S.ID = P_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.STAGE_GET_BY_ID',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE STAGE_GETLIST_BYFILTER(P_ORG_ID     IN NUMBER,
                                   P_STAGE_YEAR IN NUMBER,
                                   P_CUR        OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT S.*, OT.NAME_VN AS SOURCE_NAME, OT.CODE AS SOURCE_ID
        FROM RC_STAGE S
       INNER JOIN HU_ORGANIZATION O
          ON (O.ID = S.ORG_ID OR O.PARENT_ID = S.ORG_ID)
        LEFT JOIN OT_OTHER_LIST OT
          ON S.SOURCEOFREC_ID = OT.CODE
       WHERE O.ID = P_ORG_ID
         AND S.YEAR = P_STAGE_YEAR;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.STAGE_GETLIST_BYFILTER',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE STAGE_GETDATA_COMBOBOX(P_ORG_ID IN NUMBER,
                                   P_CUR    OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT S.ID, S.TITLE
        FROM RC_STAGE S
       INNER JOIN HU_ORGANIZATION O
          ON O.ID = S.ORG_ID
       WHERE S.ORG_ID = P_ORG_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.STAGE_GETDATA_COMBOBOX',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE STAGE_UPDATE_COSTESTIMATE(P_STAGE_ID IN NUMBER,
                                      P_OUT      OUT NUMBER) IS
  BEGIN
    UPDATE RC_STAGE
       SET COSTESTIMATE =
           (select SUM(NVL(C.COSTAMOUNT, 0))
              from Rc_Costallocate C
             where C.RC_STAGE_ID = P_STAGE_ID)
     WHERE ID = P_STAGE_ID;
    --update vao quan ly chi phi tuyen dung thuc te
    UPDATE RC_COST
       SET COSTESTIMATE =
           (select SUM(NVL(C.COSTAMOUNT, 0))
              from Rc_Costallocate C
             where C.RC_STAGE_ID = P_STAGE_ID)
     WHERE RC_STAGE_ID = P_STAGE_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.STAGE_UPDATE_COSTESTIMATE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE ADDNEW_STAGE(P_ORG_ID           IN NUMBER,
                         P_YEAR             IN NUMBER,
                         P_ORGANIZATIONNAME IN NVARCHAR2,
                         P_TITLE            IN NVARCHAR2,
                         P_STARTDATE        IN DATE,
                         P_ENDDATE          IN DATE,
                         P_SOURCEOFREC_ID   IN NVARCHAR2,
                         P_REMARK           IN NVARCHAR2,
                         P_USERNAME         IN NVARCHAR2,
                         P_CREATED_LOG      IN NVARCHAR2,
                         P_OUT              OUT NUMBER) IS
  
  BEGIN
    INSERT INTO RC_STAGE
      (ID,
       ORG_ID,
       YEAR,
       ORGANIZATIONNAME,
       TITLE,
       STARTDATE,
       ENDDATE,
       SOURCEOFREC_ID,
       REMARK,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG)
    VALUES
      (SEQ_RC_STAGE.NEXTVAL,
       P_ORG_ID,
       P_YEAR,
       P_ORGANIZATIONNAME,
       P_TITLE,
       P_STARTDATE,
       P_ENDDATE,
       P_SOURCEOFREC_ID,
       P_REMARK,
       SYSDATE,
       P_USERNAME,
       P_CREATED_LOG);
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.ADDNEW_STAGE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_STAGE(P_ID               IN NUMBER,
                         P_ORG_ID           IN NUMBER,
                         P_YEAR             IN NUMBER,
                         P_ORGANIZATIONNAME IN NVARCHAR2,
                         P_TITLE            IN NVARCHAR2,
                         P_STARTDATE        IN DATE,
                         P_ENDDATE          IN DATE,
                         P_SOURCEOFREC_ID   IN NVARCHAR2,
                         P_REMARK           IN NVARCHAR2,
                         P_USERNAME         IN NVARCHAR2,
                         P_MODIFY_BY_LOG    IN NVARCHAR2,
                         P_OUT              OUT NUMBER) IS
  BEGIN
    UPDATE RC_STAGE
       SET ID               = P_ID,
           ORG_ID           = P_ORG_ID,
           YEAR             = P_YEAR,
           ORGANIZATIONNAME = P_ORGANIZATIONNAME,
           TITLE            = P_TITLE,
           STARTDATE        = P_STARTDATE,
           ENDDATE          = P_ENDDATE,
           SOURCEOFREC_ID   = P_SOURCEOFREC_ID,
           REMARK           = P_REMARK,
           MODIFIED_DATE    = SYSDATE,
           MODIFIED_BY      = P_USERNAME,
           MODIFIED_LOG     = P_MODIFY_BY_LOG
     WHERE ID = P_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_STAGE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE DELETE_STAGE(P_ID IN NVARCHAR2, P_OUT OUT NUMBER) IS
  BEGIN
    DELETE FROM RC_STAGE
     WHERE INSTR(',' || P_ID || ',', ',' || ID || ',') > 0;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.DELETE_STAGE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE COSTALLOCATE_GET_BY_STAGE(P_RC_STAGE_ID IN NUMBER,
                                      P_CUR         OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT C.*, O.NAME_VN AS ORG_NAME, O.ID AS ORG_ID
        FROM RC_COSTALLOCATE C
       INNER JOIN HU_ORGANIZATION O
          ON O.ID = C.ORG_ID
       WHERE C.RC_STAGE_ID = P_RC_STAGE_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.COSTALLOCATE_GET_BY_STAGE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE ADDNEW_COSTALLOCATE(P_RC_STAGE_ID IN NUMBER,
                                P_ORG_ID      IN NUMBER,
                                P_COSTAMOUNT  IN NUMBER,
                                P_REMARK      IN NVARCHAR2,
                                P_USERNAME    IN NVARCHAR2,
                                P_CREATED_LOG IN NVARCHAR2,
                                P_OUT         OUT NUMBER) IS
  
  BEGIN
    INSERT INTO RC_COSTALLOCATE
      (ID,
       RC_STAGE_ID,
       ORG_ID,
       COSTAMOUNT,
       REMARK,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG)
    VALUES
      (SEQ_RC_COSTALLOCATE.NEXTVAL,
       P_RC_STAGE_ID,
       P_ORG_ID,
       P_COSTAMOUNT,
       P_REMARK,
       SYSDATE,
       P_USERNAME,
       P_CREATED_LOG);
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.ADDNEW_COSTALLOCATE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_COSTALLOCATE(P_ID            IN NUMBER,
                                P_RC_STAGE_ID   IN NUMBER,
                                P_ORG_ID        IN NUMBER,
                                P_COSTAMOUNT    IN NUMBER,
                                P_REMARK        IN NVARCHAR2,
                                P_USERNAME      IN NVARCHAR2,
                                P_MODIFY_BY_LOG IN NVARCHAR2,
                                P_OUT           OUT NUMBER) IS
  BEGIN
    UPDATE RC_COSTALLOCATE
       SET ID            = P_ID,
           RC_STAGE_ID   = P_RC_STAGE_ID,
           ORG_ID        = P_ORG_ID,
           COSTAMOUNT    = P_COSTAMOUNT,
           REMARK        = P_REMARK,
           MODIFIED_DATE = SYSDATE,
           MODIFIED_BY   = P_USERNAME,
           MODIFIED_LOG  = P_MODIFY_BY_LOG
     WHERE ID = P_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_COSTALLOCATE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE DELETE_COSTALLOCATE(P_ID IN NVARCHAR2, P_OUT OUT NUMBER) IS
  BEGIN
    DELETE FROM RC_COSTALLOCATE
     WHERE INSTR(',' || P_ID || ',', ',' || ID || ',') > 0;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.DELETE_COSTALLOCATE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE ADDNEW_MANNING_ORG(P_ORG_ID      IN NUMBER,
                               P_NAME        IN NVARCHAR2,
                               P_EFFECT_DATE IN DATE,
                               P_NOTE        IN NVARCHAR2,
                               P_OLD_MAN     IN NUMBER,
                               P_CURRENT_MAN IN NUMBER,
                               P_NEW_MAN     IN NUMBER,
                               P_MOBILIZE    IN NUMBER,
                               P_STATUS      IN NUMBER,
                               p_YEAR        IN NUMBER,
                               P_OUT         OUT NUMBER) IS
  BEGIN
    INSERT INTO RC_MANNING_ORG
      (ID,
       ORG_ID,
       NAME,
       EFFECT_DATE,
       NOTE,
       OLD_MANNING,
       CURRENT_MANNING,
       NEW_MANNING,
       MOBILIZE_COUNT_MANNING,
       STATUS,
       YEAR,
       MONTH)
    VALUES
      (SEQ_RC_MANNING_ORG.NEXTVAL,
       P_ORG_ID,
       P_NAME,
       P_EFFECT_DATE,
       P_NOTE,
       P_OLD_MAN,
       P_CURRENT_MAN,
       P_NEW_MAN,
       P_MOBILIZE,
       P_STATUS,
       p_YEAR,
       TO_CHAR(P_EFFECT_DATE, 'mm'));
  
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.ADDNEW_MANNING',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE ADDNEW_MANNING_TITLE(P_ORG_ID         IN NUMBER,
                                 P_MANNING_ORG_ID IN NUMBER,
                                 P_OUT            OUT NUMBER) IS
    PV_DEM NUMBER := 0;
  BEGIN
    SELECT COUNT(*)
      INTO PV_DEM
      FROM RC_MANNING_TITLE T
     WHERE T.ORG_ID IN
           (SELECT * FROM TABLE(TABLE_ORG_RIGHT('admin', P_ORG_ID)));
    IF PV_DEM > 0 THEN
      INSERT INTO RC_MANNING_TITLE
        (ID,
         Manning_Org_ID,
         TITLE_ID,
         OLD_MANNING,
         NEW_MANNING,
         CURRENT_MANNING,
         MOBILIZE_COUNT_MANNING,
         ORG_ID)
        SELECT SEQ_RC_MANNING_TITLE.NEXTVAL,
               P_MANNING_ORG_ID,
               Position.TITLE_ID AS TITLE_ID,
               NVL(Manning.New_Manning, 0) AS Manning_Old,
               --NVL(Manning.New_Manning,0) AS Manning_New,
               0 AS Manning_New,
               (select NVL(Count(ID), 0)
                  from Hu_Employee EMPL
                 where ORG_ID = Position.ORG_ID
                   AND (EMPL.JOIN_DATE <=
                       (select MO.EFFECT_DATE
                           from Rc_Manning_Org MO
                          where id = P_MANNING_ORG_ID) OR
                       EMPL.JOIN_DATE is null)
                   AND (select CODE
                          from Ot_Other_List
                         where ID = EMPL.Work_Status) = 'WORKING'
                   AND EMPL.TITLE_ID = Position.TITLE_ID) AS Manning_Current,
               0 as Manning_Mobilize,
               Position.ORG_ID
          FROM (select OT.ORG_ID, OT.TITLE_ID AS TITLE_ID
                  from hu_org_title OT
                /*  join hu_title T
                on OT.TITLE_ID = T.ID*/
                 where OT.ORG_ID IN
                       (SELECT *
                          FROM TABLE(TABLE_ORG_RIGHT('admin', P_ORG_ID)))
                   and OT.ACTFLG = 'A') Position
          LEFT JOIN (SELECT *
                       FROM (select MO.EFFECT_DATE,
                                    ROW_NUMBER() OVER(PARTITION BY MT.TITLE_ID ORDER BY MO.EFFECT_DATE DESC) AS ROW_NUMBER,
                                    MT.*
                               from rc_manning_title MT
                               join Rc_Manning_Org MO
                                 on MT.MANNING_ORG_ID = MO.ID
                              where MO.ORG_ID IN
                                    (SELECT *
                                       FROM TABLE(TABLE_ORG_RIGHT('admin',
                                                                  P_ORG_ID)))
                                and MO.Status = 1) A
                      WHERE A.ROW_NUMBER = 1) Manning
            on Position.Title_Id = Manning.TITLE_ID;
    ELSE
      INSERT INTO RC_MANNING_TITLE
        (ID,
         Manning_Org_ID,
         TITLE_ID,
         OLD_MANNING,
         NEW_MANNING,
         CURRENT_MANNING,
         MOBILIZE_COUNT_MANNING,
         ORG_ID)
        SELECT SEQ_RC_MANNING_TITLE.NEXTVAL,
               P_MANNING_ORG_ID,
               Position.TITLE_ID AS TITLE_ID,
               NVL(Manning.New_Manning, 0) AS Manning_Old,
               --NVL(Manning.New_Manning,0) AS Manning_New,
               0 AS Manning_New,
               (select NVL(Count(ID), 0)
                  from Hu_Employee EMPL
                 where ORG_ID = Position.ORG_ID
                   AND (EMPL.JOIN_DATE <=
                       (select MO.EFFECT_DATE
                           from Rc_Manning_Org MO
                          where id = P_MANNING_ORG_ID) OR
                       EMPL.JOIN_DATE is null)
                   AND (select CODE
                          from Ot_Other_List
                         where ID = EMPL.Work_Status) = 'WORKING'
                   AND EMPL.TITLE_ID = Position.TITLE_ID) AS Manning_Current,
               0 as Manning_Mobilize,
               Position.ORG_ID
          FROM (select OT.ORG_ID, T.ID AS TITLE_ID, T.NAME_VN
                  from hu_org_title OT
                  join hu_title T
                    on OT.TITLE_ID = T.ID
                 where OT.ORG_ID IN
                       (SELECT *
                          FROM TABLE(TABLE_ORG_RIGHT('admin', P_ORG_ID)))
                   and OT.ACTFLG = 'A'
                   and T.ACTFLG = 'A') Position
          LEFT JOIN (select MO.EFFECT_DATE, MT.*
                       from rc_manning_title MT
                       join Rc_Manning_Org MO
                         on MT.MANNING_ORG_ID = MO.ID
                      where MO.ORG_ID IN
                            (SELECT *
                               FROM TABLE(TABLE_ORG_RIGHT('admin', P_ORG_ID)))
                        and MO.Status = 1) Manning
            on Position.Title_Id = Manning.TITLE_ID;
    END IF;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.ADDNEW_MANNING',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_MANNING_ORG_ID(P_OUT OUT NUMBER) IS
  BEGIN
    SELECT MAX(MO.ID) INTO P_OUT FROM RC_MANNING_ORG MO;
  END;

  PROCEDURE UPDATE_MANNING_ORG(P_ID          IN NUMBER,
                               P_NAME        IN NVARCHAR2,
                               P_EFFECT_DATE IN DATE,
                               P_NOTE        IN NVARCHAR2,
                               P_STATUS      IN NUMBER,
                               P_OUT         OUT NUMBER) IS
  BEGIN
    UPDATE RC_MANNING_ORG
       SET NAME        = P_NAME,
           EFFECT_DATE = P_EFFECT_DATE,
           NOTE        = P_NOTE, --note update khi nhap trc tiep tren textbo
           STATUS      = P_STATUS
     WHERE ID = P_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_MANNING',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;
  PROCEDURE DELETE_RECORD_IMPORT(P_ID IN CLOB, P_OUT OUT NUMBER) IS
  BEGIN
    DELETE RC_MANNING_TITLE M
     WHERE INSTR(',' || P_ID || ',', ',' || M.ID || ',') > 0;
    P_OUT := 1;
  END;

  PROCEDURE UPDATE_NEW_MANNING_TITLE(P_ID       IN NUMBER,
                                     P_NEW_MAN  IN NUMBER,
                                     P_NOTE     IN NVARCHAR2,
                                     P_MOBILIZE IN NUMBER,
                                     P_OUT      OUT NUMBER) IS
    /* PV_ID NUMBER;*/
  BEGIN
    /*    SELECT T.MANNING_ORG_ID INTO PV_ID
    FROM RC_MANNING_TITLE T 
     WHERE T.ID = P_ID; */
    UPDATE RC_MANNING_TITLE M
       SET M.NEW_MANNING            = P_NEW_MAN,
           M.NOTE                   = P_NOTE,
           M.MOBILIZE_COUNT_MANNING = P_MOBILIZE
     WHERE M.ID = P_ID;
    /*UPDATE RC_MANNING_ORG T
    SET T.NOTE = P_NOTE
    WHERE T.ID = PV_ID;*/
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_NEW_MANNING_TITLE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_SUMMARY_MANNING_ORG(P_MANNING_ORG_ID IN NUMBER,
                                       P_OUT            OUT NUMBER) IS
  BEGIN
    UPDATE RC_MANNING_ORG MO
       SET MO.OLD_MANNING           =
           (select NVL(SUM(MT.OLD_MANNING), 0)
              from Rc_Manning_Title MT
             where MT.MANNING_ORG_ID = P_MANNING_ORG_ID),
           MO.CURRENT_MANNING       =
           (select NVL(SUM(MT.CURRENT_MANNING), 0)
              from Rc_Manning_Title MT
             where MT.MANNING_ORG_ID = P_MANNING_ORG_ID),
           MO.NEW_MANNING           =
           (select NVL(SUM(MT.NEW_MANNING), 0)
              from Rc_Manning_Title MT
             where MT.MANNING_ORG_ID = P_MANNING_ORG_ID),
           MO.MOBILIZE_COUNT_MANNING =
           (select NVL(SUM(MT.MOBILIZE_COUNT_MANNING), 0)
              from Rc_Manning_Title MT
             where MT.MANNING_ORG_ID = P_MANNING_ORG_ID)
     WHERE MO.ID = P_MANNING_ORG_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_NEW_MANNING_ORG',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_SUMMARY_MANNING_TITLE(P_MANNING_ORG_ID IN NUMBER,
                                         P_OUT            OUT NUMBER) IS
  BEGIN
    FOR C IN (SELECT *
                FROM RC_MANNING_TITLE MT
               WHERE MT.MANNING_ORG_ID = P_MANNING_ORG_ID) LOOP
      UPDATE RC_MANNING_TITLE MT
         SET MT.CURRENT_MANNING =
             (SELECT COUNT(*)
                FROM HU_EMPLOYEE E
               WHERE E.WORK_STATUS = 258
                 AND E.ORG_ID = C.ORG_ID
                 AND E.TITLE_ID = C.TITLE_ID)
       WHERE MT.ORG_ID = C.ORG_ID
         AND MT.TITLE_ID = C.TITLE_ID
         AND MT.MANNING_ORG_ID = P_MANNING_ORG_ID;
    END LOOP;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_NEW_MANNING_TITLE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE DELETE_MANNING(P_ID             IN NVARCHAR2,
                           P_MANNING_ORG_ID IN NUMBER,
                           P_OUT            OUT NUMBER) IS
    PV_COUNT NUMBER := 0;
  BEGIN
    DELETE FROM RC_MANNING_TITLE T
     WHERE INSTR(',' || P_ID || ',', ',' || ID || ',') > 0;
    SELECT COUNT(1)
      INTO PV_COUNT
      FROM RC_MANNING_TITLE T
     WHERE T.MANNING_ORG_ID = P_ID;
    IF PV_COUNT <= 0 THEN
      DELETE FROM RC_MANNING_ORG WHERE ID = P_MANNING_ORG_ID;
    END IF;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.DELETE_MANNING',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GETLIST_MANNING(P_ORG_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT MT.ID,
             ORG.NAME_VN               AS ORG_NAME,
             TI.NAME_VN                AS TITLE_NAME,
             MO.NAME,
             MO.EFFECT_DATE,
             MT.OLD_MANNING,
             MT.CURRENT_MANNING,
             MT.NEW_MANNING,
             MT.MOBILIZE_COUNT_MANNING,
             MT.STATUS,
             MT.NOTE
        FROM RC_MANNING_ORG MO
       INNER JOIN RC_MANNING_TITLE MT
          ON MO.ID = MT.MANNING_ORG_ID
       INNER JOIN HU_ORGANIZATION ORG
          ON MO.ORG_ID = ORG.ID
       INNER JOIN HU_TITLE TI
          ON MT.TITLE_ID = TI.ID
       INNER JOIN HU_ORG_TITLE OT
          ON OT.ORG_ID = ORG.ID
         AND OT.TITLE_ID = TI.ID
        LEFT JOIN OT_OTHER_LIST GRP
          ON TI.TITLE_GROUP_ID = GRP.ID
       WHERE TI.ACTFLG = 'A'
         AND OT.ACTFLG = 'A'
         AND MO.ORG_ID = P_ORG_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GETLIST_MANNING',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GETLIST_MANNING_BY_NAME(P_MANNING_ORG_ID IN NUMBER,
                                    P_ORG_ID         IN NUMBER,
                                    P_YEAR           IN NUMBER,
                                    P_CUR            OUT CURSOR_TYPE) IS
    query            NVARCHAR2(2000);
    PV_CHECK_COMPANY NUMBER := 0;
  BEGIN
    SELECT COUNT(1)
      INTO PV_CHECK_COMPANY
      FROM HUV_ORGANIZATION O
     WHERE O.ID = P_ORG_ID
       AND O.ORG_ID2 = P_ORG_ID;
    query := 'SELECT TI.ID as TITLE_ID, TI.NAME_VN AS TITLE_NAME,
             MO.NAME,
             MO.EFFECT_DATE,
             MT.ID,
             MT.OLD_MANNING,
             MT.CURRENT_MANNING as CURRENT_MANNING,
             MT.NEW_MANNING,
             MT.MOBILIZE_COUNT_MANNING,
             MT.STATUS,
             MT.NOTE,
             OT.NAME_VN TITLE_GROUP,
             ORG_N.NAME_VN AS ORG_NAME, 
      (select Count(ID) from Hu_Employee EMPL
      where ORG_ID = MT.ORG_ID
      AND (EMPL.JOIN_DATE <= MO.EFFECT_DATE OR EMPL.JOIN_DATE is null)
      AND (select CODE from Ot_Other_List where ID = EMPL.Work_Status) = ' ||
             '''WORKING''' || '
      AND EMPL.TITLE_ID = TI.ID) as EmployeeCount
      FROM RC_MANNING_TITLE MT
      INNER JOIN RC_MANNING_ORG MO ON MO.ID = MT.MANNING_ORG_ID
      INNER JOIN HU_ORGANIZATION ORG ON MO.ORG_ID = ORG.ID
      INNER JOIN HU_TITLE TI ON MT.TITLE_ID = TI.ID      
      LEFT JOIN OT_OTHER_LIST OT ON OT.ID = TI.TITLE_GROUP_ID 
      LEFT JOIN HU_ORGANIZATION ORG_N ON ORG_N.ID = MT.ORG_ID
      WHERE TI.ACTFLG = ''A'' ';
  
    IF P_MANNING_ORG_ID > 0 THEN
      IF PV_CHECK_COMPANY > 0 THEN
        query := query || ' AND MT.MANNING_ORG_ID = ' || P_MANNING_ORG_ID ||
                 ' AND MT.ORG_ID IN (SELECT * FROM TABLE(TABLE_ORG_RIGHT(' ||
                 '''ADMIN''' || ')))';
      ELSE
        query := query || ' AND MT.MANNING_ORG_ID = ' || P_MANNING_ORG_ID ||
                 ' AND MT.ORG_ID IN (SELECT * FROM TABLE(TABLE_ORG_RIGHT(' ||
                 '''ADMIN''' || ', ' || P_ORG_ID || ')))';
      END IF;
      IF P_YEAR > 0 THEN
        query := query || ' AND TO_CHAR(MO.EFFECT_DATE,' || '''RRRR''' ||
                 ') = ' || P_YEAR;
      END IF;
    ELSE
      query := query ||
               ' AND MT.ORG_ID IN (SELECT * FROM TABLE(TABLE_ORG_RIGHT(' ||
               '''ADMIN''' || ', ' || P_ORG_ID || ')))';
      IF P_YEAR > 0 THEN
        query := query || ' AND TO_CHAR(MO.EFFECT_DATE,' || '''RRRR''' ||
                 ') = ' || P_YEAR;
      END IF;
    END IF;
  
    query := query || ' ORDER BY MO.EFFECT_DATE DESC';
  
    insert into at_strsql values (seq_at_strsql.nextval, query);
    commit;
    OPEN P_CUR FOR TO_CHAR(query);
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GETLIST_MANNING_FROM_YEAR',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GETCOUNT_EMPWORKING(P_TITLE IN NUMBER, P_OUT OUT NUMBER) IS
  BEGIN
    P_OUT := 0;
    SELECT COUNT(*)
      INTO P_OUT
      FROM HU_EMPLOYEE EMP
     INNER JOIN HU_ORGANIZATION ORG
        ON EMP.ORG_ID = ORG.ID
     INNER JOIN HU_TITLE TI
        ON EMP.TITLE_ID = TI.ID
     INNER JOIN HU_ORG_TITLE OT
        ON OT.ORG_ID = ORG.ID
       AND OT.TITLE_ID = TI.ID
      LEFT JOIN OT_OTHER_LIST GRP
        ON TI.TITLE_GROUP_ID = GRP.ID
     WHERE TI.ACTFLG = 'A'
       AND OT.ACTFLG = 'A'
       AND TI.ID = P_TITLE; --EMP.WORK_STATUS
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GETCOUNT_EMPWORKING',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_OLD_MANNING(P_EFFECT_DATE_NEW IN DATE, P_OUT OUT NUMBER) IS
  BEGIN
    P_OUT := 0;
    SELECT M.NEW_MANNING
      INTO P_OUT
      FROM RC_MANNING_TITLE M
      LEFT JOIN RC_MANNING_ORG O
        ON O.ID = M.MANNING_ORG_ID
     WHERE O.EFFECT_DATE < P_EFFECT_DATE_NEW;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_OLD_MANNING',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_CURRENT_MANNING_TITLE(P_ORG_ID   IN NUMBER,
                                      P_TITLE_ID IN NUMBER,
                                      P_CUR      OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      select MT.*
        from Rc_Manning_Title MT
        join Rc_Manning_Org MO
          on MT.MANNING_ORG_ID = MO.ID
      -- Status = 1: ?? duy?t
       where MO.Status = 1
            -- AND MT.ORG_ID = P_ORG_ID
         AND MT.Title_Id = P_TITLE_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_CURRENT_MANNING_TITLE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;
  PROCEDURE GET_CURRENT_MANNING_TITLE1(P_ORG_ID   IN NUMBER,
                                       P_TITLE_ID IN NUMBER,
                                       P_DATE     IN DATE,
                                       P_CUR      OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      select MT.*
        from Rc_Manning_Title MT
        join Rc_Manning_Org MO
          on MT.MANNING_ORG_ID = MO.ID
      -- Status = 1: ?? duy?t
       where MO.Status = 1
         AND MT.ORG_ID = P_ORG_ID
         AND MT.Title_Id = P_TITLE_ID
         AND TO_CHAR(MO.EFFECT_DATE, 'DD/MM/YYYY') <=
             TO_CHAR(P_DATE, 'DD/MM/YYYY')
         AND ROWNUM = 1
       ORDER BY MO.EFFECT_DATE DESC;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_CURRENT_MANNING_TITLE1',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;
  PROCEDURE LOAD_INFO_MANNING_EMPBYORG(P_ORG   IN NUMBER,
                                       P_TITLE IN NUMBER,
                                       P_CUR   OUT CURSOR_TYPE) IS
  BEGIN
    /*    SELECT SEQ_ORG_TEMP_TABLE.NEXTVAL INTO PV_SEQ FROM DUAL;
    PKG_FUNCTION.GET_ORG_TABLE_TEMP(P_LISTORG, PV_SEQ);*/
    -- L?y danh s?ch dang k? ngh?
    OPEN P_CUR FOR
      SELECT M.ID ID,
             ORG.NAME_VN AS ORG_NAME,
             TI.NAME_VN AS TITLE_NAME,
             MO.NAME AS "NAME",
             TO_CHAR(MO.EFFECT_DATE, 'DD/MM/RRRR') AS "EFFECT_DATE",
             M.NOTE AS NOTE,
             M.STATUS AS STATUS
        FROM RC_MANNING_TITLE M
        LEFT JOIN RC_MANNING_ORG MO
          ON MO.ID = M.MANNING_ORG_ID
       INNER JOIN HU_ORGANIZATION ORG
          ON MO.ORG_ID = ORG.ID
       INNER JOIN HU_TITLE TI
          ON M.TITLE_ID = TI.ID
       INNER JOIN HU_ORG_TITLE OT
          ON OT.ORG_ID = ORG.ID
         AND OT.TITLE_ID = TI.ID
        LEFT JOIN OT_OTHER_LIST GRP
          ON TI.TITLE_GROUP_ID = GRP.ID
       WHERE TI.ACTFLG = 'A'
         AND OT.ACTFLG = 'A'
         AND MO.ORG_ID = P_ORG
         AND M.TITLE_ID = P_TITLE;
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.LOAD_INFO_MANNING_EMPBYORG',
                              SQLERRM,
                              NULL,
                              NULL,
                              null,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE EXPORT_DATA_TEMPLATE_MANNING(P_ORG_ID IN NVARCHAR2,
                                         P_TITLE  IN NUMBER,
                                         -- P_REQUEST_ID  IN NUMBER,
                                         p_CUR_OUT OUT CURSOR_TYPE) IS
  
  BEGIN
    OPEN P_CUR_OUT FOR
      SELECT ORG.NAME_VN AS ORG_NAME,
             TI.NAME_VN AS TITLE_NAME,
             MO.NAME,
             TO_CHAR(MO.EFFECT_DATE, 'DD/MM/RRRR') AS EFFECT_DATE,
             M.NOTE
      --INTO PV_NAME, PV_DAY, PV_NOTE
        FROM RC_MANNING_TITLE M
        LEFT JOIN RC_MANNING_ORG MO
          ON MO.ID = M.MANNING_ORG_ID
       INNER JOIN HU_ORGANIZATION ORG
          ON MO.ORG_ID = ORG.ID
       INNER JOIN HU_TITLE TI
          ON M.TITLE_ID = TI.ID
       INNER JOIN HU_ORG_TITLE OT
          ON OT.ORG_ID = ORG.ID
         AND OT.TITLE_ID = TI.ID
        LEFT JOIN OT_OTHER_LIST GRP
          ON TI.TITLE_GROUP_ID = GRP.ID
       WHERE TI.ACTFLG = 'A'
         AND OT.ACTFLG = 'A'
         AND M.TITLE_ID = P_TITLE;
  EXCEPTION
    WHEN OTHERS THEN
      --  PKG_HCM_SYSTEM.PRU_UPDATE_LOG_IN_REQUEST(P_REQUEST_ID, SQLERRM);
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.EXPORT_DATA_TEMPLATE_MANNING',
                              SQLERRM,
                              P_ORG_ID,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
    
  END;

  PROCEDURE LOAD_COMBOBOX_YEAR(P_ORG_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
    PV_CHECK_COMPANY NUMBER := 0;
  BEGIN
    SELECT COUNT(1)
      INTO PV_CHECK_COMPANY
      FROM HUV_ORGANIZATION O
     WHERE O.ID = P_ORG_ID
       AND O.ORG_ID2 = P_ORG_ID;
    IF PV_CHECK_COMPANY > 0 THEN
      OPEN P_CUR FOR
        SELECT DISTINCT TO_CHAR(M.EFFECT_DATE, 'RRRR') AS "YEAR"
          FROM RC_MANNING_ORG M
         INNER JOIN RC_MANNING_TITLE T
            ON T.MANNING_ORG_ID = M.ID
         WHERE M.ORG_ID = P_ORG_ID
         ORDER BY "YEAR" DESC;
    ELSE
      OPEN P_CUR FOR
        SELECT DISTINCT TO_CHAR(M.EFFECT_DATE, 'RRRR') AS "YEAR"
          FROM RC_MANNING_ORG M
         INNER JOIN RC_MANNING_TITLE T
            ON T.MANNING_ORG_ID = M.ID
         WHERE T.ORG_ID = P_ORG_ID
         ORDER BY "YEAR" DESC;
    END IF;
  END;

  PROCEDURE LOAD_YEAR_PLANREG(P_ORG_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT DISTINCT TO_CHAR(M.SEND_DATE, 'RRRR') AS "YEAR"
        FROM RC_PLAN_REG M
        JOIN Hu_Organization O
          on (O.Id = M.Org_Id OR O.Parent_Id = M.ORG_ID)
       WHERE O.ID = P_ORG_ID
          OR O.PARENT_ID = P_ORG_ID
       ORDER BY "YEAR" DESC;
  END;

  PROCEDURE LOAD_COMBOBOX_LISTMANNINGNAME(P_ORG_ID IN NUMBER,
                                          P_YEAR   IN NUMBER,
                                          P_CUR    OUT CURSOR_TYPE) IS
    PV_CHECK_COMPANY NUMBER := 0;
  BEGIN
    SELECT COUNT(1)
      INTO PV_CHECK_COMPANY
      FROM HUV_ORGANIZATION O
     WHERE O.ID = P_ORG_ID
       AND O.ORG_ID2 = P_ORG_ID;
    IF PV_CHECK_COMPANY > 0 THEN
      OPEN P_CUR FOR
        SELECT DISTINCT M.ID, M.NAME, M.EFFECT_DATE
          FROM RC_MANNING_ORG M
         INNER JOIN RC_MANNING_TITLE T
            ON T.MANNING_ORG_ID = M.ID
         WHERE TO_CHAR(M.EFFECT_DATE, 'RRRR') = P_YEAR
           AND M.ORG_ID = P_ORG_ID
         ORDER BY M.EFFECT_DATE DESC;
    ELSE
      OPEN P_CUR FOR
        SELECT DISTINCT M.ID, M.NAME, M.EFFECT_DATE
          FROM RC_MANNING_ORG M
         INNER JOIN RC_MANNING_TITLE T
            ON T.MANNING_ORG_ID = M.ID
         WHERE TO_CHAR(M.EFFECT_DATE, 'RRRR') = P_YEAR
           AND T.ORG_ID = P_ORG_ID
         ORDER BY M.EFFECT_DATE DESC;
    END IF;
  
  END;

  PROCEDURE XUAT_TO_TRINH(P_STAGE_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT ROWNUM STT,
             T.NAME_VN AS "CHUCDANH",
             O.NAME_VN AS "BOPHAN",
             R.MALE_NUMBER + R.FEMALE_NUMBER AS "SOLUONG",
             R.AGE_FROM || '-' || R.AGE_TO AS "DOTUOI",
             OT_L.NAME_VN || '-' || OT_LV.NAME_VN || '-' ||
             R.LANGUAGESCORES AS "TRINHDONN",
             OT_M.NAME_VN AS "CHUYENMON",
             OT_CO.NAME_VN AS "TINHOC",
             OT_SP.NAME_VN AS "KNDB"
        FROM RC_REQUEST R
       INNER JOIN HU_TITLE T
          ON T.ID = R.TITLE_ID
       INNER JOIN HU_ORGANIZATION O
          ON O.ID = R.ORG_ID
       INNER JOIN RC_PROGRAM P
          ON P.RC_REQUEST_ID = R.ID
       INNER JOIN RC_STAGE S
          ON S.ID = P.STAGE_ID
        LEFT JOIN OT_OTHER_LIST OT_L
          ON OT_L.ID = R.LANGUAGE
         AND OT_L.TYPE_CODE = 'LANGUAGE'
        LEFT JOIN OT_OTHER_LIST OT_LV
          ON OT_LV.ID = R.LANGUAGELEVEL
         AND OT_LV.TYPE_CODE = 'LANGUAGE_LEVEL'
        LEFT JOIN OT_OTHER_LIST OT_SP
          ON OT_SP.ID = R.SPECIALSKILLS
         AND OT_SP.TYPE_CODE = 'SPECIALSKILLS'
        LEFT JOIN OT_OTHER_LIST OT_CO
          ON OT_CO.ID = R.COMPUTER_LEVEL
         AND OT_CO.TYPE_CODE = 'COMPUTER_LEVEL'
        LEFT JOIN OT_OTHER_LIST OT_M
          ON OT_M.ID = R.QUALIFICATION
         AND OT_M.TYPE_CODE = 'MAJOR'
       WHERE S.ID = P_STAGE_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.XUAT_TO_TRINH',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE COST_GETLIST_BYFILTER(P_ORG_ID IN NUMBER,
                                  P_CUR    OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT C.*, ST.TITLE, OT.NAME_VN AS SOURCE_NAME, OT.ID AS SOURCE_ID
        FROM RC_COST C
        LEFT JOIN HU_ORGANIZATION O
          ON C.ORG_ID = O.ID
        LEFT JOIN OT_OTHER_LIST OT
          ON C.SourceOfRec_ID = OT.CODE
        LEFT JOIN RC_STAGE ST
          ON C.RC_STAGE_ID = ST.ID
       WHERE C.ORG_ID = P_ORG_ID
       ORDER BY C.ID DESC;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.COST_GETLIST_BYFILTER',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE COST_GET_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT S.*, OT.NAME_VN AS SOURCE_NAME, OT.ID AS SOURCE_ID
        FROM RC_COST S
       INNER JOIN HU_ORGANIZATION O
          ON O.ID = S.ORG_ID
        LEFT JOIN OT_OTHER_LIST OT
          ON S.Sourceofrec_Id = OT.CODE
       WHERE S.ID = P_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.COST_GET_BY_ID',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE ADDNEW_COST(P_RC_STAGE_ID    IN NUMBER,
                        P_ORG_ID         IN NUMBER,
                        P_SOURCEOFREC_ID IN NVARCHAR2,
                        P_COSTESTIMATE   IN NUMBER,
                        P_COSTREALITY    IN NUMBER,
                        P_REMARK         IN NVARCHAR2,
                        P_USERNAME       IN NVARCHAR2,
                        P_CREATED_LOG    IN NVARCHAR2,
                        P_OUT            OUT NUMBER) IS
  
  BEGIN
    INSERT INTO RC_COST
      (ID,
       RC_STAGE_ID,
       ORG_ID,
       SOURCEOFREC_ID,
       COSTESTIMATE,
       COSTREALITY,
       REMARK,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG)
    VALUES
      (SEQ_RC_COST.NEXTVAL,
       P_RC_STAGE_ID,
       P_ORG_ID,
       P_SOURCEOFREC_ID,
       P_COSTESTIMATE,
       P_COSTREALITY,
       P_REMARK,
       SYSDATE,
       P_USERNAME,
       P_CREATED_LOG);
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.ADDNEW_COST',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_COST(P_ID             IN NUMBER,
                        P_RC_STAGE_ID    IN NUMBER,
                        P_ORG_ID         IN NUMBER,
                        P_SOURCEOFREC_ID IN NVARCHAR2,
                        P_COSTESTIMATE   IN NUMBER,
                        P_COSTREALITY    IN NUMBER,
                        P_REMARK         IN NVARCHAR2,
                        P_USERNAME       IN NVARCHAR2,
                        P_MODIFY_BY_LOG  IN NVARCHAR2,
                        P_OUT            OUT NUMBER) IS
  BEGIN
    UPDATE RC_COST
       SET ID             = P_ID,
           RC_STAGE_ID    = P_RC_STAGE_ID,
           ORG_ID         = P_ORG_ID,
           SOURCEOFREC_ID = P_SOURCEOFREC_ID,
           COSTESTIMATE   = P_COSTESTIMATE,
           COSTREALITY    = P_COSTREALITY,
           REMARK         = P_REMARK,
           MODIFIED_DATE  = SYSDATE,
           MODIFIED_BY    = P_USERNAME,
           MODIFIED_LOG   = P_MODIFY_BY_LOG
     WHERE ID = P_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_COST',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE DELETE_COST(P_ID IN NVARCHAR2, P_OUT OUT NUMBER) IS
  BEGIN
    DELETE FROM RC_COST
     WHERE INSTR(',' || P_ID || ',', ',' || ID || ',') > 0;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.DELETE_COST',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE COST_COSTALLOCATE_GET_BY_COST(P_RC_COST_ID IN NUMBER,
                                          P_CUR        OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT C.*, O.NAME_VN AS ORG_NAME, O.ID AS ORG_ID
        FROM RC_COST_COSTALLOCATE C
       INNER JOIN HU_ORGANIZATION O
          ON O.ID = C.ORG_ID
       WHERE C.RC_COST_ID = P_RC_COST_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.COST_COSTALLOCATE_GET_BY_COST',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE COST_UPDATE_COSTREALITY(P_COST_ID IN NUMBER, P_OUT OUT NUMBER) IS
  BEGIN
    UPDATE RC_COST
       SET COSTREALITY =
           (select SUM(NVL(C.COSTAMOUNT, 0))
              from Rc_Cost_Costallocate C
             where C.RC_COST_ID = P_COST_ID)
     WHERE ID = P_COST_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.COST_UPDATE_COSTESTIMATE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE ADDNEW_COST_COSTALLOCATE(P_RC_COST_ID  IN NUMBER,
                                     P_ORG_ID      IN NUMBER,
                                     P_COSTAMOUNT  IN NUMBER,
                                     P_REMARK      IN NVARCHAR2,
                                     P_USERNAME    IN NVARCHAR2,
                                     P_CREATED_LOG IN NVARCHAR2,
                                     P_OUT         OUT NUMBER) IS
  
  BEGIN
    INSERT INTO RC_COST_COSTALLOCATE
      (ID,
       RC_COST_ID,
       ORG_ID,
       COSTAMOUNT,
       REMARK,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG)
    VALUES
      (SEQ_RC_COST_COSTALLOCATE.NEXTVAL,
       P_RC_COST_ID,
       P_ORG_ID,
       P_COSTAMOUNT,
       P_REMARK,
       SYSDATE,
       P_USERNAME,
       P_CREATED_LOG);
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.ADDNEW_COST_COSTALLOCATE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_COST_COSTALLOCATE(P_ID            IN NUMBER,
                                     P_RC_COST_ID    IN NUMBER,
                                     P_ORG_ID        IN NUMBER,
                                     P_COSTAMOUNT    IN NUMBER,
                                     P_REMARK        IN NVARCHAR2,
                                     P_USERNAME      IN NVARCHAR2,
                                     P_MODIFY_BY_LOG IN NVARCHAR2,
                                     P_OUT           OUT NUMBER) IS
  BEGIN
    UPDATE RC_COST_COSTALLOCATE
       SET ID            = P_ID,
           RC_COST_ID    = P_RC_COST_ID,
           ORG_ID        = P_ORG_ID,
           COSTAMOUNT    = P_COSTAMOUNT,
           REMARK        = P_REMARK,
           MODIFIED_DATE = SYSDATE,
           MODIFIED_BY   = P_USERNAME,
           MODIFIED_LOG  = P_MODIFY_BY_LOG
     WHERE ID = P_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_COST_COSTALLOCATE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE DELETE_COST_COSTALLOCATE(P_ID IN NVARCHAR2, P_OUT OUT NUMBER) IS
  BEGIN
    DELETE FROM RC_COST_COSTALLOCATE
     WHERE INSTR(',' || P_ID || ',', ',' || ID || ',') > 0;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.DELETE_COST_COSTALLOCATE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE CANDIDATE_RESULT_GETBYFILTER(P_PROGRAM_ID       IN NUMBER,
                                         P_PROGRAM_EXAMS_ID IN NUMBER,
                                         P_CUR              OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      select C.CANDIDATE_CODE Code,
             C.Fullname_Vn as FullName,
             CCV.Birth_Date as DOB,
             CCV.MOBILE_PHONE as Mobile,
             CCV.ID_NO as IDNO,
             PS.Schedule_Date as ScheduleDate,
             PSC.Point_Result as Marks,
             PSC.Comment_Info as CommentInfo,
             PSC.Assessment_Info as Assessment,
             PSC.Is_Pass as IsPass,
             (select NAME_VN from OT_Other_List where id = CCV.GENDER) as Gender,
             (select NAME_VN from OT_Other_List where CODE = C.Status_Id) as Status
        from Rc_Candidate C
        left join RC_Candidate_CV CCV
          on C.ID = CCV.CANDIDATE_ID
        left join Rc_Program_Exams PE
          on C.rc_program_id = PE.rc_program_id
        left join Rc_Program_Schedule PS
          on (C.rc_program_id = PS.RC_PROGRAM_ID and
             PE.ID = PS.RC_PROGRAM_EXAMS_ID)
        left join Rc_Program_Schedule_Can PSC
          on PS.ID = PSC.RC_PROGRAM_SCHEDULE_ID
       where C.RC_PROGRAM_ID = P_PROGRAM_ID
         and PE.ID = P_PROGRAM_EXAMS_ID
       ORDER BY PS.Schedule_Date DESC;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.CANDIDATE_RESULT_GETBYFILTER',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE CANDIDATE_LIST_GETBYPROGRAM(P_PROGRAM_ID IN NUMBER,
                                        P_CUR        OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      select distinct C.ID,
                      C.CANDIDATE_CODE Code,
                      C.Fullname_Vn as FullName,
                      CCV.Birth_Date as DOB,
                      CCV.MOBILE_PHONE as Mobile,
                      CCV.ID_NO as IDNO,
                      (select NAME_VN
                         from OT_Other_List
                        where ID = CCV.GENDER) as Gender,
                      CCV.Per_Email as Email,
                      (select NAME_VN
                         from OT_Other_List
                        where CODE = C.Status_Id) as Status
        from rc_program_schedule_can PSC
        left join RC_Candidate C
          on psc.candidate_id = c.ID
        left join RC_Candidate_CV CCV
          on C.ID = CCV.CANDIDATE_ID
       where C.RC_PROGRAM_ID = P_PROGRAM_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.CANDIDATE_LIST_GETBYPROGRAM',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE CANDIDATE_GET_AVERAGE_MARKS(P_PROGRAM_ID   IN NUMBER,
                                        P_CANDIDATE_ID IN NUMBER,
                                        P_OUT          OUT NUMBER) IS
  BEGIN
    P_OUT := 0;
    SELECT CAST(AVG(Point_Result) AS DECIMAL(10, 1))
      Into P_OUT
      from Rc_Program_Schedule_Can PSC
      left join Rc_Program_Schedule PS
        on PSC.RC_PROGRAM_SCHEDULE_ID = PS.ID
      left join Rc_Program_Exams PE
        on PSC.RC_PROGRAM_EXAMS_ID = PE.ID
     where PS.Rc_Program_Id = P_PROGRAM_ID
       and PSC.CANDIDATE_ID = P_CANDIDATE_ID
       and PE.Is_PV = 0;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.CANDIDATE_GET_AVERAGE_MARKS',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE EXAMS_GETBYCANDIDATE(P_PROGRAM_ID   IN NUMBER,
                                 P_CANDIDATE_ID IN NUMBER,
                                 P_IS_PV        IN NUMBER,
                                 P_CUR          OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      select PE.NAME as EXAM_NAME,
             PE.POINT_LADDER,
             PE.POINT_PASS,
             PS.SCHEDULE_DATE,
             PSC.*,
             Case PSC.Is_Pass
               When 1 then
                'True'
               ELSE
                'False'
             END as ISPASS,
             PSC.Is_Pass
        from Rc_Program_Schedule_Can PSC
        left join Rc_Program_Schedule PS
          on PSC.RC_PROGRAM_SCHEDULE_ID = PS.ID
        left join Rc_Program_Exams PE
          on PSC.RC_PROGRAM_EXAMS_ID = PE.ID
       where PS.Rc_Program_Id = P_PROGRAM_ID
         and PSC.CANDIDATE_ID = P_CANDIDATE_ID
         and PE.Is_PV = P_IS_PV
       ORDER BY PS.SCHEDULE_DATE DESC;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.EXAMS_GETBYCANDIDATE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_CANDIDATE_RESULT(P_ID              IN NUMBER,
                                    P_POINT_RESULT    IN NUMBER,
                                    P_COMMENT_INFO    IN NVARCHAR2,
                                    P_ASSESSMENT_INFO IN NVARCHAR2,
                                    P_IS_PASS         IN NUMBER,
                                    P_OUT             OUT NUMBER) IS
  BEGIN
    UPDATE RC_PROGRAM_SCHEDULE_CAN
       SET POINT_RESULT    = P_POINT_RESULT,
           COMMENT_INFO    = P_COMMENT_INFO,
           ASSESSMENT_INFO = P_ASSESSMENT_INFO,
           IS_PASS         = P_IS_PASS
     WHERE ID = P_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_CANDIDATE_RESULT',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE DELETE_CANDIDATE_RESULT(P_ID IN NVARCHAR2, P_OUT OUT NUMBER) IS
  BEGIN
    DELETE FROM Rc_Program_Schedule_Can
     WHERE INSTR(',' || P_ID || ',', ',' || ID || ',') > 0;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.DELETE_CANDIDATE_RESULT',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_CANDIDATE_STATUS(P_ID          IN NUMBER,
                                    P_STATUS_CODE IN NVARCHAR2,
                                    P_OUT         OUT NUMBER) IS
    PV_COUNT NUMBER;
  BEGIN
    IF P_STATUS_CODE = 'DAT' THEN
      SELECT COUNT(*)
        INTO PV_COUNT
        FROM RC_CANDIDATE E
       INNER JOIN RC_PROGRAM_SCHEDULE_CAN C
          ON C.CANDIDATE_ID = E.ID
       INNER JOIN RC_PROGRAM_EXAMS EX
          ON EX.ID = C.RC_PROGRAM_EXAMS_ID
       WHERE E.ID = P_ID
         AND NVL(EX.IS_PV, 0) <> 0
         AND NVL(C.IS_PASS, -1) IN (-1, 0);
    
      IF PV_COUNT = 0 THEN
        UPDATE RC_CANDIDATE SET STATUS_ID = P_STATUS_CODE WHERE ID = P_ID;
      END IF;
    ELSE
      UPDATE RC_CANDIDATE SET STATUS_ID = P_STATUS_CODE WHERE ID = P_ID;
    END IF;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_CANDIDATE_STATUS',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE EXPORT_RECRUITMENT_NEEDS(P_ID         IN NUMBER,
                                     P_DinhBien   IN NUMBER,
                                     P_HienCo     IN NUMBER,
                                     P_SLCanTuyen IN NUMBER,
                                     p_CUR_OUT    OUT CURSOR_TYPE) IS
  
  BEGIN
    OPEN P_CUR_OUT FOR
      SELECT P_DinhBien AS DINHBIEN,
             TT.NAME_VN AS CHUCDANH,
             ORG.NAME_VN AS BOPHAN,
             P_HienCo AS HIENCO,
             P_SLCanTuyen AS SLCANTUYEN,
             EXPERIENCE_NUMBER AS TIEUCHUANTUYENDUNG,
             OT4.Name_Vn AS YEUCAUKHAC,
             TO_CHAR(EXPECTED_JOIN_DATE, 'DD/MM/YYYY') AS NGAYBATDAU,
             TO_CHAR(SYSDATE, 'YYYY') AS NAM,
             TO_CHAR(SYSDATE, 'MM') AS THANG,
             TO_CHAR(SYSDATE, 'DD') AS NGAY,
             (SELECT FN_GET_QUY_HIENTAITRONGNAM(TO_CHAR(SYSDATE, 'MM'))
                FROM DUAL) AS QUY
        FROM RC_REQUEST RC
       INNER JOIN HU_ORGANIZATION ORG
          ON ORG.ID = RC.ORG_ID
       INNER JOIN HU_TITLE TT
          ON TT.ID = RC.TITLE_ID
        LEFT JOIN OT_OTHER_LIST OT1
          ON OT1.ID = RC.RECRUIT_REASON_ID
        LEFT JOIN OT_OTHER_LIST OT2
          ON OT2.ID = RC.STATUS_ID
        LEFT JOIN OT_OTHER_LIST OT3
          ON OT3.ID = RC.LEARNING_LEVEL_ID
        LEFT JOIN OT_OTHER_LIST OT4
          ON OT4.ID = RC.SPECIALSKILLS
        LEFT JOIN HU_CONTRACT_TYPE HUC
          ON HUC.ID = RC.LEARNING_LEVEL_ID
       WHERE RC.ID = P_ID;
  
  EXCEPTION
    WHEN OTHERS THEN
      --  PKG_HCM_SYSTEM.PRU_UPDATE_LOG_IN_REQUEST(P_REQUEST_ID, SQLERRM);
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.EXPORT_RECRUITMENT_NEEDS',
                              SQLERRM,
                              P_ID,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
    
  END;
  PROCEDURE EXPORT_RC_NEEDS_BYLISTID(P_ID IN NVARCHAR2,
                                     --p_CUR_OUT     OUT CURSOR_TYPE,
                                     p_CUR_OUT OUT CURSOR_TYPE) IS
    P_DINHBIEN       NUMBER;
    P_MANNING_ORG_ID NUMBER;
  BEGIN
  
    select M.NEW_MANNING, M.MANNING_ORG_ID
      INTO P_DINHBIEN, P_MANNING_ORG_ID
      from (select MT.*
              from Rc_Manning_Title MT
              join Rc_Manning_Org MO
                on MO.ID = MT.MANNING_ORG_ID
             where MO.ORG_ID =
                   (select rq.org_id from RC_REQUEST rq where id = P_ID)
               AND MT.TITLE_ID =
                   (select rq.title_id from RC_REQUEST rq where id = P_ID)
               AND MO.Status = 1
             ORDER BY MO.Effect_Date DESC) M
     where rownum = 1;
  
    /*OPEN P_CUR_OUT FOR
    SELECT TO_CHAR (SYSDATE, 'YYYY') AS NAM
      ,TO_CHAR (SYSDATE, 'MM') AS THANG
      ,TO_CHAR (SYSDATE, 'DD') AS NGAY
      FROM DUAL; */
    OPEN P_CUR_OUT FOR
      SELECT ROW_NUMBER() OVER(ORDER BY RC.ID desc) AS STT,
             p_DINHBIEN AS DINHBIEN,
             TT.NAME_VN AS CHUCDANH,
             ORG.NAME_VN AS BOPHAN,
             (MALE_NUMBER + FEMALE_NUMBER) AS SLCANTUYEN,
             (select NVL(Count(ID), 0)
                from Hu_Employee EMPL
               where ORG_ID = ORG.Id
                 AND (EMPL.JOIN_DATE <=
                     (select MO.EFFECT_DATE
                         from Rc_Manning_Org MO
                        where ID = P_MANNING_ORG_ID) OR
                     EMPL.JOIN_DATE is null)
                 AND (select CODE
                        from Ot_Other_List
                       where ID = EMPL.Work_Status) = 'WORKING'
                 AND EMPL.TITLE_ID = TT.Id) AS HIENCO,
             EXPERIENCE_NUMBER AS TIEUCHUANTUYENDUNG,
             OT4.Name_Vn AS YEUCAUKHAC,
             TO_CHAR(EXPECTED_JOIN_DATE, 'DD/MM/YYYY') AS NGAYBATDAU,
             TO_CHAR(SYSDATE, 'YYYY') AS NAM,
             TO_CHAR(SYSDATE, 'MM') AS THANG,
             TO_CHAR(SYSDATE, 'DD') AS NGAY,
             (SELECT FN_GET_QUY_HIENTAITRONGNAM(TO_CHAR(SYSDATE, 'MM'))
                FROM DUAL) AS QUY,
             (SELECT O.NAME_VN
                FROM RC_REQUEST R
                left JOIN OT_OTHER_LIST O
                  ON R.RECRUIT_REASON_ID = O.ID
               WHERE R.ID = RC.ID) AS LYDOTUYENDUNG,
             TO_CHAR(RC.EXPECTED_JOIN_DATE, 'DD/MM/YYYY') AS NGAYHOANTHANH
        FROM RC_REQUEST RC
       INNER JOIN HU_ORGANIZATION ORG
          ON ORG.ID = RC.ORG_ID
       INNER JOIN HU_TITLE TT
          ON TT.ID = RC.TITLE_ID
        LEFT JOIN OT_OTHER_LIST OT1
          ON OT1.ID = RC.RECRUIT_REASON_ID
        LEFT JOIN OT_OTHER_LIST OT2
          ON OT2.ID = RC.STATUS_ID
        LEFT JOIN OT_OTHER_LIST OT3
          ON OT3.ID = RC.LEARNING_LEVEL_ID
        LEFT JOIN OT_OTHER_LIST OT4
          ON OT4.ID = RC.SPECIALSKILLS
        LEFT JOIN HU_CONTRACT_TYPE HUC
          ON HUC.ID = RC.LEARNING_LEVEL_ID
       WHERE INSTR(',' || P_ID || ',', ',' || RC.ID || ',') > 0;
  EXCEPTION
    WHEN OTHERS THEN
      --  PKG_HCM_SYSTEM.PRU_UPDATE_LOG_IN_REQUEST(P_REQUEST_ID, SQLERRM);
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.EXPORT_RECRUITMENT_NEEDS_BY_LISTID',
                              SQLERRM,
                              P_ID,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
    
  END;

  -- Danh sach De Nghi Ky HDLD Thu Viec
  PROCEDURE EXPORT_DE_NGHI_THU_VIEC(P_PROGRAM_ID             IN NUMBER,
                                    table_CELL_EXAMS_MAPPING OUT CURSOR_TYPE,
                                    table_CELL_MAPPING       OUT CURSOR_TYPE,
                                    table_INFO               OUT CURSOR_TYPE,
                                    table_DETAIL             OUT CURSOR_TYPE) IS
  
    Lst   NVARCHAR2(4000);
    query NVARCHAR2(4000);
  BEGIN
  
    -- Format cell Mon Thi/PV mapping --> [Table]
    OPEN table_CELL_EXAMS_MAPPING FOR
      SELECT PE.NAME, '&=[TABLE3].COL' || PE.ID as DATA
        FROM rc_program_exams PE
       where PE.rc_program_id = P_PROGRAM_ID;
  
    -- Format cell mapping theo [Table2]--> [Table1]
    OPEN table_CELL_MAPPING FOR
      SELECT P.JOB_NAME as PROGRAM_NAME,
             '&=[TABLE2].NO' as NO,
             '&=[TABLE2].FULLNAME' as FULLNAME,
             '&=[TABLE2].IDNO' as IDNO,
             '&=[TABLE2].DOB_NAM' as DOB_NAM,
             '&=[TABLE2].DOB_NU' as DOB_NU,
             '&=[TABLE2].Birth_Place' as Birth_Place,
             '&=[TABLE2].Contact_Mobile' as Contact_Mobile,
             '&=[TABLE2].Contact_Mobile' as Contact_Address,
             '&=[TABLE2].Chieu_Cao' as Chieu_Cao,
             '&=[TABLE2].Can_Nang' as Can_Nang,
             '&=[TABLE2].Education' as Education
        FROM RC_PROGRAM P
       where P.ID = P_PROGRAM_ID;
  
    -- Format data theo [Table2]
    OPEN table_INFO FOR
      SELECT distinct ROWNUM NO,
                      C.Fullname_Vn as FULLNAME,
                      C.LAST_NAME_VN,
                      CCV.Id_No as IDNO,
                      CASE CCV.Gender
                        WHEN TO_NCHAR('NAM') THEN
                         CCV.BIRTH_DATE
                      END as DOB_NAM,
                      CASE CCV.Gender
                        WHEN TO_NCHAR('NU') THEN
                         CCV.BIRTH_DATE
                      END as DOB_NU,
                      CCV.Birth_Place,
                      CCV.Contact_Mobile,
                      CCV.Contact_Address,
                      CH.Chieu_Cao,
                      CH.Can_Nang,
                      /* (select TO_CHAR(WM_CONCAT(CE.Field || '''' ))from Rc_Candidate_Education CE
                      where CE.CANDIDATE_ID = C.ID) as Education,*/
                      (SELECT AVG(Point_Result)
                         from Rc_Program_Schedule_Can PSC
                         left join Rc_Program_Schedule PS
                           on PSC.RC_PROGRAM_SCHEDULE_ID = PS.ID
                         left join Rc_Program_Exams PE
                           on PS.RC_PROGRAM_EXAMS_ID = PE.ID
                        where PS.Rc_Program_Id = P_PROGRAM_ID
                          and PSC.CANDIDATE_ID = C.ID
                          and PE.Is_PV = 0) as AGV
        FROM Rc_Candidate C
        join Rc_Program P
          on C.RC_PROGRAM_ID = P.ID
        join Rc_Candidate_CV CCV
          on C.Id = CCV.CANDIDATE_ID
        join RC_Candidate_Health CH
          on C.ID = CH.CANDIDATE_ID
        join Rc_Candidate_Education CE
          on C.ID = CE.CANDIDATE_ID
       where P.ID = P_PROGRAM_ID
         AND C.ID in (select distinct PSC.CANDIDATE_ID
                        from Rc_Program_Schedule_Can PSC
                        join RC_PROGRAM_SCHEDULE PS
                          on PSC.RC_PROGRAM_SCHEDULE_ID = PS.ID
                       where PS.RC_PROGRAM_ID = P_PROGRAM_ID)
       ORDER BY C.LAST_NAME_VN;
  
    -- ?? d? li?u M?n thi & di?m tuong ?ng theo table_CELL_EXAMS_MAPPING --> [Table3]
    /*select TO_CHAR(WM_CONCAT('''' || TO_CHAR(PE.NAME) || '''' || ' AS ' || '"' ||
                             'COL' || PE.ID || '"'))
    INTO Lst
    from rc_program_exams PE
    where PE.rc_program_id = P_PROGRAM_ID;*/
  
    query := 'SELECT * FROM
    (
      select
      (select PE.NAME from rc_program_schedule PS
      join rc_program_exams PE on PS.RC_PROGRAM_EXAMS_ID = PE.ID
      where psc.rc_program_schedule_id = PS.ID) as NAME,
      psc.point_result as Marks
      from Rc_Candidate C
      join rc_program_schedule_can PSC on c.Id = psc.candidate_id
      where C.rc_program_id = ' || P_PROGRAM_ID || '
      ORDER BY C.LAST_NAME_VN
    )
    PIVOT (MAX(Marks) FOR (name) IN (' || Lst || '))';
    OPEN table_DETAIL FOR TO_CHAR(query);
  END;

  PROCEDURE AUTO_GENERATE_PRO_EXAMS(P_ORG_ID     IN NUMBER,
                                    P_TITLE_ID   IN NUMBER,
                                    P_PROGRAM_ID IN NUMBER,
                                    P_OUT        OUT NUMBER) IS
  BEGIN
    INSERT INTO Rc_Program_Exams pe
      (ID, NAME, POINT_LADDER, POINT_PASS, EXAMS_ORDER, IS_PV, REMARK)
      select SEQ_Rc_Program_Exams.NEXTVAL,
             ed.NAME,
             ed.POINT_LADDER,
             ed.POINT_PASS,
             ed.EXAMS_ORDER,
             ed.IS_PV,
             ed.NOTE
        from Rc_Exams_Dtl ed
        join Rc_Exams e
          on ed.rc_exams_id = e.id
       where e.org_id = P_ORG_ID
         and e.title_id = P_TITLE_ID
         AND NOT EXISTS
       (SELECT *
                FROM Rc_Program_Exams pe
               WHERE pe.NAME = ed.name
                 AND pe.rc_program_id = P_PROGRAM_ID);
  
    update Rc_Program_Exams set RC_Program_ID = P_PROGRAM_ID;
  
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.AUTO_GENERATE_PRO_EXAMS',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_PROGRAM_SCHCEDULE_LIST(P_PROGRAM_ID IN NUMBER,
                                       P_CUR        OUT CURSOR_TYPE) IS
    list_exam_subjects nvarchar2(4000);
  BEGIN
  
    /* SELECT LISTAGG(pe.name, ', ') WITHIN GROUP(ORDER BY pe.id) a
     into list_exam_subjects
     FROM rc_program_exams pe
    WHERE pe.rc_program_id = P_PROGRAM_ID
    GROUP BY pe.rc_program_id;*/
  
    OPEN P_CUR FOR
      select PS.ID,
             PS.SCHEDULE_DATE,
             /*(select EMP.FULLNAME_VN
                from HU_EMPLOYEE EMP
               where EMP.ID = PS.EMPLOYEE_ID) AS EMPLOYEE_NAME,*/
             NAME_EMP.NAMES EMPLOYEE_NAME,
             PS.EXAMS_PLACE,
             PS.NOTE,
             PS.TIME,
             --list_exam_subjects Exams_Name,
             (SELECT LISTAGG(E.name, ', ') WITHIN GROUP(ORDER BY E.id)
                FROM (SELECT DISTINCT PSC.RC_PROGRAM_EXAMS_ID
                        FROM Rc_Program_Schedule_Can PSC
                       where PSC.RC_PROGRAM_SCHEDULE_ID = PS.ID
                         AND PS.RC_PROGRAM_ID = P_PROGRAM_ID) T
               INNER JOIN rc_program_exams E
                  ON E.ID = T.RC_PROGRAM_EXAMS_ID
               WHERE E.rc_program_id = P_PROGRAM_ID) Exams_Name,
             (select Count(distinct Candidate_id)
                from Rc_Program_Schedule_Can PSC
               where PSC.RC_PROGRAM_SCHEDULE_ID = PS.ID) as Candidate_Count,
               N.NAMES,
              NULL EMPLOYEE_NAME
        from Rc_Program_Schedule PS
        LEFT JOIN (SELECT tT.RC_PROGRAM_SCHEDULE_ID,
                          LISTAGG(P.CANDIDATE_CODE || ' - ' ||P.FULLNAME_VN,
                                  '<br />') WITHIN GROUP(ORDER BY P.CANDIDATE_CODE) AS NAMES
                     FROM Rc_Program_Schedule_Can tt
                     left join rc_candidate p
                       ON P.Id =tt.candidate_id
                    GROUP BY TT.RC_PROGRAM_SCHEDULE_ID
                    ORDER BY TT.RC_PROGRAM_SCHEDULE_ID) N
          ON N.RC_PROGRAM_SCHEDULE_ID = PS.ID
        LEFT JOIN (SELECT A.ID, 
                      LISTAGG(n.employee_code || ' - ' ||n.fullname_vn,
                                  '<br />') WITHIN GROUP(ORDER BY n.employee_code) AS NAMES
                FROM HU_EMPLOYEE n
                JOIN RC_PROGRAM_SCHEDULE A ON INSTR(','||A.ID_EXAM||',', ','||TO_CHAR(N.ID)||',') > 0
               GROUP BY A.ID
               ORDER BY A.ID) NAME_EMP
          ON PS.ID=NAME_EMP.ID
       where PS.RC_PROGRAM_ID = P_PROGRAM_ID
       ORDER BY PS.SCHEDULE_DATE DESC;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_PROGRAM_SCHCEDULE_LIST',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;
  PROCEDURE GET_PROGRAM_SCHCEDULE_LIST_ALL (P_PROGRAM_SCHEDULE_ID IN NVARCHAR2,
                                            P_PROGRAM_ID IN NUMBER,
                                       P_CUR        OUT CURSOR_TYPE) IS
    list_exam_subjects nvarchar2(4000);
  BEGIN
  OPEN P_CUR FOR
    SELECT C.ID,
             TITLE.NAME_VN     VITRI,
             ORG.NAME_VN       PHONGBAN,
             C.CANDIDATE_CODE,
             C.FULLNAME_VN,
             CC.BIRTH_DATE,
             CC.ID_NO,
             CC.CONTACT_MOBILE,
             CC.PER_EMAIL,
             PS.SCHEDULE_DATE,
             E.FULLNAME_VN     NGUOIPV,
             PS.EXAMS_PLACE,
             PS.NOTE,
             (SELECT LISTAGG(E.name, ', ') WITHIN GROUP(ORDER BY E.id)
                FROM (SELECT DISTINCT PSC.RC_PROGRAM_EXAMS_ID
                        FROM Rc_Program_Schedule_Can PSC
                       where PSC.RC_PROGRAM_SCHEDULE_ID = PS.ID
                         AND PS.RC_PROGRAM_ID = P_PROGRAM_ID) T
               INNER JOIN rc_program_exams E
                  ON E.ID = T.RC_PROGRAM_EXAMS_ID
               WHERE E.rc_program_id = P_PROGRAM_ID) Exams_Name
        FROM RC_PROGRAM_SCHEDULE PS
        LEFT JOIN RC_PROGRAM_SCHEDULE_CAN PSC
          ON PS.ID = PSC.RC_PROGRAM_SCHEDULE_ID
        LEFT JOIN RC_CANDIDATE C
          ON PSC.CANDIDATE_ID = C.ID
        LEFT JOIN RC_CANDIDATE_CV CC
          ON C.ID = CC.CANDIDATE_ID
        LEFT JOIN RC_PROGRAM P
          ON P.ID = PS.RC_PROGRAM_ID
        LEFT JOIN HU_ORGANIZATION ORG
          ON ORG.ID = P.ORG_ID
        LEFT JOIN HU_TITLE TITLE
          ON TITLE.ID = P.TITLE_ID
        LEFT JOIN HU_EMPLOYEE E
          ON E.ID = PS.EMPLOYEE_ID
        LEFT JOIN RC_PROGRAM_CANDIDATE PC
          ON PC.CANDIDATE_ID = C.ID
         AND PC.RC_PROGRAM_ID = P_PROGRAM_ID
       WHERE PS.RC_PROGRAM_ID = P_PROGRAM_ID
         AND INSTR(',' || P_PROGRAM_SCHEDULE_ID || ',', ',' || PS.ID || ',') > 0;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_PROGRAM_SCHCEDULE_LIST_ALL',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_SCHCEDULE_CAN_BY_STATUS(P_PROGRAM_ID          IN NUMBER,
                                        P_PROGRAM_SCHEDULE_ID IN NUMBER,
                                        P_CUR                 OUT CURSOR_TYPE) IS
    query NVARCHAR2(500);
  BEGIN
    query := 'select distinct C.ID, C.CANDIDATE_CODE, C.Fullname_Vn, CC.BIRTH_DATE, CC.ID_NO,CC.CONTACT_MOBILE,CC.PER_EMAIL
      FROM RC_PROGRAM_SCHEDULE PS
      left join Rc_Program_Schedule_Can PSC on ps.id=psc.RC_PROGRAM_SCHEDULE_ID
      left join RC_CANDIDATE C on PSC.CANDIDATE_ID = C.Id
      left join RC_Candidate_Cv CC on C.ID = CC.CANDIDATE_ID
      left join rc_program_candidate pc on pc.candidate_id=c.id and pc.RC_PROGRAM_ID=' ||
             P_PROGRAM_ID || '
      where Ps.RC_PROGRAM_ID = ' || P_PROGRAM_ID || '
      AND PS.ID = ' || P_PROGRAM_SCHEDULE_ID || '';
    /*
    IF P_PROGRAM_SCHEDULE_ID > 0 THEN
      query := query || ' AND PSC.RC_PROGRAM_SCHEDULE_ID = ' ||
               P_PROGRAM_SCHEDULE_ID;
    END IF;*/
  
    OPEN P_CUR FOR TO_CHAR(query);
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_SCHCEDULE_CAN_BY_STATUS',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_CANDIDATE_NOT_SCHEDULE(P_PROGRAM_ID IN NUMBER,
                                       P_SCHEDULE   IN NUMBER,
                                       P_CUR        OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      select C.ID,
             C.CANDIDATE_CODE Code,
             C.Fullname_Vn as FullName,
             CCV.Birth_Date as DOB,
             CCV.MOBILE_PHONE as Mobile,
             CCV.ID_NO as IDNO,
             (select NAME_VN from OT_Other_List where ID = CCV.GENDER) as Gender,
             CCV.Per_Email as Email,
             (select NAME_VN from OT_Other_List where CODE = C.Status_Id) as Status
        from RC_Candidate C
        left join RC_Candidate_CV CCV
          on C.ID = CCV.CANDIDATE_ID
       where C.RC_PROGRAM_ID = P_PROGRAM_ID
         AND C.STATUS_ID IN ('DUDK', 'PROCESS')
         AND ((P_SCHEDULE = 0 AND 1 = 1) OR
             C.ID NOT IN
             (select PSC.CANDIDATE_ID
                 from Rc_Program_Schedule_Can PSC
                 left join RC_PROGRAM_SCHEDULE PS
                   on PSC.RC_PROGRAM_SCHEDULE_ID = PS.ID
                where PS.RC_PROGRAM_ID = P_PROGRAM_ID
                  AND PSC.RC_PROGRAM_SCHEDULE_ID = P_SCHEDULE))
      /*AND C.ID NOT IN
      (select PSC.CANDIDATE_ID
         from Rc_Program_Schedule_Can PSC
         left join RC_PROGRAM_SCHEDULE PS
           on PSC.RC_PROGRAM_SCHEDULE_ID = PS.ID
        where PS.RC_PROGRAM_ID = P_PROGRAM_ID
        )*/
       ORDER BY C.LAST_NAME_VN;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_CANDIDATE_NOT_SCHEDULE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE CHECK_EXAMS_PRO_SCHEDULE_CAN(P_PROGRAM_SCHEDULE_ID IN NUMBER,
                                         P_CANDIDATE_ID        IN NUMBER,
                                         P_OUT                 OUT NUMBER) IS
  BEGIN
    P_OUT := 0;
    select ID
      Into P_OUT
      from RC_PROGRAM_SCHEDULE_CAN PSC
     where
    /*PSC.RC_PROGRAM_EXAMS_ID = P_PROGRAM_EXAMS_ID */
     RC_PROGRAM_SCHEDULE_ID = P_PROGRAM_SCHEDULE_ID
     and CANDIDATE_ID = P_CANDIDATE_ID
     and (PSC.POINT_RESULT > 0 or PSC.Is_Pass != -1)
     and ROWNUM = 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.CHECK_EXAMS_PRO_SCHEDULE_CAN',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE ADDNEW_CAN_PRO_SCHEDULE(P_CANDIDATE_ID        IN NUMBER,
                                    P_PROGRAM_SCHEDULE_ID IN NUMBER,
                                    P_PROGRAM_EXAMS_ID    IN NUMBER,
                                    P_OUT                 OUT NUMBER) IS
  
  BEGIN
    INSERT INTO RC_PROGRAM_SCHEDULE_CAN
      (ID,
       CANDIDATE_ID,
       RC_PROGRAM_SCHEDULE_ID,
       RC_PROGRAM_EXAMS_ID,
       POINT_RESULT,
       IS_PASS)
    VALUES
      (SEQ_RC_PROGRAM_SCHEDULE_CAN.NEXTVAL,
       P_CANDIDATE_ID,
       P_PROGRAM_SCHEDULE_ID,
       P_PROGRAM_EXAMS_ID,
       0,
       -1);
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.ADDNEW_CAN_PRO_SCHEDULE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE ADDNEW_PRO_SCHEDULE(P_PROGRAM_ID    IN NUMBER,
                                P_EMPLOYEE_ID   IN NUMBER,
                                P_SCHEDULE_DATE IN DATE,
                                P_EXAMS_PLACE   IN NVARCHAR2,
                                P_NOTE          IN NVARCHAR2,
                                P_TIME          IN NVARCHAR2,
                                P_ID_EXAM          IN NVARCHAR2,
                                P_USERNAME      IN NVARCHAR2,
                                P_CREATED_LOG   IN NVARCHAR2,
                                P_OUT           OUT NUMBER) IS
  
  BEGIN
    INSERT INTO RC_PROGRAM_SCHEDULE
      (ID,
       RC_PROGRAM_ID,
       EMPLOYEE_ID,
       SCHEDULE_DATE,
       EXAMS_PLACE,
       NOTE,
       TIME,
       ID_EXAM,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG)
    VALUES
      (SEQ_RC_PROGRAM_SCHEDULE.NEXTVAL,
       P_PROGRAM_ID,
       nvl(P_EMPLOYEE_ID, null),
       P_SCHEDULE_DATE,
       P_EXAMS_PLACE,
       P_NOTE,
       P_TIME,
       P_ID_EXAM,
       SYSDATE,
       P_USERNAME,
       P_CREATED_LOG);
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.ADDNEW_PRO_SCHEDULE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_PRO_SCHEDULE(P_ID            IN NUMBER,
                                P_EMPLOYEE_ID   IN NUMBER,
                                P_SCHEDULE_DATE IN DATE,
                                P_EXAMS_PLACE   IN NVARCHAR2,
                                P_NOTE          IN NVARCHAR2,
                                P_TIME          IN NVARCHAR2,
                                P_ID_EXAM          IN NVARCHAR2,
                                P_USERNAME      IN NVARCHAR2,
                                P_MODIFY_BY_LOG IN NVARCHAR2,
                                P_OUT           OUT NUMBER) IS
  BEGIN
    UPDATE RC_PROGRAM_SCHEDULE
       SET ID            = P_ID,
           EMPLOYEE_ID   = nvl(P_EMPLOYEE_ID, null),
           SCHEDULE_DATE = P_SCHEDULE_DATE,
           EXAMS_PLACE   = P_EXAMS_PLACE,
           NOTE          = P_NOTE,
           TIME          = P_TIME,
           ID_EXAM       = P_ID_EXAM,
           MODIFIED_DATE = SYSDATE,
           MODIFIED_BY   = P_USERNAME,
           MODIFIED_LOG  = P_MODIFY_BY_LOG
     WHERE ID = P_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_PRO_SCHEDULE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_TOPID_PRO_SCHEDULE(P_PROGRAM_ID IN NUMBER,
                                   P_OUT        OUT NUMBER) IS
  BEGIN
    P_OUT := 0;
    select ID
      Into P_OUT
      from (select id
              from RC_PROGRAM_SCHEDULE
             where RC_PROGRAM_ID = P_PROGRAM_ID
             order by id desc)
     where rownum = 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.CANDIDATE_GET_AVERAGE_MARKS',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_PRO_SCHEDULE_ID(P_ID IN NUMBER, P_OUT OUT NUMBER) IS
  BEGIN
    UPDATE RC_PROGRAM_SCHEDULE_CAN
       SET RC_PROGRAM_SCHEDULE_ID = P_ID
     WHERE RC_PROGRAM_SCHEDULE_ID = 0;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_PRO_SCHEDULE_ID',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE DELETE_PRO_SCHEDULE_CAN_ISNULL(P_OUT OUT NUMBER) IS
  BEGIN
    DELETE FROM Rc_Program_Schedule_Can WHERE RC_PROGRAM_SCHEDULE_ID = 0;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.DELETE_CANDIDATE_RESULT',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_PRO_SCHEDULE_BYID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      select PS.*, T.FULLNAME_VN FULLNAME
        from Rc_Program_Schedule PS
        LEFT JOIN HU_EMPLOYEE T
          ON T.ID = PS.EMPLOYEE_ID
       where PS.ID = P_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_PRO_SCHEDULE_BYID',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE DELETE_PRO_SCHEDULE_CAN(P_PRO_SCHEDULE_ID IN NUMBER,
                                    P_CANDIDATE_ID    IN NUMBER,
                                    P_OUT             OUT NUMBER) IS
  BEGIN
    DELETE FROM Rc_Program_Schedule_Can
     WHERE RC_PROGRAM_SCHEDULE_ID = P_PRO_SCHEDULE_ID
       AND CANDIDATE_ID = P_CANDIDATE_ID;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.DELETE_PRO_SCHEDULE_CAN',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_ALL_EXAMS_BYPRO(P_PROGRAM_ID  IN NUMBER,
                                P_SCHEDULE_ID IN NUMBER,
                                P_CUR         OUT CURSOR_TYPE) IS
    PV_COUNT NUMBER;
  BEGIN
  
    SELECT COUNT(*)
      INTO PV_COUNT
      FROM RC_PROGRAM_SCHEDULE_CAN C
     WHERE C.RC_PROGRAM_SCHEDULE_ID = P_SCHEDULE_ID;
  
    IF PV_COUNT = 0 THEN
      OPEN P_CUR FOR
        select pe.*,
               pe.name || ' _ ' || TO_CHAR(UNISTR('V\00F2ng ')) ||
               pe.exams_order as NAME_SORT
          from Rc_Program_Exams PE
         where PE.RC_PROGRAM_ID = P_PROGRAM_ID
         ORDER BY PE.EXAMS_ORDER ASC;
    ELSE
      OPEN P_CUR FOR
        select DISTINCT PE.*,
                        pe.name || ' _ ' || TO_CHAR(UNISTR('V\00F2ng ')) ||
                        pe.exams_order as NAME_SORT
          from Rc_Program_Exams PE
         INNER JOIN RC_PROGRAM_SCHEDULE_CAN C
            ON C.RC_PROGRAM_EXAMS_ID = PE.ID
         where PE.RC_PROGRAM_ID = P_PROGRAM_ID
           AND C.RC_PROGRAM_SCHEDULE_ID = P_SCHEDULE_ID
         ORDER BY PE.EXAMS_ORDER ASC;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_ALL_EXAMS_BYPRO',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE MANNING_ORG_GET_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT * FROM Rc_Manning_Org WHERE ID = P_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.MANNING_ORG_GET_BY_ID',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_TITLE_IN_PLAN(P_ORGID   IN NUMBER,
                              P_IS_PLAN IN NUMBER,
                              P_LANG    IN VARCHAR,
                              P_CUR     OUT CURSOR_TYPE) IS
    PV_VALUE_CONFIG VARCHAR(50) DEFAULT 'FALSE';
    PV_OUT          NUMBER DEFAULT 0;
  BEGIN
    BEGIN
      SELECT NVL((SELECT TO_CHAR(SC.VALUE)
                   FROM SE_CONFIG SC
                  WHERE SC.CODE = 'CONFIGTITLE'
                    AND ROWNUM = 1),
                 'FALSE')
        INTO PV_VALUE_CONFIG
        FROM DUAL;
      IF UPPER(PV_VALUE_CONFIG) = 'TRUE' or PV_VALUE_CONFIG = '-1' THEN
        PV_OUT := -1;
      ELSE
        PV_OUT := 0;
      END IF;
    EXCEPTION
      WHEN OTHERS THEN
        PV_OUT := 0;
    END;
  
    IF P_IS_PLAN = 0 THEN
      IF PV_OUT = -1 THEN
        OPEN P_CUR FOR
          SELECT L.ID,
                 DECODE(P_LANG, 'vi-VN', L.NAME_VN, L.NAME_EN) NAME,
                 GRP.NAME_VN GROUP_NAME
            FROM HU_TITLE L
           --INNER JOIN HU_ORG_TITLE O
            --  ON O.TITLE_ID = L.ID
            LEFT JOIN OT_OTHER_LIST GRP
              ON L.TITLE_GROUP_ID = GRP.ID
           WHERE L.ACTFLG = 'A'
            -- AND O.ACTFLG = 'A'
          --AND O.ORG_ID = P_ORGID
           ORDER BY NLSSORT(DECODE(P_LANG, 'vi-VN', L.NAME_VN, L.NAME_EN),
                            'NLS_SORT=vietnamese');
      ELSE
        OPEN P_CUR FOR
          SELECT L.ID,
                 DECODE(P_LANG, 'vi-VN', L.NAME_VN, L.NAME_EN) NAME,
                 GRP.NAME_VN GROUP_NAME
            FROM HU_TITLE L
           INNER JOIN HU_ORG_TITLE O
              ON O.TITLE_ID = L.ID
            LEFT JOIN OT_OTHER_LIST GRP
              ON L.TITLE_GROUP_ID = GRP.ID
           WHERE L.ACTFLG = 'A'
             AND O.ACTFLG = 'A'
             AND O.ORG_ID = P_ORGID
           ORDER BY NLSSORT(DECODE(P_LANG, 'vi-VN', L.NAME_VN, L.NAME_EN),
                            'NLS_SORT=vietnamese');
      END IF;
    ELSE
      IF PV_OUT = -1 THEN
        OPEN P_CUR FOR
          SELECT PR.ID AS PLAN_ID,
                 L.ID,
                 DECODE(P_LANG, 'vi-VN', L.NAME_VN, L.NAME_EN) || ' - ' ||
                 TO_CHAR(PR.EXPECTED_JOIN_DATE, 'dd/MM/yyyy') NAME
            FROM Rc_Plan_Reg PR
            LEFT JOIN HU_TITLE L
              on PR.TITLE_ID = L.ID
           --INNER JOIN HU_ORG_TITLE O
             -- ON O.TITLE_ID = L.ID
            LEFT JOIN OT_OTHER_LIST GRP
              ON L.TITLE_GROUP_ID = GRP.ID
           WHERE L.ACTFLG = 'A'
            -- AND O.ACTFLG = 'A'
             --AND O.ORG_ID = P_ORGID
             AND PR.Status_Id = 4051
           ORDER BY NLSSORT(DECODE(P_LANG, 'vi-VN', L.NAME_VN, L.NAME_EN),
                            'NLS_SORT=vietnamese');
      ELSE
        OPEN P_CUR FOR
          SELECT PR.ID AS PLAN_ID,
                 L.ID,
                 DECODE(P_LANG, 'vi-VN', L.NAME_VN, L.NAME_EN) || ' - ' ||
                 TO_CHAR(PR.EXPECTED_JOIN_DATE, 'dd/MM/yyyy') NAME
            FROM Rc_Plan_Reg PR
            LEFT JOIN HU_TITLE L
              on PR.TITLE_ID = L.ID
           INNER JOIN HU_ORG_TITLE O
              ON O.TITLE_ID = L.ID
            LEFT JOIN OT_OTHER_LIST GRP
              ON L.TITLE_GROUP_ID = GRP.ID
           WHERE L.ACTFLG = 'A'
             AND O.ACTFLG = 'A'
             AND O.ORG_ID = P_ORGID
             AND PR.Status_Id = 4051
           ORDER BY NLSSORT(DECODE(P_LANG, 'vi-VN', L.NAME_VN, L.NAME_EN),
                            'NLS_SORT=vietnamese');
      END IF;
    END IF;
  END;

  PROCEDURE CHECK_REQUEST_NOT_APPROVE(P_ID IN NVARCHAR2, P_OUT OUT NUMBER) IS
  BEGIN
    P_OUT := 0;
    select ID
      INTO P_OUT
      from Rc_Request
     where id in (P_ID)
       and Status_ID <> 4101
       and rownum = 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.CHECK_REQUEST_NOT_APPROVE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE PLAN_GET_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT * FROM RC_PLAN_REG PR WHERE PR.ID = P_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.PLAN_GET_BY_ID',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;
  PROCEDURE Get_Email_Candidate(P_ID IN NVARCHAR2, P_OUT OUT nvarchar2) IS
  BEGIN
    P_OUT := 0;
    select CV.PER_EMAIL
      INTO P_OUT
      from RC_CANDIDATE_CV CV
     where CV.CANDIDATE_ID = P_ID;
  END;
  PROCEDURE Get_Email_Employee(P_ID IN NVARCHAR2, P_OUT OUT nvarchar2) IS
  BEGIN
    P_OUT := 0;
    select CV.WORK_EMAIL
      INTO P_OUT
      from hu_employee_cv CV
     where CV.EMPLOYEE_ID = P_ID;
  END;
  PROCEDURE GET_INFO_CADIDATE(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT CASE
               WHEN GENDER.CODE = 1 THEN --Nu
                TO_CHAR(UNISTR('Ch\1ECB'))
               WHEN GENDER.CODE = 0 THEN --Nam
                'Anh'
             END GENDER_VN,
             T.FULLNAME_VN,
             TITLE.NAME_VN,
             RCPS.SCHEDULE_DATE
        FROM RC_CANDIDATE T
       INNER JOIN RC_CANDIDATE_CV CV
          ON T.ID = CV.CANDIDATE_ID
       INNER JOIN OT_OTHER_LIST GENDER
          ON CV.GENDER = GENDER.ID
       INNER JOIN RC_PROGRAM RCP
          ON RCP.ID = T.RC_PROGRAM_ID
       INNER JOIN HU_TITLE TITLE
          ON TITLE.ID = RCP.TITLE_ID
       INNER JOIN RC_PROGRAM_SCHEDULE RCPS
          ON RCPS.RC_PROGRAM_ID = T.RC_PROGRAM_ID
       WHERE T.ID = P_ID
         AND ROWNUM = 1;
  END;

  PROCEDURE GET_PROGRAM_EXAMS(P_PROGRAMID IN NUMBER,
                              P_ORGID     IN NUMBER,
                              P_TILID     IN NUMBER,
                              P_CUR       OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      select p.id,
             pe.name,
             PE.POINT_LADDER,
             PE.POINT_PASS,
             PE.EXAMS_ORDER,
             PE.COEFFICIENT,
             PE.REMARK,
             PE.IS_PV
        from rc_program p
       inner join rc_program_exams pe
          on pe.rc_program_id = p.id
       inner join hu_organization o
          on o.id = p.org_id
       inner join hu_title t
          on t.id = p.title_id
       where pe.rc_program_id = P_PROGRAMID
      union all
      select e.id,
             ed.name,
             ED.POINT_LADDER,
             ED.POINT_PASS,
             ED.EXAMS_ORDER,
             ED.COEFFICIENT,
             ED.NOTE,
             ED.IS_PV
        from rc_exams e
       inner join rc_exams_dtl ed
          on e.id = ed.rc_exams_id
       inner join hu_organization oo
          on oo.id = e.org_id
       inner join hu_title tt
          on tt.id = e.title_id
       where e.org_id = P_ORGID
         and e.title_id = P_TILID;
  END;
  PROCEDURE GET_MAIL_COMPANY(P_EMPID IN NUMBER, P_CUR OUT NVARCHAR2) IS
  BEGIN
    SELECT T.WORK_EMAIL
      INTO P_CUR
      FROM HU_EMPLOYEE_CV T
     where t.employee_id = P_EMPID;
  END;
  PROCEDURE IMPORT_DINH_BIEN(P_USER   IN NVARCHAR2,
                             P_DOCXML IN NCLOB,
                             P_OUT    OUT NUMBER) IS
    V_DOCXML XMLTYPE;
  BEGIN
    V_DOCXML := XMLTYPE.CREATEXML(P_DOCXML);
  
    FOR DATA IN (SELECT DOM.ID, DOM.NEW_MANNING, DOM.NOTE
                   FROM XMLTABLE('/DocumentElement/table' PASSING V_DOCXML
                                 COLUMNS ID NUMBER PATH './ID',
                                 NEW_MANNING NUMBER PATH './NEW_MANNING',
                                 NOTE NVARCHAR2(1000) PATH './NOTE') DOM) LOOP
      UPDATE RC_MANNING_TITLE S
         SET S.NEW_MANNING            = DATA.NEW_MANNING,
             S.MOBILIZE_COUNT_MANNING = DATA.NEW_MANNING - S.CURRENT_MANNING,
             S.NOTE                   = DATA.NOTE
       WHERE s.id = DATA.ID;
    
    END LOOP;
    commit;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      ROLLBACK;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.IMPORT_DINH_BIEN',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_GROUP_TITLE_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  
  BEGIN
    /*  BEGIN
         SELECT TO_NUMBER(OT.REMARK)
         INTO PV_NUMBER
         FROM HU_TITLE T
         INNER JOIN OT_OTHER_LIST OT 
         ON OT.ID=T.TITLE_GROUP_ID
         AND OT.TYPE_CODE='HU_TITLE_GROUP'
         WHERE T.ID=P_ID AND OT.REMARK IS NOT NULL AND PKG_FUNCTION.IsNumber(OT.REMARK)=1;
    EXCEPTION WHEN NO_DATA_FOUND THEN
      PV_NUMBER:=0;
    END;*/
  
    OPEN P_CUR FOR
      SELECT OT.NAME_VN, NVL(OT.ORDERBYID, 0) AS DAY_NUM
        FROM HU_TITLE T
       INNER JOIN OT_OTHER_LIST OT
          ON OT.ID = T.TITLE_GROUP_ID
         AND OT.TYPE_CODE = 'HU_TITLE_GROUP'
       WHERE T.ID = P_ID;
  
  END;

  PROCEDURE GET_INF_EMP_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT E.ID,
             E.EMPLOYEE_CODE,
             E.FULLNAME_VN,
             O.NAME_VN       ORG_NAME,
             T.NAME_VN       TITLE_NAME,
             E.JOIN_DATE
        FROM HU_EMPLOYEE E
       INNER JOIN HU_ORGANIZATION O
          ON O.ID = E.ORG_ID
       INNER JOIN HU_TITLE T
          ON T.ID = E.TITLE_ID
       WHERE E.ID = 1;
  
  END;

  PROCEDURE GET_HR_PLANINGS_DEFAULT(IS_BLANK IN NUMBER,
                                    P_CUR    OUT CURSOR_TYPE) IS
  BEGIN
    IF IS_BLANK = 0 THEN
      OPEN P_CUR FOR
        SELECT NULL ID,
               NULL YEAR,
               NULL VERSION,
               NULL EFFECT_DATE,
               NULL NOTE,
               NULL NAME
          FROM DUAL
        UNION ALL
        SELECT *
          FROM (SELECT R.ID,
                       R.YEAR,
                       R.VERSION,
                       R.EFFECT_DATE,
                       R.NOTE,
                       TO_CHAR(R.YEAR) || ' - ' || R.VERSION || ' - ' ||
                       TO_CHAR(R.EFFECT_DATE, 'DD/MM/YYYY') NAME
                  FROM RC_HR_YEAR_PLANING R
                 ORDER BY R.YEAR DESC, R.VERSION DESC);
    ELSE
      OPEN P_CUR FOR
        SELECT R.ID,
               R.YEAR,
               R.VERSION,
               R.EFFECT_DATE,
               R.NOTE,
               TO_CHAR(R.YEAR) || ' - ' || R.VERSION || ' - ' ||
               TO_CHAR(R.EFFECT_DATE, 'DD/MM/YYYY') NAME
          FROM RC_HR_YEAR_PLANING R
         ORDER BY R.YEAR DESC, R.VERSION DESC;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_HR_PLANINGS_DEFAULT',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE AUTO_GEN_CODE_RC(ORG_ID       IN NUMBER,
                             DATE_REQUEST IN DATE,
                             P_CUR        OUT CURSOR_TYPE) IS
    PV_CODE_ORG        NVARCHAR2(100);
    PV_STT             VARCHAR2(10);
    PV_CODE_RC_CURRENT NVARCHAR2(100);
  BEGIN
    PV_STT := '001';
    SELECT O.SHORT_NAME
      INTO PV_CODE_ORG
      FROM HU_ORGANIZATION O
     WHERE O.ID = ORG_ID;
  
    SELECT MAX(SUBSTR(R.CODE_RC, 1, 3))
      INTO PV_CODE_RC_CURRENT
      FROM RC_REQUEST R
     WHERE TO_CHAR(R.SEND_DATE, 'YYYY') = EXTRACT(YEAR FROM DATE_REQUEST);
  
    PV_CODE_RC_CURRENT := TO_CHAR(NVL(PV_CODE_RC_CURRENT, 0) + 1, '000');
  
    OPEN P_CUR FOR
      SELECT TRIM(PV_CODE_RC_CURRENT || '/YCTD/' || PV_CODE_ORG || '.' ||
                  EXTRACT(YEAR FROM DATE_REQUEST)) AS CODE_REQUEST
        FROM DUAL;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.AUTO_GEN_CODE_RC',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_NUM_PLANNING_DETAIL_BY_MONTH(P_ORG_ID       IN NUMBER,
                                             P_TITLE_ID     IN NUMBER,
                                             P_DATE_REQUEST IN DATE,
                                             P_CUR          OUT CURSOR_TYPE) IS
    PV_YEAR  NUMBER;
    PV_MONTH NUMBER;
    PV_SEL   NVARCHAR2(1000);
    PV_ID    NUMBER;
  BEGIN
  
    SELECT EXTRACT(YEAR FROM P_DATE_REQUEST) AS Y,
           EXTRACT(MONTH FROM P_DATE_REQUEST) AS M
      INTO PV_YEAR, PV_MONTH
      FROM DUAL;
  
    SELECT A.ID
      INTO PV_ID
      FROM (SELECT R.ID,
                   ROW_NUMBER() OVER(PARTITION BY R.YEAR ORDER BY R.EFFECT_DATE DESC, R.CREATED_DATE DESC) AS ROW_NUMBER
              FROM RC_HR_YEAR_PLANING R
             WHERE R.YEAR = PV_YEAR
               AND TO_CHAR(R.EFFECT_DATE, 'YYYYMMDD') <=
                   TO_CHAR(P_DATE_REQUEST, 'YYYYMMDD')) A
     WHERE A.ROW_NUMBER = 1;
  
    PV_SEL := 'MONTH_' || TO_CHAR(PV_MONTH);
    PV_SEL := 'SELECT NVL(' || PV_SEL ||
              ',0) AS NEW_MANNING FROM RC_HR_PLANING_DETAIL WHERE YEAR_PLAN_ID=' ||
              PV_ID || ' AND ORG_ID=' || P_ORG_ID || ' AND TITLE_ID=' ||
              P_TITLE_ID;
  
    DELETE SQL_TEST;
    COMMIT;
  
    INSERT INTO SQL_TEST VALUES (PV_SEL, SYSDATE);
    COMMIT;
  
    OPEN P_CUR FOR TO_CHAR(PV_SEL);
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_NUM_PLANNING_DETAIL_BY_MONTH',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;
  PROCEDURE GET_TITLE_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT T.ID,
             T.ACTFLG,
             T.CODE,
             T.NAME_EN,
             T.NAME_VN,
             T.CREATED_DATE,
             T.CREATED_BY,
             T.CREATED_LOG,
             T.MODIFIED_DATE,
             T.MODIFIED_BY,
             T.MODIFIED_LOG,
             T.REMARK,
             T.TITLE_GROUP_ID,
             T.STATUS,
             T.LIST_PC_ID,
             T.DRIVE_INFOR,
             T.WORK_INVOLVE_ID,
             T.OVT,
             T.UPLOAD_FILE,
             T.LEVEL_ID,
             T.IS_OT,
             T.HURTFUL,
             T.RANK_ID,
             T.SPEC_HURFUL,
             T.ORG_ID,
             T.ORG_TYPE,
             T.FILENAME,
             T.HURT_TYPE_ID
        FROM HU_TITLE T
       WHERE T.ID = P_ID;
  
  END;
  --ANHVN, GET_CANDIDATE_NOT_SCHEDULE_1
  PROCEDURE GET_CANDIDATE_NOT_SCHEDULE_1(P_PROGRAM_ID IN NUMBER,
                                         P_SCHEDULE   IN NUMBER,
                                         P_EXAMS_ID   IN NUMBER,
                                         P_CUR        OUT CURSOR_TYPE) IS
    PV_EXAMS_ORDER NUMBER;
  BEGIN
    BEGIN
      SELECT EX.EXAMS_ORDER
        INTO PV_EXAMS_ORDER
        FROM RC_PROGRAM_EXAMS EX
       WHERE EX.ID = NVL(P_EXAMS_ID, 0);
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        PV_EXAMS_ORDER := 0;
    END;
    OPEN P_CUR FOR
      SELECT C.ID,
             PCA.ID AS PRO_CAN,
             C.CANDIDATE_CODE CODE,
             C.FULLNAME_VN AS FULLNAME,
             CCV.BIRTH_DATE AS DOB,
             CCV.MOBILE_PHONE AS MOBILE,
             CCV.ID_NO AS IDNO,
             (SELECT NAME_VN FROM OT_OTHER_LIST WHERE ID = CCV.GENDER) AS GENDER,
             CCV.PER_EMAIL AS EMAIL,
             (SELECT NAME_VN FROM OT_OTHER_LIST WHERE CODE = C.STATUS_ID) AS STATUS
        FROM RC_PROGRAM_CANDIDATE PCA
        LEFT JOIN RC_CANDIDATE C
          ON PCA.CANDIDATE_ID = C.ID
        LEFT JOIN RC_CANDIDATE_CV CCV
          ON C.ID = CCV.CANDIDATE_ID
      --INNER JOIN RC_PROGRAM_CANDIDATE PCA
      -- ON PCA.CANDIDATE_ID = C.ID
       WHERE PCA.RC_PROGRAM_ID = P_PROGRAM_ID
            --AND PCA.STATUS_ID IN ('DUDK', 'PROCESS')
         AND PCA.SCHEDULE = PV_EXAMS_ORDER - 1
            --AND ((P_SCHEDULE = 0 AND 1 = 1) OR
         AND C.ID NOT IN
             (SELECT PSC.CANDIDATE_ID
                FROM RC_PROGRAM_SCHEDULE PS
               INNER JOIN RC_PROGRAM_SCHEDULE_CAN PSC
                  ON PSC.RC_PROGRAM_SCHEDULE_ID = PS.ID
               WHERE PS.RC_PROGRAM_ID = P_PROGRAM_ID
                 AND PSC.RC_PROGRAM_EXAMS_ID = P_EXAMS_ID)
      --AND PSC.RC_PROGRAM_SCHEDULE_ID = P_SCHEDULE))
       ORDER BY C.LAST_NAME_VN;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_CANDIDATE_NOT_SCHEDULE_1',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_PROGRAM_CANDIDATE_STATUS(P_ID          IN NUMBER,
                                            P_STATUS_CODE IN NVARCHAR2,
                                            P_OUT         OUT NUMBER) IS
    PV_COUNT NUMBER;
  BEGIN
    IF P_STATUS_CODE = 'DAT' THEN
      SELECT COUNT(*)
        INTO PV_COUNT
        FROM RC_CANDIDATE E
       INNER JOIN RC_PROGRAM_SCHEDULE_CAN C
          ON C.CANDIDATE_ID = E.ID
       INNER JOIN RC_PROGRAM_EXAMS EX
          ON EX.ID = C.RC_PROGRAM_EXAMS_ID
       WHERE E.ID = P_ID
         AND NVL(EX.IS_PV, 0) <> 0
         AND NVL(C.IS_PASS, -1) IN (-1, 0);
    
      IF PV_COUNT = 0 THEN
        UPDATE RC_PROGRAM_CANDIDATE
           SET STATUS_ID = P_STATUS_CODE
         WHERE ID = P_ID;
      END IF;
    ELSE
      UPDATE RC_PROGRAM_CANDIDATE
         SET STATUS_ID = P_STATUS_CODE
       WHERE ID = P_ID;
    END IF;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_PROGRAM_CANDIDATE_STATUS',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE CANDIDATE_LIST_GETBYPROGRAM_BY_EXAMS(P_PROGRAM_ID IN NUMBER,
                                                 P_EXAMS_ID   IN NUMBER,
                                                 P_CUR        OUT CURSOR_TYPE) IS
    PV_EXAMS_ORDER NUMBER;
    PV_IS_PV       NUMBER;
  BEGIN
    OPEN P_CUR FOR
      select distinct C.ID,
                      PC.ID AS PRO_CAN_ID,
                      PSC.ID AS PSC_ID,
                      PE.IS_PV,
                      Case PSC.Is_Pass
                        When 1 then
                         TO_CHAR(UNISTR('\0110\1EADu v\00F2ng'))
                        When 0 then
                         TO_CHAR(UNISTR('R\1EDBt v\00F2ng'))
                      END as ISPASS,
                      PE.EXAMS_ORDER,
                      PE.POINT_PASS,
                      C.CANDIDATE_CODE Code,
                      C.Fullname_Vn as FullName,
                      CCV.Birth_Date as DOB,
                      CCV.MOBILE_PHONE as Mobile,
                      CCV.ID_NO as IDNO,
                      (select NAME_VN
                         from OT_Other_List
                        where ID = CCV.GENDER) as Gender,
                      CCV.Per_Email as Email,
                      --(select NAME_VN
                      --   from OT_Other_List
                      --  where CODE = C.Status_Id) as Status,
                      STATUS.CODE STATUS_CODE
      -- (SELECT COUNT(*)
      --  FROM RC_PROGRAM_SCHEDULE PSCHE
      --  LEFT JOIN RC_PROGRAM_SCHEDULE_CAN PSCHE_CAN
      --    ON PSCHE.ID = PSCHE_CAN.RC_PROGRAM_SCHEDULE_ID
      --   LEFT JOIN RC_PROGRAM_EXAMS PEXAM
      --     ON PEXAM.ID = PSCHE_CAN.RC_PROGRAM_EXAMS_ID
      -- WHERE PSCHE_CAN.CANDIDATE_ID = PSC.CANDIDATE_ID
      --   AND PSCHE.RC_PROGRAM_ID = P_PROGRAM_ID
      --   AND PEXAM.EXAMS_ORDER > PE.EXAMS_ORDER) COUNT_CAN
        FROM RC_PROGRAM_SCHEDULE PS
        LEFT JOIN RC_PROGRAM_SCHEDULE_CAN PSC
          ON PS.ID = PSC.RC_PROGRAM_SCHEDULE_ID
       INNER JOIN (SELECT *
                     FROM RC_PROGRAM_CANDIDATE RCPC
                    WHERE RCPC.RC_PROGRAM_ID = P_PROGRAM_ID) PC
          ON PC.CANDIDATE_ID = PSC.CANDIDATE_ID
        LEFT JOIN RC_CANDIDATE C
          ON PSC.CANDIDATE_ID = C.ID
        LEFT JOIN RC_CANDIDATE_CV CCV
          ON C.ID = CCV.CANDIDATE_ID
        LEFT JOIN RC_PROGRAM_EXAMS PE
          ON PSC.RC_PROGRAM_EXAMS_ID = PE.ID
      
      --and PC.RC_PROGRAM_ID = P_PROGRAM_ID
        LEFT JOIN (SELECT O.ID, O.NAME_EN, O.CODE
                     FROM OT_OTHER_LIST O
                    WHERE O.TYPE_ID = 1029) STATUS
          ON STATUS.CODE = PC.STATUS_ID
       WHERE PS.RC_PROGRAM_ID = P_PROGRAM_ID
         AND PSC.RC_PROGRAM_EXAMS_ID = P_EXAMS_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.CANDIDATE_LIST_GETBYPROGRAM_BY_EXAMS',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;
  PROCEDURE EXAMS_GETBYCANDIDATE_BY_PCS_ID(P_PSC_ID IN NUMBER,
                                           P_IS_PV  IN NUMBER,
                                           P_CUR    OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      select PE.NAME as EXAM_NAME,
             PE.POINT_LADDER,
             PE.POINT_PASS,
             PS.SCHEDULE_DATE,
             PSC.*,
             Case PSC.Is_Pass
               When 1 then
                'True'
               ELSE
                'False'
             END as ISPASS,
             PSC.Is_Pass,
             emp.fullname_vn EMPLOYEE_NAME
        from Rc_Program_Schedule_Can PSC
        left join Rc_Program_Schedule PS
          on PSC.RC_PROGRAM_SCHEDULE_ID = PS.ID
        left join hu_employee emp
          on emp.id = PS.EMPLOYEE_ID
        left join Rc_Program_Exams PE
          on PSC.RC_PROGRAM_EXAMS_ID = PE.ID
       where PSC.ID = P_PSC_ID
       ORDER BY PS.SCHEDULE_DATE DESC;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.EXAMS_GETBYCANDIDATE_BY_PCS_ID',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE DELETE_RC_PROGRAM_SCHEDULE(P_ID IN NVARCHAR2, P_OUT OUT NUMBER) IS
  BEGIN
    DELETE FROM RC_PROGRAM_SCHEDULE
     WHERE INSTR(',' || P_ID || ',', ',' || ID || ',') > 0;
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.DELETE_RC_PROGRAM_SCHEDULE',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE GET_SCHCEDULE_CAN_BY_SCHCEDULEID(P_PROGRAM_ID          IN NUMBER,
                                             P_PROGRAM_SCHEDULE_ID IN NUMBER,
                                             P_CUR                 OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT C.ID,
             TITLE.NAME_VN     VITRI,
             ORG.NAME_VN       PHONGBAN,
             C.CANDIDATE_CODE,
             C.FULLNAME_VN,
             CC.BIRTH_DATE,
             CC.ID_NO,
             CC.CONTACT_MOBILE,
             CC.PER_EMAIL,
             PS.SCHEDULE_DATE,
             E.FULLNAME_VN     NGUOIPV,
             PS.EXAMS_PLACE,
             PS.NOTE
      
        FROM RC_PROGRAM_SCHEDULE PS
        LEFT JOIN RC_PROGRAM_SCHEDULE_CAN PSC
          ON PS.ID = PSC.RC_PROGRAM_SCHEDULE_ID
        LEFT JOIN RC_CANDIDATE C
          ON PSC.CANDIDATE_ID = C.ID
        LEFT JOIN RC_CANDIDATE_CV CC
          ON C.ID = CC.CANDIDATE_ID
        LEFT JOIN RC_PROGRAM P
          ON P.ID = PS.RC_PROGRAM_ID
        LEFT JOIN HU_ORGANIZATION ORG
          ON ORG.ID = P.ORG_ID
        LEFT JOIN HU_TITLE TITLE
          ON TITLE.ID = P.TITLE_ID
        LEFT JOIN HU_EMPLOYEE E
          ON E.ID = PS.EMPLOYEE_ID
        LEFT JOIN RC_PROGRAM_CANDIDATE PC
          ON PC.CANDIDATE_ID = C.ID
         AND PC.RC_PROGRAM_ID = P_PROGRAM_ID
       WHERE PS.RC_PROGRAM_ID = P_PROGRAM_ID
         AND PS.ID = P_PROGRAM_SCHEDULE_ID;
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.GET_SCHCEDULE_CAN_BY_SCHCEDULEID',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;
  PROCEDURE UPDATE_APPROVED_REQUEST_PORTAL(P_ID  IN VARCHAR2,
                                           P_OUT OUT NUMBER) IS
    ID_NOT_APPROVED NUMBER;
  BEGIN
    SELECT O.ID
      INTO ID_NOT_APPROVED
      FROM OT_OTHER_LIST O
     INNER JOIN OT_OTHER_LIST_TYPE T
        ON T.ID = O.TYPE_ID
     WHERE T.CODE = 'PROCESS_STATUS'
     AND O.CODE = 'W';
  
    UPDATE RC_REQUEST
       SET IS_APPROVED = ID_NOT_APPROVED
     WHERE INSTR(',' || P_ID || ',', ',' || ID || ',') > 0
     AND NVL(IS_PORTAL,0) <> 0 ;
  
    P_OUT := 1;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_APPROVED_REQUEST_PORTAL',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;
 PROCEDURE GET_REQUESTPORTAL_APPROVE(P_EMPLOYEE_APPROVED  IN NUMBER,
                                     P_IS_APPROVED        IN NUMBER,
                                     P_USERNAME           IN VARCHAR2,
                                     P_ORG_ID             IN NUMBER,                        
                                     P_OUT                OUT CURSOR_TYPE) IS
  BEGIN
   OPEN P_OUT FOR
   SELECT p.ID                  AS ID,
       p.EXPECTED_JOIN_DATE  AS EXPECTED_JOIN_DATE,
       p.ORG_ID              AS ORG_ID,
       org.NAME_VN           AS ORG_NAME,
       org.DESCRIPTION_PATH  AS ORG_DESC,
       p.TITLE_ID            AS TITLE_ID,
       title.NAME_VN         AS TITLE_NAME,
       p.AGE_FROM            AS AGE_FROM,
       p.AGE_TO              AS AGE_TO,
       p.CONTRACT_TYPE_ID    AS CONTRACT_TYPE_ID,
       ctracttype.NAME       AS CONTRACT_TYPE_NAME,
       p.DESCRIPTION         AS DESCRIPTION,
       p.EXPERIENCE_NUMBER   AS EXPERIENCE_NUMBER,
       p.FEMALE_NUMBER       AS FEMALE_NUMBER,
       p.IS_IN_PLAN          AS IS_IN_PLAN,
       p.LEARNING_LEVEL_ID   AS LEARNING_LEVEL_ID,
       learning.NAME_VN      AS LEARNING_LEVEL_NAME,
       p.MALE_NUMBER         AS MALE_NUMBER,
       p.RECRUIT_REASON      AS RECRUIT_REASON,
       p.RECRUIT_REASON_ID   AS RECRUIT_REASON_ID,
       reason.NAME_VN        AS RECRUIT_REASON_NAME,
       p.REQUEST_EXPERIENCE  AS REQUEST_EXPERIENCE,
       p.REQUEST_OTHER       AS REQUEST_OTHER,
       p.REMARK              AS REMARK,
       p.SEND_DATE           AS SEND_DATE,
       p.STATUS_ID           AS STATUS_ID,
       status.NAME_VN        AS STATUS_NAME,
       p.CREATED_DATE        AS CREATED_DATE,
       p.REMARK_REJECT       AS REMARK_REJECT,
       p.RC_RECRUIT_PROPERTY AS RC_RECRUIT_PROPERTY,
       rc_prop.NAME_VN       AS RC_RECRUIT_PROPERTY_NAME,
       p.IS_OVER_LIMIT       AS IS_OVER_LIMIT,
       p.IS_REQUEST_COMPUTER AS IS_REQUEST_COMPUTER,
       p.FOREIGN_ABILITY     AS FOREIGN_ABILITY,
       p.COMPUTER_APP_LEVEL  AS COMPUTER_APP_LEVEL,
       p.GENDER_PRIORITY     AS GENDER_PRIORITY,
       gender.NAME_VN        AS GENDER_PRIORITY_NAME,
       p.RECRUIT_NUMBER      AS RECRUIT_NUMBER,
       p.UPLOAD_FILE         AS UPLOAD_FILE,
       p.FILE_NAME           AS FILE_NAME,
       p.CODE_RC             AS CODE_RC,
       p.NAME_RC             AS NAME_RC,
       p.REQUIRER            AS REQUIRER,
       req_name.FULLNAME_VN  AS REQUIRER_NAME,
       p.WORKING_PLACE_ID    AS WORKING_PLACE_ID,
       wp_name.NAME_VN       AS WORKING_PLACE_NAME,
       p.RC_GRPOS            AS RC_GRPOS,
       p.SAL_OFFER           AS SAL_OFFER,
       p.PERSON_PT_RC        AS PERSON_PT_RC,
       p_pt.FULLNAME_VN      AS PERSON_PT_RC_NAME,
       p.OTHER_QUALIFICATION AS OTHER_QUALIFICATION,
       p.IS_CLOCK            AS IS_CLOCK,
       p.IS_APPROVED         AS IS_APPROVED,
       approve.CODE          AS IS_APPROVED_CODE,
       approve.NAME_VN       AS IS_APPROVED_NAME
  FROM RC_REQUEST p
 INNER JOIN HU_ORGANIZATION org
    ON org.ID = p.ORG_ID
 INNER JOIN HU_TITLE title
    ON title.ID = p.TITLE_ID
 INNER JOIN PROCESS_APPROVED_STATUS A
    ON P.ID = A.ID_REGGROUP
 INNER JOIN OT_OTHER_LIST OT
    ON OT.ID = A.APP_STATUS
  LEFT OUTER JOIN OT_OTHER_LIST reason
    ON reason.ID = p.RECRUIT_REASON_ID
  LEFT OUTER JOIN OT_OTHER_LIST status
    ON status.ID = p.STATUS_ID
  LEFT OUTER JOIN OT_OTHER_LIST approve
    ON approve.ID = p.IS_APPROVED
  LEFT OUTER JOIN OT_OTHER_LIST learning
    ON learning.ID = p.LEARNING_LEVEL_ID
  LEFT OUTER JOIN OT_OTHER_LIST rc_prop
    ON rc_prop.ID = p.RC_RECRUIT_PROPERTY
  LEFT OUTER JOIN OT_OTHER_LIST gender
    ON gender.ID = p.GENDER_PRIORITY
  LEFT OUTER JOIN HU_CONTRACT_TYPE ctracttype
    ON ctracttype.ID = p.LEARNING_LEVEL_ID
  LEFT OUTER JOIN HU_EMPLOYEE req_name
    ON req_name.ID = p.REQUIRER
  LEFT OUTER JOIN HU_EMPLOYEE p_pt
    ON p_pt.ID = p.PERSON_PT_RC
  LEFT OUTER JOIN OT_OTHER_LIST wp_name
    ON wp_name.ID = p.WORKING_PLACE_ID
 INNER JOIN SE_CHOSEN_ORG k
    ON p.ORG_ID = k.ORG_ID
   AND UPPER(k.USERNAME) = UPPER(P_USERNAME)
 WHERE A.EMPLOYEE_APPROVED = P_EMPLOYEE_APPROVED
   AND A.PROCESS_TYPE = 'RECRUITMENT'
   AND ((P_IS_APPROVED = 5 OR P_IS_APPROVED IS NULL) OR A.APP_STATUS = P_IS_APPROVED)
   AND ((A.APP_STATUS = 0 AND
       PKG_FUNCTION.FN_CHECK_APP(A.EMPLOYEE_ID,
                                   A.PE_PERIOD_ID,
                                   A.APP_LEVEL - 1,
                                   A.ID_REGGROUP) = 1 AND
       PKG_FUNCTION.FN_CHECK_APP(A.EMPLOYEE_ID,
                                   A.PE_PERIOD_ID,
                                   A.APP_LEVEL + 1,
                                   A.ID_REGGROUP) in (0, 2)) OR
       (A.APP_STATUS IN (1, 2)))
   AND NVL(IS_PORTAL,0) <> 0;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_RECRUITMENT.UPDATE_APPROVED_REQUEST_PORTAL',
                              SQLERRM,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL);
  END;
  
  PROCEDURE DELETE_SALARY_ALLOWANCE_CANDIDATE(RC_PROGRAM_ID IN NUMBER, RC_CANDIDATE_ID IN NUMBER, P_OUT OUT NUMBER) IS
    BEGIN
      DELETE FROM RC_SALARY_CANDIDATE T WHERE T.RC_PROGRAM_ID = RC_PROGRAM_ID AND T.RC_CANDIDATE_ID = RC_CANDIDATE_ID;
      DELETE FROM RC_ALLOWANCE_CANDIDATE T WHERE T.RC_PROGRAM_ID = RC_PROGRAM_ID AND T.RC_CANDIDATE_ID = RC_CANDIDATE_ID;
      P_OUT := 1;
    EXCEPTION
      WHEN OTHERS THEN
        P_OUT := 0;
    END;
    
    PROCEDURE GET_SE_USER_BY_IS_RC(P_IS_RC IN NUMBER,
                                    P_CUR  OUT CURSOR_TYPE) IS

  BEGIN
    OPEN P_CUR FOR
      SELECT U.USERNAME,U.ID FROM SE_USER U WHERE U.IS_RC=-1;
  END;
END;
