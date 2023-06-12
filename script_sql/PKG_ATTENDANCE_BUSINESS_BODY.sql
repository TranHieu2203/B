CREATE OR REPLACE PACKAGE BODY PKG_ATTENDANCE_BUSINESS IS

  /*PROCEDURE GET_WORKING_TYPE_BY_DATE(P_EMPLOYEE_ID IN NUMBER,
                                      P_DATE        IN DATE,
                                      P_OUT         OUT NUMBER) IS
  BEGIN
    SELECT W.WORKINGTYPE_ID INTO P_OUT
        FROM (
             SELECT ASE.EMPLOYEE_ID,
                   ASE.SHIFT_ID,
                   S.WORKINGTYPE_ID,
                   ROW_NUMBER() OVER(PARTITION BY ASE.EMPLOYEE_ID ORDER BY TRUNC( ASE.FROMDATE) DESC) ROW_NUMBER
              FROM AT_SHIFT S
              INNER JOIN AT_SHIFTCYCLE_EMP ASE ON S.ID = ASE.SHIFT_ID
              LEFT JOIN OT_OTHER_LIST O
                ON O.ID = S.WORKINGTYPE_ID
              WHERE ASE.EMPLOYEE_ID = P_EMPLOYEE_ID
              AND TRUNC(ASE.FROMDATE) <= TRUNC(P_DATE)
              ORDER BY ASE.FROMDATE DESC
        ) W
        WHERE ROW_NUMBER = 1;
  END;*/

  PROCEDURE GET_TOTAL_ACCUMULATIVE_OT(P_EMPLOYEE_ID IN NUMBER,
                                      P_DATE        IN DATE,
                                      P_OUT         OUT NUMBER) IS
  BEGIN
    --Lay tong so gio OT trong nam by P_EMPLOYEE_ID
    SELECT NVL(SUM(W.TOTAL_OT), 0)
      INTO P_OUT
      FROM AT_OT_REGISTRATION W
      LEFT JOIN OT_OTHER_LIST L
        ON W.OT_TYPE_ID = L.ID
     WHERE W.EMPLOYEE_ID = P_EMPLOYEE_ID
       AND W.STATUS = 1
       AND EXTRACT(YEAR FROM W.REGIST_DATE) = EXTRACT(YEAR FROM P_DATE);
  
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
  END;

  PROCEDURE DELETE_LOG_AT(P_ID IN NUMBER) IS
  BEGIN
    DELETE AT_ACTION_LOG E WHERE E.ID = P_ID;
  END;

  PROCEDURE CALCULATOR_DAY(P_FROM_DATE   IN DATE,
                           P_TO_DATE     IN DATE,
                           P_EMPLOYEE_ID IN NUMBER,
                           P_TYPE_MANUAL IN NUMBER,
                           CUR           OUT CURSOR_TYPE) IS
  BEGIN
    OPEN CUR FOR
      SELECT COUNT(*) COUNTDATA
        FROM TABLE(TABLE_LISTDATE(P_FROM_DATE, P_TO_DATE)) T
       WHERE T.CDATE NOT IN (SELECT H.WORKINGDAY FROM AT_HOLIDAY H)
         AND T.CDATE NOT IN
             (SELECT NVL2(D.WORKINGDAY,
                          D.WORKINGDAY,
                          NVL2(L.Leave_Day, L.Leave_Day, NULL))
                FROM At_Leavesheet_Detail L
                LEFT JOIN AT_TIME_MANUAL M
                  ON L.MANUAL_ID = M.ID
                LEFT JOIN AT_TIME_TIMESHEET_DAILY D
                  ON L.Leave_Day = D.WORKINGDAY
                 AND L.EMPLOYEE_ID = D.EMPLOYEE_ID
                 AND L.MANUAL_ID = D.MANUAL_ID
               WHERE L.EMPLOYEE_ID = P_EMPLOYEE_ID
                 AND (M.MORNING_ID = P_TYPE_MANUAL OR
                     M.AFTERNOON_ID = P_TYPE_MANUAL))
         AND T.CDATE >= P_FROM_DATE
         AND T.CDATE <= P_TO_DATE;
  END CALCULATOR_DAY;

  PROCEDURE CAL_DAY_LEAVE_OLD(P_FROM_DATE   IN DATE,
                              P_TO_DATE     IN DATE,
                              P_EMPLOYEE_ID IN NUMBER,
                              CUR           OUT CURSOR_TYPE) IS
  BEGIN
    OPEN CUR FOR
      SELECT COUNT(*) COUNTDATA
        FROM TABLE(TABLE_LISTDATE(P_FROM_DATE, P_TO_DATE)) T
       WHERE T.CDATE NOT IN (SELECT H.WORKINGDAY FROM AT_HOLIDAY H)
         AND T.CDATE >= P_FROM_DATE
         AND T.CDATE <= P_TO_DATE;
  
    /*MOD(TO_CHAR(T.CDATE, 'J'), 7) NOT IN (6, 5) -- neu check khong tinh thu 7 va chu nhat thi them dieu kien nay
    AND*/
  END CAL_DAY_LEAVE_OLD;

  PROCEDURE GET_TOTAL_PHEPNAM(P_EMPLOYEE_ID   IN NUMBER,
                              P_YEAR          IN NUMBER,
                              P_TYPE_LEAVE_ID IN NUMBER,
                              P_CUR           OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT NVL(SUM(TB.TOTAL), 0) TOTAL
        FROM (SELECT CASE
                       WHEN M.MORNING_ID = 251 AND M.AFTERNOON_ID = 251 THEN
                        1
                       WHEN M.MORNING_ID = 251 AND M.AFTERNOON_ID <> 251 THEN
                        0.5
                       WHEN M.MORNING_ID <> 251 AND M.AFTERNOON_ID = 251 THEN
                        0.5
                       ELSE
                        0
                     END TOTAL
                FROM At_Leavesheet_Detail L
               INNER JOIN AT_TIME_MANUAL M
                  ON L.MANUAL_ID = M.ID
               WHERE L.EMPLOYEE_ID = P_EMPLOYEE_ID
                 AND L.Leave_Day NOT IN
                     (SELECT H.WORKINGDAY FROM AT_HOLIDAY H)
                    /*AND MOD(TO_CHAR(L.WORKINGDAY, 'J'), 7) NOT IN (6, 5)*/
                 AND TO_CHAR(L.Leave_Day, 'yyyy') = P_YEAR) TB;
  END GET_TOTAL_PHEPNAM;
  PROCEDURE GET_TOTAL_PHEPBU(P_EMPLOYEE_ID   IN NUMBER,
                             P_YEAR          IN NUMBER,
                             P_TYPE_LEAVE_ID IN NUMBER,
                             P_CUR           OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT NVL(SUM(TB.TOTAL), 0) TOTAL
        FROM (SELECT CASE
                       WHEN M.MORNING_ID = 255 AND M.AFTERNOON_ID = 255 THEN
                        1
                       WHEN M.MORNING_ID = 255 AND M.AFTERNOON_ID <> 255 THEN
                        0.5
                       WHEN M.MORNING_ID <> 255 AND M.AFTERNOON_ID = 255 THEN
                        0.5
                       ELSE
                        0
                     END TOTAL
                FROM At_Leavesheet_Detail L
               INNER JOIN AT_TIME_MANUAL M
                  ON L.MANUAL_ID = M.ID
               WHERE L.EMPLOYEE_ID = P_EMPLOYEE_ID
                 AND L.Leave_Day NOT IN
                     (SELECT H.WORKINGDAY FROM AT_HOLIDAY H)
                    /* AND MOD(TO_CHAR(L.WORKINGDAY, 'J'), 7) NOT IN (6, 5)*/
                 AND TO_CHAR(L.Leave_Day, 'yyyy') = P_YEAR) TB;
  END GET_TOTAL_PHEPBU;
  --NGHI BU
  PROCEDURE CALL_ENTITLEMENT_NB(P_USERNAME   NVARCHAR2,
                                P_ORG_ID     IN NUMBER,
                                P_PERIOD_ID  IN NUMBER,
                                P_ISDISSOLVE IN NUMBER) IS
  
    P_CAL_DATE    DATE;
    PV_FROMDATE   DATE;
    PV_ENDDATE    DATE;
    PV_REQUEST_ID NUMBER;
    PV_YEAR       NUMBER;
    PV_MONTH      NUMBER;
    PV_SQL        NVARCHAR2(1000);
    PV_STRTANG    NVARCHAR2(200);
    PV_STRGIAM    NVARCHAR2(200);
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
    SELECT P.START_DATE,
           P.END_DATE,
           P.END_DATE,
           EXTRACT(YEAR FROM P.END_DATE),
           EXTRACT(MONTH FROM P.END_DATE)
      INTO PV_FROMDATE, PV_ENDDATE, P_CAL_DATE, PV_YEAR, PV_MONTH
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
  
    INSERT INTO AT_CHOSEN_EMP_TEMP
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       TER_EFFECT_DATE,
       USERNAME,
       REQUEST_ID,
       JOIN_DATE,
       JOIN_DATE_STATE,
       EFFECT_DATE_TO_NB,
       EFFECT_DATE_NB)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              CASE
                WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                 T.TER_EFFECT_DATE + 1
                ELSE
                 NULL
              END TER_EFFECT_DATE,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              T.JOIN_DATE,
              T.JOIN_DATE_STATE,
              FN_GET_PHEP('EFFECT_DATE_TO_NB', W.ORG_ID, PV_YEAR),
              CASE
                WHEN FN_GET_EXPIREDATE_NB(W.ORG_ID, PV_YEAR) = '0' THEN
                 NULL
                ELSE
                 TO_DATE(FN_GET_EXPIREDATE_NB(W.ORG_ID, PV_YEAR) || '/' ||
                         PV_YEAR,
                         'dd/MM/yyyy')
              END
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_ENDDATE
                      AND E.STATUS_ID = 447
                      AND E.IS_WAGE = 0
                      AND E.IS_3B = 0) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
           ON O.ORG_ID = W.ORG_ID
        WHERE NVL(T.IS_KIEM_NHIEM, 0) = 0
          AND (NVL(T.WORK_STATUS, 0) <> 257 OR
               (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  
    --Insert AT_COMPENSATORY cho cac nhan vien chua co du lieu phep bu (nhan vien moi)
    INSERT INTO AT_COMPENSATORY T
      (ID, EMPLOYEE_ID, YEAR, PREV_HAVE, CREATED_BY, CREATED_DATE)
      SELECT SEQ_AT_COMPENSATORY.NEXTVAL,
             EMP.EMPLOYEE_ID,
             PV_YEAR,
             0,
             UPPER(P_USERNAME),
             SYSDATE
        FROM AT_CHOSEN_EMP_TEMP EMP
       WHERE NOT EXISTS (SELECT T.ID
                FROM AT_COMPENSATORY T
               WHERE T.EMPLOYEE_ID = EMP.EMPLOYEE_ID
                 AND T.YEAR = PV_YEAR)
         AND EMP.REQUEST_ID = PV_REQUEST_ID;
  
    -- Tinh so phep nam truoc con lai
    MERGE INTO AT_COMPENSATORY A
    USING (SELECT ENT1.EMPLOYEE_ID, EMP.EFFECT_DATE_TO_NB
             FROM AT_COMPENSATORY ENT1
            INNER JOIN AT_CHOSEN_EMP_TEMP EMP
               ON ENT1.EMPLOYEE_ID = EMP.EMPLOYEE_ID
              AND EMP.REQUEST_ID = PV_REQUEST_ID
            WHERE ENT1.YEAR = PV_YEAR
              AND EXISTS (SELECT EMP.EMPLOYEE_ID
                     FROM AT_CHOSEN_EMP_TEMP EMP
                    WHERE EMP.EMPLOYEE_ID = ENT1.EMPLOYEE_ID
                      AND EMP.REQUEST_ID = PV_REQUEST_ID)) B
    ON (a.EMPLOYEE_ID = b.EMPLOYEE_ID)
    WHEN MATCHED THEN
      UPDATE
         SET A.PREV_HAVE = CASE
                             WHEN NVL(B.EFFECT_DATE_TO_NB, 0) = PV_MONTH THEN
                              0
                             ELSE
                              NVL(A.PREV_HAVE, 0)
                           END
       WHERE A.YEAR = PV_YEAR
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP_TEMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID);
  
    --Update lai da tong hop toi thang nao
    UPDATE AT_COMPENSATORY ENT
       SET ENT.CAL_MONTH = PV_MONTH
     WHERE ENT.YEAR = PV_YEAR
       AND ENT.CAL_MONTH < PV_MONTH
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP_TEMP EMP
             WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
               AND EMP.REQUEST_ID = PV_REQUEST_ID);
  
    --Update so nghi bu tang trong thang
    PV_SQL := 'UPDATE AT_COMPENSATORY ENT SET ENT.AL_T' || PV_MONTH ||
              ' = NVL(FN_GET_NB1(ENT.EMPLOYEE_ID, ' || P_PERIOD_ID ||
              '),0)
                WHERE ENT.YEAR = ' || PV_YEAR || '
                    AND EXISTS(SELECT EMP.EMPLOYEE_ID FROM AT_CHOSEN_EMP_TEMP EMP
                                WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                                AND EMP.REQUEST_ID = ' ||
              PV_REQUEST_ID || ')';
    EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
    /* INSERT INTO AT_STRSQL(ID,STRINGSQL)
    VALUES(12,PV_SQL); */
  
    --TInh lai CUR_HAVE: so nghi bu tinh toi cua tung thang
    FOR i IN 1 .. (PV_MONTH - 1) LOOP
      PV_STRTANG := PV_STRTANG || ' + ENT.AL_T' || i;
      PV_STRGIAM := PV_STRGIAM || ' - ENT.CUR_USED' || i;
    END LOOP;
  
    FOR month IN PV_MONTH .. 12 LOOP
      PV_STRTANG := PV_STRTANG || ' + ENT.AL_T' || month;
      PV_STRGIAM := PV_STRGIAM || ' - ENT.CUR_USED' || month;
    
      PV_SQL := 'UPDATE AT_COMPENSATORY ENT SET ENT.CUR_HAVE' || month ||
                ' = ENT.PREV_HAVE' || PV_STRTANG || PV_STRGIAM ||
                ' + ENT.CUR_USED' || month || '
                WHERE ENT.YEAR = ' || PV_YEAR || ' AND ' ||
                month ||
                ' <= ENT.CAL_MONTH
                    AND EXISTS(SELECT EMP.EMPLOYEE_ID FROM AT_CHOSEN_EMP_TEMP EMP
                                WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                                AND EMP.REQUEST_ID = ' ||
                PV_REQUEST_ID || ')';
      /*INSERT INTO AT_STRSQL(ID,STRINGSQL)
      VALUES(month,PV_SQL); */
    
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
    END LOOP;
  
    --Update so nghi bu tu bang cong thang qua
    PV_SQL := 'UPDATE AT_COMPENSATORY ENT SET ENT.CUR_USED' || PV_MONTH ||
              ' = NVL((SELECT T.WORKING_B FROM(SELECT SUM(T.WORKING_B) WORKING_B, T.EMPLOYEE_ID
                        FROM AT_TIME_TIMESHEET_MONTHLY T
                   WHERE T.PERIOD_ID = ' || P_PERIOD_ID || '
                   GROUP BY T.EMPLOYEE_ID) T
                   WHERE T.EMPLOYEE_ID = ENT.EMPLOYEE_ID),0)
                WHERE ENT.YEAR = ' || PV_YEAR || '
                    AND EXISTS(SELECT EMP.EMPLOYEE_ID FROM AT_CHOSEN_EMP_TEMP EMP
                                WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                                AND EMP.REQUEST_ID = ' ||
              PV_REQUEST_ID || ')';
    /* INSERT INTO AT_STRSQL(ID,STRINGSQL)
    VALUES(PV_REQUEST_ID,PV_SQL);*/
    EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
  
    --Tinh toan lai so phep nghi bu da sd
    UPDATE AT_COMPENSATORY ENT
       SET ENT.CUR_USED = ROUND((NVL(ENT.CUR_USED1, 0) +
                                NVL(ENT.CUR_USED2, 0) +
                                NVL(ENT.CUR_USED3, 0) +
                                NVL(ENT.CUR_USED4, 0) +
                                NVL(ENT.CUR_USED5, 0) +
                                NVL(ENT.CUR_USED6, 0) +
                                NVL(ENT.CUR_USED7, 0) +
                                NVL(ENT.CUR_USED8, 0) +
                                NVL(ENT.CUR_USED9, 0) +
                                NVL(ENT.CUR_USED10, 0) +
                                NVL(ENT.CUR_USED11, 0) +
                                NVL(ENT.CUR_USED12, 0)),
                                0)
     WHERE ENT.YEAR = PV_YEAR
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP_TEMP EMP
             WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
               AND EMP.REQUEST_ID = PV_REQUEST_ID);
    --Xu ly chuyen sang nam moi
    -- Mac dinh xu ly cho ngay chuyen phep nghi bu nam la thang 12
    -- Chu khong xu ly lay theo ngay tren tham so he thong
    IF EXTRACT(MONTH FROM PV_ENDDATE) = 12 THEN
      --Chuyen phep bu con lai sang nam moi
      INSERT INTO AT_COMPENSATORY T
        (ID,
         EMPLOYEE_ID,
         YEAR,
         PREV_HAVE,
         CUR_HAVE,
         CREATED_BY,
         CREATED_DATE)
        SELECT SEQ_AT_ENTITLEMENT.NEXTVAL,
               EMP.EMPLOYEE_ID,
               PV_YEAR + 1,
               ENT.CUR_HAVE,
               ENT.CUR_HAVE,
               UPPER(P_USERNAME),
               SYSDATE
          FROM AT_CHOSEN_EMP_TEMP EMP
         INNER JOIN AT_COMPENSATORY ENT
            ON ENT.YEAR = PV_YEAR
           AND ENT.EMPLOYEE_ID = EMP.EMPLOYEE_ID
         WHERE NOT EXISTS (SELECT T.ID
                  FROM AT_COMPENSATORY T
                 WHERE T.EMPLOYEE_ID = EMP.EMPLOYEE_ID
                   AND T.YEAR = PV_YEAR + 1)
           AND EMP.REQUEST_ID = PV_REQUEST_ID;
    
      MERGE INTO AT_COMPENSATORY A
      USING (SELECT ENT1.EMPLOYEE_ID, ENT1.CUR_HAVE
               FROM AT_COMPENSATORY ENT1
              WHERE ENT1.YEAR = PV_YEAR
                AND EXISTS
              (SELECT EMP.EMPLOYEE_ID
                       FROM AT_CHOSEN_EMP_TEMP EMP
                      WHERE EMP.EMPLOYEE_ID = ENT1.EMPLOYEE_ID
                        AND EMP.REQUEST_ID = PV_REQUEST_ID)) B
      ON (a.EMPLOYEE_ID = b.EMPLOYEE_ID)
      WHEN MATCHED THEN
        UPDATE
           SET A.PREV_HAVE = B.CUR_HAVE
         WHERE A.YEAR = PV_YEAR + 1
           AND EXISTS (SELECT EMP.EMPLOYEE_ID
                  FROM AT_CHOSEN_EMP_TEMP EMP
                 WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                   AND EMP.REQUEST_ID = PV_REQUEST_ID);
    
    END IF;
  
    -- Tinh so nghi bu con lai
    UPDATE AT_COMPENSATORY ENT
       SET ENT.TOTAL_HAVE = CASE
                              WHEN PV_MONTH = 1 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1)
                              WHEN PV_MONTH = 2 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1 + ENT.AL_T2 -
                               ENT.CUR_USED1)
                              WHEN PV_MONTH = 3 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1 + ENT.AL_T2 +
                               ENT.AL_T3 - ENT.CUR_USED1 - ENT.CUR_USED2)
                              WHEN PV_MONTH = 4 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1 + ENT.AL_T2 +
                               ENT.AL_T3 + ENT.AL_T4 - ENT.CUR_USED1 -
                               ENT.CUR_USED2 - ENT.CUR_USED3)
                              WHEN PV_MONTH = 5 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1 + ENT.AL_T2 +
                               ENT.AL_T3 + ENT.AL_T4 + ENT.AL_T5 -
                               ENT.CUR_USED1 - ENT.CUR_USED2 - ENT.CUR_USED3 -
                               ENT.CUR_USED4)
                              WHEN PV_MONTH = 6 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1 + ENT.AL_T2 +
                               ENT.AL_T3 + ENT.AL_T4 + ENT.AL_T5 + ENT.AL_T6 -
                               ENT.CUR_USED1 - ENT.CUR_USED2 - ENT.CUR_USED3 -
                               ENT.CUR_USED4 - ENT.CUR_USED5)
                              WHEN PV_MONTH = 7 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1 + ENT.AL_T2 +
                               ENT.AL_T3 + ENT.AL_T4 + ENT.AL_T5 + ENT.AL_T6 +
                               ENT.AL_T7 - ENT.CUR_USED1 - ENT.CUR_USED2 -
                               ENT.CUR_USED3 - ENT.CUR_USED4 - ENT.CUR_USED5 -
                               ENT.CUR_USED6)
                              WHEN PV_MONTH = 8 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1 + ENT.AL_T2 +
                               ENT.AL_T3 + ENT.AL_T4 + ENT.AL_T5 + ENT.AL_T6 +
                               ENT.AL_T7 + ENT.AL_T8 - ENT.CUR_USED1 -
                               ENT.CUR_USED2 - ENT.CUR_USED3 - ENT.CUR_USED4 -
                               ENT.CUR_USED5 - ENT.CUR_USED6 - ENT.CUR_USED7)
                              WHEN PV_MONTH = 9 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1 + ENT.AL_T2 +
                               ENT.AL_T3 + ENT.AL_T4 + ENT.AL_T5 + ENT.AL_T6 +
                               ENT.AL_T7 + ENT.AL_T8 + ENT.AL_T9 -
                               ENT.CUR_USED1 - ENT.CUR_USED2 - ENT.CUR_USED3 -
                               ENT.CUR_USED4 - ENT.CUR_USED5 - ENT.CUR_USED6 -
                               ENT.CUR_USED7 - ENT.CUR_USED8)
                              WHEN PV_MONTH = 10 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1 + ENT.AL_T2 +
                               ENT.AL_T3 + ENT.AL_T4 + ENT.AL_T5 + ENT.AL_T6 +
                               ENT.AL_T7 + ENT.AL_T8 + ENT.AL_T9 +
                               ENT.AL_T10 - ENT.CUR_USED1 - ENT.CUR_USED2 -
                               ENT.CUR_USED3 - ENT.CUR_USED4 - ENT.CUR_USED5 -
                               ENT.CUR_USED6 - ENT.CUR_USED7 - ENT.CUR_USED8 -
                               ENT.CUR_USED9)
                              WHEN PV_MONTH = 11 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1 + ENT.AL_T2 +
                               ENT.AL_T3 + ENT.AL_T4 + ENT.AL_T5 + ENT.AL_T6 +
                               ENT.AL_T7 + ENT.AL_T8 + ENT.AL_T9 +
                               ENT.AL_T10 + ENT.AL_T11 - ENT.CUR_USED1 -
                               ENT.CUR_USED2 - ENT.CUR_USED3 - ENT.CUR_USED4 -
                               ENT.CUR_USED5 - ENT.CUR_USED6 - ENT.CUR_USED7 -
                               ENT.CUR_USED8 - ENT.CUR_USED9 -
                               ENT.CUR_USED10)
                              WHEN PV_MONTH = 12 THEN
                               (ENT.PREV_HAVE + ENT.AL_T1 + ENT.AL_T2 +
                               ENT.AL_T3 + ENT.AL_T4 + ENT.AL_T5 + ENT.AL_T6 +
                               ENT.AL_T7 + ENT.AL_T8 + ENT.AL_T9 +
                               ENT.AL_T10 + ENT.AL_T11 + ENT.AL_T12 -
                               ENT.CUR_USED1 - ENT.CUR_USED2 - ENT.CUR_USED3 -
                               ENT.CUR_USED4 - ENT.CUR_USED5 - ENT.CUR_USED6 -
                               ENT.CUR_USED7 - ENT.CUR_USED8 - ENT.CUR_USED9 -
                               ENT.CUR_USED10 - ENT.CUR_USED11)
                              ELSE
                               0
                            END
     WHERE ENT.YEAR = PV_YEAR
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP_TEMP EMP
             WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
               AND EMP.REQUEST_ID = PV_REQUEST_ID);
  
    UPDATE AT_COMPENSATORY ENT
       SET ENT.CUR_HAVE = CASE
                            WHEN PV_MONTH = 1 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED1)
                            WHEN PV_MONTH = 2 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED2)
                            WHEN PV_MONTH = 3 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED3)
                            WHEN PV_MONTH = 4 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED4)
                            WHEN PV_MONTH = 5 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED5)
                            WHEN PV_MONTH = 6 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED6)
                            WHEN PV_MONTH = 7 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED7)
                            WHEN PV_MONTH = 8 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED8)
                            WHEN PV_MONTH = 9 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED9)
                            WHEN PV_MONTH = 10 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED10)
                            WHEN PV_MONTH = 11 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED11)
                            WHEN PV_MONTH = 12 THEN
                             (ENT.TOTAL_HAVE - ENT.CUR_USED12)
                            ELSE
                             0
                          END
     WHERE ENT.YEAR = PV_YEAR
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP_TEMP EMP
             WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
               AND EMP.REQUEST_ID = PV_REQUEST_ID);
  
    -- Tinh so phep nam truoc con lai
    MERGE INTO AT_COMPENSATORY A
    USING (SELECT EMP.EMPLOYEE_ID, EMP.EFFECT_DATE_NB
             FROM AT_CHOSEN_EMP_TEMP EMP
            WHERE EMP.REQUEST_ID = PV_REQUEST_ID) B
    ON (a.EMPLOYEE_ID = b.EMPLOYEE_ID)
    WHEN MATCHED THEN
      UPDATE
         SET A.EXPIREDATE = B.EFFECT_DATE_NB
       WHERE A.YEAR = PV_YEAR
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP_TEMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID);
  
    DELETE AT_CHOSEN_EMP_TEMP E WHERE E.REQUEST_ID = PV_REQUEST_ID;
    DELETE AT_CHOSEN_ORG A WHERE A.REQUEST_ID = PV_REQUEST_ID;
  
  EXCEPTION
    WHEN OTHERS THEN
      DELETE AT_CHOSEN_EMP_TEMP E WHERE E.REQUEST_ID = PV_REQUEST_ID;
      DELETE AT_CHOSEN_ORG A WHERE A.REQUEST_ID = PV_REQUEST_ID;
    
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT_NB',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              null,
                              null,
                              null,
                              null);
    
  END;

  PROCEDURE IMPORT_AT_SWIPE_DATA(P_USER IN NVARCHAR2,
                                 P_DATA IN CLOB,
                                 P_CUR  OUT CURSOR_TYPE) IS
  
  BEGIN
    --INSERT DATA IMPORT INTO TABLE TEMP
    INSERT INTO TEMP1 (TMP, WCODE) VALUES (P_DATA, 'IMPORT_AT_SWIPE_DATA');
    INSERT INTO AT_SWIPE_DATA_IMPORT_TEMP
      (ITIME_ID, VALTIME, WORKINGDAY, MACHINE_TYPE, TERMINAL_ID)
      WITH JSON AS
       (SELECT (P_DATA) DOC FROM DUAL)
      SELECT USER_ID,
             TO_DATE(DATA.WORKING_DAY, 'DD/MM/YYYY HH24:MI:SS'),
             TRUNC(TO_DATE(DATA.WORKING_DAY, 'DD/MM/YYYY HH24:MI:SS')),
             MACHINE_TYPE,
             NVL(TERMINAL_ID, 0) -- TERMINAL_ID=0 => DU LIEU ACCESS CONTROL
        FROM JSON_TABLE((SELECT DOC FROM JSON),
                        '$[*]' COLUMNS(USER_ID PATH '$.USER_ID',
                                TERMINAL_ID PATH '$.TERMINAL_ID',
                                WORKING_DAY PATH '$.WORKING_DAY',
                                MACHINE_TYPE PATH '$.MACHINE_TYPE')) DATA;
  
    DELETE AT_SWIPE_DATA AT
     WHERE EXISTS (SELECT T.ITIME_ID
              FROM AT_SWIPE_DATA_TEMP T
             WHERE T.ITIME_ID = AT.ITIME_ID
               AND T.Workingday = AT.Workingday);
    --INSERT DU LIEU VAO AT_SWIPE_DATA ALL DATA
    INSERT INTO AT_SWIPE_DATA
      (ID,
       EMPLOYEE_ID,
       ITIME_ID,
       VALTIME,
       TERMINAL_ID,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       WORKINGDAY,
       MACHINE_TYPE)
      SELECT SEQ_AT_SWIPE_DATA_ALL.NEXTVAL,
             PKG_FUNCTION.GET_EMP_BY_ATT_CODE(T.ITIME_ID,
                                              T.MACHINE_TYPE,
                                              T.WORKINGDAY),
             T.ITIME_ID,
             T.VALTIME,
             T.TERMINAL_ID,
             SYSDATE,
             P_USER,
             P_USER,
             T.WORKINGDAY,
             T.MACHINE_TYPE
        FROM AT_SWIPE_DATA_IMPORT_TEMP T;
    ------------------------------------------------------
    --XU LY DU LIEU ACCESS CONTROL LAY MAX MIN
    --INSERT DATA IN
    INSERT INTO AT_SWIPE_DATA_TEMP
      (ID,
       EMPLOYEE_ID,
       VALTIME,
       WORKINGDAY,
       MACHINE_TYPE,
       TERMINAL_ID,
       ITIME_ID)
      SELECT SEQ_AT_SWIPE_DATA_TEMP.NEXTVAL,
             PKG_FUNCTION.GET_EMP_BY_ATT_CODE(T.ITIME_ID,
                                              T.MACHINE_TYPE,
                                              T.WORKINGDAY),
             T.*
        FROM (SELECT MIN(TEMP.VALTIME),
                     TEMP.WORKINGDAY,
                     TEMP.MACHINE_TYPE,
                     1, --DATA IN
                     TEMP.ITIME_ID
                FROM AT_SWIPE_DATA_IMPORT_TEMP TEMP
               WHERE NVL(TEMP.TERMINAL_ID, 0) = 0
               GROUP BY TEMP.ITIME_ID, TEMP.WORKINGDAY, TEMP.MACHINE_TYPE) T;
  
    --INSERT DATA OUT
    INSERT INTO AT_SWIPE_DATA_TEMP
      (ID,
       EMPLOYEE_ID,
       VALTIME,
       WORKINGDAY,
       MACHINE_TYPE,
       TERMINAL_ID,
       ITIME_ID)
      SELECT SEQ_AT_SWIPE_DATA_TEMP.NEXTVAL,
             PKG_FUNCTION.GET_EMP_BY_ATT_CODE(T.ITIME_ID,
                                              T.MACHINE_TYPE,
                                              T.WORKINGDAY),
             T.*
        FROM (SELECT MAX(TEMP.VALTIME),
                     TEMP.WORKINGDAY,
                     TEMP.MACHINE_TYPE,
                     2, --DATA OUT
                     TEMP.ITIME_ID
                FROM AT_SWIPE_DATA_IMPORT_TEMP TEMP
               WHERE NVL(TEMP.TERMINAL_ID, 0) = 0
               GROUP BY TEMP.ITIME_ID, TEMP.WORKINGDAY, TEMP.MACHINE_TYPE
              HAVING COUNT(*) > 1) T;
    ----END XU LY DU LIEU ACCESS CONTROL LAY MAX MIN------
    --LAY DU LIEU DU LIEU VAN TAY VA CARPARKING-----------
    INSERT INTO AT_SWIPE_DATA_TEMP
      (ID,
       EMPLOYEE_ID,
       VALTIME,
       WORKINGDAY,
       MACHINE_TYPE,
       TERMINAL_ID,
       ITIME_ID)
      SELECT SEQ_AT_SWIPE_DATA_TEMP.NEXTVAL,
             PKG_FUNCTION.GET_EMP_BY_ATT_CODE(TEMP.ITIME_ID,
                                              TEMP.MACHINE_TYPE,
                                              TEMP.WORKINGDAY),
             TEMP.VALTIME,
             TEMP.WORKINGDAY,
             TEMP.MACHINE_TYPE,
             TEMP.TERMINAL_ID,
             TEMP.ITIME_ID
        FROM AT_SWIPE_DATA_IMPORT_TEMP TEMP
       WHERE NVL(TEMP.TERMINAL_ID, 0) > 0;
  
    DELETE FROM AT_SWIPE_DATA_IMPORT_TEMP;
    DELETE AT_SWIPE_DATA AT
     WHERE EXISTS (SELECT T.ITIME_ID
              FROM AT_SWIPE_DATA_TEMP T
             WHERE T.ITIME_ID = AT.ITIME_ID
               AND T.WORKINGDAY = AT.WORKINGDAY);
    --INSERT DU LIEU VAO AT_SWIPE_DATA
    INSERT INTO AT_SWIPE_DATA_SUMMARY
      (ID,
       EMPLOYEE_ID,
       ITIME_ID,
       VALTIME,
       TERMINAL_ID,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       WORKINGDAY,
       MACHINE_TYPE)
      SELECT SEQ_AT_SWIPE_DATA_SUMMARY.NEXTVAL,
             T.EMPLOYEE_ID,
             T.ITIME_ID,
             T.VALTIME,
             T.TERMINAL_ID,
             SYSDATE,
             P_USER,
             P_USER,
             T.WORKINGDAY,
             T.MACHINE_TYPE
        FROM AT_SWIPE_DATA_TEMP T;
    DELETE FROM AT_SWIPE_DATA_TEMP;
    OPEN P_CUR FOR
      SELECT 1 FROM DUAL;
  EXCEPTION
    WHEN OTHERS THEN
      OPEN P_CUR FOR
        SELECT 0 FROM DUAL;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.IMPORT_AT_SWIPE_DATA',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace);
  END;

  --TINH PHEP NAM
  /*PROCEDURE CALL_ENTITLEMENT(P_USERNAME   VARCHAR2,
                               P_ORG_ID     IN NUMBER,
                               P_PERIOD_ID  IN NUMBER,
                               P_ISDISSOLVE IN NUMBER) IS
      P_CAL_DATE     DATE;
      PV_FROMDATE    DATE;
      PV_ENDDATE     DATE;
      PV_REQUEST_ID  NUMBER;
      PV_YEAR        NUMBER;
      PV_SQL         NVARCHAR2(1000);
      PV_TOTAL_P     NUMBER;
      PV_RESET_MONTH NUMBER;
      PV_MONTH       NUMBER;
      PV_MONTH_P     NUMBER;
      PV_YEAR_TN     NUMBER;
      PV_DAY_TN      NUMBER;
      PV_I           NUMBER;
      PV_STRING      VARCHAR2(2000);
    BEGIN
      PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
      SELECT P.START_DATE,
             P.END_DATE,
             P.END_DATE,
             EXTRACT(YEAR FROM P.END_DATE),
             p.month
        INTO PV_FROMDATE, PV_ENDDATE, P_CAL_DATE, PV_YEAR,PV_MONTH
        FROM AT_PERIOD P
       WHERE P.ID = P_PERIOD_ID;
  
          SELECT  t.year_p,T.TO_LEAVE_YEAR,ROUND(to_number(t.year_p)/12,2),
                  T.YEAR_TN,T.DAY_TN
           INTO PV_TOTAL_P,PV_RESET_MONTH,PV_MONTH_P,PV_YEAR_TN,PV_DAY_TN
          FROM (  select t.year_p,TO_CHAR(T.TO_LEAVE_YEAR,'MM') TO_LEAVE_YEAR,
                         T.YEAR_TN,T.DAY_TN
           from AT_LIST_PARAM_SYSTEM t
           WHERE TO_CHAR(T.EFFECT_DATE_FROM,'yyyy') <= PV_YEAR
                 AND T.ACTFLG = 'A'
           ORDER BY  T.EFFECT_DATE_FROM DESC ) T
           WHERE ROWNUM = 1;
  
      -- Insert org can tinh toan
      INSERT INTO AT_CHOSEN_ORG E
        (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
           FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                      P_ORG_ID,
                                      P_ISDISSOLVE)) O);
  
      INSERT INTO AT_CHOSEN_EMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE
           FROM HU_EMPLOYEE T
          INNER JOIN (SELECT E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          INNER JOIN (SELECT E.EMPLOYEE_ID,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.START_DATE DESC) AS ROW_NUMBER
                       FROM HU_CONTRACT E
                       INNER JOIN HU_CONTRACT_TYPE S
                         ON S.ID = E.CONTRACT_TYPE
                       INNER JOIN OT_OTHER_LIST O
                         ON O.ID = S.TYPE_ID
                       AND O.CODE <> 'HDTV'
                      WHERE E.START_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447) CT
             ON T.ID = CT.EMPLOYEE_ID
            AND CT.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
             ON O.ORG_ID = W.ORG_ID
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  
        DELETE FROM  AT_ENTITLEMENT T
        WHERE  EXISTS (SELECT a.ID
                  FROM AT_ENTITLEMENT a
                  INNER JOIN AT_CHOSEN_EMP EMP
                   ON EMP.EMPLOYEE_ID = a.EMPLOYEE_ID
                 WHERE T.EMPLOYEE_ID = EMP.EMPLOYEE_ID
                   AND a.YEAR = PV_YEAR
                   AND a.MONTH = PV_MONTH)
               and t.YEAR = PV_YEAR
               AND t.MONTH = PV_MONTH;
  
      INSERT INTO AT_ENTITLEMENT T
        (ID,
         EMPLOYEE_ID,
         YEAR,
         MONTH,
         PERIOD_ID,
         WORKING_TIME_HAVE,
         TOTAL_HAVE,
         CREATED_BY,
         CREATED_DATE,
         PREV_HAVE,
         PREV_USED,
         EXPIREDATE,
          CUR_HAVE,
          CUR_USED ,
          FUND ,
          SPECIAL,
          CUR_HAVE1,
          CUR_HAVE2,
          CUR_HAVE3,
          CUR_HAVE4,
          CUR_HAVE5,
          CUR_HAVE6,
          CUR_HAVE7,
          CUR_HAVE8,
          CUR_HAVE9,
          CUR_HAVE10,
          CUR_HAVE11,
          CUR_HAVE12,
          CUR_USED1,
          CUR_USED2,
          CUR_USED3,
          CUR_USED4,
          CUR_USED5,
          CUR_USED6,
          CUR_USED7,
          CUR_USED8,
          CUR_USED9,
          CUR_USED10,
          CUR_USED11,
          CUR_USED12,
          AL_TO_CASH,
          REASON,
          EXPIREDATE_NB,
          FIXADD,
          PAY_PREV_ENT,
          AL_T1,
          AL_T2,
          AL_T3,
          AL_T4,
          AL_T5,
          AL_T6,
          AL_T7,
          AL_T8,
          AL_T9,
          AL_T10,
          AL_T11,
          AL_T12,
          AL_ADD_T1,
          AL_ADD_T2,
          AL_ADD_T3,
          AL_ADD_T4,
          AL_ADD_T5,
          AL_ADD_T6,
          AL_ADD_T7,
          AL_ADD_T8,
          AL_ADD_T9,
          AL_ADD_T10,
          AL_ADD_T11,
          AL_ADD_T12,
          PREV_USED1,
          PREV_USED2,
          PREV_USED3,
          PREV_USED4,
          PREV_USED5,
          PREV_USED6,
          PREV_USED7,
          PREV_USED8,
          PREV_USED9,
          PREV_USED10,
          PREV_USED11,
          PREV_USED12,
          TOTAL_PAY_PREV_ENT,
          SUB_PREV_ENT,
          BALANCE_WORKING_TIME_3B,
          SENIORITYHAVE,
          TOTAL_HAVE1)
        SELECT SEQ_AT_ENTITLEMENT.NEXTVAL,
               EMP.EMPLOYEE_ID,
               PV_YEAR,
               PV_MONTH,
               P_PERIOD_ID,
               EXTRACT(YEAR FROM EMP.JOIN_DATE),
               CASE
                 WHEN PV_YEAR - EXTRACT(YEAR FROM EMP.JOIN_DATE) = 0 THEN
                  PV_TOTAL_P - EXTRACT(MONTH FROM EMP.JOIN_DATE) + 1 + FN_CALL_TN(emp.employee_id,PV_ENDDATE,PV_YEAR_TN,PV_DAY_TN)
                 ELSE
                  PV_TOTAL_P + FN_CALL_TN(emp.employee_id,PV_ENDDATE,PV_YEAR_TN,PV_DAY_TN)
               END,
               UPPER(P_USERNAME),
               SYSDATE,
               PREV_HAVE,
         PREV_USED,
         EXPIREDATE,
          CUR_HAVE,
          CUR_USED ,
          FUND ,
          SPECIAL,
          CUR_HAVE1,
          CUR_HAVE2,
          CUR_HAVE3,
          CUR_HAVE4,
          CUR_HAVE5,
          CUR_HAVE6,
          CUR_HAVE7,
          CUR_HAVE8,
          CUR_HAVE9,
          CUR_HAVE10,
          CUR_HAVE11,
          CUR_HAVE12,
          CUR_USED1,
          CUR_USED2,
          CUR_USED3,
          CUR_USED4,
          CUR_USED5,
          CUR_USED6,
          CUR_USED7,
          CUR_USED8,
          CUR_USED9,
          CUR_USED10,
          CUR_USED11,
          CUR_USED12,
          AL_TO_CASH,
          REASON,
          EXPIREDATE_NB,
          FIXADD,
          PAY_PREV_ENT,
          AL_T1,
          AL_T2,
          AL_T3,
          AL_T4,
          AL_T5,
          AL_T6,
          AL_T7,
          AL_T8,
          AL_T9,
          AL_T10,
          AL_T11,
          AL_T12,
          AL_ADD_T1,
          AL_ADD_T2,
          AL_ADD_T3,
          AL_ADD_T4,
          AL_ADD_T5,
          AL_ADD_T6,
          AL_ADD_T7,
          AL_ADD_T8,
          AL_ADD_T9,
          AL_ADD_T10,
          AL_ADD_T11,
          AL_ADD_T12,
          PREV_USED1,
          PREV_USED2,
          PREV_USED3,
          PREV_USED4,
          PREV_USED5,
          PREV_USED6,
          PREV_USED7,
          PREV_USED8,
          PREV_USED9,
          PREV_USED10,
          PREV_USED11,
          PREV_USED12,
          TOTAL_PAY_PREV_ENT,
          SUB_PREV_ENT,
          BALANCE_WORKING_TIME_3B,
          SENIORITYHAVE,
          PV_TOTAL_P
          FROM AT_CHOSEN_EMP EMP
          LEFT JOIN AT_ENTITLEMENT A
           ON A.EMPLOYEE_ID = EMP.EMPLOYEE_ID
           AND A.YEAR = PV_YEAR
           AND A.MONTH = PV_MONTH -1 ;
  
      --Update so phep tu bang cong thang qua
      PV_SQL := 'UPDATE AT_ENTITLEMENT ENT SET ENT.CUR_USED' ||
                EXTRACT(MONTH FROM PV_ENDDATE) ||
                ' = NVL((SELECT T.WORKING_P FROM AT_TIME_TIMESHEET_MONTHLY T
                     WHERE T.PERIOD_ID = ' || P_PERIOD_ID ||
                ' AND T.EMPLOYEE_ID = ENT.EMPLOYEE_ID),0)
                  WHERE ENT.YEAR = ' || PV_YEAR || '
                  AND ENT.MONTH = ' || PV_MONTH || '
                      AND EXISTS(SELECT EMP.EMPLOYEE_ID FROM AT_CHOSEN_EMP EMP
                                  WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID)';
      \*INSERT INTO AT_STRSQL(ID,STRINGSQL)
      VALUES(PV_REQUEST_ID,PV_SQL); *\
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
  
     FOR PV_I IN 1 .. PV_RESET_MONTH LOOP
       IF PV_STRING IS NOT NULL THEN
       PV_STRING := PV_STRING || ' + ' || 'NVL(A.CUR_USED' || PV_I || ',0)' ;
       ELSE
         PV_STRING := 'NVL(A.CUR_USED' || PV_I || ',0)';
       END IF;
     END LOOP;
      -- Tinh so ngay nghi phep tu dau nam den ngay reset p nam truoc
     PV_SQL := 'UPDATE AT_ENTITLEMENT A
       SET A.PREV_USED =  '|| PV_STRING ||'
       WHERE A.YEAR =  '|| PV_YEAR ||'
         AND A.MONTH = '|| PV_MONTH ||'
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID)';
       EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
  
       -- Tinh so phep nam truoc con lai
  
       MERGE INTO AT_ENTITLEMENT A
       USING (SELECT  ENT1.EMPLOYEE_ID,ENT1.CUR_HAVE
              FROM AT_ENTITLEMENT ENT1
              WHERE ENT1.YEAR = PV_YEAR -1
                AND ENT1.MONTH = 12
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = ENT1.EMPLOYEE_ID)) B
        ON (a.EMPLOYEE_ID = b.EMPLOYEE_ID)
      WHEN MATCHED THEN
        UPDATE SET A.PREV_HAVE = NVL(B.CUR_HAVE,0)
      WHERE A.YEAR = PV_YEAR
         AND A.MONTH = PV_MONTH
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID);
  
      -- TINH LAI SO PHEP NAM TRUOC DA NGHI
       UPDATE AT_ENTITLEMENT A
           SET A.PREV_USED = CASE WHEN  A.PREV_HAVE = 0 THEN
                                  0
                                  WHEN  A.PREV_HAVE < A.PREV_USED THEN
                                    A.PREV_HAVE
                                    ELSE
                                   A.PREV_USED
                                   END
           WHERE A.YEAR =   PV_YEAR
             AND A.MONTH =  PV_MONTH
             AND EXISTS (SELECT EMP.EMPLOYEE_ID
                          FROM AT_CHOSEN_EMP EMP
                         WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID);
  
       -- Tinh so phep nam truoc con lai su dung v? duoc reset vao theo thang thiet lap
  
       IF PV_MONTH > PV_RESET_MONTH THEN
         UPDATE AT_ENTITLEMENT A
           SET A.PREVTOTAL_HAVE = 0
           WHERE A.YEAR =   PV_YEAR
             AND A.MONTH =  PV_MONTH
             AND EXISTS (SELECT EMP.EMPLOYEE_ID
                          FROM AT_CHOSEN_EMP EMP
                         WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID);
       ELSE
         UPDATE AT_ENTITLEMENT A
           SET A.PREVTOTAL_HAVE = NVL(A.PREV_HAVE,0) - NVL(A.PREV_USED,0)
           WHERE A.YEAR =   PV_YEAR
             AND A.MONTH =  PV_MONTH
             AND EXISTS (SELECT EMP.EMPLOYEE_ID
                          FROM AT_CHOSEN_EMP EMP
                         WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID);
       END IF;
  
      --Tinh toan lai so phep da sd
      UPDATE AT_ENTITLEMENT ENT
         SET ENT.CUR_USED = NVL(ENT.CUR_USED1,0) + NVL(ENT.CUR_USED2,0) + NVL(ENT.CUR_USED3,0) +
                            NVL(ENT.CUR_USED4,0) + NVL(ENT.CUR_USED5,0) + NVL(ENT.CUR_USED6,0) +
                            NVL(ENT.CUR_USED7,0) + NVL(ENT.CUR_USED8,0) + NVL(ENT.CUR_USED9,0) +
                            NVL(ENT.CUR_USED10,0) + NVL(ENT.CUR_USED11,0) + NVL(ENT.CUR_USED12,0) -NVL(ENT.PREV_USED,0),
             ENT.SENIORITYHAVE = FN_CALL_TN(ENT.EMPLOYEE_ID,PV_ENDDATE,PV_YEAR_TN,PV_DAY_TN)
       WHERE ENT.YEAR = PV_YEAR
             AND ENT.MONTH = PV_MONTH
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID);
  
      --Tinh toan lai so phep con lai
      IF PV_RESET_MONTH >= PV_MONTH THEN
      UPDATE AT_ENTITLEMENT ENT
         SET ENT.CUR_HAVE = NVL(ENT.PREV_HAVE,0) + NVL(ENT.TOTAL_HAVE,0) - NVL(ENT.CUR_USED,0)
       WHERE ENT.YEAR = PV_YEAR
             AND ENT.MONTH = PV_MONTH
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID);
       ELSE
          UPDATE AT_ENTITLEMENT ENT
         SET ENT.CUR_HAVE = NVL(ENT.PREV_USED,0) + NVL(ENT.TOTAL_HAVE,0) - NVL(ENT.CUR_USED,0)
       WHERE ENT.YEAR = PV_YEAR
             AND ENT.MONTH = PV_MONTH
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID);
       END IF;
       EXCEPTION
      WHEN OTHERS THEN
        SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                                'PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT',
                                SQLERRM || '_' ||
                                DBMS_UTILITY.format_error_backtrace,
                                null,
                                null,
                                null,
                                null);
  
  
    END;
  */
  PROCEDURE UPDATE_DATAINOUT(P_ITIMEID  IN NUMBER,
                             P_USERNAME NVARCHAR2,
                             P_FROMDATE IN DATE,
                             P_ENDDATE  IN DATE) IS
    PV_FROM DATE;
  BEGIN
    PV_FROM := P_FROMDATE;
    DELETE AT_DATA_INOUT T
     WHERE T.WORKINGDAY >= PV_FROM
       AND T.WORKINGDAY <= P_ENDDATE
       AND (T.ITIME_ID = P_ITIMEID OR P_ITIMEID = 0);
  
    INSERT INTO AT_DATA_INOUT S
      (S.ID,
       S.EMPLOYEE_ID,
       S.WORKINGDAY,
       S.VALIN1,
       S.VALIN2,
       S.VALIN3,
       S.VALIN4,
       S.VALIN5,
       S.VALIN6,
       S.VALIN7,
       S.VALIN8,
       S.VALIN9,
       S.VALIN10,
       S.VALIN11,
       S.VALIN12,
       S.VALIN13,
       S.VALIN14,
       S.VALIN15,
       S.ITIME_ID,
       S.CREATED_DATE,
       S.CREATED_BY,
       S.CREATED_LOG)
      SELECT SEQ_AT_DATA_INOUT.NEXTVAL,
             A.EMPLOYEE_ID,
             A.WORKINGDAY,
             A.VAL1,
             A.VAL2,
             A.VAL3,
             A.VAL4,
             A.VAL5,
             A.VAL6,
             A.VAL7,
             A.VAL8,
             A.VAL9,
             A.VAL10,
             A.VAL11,
             A.VAL12,
             A.VAL13,
             A.VAL14,
             A.VAL15,
             A.ITIME_ID,
             SYSDATE,
             P_USERNAME,
             P_USERNAME
        FROM (SELECT *
                FROM (SELECT T.ITIME_ID,
                             E.ID EMPLOYEE_ID,
                             T.WORKINGDAY,
                             T.VALTIME,
                             ROW_NUMBER() OVER(PARTITION BY T.ITIME_ID, T.WORKINGDAY, E.ID ORDER BY T.ITIME_ID, T.VALTIME ASC) AS STT
                        FROM AT_SWIPE_DATA T
                       INNER JOIN HU_EMPLOYEE E
                          ON T.ITIME_ID = E.ITIME_ID
                       WHERE T.WORKINGDAY >= PV_FROM
                         AND T.WORKINGDAY <= P_ENDDATE
                         AND (T.ITIME_ID = P_ITIMEID OR P_ITIMEID = 0)
                         AND (NVL(E.WORK_STATUS, 0) <> 257 OR
                             (NVL(E.WORK_STATUS, 0) = 257 AND
                             E.TER_EFFECT_DATE >= PV_FROM AND
                             T.WORKINGDAY < E.TER_EFFECT_DATE))
                         AND T.WORKINGDAY >= E.JOIN_DATE) PIVOT(MAX(VALTIME) FOR STT IN(1 AS VAL1,
                                                                                        2 AS VAL2,
                                                                                        3 AS VAL3,
                                                                                        4 AS VAL4,
                                                                                        5 AS VAL5,
                                                                                        6 AS VAL6,
                                                                                        7 AS VAL7,
                                                                                        8 AS VAL8,
                                                                                        9 AS VAL9,
                                                                                        10 AS
                                                                                        VAL10,
                                                                                        11 AS
                                                                                        VAL11,
                                                                                        12 AS
                                                                                        VAL12,
                                                                                        13 AS
                                                                                        VAL13,
                                                                                        14 AS
                                                                                        VAL14,
                                                                                        15 AS
                                                                                        VAL15,
                                                                                        16 AS
                                                                                        VAL16,
                                                                                        17 AS
                                                                                        VAL17,
                                                                                        18 AS
                                                                                        VAL18,
                                                                                        19 AS
                                                                                        VAL19,
                                                                                        20 AS
                                                                                        VAL20))) A;
  END;

  PROCEDURE MANAGEMENT_TOTAL_COMPENSATORY(P_EMPLOYEE_ID IN NUMBER,
                                          P_DATE_TIME   IN DATE,
                                          P_OUT         OUT CURSOR_TYPE) IS
    PHEP_NGHI_NAM NUMBER;
    P_LIMIT_DAY   NVARCHAR2(100);
    P_LIMIT_YEAR  NVARCHAR2(100);
  BEGIN
    SELECT SUM(NVL(PR.NVALUE, 0))
      INTO PHEP_NGHI_NAM
      FROM AT_PORTAL_REG PR
     INNER JOIN AT_TIME_MANUAL M
        ON M.ID = PR.ID_SIGN
     WHERE PR.ID_EMPLOYEE = P_EMPLOYEE_ID
       AND TO_CHAR(PR.FROM_DATE, 'yyyy') = TO_CHAR(P_DATE_TIME, 'yyyy')
       AND PR.STATUS <> 3
       AND M.CODE = 'B'
       AND PR.SVALUE = 'LEAVE';
  
    SELECT M.LIMIT_DAY, M.LIMIT_YEAR
      INTO P_LIMIT_DAY, P_LIMIT_YEAR
      FROM AT_TIME_MANUAL M
     WHERE M.CODE = 'B';
  
    OPEN P_OUT FOR
      SELECT EE.ID AS EMPLOYEE_ID,
             EE.EMPLOYEE_CODE,
             EE.FULLNAME_VN,
             TO_CHAR(EE.JOIN_DATE, 'dd/MM/yyyy') JOIN_DATE,
             NVL(NB.PREV_HAVE, 0) AS NB_NAM_TRUOC,
             NVL(NB.TOTAL_HAVE, 0) AS TONG_NB,
             NVL(PHEP_NGHI_NAM, 0) AS DA_NB,
             (NVL(NB.TOTAL_HAVE, 0) - NVL(PHEP_NGHI_NAM, 0)) AS NB_CON_LAI,
             NVL(P_LIMIT_DAY, '') AS LIMIT_DAY,
             NVL(P_LIMIT_YEAR, '') AS LIMIT_YEAR
        FROM AT_COMPENSATORY NB
       INNER JOIN HU_EMPLOYEE EE
          ON NB.EMPLOYEE_ID = EE.ID
       WHERE NB.YEAR = TO_CHAR(P_DATE_TIME, 'yyyy')
         AND EE.ID = P_EMPLOYEE_ID;
  END;
  PROCEDURE MANAGEMENT_TOTAL_ENTITLEMENT(P_EMPLOYEE_ID   IN NUMBER,
                                         P_ID_PORTAL_REG IN NUMBER,
                                         P_DATE_TIME     IN DATE,
                                         P_OUT           OUT CURSOR_TYPE) IS
    PHEP_NGHI_NAM NUMBER;
    P_LIMIT_DAY   NVARCHAR2(100);
    P_LIMIT_YEAR  NVARCHAR2(100);
    PV_PHEP_APP   NUMBER;
  
  BEGIN
  
    SELECT SUM(CASE
                 WHEN F.ID = F2.ID AND F.CODE = 'P' THEN
                  1
                 WHEN (F.CODE = 'P' OR F2.CODE = 'P') AND F.ID <> F2.ID THEN
                  .5
                 ELSE
                  0
               END)
      INTO PV_PHEP_APP
      from At_Leavesheet_Detail A
      LEFT JOIN AT_TIME_MANUAL M
        ON A.MANUAL_ID = M.ID
      LEFT JOIN AT_FML F
        ON M.MORNING_ID = F.ID
      LEFT JOIN AT_FML F2
        ON M.AFTERNOON_ID = F2.ID
     WHERE A.EMPLOYEE_ID = P_EMPLOYEE_ID
       AND TO_CHAR(A.Leave_Day, 'yyyy') = TO_CHAR(P_DATE_TIME, 'yyyy')
       and M.CODE like '%P%';
  
    SELECT SUM(NVL(PR.NVALUE, 0))
      INTO PHEP_NGHI_NAM
      FROM AT_PORTAL_REG PR
     INNER JOIN AT_TIME_MANUAL M
        ON M.ID = PR.ID_SIGN
     WHERE PR.ID_EMPLOYEE = P_EMPLOYEE_ID
       AND TO_CHAR(PR.FROM_DATE, 'yyyy') = TO_CHAR(P_DATE_TIME, 'yyyy')
       AND PR.STATUS <> 2
       and pr.status <> 1
          --AND PR.STATUS <> 0
       AND (M.MORNING_ID = 251 OR M.AFTERNOON_ID = 251)
       AND PR.SVALUE = 'LEAVE'
       AND M.CODE like '%P%'
       AND (PR.Id_Reggroup <> P_ID_PORTAL_REG OR P_ID_PORTAL_REG = 0);
  
    SELECT M.LIMIT_DAY, M.LIMIT_YEAR
      INTO P_LIMIT_DAY, P_LIMIT_YEAR
      FROM AT_TIME_MANUAL M
     WHERE M.CODE = 'P';
  
    OPEN P_OUT FOR
      SELECT *
        FROM (SELECT EE.ID AS EMPLOYEE_ID,
                     EE.EMPLOYEE_CODE,
                     EE.FULLNAME_VN,
                     TO_CHAR(EE.JOIN_DATE, 'dd/MM/yyyy') JOIN_DATE,
                     EN.CAL_DATE AS NGAY_TINH_PHEP,
                     EN.EXPIREDATE NGAY_HET_HAN_TRUOC,
                     NVL(EN.BALANCE_WORKING_TIME, 0) AS PHEP_THAM_NIEN,
                     NVL(EN.PREV_HAVE, 0) AS PHEP_NAM_TRUOC,
                     NVL(EN.TOTAL_HAVE, 0) AS PHEP_TRONG_NAM,
                     NVL(PHEP_NGHI_NAM, 0) + NVL(PV_PHEP_APP, 0) AS PHEP_DA_NGHI,
                     /*  (NVL(EN.CUR_HAVE, 0) - NVL(PHEP_NGHI_NAM, 0) -
                     NVL(PV_PHEP_APP, 0)) AS PHEP_CON_LAI,*/
                     NVL(TOTAL_HAVE1, 0) + NVL(EN.SENIORITYHAVE, 0) +
                     NVL(EN.PREVTOTAL_HAVE, 0) - NVL(PHEP_NGHI_NAM, 0) -
                     NVL(PV_PHEP_APP, 0) - NVL(EN.TIME_OUTSIDE_COMPANY, 0) AS PHEP_CON_LAI,
                     NVL(P_LIMIT_DAY, '') AS LIMIT_DAY,
                     NVL(P_LIMIT_YEAR, '') AS LIMIT_YEAR,
                     NVL(EN.PREV_HAVE, 0) PREV_HAVE,
                     NVL(EN.PREV_USED, 0) PREV_USED,
                     NVL(EN.PREVTOTAL_HAVE, 0) PREVTOTAL_HAVE,
                     NVL(EN.SENIORITYHAVE, 0) SENIORITYHAVE,
                     NVL(TOTAL_HAVE1, 0) TOTAL_HAVE1,
                     NVL(EN.TIME_OUTSIDE_COMPANY, 0) TIME_OUTSIDE_COMPANY
                FROM AT_ENTITLEMENT EN
               INNER JOIN HU_EMPLOYEE EE
                  ON EN.EMPLOYEE_ID = EE.ID
               WHERE EN.YEAR = TO_CHAR(P_DATE_TIME, 'yyyy')
                 AND EN.MONTH <= TO_CHAR(P_DATE_TIME, 'MM')
                 AND EE.ID = P_EMPLOYEE_ID
               ORDER BY EN.YEAR, EN.MONTH DESC)
       WHERE ROWNUM = 1;
  END;

  /*PROCEDURE CAL_TIME_TIMESHEET_ALL(P_USERNAME   IN NVARCHAR2,
                                     P_ORG_ID     IN NUMBER,
                                     P_PERIOD_ID  IN NUMBER,
                                     P_ISDISSOLVE IN NUMBER,
                                     P_DELETE_ALL IN NUMBER:= 0) IS
      PV_FROMDATE    DATE;
      PV_ENDDATE     DATE;
      PV_SQL         CLOB;
      PV_REQUEST_ID  NUMBER;
      PV_MINUS_ALLOW NUMBER := 50;
      PV_SUNDAY      DATE; --Lay ngay chu nhat trong thang
      PV_TEST1 NUMBER;
      PV_TEST2 NUMBER;
      PV_CHECK NUMBER;
      PV_DEL_ALL NUMBER;
      PV_CHECKNV NUMBER;
    BEGIN
      PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
      PV_DEL_ALL := P_DELETE_ALL;
  
      SELECT P.START_DATE, P.END_DATE
        INTO PV_FROMDATE, PV_ENDDATE
        FROM AT_PERIOD P
       WHERE P.ID = P_PERIOD_ID;
  
      PV_SUNDAY := PV_FROMDATE + FN_GET_CN_START(PV_FROMDATE);
      -- Insert org can tinh toan
      INSERT INTO AT_CHOSEN_ORG E
        (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
           FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                      P_ORG_ID,
                                      P_ISDISSOLVE)) O
          WHERE EXISTS (SELECT 1
                   FROM AT_ORG_PERIOD OP
                  WHERE OP.PERIOD_ID = P_PERIOD_ID
                    AND OP.ORG_ID = O.ORG_ID
                    AND OP.STATUSCOLEX = 1));
  
        INSERT INTO AT_CHOSEN_EMP_CLEAR --==NHUNG NV DO HE THONG TU DONG TINH
        (EMPLOYEE_ID)
        SELECT DISTINCT S.EMPLOYEE_ID
          FROM AT_TIME_TIMESHEET_DAILY S
         INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
            ON O.ORG_ID = S.ORG_ID
         WHERE S.CREATED_BY = 'AUTO'
           AND S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
  
         SELECT COUNT(1)
            INTO PV_CHECKNV
         FROM AT_TIME_TIMESHEET_DAILY S
         INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
            ON O.ORG_ID = S.ORG_ID
         WHERE  S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
  
         SELECT COUNT(*)
         INTO PV_CHECK
         FROM AT_CHOSEN_EMP_CLEAR ;
         IF PV_CHECK = 0 AND PV_CHECKNV =0 THEN --==Neu tinh lan dau se k co CREATED_BY = 'AUTO' se tinh all
           PV_DEL_ALL := 1;
         END IF;
  
      IF PV_DEL_ALL = 1 THEN --==TINH LAI TAT CA CA NV
        -- insert emp can tinh toan
      INSERT INTO AT_CHOSEN_EMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE,
         DECISION_ID)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE,
                W.ID DECISION_ID
           FROM HU_EMPLOYEE T
          INNER JOIN (SELECT E.ID,
                            E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
             ON O.ORG_ID = W.ORG_ID
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
      ELSE --==CHI TINH NV AUTO
        -- insert emp can tinh toan
      INSERT INTO AT_CHOSEN_EMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE,
         DECISION_ID)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE,
                W.ID DECISION_ID
           FROM HU_EMPLOYEE T
          INNER JOIN AT_CHOSEN_EMP_CLEAR C --==CHI TINH NV AUTO
             ON T.ID = C.EMPLOYEE_ID
          INNER JOIN (SELECT E.ID,
                            E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
         \* INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
             ON O.ORG_ID = W.ORG_ID*\
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
      END IF;
  
  
  
                SELECT COUNT(1) INTO PV_TEST1 FROM AT_CHOSEN_ORG T;
                SELECT COUNT(1) INTO PV_TEST2 FROM AT_CHOSEN_EMP;
         INSERT INTO TEMP1
           (TMP, WCODE, EXEDATE, TYPE)
         VALUES
           ('INSERT INTO AT_CHOSEN_EMP' || PV_TEST1 || '_' || PV_TEST2,
            '1',
            SYSDATE,
            400);
  
      --Xu ly lay ca mac dinh truoc
      -- XOA DU LIEU DA TON TAI
      DELETE FROM AT_WORKSIGN T1
       WHERE EXISTS
       (SELECT 1
                FROM (SELECT T.EMPLOYEE_ID,
                             CASE
                               WHEN T.EFFECT_DATE_FROM > PV_FROMDATE THEN
                                T.EFFECT_DATE_FROM
                               ELSE
                                PV_FROMDATE
                             END START_DELETE,
                             CASE
                               WHEN T.EFFECT_DATE_TO < PV_ENDDATE THEN
                                T.EFFECT_DATE_TO
                               ELSE
                                PV_ENDDATE
                             END END_DELETE
                        FROM AT_SIGNDEFAULT T
                       INNER JOIN AT_CHOSEN_EMP EE
                          ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
                       INNER JOIN AT_SHIFT S
                          ON T.SINGDEFAULE = S.ID
                       WHERE S.ACTFLG = 'A'
                         AND T.EFFECT_DATE_FROM <= PV_ENDDATE
                         AND NVL(T.EFFECT_DATE_TO, PV_FROMDATE) >= PV_FROMDATE) E
               WHERE T1.EMPLOYEE_ID = E.EMPLOYEE_ID
                 AND T1.WORKINGDAY >= E.START_DELETE
                 AND T1.WORKINGDAY <= E.END_DELETE);
      --INSERT CA MAC DINH
      INSERT INTO AT_WORKSIGN T
        (T.ID,
         T.EMPLOYEE_ID,
         T.WORKINGDAY,
         T.SHIFT_ID,
         T.PERIOD_ID,
         T.CREATED_DATE,
         T.CREATED_BY)
        SELECT SEQ_AT_WORKSIGN.NEXTVAL,
               EE.EMPLOYEE_ID,
               C.CDATE,
               EE.SHIFT_ID SHIFT_ID,
               P_PERIOD_ID,
               SYSDATE,
               UPPER(P_USERNAME)
          FROM (SELECT T.EMPLOYEE_ID,
                       CASE
                         WHEN T.EFFECT_DATE_FROM > PV_FROMDATE THEN
                          T.EFFECT_DATE_FROM
                         ELSE
                          PV_FROMDATE
                       END START_DELETE,
                       CASE
                         WHEN T.EFFECT_DATE_TO < PV_ENDDATE THEN
                          T.EFFECT_DATE_TO
                         ELSE
                          PV_ENDDATE
                       END END_DELETE,
                       S.ID SHIFT_ID
                  FROM AT_SIGNDEFAULT T
                 INNER JOIN AT_CHOSEN_EMP EE
                    ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
                 INNER JOIN AT_SHIFT S
                    ON T.SINGDEFAULE = S.ID
                 WHERE T.ACTFLG = 'A'
                   AND S.ACTFLG = 'A'
                   AND T.EFFECT_DATE_FROM <= PV_ENDDATE
                   AND NVL(T.EFFECT_DATE_TO, PV_FROMDATE) >= PV_FROMDATE) EE
         CROSS JOIN TABLE(TABLE_LISTDATE(PV_FROMDATE, PV_ENDDATE)) C
         WHERE TO_CHAR(C.CDATE, 'DY') <> 'SUN'
           AND NOT EXISTS
         (SELECT H.ID FROM AT_HOLIDAY H WHERE H.WORKINGDAY = C.CDATE);
      -- Phan theo ca lam viec:
      -- 1.Co DK ca
  
      -- 2: Kh?ng DK ca
      -- TH1: So lan cham cong la so chan
      INSERT INTO AT_CAL_INOUT_TEMP
        (ID,
         EMPLOYEE_ID,
         WORKINGDAY,
         \*SHIFT_ID,
         SHIFT_CODE,
         SHIFT_MANUAL_ID,
         SHIFT_HOURS_START,
         SHIFT_HOURS_STOP,*\
         VALTIME1,
         VALTIME2,
         REQUEST_ID,
         ITIME_ID)
    SELECT SEQ_AT_CAL_INOUT_TEMP.NEXTVAL,
           T.EMPLOYEE_ID,
           INOUT.WORKINGDAY,
           \* WSIGN.SHIFT_ID,
           WSIGN.SHIFT_CODE,
           WSIGN.SHIFT_MANUAL_ID,
           WSIGN.HOURS_START,
           WSIGN.HOURS_STOP,*\
           INOUT.VAL_IN,
           INOUT.VAL_OUT,
           PV_REQUEST_ID,
           T.ITIME_ID
      FROM AT_CHOSEN_EMP T
      INNER JOIN (SELECT T.ITIME_ID,
                        T.WORKINGDAY,
                        MIN(T.VALTIME) VAL_IN,
                        MAX(T.VALTIME) VAL_OUT
                   from AT_SWIPE_DATA T
                  WHERE T.WORKINGDAY >= PV_FROMDATE
                    AND T.WORKINGDAY <= PV_ENDDATE
                  GROUP BY T.ITIME_ID, T.WORKINGDAY
                  HAVING COUNT(1) > 1 ---Khi co 2 check tro len
                  ) INOUT
        ON T.ITIME_ID = INOUT.ITIME_ID
        WHERE INOUT.WORKINGDAY IS NOT NULL;
  
      --Update lai ca lam viec neu DK ca mac dinh
      UPDATE AT_CAL_INOUT_TEMP T
     SET (T.SHIFT_ID, T.SHIFT_CODE) =
         (SELECT WS.SHIFT_ID, SH.CODE
            FROM AT_WORKSIGN WS
           INNER JOIN AT_SHIFT SH
              ON WS.SHIFT_ID = SH.ID
           WHERE WS.EMPLOYEE_ID = T.EMPLOYEE_ID
             AND WS.WORKINGDAY = T.WORKINGDAY);
  
     \* UPDATE AT_CAL_INOUT_TEMP T
         SET T.SHIFT_ID = 4, T.SHIFT_CODE = 'HC'
       WHERE EXISTS (SELECT WS.ID
                FROM AT_WORKSIGN WS
               WHERE WS.EMPLOYEE_ID = T.EMPLOYEE_ID
                 AND WS.WORKINGDAY = T.WORKINGDAY
                 AND WS.SHIFT_ID = 4);*\
      --Phan loai lam binh thuong va OT
      --column IS_OT: null: binh thuong, '1': OT
      UPDATE AT_CAL_INOUT_TEMP T
         SET T.IS_OT = '1'
       WHERE EXISTS (select OT.ID
                from at_register_ot OT
               where OT.TYPE_INPUT = -1
                 AND OT.EMPLOYEE_ID = T.EMPLOYEE_ID
                 AND OT.WORKINGDAY = T.WORKINGDAY
                 AND FN_SET_SHIFT(extract(hour from cast(OT.From_Hour as
                                               timestamp))) =
                     DECODE(T.SHIFT_ID, 4, 1, T.SHIFT_ID))
         AND NOT EXISTS
       (SELECT K.ID
                FROM AT_REGISTER_OT K
               WHERE K.TYPE_INPUT = 0
                 AND K.EMPLOYEE_ID = T.EMPLOYEE_ID
                 AND k.workingday BETWEEN PV_FROMDATE AND PV_ENDDATE)
         AND (SELECT COUNT(AT.ID)
                FROM AT_CAL_INOUT_TEMP AT
               WHERE AT.EMPLOYEE_ID = T.EMPLOYEE_ID
                 AND AT.WORKINGDAY = T.WORKINGDAY) > 1;
      --Ghi nhan OT cho ngay CN & Le
      UPDATE AT_CAL_INOUT_TEMP T
         SET T.IS_OT = '1'
       WHERE NOT EXISTS (select W.ID
                from AT_WORKSIGN W
               where W.EMPLOYEE_ID = T.EMPLOYEE_ID
                 AND W.WORKINGDAY = T.WORKINGDAY)
         AND NOT EXISTS
       (SELECT K.ID
                FROM AT_REGISTER_OT K
               WHERE K.TYPE_INPUT = 0
                 AND K.EMPLOYEE_ID = T.EMPLOYEE_ID
                 AND k.workingday BETWEEN PV_FROMDATE AND PV_ENDDATE);
      --###: Xu ly cong lam binh thuong
  
      --Insert vao table AT_TIME_TIMESHEET_MACHINE_TEMP
       INSERT INTO AT_TIME_TIMESHEET_MACHINE_TEMP M
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         WORKINGDAY,
         SHIFT_ID,
         SHIFT_CODE,
         SHIFT_MANUAL_ID,
         SHIFT_HOURS_START,
         SHIFT_HOURS_STOP,
         WORKINGHOUR,
         VALIN1,
         VALIN2,
         VALIN3,
         VALIN4,
         CREATED_DATE,
         CREATED_BY,
         REQUEST_ID)
        SELECT SEQ_AT_TIME_MACHINE_TEMP.NEXTVAL,
               T.EMPLOYEE_ID,
               T.ORG_ID,
               T.TITLE_ID,
               T.STAFF_RANK_ID,
               WSIGN.WORKINGDAY,
               WSIGN.SHIFT_ID,
               WSIGN.SHIFT_CODE,
               WSIGN.SHIFT_MANUAL_ID,
               CASE
                  WHEN ATS.HOURS_START IS NOT NULL THEN
                   TO_DATE(TO_CHAR(WSIGN.WORKINGDAY, 'yyyymmdd') ||
                           TO_CHAR(ATS.HOURS_START, 'HH24MI'),
                           'yyyymmddHH24MI')
                END SHIFT_HOURS_START,
                CASE
                  WHEN ATS.HOURS_STOP IS NOT NULL THEN
                   TO_DATE(TO_CHAR(WSIGN.WORKINGDAY, 'yyyymmdd') ||
                           TO_CHAR(ATS.HOURS_STOP, 'HH24MI'),
                           'yyyymmddHH24MI')
                END SHIFT_HOURS_STOP,
               CASE
                 WHEN WSIGN.SHIFT_HOURS_START >= WSIGN.VALTIME1 THEN
                  ROUND((WSIGN.VALTIME2 - WSIGN.SHIFT_HOURS_START) * 24, 2)
                 ELSE
                  ROUND((WSIGN.VALTIME2 - WSIGN.VALTIME1) * 24, 2)
               END AS WORKINGHOUR,
               WSIGN.VALTIME1,
               WSIGN.VALTIME2,
               WSIGN.VALTIME3,
               WSIGN.VALTIME4,
               SYSDATE,
               UPPER(P_USERNAME),
               PV_REQUEST_ID
          FROM AT_CHOSEN_EMP T
         INNER JOIN (SELECT *
                       FROM AT_CAL_INOUT_TEMP WSIGN
                      WHERE WSIGN.IS_OT IS NULL) WSIGN
            ON T.EMPLOYEE_ID = WSIGN.EMPLOYEE_ID
         INNER JOIN AT_SHIFT ATS
            ON WSIGN.SHIFT_ID = ATS.ID;
  
      --Tinh lai so gio
      UPDATE AT_TIME_TIMESHEET_MACHINE_TEMP T
         SET T.WORKINGHOUR = (TRUNC(T.WORKINGHOUR)) + CASE
                               WHEN MOD(T.WORKINGHOUR, 1) >= 0.67 THEN
                                1
                               WHEN MOD(T.WORKINGHOUR, 1) >= 0.25 THEN
                                0.5
                               ELSE
                                0
                             END;
      UPDATE AT_TIME_TIMESHEET_MACHINE_TEMP T
         SET T.LATE = ROUND(CASE
                 WHEN NVL(IS_NOON, 0) = -1 THEN -- ca c? qu?t trua
                  CASE
                    WHEN VALIN1 > SHIFT_HOURS_START + NVL(MINUTE_DM,0) /60/24 THEN
                     (VALIN1 - (SHIFT_HOURS_START + NVL(MINUTE_DM,0) /60/24)) * 24
                    ELSE
                     0
                  END + CASE
                    WHEN VALIN3 > BREAKS_TO THEN
                     (VALIN3 - BREAKS_TO) * 24
                    ELSE
                     0
                  END
                 WHEN NVL(IS_NOON, 0) = 0 THEN -- ca kh?ng qu?t trua
                  CASE
                    WHEN VALIN1 > SHIFT_HOURS_START + NVL(MINUTE_DM,0) /60/24 THEN
                    (VALIN1 - (SHIFT_HOURS_START + NVL(MINUTE_DM,0) /60/24)) * 24
                    ELSE
                     0
                  END
                 ELSE
                  0
               END * 60,
               2),
               T.COMEBACKOUT = ROUND(CASE
                 WHEN NVL(IS_NOON, 0) = -1 THEN -- ca c? qu?t trua
                  CASE
                    WHEN VALIN2 < SHIFT_HOURS_STOP - NVL(MINUTE_VS, 0) / 60 / 24 THEN
                     (SHIFT_HOURS_STOP - NVL(MINUTE_VS, 0) / 60 / 24 - VALIN2) * 24
                    ELSE
                     0
                  END + CASE
                    WHEN VALIN2 < BREAKS_FORM THEN
                     (BREAKS_FORM - VALIN2) * 24
                    ELSE
                     0
                  END
                 WHEN NVL(IS_NOON, 0) = 0 THEN -- ca kh?ng qu?t trua
                  CASE
                    WHEN SHIFT_HOURS_STOP < SHIFT_HOURS_START THEN
                     CASE
                       WHEN VALIN2 < SHIFT_HOURS_STOP + 1 - NVL(MINUTE_VS, 0) / 60 / 24 THEN
                        (SHIFT_HOURS_STOP + 1 - NVL(MINUTE_VS, 0) / 60 / 24 - VALIN2) * 24
                       ELSE
                        0
                     END
                    ELSE
                     CASE
                       WHEN VALIN2 < SHIFT_HOURS_STOP - NVL(MINUTE_VS, 0) / 60 / 24 THEN
                        (SHIFT_HOURS_STOP - NVL(MINUTE_VS, 0) / 60 / 24 - VALIN2) * 24
                       ELSE
                        0
                     END
                  END
                 ELSE
                  0
               END * 60,
               2)
               ;
  
     INSERT INTO TEMP1
          (TMP, WCODE, EXEDATE, TYPE)
        VALUES
          ('INSERT INTO AT_CHOSEN_EMP', '9', SYSDATE, 400);
        --COMMIT;
      --Lay tang ca cho nhung dong qua thoi gian lam cua ca HC
      INSERT INTO AT_CAL_INOUT_TEMP
        (ID,
         EMPLOYEE_ID,
         WORKINGDAY,
         VALTIME1,
         VALTIME2,
         REQUEST_ID,
         ITIME_ID,
         IS_OT)
        SELECT SEQ_AT_CAL_INOUT_TEMP.NEXTVAL,
               T.EMPLOYEE_ID,
               T.WORKINGDAY,
               CASE
                 WHEN OT.FROM_HOUR < T.SHIFT_HOURS_STOP THEN
                  T.VALIN1
                 ELSE
                  T.SHIFT_HOURS_STOP
               END,
               CASE
                 WHEN OT.FROM_HOUR < T.SHIFT_HOURS_STOP THEN
                  OT.TO_HOUR
                 ELSE
                  T.VALIN2
               END,
               PV_REQUEST_ID,
               EMP.ITIME_ID,
               '1' IS_OT
          FROM AT_REGISTER_OT OT
         INNER JOIN AT_TIME_TIMESHEET_MACHINE_TEMP T
            ON OT.EMPLOYEE_ID = T.EMPLOYEE_ID
           AND OT.WORKINGDAY = T.WORKINGDAY
         INNER JOIN AT_CHOSEN_EMP EMP
            ON T.EMPLOYEE_ID = EMP.EMPLOYEE_ID
         WHERE T.WORKINGHOUR > 8
           AND T.SHIFT_ID = 4
           AND OT.Type_Input = -1
           AND NOT EXISTS
         (SELECT K.ID
                  FROM AT_REGISTER_OT K
                 WHERE K.TYPE_INPUT = 0
                   AND K.EMPLOYEE_ID = T.EMPLOYEE_ID
                   AND k.workingday BETWEEN PV_FROMDATE AND PV_ENDDATE);
  
      -- XOA DU LIEU CU TRUOC KHI TINH
      DELETE FROM AT_TIME_TIMESHEET_MACHINET D
       WHERE D.WORKINGDAY >= PV_FROMDATE
         AND D.WORKINGDAY <= PV_ENDDATE
         AND D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O);
  
      INSERT INTO AT_TIME_TIMESHEET_MACHINET
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         WORKINGDAY,
         SHIFT_CODE,
         LEAVE_CODE,
         LATE,
         COMEBACKOUT,
         WORKDAY_OT,
         WORKDAY_NIGHT,
         TYPE_DAY,
         CREATED_DATE,
         CREATED_BY,
         CREATED_LOG,
         MODIFIED_DATE,
         MODIFIED_BY,
         MODIFIED_LOG,
         WORKINGHOUR,
         VALIN1,
         VALIN2,
         VALIN3,
         VALIN4,
         SHIFT_ID,
         MANUAL_ID,
         LEAVE_ID,
         WORKINGHOUR_SHIFT,
         NUMBER_SWIPE,
         IS_HOLIDAY,
         IS_FULLDAY,
         SHIFT_MANUAL_ID,
         IS_NOON,
         SHIFT_HOURS_START,
         SHIFT_HOURS_STOP,
         BREAKS_FORM,
         BREAKS_TO,
         MINUTE_DM,
         MINUTE_VS)
        SELECT SEQ_AT_TIME_TIMESHEET_MACHINET.NEXTVAL,
               EMPLOYEE_ID,
               ORG_ID,
               TITLE_ID,
               STAFF_RANK_ID,
               WORKINGDAY,
               SHIFT_CODE,
               LEAVE_CODE,
               LATE,
               COMEBACKOUT,
               WORKDAY_OT,
               WORKDAY_NIGHT,
               TYPE_DAY,
               SYSDATE,
               UPPER(P_USERNAME),
               UPPER(P_USERNAME),
               SYSDATE,
               UPPER(P_USERNAME),
               UPPER(P_USERNAME),
               WORKINGHOUR,
               VALIN1,
               VALIN2,
               VALIN3,
               VALIN4,
               AW.SHIFT_ID, --
               --MANUAL_ID,
               Case
                 When AW.MANUAL_ID = 0 then
                  (Case
                    When AW.WORKINGHOUR >= ATS.MINHOURS then
                     ATS.MANUAL_ID
                    else
                     AW.MANUAL_ID
                  end)
                 else
                  AW.MANUAL_ID
               end MANUAL_ID, --HongDX S?a SpinDEX 08/09/2017
               LEAVE_ID,
               AW.WORKINGHOUR_SHIFT,
               AW.NUMBER_SWIPE,
               AW.IS_HOLIDAY,
               AW.IS_FULLDAY,
               AW.SHIFT_MANUAL_ID, --
               AW.IS_NOON,
               AW.SHIFT_HOURS_START,
               AW.SHIFT_HOURS_STOP,
               AW.BREAKS_FORM,
               AW.BREAKS_TO,
               AW.MINUTE_DM,
               AW.MINUTE_VS
          FROM AT_TIME_TIMESHEET_MACHINE_TEMP AW
         INNER JOIN AT_SHIFT ATS
            ON AW.SHIFT_ID = ATS.ID; --HongDX S?a SpinDEX 08/09/2017;
     --IF P_DELETE_ALL  = 0 THEN
       DELETE FROM AT_TIME_TIMESHEET_DAILY D
       WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
         AND D.WORKINGDAY >= PV_FROMDATE
         AND D.WORKINGDAY <= PV_ENDDATE;
    \* ELSE --xu ly trong AT_CHOSEN_EMP
       DELETE FROM AT_TIME_TIMESHEET_DAILY D
       WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
         AND D.WORKINGDAY >= PV_FROMDATE
         AND D.WORKINGDAY <= PV_ENDDATE
         AND D.CREATED_BY ='AUTO';
     END IF;*\
  
  
      \*DELETE FROM AT_TIME_TIMESHEET_ORIGIN D
       WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
         AND D.WORKINGDAY >= PV_FROMDATE
         AND D.WORKINGDAY <= PV_ENDDATE;*\
  
    INSERT INTO AT_TIME_TIMESHEET_DAILY T
        (T.ID,
         T.EMPLOYEE_ID,
         T.ORG_ID,
         T.TITLE_ID,
         T.WORKINGDAY,
         T.SHIFT_CODE,
         T.WORKINGHOUR,
         --T.WORKINGHOUR_SHIFT,
         --T.NUMBER_SWIPE,
         T.SHIFT_ID,
         T.LEAVE_CODE,
         T.MANUAL_ID,
         T.LEAVE_ID,
         T.LATE,
         T.COMEBACKOUT,
         T.VALIN1,
         T.VALIN2,
         T.VALIN3,
         T.VALIN4,
         T.CREATED_DATE,
         T.CREATED_BY,
         T.CREATED_LOG,
         T.MODIFIED_DATE,
         T.MODIFIED_BY,
         T.MODIFIED_LOG)
        SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
               W.EMPLOYEE_ID,
               W.ORG_ID,
               W.TITLE_ID,
               W.WORKINGDAY,
               W.SHIFT_CODE,
               W.WORKINGHOUR,
               --W.WORKINGHOUR_SHIFT,
               --W.NUMBER_SWIPE,
               W.SHIFT_ID,
               W.LEAVE_CODE,
               W.MANUAL_ID,
               W.LEAVE_ID,
               W.LATE,
               W.COMEBACKOUT,
               W.VALIN1,
               W.VALIN2,
               W.VALIN3,
               W.VALIN4,
               SYSDATE,
               'AUTO',
               UPPER(P_USERNAME),
               SYSDATE,
               UPPER(P_USERNAME),
               UPPER(P_USERNAME)
          FROM (SELECT W.EMPLOYEE_ID,
                       W.ORG_ID,
                       W.TITLE_ID,
                       W.WORKINGDAY,
                       W.SHIFT_CODE,
                       8 WORKINGHOUR,
                       W.SHIFT_ID,
                       W.LEAVE_CODE,
                       25 MANUAL_ID,
                       W.LEAVE_ID,
                       W.LATE,
                       W.COMEBACKOUT,
                       W.VALIN1,
                       W.VALIN2,
                       W.VALIN3,
                       W.VALIN4
                  FROM AT_TIME_TIMESHEET_MACHINE_TEMP W
                 \*GROUP BY W.EMPLOYEE_ID, W.WORKINGDAY, W.ORG_ID, W.TITLE_ID*\) W;
  
      UPDATE AT_TIME_TIMESHEET_DAILY D
         SET (WORKINGHOUR, MANUAL_ID) =
             (SELECT SUM(W.WORKINGHOUR),
                     CASE
                       WHEN SUM(W.WORKINGHOUR) >= 6 THEN
                        25 --anh xa theo id danh muc KHCong
                       WHEN SUM(W.WORKINGHOUR) >= 4 THEN
                        26 --lamviec nua ngay
                       ELSE
                        NULL
                     END
                FROM AT_TIME_TIMESHEET_MACHINE_TEMP W
               WHERE D.EMPLOYEE_ID = W.EMPLOYEE_ID
               AND D.WORKINGDAY = W.WORKINGDAY
               AND W.SHIFT_ID IS NOT NULL --==UPDATE KIEU CONG CHO NV C? CA
               GROUP BY W.EMPLOYEE_ID, W.WORKINGDAY, W.ORG_ID, W.TITLE_ID)
       WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
         AND D.WORKINGDAY >= PV_FROMDATE
         AND D.WORKINGDAY <= PV_ENDDATE;
  
     --Insert ky hieu cho ngay dang ky nghi
      UPDATE AT_TIME_TIMESHEET_DAILY T
         SET T.MANUAL_ID =
             (SELECT ATL.MANUAL_ID
                FROM AT_LEAVESHEET ATL
               WHERE T.WORKINGDAY = ATL.WORKINGDAY
                 AND T.EMPLOYEE_ID = ATL.EMPLOYEE_ID)
       WHERE T.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
         AND EXISTS (SELECT ID
                FROM AT_LEAVESHEET ATL
               WHERE T.WORKINGDAY = ATL.WORKINGDAY
                 AND T.EMPLOYEE_ID = ATL.EMPLOYEE_ID);
  
      --Cham cong chi tiet hang ngay
      INSERT INTO AT_TIME_TIMESHEET_DAILY T
        (T.ID,
         T.EMPLOYEE_ID,
         T.ORG_ID,
         T.TITLE_ID,
         T.WORKINGDAY,
         T.MANUAL_ID,
         T.CREATED_DATE,
         T.CREATED_BY,
         T.CREATED_LOG,
         T.MODIFIED_DATE,
         T.MODIFIED_BY,
         T.MODIFIED_LOG)
        SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
               EMP.EMPLOYEE_ID,
               EMP.ORG_ID,
               EMP.TITLE_ID,
               H.WORKINGDAY,
               H.MANUAL_ID,
               SYSDATE,
               'AUTO',
               UPPER(P_USERNAME),
               SYSDATE,
               UPPER(P_USERNAME),
               UPPER(P_USERNAME)
          FROM AT_CHOSEN_EMP EMP
         INNER JOIN (SELECT ATL.EMPLOYEE_ID, ATL.WORKINGDAY, ATL.MANUAL_ID
                       FROM AT_LEAVESHEET ATL
                      WHERE ATL.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE) H
            ON H.EMPLOYEE_ID = EMP.EMPLOYEE_ID
         WHERE NOT EXISTS (SELECT D.ID
                  FROM AT_TIME_TIMESHEET_DAILY D
                 WHERE D.EMPLOYEE_ID = EMP.EMPLOYEE_ID
                   AND D.WORKINGDAY = H.WORKINGDAY);
  
      --Insert them k? hi?u cho ngay nghi CN
      WHILE PV_SUNDAY <= PV_ENDDATE LOOP
        --Uu tien lay ky hieu OFF
        UPDATE AT_TIME_TIMESHEET_DAILY
           SET MANUAL_ID = 24
         WHERE WORKINGDAY = PV_SUNDAY;
  
        --Cham cong chi tiet hang ngay
        INSERT INTO AT_TIME_TIMESHEET_DAILY T
          (T.ID,
           T.EMPLOYEE_ID,
           T.ORG_ID,
           T.TITLE_ID,
           T.WORKINGDAY,
           T.MANUAL_ID,
           T.CREATED_DATE,
           T.CREATED_BY,
           T.CREATED_LOG,
           T.MODIFIED_DATE,
           T.MODIFIED_BY,
           T.MODIFIED_LOG)
          SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
                 EMP.EMPLOYEE_ID,
                 EMP.ORG_ID,
                 EMP.TITLE_ID,
                 PV_SUNDAY,
                 24,--==OFF
                 SYSDATE,
                 'AUTO',
                 UPPER(P_USERNAME),
                 SYSDATE,
                 UPPER(P_USERNAME),
                 UPPER(P_USERNAME)
            FROM AT_CHOSEN_EMP EMP
           WHERE NOT EXISTS (SELECT D.ID
                    FROM AT_TIME_TIMESHEET_DAILY D
                   WHERE D.EMPLOYEE_ID = EMP.EMPLOYEE_ID
                     AND D.WORKINGDAY = PV_SUNDAY);
  
        PV_SUNDAY := PV_SUNDAY + 7;
      END LOOP;
     INSERT INTO TEMP1
          (TMP, WCODE, EXEDATE, TYPE)
        VALUES
          ('INSERT INTO AT_CHOSEN_EMP', '12', SYSDATE, 400);
        --COMMIT;
      --Insert ky hieu cho ngay nghi Le
      --Uu tien lay ky hieu L
      UPDATE AT_TIME_TIMESHEET_DAILY T
         SET T.MANUAL_ID = 23--==L
       WHERE T.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
         AND EXISTS (SELECT H.ID
                FROM AT_HOLIDAY H
               WHERE H.ACTFLG = 'A'
                 AND H.WORKINGDAY = T.WORKINGDAY);
  
      --Cham cong chi tiet hang ngay
      INSERT INTO AT_TIME_TIMESHEET_DAILY T
        (T.ID,
         T.EMPLOYEE_ID,
         T.ORG_ID,
         T.TITLE_ID,
         T.WORKINGDAY,
         T.MANUAL_ID,
         T.CREATED_DATE,
         T.CREATED_BY,
         T.CREATED_LOG,
         T.MODIFIED_DATE,
         T.MODIFIED_BY,
         T.MODIFIED_LOG)
        SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
               EMP.EMPLOYEE_ID,
               EMP.ORG_ID,
               EMP.TITLE_ID,
               H.WORKINGDAY,
               23,--==L
               SYSDATE,
               'AUTO',
               UPPER(P_USERNAME),
               SYSDATE,
               UPPER(P_USERNAME),
               UPPER(P_USERNAME)
          FROM AT_CHOSEN_EMP EMP,
               (SELECT DISTINCT H.WORKINGDAY
                  FROM AT_HOLIDAY H
                 WHERE H.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE) H
         WHERE NOT EXISTS (SELECT D.ID
                  FROM AT_TIME_TIMESHEET_DAILY D
                 WHERE D.EMPLOYEE_ID = EMP.EMPLOYEE_ID
                   AND D.WORKINGDAY = H.WORKINGDAY);
  
  
        UPDATE AT_TIME_TIMESHEET_DAILY D
           SET D.WORKING_MEAL =
               (SELECT SUM(CASE
                             WHEN F.ID = F2.ID AND A.WORKINGHOUR > 4 THEN
                              1
                             WHEN F.ID = F2.ID AND F.CODE = 'CT' THEN
                              1
                             WHEN F.ID = F2.ID AND F.CODE = 'DH' THEN
                              1
                             WHEN M.CODE = '1/2X' THEN
                              1
                             ELSE
                              0
                           END)
                  FROM AT_TIME_TIMESHEET_DAILY A
                  LEFT JOIN AT_TIME_MANUAL M
                    ON A.MANUAL_ID = M.ID
                  LEFT JOIN AT_FML F
                    ON M.MORNING_ID = F.ID
                  LEFT JOIN AT_FML F2
                    ON M.AFTERNOON_ID = F2.ID
                 WHERE D.ID = A.ID)
         WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
           AND D.WORKINGDAY >= PV_FROMDATE
           AND D.WORKINGDAY <= PV_ENDDATE;
  
      --###: Xu ly cong lam OT
     INSERT INTO TEMP1
          (TMP, WCODE, EXEDATE, TYPE)
        VALUES
          ('INSERT INTO AT_CHOSEN_EMP', '13', SYSDATE, 400);
        --COMMIT;
      INSERT INTO AT_REGISTER_OT_TEMP
        (ID,
         EMPLOYEE_ID,
         WORKINGDAY,
         FROM_HOUR,
         TO_HOUR,
         HOUR,
         HS_OT,
         VAL_IN,
         VAL_OUT)
        SELECT SEQ_AT_REGISTER_OT_TEMP.NEXTVAL,
               T.EMPLOYEE_ID,
               T.WORKINGDAY,
               OT.FROM_HOUR,
               OT.TO_HOUR,
               CASE
                 WHEN OT.FROM_HOUR >= T.VALTIME1 THEN
                  ROUND((T.VALTIME2 - OT.FROM_HOUR) * 24, 2)
                 ELSE
                  ROUND((T.VALTIME2 - T.VALTIME1) * 24, 2)
               END AS HOUR,
               OT.HS_OT,
               T.VALTIME1,
               T.VALTIME2
          FROM (SELECT * FROM AT_CAL_INOUT_TEMP T WHERE T.IS_OT = '1') T
         INNER JOIN AT_REGISTER_OT OT
            ON OT.EMPLOYEE_ID = T.EMPLOYEE_ID
           AND OT.WORKINGDAY = T.WORKINGDAY;
  
      --Lay gia tri cap nhat gio lam them neu co
      INSERT INTO AT_REGISTER_OT_TEMP
        (ID,
         EMPLOYEE_ID,
         WORKINGDAY,
         FROM_HOUR,
         TO_HOUR,
         BREAK_HOUR,
         HOUR,
         HS_OT)
        SELECT SEQ_AT_REGISTER_OT_TEMP.NEXTVAL,
               OT.EMPLOYEE_ID,
               OT.WORKINGDAY,
               OT.FROM_HOUR,
               OT.TO_HOUR,
               OT.BREAK_HOUR,
               OT.HOUR,
               OT.HS_OT
          FROM AT_REGISTER_OT OT
         WHERE OT.TYPE_INPUT = 0
           AND OT.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
  
      UPDATE AT_REGISTER_OT_TEMP T
         SET T.HOUR = (TRUNC(T.HOUR)) + CASE
                        WHEN MOD(T.HOUR, 1) >= 0.67 THEN
                         1
                        WHEN MOD(T.HOUR, 1) >= 0.25 THEN
                         0.5
                        ELSE
                         0
                      END;
     INSERT INTO TEMP1
          (TMP, WCODE, EXEDATE, TYPE)
        VALUES
          ('INSERT INTO AT_CHOSEN_EMP', '14', SYSDATE, 400);
        --COMMIT;
      --Khoi tao du lieu
      INSERT INTO AT_TIME_TIMESHEET_OT_TEMP
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         PERIOD_ID,
         FROM_DATE,
         END_DATE,
         D1,
         D2,
         D3,
         D4,
         D5,
         D6,
         D7,
         D8,
         D9,
         D10,
         D11,
         D12,
         D13,
         D14,
         D15,
         D16,
         D17,
         D18,
         D19,
         D20,
         D21,
         D22,
         D23,
         D24,
         D25,
         D26,
         D27,
         D28,
         D29,
         D30,
         D31,
         D1_1,
         D1_15,
         D1_2,
         D1_27,
         D1_3,
         D1_39,
         D2_1,
         D2_15,
         D2_2,
         D2_27,
         D2_3,
         D2_39,
         D3_1,
         D3_15,
         D3_2,
         D3_27,
         D3_3,
         D3_39,
         D4_1,
         D4_15,
         D4_2,
         D4_27,
         D4_3,
         D4_39,
         D5_1,
         D5_15,
         D5_2,
         D5_27,
         D5_3,
         D5_39,
         D6_1,
         D6_15,
         D6_2,
         D6_27,
         D6_3,
         D6_39,
         D7_1,
         D7_15,
         D7_2,
         D7_27,
         D7_3,
         D7_39,
         D8_1,
         D8_15,
         D8_2,
         D8_27,
         D8_3,
         D8_39,
         D9_1,
         D9_15,
         D9_2,
         D9_27,
         D9_3,
         D9_39,
         D10_1,
         D10_15,
         D10_2,
         D10_27,
         D10_3,
         D10_39,
         D11_1,
         D11_15,
         D11_2,
         D11_27,
         D11_3,
         D11_39,
         D12_1,
         D12_15,
         D12_2,
         D12_27,
         D12_3,
         D12_39,
         D13_1,
         D13_15,
         D13_2,
         D13_27,
         D13_3,
         D13_39,
         D14_1,
         D14_15,
         D14_2,
         D14_27,
         D14_3,
         D14_39,
         D15_1,
         D15_15,
         D15_2,
         D15_27,
         D15_3,
         D15_39,
         D16_1,
         D16_15,
         D16_2,
         D16_27,
         D16_3,
         D16_39,
         D17_1,
         D17_15,
         D17_2,
         D17_27,
         D17_3,
         D17_39,
         D18_1,
         D18_15,
         D18_2,
         D18_27,
         D18_3,
         D18_39,
         D19_1,
         D19_15,
         D19_2,
         D19_27,
         D19_3,
         D19_39,
         D20_1,
         D20_15,
         D20_2,
         D20_27,
         D20_3,
         D20_39,
         D21_1,
         D21_15,
         D21_2,
         D21_27,
         D21_3,
         D21_39,
         D22_1,
         D22_15,
         D22_2,
         D22_27,
         D22_3,
         D22_39,
         D23_1,
         D23_15,
         D23_2,
         D23_27,
         D23_3,
         D23_39,
         D24_1,
         D24_15,
         D24_2,
         D24_27,
         D24_3,
         D24_39,
         D25_1,
         D25_15,
         D25_2,
         D25_27,
         D25_3,
         D25_39,
         D26_1,
         D26_15,
         D26_2,
         D26_27,
         D26_3,
         D26_39,
         D27_1,
         D27_15,
         D27_2,
         D27_27,
         D27_3,
         D27_39,
         D28_1,
         D28_15,
         D28_2,
         D28_27,
         D28_3,
         D28_39,
         D29_1,
         D29_15,
         D29_2,
         D29_27,
         D29_3,
         D29_39,
         D30_1,
         D30_15,
         D30_2,
         D30_27,
         D30_3,
         D30_39,
         D31_1,
         D31_15,
         D31_2,
         D31_27,
         D31_3,
         D31_39,
         CREATED_DATE,
         CREATED_BY,
         REQUEST_ID,
         TOTAL_FACTOR1,
         TOTAL_FACTOR1_5,
         TOTAL_FACTOR2,
         TOTAL_FACTOR2_7,
         TOTAL_FACTOR3,
         TOTAL_FACTOR3_9,
         TOTAL_FACTOR_CONVERT,
         NUMBER_FACTOR_PAY,
         NUMBER_FACTOR_CP)
        SELECT SEQ_AT_TIME_OT_TEMP.NEXTVAL,
               A.EMPLOYEE_ID,
               A.ORG_ID,
               A.TITLE_ID,
               A.STAFF_RANK_ID,
               P_PERIOD_ID,
               PV_FROMDATE,
               PV_ENDDATE,
               NVL(A.D1_1, 0) + NVL(A.D1_15, 0) + NVL(A.D1_2, 0) +
               NVL(A.D1_27, 0) + NVL(A.D1_3, 0) + NVL(A.D1_39, 0) D1,
               NVL(A.D2_1, 0) + NVL(A.D2_15, 0) + NVL(A.D2_2, 0) +
               NVL(A.D2_27, 0) + NVL(A.D2_3, 0) + NVL(A.D2_39, 0) D2,
               NVL(A.D3_1, 0) + NVL(A.D3_15, 0) + NVL(A.D3_2, 0) +
               NVL(A.D3_27, 0) + NVL(A.D3_3, 0) + NVL(A.D3_39, 0) D3,
               NVL(A.D4_1, 0) + NVL(A.D4_15, 0) + NVL(A.D4_2, 0) +
               NVL(A.D4_27, 0) + NVL(A.D4_3, 0) + NVL(A.D4_39, 0) D4,
               NVL(A.D5_1, 0) + NVL(A.D5_15, 0) + NVL(A.D5_2, 0) +
               NVL(A.D5_27, 0) + NVL(A.D5_3, 0) + NVL(A.D5_39, 0) D5,
               NVL(A.D6_1, 0) + NVL(A.D6_15, 0) + NVL(A.D6_2, 0) +
               NVL(A.D6_27, 0) + NVL(A.D6_3, 0) + NVL(A.D6_39, 0) D6,
               NVL(A.D7_1, 0) + NVL(A.D7_15, 0) + NVL(A.D7_2, 0) +
               NVL(A.D7_27, 0) + NVL(A.D7_3, 0) + NVL(A.D7_39, 0) D7,
               NVL(A.D8_1, 0) + NVL(A.D8_15, 0) + NVL(A.D8_2, 0) +
               NVL(A.D8_27, 0) + NVL(A.D8_3, 0) + NVL(A.D8_39, 0) D8,
               NVL(A.D9_1, 0) + NVL(A.D9_15, 0) + NVL(A.D9_2, 0) +
               NVL(A.D9_27, 0) + NVL(A.D9_3, 0) + NVL(A.D9_39, 0) D9,
               NVL(A.D10_1, 0) + NVL(A.D10_15, 0) + NVL(A.D10_2, 0) +
               NVL(A.D10_27, 0) + NVL(A.D10_3, 0) + NVL(A.D10_39, 0) D10,
               NVL(A.D11_1, 0) + NVL(A.D11_15, 0) + NVL(A.D11_2, 0) +
               NVL(A.D11_27, 0) + NVL(A.D11_3, 0) + NVL(A.D11_39, 0) D11,
               NVL(A.D12_1, 0) + NVL(A.D12_15, 0) + NVL(A.D12_2, 0) +
               NVL(A.D12_27, 0) + NVL(A.D12_3, 0) + NVL(A.D12_39, 0) D12,
               NVL(A.D13_1, 0) + NVL(A.D13_15, 0) + NVL(A.D13_2, 0) +
               NVL(A.D13_27, 0) + NVL(A.D13_3, 0) + NVL(A.D13_39, 0) D13,
               NVL(A.D14_1, 0) + NVL(A.D14_15, 0) + NVL(A.D14_2, 0) +
               NVL(A.D14_27, 0) + NVL(A.D14_3, 0) + NVL(A.D14_39, 0) D14,
               NVL(A.D15_1, 0) + NVL(A.D15_15, 0) + NVL(A.D15_2, 0) +
               NVL(A.D15_27, 0) + NVL(A.D15_3, 0) + NVL(A.D15_39, 0) D15,
               NVL(A.D16_1, 0) + NVL(A.D16_15, 0) + NVL(A.D16_2, 0) +
               NVL(A.D16_27, 0) + NVL(A.D16_3, 0) + NVL(A.D16_39, 0) D16,
               NVL(A.D17_1, 0) + NVL(A.D17_15, 0) + NVL(A.D17_2, 0) +
               NVL(A.D17_27, 0) + NVL(A.D17_3, 0) + NVL(A.D17_39, 0) D17,
               NVL(A.D18_1, 0) + NVL(A.D18_15, 0) + NVL(A.D18_2, 0) +
               NVL(A.D18_27, 0) + NVL(A.D18_3, 0) + NVL(A.D18_39, 0) D18,
               NVL(A.D19_1, 0) + NVL(A.D19_15, 0) + NVL(A.D19_2, 0) +
               NVL(A.D19_27, 0) + NVL(A.D19_3, 0) + NVL(A.D19_39, 0) D19,
               NVL(A.D20_1, 0) + NVL(A.D20_15, 0) + NVL(A.D20_2, 0) +
               NVL(A.D20_27, 0) + NVL(A.D20_3, 0) + NVL(A.D20_39, 0) D20,
               NVL(A.D21_1, 0) + NVL(A.D21_15, 0) + NVL(A.D21_2, 0) +
               NVL(A.D21_27, 0) + NVL(A.D21_3, 0) + NVL(A.D21_39, 0) D21,
               NVL(A.D22_1, 0) + NVL(A.D22_15, 0) + NVL(A.D22_2, 0) +
               NVL(A.D22_27, 0) + NVL(A.D22_3, 0) + NVL(A.D22_39, 0) D22,
               NVL(A.D23_1, 0) + NVL(A.D23_15, 0) + NVL(A.D23_2, 0) +
               NVL(A.D23_27, 0) + NVL(A.D23_3, 0) + NVL(A.D23_39, 0) D23,
               NVL(A.D24_1, 0) + NVL(A.D24_15, 0) + NVL(A.D24_2, 0) +
               NVL(A.D24_27, 0) + NVL(A.D24_3, 0) + NVL(A.D24_39, 0) D24,
               NVL(A.D25_1, 0) + NVL(A.D25_15, 0) + NVL(A.D25_2, 0) +
               NVL(A.D25_27, 0) + NVL(A.D25_3, 0) + NVL(A.D25_39, 0) D25,
               NVL(A.D26_1, 0) + NVL(A.D26_15, 0) + NVL(A.D26_2, 0) +
               NVL(A.D26_27, 0) + NVL(A.D26_3, 0) + NVL(A.D26_39, 0) D26,
               NVL(A.D27_1, 0) + NVL(A.D27_15, 0) + NVL(A.D27_2, 0) +
               NVL(A.D27_27, 0) + NVL(A.D27_3, 0) + NVL(A.D27_39, 0) D27,
               NVL(A.D28_1, 0) + NVL(A.D28_15, 0) + NVL(A.D28_2, 0) +
               NVL(A.D28_27, 0) + NVL(A.D28_3, 0) + NVL(A.D28_39, 0) D28,
               NVL(A.D29_1, 0) + NVL(A.D29_15, 0) + NVL(A.D29_2, 0) +
               NVL(A.D29_27, 0) + NVL(A.D29_3, 0) + NVL(A.D29_39, 0) D29,
               NVL(A.D30_1, 0) + NVL(A.D30_15, 0) + NVL(A.D30_2, 0) +
               NVL(A.D30_27, 0) + NVL(A.D30_3, 0) + NVL(A.D30_39, 0) D30,
               NVL(A.D31_1, 0) + NVL(A.D31_15, 0) + NVL(A.D31_2, 0) +
               NVL(A.D31_27, 0) + NVL(A.D31_3, 0) + NVL(A.D31_39, 0) D31,
               A.D1_1,--==OT 1.0
               A.D1_15,--==OT 1.5
               A.D1_2,--==OT 2.0
               A.D1_27,--==OT 2.7
               A.D1_3,--==OT 3.0
               A.D1_39,--==OT 3.9
               A.D2_1,
               A.D2_15,
               A.D2_2,
               A.D2_27,
               A.D2_3,
               A.D2_39,
               A.D3_1,
               A.D3_15,
               A.D3_2,
               A.D3_27,
               A.D3_3,
               A.D3_39,
               A.D4_1,
               A.D4_15,
               A.D4_2,
               A.D4_27,
               A.D4_3,
               A.D4_39,
               A.D5_1,
               A.D5_15,
               A.D5_2,
               A.D5_27,
               A.D5_3,
               A.D5_39,
               A.D6_1,
               A.D6_15,
               A.D6_2,
               A.D6_27,
               A.D6_3,
               A.D6_39,
               A.D7_1,
               A.D7_15,
               A.D7_2,
               A.D7_27,
               A.D7_3,
               A.D7_39,
               A.D8_1,
               A.D8_15,
               A.D8_2,
               A.D8_27,
               A.D8_3,
               A.D8_39,
               A.D9_1,
               A.D9_15,
               A.D9_2,
               A.D9_27,
               A.D9_3,
               A.D9_39,
               A.D10_1,
               A.D10_15,
               A.D10_2,
               A.D10_27,
               A.D10_3,
               A.D10_39,
               A.D11_1,
               A.D11_15,
               A.D11_2,
               A.D11_27,
               A.D11_3,
               A.D11_39,
               A.D12_1,
               A.D12_15,
               A.D12_2,
               A.D12_27,
               A.D12_3,
               A.D12_39,
               A.D13_1,
               A.D13_15,
               A.D13_2,
               A.D13_27,
               A.D13_3,
               A.D13_39,
               A.D14_1,
               A.D14_15,
               A.D14_2,
               A.D14_27,
               A.D14_3,
               A.D14_39,
               A.D15_1,
               A.D15_15,
               A.D15_2,
               A.D15_27,
               A.D15_3,
               A.D15_39,
               A.D16_1,
               A.D16_15,
               A.D16_2,
               A.D16_27,
               A.D16_3,
               A.D16_39,
               A.D17_1,
               A.D17_15,
               A.D17_2,
               A.D17_27,
               A.D17_3,
               A.D17_39,
               A.D18_1,
               A.D18_15,
               A.D18_2,
               A.D18_27,
               A.D18_3,
               A.D18_39,
               A.D19_1,
               A.D19_15,
               A.D19_2,
               A.D19_27,
               A.D19_3,
               A.D19_39,
               A.D20_1,
               A.D20_15,
               A.D20_2,
               A.D20_27,
               A.D20_3,
               A.D20_39,
               A.D21_1,
               A.D21_15,
               A.D21_2,
               A.D21_27,
               A.D21_3,
               A.D21_39,
               A.D22_1,
               A.D22_15,
               A.D22_2,
               A.D22_27,
               A.D22_3,
               A.D22_39,
               A.D23_1,
               A.D23_15,
               A.D23_2,
               A.D23_27,
               A.D23_3,
               A.D23_39,
               A.D24_1,
               A.D24_15,
               A.D24_2,
               A.D24_27,
               A.D24_3,
               A.D24_39,
               A.D25_1,
               A.D25_15,
               A.D25_2,
               A.D25_27,
               A.D25_3,
               A.D25_39,
               A.D26_1,
               A.D26_15,
               A.D26_2,
               A.D26_27,
               A.D26_3,
               A.D26_39,
               A.D27_1,
               A.D27_15,
               A.D27_2,
               A.D27_27,
               A.D27_3,
               A.D27_39,
               A.D28_1,
               A.D28_15,
               A.D28_2,
               A.D28_27,
               A.D28_3,
               A.D28_39,
               A.D29_1,
               A.D29_15,
               A.D29_2,
               A.D29_27,
               A.D29_3,
               A.D29_39,
               A.D30_1,
               A.D30_15,
               A.D30_2,
               A.D30_27,
               A.D30_3,
               A.D30_39,
               A.D31_1,
               A.D31_15,
               A.D31_2,
               A.D31_27,
               A.D31_3,
               A.D31_39,
               TRUNC(SYSDATE),
               UPPER(P_USERNAME),
               PV_REQUEST_ID,
               CASE
                 WHEN OT.FACTOR1 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR1
               END FACTOR1,
               CASE
                 WHEN OT.FACTOR1_5 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR1_5
               END FACTOR1_5,
               CASE
                 WHEN OT.FACTOR2 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR2
               END FACTOR2,
               CASE
                 WHEN OT.FACTOR2_7 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR2_7
               END FACTOR2_7,
               CASE
                 WHEN OT.FACTOR3 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR3
               END FACTOR3,
               CASE
                 WHEN OT.FACTOR3_9 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR3_9
               END FACTOR3_9,
               CASE
                 WHEN OT.TOTAL_FACTOR_CONVERT = 0 THEN
                  NULL
                 ELSE
                  OT.TOTAL_FACTOR_CONVERT
               END TOTAL_FACTOR_CONVERT,
               NULL
               \*CASE
                 WHEN NVL(A.STAFF_RANK_LEVEL, 0) > PV_LEVEL_STAFF THEN
                  CASE
                    WHEN NVL(OT.TOTAL_FACTOR_CONVERT, 0) <= PV_MAX_PAYOT THEN
                     NVL(OT.TOTAL_FACTOR_CONVERT, 0)
                    WHEN NVL(OT.TOTAL_FACTOR_CONVERT, 0) > PV_MAX_PAYOT THEN
                     PV_MAX_PAYOT
                  END
               END*\,
               NULL
        \*(NVL(OT.TOTAL_FACTOR_CONVERT, 0) -
        NVL(CASE
               WHEN NVL(A.STAFF_RANK_LEVEL, 0) > PV_LEVEL_STAFF THEN
                CASE
                  WHEN NVL(OT.TOTAL_FACTOR_CONVERT, 0) <= PV_MAX_PAYOT THEN
                   NVL(OT.TOTAL_FACTOR_CONVERT, 0)
                  WHEN NVL(OT.TOTAL_FACTOR_CONVERT, 0) > PV_MAX_PAYOT THEN
                   PV_MAX_PAYOT
                END
             END,
             0)) + NVL(NB.TOTAL_FACTOR_CONVERT, 0)*\
          FROM (SELECT T.EMPLOYEE_ID,
                       EE.ORG_ID,
                       EE.TITLE_ID,
                       EE.STAFF_RANK_ID,
                       EE.STAFF_RANK_LEVEL,
                       TO_CHAR(T.WORKINGDAY, 'dd') || '_' || CASE
                         WHEN T.HS_OT = 4236 THEN -- 1
                          '1'
                         WHEN T.HS_OT = 4237 THEN -- 1.5
                          '15'
                         WHEN T.HS_OT = 4238 THEN -- 2
                          '2'
                         WHEN T.HS_OT = 4239 THEN -- 2.7
                          '27'
                         WHEN T.HS_OT = 4240 THEN -- 3
                          '3'
                         WHEN T.HS_OT = 4241 THEN -- 3.9
                          '39'
                       END AS DAY,
                       CASE
                         WHEN NVL(T.IS_NB, 0) = 0 THEN
                          T.HOUR
                         ELSE
                          0
                       END HOUR
                  FROM AT_REGISTER_OT_TEMP T
                 INNER JOIN AT_CHOSEN_EMP EE
                    ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID) T
        PIVOT(SUM(HOUR)
           FOR DAY IN('01_1' AS D1_1,
                      '01_15' AS D1_15,
                      '01_2' AS D1_2,
                      '01_27' AS D1_27,
                      '01_3' AS D1_3,
                      '01_39' AS D1_39,
                      '02_1' AS D2_1,
                      '02_15' AS D2_15,
                      '02_2' AS D2_2,
                      '02_27' AS D2_27,
                      '02_3' AS D2_3,
                      '02_39' AS D2_39,
                      '03_1' AS D3_1,
                      '03_15' AS D3_15,
                      '03_2' AS D3_2,
                      '03_27' AS D3_27,
                      '03_3' AS D3_3,
                      '03_39' AS D3_39,
                      '04_1' AS D4_1,
                      '04_15' AS D4_15,
                      '04_2' AS D4_2,
                      '04_27' AS D4_27,
                      '04_3' AS D4_3,
                      '04_39' AS D4_39,
                      '05_1' AS D5_1,
                      '05_15' AS D5_15,
                      '05_2' AS D5_2,
                      '05_27' AS D5_27,
                      '05_3' AS D5_3,
                      '05_39' AS D5_39,
                      '06_1' AS D6_1,
                      '06_15' AS D6_15,
                      '06_2' AS D6_2,
                      '06_27' AS D6_27,
                      '06_3' AS D6_3,
                      '06_39' AS D6_39,
                      '07_1' AS D7_1,
                      '07_15' AS D7_15,
                      '07_2' AS D7_2,
                      '07_27' AS D7_27,
                      '07_3' AS D7_3,
                      '07_39' AS D7_39,
                      '08_1' AS D8_1,
                      '08_15' AS D8_15,
                      '08_2' AS D8_2,
                      '08_27' AS D8_27,
                      '08_3' AS D8_3,
                      '08_39' AS D8_39,
                      '09_1' AS D9_1,
                      '09_15' AS D9_15,
                      '09_2' AS D9_2,
                      '09_27' AS D9_27,
                      '09_3' AS D9_3,
                      '09_39' AS D9_39,
                      '10_1' AS D10_1,
                      '10_15' AS D10_15,
                      '10_2' AS D10_2,
                      '10_27' AS D10_27,
                      '10_3' AS D10_3,
                      '10_39' AS D10_39,
                      '11_1' AS D11_1,
                      '11_15' AS D11_15,
                      '11_2' AS D11_2,
                      '11_27' AS D11_27,
                      '11_3' AS D11_3,
                      '11_39' AS D11_39,
                      '12_1' AS D12_1,
                      '12_15' AS D12_15,
                      '12_2' AS D12_2,
                      '12_27' AS D12_27,
                      '12_3' AS D12_3,
                      '12_39' AS D12_39,
                      '13_1' AS D13_1,
                      '13_15' AS D13_15,
                      '13_2' AS D13_2,
                      '13_27' AS D13_27,
                      '13_3' AS D13_3,
                      '13_39' AS D13_39,
                      '14_1' AS D14_1,
                      '14_15' AS D14_15,
                      '14_2' AS D14_2,
                      '14_27' AS D14_27,
                      '14_3' AS D14_3,
                      '14_39' AS D14_39,
                      '15_1' AS D15_1,
                      '15_15' AS D15_15,
                      '15_2' AS D15_2,
                      '15_27' AS D15_27,
                      '15_3' AS D15_3,
                      '15_39' AS D15_39,
                      '16_1' AS D16_1,
                      '16_15' AS D16_15,
                      '16_2' AS D16_2,
                      '16_27' AS D16_27,
                      '16_3' AS D16_3,
                      '16_39' AS D16_39,
                      '17_1' AS D17_1,
                      '17_15' AS D17_15,
                      '17_2' AS D17_2,
                      '17_27' AS D17_27,
                      '17_3' AS D17_3,
                      '17_39' AS D17_39,
                      '18_1' AS D18_1,
                      '18_15' AS D18_15,
                      '18_2' AS D18_2,
                      '18_27' AS D18_27,
                      '18_3' AS D18_3,
                      '18_39' AS D18_39,
                      '19_1' AS D19_1,
                      '19_15' AS D19_15,
                      '19_2' AS D19_2,
                      '19_27' AS D19_27,
                      '19_3' AS D19_3,
                      '19_39' AS D19_39,
                      '20_1' AS D20_1,
                      '20_15' AS D20_15,
                      '20_2' AS D20_2,
                      '20_27' AS D20_27,
                      '20_3' AS D20_3,
                      '20_39' AS D20_39,
                      '21_1' AS D21_1,
                      '21_15' AS D21_15,
                      '21_2' AS D21_2,
                      '21_27' AS D21_27,
                      '21_3' AS D21_3,
                      '21_39' AS D21_39,
                      '22_1' AS D22_1,
                      '22_15' AS D22_15,
                      '22_2' AS D22_2,
                      '22_27' AS D22_27,
                      '22_3' AS D22_3,
                      '22_39' AS D22_39,
                      '23_1' AS D23_1,
                      '23_15' AS D23_15,
                      '23_2' AS D23_2,
                      '23_27' AS D23_27,
                      '23_3' AS D23_3,
                      '23_39' AS D23_39,
                      '24_1' AS D24_1,
                      '24_15' AS D24_15,
                      '24_2' AS D24_2,
                      '24_27' AS D24_27,
                      '24_3' AS D24_3,
                      '24_39' AS D24_39,
                      '25_1' AS D25_1,
                      '25_15' AS D25_15,
                      '25_2' AS D25_2,
                      '25_27' AS D25_27,
                      '25_3' AS D25_3,
                      '25_39' AS D25_39,
                      '26_1' AS D26_1,
                      '26_15' AS D26_15,
                      '26_2' AS D26_2,
                      '26_27' AS D26_27,
                      '26_3' AS D26_3,
                      '26_39' AS D26_39,
                      '27_1' AS D27_1,
                      '27_15' AS D27_15,
                      '27_2' AS D27_2,
                      '27_27' AS D27_27,
                      '27_3' AS D27_3,
                      '27_39' AS D27_39,
                      '28_1' AS D28_1,
                      '28_15' AS D28_15,
                      '28_2' AS D28_2,
                      '28_27' AS D28_27,
                      '28_3' AS D28_3,
                      '28_39' AS D28_39,
                      '29_1' AS D29_1,
                      '29_15' AS D29_15,
                      '29_2' AS D29_2,
                      '29_27' AS D29_27,
                      '29_3' AS D29_3,
                      '29_39' AS D29_39,
                      '30_1' AS D30_1,
                      '30_15' AS D30_15,
                      '30_2' AS D30_2,
                      '30_27' AS D30_27,
                      '30_3' AS D30_3,
                      '30_39' AS D30_39,
                      '31_1' AS D31_1,
                      '31_15' AS D31_15,
                      '31_2' AS D31_2,
                      '31_27' AS D31_27,
                      '31_3' AS D31_3,
                      '31_39' AS D31_39)) A
          LEFT JOIN (SELECT OT.EMPLOYEE_ID,
                            NVL(OT.FACTOR1, 0) FACTOR1,
                            NVL(OT.FACTOR1_5, 0) FACTOR1_5,
                            NVL(OT.FACTOR2, 0) FACTOR2,
                            NVL(OT.FACTOR2_7, 0) FACTOR2_7,
                            NVL(OT.FACTOR3, 0) FACTOR3,
                            NVL(OT.FACTOR3_9, 0) FACTOR3_9,
                            ROUND(NVL(OT.FACTOR1, 0) * 1.5 +
                                  NVL(OT.FACTOR1_5, 0) * 2.1 +
                                  NVL(OT.FACTOR2, 0) * 2 +
                                  NVL(OT.FACTOR2_7, 0) * 2.7 +
                                  NVL(OT.FACTOR3, 0) * 3 +
                                  NVL(OT.FACTOR3_9, 0) * 3.9,
                                  2) TOTAL_FACTOR_CONVERT
                       FROM (SELECT T.EMPLOYEE_ID,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4236 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR1,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4237 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR1_5,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4238 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR2,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4239 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR2_7,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4240 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR3,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4241 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR3_9
                               FROM AT_REGISTER_OT_TEMP T
                              WHERE NVL(T.IS_NB, 0) = 0
                              GROUP BY T.EMPLOYEE_ID) OT) OT
            ON A.EMPLOYEE_ID = OT.EMPLOYEE_ID;
  
      -- XOA DU LIEU CU TRUOC KHI TINH
      DELETE FROM AT_TIME_TIMESHEET_OT D
       WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
         AND D.PERIOD_ID = P_PERIOD_ID;
  
      INSERT INTO AT_TIME_TIMESHEET_OT
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         PERIOD_ID,
         TOTAL_FACTOR1,
         TOTAL_FACTOR1_5,
         TOTAL_FACTOR2,
         TOTAL_FACTOR2_7,
         TOTAL_FACTOR3,
         TOTAL_FACTOR3_9,
         TOTAL_FACTOR_CONVERT,
         NUMBER_FACTOR_PAY,
         NUMBER_FACTOR_CP,
         CONGHIBU,
         CREATED_DATE,
         CREATED_BY,
         CREATED_LOG,
         D1,
         D2,
         D3,
         D4,
         D5,
         D6,
         D7,
         D8,
         D9,
         D10,
         D11,
         D12,
         D13,
         D14,
         D15,
         D16,
         D17,
         D18,
         D19,
         D20,
         D21,
         D22,
         D23,
         D24,
         D25,
         D26,
         D27,
         D28,
         D29,
         D30,
         D31,
         D1_1,
         D1_15,
         D1_2,
         D1_27,
         D1_3,
         D1_39,
         D2_1,
         D2_15,
         D2_2,
         D2_27,
         D2_3,
         D2_39,
         D3_1,
         D3_15,
         D3_2,
         D3_27,
         D3_3,
         D3_39,
         D4_1,
         D4_15,
         D4_2,
         D4_27,
         D4_3,
         D4_39,
         D5_1,
         D5_15,
         D5_2,
         D5_27,
         D5_3,
         D5_39,
         D6_1,
         D6_15,
         D6_2,
         D6_27,
         D6_3,
         D6_39,
         D7_1,
         D7_15,
         D7_2,
         D7_27,
         D7_3,
         D7_39,
         D8_1,
         D8_15,
         D8_2,
         D8_27,
         D8_3,
         D8_39,
         D9_1,
         D9_15,
         D9_2,
         D9_27,
         D9_3,
         D9_39,
         D10_1,
         D10_15,
         D10_2,
         D10_27,
         D10_3,
         D10_39,
         D11_1,
         D11_15,
         D11_2,
         D11_27,
         D11_3,
         D11_39,
         D12_1,
         D12_15,
         D12_2,
         D12_27,
         D12_3,
         D12_39,
         D13_1,
         D13_15,
         D13_2,
         D13_27,
         D13_3,
         D13_39,
         D14_1,
         D14_15,
         D14_2,
         D14_27,
         D14_3,
         D14_39,
         D15_1,
         D15_15,
         D15_2,
         D15_27,
         D15_3,
         D15_39,
         D16_1,
         D16_15,
         D16_2,
         D16_27,
         D16_3,
         D16_39,
         D17_1,
         D17_15,
         D17_2,
         D17_27,
         D17_3,
         D17_39,
         D18_1,
         D18_15,
         D18_2,
         D18_27,
         D18_3,
         D18_39,
         D19_1,
         D19_15,
         D19_2,
         D19_27,
         D19_3,
         D19_39,
         D20_1,
         D20_15,
         D20_2,
         D20_27,
         D20_3,
         D20_39,
         D21_1,
         D21_15,
         D21_2,
         D21_27,
         D21_3,
         D21_39,
         D22_1,
         D22_15,
         D22_2,
         D22_27,
         D22_3,
         D22_39,
         D23_1,
         D23_15,
         D23_2,
         D23_27,
         D23_3,
         D23_39,
         D24_1,
         D24_15,
         D24_2,
         D24_27,
         D24_3,
         D24_39,
         D25_1,
         D25_15,
         D25_2,
         D25_27,
         D25_3,
         D25_39,
         D26_1,
         D26_15,
         D26_2,
         D26_27,
         D26_3,
         D26_39,
         D27_1,
         D27_15,
         D27_2,
         D27_27,
         D27_3,
         D27_39,
         D28_1,
         D28_15,
         D28_2,
         D28_27,
         D28_3,
         D28_39,
         D29_1,
         D29_15,
         D29_2,
         D29_27,
         D29_3,
         D29_39,
         D30_1,
         D30_15,
         D30_2,
         D30_27,
         D30_3,
         D30_39,
         D31_1,
         D31_15,
         D31_2,
         D31_27,
         D31_3,
         D31_39,
         FROM_DATE,
         END_DATE,
         BACKUP_MONTH_BEFORE,
         GHINHAN_OT)
        SELECT SEQ_AT_TIME_TIMESHEET_OT.NEXTVAL,
               EMPLOYEE_ID,
               ORG_ID,
               TITLE_ID,
               STAFF_RANK_ID,
               PERIOD_ID,
               TOTAL_FACTOR1,
               TOTAL_FACTOR1_5,
               TOTAL_FACTOR2,
               TOTAL_FACTOR2_7,
               TOTAL_FACTOR3,
               TOTAL_FACTOR3_9,
               TOTAL_FACTOR_CONVERT,
               NUMBER_FACTOR_PAY,
               NUMBER_FACTOR_CP,
               CONGHIBU,
               CREATED_DATE,
               CREATED_BY,
               CREATED_LOG,
               CASE
                 WHEN NVL(D1, 0) = 0 THEN
                  NULL
                 ELSE
                  D1
               END D1,
               CASE
                 WHEN NVL(D2, 0) = 0 THEN
                  NULL
                 ELSE
                  D2
               END D2,
               CASE
                 WHEN NVL(D3, 0) = 0 THEN
                  NULL
                 ELSE
                  D3
               END D3,
               CASE
                 WHEN NVL(D4, 0) = 0 THEN
                  NULL
                 ELSE
                  D4
               END D4,
               CASE
                 WHEN NVL(D5, 0) = 0 THEN
                  NULL
                 ELSE
                  D5
               END D5,
               CASE
                 WHEN NVL(D6, 0) = 0 THEN
                  NULL
                 ELSE
                  D6
               END D6,
               CASE
                 WHEN NVL(D7, 0) = 0 THEN
                  NULL
                 ELSE
                  D7
               END D7,
               CASE
                 WHEN NVL(D8, 0) = 0 THEN
                  NULL
                 ELSE
                  D8
               END D8,
               CASE
                 WHEN NVL(D9, 0) = 0 THEN
                  NULL
                 ELSE
                  D9
               END D9,
               CASE
                 WHEN NVL(D10, 0) = 0 THEN
                  NULL
                 ELSE
                  D10
               END D10,
               CASE
                 WHEN NVL(D11, 0) = 0 THEN
                  NULL
                 ELSE
                  D11
               END D11,
               CASE
                 WHEN NVL(D12, 0) = 0 THEN
                  NULL
                 ELSE
                  D12
               END D12,
               CASE
                 WHEN NVL(D13, 0) = 0 THEN
                  NULL
                 ELSE
                  D13
               END D13,
               CASE
                 WHEN NVL(D14, 0) = 0 THEN
                  NULL
                 ELSE
                  D14
               END D14,
               CASE
                 WHEN NVL(D15, 0) = 0 THEN
                  NULL
                 ELSE
                  D15
               END D15,
               CASE
                 WHEN NVL(D16, 0) = 0 THEN
                  NULL
                 ELSE
                  D16
               END D16,
               CASE
                 WHEN NVL(D17, 0) = 0 THEN
                  NULL
                 ELSE
                  D17
               END D17,
               CASE
                 WHEN NVL(D18, 0) = 0 THEN
                  NULL
                 ELSE
                  D18
               END D18,
               CASE
                 WHEN NVL(D19, 0) = 0 THEN
                  NULL
                 ELSE
                  D19
               END D19,
               CASE
                 WHEN NVL(D20, 0) = 0 THEN
                  NULL
                 ELSE
                  D20
               END D20,
               CASE
                 WHEN NVL(D21, 0) = 0 THEN
                  NULL
                 ELSE
                  D21
               END D21,
               CASE
                 WHEN NVL(D22, 0) = 0 THEN
                  NULL
                 ELSE
                  D22
               END D22,
               CASE
                 WHEN NVL(D23, 0) = 0 THEN
                  NULL
                 ELSE
                  D23
               END D23,
               CASE
                 WHEN NVL(D24, 0) = 0 THEN
                  NULL
                 ELSE
                  D24
               END D24,
               CASE
                 WHEN NVL(D25, 0) = 0 THEN
                  NULL
                 ELSE
                  D25
               END D25,
               CASE
                 WHEN NVL(D26, 0) = 0 THEN
                  NULL
                 ELSE
                  D26
               END D26,
               CASE
                 WHEN NVL(D27, 0) = 0 THEN
                  NULL
                 ELSE
                  D27
               END D27,
               CASE
                 WHEN NVL(D28, 0) = 0 THEN
                  NULL
                 ELSE
                  D28
               END D28,
               CASE
                 WHEN NVL(D29, 0) = 0 THEN
                  NULL
                 ELSE
                  D29
               END D29,
               CASE
                 WHEN NVL(D30, 0) = 0 THEN
                  NULL
                 ELSE
                  D30
               END D30,
               CASE
                 WHEN NVL(D31, 0) = 0 THEN
                  NULL
                 ELSE
                  D31
               END D31,
               D1_1,
               D1_15,
               D1_2,
               D1_27,
               D1_3,
               D1_39,
               D2_1,
               D2_15,
               D2_2,
               D2_27,
               D2_3,
               D2_39,
               D3_1,
               D3_15,
               D3_2,
               D3_27,
               D3_3,
               D3_39,
               D4_1,
               D4_15,
               D4_2,
               D4_27,
               D4_3,
               D4_39,
               D5_1,
               D5_15,
               D5_2,
               D5_27,
               D5_3,
               D5_39,
               D6_1,
               D6_15,
               D6_2,
               D6_27,
               D6_3,
               D6_39,
               D7_1,
               D7_15,
               D7_2,
               D7_27,
               D7_3,
               D7_39,
               D8_1,
               D8_15,
               D8_2,
               D8_27,
               D8_3,
               D8_39,
               D9_1,
               D9_15,
               D9_2,
               D9_27,
               D9_3,
               D9_39,
               D10_1,
               D10_15,
               D10_2,
               D10_27,
               D10_3,
               D10_39,
               D11_1,
               D11_15,
               D11_2,
               D11_27,
               D11_3,
               D11_39,
               D12_1,
               D12_15,
               D12_2,
               D12_27,
               D12_3,
               D12_39,
               D13_1,
               D13_15,
               D13_2,
               D13_27,
               D13_3,
               D13_39,
               D14_1,
               D14_15,
               D14_2,
               D14_27,
               D14_3,
               D14_39,
               D15_1,
               D15_15,
               D15_2,
               D15_27,
               D15_3,
               D15_39,
               D16_1,
               D16_15,
               D16_2,
               D16_27,
               D16_3,
               D16_39,
               D17_1,
               D17_15,
               D17_2,
               D17_27,
               D17_3,
               D17_39,
               D18_1,
               D18_15,
               D18_2,
               D18_27,
               D18_3,
               D18_39,
               D19_1,
               D19_15,
               D19_2,
               D19_27,
               D19_3,
               D19_39,
               D20_1,
               D20_15,
               D20_2,
               D20_27,
               D20_3,
               D20_39,
               D21_1,
               D21_15,
               D21_2,
               D21_27,
               D21_3,
               D21_39,
               D22_1,
               D22_15,
               D22_2,
               D22_27,
               D22_3,
               D22_39,
               D23_1,
               D23_15,
               D23_2,
               D23_27,
               D23_3,
               D23_39,
               D24_1,
               D24_15,
               D24_2,
               D24_27,
               D24_3,
               D24_39,
               D25_1,
               D25_15,
               D25_2,
               D25_27,
               D25_3,
               D25_39,
               D26_1,
               D26_15,
               D26_2,
               D26_27,
               D26_3,
               D26_39,
               D27_1,
               D27_15,
               D27_2,
               D27_27,
               D27_3,
               D27_39,
               D28_1,
               D28_15,
               D28_2,
               D28_27,
               D28_3,
               D28_39,
               D29_1,
               D29_15,
               D29_2,
               D29_27,
               D29_3,
               D29_39,
               D30_1,
               D30_15,
               D30_2,
               D30_27,
               D30_3,
               D30_39,
               D31_1,
               D31_15,
               D31_2,
               D31_27,
               D31_3,
               D31_39,
               FROM_DATE,
               END_DATE,
               BACKUP_MONTH_BEFORE,
               GHINHAN_OT
          FROM AT_TIME_TIMESHEET_OT_TEMP E;
  
           MERGE INTO AT_TIME_TIMESHEET_OT D
      USING (select OT.EMPLOYEE_ID,
                    SUM (OT.HOUR * (SELECT CODE FROM OT_OTHER_LIST O WHERE O.ID = OT.HS_OT AND NVL(OT.IS_NB,0) = 0)) NUMBER_FACTOR_PAY1,
                    SUM (OT.HOUR * (SELECT CODE FROM OT_OTHER_LIST O WHERE O.ID = OT.HS_OT AND NVL(OT.IS_NB,0) = -1)) NUMBER_FACTOR_CP1,
                    ROUND(SUM (OT.HOUR * (SELECT CODE FROM OT_OTHER_LIST O WHERE O.ID = OT.HS_OT AND NVL(OT.IS_NB,0) = -1))/8,2) CONGHIBU1
              from at_register_ot OT
              WHERE TO_CHAR(OT.WORKINGDAY,'yyyyMM') = TO_CHAR(PV_ENDDATE,'yyyyMM')
              GROUP BY OT.EMPLOYEE_ID) C
      ON (D.EMPLOYEE_ID = C.EMPLOYEE_ID)
      WHEN MATCHED THEN
        UPDATE
           SET D.NUMBER_FACTOR_PAY = C.NUMBER_FACTOR_PAY1,
            D.NUMBER_FACTOR_CP = C.NUMBER_FACTOR_CP1,
            D.CONGHIBU = C.CONGHIBU1
      WHERE D.PERIOD_ID = P_PERIOD_ID;
  
   EXCEPTION
      WHEN OTHERS THEN
        SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                                'PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_ALL',
                                SQLERRM || '_' ||
                                DBMS_UTILITY.format_error_backtrace,
                                P_ORG_ID,
                                P_PERIOD_ID,
                                P_USERNAME,
                                PV_REQUEST_ID);
    END;
  */
  /*PROCEDURE CAL_TIME_TIMESHEET_MACHINES(P_USERNAME   IN NVARCHAR2,
                                        P_ORG_ID     IN NUMBER,
                                        P_FROMDATE   IN DATE,
                                        P_ENDDATE    IN DATE,
                                        P_ISDISSOLVE IN NUMBER) IS
    PV_FROMDATE    DATE;
    PV_ENDDATE     DATE;
    PV_SQL         CLOB;
    PV_PERIOD_ID   NUMBER;
    PV_REQUEST_ID  NUMBER;
    PV_MINUS_ALLOW NUMBER := 50;
  BEGIN
  
    PV_FROMDATE   := P_FROMDATE;
    PV_ENDDATE    := P_ENDDATE;
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
    SELECT P.ID
      INTO PV_PERIOD_ID
      FROM AT_PERIOD P
     WHERE PV_FROMDATE BETWEEN P.START_DATE AND P.END_DATE
       AND ROWNUM = 1;
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O
        WHERE EXISTS (SELECT 1
                 FROM AT_ORG_PERIOD OP
                WHERE OP.PERIOD_ID = PV_PERIOD_ID
                  AND OP.ORG_ID = O.ORG_ID
                  AND OP.STATUSCOLEX = 1));
  
    -- insert emp can tinh toan
    INSERT INTO AT_CHOSEN_EMP
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       TER_EFFECT_DATE,
       USERNAME,
       REQUEST_ID,
       JOIN_DATE,
       JOIN_DATE_STATE)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              CASE
                WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                 T.TER_EFFECT_DATE + 1
                ELSE
                 NULL
              END TER_EFFECT_DATE,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              T.JOIN_DATE,
              T.JOIN_DATE_STATE
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_ENDDATE
                      AND E.STATUS_ID = 447
                      AND E.IS_3B = 0) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
           ON O.ORG_ID = W.ORG_ID
        WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
    -- Phan theo ca lam viec:
    -- 1.Co DK ca
    INSERT INTO AT_CAL_INOUT_TEMP
      (ID,
       EMPLOYEE_ID,
       WORKINGDAY,
       SHIFT_ID,
       SHIFT_CODE,
       SHIFT_MANUAL_ID,
       SHIFT_HOURS_START,
       SHIFT_HOURS_STOP,
       BREAKS_FORM,
       BREAKS_TO,
       IS_NOON,
       WORKINGHOUR_SHIFT,
       VALTIME1,
       MINUTE_DM,
       MINUTE_VS,
       REQUEST_ID,
       ITIME_ID)
      SELECT SEQ_AT_CAL_INOUT_TEMP.NEXTVAL,
             T.EMPLOYEE_ID,
             WSIGN.WORKINGDAY,
             WSIGN.SHIFT_ID,
             WSIGN.SHIFT_CODE,
             WSIGN.SHIFT_MANUAL_ID,
             WSIGN.HOURS_START,
             WSIGN.HOURS_STOP,
             WSIGN.BREAKS_FORM,
             WSIGN.BREAKS_TO,
             WSIGN.IS_NOON,
             CASE
               WHEN WSIGN.SHIFT_ID IS NOT NULL THEN
                CASE
                  WHEN WSIGN.BREAKS_FORM IS NULL AND WSIGN.BREAKS_TO IS NULL THEN
                   CASE
                     WHEN WSIGN.HOURS_STOP < WSIGN.HOURS_START THEN
                      ROUND((WSIGN.HOURS_STOP + 1 - WSIGN.HOURS_START) * 24, 2)
                     ELSE
                      ROUND((WSIGN.HOURS_STOP - WSIGN.HOURS_START) * 24, 2)
                   END
                  ELSE
                   ROUND((WSIGN.BREAKS_FORM - WSIGN.HOURS_START) * 24, 2) +
                   ROUND((WSIGN.HOURS_STOP - WSIGN.BREAKS_TO) * 24, 2)
                END
               ELSE
                0
             END,
             CASE
               WHEN WSIGN.HOURS_START IS NOT NULL THEN
                (SELECT INOUT.VALTIME
                   FROM (SELECT INOUT.*,
                                ROW_NUMBER() OVER(PARTITION BY INOUT.ITIME_ID, INOUT.WORKINGDAY ORDER BY INOUT.VALTIME) AS ROW_NUMBER
                           FROM AT_SWIPE_DATA INOUT) INOUT
                  WHERE INOUT.ITIME_ID = T.ITIME_ID
                    AND INOUT.WORKINGDAY = WSIGN.WORKINGDAY
                    AND INOUT.ROW_NUMBER = 1)
             END,
             WSIGN.MINUTE_DM,
             WSIGN.MINUTE_VS,
             PV_REQUEST_ID,
             T.ITIME_ID
        FROM AT_CHOSEN_EMP T
       INNER JOIN (SELECT WSIGN.EMPLOYEE_ID,
                          WSIGN.SHIFT_ID,
                          WSIGN.WORKINGDAY,
                          SHIFT.CODE SHIFT_CODE,
                          SHIFT.MANUAL_ID SHIFT_MANUAL_ID,
                          CASE
                            WHEN SHIFT.BREAKS_FORM IS NOT NULL THEN
                             TO_DATE(TO_CHAR(WSIGN.WORKINGDAY, 'yyyymmdd') ||
                                     TO_CHAR(SHIFT.BREAKS_FORM, 'HH24MI'),
                                     'yyyymmddHH24MI')
                          END BREAKS_FORM,
                          CASE
                            WHEN SHIFT.BREAKS_TO IS NOT NULL THEN
                             TO_DATE(TO_CHAR(WSIGN.WORKINGDAY, 'yyyymmdd') ||
                                     TO_CHAR(SHIFT.BREAKS_TO, 'HH24MI'),
                                     'yyyymmddHH24MI')
                          END BREAKS_TO,
  
                          CASE
                            WHEN SHIFT.HOURS_START IS NOT NULL THEN
                             TO_DATE(TO_CHAR(WSIGN.WORKINGDAY, 'yyyymmdd') ||
                                     TO_CHAR(SHIFT.HOURS_START, 'HH24MI'),
                                     'yyyymmddHH24MI')
                          END HOURS_START,
  
                          CASE
                            WHEN SHIFT.HOURS_STOP IS NOT NULL THEN
                             TO_DATE(TO_CHAR(WSIGN.WORKINGDAY, 'yyyymmdd') ||
                                     TO_CHAR(SHIFT.HOURS_STOP, 'HH24MI'),
                                     'yyyymmddHH24MI')
                          END HOURS_STOP,
                          NVL(SHIFT.IS_NOON, 0) IS_NOON,
                          NVL(LATE.MINUTE_DM, 0) MINUTE_DM,
                          NVL(LATE.MINUTE_VS, 0) MINUTE_VS
                     FROM AT_WORKSIGN WSIGN
                    INNER JOIN AT_SHIFT SHIFT
                       ON WSIGN.SHIFT_ID = SHIFT.ID
                     LEFT JOIN (SELECT LATE.EMPLOYEE_ID,
                                      LATE.WORKINGDAY,
                                      SUM(CASE
                                            WHEN LATE.TYPE_DSVM = 168 THEN
                                             LATE.MINUTE
                                            ELSE
                                             0
                                          END) MINUTE_DM,
                                      SUM(CASE
                                            WHEN LATE.TYPE_DSVM = 167 THEN
                                             LATE.MINUTE
                                            ELSE
                                             0
                                          END) MINUTE_VS
                                 FROM AT_LATE_COMBACKOUT LATE
                                WHERE LATE.WORKINGDAY >= PV_FROMDATE
                                  AND LATE.WORKINGDAY <= PV_ENDDATE
                                GROUP BY LATE.EMPLOYEE_ID, LATE.WORKINGDAY) LATE
                       ON WSIGN.EMPLOYEE_ID = LATE.EMPLOYEE_ID
                      AND WSIGN.WORKINGDAY = LATE.WORKINGDAY
                    WHERE WSIGN.WORKINGDAY >= PV_FROMDATE
                      AND WSIGN.WORKINGDAY <= PV_ENDDATE) WSIGN
          ON WSIGN.EMPLOYEE_ID = T.EMPLOYEE_ID;
    -- 2: Kh?ng DK ca
    -- TH1: So lan cham cong la so chan
    INSERT INTO AT_CAL_INOUT_TEMP
      (ID,
       EMPLOYEE_ID,
       WORKINGDAY,
       SHIFT_ID,
       SHIFT_CODE,
       SHIFT_MANUAL_ID,
       SHIFT_HOURS_START,
       SHIFT_HOURS_STOP,
       VALTIME1,
       VALTIME2,
       REQUEST_ID,
       ITIME_ID)
      SELECT SEQ_AT_CAL_INOUT_TEMP.NEXTVAL,
             T.EMPLOYEE_ID,
             WSIGN.WORKINGDAY,
             WSIGN.SHIFT_ID,
             WSIGN.SHIFT_CODE,
             WSIGN.SHIFT_MANUAL_ID,
             WSIGN.HOURS_START,
             WSIGN.HOURS_STOP,
             WSIGN.VALTIME,
             WSIGN.VALTIME_OUT,
             PV_REQUEST_ID,
             T.ITIME_ID
        FROM AT_CHOSEN_EMP T
       INNER JOIN (SELECT T.*,
                          T.ID AS EMPLOYEE_ID,
                          CASE
                            WHEN ATS.HOURS_START IS NOT NULL THEN
                             TO_DATE(TO_CHAR(T.WORKINGDAY, 'yyyymmdd') ||
                                     TO_CHAR(ATS.HOURS_START, 'HH24MI'),
                                     'yyyymmddHH24MI')
                          END HOURS_START,
                          CASE
                            WHEN ATS.HOURS_STOP IS NOT NULL THEN
                             TO_DATE(TO_CHAR(T.WORKINGDAY, 'yyyymmdd') ||
                                     TO_CHAR(ATS.HOURS_STOP, 'HH24MI'),
                                     'yyyymmddHH24MI')
                          END HOURS_STOP,
                          ATS.CODE SHIFT_CODE,
                          ATS.MANUAL_ID SHIFT_MANUAL_ID
                     FROM (SELECT E.*, HU.ID
                             FROM (SELECT T.ITIME_ID,
                                          T.TERMINAL_ID,
                                          T.WORKINGDAY,
                                          T.VALTIME,
                                          FN_SET_SHIFT(extract(hour from
                                                               cast(T.VALTIME as
                                                                    timestamp))) AS SHIFT_ID,
                                          CASE T.STT
                                            WHEN 1 THEN
                                             IO.VALIN2
                                            WHEN 3 THEN
                                             IO.VALIN4
                                            ELSE
                                             NULL
                                          END AS VALTIME_OUT
                                     FROM (SELECT T.ITIME_ID,
                                                  T.TERMINAL_ID,
                                                  T.WORKINGDAY,
                                                  T.VALTIME,
                                                  ROW_NUMBER() OVER(PARTITION BY T.WORKINGDAY ORDER BY T.VALTIME) AS STT
                                             from AT_SWIPE_DATA T
                                            WHERE T.WORKINGDAY >= DATE
                                            '2017-01-01'
                                              AND T.WORKINGDAY <= DATE
                                            '2017-01-31'
                                              AND EXISTS
                                            (SELECT ASD.ITIME_ID
                                                     FROM AT_SWIPE_DATA ASD
                                                    WHERE ASD.ITIME_ID =
                                                          T.ITIME_ID
                                                      AND ASD.WORKINGDAY =
                                                          T.WORKINGDAY
                                                      AND ASD.TERMINAL_ID =
                                                          T.TERMINAL_ID
                                                    GROUP BY ASD.ITIME_ID,
                                                             ASD.WORKINGDAY,
                                                             ASD.TERMINAL_ID
                                                   HAVING MOD(COUNT(*), 2) = 0)) T
                                    INNER JOIN AT_DATA_INOUT IO
                                       ON IO.WORKINGDAY = T.WORKINGDAY
                                      AND IO.ITIME_ID = IO.ITIME_ID
                                    WHERE MOD(T.STT, 2) = 1) E
                            INNER JOIN HU_EMPLOYEE HU
                               ON HU.ITIME_ID = E.ITIME_ID
                            WHERE NOT EXISTS
                            (select T.ID
                                     from at_register_ot T
                                    where T.EMPLOYEE_ID = HU.ID
                                      AND T.WORKINGDAY = E.WORKINGDAY
                                      AND FN_SET_SHIFT(extract(hour from
                                                               cast(T.From_Hour as
                                                                    timestamp))) =
                                          E.SHIFT_ID)) T
                    INNER JOIN AT_SHIFT ATS
                       ON ATS.ID = T.SHIFT_ID) WSIGN
          ON WSIGN.EMPLOYEE_ID = T.EMPLOYEE_ID;
  
    -- TH2: So lan cham cong le va chi thuoc ca 3
    INSERT INTO AT_CAL_INOUT_TEMP
      (ID,
       EMPLOYEE_ID,
       WORKINGDAY,
       SHIFT_ID,
       SHIFT_CODE,
       SHIFT_MANUAL_ID,
       SHIFT_HOURS_START,
       SHIFT_HOURS_STOP,
       VALTIME1,
       REQUEST_ID,
       ITIME_ID)
      SELECT SEQ_AT_CAL_INOUT_TEMP.NEXTVAL,
             T.EMPLOYEE_ID,
             WSIGN.WORKINGDAY,
             WSIGN.SHIFT_ID,
             WSIGN.SHIFT_CODE,
             WSIGN.SHIFT_MANUAL_ID,
             WSIGN.HOURS_START,
             WSIGN.HOURS_STOP,
             WSIGN.VALTIME,
             PV_REQUEST_ID,
             T.ITIME_ID
        FROM AT_CHOSEN_EMP T
       INNER JOIN (SELECT T.*,
                          HU.ID EMPLOYEE_ID,
                          CASE
                            WHEN ATS.HOURS_START IS NOT NULL THEN
                             TO_DATE(TO_CHAR(T.WORKINGDAY, 'yyyymmdd') ||
                                     TO_CHAR(ATS.HOURS_START, 'HH24MI'),
                                     'yyyymmddHH24MI')
                          END HOURS_START,
                          CASE
                            WHEN ATS.HOURS_STOP IS NOT NULL THEN
                             TO_DATE(TO_CHAR(T.WORKINGDAY, 'yyyymmdd') ||
                                     TO_CHAR(ATS.HOURS_STOP, 'HH24MI'),
                                     'yyyymmddHH24MI')
                          END HOURS_STOP,
                          ATS.CODE SHIFT_CODE,
                          ATS.MANUAL_ID SHIFT_MANUAL_ID
                     FROM (SELECT T.ITIME_ID,
                                  T.TERMINAL_ID,
                                  T.WORKINGDAY,
                                  Min(T.VALTIME) AS VALTIME,
                                  FN_SET_SHIFT(extract(hour from
                                                       cast(Min(T.VALTIME) as
                                                            timestamp))) AS SHIFT_ID
                             from AT_SWIPE_DATA T
                            WHERE T.WORKINGDAY >= PV_FROMDATE
                              AND T.WORKINGDAY <= PV_ENDDATE
                            group by T.WORKINGDAY, T.ITIME_ID, T.TERMINAL_ID
                           Having Mod(Count(T.ID), 2) = 1) T
                    INNER JOIN HU_EMPLOYEE HU
                       ON HU.ITIME_ID = T.ITIME_ID
                    INNER JOIN AT_SHIFT ATS
                       ON ATS.ID = T.SHIFT_ID) WSIGN
          ON WSIGN.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND WSIGN.SHIFT_CODE = 'C3';
  
    --Cap nhat cai gi do
    \*UPDATE AT_CAL_INOUT_TEMP WSIGN
    SET WSIGN.VALTIME2 = CASE
                           WHEN NVL(WSIGN.IS_NOON, 0) = -1 THEN
                            (SELECT MIN(INOUT.VALTIME)
                               FROM AT_SWIPE_DATA INOUT
                              WHERE INOUT.ITIME_ID = WSIGN.ITIME_ID
                                AND INOUT.WORKINGDAY = WSIGN.WORKINGDAY
                                AND INOUT.VALTIME >
                                    NVL(WSIGN.VALTIME1, '1/jan/1900')
                                AND INOUT.VALTIME >=
                                    (WSIGN.BREAKS_FORM -
                                    PV_MINUS_ALLOW / 60 / 24)
                                AND INOUT.VALTIME <=
                                    (WSIGN.BREAKS_FORM +
                                    PV_MINUS_ALLOW / 60 / 24))
                         END;*\
  
    \*UPDATE AT_CAL_INOUT_TEMP WSIGN
    SET WSIGN.VALTIME3 = CASE
                           WHEN NVL(WSIGN.IS_NOON, 0) = -1 THEN
  
                            (SELECT MIN(INOUT.VALTIME)
                               FROM AT_SWIPE_DATA INOUT
                              WHERE INOUT.ITIME_ID = WSIGN.ITIME_ID
                                AND INOUT.WORKINGDAY = WSIGN.WORKINGDAY
                                AND INOUT.VALTIME >
                                    NVL(WSIGN.VALTIME2, '1/jan/1900')
                                AND INOUT.VALTIME >
                                    NVL(WSIGN.VALTIME1, '1/jan/1900'))
                         END;*\
  
    \*UPDATE AT_CAL_INOUT_TEMP WSIGN
    SET WSIGN.VALTIME4 = CASE
                           WHEN WSIGN.SHIFT_HOURS_STOP IS NOT NULL AND
                                WSIGN.SHIFT_HOURS_START IS NOT NULL THEN
                            CASE
                              WHEN SHIFT_HOURS_STOP < SHIFT_HOURS_START THEN
                               (SELECT MAX(INOUT.VALTIME)
                                  FROM AT_SWIPE_DATA INOUT
                                 WHERE INOUT.ITIME_ID = WSIGN.ITIME_ID
                                   AND INOUT.WORKINGDAY = WSIGN.WORKINGDAY + 1
                                   AND INOUT.VALTIME <=
                                       (trunc(SHIFT_HOURS_STOP) + 1 + 16/24 +
                                       PV_MINUS_ALLOW / 60 / 24))
                              ELSE
                               (SELECT MAX(INOUT.VALTIME)
                                  FROM AT_SWIPE_DATA INOUT
                                 WHERE INOUT.ITIME_ID = WSIGN.ITIME_ID
                                   AND INOUT.WORKINGDAY = WSIGN.WORKINGDAY
                                   AND INOUT.VALTIME >
                                       NVL(WSIGN.VALTIME3, '1/jan/1900')
                                   AND INOUT.VALTIME >
                                       NVL(WSIGN.VALTIME2, '1/jan/1900')
                                   AND INOUT.VALTIME >
                                       NVL(WSIGN.VALTIME1, '1/jan/1900'))
                            END
                         END;*\
    --> CHUA BO SUNG THEO NGAY NGHI VIEC
    \*INSERT INTO AT_TIME_TIMESHEET_MACHINE_TEMP M
    (ID,
     EMPLOYEE_ID,
     ORG_ID,
     TITLE_ID,
     STAFF_RANK_ID,
     WORKINGDAY,
     SHIFT_ID,
     SHIFT_CODE,
     SHIFT_MANUAL_ID,
     SHIFT_HOURS_START,
     SHIFT_HOURS_STOP,
     BREAKS_FORM,
     BREAKS_TO,
     IS_NOON,
     WORKINGHOUR_SHIFT,
     VALIN1,
     VALIN2,
     VALIN3,
     VALIN4,
     LEAVE_ID,
     LEAVE_CODE,
     IS_HOLIDAY,
     IS_FULLDAY,
     MINUTE_DM,
     MINUTE_VS,
     CREATED_DATE,
     CREATED_BY,
     REQUEST_ID,
     WORKINGHOUR)
    SELECT SEQ_AT_TIME_MACHINE_TEMP.NEXTVAL,
           T.EMPLOYEE_ID,
           T.ORG_ID,
           T.TITLE_ID,
           T.STAFF_RANK_ID,
           CA.CDATE,
           WSIGN.SHIFT_ID,
           WSIGN.SHIFT_CODE,
           WSIGN.SHIFT_MANUAL_ID,
           WSIGN.SHIFT_HOURS_START,
           WSIGN.SHIFT_HOURS_STOP,
           WSIGN.BREAKS_FORM,
           WSIGN.BREAKS_TO,
           WSIGN.IS_NOON,
           WSIGN.WORKINGHOUR_SHIFT,
           WSIGN.VALTIME1,
           WSIGN.VALTIME2,
           WSIGN.VALTIME3,
           WSIGN.VALTIME4,
  
           CASE
             WHEN NVL(WSIGN.SHIFT_MANUAL_ID, 0) = 22 THEN
              22
             ELSE
              LEAVE.MANUAL_ID
           END MANUAL_ID,
  
           CASE
             WHEN NVL(WSIGN.SHIFT_MANUAL_ID, 0) = 22 THEN
              'OFF'
             ELSE
              TO_CHAR(LEAVE.MANUAL_CODE)
           END MANUAL_CODE,
           CASE
             WHEN HOLIDAY.WORKINGDAY IS NOT NULL THEN
              1
             ELSE
              0
           END,
           CASE
             WHEN NVL(WSIGN.SHIFT_MANUAL_ID, 0) = 22 THEN
              1
             ELSE
              LEAVE.IS_FULLDAY
           END IS_FULLDAY,
           WSIGN.MINUTE_DM,
           WSIGN.MINUTE_VS,
           SYSDATE,
           UPPER(P_USERNAME),
           PV_REQUEST_ID,
           PKG_ATTENDANCE_FUNTION.WORKINGHOUR(SHIFT_HOURS_START,
                                              SHIFT_HOURS_STOP,
                                              BREAKS_FORM,
                                              BREAKS_TO,
                                              IS_NOON,
                                              VALTIME1,
                                              VALTIME2,
                                              VALTIME3,
                                              VALTIME4,
                                              MINUTE_DM,
                                              MINUTE_VS)
      FROM AT_CHOSEN_EMP T
     CROSS JOIN TABLE(TABLE_LISTDATE(PV_FROMDATE, PV_ENDDATE)) CA
      LEFT JOIN (SELECT * FROM AT_CAL_INOUT_TEMP WSIGN) WSIGN
        ON T.EMPLOYEE_ID = WSIGN.EMPLOYEE_ID
       AND WSIGN.WORKINGDAY = CA.CDATE
      LEFT JOIN (SELECT T.MANUAL_ID,
                        M.CODE MANUAL_CODE,
                        T.EMPLOYEE_ID,
                        T.WORKINGDAY,
                        CASE
                          WHEN NVL(F.IS_LEAVE, 0) = -1 AND
                               NVL(F2.IS_LEAVE, 0) = -1 THEN
                           1
                          WHEN NVL(F.IS_LEAVE, 0) = -1 OR
                               NVL(F2.IS_LEAVE, 0) = -1 THEN
                           .5
                          ELSE
                           0
                        END IS_FULLDAY
                   FROM AT_LEAVESHEET T
                   LEFT JOIN AT_TIME_MANUAL M
                     ON T.MANUAL_ID = M.ID
                   LEFT JOIN AT_FML F
                     ON M.MORNING_ID = F.ID
                   LEFT JOIN AT_FML F2
                     ON M.AFTERNOON_ID = F2.ID
                  WHERE T.WORKINGDAY >= PV_FROMDATE
                    AND T.WORKINGDAY <= PV_ENDDATE) LEAVE
        ON T.EMPLOYEE_ID = LEAVE.EMPLOYEE_ID
       AND LEAVE.WORKINGDAY = CA.CDATE
      LEFT JOIN (SELECT DISTINCT HOLIDAY.WORKINGDAY
                   FROM AT_HOLIDAY HOLIDAY
                  WHERE HOLIDAY.WORKINGDAY >= PV_FROMDATE
                    AND HOLIDAY.WORKINGDAY <= PV_ENDDATE
                    AND HOLIDAY.ACTFLG = 'A') HOLIDAY
        ON HOLIDAY.WORKINGDAY = CA.CDATE
     WHERE (T.TER_EFFECT_DATE IS NULL OR (T.TER_EFFECT_DATE IS NOT NULL AND
           CA.CDATE < T.TER_EFFECT_DATE))
       AND CA.CDATE >= T.JOIN_DATE;*\
  
    --Insert vao table AT_TIME_TIMESHEET_MACHINE_TEMP
    INSERT INTO AT_TIME_TIMESHEET_MACHINE_TEMP M
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       WORKINGDAY,
       SHIFT_ID,
       SHIFT_CODE,
       SHIFT_MANUAL_ID,
       SHIFT_HOURS_START,
       SHIFT_HOURS_STOP,
       WORKINGHOUR,
       VALIN1,
       VALIN2,
       VALIN3,
       VALIN4,
       CREATED_DATE,
       CREATED_BY,
       REQUEST_ID)
      SELECT SEQ_AT_TIME_MACHINE_TEMP.NEXTVAL,
             T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             T.STAFF_RANK_ID,
             WSIGN.WORKINGDAY,
             WSIGN.SHIFT_ID,
             WSIGN.SHIFT_CODE,
             WSIGN.SHIFT_MANUAL_ID,
             WSIGN.SHIFT_HOURS_START,
             WSIGN.SHIFT_HOURS_STOP,
             CASE
               WHEN WSIGN.SHIFT_HOURS_START >= WSIGN.VALTIME1 THEN
                ROUND((WSIGN.VALTIME2 - WSIGN.SHIFT_HOURS_START) * 24, 2)
               ELSE
                ROUND((WSIGN.VALTIME2 - WSIGN.VALTIME1) * 24, 2)
             END AS WORKINGHOUR,
             WSIGN.VALTIME1,
             WSIGN.VALTIME2,
             WSIGN.VALTIME3,
             WSIGN.VALTIME4,
             SYSDATE,
             UPPER(P_USERNAME),
             PV_REQUEST_ID
        FROM AT_CHOSEN_EMP T
       INNER JOIN (SELECT * FROM AT_CAL_INOUT_TEMP WSIGN) WSIGN
          ON T.EMPLOYEE_ID = WSIGN.EMPLOYEE_ID;
  
    --Tinh lai so gio
    UPDATE AT_TIME_TIMESHEET_MACHINE_TEMP T
       SET T.WORKINGHOUR = (TRUNC(T.WORKINGHOUR)) + CASE
                             WHEN MOD(T.WORKINGHOUR, 1) >= 0.67 THEN
                              1
                             WHEN MOD(T.WORKINGHOUR, 1) >= 0.25 THEN
                              0.5
                             ELSE
                              0
                           END;
    --------------------------------------------------------------------------------------
    -- AP DUNG CONG THUC TINH CHO CAC COT TREN BANG CONG TONG HOP
    --------------------------------------------------------------------------------------
    \*FOR CUR_ITEM IN (SELECT T.FORMULAR_CODE, T.FORMULAR_VALUE
                       FROM AT_TIME_FORMULAR T
                      WHERE T.TYPE = 1
                        AND T.STATUS = 1
                      ORDER BY T.FORMULAR_ID) LOOP
  
      PV_SQL := 'UPDATE AT_TIME_TIMESHEET_MACHINE_TEMP T SET ' ||
                CUR_ITEM.FORMULAR_CODE || '= NVL((' ||
                CUR_ITEM.FORMULAR_VALUE || '),0)';
      EXECUTE IMMEDIATE PV_SQL;
  
    END LOOP;*\
  
    -- XOA DU LIEU CU TRUOC KHI TINH
    DELETE FROM AT_TIME_TIMESHEET_MACHINET D
     WHERE D.WORKINGDAY >= PV_FROMDATE
       AND D.WORKINGDAY <= PV_ENDDATE
       AND D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O);
  
    INSERT INTO AT_TIME_TIMESHEET_MACHINET
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       WORKINGDAY,
       SHIFT_CODE,
       LEAVE_CODE,
       LATE,
       COMEBACKOUT,
       WORKDAY_OT,
       WORKDAY_NIGHT,
       TYPE_DAY,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG,
       WORKINGHOUR,
       VALIN1,
       VALIN2,
       VALIN3,
       VALIN4,
       SHIFT_ID,
       MANUAL_ID,
       LEAVE_ID,
       WORKINGHOUR_SHIFT,
       NUMBER_SWIPE,
       IS_HOLIDAY,
       IS_FULLDAY,
       SHIFT_MANUAL_ID,
       IS_NOON,
       SHIFT_HOURS_START,
       SHIFT_HOURS_STOP,
       BREAKS_FORM,
       BREAKS_TO,
       MINUTE_DM,
       MINUTE_VS)
      SELECT SEQ_AT_TIME_TIMESHEET_MACHINET.NEXTVAL,
             EMPLOYEE_ID,
             ORG_ID,
             TITLE_ID,
             STAFF_RANK_ID,
             WORKINGDAY,
             SHIFT_CODE,
             LEAVE_CODE,
             LATE,
             COMEBACKOUT,
             WORKDAY_OT,
             WORKDAY_NIGHT,
             TYPE_DAY,
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME),
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME),
             WORKINGHOUR,
             VALIN1,
             VALIN2,
             VALIN3,
             VALIN4,
             AW.SHIFT_ID, --
             --MANUAL_ID,
             Case
               When AW.MANUAL_ID = 0 then
                (Case
                  When AW.WORKINGHOUR >= ATS.MINHOURS then
                   ATS.MANUAL_ID
                  else
                   AW.MANUAL_ID
                end)
               else
                AW.MANUAL_ID
             end MANUAL_ID, --HongDX S?a SpinDEX 08/09/2017
             LEAVE_ID,
             AW.WORKINGHOUR_SHIFT,
             AW.NUMBER_SWIPE,
             AW.IS_HOLIDAY,
             AW.IS_FULLDAY,
             AW.SHIFT_MANUAL_ID, --
             AW.IS_NOON,
             AW.SHIFT_HOURS_START,
             AW.SHIFT_HOURS_STOP,
             AW.BREAKS_FORM,
             AW.BREAKS_TO,
             AW.MINUTE_DM,
             AW.MINUTE_VS
        FROM AT_TIME_TIMESHEET_MACHINE_TEMP AW
       INNER JOIN AT_SHIFT ATS
          ON AW.SHIFT_ID = ATS.ID; --HongDX S?a SpinDEX 08/09/2017;
  
    DELETE FROM AT_TIME_TIMESHEET_DAILY D
     WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
       AND D.WORKINGDAY >= PV_FROMDATE
       AND D.WORKINGDAY <= PV_ENDDATE;
  
    DELETE FROM AT_TIME_TIMESHEET_ORIGIN D
     WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
       AND D.WORKINGDAY >= PV_FROMDATE
       AND D.WORKINGDAY <= PV_ENDDATE;
  
    INSERT INTO AT_TIME_TIMESHEET_DAILY T
      (T.ID,
       T.EMPLOYEE_ID,
       T.ORG_ID,
       T.TITLE_ID,
       T.WORKINGDAY,
       T.SHIFT_CODE,
       T.WORKINGHOUR,
       T.WORKINGHOUR_SHIFT,
       T.NUMBER_SWIPE,
       T.SHIFT_ID,
       T.LEAVE_CODE,
       T.MANUAL_ID,
       T.LEAVE_ID,
       T.LATE,
       T.COMEBACKOUT,
       T.VALIN1,
       T.VALIN2,
       T.VALIN3,
       T.VALIN4,
       T.CREATED_DATE,
       T.CREATED_BY)
      SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
             W.EMPLOYEE_ID,
             W.ORG_ID,
             W.TITLE_ID,
             W.WORKINGDAY,
             W.SHIFT_CODE,
             W.WORKINGHOUR,
             W.WORKINGHOUR_SHIFT,
             W.NUMBER_SWIPE,
             W.SHIFT_ID,
             W.LEAVE_CODE,
             Case
               When W.MANUAL_ID = 0 then
                (Case
                  When W.WORKINGHOUR >= ATS.MINHOURS then
                   ATS.MANUAL_ID
                  else
                   W.MANUAL_ID
                end)
               else
                W.MANUAL_ID
             end MANUAL_ID, --HongDX S?a SpinDEX 08/09/2017
             --W.MANUAL_ID, --HongDX S?a SpinDEX 08/09/2017
             W.LEAVE_ID,
             W.LATE,
             W.COMEBACKOUT,
             W.VALIN1,
             W.VALIN2,
             W.VALIN3,
             W.VALIN4,
             SYSDATE,
             UPPER(P_USERNAME)
        FROM AT_TIME_TIMESHEET_MACHINE_TEMP W
       INNER JOIN AT_SHIFT ATS
          ON W.SHIFT_ID = ATS.ID; --HongDX S?a SpinDEX 08/09/2017
  
    INSERT INTO AT_TIME_TIMESHEET_ORIGIN T
      (T.ID,
       T.EMPLOYEE_ID,
       T.ORG_ID,
       T.TITLE_ID,
       T.WORKINGDAY,
       T.SHIFT_CODE,
       T.WORKINGHOUR,
       T.WORKINGHOUR_SHIFT,
       T.NUMBER_SWIPE,
       T.SHIFT_ID,
       T.LEAVE_CODE,
       T.MANUAL_ID,
       T.LEAVE_ID,
       T.LATE,
       T.COMEBACKOUT,
       T.VALIN1,
       T.VALIN2,
       T.VALIN3,
       T.VALIN4,
       T.CREATED_DATE,
       T.CREATED_BY)
      SELECT SEQ_AT_TIME_TIMESHEET_ORIGIN.NEXTVAL,
             W.EMPLOYEE_ID,
             W.ORG_ID,
             W.TITLE_ID,
             W.WORKINGDAY,
             W.SHIFT_CODE,
             W.WORKINGHOUR,
             W.WORKINGHOUR_SHIFT,
             W.NUMBER_SWIPE,
             W.SHIFT_ID,
             W.LEAVE_CODE,
             W.MANUAL_ID,
             W.LEAVE_ID,
             W.LATE,
             W.COMEBACKOUT,
             W.VALIN1,
             W.VALIN2,
             W.VALIN3,
             W.VALIN4,
             SYSDATE,
             UPPER(P_USERNAME)
        FROM AT_TIME_TIMESHEET_MACHINE_TEMP W;
  
    \*DELETE AT_CHOSEN_ORG E WHERE E.REQUEST_ID = PV_REQUEST_ID;
    DELETE AT_CHOSEN_EMP E WHERE E.REQUEST_ID = PV_REQUEST_ID;
    DELETE AT_SWIPE_DATA_TEMP E WHERE E.REQUEST_ID = PV_REQUEST_ID;
    DELETE AT_CAL_INOUT_TEMP E WHERE E.REQUEST_ID = PV_REQUEST_ID;
    DELETE AT_TIME_TIMESHEET_MACHINE_TEMP E WHERE E.REQUEST_ID = PV_REQUEST_ID;*\
  
  END;
  
  
  
  PROCEDURE CAL_TIMETIMESHEET_NB(P_USERNAME   NVARCHAR2,
                                 P_ORG_ID     IN NUMBER,
                                 P_PERIOD_ID  IN NUMBER,
                                 P_ISDISSOLVE IN NUMBER) IS
    PV_FROMDATE    DATE;
    PV_ENDDATE     DATE;
    PV_MAX_PAYOT   NUMBER;
    PV_LEVEL_STAFF NUMBER;
    PV_HOUR_CAL_OT NUMBER;
    PV_REQUEST_ID  NUMBER;
    PV_MINUS_ALLOW NUMBER := 50;
  BEGIN
  
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
    -- TONG HOP DU LIEU TU DANG KY LAM THEM VS KHAI BAO LAM THEM RA BANG LAM THEM TONG HOP
    SELECT P.START_DATE, P.END_DATE
      INTO PV_FROMDATE, PV_ENDDATE
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O
        WHERE EXISTS (SELECT 1
                 FROM AT_ORG_PERIOD OP
                WHERE OP.PERIOD_ID = P_PERIOD_ID
                  AND OP.ORG_ID = O.ORG_ID
                  AND OP.STATUSCOLEX = 1));
  
    -- insert emp can tinh toan
    INSERT INTO AT_CHOSEN_EMP E
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       USERNAME,
       REQUEST_ID)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              UPPER(P_USERNAME),
              PV_REQUEST_ID
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.STAFF_RANK_ID,
                          E.IS_3B,
                          S.LEVEL_STAFF,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_ENDDATE
                      AND E.STATUS_ID = 447
                      AND E.IS_3B = 0) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
           ON O.ORG_ID = W.ORG_ID
        WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  
    INSERT INTO AT_CHOSEN_EMP_CLEAR
      (EMPLOYEE_ID, REQUEST_ID)
      (SELECT EMPLOYEE_ID, PV_REQUEST_ID
         FROM (SELECT A.*,
                      ROW_NUMBER() OVER(PARTITION BY A.EMPLOYEE_ID ORDER BY A.EFFECT_DATE DESC, A.ID DESC) AS ROW_NUMBER
                 FROM HU_WORKING A
                WHERE A.STATUS_ID = 447
                  AND A.EFFECT_DATE <= PV_ENDDATE
                  AND A.IS_3B = 0) C
        INNER JOIN HU_EMPLOYEE EE
           ON C.EMPLOYEE_ID = EE.ID
          AND C.ROW_NUMBER = 1
        WHERE (NVL(EE.WORK_STATUS, 0) <> 257 OR
              (EE.WORK_STATUS = 257 AND EE.TER_LAST_DATE >= PV_FROMDATE)));
  
    --1. TONG HOP GIO LAM THEM KHI DANG KY LAM THEM
  
    SELECT S.HOUR_MAX_OT, S.LEVEL_STAFF, S.HOUR_CAL_OT
      INTO PV_MAX_PAYOT, PV_LEVEL_STAFF, PV_HOUR_CAL_OT
      FROM (SELECT T.HOUR_MAX_OT, K.LEVEL_STAFF, T.HOUR_CAL_OT
              FROM AT_LIST_PARAM_SYSTEM T
              LEFT JOIN HU_STAFF_RANK K
                ON T.RANK_PAY_OT = K.ID
             WHERE T.ACTFLG = 'A'
               AND T.EFFECT_DATE_FROM <= PV_ENDDATE
             ORDER BY ROW_NUMBER() OVER(ORDER BY T.EFFECT_DATE_FROM DESC)) S
     WHERE ROWNUM = 1;
  
    -- DELETE AT_REGISTER_OT_TEMP;
    INSERT INTO AT_REGISTER_OT_TEMP
      (ID,
       EMPLOYEE_ID,
       WORKINGDAY,
       FROM_HOUR,
       TO_HOUR,
       BREAK_HOUR,
       NOTE,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG,
       TYPE_OT,
       HOUR,
       HS_OT,
       TYPE_INPUT,
       IS_NB,
       VAL_IN,
       VAL_OUT)
      SELECT ID,
             EMPLOYEE_ID,
             WORKINGDAY,
             FROM_HOUR,
             TO_HOUR,
             BREAK_HOUR,
             NOTE,
             CREATED_DATE,
             CREATED_BY,
             CREATED_LOG,
             MODIFIED_DATE,
             MODIFIED_BY,
             MODIFIED_LOG,
             TYPE_OT,
             HOUR,
             HS_OT,
             TYPE_INPUT,
             IS_NB,
             VAL_IN,
             VAL_OUT
  
        FROM (SELECT OT.*
                FROM AT_REGISTER_OT OT
               INNER JOIN AT_CHOSEN_EMP EE
                  ON OT.EMPLOYEE_ID = EE.EMPLOYEE_ID
               WHERE OT.WORKINGDAY >= PV_FROMDATE
                 AND OT.WORKINGDAY <= PV_ENDDATE
                 AND nvl(ot.IS_NB, 0) = -1) C;
  
    -- KHOI TAO DDU LIEU
    INSERT INTO AT_TIME_TIMESHEET_NB_TEMP
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       PERIOD_ID,
       FROM_DATE,
       END_DATE,
       D1,
       D2,
       D3,
       D4,
       D5,
       D6,
       D7,
       D8,
       D9,
       D10,
       D11,
       D12,
       D13,
       D14,
       D15,
       D16,
       D17,
       D18,
       D19,
       D20,
       D21,
       D22,
       D23,
       D24,
       D25,
       D26,
       D27,
       D28,
       D29,
       D30,
       D31,
       D1_1,
       D1_15,
       D1_2,
       D1_27,
       D1_3,
       D1_39,
       D2_1,
       D2_15,
       D2_2,
       D2_27,
       D2_3,
       D2_39,
       D3_1,
       D3_15,
       D3_2,
       D3_27,
       D3_3,
       D3_39,
       D4_1,
       D4_15,
       D4_2,
       D4_27,
       D4_3,
       D4_39,
       D5_1,
       D5_15,
       D5_2,
       D5_27,
       D5_3,
       D5_39,
       D6_1,
       D6_15,
       D6_2,
       D6_27,
       D6_3,
       D6_39,
       D7_1,
       D7_15,
       D7_2,
       D7_27,
       D7_3,
       D7_39,
       D8_1,
       D8_15,
       D8_2,
       D8_27,
       D8_3,
       D8_39,
       D9_1,
       D9_15,
       D9_2,
       D9_27,
       D9_3,
       D9_39,
       D10_1,
       D10_15,
       D10_2,
       D10_27,
       D10_3,
       D10_39,
       D11_1,
       D11_15,
       D11_2,
       D11_27,
       D11_3,
       D11_39,
       D12_1,
       D12_15,
       D12_2,
       D12_27,
       D12_3,
       D12_39,
       D13_1,
       D13_15,
       D13_2,
       D13_27,
       D13_3,
       D13_39,
       D14_1,
       D14_15,
       D14_2,
       D14_27,
       D14_3,
       D14_39,
       D15_1,
       D15_15,
       D15_2,
       D15_27,
       D15_3,
       D15_39,
       D16_1,
       D16_15,
       D16_2,
       D16_27,
       D16_3,
       D16_39,
       D17_1,
       D17_15,
       D17_2,
       D17_27,
       D17_3,
       D17_39,
       D18_1,
       D18_15,
       D18_2,
       D18_27,
       D18_3,
       D18_39,
       D19_1,
       D19_15,
       D19_2,
       D19_27,
       D19_3,
       D19_39,
       D20_1,
       D20_15,
       D20_2,
       D20_27,
       D20_3,
       D20_39,
       D21_1,
       D21_15,
       D21_2,
       D21_27,
       D21_3,
       D21_39,
       D22_1,
       D22_15,
       D22_2,
       D22_27,
       D22_3,
       D22_39,
       D23_1,
       D23_15,
       D23_2,
       D23_27,
       D23_3,
       D23_39,
       D24_1,
       D24_15,
       D24_2,
       D24_27,
       D24_3,
       D24_39,
       D25_1,
       D25_15,
       D25_2,
       D25_27,
       D25_3,
       D25_39,
       D26_1,
       D26_15,
       D26_2,
       D26_27,
       D26_3,
       D26_39,
       D27_1,
       D27_15,
       D27_2,
       D27_27,
       D27_3,
       D27_39,
       D28_1,
       D28_15,
       D28_2,
       D28_27,
       D28_3,
       D28_39,
       D29_1,
       D29_15,
       D29_2,
       D29_27,
       D29_3,
       D29_39,
       D30_1,
       D30_15,
       D30_2,
       D30_27,
       D30_3,
       D30_39,
       D31_1,
       D31_15,
       D31_2,
       D31_27,
       D31_3,
       D31_39,
       CREATED_DATE,
       CREATED_BY,
       REQUEST_ID,
       TOTAL_FACTOR1,
       TOTAL_FACTOR1_5,
       TOTAL_FACTOR2,
       TOTAL_FACTOR2_7,
       TOTAL_FACTOR3,
       TOTAL_FACTOR3_9,
       TOTAL_FACTOR_CONVERT)
      SELECT SEQ_AT_TIME_OT_TEMP.NEXTVAL,
             A.EMPLOYEE_ID,
             A.ORG_ID,
             A.TITLE_ID,
             A.STAFF_RANK_ID,
             P_PERIOD_ID,
             PV_FROMDATE,
             PV_ENDDATE,
             NVL(A.D1_1, 0) + NVL(A.D1_15, 0) + NVL(A.D1_2, 0) +
             NVL(A.D1_27, 0) + NVL(A.D1_3, 0) + NVL(A.D1_39, 0) D1,
             NVL(A.D2_1, 0) + NVL(A.D2_15, 0) + NVL(A.D2_2, 0) +
             NVL(A.D2_27, 0) + NVL(A.D2_3, 0) + NVL(A.D2_39, 0) D2,
             NVL(A.D3_1, 0) + NVL(A.D3_15, 0) + NVL(A.D3_2, 0) +
             NVL(A.D3_27, 0) + NVL(A.D3_3, 0) + NVL(A.D3_39, 0) D3,
             NVL(A.D4_1, 0) + NVL(A.D4_15, 0) + NVL(A.D4_2, 0) +
             NVL(A.D4_27, 0) + NVL(A.D4_3, 0) + NVL(A.D4_39, 0) D4,
             NVL(A.D5_1, 0) + NVL(A.D5_15, 0) + NVL(A.D5_2, 0) +
             NVL(A.D5_27, 0) + NVL(A.D5_3, 0) + NVL(A.D5_39, 0) D5,
             NVL(A.D6_1, 0) + NVL(A.D6_15, 0) + NVL(A.D6_2, 0) +
             NVL(A.D6_27, 0) + NVL(A.D6_3, 0) + NVL(A.D6_39, 0) D6,
             NVL(A.D7_1, 0) + NVL(A.D7_15, 0) + NVL(A.D7_2, 0) +
             NVL(A.D7_27, 0) + NVL(A.D7_3, 0) + NVL(A.D7_39, 0) D7,
             NVL(A.D8_1, 0) + NVL(A.D8_15, 0) + NVL(A.D8_2, 0) +
             NVL(A.D8_27, 0) + NVL(A.D8_3, 0) + NVL(A.D8_39, 0) D8,
             NVL(A.D9_1, 0) + NVL(A.D9_15, 0) + NVL(A.D9_2, 0) +
             NVL(A.D9_27, 0) + NVL(A.D9_3, 0) + NVL(A.D9_39, 0) D9,
             NVL(A.D10_1, 0) + NVL(A.D10_15, 0) + NVL(A.D10_2, 0) +
             NVL(A.D10_27, 0) + NVL(A.D10_3, 0) + NVL(A.D10_39, 0) D10,
             NVL(A.D11_1, 0) + NVL(A.D11_15, 0) + NVL(A.D11_2, 0) +
             NVL(A.D11_27, 0) + NVL(A.D11_3, 0) + NVL(A.D11_39, 0) D11,
             NVL(A.D12_1, 0) + NVL(A.D12_15, 0) + NVL(A.D12_2, 0) +
             NVL(A.D12_27, 0) + NVL(A.D12_3, 0) + NVL(A.D12_39, 0) D12,
             NVL(A.D13_1, 0) + NVL(A.D13_15, 0) + NVL(A.D13_2, 0) +
             NVL(A.D13_27, 0) + NVL(A.D13_3, 0) + NVL(A.D13_39, 0) D13,
             NVL(A.D14_1, 0) + NVL(A.D14_15, 0) + NVL(A.D14_2, 0) +
             NVL(A.D14_27, 0) + NVL(A.D14_3, 0) + NVL(A.D14_39, 0) D14,
             NVL(A.D15_1, 0) + NVL(A.D15_15, 0) + NVL(A.D15_2, 0) +
             NVL(A.D15_27, 0) + NVL(A.D15_3, 0) + NVL(A.D15_39, 0) D15,
             NVL(A.D16_1, 0) + NVL(A.D16_15, 0) + NVL(A.D16_2, 0) +
             NVL(A.D16_27, 0) + NVL(A.D16_3, 0) + NVL(A.D16_39, 0) D16,
             NVL(A.D17_1, 0) + NVL(A.D17_15, 0) + NVL(A.D17_2, 0) +
             NVL(A.D17_27, 0) + NVL(A.D17_3, 0) + NVL(A.D17_39, 0) D17,
             NVL(A.D18_1, 0) + NVL(A.D18_15, 0) + NVL(A.D18_2, 0) +
             NVL(A.D18_27, 0) + NVL(A.D18_3, 0) + NVL(A.D18_39, 0) D18,
             NVL(A.D19_1, 0) + NVL(A.D19_15, 0) + NVL(A.D19_2, 0) +
             NVL(A.D19_27, 0) + NVL(A.D19_3, 0) + NVL(A.D19_39, 0) D19,
             NVL(A.D20_1, 0) + NVL(A.D20_15, 0) + NVL(A.D20_2, 0) +
             NVL(A.D20_27, 0) + NVL(A.D20_3, 0) + NVL(A.D20_39, 0) D20,
             NVL(A.D21_1, 0) + NVL(A.D21_15, 0) + NVL(A.D21_2, 0) +
             NVL(A.D21_27, 0) + NVL(A.D21_3, 0) + NVL(A.D21_39, 0) D21,
             NVL(A.D22_1, 0) + NVL(A.D22_15, 0) + NVL(A.D22_2, 0) +
             NVL(A.D22_27, 0) + NVL(A.D22_3, 0) + NVL(A.D22_39, 0) D22,
             NVL(A.D23_1, 0) + NVL(A.D23_15, 0) + NVL(A.D23_2, 0) +
             NVL(A.D23_27, 0) + NVL(A.D23_3, 0) + NVL(A.D23_39, 0) D23,
             NVL(A.D24_1, 0) + NVL(A.D24_15, 0) + NVL(A.D24_2, 0) +
             NVL(A.D24_27, 0) + NVL(A.D24_3, 0) + NVL(A.D24_39, 0) D24,
             NVL(A.D25_1, 0) + NVL(A.D25_15, 0) + NVL(A.D25_2, 0) +
             NVL(A.D25_27, 0) + NVL(A.D25_3, 0) + NVL(A.D25_39, 0) D25,
             NVL(A.D26_1, 0) + NVL(A.D26_15, 0) + NVL(A.D26_2, 0) +
             NVL(A.D26_27, 0) + NVL(A.D26_3, 0) + NVL(A.D26_39, 0) D26,
             NVL(A.D27_1, 0) + NVL(A.D27_15, 0) + NVL(A.D27_2, 0) +
             NVL(A.D27_27, 0) + NVL(A.D27_3, 0) + NVL(A.D27_39, 0) D27,
             NVL(A.D28_1, 0) + NVL(A.D28_15, 0) + NVL(A.D28_2, 0) +
             NVL(A.D28_27, 0) + NVL(A.D28_3, 0) + NVL(A.D28_39, 0) D28,
             NVL(A.D29_1, 0) + NVL(A.D29_15, 0) + NVL(A.D29_2, 0) +
             NVL(A.D29_27, 0) + NVL(A.D29_3, 0) + NVL(A.D29_39, 0) D29,
             NVL(A.D30_1, 0) + NVL(A.D30_15, 0) + NVL(A.D30_2, 0) +
             NVL(A.D30_27, 0) + NVL(A.D30_3, 0) + NVL(A.D30_39, 0) D30,
             NVL(A.D31_1, 0) + NVL(A.D31_15, 0) + NVL(A.D31_2, 0) +
             NVL(A.D31_27, 0) + NVL(A.D31_3, 0) + NVL(A.D31_39, 0) D31,
             A.D1_1,
             A.D1_15,
             A.D1_2,
             A.D1_27,
             A.D1_3,
             A.D1_39,
             A.D2_1,
             A.D2_15,
             A.D2_2,
             A.D2_27,
             A.D2_3,
             A.D2_39,
             A.D3_1,
             A.D3_15,
             A.D3_2,
             A.D3_27,
             A.D3_3,
             A.D3_39,
             A.D4_1,
             A.D4_15,
             A.D4_2,
             A.D4_27,
             A.D4_3,
             A.D4_39,
             A.D5_1,
             A.D5_15,
             A.D5_2,
             A.D5_27,
             A.D5_3,
             A.D5_39,
             A.D6_1,
             A.D6_15,
             A.D6_2,
             A.D6_27,
             A.D6_3,
             A.D6_39,
             A.D7_1,
             A.D7_15,
             A.D7_2,
             A.D7_27,
             A.D7_3,
             A.D7_39,
             A.D8_1,
             A.D8_15,
             A.D8_2,
             A.D8_27,
             A.D8_3,
             A.D8_39,
             A.D9_1,
             A.D9_15,
             A.D9_2,
             A.D9_27,
             A.D9_3,
             A.D9_39,
             A.D10_1,
             A.D10_15,
             A.D10_2,
             A.D10_27,
             A.D10_3,
             A.D10_39,
             A.D11_1,
             A.D11_15,
             A.D11_2,
             A.D11_27,
             A.D11_3,
             A.D11_39,
             A.D12_1,
             A.D12_15,
             A.D12_2,
             A.D12_27,
             A.D12_3,
             A.D12_39,
             A.D13_1,
             A.D13_15,
             A.D13_2,
             A.D13_27,
             A.D13_3,
             A.D13_39,
             A.D14_1,
             A.D14_15,
             A.D14_2,
             A.D14_27,
             A.D14_3,
             A.D14_39,
             A.D15_1,
             A.D15_15,
             A.D15_2,
             A.D15_27,
             A.D15_3,
             A.D15_39,
             A.D16_1,
             A.D16_15,
             A.D16_2,
             A.D16_27,
             A.D16_3,
             A.D16_39,
             A.D17_1,
             A.D17_15,
             A.D17_2,
             A.D17_27,
             A.D17_3,
             A.D17_39,
             A.D18_1,
             A.D18_15,
             A.D18_2,
             A.D18_27,
             A.D18_3,
             A.D18_39,
             A.D19_1,
             A.D19_15,
             A.D19_2,
             A.D19_27,
             A.D19_3,
             A.D19_39,
             A.D20_1,
             A.D20_15,
             A.D20_2,
             A.D20_27,
             A.D20_3,
             A.D20_39,
             A.D21_1,
             A.D21_15,
             A.D21_2,
             A.D21_27,
             A.D21_3,
             A.D21_39,
             A.D22_1,
             A.D22_15,
             A.D22_2,
             A.D22_27,
             A.D22_3,
             A.D22_39,
             A.D23_1,
             A.D23_15,
             A.D23_2,
             A.D23_27,
             A.D23_3,
             A.D23_39,
             A.D24_1,
             A.D24_15,
             A.D24_2,
             A.D24_27,
             A.D24_3,
             A.D24_39,
             A.D25_1,
             A.D25_15,
             A.D25_2,
             A.D25_27,
             A.D25_3,
             A.D25_39,
             A.D26_1,
             A.D26_15,
             A.D26_2,
             A.D26_27,
             A.D26_3,
             A.D26_39,
             A.D27_1,
             A.D27_15,
             A.D27_2,
             A.D27_27,
             A.D27_3,
             A.D27_39,
             A.D28_1,
             A.D28_15,
             A.D28_2,
             A.D28_27,
             A.D28_3,
             A.D28_39,
             A.D29_1,
             A.D29_15,
             A.D29_2,
             A.D29_27,
             A.D29_3,
             A.D29_39,
             A.D30_1,
             A.D30_15,
             A.D30_2,
             A.D30_27,
             A.D30_3,
             A.D30_39,
             A.D31_1,
             A.D31_15,
             A.D31_2,
             A.D31_27,
             A.D31_3,
             A.D31_39,
             TRUNC(SYSDATE),
             UPPER(P_USERNAME),
             PV_REQUEST_ID,
             CASE
               WHEN OT.FACTOR1 = 0 THEN
                NULL
               ELSE
                OT.FACTOR1
             END FACTOR1,
             CASE
               WHEN OT.FACTOR1_5 = 0 THEN
                NULL
               ELSE
                OT.FACTOR1_5
             END FACTOR1_5,
             CASE
               WHEN OT.FACTOR2 = 0 THEN
                NULL
               ELSE
                OT.FACTOR2
             END FACTOR2,
             CASE
               WHEN OT.FACTOR2_7 = 0 THEN
                NULL
               ELSE
                OT.FACTOR2_7
             END FACTOR2_7,
             CASE
               WHEN OT.FACTOR3 = 0 THEN
                NULL
               ELSE
                OT.FACTOR3
             END FACTOR3,
             CASE
               WHEN OT.FACTOR3_9 = 0 THEN
                NULL
               ELSE
                OT.FACTOR3_9
             END FACTOR3_9,
             CASE
               WHEN OT.TOTAL_FACTOR_CONVERT = 0 THEN
                NULL
               ELSE
                OT.TOTAL_FACTOR_CONVERT
             END TOTAL_FACTOR_CONVERT
        FROM (SELECT T.EMPLOYEE_ID,
                     EE.ORG_ID,
                     EE.TITLE_ID,
                     EE.STAFF_RANK_ID,
                     EE.STAFF_RANK_LEVEL,
                     TO_CHAR(T.WORKINGDAY, 'dd') || '_' || CASE
                       WHEN T.HS_OT = 4236 THEN -- 1
                        '1'
                       WHEN T.HS_OT = 4237 THEN -- 1.5
                        '15'
                       WHEN T.HS_OT = 4238 THEN -- 2
                        '2'
                       WHEN T.HS_OT = 4239 THEN -- 2.7
                        '27'
                       WHEN T.HS_OT = 4240 THEN -- 3
                        '3'
                       WHEN T.HS_OT = 4241 THEN -- 3.9
                        '39'
                     END AS DAY,
                     T.HOUR
                FROM AT_REGISTER_OT_TEMP T
               INNER JOIN AT_CHOSEN_EMP EE
                  ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID) T
      PIVOT(SUM(HOUR)
         FOR DAY IN('01_1' AS D1_1,
                    '01_15' AS D1_15,
                    '01_2' AS D1_2,
                    '01_27' AS D1_27,
                    '01_3' AS D1_3,
                    '01_39' AS D1_39,
                    '02_1' AS D2_1,
                    '02_15' AS D2_15,
                    '02_2' AS D2_2,
                    '02_27' AS D2_27,
                    '02_3' AS D2_3,
                    '02_39' AS D2_39,
                    '03_1' AS D3_1,
                    '03_15' AS D3_15,
                    '03_2' AS D3_2,
                    '03_27' AS D3_27,
                    '03_3' AS D3_3,
                    '03_39' AS D3_39,
                    '04_1' AS D4_1,
                    '04_15' AS D4_15,
                    '04_2' AS D4_2,
                    '04_27' AS D4_27,
                    '04_3' AS D4_3,
                    '04_39' AS D4_39,
                    '05_1' AS D5_1,
                    '05_15' AS D5_15,
                    '05_2' AS D5_2,
                    '05_27' AS D5_27,
                    '05_3' AS D5_3,
                    '05_39' AS D5_39,
                    '06_1' AS D6_1,
                    '06_15' AS D6_15,
                    '06_2' AS D6_2,
                    '06_27' AS D6_27,
                    '06_3' AS D6_3,
                    '06_39' AS D6_39,
                    '07_1' AS D7_1,
                    '07_15' AS D7_15,
                    '07_2' AS D7_2,
                    '07_27' AS D7_27,
                    '07_3' AS D7_3,
                    '07_39' AS D7_39,
                    '08_1' AS D8_1,
                    '08_15' AS D8_15,
                    '08_2' AS D8_2,
                    '08_27' AS D8_27,
                    '08_3' AS D8_3,
                    '08_39' AS D8_39,
                    '09_1' AS D9_1,
                    '09_15' AS D9_15,
                    '09_2' AS D9_2,
                    '09_27' AS D9_27,
                    '09_3' AS D9_3,
                    '09_39' AS D9_39,
                    '10_1' AS D10_1,
                    '10_15' AS D10_15,
                    '10_2' AS D10_2,
                    '10_27' AS D10_27,
                    '10_3' AS D10_3,
                    '10_39' AS D10_39,
                    '11_1' AS D11_1,
                    '11_15' AS D11_15,
                    '11_2' AS D11_2,
                    '11_27' AS D11_27,
                    '11_3' AS D11_3,
                    '11_39' AS D11_39,
                    '12_1' AS D12_1,
                    '12_15' AS D12_15,
                    '12_2' AS D12_2,
                    '12_27' AS D12_27,
                    '12_3' AS D12_3,
                    '12_39' AS D12_39,
                    '13_1' AS D13_1,
                    '13_15' AS D13_15,
                    '13_2' AS D13_2,
                    '13_27' AS D13_27,
                    '13_3' AS D13_3,
                    '13_39' AS D13_39,
                    '14_1' AS D14_1,
                    '14_15' AS D14_15,
                    '14_2' AS D14_2,
                    '14_27' AS D14_27,
                    '14_3' AS D14_3,
                    '14_39' AS D14_39,
                    '15_1' AS D15_1,
                    '15_15' AS D15_15,
                    '15_2' AS D15_2,
                    '15_27' AS D15_27,
                    '15_3' AS D15_3,
                    '15_39' AS D15_39,
                    '16_1' AS D16_1,
                    '16_15' AS D16_15,
                    '16_2' AS D16_2,
                    '16_27' AS D16_27,
                    '16_3' AS D16_3,
                    '16_39' AS D16_39,
                    '17_1' AS D17_1,
                    '17_15' AS D17_15,
                    '17_2' AS D17_2,
                    '17_27' AS D17_27,
                    '17_3' AS D17_3,
                    '17_39' AS D17_39,
                    '18_1' AS D18_1,
                    '18_15' AS D18_15,
                    '18_2' AS D18_2,
                    '18_27' AS D18_27,
                    '18_3' AS D18_3,
                    '18_39' AS D18_39,
                    '19_1' AS D19_1,
                    '19_15' AS D19_15,
                    '19_2' AS D19_2,
                    '19_27' AS D19_27,
                    '19_3' AS D19_3,
                    '19_39' AS D19_39,
                    '20_1' AS D20_1,
                    '20_15' AS D20_15,
                    '20_2' AS D20_2,
                    '20_27' AS D20_27,
                    '20_3' AS D20_3,
                    '20_39' AS D20_39,
                    '21_1' AS D21_1,
                    '21_15' AS D21_15,
                    '21_2' AS D21_2,
                    '21_27' AS D21_27,
                    '21_3' AS D21_3,
                    '21_39' AS D21_39,
                    '22_1' AS D22_1,
                    '22_15' AS D22_15,
                    '22_2' AS D22_2,
                    '22_27' AS D22_27,
                    '22_3' AS D22_3,
                    '22_39' AS D22_39,
                    '23_1' AS D23_1,
                    '23_15' AS D23_15,
                    '23_2' AS D23_2,
                    '23_27' AS D23_27,
                    '23_3' AS D23_3,
                    '23_39' AS D23_39,
                    '24_1' AS D24_1,
                    '24_15' AS D24_15,
                    '24_2' AS D24_2,
                    '24_27' AS D24_27,
                    '24_3' AS D24_3,
                    '24_39' AS D24_39,
                    '25_1' AS D25_1,
                    '25_15' AS D25_15,
                    '25_2' AS D25_2,
                    '25_27' AS D25_27,
                    '25_3' AS D25_3,
                    '25_39' AS D25_39,
                    '26_1' AS D26_1,
                    '26_15' AS D26_15,
                    '26_2' AS D26_2,
                    '26_27' AS D26_27,
                    '26_3' AS D26_3,
                    '26_39' AS D26_39,
                    '27_1' AS D27_1,
                    '27_15' AS D27_15,
                    '27_2' AS D27_2,
                    '27_27' AS D27_27,
                    '27_3' AS D27_3,
                    '27_39' AS D27_39,
                    '28_1' AS D28_1,
                    '28_15' AS D28_15,
                    '28_2' AS D28_2,
                    '28_27' AS D28_27,
                    '28_3' AS D28_3,
                    '28_39' AS D28_39,
                    '29_1' AS D29_1,
                    '29_15' AS D29_15,
                    '29_2' AS D29_2,
                    '29_27' AS D29_27,
                    '29_3' AS D29_3,
                    '29_39' AS D29_39,
                    '30_1' AS D30_1,
                    '30_15' AS D30_15,
                    '30_2' AS D30_2,
                    '30_27' AS D30_27,
                    '30_3' AS D30_3,
                    '30_39' AS D30_39,
                    '31_1' AS D31_1,
                    '31_15' AS D31_15,
                    '31_2' AS D31_2,
                    '31_27' AS D31_27,
                    '31_3' AS D31_3,
                    '31_39' AS D31_39)) A
        LEFT JOIN (SELECT OT.EMPLOYEE_ID,
                          NVL(OT.FACTOR1, 0) FACTOR1,
                          NVL(OT.FACTOR1_5, 0) FACTOR1_5,
                          NVL(OT.FACTOR2, 0) FACTOR2,
                          NVL(OT.FACTOR2_7, 0) FACTOR2_7,
                          NVL(OT.FACTOR3, 0) FACTOR3,
                          NVL(OT.FACTOR3_9, 0) FACTOR3_9,
                          ROUND(NVL(OT.FACTOR1, 0) * 1 +
                                NVL(OT.FACTOR1_5, 0) * 1.5 +
                                NVL(OT.FACTOR2, 0) * 2 +
                                NVL(OT.FACTOR2_7, 0) * 2.7 +
                                NVL(OT.FACTOR3, 0) * 3 +
                                NVL(OT.FACTOR3_9, 0) * 3.9,
                                2) TOTAL_FACTOR_CONVERT
                     FROM (SELECT T.EMPLOYEE_ID,
                                  SUM(NVL(CASE
                                            WHEN T.HS_OT = 4236 AND
                                                 NVL(T.HOUR, 0) * 60 >=
                                                 NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                             T.HOUR
                                          END,
                                          0)) FACTOR1,
                                  SUM(NVL(CASE
                                            WHEN T.HS_OT = 4237 AND
                                                 NVL(T.HOUR, 0) * 60 >=
                                                 NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                             T.HOUR
                                          END,
                                          0)) FACTOR1_5,
                                  SUM(NVL(CASE
                                            WHEN T.HS_OT = 4238 AND
                                                 NVL(T.HOUR, 0) * 60 >=
                                                 NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                             T.HOUR
                                          END,
                                          0)) FACTOR2,
                                  SUM(NVL(CASE
                                            WHEN T.HS_OT = 4239 AND
                                                 NVL(T.HOUR, 0) * 60 >=
                                                 NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                             T.HOUR
                                          END,
                                          0)) FACTOR2_7,
                                  SUM(NVL(CASE
                                            WHEN T.HS_OT = 4240 AND
                                                 NVL(T.HOUR, 0) * 60 >=
                                                 NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                             T.HOUR
                                          END,
                                          0)) FACTOR3,
                                  SUM(NVL(CASE
                                            WHEN T.HS_OT = 4241 AND
                                                 NVL(T.HOUR, 0) * 60 >=
                                                 NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                             T.HOUR
                                          END,
                                          0)) FACTOR3_9
                             FROM AT_REGISTER_OT_TEMP T
                            GROUP BY T.EMPLOYEE_ID) OT) OT
          ON A.EMPLOYEE_ID = OT.EMPLOYEE_ID;
  
    -- XOA DU LIEU CU TRUOC KHI TINH
    DELETE FROM AT_TIME_TIMESHEET_NB D
     WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
       AND D.PERIOD_ID = P_PERIOD_ID;
  
    INSERT INTO AT_TIME_TIMESHEET_NB
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       PERIOD_ID,
       TOTAL_FACTOR1,
       TOTAL_FACTOR1_5,
       TOTAL_FACTOR2,
       TOTAL_FACTOR2_7,
       TOTAL_FACTOR3,
       TOTAL_FACTOR3_9,
       TOTAL_FACTOR_CONVERT,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       D1,
       D2,
       D3,
       D4,
       D5,
       D6,
       D7,
       D8,
       D9,
       D10,
       D11,
       D12,
       D13,
       D14,
       D15,
       D16,
       D17,
       D18,
       D19,
       D20,
       D21,
       D22,
       D23,
       D24,
       D25,
       D26,
       D27,
       D28,
       D29,
       D30,
       D31,
       D1_1,
       D1_15,
       D1_2,
       D1_27,
       D1_3,
       D1_39,
       D2_1,
       D2_15,
       D2_2,
       D2_27,
       D2_3,
       D2_39,
       D3_1,
       D3_15,
       D3_2,
       D3_27,
       D3_3,
       D3_39,
       D4_1,
       D4_15,
       D4_2,
       D4_27,
       D4_3,
       D4_39,
       D5_1,
       D5_15,
       D5_2,
       D5_27,
       D5_3,
       D5_39,
       D6_1,
       D6_15,
       D6_2,
       D6_27,
       D6_3,
       D6_39,
       D7_1,
       D7_15,
       D7_2,
       D7_27,
       D7_3,
       D7_39,
       D8_1,
       D8_15,
       D8_2,
       D8_27,
       D8_3,
       D8_39,
       D9_1,
       D9_15,
       D9_2,
       D9_27,
       D9_3,
       D9_39,
       D10_1,
       D10_15,
       D10_2,
       D10_27,
       D10_3,
       D10_39,
       D11_1,
       D11_15,
       D11_2,
       D11_27,
       D11_3,
       D11_39,
       D12_1,
       D12_15,
       D12_2,
       D12_27,
       D12_3,
       D12_39,
       D13_1,
       D13_15,
       D13_2,
       D13_27,
       D13_3,
       D13_39,
       D14_1,
       D14_15,
       D14_2,
       D14_27,
       D14_3,
       D14_39,
       D15_1,
       D15_15,
       D15_2,
       D15_27,
       D15_3,
       D15_39,
       D16_1,
       D16_15,
       D16_2,
       D16_27,
       D16_3,
       D16_39,
       D17_1,
       D17_15,
       D17_2,
       D17_27,
       D17_3,
       D17_39,
       D18_1,
       D18_15,
       D18_2,
       D18_27,
       D18_3,
       D18_39,
       D19_1,
       D19_15,
       D19_2,
       D19_27,
       D19_3,
       D19_39,
       D20_1,
       D20_15,
       D20_2,
       D20_27,
       D20_3,
       D20_39,
       D21_1,
       D21_15,
       D21_2,
       D21_27,
       D21_3,
       D21_39,
       D22_1,
       D22_15,
       D22_2,
       D22_27,
       D22_3,
       D22_39,
       D23_1,
       D23_15,
       D23_2,
       D23_27,
       D23_3,
       D23_39,
       D24_1,
       D24_15,
       D24_2,
       D24_27,
       D24_3,
       D24_39,
       D25_1,
       D25_15,
       D25_2,
       D25_27,
       D25_3,
       D25_39,
       D26_1,
       D26_15,
       D26_2,
       D26_27,
       D26_3,
       D26_39,
       D27_1,
       D27_15,
       D27_2,
       D27_27,
       D27_3,
       D27_39,
       D28_1,
       D28_15,
       D28_2,
       D28_27,
       D28_3,
       D28_39,
       D29_1,
       D29_15,
       D29_2,
       D29_27,
       D29_3,
       D29_39,
       D30_1,
       D30_15,
       D30_2,
       D30_27,
       D30_3,
       D30_39,
       D31_1,
       D31_15,
       D31_2,
       D31_27,
       D31_3,
       D31_39,
       FROM_DATE,
       END_DATE,
       BACKUP_MONTH_BEFORE,
       GHINHAN_OT)
      SELECT SEQ_AT_TIME_TIMESHEET_OT.NEXTVAL,
             EMPLOYEE_ID,
             ORG_ID,
             TITLE_ID,
             STAFF_RANK_ID,
             PERIOD_ID,
             TOTAL_FACTOR1,
             TOTAL_FACTOR1_5,
             TOTAL_FACTOR2,
             TOTAL_FACTOR2_7,
             TOTAL_FACTOR3,
             TOTAL_FACTOR3_9,
             TOTAL_FACTOR_CONVERT,
             CREATED_DATE,
             CREATED_BY,
             CREATED_LOG,
             CASE
               WHEN NVL(D1, 0) = 0 THEN
                NULL
               ELSE
                D1
             END D1,
             CASE
               WHEN NVL(D2, 0) = 0 THEN
                NULL
               ELSE
                D2
             END D2,
             CASE
               WHEN NVL(D3, 0) = 0 THEN
                NULL
               ELSE
                D3
             END D3,
             CASE
               WHEN NVL(D4, 0) = 0 THEN
                NULL
               ELSE
                D4
             END D4,
             CASE
               WHEN NVL(D5, 0) = 0 THEN
                NULL
               ELSE
                D5
             END D5,
             CASE
               WHEN NVL(D6, 0) = 0 THEN
                NULL
               ELSE
                D6
             END D6,
             CASE
               WHEN NVL(D7, 0) = 0 THEN
                NULL
               ELSE
                D7
             END D7,
             CASE
               WHEN NVL(D8, 0) = 0 THEN
                NULL
               ELSE
                D8
             END D8,
             CASE
               WHEN NVL(D9, 0) = 0 THEN
                NULL
               ELSE
                D9
             END D9,
             CASE
               WHEN NVL(D10, 0) = 0 THEN
                NULL
               ELSE
                D10
             END D10,
             CASE
               WHEN NVL(D11, 0) = 0 THEN
                NULL
               ELSE
                D11
             END D11,
             CASE
               WHEN NVL(D12, 0) = 0 THEN
                NULL
               ELSE
                D12
             END D12,
             CASE
               WHEN NVL(D13, 0) = 0 THEN
                NULL
               ELSE
                D13
             END D13,
             CASE
               WHEN NVL(D14, 0) = 0 THEN
                NULL
               ELSE
                D14
             END D14,
             CASE
               WHEN NVL(D15, 0) = 0 THEN
                NULL
               ELSE
                D15
             END D15,
             CASE
               WHEN NVL(D16, 0) = 0 THEN
                NULL
               ELSE
                D16
             END D16,
             CASE
               WHEN NVL(D17, 0) = 0 THEN
                NULL
               ELSE
                D17
             END D17,
             CASE
               WHEN NVL(D18, 0) = 0 THEN
                NULL
               ELSE
                D18
             END D18,
             CASE
               WHEN NVL(D19, 0) = 0 THEN
                NULL
               ELSE
                D19
             END D19,
             CASE
               WHEN NVL(D20, 0) = 0 THEN
                NULL
               ELSE
                D20
             END D20,
             CASE
               WHEN NVL(D21, 0) = 0 THEN
                NULL
               ELSE
                D21
             END D21,
             CASE
               WHEN NVL(D22, 0) = 0 THEN
                NULL
               ELSE
                D22
             END D22,
             CASE
               WHEN NVL(D23, 0) = 0 THEN
                NULL
               ELSE
                D23
             END D23,
             CASE
               WHEN NVL(D24, 0) = 0 THEN
                NULL
               ELSE
                D24
             END D24,
             CASE
               WHEN NVL(D25, 0) = 0 THEN
                NULL
               ELSE
                D25
             END D25,
             CASE
               WHEN NVL(D26, 0) = 0 THEN
                NULL
               ELSE
                D26
             END D26,
             CASE
               WHEN NVL(D27, 0) = 0 THEN
                NULL
               ELSE
                D27
             END D27,
             CASE
               WHEN NVL(D28, 0) = 0 THEN
                NULL
               ELSE
                D28
             END D28,
             CASE
               WHEN NVL(D29, 0) = 0 THEN
                NULL
               ELSE
                D29
             END D29,
             CASE
               WHEN NVL(D30, 0) = 0 THEN
                NULL
               ELSE
                D30
             END D30,
             CASE
               WHEN NVL(D31, 0) = 0 THEN
                NULL
               ELSE
                D31
             END D31,
             D1_1,
             D1_15,
             D1_2,
             D1_27,
             D1_3,
             D1_39,
             D2_1,
             D2_15,
             D2_2,
             D2_27,
             D2_3,
             D2_39,
             D3_1,
             D3_15,
             D3_2,
             D3_27,
             D3_3,
             D3_39,
             D4_1,
             D4_15,
             D4_2,
             D4_27,
             D4_3,
             D4_39,
             D5_1,
             D5_15,
             D5_2,
             D5_27,
             D5_3,
             D5_39,
             D6_1,
             D6_15,
             D6_2,
             D6_27,
             D6_3,
             D6_39,
             D7_1,
             D7_15,
             D7_2,
             D7_27,
             D7_3,
             D7_39,
             D8_1,
             D8_15,
             D8_2,
             D8_27,
             D8_3,
             D8_39,
             D9_1,
             D9_15,
             D9_2,
             D9_27,
             D9_3,
             D9_39,
             D10_1,
             D10_15,
             D10_2,
             D10_27,
             D10_3,
             D10_39,
             D11_1,
             D11_15,
             D11_2,
             D11_27,
             D11_3,
             D11_39,
             D12_1,
             D12_15,
             D12_2,
             D12_27,
             D12_3,
             D12_39,
             D13_1,
             D13_15,
             D13_2,
             D13_27,
             D13_3,
             D13_39,
             D14_1,
             D14_15,
             D14_2,
             D14_27,
             D14_3,
             D14_39,
             D15_1,
             D15_15,
             D15_2,
             D15_27,
             D15_3,
             D15_39,
             D16_1,
             D16_15,
             D16_2,
             D16_27,
             D16_3,
             D16_39,
             D17_1,
             D17_15,
             D17_2,
             D17_27,
             D17_3,
             D17_39,
             D18_1,
             D18_15,
             D18_2,
             D18_27,
             D18_3,
             D18_39,
             D19_1,
             D19_15,
             D19_2,
             D19_27,
             D19_3,
             D19_39,
             D20_1,
             D20_15,
             D20_2,
             D20_27,
             D20_3,
             D20_39,
             D21_1,
             D21_15,
             D21_2,
             D21_27,
             D21_3,
             D21_39,
             D22_1,
             D22_15,
             D22_2,
             D22_27,
             D22_3,
             D22_39,
             D23_1,
             D23_15,
             D23_2,
             D23_27,
             D23_3,
             D23_39,
             D24_1,
             D24_15,
             D24_2,
             D24_27,
             D24_3,
             D24_39,
             D25_1,
             D25_15,
             D25_2,
             D25_27,
             D25_3,
             D25_39,
             D26_1,
             D26_15,
             D26_2,
             D26_27,
             D26_3,
             D26_39,
             D27_1,
             D27_15,
             D27_2,
             D27_27,
             D27_3,
             D27_39,
             D28_1,
             D28_15,
             D28_2,
             D28_27,
             D28_3,
             D28_39,
             D29_1,
             D29_15,
             D29_2,
             D29_27,
             D29_3,
             D29_39,
             D30_1,
             D30_15,
             D30_2,
             D30_27,
             D30_3,
             D30_39,
             D31_1,
             D31_15,
             D31_2,
             D31_27,
             D31_3,
             D31_39,
             FROM_DATE,
             END_DATE,
             BACKUP_MONTH_BEFORE,
             GHINHAN_OT
        FROM AT_TIME_TIMESHEET_NB_TEMP E;
  
    DELETE AT_TIME_TIMESHEET_NB E
     WHERE E.PERIOD_ID = P_PERIOD_ID
       AND E.EMPLOYEE_ID NOT IN
           (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);
  
  END;
  
  PROCEDURE CAL_TIME_TIMESHEET_RICE(P_USERNAME   NVARCHAR2,
                                    P_ORG_ID     IN NUMBER,
                                    P_PERIOD_ID  IN NUMBER,
                                    P_ISDISSOLVE IN NUMBER) IS
    PV_FROMDATE   DATE;
    PV_ENDDATE    DATE;
    PV_REQUEST_ID NUMBER;
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
    SELECT P.START_DATE, P.END_DATE
      INTO PV_FROMDATE, PV_ENDDATE
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
    -- XOA DU LIEU CU TRUOC KHI TINH
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O
        WHERE EXISTS (SELECT 1
                 FROM AT_ORG_PERIOD OP
                WHERE OP.PERIOD_ID = P_PERIOD_ID
                  AND OP.ORG_ID = O.ORG_ID
                  AND OP.STATUSCOLEX = 1));
  
    -- insert emp can tinh toan
    INSERT INTO AT_CHOSEN_EMP E
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       USERNAME,
       REQUEST_ID)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              UPPER(P_USERNAME),
              PV_REQUEST_ID
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.STAFF_RANK_ID,
                          E.IS_3B,
                          S.LEVEL_STAFF,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_ENDDATE
                      AND E.STATUS_ID = 447
                      AND E.IS_3B = 0) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
           ON O.ORG_ID = W.ORG_ID
        WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  
    INSERT INTO AT_CHOSEN_EMP_CLEAR
      (EMPLOYEE_ID, REQUEST_ID)
      (SELECT EMPLOYEE_ID, PV_REQUEST_ID
         FROM (SELECT A.*,
                      ROW_NUMBER() OVER(PARTITION BY A.EMPLOYEE_ID ORDER BY A.EFFECT_DATE DESC, A.ID DESC) AS ROW_NUMBER
                 FROM HU_WORKING A
                WHERE A.STATUS_ID = 447
                  AND A.EFFECT_DATE <= PV_ENDDATE
                  AND A.IS_3B = 0) C
        INNER JOIN HU_EMPLOYEE EE
           ON C.EMPLOYEE_ID = EE.ID
          AND C.ROW_NUMBER = 1
        WHERE (NVL(EE.WORK_STATUS, 0) <> 257 OR
              (EE.WORK_STATUS = 257 AND EE.TER_LAST_DATE >= PV_FROMDATE)));
  
    DELETE FROM AT_TIME_TIMESHEET_RICE D
     WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
       AND D.PERIOD_ID = P_PERIOD_ID;
  
    INSERT INTO AT_TIME_RICE_TEM
      SELECT C.EMPLOYEE_ID,
             C.WORKINGDAY,
             C.DAY,
             C.ORG_ID,
             P_PERIOD_ID,
             PV_FROMDATE,
             PV_ENDDATE,
             C.MANUAL_ID,
             CASE
               WHEN C.MANUAL_ID > 0 THEN
                (SELECT NVL(P.PRICE, 0)
                   FROM PA_PRICE_LUNCH P
                  INNER JOIN PA_ORG_LUNCH L
                     ON P.ID = L.LUNCH_ID
                  WHERE L.ORG_ID = C.ORG_ID
                    AND C.WORKINGDAY >= P.EFFECT_DATE
                    AND C.WORKINGDAY <= P.EXPIRE_DATE
                    AND ROWNUM = 1) * C.MANUAL_ID
               ELSE
                0
             END PRICE,
             C.TITLE_ID,
             C.STAFF_RANK_ID
        FROM (SELECT D.EMPLOYEE_ID,
                     D.WORKINGDAY,
                     TO_NUMBER(TO_CHAR(D.WORKINGDAY, 'dd')) AS DAY,
                     E.ORG_ID,
                     E.TITLE_ID,
                     E.STAFF_RANK_ID,
                     CASE
                       WHEN ((WORKSIGN.SHIFT_ID IS NULL) OR
                            (WORKSIGN.SHIFT_ID IS NOT NULL AND
                            WORKSIGN.SHIFT_ID <> 81 AND
                            NVL(SHIFT.NVALUE, 0) > 4)) THEN
                        1
                       ELSE
                        0
                     END MANUAL_ID
                FROM AT_TIME_TIMESHEET_DAILY D
               INNER JOIN AT_TIME_MANUAL M
                  ON D.MANUAL_ID = M.ID
                 AND M.IS_PAID_RICE = -1
                LEFT JOIN AT_WORKSIGN WORKSIGN
                  ON D.WORKINGDAY = WORKSIGN.WORKINGDAY
                 AND D.EMPLOYEE_ID = WORKSIGN.EMPLOYEE_ID
                LEFT JOIN (SELECT CASE
                                   WHEN SHIFT.HOURS_STOP IS NOT NULL AND
                                        SHIFT.HOURS_START IS NOT NULL THEN
                                    CASE
                                      WHEN SHIFT.HOURS_STOP < SHIFT.HOURS_START THEN
                                       (SHIFT.HOURS_STOP + 1 - SHIFT.HOURS_START) * 24
                                      ELSE
                                       (SHIFT.HOURS_STOP - SHIFT.HOURS_START) * 24
                                    END
                                   ELSE
                                    0
                                 END - CASE
                                   WHEN SHIFT.BREAKS_FORM IS NOT NULL AND
                                        SHIFT.BREAKS_TO IS NOT NULL THEN
                                    (SHIFT.BREAKS_TO - SHIFT.BREAKS_FORM) * 24
                                   ELSE
                                    0
                                 END NVALUE,
                                 SHIFT.ID SHIFT_ID
                            FROM AT_SHIFT SHIFT) SHIFT
                  ON WORKSIGN.SHIFT_ID = SHIFT.SHIFT_ID
                LEFT JOIN AT_FML F
                  ON F.ID = M.MORNING_ID
                LEFT JOIN AT_FML F2
                  ON F2.ID = M.AFTERNOON_ID
               INNER JOIN AT_CHOSEN_EMP E
                  ON D.EMPLOYEE_ID = E.EMPLOYEE_ID
               WHERE D.WORKINGDAY >= PV_FROMDATE
                 AND D.WORKINGDAY <= PV_ENDDATE) C;
  
    -- KHOI TAO DDU LIEU
    INSERT INTO AT_TIME_TIMESHEET_RICE_TEM
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       PERIOD_ID,
       FROM_DATE,
       END_DATE,
       D1,
       D2,
       D3,
       D4,
       D5,
       D6,
       D7,
       D8,
       D9,
       D10,
       D11,
       D12,
       D13,
       D14,
       D15,
       D16,
       D17,
       D18,
       D19,
       D20,
       D21,
       D22,
       D23,
       D24,
       D25,
       D26,
       D27,
       D28,
       D29,
       D30,
       D31,
       CREATED_DATE,
       CREATED_BY,
       NDAY_RICE,
       TOTAL_RICE_PRICE,
       TOTAL_RICE_DECLARE,
       TOTAL_RICE)
      SELECT SEQ_AT_TIME_TIMESHEET_RICE.NEXTVAL,
             A.EMPLOYEE_ID,
             A.ORG_ID,
             A.TITLE_ID,
             A.STAFF_RANK_ID,
             P_PERIOD_ID,
             PV_FROMDATE,
             PV_ENDDATE,
             A.D1,
             A.D2,
             A.D3,
             A.D4,
             A.D5,
             A.D6,
             A.D7,
             A.D8,
             A.D9,
             A.D10,
             A.D11,
             A.D12,
             A.D13,
             A.D14,
             A.D15,
             A.D16,
             A.D17,
             A.D18,
             A.D19,
             A.D20,
             A.D21,
             A.D22,
             A.D23,
             A.D24,
             A.D25,
             A.D26,
             A.D27,
             A.D28,
             A.D29,
             A.D30,
             A.D31,
             SYSDATE,
             UPPER(P_USERNAME),
             NVL(D.NDAY_RICE, 0),
             NVL(RICE_PRICE.TOTAL_RICE_PRICE, 0),
             NVL(RICE_DECLARE.TOTAL_RICE_DECLARE, 0),
             NVL(RICE_PRICE.TOTAL_RICE_PRICE, 0) +
             NVL(RICE_DECLARE.TOTAL_RICE_DECLARE, 0)
        FROM (SELECT E.EMPLOYEE_ID,
                     E.ORG_ID,
                     E1.DAY,
                     E1.MANUAL_ID,
                     E.TITLE_ID,
                     E.STAFF_RANK_ID
                FROM AT_CHOSEN_EMP E
                LEFT JOIN (SELECT D.EMPLOYEE_ID,
                                 D.ORG_ID,
                                 D.DAYS DAY,
                                 D.MANUAL_ID,
                                 D.TITLE_ID,
                                 D.STAFF_RANK_ID
                            FROM AT_TIME_RICE_TEM D) E1
                  ON E.EMPLOYEE_ID = E1.EMPLOYEE_ID)
      PIVOT(MAX(MANUAL_ID)
         FOR DAY IN(1 AS D1,
                    2 AS D2,
                    3 AS D3,
                    4 AS D4,
                    5 AS D5,
                    6 AS D6,
                    7 AS D7,
                    8 AS D8,
                    9 AS D9,
                    10 AS D10,
                    11 AS D11,
                    12 AS D12,
                    13 AS D13,
                    14 AS D14,
                    15 AS D15,
                    16 AS D16,
                    17 AS D17,
                    18 AS D18,
                    19 AS D19,
                    20 AS D20,
                    21 AS D21,
                    22 AS D22,
                    23 AS D23,
                    24 AS D24,
                    25 AS D25,
                    26 AS D26,
                    27 AS D27,
                    28 AS D28,
                    29 AS D29,
                    30 AS D30,
                    31 AS D31)) A
        LEFT JOIN (SELECT D.EMPLOYEE_ID,
                          SUM(CASE
                                WHEN ((WORKSIGN.SHIFT_ID IS NULL) OR
                                     (WORKSIGN.SHIFT_ID IS NOT NULL AND
                                     WORKSIGN.SHIFT_ID <> 81 AND
                                     NVL(SHIFT.NVALUE, 0) > 4)) THEN
                                 1
                                ELSE
                                 0
                              END) NDAY_RICE
                     FROM AT_TIME_TIMESHEET_DAILY D
                     LEFT JOIN AT_WORKSIGN WORKSIGN
                       ON D.WORKINGDAY = WORKSIGN.WORKINGDAY
                      AND D.EMPLOYEE_ID = WORKSIGN.EMPLOYEE_ID
                     LEFT JOIN (SELECT CASE
                                        WHEN SHIFT.HOURS_STOP IS NOT NULL AND
                                             SHIFT.HOURS_START IS NOT NULL THEN
                                         CASE
                                           WHEN SHIFT.HOURS_STOP <
                                                SHIFT.HOURS_START THEN
                                            (SHIFT.HOURS_STOP + 1 -
                                            SHIFT.HOURS_START) * 24
                                           ELSE
                                            (SHIFT.HOURS_STOP -
                                            SHIFT.HOURS_START) * 24
                                         END
                                        ELSE
                                         0
                                      END - CASE
                                        WHEN SHIFT.BREAKS_FORM IS NOT NULL AND
                                             SHIFT.BREAKS_TO IS NOT NULL THEN
                                         (SHIFT.BREAKS_TO - SHIFT.BREAKS_FORM) * 24
                                        ELSE
                                         0
                                      END NVALUE,
                                      SHIFT.ID SHIFT_ID
                                 FROM AT_SHIFT SHIFT) SHIFT
                       ON WORKSIGN.SHIFT_ID = SHIFT.SHIFT_ID
                    INNER JOIN AT_TIME_MANUAL M
                       ON D.MANUAL_ID = M.ID
                     LEFT JOIN AT_FML F
                       ON F.ID = M.MORNING_ID
                     LEFT JOIN AT_FML F2
                       ON F2.ID = M.AFTERNOON_ID
                    WHERE D.WORKINGDAY >= PV_FROMDATE
                      AND D.WORKINGDAY <= PV_ENDDATE
                      AND M.IS_PAID_RICE = -1
                    GROUP BY D.EMPLOYEE_ID) D
          ON D.EMPLOYEE_ID = A.EMPLOYEE_ID
        LEFT JOIN (SELECT O.EMPLOYEE_ID,
                          SUM(NVL(O.PRICE_GENERAL, 0)) TOTAL_RICE_PRICE
                     FROM AT_TIME_RICE_TEM O
                    GROUP BY O.EMPLOYEE_ID) RICE_PRICE
          ON RICE_PRICE.EMPLOYEE_ID = A.EMPLOYEE_ID
        LEFT JOIN (SELECT C.EMPLOYEE_ID,
                          SUM(NVL(C.PRICE, 0)) TOTAL_RICE_DECLARE
                     FROM AT_TIME_RICE C
                    WHERE C.WORKINGDAY >= PV_FROMDATE
                      AND C.WORKINGDAY <= PV_ENDDATE
                    GROUP BY C.EMPLOYEE_ID) RICE_DECLARE
          ON RICE_DECLARE.EMPLOYEE_ID = A.EMPLOYEE_ID;
  
    ----------------------------------------------------------------
    -- TONG HOP CONG COM
    --------------------------------------------------------------
    INSERT INTO AT_TIME_TIMESHEET_RICE T
      SELECT * FROM AT_TIME_TIMESHEET_RICE_TEM T1;
  
    DELETE AT_TIME_TIMESHEET_RICE E
     WHERE E.PERIOD_ID = P_PERIOD_ID
       AND E.EMPLOYEE_ID NOT IN
           (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);
  
  END;*/

  PROCEDURE DELETE_WORKSIGN(P_EMPLOYEE_ID IN NVARCHAR2,
                            P_FROM        IN DATE,
                            P_TO          IN DATE) IS
  BEGIN
    DELETE AT_WORKSIGN A
     WHERE INSTR(',' || P_EMPLOYEE_ID || ',', ',' || A.EMPLOYEE_ID || ',') > 0
          --A.EMPLOYEE_ID IN (P_EMPLOYEE_ID)
       AND TO_CHAR(A.WORKINGDAY, 'YYYYMMDD') >= TO_CHAR(P_FROM, 'YYYYMMDD')
       AND TO_CHAR(A.WORKINGDAY, 'YYYYMMDD') <= TO_CHAR(P_TO, 'YYYYMMDD');
  
    DELETE TEMP_COL T WHERE T.COL_INDEX = 113;
    COMMIT;
  END;

  PROCEDURE GET_WORKSIGN(P_USERNAME      IN NVARCHAR2,
                         P_ORG_ID        IN NUMBER,
                         P_ISDISSOLVE    IN NUMBER,
                         P_PAGE_INDEX    IN NUMBER,
                         P_EMPLOYEE_CODE IN VARCHAR2,
                         P_PAGE_SIZE     IN NUMBER,
                         P_PERIOD_ID     IN NUMBER,
                         P_EMP_OBJ       in number,
                         P_START_DATE    in DATE,
                         P_END_DATE      in DATE,
                         P_CUR           OUT CURSOR_TYPE,
                         P_CURCOUNR      OUT CURSOR_TYPE) IS
    PV_STARTDATE DATE;
    PV_ENDDATE   DATE;
    PV_SEQ       NUMBER := 0;
    V_COL        CLOB;
    V_COL_V      CLOB;
    V_COL1       CLOB;
    PV_DAYEND    NUMBER;
    PV_SQL       CLOB;
    PV_EMP_OBJ   NVARCHAR2(1000);
  BEGIN
    if P_EMP_OBJ is not null then
      PV_EMP_OBJ := 'WK.object_employee_id= ' || P_EMP_OBJ;
    else
      PV_EMP_OBJ := '1=1';
    end if;
  
    SELECT SEQ_ORG_TEMP_TABLE.NEXTVAL INTO PV_SEQ FROM DUAL;
    IF P_START_DATE IS NULL and P_END_DATE is null THEN
      PV_STARTDATE := TO_DATE('01/01/2016', 'dd/MM/yyyy');
      PV_ENDDATE   := TO_DATE('31/01/2016', 'dd/MM/yyyy');
    else
      PV_STARTDATE := P_START_DATE;
      PV_ENDDATE   := P_END_DATE;
    end if;
  
    select EXTRACT(DAY FROM LAST_DAY(P_START_DATE))
      into PV_DAYEND
      from dual;
    /* IF P_PERIOD_ID IS NULL OR P_PERIOD_ID = 0 THEN
      PV_STARTDATE := TO_DATE('01/01/2016', 'dd/MM/yyyy');
      PV_ENDDATE   := TO_DATE('31/01/2016', 'dd/MM/yyyy');
    ELSE
      SELECT P.START_DATE,
             P.END_DATE,
             CASE
               WHEN TO_CHAR(P.START_DATE, 'MM') = TO_CHAR(P.END_DATE, 'MM') THEN
                EXTRACT(DAY FROM P.END_DATE)
               ELSE
                EXTRACT(DAY FROM
                        TO_DATE('01/' || TO_CHAR(P.END_DATE, 'MM/yyyy'),
                                'dd/MM/yyyy') - 1)
             END
        INTO PV_STARTDATE, PV_ENDDATE, PV_DAYEND
        FROM AT_PERIOD P
       WHERE P.ID = P_PERIOD_ID;
    END IF;*/
  
    SELECT CASE
             WHEN PV_DAYEND = 31 THEN
              ''
             WHEN PV_DAYEND = 30 THEN
              ', NULL AS D31'
             WHEN PV_DAYEND = 29 THEN
              ', NULL AS D31, NULL AS D30'
             WHEN PV_DAYEND = 28 THEN
              ', NULL AS D31, NULL AS D30, NULL AS D29'
             ELSE
              ''
           END
      INTO V_COL1
      FROM DUAL;
  
    INSERT INTO AT_TEMP_DATE
      SELECT A.*, PV_SEQ, rownum
        FROM table(PKG_FUNCTION.table_listdate(PV_STARTDATE, PV_ENDDATE)) A;
  
    -- LAY DANH SACH COT DONG THEO THANG
    SELECT LISTAGG('A.D' || TO_CHAR(EXTRACT(DAY FROM A.CDATE)), ',') WITHIN GROUP(ORDER BY A.STT)
      INTO V_COL
      FROM AT_TEMP_DATE A
     WHERE A.REQUEST_ID = PV_SEQ;
  
    -- LAY DU LIEU PIVOT
    SELECT LISTAGG('''' || TO_CHAR(EXTRACT(DAY FROM A.CDATE)) || '''' ||
                   ' AS "D' || A.STT || '"',
                   ',') WITHIN GROUP(ORDER BY A.STT)
      INTO V_COL_V
      FROM AT_TEMP_DATE A
     WHERE A.REQUEST_ID = PV_SEQ;
  
    PV_SQL := '
      SELECT EE.ID,
             EE.EMPLOYEE_CODE,
             EE.FULLNAME_VN     VN_FULLNAME,
             O.NAME_VN          ORG_NAME,
             O.DESCRIPTION_PATH ORG_DESC,
             TI.NAME_VN         TITLE_NAME,
             ' || V_COL || '  ' || V_COL1 || '
        FROM (SELECT T.EMPLOYEE_ID,
                     WK.ORG_ID,
                     TO_NUMBER(TO_CHAR(T.WORKINGDAY, ''dd'')) AS DAY,
                     CASE WHEN TO_CHAR(T.WORKINGDAY,''yyyyMMdd'')> TO_CHAR(R.LAST_DATE,''yyyyMMdd'')  THEN
                          NULL
                     ELSE
                          L.CODE
                     END CODE
               FROM AT_WORKSIGN T
               /*left join hu_employee e1 on t.employee_id = e1.id
               left join (select k.*
  from (select distinct w.employee_id,
               w.object_employee_id,
               w.effect_date,               
               RANK() OVER (PARTITION BY w.employee_id ORDER BY w.effect_date desc) AS  k
          from hu_working w
        WHERE W.effect_date <=  ''' || PV_ENDDATE ||
              ''') k
 where  k.k = 1) l on l.employee_id = t.employee_id*/
               LEFT JOIN AT_SHIFT L
                 ON T.SHIFT_ID = L.ID
               LEFT JOIN HU_TERMINATE R
                 ON T.EMPLOYEE_ID=R.EMPLOYEE_ID
               INNER JOIN HU_WORKING WK
                 ON WK.ID = PKG_FUNCTION.WORKINGMAX_BYDATE(T.EMPLOYEE_ID,T.WORKINGDAY)
               INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(''' ||
              P_USERNAME || '''),' || P_ORG_ID || ',' || P_ISDISSOLVE ||
              ')) O1
                  ON WK.ORG_ID = O1.ORG_ID
               WHERE  ' || '((R.LAST_DATE IS NOT NULL AND R.LAST_DATE >= ''' || PV_STARTDATE || ''') OR R.LAST_DATE IS NULL) AND ' || PV_EMP_OBJ ||
              ' and
               T.WORKINGDAY BETWEEN ''' || PV_STARTDATE ||
              ''' AND ''' || PV_ENDDATE ||
              ''') PIVOT(MAX(CODE) FOR DAY IN(' || V_COL_V ||
              ')) A
       INNER JOIN HU_EMPLOYEE EE
          ON EE.ID = A.EMPLOYEE_ID
        LEFT JOIN HU_ORGANIZATION O
          ON O.ID = A.ORG_ID
       INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(''' ||
              P_USERNAME || '''),' || P_ORG_ID || ',' || P_ISDISSOLVE ||
              ')) O1
          ON A.ORG_ID = O1.ORG_ID
        LEFT JOIN HU_TITLE TI
          ON TI.ID = EE.TITLE_ID
       WHERE (''' || P_EMPLOYEE_CODE || ''' IS NULL OR
             (''' || P_EMPLOYEE_CODE ||
              ''' IS NOT NULL AND
             EE.EMPLOYEE_CODE LIKE ''%' || P_EMPLOYEE_CODE ||
              '%''))
          OR (''' || P_EMPLOYEE_CODE || ''' IS NULL OR
             (''' || P_EMPLOYEE_CODE ||
              ''' IS NOT NULL AND
             EE.FULLNAME_VN LIKE ''%' || P_EMPLOYEE_CODE ||
              '%''))
       ORDER BY EE.EMPLOYEE_CODE';
  
    INSERT INTO AT_STRSQL VALUES (SEQ_AT_STRSQL.NEXTVAL, PV_SQL);
    COMMIT;
  
    OPEN P_CUR FOR TO_CHAR(PV_SQL);
    --OPEN P_CUR FOR PV_SQL;
  
    DELETE AT_TEMP_DATE A WHERE A.REQUEST_ID = PV_SEQ;
  
    OPEN P_CURCOUNR FOR
      SELECT COUNT(*) TOTAL
        FROM (SELECT T.EMPLOYEE_ID,
                      WK.ORG_ID,
                      TO_NUMBER(TO_CHAR(T.WORKINGDAY, 'dd')) AS DAY,
                      L.CODE
                 FROM AT_WORKSIGN T
                 left join hu_employee e1
                   on t.employee_id = e1.id
                 LEFT JOIN AT_SHIFT L
                   ON T.SHIFT_ID = L.ID
                INNER JOIN (SELECT E.EMPLOYEE_ID,
                                  E.TITLE_ID,
                                  E.ORG_ID,
                                  E.ID,
                                  E.STAFF_RANK_ID,
                                  EMP.LAST_WORKING_ID,
                                  E.EFFECT_DATE,
                                  E.IS_3B,
                                  ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                             FROM HU_WORKING E
                            INNER JOIN HU_EMPLOYEE EMP
                               ON E.EMPLOYEE_ID = EMP.ID
                            WHERE E.EFFECT_DATE <= TRUNC(SYSDATE)
                              AND E.STATUS_ID = 447
                              AND E.IS_3B = 0) WK
                   ON T.EMPLOYEE_ID = WK.EMPLOYEE_ID
                  AND WK.ROW_NUMBER = 1
                INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, P_ISDISSOLVE)) O1
                   ON WK.ORG_ID = O1.ORG_ID
                where e1.object_employee_id = P_EMP_OBJ
                   or P_EMP_OBJ is null)
             /*WHERE TO_CHAR(T.WORKINGDAY, 'YYYYMMDD') >=
                 TO_CHAR(PV_STARTDATE, 'YYYYMMDD')
             AND TO_CHAR(T.WORKINGDAY, 'YYYYMMDD') <=
                 TO_CHAR(PV_ENDDATE, 'YYYYMMDD'))*/ PIVOT(MAX(CODE) FOR DAY IN(21 AS D1,
                                                                               22 AS D2,
                                                                               23 AS D3,
                                                                               24 AS D4,
                                                                               25 AS D5,
                                                                               26 AS D6,
                                                                               27 AS D7,
                                                                               28 AS D8,
                                                                               29 AS D9,
                                                                               30 AS D10,
                                                                               31 AS D11,
                                                                               1 AS D12,
                                                                               2 AS D13,
                                                                               3 AS D14,
                                                                               4 AS D15,
                                                                               5 AS D16,
                                                                               6 AS D17,
                                                                               7 AS D18,
                                                                               8 AS D19,
                                                                               9 AS D20,
                                                                               10 AS D21,
                                                                               11 AS D22,
                                                                               12 AS D23,
                                                                               13 AS D24,
                                                                               14 AS D25,
                                                                               15 AS D26,
                                                                               16 AS D27,
                                                                               17 AS D28,
                                                                               18 AS D29,
                                                                               19 AS D30,
                                                                               20 AS D31)) A
       INNER JOIN HU_EMPLOYEE EE
          ON EE.ID = A.EMPLOYEE_ID
        LEFT JOIN HU_ORGANIZATION O
          ON O.ID = A.ORG_ID
       INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, P_ISDISSOLVE)) O1
          ON A.ORG_ID = O1.ORG_ID
        LEFT JOIN HU_TITLE TI
          ON TI.ID = EE.TITLE_ID
       WHERE (P_EMPLOYEE_CODE IS NULL OR
             (P_EMPLOYEE_CODE IS NOT NULL AND
             EE.EMPLOYEE_CODE LIKE '%' || P_EMPLOYEE_CODE || '%'))
          OR (P_EMPLOYEE_CODE IS NULL OR
             (P_EMPLOYEE_CODE IS NOT NULL AND
             EE.FULLNAME_VN LIKE '%' || P_EMPLOYEE_CODE || '%'));
  END;
  /*PROCEDURE CAL_TIME_TIMESHEET_MONTHLY(P_USERNAME  IN VARCHAR2,
                                         P_PERIOD_ID IN NUMBER,
                                         P_ORG_ID    IN NUMBER) IS
      PV_FROMDATE      DATE;
      PV_TODATE        DATE;
      PV_SQL           VARCHAR2(4000);
      PV_ERROR         NVARCHAR2(200);
      WORKING_STANDARD NUMBER;
      PV_EXISTS        NUMBER;
    BEGIN
      SELECT P.START_DATE, P.END_DATE
        INTO PV_FROMDATE, PV_TODATE
        FROM AT_PERIOD P
       WHERE P.ID = P_PERIOD_ID;
  
      ---------------------------------------------------------------------------------
      -- XOA BO DU LIEU CU TRONG BANG TAM
      ---------------------------------------------------------------------------------
      DELETE AT_TIME_TIMESHEET_DAILY_TEM T
       WHERE T.WORKINGDAY >= PV_FROMDATE
         AND T.WORKINGDAY <= PV_TODATE
         AND T.EMPLOYEE_ID IN (SELECT ID
                                 FROM HU_EMPLOYEE EE
                                INNER JOIN TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID)) O
                                   ON EE.ORG_ID = O.ORG_ID);
      COMMIT;
      ---------------------------------------------------------------------------------
      -- INSERT DU LIEU MOI CAN TINH TRONG BANG TAM
      ---------------------------------------------------------------------------------
      INSERT INTO AT_TIME_TIMESHEET_DAILY_TEM P
        (EMPLOYEE_ID, ORG_ID, TITLE_ID, WORKINGDAY, PA_OBJECT_SALARY_ID)
        SELECT DISTINCT T.EMPLOYEE_ID,
                        T.ORG_ID,
                        T.TITLE_ID,
                        T.WORKINGDAY,
                        E.PA_OBJECT_SALARY_ID
          FROM AT_TIME_TIMESHEET_DAILY T
         INNER JOIN TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID)) O
            ON T.ORG_ID = O.ORG_ID
         INNER JOIN (SELECT DISTINCT EMPLOYEE_ID, ORG_ID, TITLE_ID
                       FROM (SELECT W.EMPLOYEE_ID,
                                    W.ORG_ID,
                                    W.TITLE_ID,
                                    ROW_NUMBER() OVER(PARTITION BY W.EMPLOYEE_ID ORDER BY W.EFFECT_DATE DESC) AS ROW_NUMBER
                               FROM HU_WORKING W
                              WHERE W.EFFECT_DATE <= PV_TODATE) WK
                      WHERE WK.ROW_NUMBER = 1) W
            ON T.EMPLOYEE_ID = W.EMPLOYEE_ID
          LEFT JOIN HU_EMPLOYEE E
            ON T.EMPLOYEE_ID = E.ID
         WHERE T.WORKINGDAY >= PV_FROMDATE
           AND T.WORKINGDAY <= PV_TODATE
         GROUP BY T.EMPLOYEE_ID,
                  T.WORKINGDAY,
                  T.ORG_ID,
                  T.TITLE_ID,
                  E.PA_OBJECT_SALARY_ID
         ORDER BY T.EMPLOYEE_ID, T.WORKINGDAY;
      ----------------------------------------------------------------------------------
      UPDATE AT_TIME_TIMESHEET_DAILY_TEM T
         SET T.DECISION_ID =
             (SELECT MAX(W1.ID)
                FROM HU_WORKING W1
               WHERE W1.EMPLOYEE_ID = T.EMPLOYEE_ID
                 AND T.WORKINGDAY >= W1.EFFECT_DATE)
       WHERE T.ORG_ID IN
             (SELECT ORG_ID FROM TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID)))
         AND T.WORKINGDAY >= PV_FROMDATE
         AND T.WORKINGDAY <= PV_TODATE;
      ----------------------------------------------------------------------------------
  
      UPDATE AT_TIME_TIMESHEET_DAILY_TEM T
         SET T.PA_OBJECT_SALARY_ID =
             (SELECT W1.PA_OBJECT_SALARY_ID
                FROM HU_EMPLOYEE W1
               WHERE W1.ID = T.EMPLOYEE_ID)
       WHERE T.ORG_ID IN
             (SELECT ORG_ID FROM TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID)))
         AND T.WORKINGDAY >= PV_FROMDATE
         AND T.WORKINGDAY <= PV_TODATE;
  
      ---------------------------------------------------------------------------------
      -- XOA DU LIEU CU CAN TINH TRONG BANG TONG HOP
      ---------------------------------------------------------------------------------
      DELETE AT_TIME_TIMESHEET_MONTHLY T
       WHERE T.ORG_ID IN
             (SELECT ORG_ID FROM TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID)))
         AND T.PERIOD_ID >= P_PERIOD_ID;
      ---------------------------------------------------------------------------------
      -- INSERT DU LIEU VAO BANG TONG HOP CONG
      ---------------------------------------------------------------------------------
      INSERT INTO AT_TIME_TIMESHEET_MONTHLY
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         DECISION_ID,
         PA_OBJECT_SALARY_ID,
         PERIOD_ID,
         FROM_DATE,
         END_DATE)
        SELECT SEQ_AT_TIME_TIMESHEET_MONTHLY.NEXTVAL,
               T.EMPLOYEE_ID,
               T.ORG_ID,
               T.TITLE_ID,
               T.DECISION_ID,
               PA_OBJECT_SALARY_ID,
               P_PERIOD_ID,
               PV_FROMDATE,
               PV_TODATE
          FROM (SELECT DISTINCT A.EMPLOYEE_ID,
                                A.ORG_ID,
                                A.TITLE_ID,
                                A.DECISION_ID,
                                A.PA_OBJECT_SALARY_ID
                  FROM AT_TIME_TIMESHEET_DAILY_TEM A
                 INNER JOIN TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID)) O
                    ON A.ORG_ID = O.ORG_ID
                 WHERE A.WORKINGDAY >= PV_FROMDATE
                   AND A.WORKINGDAY <= PV_TODATE
                 GROUP BY A.EMPLOYEE_ID,
                          A.DECISION_ID,
                          A.ORG_ID,
                          A.TITLE_ID,
                          A.PA_OBJECT_SALARY_ID) T;
  
      ---------------------------------------------------------------------------------
      -- INSERT DU LIEU VAO BANG TONG HOP CONG
      ---------------------------------------------------------------------------------
      DELETE AT_TIME_TIMESHEET_DAILY_TEM T
       WHERE T.WORKINGDAY >= PV_FROMDATE
         AND T.WORKINGDAY <= PV_TODATE
         AND T.EMPLOYEE_ID IN (SELECT ID
                                 FROM HU_EMPLOYEE EE
                                INNER JOIN TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID)) O
                                   ON EE.ORG_ID = O.ORG_ID)
         AND T.CREATED_BY = P_USERNAME;
      ---------------------------------------------------------------------------------
      -- SAO CHEP TOAN BO DU LIEU TU BANG CONG TAY SANG BANG CONG TAM
      ---------------------------------------------------------------------------------
      INSERT INTO AT_TIME_TIMESHEET_DAILY_TEM
        (EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         WORKINGDAY,
         SHIFT_CODE,
         LEAVE_CODE,
         LATE,
         LATE_DEDUCTION,
         LATE_OFFICE,
         COMEBACKOUT,
         COMEBACKOUT_DEDUCTION,
         COMEBACKOUT_OFFICE,
         WORKDAY_OT,
         LATE_PERMISSION,
         COMEBACKOUT_PERMISSION,
         MANUAL_ID,
         VALIN1,
         VALIN2,
         VALIN3,
         VALIN4,
         CREATED_DATE,
         CREATED_BY,
         CREATED_LOG,
         MODIFIED_DATE,
         MODIFIED_BY,
         MODIFIED_LOG)
        SELECT EMPLOYEE_ID,
               T.ORG_ID,
               TITLE_ID,
               WORKINGDAY,
               SHIFT_CODE,
               LEAVE_CODE,
               LATE,
               LATE_DEDUCTION,
               LATE_OFFICE,
               COMEBACKOUT,
               COMEBACKOUT_DEDUCTION,
               COMEBACKOUT_OFFICE,
               WORKDAY_OT,
               LATE_PERMISSION,
               COMEBACKOUT_PERMISSION,
               MANUAL_ID,
               VALIN1,
               VALIN2,
               VALIN3,
               VALIN4,
               CREATED_DATE,
               CREATED_BY,
               CREATED_LOG,
               MODIFIED_DATE,
               MODIFIED_BY,
               MODIFIED_LOG
          FROM AT_TIME_TIMESHEET_DAILY T
         INNER JOIN TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID)) O
            ON T.ORG_ID = O.ORG_ID
         WHERE T.WORKINGDAY >= PV_FROMDATE
           AND T.WORKINGDAY <= PV_TODATE;
      --------------------------------------------------------------------------------------
      -- AP DUNG CONG THUC TINH CHO CAC COT TREN BANG CONG TONG HOP
      --------------------------------------------------------------------------------------
      FOR CUR_ITEM IN (SELECT *
                         FROM AT_TIME_FORMULAR T
                        WHERE T.TYPE = 2
                          AND T.STATUS = 1
                        ORDER BY T.FORMULAR_ID) LOOP
  
        PV_SQL := 'UPDATE AT_TIME_TIMESHEET_MONTHLY T SET ' ||
                  CUR_ITEM.FORMULAR_CODE || '= NVL((' ||
                  CUR_ITEM.FORMULAR_VALUE || '),0)' || CHR(10) || ' WHERE' ||
                  CHR(10) || ' ORG_ID IN(' ||
                  '(SELECT ORG_ID FROM TABLE(TABLE_ORG_RIGHT(''' ||
                  P_USERNAME || ''',' || P_ORG_ID || ')))) AND T.PERIOD_ID =' ||
                  P_PERIOD_ID;
        EXECUTE IMMEDIATE PV_SQL;
  
      END LOOP;
      --------------------------------------------------------------------------------------
      -- XOA TOAN BO DU LIEU TRONG BANG TAM
      --------------------------------------------------------------------------------------
  
      DELETE AT_TIME_TIMESHEET_DAILY_TEM T
       WHERE T.WORKINGDAY >= PV_FROMDATE
         AND T.WORKINGDAY <= PV_TODATE
         AND T.ORG_ID IN
             (SELECT ORG_ID FROM TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID)))
         AND T.CREATED_BY = P_USERNAME;
    END;
  */
  PROCEDURE CAL_TIME_TIMESHEET_IN(P_USERNAME   IN NVARCHAR2,
                                  P_ORG_ID     IN NUMBER,
                                  P_PERIOD_ID  IN NUMBER,
                                  P_ISDISSOLVE IN NUMBER,
                                  P_DELETE_ALL IN NUMBER := 1,
                                  P_OBJ_EMP_ID IN NUMBER,
                                  P_FROM_DATE  IN nvarchar2,
                                  P_TO_DATE    IN nvarchar2,
                                  P_REQUEST_ID IN NUMBER,
                                  P_OUT        OUT NUMBER) IS
  BEGIN
    P_OUT := 3;
  END;
  PROCEDURE CAL_TIME_TIMESHEET_ALL_NEW(P_USERNAME   IN NVARCHAR2,
                                       P_ORG_ID     IN NUMBER,
                                       P_PERIOD_ID  IN NUMBER,
                                       P_ISDISSOLVE IN NUMBER,
                                       P_DELETE_ALL IN NUMBER := 1,
                                       P_OBJ_EMP_ID IN NUMBER,
                                       P_FROM_DATE  IN nvarchar2,
                                       P_TO_DATE    IN nvarchar2,
                                       P_EMPLIST    IN CLOB,
                                       P_REQUEST_ID IN NUMBER,
                                       P_OUT        OUT NUMBER) IS
    PV_FROMDATE    DATE;
    PV_ENDDATE     DATE;
    PV_SQL         CLOB;
    PV_REQUEST_ID  NUMBER;
    PV_MINUS_ALLOW NUMBER := 50;
    PV_SUNDAY      DATE; --Lay ngay chu nhat trong thang
    PV_TEST1       NUMBER;
    PV_TEST2       NUMBER;
    PV_CHECK       NUMBER;
    PV_DEL_ALL     NUMBER;
    PV_CHECKNV     NUMBER;
    PV_TG_BD_CA    DATE;
    PV_TG_KT_CA    DATE;
    PV_TG_BD_NGHI  DATE;
    PV_TG_KT_NGHI  DATE;
    PV_IN_MIN      DATE;
    PV_OUT_MAX     DATE;
    PV_TRAN_INDX   NUMBER;
    PV_SQL_CRE_TB  CLOB;
    PV_TBL_NAME    NVARCHAR2(50);
    --PV_TOKEN       NVARCHAR2(40);
  BEGIN
   /* SELECT standard_hash(TO_CHAR(P_ORG_ID) || P_USERNAME ||
                         TO_CHAR(P_PERIOD_ID),
                         'MD5')
      INTO PV_TOKEN
      FROM DUAL;
  
    SELECT COUNT(*)
      INTO PV_CHECK
      FROM user_tables UT
     WHERE UPPER(UT.TABLE_NAME) = UPPER(PV_TOKEN);
  
    IF PV_CHECK > 0 THEN
      PV_SQL_CRE_TB := '
            DROP TABLE ' || 'TBL' || PV_TOKEN || '
    ';
      --COMMIT;
      EXECUTE IMMEDIATE PV_SQL_CRE_TB;
    END IF;*/
  
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    PV_DEL_ALL    := P_DELETE_ALL;
    PV_TRAN_INDX  := ABS(REMAINDER(PV_REQUEST_ID, 15));
  
    PV_FROMDATE := to_date(P_FROM_DATE, 'dd/mm/yyyy');
    PV_ENDDATE  := to_date(P_TO_DATE, 'dd/mm/yyyy');
  
    /*SELECT P.START_DATE, P.END_DATE
     INTO PV_FROMDATE, PV_ENDDATE
     FROM AT_PERIOD P
    WHERE P.ID = P_PERIOD_ID;*/
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E --temp
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
    
  
    INSERT INTO AT_CHOSEN_EMP_CLEAR_TMP -- temp
      (EMPLOYEE_ID, REQUEST_ID)
      SELECT DISTINCT S.EMPLOYEE_ID, PV_REQUEST_ID
        FROM AT_TIME_TIMESHEET_MACHINET S
       INNER JOIN (SELECT ORG_ID
                     FROM AT_CHOSEN_ORG O
                    WHERE O.REQUEST_ID = PV_REQUEST_ID) O
          ON O.ORG_ID = S.ORG_ID
       WHERE (S.CREATED_BY = 'AUTO')
         AND S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
    
  
    SELECT COUNT(1)
      INTO PV_CHECKNV
      FROM AT_TIME_TIMESHEET_MACHINET S
     INNER JOIN (SELECT ORG_ID
                   FROM AT_CHOSEN_ORG O
                  WHERE O.REQUEST_ID = PV_REQUEST_ID) O
        ON O.ORG_ID = S.ORG_ID
     WHERE S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
  
    SELECT COUNT(1) INTO PV_CHECK FROM AT_CHOSEN_EMP_CLEAR;
    IF (PV_CHECK = 0 AND PV_CHECKNV = 0) OR P_DELETE_ALL <> 0 THEN
      PV_DEL_ALL := 1;
    END IF;
    IF P_EMPLIST IS NOT NULL THEN
      INSERT INTO AT_CHOSEN_EMP --temp
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE,
         DECISION_ID,
         OBJECT_ATTENDANCE)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE,
                W.ID DECISION_ID,
                W.OBJECT_ATTENDANCE
           FROM HU_EMPLOYEE T
          INNER JOIN (SELECT E.ID,
                            E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            E.OBJECT_ATTENDANCE,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        and e.OBJECT_EMPLOYEE_ID = P_OBJ_EMP_ID
                        AND E.STATUS_ID = 447
                        AND E.IS_WAGE = 0
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID
                       FROM AT_CHOSEN_ORG O
                      WHERE O.REQUEST_ID = PV_REQUEST_ID) O
             ON O.ORG_ID = W.ORG_ID
          WHERE INSTR(',' || P_EMPLIST || ',', ',' || T.ID || ',') > 0
              AND (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
     ELSE
          IF PV_DEL_ALL = 1 THEN
            --==TINH LAI TAT CA CA NV
            -- insert emp can tinh toan
            INSERT INTO AT_CHOSEN_EMP --temp
              (EMPLOYEE_ID,
               ITIME_ID,
               ORG_ID,
               TITLE_ID,
               STAFF_RANK_ID,
               STAFF_RANK_LEVEL,
               TER_EFFECT_DATE,
               USERNAME,
               REQUEST_ID,
               JOIN_DATE,
               JOIN_DATE_STATE,
               DECISION_ID,
               OBJECT_ATTENDANCE)
              (SELECT T.ID,
                      T.ITIME_ID,
                      W.ORG_ID,
                      W.TITLE_ID,
                      W.STAFF_RANK_ID,
                      W.LEVEL_STAFF,
                      CASE
                        WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                         T.TER_EFFECT_DATE + 1
                        ELSE
                         NULL
                      END TER_EFFECT_DATE,
                      UPPER(P_USERNAME),
                      PV_REQUEST_ID,
                      T.JOIN_DATE,
                      T.JOIN_DATE_STATE,
                      W.ID DECISION_ID,
                      W.OBJECT_ATTENDANCE
                 FROM HU_EMPLOYEE T
                INNER JOIN (SELECT E.ID,
                                  E.EMPLOYEE_ID,
                                  E.TITLE_ID,
                                  E.ORG_ID,
                                  E.IS_3B,
                                  E.STAFF_RANK_ID,
                                  S.LEVEL_STAFF,
                                  E.OBJECT_ATTENDANCE,
                                  ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                             FROM HU_WORKING E
                             LEFT JOIN HU_STAFF_RANK S
                               ON E.STAFF_RANK_ID = S.ID
                            WHERE E.EFFECT_DATE <= PV_ENDDATE
                              and e.OBJECT_EMPLOYEE_ID = P_OBJ_EMP_ID
                              AND E.STATUS_ID = 447
                              AND E.IS_WAGE = 0
                              AND E.IS_3B = 0) W
                   ON T.ID = W.EMPLOYEE_ID
                  AND W.ROW_NUMBER = 1
                INNER JOIN (SELECT ORG_ID
                             FROM AT_CHOSEN_ORG O
                            WHERE O.REQUEST_ID = PV_REQUEST_ID) O
                   ON O.ORG_ID = W.ORG_ID
                WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                      (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
          ELSE
            --==CHI TINH NV AUTO
            -- insert emp can tinh toan
            INSERT INTO AT_CHOSEN_EMP
              (EMPLOYEE_ID,
               ITIME_ID,
               ORG_ID,
               TITLE_ID,
               STAFF_RANK_ID,
               STAFF_RANK_LEVEL,
               TER_EFFECT_DATE,
               USERNAME,
               REQUEST_ID,
               JOIN_DATE,
               JOIN_DATE_STATE,
               DECISION_ID,
               OBJECT_ATTENDANCE)
              (SELECT T.ID,
                      T.ITIME_ID,
                      W.ORG_ID,
                      W.TITLE_ID,
                      W.STAFF_RANK_ID,
                      W.LEVEL_STAFF,
                      CASE
                        WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                         T.TER_EFFECT_DATE + 1
                        ELSE
                         NULL
                      END TER_EFFECT_DATE,
                      UPPER(P_USERNAME),
                      PV_REQUEST_ID,
                      T.JOIN_DATE,
                      T.JOIN_DATE_STATE,
                      W.ID DECISION_ID,
                      W.OBJECT_ATTENDANCE
                 FROM HU_EMPLOYEE T
                INNER JOIN AT_CHOSEN_EMP_CLEAR_TMP C --==CHI TINH NV AUTO
                   ON T.ID = C.EMPLOYEE_ID
                  AND C.REQUEST_ID = PV_REQUEST_ID
                INNER JOIN (SELECT E.ID,
                                  E.EMPLOYEE_ID,
                                  E.TITLE_ID,
                                  E.ORG_ID,
                                  E.IS_3B,
                                  E.STAFF_RANK_ID,
                                  S.LEVEL_STAFF,
                                  E.OBJECT_ATTENDANCE,
                                  ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                             FROM HU_WORKING E
                             LEFT JOIN HU_STAFF_RANK S
                               ON E.STAFF_RANK_ID = S.ID
                            WHERE E.EFFECT_DATE <= PV_ENDDATE
                              and e.OBJECT_EMPLOYEE_ID = P_OBJ_EMP_ID
                              AND E.STATUS_ID = 447
                              AND E.IS_WAGE = 0
                              AND E.IS_3B = 0) W
                   ON T.ID = W.EMPLOYEE_ID
                  AND W.ROW_NUMBER = 1
                INNER JOIN (SELECT ORG_ID
                             FROM AT_CHOSEN_ORG O
                            WHERE O.REQUEST_ID = PV_REQUEST_ID) O
                   ON O.ORG_ID = W.ORG_ID
                WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                      (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
          END IF;
    END IF;
    
  
    --RETURN;
    --==Start Chuc nang bang cong goc
    --==insert data v?o bang tam de tinh toan
    /*UPDATE AT_SWIPE_DATA S
    SET S.EMPLOYEE_ID = NVL((SELECT ID
                              FROM HU_EMPLOYEE E
                             WHERE E.ITIME_ID = S.ITIME_ID
                               AND NVL(E.IS_KIEM_NHIEM, 0) = 0),
                            0);*/
    INSERT INTO AT_SWIPE_DATA_TMP
      SELECT SW.*, PV_REQUEST_ID
        FROM AT_SWIPE_DATA SW
       INNER JOIN AT_CHOSEN_EMP E
          ON E.EMPLOYEE_ID = SW.EMPLOYEE_ID
         AND E.REQUEST_ID = PV_REQUEST_ID
       WHERE SW.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
    
    INSERT INTO AT_WORKSIGN_TMP
      SELECT WS.*,
             PV_REQUEST_ID
        FROM AT_WORKSIGN WS
       INNER JOIN AT_CHOSEN_EMP E
          ON E.EMPLOYEE_ID = WS.EMPLOYEE_ID
         AND E.REQUEST_ID = PV_REQUEST_ID
       INNER JOIN HU_EMPLOYEE EMP
          ON EMP.ID = WS.EMPLOYEE_ID
       WHERE WS.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
         AND (EMP.TER_LAST_DATE IS NULL OR
             (EMP.TER_LAST_DATE IS NOT NULL AND EMP.TER_LAST_DATE >= WS.WORKINGDAY));
    
    INSERT INTO AT_LEAVESHEET_DETAIL_TMP
      SELECT D.ID,
             D.LEAVESHEET_ID,
             D.EMPLOYEE_ID,
             D.LEAVE_DAY,
             D.MANUAL_ID,
             D.DAY_NUM,
             D.STATUS_SHIFT,
             D.SHIFT_ID,
             D.CREATED_DATE,
             D.CREATED_BY,
             D.CREATED_LOG,
             D.MODIFIED_DATE,
             D.MODIFIED_BY,
             D.MODIFIED_LOG,
             PV_REQUEST_ID,
             D.OLD_LEAVE,
             D.REASON_LEAVE
        FROM AT_LEAVESHEET_DETAIL D
        JOIN AT_LEAVESHEET L
          ON L.ID = D.LEAVESHEET_ID       
         INNER JOIN AT_CHOSEN_EMP E
          ON E.EMPLOYEE_ID = L.EMPLOYEE_ID
         AND E.REQUEST_ID = PV_REQUEST_ID 
       WHERE D.LEAVE_DAY BETWEEN PV_FROMDATE AND PV_ENDDATE
         AND L.STATUS = 1;
    
    INSERT INTO AT_TIME_TIMESHEET_MACHINE_TMP M --temp
      (ID,
       OBJ_EMP_ID,
       PERIOD_ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       WORKINGDAY,
       SHIFT_ID,
       SHIFT_CODE,
       CREATED_DATE,
       --CREATED_BY,
       REQUEST_ID,
       VALIN1,
       VALOUT1,
       TIMEVALIN,
       TIMEVALOUT,
       TIMEIN_REALITY, --GIO VAO TT
       TIMEOUT_REALITY, -- GIO RA THUC TE
       OBJECT_ATTENDANCE,
       OBJECT_ATTENDANCE_CODE,
       WORK_HOUR,
       HOURS_STOP,
       HOURS_START,
       START_MID_HOURS,
       END_MID_HOURS,
       LATE_HOUR,
       EARLY_HOUR,
       SHIFT_DAY,
       HOURS_STAR_CHECKIN,
       HOURS_STAR_CHECKOUT,
       NOTE,
       --MIN_ON_LEAVE,
       MANUAL_ID,
       STATUS_SHIFT,
       MANUAL_CODE,
       MORNING_RATE,
       AFTERNOON_RATE,
       DAY_NUM,
       STATUS_SHIFT_NAME,
       CREATED_BY,
       ORG_ACCOUNTING,
       SHIFT_HOURS_START,
       SHIFT_HOURS_STOP,
       TOMORROW_SHIFT   
       )
      SELECT SEQ_AT_TIME_TIMESHEET_MACHINET.NEXTVAL,
             P_OBJ_EMP_ID,
             P_PERIOD_ID,
             T.EMPLOYEE_ID,
             T1.ORG_ID,
             T1.TITLE_ID,
             T.STAFF_RANK_ID,
             WS.WORKINGDAY,
             WS.SHIFT_ID,
             SH.CODE SHIFT_CODE,
             SYSDATE,
             --P_USERNAME,
             PV_REQUEST_ID,
             null,
             null,
             --gio vao goc
             CASE
               WHEN SH.CODE IN( 'OFF','L') THEN
                --DATA_IN.VALTIME
                NULL
               ELSE
                PA_FUNC.FN_GET_VALTIME_1(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       0,
                                       PV_REQUEST_ID)
             END,
             --gio ra goc
             CASE
               WHEN SH.CODE IN('OFF','L') THEN
                --DATA_OUT.VALTIME  
                NULL
               ELSE
                PA_FUNC.FN_GET_VALTIME_1(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       1,
                                       PV_REQUEST_ID)
             END,
              --gio vao thuc te
             CASE
               WHEN SH.CODE IN( 'OFF','L') THEN
                --DATA_IN.VALTIME 
                NULL
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       0,
                                       PV_REQUEST_ID)
             END,
              --gio ra thuc te
             CASE
               WHEN SH.CODE IN( 'OFF','L') THEN
                --DATA_OUT.VALTIME 
                NULL
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       1,
                                       PV_REQUEST_ID)
             END,                  
             T.OBJECT_ATTENDANCE,
             OL.CODE OBJECT_ATTENDANCE_CODE,
             --SH.WORK_HOUR,
             SH.SHIFT_HOUR,
             CASE WHEN SH.CODE='L' OR SH.CODE='OFF' THEN
                  NULL
             ELSE
                  TO_DATE(TO_CHAR(CASE WHEN NVL(SH.IS_HOURS_STOP,0) <> 0 THEN WS.WORKINGDAY+1 ELSE WS.WORKINGDAY END, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STOP, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI')
             END HOURS_STOP, -- GIO KET THUC CA
             CASE WHEN SH.CODE='L' OR SH.CODE='OFF' THEN
                  NULL
             ELSE
                  TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_START, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI')
             END HOURS_START, -- GIO BAT DAU CA
             CASE WHEN SH.CODE='L' OR SH.CODE='OFF' THEN
                  NULL
             ELSE
                  TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.START_MID_HOURS, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI')
             END,
             CASE WHEN SH.CODE='L' OR SH.CODE='OFF' THEN
                  NULL
             ELSE
                  TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.END_MID_HOURS, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI')
             END,
             CASE
               WHEN SH.LATE_HOUR IS NOT NULL THEN
                TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                        TO_CHAR(SH.LATE_HOUR, 'HH24:MI'),
                        'DD/MM/RRRR HH24:MI')
               ELSE
                NULL
             END,
             CASE
               WHEN SH.EARLY_HOUR IS NOT NULL THEN
                TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                        TO_CHAR(SH.EARLY_HOUR, 'HH24:MI'),
                        'DD/MM/RRRR HH24:MI')
               ELSE
                NULL
             END,
             SH.SHIFT_DAY,
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STAR_CHECKIN, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STAR_CHECKOUT, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             I.NOTE,
             --LEAVE.DAY_NUM,
             CASE
               WHEN SH.CODE = 'OFF' OR SH.CODE = 'L' THEN
                NULL
               ELSE
                LEAVE.MANUAL_ID
             END,
             /*LEAVE.MANUAL_ID,*/
             LEAVE.STATUS_SHIFT,
             CASE
               WHEN SH.CODE = 'OFF' OR SH.CODE = 'L' THEN
                NULL
               ELSE
                FM.CODE
             END,
             /*FM.CODE,*/
             MR.VALUE_RATE,
             AF.VALUE_RATE,
             LEAVE.DAY_NUM,
             CASE
               WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 0 THEN
                TO_CHAR(UNISTR(''))
               WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 1 THEN
                TO_CHAR(UNISTR('\0110\00E2\0300u ca'))
               WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 2 THEN
                TO_CHAR(UNISTR('cu\00F4\0301i ca'))
             END,
             CASE
               WHEN WS.EMPLOYEE_ID || TO_CHAR(WS.WORKINGDAY, 'DDMMYYYY') IN
                    (SELECT T.EMPLOYEE_ID ||
                            TO_CHAR(T.WORKING_DAY, 'DDMMYYYY')
                       FROM AT_TIMESHEET_MACHINET_IMPORT T
                      WHERE T.EMPLOYEE_ID = WS.EMPLOYEE_ID
                        AND T.WORKING_DAY = WS.WORKINGDAY) THEN
                P_USERNAME
               ELSE
                CAST('AUTO' AS NVARCHAR2(20))
             END,
             --PKG_FUNCTION.ORG_WORKING_MAX(T.EMPLOYEE_ID, WS.WORKINGDAY),
             T1.ORG_ID,
              PKG_FUNCTION.GET_TIME_OT_COEFF_OVER(WS.WORKINGDAY,'START') SHIFT_HOURS_START, -- GIO BAT DAU CUA KHUNG GIO LAM DEM
              PKG_FUNCTION.GET_TIME_OT_COEFF_OVER(WS.WORKINGDAY,'STOP') SHIFT_HOURS_STOP, -- GIO KET THUC CUA KHUNG GIO LAM DEM   
             CASE
               WHEN SH.IS_HOURS_STOP = -1 THEN 
                 -1
               ELSE
                 0
              END         
        FROM AT_WORKSIGN_TMP WS
       INNER JOIN AT_CHOSEN_EMP T
          ON T.EMPLOYEE_ID = WS.EMPLOYEE_ID
         AND T.REQUEST_ID = PV_REQUEST_ID
        LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                          SW.WORKING_DAY,
                          SW.TIMEIN_REALITY,
                          SW.TIMEOUT_REALITY,
                          SW.NOTE,
                          ROW_NUMBER() OVER(PARTITION BY SW.EMPLOYEE_ID, SW.WORKING_DAY ORDER BY SW.TIMEIN_REALITY, SW.TIMEOUT_REALITY) AS ROW_NUMBER
                     FROM AT_TIMESHEET_MACHINET_IMPORT SW
                    WHERE SW.WORKING_DAY BETWEEN PV_FROMDATE AND PV_ENDDATE
                   
                   ) I
          ON I.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND I.WORKING_DAY = WS.WORKINGDAY
         AND I.ROW_NUMBER = 1
        LEFT JOIN HU_WORKING T1
          ON T1.ID = PKG_FUNCTION.WORKINGMAX_BYDATE(WS.EMPLOYEE_ID,WS.WORKINGDAY)
        LEFT JOIN AT_SHIFT SH
          ON WS.SHIFT_ID = SH.ID
        LEFT JOIN AT_LEAVESHEET_DETAIL_TMP LEAVE
          ON WS.EMPLOYEE_ID = LEAVE.EMPLOYEE_ID
         AND LEAVE.REQUEST_ID = PV_REQUEST_ID
         AND TRUNC(WS.WORKINGDAY) = TRUNC(LEAVE.LEAVE_DAY)
        LEFT JOIN AT_TIME_MANUAL FM
          ON FM.ID = LEAVE.MANUAL_ID
        LEFT JOIN AT_TIME_MANUAL_RATE MR
          ON MR.ID = FM.MORNING_RATE_ID
        LEFT JOIN AT_TIME_MANUAL_RATE AF
          ON AF.ID = FM.AFTERNOON_RATE_ID
        LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                          SW.WORKINGDAY,
                          MIN(SW.VALTIME) AS VALTIME
                     FROM AT_SWIPE_DATA_TMP SW
                    WHERE SW.REQUEST_ID = PV_REQUEST_ID
                    GROUP BY SW.EMPLOYEE_ID, SW.WORKINGDAY) DATA_IN
          ON DATA_IN.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND DATA_IN.WORKINGDAY = WS.WORKINGDAY
        LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                          SW.WORKINGDAY,
                          MAX(SW.VALTIME) AS VALTIME
                     FROM AT_SWIPE_DATA_TMP SW
                    WHERE SW.REQUEST_ID = PV_REQUEST_ID
                    GROUP BY SW.EMPLOYEE_ID, SW.WORKINGDAY) DATA_OUT
          ON DATA_OUT.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND DATA_OUT.WORKINGDAY = WS.WORKINGDAY
        LEFT JOIN OT_OTHER_LIST OL
          ON T.OBJECT_ATTENDANCE = OL.ID
       WHERE WS.REQUEST_ID = PV_REQUEST_ID;
  
    --PHAN CHIA TRAN DE TINH TOAN
    PV_TBL_NAME   := TRIM('AT_TIME_TIMESHEET_MACHINE_TEMP' ||
                          TRIM(TO_CHAR(PV_REQUEST_ID)));                      
                          
    PV_SQL_CRE_TB := '
        CREATE TABLE ' || PV_TBL_NAME || '
        AS (SELECT * FROM AT_TIME_TIMESHEET_MACHINE_TMP WHERE 0=1)
    ';
    --INSERT INTO TEMP(TEXT)VALUES(PV_SQL_CRE_TB);
    EXECUTE IMMEDIATE PV_SQL_CRE_TB;
     
    PV_SQL_CRE_TB := '
       INSERT INTO ' || PV_TBL_NAME || '
       SELECT * FROM AT_TIME_TIMESHEET_MACHINE_TMP WHERE REQUEST_ID=' ||
                     PV_REQUEST_ID || '
    ';
    EXECUTE IMMEDIATE PV_SQL_CRE_TB;
         
    
    --================================================================================
  
    --insert into AT_TIME_TIMESHEET_MACHINE_TEMP_1 select * from AT_TIME_TIMESHEET_MACHINE_TEMP;
    --== ap dung cong thuc
    FOR CUR_ITEM IN (SELECT *
                       FROM AT_TIME_FORMULAR T
                      WHERE T.TYPE IN (5)
                        AND T.STATUS = 1
                      ORDER BY T.ORDERBY) LOOP
      IF CUR_ITEM.FORMULAR_CODE = 'STATUS' OR CUR_ITEM.FORMULAR_CODE = 'DAYOFWEEK' THEN
        PV_SQL := 'UPDATE ' || PV_TBL_NAME || '  T SET ' ||
                  CUR_ITEM.FORMULAR_CODE || ' = (' || CUR_ITEM.FORMULAR_VALUE || ')
                   WHERE  REQUEST_ID = ' || PV_REQUEST_ID || ' ';
      ELSE
        PV_SQL := 'UPDATE ' || PV_TBL_NAME || '  T SET ' ||
                  CUR_ITEM.FORMULAR_CODE || ' = CASE WHEN NVL((' || CUR_ITEM.FORMULAR_VALUE || '),0) <= 0 THEN 
                                                       0 
                                                   ELSE NVL((' || CUR_ITEM.FORMULAR_VALUE || '),0) 
                                                END   
                   WHERE  REQUEST_ID = ' || PV_REQUEST_ID || ' ';
      END IF;
      
       INSERT INTO TEMP1
        (TMP, WCODE)
      VALUES
        (PV_SQL, 'CUR_ITEM.FORMULAR_CODE1');    
      BEGIN
        EXECUTE IMMEDIATE PV_SQL;
        
      EXCEPTION
        WHEN OTHERS THEN
          INSERT INTO TEMP1
            (TMP, WCODE)
          VALUES
            (PV_SQL, 'CUR_ITEM.FORMULAR_CODE');
          
          CONTINUE;
      END;
    END LOOP;
    -- XOA DU LIEU CU TRUOC KHI TINH
    DELETE FROM AT_TIME_TIMESHEET_MACHINET D
     WHERE D.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
       AND D.Employee_Id IN (SELECT P. EMPLOYEE_ID
                          FROM AT_CHOSEN_EMP P
                         WHERE P.REQUEST_ID = PV_REQUEST_ID);
    
    --==Insert thong tin da tin vao table that
    DELETE FROM AT_TIME_TIMESHEET_MACHINET T
     WHERE T.REQUEST_ID = PV_REQUEST_ID;
   /* DELETE FROM AT_TIME_TIMESHEET_MACHINE_TMP T
     WHERE T.REQUEST_ID = PV_REQUEST_ID;*/
    PV_SQL := '
        INSERT INTO AT_TIME_TIMESHEET_MACHINET
        SELECT  p.* FROM ' || PV_TBL_NAME ||
              ' P WHERE REQUEST_ID=' || PV_REQUEST_ID || '
    ';
    --INSERT INTO TEMP(TEXT)VALUES(PV_SQL_CRE_TB);
    --INSERT INTO SQL_TEST(SQL_TEXT,CREATE_DATE) VALUES(PV_SQL,SYSDATE);
    EXECUTE IMMEDIATE PV_SQL;
    
UPDATE AT_TIME_TIMESHEET_MACHINET T
   SET T.MIN_NIGHT = (CASE
                       WHEN NVL(T.MIN_NIGHT, 0) <> 0 THEN
                        TRUNC(T.MIN_NIGHT) || '.' ||
                        ROUND(((T.MIN_NIGHT) - TRUNC(T.MIN_NIGHT)) * 60, 0)
                     END)

 WHERE T.REQUEST_ID = PV_REQUEST_ID;
    
   /* PV_SQL := '
        INSERT INTO AT_TIME_TIMESHEET_MACHINE_TMP
        SELECT  p.* FROM ' || PV_TBL_NAME ||
              ' P WHERE REQUEST_ID=' || PV_REQUEST_ID || '
    ';
  
    EXECUTE IMMEDIATE PV_SQL;
  */
    PV_SQL_CRE_TB := '
            DROP TABLE ' || PV_TBL_NAME || '
    ';
    --INSERT INTO TEMP(TEXT)VALUES(PV_SQL_CRE_TB);
    
    EXECUTE IMMEDIATE PV_SQL_CRE_TB;
    /*INSERT INTO AT_TIME_TIMESHEET_MACHINET T
    SELECT DISTINCT P.*
      FROM AT_TIME_TIMESHEET_MACHINE_TEMP P
     WHERE P.REQUEST_ID = PV_REQUEST_ID;*/
  /*
    --==End Chuc nang bang cong goc
    --==Start Chuc nang tong hop cong
    DELETE FROM AT_TIME_TIMESHEET_DAILY D
     WHERE D.EMPLOYEE_ID IN
           (SELECT EMPLOYEE_ID
              FROM At_Chosen_Emp_Tmp O
             WHERE O.REQUEST_ID = PV_REQUEST_ID)
       AND EXISTS
     (SELECT D.ID
              FROM AT_TIME_TIMESHEET_DAILY D_CUR
             WHERE D_CUR.EMPLOYEE_ID = D.EMPLOYEE_ID
               AND D_CUR.WORKINGDAY = D.WORKINGDAY
               AND (D_CUR.CREATED_BY = 'AUTO' OR P_DELETE_ALL <> 0))
       AND D.WORKINGDAY >= PV_FROMDATE
       AND D.WORKINGDAY <= PV_ENDDATE;
  
    INSERT INTO AT_TIME_TIMESHEET_DAILY T
      (ID,
       T.EMPLOYEE_ID,
       T.ORG_ID,
       T.TITLE_ID,
       T.WORKINGDAY,
       T.SHIFT_CODE,
       T.SHIFT_ID,
       T.WORKINGHOUR,
       T.MANUAL_ID,
       T.CREATED_DATE,
       T.CREATED_BY,
       T.CREATED_LOG,
       T.MODIFIED_DATE,
       T.MODIFIED_BY,
       T.MODIFIED_LOG,
       \* VALIN1,
       VALIN2,
       VALIN3,
       VALIN4,
       VALIN5,
       VALIN6,
       VALIN7,
       VALIN8,
       VALIN9,
       VALIN10,
       VALIN11,
       VALIN12,
       VALIN13,
       VALIN14,
       VALIN15,
       VALIN16,
       VALOUT1,
       VALOUT2,
       VALOUT3,
       VALOUT4,
       VALOUT5,
       VALOUT6,
       VALOUT7,
       VALOUT8,
       VALOUT9,
       VALOUT10,
       VALOUT11,
       VALOUT12,
       VALOUT13,
       VALOUT14,
       VALOUT15,
       VALOUT16,*\
       T.TIMEVALIN,
       TIMEVALOUT,
       OBJECT_ATTENDANCE,
       MIN_IN_WORK,
       MIN_DEDUCT_WORK,
       MIN_ON_LEAVE,
       MIN_LATE,
       MIN_EARLY,
       MIN_OUT_WORK_DEDUCT,
       MIN_LATE_EARLY,
       MIN_OUT_WORK,
       MIN_DEDUCT)
      SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
             M.EMPLOYEE_ID,
             M.ORG_ID,
             M.TITLE_ID,
             M.WORKINGDAY,
             M.SHIFT_CODE,
             M.SHIFT_ID,
             CASE
               WHEN NVL(L.EMPLOYEE_ID, 0) > 0 AND L.Leave_Day IS NOT NULL THEN
                0 --==UU TIEN XET DANG KY NGHI
               ELSE
                CASE
                  WHEN O.CODE = 'KCC' THEN
                   (SELECT WORK_HOUR FROM AT_SHIFT WHERE CODE = 'HC')
                  WHEN M.TIMEVALIN IS NOT NULL THEN --==CO GIO VAO-RA SE LAY CA DI LAM [X]
                   (SELECT WORK_HOUR FROM AT_SHIFT WHERE CODE = 'HC')
                  ELSE
                   0 --==K CO GIO VAO-RA LAY NGHI K LY DO
                END
             END WORKINGHOUR,
             M.SHIFT_TYPE_ID AS MANUAL_ID,
             
             SYSDATE,
             'AUTO',
             UPPER(P_USERNAME),
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME),
             \* VALIN1,
             VALIN2,
             VALIN3,
             VALIN4,
             VALIN5,
             VALIN6,
             VALIN7,
             VALIN8,
             VALIN9,
             VALIN10,
             VALIN11,
             VALIN12,
             VALIN13,
             VALIN14,
             VALIN15,
             VALIN16,
             VALOUT1,
             VALOUT2,
             VALOUT3,
             VALOUT4,
             VALOUT5,
             VALOUT6,
             VALOUT7,
             VALOUT8,
             VALOUT9,
             VALOUT10,
             VALOUT11,
             VALOUT12,
             VALOUT13,
             VALOUT14,
             VALOUT15,
             VALOUT16,*\
             M.TIMEVALIN,
             M.TIMEVALOUT,
             M.OBJECT_ATTENDANCE,
             M.MIN_IN_WORK,
             M.MIN_DEDUCT_WORK,
             M.MIN_ON_LEAVE,
             M.MIN_LATE,
             M.MIN_EARLY,
             M.MIN_OUT_WORK_DEDUCT,
             M.MIN_LATE_EARLY,
             M.MIN_OUT_WORK,
             M.MIN_DEDUCT
        FROM AT_TIME_TIMESHEET_MACHINE_TMP M
        LEFT JOIN AT_LEAVESHEET_DETAIL_TMP L
          ON M.EMPLOYEE_ID = L.EMPLOYEE_ID
         AND M.WORKINGDAY = L.LEAVE_DAY
        LEFT JOIN AT_SHIFT SH
          ON SH.ID = M.SHIFT_ID
        LEFT JOIN OT_OTHER_LIST O
          ON M.OBJECT_ATTENDANCE = O.ID
       WHERE M.REQUEST_ID = PV_REQUEST_ID
         AND NOT EXISTS (SELECT D.ID
                FROM AT_TIME_TIMESHEET_DAILY D
               WHERE D.EMPLOYEE_ID = M.EMPLOYEE_ID
                 AND D.WORKINGDAY = M.WORKINGDAY);
  */
   /* INSERT INTO TEMP1
      (TMP, WCODE, EXEDATE, TYPE)
    VALUES
      ('INSERT INTO AT_CHOSEN_EMP', '12', SYSDATE, 400);
    COMMIT;*/
    --Cham cong chi tiet hang ngay
    /* INSERT INTO AT_TIME_TIMESHEET_DAILY T
    (T.ID,
     T.EMPLOYEE_ID,
     T.ORG_ID,
     T.TITLE_ID,
     T.WORKINGDAY,
     T.MANUAL_ID,
     T.CREATED_DATE,
     T.CREATED_BY,
     T.CREATED_LOG,
     T.MODIFIED_DATE,
     T.MODIFIED_BY,
     T.MODIFIED_LOG)
    SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
           EMP.EMPLOYEE_ID,
           EMP.ORG_ID,
           EMP.TITLE_ID,
           H.WORKINGDAY,
           23, --==L
           SYSDATE,
           'AUTO',
           UPPER(P_USERNAME),
           SYSDATE,
           UPPER(P_USERNAME),
           UPPER(P_USERNAME)
      FROM AT_CHOSEN_EMP_TMP EMP,
           (SELECT DISTINCT H.WORKINGDAY
              FROM AT_HOLIDAY H
             WHERE H.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE) H
     WHERE H.WORKINGDAY >= EMP.JOIN_DATE and emp.request_id=PV_REQUEST_ID
       AND
          -- EMP.EMPLOYEE_ID NOT IN (SELECT T.EMPLOYEE_ID FROM AT_TIME_TIMESHEET_DAILY T WHERE T.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE );
           NOT EXISTS (SELECT D.ID
              FROM AT_TIME_TIMESHEET_DAILY D
             WHERE D.EMPLOYEE_ID = EMP.EMPLOYEE_ID
               AND D.WORKINGDAY = H.WORKINGDAY);*/
  
    DELETE FROM AT_CHOSEN_ORG WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_CHOSEN_EMP_CLEAR_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_CHOSEN_EMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_SWIPE_DATA_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_WORKSIGN_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_TIME_TIMESHEET_MACHINE_TMP
     WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_LEAVESHEET_DETAIL_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_CHOSEN_EMP_CLEAR_TMP WHERE REQUEST_ID = PV_REQUEST_ID;           
    P_OUT := 3;
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
    
      P_OUT := 4;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_ALL_NEW',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              P_ORG_ID,
                              P_PERIOD_ID,
                              P_USERNAME,
                              PV_REQUEST_ID,
                              PV_TBL_NAME,
                              PV_SQL);
      PV_TBL_NAME := TRIM('AT_TIME_TIMESHEET_MACHINE_TEMP' ||
                          TRIM(TO_CHAR(PV_REQUEST_ID)));
      BEGIN
        PV_SQL_CRE_TB := '
            DROP TABLE ' || PV_TBL_NAME || '
       ';
        --INSERT INTO TEMP(TEXT)VALUES(PV_SQL_CRE_TB);
        --COMMIT;
        EXECUTE IMMEDIATE PV_SQL_CRE_TB;
      EXCEPTION
        WHEN OTHERS THEN
          P_OUT := 4;
      END;
      DELETE FROM AT_CHOSEN_ORG WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_CHOSEN_EMP_CLEAR_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_CHOSEN_EMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_SWIPE_DATA_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_WORKSIGN_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_TIME_TIMESHEET_MACHINE_TMP
       WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_LEAVESHEET_DETAIL_TMP
       WHERE REQUEST_ID = PV_REQUEST_ID;
      COMMIT;
  END;

  /* PROCEDURE CAL_TIME_TIMESHEET_ALL(P_USERNAME   IN NVARCHAR2,
                                     P_ORG_ID     IN NUMBER,
                                     P_PERIOD_ID  IN NUMBER,
                                     P_ISDISSOLVE IN NUMBER,
                                     P_DELETE_ALL IN NUMBER := 1) IS
      PV_FROMDATE    DATE;
      PV_ENDDATE     DATE;
      PV_SQL         CLOB;
      PV_REQUEST_ID  NUMBER;
      PV_MINUS_ALLOW NUMBER := 50;
      PV_SUNDAY      DATE; --Lay ngay chu nhat trong thang
      PV_TEST1       NUMBER;
      PV_TEST2       NUMBER;
      PV_CHECK       NUMBER;
      PV_DEL_ALL     NUMBER;
      PV_CHECKNV     NUMBER;
      PV_TG_BD_CA    DATE;
      PV_TG_KT_CA    DATE;
      PV_TG_BD_NGHI  DATE;
      PV_TG_KT_NGHI  DATE;
      PV_IN_MIN      DATE;
      PV_OUT_MAX     DATE;
    BEGIN
      return;
      PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
      PV_DEL_ALL    := P_DELETE_ALL;
  
      SELECT P.START_DATE, P.END_DATE
        INTO PV_FROMDATE, PV_ENDDATE
        FROM AT_PERIOD P
       WHERE P.ID = P_PERIOD_ID;
      -- Insert org can tinh toan
      INSERT INTO AT_CHOSEN_ORG E --temp
        (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
           FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                      P_ORG_ID,
                                      P_ISDISSOLVE)) O);
      INSERT INTO AT_CHOSEN_EMP_CLEAR -- temp
        (EMPLOYEE_ID)
        SELECT DISTINCT S.EMPLOYEE_ID
          FROM AT_TIME_TIMESHEET_MACHINET S
         INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
            ON O.ORG_ID = S.ORG_ID
         WHERE (S.CREATED_BY = 'AUTO')
           AND S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
  
      SELECT COUNT(1)
        INTO PV_CHECKNV
        FROM AT_TIME_TIMESHEET_MACHINET S
       INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
          ON O.ORG_ID = S.ORG_ID
       WHERE S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
  
      SELECT COUNT(1) INTO PV_CHECK FROM AT_CHOSEN_EMP_CLEAR;
      IF (PV_CHECK = 0 AND PV_CHECKNV = 0) OR P_DELETE_ALL <> 0 THEN
        PV_DEL_ALL := 1;
      END IF;
  
      IF PV_DEL_ALL = 1 THEN
        --==TINH LAI TAT CA CA NV
        -- insert emp can tinh toan
        INSERT INTO AT_CHOSEN_EMP --temp
          (EMPLOYEE_ID,
           ITIME_ID,
           ORG_ID,
           TITLE_ID,
           STAFF_RANK_ID,
           STAFF_RANK_LEVEL,
           TER_EFFECT_DATE,
           USERNAME,
           REQUEST_ID,
           JOIN_DATE,
           JOIN_DATE_STATE,
           DECISION_ID,
           OBJECT_ATTENDANCE)
          (SELECT T.ID,
                  T.ITIME_ID,
                  W.ORG_ID,
                  W.TITLE_ID,
                  W.STAFF_RANK_ID,
                  W.LEVEL_STAFF,
                  CASE
                    WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                     T.TER_EFFECT_DATE + 1
                    ELSE
                     NULL
                  END TER_EFFECT_DATE,
                  UPPER(P_USERNAME),
                  PV_REQUEST_ID,
                  T.JOIN_DATE,
                  T.JOIN_DATE_STATE,
                  W.ID DECISION_ID,
                  W.OBJECT_ATTENDANCE
             FROM HU_EMPLOYEE T
            INNER JOIN (SELECT E.ID,
                              E.EMPLOYEE_ID,
                              E.TITLE_ID,
                              E.ORG_ID,
                              E.IS_3B,
                              E.STAFF_RANK_ID,
                              S.LEVEL_STAFF,
                              E.OBJECT_ATTENDANCE,
                              ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                         FROM HU_WORKING E
                         LEFT JOIN HU_STAFF_RANK S
                           ON E.STAFF_RANK_ID = S.ID
                        WHERE E.EFFECT_DATE <= PV_ENDDATE
                          AND E.STATUS_ID = 447
                          AND E.IS_WAGE = 0
                          AND E.IS_3B = 0) W
               ON T.ID = W.EMPLOYEE_ID
              AND W.ROW_NUMBER = 1
            INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
               ON O.ORG_ID = W.ORG_ID
            WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                  (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
      ELSE
        --==CHI TINH NV AUTO
        -- insert emp can tinh toan
        INSERT INTO AT_CHOSEN_EMP
          (EMPLOYEE_ID,
           ITIME_ID,
           ORG_ID,
           TITLE_ID,
           STAFF_RANK_ID,
           STAFF_RANK_LEVEL,
           TER_EFFECT_DATE,
           USERNAME,
           REQUEST_ID,
           JOIN_DATE,
           JOIN_DATE_STATE,
           DECISION_ID,
           OBJECT_ATTENDANCE)
          (SELECT T.ID,
                  T.ITIME_ID,
                  W.ORG_ID,
                  W.TITLE_ID,
                  W.STAFF_RANK_ID,
                  W.LEVEL_STAFF,
                  CASE
                    WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                     T.TER_EFFECT_DATE + 1
                    ELSE
                     NULL
                  END TER_EFFECT_DATE,
                  UPPER(P_USERNAME),
                  PV_REQUEST_ID,
                  T.JOIN_DATE,
                  T.JOIN_DATE_STATE,
                  W.ID DECISION_ID,
                  W.OBJECT_ATTENDANCE
             FROM HU_EMPLOYEE T
            INNER JOIN AT_CHOSEN_EMP_CLEAR C --==CHI TINH NV AUTO
               ON T.ID = C.EMPLOYEE_ID
            INNER JOIN (SELECT E.ID,
                              E.EMPLOYEE_ID,
                              E.TITLE_ID,
                              E.ORG_ID,
                              E.IS_3B,
                              E.STAFF_RANK_ID,
                              S.LEVEL_STAFF,
                              E.OBJECT_ATTENDANCE,
                              ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                         FROM HU_WORKING E
                         LEFT JOIN HU_STAFF_RANK S
                           ON E.STAFF_RANK_ID = S.ID
                        WHERE E.EFFECT_DATE <= PV_ENDDATE
                          AND E.STATUS_ID = 447
                          AND E.IS_WAGE = 0
                          AND E.IS_3B = 0) W
               ON T.ID = W.EMPLOYEE_ID
              AND W.ROW_NUMBER = 1
            INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
               ON O.ORG_ID = W.ORG_ID
            WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                  (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
      END IF;
  
      SELECT COUNT(1) INTO PV_TEST1 FROM AT_CHOSEN_ORG T;
      SELECT COUNT(1) INTO PV_TEST2 FROM AT_CHOSEN_EMP T;
      INSERT INTO TEMP1
        (TMP, WCODE, EXEDATE, TYPE)
      VALUES
        ('INSERT INTO AT_CHOSEN_EMP' || PV_TEST1 || '-' || PV_TEST2 || '-' ||
         PV_DEL_ALL,
         '1',
         SYSDATE,
         400);
      --RETURN;
      --==Start Chuc nang bang cong goc
      --==insert data v?o bang tam de tinh toan
      UPDATE AT_SWIPE_DATA S
         SET S.EMPLOYEE_ID = NVL((SELECT ID
                                   FROM HU_EMPLOYEE E
                                  WHERE E.ITIME_ID = S.ITIME_ID
                                    AND NVL(E.IS_KIEM_NHIEM, 0) = 0),
                                 0);
      INSERT INTO AT_TIME_TIMESHEET_MACHINE_TEMP M --temp
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         WORKINGDAY,
         SHIFT_ID,
         SHIFT_CODE,
         CREATED_DATE,
         --CREATED_BY,
         REQUEST_ID,
         VALIN1,
         VALOUT1,
         TIMEVALIN,
         TIMEVALOUT,
         TIMEIN_REALITY, --GIO VAO TT
         TIMEOUT_REALITY, -- GIO RA THUC TE
         OBJECT_ATTENDANCE,
         OBJECT_ATTENDANCE_CODE,
         WORK_HOUR,
         HOURS_STOP,
         HOURS_START,
         START_MID_HOURS,
         END_MID_HOURS,
         SHIFT_DAY,
         HOURS_STAR_CHECKIN,
         HOURS_STAR_CHECKOUT,
         NOTE,
         --MIN_ON_LEAVE,
         MANUAL_ID,
         STATUS_SHIFT,
         MANUAL_CODE,
         MORNING_RATE,
         AFTERNOON_RATE,
         DAY_NUM,
         STATUS_SHIFT_NAME,
         CREATED_BY)
        SELECT SEQ_AT_TIME_TIMESHEET_MACHINET.NEXTVAL,
               T.EMPLOYEE_ID,
               T.ORG_ID,
               T.TITLE_ID,
               T.STAFF_RANK_ID,
               WS.WORKINGDAY,
               WS.SHIFT_ID,
               SH.CODE SHIFT_CODE,
               SYSDATE,
               --P_USERNAME,
               PV_REQUEST_ID,
               CASE
                 WHEN SH.CODE = 'OFF' THEN
                  DATA_IN.VALTIME
                 ELSE
                  PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                         WS.WORKINGDAY,
                                         WS.SHIFT_ID,
                                         T.OBJECT_ATTENDANCE,
                                         CASE
                                           WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                            NVL(LEAVE.STATUS_SHIFT, 0)
                                           ELSE
                                            3
                                         END,
                                         0,
                                         PV_REQUEST_ID)
               END,
  
               CASE
                 WHEN SH.CODE = 'OFF' THEN
                  DATA_OUT.VALTIME
                 ELSE
                  PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                         WS.WORKINGDAY,
                                         WS.SHIFT_ID,
                                         T.OBJECT_ATTENDANCE,
                                         CASE
                                           WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                            NVL(LEAVE.STATUS_SHIFT, 0)
                                           ELSE
                                            3
                                         END,
                                         1,
                                         PV_REQUEST_ID)
               END,
  
               CASE
                 WHEN SH.CODE = 'OFF' THEN
                  DATA_IN.VALTIME
                 ELSE
                  PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                         WS.WORKINGDAY,
                                         WS.SHIFT_ID,
                                         T.OBJECT_ATTENDANCE,
                                         CASE
                                           WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                            NVL(LEAVE.STATUS_SHIFT, 0)
                                           ELSE
                                            3
                                         END,
                                         0,
                                         PV_REQUEST_ID)
               END,
  
               CASE
                 WHEN SH.CODE = 'OFF' THEN
                  DATA_OUT.VALTIME
                 ELSE
                  PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                         WS.WORKINGDAY,
                                         WS.SHIFT_ID,
                                         T.OBJECT_ATTENDANCE,
                                         CASE
                                           WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                            NVL(LEAVE.STATUS_SHIFT, 0)
                                           ELSE
                                            3
                                         END,
                                         1,
                                         PV_REQUEST_ID)
               END,
               CASE
                 WHEN SH.CODE = 'OFF' THEN
                  DATA_IN.VALTIME
                 ELSE
                  I.TIMEIN_REALITY
               END,
  
               CASE
                 WHEN SH.CODE = 'OFF' THEN
                  DATA_OUT.VALTIME
                 ELSE
                  I.TIMEOUT_REALITY
               END,
               T.OBJECT_ATTENDANCE,
               OL.CODE OBJECT_ATTENDANCE_CODE,
               SH.WORK_HOUR,
               TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                       TO_CHAR(SH.HOURS_STOP, 'HH24:MI'),
                       'DD/MM/RRRR HH24:MI'),
               TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                       TO_CHAR(SH.HOURS_START, 'HH24:MI'),
                       'DD/MM/RRRR HH24:MI'),
               TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                       TO_CHAR(SH.START_MID_HOURS, 'HH24:MI'),
                       'DD/MM/RRRR HH24:MI'),
               TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                       TO_CHAR(SH.END_MID_HOURS, 'HH24:MI'),
                       'DD/MM/RRRR HH24:MI'),
               SH.SHIFT_DAY,
               TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                       TO_CHAR(SH.HOURS_STAR_CHECKIN, 'HH24:MI'),
                       'DD/MM/RRRR HH24:MI'),
               TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                       TO_CHAR(SH.HOURS_STAR_CHECKOUT, 'HH24:MI'),
                       'DD/MM/RRRR HH24:MI'),
               I.NOTE,
               --LEAVE.DAY_NUM,
               LEAVE.MANUAL_ID,
               LEAVE.STATUS_SHIFT,
               FM.CODE,
               MR.VALUE_RATE,
               AF.VALUE_RATE,
               LEAVE.DAY_NUM,
               CASE
                 WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 0 THEN
                  TO_CHAR(UNISTR(''))
                 WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 1 THEN
                  TO_CHAR(UNISTR('\0110\00E2\0300u ca'))
                 WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 2 THEN
                  TO_CHAR(UNISTR('cu\00F4\0301i ca'))
               END,
               CASE
                 WHEN WS.EMPLOYEE_ID || TO_CHAR(WS.WORKINGDAY, 'DDMMYYYY') IN
                      (SELECT T.EMPLOYEE_ID ||
                              TO_CHAR(T.WORKING_DAY, 'DDMMYYYY')
                         FROM AT_TIMESHEET_MACHINET_IMPORT T
                        WHERE T.EMPLOYEE_ID = WS.EMPLOYEE_ID
                          AND T.WORKING_DAY = WS.WORKINGDAY) THEN
                  P_USERNAME
                 ELSE
                  CAST('AUTO' AS NVARCHAR2(20))
               END
          FROM AT_WORKSIGN WS
         INNER JOIN AT_CHOSEN_EMP T
            ON T.EMPLOYEE_ID = WS.EMPLOYEE_ID
           AND T.REQUEST_ID = PV_REQUEST_ID
          LEFT JOIN AT_TIMESHEET_MACHINET_IMPORT I
            ON I.EMPLOYEE_ID = T.EMPLOYEE_ID
           AND I.WORKING_DAY = WS.WORKINGDAY
          LEFT JOIN AT_SHIFT SH
            ON WS.SHIFT_ID = SH.ID
          LEFT JOIN AT_LEAVESHEET_DETAIL LEAVE
            ON WS.EMPLOYEE_ID = LEAVE.EMPLOYEE_ID
           AND TRUNC(WS.WORKINGDAY) = TRUNC(LEAVE.LEAVE_DAY)
          LEFT JOIN AT_TIME_MANUAL FM
            ON FM.ID = LEAVE.MANUAL_ID
          LEFT JOIN AT_TIME_MANUAL_RATE MR
            ON MR.ID = FM.MORNING_RATE_ID
          LEFT JOIN AT_TIME_MANUAL_RATE AF
            ON AF.ID = FM.AFTERNOON_RATE_ID
          LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                            SW.WORKINGDAY,
                            MIN(SW.VALTIME) AS VALTIME
                       FROM AT_SWIPE_DATA SW
                      WHERE SW.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
                      GROUP BY SW.EMPLOYEE_ID, SW.WORKINGDAY) DATA_IN
            ON DATA_IN.EMPLOYEE_ID = T.EMPLOYEE_ID
           AND DATA_IN.WORKINGDAY = WS.WORKINGDAY
          LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                            SW.WORKINGDAY,
                            MAX(SW.VALTIME) AS VALTIME
                       FROM AT_SWIPE_DATA SW
                      WHERE SW.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
                      GROUP BY SW.EMPLOYEE_ID, SW.WORKINGDAY) DATA_OUT
            ON DATA_OUT.EMPLOYEE_ID = T.EMPLOYEE_ID
           AND DATA_OUT.WORKINGDAY = WS.WORKINGDAY
          LEFT JOIN OT_OTHER_LIST OL
            ON T.OBJECT_ATTENDANCE = OL.ID;
  
      SELECT COUNT(1) INTO PV_TEST1 FROM AT_CHOSEN_ORG T;
      SELECT COUNT(1) INTO PV_TEST2 FROM AT_TIME_TIMESHEET_MACHINE_TEMP T;
      INSERT INTO TEMP1
        (TMP, WCODE, EXEDATE, TYPE)
      VALUES
        ('INSERT INTO AT_CHOSEN_EMP' || PV_TEST1 || '-' || PV_TEST2 || '-' ||
         PV_DEL_ALL,
         '2',
         SYSDATE,
         400);
      --insert into AT_TIME_TIMESHEET_MACHINE_TEMP_1 select * from AT_TIME_TIMESHEET_MACHINE_TEMP;
      --== ap dung cong thuc
      FOR CUR_ITEM IN (SELECT *
                         FROM AT_TIME_FORMULAR T
                        WHERE T.TYPE IN (5)
                          AND T.STATUS = 1
                        ORDER BY T.ORDERBY) LOOP
        PV_SQL := 'UPDATE AT_TIME_TIMESHEET_MACHINE_TEMP T SET ' ||
                  CUR_ITEM.FORMULAR_CODE || '= NVL((' ||
                  CUR_ITEM.FORMULAR_VALUE || '),NULL)';
        INSERT INTO TEMP1
          (TMP, WCODE)
        VALUES
          (PV_SQL, 'CUR_ITEM.FORMULAR_CODE');
        BEGIN
          EXECUTE IMMEDIATE PV_SQL;
        EXCEPTION
          WHEN OTHERS THEN
            CONTINUE;
        END;
      END LOOP;
      -- XOA DU LIEU CU TRUOC KHI TINH
      DELETE FROM AT_TIME_TIMESHEET_MACHINET D
       WHERE D.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
         AND D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID
                                 FROM AT_TIME_TIMESHEET_MACHINE_TEMP P
                               -- WHERE P.REQUEST_ID=PV_REQUEST_ID
                               );
  
      --==Insert thong tin da tin vao table that
      INSERT INTO AT_TIME_TIMESHEET_MACHINET T
        SELECT DISTINCT P.*
          FROM AT_TIME_TIMESHEET_MACHINE_TEMP P
         WHERE P.REQUEST_ID = PV_REQUEST_ID
           AND P.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
      SELECT COUNT(1) INTO PV_TEST1 FROM AT_CHOSEN_EMP T;
      SELECT COUNT(1) INTO PV_TEST2 FROM AT_TIME_TIMESHEET_MACHINET T;
      INSERT INTO TEMP1
        (TMP, WCODE, EXEDATE, TYPE)
      VALUES
        ('INSERT INTO AT_CHOSEN_EMP' || PV_TEST1 || '-' || PV_TEST2 || '-' ||
         PV_DEL_ALL,
         '3',
         SYSDATE,
         400);
    EXCEPTION
      WHEN OTHERS THEN
        SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                                'PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_ALL',
                                SQLERRM || '_' ||
                                DBMS_UTILITY.format_error_backtrace,
                                P_ORG_ID,
                                P_PERIOD_ID,
                                P_USERNAME,
                                PV_REQUEST_ID);
  
        RETURN;
        --==End Chuc nang bang cong goc
        --==Start Chuc nang tong hop cong
        DELETE FROM AT_TIME_TIMESHEET_DAILY D
         WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
           AND EXISTS
         (SELECT D.ID
                  FROM AT_TIME_TIMESHEET_DAILY D_CUR
                 WHERE D_CUR.EMPLOYEE_ID = D.EMPLOYEE_ID
                   AND D_CUR.WORKINGDAY = D.WORKINGDAY
                   AND (D_CUR.CREATED_BY = 'AUTO' OR P_DELETE_ALL <> 0))
           AND D.WORKINGDAY >= PV_FROMDATE
           AND D.WORKINGDAY <= PV_ENDDATE;
  
        INSERT INTO AT_TIME_TIMESHEET_DAILY T
          (ID,
           T.EMPLOYEE_ID,
           T.ORG_ID,
           T.TITLE_ID,
           T.WORKINGDAY,
           T.SHIFT_CODE,
           T.SHIFT_ID,
           T.WORKINGHOUR,
           T.MANUAL_ID,
           T.CREATED_DATE,
           T.CREATED_BY,
           T.CREATED_LOG,
           T.MODIFIED_DATE,
           T.MODIFIED_BY,
           T.MODIFIED_LOG,
           VALIN1,
           VALIN2,
           VALIN3,
           VALIN4,
           VALIN5,
           VALIN6,
           VALIN7,
           VALIN8,
           VALIN9,
           VALIN10,
           VALIN11,
           VALIN12,
           VALIN13,
           VALIN14,
           VALIN15,
           VALIN16,
           VALOUT1,
           VALOUT2,
           VALOUT3,
           VALOUT4,
           VALOUT5,
           VALOUT6,
           VALOUT7,
           VALOUT8,
           VALOUT9,
           VALOUT10,
           VALOUT11,
           VALOUT12,
           VALOUT13,
           VALOUT14,
           VALOUT15,
           VALOUT16,
           T.TIMEVALIN,
           TIMEVALOUT,
           OBJECT_ATTENDANCE,
           MIN_IN_WORK,
           MIN_DEDUCT_WORK,
           MIN_ON_LEAVE,
           MIN_LATE,
           MIN_EARLY,
           MIN_OUT_WORK_DEDUCT,
           MIN_LATE_EARLY,
           MIN_OUT_WORK,
           MIN_DEDUCT)
          SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
                 M.EMPLOYEE_ID,
                 M.ORG_ID,
                 M.TITLE_ID,
                 M.WORKINGDAY,
                 M.SHIFT_CODE,
                 M.SHIFT_ID,
                 CASE
                   WHEN NVL(L.EMPLOYEE_ID, 0) > 0 AND L.Leave_Day IS NOT NULL THEN
                    0 --==UU TIEN XET DANG KY NGHI
                   ELSE
                    CASE
                      WHEN O.CODE = 'KCC' THEN
                       (SELECT WORK_HOUR FROM AT_SHIFT WHERE CODE = 'HC')
                      WHEN M.TIMEVALIN IS NOT NULL THEN --==CO GIO VAO-RA SE LAY CA DI LAM [X]
                       (SELECT WORK_HOUR FROM AT_SHIFT WHERE CODE = 'HC')
                      ELSE
                       0 --==K CO GIO VAO-RA LAY NGHI K LY DO
                    END
                 END WORKINGHOUR,
                 M.SHIFT_TYPE_ID AS MANUAL_ID,
  
                 SYSDATE,
                 'AUTO',
                 UPPER(P_USERNAME),
                 SYSDATE,
                 UPPER(P_USERNAME),
                 UPPER(P_USERNAME),
                 VALIN1,
                 VALIN2,
                 VALIN3,
                 VALIN4,
                 VALIN5,
                 VALIN6,
                 VALIN7,
                 VALIN8,
                 VALIN9,
                 VALIN10,
                 VALIN11,
                 VALIN12,
                 VALIN13,
                 VALIN14,
                 VALIN15,
                 VALIN16,
                 VALOUT1,
                 VALOUT2,
                 VALOUT3,
                 VALOUT4,
                 VALOUT5,
                 VALOUT6,
                 VALOUT7,
                 VALOUT8,
                 VALOUT9,
                 VALOUT10,
                 VALOUT11,
                 VALOUT12,
                 VALOUT13,
                 VALOUT14,
                 VALOUT15,
                 VALOUT16,
                 M.TIMEVALIN,
                 M.TIMEVALOUT,
                 M.OBJECT_ATTENDANCE,
                 M.MIN_IN_WORK,
                 M.MIN_DEDUCT_WORK,
                 M.MIN_ON_LEAVE,
                 M.MIN_LATE,
                 M.MIN_EARLY,
                 M.MIN_OUT_WORK_DEDUCT,
                 M.MIN_LATE_EARLY,
                 M.MIN_OUT_WORK,
                 M.MIN_DEDUCT
            FROM AT_TIME_TIMESHEET_MACHINE_TEMP M
            LEFT JOIN AT_LEAVESHEET_DETAIL L
              ON M.EMPLOYEE_ID = L.EMPLOYEE_ID
             AND M.WORKINGDAY = L.LEAVE_DAY
            LEFT JOIN AT_SHIFT SH
              ON SH.ID = M.SHIFT_ID
            LEFT JOIN OT_OTHER_LIST O
              ON M.OBJECT_ATTENDANCE = O.ID
           WHERE NOT EXISTS (SELECT D.ID
                    FROM AT_TIME_TIMESHEET_DAILY D
                   WHERE D.EMPLOYEE_ID = M.EMPLOYEE_ID
                     AND D.WORKINGDAY = M.WORKINGDAY);
  
        INSERT INTO TEMP1
          (TMP, WCODE, EXEDATE, TYPE)
        VALUES
          ('INSERT INTO AT_CHOSEN_EMP', '12', SYSDATE, 400);
  
        --Cham cong chi tiet hang ngay
        INSERT INTO AT_TIME_TIMESHEET_DAILY T
          (T.ID,
           T.EMPLOYEE_ID,
           T.ORG_ID,
           T.TITLE_ID,
           T.WORKINGDAY,
           T.MANUAL_ID,
           T.CREATED_DATE,
           T.CREATED_BY,
           T.CREATED_LOG,
           T.MODIFIED_DATE,
           T.MODIFIED_BY,
           T.MODIFIED_LOG)
          SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
                 EMP.EMPLOYEE_ID,
                 EMP.ORG_ID,
                 EMP.TITLE_ID,
                 H.WORKINGDAY,
                 23, --==L
                 SYSDATE,
                 'AUTO',
                 UPPER(P_USERNAME),
                 SYSDATE,
                 UPPER(P_USERNAME),
                 UPPER(P_USERNAME)
            FROM AT_CHOSEN_EMP EMP,
                 (SELECT DISTINCT H.WORKINGDAY
                    FROM AT_HOLIDAY H
                   WHERE H.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE) H
           WHERE
          -- EMP.EMPLOYEE_ID NOT IN (SELECT T.EMPLOYEE_ID FROM AT_TIME_TIMESHEET_DAILY T WHERE T.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE );
           NOT EXISTS (SELECT D.ID
              FROM AT_TIME_TIMESHEET_DAILY D
             WHERE D.EMPLOYEE_ID = EMP.EMPLOYEE_ID
               AND D.WORKINGDAY = H.WORKINGDAY);
        --==End Chuc nang tong hop cong
  
    END;
  *//*
  PROCEDURE CAL_TIMESHEET_DAILY(P_USERNAME   IN NVARCHAR2,
                                P_ORG_ID     IN NUMBER,
                                P_PERIOD_ID  IN NUMBER,
                                P_ISDISSOLVE IN NUMBER,
                                P_DELETE_ALL IN NUMBER := 1,
                                P_REQUEST_ID IN NUMBER,
                                P_OUT        OUT NUMBER) IS
    PV_CHECK             NUMBER;
    PV_DEL_ALL           NUMBER;
    PV_CHECKNV           NUMBER;
    PV_FROMDATE          DATE;
    PV_ENDDATE           DATE;
    PV_SQL               CLOB;
    PV_REQUEST_ID        NUMBER;
    PV_TBL_NAME          NVARCHAR2(50);
    PV_TOKEN             NVARCHAR2(40);
    PV_SQL_CRE_TB        NVARCHAR2(1000);
    PV_STARTDATE         DATE;
    PV_ENDDATE1          DATE;
    PV_DAYEND            NUMBER;
    V_COL1               CLOB;
    V_COL                CLOB;
    V_COL_V              CLOB;
    PV_HIERARCHICAL_PATH NVARCHAR2(20000);
  BEGIN
    --return;
    SELECT O.HIERARCHICAL_PATH
      INTO PV_HIERARCHICAL_PATH
      FROM HU_ORGANIZATION O
     WHERE O.ID = P_ORG_ID;
    --=======================================================
    SELECT standard_hash(TO_CHAR(P_ORG_ID) || P_USERNAME ||
                         TO_CHAR(P_PERIOD_ID),
                         'MD5')
      INTO PV_TOKEN
      FROM DUAL;
  
    SELECT COUNT(*)
      INTO PV_CHECK
      FROM user_tables UT
     WHERE UPPER(UT.TABLE_NAME) = UPPER(PV_TOKEN);
  
    IF PV_CHECK > 0 THEN
      PV_SQL_CRE_TB := '
            DROP TABLE ' || 'TBL' || PV_TOKEN || '
    ';
      --COMMIT;
      EXECUTE IMMEDIATE PV_SQL_CRE_TB;
    END IF;
    --=======================================================
  
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    PV_DEL_ALL    := P_DELETE_ALL;
  
    SELECT P.START_DATE, P.END_DATE
      INTO PV_FROMDATE, PV_ENDDATE
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
    -- Insert org can tinh toan
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG_TMP E --temp
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
    COMMIT;
  
    INSERT INTO AT_CHOSEN_EMP_CLEAR_TMP -- temp
      (EMPLOYEE_ID, REQUEST_ID)
      SELECT DISTINCT S.EMPLOYEE_ID, PV_REQUEST_ID
        FROM AT_TIME_TIMESHEET_DAILY S
       INNER JOIN (SELECT ORG_ID
                     FROM AT_CHOSEN_ORG_TMP O
                    WHERE O.REQUEST_ID = PV_REQUEST_ID) O
          ON O.ORG_ID = S.ORG_ID
       WHERE (S.CREATED_BY = 'AUTO')
         AND S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
    COMMIT;
  
    SELECT COUNT(1)
      INTO PV_CHECKNV
      FROM AT_TIME_TIMESHEET_DAILY S
     INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
        ON O.ORG_ID = S.ORG_ID
     WHERE S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
  
    SELECT COUNT(*) INTO PV_CHECK FROM AT_CHOSEN_EMP_CLEAR;
    IF (PV_CHECK = 0 AND PV_CHECKNV = 0) OR P_DELETE_ALL <> 0 THEN
      PV_DEL_ALL := 1;
    END IF;
  
    IF PV_DEL_ALL = 1 THEN
      --==TINH LAI TAT CA CA NV
      -- insert emp can tinh toan
      INSERT INTO AT_CHOSEN_EMP_TMP --temp
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE,
         DECISION_ID,
         OBJECT_ATTENDANCE)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE,
                W.ID DECISION_ID,
                W.OBJECT_ATTENDANCE
           FROM HU_EMPLOYEE T
          INNER JOIN (SELECT E.ID,
                            E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            E.OBJECT_ATTENDANCE,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_WAGE = 0
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID
                       FROM AT_CHOSEN_ORG_TMP O
                      WHERE O.REQUEST_ID = PV_REQUEST_ID) O
             ON O.ORG_ID = W.ORG_ID
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
    ELSE
      --==CHI TINH NV AUTO
      -- insert emp can tinh toan
      INSERT INTO AT_CHOSEN_EMP_TMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE,
         DECISION_ID,
         OBJECT_ATTENDANCE)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE,
                W.ID DECISION_ID,
                W.OBJECT_ATTENDANCE
           FROM HU_EMPLOYEE T
          INNER JOIN AT_CHOSEN_EMP_CLEAR_TMP C --==CHI TINH NV AUTO
             ON T.ID = C.EMPLOYEE_ID
            AND C.REQUEST_ID = PV_REQUEST_ID
          INNER JOIN (SELECT E.ID,
                            E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            E.OBJECT_ATTENDANCE,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_WAGE = 0
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID
                       FROM AT_CHOSEN_ORG_TMP O
                      WHERE O.REQUEST_ID = PV_REQUEST_ID) O
             ON O.ORG_ID = W.ORG_ID
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
    END IF;
    COMMIT;
  
    DELETE FROM AT_TIME_TIMESHEET_DAILY D
     WHERE D.EMPLOYEE_ID IN
           (SELECT EMPLOYEE_ID
              FROM At_Chosen_Emp_Tmp O
             WHERE O.REQUEST_ID = PV_REQUEST_ID)
       AND EXISTS
     (SELECT D.ID
              FROM AT_TIME_TIMESHEET_DAILY D_CUR
             WHERE D_CUR.EMPLOYEE_ID = D.EMPLOYEE_ID
               AND D_CUR.WORKINGDAY = D.WORKINGDAY
               AND (D_CUR.CREATED_BY = 'AUTO' OR PV_DEL_ALL <> 0))
       AND D.WORKINGDAY >= PV_FROMDATE
       AND D.WORKINGDAY <= PV_ENDDATE;
    COMMIT;
    INSERT INTO AT_TIME_TIMESHEET_MACHINE_TMP T
      SELECT d.id,
             d.employee_id,
             d.org_id,
             d.title_id,
             d.workingday,
             d.shift_code,
             d.leave_code,
             d.late,
             d.comebackout,
             d.workday_ot,
             d.workday_night,
             d.type_day,
             d.created_date,
             d.created_by,
             d.created_log,
             d.modified_date,
             d.modified_by,
             d.modified_log,
             d.workinghour,
             d.valin1,
             d.valin2,
             d.valin3,
             d.valin4,
             d.shift_id,
             d.manual_id,
             d.leave_id,
             d.workinghour_shift,
             d.number_swipe,
             d.is_holiday,
             d.is_fullday,
             d.shift_manual_id,
             d.is_noon,
             d.shift_hours_start,
             d.shift_hours_stop,
             d.breaks_form,
             d.breaks_to,
             d.minute_dm,
             d.minute_vs,
             d.request_id,
             d.staff_rank_id,
             d.valin5,
             d.valin6,
             d.valin7,
             d.valin8,
             d.valin9,
             d.valin10,
             d.valin11,
             d.valin12,
             d.valin13,
             d.valin14,
             d.valin15,
             d.valin16,
             d.valout1,
             d.valout2,
             d.valout3,
             d.valout4,
             d.valout5,
             d.valout6,
             d.valout7,
             d.valout8,
             d.valout9,
             d.valout10,
             d.valout11,
             d.valout12,
             d.valout13,
             d.valout14,
             d.valout15,
             d.valout16,
             d.timevalin,
             d.timevalout,
             d.object_attendance,
             d.min_in_work,
             d.min_out_work,
             d.min_deduct_work,
             d.min_on_leave,
             d.min_deduct,
             d.min_out_work_deduct,
             d.min_late,
             d.min_early,
             d.min_late_early,
             d.work_hour,
             d.timevalin_temp,
             d.object_attendance_code,
             d.hours_stop,
             d.hours_start,
             d.late_minutes,
             d.timein_reality,
             d.timeout_reality,
             d.start_mid_hours,
             d.end_mid_hours,
             d.working_value,
             d.shift_day,
             d.shift_type_code,
             d.shift_type_id,
             d.note,
             d.is_update,
             d.hours_star_checkin,
             d.hours_star_checkout,
             d.status_shift,
             d.manual_code,
             d.morning_rate,
             d.afternoon_rate,
             d.day_num,
             d.status_shift_name,
             d.late_hour,
             d.early_hour,
             d.obj_emp_id,
             d.status,
             d.period_id,
             d.min_night,
             D.ORG_ACCOUNTING,
             D.COUNT_SUPPORT,
             null,
             null,
             null
        FROM AT_TIME_TIMESHEET_MACHINET D
       INNER JOIN At_Chosen_Emp_Tmp EMP
          ON EMP.EMPLOYEE_ID = D.EMPLOYEE_ID
         AND EMP.REQUEST_ID = PV_REQUEST_ID
         AND D.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
  
    COMMIT;
  
    INSERT INTO AT_TIME_TIMESHEET_DAILY T
      (ID,
       T.EMPLOYEE_ID,
       T.ORG_ID,
       T.TITLE_ID,
       T.WORKINGDAY,
       T.SHIFT_CODE,
       T.SHIFT_ID,
       T.WORKINGHOUR,
       T.MANUAL_ID,
       T.CREATED_DATE,
       T.CREATED_BY,
       T.CREATED_LOG,
       T.MODIFIED_DATE,
       T.MODIFIED_BY,
       T.MODIFIED_LOG,
       \* VALIN1,
       VALIN2,
       VALIN3,
       VALIN4,
       VALIN5,
       VALIN6,
       VALIN7,
       VALIN8,
       VALIN9,
       VALIN10,
       VALIN11,
       VALIN12,
       VALIN13,
       VALIN14,
       VALIN15,
       VALIN16,
       VALOUT1,
       VALOUT2,
       VALOUT3,
       VALOUT4,
       VALOUT5,
       VALOUT6,
       VALOUT7,
       VALOUT8,
       VALOUT9,
       VALOUT10,
       VALOUT11,
       VALOUT12,
       VALOUT13,
       VALOUT14,
       VALOUT15,
       VALOUT16,*\
       T.TIMEVALIN,
       TIMEVALOUT,
       OBJECT_ATTENDANCE,
       MIN_IN_WORK,
       MIN_DEDUCT_WORK,
       MIN_ON_LEAVE,
       MIN_LATE,
       MIN_EARLY,
       MIN_OUT_WORK_DEDUCT,
       MIN_LATE_EARLY,
       MIN_OUT_WORK,
       MIN_DEDUCT)
      SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
             M.EMPLOYEE_ID,
             M.ORG_ID,
             M.TITLE_ID,
             M.WORKINGDAY,
             M.SHIFT_CODE,
             M.SHIFT_ID,
             CASE
               WHEN NVL(L.EMPLOYEE_ID, 0) > 0 AND L.Leave_Day IS NOT NULL THEN
                0 --==UU TIEN XET DANG KY NGHI
               ELSE
                CASE
                  WHEN O.CODE = 'KCC' THEN
                   (SELECT WORK_HOUR FROM AT_SHIFT WHERE CODE = 'HC')
                  WHEN M.TIMEVALIN IS NOT NULL THEN --==CO GIO VAO-RA SE LAY CA DI LAM [X]
                   (SELECT WORK_HOUR FROM AT_SHIFT WHERE CODE = 'HC')
                  ELSE
                   0 --==K CO GIO VAO-RA LAY NGHI K LY DO
                END
             END WORKINGHOUR,
             M.SHIFT_TYPE_ID AS MANUAL_ID,
             
             SYSDATE,
             'AUTO',
             UPPER(P_USERNAME),
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME),
             \*  VALIN1,
             VALIN2,
             VALIN3,
             VALIN4,
             VALIN5,
             VALIN6,
             VALIN7,
             VALIN8,
             VALIN9,
             VALIN10,
             VALIN11,
             VALIN12,
             VALIN13,
             VALIN14,
             VALIN15,
             VALIN16,
             VALOUT1,
             VALOUT2,
             VALOUT3,
             VALOUT4,
             VALOUT5,
             VALOUT6,
             VALOUT7,
             VALOUT8,
             VALOUT9,
             VALOUT10,
             VALOUT11,
             VALOUT12,
             VALOUT13,
             VALOUT14,
             VALOUT15,
             VALOUT16,*\
             M.TIMEVALIN,
             M.TIMEVALOUT,
             M.OBJECT_ATTENDANCE,
             M.MIN_IN_WORK,
             M.MIN_DEDUCT_WORK,
             M.MIN_ON_LEAVE,
             M.MIN_LATE,
             M.MIN_EARLY,
             M.MIN_OUT_WORK_DEDUCT,
             M.MIN_LATE_EARLY,
             M.MIN_OUT_WORK,
             M.MIN_DEDUCT
        FROM AT_TIME_TIMESHEET_MACHINE_TMP M
        LEFT JOIN AT_LEAVESHEET_DETAIL L
          ON M.EMPLOYEE_ID = L.EMPLOYEE_ID
         AND M.WORKINGDAY = L.LEAVE_DAY
        LEFT JOIN AT_SHIFT SH
          ON SH.ID = M.SHIFT_ID
        LEFT JOIN OT_OTHER_LIST O
          ON M.OBJECT_ATTENDANCE = O.ID
       WHERE M.REQUEST_ID = PV_REQUEST_ID
         AND NOT EXISTS (SELECT D.ID
                FROM AT_TIME_TIMESHEET_DAILY D
               WHERE D.EMPLOYEE_ID = M.EMPLOYEE_ID
                 AND D.WORKINGDAY = M.WORKINGDAY);
    COMMIT;
    --Cham cong chi tiet hang ngay
    \*INSERT INTO AT_TIME_TIMESHEET_DAILY T
    (T.ID,
     T.EMPLOYEE_ID,
     T.ORG_ID,
     T.TITLE_ID,
     T.WORKINGDAY,
     T.MANUAL_ID,
     T.CREATED_DATE,
     T.CREATED_BY,
     T.CREATED_LOG,
     T.MODIFIED_DATE,
     T.MODIFIED_BY,
     T.MODIFIED_LOG)
    SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
           EMP.EMPLOYEE_ID,
           EMP.ORG_ID,
           EMP.TITLE_ID,
           H.WORKINGDAY,
           23, --==L
           SYSDATE,
           'AUTO',
           UPPER(P_USERNAME),
           SYSDATE,
           UPPER(P_USERNAME),
           UPPER(P_USERNAME)
      FROM At_Chosen_Emp_Tmp EMP,
           (SELECT DISTINCT H.WORKINGDAY
              FROM AT_HOLIDAY H
             WHERE H.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE) H
     WHERE EMP.JOIN_DATE <= H.WORKINGDAY AND EMP.REQUEST_ID=PV_REQUEST_ID
       AND NOT EXISTS (SELECT D.ID
              FROM AT_TIME_TIMESHEET_DAILY D
             WHERE D.EMPLOYEE_ID = EMP.EMPLOYEE_ID
               AND D.WORKINGDAY = H.WORKINGDAY)
       AND H.WORKINGDAY >= EMP.JOIN_DATE;*\
    --==End Chuc nang tong hop cong
    COMMIT;
  
    --===tAO CAHCE===========================================================>
    INSERT INTO AT_TIME_TIMESHEET_DAILY_TMP
      SELECT D.*, PV_REQUEST_ID
        FROM AT_TIME_TIMESHEET_DAILY D
       INNER JOIN AT_CHOSEN_ORG_TMP CHOSEN
          ON CHOSEN.ORG_ID = D.ORG_ID
         AND CHosEN.Request_Id = PV_REQUEST_ID
       WHERE D.WORKINGDAY BETWEEN PV_STARTDATE AND PV_ENDDATE;
    COMMIT;
    PV_TOKEN := 'TBL' || PV_TOKEN;
    SELECT COUNT(*)
      INTO PV_CHECK
      FROM user_tables UT
     WHERE UPPER(UT.TABLE_NAME) = UPPER(PV_TOKEN);
  
    IF PV_CHECK = 1 THEN
      PV_SQL_CRE_TB := '
            DROP TABLE ' || PV_TOKEN || '
    ';
      EXECUTE IMMEDIATE PV_SQL_CRE_TB;
      PV_CHECK := 0;
    END IF;
    IF PV_CHECK = 0 THEN
      IF P_PERIOD_ID IS NULL OR P_PERIOD_ID = 0 THEN
        PV_STARTDATE := TO_DATE('01/01/2016', 'dd/MM/yyyy');
        PV_ENDDATE1  := TO_DATE('31/01/2016', 'dd/MM/yyyy');
      ELSE
        SELECT P.START_DATE,
               P.END_DATE,
               CASE
                 WHEN TO_CHAR(P.START_DATE, 'MM') =
                      TO_CHAR(P.END_DATE, 'MM') THEN
                  EXTRACT(DAY FROM P.END_DATE)
                 ELSE
                  EXTRACT(DAY FROM
                          TO_DATE('01/' || TO_CHAR(P.END_DATE, 'MM/yyyy'),
                                  'dd/MM/yyyy') - 1)
               END
          INTO PV_STARTDATE, PV_ENDDATE1, PV_DAYEND
          FROM AT_PERIOD P
         WHERE P.ID = P_PERIOD_ID;
      END IF;
    
      SELECT CASE
               WHEN PV_DAYEND = 31 THEN
                ''
               WHEN PV_DAYEND = 30 THEN
                ', '' '' AS D31'
               WHEN PV_DAYEND = 29 THEN
                ', '' '' AS D31, '' '' AS D30'
               WHEN PV_DAYEND = 28 THEN
                ', '' '' AS D31, '' '' AS D30, '' '' AS D29'
               ELSE
                ''
             END
        INTO V_COL1
        FROM DUAL;
    
      INSERT INTO AT_TEMP_DATE
        SELECT A.*, PV_REQUEST_ID, rownum
          FROM table(PKG_FUNCTION.table_listdate(PV_STARTDATE, PV_ENDDATE1)) A;
      COMMIT;
      -- LAY DANH SACH COT DONG THEO THANG
      SELECT LISTAGG('A.D' || TO_CHAR(EXTRACT(DAY FROM A.CDATE)), ',') WITHIN GROUP(ORDER BY A.STT)
        INTO V_COL
        FROM AT_TEMP_DATE A
       WHERE A.REQUEST_ID = PV_REQUEST_ID;
    
      -- LAY DU LIEU PIVOT
      SELECT LISTAGG('''' || TO_CHAR(EXTRACT(DAY FROM A.CDATE)) || '''' ||
                     ' AS "D' || A.STT || '"',
                     ',') WITHIN GROUP(ORDER BY A.STT)
        INTO V_COL_V
        FROM AT_TEMP_DATE A
       WHERE A.REQUEST_ID = PV_REQUEST_ID;
      \* PKG_COMMON_LIST.INSERT_CHOSEN_ORG(P_USERNAME, P_ORG_ID, P_ISDISSOLVE);
      COMMIT;*\
    
      \*  INSERT INTO AT_CHOSEN_ORG_TMP E --temp
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
                                    COMMIT;*\
      ---================================================================
      --TAO DU LIEU CAN TIN TOAN
      INSERT INTO AT_TIME_TIMESHEET_DAILY_TMP
        SELECT D.*, PV_REQUEST_ID
          FROM AT_TIME_TIMESHEET_DAILY D
         INNER JOIN AT_CHOSEN_ORG_TMP CHOSEN
            ON CHOSEN.ORG_ID = D.ORG_ID
           AND CHosEN.Request_Id = PV_REQUEST_ID
         WHERE D.WORKINGDAY BETWEEN PV_STARTDATE AND PV_ENDDATE1;
      COMMIT;
      --INSERT INTO TEMP(TEXT)VALUES('NHAY VO DAY LAM GI');
      --==================================================================
      PV_SQL_CRE_TB := '
        CREATE TABLE ' || PV_TOKEN || '
        AS
    ';
      --======INSERT THONG TIN VAO BANG QUAN LY CACHE
      INSERT INTO SE_MNG_TABLE_CACHE
        (ID,
         TBL_NAME,
         CREATED_DATE,
         CREATED_BY,
         CREATED_LOG,
         USE_COUNT,
         PERIOD_ID,
         HIERARCHICAL_PATH)
      VALUES
        (SEQ_SE_MNG_TABLE_CACHE.NEXTVAL,
         PV_TOKEN,
         SYSDATE,
         P_USERNAME,
         P_USERNAME,
         1,
         P_PERIOD_ID,
         PV_HIERARCHICAL_PATH);
      COMMIT;
      --=================================================
      PV_SQL := '
        SELECT *
        FROM (
             SELECT ROWNUM TT,
             EE.ID EMPLOYEE_ID,
             EE.EMPLOYEE_CODE,
             EE.FULLNAME_VN VN_FULLNAME,
             O.NAME_VN ORG_NAME,
             O.ORG_PATH,
              O.ORG_NAME2,
             O.DESCRIPTION_PATH ORG_DESC,
             TI.NAME_VN TITLE_NAME,
             S.NAME STAFF_RANK_NAME,
             A.OBJECT_ATTENDANCE,
             OBJECT_ATT.NAME_VN OBJECT_ATTENDANCE_NAME,
             B.TOTAL_DAY_SAL,
             B.TOTAL_NON_SAL,
             ' || V_COL || '  ' || V_COL1 || '
             FROM (
                  SELECT
                     T.EMPLOYEE_ID,
                     T.ORG_ID,
                     T.TITLE_ID,
                     T.OBJECT_ATTENDANCE,
                     TO_NUMBER(TO_CHAR(T.WORKINGDAY, ''dd'')) AS DAY,
                     NVL(L.CODE, '' '') CODE
                    FROM AT_TIME_TIMESHEET_DAILY_TMP T
                    LEFT JOIN AT_TIME_MANUAL L
                      ON T.MANUAL_ID = L.ID
                   WHERE T.REQUEST_ID=' || PV_REQUEST_ID ||
                ' AND  T.WORKINGDAY BETWEEN ''' || PV_STARTDATE ||
                ''' AND ''' || PV_ENDDATE1 || '''
                  )
                 PIVOT(MAX(CODE)
                 FOR DAY IN(
                   ' || V_COL_V || '
                 )
               ) A
                LEFT JOIN(
                    SELECT T.EMPLOYEE_ID,
                    SUM(CASE WHEN NVL(M.CODE,'''') IN(''VR'', ''DH'', ''CT'', ''B'', ''P'',''LT'',''X'') THEN 1 ELSE 0 END) TOTAL_DAY_SAL,
                    SUM(CASE WHEN NVL(M.CODE ,'''') IN(''NKL'',''NKL/X'') THEN 1 ELSE 0 END ) TOTAL_NON_SAL
                    FROM AT_TIME_TIMESHEET_DAILY_TMP T
                    LEFT JOIN AT_TIME_MANUAL M ON M.ID=T.MANUAL_ID
                    WHERE T.REQUEST_ID =' || PV_REQUEST_ID ||
                ' AND  T.WORKINGDAY BETWEEN ''' || PV_STARTDATE ||
                ''' AND ''' || PV_ENDDATE1 || '''
                    GROUP BY T.EMPLOYEE_ID
               ) B ON A.EMPLOYEE_ID=B.EMPLOYEE_ID

               INNER JOIN HU_EMPLOYEE EE
                  ON EE.ID = A.EMPLOYEE_ID
                LEFT JOIN HUV_ORGANIZATION O
                  ON O.ID = A.ORG_ID
                LEFT JOIN HU_TITLE TI
                  ON TI.ID = A.TITLE_ID
                  INNER JOIN OT_OTHER_LIST OT
                  ON OT.ID=EE.OBJECTTIMEKEEPING
                LEFT JOIN HU_STAFF_RANK S
                  ON S.ID = EE.STAFF_RANK_ID
                LEFT JOIN (SELECT *
                            FROM AT_TIME_TIMESHEET_OT OT
                           WHERE OT.FROM_DATE >= ''' ||
                PV_STARTDATE || '''
                             AND OT.END_DATE <= ''' ||
                PV_ENDDATE1 || ''') OT
                  ON OT.EMPLOYEE_ID = EE.ID
                LEFT JOIN OT_OTHER_LIST OBJECT_ATT
                  ON OBJECT_ATT.ID = (CASE
                                        WHEN A.OBJECT_ATTENDANCE IS NULL OR A.OBJECT_ATTENDANCE = 0 THEN
                                          NULL
                                        WHEN A.OBJECT_ATTENDANCE > 0 THEN
                                          A.OBJECT_ATTENDANCE
                                        END)
             WHERE
              (NVL(EE.WORK_STATUS, 0) <> 257 OR
                     (NVL(EE.WORK_STATUS, 0) = 257 AND
                     EE.TER_LAST_DATE >= ''' || PV_STARTDATE ||
                ''')))
             ';
      INSERT INTO AT_STRSQL
      VALUES
        (SEQ_AT_STRSQL.NEXTVAL, PV_SQL_CRE_TB || PV_SQL);
      COMMIT;
    
      EXECUTE IMMEDIATE PV_SQL_CRE_TB || PV_SQL;
    END IF;
    --=======================================================================>
    DELETE FROM AT_CHOSEN_ORG_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_CHOSEN_EMP_CLEAR_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_CHOSEN_EMP_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_TIME_TIMESHEET_MACHINE_TMP
     WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_TIME_TIMESHEET_DAILY_TMP T
     WHERE T.REQUEST_ID = PV_REQUEST_ID;
    COMMIT;
    P_OUT := 3;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 4;
      DELETE FROM AT_CHOSEN_ORG_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_CHOSEN_EMP_CLEAR_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_CHOSEN_EMP_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_TIME_TIMESHEET_MACHINE_TMP
       WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_TIME_TIMESHEET_DAILY_TMP T
       WHERE T.REQUEST_ID = PV_REQUEST_ID;
      COMMIT;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CAL_TIMESHEET_DAILY',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              P_ORG_ID,
                              P_PERIOD_ID,
                              P_USERNAME,
                              PV_REQUEST_ID);
  END;
*/
  /*PROCEDURE CAL_TIME_TIMESHEET_HOSE(P_USERNAME   IN NVARCHAR2,
                                    P_ORG_ID     IN NUMBER,
                                    P_PERIOD_ID  IN NUMBER,
                                    P_ISDISSOLVE IN NUMBER,
                                    P_DELETE_ALL IN NUMBER := 1) IS
    PV_FROMDATE    DATE;
    PV_ENDDATE     DATE;
    PV_SQL         CLOB;
    PV_REQUEST_ID  NUMBER;
    PV_MINUS_ALLOW NUMBER := 50;
    PV_SUNDAY      DATE; --Lay ngay chu nhat trong thang
    PV_TEST1       NUMBER;
    PV_TEST2       NUMBER;
    PV_CHECK       NUMBER;
    PV_DEL_ALL     NUMBER;
    PV_CHECKNV     NUMBER;
    PV_TG_BD_CA    DATE;
    PV_TG_KT_CA    DATE;
    PV_TG_BD_NGHI  DATE;
    PV_TG_KT_NGHI  DATE;
    PV_IN_MIN      DATE;
    PV_OUT_MAX     DATE;
  BEGIN
    return;
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    PV_DEL_ALL    := P_DELETE_ALL;
  
    SELECT P.START_DATE, P.END_DATE
      INTO PV_FROMDATE, PV_ENDDATE
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
    INSERT INTO AT_CHOSEN_EMP_CLEAR --==NHUNG NV DO HE THONG TU DONG TINH
      (EMPLOYEE_ID)
      SELECT DISTINCT S.EMPLOYEE_ID
        FROM AT_TIME_TIMESHEET_DAILY S
       INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
          ON O.ORG_ID = S.ORG_ID
       WHERE (S.CREATED_BY = 'AUTO' OR P_DELETE_ALL <> 0)
         AND S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
  
    SELECT COUNT(1)
      INTO PV_CHECKNV
      FROM AT_TIME_TIMESHEET_DAILY S
     INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
        ON O.ORG_ID = S.ORG_ID
     WHERE S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
  
    SELECT COUNT(*) INTO PV_CHECK FROM AT_CHOSEN_EMP_CLEAR;
    IF PV_CHECK = 0 AND PV_CHECKNV = 0 THEN
      --==Neu tinh lan dau se k co CREATED_BY = 'AUTO' se tinh all
      PV_DEL_ALL := 1;
    END IF;
  
    IF PV_DEL_ALL = 1 THEN
      --==TINH LAI TAT CA CA NV
      -- insert emp can tinh toan
      INSERT INTO AT_CHOSEN_EMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE,
         DECISION_ID,
         OBJECT_ATTENDANCE)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE,
                W.ID DECISION_ID,
                W.OBJECT_ATTENDANCE
           FROM HU_EMPLOYEE T
          INNER JOIN (SELECT E.ID,
                            E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            E.OBJECT_ATTENDANCE,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_WAGE = 0
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
             ON O.ORG_ID = W.ORG_ID
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
    ELSE
      --==CHI TINH NV AUTO
      -- insert emp can tinh toan
      INSERT INTO AT_CHOSEN_EMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE,
         DECISION_ID,
         OBJECT_ATTENDANCE)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE,
                W.ID DECISION_ID,
                W.OBJECT_ATTENDANCE
           FROM HU_EMPLOYEE T
          INNER JOIN AT_CHOSEN_EMP_CLEAR C --==CHI TINH NV AUTO
             ON T.ID = C.EMPLOYEE_ID
          INNER JOIN (SELECT E.ID,
                            E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            E.OBJECT_ATTENDANCE,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_WAGE = 0
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
             ON O.ORG_ID = W.ORG_ID
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
    END IF;
  
    SELECT COUNT(1) INTO PV_TEST1 FROM AT_CHOSEN_ORG T;
    SELECT COUNT(1)
      INTO PV_TEST2
      FROM AT_CHOSEN_EMP T
     WHERE T.EMPLOYEE_ID = 1;
    INSERT INTO TEMP1
      (TMP, WCODE, EXEDATE, TYPE)
    VALUES
      ('INSERT INTO AT_CHOSEN_EMP' || PV_TEST1 || '-' || PV_TEST2 || '-' ||
       PV_DEL_ALL,
       '1',
       SYSDATE,
       400);
    --RETURN;
    --==Start Chuc nang bang cong goc
    --==insert data v?o bang tam de tinh toan
    INSERT INTO AT_TIME_TIMESHEET_MACHINE_TEMP M
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       WORKINGDAY,
       SHIFT_ID,
       SHIFT_CODE,
       CREATED_DATE,
       CREATED_BY,
       REQUEST_ID,
       VALIN1,
       VALOUT1,
       TIMEVALIN,
       TIMEVALOUT,
       TIMEIN_REALITY, --GIO VAO TT
       TIMEOUT_REALITY, -- GIO RA THUC TE
       OBJECT_ATTENDANCE,
       OBJECT_ATTENDANCE_CODE,
       WORK_HOUR,
       HOURS_STOP,
       HOURS_START,
       START_MID_HOURS,
       END_MID_HOURS,
       SHIFT_DAY,
       HOURS_STAR_CHECKIN,
       HOURS_STAR_CHECKOUT,
       NOTE,
       MIN_ON_LEAVE,
       MANUAL_ID,
       STATUS_SHIFT)
      SELECT SEQ_AT_TIME_TIMESHEET_MACHINET.NEXTVAL,
             T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             T.STAFF_RANK_ID,
             WS.WORKINGDAY,
             WS.SHIFT_ID,
             SH.CODE SHIFT_CODE,
             SYSDATE,
             P_USERNAME,
             PV_REQUEST_ID,
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_IN.VALTIME
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       3,
                                       3,
                                       0,
                                       PV_REQUEST_ID)
             END,
  
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_OUT.VALTIME
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       3,
                                       3,
                                       1,
                                       PV_REQUEST_ID)
             END,
  
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_IN.VALTIME
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       3,
                                       3,
                                       0,
                                       PV_REQUEST_ID)
             END,
  
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_OUT.VALTIME
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       3,
                                       3,
                                       1,
                                       PV_REQUEST_ID)
             END,
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_IN.VALTIME
               ELSE
                I.TIMEIN_REALITY
             END,
  
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_OUT.VALTIME
               ELSE
                I.TIMEOUT_REALITY
             END,
             T.OBJECT_ATTENDANCE,
             OL.CODE OBJECT_ATTENDANCE_CODE,
             SH.WORK_HOUR,
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STOP, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_START, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.START_MID_HOURS, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.END_MID_HOURS, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             SH.SHIFT_DAY,
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STAR_CHECKIN, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STAR_CHECKOUT, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             I.NOTE,
             LEAVE.DAY_NUM,
             LEAVE.MANUAL_ID,
             LEAVE.STATUS_SHIFT
        FROM AT_WORKSIGN WS
       INNER JOIN AT_CHOSEN_EMP T
          ON T.EMPLOYEE_ID = WS.EMPLOYEE_ID
        LEFT JOIN AT_TIMESHEET_MACHINET_IMPORT I
          ON I.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND I.WORKING_DAY = WS.WORKINGDAY
        LEFT JOIN AT_SHIFT SH
          ON WS.SHIFT_ID = SH.ID
        LEFT JOIN AT_LEAVESHEET_DETAIL LEAVE
          ON T.EMPLOYEE_ID = LEAVE.EMPLOYEE_ID
         AND WS.WORKINGDAY = LEAVE.LEAVE_DAY
        LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                          SW.WORKINGDAY,
                          MIN(SW.VALTIME) AS VALTIME
                     FROM AT_SWIPE_DATA SW
                    WHERE SW.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
                   --AND SW.VALTIME >= SH.HOURS_STAR_CHECKIN
                    GROUP BY SW.EMPLOYEE_ID, SW.WORKINGDAY) DATA_IN
          ON DATA_IN.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND DATA_IN.WORKINGDAY = WS.WORKINGDAY
        LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                          SW.WORKINGDAY,
                          MAX(SW.VALTIME) AS VALTIME
                     FROM AT_SWIPE_DATA SW
                    WHERE SW.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
                   --AND SW.VALTIME <= SH.HOURS_STAR_CHECKOUT
                    GROUP BY SW.EMPLOYEE_ID, SW.WORKINGDAY) DATA_OUT
          ON DATA_OUT.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND DATA_OUT.WORKINGDAY = WS.WORKINGDAY
        LEFT JOIN OT_OTHER_LIST OL
          ON T.OBJECT_ATTENDANCE = OL.ID;
  
    SELECT COUNT(1) INTO PV_TEST1 FROM AT_CHOSEN_ORG T;
    SELECT COUNT(1)
      INTO PV_TEST2
      FROM AT_TIME_TIMESHEET_MACHINE_TEMP T
     WHERE T.EMPLOYEE_ID = 1;
    INSERT INTO TEMP1
      (TMP, WCODE, EXEDATE, TYPE)
    VALUES
      ('INSERT INTO AT_CHOSEN_EMP' || PV_TEST1 || '-' || PV_TEST2 || '-' ||
       PV_DEL_ALL,
       '2',
       SYSDATE,
       400);
  
    --== ap dung cong thuc
    FOR CUR_ITEM IN (SELECT *
                       FROM AT_TIME_FORMULAR T
                      WHERE T.TYPE IN (5)
                        AND T.STATUS = 1
                      ORDER BY T.ORDERBY) LOOP
      PV_SQL := 'UPDATE AT_TIME_TIMESHEET_MACHINE_TEMP T SET ' ||
                CUR_ITEM.FORMULAR_CODE || '= NVL((' ||
                CUR_ITEM.FORMULAR_VALUE || '),NULL)';
      INSERT INTO TEMP1
        (TMP, WCODE)
      VALUES
        (PV_SQL, 'CUR_ITEM.FORMULAR_CODE');
      BEGIN
        EXECUTE IMMEDIATE PV_SQL;
      EXCEPTION
        WHEN OTHERS THEN
          CONTINUE;
      END;
    END LOOP;
    -- XOA DU LIEU CU TRUOC KHI TINH
    DELETE FROM AT_TIME_TIMESHEET_MACHINET D
     WHERE D.WORKINGDAY >= PV_FROMDATE
       AND D.WORKINGDAY <= PV_ENDDATE
       AND D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O);
    --==Insert thong tin da tin vao table that
    INSERT INTO AT_TIME_TIMESHEET_MACHINET T
      SELECT * FROM AT_TIME_TIMESHEET_MACHINE_TEMP;
    SELECT COUNT(1) INTO PV_TEST1 FROM AT_CHOSEN_ORG T;
    SELECT COUNT(1)
      INTO PV_TEST2
      FROM AT_TIME_TIMESHEET_MACHINET T
     WHERE T.EMPLOYEE_ID = 1;
    INSERT INTO TEMP1
      (TMP, WCODE, EXEDATE, TYPE)
    VALUES
      ('INSERT INTO AT_CHOSEN_EMP' || PV_TEST1 || '-' || PV_TEST2 || '-' ||
       PV_DEL_ALL,
       '3',
       SYSDATE,
       400);
  
    --==End Chuc nang bang cong goc
    --==Start Chuc nang tong hop cong
    DELETE FROM AT_TIME_TIMESHEET_DAILY D
     WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
       AND EXISTS
     (SELECT D.ID
              FROM AT_TIME_TIMESHEET_DAILY D_CUR
             WHERE D_CUR.EMPLOYEE_ID = D.EMPLOYEE_ID
               AND D_CUR.WORKINGDAY = D.WORKINGDAY
               AND (D_CUR.CREATED_BY = 'AUTO' OR P_DELETE_ALL <> 0))
       AND D.WORKINGDAY >= PV_FROMDATE
       AND D.WORKINGDAY <= PV_ENDDATE;
  
    INSERT INTO AT_TIME_TIMESHEET_DAILY T
      (ID,
       T.EMPLOYEE_ID,
       T.ORG_ID,
       T.TITLE_ID,
       T.WORKINGDAY,
       T.SHIFT_CODE,
       T.SHIFT_ID,
       T.WORKINGHOUR,
       T.MANUAL_ID,
       T.CREATED_DATE,
       T.CREATED_BY,
       T.CREATED_LOG,
       T.MODIFIED_DATE,
       T.MODIFIED_BY,
       T.MODIFIED_LOG,
       VALIN1,
       VALIN2,
       VALIN3,
       VALIN4,
       VALIN5,
       VALIN6,
       VALIN7,
       VALIN8,
       VALIN9,
       VALIN10,
       VALIN11,
       VALIN12,
       VALIN13,
       VALIN14,
       VALIN15,
       VALIN16,
       VALOUT1,
       VALOUT2,
       VALOUT3,
       VALOUT4,
       VALOUT5,
       VALOUT6,
       VALOUT7,
       VALOUT8,
       VALOUT9,
       VALOUT10,
       VALOUT11,
       VALOUT12,
       VALOUT13,
       VALOUT14,
       VALOUT15,
       VALOUT16,
       T.TIMEVALIN,
       TIMEVALOUT,
       OBJECT_ATTENDANCE,
       MIN_IN_WORK,
       MIN_DEDUCT_WORK,
       MIN_ON_LEAVE,
       MIN_LATE,
       MIN_EARLY,
       MIN_OUT_WORK_DEDUCT,
       MIN_LATE_EARLY,
       MIN_OUT_WORK,
       MIN_DEDUCT)
      SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
             M.EMPLOYEE_ID,
             M.ORG_ID,
             M.TITLE_ID,
             M.WORKINGDAY,
             M.SHIFT_CODE,
             M.SHIFT_ID,
             CASE
               WHEN NVL(L.EMPLOYEE_ID, 0) > 0 AND L.Leave_Day IS NOT NULL THEN
                0 --==UU TIEN XET DANG KY NGHI
               ELSE
                CASE
                  WHEN O.CODE = 'KCC' THEN
                   (SELECT WORK_HOUR FROM AT_SHIFT WHERE CODE = 'HC')
                  WHEN M.TIMEVALIN IS NOT NULL THEN --==CO GIO VAO-RA SE LAY CA DI LAM [X]
                   (SELECT WORK_HOUR FROM AT_SHIFT WHERE CODE = 'HC')
                  ELSE
                   0 --==K CO GIO VAO-RA LAY NGHI K LY DO
                END
             END WORKINGHOUR,
             CASE
               WHEN NVL(L.EMPLOYEE_ID, 0) > 0 AND L.Leave_Day IS NOT NULL THEN
                L.MANUAL_ID
               ELSE
                CASE
                  WHEN O.CODE = 'KCC' THEN
                   CASE
                     WHEN NVL(M.SHIFT_ID, 0) > 0 THEN --CO CA
                      CASE
                        WHEN NVL(SH.CODE, '') = 'OFF' THEN
                         (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'OFF')
                        ELSE
                         (SELECT ID
                            FROM AT_TIME_MANUAL T
                           WHERE T.CODE = M.SHIFT_TYPE_CODE)
                      END
                     ELSE -- KHONG CO CA
                      CASE
                        WHEN M.WORKINGDAY IN (SELECT H.WORKINGDAY FROM AT_HOLIDAY H) THEN
                         (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'L')
                      END
                   END
                  ELSE --DOI TUONG KHAC
                   CASE
                     WHEN NVL(M.SHIFT_ID, 0) > 0 THEN --CO CA
                     \* CASE WHEN M.TIMEVALIN IS NOT NULL THEN
                       (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'X')
                     ELSE
                       (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'KLD')
                     END*\
                      CASE
                        WHEN SH.CODE = 'OFF' THEN --KY HIEU OFF
                         CASE
                           WHEN M.TIMEVALIN IS NOT NULL THEN
                            (SELECT ID
                               FROM AT_TIME_MANUAL T
                              WHERE T.CODE = M.SHIFT_TYPE_CODE)
                           ELSE
                            (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'OFF')
                         END
                        ELSE
                         CASE
                           WHEN M.TIMEVALIN IS NOT NULL THEN
                            (SELECT ID
                               FROM AT_TIME_MANUAL T
                              WHERE T.CODE = M.SHIFT_TYPE_CODE)
                           ELSE
                            (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'KLD')
                         END
                      END
                     ELSE --KO CO CA
                      CASE
                        WHEN M.WORKINGDAY IN (SELECT H.WORKINGDAY FROM AT_HOLIDAY H) THEN
                         CASE
                           WHEN M.TIMEVALIN IS NOT NULL THEN
                            (SELECT ID
                               FROM AT_TIME_MANUAL T
                              WHERE T.CODE = M.SHIFT_TYPE_CODE)
                           ELSE
                            (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'L')
                         END
                      END
                   END
                END
             END MANUAL_ID,
             SYSDATE,
             'AUTO',
             UPPER(P_USERNAME),
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME),
             VALIN1,
             VALIN2,
             VALIN3,
             VALIN4,
             VALIN5,
             VALIN6,
             VALIN7,
             VALIN8,
             VALIN9,
             VALIN10,
             VALIN11,
             VALIN12,
             VALIN13,
             VALIN14,
             VALIN15,
             VALIN16,
             VALOUT1,
             VALOUT2,
             VALOUT3,
             VALOUT4,
             VALOUT5,
             VALOUT6,
             VALOUT7,
             VALOUT8,
             VALOUT9,
             VALOUT10,
             VALOUT11,
             VALOUT12,
             VALOUT13,
             VALOUT14,
             VALOUT15,
             VALOUT16,
             M.TIMEVALIN,
             M.TIMEVALOUT,
             M.OBJECT_ATTENDANCE,
             M.MIN_IN_WORK,
             M.MIN_DEDUCT_WORK,
             M.MIN_ON_LEAVE,
             M.MIN_LATE,
             M.MIN_EARLY,
             M.MIN_OUT_WORK_DEDUCT,
             M.MIN_LATE_EARLY,
             M.MIN_OUT_WORK,
             M.MIN_DEDUCT
        FROM AT_TIME_TIMESHEET_MACHINE_TEMP M
        LEFT JOIN At_Leavesheet_Detail L
          ON M.EMPLOYEE_ID = L.EMPLOYEE_ID
         AND M.WORKINGDAY = L.Leave_Day
        LEFT JOIN AT_SHIFT SH
          ON SH.ID = M.SHIFT_ID
        LEFT JOIN OT_OTHER_LIST O
          ON M.OBJECT_ATTENDANCE = O.ID
       WHERE NOT EXISTS (SELECT D.ID
                FROM AT_TIME_TIMESHEET_DAILY D
               WHERE D.EMPLOYEE_ID = M.EMPLOYEE_ID
                 AND D.WORKINGDAY = M.WORKINGDAY);
  
    --==Cac ngay k co du lieu ca, in/out se quy ve nghi k ly do
    INSERT INTO AT_TIME_TIMESHEET_DAILY T
      (T.ID,
       T.EMPLOYEE_ID,
       T.ORG_ID,
       T.TITLE_ID,
       T.WORKINGDAY,
       T.MANUAL_ID,
       T.OBJECT_ATTENDANCE,
       T.CREATED_DATE,
       T.CREATED_BY,
       T.CREATED_LOG,
       T.MODIFIED_DATE,
       T.MODIFIED_BY,
       T.MODIFIED_LOG)
      SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
             EMP.EMPLOYEE_ID,
             EMP.ORG_ID,
             EMP.TITLE_ID,
             D.CDATE,
             CASE
               WHEN NVL(L.EMPLOYEE_ID, 0) > 0 AND L.Leave_Day IS NOT NULL THEN
                L.MANUAL_ID
               ELSE
                CASE
                  WHEN O.CODE = 'KCC' THEN
                   CASE
                     WHEN NVL(AW.SHIFT_ID, 0) > 0 THEN --CO CA
                      CASE
                        WHEN NVL(SH.CODE, '') = 'OFF' THEN
                         (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'OFF')
                        ELSE
                         (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'X')
                      END
                     ELSE -- KHONG CO CA
                      CASE
                        WHEN AW.WORKINGDAY IN (SELECT H.WORKINGDAY FROM AT_HOLIDAY H) THEN
                         (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'L')
                      END
                   END
                  ELSE --DOI TUONG KHAC
                   CASE
                     WHEN NVL(AW.SHIFT_ID, 0) > 0 THEN --CO CA
                     --(SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'KLD')
                      CASE
                        WHEN SH.CODE = 'OFF' THEN --KY HIEU OFF
                         (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'OFF')
                        ELSE
                         (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'KLD')
                      END
                     ELSE --KO CO CA
                      CASE
                        WHEN AW.WORKINGDAY IN (SELECT H.WORKINGDAY FROM AT_HOLIDAY H) THEN
                         (SELECT ID FROM AT_TIME_MANUAL T WHERE T.CODE = 'L')
                      END
                   END
                END
             END MANUAL_ID,
             EMP.OBJECT_ATTENDANCE,
             SYSDATE,
             'AUTO',
             UPPER(P_USERNAME),
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME)
        FROM AT_CHOSEN_EMP EMP
       CROSS JOIN (SELECT CDATE
                     FROM TABLE(TABLE_LISTDATE(PV_FROMDATE, PV_ENDDATE))) D
        LEFT JOIN AT_WORKSIGN AW
          ON EMP.EMPLOYEE_ID = AW.EMPLOYEE_ID
         AND AW.WORKINGDAY = D.CDATE
        LEFT JOIN AT_SHIFT SH
          ON AW.SHIFT_ID = SH.ID
        LEFT JOIN OT_OTHER_LIST O
          ON O.ID = EMP.OBJECT_ATTENDANCE
        LEFT JOIN At_Leavesheet_Detail L
          ON AW.EMPLOYEE_ID = L.EMPLOYEE_ID
         AND AW.WORKINGDAY = L.Leave_Day
       WHERE --EMP.EMPLOYEE_ID NOT IN (SELECT T.EMPLOYEE_ID FROM AT_TIME_TIMESHEET_DAILY T WHERE T.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE );
       NOT EXISTS (SELECT D.ID
          FROM AT_TIME_TIMESHEET_DAILY D
         WHERE D.EMPLOYEE_ID = EMP.EMPLOYEE_ID
           AND D.WORKINGDAY = D.CDATE);
  
    INSERT INTO TEMP1
      (TMP, WCODE, EXEDATE, TYPE)
    VALUES
      ('INSERT INTO AT_CHOSEN_EMP', '12', SYSDATE, 400);
    --Insert ky hieu cho ngay nghi Le
    --Uu tien lay ky hieu L
    UPDATE AT_TIME_TIMESHEET_DAILY T
       SET T.MANUAL_ID = CASE
                           WHEN T.SHIFT_CODE = 'OFF' THEN
                            (SELECT ID
                               FROM AT_TIME_MANUAL T
                              WHERE T.CODE = 'L'
                                AND ROWNUM = 1)
                           ELSE
                            CASE
                              WHEN T.TIMEVALIN IS NOT NULL THEN
                               (SELECT ID
                                  FROM AT_TIME_MANUAL T
                                 WHERE T.CODE = 'X'
                                   AND ROWNUM = 1)
                              ELSE
                               (SELECT ID
                                  FROM AT_TIME_MANUAL T
                                 WHERE T.CODE = 'L'
                                   AND ROWNUM = 1)
                            END
                         END
     WHERE T.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
       AND EXISTS (SELECT H.ID
              FROM AT_HOLIDAY H
             WHERE H.ACTFLG = 'A'
               AND H.WORKINGDAY = T.WORKINGDAY);
  
    --Cham cong chi tiet hang ngay
    INSERT INTO AT_TIME_TIMESHEET_DAILY T
      (T.ID,
       T.EMPLOYEE_ID,
       T.ORG_ID,
       T.TITLE_ID,
       T.WORKINGDAY,
       T.MANUAL_ID,
       T.CREATED_DATE,
       T.CREATED_BY,
       T.CREATED_LOG,
       T.MODIFIED_DATE,
       T.MODIFIED_BY,
       T.MODIFIED_LOG)
      SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
             EMP.EMPLOYEE_ID,
             EMP.ORG_ID,
             EMP.TITLE_ID,
             H.WORKINGDAY,
             23, --==L
             SYSDATE,
             'AUTO',
             UPPER(P_USERNAME),
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME)
        FROM AT_CHOSEN_EMP EMP,
             (SELECT DISTINCT H.WORKINGDAY
                FROM AT_HOLIDAY H
               WHERE H.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE) H
       WHERE
      -- EMP.EMPLOYEE_ID NOT IN (SELECT T.EMPLOYEE_ID FROM AT_TIME_TIMESHEET_DAILY T WHERE T.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE );
       NOT EXISTS (SELECT D.ID
          FROM AT_TIME_TIMESHEET_DAILY D
         WHERE D.EMPLOYEE_ID = EMP.EMPLOYEE_ID
           AND D.WORKINGDAY = H.WORKINGDAY);
    --==End Chuc nang tong hop cong
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_ALL_HOSE',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              P_ORG_ID,
                              P_PERIOD_ID,
                              P_USERNAME,
                              PV_REQUEST_ID);
  END;
   */
  PROCEDURE GET_CCT(P_USERNAME               IN NVARCHAR2,
                    P_ORG_ID                 IN NUMBER,
                    P_ISDISSOLVE             IN NUMBER,
                    P_PAGE_INDEX             IN NUMBER,
                    P_EMPLOYEE_CODE          IN NVARCHAR2,
                    P_EMPLOYEE_NAME          IN NVARCHAR2,
                    P_ORG_NAME               IN NVARCHAR2,
                    P_TITLE_NAME             IN NVARCHAR2,
                    P_OBJECT_ATTENDANCE_NAME IN NVARCHAR2,
                    P_PAGE_SIZE              IN NUMBER,
                    P_PERIOD_ID              IN NUMBER,
                    P_TERMINATE              IN NUMBER, --muon pram de gan check clear cache
                    P_EMP_OBJ                in number,
                    P_START_DATE             in date,
                    P_END_DATE               in date,
                    P_CUR                    OUT CURSOR_TYPE,
                    P_CURCOUNR               OUT CURSOR_TYPE) IS
    PV_STARTDATE         DATE;
    PV_ENDDATE           DATE;
    PV_DAYEND            NUMBER;
    V_COL1               CLOB;
    V_COL                CLOB;
    V_COL_V              CLOB;
    PV_SQL               CLOB;
    PV_SEQ               NUMBER;
    PV_CHECK             NUMBER;
    PV_TOKEN             NVARCHAR2(40);
    PV_SQL_CRE_TB        NVARCHAR2(1000);
    PV_WHERE             NVARCHAR2(1000);
    PV_TABLE_NAME        NVARCHAR2(40) := '';
    PV_REQUEST_ID        number;
    PV_HIERARCHICAL_PATH NVARCHAR2(20000);
  BEGIN
  
    SELECT O.HIERARCHICAL_PATH
      INTO PV_HIERARCHICAL_PATH
      FROM HU_ORGANIZATION O
     WHERE O.ID = P_ORG_ID;
    SELECT standard_hash(TO_CHAR(P_ORG_ID) || P_USERNAME ||
                         TO_CHAR(P_PERIOD_ID),
                         'MD5')
      INTO PV_TOKEN
      FROM DUAL;
    --========================================================>
    SELECT SEQ_ORG_TEMP_TABLE.NEXTVAL INTO PV_SEQ FROM DUAL;
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    INSERT INTO AT_CHOSEN_ORG_TMP E --temp
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
    COMMIT;
    --==============================================================
  
    /*SELECT COUNT(*)
    INTO PV_CHECK
    FROM SE_MNG_TABLE_CACHE CACHE
    WHERE CACHE.PERIOD_ID=P_PERIOD_ID
    AND CACHE.CREATED_BY =P_USERNAME
    AND INSTR(';'||PV_HIERARCHICAL_PATH||';',';'||CACHE.HIERARCHICAL_PATH||';')>0;
    
    IF PV_CHECK >0 THEN
      SELECT TBL_NAME
      INTO PV_TOKEN
      FROM(
      SELECT CACHE.TBL_NAME TBL_NAME,
             ROW_NUMBER() OVER(PARTITION BY CACHE.PERIOD_ID ORDER BY cache.created_date asc,CACHE.HIERARCHICAL_PATH desc) AS ROW_NUMBER
             --ROW_NUMBER() OVER(PARTITION BY CACHE.PERIOD_ID ORDER BY CACHE.HIERARCHICAL_PATH ASC) AS ROW_NUMBER
      FROM SE_MNG_TABLE_CACHE CACHE
      WHERE CACHE.PERIOD_ID=P_PERIOD_ID
      AND CACHE.CREATED_BY =P_USERNAME
      AND INSTR(';'||PV_HIERARCHICAL_PATH||';',';'||CACHE.HIERARCHICAL_PATH||';')>0
      )WHERE ROW_NUMBER=1;
    ELSE
      PV_TOKEN:='TBL'||PV_TOKEN;
    END IF;*/
  
    PV_TOKEN := 'TBL' || PV_TOKEN;
    SELECT COUNT(*)
      INTO PV_CHECK
      FROM user_tables UT
     WHERE UPPER(UT.TABLE_NAME) = UPPER(PV_TOKEN);
  
    IF PV_CHECK > 0 THEN
      PV_SQL_CRE_TB := '
            DROP TABLE ' || PV_TOKEN || '
    ';
      EXECUTE IMMEDIATE PV_SQL_CRE_TB;
      --PV_CHECK:=0;
    END IF;
  
    -- IF PV_CHECK =0  THEN
  
    if P_START_DATE is null or P_END_DATE is null then
      PV_STARTDATE := TO_DATE('01/01/2016', 'dd/MM/yyyy');
      PV_ENDDATE   := TO_DATE('31/01/2016', 'dd/MM/yyyy');
    else
      PV_STARTDATE := TO_DATE(P_START_DATE, 'dd/MM/yyyy');
      PV_ENDDATE   := TO_DATE(P_END_DATE, 'dd/MM/yyyy');
    end if;
  
    /*IF P_PERIOD_ID IS NULL OR P_PERIOD_ID = 0 THEN
      PV_STARTDATE := TO_DATE('01/01/2016', 'dd/MM/yyyy');
      PV_ENDDATE   := TO_DATE('31/01/2016', 'dd/MM/yyyy');
    ELSE
      SELECT P.START_DATE,
             P.END_DATE,
             CASE
               WHEN TO_CHAR(P.START_DATE, 'MM') = TO_CHAR(P.END_DATE, 'MM') THEN
                EXTRACT(DAY FROM P.END_DATE)
               ELSE
                EXTRACT(DAY FROM
                        TO_DATE('01/' || TO_CHAR(P.END_DATE, 'MM/yyyy'),
                                'dd/MM/yyyy') - 1)
             END
        INTO PV_STARTDATE, PV_ENDDATE, PV_DAYEND
        FROM AT_PERIOD P
       WHERE P.ID = P_PERIOD_ID;
    END IF;*/
  
    SELECT CASE
             WHEN PV_DAYEND = 31 THEN
              ''
             WHEN PV_DAYEND = 30 THEN
              ', '' '' AS D31'
             WHEN PV_DAYEND = 29 THEN
              ', '' '' AS D31, '' '' AS D30'
             WHEN PV_DAYEND = 28 THEN
              ', '' '' AS D31, '' '' AS D30, '' '' AS D29'
             ELSE
              ''
           END
      INTO V_COL1
      FROM DUAL;
  
    INSERT INTO AT_TEMP_DATE
      SELECT A.*, PV_SEQ, rownum
        FROM table(PKG_FUNCTION.table_listdate(PV_STARTDATE, PV_ENDDATE)) A;
    COMMIT;
    -- LAY DANH SACH COT DONG THEO THANG
    SELECT LISTAGG('A.D' || TO_CHAR(EXTRACT(DAY FROM A.CDATE)), ',') WITHIN GROUP(ORDER BY A.STT)
      INTO V_COL
      FROM AT_TEMP_DATE A
     WHERE A.REQUEST_ID = PV_SEQ;
  
    -- LAY DU LIEU PIVOT
    SELECT LISTAGG('''' || TO_CHAR(EXTRACT(DAY FROM A.CDATE)) || '''' ||
                   ' AS "D' || A.STT || '"',
                   ',') WITHIN GROUP(ORDER BY A.STT)
      INTO V_COL_V
      FROM AT_TEMP_DATE A
     WHERE A.REQUEST_ID = PV_SEQ;
    /* PKG_COMMON_LIST.INSERT_CHOSEN_ORG(P_USERNAME, P_ORG_ID, P_ISDISSOLVE);
    COMMIT;*/
  
    ---================================================================
    --TAO DU LIEU CAN TIN TOAN
    INSERT INTO AT_TIME_TIMESHEET_DAILY_TMP
      SELECT D.*, PV_REQUEST_ID
        FROM AT_TIME_TIMESHEET_DAILY D
       INNER JOIN AT_CHOSEN_ORG_TMP CHOSEN
          ON CHOSEN.ORG_ID = D.ORG_ID
         AND CHosEN.Request_Id = PV_REQUEST_ID
       WHERE D.WORKINGDAY BETWEEN PV_STARTDATE AND PV_ENDDATE;
    COMMIT;
    --INSERT INTO TEMP(TEXT)VALUES('NHAY VO DAY LAM GI');
    --==================================================================
    PV_SQL_CRE_TB := '
        CREATE TABLE ' || PV_TOKEN || '
        AS
    ';
    --======INSERT THONG TIN VAO BANG QUAN LY CACHE
    INSERT INTO SE_MNG_TABLE_CACHE
      (ID,
       TBL_NAME,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       USE_COUNT,
       HIERARCHICAL_PATH,
       PERIOD_ID)
    VALUES
      (SEQ_SE_MNG_TABLE_CACHE.NEXTVAL,
       PV_TOKEN,
       SYSDATE,
       P_USERNAME,
       P_USERNAME,
       1,
       PV_HIERARCHICAL_PATH,
       P_PERIOD_ID);
    COMMIT;
    --=================================================
  
    PV_SQL := '
        SELECT *
        FROM (
             SELECT ROWNUM TT,
             A.ORG_ID,
             EE.ID EMPLOYEE_ID,
             EE.EMPLOYEE_CODE,
             EE.FULLNAME_VN VN_FULLNAME,
             O.NAME_VN ORG_NAME,
             O.ORG_PATH,
             O.ORG_NAME2,
             O.DESCRIPTION_PATH ORG_DESC,
             TI.NAME_VN TITLE_NAME,
            -- S.NAME STAFF_RANK_NAME,
             A.OBJECT_ATTENDANCE,
             OBJECT_ATT.NAME_VN OBJECT_ATTENDANCE_NAME,
             B.TOTAL_DAY_SAL,
             B.TOTAL_NON_SAL,
             ' || V_COL || '  ' || V_COL1 || '
             FROM (
                  SELECT
                     T.EMPLOYEE_ID,
                     T.ORG_ID,
                     T.TITLE_ID,
                     T.OBJECT_ATTENDANCE,
                     TO_NUMBER(TO_CHAR(T.WORKINGDAY, ''dd'')) AS DAY,
                     NVL(L.CODE, '' '') CODE
                    FROM AT_TIME_TIMESHEET_DAILY_TMP T
                    LEFT JOIN AT_TIME_MANUAL L
                      ON T.MANUAL_ID = L.ID
                   WHERE T.REQUEST_ID=' || PV_REQUEST_ID || ' 
                   and (t.EMP_OBJ_ID = ' || P_EMP_OBJ ||
              ' or ' || P_EMP_OBJ ||
              ' =0) 
                   AND  T.WORKINGDAY BETWEEN ''' ||
              PV_STARTDATE || ''' AND ''' || PV_ENDDATE || '''
                  )
                 PIVOT(MAX(CODE)
                 FOR DAY IN(
                   ' || V_COL_V || '
                 )
               ) A
                LEFT JOIN(
                    SELECT T.EMPLOYEE_ID,
                    SUM(CASE WHEN  NVL(M.CODE,'''') IN(''VR'', ''DH'', ''CT'', ''B'', ''P'',''LT'',''X'') THEN 1 ELSE 0 END) TOTAL_DAY_SAL,
                    SUM(CASE WHEN NVL(M.CODE ,'''') IN(''NKL'',''NKL/X'') THEN 1 ELSE 0 END ) TOTAL_NON_SAL
                    FROM AT_TIME_TIMESHEET_DAILY_TMP T
                    LEFT JOIN AT_TIME_MANUAL M ON M.ID=T.MANUAL_ID
                    WHERE T.REQUEST_ID =' || PV_SEQ || '
                   and (t.EMP_OBJ_ID = ' || P_EMP_OBJ ||
              ' or ' || P_EMP_OBJ ||
              ' =0)
                     AND  T.WORKINGDAY BETWEEN ''' ||
              PV_STARTDATE || ''' AND ''' || PV_ENDDATE || '''
                    GROUP BY T.EMPLOYEE_ID
               ) B ON A.EMPLOYEE_ID=B.EMPLOYEE_ID

               INNER JOIN HU_EMPLOYEE EE
                  ON EE.ID = A.EMPLOYEE_ID
                LEFT JOIN HUV_ORGANIZATION O
                  ON O.ID = A.ORG_ID
                LEFT JOIN HU_TITLE TI
                  ON TI.ID = A.TITLE_ID
                 /* INNER JOIN OT_OTHER_LIST OT
                  ON OT.ID=EE.OBJECTTIMEKEEPING*/
               /* LEFT JOIN HU_STAFF_RANK S
                  ON S.ID = EE.STAFF_RANK_ID*/

                LEFT JOIN OT_OTHER_LIST OBJECT_ATT
                  ON OBJECT_ATT.ID = (CASE
                                        WHEN A.OBJECT_ATTENDANCE IS NULL OR A.OBJECT_ATTENDANCE = 0 THEN
                                          NULL
                                        WHEN A.OBJECT_ATTENDANCE > 0 THEN
                                          A.OBJECT_ATTENDANCE
                                        END)
             WHERE
              (NVL(EE.WORK_STATUS, 0) <> 257 OR
                     (NVL(EE.WORK_STATUS, 0) = 257 AND
                     EE.TER_LAST_DATE >= ''' || PV_STARTDATE ||
              ''')))
             ';
    INSERT INTO AT_STRSQL
    VALUES
      (SEQ_AT_STRSQL.NEXTVAL, PV_SQL_CRE_TB || PV_SQL);
    COMMIT;
  
    EXECUTE IMMEDIATE PV_SQL_CRE_TB || PV_SQL;
    -- END IF;
  
    PV_WHERE := '
     WHERE
     (''' || P_EMPLOYEE_CODE || ''' IS NULL OR
     (''' || P_EMPLOYEE_CODE || ''' IS NOT NULL AND
     EMPLOYEE_CODE LIKE ''%' || P_EMPLOYEE_CODE ||
                '%''))
     AND (''' || P_EMPLOYEE_NAME || ''' IS NULL OR
     (''' || P_EMPLOYEE_NAME ||
                ''' IS NOT NULL AND
      UPPER(VN_FULLNAME) LIKE ''%' || UPPER(P_EMPLOYEE_NAME) ||
                '%''))
   AND (''' || P_TITLE_NAME || ''' IS NULL OR
     (''' || P_TITLE_NAME ||
                ''' IS NOT NULL AND
      UPPER(TITLE_NAME) LIKE ''%' || UPPER(P_TITLE_NAME) ||
                '%''))
      AND (''' || P_ORG_NAME || ''' IS NULL OR
     (''' || P_ORG_NAME || ''' IS NOT NULL AND
      UPPER(ORG_NAME) LIKE ''%' || UPPER(P_ORG_NAME) ||
                '%''))
      AND (''' || P_OBJECT_ATTENDANCE_NAME || ''' IS NULL OR
     (''' || P_OBJECT_ATTENDANCE_NAME ||
                ''' IS NOT NULL AND
      UPPER(OBJECT_ATTENDANCE_NAME) LIKE ''%' ||
                UPPER(P_OBJECT_ATTENDANCE_NAME) || '%''))
     AND TT < (' || P_PAGE_INDEX || ' * ' || P_PAGE_SIZE ||
                ') + 1
     AND TT >= ((' || P_PAGE_INDEX || ' - 1) * ' ||
                P_PAGE_SIZE || ') + 1
    ';
    PV_SQL   := '
           SELECT * FROM ' || PV_TOKEN || '
    ';
    --==UPDATE THONG TIN SU DUNG CACHE DE PHUC VU CHO THU HOI CACHE SAU NAY
    UPDATE SE_MNG_TABLE_CACHE S
       SET S.USE_COUNT     = S.USE_COUNT + 1,
           S.MODIFIED_DATE = SYSDATE,
           S.MODIFIED_BY   = P_USERNAME
     WHERE S.TBL_NAME = PV_TOKEN;
    --=========================================
    INSERT INTO AT_STRSQL
    VALUES
      (SEQ_AT_STRSQL.NEXTVAL, PV_SQL || ' ' || PV_WHERE);
    COMMIT;
    OPEN P_CUR FOR TO_CHAR(PV_SQL || ' ' || PV_WHERE);
    DELETE AT_TEMP_DATE A WHERE A.REQUEST_ID = PV_SEQ;
    COMMIT;
    --=====================================================================
  
    PV_SQL := '
             SELECT COUNT(*) as TOTAL
             FROM ' || PV_TOKEN || '
    ';
  
    OPEN P_CURCOUNR FOR TO_CHAR(PV_SQL);
    --IF PV_CHECK =0 THEN
    DELETE FROM AT_TIME_TIMESHEET_DAILY_TMP T
     WHERE T.REQUEST_ID = PV_REQUEST_ID;
    --END IF;
    DELETE FROM AT_CHOSEN_ORG_TMP T where T.REQUEST_ID = PV_REQUEST_ID;
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      --IF PV_CHECK =0 THEN
      DELETE FROM AT_TIME_TIMESHEET_DAILY_TMP T
       WHERE T.REQUEST_ID = PV_REQUEST_ID;
      --END IF;
      DELETE FROM AT_CHOSEN_ORG_TMP T where T.REQUEST_ID = PV_REQUEST_ID;
      COMMIT;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.GET_CCT',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              P_ORG_ID,
                              P_PERIOD_ID,
                              P_USERNAME,
                              NULL,
                              NULL,
                              NULL);
  END;

  PROCEDURE UPDATE_TIMESHEET_CTT(P_FROMDATE  IN DATE,
                                 P_TODATE    IN DATE,
                                 P_EMP_ID    IN NUMBER,
                                 P_MANUAL_ID IN NUMBER,
                                 P_PERIOD_ID IN NUMBER,
                                 P_USERNAME  IN VARCHAR2) IS
    PV_TODATE DATE;
  BEGIN
    SELECT P.END_DATE
      INTO PV_TODATE
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
    -- Delete Data cu
    DELETE AT_TIME_TIMESHEET_DAILY T
     WHERE P_EMP_ID = T.EMPLOYEE_ID
       AND EXISTS (SELECT CDATE
              FROM TABLE(TABLE_LISTDATE(P_FROMDATE, P_TODATE))
             WHERE CDATE = T.WORKINGDAY);
  
    INSERT INTO AT_TIME_TIMESHEET_DAILY
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       WORKINGDAY,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG,
       MANUAL_ID,
       OBJECT_ATTENDANCE)
      SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
             T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             T.CDATE,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             T.MANUAL_ID,
             T.OBJECT_ATTENDANCE
        FROM (SELECT W.EMPLOYEE_ID,
                     W.ORG_ID,
                     W.TITLE_ID,
                     D.CDATE,
                     P_MANUAL_ID AS MANUAL_ID,
                     W.OBJECT_ATTENDANCE,
                     W.ROW_NUMBER
                FROM (SELECT E.EMPLOYEE_ID,
                             E.OBJECT_ATTENDANCE,
                             E.ORG_ID,
                             E.TITLE_ID,
                             ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                        FROM HU_WORKING E
                       WHERE E.EFFECT_DATE <= PV_TODATE
                         AND E.STATUS_ID = 447
                         AND E.IS_WAGE = 0
                         AND E.EMPLOYEE_ID = P_EMP_ID
                         AND E.IS_3B = 0) W
               CROSS JOIN (SELECT CDATE
                            FROM TABLE(TABLE_LISTDATE(P_FROMDATE, P_TODATE))) D) T
       WHERE T.ROW_NUMBER = 1;
  
    --==import cong khong c? gio cong nen phai su dung ky hieu
    UPDATE AT_TIME_TIMESHEET_DAILY D
       SET D.WORKING_MEAL =
           (SELECT SUM(CASE
                         WHEN F.ID = F2.ID AND F.CODE = 'X' THEN
                          1
                         WHEN F.ID = F2.ID AND F.CODE = 'CT' THEN
                          1
                         WHEN F.ID = F2.ID AND F.CODE = 'DH' THEN
                          1
                         ELSE
                          0
                       END)
              FROM AT_TIME_TIMESHEET_DAILY A
              LEFT JOIN AT_TIME_MANUAL M
                ON A.MANUAL_ID = M.ID
              LEFT JOIN AT_FML F
                ON M.MORNING_ID = F.ID
              LEFT JOIN AT_FML F2
                ON M.AFTERNOON_ID = F2.ID
             WHERE A.EMPLOYEE_ID = P_EMP_ID
               AND A.WORKINGDAY >= P_FROMDATE
               AND A.WORKINGDAY <= P_TODATE)
     WHERE D.WORKINGDAY >= P_FROMDATE
       AND D.WORKINGDAY <= P_TODATE
       AND D.EMPLOYEE_ID = P_EMP_ID;
  END;

  PROCEDURE INSERT_CHOSEN_LOGORG(P_USERNAME   IN NVARCHAR2,
                                 P_ORGID      IN NUMBER,
                                 P_ISDISSOLVE IN NUMBER,
                                 P_ACTION_ID  IN NUMBER) IS
  BEGIN
    INSERT INTO AT_ACTION_ORG_LOG E
      (SELECT SEQ_AT_ACTION_ORG_LOG.NEXTVAL, ORG_ID, NULL, P_ACTION_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(UPPER(P_USERNAME)),
                                    P_ORGID,
                                    NVL(P_ISDISSOLVE, 0))));
  END;

  PROCEDURE CAL_TIME_TIMESHEET_MONTHLY(P_USERNAME   VARCHAR2,
                                       P_PERIOD_ID  IN NUMBER,
                                       P_ORG_ID     IN NUMBER,
                                       P_ISDISSOLVE IN NUMBER,
                                       P_FROM_DATE  IN DATE,
                                       P_TO_DATE    IN DATE,
                                       P_OBJ_EMPLOYEE_ID IN NUMBER) IS
    PV_FROMDATE         DATE;
    PV_TODATE           DATE;
    PV_YEAR             NUMBER;
    PV_MONTH            NUMBER;
    PV_SQL              CLOB;
    PV_REQUEST_ID       NUMBER;
    PV_WORKING_STANDARD NUMBER := 26;
    PV_TBL              VARCHAR2(50);
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
    SELECT P_FROM_DATE,
           P_TO_DATE,
           EXTRACT(YEAR FROM P_TO_DATE),
           EXTRACT(YEAR FROM P_TO_DATE),
           P.PERIOD_STANDARD
      INTO PV_FROMDATE, PV_TODATE, PV_YEAR, PV_MONTH, PV_WORKING_STANDARD
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
  
    -- insert emp can tinh toan
    INSERT INTO AT_CHOSEN_EMP
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       PA_OBJECT_SALARY_ID,
       USERNAME,
       REQUEST_ID,
       TER_EFFECT_DATE,
       JOIN_DATE,
       JOIN_DATE_STATE,
       EXPIREDATE_ENT,
       DECISION_ID,
       OBJECT_ATTENDANCE,
       OBJECT_LABOR,
       OBJECT_EMPLOYEE_ID)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              T.PA_OBJECT_SALARY_ID,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              CASE
                WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                 T.TER_EFFECT_DATE + 1
                ELSE
                 NULL
              END TER_EFFECT_DATE,
              T.JOIN_DATE,
              T.JOIN_DATE_STATE,
              TO_DATE(TO_CHAR(NVL(EE.EXPIREDATE_ENT,
                                  NVL(E_PARAM.TO_LEAVE_YEAR,
                                      TO_DATE('3103' || PV_YEAR, 'ddmmyyyy'))),
                              'ddmm') || PV_YEAR,
                      'ddmmyyyy'),
              W.ID DECISION_ID,
              W.OBJECT_ATTENDANCE,
              T.OBJECT_LABOR,
              T.OBJECT_EMPLOYEE_ID
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.ID,
                          E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          E.OBJECT_ATTENDANCE,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_TODATE
                      AND E.STATUS_ID = 447
                      AND E.IS_MISSION = -1) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O WHERE O.REQUEST_ID = PV_REQUEST_ID) O
           ON O.ORG_ID = W.ORG_ID
         LEFT JOIN (SELECT E.EMPLOYEE_ID,
                          MAX(TO_DATE('01' ||
                                      TO_CHAR(E.START_MONTH_EXTEND, '00') ||
                                      E.YEAR_ENTITLEMENT,
                                      'ddmmyyyy')) EXPIREDATE_ENT
                     FROM AT_DECLARE_ENTITLEMENT E
                    WHERE E.START_MONTH_EXTEND IS NOT NULL
                      AND E.START_MONTH_EXTEND > 0
                      AND E.YEAR_ENTITLEMENT IS NOT NULL
                      AND E.YEAR_ENTITLEMENT = PV_YEAR
                      AND E.YEAR_ENTITLEMENT >= 1900
                    GROUP BY E.EMPLOYEE_ID) EE
           ON W.EMPLOYEE_ID = EE.EMPLOYEE_ID
         LEFT JOIN (SELECT *
                     FROM (SELECT E.TO_LEAVE_YEAR
                             FROM AT_LIST_PARAM_SYSTEM E
                            WHERE E.EFFECT_DATE_FROM <= PV_TODATE
                              AND E.TO_LEAVE_YEAR IS NOT NULL
                              AND E.ACTFLG = 'A')
                    WHERE ROWNUM = 1) E_PARAM
           ON 1 = 1
        WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE))
         AND T.OBJECT_EMPLOYEE_ID = P_OBJ_EMPLOYEE_ID
         AND T.ID NOT IN (SELECT DISTINCT (M.EMPLOYEE_ID) 
                           FROM AT_TIME_TIMESHEET_MONTHLY M 
                           WHERE M.PERIOD_ID=P_PERIOD_ID 
                                 AND M.IS_LOCK=-1));
  
    /*INSERT INTO AT_CHOSEN_EMP_CLEAR
      (EMPLOYEE_ID, REQUEST_ID)
      (SELECT EMPLOYEE_ID, PV_REQUEST_ID
         FROM (SELECT A.*,
                      ROW_NUMBER() OVER(PARTITION BY A.EMPLOYEE_ID ORDER BY A.EFFECT_DATE DESC, A.ID DESC) AS ROW_NUMBER
                 FROM HU_WORKING A
                WHERE A.STATUS_ID = 447
                  AND A.EFFECT_DATE <= PV_TODATE
                  AND A.IS_3B = 0) C
        INNER JOIN HU_EMPLOYEE EE
           ON C.EMPLOYEE_ID = EE.ID
          AND C.ROW_NUMBER = 1
        WHERE (NVL(EE.WORK_STATUS, 0) <> 257 OR
              (EE.WORK_STATUS = 257 AND EE.TER_LAST_DATE >= PV_FROMDATE)));*/
  
    ---------------------------------------------------------------------------------
    -- XOA BO DU LIEU CU TRONG BANG TAM
    ---------------------------------------------------------------------------------
    DELETE AT_TIME_TIMESHEET_MACHINE_TMP T;
  
    ---------------------------------------------------------------------------------
    -- INSERT DU LIEU MOI CAN TINH TRONG BANG TAM
    ---------------------------------------------------------------------------------
    INSERT INTO AT_TIME_TIMESHEET_MACHINE_TMP
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       WORKINGDAY,
       SHIFT_CODE,
       LEAVE_CODE,
       MANUAL_ID,
       MANUAL_CODE,
       SHIFT_ID,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG,
       TIMEVALIN,
       TIMEVALOUT,
       OBJECT_ATTENDANCE,
       MIN_IN_WORK,
       MIN_DEDUCT_WORK,
       MIN_ON_LEAVE,
       MIN_LATE,
       MIN_EARLY,
       MIN_OUT_WORK_DEDUCT,
       MIN_LATE_EARLY,
       MIN_OUT_WORK,
       MIN_DEDUCT,
       PERIOD_ID,
       REQUEST_ID,
       WORKING_VALUE,
       DAY_NUM,
       MIN_NIGHT,
       COUNT_SUPPORT,
       SALARY_ID_NEW)
      SELECT SEQ_AT_TIME_TIMESHEET_MACHINET.NEXTVAL,
             T.EMPLOYEE_ID,
             T1.ORG_ID,
             T1.TITLE_ID,
             WORKINGDAY,
             SHIFT_CODE,
             LEAVE_CODE,
             MANUAL_ID,
             MANUAL_CODE,
             SHIFT_ID,
             T.CREATED_DATE,
             T.CREATED_BY,
             T.CREATED_LOG,
             T.MODIFIED_DATE,
             T.MODIFIED_BY,
             T.MODIFIED_LOG,
             TIMEVALIN,
             TIMEVALOUT,
             T.OBJECT_ATTENDANCE,
             MIN_IN_WORK,
             MIN_DEDUCT_WORK,
             MIN_ON_LEAVE,
             MIN_LATE,
             MIN_EARLY,
             MIN_OUT_WORK_DEDUCT,
             MIN_LATE_EARLY,
             MIN_OUT_WORK,
             T.MIN_DEDUCT,
             P_PERIOD_ID,
             PV_REQUEST_ID,
             T.WORKING_VALUE,
             T.DAY_NUM,
             T.MIN_NIGHT,
             T.COUNT_SUPPORT,
             SAL.ID
             /*,
             --SAL.ID
             PKG_FUNCTION.SALARYMAX_BYDATE(T.EMPLOYEE_ID, T.WORKINGDAY)*/
        FROM AT_TIME_TIMESHEET_MACHINET T
       INNER JOIN (SELECT E.ID,
                          E.EMPLOYEE_ID,
                          ROW_NUMBER() OVER(PARTITION BY EMPLOYEE_ID ORDER BY TRUNC(EFFECT_DATE) DESC, E.ID DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                    WHERE TRUNC(E.EFFECT_DATE) <= TRUNC(PV_TODATE)
                      AND NVL(E.IS_WAGE, 0) <> 0
                      AND E.STATUS_ID = 447                   
                   ) SAL
          ON SAL.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND SAL.ROW_NUMBER = 1
       INNER JOIN AT_CHOSEN_EMP EE
          ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
          AND EE.REQUEST_ID = PV_REQUEST_ID
        LEFT JOIN HU_WORKING T1
          ON T1.ID = PKG_FUNCTION.WORKINGMAX_BYDATE(T.EMPLOYEE_ID, T.WORKINGDAY)
       WHERE T.WORKINGDAY BETWEEN PV_FROMDATE AND PV_TODATE;
    ---------------------------------------------------------------------------------
    -- INSERT DU LIEU VAO BANG TONG HOP CONG
    ---------------------------------------------------------------------------------




    INSERT INTO AT_TIME_MONTHLY_TEMP
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       DECISION_ID,
       SALARY_ID,
       SALARY_ID_NEW,
       PA_OBJECT_SALARY_ID,
       PERIOD_ID,
       FROM_DATE,
       END_DATE,
       MIN_IN_WORK,
       MIN_OUT_WORK,
       HOURS_NEW,
       MIN_DEDUCT_WORK,
       MIN_ON_LEAVE,
       --MIN_DEDUCT,
       MIN_OUT_WORK_DEDUCT,
       MIN_LATE,
       MIN_EARLY,
       MIN_LATE_EARLY,
       WORKING_X,
       WORKING_F,
       --WORKING_E,
       WORKING_A,
       WORKING_H,
       --WORKING_D,
       WORKING_C,
       WORKING_T,
       WORKING_Q,
       WORKING_N,
       WORKING_P,
       WORKING_L,
       WORKING_R,
       WORKING_B,
       WORKING_K,
       WORKING_J,
       WORKING_TS,
       WORKING_O,
       WORKING_V,
       WORKING_ADD,
       WORKING_STANDARD,
       REQUEST_ID,
       OBJECT_ATTENDANCE,
       WORKING_KLD,
       WORKING_TN,
       WORKING_DEDUCT,       
       WORKING_MEAL,
       WORKING_DA,
       EFFECT_DATE_SALARY,
       EFFECT_DATE_SALARY_MIN,
       WORKING_BHXH,
       SUM_SUPPORT,
       WORKING_WFH--,
       --FLAG_CHANGE_SALARY
       /*,
       C_AL_T,
       TOTAL_FACTOR1,
       TOTAL_FACTOR1_5,
       TOTAL_FACTOR1_8,
       TOTAL_FACTOR2,
       TOTAL_FACTOR2_1,
       TOTAL_FACTOR2_7,
       TOTAL_FACTOR3,
       TOTAL_FACTOR3_9,
       NGHIBU_CONLAI,
       WORKING_OUT_CN*/)
      SELECT SEQ_AT_TIME_MONTHLY_TEMP.NEXTVAL,
             T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             T.STAFF_RANK_ID,
             T.DECISION_ID,
             T.SALARY_ID,
             T.SALARY_ID_NEW,
             T.PA_OBJECT_SALARY_ID,
             P_PERIOD_ID,
             PV_FROMDATE,
             PV_TODATE,
             MIN_IN_WORK,
             MIN_OUT_WORK,
             HOURS_NEW,
             MIN_DEDUCT_WORK,
             MIN_ON_LEAVE,
             --MIN_DEDUCT,
             MIN_OUT_WORK_DEDUCT,
             MIN_LATE,
             MIN_EARLY,
             MIN_LATE_EARLY,
             CASE
               WHEN WORKING_X = 0 THEN
                NULL
               ELSE
                WORKING_X
             END WORKING_X,
             CASE
               WHEN WORKING_F = 0 THEN
                NULL
               ELSE
                WORKING_F
             END WORKING_F,
             CASE
               WHEN WORKING_A = 0 THEN
                NULL
               ELSE
                WORKING_A
             END WORKING_A,
             CASE
               WHEN WORKING_H = 0 THEN
                NULL
               ELSE
                WORKING_H
             END WORKING_H,
             CASE
               WHEN WORKING_C = 0 THEN
                NULL
               ELSE
                WORKING_C
             END WORKING_C,
             CASE
               WHEN WORKING_T = 0 THEN
                NULL
               ELSE
                WORKING_T
             END WORKING_T,
             CASE
               WHEN WORKING_Q = 0 THEN
                NULL
               ELSE
                WORKING_Q
             END WORKING_Q,
             CASE
               WHEN WORKING_N = 0 THEN
                NULL
               ELSE
                WORKING_N
             END WORKING_N,
             CASE
               WHEN WORKING_P = 0 THEN
                NULL
               ELSE
                WORKING_P
             END WORKING_P,
             CASE
               WHEN WORKING_L = 0 THEN
                NULL
               ELSE
                WORKING_L
             END WORKING_L,
             CASE
               WHEN WORKING_R = 0 THEN
                NULL
               ELSE
                WORKING_R
             END WORKING_R,
             CASE
               WHEN WORKING_B = 0 THEN
                NULL
               ELSE
                WORKING_B
             END WORKING_B,
             CASE
                   WHEN WORKING_K = 0 THEN
                    NULL
                   ELSE
                    WORKING_K
             END WORKING_K,
             CASE
               WHEN WORKING_J = 0 THEN
                NULL
               ELSE
                WORKING_J
             END WORKING_J,
             CASE
               WHEN WORKING_TS = 0 THEN
                NULL
               ELSE
                WORKING_TS
             END WORKING_TS,
             CASE
               WHEN WORKING_O = 0 THEN
                NULL
               ELSE
                WORKING_O
             END WORKING_O,
             CASE
               WHEN WORKING_V = 0 THEN
                NULL
               ELSE
                WORKING_V
             END WORKING_V,
             CASE
               WHEN WORKING_ADD = 0 THEN
                NULL
               ELSE
                WORKING_ADD
             END WORKING_ADD,
             PKG_ATTENDANCE_LIST.AT_GET_WORKSTANDARD(EMPLOYEE_ID,P_PERIOD_ID),
             PV_REQUEST_ID,
             OBJECT_ATTENDANCE,
             CASE
               WHEN WORKING_KLD = 0 THEN
                NULL
               ELSE
                WORKING_KLD
             END WORKING_KLD,
             CASE
               WHEN WORKING_TN = 0 THEN
                NULL
               ELSE
                WORKING_TN
             END WORKING_TN,         
             NVL(FN_CALL_DEDUCT(T.MIN_LATE,
                                T.MIN_EARLY,
                                T.MIN_LATE_EARLY,
                                T.ORG_ID,
                                PV_TODATE),
                 0),    
             CASE
               WHEN WORKING_MEAL = 0 THEN
                NULL
               ELSE
                WORKING_MEAL
             END WORKING_MEAL,
             CASE
               WHEN WORKING_DA = 0 THEN
                NULL
               ELSE
                WORKING_DA
             END WORKING_DA ,
             EFFECT_DATE_SALARY,
             EFFECT_DATE_SALARY_MIN,/*,
             FLAG_CHANGE_SALARY*/
             CASE
               WHEN WORKING_BHXH = 0 THEN
                NULL
               ELSE
                WORKING_BHXH
             END WORKING_BHXH,
             CASE
               WHEN SUM_SUPPORT = 0 THEN
                NULL
               ELSE
                SUM_SUPPORT
             END SUM_SUPPORT,
             NVL(T.WFH_DAY,0) + NVL(T.WFH_LEAVE,0) WORKING_WFH   
        FROM (SELECT A.EMPLOYEE_ID,
                     EE.ORG_ID,
                     EE.TITLE_ID,
                     EE.DECISION_ID,
                     EE.STAFF_RANK_ID,
                     EE.PA_OBJECT_SALARY_ID,
                     PKG_FUNCTION.SALARYMAX_BYDATE(A.EMPLOYEE_ID, A.WORKINGDAY) SALARY_ID,
                     A.SALARY_ID_NEW,
                     SUM(NVL(MIN_IN_WORK, 0)) MIN_IN_WORK,
                     SUM(NVL(MIN_NIGHT, 0)) MIN_OUT_WORK,
                     SUM(NVL(MIN_OUT_WORK, 0)) HOURS_NEW,
                     SUM(NVL(MIN_DEDUCT_WORK, 0)) MIN_DEDUCT_WORK,
                     SUM(NVL(MIN_ON_LEAVE, 0)) MIN_ON_LEAVE,
                     --SUM(NVL(MIN_DEDUCT, 0)) MIN_DEDUCT,
                     SUM(NVL(MIN_OUT_WORK_DEDUCT, 0)) MIN_OUT_WORK_DEDUCT,
                     SUM( CASE 
                              WHEN MIN_LATE_EARLY >= 60 THEN
                              MIN_LATE_EARLY 
                          ELSE
                              0
                          END                                        
                     ) MIN_LATE,
                     SUM(CASE 
                              WHEN MIN_LATE_EARLY >= 60 THEN
                              MIN_LATE_EARLY 
                          ELSE
                              0
                          END                     
                     ) MIN_EARLY,
                     SUM(NVL(MIN_LATE_EARLY, 0)) MIN_LATE_EARLY,
                     --A.PERIOD_ID PERIOD_ID,
                     /*map ky hieu AT_FML voi collumn trong AT_TIME_TIMESHEET_DAILY*/
                     SUM(CASE
                           WHEN S.CODE NOT IN ('OFF','L')THEN
                            A.WORKING_VALUE
                           ELSE
                            0
                         END) WORKING_X, --==Ngay di lam
                     SUM(CASE
                           WHEN S.CODE = 'OFF' THEN
                                1
                           ELSE
                            0
                         END) WORKING_F, --==OFF
                     SUM(CASE
                           WHEN S.CODE = 'L' THEN
                            1
                           ELSE
                            0
                         END) WORKING_L, --==Ngay Le Tet
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) = 'TS' THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_TS, --== Nghi thai san
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) IN ('O','O1','O2','O3') THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_O, --==nghi om
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) = 'P' THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_P, --==nghi phep
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) = 'B' THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_B, --==nghi bu
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) = 'C' THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_C, --== cong tac
                     SUM(CASE WHEN NVL(A.MIN_IN_WORK,0) +
                                   (CASE WHEN NVL(OT.IS_DELETED,0) = 0 AND NVL(OT.STATUS,0) <> 0 THEN                                                        
                                        NVL(OT.TOTAL_OT_TT,0)
                                   ELSE
                                        0
                                   END) >= 4 THEN
                               1 
                             ELSE 
                               0 
                             END) WORKING_A, --==CONG XE
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) IN ('VR','NKL') THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_V, --==NGHI KHONG LUONG
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) ='DS' THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_H, --==Nghi bien chung thai san
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) = 'KT' THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_Q, --==Nghi kham thai
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) IN ('VS','VS1','VS2','VS3') THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_N, --==Lao dong Nam nghi vo sinh
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) IN ('KH','CKH') THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_R, --==Nghi ket hon
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) = 'TG' THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_T, --==Nghi tang gia
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) = 'CV' THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_K, --==Nghi COVID
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) IN ('CO','CO1') THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_J, --==Nghi CON OM
                     A.OBJECT_ATTENDANCE,
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) IN ('KL1','KL2','KL3') THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_KLD,
                     SUM(CASE
                           WHEN TRIM(UPPER(A.MANUAL_CODE)) IN ('HL1','HL2','HL3') THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_TN,
                     EE.OBJECT_LABOR, 
                     SUM(CASE 
                           WHEN A.MIN_IN_WORK >= 4 THEN
                           1
                           ELSE
                           0
                           END) +
                      SUM(CASE 
                           WHEN NVL(OT.TOTAL_OT_TT,0) >= 4 AND NVL(OT.IS_DELETED,0) = 0 AND NVL(OT.STATUS,0) = 1 THEN
                           1
                           ELSE
                           0
                           END) WORKING_MEAL, -- CONG COM 
                     NVL(WE.WAGE_OFFSET,0) WORKING_ADD, -- BU CONG
                     SUM(CASE 
                           WHEN A.MIN_IN_WORK >= 4 THEN
                           1
                           ELSE
                           0
                           END) WORKING_DA, --SO GIO LAM DEM                 
                         W.EFFECT_DATE EFFECT_DATE_SALARY,        -- NGAY HIEU LUC QUYET DINH THAY DOI LUONG    
                         MIN(W.EFFECT_DATE) EFFECT_DATE_SALARY_MIN,
                         SUM(CASE
                           WHEN FML.CODE <> '' AND TRIM(UPPER(A.MANUAL_CODE)) IN (UPPER(TRIM(FML.CODE))) THEN
                             A.DAY_NUM
                           ELSE
                            0
                         END) WORKING_BHXH,
                         SUM(A.COUNT_SUPPORT) SUM_SUPPORT,
                         SUM(CASE
                                WHEN A.SHIFT_CODE='WFH' THEN 
                                  1
                                ELSE
                                  0
                              END) WFH_DAY,
                         SUM(CASE
                                WHEN A.MANUAL_CODE='WFH' THEN 
                                  A.DAY_NUM
                                ELSE
                                  0
                              END) WFH_LEAVE   
                         --TB.FLAG_CHANGE_SALARY FLAG_CHANGE_SALARY                                
                FROM AT_TIME_TIMESHEET_MACHINE_TMP A             
                LEFT JOIN AT_OT_REGISTRATION OT
                  ON OT.EMPLOYEE_ID = A.EMPLOYEE_ID
                 And TRUNC(OT.REGIST_DATE) = TRUNC(A.WORKINGDAY)
                --AND OT.SALARY_ID = PKG_FUNCTION.SALARYMAX_BYDATE(A.EMPLOYEE_ID,A.WORKINGDAY)
               INNER JOIN AT_CHOSEN_EMP EE
                  ON A.EMPLOYEE_ID = EE.EMPLOYEE_ID
                  AND EE.REQUEST_ID = PV_REQUEST_ID
                LEFT JOIN AT_TIME_MANUAL M
                  ON A.MANUAL_ID = M.ID                 
                LEFT JOIN AT_SHIFT S
                  ON A.SHIFT_ID = S.ID
                LEFT JOIN AT_WAGEOFFSET_EMP WE
                  ON WE.EMPLOYEE_ID = A.EMPLOYEE_ID
                  AND WE.PERIOD_ID = A.PERIOD_ID
                /*LEFT JOIN (SELECT E.EMPLOYEE_ID,
                              COUNT(E.ID),
                              CASE WHEN COUNT(E.ID) > 1 THEN 1 ELSE 0 END FLAG_CHANGE_SALARY
                         FROM HU_WORKING E
                        WHERE TRUNC(E.EFFECT_DATE) <= TRUNC(P_TO_DATE)
                          AND NVL(E.IS_WAGE, 0) <> 0
                          AND E.STATUS_ID = 447
                          AND NVL(E.IS_DELETED,0) = 0
                          GROUP BY E.EMPLOYEE_ID
                          HAVING COUNT(E.ID) > 1) TB
                  ON TB.EMPLOYEE_ID = A.EMPLOYEE_ID*/
                LEFT JOIN HU_WORKING W 
                  ON W.EMPLOYEE_ID = A.EMPLOYEE_ID 
                 AND W.ID = PKG_FUNCTION.SALARYMAX_BYDATE(A.EMPLOYEE_ID,A.WORKINGDAY)
                LEFT JOIN (SELECT LISTAGG(''''||FML.CODE||'''',',') WITHIN GROUP(ORDER BY FML.CODE) CODE FROM AT_FML FML WHERE NVL(FML.IS_BHXH,0) = -1) FML
                  ON UPPER(TRIM(A.MANUAL_CODE)) IN (UPPER(TRIM(FML.CODE)))
              WHERE A.REQUEST_ID = PV_REQUEST_ID           
               GROUP BY A.EMPLOYEE_ID,
                        EE.DECISION_ID,
                        --A.SALARY_ID,
                        /* S.ID,*/
                        PKG_FUNCTION.SALARYMAX_BYDATE(A.EMPLOYEE_ID,A.WORKINGDAY),
                        EE.ORG_ID,
                        EE.TITLE_ID,
                        EE.STAFF_RANK_ID,
                        EE.PA_OBJECT_SALARY_ID,
                        A.OBJECT_ATTENDANCE,
                        EE.OBJECT_LABOR,                        
                        WE.WAGE_OFFSET,
                        W.EFFECT_DATE,
                        A.SALARY_ID_NEW
                        --FLAG_CHANGE_SALARY
                        ) T;
  
    ---------------------------------------------------------------------------------
    -- INSERT DU LIEU VAO BANG TONG HOP CONG
  
    --------------------------------------------------------------------------------------
    -- AP DUNG CONG THUC TINH CHO CAC COT TREN BANG CONG TONG HOP
    --------------------------------------------------------------------------------------
    FOR CUR_ITEM IN (SELECT *
                       FROM AT_TIME_FORMULAR T
                      WHERE T.TYPE IN (2)
                        AND T.STATUS = 1
                      ORDER BY T.ORDERBY) LOOP
    
      PV_SQL := 'UPDATE AT_TIME_MONTHLY_TEMP T SET ' ||
                CUR_ITEM.FORMULAR_CODE || '= ' ||
                CUR_ITEM.FORMULAR_VALUE || '';
      INSERT INTO TEMP (TEXT) VALUES (PV_SQL);
      EXECUTE IMMEDIATE PV_SQL;
    
    END LOOP;
    DELETE AT_TIME_TIMESHEET_MONTHLY T
     WHERE t.period_id = P_PERIOD_ID
       and T.ORG_ID IN (SELECT A.ORG_ID
                          FROM AT_CHOSEN_ORG A
                         WHERE A.REQUEST_ID = PV_REQUEST_ID)
       AND T.EMPLOYEE_ID IN (SELECT E.EMPLOYEE_ID
                               FROM AT_CHOSEN_EMP E
                              WHERE E.REQUEST_ID = PV_REQUEST_ID);
    /* WHERE EXISTS (SELECT EMPLOYEE_ID
           FROM AT_CHOSEN_EMP O
          WHERE T.EMPLOYEE_ID = O.EMPLOYEE_ID)
    AND T.PERIOD_ID = P_PERIOD_ID;*/
    PV_TBL := 'AT_TIME_MONTHLY_TEMP_'||TO_CHAR(PV_REQUEST_ID);
  PV_SQL := 'CREATE TABLE '||PV_TBL||' AS(
            SELECT EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         DECISION_ID,
         PA_OBJECT_SALARY_ID,
         PERIOD_ID,
         FROM_DATE,
         END_DATE,         
         SUM(CASE WHEN T.SALARY_ID_NEW <> T.SALARY_ID THEN
              MIN_OUT_WORK
         ELSE 0 END) MIN_IN_WORK,
         SUM(CASE WHEN T.SALARY_ID_NEW = T.SALARY_ID THEN
              MIN_OUT_WORK
         ELSE 0 END) MIN_OUT_WORK,  
         SUM(CASE WHEN T.SALARY_ID_NEW <> T.SALARY_ID THEN
              HOURS_NEW
         ELSE 0 END) HOURS_OLD,
         SUM(CASE WHEN T.SALARY_ID_NEW = T.SALARY_ID THEN
              HOURS_NEW
         ELSE 0 END) HOURS_NEW,  
         /*
         CASE WHEN COUNT(EMPLOYEE_ID) >= 2 THEN            
           MIN(MIN_OUT_WORK)
         ELSE 
           0
         END MIN_IN_WORK,
         CASE WHEN COUNT(EMPLOYEE_ID) > 2 THEN            
              0
         ELSE 
              MAX(MIN_OUT_WORK)
         END MIN_OUT_WORK,
         CASE WHEN COUNT(EMPLOYEE_ID) >= 2 THEN            
              MIN(HOURS_NEW)
         ELSE 
           0
         END HOURS_OLD,
         CASE WHEN COUNT(EMPLOYEE_ID) > 2 THEN            
              0
         ELSE 
              MAX(HOURS_NEW)
         END HOURS_NEW,*/
         SUM(MIN_DEDUCT_WORK) MIN_DEDUCT_WORK,
         SUM(MIN_ON_LEAVE) MIN_ON_LEAVE,
         SUM(MIN_LATE) MIN_DEDUCT,
         SUM(MIN_OUT_WORK_DEDUCT) MIN_OUT_WORK_DEDUCT,
         SUM(CASE WHEN T.SALARY_ID_NEW <> T.SALARY_ID THEN
              MIN_LATE
         ELSE 0 END) MIN_LATE,
         SUM(CASE WHEN T.SALARY_ID_NEW = T.SALARY_ID THEN
              MIN_LATE
         ELSE 0 END) MIN_EARLY,
         /*CASE WHEN COUNT(EMPLOYEE_ID) >= 2 THEN            
              MIN(MIN_LATE) 
         ELSE 
           0
         END MIN_LATE,
         CASE WHEN COUNT(EMPLOYEE_ID) > 2 THEN            
              0
         ELSE 
            MAX(MIN_LATE)
         END MIN_EARLY, */        
         SUM(MIN_LATE_EARLY) MIN_LATE_EARLY,
         SUM(WORKING_X) WORKING_X,
         SUM(WORKING_S) WORKING_S,
         SUM(WORKING_F) WORKING_F,
         SUM(WORKING_A) WORKING_A,
         SUM(WORKING_H) WORKING_H,
         SUM(WORKING_C) WORKING_C,
         SUM(WORKING_T) WORKING_T,
         SUM(WORKING_Q) WORKING_Q,
         SUM(WORKING_N) WORKING_N,
         SUM(WORKING_P) WORKING_P,
         SUM(WORKING_L) WORKING_L,
         SUM(WORKING_R) WORKING_R,
         SUM(WORKING_B) WORKING_B,
         SUM(WORKING_K) WORKING_K,
         SUM(WORKING_J) WORKING_J,
         SUM(WORKING_TS) WORKING_TS,
         SUM(WORKING_O) WORKING_O,
         SUM(WORKING_V) WORKING_V,
         WORKING_STANDARD WORKING_STANDARD,
         REQUEST_ID,
         OBJECT_ATTENDANCE,
         SUM(WORKING_KLD) WORKING_KLD,
         SUM(WORKING_TN) WORKING_TN,
         SUM(WORKING_DEDUCT) WORKING_DEDUCT,
         SUM(WORKING_MEAL) WORKING_MEAL,
         WORKING_ADD WORKING_ADD,
         SUM(WORKING_DA) WORKING_DA,                
         SUM(TOTAL_W_NOSALARY) TOTAL_W_H,
         SUM(CASE WHEN T.SALARY_ID_NEW <> T.SALARY_ID THEN
              TOTAL_W_NOSALARY
         ELSE 0 END) TOTAL_W_SALARY,
         SUM(CASE WHEN T.SALARY_ID_NEW = T.SALARY_ID THEN
              TOTAL_W_NOSALARY
         ELSE 0 END) TOTAL_W_NOSALARY,      
         
         /*CASE WHEN COUNT(EMPLOYEE_ID) >= 2 THEN            
              MIN(TOTAL_W_NOSALARY)
         ELSE 
           0
         END TOTAL_W_SALARY,
         CASE WHEN COUNT(EMPLOYEE_ID) > 2 THEN            
              0
         ELSE 
              MAX(TOTAL_W_NOSALARY)
         END TOTAL_W_NOSALARY,*/
         DECISION_START,
         DECISION_END,         
         SUM(WORKING_BHXH) WORKING_BHXH,    
         SUM(SUM_SUPPORT) SUM_SUPPORT,
         SUM(WORKING_WFH) WORKING_WFH
    FROM AT_TIME_MONTHLY_TEMP T
   WHERE T.REQUEST_ID = '||PV_REQUEST_ID||'
   GROUP BY EMPLOYEE_ID,
            ORG_ID,
            TITLE_ID,
            STAFF_RANK_ID,
            DECISION_ID,
            PA_OBJECT_SALARY_ID,
            PERIOD_ID,
            FROM_DATE,
            END_DATE,
            REQUEST_ID,
            OBJECT_ATTENDANCE,
            DECISION_START,
            DECISION_END,
            WORKING_STANDARD,
            WORKING_ADD      
  )';
       


 INSERT INTO AT_STRSQL(ID,STRINGSQL)
    VALUES(SEQ_AT_STRSQL.NEXTVAL,PV_SQL); 
    COMMIT;
  EXECUTE IMMEDIATE PV_SQL;
  
    PV_SQL:='INSERT INTO AT_TIME_TIMESHEET_MONTHLY
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       DECISION_ID,
       --SALARY_ID,
       DECISION_START,
       DECISION_END,
       PA_OBJECT_SALARY_ID,
       PERIOD_ID,
       FROM_DATE,
       END_DATE,
       WORKING_X,
       WORKING_F,
       --WORKING_E,
       WORKING_A,
       WORKING_H,
       --WORKING_D,
       WORKING_C,
       WORKING_T,
       WORKING_Q,
       WORKING_N,
       WORKING_P,
       WORKING_L,
       WORKING_R,
       WORKING_S,
       WORKING_B,
       WORKING_K,
       WORKING_J,
       WORKING_TS,
       WORKING_O,
       WORKING_V,
       WORKING_ADD,
       WORKING_DA,
       WORKING_MEAL,
       MIN_IN_WORK,
       MIN_OUT_WORK,
       HOURS_OLD,
       HOURS_NEW,
       MIN_DEDUCT_WORK,
       MIN_ON_LEAVE,
       MIN_DEDUCT,
       MIN_OUT_WORK_DEDUCT,
       MIN_LATE,
       MIN_EARLY,
       MIN_LATE_EARLY,
       WORKING_STANDARD,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG,
       OBJECT_ATTENDANCE,
       TOTAL_W_SALARY,
       TOTAL_W_NOSALARY,
       TOTAL_W_H,
       WORKING_KLD,
       WORKING_TN,
       SALARY_ID_NEW,
       WORKING_DEDUCT,
       WORKING_BHXH,
       SUM_SUPPORT,
       WORKING_WFH)
      SELECT SEQ_AT_TIME_TIMESHEET_MONTHLY.NEXTVAL,
             T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             T.STAFF_RANK_ID,
             T.DECISION_ID,
             --T.SALARY_ID,
             T.DECISION_START,
             T.DECISION_END,
             T.PA_OBJECT_SALARY_ID,
             T.PERIOD_ID,
             T.FROM_DATE,
             T.END_DATE,
             T.WORKING_X,
             T.WORKING_F,
             --T.WORKING_E,
             T.WORKING_A,
             T.WORKING_H,
             --T.WORKING_D,
             T.WORKING_C,
             T.WORKING_T,
             T.WORKING_Q,
             T.WORKING_N,
             T.WORKING_P,
             T.WORKING_L,
             T.WORKING_R,
             T.WORKING_S,
             T.WORKING_B,
             NVL(T.WORKING_K, 0),
             T.WORKING_J,
             T.WORKING_TS,
             T.WORKING_O,
             T.WORKING_V,
             T.WORKING_ADD,
             T.WORKING_DA,
             T.WORKING_MEAL,
             T.MIN_IN_WORK,
             T.MIN_OUT_WORK,
             T.HOURS_OLD,
             T.HOURS_NEW,
             T.MIN_DEDUCT_WORK,
             T.MIN_ON_LEAVE,
             T.MIN_DEDUCT,
             T.MIN_OUT_WORK_DEDUCT,
             T.MIN_LATE,
             T.MIN_EARLY,
             T.MIN_LATE_EARLY,
             /*P.PERIOD_STANDARD,*/
             T.WORKING_STANDARD,
             SYSDATE,
             UPPER('''||TRIM(UPPER(P_USERNAME))||'''),
             UPPER('''||TRIM(UPPER(P_USERNAME))||'''),
             SYSDATE,
             UPPER('''||TRIM(UPPER(P_USERNAME))||'''),
             UPPER('''||TRIM(UPPER(P_USERNAME))||'''),
             T.OBJECT_ATTENDANCE,
             NVL(T.TOTAL_W_SALARY,0),
             NVL(T.TOTAL_W_NOSALARY,0),
             NVL(T.TOTAL_W_H,0),
             T.WORKING_KLD,
             T.WORKING_TN,
             S.ID,
             T.WORKING_DEDUCT,
             T.WORKING_BHXH,
             T.SUM_SUPPORT,
             T.WORKING_WFH
        FROM '||PV_TBL||' T
       INNER JOIN AT_PERIOD P
          ON T.PERIOD_ID = P.ID
       INNER JOIN (SELECT E.*
                     FROM (SELECT W.ID,
                                  W.EMPLOYEE_ID,
                                  ROW_NUMBER() OVER(PARTITION BY W.EMPLOYEE_ID ORDER BY W.EFFECT_DATE DESC, W.ID DESC) RN
                             FROM HU_WORKING W
                            WHERE W.STATUS_ID = 447
                              AND W.IS_3B = 0
                              AND W.IS_WAGE = -1
                              AND W.EFFECT_DATE <= '''||PV_TODATE||''') E
                    WHERE E.RN = 1) S
          ON T.EMPLOYEE_ID = S.EMPLOYEE_ID
          WHERE T.REQUEST_ID = '||PV_REQUEST_ID||'
          ';
          /* INSERT INTO AT_STRSQL(ID,STRINGSQL)
    VALUES(SEQ_AT_STRSQL.NEXTVAL,PV_SQL); 
    COMMIT;*/
  EXECUTE IMMEDIATE PV_SQL;
  /*
    DELETE AT_TIME_TIMESHEET_DAILY E
     WHERE E.WORKINGDAY >= PV_FROMDATE
       AND E.WORKINGDAY <= PV_TODATE
       AND E.EMPLOYEE_ID NOT IN
           (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);*/
  
    COMMIT;
    /*CALL_ENTITLEMENT_TNG(P_USERNAME, P_ORG_ID, P_PERIOD_ID, P_ISDISSOLVE);
    COMMIT;
    CALL_ENTITLEMENT_NB(P_USERNAME, P_ORG_ID, P_PERIOD_ID, P_ISDISSOLVE);
    COMMIT; --TINH SAU*/
    /*INSERT_INS_CHANGE(P_USERNAME, P_ORG_ID, P_PERIOD_ID, P_ISDISSOLVE);*/
    
    DELETE AT_TIME_TIMESHEET_MACHINE_TMP W WHERE W.REQUEST_ID = PV_REQUEST_ID;
    DELETE AT_TIME_MONTHLY_TEMP W WHERE W.REQUEST_ID = PV_REQUEST_ID;
    DELETE AT_CHOSEN_EMP W WHERE W.REQUEST_ID = PV_REQUEST_ID;
    DELETE AT_CHOSEN_ORG W WHERE W.REQUEST_ID = PV_REQUEST_ID;
    EXECUTE IMMEDIATE 'DROP TABLE '|| PV_TBL;   
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;      
      DELETE AT_TIME_TIMESHEET_MACHINE_TMP W WHERE W.REQUEST_ID = PV_REQUEST_ID;
      DELETE AT_TIME_MONTHLY_TEMP W WHERE W.REQUEST_ID = PV_REQUEST_ID;
      DELETE AT_CHOSEN_EMP W WHERE W.REQUEST_ID = PV_REQUEST_ID;
      DELETE AT_CHOSEN_ORG W WHERE W.REQUEST_ID = PV_REQUEST_ID;      
       BEGIN
        EXECUTE IMMEDIATE 'DROP TABLE '|| PV_TBL;
        EXCEPTION
          WHEN OTHERS THEN
            IF SQLCODE != -942 THEN
                    ROLLBACK;      
            END IF;
        END;
      COMMIT;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_MONTHLY',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              P_ORG_ID,
                              P_PERIOD_ID,
                              P_USERNAME,
                              PV_REQUEST_ID,
                              NULL);
  END;

  PROCEDURE CAL_TIMESHEET_MONTHLY_HOSE(P_USERNAME   VARCHAR2,
                                       P_PERIOD_ID  IN NUMBER,
                                       P_ORG_ID     IN NUMBER,
                                       P_ISDISSOLVE IN NUMBER) IS
    PV_FROMDATE         DATE;
    PV_TODATE           DATE;
    PV_YEAR             NUMBER;
    PV_SQL              CLOB;
    PV_REQUEST_ID       NUMBER;
    PV_WORKING_STANDARD NUMBER := 26;
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
    SELECT P.START_DATE,
           P.END_DATE,
           EXTRACT(YEAR FROM P.END_DATE),
           P.PERIOD_STANDARD
      INTO PV_FROMDATE, PV_TODATE, PV_YEAR, PV_WORKING_STANDARD
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O
       /*WHERE EXISTS (SELECT 1
        FROM AT_ORG_PERIOD OP
       WHERE OP.PERIOD_ID = P_PERIOD_ID
         AND OP.ORG_ID = O.ORG_ID
         AND OP.STATUSCOLEX = 1)*/
       );
  
    -- insert emp can tinh toan
    INSERT INTO AT_CHOSEN_EMP
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       PA_OBJECT_SALARY_ID,
       USERNAME,
       REQUEST_ID,
       TER_EFFECT_DATE,
       JOIN_DATE,
       JOIN_DATE_STATE,
       EXPIREDATE_ENT,
       DECISION_ID,
       OBJECT_ATTENDANCE)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              T.PA_OBJECT_SALARY_ID,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              CASE
                WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                 T.TER_EFFECT_DATE + 1
                ELSE
                 NULL
              END TER_EFFECT_DATE,
              T.JOIN_DATE,
              T.JOIN_DATE_STATE,
              TO_DATE(TO_CHAR(NVL(EE.EXPIREDATE_ENT,
                                  NVL(E_PARAM.TO_LEAVE_YEAR,
                                      TO_DATE('3103' || PV_YEAR, 'ddmmyyyy'))),
                              'ddmm') || PV_YEAR,
                      'ddmmyyyy'),
              W.ID DECISION_ID,
              W.OBJECT_ATTENDANCE
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.ID,
                          E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          E.OBJECT_ATTENDANCE,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_TODATE
                      AND E.STATUS_ID = 447
                      AND E.IS_3B = 0) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
           ON O.ORG_ID = W.ORG_ID
         LEFT JOIN (SELECT E.EMPLOYEE_ID,
                          MAX(TO_DATE('01' ||
                                      TO_CHAR(E.START_MONTH_EXTEND, '00') ||
                                      E.YEAR_ENTITLEMENT,
                                      'ddmmyyyy')) EXPIREDATE_ENT
                     FROM AT_DECLARE_ENTITLEMENT E
                    WHERE E.START_MONTH_EXTEND IS NOT NULL
                      AND E.START_MONTH_EXTEND > 0
                      AND E.YEAR_ENTITLEMENT IS NOT NULL
                      AND E.YEAR_ENTITLEMENT = PV_YEAR
                      AND E.YEAR_ENTITLEMENT >= 1900
                    GROUP BY E.EMPLOYEE_ID) EE
           ON W.EMPLOYEE_ID = EE.EMPLOYEE_ID
         LEFT JOIN (SELECT *
                     FROM (SELECT E.TO_LEAVE_YEAR
                             FROM AT_LIST_PARAM_SYSTEM E
                            WHERE E.EFFECT_DATE_FROM <= PV_TODATE
                              AND E.TO_LEAVE_YEAR IS NOT NULL
                              AND E.ACTFLG = 'A')
                    WHERE ROWNUM = 1) E_PARAM
           ON 1 = 1
        WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  
    INSERT INTO AT_CHOSEN_EMP_CLEAR
      (EMPLOYEE_ID, REQUEST_ID)
      (SELECT EMPLOYEE_ID, PV_REQUEST_ID
         FROM (SELECT A.*,
                      ROW_NUMBER() OVER(PARTITION BY A.EMPLOYEE_ID ORDER BY A.EFFECT_DATE DESC, A.ID DESC) AS ROW_NUMBER
                 FROM HU_WORKING A
                WHERE A.STATUS_ID = 447
                  AND A.EFFECT_DATE <= PV_TODATE
                  AND A.IS_3B = 0) C
        INNER JOIN HU_EMPLOYEE EE
           ON C.EMPLOYEE_ID = EE.ID
          AND C.ROW_NUMBER = 1
        WHERE (NVL(EE.WORK_STATUS, 0) <> 257 OR
              (EE.WORK_STATUS = 257 AND EE.TER_LAST_DATE >= PV_FROMDATE)));
  
    ---------------------------------------------------------------------------------
    -- XOA BO DU LIEU CU TRONG BANG TAM
    ---------------------------------------------------------------------------------
    DELETE AT_TIME_TIMESHEET_DAILY_TEM T
    /* WHERE T.WORKINGDAY >= PV_FROMDATE
    AND T.WORKINGDAY <= PV_TODATE
    AND T.EMPLOYEE_ID IN (SELECT EE.EMPLOYEE_ID FROM AT_CHOSEN_EMP EE)*/
    ;
  
    ---------------------------------------------------------------------------------
    -- INSERT DU LIEU MOI CAN TINH TRONG BANG TAM
    ---------------------------------------------------------------------------------
    INSERT INTO AT_TIME_TIMESHEET_DAILY_TEM
      (EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       WORKINGDAY,
       SHIFT_CODE,
       LEAVE_CODE,
       MANUAL_ID,
       SHIFT_ID,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG,
       PA_OBJECT_SALARY_ID,
       TIMEVALIN,
       TIMEVALOUT,
       OBJECT_ATTENDANCE,
       MIN_IN_WORK,
       MIN_DEDUCT_WORK,
       MIN_ON_LEAVE,
       MIN_LATE,
       MIN_EARLY,
       MIN_OUT_WORK_DEDUCT,
       MIN_LATE_EARLY,
       MIN_OUT_WORK,
       MIN_DEDUCT)
      SELECT T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             WORKINGDAY,
             SHIFT_CODE,
             LEAVE_CODE,
             MANUAL_ID,
             SHIFT_ID,
             CREATED_DATE,
             CREATED_BY,
             CREATED_LOG,
             MODIFIED_DATE,
             MODIFIED_BY,
             MODIFIED_LOG,
             EE.PA_OBJECT_SALARY_ID,
             TIMEVALIN,
             TIMEVALOUT,
             T.OBJECT_ATTENDANCE,
             MIN_IN_WORK,
             MIN_DEDUCT_WORK,
             MIN_ON_LEAVE,
             MIN_LATE,
             MIN_EARLY,
             MIN_OUT_WORK_DEDUCT,
             MIN_LATE_EARLY,
             MIN_OUT_WORK,
             T.MIN_DEDUCT
        FROM AT_TIME_TIMESHEET_DAILY T
       INNER JOIN AT_CHOSEN_EMP EE
          ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
       WHERE T.WORKINGDAY BETWEEN PV_FROMDATE AND PV_TODATE;
    ---------------------------------------------------------------------------------
    -- INSERT DU LIEU VAO BANG TONG HOP CONG
    ---------------------------------------------------------------------------------
    INSERT INTO AT_TIME_MONTHLY_TEMP
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       DECISION_ID,
       SALARY_ID,
       PA_OBJECT_SALARY_ID,
       PERIOD_ID,
       FROM_DATE,
       END_DATE,
       MIN_IN_WORK,
       MIN_OUT_WORK,
       MIN_DEDUCT_WORK,
       MIN_ON_LEAVE,
       MIN_DEDUCT,
       MIN_OUT_WORK_DEDUCT,
       MIN_LATE,
       MIN_EARLY,
       MIN_LATE_EARLY,
       WORKING_X,
       WORKING_F,
       WORKING_E,
       WORKING_A,
       WORKING_H,
       WORKING_D,
       WORKING_C,
       WORKING_T,
       WORKING_Q,
       WORKING_N,
       WORKING_P,
       WORKING_L,
       WORKING_R,
       --WORKING_S,
       WORKING_B,
       WORKING_K,
       WORKING_J,
       WORKING_TS,
       WORKING_O,
       WORKING_V,
       WORKING_ADD,
       WORKING_STANDARD,
       REQUEST_ID,
       OBJECT_ATTENDANCE,
       WORKING_KLD,
       WORKING_TN)
      SELECT SEQ_AT_TIME_MONTHLY_TEMP.NEXTVAL,
             T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             T.STAFF_RANK_ID,
             T.DECISION_ID,
             T.SALARY_ID,
             T.PA_OBJECT_SALARY_ID,
             P_PERIOD_ID,
             PV_FROMDATE,
             PV_TODATE,
             MIN_IN_WORK,
             MIN_OUT_WORK,
             MIN_DEDUCT_WORK,
             MIN_ON_LEAVE,
             MIN_DEDUCT,
             MIN_OUT_WORK_DEDUCT,
             MIN_LATE,
             MIN_EARLY,
             MIN_LATE_EARLY,
             CASE
               WHEN WORKING_X = 0 THEN
                NULL
               ELSE
                WORKING_X
             END WORKING_X,
             CASE
               WHEN WORKING_F = 0 THEN
                NULL
               ELSE
                WORKING_F
             END WORKING_F,
             CASE
               WHEN WORKING_E = 0 THEN
                NULL
               ELSE
                WORKING_E
             END WORKING_E,
             CASE
               WHEN WORKING_A = 0 THEN
                NULL
               ELSE
                WORKING_A
             END WORKING_A,
             CASE
               WHEN WORKING_H = 0 THEN
                NULL
               ELSE
                WORKING_H
             END WORKING_H,
             CASE
               WHEN WORKING_D = 0 THEN
                NULL
               ELSE
                WORKING_D
             END WORKING_D,
             CASE
               WHEN WORKING_C = 0 THEN
                NULL
               ELSE
                WORKING_C
             END WORKING_C,
             CASE
               WHEN WORKING_T = 0 THEN
                NULL
               ELSE
                WORKING_T
             END WORKING_T,
             CASE
               WHEN WORKING_Q = 0 THEN
                NULL
               ELSE
                WORKING_Q
             END WORKING_Q,
             CASE
               WHEN WORKING_N = 0 THEN
                NULL
               ELSE
                WORKING_N
             END WORKING_N,
             CASE
               WHEN WORKING_P = 0 THEN
                NULL
               ELSE
                WORKING_P
             END WORKING_P,
             CASE
               WHEN WORKING_L = 0 THEN
                NULL
               ELSE
                WORKING_L
             END WORKING_L,
             CASE
               WHEN WORKING_R = 0 THEN
                NULL
               ELSE
                WORKING_R
             END WORKING_R,
             CASE
               WHEN WORKING_B = 0 THEN
                NULL
               ELSE
                WORKING_B
             END WORKING_B,
             CASE
               WHEN WORKING_K = 0 THEN
                NULL
               ELSE
                WORKING_K
             END WORKING_K,
             CASE
               WHEN WORKING_J = 0 THEN
                NULL
               ELSE
                WORKING_J
             END WORKING_J,
             CASE
               WHEN WORKING_TS = 0 THEN
                NULL
               ELSE
                WORKING_TS
             END WORKING_TS,
             CASE
               WHEN WORKING_O = 0 THEN
                NULL
               ELSE
                WORKING_O
             END WORKING_O,
             CASE
               WHEN WORKING_V = 0 THEN
                NULL
               ELSE
                WORKING_V
             END WORKING_V,
             CASE
               WHEN WORKING_ADD = 0 THEN
                NULL
               ELSE
                WORKING_ADD
             END WORKING_ADD,
             PV_WORKING_STANDARD,
             PV_REQUEST_ID,
             OBJECT_ATTENDANCE,
             CASE
               WHEN WORKING_KLD = 0 THEN
                NULL
               ELSE
                WORKING_KLD
             END WORKING_KLD,
             CASE
               WHEN WORKING_TN = 0 THEN
                NULL
               ELSE
                WORKING_TN
             END WORKING_TN
        FROM (SELECT A.EMPLOYEE_ID,
                     EE.ORG_ID,
                     EE.TITLE_ID,
                     EE.DECISION_ID,
                     EE.STAFF_RANK_ID,
                     EE.PA_OBJECT_SALARY_ID,
                     S.ID SALARY_ID,
                     SUM(NVL(MIN_IN_WORK, 0)) MIN_IN_WORK,
                     SUM(NVL(MIN_OUT_WORK, 0)) MIN_OUT_WORK,
                     SUM(NVL(MIN_DEDUCT_WORK, 0)) MIN_DEDUCT_WORK,
                     SUM(NVL(MIN_ON_LEAVE, 0)) MIN_ON_LEAVE,
                     SUM(NVL(MIN_DEDUCT, 0)) MIN_DEDUCT,
                     SUM(NVL(MIN_OUT_WORK_DEDUCT, 0)) MIN_OUT_WORK_DEDUCT,
                     SUM(NVL(MIN_LATE, 0)) MIN_LATE,
                     SUM(NVL(MIN_EARLY, 0)) MIN_EARLY,
                     SUM(NVL(MIN_LATE_EARLY, 0)) MIN_LATE_EARLY,
                     
                     /*map ky hieu AT_FML voi collumn trong AT_TIME_TIMESHEET_DAILY*/
                     SUM(CASE
                           WHEN M2.CODE = '0X' THEN
                            0
                           WHEN M2.CODE = '0.5X' THEN
                            0.5
                           WHEN M2.CODE = '0.75X' THEN
                            0.75
                           WHEN M2.CODE = 'X' THEN
                            1
                           WHEN M2.CODE = '1.5X' THEN
                            1.5
                           ELSE
                            0
                         END) WORKING_X, --==Ngay di lam
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'OFF' THEN
                            1
                           WHEN (F.CODE = 'OFF' OR F2.CODE = 'OFF') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_F, --==OFF
                     SUM(CASE
                           WHEN M2.CODE = 'L' THEN
                            1
                           ELSE
                            0
                         END) WORKING_L, --==Ngay Le Tet
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'DD' THEN
                            1
                           WHEN (F.CODE = 'DD' OR F2.CODE = 'DD') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_D, --==Ngay di duong
                     SUM(CASE
                           WHEN F.CODE = 'TS' THEN
                            1
                           ELSE
                            0
                         END) WORKING_TS, --== Nghi thai san
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'O' THEN
                            1
                           WHEN (F.CODE = 'O' OR F2.CODE = 'O') AND F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_O, --==nghi om
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'P' THEN
                            1
                           WHEN (F.CODE = 'P' OR F2.CODE = 'P') AND F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_P, --==nghi phep
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'B' THEN
                            1
                           WHEN (F.CODE = 'B' OR F2.CODE = 'B') AND F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_B, --==nghi bu
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'CT' THEN
                            1
                           WHEN (F.CODE = 'CT' OR F2.CODE = 'CT') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_C, --== cong tac
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'E' THEN
                            1
                           WHEN (F.CODE = 'E' OR F2.CODE = 'E') AND F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_E,
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'DH' THEN
                            1
                           WHEN (F.CODE = 'DH' OR F2.CODE = 'DH') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_A, --==nghi di hoc
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'VR' THEN
                            1
                           WHEN (F.CODE = 'VR' OR F2.CODE = 'VR') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_V, --==Nghi viec rieng
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'BCTS' THEN
                            1
                           WHEN (F.CODE = 'BCTS' OR F2.CODE = 'BCTS') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_H, --==Nghi bien chung thai san
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'KT' THEN
                            1
                           WHEN (F.CODE = 'KT' OR F2.CODE = 'KT') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_Q, --==Nghi kham thai
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'VS' THEN
                            1
                           WHEN (F.CODE = 'VS' OR F2.CODE = 'VS') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_N, --==Lao dong Nam nghi vo sinh
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'KH' THEN
                            1
                           WHEN (F.CODE = 'KH' OR F2.CODE = 'KH') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_R, --==Nghi ket hon
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'TG' THEN
                            1
                           WHEN (F.CODE = 'TG' OR F2.CODE = 'TG') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_T, --==Nghi tang gia
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'NKL' THEN
                            1
                           WHEN (F.CODE = 'NKL' OR F2.CODE = 'NKL') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_K, --==Nghi KHONG huong luong
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'NCL' THEN
                            1
                           WHEN (F.CODE = 'NCL' OR F2.CODE = 'NCL') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_J, --==Nghi co huong luong
                     0 WORKING_ADD,
                     A.OBJECT_ATTENDANCE,
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'KLD' THEN
                            1
                           WHEN (F.CODE = 'KLD' OR F2.CODE = 'KLD') AND
                                F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_KLD,
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'T' THEN
                            1
                           WHEN (F.CODE = 'T' OR F2.CODE = 'T') AND F.ID <> F2.ID THEN
                            0.5
                           ELSE
                            0
                         END) WORKING_TN
                FROM AT_TIME_TIMESHEET_DAILY_TEM A
               INNER JOIN (SELECT E.*
                            FROM (SELECT W.ID,
                                         W.EMPLOYEE_ID,
                                         ROW_NUMBER() OVER(PARTITION BY W.EMPLOYEE_ID ORDER BY W.EFFECT_DATE DESC, W.ID DESC) RN
                                    FROM HU_WORKING W
                                   WHERE W.STATUS_ID = 447
                                     AND W.IS_3B = 0
                                     AND W.IS_WAGE = -1
                                     AND W.EFFECT_DATE <= PV_TODATE) E
                           WHERE E.RN = 1) S
                  ON A.EMPLOYEE_ID = S.EMPLOYEE_ID
               INNER JOIN AT_CHOSEN_EMP EE
                  ON A.EMPLOYEE_ID = EE.EMPLOYEE_ID
                LEFT JOIN AT_TIME_MANUAL M
                  ON A.MANUAL_ID = M.ID
                LEFT JOIN AT_TIME_MANUAL M2
                  ON A.MANUAL_ID = M2.ID
                LEFT JOIN AT_FML F
                  ON M.MORNING_ID = F.ID
                LEFT JOIN AT_FML F2
                  ON M.AFTERNOON_ID = F2.ID
                LEFT JOIN AT_SHIFT S
                  ON A.SHIFT_ID = S.ID
               GROUP BY A.EMPLOYEE_ID,
                        EE.DECISION_ID,
                        S.ID,
                        --EE.START_DATE,
                        --EE.END_DATE,
                        EE.ORG_ID,
                        EE.TITLE_ID,
                        EE.STAFF_RANK_ID,
                        EE.PA_OBJECT_SALARY_ID,
                        A.OBJECT_ATTENDANCE) T;
  
    ---------------------------------------------------------------------------------
    -- INSERT DU LIEU VAO BANG TONG HOP CONG
  
    --------------------------------------------------------------------------------------
    -- AP DUNG CONG THUC TINH CHO CAC COT TREN BANG CONG TONG HOP
    --------------------------------------------------------------------------------------
    FOR CUR_ITEM IN (SELECT *
                       FROM AT_TIME_FORMULAR T
                      WHERE T.TYPE IN (2)
                        AND T.STATUS = 1
                      ORDER BY T.FORMULAR_ID) LOOP
    
      PV_SQL := 'UPDATE AT_TIME_MONTHLY_TEMP T SET ' ||
                CUR_ITEM.FORMULAR_CODE || '= NVL((' ||
                CUR_ITEM.FORMULAR_VALUE || '),0)';
      INSERT INTO TEMP (TEXT) VALUES (PV_SQL);
      EXECUTE IMMEDIATE PV_SQL;
    
    END LOOP;
  
    DELETE AT_TIME_TIMESHEET_MONTHLY T
     WHERE EXISTS (SELECT EMPLOYEE_ID
              FROM AT_CHOSEN_EMP O
             WHERE T.EMPLOYEE_ID = O.EMPLOYEE_ID)
       AND T.PERIOD_ID = P_PERIOD_ID;
    /* update AT_TIME_MONTHLY_TEMP t
    set t.working_da =
        (select ROUND(sum(NVL(ap.hours, 0)) / 8, 2)
           from at_project_assign ap
          where ap.employee_id = t.employee_id
            and ap.workingday between t.from_date and t.end_date);*/
    INSERT INTO AT_TIME_TIMESHEET_MONTHLY
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       DECISION_ID,
       SALARY_ID,
       DECISION_START,
       DECISION_END,
       PA_OBJECT_SALARY_ID,
       PERIOD_ID,
       FROM_DATE,
       END_DATE,
       WORKING_X,
       WORKING_F,
       WORKING_E,
       WORKING_A,
       WORKING_H,
       WORKING_D,
       WORKING_C,
       WORKING_T,
       WORKING_Q,
       WORKING_N,
       WORKING_P,
       WORKING_L,
       WORKING_R,
       WORKING_S,
       WORKING_B,
       WORKING_K,
       WORKING_J,
       WORKING_TS,
       WORKING_O,
       WORKING_V,
       WORKING_ADD,
       MIN_IN_WORK,
       MIN_OUT_WORK,
       MIN_DEDUCT_WORK,
       MIN_ON_LEAVE,
       MIN_DEDUCT,
       MIN_OUT_WORK_DEDUCT,
       MIN_LATE,
       MIN_EARLY,
       MIN_LATE_EARLY,
       WORKING_STANDARD,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG,
       OBJECT_ATTENDANCE,
       TOTAL_W_SALARY,
       TOTAL_W_NOSALARY,
       WORKING_KLD,
       WORKING_TN)
      SELECT SEQ_AT_TIME_TIMESHEET_MONTHLY.NEXTVAL,
             T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             T.STAFF_RANK_ID,
             T.DECISION_ID,
             T.SALARY_ID,
             T.DECISION_START,
             T.DECISION_END,
             T.PA_OBJECT_SALARY_ID,
             T.PERIOD_ID,
             T.FROM_DATE,
             T.END_DATE,
             T.WORKING_X,
             T.WORKING_F,
             T.WORKING_E,
             T.WORKING_A,
             T.WORKING_H,
             T.WORKING_D,
             T.WORKING_C,
             T.WORKING_T,
             T.WORKING_Q,
             T.WORKING_N,
             T.WORKING_P,
             T.WORKING_L,
             T.WORKING_R,
             T.WORKING_S,
             T.WORKING_B,
             T.WORKING_K,
             T.WORKING_J,
             T.WORKING_TS,
             T.WORKING_O,
             T.WORKING_V,
             T.WORKING_ADD,
             T.MIN_IN_WORK,
             T.MIN_OUT_WORK,
             T.MIN_DEDUCT_WORK,
             T.MIN_ON_LEAVE,
             T.MIN_DEDUCT,
             T.MIN_OUT_WORK_DEDUCT,
             T.MIN_LATE,
             T.MIN_EARLY,
             T.MIN_LATE_EARLY,
             /*P.PERIOD_STANDARD,*/
             T.WORKING_STANDARD,
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME),
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME),
             T.OBJECT_ATTENDANCE,
             T.TOTAL_W_SALARY,
             T.TOTAL_W_NOSALARY,
             T.WORKING_KLD,
             T.WORKING_TN
        FROM AT_TIME_MONTHLY_TEMP T
       INNER JOIN AT_PERIOD P
          ON T.PERIOD_ID = P.ID;
  
    DELETE AT_TIME_TIMESHEET_DAILY E
     WHERE E.WORKINGDAY >= PV_FROMDATE
       AND E.WORKINGDAY <= PV_TODATE
       AND E.EMPLOYEE_ID NOT IN
           (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);
  
    /* DELETE AT_TIME_TIMESHEET_ORIGIN E
    WHERE E.WORKINGDAY >= PV_FROMDATE
      AND E.WORKINGDAY <= PV_TODATE
      AND E.EMPLOYEE_ID NOT IN
          (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);*/
  
    /*DELETE AT_TIME_TIMESHEET_MONTHLY E
    WHERE E.PERIOD_ID = P_PERIOD_ID
      AND E.EMPLOYEE_ID NOT IN
          (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);*/
    COMMIT;
    CALL_ENTITLEMENT_HOSE(P_USERNAME, P_ORG_ID, P_PERIOD_ID, P_ISDISSOLVE);
    COMMIT;
    CALL_ENTITLEMENT_NB(P_USERNAME, P_ORG_ID, P_PERIOD_ID, P_ISDISSOLVE);
    COMMIT;
    /*INSERT_INS_CHANGE(P_USERNAME, P_ORG_ID, P_PERIOD_ID, P_ISDISSOLVE);*/
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_MONTHLY',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              P_ORG_ID,
                              P_PERIOD_ID,
                              P_USERNAME,
                              PV_REQUEST_ID,
                              PV_SQL);
  END;

  PROCEDURE CALL_ENTITLEMENT(P_USERNAME   VARCHAR2,
                             P_ORG_ID     IN NUMBER,
                             P_PERIOD_ID  IN NUMBER,
                             P_ISDISSOLVE IN NUMBER) IS
    P_CAL_DATE     DATE;
    PV_FROMDATE    DATE;
    PV_ENDDATE     DATE;
    PV_REQUEST_ID  NUMBER;
    PV_YEAR        NUMBER;
    PV_SQL         NVARCHAR2(1000);
    PV_TOTAL_P     NUMBER;
    PV_RESET_MONTH NUMBER;
    PV_MONTH       NUMBER;
    PV_MONTH_P     NUMBER;
    PV_YEAR_TN     NUMBER;
    PV_DAY_TN      NUMBER;
    PV_I           NUMBER;
    PV_STRING      VARCHAR2(2000);
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
    SELECT P.START_DATE,
           P.END_DATE,
           P.END_DATE,
           EXTRACT(YEAR FROM P.END_DATE),
           p.month
      INTO PV_FROMDATE, PV_ENDDATE, P_CAL_DATE, PV_YEAR, PV_MONTH
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
  
    SELECT t.year_p,
           T.TO_LEAVE_YEAR,
           ROUND(to_number(t.year_p) / 12, 2),
           T.YEAR_TN,
           T.DAY_TN
      INTO PV_TOTAL_P, PV_RESET_MONTH, PV_MONTH_P, PV_YEAR_TN, PV_DAY_TN
      FROM (select t.year_p,
                   TO_CHAR(T.TO_LEAVE_YEAR, 'MM') TO_LEAVE_YEAR,
                   T.YEAR_TN,
                   T.DAY_TN
              from AT_LIST_PARAM_SYSTEM t
             WHERE TO_CHAR(T.EFFECT_DATE_FROM, 'yyyy') <= PV_YEAR
               AND T.ACTFLG = 'A'
             ORDER BY T.EFFECT_DATE_FROM DESC) T
     WHERE ROWNUM = 1;
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
  
    INSERT INTO AT_CHOSEN_EMP
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       TER_EFFECT_DATE,
       USERNAME,
       REQUEST_ID,
       JOIN_DATE,
       JOIN_DATE_STATE)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              CASE
                WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                 T.TER_EFFECT_DATE + 1
                ELSE
                 NULL
              END TER_EFFECT_DATE,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              T.JOIN_DATE,
              T.JOIN_DATE_STATE
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_ENDDATE
                      AND E.STATUS_ID = 447
                      AND E.IS_3B = 0) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.START_DATE DESC) AS ROW_NUMBER
                     FROM HU_CONTRACT E
                    INNER JOIN HU_CONTRACT_TYPE S
                       ON S.ID = E.CONTRACT_TYPE_ID
                    INNER JOIN OT_OTHER_LIST O
                       ON O.ID = S.TYPE_ID
                      AND O.CODE <> 'HDTV'
                    WHERE E.START_DATE <= PV_ENDDATE
                      AND E.STATUS_ID = 447) CT
           ON T.ID = CT.EMPLOYEE_ID
          AND CT.ROW_NUMBER = 1
        INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
           ON O.ORG_ID = W.ORG_ID
        WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  
    DELETE FROM AT_ENTITLEMENT T
     WHERE EXISTS (SELECT a.ID
              FROM AT_ENTITLEMENT a
             INNER JOIN AT_CHOSEN_EMP EMP
                ON EMP.EMPLOYEE_ID = a.EMPLOYEE_ID
             WHERE T.EMPLOYEE_ID = EMP.EMPLOYEE_ID
               AND a.YEAR = PV_YEAR
               AND a.MONTH = PV_MONTH)
       and t.YEAR = PV_YEAR
       AND t.MONTH = PV_MONTH;
  
    INSERT INTO AT_ENTITLEMENT T
      (ID,
       EMPLOYEE_ID,
       YEAR,
       MONTH,
       PERIOD_ID,
       WORKING_TIME_HAVE,
       TOTAL_HAVE,
       CREATED_BY,
       CREATED_DATE,
       PREV_HAVE,
       PREV_USED,
       EXPIREDATE,
       CUR_HAVE,
       CUR_USED,
       FUND,
       SPECIAL,
       CUR_HAVE1,
       CUR_HAVE2,
       CUR_HAVE3,
       CUR_HAVE4,
       CUR_HAVE5,
       CUR_HAVE6,
       CUR_HAVE7,
       CUR_HAVE8,
       CUR_HAVE9,
       CUR_HAVE10,
       CUR_HAVE11,
       CUR_HAVE12,
       CUR_USED1,
       CUR_USED2,
       CUR_USED3,
       CUR_USED4,
       CUR_USED5,
       CUR_USED6,
       CUR_USED7,
       CUR_USED8,
       CUR_USED9,
       CUR_USED10,
       CUR_USED11,
       CUR_USED12,
       AL_TO_CASH,
       REASON,
       EXPIREDATE_NB,
       FIXADD,
       PAY_PREV_ENT,
       AL_T1,
       AL_T2,
       AL_T3,
       AL_T4,
       AL_T5,
       AL_T6,
       AL_T7,
       AL_T8,
       AL_T9,
       AL_T10,
       AL_T11,
       AL_T12,
       AL_ADD_T1,
       AL_ADD_T2,
       AL_ADD_T3,
       AL_ADD_T4,
       AL_ADD_T5,
       AL_ADD_T6,
       AL_ADD_T7,
       AL_ADD_T8,
       AL_ADD_T9,
       AL_ADD_T10,
       AL_ADD_T11,
       AL_ADD_T12,
       PREV_USED1,
       PREV_USED2,
       PREV_USED3,
       PREV_USED4,
       PREV_USED5,
       PREV_USED6,
       PREV_USED7,
       PREV_USED8,
       PREV_USED9,
       PREV_USED10,
       PREV_USED11,
       PREV_USED12,
       TOTAL_PAY_PREV_ENT,
       SUB_PREV_ENT,
       BALANCE_WORKING_TIME_3B,
       SENIORITYHAVE,
       TOTAL_HAVE1)
      SELECT SEQ_AT_ENTITLEMENT.NEXTVAL,
             EMP.EMPLOYEE_ID,
             PV_YEAR,
             PV_MONTH,
             P_PERIOD_ID,
             EXTRACT(YEAR FROM EMP.JOIN_DATE),
             CASE
               WHEN PV_YEAR - EXTRACT(YEAR FROM EMP.JOIN_DATE) = 0 THEN
                PV_TOTAL_P - EXTRACT(MONTH FROM EMP.JOIN_DATE) + 1 +
                FN_CALL_TN(emp.employee_id,
                           PV_ENDDATE,
                           PV_YEAR_TN,
                           PV_DAY_TN)
               ELSE
                PV_TOTAL_P + FN_CALL_TN(emp.employee_id,
                                        PV_ENDDATE,
                                        PV_YEAR_TN,
                                        PV_DAY_TN)
             END,
             UPPER(P_USERNAME),
             SYSDATE,
             PREV_HAVE,
             PREV_USED,
             EXPIREDATE,
             CUR_HAVE,
             CUR_USED,
             FUND,
             SPECIAL,
             CUR_HAVE1,
             CUR_HAVE2,
             CUR_HAVE3,
             CUR_HAVE4,
             CUR_HAVE5,
             CUR_HAVE6,
             CUR_HAVE7,
             CUR_HAVE8,
             CUR_HAVE9,
             CUR_HAVE10,
             CUR_HAVE11,
             CUR_HAVE12,
             CUR_USED1,
             CUR_USED2,
             CUR_USED3,
             CUR_USED4,
             CUR_USED5,
             CUR_USED6,
             CUR_USED7,
             CUR_USED8,
             CUR_USED9,
             CUR_USED10,
             CUR_USED11,
             CUR_USED12,
             AL_TO_CASH,
             REASON,
             EXPIREDATE_NB,
             FIXADD,
             PAY_PREV_ENT,
             AL_T1,
             AL_T2,
             AL_T3,
             AL_T4,
             AL_T5,
             AL_T6,
             AL_T7,
             AL_T8,
             AL_T9,
             AL_T10,
             AL_T11,
             AL_T12,
             AL_ADD_T1,
             AL_ADD_T2,
             AL_ADD_T3,
             AL_ADD_T4,
             AL_ADD_T5,
             AL_ADD_T6,
             AL_ADD_T7,
             AL_ADD_T8,
             AL_ADD_T9,
             AL_ADD_T10,
             AL_ADD_T11,
             AL_ADD_T12,
             PREV_USED1,
             PREV_USED2,
             PREV_USED3,
             PREV_USED4,
             PREV_USED5,
             PREV_USED6,
             PREV_USED7,
             PREV_USED8,
             PREV_USED9,
             PREV_USED10,
             PREV_USED11,
             PREV_USED12,
             TOTAL_PAY_PREV_ENT,
             SUB_PREV_ENT,
             BALANCE_WORKING_TIME_3B,
             SENIORITYHAVE,
             PV_TOTAL_P
        FROM AT_CHOSEN_EMP EMP
        LEFT JOIN AT_ENTITLEMENT A
          ON A.EMPLOYEE_ID = EMP.EMPLOYEE_ID
         AND A.YEAR = PV_YEAR
         AND A.MONTH = PV_MONTH - 1;
  
    --Update so phep tu bang cong thang qua
    PV_SQL := 'UPDATE AT_ENTITLEMENT ENT SET ENT.CUR_USED' ||
              EXTRACT(MONTH FROM PV_ENDDATE) ||
              ' = NVL((SELECT T.WORKING_P FROM AT_TIME_TIMESHEET_MONTHLY T
                   WHERE T.PERIOD_ID = ' || P_PERIOD_ID ||
              ' AND T.EMPLOYEE_ID = ENT.EMPLOYEE_ID),0)
                WHERE ENT.YEAR = ' || PV_YEAR || '
                AND ENT.MONTH = ' || PV_MONTH || '
                    AND EXISTS(SELECT EMP.EMPLOYEE_ID FROM AT_CHOSEN_EMP EMP
                                WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID)';
    /*INSERT INTO AT_STRSQL(ID,STRINGSQL)
    VALUES(PV_REQUEST_ID,PV_SQL); */
    EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
  
    FOR PV_I IN 1 .. PV_RESET_MONTH LOOP
      IF PV_STRING IS NOT NULL THEN
        PV_STRING := PV_STRING || ' + ' || 'NVL(A.CUR_USED' || PV_I ||
                     ',0)';
      ELSE
        PV_STRING := 'NVL(A.CUR_USED' || PV_I || ',0)';
      END IF;
    END LOOP;
    -- Tinh so ngay nghi phep tu dau nam den ngay reset p nam truoc
    PV_SQL := 'UPDATE AT_ENTITLEMENT A
     SET A.PREV_USED =  ' || PV_STRING || '
     WHERE A.YEAR =  ' || PV_YEAR || '
       AND A.MONTH = ' || PV_MONTH || '
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP EMP
             WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID)';
    EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
  
    -- Tinh so phep nam truoc con lai
  
    MERGE INTO AT_ENTITLEMENT A
    USING (SELECT ENT1.EMPLOYEE_ID, ENT1.CUR_HAVE
             FROM AT_ENTITLEMENT ENT1
            WHERE ENT1.YEAR = PV_YEAR - 1
              AND ENT1.MONTH = 12
              AND EXISTS
            (SELECT EMP.EMPLOYEE_ID
                     FROM AT_CHOSEN_EMP EMP
                    WHERE EMP.EMPLOYEE_ID = ENT1.EMPLOYEE_ID)) B
    ON (a.EMPLOYEE_ID = b.EMPLOYEE_ID)
    WHEN MATCHED THEN
      UPDATE
         SET A.PREV_HAVE = NVL(B.CUR_HAVE, 0)
       WHERE A.YEAR = PV_YEAR
         AND A.MONTH = PV_MONTH
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID);
  
    -- TINH LAI SO PHEP NAM TRUOC DA NGHI
    UPDATE AT_ENTITLEMENT A
       SET A.PREV_USED = CASE
                           WHEN A.PREV_HAVE = 0 THEN
                            0
                           WHEN A.PREV_HAVE < A.PREV_USED THEN
                            A.PREV_HAVE
                           ELSE
                            A.PREV_USED
                         END
     WHERE A.YEAR = PV_YEAR
       AND A.MONTH = PV_MONTH
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP EMP
             WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID);
  
    -- Tinh so phep nam truoc con lai su dung v? duoc reset vao theo thang thiet lap
  
    IF PV_MONTH > PV_RESET_MONTH THEN
      UPDATE AT_ENTITLEMENT A
         SET A.PREVTOTAL_HAVE = 0
       WHERE A.YEAR = PV_YEAR
         AND A.MONTH = PV_MONTH
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID);
    ELSE
      UPDATE AT_ENTITLEMENT A
         SET A.PREVTOTAL_HAVE = NVL(A.PREV_HAVE, 0) - NVL(A.PREV_USED, 0)
       WHERE A.YEAR = PV_YEAR
         AND A.MONTH = PV_MONTH
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID);
    END IF;
  
    --Tinh toan lai so phep da sd
    UPDATE AT_ENTITLEMENT ENT
       SET ENT.CUR_USED      = NVL(ENT.CUR_USED1, 0) + NVL(ENT.CUR_USED2, 0) +
                               NVL(ENT.CUR_USED3, 0) + NVL(ENT.CUR_USED4, 0) +
                               NVL(ENT.CUR_USED5, 0) + NVL(ENT.CUR_USED6, 0) +
                               NVL(ENT.CUR_USED7, 0) + NVL(ENT.CUR_USED8, 0) +
                               NVL(ENT.CUR_USED9, 0) +
                               NVL(ENT.CUR_USED10, 0) +
                               NVL(ENT.CUR_USED11, 0) +
                               NVL(ENT.CUR_USED12, 0) -
                               NVL(ENT.PREV_USED, 0),
           ENT.SENIORITYHAVE = FN_CALL_TN(ENT.EMPLOYEE_ID,
                                          PV_ENDDATE,
                                          PV_YEAR_TN,
                                          PV_DAY_TN)
     WHERE ENT.YEAR = PV_YEAR
       AND ENT.MONTH = PV_MONTH
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP EMP
             WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID);
  
    --Tinh toan lai so phep con lai
    IF PV_RESET_MONTH >= PV_MONTH THEN
      UPDATE AT_ENTITLEMENT ENT
         SET ENT.CUR_HAVE = NVL(ENT.PREV_HAVE, 0) + NVL(ENT.TOTAL_HAVE, 0) -
                            NVL(ENT.CUR_USED, 0) - NVL(ENT.PREV_USED, 0)
       WHERE ENT.YEAR = PV_YEAR
         AND ENT.MONTH = PV_MONTH
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID);
    ELSE
      UPDATE AT_ENTITLEMENT ENT
         SET ENT.CUR_HAVE = NVL(ENT.TOTAL_HAVE, 0) - NVL(ENT.CUR_USED, 0)
       WHERE ENT.YEAR = PV_YEAR
         AND ENT.MONTH = PV_MONTH
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID);
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              null,
                              null,
                              null,
                              null);
    
  END;

  FUNCTION FN_CALL_TN(P_EMPLOYEE_ID IN NUMBER,
                      P_ENDDATE     IN DATE,
                      P_YEAR_TN     NUMBER,
                      P_DAY_TN      NUMBER) RETURN NUMBER AS
    PRAGMA AUTONOMOUS_TRANSACTION;
    RTNVALUE     NUMBER;
    PV_MONTH_TN  NUMBER;
    PV_JOIN_DATE DATE;
  BEGIN
  
    SELECT E.JOIN_DATE
      INTO PV_JOIN_DATE
      FROM HU_EMPLOYEE E
     WHERE E.ID = P_EMPLOYEE_ID;
  
    SELECT ROUND(MONTHS_BETWEEN(P_ENDDATE + 1, PV_JOIN_DATE) / 12, 2)
      INTO PV_MONTH_TN
      FROM DUAL;
  
    SELECT CASE
             WHEN PV_MONTH_TN < P_YEAR_TN THEN
              0
             WHEN PV_MONTH_TN >= P_YEAR_TN AND PV_MONTH_TN < P_YEAR_TN * 2 THEN
              P_DAY_TN
             WHEN PV_MONTH_TN >= P_YEAR_TN * 2 AND
                  PV_MONTH_TN < P_YEAR_TN * 3 THEN
              P_DAY_TN * 2
             WHEN PV_MONTH_TN >= P_YEAR_TN * 3 AND
                  PV_MONTH_TN < P_YEAR_TN * 4 THEN
              P_DAY_TN * 3
             WHEN PV_MONTH_TN >= P_YEAR_TN * 4 AND
                  PV_MONTH_TN < P_YEAR_TN * 5 THEN
              P_DAY_TN * 4
             WHEN PV_MONTH_TN >= P_YEAR_TN * 5 AND
                  PV_MONTH_TN < P_YEAR_TN * 6 THEN
              P_DAY_TN * 5
             WHEN PV_MONTH_TN >= P_YEAR_TN * 6 AND
                  PV_MONTH_TN < P_YEAR_TN * 7 THEN
              P_DAY_TN * 6
             WHEN PV_MONTH_TN >= P_YEAR_TN * 7 AND
                  PV_MONTH_TN < P_YEAR_TN * 8 THEN
              P_DAY_TN * 7
           END
      INTO RTNVALUE
      FROM DUAL;
  
    RETURN RTNVALUE;
  EXCEPTION
    WHEN OTHERS THEN
      RETURN 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.FN_CALL_TN',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                              NULL,
                              NULL);
    
  END FN_CALL_TN;
  PROCEDURE GETSIGNDEFAULT(P_USERNAME   IN NVARCHAR2,
                           P_ORG_ID     IN NUMBER,
                           P_PERIOD_ID  IN NUMBER,
                           P_ISDISSOLVE IN NUMBER,
                           P_FROMDATE   IN DATE,
                           P_ENDATE     IN DATE,
                           P_CUR        OUT CURSOR_TYPE) IS
    PV_FROMDATE   DATE;
    PV_ENDDATE    DATE;
    PV_REQUEST_ID NUMBER;
    PV_SQL        CLOB;
    PV_TBL        VARCHAR2(50);
    PV_TBL_ORG    VARCHAR2(50);
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    PV_FROMDATE   := P_FROMDATE;
    PV_ENDDATE    := P_ENDATE;
    PV_TBL        := TRIM('AT_CHOSEN_EMP_' || TRIM(TO_CHAR(PV_REQUEST_ID)));
    PV_TBL_ORG    := TRIM('AT_CHOSEN_ORG_' || TRIM(TO_CHAR(PV_REQUEST_ID)));
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O
       /*WHERE EXISTS (SELECT 1
        FROM AT_ORG_PERIOD OP
       WHERE OP.PERIOD_ID = P_PERIOD_ID
         AND OP.ORG_ID = O.ORG_ID
         AND OP.STATUSCOLEX = 1)*/
       );
  
    -- insert emp can tinh toan
    INSERT INTO AT_CHOSEN_EMP
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       USERNAME,
       REQUEST_ID,
       JOIN_DATE,
       WORK_STATUS)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              T.JOIN_DATE,
              T.WORK_STATUS
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_ENDDATE
                      AND E.STATUS_ID = 447
                      AND E.IS_MISSION = -1) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN AT_CHOSEN_ORG O
           ON O.ORG_ID = W.ORG_ID
          AND O.REQUEST_ID = PV_REQUEST_ID
        WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE))
          AND T.ID NOT IN
              (SELECT A.EMPLOYEE_ID
                 FROM AT_WORKSIGN A
                WHERE A.WORKINGDAY BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE)
                  AND NVL(A.IS_IMPORT, 0) = 1
                GROUP BY A.EMPLOYEE_ID));
    -- XOA DU LIEU DA TON TAI
    DELETE FROM AT_WORKSIGN T1
     WHERE EXISTS
     (SELECT 1
              FROM (SELECT T.EMPLOYEE_ID,
                           CASE
                             WHEN T.EFFECT_DATE_FROM > PV_FROMDATE THEN
                              T.EFFECT_DATE_FROM
                             ELSE
                              PV_FROMDATE
                           END START_DELETE,
                           CASE
                             WHEN T.EFFECT_DATE_TO < PV_ENDDATE THEN
                              T.EFFECT_DATE_TO
                             ELSE
                              PV_ENDDATE
                           END END_DELETE
                      FROM AT_SIGNDEFAULT T
                     INNER JOIN AT_CHOSEN_EMP EE
                        ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
                       AND EE.REQUEST_ID = PV_REQUEST_ID
                     WHERE TRUNC(T.EFFECT_DATE_FROM) <= TRUNC(PV_ENDDATE)
                       AND NVL(TRUNC(T.EFFECT_DATE_TO), TRUNC(PV_FROMDATE)) >=
                           TRUNC(PV_FROMDATE)) E
             WHERE T1.EMPLOYEE_ID = E.EMPLOYEE_ID
               AND T1.WORKINGDAY >= E.START_DELETE
               AND T1.WORKINGDAY <= E.END_DELETE);
  
    --INSERT CA MAC DINH
    INSERT INTO AT_WORKSIGN T
      (T.ID,
       T.EMPLOYEE_ID,
       T.WORKINGDAY,
       T.SHIFT_ID,
       T.PERIOD_ID,
       T.CREATED_DATE,
       T.CREATED_BY,
       T.CREATED_LOG)
      SELECT SEQ_AT_WORKSIGN.NEXTVAL,
             EE.EMPLOYEE_ID,
             C.CDATE,
             CASE
               WHEN C.CDATE IN
                    (SELECT H.WORKINGDAY
                       FROM AT_HOLIDAY H
                      WHERE H.YEAR = TO_CHAR(TRUNC(PV_ENDDATE), 'RRRR')) THEN
                (SELECT ID FROM AT_SHIFT WHERE CODE = 'L')
             
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 6 THEN
                EE.SUNDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 5 THEN
                EE.SATURDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 4 THEN
                EE.FRIDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 3 THEN
                EE.THURSDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 2 THEN
                EE.WEDNESDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 1 THEN
                EE.TUEDAY
               ELSE
                EE.MONDAY
             END SHIFT_ID,
             P_PERIOD_ID,
             SYSDATE,
             'AUTO', --==BAT BUOC DE AUTO DE PHAN BIET IMPORT
             UPPER(P_USERNAME)
        FROM (SELECT T.EMPLOYEE_ID,
                     CASE
                       WHEN T.EFFECT_DATE_FROM > PV_FROMDATE THEN
                        T.EFFECT_DATE_FROM
                       ELSE
                        PV_FROMDATE
                     END START_DELETE,
                     CASE
                       WHEN T.EFFECT_DATE_TO < PV_ENDDATE THEN
                        T.EFFECT_DATE_TO
                       ELSE
                        PV_ENDDATE
                     END END_DELETE,
                     NVL(T.SINGDEFAULE, 0) MONDAY,
                     NVL(T.SIGN_TUE, 0) TUEDAY,
                     NVL(T.SIGN_WED, 0) WEDNESDAY,
                     NVL(T.SIGN_THU, 0) THURSDAY,
                     NVL(T.SIGN_FRI, 0) FRIDAY,
                     NVL(T.SING_SAT, 0) SATURDAY,
                     NVL(T.SING_SUN, 0) SUNDAY,
                     EE.JOIN_DATE,
                     EE.WORK_STATUS                     
                FROM AT_SIGNDEFAULT T
               INNER JOIN AT_CHOSEN_EMP EE
                  ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
                 AND EE.REQUEST_ID = PV_REQUEST_ID
               WHERE T.ACTFLG = 'A'
                 AND TRUNC(T.EFFECT_DATE_FROM) <= TRUNC(PV_ENDDATE)
                 AND NVL(T.EFFECT_DATE_TO, PV_FROMDATE) >= PV_FROMDATE) EE
       CROSS JOIN TABLE(TABLE_LISTDATE(PV_FROMDATE, PV_ENDDATE)) C
        LEFT JOIN HU_TERMINATE R
          ON EE.EMPLOYEE_ID = R.EMPLOYEE_ID
       WHERE (NVL(EE.WORK_STATUS,0) = 258 OR 
             ((NVL(EE.WORK_STATUS,0) = 257 OR NVL(EE.WORK_STATUS,0) = 256) 
             AND TRUNC(R.TER_DATE) BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE)))
         AND C.CDATE >= EE.START_DELETE
         AND C.CDATE <= EE.END_DELETE
         AND (C.CDATE <= R.LAST_DATE OR R.LAST_DATE IS NULL)
         AND TRUNC(C.CDATE) >= TRUNC(EE.JOIN_DATE);
    /* AND NOT EXISTS
    (SELECT H.ID FROM AT_HOLIDAY H WHERE H.WORKINGDAY = C.CDATE)*/
    --INSERT CA MAC DINH THEO CO CAU
    -- Insert org can tinh toan
  
    PV_SQL := 'CREATE TABLE ' || PV_TBL_ORG ||
              ' AS (
    SELECT O.ORG_ID, UPPER(''' || P_USERNAME ||
              ''') USERNAME, ' || PV_REQUEST_ID ||
              ' REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(''' || P_USERNAME ||
              '''),' || P_ORG_ID || ',' || P_ISDISSOLVE || ')) O
       
    )';
  
   /* INSERT INTO AT_STRSQL
      (ID, STRINGSQL)
    VALUES
      (SEQ_AT_STRSQL.NEXTVAL, PV_SQL);*/
    EXECUTE IMMEDIATE PV_SQL;
  
    PV_SQL := 'CREATE TABLE ' || PV_TBL || ' AS (
  SELECT T.ID EMPLOYEE_ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              UPPER(''' || P_USERNAME || ''') USERNAME,
              ' || PV_REQUEST_ID ||
              ' REQUEST_ID,
              T.JOIN_DATE,
              NVL(T.ORG_ID, 0) E_ORG_ID,
              NVL(T.OBJECT_EMPLOYEE_ID, 0) E_OBJECT_EMPLOYEE_ID,
              NVL(T.OBJECT_ATTENDANT_ID, 0) E_OBJECT_ATTENDANT_ID,
              NVL(T.WORK_PLACE_ID, 0) E_WORK_PLACE_ID,
              (SELECT TB.ID
               FROM (SELECT SO.ID,
                             CASE
                               WHEN NVL(SO.ORG_ID, 0) = 0 THEN
                                0
                               ELSE
                                1
                             END + CASE
                               WHEN NVL(SO.OBJ_EMPLOYEE_ID, 0) = 0 THEN
                                0
                               ELSE
                                1
                             END + CASE
                               WHEN NVL(SO.OBJ_ATTENDANT_ID, 0) = 0 THEN
                                0
                               ELSE
                                1
                             END + CASE
                               WHEN NVL(SO.WORKPLACE_ID, 0) = 0 THEN
                                0
                               ELSE
                                1
                             END CC,
                             NVL(LENGTH(O.HIERARCHICAL_PATH), 0) AS CCC,
                     ROW_NUMBER() OVER(ORDER BY NVL(SO.ORG_ID,0) DESC, NVL(SO.OBJ_EMPLOYEE_ID,0) DESC, NVL(SO.OBJ_ATTENDANT_ID,0) DESC, NVL(SO.WORKPLACE_ID,0) DESC, SO.FROMDATE_EFFECT DESC) ID_CO_CAU
                     FROM AT_SIGNDEFAULT_ORG SO
                     LEFT JOIN HU_ORGANIZATION O
                       ON O.ID = SO.ORG_ID
                     WHERE (
                         ((NVL(SO.ORG_ID, 0) = 0) OR (W.ORG_ID IN O.ORG_ID)) AND
                         ((NVL(SO.OBJ_EMPLOYEE_ID, 0) = 0) OR
                         (SO.OBJ_EMPLOYEE_ID = W.OBJECT_EMPLOYEE_ID)) AND
                         ((NVL(SO.OBJ_ATTENDANT_ID, 0) = 0) OR
                         (SO.OBJ_ATTENDANT_ID = W.OBJECT_ATTENDANT_ID)) AND
                         ((NVL(SO.WORKPLACE_ID, 0) = 0) OR
                         (SO.WORKPLACE_ID = W.WORK_PLACE_ID))
                         )                     
                     ORDER BY CC DESC, CCC DESC
                    ) TB
               WHERE TB.ID_CO_CAU = 1) ID_CO_CAU,
               T.WORK_STATUS
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          E.OBJECT_EMPLOYEE_ID,
                          E.OBJECT_ATTENDANT_ID,
                          E.WORK_PLACE_ID,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= ''' || PV_ENDDATE || '''
                      AND E.STATUS_ID = 447
                      AND E.IS_MISSION = -1) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN ' || PV_TBL_ORG || ' O
           ON O.ORG_ID = W.ORG_ID
           AND O.REQUEST_ID = ' || PV_REQUEST_ID || '
        WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= ''' ||
              PV_FROMDATE ||
              '''))
          AND T.ID NOT IN
              (SELECT A.EMPLOYEE_ID
               FROM AT_WORKSIGN A
               WHERE A.WORKINGDAY BETWEEN ''' ||
               PV_FROMDATE || ''' AND ''' || PV_ENDDATE || '''
                AND NVL(A.IS_IMPORT, 0) = 1               
                ) 
          AND T.ID NOT IN
              (SELECT A.EMPLOYEE_ID
                 FROM AT_SIGNDEFAULT A
                --WHERE A.WORKINGDAY BETWEEN ''' ||
              PV_FROMDATE || ''' AND ''' || PV_ENDDATE || '''
                  --AND NVL(A.IS_IMPORT, 0) = 0
                --GROUP BY A.EMPLOYEE_ID
                )             
       )';
    /*INSERT INTO AT_STRSQL
      (ID, STRINGSQL)
    VALUES
      (SEQ_AT_STRSQL.NEXTVAL, PV_SQL);*/
    EXECUTE IMMEDIATE PV_SQL;
    -- XOA DU LIEU DA TON TAI
    PV_SQL := ' DELETE FROM AT_WORKSIGN T1
     WHERE EXISTS
     (SELECT 1
              FROM (SELECT EE.EMPLOYEE_ID,
                     CASE
                       WHEN T.FROMDATE_EFFECT > ''' ||
              PV_FROMDATE || ''' THEN
                        T.FROMDATE_EFFECT
                       ELSE
                        TO_DATE(''' || PV_FROMDATE ||
              ''',''DD/MM/RRRR'')
                     END START_DELETE,
                     CASE
                       WHEN T.TODATE_EFFECT < ''' ||
              PV_ENDDATE || ''' THEN
                        T.TODATE_EFFECT
                       ELSE
                        TO_DATE(''' || PV_ENDDATE ||
              ''',''DD/MM/RRRR'')
                     END END_DELETE
                      FROM AT_SIGNDEFAULT_ORG T
               INNER JOIN ' || PV_TBL || ' EE
                  ON T.ID = EE.ID_CO_CAU
                 AND EE.REQUEST_ID = ' || PV_REQUEST_ID || '
               WHERE TRUNC(T.FROMDATE_EFFECT) <= TRUNC(TO_DATE(''' ||
              PV_ENDDATE || ''',''DD/MM/RRRR''))
                 AND NVL(T.TODATE_EFFECT, ''' || PV_FROMDATE ||
              ''') >= ''' || PV_FROMDATE ||
              ''') E
             WHERE T1.EMPLOYEE_ID = E.EMPLOYEE_ID
               AND T1.WORKINGDAY >= E.START_DELETE
               AND T1.WORKINGDAY <= E.END_DELETE)';
    /*INSERT INTO AT_STRSQL
      (ID, STRINGSQL)
    VALUES
      (SEQ_AT_STRSQL.NEXTVAL, PV_SQL);*/
  
    EXECUTE IMMEDIATE PV_SQL;
    PV_SQL := ' INSERT INTO AT_WORKSIGN T
      (T.ID,
       T.EMPLOYEE_ID,
       T.WORKINGDAY,
       T.SHIFT_ID,
       T.PERIOD_ID,
       T.CREATED_DATE,
       T.CREATED_BY,
       T.CREATED_LOG)
      SELECT SEQ_AT_WORKSIGN.NEXTVAL,
             EE.EMPLOYEE_ID,
             C.CDATE,
             CASE
               WHEN C.CDATE IN
                    (SELECT H.WORKINGDAY
                       FROM AT_HOLIDAY H
                      WHERE H.YEAR = EXTRACT( YEAR FROM TO_DATE(''' ||
              PV_ENDDATE || ''',''DD-MM-RRRR''))) THEN
                (SELECT ID FROM AT_SHIFT WHERE CODE = ''L'')
             
               WHEN MOD(TO_CHAR(C.CDATE, ''J''), 7) = 6 THEN
                EE.SUNDAY
               WHEN MOD(TO_CHAR(C.CDATE, ''J''), 7) = 5 THEN
                EE.SATURDAY
               WHEN MOD(TO_CHAR(C.CDATE, ''J''), 7) = 4 THEN
                EE.FRIDAY
               WHEN MOD(TO_CHAR(C.CDATE, ''J''), 7) = 3 THEN
                EE.THURSDAY
               WHEN MOD(TO_CHAR(C.CDATE, ''J''), 7) = 2 THEN
                EE.WEDNESDAY
               WHEN MOD(TO_CHAR(C.CDATE, ''J''), 7) = 1 THEN
                EE.TUEDAY
               ELSE
                EE.MONDAY
             END SHIFT_ID,
             ' || P_PERIOD_ID || ' PERIOD_ID,
             SYSDATE CREATED_DATE,
             ''AUTO'', --==BAT BUOC DE AUTO DE PHAN BIET IMPORT
             UPPER(''' || P_USERNAME || ''')
        FROM (SELECT EE.EMPLOYEE_ID,
                     CASE
                       WHEN T.FROMDATE_EFFECT > ''' ||
              PV_FROMDATE || ''' THEN
                        T.FROMDATE_EFFECT
                       ELSE
                        TO_DATE(''' || PV_FROMDATE ||
              ''',''DD/MM/RRRR'')
                     END START_DELETE,
                     CASE
                       WHEN T.TODATE_EFFECT < ''' ||
              PV_ENDDATE || ''' THEN
                        T.TODATE_EFFECT
                       ELSE
                        TO_DATE(''' || PV_ENDDATE ||
              ''',''DD/MM/RRRR'')
                     END END_DELETE,
                     NVL(T.SIGNID_MON, 0) MONDAY,
                     NVL(T.SIGNID_TUE, 0) TUEDAY,
                     NVL(T.SIGNID_WED, 0) WEDNESDAY,
                     NVL(T.SIGNID_THU, 0) THURSDAY,
                     NVL(T.SIGNID_FRI, 0) FRIDAY,
                     NVL(T.SIGNID_SAT, 0) SATURDAY,
                     NVL(T.SIGNID_SUN, 0) SUNDAY,
                     EE.JOIN_DATE,
                     EE.WORK_STATUS
                FROM AT_SIGNDEFAULT_ORG T
               INNER JOIN ' || PV_TBL || ' EE
                  ON T.ID = EE.ID_CO_CAU
                 AND EE.REQUEST_ID = ' || PV_REQUEST_ID || '
               WHERE TRUNC(T.FROMDATE_EFFECT) <= TRUNC(TO_DATE(''' ||
              PV_ENDDATE || ''',''DD/MM/RRRR''))
                 AND NVL(T.TODATE_EFFECT, ''' || PV_FROMDATE ||
              ''') >= ''' || PV_FROMDATE ||
              ''') EE
       CROSS JOIN TABLE(TABLE_LISTDATE(''' || PV_FROMDATE ||
              ''', ''' || PV_ENDDATE ||
              ''')) C
        LEFT JOIN HU_TERMINATE R
          ON EE.EMPLOYEE_ID = R.EMPLOYEE_ID
       WHERE (NVL(EE.WORK_STATUS,0) = 258 OR 
             ((NVL(EE.WORK_STATUS,0) = 257 OR NVL(EE.WORK_STATUS,0) = 256) 
             AND TRUNC(R.TER_DATE) BETWEEN '''||TRUNC(PV_FROMDATE)||''' AND '''||TRUNC(PV_ENDDATE)||'''))
         AND C.CDATE >= EE.START_DELETE
         AND C.CDATE <= EE.END_DELETE
         AND (C.CDATE <= R.LAST_DATE OR R.LAST_DATE IS NULL)
         AND TRUNC(C.CDATE) >= TRUNC(EE.JOIN_DATE)';
  
   /* INSERT INTO AT_STRSQL
      (ID, STRINGSQL)
    VALUES
      (SEQ_AT_STRSQL.NEXTVAL, PV_SQL);
  */
    EXECUTE IMMEDIATE PV_SQL;
  
    OPEN P_CUR FOR
      SELECT 1 FROM DUAL;
  
    PV_SQL := 'DROP TABLE ' || PV_TBL || ' ';
    EXECUTE IMMEDIATE PV_SQL;
  
    PV_SQL := 'DROP TABLE ' || PV_TBL_ORG || ' ';
    EXECUTE IMMEDIATE PV_SQL;
  
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      PV_SQL := 'DROP TABLE ' || PV_TBL || ' ';
      EXECUTE IMMEDIATE PV_SQL;
    
      PV_SQL := 'DROP TABLE ' || PV_TBL_ORG || ' ';
      EXECUTE IMMEDIATE PV_SQL;
    
      COMMIT;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSSINESS.GETSIGNDEFAULT',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              P_FROMDATE,
                              P_ENDATE,
                              P_ORG_ID,
                              P_PERIOD_ID);
    
  END;

  PROCEDURE GETSIGNDEFAULT_FOREMP(P_EMPLOYEE_ID IN NUMBER,
                                  P_USERNAME   IN VARCHAR2,
                                  P_ORG_ID     IN NUMBER,
                                  P_PERIOD_ID  IN NUMBER,
                                  P_ISDISSOLVE IN NUMBER,
                                  P_FROMDATE   IN DATE,
                                  P_ENDATE     IN DATE,
                                  P_OUT        OUT NUMBER) IS
    PV_FROMDATE   DATE;
    PV_ENDDATE    DATE;
    PV_REQUEST_ID NUMBER;
    PV_SQL        CLOB;
    PV_TBL        VARCHAR2(50);
    PV_TBL_ORG    VARCHAR2(50);
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    PV_FROMDATE   := P_FROMDATE;
    PV_ENDDATE    := P_ENDATE;
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O
       /*WHERE EXISTS (SELECT 1
        FROM AT_ORG_PERIOD OP
       WHERE OP.PERIOD_ID = P_PERIOD_ID
         AND OP.ORG_ID = O.ORG_ID
         AND OP.STATUSCOLEX = 1)*/
       );
  
    -- insert emp can tinh toan
    INSERT INTO AT_CHOSEN_EMP
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       USERNAME,
       REQUEST_ID,
       JOIN_DATE,
       WORK_STATUS)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              T.JOIN_DATE,
              T.WORK_STATUS
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_ENDDATE
                      AND E.STATUS_ID = 447
                      AND E.IS_MISSION = -1) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN AT_CHOSEN_ORG O
           ON O.ORG_ID = W.ORG_ID
          AND O.REQUEST_ID = PV_REQUEST_ID
        WHERE T.ID = P_EMPLOYEE_ID
          AND T.WORK_STATUS IS NOT NULL 
          AND (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE))
          AND T.ID NOT IN
              (SELECT A.EMPLOYEE_ID
                 FROM AT_WORKSIGN A
                WHERE A.WORKINGDAY BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE)
                  AND NVL(A.IS_IMPORT, 0) = 1
                GROUP BY A.EMPLOYEE_ID));
   
    --INSERT CA MAC DINH
    MERGE INTO AT_WORKSIGN WS
    USING (SELECT
             EE.EMPLOYEE_ID,
             C.CDATE,
             CASE
               WHEN C.CDATE IN
                    (SELECT H.WORKINGDAY
                       FROM AT_HOLIDAY H
                      WHERE H.YEAR = TO_CHAR(TRUNC(PV_ENDDATE), 'RRRR') 
                        AND NVL(H.OFFDAY,0) <> -1) THEN
                (SELECT ID FROM AT_SHIFT WHERE CODE = 'L')
               WHEN C.CDATE IN
                    (SELECT H.WORKINGDAY
                       FROM AT_HOLIDAY H
                      WHERE H.YEAR = TO_CHAR(TRUNC(PV_ENDDATE), 'RRRR')
                      AND NVL(H.OFFDAY,0) = -1) THEN
                (SELECT ID FROM AT_SHIFT WHERE CODE = 'OFF')
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 6 THEN
                EE.SUNDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 5 THEN
                EE.SATURDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 4 THEN
                EE.FRIDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 3 THEN
                EE.THURSDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 2 THEN
                EE.WEDNESDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 1 THEN
                EE.TUEDAY
               ELSE
                EE.MONDAY
             END SHIFT_ID,
             P_PERIOD_ID PERIOD_ID,
             SYSDATE CREATED_DATE,
             'AUTO' CREATED_BY, --==BAT BUOC DE AUTO DE PHAN BIET IMPORT
             UPPER(P_USERNAME) CREATED_LOG
        FROM (SELECT T.EMPLOYEE_ID,
                     CASE
                       WHEN T.EFFECT_DATE_FROM > PV_FROMDATE THEN
                        T.EFFECT_DATE_FROM
                       ELSE
                        PV_FROMDATE
                     END START_DELETE,
                     CASE
                       WHEN T.EFFECT_DATE_TO < PV_ENDDATE THEN
                        T.EFFECT_DATE_TO
                       ELSE
                        PV_ENDDATE
                     END END_DELETE,
                     NVL(T.SINGDEFAULE, 0) MONDAY,
                     NVL(T.SIGN_TUE, 0) TUEDAY,
                     NVL(T.SIGN_WED, 0) WEDNESDAY,
                     NVL(T.SIGN_THU, 0) THURSDAY,
                     NVL(T.SIGN_FRI, 0) FRIDAY,
                     NVL(T.SING_SAT, 0) SATURDAY,
                     NVL(T.SING_SUN, 0) SUNDAY,
                     EE.JOIN_DATE,
                     EE.WORK_STATUS                     
                FROM AT_SIGNDEFAULT T
               INNER JOIN AT_CHOSEN_EMP EE
                  ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
                 AND EE.REQUEST_ID = PV_REQUEST_ID
               WHERE T.ACTFLG = 'A'
                 AND T.EMPLOYEE_ID = P_EMPLOYEE_ID
                 /*AND TRUNC(T.EFFECT_DATE_FROM) = TRUNC(PV_FROMDATE)
                 AND TRUNC(T.EFFECT_DATE_TO) = TRUNC(PV_ENDDATE)*/) EE
       CROSS JOIN TABLE(TABLE_LISTDATE(PV_FROMDATE, PV_ENDDATE)) C
        LEFT JOIN HU_TERMINATE R
          ON EE.EMPLOYEE_ID = R.EMPLOYEE_ID
       WHERE (NVL(EE.WORK_STATUS,0) = 258 OR 
             ((NVL(EE.WORK_STATUS,0) = 257 OR NVL(EE.WORK_STATUS,0) = 256) 
             AND TRUNC(R.TER_DATE) BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE)))
         AND C.CDATE >= EE.START_DELETE
         AND C.CDATE <= EE.END_DELETE
         AND (C.CDATE <= R.LAST_DATE OR R.LAST_DATE IS NULL)
         AND TRUNC(C.CDATE) >= TRUNC(EE.JOIN_DATE)) H
    ON (WS.WORKINGDAY = H.CDATE AND WS.EMPLOYEE_ID = H.EMPLOYEE_ID)
  WHEN MATCHED THEN
    UPDATE SET WS.SHIFT_ID = H.SHIFT_ID,
               WS.MODIFIED_DATE = SYSDATE,
               WS.MODIFIED_BY = UPPER(P_USERNAME),
               WS.MODIFIED_LOG = UPPER(P_USERNAME)
  WHEN NOT MATCHED THEN
    INSERT
      (ID,
       EMPLOYEE_ID,
       WORKINGDAY,
       SHIFT_ID,
       PERIOD_ID,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG)       
    VALUES
      (SEQ_AT_WORKSIGN.NEXTVAL,
       H.EMPLOYEE_ID,
       H.CDATE,
       H.SHIFT_ID,
       NULL,
       H.CREATED_DATE,
       H.CREATED_BY,
       H.CREATED_LOG      
      );
    /* AND NOT EXISTS
    (SELECT H.ID FROM AT_HOLIDAY H WHERE H.WORKINGDAY = C.CDATE)*/
   
     P_OUT := 3;
     
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSSINESS.GETSIGNDEFAULT_FOREMP',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              P_USERNAME, 
                              P_FROMDATE,
                              P_ENDATE,
                              P_ORG_ID,
                              P_PERIOD_ID,
                              P_OUT);
    
  END;
  PROCEDURE GET_SUMMARY_OT(P_USERNAME        IN NVARCHAR2,
                           P_ORG_ID          IN NUMBER,
                           P_ISDISSOLVE      IN NUMBER,
                           P_PAGE_INDEX      IN NUMBER,
                           P_EMPLOYEE_CODE   IN VARCHAR2,
                           P_PAGE_SIZE       IN NUMBER,
                           P_PERIOD_ID       IN NUMBER,
                           P_EMPLOYEE_NAME   IN NVARCHAR2,
                           P_ORG_NAME        IN NVARCHAR2,
                           P_TITLE_NAME      IN NVARCHAR2,
                           P_STAFF_RANK_NAME IN NVARCHAR2,
                           P_EMP_OBJ         IN NUMBER,
                           P_START_DATE      IN DATE,
                           P_END_DATE        IN DATE,
                           P_CUR             OUT CURSOR_TYPE,
                           P_CURCOUNR        OUT CURSOR_TYPE) IS
    PV_STARTDATE DATE;
    PV_ENDDATE   DATE;
  BEGIN
  
    IF P_START_DATE IS NULL AND P_END_DATE IS NULL THEN
      SELECT P.START_DATE, P.END_DATE
        INTO PV_STARTDATE, PV_ENDDATE
        FROM AT_PERIOD P
       WHERE P.ID = P_PERIOD_ID;
    ELSE
      PV_STARTDATE := P_START_DATE;
      PV_ENDDATE   := P_END_DATE;
    END IF;
  
    OPEN P_CUR FOR
      SELECT 0 AS CBSTATUS,
             C.*
        FROM (SELECT ROWNUM             STT,
                     A.ID,
                     EE.ID              EMPLOYEE_ID,
                     EE.EMPLOYEE_CODE,
                     EE.FULLNAME_VN     FULLNAME_VN,
                     O.NAME_VN          ORG_NAME,
                     O.DESCRIPTION_PATH ORG_DESC,
                     TI.NAME_VN         TITLE_NAME,
                     A.TOTAL_FACTOR1,
                     A.TOTAL_FACTOR1_5,
                     A.TOTAL_FACTOR1_8,
                     A.TOTAL_FACTOR2,
                     A.TOTAL_FACTOR2_1,
                     A.TOTAL_FACTOR2_7,
                     A.TOTAL_FACTOR3,
                     A.TOTAL_FACTOR3_9,
                     A.TOTAL_NB1,
                     A.TOTAL_NB1_5,
                     A.TOTAL_NB1_8,
                     A.TOTAL_NB2,
                     A.TOTAL_NB2_1,
                     A.TOTAL_NB2_7,
                     A.TOTAL_NB3,
                     A.TOTAL_NB3_9,
                     A.NUMBER_FACTOR_CP,
                     A.OT_DAY,
                     A.OT_NIGHT,
                     A.OT_WEEKEND_DAY,
                     A.OT_WEEKEND_NIGHT,
                     A.OT_HOLIDAY_DAY,
                     A.OT_HOLIDAY_NIGHT,
                     A.NEW_OT_DAY,
                     A.NEW_OT_NIGHT,
                     A.NEW_OT_WEEKEND_DAY,
                     A.NEW_OT_WEEKEND_NIGHT,
                     A.NEW_OT_HOLIDAY_DAY,
                     A.NEW_OT_HOLIDAY_NIGHT
                FROM AT_TIME_TIMESHEET_OT A
               INNER JOIN HU_EMPLOYEE EE
                  ON EE.ID = A.EMPLOYEE_ID
               INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, P_ISDISSOLVE)) O1
                  ON A.ORG_ID = O1.ORG_ID
                LEFT JOIN HU_ORGANIZATION O
                  ON O.ID = A.ORG_ID
                LEFT JOIN HU_TITLE TI
                  ON TI.ID = EE.TITLE_ID
                LEFT JOIN HU_STAFF_RANK S
                  ON S.ID = EE.STAFF_RANK_ID
               WHERE A.PERIOD_ID = P_PERIOD_ID
                 AND (P_EMPLOYEE_CODE IS NULL OR
                     (P_EMPLOYEE_CODE IS NOT NULL AND
                     EE.EMPLOYEE_CODE LIKE '%' || P_EMPLOYEE_CODE || '%'))
                 AND (P_EMPLOYEE_NAME IS NULL OR
                     (P_EMPLOYEE_NAME IS NOT NULL AND
                     UPPER(EE.FULLNAME_VN) LIKE
                     '%' || UPPER(P_EMPLOYEE_NAME) || '%'))
                 AND (P_ORG_NAME IS NULL OR
                     (P_ORG_NAME IS NOT NULL AND
                     UPPER(O.NAME_VN) LIKE '%' || UPPER(P_ORG_NAME) || '%'))
                 AND (P_TITLE_NAME IS NULL OR
                     (P_TITLE_NAME IS NOT NULL AND
                     UPPER(TI.NAME_VN) LIKE
                     '%' || UPPER(P_TITLE_NAME) || '%'))
                 AND (P_STAFF_RANK_NAME IS NULL OR
                     (P_STAFF_RANK_NAME IS NOT NULL AND
                     UPPER(S.NAME) LIKE
                     '%' || UPPER(P_STAFF_RANK_NAME) || '%'))
                 AND (NVL(EE.WORK_STATUS, 0) <> 257 OR
                     (NVL(EE.WORK_STATUS, 0) = 257 AND
                     EE.TER_LAST_DATE >= PV_STARTDATE))
                 and (a.emp_obj_id = P_EMP_OBJ or P_EMP_OBJ = 0 or a.emp_obj_id is null)) C
       WHERE C.STT < (P_PAGE_INDEX * P_PAGE_SIZE) + 1
         AND C.STT >= ((P_PAGE_INDEX - 1) * P_PAGE_SIZE) + 1;
    OPEN P_CURCOUNR FOR
      SELECT COUNT(*) TOTAL
        FROM AT_TIME_TIMESHEET_OT A
       INNER JOIN HU_EMPLOYEE EE
          ON EE.ID = A.EMPLOYEE_ID
       INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, P_ISDISSOLVE)) O
          ON A.ORG_ID = O.ORG_ID
        LEFT JOIN HU_STAFF_RANK S
          ON S.ID = EE.STAFF_RANK_ID
        LEFT JOIN HU_TITLE TI
          ON TI.ID = EE.TITLE_ID
        LEFT JOIN HU_ORGANIZATION O
          ON O.ID = A.ORG_ID
       WHERE A.PERIOD_ID = P_PERIOD_ID
         AND (P_EMPLOYEE_CODE IS NULL OR
             (P_EMPLOYEE_CODE IS NOT NULL AND
             EE.EMPLOYEE_CODE LIKE '%' || P_EMPLOYEE_CODE || '%'))
         AND (P_EMPLOYEE_NAME IS NULL OR
             (P_EMPLOYEE_NAME IS NOT NULL AND
             UPPER(EE.FULLNAME_VN) LIKE
             '%' || UPPER(P_EMPLOYEE_NAME) || '%'))
         AND (P_ORG_NAME IS NULL OR
             (P_ORG_NAME IS NOT NULL AND
             UPPER(O.NAME_VN) LIKE '%' || UPPER(P_ORG_NAME) || '%'))
         AND (P_TITLE_NAME IS NULL OR
             (P_TITLE_NAME IS NOT NULL AND
             UPPER(TI.NAME_VN) LIKE '%' || UPPER(P_TITLE_NAME) || '%'))
         AND (P_STAFF_RANK_NAME IS NULL OR
             (P_STAFF_RANK_NAME IS NOT NULL AND
             UPPER(S.NAME) LIKE '%' || UPPER(P_STAFF_RANK_NAME) || '%'))
         AND (NVL(EE.WORK_STATUS, 0) <> 257 OR
             (NVL(EE.WORK_STATUS, 0) = 257 AND
             EE.TER_LAST_DATE >= PV_STARTDATE))
         and (a.emp_obj_id = P_EMP_OBJ or P_EMP_OBJ = 0);
  END;

  PROCEDURE INSERT_WORKSIGN_DATE(P_EMPLOYEEID IN NUMBER,
                                 P_PERIODID   IN NUMBER,
                                 P_USERNAME   IN VARCHAR2,
                                 P_D1         IN NUMBER,
                                 P_D2         IN NUMBER,
                                 P_D3         IN NUMBER,
                                 P_D4         IN NUMBER,
                                 P_D5         IN NUMBER,
                                 P_D6         IN NUMBER,
                                 P_D7         IN NUMBER,
                                 P_D8         IN NUMBER,
                                 P_D9         IN NUMBER,
                                 P_D10        IN NUMBER,
                                 P_D11        IN NUMBER,
                                 P_D12        IN NUMBER,
                                 P_D13        IN NUMBER,
                                 P_D14        IN NUMBER,
                                 P_D15        IN NUMBER,
                                 P_D16        IN NUMBER,
                                 P_D17        IN NUMBER,
                                 P_D18        IN NUMBER,
                                 P_D19        IN NUMBER,
                                 P_D20        IN NUMBER,
                                 P_D21        IN NUMBER,
                                 P_D22        IN NUMBER,
                                 P_D23        IN NUMBER,
                                 P_D24        IN NUMBER,
                                 P_D25        IN NUMBER,
                                 P_D26        IN NUMBER,
                                 P_D27        IN NUMBER,
                                 P_D28        IN NUMBER,
                                 P_D29        IN NUMBER,
                                 P_D30        IN NUMBER,
                                 P_D31        IN NUMBER) AS
  BEGIN
    MERGE INTO AT_WORKSIGN_DATE_TEMP T
    USING (SELECT P_EMPLOYEEID EMPLOYEE_ID FROM DUAL) D
    ON (T.EMPLOYEE_ID = D.EMPLOYEE_ID)
    WHEN NOT MATCHED THEN
      INSERT
        (ID,
         EMPLOYEE_ID,
         PERIOD_ID,
         D1,
         D2,
         D3,
         D4,
         D5,
         D6,
         D7,
         D8,
         D9,
         D10,
         D11,
         D12,
         D13,
         D14,
         D15,
         D16,
         D17,
         D18,
         D19,
         D20,
         D21,
         D22,
         D23,
         D24,
         D25,
         D26,
         D27,
         D28,
         D29,
         D30,
         D31)
      VALUES
        (SEQ_AT_WORKSIGN_DATE_TEMP.NEXTVAL,
         P_EMPLOYEEID,
         P_PERIODID,
         P_D1,
         P_D2,
         P_D3,
         P_D4,
         P_D5,
         P_D6,
         P_D7,
         P_D8,
         P_D9,
         P_D10,
         P_D11,
         P_D12,
         P_D13,
         P_D14,
         P_D15,
         P_D16,
         P_D17,
         P_D18,
         P_D19,
         P_D20,
         P_D21,
         P_D22,
         P_D23,
         P_D24,
         P_D25,
         P_D26,
         P_D27,
         P_D28,
         P_D29,
         P_D30,
         P_D31);
  
  END;

  PROCEDURE IMPORT_WORKSIGN_DATE(P_STARTDATE IN DATE,
                                 P_ENDDATE   IN DATE,
                                 P_USERNAME  IN VARCHAR2) AS
    PV_START      NUMBER;
    PV_END        NUMBER;
    PV_START_CHAR VARCHAR2(100);
    PV_END_CHAR   VARCHAR2(100);
    PV_DAYEND     NUMBER;
    PV_DAYSTART   NUMBER;
    PV_SEQ        NUMBER := 0;
    V_COL_V       CLOB;
    PV_SQL        CLOB;
  BEGIN
    PV_START      := TO_CHAR(P_STARTDATE, 'yyyymmdd');
    PV_END        := TO_CHAR(P_ENDDATE, 'yyyymmdd');
    PV_START_CHAR := TO_CHAR(P_STARTDATE, 'yyyymm');
    PV_END_CHAR   := TO_CHAR(P_ENDDATE, 'yyyymm');
    PV_DAYSTART   := EXTRACT(DAY FROM P_STARTDATE);
    PV_DAYEND     := EXTRACT(DAY FROM
                             TO_DATE('01/' || TO_CHAR(P_ENDDATE, 'MM/yyyy'),
                                     'dd/MM/yyyy') - 1);
  
    SELECT SEQ_ORG_TEMP_TABLE.NEXTVAL INTO PV_SEQ FROM DUAL;
  
    INSERT INTO AT_TEMP_DATE
      SELECT A.*, PV_SEQ, rownum
        FROM table(PKG_FUNCTION.table_listdate(P_STARTDATE, P_ENDDATE)) A;
  
    -- LAY DU LIEU PIVOT
    SELECT LISTAGG('D' || A.STT || ' ' || ' AS ' || '''' ||
                   TO_CHAR(A.CDATE, 'DD') || '''',
                   ',') WITHIN GROUP(ORDER BY A.STT)
      INTO V_COL_V
      FROM AT_TEMP_DATE A
     WHERE A.REQUEST_ID = PV_SEQ;
  
    DELETE AT_WORKSIGN
     WHERE EMPLOYEE_ID IN
           (SELECT E.EMPLOYEE_ID FROM AT_WORKSIGN_DATE_TEMP E)
       AND WORKINGDAY >= P_STARTDATE
       AND WORKINGDAY <= P_ENDDATE;
    PV_SQL := '
      INSERT INTO AT_WORKSIGN
        (ID,
         EMPLOYEE_ID,
         WORKINGDAY,
         PERIOD_ID,
         CREATED_DATE,
         CREATED_BY,
         CREATED_LOG,
         MODIFIED_DATE,
         MODIFIED_BY,
         MODIFIED_LOG,
         SHIFT_ID,
         IS_IMPORT)
        SELECT SEQ_AT_WORKSIGN.NEXTVAL,
               E.EMPLOYEE_ID,
               CASE WHEN TO_NUMBER(E.QUARTER) < ' ||
              PV_DAYSTART || ' THEN
                 TO_DATE(''' || PV_END_CHAR ||
              ''' || E.QUARTER, ''yyyymmdd'')
                 ELSE
                   TO_DATE(''' || PV_START_CHAR ||
              ''' || E.QUARTER, ''yyyymmdd'')
                END ,
               E.PERIOD_ID,
               SYSDATE,
              ''' || P_USERNAME || ''',
               ''' || P_USERNAME || ''',
               SYSDATE,
               ''' || P_USERNAME || ''',
               ''' || P_USERNAME || ''',
               E.QUANTITY_SOLD AS SHIFT_ID,
               1
               
               /*CASE
                 WHEN MOD(TO_CHAR(CASE WHEN TO_NUMBER(E.QUARTER) < ' ||
              PV_DAYSTART || ' THEN
                 TO_DATE(''' || PV_END_CHAR ||
              ''' || E.QUARTER, ''yyyymmdd'')
                 ELSE
                   TO_DATE(''' || PV_START_CHAR ||
              ''' || E.QUARTER, ''yyyymmdd'')
                END,
                                  ''J''),
                          7) = 6 AND NVL(S.SUNDAY, 0) > 0 THEN
                  81 -- OFF theo bang AT_SHIFT
                 WHEN MOD(TO_CHAR(CASE WHEN TO_NUMBER(E.QUARTER) < ' ||
              PV_DAYSTART || ' THEN
                 TO_DATE(''' || PV_END_CHAR ||
              ''' || E.QUARTER, ''yyyymmdd'')
                 ELSE
                   TO_DATE(''' || PV_START_CHAR ||
              ''' || E.QUARTER, ''yyyymmdd'')
                END,
                                  ''J''),
                          7) = 5 AND NVL(S.SATURDAY, 0) > 0 THEN
                  S.SATURDAY
                 ELSE
                  E.QUANTITY_SOLD
               END */

          FROM (SELECT *
                  FROM AT_WORKSIGN_DATE_TEMP T UNPIVOT INCLUDE NULLS(QUANTITY_SOLD FOR QUARTER IN(' ||
              V_COL_V || '))) E

         INNER JOIN AT_SHIFT S
            ON E.QUANTITY_SOLD = S.ID
         WHERE CASE WHEN TO_NUMBER(E.QUARTER) < ' ||
              PV_DAYSTART || ' THEN
               ''' || PV_END_CHAR || '''
                 ELSE
                   ''' || PV_START_CHAR || '''
                END || E.QUARTER >= ''' || PV_START || '''
           AND CASE WHEN TO_NUMBER(E.QUARTER) < ' ||
              PV_DAYSTART || ' THEN
                ''' || PV_END_CHAR || '''
                 ELSE
                   ''' || PV_START_CHAR || '''
                END || E.QUARTER <= ''' || PV_END || '''';
  
    INSERT INTO TEMP VALUES (PV_SQL);
    COMMIT;
    EXECUTE IMMEDIATE PV_SQL;
  
    DELETE AT_TEMP_DATE A WHERE A.REQUEST_ID = PV_SEQ;
    COMMIT;
    delete AT_WORKSIGN_DATE_TEMP;
    commit;
  END;
  PROCEDURE GETDATAFROMORG(P_USERNAME    IN NVARCHAR2,
                           P_ORG_ID      IN NUMBER,
                           P_ISDISSOLVE  IN NUMBER,
                           P_EXPORT_TYPE IN NUMBER,
                           P_PERIOD_ID   IN NUMBER,
                           P_EMP_OBJ     IN NUMBER,
                           P_START_DATE  IN date,
                           P_END_DATE    IN date,
                           P_CUR         OUT CURSOR_TYPE,
                           P_CUR2        OUT CURSOR_TYPE,
                           P_CUR3        OUT CURSOR_TYPE) IS
    PV_STARTDATE  DATE;
    PV_ENDDATE    DATE;
    PV_PERIODNAME NVARCHAR2(1000);
    PV_SEQ        NUMBER := 0;
    V_COL         CLOB;
    V_COL_V       CLOB;
    V_COL1        CLOB;
    PV_DAYEND     NUMBER;
    PV_SQL        CLOB;
    PV_SQL1       CLOB;
    PV_EMP_OBJ    NVARCHAR2(1000);
  BEGIN
    IF P_EXPORT_TYPE <> 3 AND P_EXPORT_TYPE <> 5 AND P_EXPORT_TYPE <> 8 AND
       P_EXPORT_TYPE <> 9 and P_EXPORT_TYPE <> 6 and P_EXPORT_TYPE <> 12 THEN
      SELECT P.START_DATE,
             P.END_DATE,
             P.PERIOD_NAME,
             EXTRACT(DAY FROM
                     TO_DATE('01/' || TO_CHAR(P.END_DATE, 'MM/yyyy'),
                             'dd/MM/yyyy') - 1)
        INTO PV_STARTDATE, PV_ENDDATE, PV_PERIODNAME, PV_DAYEND
        FROM AT_PERIOD P
       WHERE P.ID = P_PERIOD_ID;
    END IF;
    if P_EXPORT_TYPE = 6 OR P_EXPORT_TYPE = 12  then
    
      SELECT P_START_DATE,
             P_END_DATE,
             P.PERIOD_NAME,
             EXTRACT(DAY FROM
                     TO_DATE('01/' || TO_CHAR(P_END_DATE, 'MM/yyyy'),
                             'dd/MM/yyyy') - 1)
        INTO PV_STARTDATE, PV_ENDDATE, PV_PERIODNAME, PV_DAYEND
        FROM AT_PERIOD P
       WHERE P.ID = P_PERIOD_ID;
    end if;
  
    IF P_EXPORT_TYPE = 1 THEN
      OPEN P_CUR FOR
        SELECT ROWNUM STT, A.*
          FROM (SELECT E.ID EMPLOYEE_ID,
                       E.EMPLOYEE_CODE,
                       E.FULLNAME_VN VN_FULLNAME,
                       O.NAME_VN ORG_NAME,
                       O.ORG_PATH,
                       T.NAME_VN TITLE_NAME,
                       TO_CHAR(CA.CDATE, 'dd/MM/yyyy') WORKINGDAY
                  FROM HU_EMPLOYEE E
                 INNER JOIN (SELECT E.EMPLOYEE_ID,
                                   E.TITLE_ID,
                                   E.ORG_ID,
                                   E.IS_3B,
                                   E.STAFF_RANK_ID,
                                   ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                              FROM HU_WORKING E
                             WHERE E.EFFECT_DATE <= PV_ENDDATE
                               AND E.STATUS_ID = 447
                               AND E.IS_3B = 0) W
                    ON E.ID = W.EMPLOYEE_ID
                   AND W.ROW_NUMBER = 1
                 INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
                    ON W.ORG_ID = O1.ORG_ID
                  LEFT JOIN HUV_ORGANIZATION O
                    ON W.ORG_ID = O.ID
                  LEFT JOIN HU_TITLE T
                    ON W.TITLE_ID = T.ID
                 CROSS JOIN TABLE(TABLE_LISTDATE(PV_STARTDATE, PV_ENDDATE)) CA
                 WHERE E.CONTRACT_ID IS NOT NULL
                   AND E.WORK_STATUS IS NOT NULL
                   AND (E.WORK_STATUS <> 257 OR
                       (E.WORK_STATUS = 257 AND
                       E.TER_EFFECT_DATE >= PV_STARTDATE))
                 ORDER BY O.NAME_VN, E.EMPLOYEE_CODE) A;
    ELSIF P_EXPORT_TYPE = 2 THEN
      OPEN P_CUR FOR
        SELECT ROWNUM STT, TB.*
          FROM (SELECT E.ID            EMPLOYEE_ID,
                       E.EMPLOYEE_CODE,
                       E.FULLNAME_VN   VN_FULLNAME,
                       O.ID            ORG_ID,
                       O.NAME_VN       ORG_NAME,
                       O.ORG_PATH,
                       T.NAME_VN       TITLE_NAME,
                       K.NAME          STAFF_RANK_NAME
                  FROM HU_EMPLOYEE E
                 INNER JOIN (SELECT E.EMPLOYEE_ID,
                                   E.TITLE_ID,
                                   E.ORG_ID,
                                   E.IS_3B,
                                   E.STAFF_RANK_ID,
                                   ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                              FROM HU_WORKING E
                             WHERE E.EFFECT_DATE <= PV_ENDDATE
                               AND E.STATUS_ID = 447
                               AND E.IS_3B = 0) W
                    ON E.ID = W.EMPLOYEE_ID
                   AND W.ROW_NUMBER = 1
                 INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
                    ON W.ORG_ID = O1.ORG_ID
                  LEFT JOIN HUV_ORGANIZATION O
                    ON W.ORG_ID = O.ID
                  LEFT JOIN HU_TITLE T
                    ON W.TITLE_ID = T.ID
                  LEFT JOIN HU_STAFF_RANK K
                    ON W.STAFF_RANK_ID = K.ID
                 WHERE E.CONTRACT_ID IS NOT NULL
                   AND (NVL(E.WORK_STATUS, 0) <> 257 OR
                       (E.WORK_STATUS = 257 AND
                       E.TER_LAST_DATE >= PV_STARTDATE))
                 ORDER BY O.NAME_VN, E.EMPLOYEE_CODE) TB;
    
      OPEN P_CUR2 FOR
        SELECT M.ID, M.NAME NAME_VN
          FROM AT_TIME_MANUAL M
         WHERE M.CODE = 'RDT'
            OR M.CODE = 'RVS'
           AND M.ACTFLG = 'A';
      OPEN P_CUR3 FOR
        SELECT S.ID,
               S.CODE,
               S.NAME_VN,
               '[' || M.CODE || '] - ' || M.NAME MANUAL_NAME,
               CASE
                 WHEN S.IS_NOON = -1 THEN
                  'X'
                 ELSE
                  ''
               END IS_NOON,
               M1.NAME SUNDAY_NAME,
               SATURDAY.NAME_VN SATURDAY_NAME,
               S.HOURS_START,
               S.HOURS_STOP,
               S.HOURS_STAR_CHECKIN,
               S.HOURS_STAR_CHECKOUT,
               S.NOTE
          FROM AT_SHIFT S
          /*INNER JOIN (SELECT DISTINCT SOA.AT_SHIFT_ID FROM AT_SHIFT_ORG_ACCESS SOA
                             INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
                             ON SOA.ORG_ID = O1.ORG_ID) ORGS
            ON ORGS.AT_SHIFT_ID=S.ID*/
          --INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
            --ON S.ORG_ID = O1.ORG_ID
          LEFT JOIN AT_SHIFT SATURDAY
            ON S.SATURDAY = SATURDAY.ID
          LEFT JOIN AT_TIME_MANUAL M
            ON S.MANUAL_ID = M.ID
          LEFT JOIN AT_TIME_MANUAL M1
            ON S.SUNDAY = M1.ID
         WHERE S.ACTFLG = 'A'
         ORDER BY S.CODE;
         /*UNION
         SELECT S.ID,
               S.CODE,
               S.NAME_VN,
               '[' || M.CODE || '] - ' || M.NAME MANUAL_NAME,
               CASE
                 WHEN S.IS_NOON = -1 THEN
                  'X'
                 ELSE
                  ''
               END IS_NOON,
               M1.NAME SUNDAY_NAME,
               SATURDAY.NAME_VN SATURDAY_NAME,
               S.HOURS_START,
               S.HOURS_STOP,
               S.HOURS_STAR_CHECKIN,
               S.HOURS_STAR_CHECKOUT,
               S.NOTE
          FROM AT_SHIFT S
         
          LEFT JOIN AT_SHIFT SATURDAY
            ON S.SATURDAY = SATURDAY.ID
          LEFT JOIN AT_TIME_MANUAL M
            ON S.MANUAL_ID = M.ID
          LEFT JOIN AT_TIME_MANUAL M1
            ON S.SUNDAY = M1.ID
         WHERE S.ACTFLG = 'A' AND S.CODE='OFF' OR S.CODE='L';*/
    ELSIF P_EXPORT_TYPE = 3 THEN
      OPEN P_CUR FOR
        SELECT ROWNUM STT, TB.*
          FROM (SELECT E.ID            EMPLOYEE_ID,
                       E.EMPLOYEE_CODE,
                       E.FULLNAME_VN   VN_FULLNAME,
                       E.ORG_ID,
                       E.TITLE_ID,
                       O.NAME_VN       ORG_NAME,
                       O.ORG_PATH,
                       T.NAME_VN       TITLE_NAME,
                       K.NAME          STAFF_RANK_NAME
                  FROM HU_EMPLOYEE E
                 INNER JOIN (SELECT E.EMPLOYEE_ID,
                                   E.TITLE_ID,
                                   E.ORG_ID,
                                   E.IS_3B,
                                   E.STAFF_RANK_ID,
                                   ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                              FROM HU_WORKING E
                             WHERE E.EFFECT_DATE <= SYSDATE
                               AND E.STATUS_ID = 447
                               AND E.IS_3B = 0) W
                    ON E.ID = W.EMPLOYEE_ID
                   AND W.ROW_NUMBER = 1
                 INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
                    ON E.ORG_ID = O1.ORG_ID
                  LEFT JOIN HUV_ORGANIZATION O
                    ON E.ORG_ID = O.ID
                  LEFT JOIN HU_TITLE T
                    ON E.TITLE_ID = T.ID
                  LEFT JOIN HU_STAFF_RANK K
                    ON E.STAFF_RANK_ID = K.ID
                 WHERE E.CONTRACT_ID IS NOT NULL
                   AND (NVL(E.WORK_STATUS, 0) <> 257 OR
                       (E.WORK_STATUS = 257 AND E.TER_LAST_DATE >= SYSDATE))
                 ORDER BY O.NAME_VN, E.EMPLOYEE_CODE) TB;
    ELSIF P_EXPORT_TYPE = 4 THEN
      OPEN P_CUR FOR
        SELECT T.ID, T.CODE FROM AT_TIME_MANUAL T WHERE T.ACTFLG = 'A';
    ELSIF P_EXPORT_TYPE = 10 THEN
      --- CASE CHO XU?T FILE M?U X? L? C?NG
      OPEN P_CUR FOR
        SELECT T.ID, T.CODE
          FROM AT_TIME_MANUAL T
         WHERE T.ACTFLG = 'A'
           AND (T.ORG_ID = -1 OR
               T.ORG_ID IN (SELECT T.ORG_ID
                               FROM SE_USER S
                               LEFT JOIN SE_USER_ORG_ACCESS T
                                 ON S.ID = T.USER_ID
                               LEFT JOIN HU_ORGANIZATION O
                                 ON T.ORG_ID = O.ID
                              WHERE S.USERNAME = P_USERNAME))
         ORDER BY T.ORDERS ASC;
    ELSIF P_EXPORT_TYPE = 5 THEN
      OPEN P_CUR FOR
        SELECT ROWNUM STT, TB.*
          FROM (SELECT E.ID            EMPLOYEE_ID,
                       E.EMPLOYEE_CODE,
                       E.FULLNAME_VN   VN_FULLNAME,
                       E.ORG_ID,
                       E.TITLE_ID,
                       O.NAME_VN       ORG_NAME,
                       O.ORG_PATH,
                       T.NAME_VN       TITLE_NAME,
                       S.NAME          STAFF_RANK_NAME
                  FROM HU_EMPLOYEE E
                 INNER JOIN (SELECT E.EMPLOYEE_ID,
                                   E.TITLE_ID,
                                   E.ORG_ID,
                                   E.IS_3B,
                                   E.STAFF_RANK_ID,
                                   ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                              FROM HU_WORKING E
                             WHERE E.EFFECT_DATE <= SYSDATE
                               AND E.STATUS_ID = 447
                               AND E.IS_3B = 0) W
                    ON E.ID = W.EMPLOYEE_ID
                   AND W.ROW_NUMBER = 1
                  LEFT JOIN HUV_ORGANIZATION O
                    ON W.ORG_ID = O.ID
                  LEFT JOIN HU_TITLE T
                    ON W.TITLE_ID = T.ID
                  LEFT JOIN HU_STAFF_RANK S
                    ON W.STAFF_RANK_ID = S.ID
                 INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
                    ON W.ORG_ID = O1.ORG_ID
                 WHERE E.CONTRACT_ID IS NOT NULL
                   AND NVL(E.WORK_STATUS, 0) <> 257
                   AND E.WORK_STATUS IS NOT NULL
                    OR (NVL(E.WORK_STATUS, 0) = 257 AND
                       E.TER_EFFECT_DATE >= SYSDATE)
                   AND E.ID NOT IN
                       (SELECT I.EMPLOYEE_ID FROM INS_INFORMATION I)
                 ORDER BY O.NAME_VN, E.EMPLOYEE_CODE) TB;
    ELSIF P_EXPORT_TYPE = 6 THEN
      IF P_START_DATE IS NULL and P_END_DATE is null THEN
        PV_STARTDATE := TO_DATE('01/01/2016', 'dd/MM/yyyy');
        PV_ENDDATE   := TO_DATE('31/01/2016', 'dd/MM/yyyy');
      else
        PV_STARTDATE := P_START_DATE;
        PV_ENDDATE   := P_END_DATE;
      end if;
      PV_EMP_OBJ := 'l.object_employee_id= ' || P_EMP_OBJ;
    
      select EXTRACT(DAY FROM LAST_DAY(P_START_DATE))
        into PV_DAYEND
        from dual;
    
      SELECT SEQ_ORG_TEMP_TABLE.NEXTVAL INTO PV_SEQ FROM DUAL;
      SELECT CASE
               WHEN PV_DAYEND = 31 THEN
                ''
               WHEN PV_DAYEND = 30 THEN
                ', NULL AS D31'
               WHEN PV_DAYEND = 29 THEN
                ', NULL AS D31, NULL AS D30'
               WHEN PV_DAYEND = 28 THEN
                ', NULL AS D31, NULL AS D30, NULL AS D29'
               ELSE
                ''
             END
        INTO V_COL1
        FROM DUAL;
    
      INSERT INTO AT_TEMP_DATE
        SELECT A.*, PV_SEQ, rownum
          FROM table(PKG_FUNCTION.table_listdate(PV_STARTDATE, PV_ENDDATE)) A;
    
      -- LAY DANH SACH COT DONG THEO THANG
      SELECT LISTAGG('A.D' || TO_CHAR(EXTRACT(DAY FROM A.CDATE)), ',') WITHIN GROUP(ORDER BY A.STT)
        INTO V_COL
        FROM AT_TEMP_DATE A
       WHERE A.REQUEST_ID = PV_SEQ;
    
      -- LAY DU LIEU PIVOT
      SELECT LISTAGG('''' || TO_CHAR(EXTRACT(DAY FROM A.CDATE)) || '''' ||
                     ' AS "D' || A.STT || '"',
                     ',') WITHIN GROUP(ORDER BY A.STT)
        INTO V_COL_V
        FROM AT_TEMP_DATE A
       WHERE A.REQUEST_ID = PV_SEQ;
    
      PV_SQL := '
        WITH CTE_WORKSIGN AS
         (SELECT A.EMPLOYEE_ID,
                 ' || V_COL || '
            FROM (SELECT T.EMPLOYEE_ID,
                         TO_NUMBER(TO_CHAR(T.WORKINGDAY, ''dd'')) AS DAY,
                         L.NAME_VN
                    FROM AT_WORKSIGN T
                     left join (select k.*
  from (select distinct w.employee_id,
               w.object_employee_id,
               w.effect_date,               
               RANK() OVER (PARTITION BY w.employee_id ORDER BY w.effect_date desc) AS  k
          from hu_working w
          WHERE W.IS_MISSION = -1
          AND W.EFFECT_DATE <= ''' || PV_ENDDATE ||
                ''' AND W.STATUS_ID = 447) k
            where  k.k = 1 ) l on l.employee_id = t.employee_id
              
                    LEFT JOIN AT_SHIFT L
                      ON T.SHIFT_ID = L.ID
                   WHERE ' || PV_EMP_OBJ || '
                   and T.WORKINGDAY BETWEEN ''' ||
                PV_STARTDATE || ''' AND ''' || PV_ENDDATE ||
                ''') PIVOT(MAX(NAME_VN) FOR DAY IN(' || V_COL_V ||
                ')) A)
        SELECT ROWNUM STT, TB.*
          FROM (SELECT E.ID            EMPLOYEE_ID,
                       E.EMPLOYEE_CODE,
                       E.FULLNAME_VN   VN_FULLNAME,
                       to_date(''' || P_START_DATE ||
                ''')  P_START_DATE ,
                       to_date(''' || P_END_DATE ||
                ''')   P_END_DATE ,
                       ' || P_PERIOD_ID ||
                ' P_PERIOD_ID,
                       ' || P_EMP_OBJ ||
                ' P_EMP_OBJ,
                       O.ID            ORG_ID,
                       O.NAME_VN       ORG_NAME,
                       O.ORG_PATH,
                       T.NAME_VN       TITLE_NAME,
                       K.NAME          STAFF_RANK_NAME,
                       PKG_ATTENDANCE_LIST.AT_GET_WORKSTANDARD(E.ID,' || P_PERIOD_ID ||') WS,
                       ' || V_COL || '
                  FROM HU_EMPLOYEE E
                 INNER JOIN (SELECT E.EMPLOYEE_ID,
                                   E.TITLE_ID,
                                   E.ORG_ID,
                                   E.IS_3B,
                                   E.STAFF_RANK_ID,
                                   ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                              FROM HU_WORKING E
                             WHERE E.EFFECT_DATE <= ''' ||
                PV_ENDDATE || '''
                               AND E.STATUS_ID = 447
                               AND E.IS_3B = 0) W
                    ON E.ID = W.EMPLOYEE_ID
                   AND W.ROW_NUMBER = 1
                  inner JOIN CTE_WORKSIGN A
                    ON E.ID = A.EMPLOYEE_ID
                 INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(''' ||
                P_USERNAME || '''),' || P_ORG_ID || ',' || P_ISDISSOLVE ||
                ')) O1
                    ON W.ORG_ID = O1.ORG_ID
                  LEFT JOIN HUV_ORGANIZATION O
                    ON W.ORG_ID = O.ID
                  LEFT JOIN HU_TITLE T
                    ON W.TITLE_ID = T.ID
                  LEFT JOIN HU_STAFF_RANK K
                    ON W.STAFF_RANK_ID = K.ID
                 WHERE E.CONTRACT_ID IS NOT NULL
                   AND (NVL(E.WORK_STATUS, 0) <> 257 OR
                       (E.WORK_STATUS = 257 AND
                       E.TER_LAST_DATE >= ''' ||
                PV_STARTDATE ||
                '''))
                 ORDER BY O.NAME_VN, E.EMPLOYEE_CODE) TB';
    
      INSERT INTO AT_STRSQL VALUES (SEQ_AT_STRSQL.NEXTVAL, PV_SQL);
      COMMIT;
    
      OPEN P_CUR FOR TO_CHAR(PV_SQL);
    
      OPEN P_CUR2 FOR
        SELECT M.ID, M.NAME NAME_VN
          FROM AT_TIME_MANUAL M
         WHERE M.CODE = 'RDT'
            OR M.CODE = 'RVS'
           AND M.ACTFLG = 'A';
      OPEN P_CUR3 FOR
        SELECT S.ID,
               S.CODE,
               S.NAME_VN,
               '[' || M.CODE || '] - ' || M.NAME MANUAL_NAME,
               CASE
                 WHEN S.IS_NOON = -1 THEN
                  'X'
                 ELSE
                  ''
               END IS_NOON,
               M1.NAME SUNDAY_NAME,
               SATURDAY.NAME_VN SATURDAY_NAME,
               S.HOURS_START,
               S.HOURS_STOP,
               S.HOURS_STAR_CHECKIN,
               S.HOURS_STAR_CHECKOUT,
               S.NOTE
          FROM AT_SHIFT S
          /*INNER JOIN (SELECT DISTINCT SOA.AT_SHIFT_ID FROM AT_SHIFT_ORG_ACCESS SOA
                             INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
                             ON SOA.ORG_ID = O1.ORG_ID) ORGS
            ON ORGS.AT_SHIFT_ID=S.ID*/
          --INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
            --ON S.ORG_ID = O1.ORG_ID
          LEFT JOIN AT_SHIFT SATURDAY
            ON S.SATURDAY = SATURDAY.ID
          LEFT JOIN AT_TIME_MANUAL M
            ON S.MANUAL_ID = M.ID
          LEFT JOIN AT_TIME_MANUAL M1
            ON S.SUNDAY = M1.ID
         WHERE S.ACTFLG = 'A'
         ORDER BY S.CODE;
         /*UNION
         SELECT S.ID,
               S.CODE,
               S.NAME_VN,
               '[' || M.CODE || '] - ' || M.NAME MANUAL_NAME,
               CASE
                 WHEN S.IS_NOON = -1 THEN
                  'X'
                 ELSE
                  ''
               END IS_NOON,
               M1.NAME SUNDAY_NAME,
               SATURDAY.NAME_VN SATURDAY_NAME,
               S.HOURS_START,
               S.HOURS_STOP,
               S.HOURS_STAR_CHECKIN,
               S.HOURS_STAR_CHECKOUT,
               S.NOTE
          FROM AT_SHIFT S
         
          LEFT JOIN AT_SHIFT SATURDAY
            ON S.SATURDAY = SATURDAY.ID
          LEFT JOIN AT_TIME_MANUAL M
            ON S.MANUAL_ID = M.ID
          LEFT JOIN AT_TIME_MANUAL M1
            ON S.SUNDAY = M1.ID
         WHERE S.ACTFLG = 'A' AND S.CODE='OFF' OR S.CODE='L';*/
    ELSIF P_EXPORT_TYPE = 7 THEN
      OPEN P_CUR FOR
        SELECT ROWNUM STT, TB.*
          FROM (SELECT E.ID            EMPLOYEE_ID,
                       E.EMPLOYEE_CODE,
                       E.FULLNAME_VN   VN_FULLNAME,
                       E.ORG_ID,
                       E.TITLE_ID,
                       O.NAME_VN       ORG_NAME,
                       O.ORG_PATH,
                       T.NAME_VN       TITLE_NAME,
                       S.NAME          STAFF_RANK_NAME,
                       PN.CUR_HAVE     BALANCE_NOW,
                       C.CUR_HAVE      NBCL,
                       PV_PERIODNAME   PERIOD_NAME
                  FROM HU_EMPLOYEE E
                 INNER JOIN (SELECT E.EMPLOYEE_ID,
                                   E.TITLE_ID,
                                   E.ORG_ID,
                                   E.IS_3B,
                                   E.STAFF_RANK_ID,
                                   ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                              FROM HU_WORKING E
                             WHERE E.EFFECT_DATE <= PV_ENDDATE
                               AND E.STATUS_ID = 447
                               AND E.IS_3B = 0) W
                    ON E.ID = W.EMPLOYEE_ID
                   AND W.ROW_NUMBER = 1
                  LEFT JOIN HUV_ORGANIZATION O
                    ON W.ORG_ID = O.ID
                  LEFT JOIN HU_TITLE T
                    ON W.TITLE_ID = T.ID
                  LEFT JOIN HU_STAFF_RANK S
                    ON W.STAFF_RANK_ID = S.ID
                  LEFT JOIN AT_ENTITLEMENT PN
                    ON E.ID = PN.EMPLOYEE_ID
                   AND TO_CHAR(PN.YEAR) = TO_CHAR(PV_STARTDATE, 'yyyy')
                   AND TO_CHAR(PN.MONTH) = TO_CHAR(PV_STARTDATE, 'MM')
                  LEFT JOIN AT_COMPENSATORY C
                    ON E.ID = C.EMPLOYEE_ID
                   AND TO_CHAR(C.YEAR) = TO_CHAR(PV_STARTDATE, 'yyyy')
                 INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
                    ON W.ORG_ID = O1.ORG_ID
                 WHERE E.CONTRACT_ID IS NOT NULL
                   AND (NVL(E.WORK_STATUS, 0) <> 257 OR
                       (E.WORK_STATUS = 257 AND
                       E.TER_LAST_DATE >= PV_STARTDATE))
                 ORDER BY O.NAME_VN, E.EMPLOYEE_CODE) TB;
      ----------------------------------------------------------------
    ELSIF P_EXPORT_TYPE = 8 THEN
      OPEN P_CUR FOR
        SELECT ROWNUM STT, TB.*
          FROM (SELECT E.ID            EMPLOYEE_ID,
                       E.EMPLOYEE_CODE,
                       E.FULLNAME_VN   VN_FULLNAME,
                       E.ORG_ID,
                       E.TITLE_ID,
                       O.NAME_VN       ORG_NAME,
                       O.ORG_PATH,
                       T.NAME_VN       TITLE_NAME,
                       S.NAME          STAFF_RANK_NAME
                  FROM HU_EMPLOYEE E
                 INNER JOIN (SELECT E.EMPLOYEE_ID,
                                   E.TITLE_ID,
                                   E.ORG_ID,
                                   E.IS_3B,
                                   E.STAFF_RANK_ID,
                                   ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                              FROM HU_WORKING E
                             WHERE E.EFFECT_DATE <= SYSDATE
                               AND E.STATUS_ID = 447
                               AND E.IS_3B = 0) W
                    ON E.ID = W.EMPLOYEE_ID
                   AND W.ROW_NUMBER = 1
                  LEFT JOIN HUV_ORGANIZATION O
                    ON W.ORG_ID = O.ID
                  LEFT JOIN HU_TITLE T
                    ON W.TITLE_ID = T.ID
                  LEFT JOIN HU_STAFF_RANK S
                    ON W.STAFF_RANK_ID = S.ID
                  LEFT JOIN INS_SUN_CARE C
                    ON E.ID = C.EMPLOYEE_ID
                   AND TO_CHAR(C.START_DATE, 'yyyy') =
                       TO_CHAR(SYSDATE, 'yyyy')
                 INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
                    ON W.ORG_ID = O1.ORG_ID
                 WHERE E.CONTRACT_ID IS NOT NULL
                   AND NVL(E.WORK_STATUS, 0) <> 257
                   AND E.WORK_STATUS IS NOT NULL
                    OR (NVL(E.WORK_STATUS, 0) = 257 AND
                       E.TER_EFFECT_DATE >= SYSDATE)
                 ORDER BY O.NAME_VN, E.EMPLOYEE_CODE) TB;
      -------------------------------------------------------------------------------
    ELSIF P_EXPORT_TYPE = 9 THEN
      OPEN P_CUR FOR
        SELECT ROWNUM STT, TB.*
          FROM (SELECT E.ID            EMPLOYEE_ID,
                       E.EMPLOYEE_CODE,
                       E.FULLNAME_VN   VN_FULLNAME,
                       E.ORG_ID,
                       E.TITLE_ID,
                       O.NAME_VN       ORG_NAME,
                       O.ORG_PATH,
                       T.NAME_VN       TITLE_NAME,
                       K.NAME          STAFF_RANK_NAME
                  FROM HU_EMPLOYEE E
                 INNER JOIN (SELECT E.EMPLOYEE_ID,
                                   E.TITLE_ID,
                                   E.ORG_ID,
                                   E.IS_3B,
                                   E.STAFF_RANK_ID,
                                   ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                              FROM HU_WORKING E
                             WHERE to_char(E.EFFECT_DATE, 'YYYYMM') <=
                                   TO_CHAR(ADD_MONTHS(SYSDATE, -1), 'YYYYMM')
                               AND E.STATUS_ID = 447
                               AND E.IS_3B = 0) W
                    ON E.ID = W.EMPLOYEE_ID
                   AND W.ROW_NUMBER = 1
                 INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
                    ON W.ORG_ID = O1.ORG_ID
                  LEFT JOIN HUV_ORGANIZATION O
                    ON W.ORG_ID = O.ID
                  LEFT JOIN HU_TITLE T
                    ON W.TITLE_ID = T.ID
                  LEFT JOIN HU_STAFF_RANK K
                    ON W.STAFF_RANK_ID = K.ID
                 WHERE E.CONTRACT_ID IS NOT NULL
                   AND (NVL(E.WORK_STATUS, 0) <> 257 OR
                       (E.WORK_STATUS = 257 AND
                       to_char(E.TER_LAST_DATE, 'YYYYMM') >=
                       TO_CHAR(ADD_MONTHS(SYSDATE, -1), 'YYYYMM')))
                 ORDER BY O.NAME_VN, E.EMPLOYEE_CODE) TB;
      ELSIF P_EXPORT_TYPE = 12 THEN
      IF P_START_DATE IS NULL and P_END_DATE is null THEN
        PV_STARTDATE := TO_DATE('01/01/2016', 'dd/MM/yyyy');
        PV_ENDDATE   := TO_DATE('31/01/2016', 'dd/MM/yyyy');
      else
        PV_STARTDATE := P_START_DATE;
        PV_ENDDATE   := P_END_DATE;
      end if;
      PV_EMP_OBJ := 'l.object_employee_id= ' || P_EMP_OBJ;
    
      select EXTRACT(DAY FROM LAST_DAY(P_START_DATE))
        into PV_DAYEND
        from dual;
    
      SELECT SEQ_ORG_TEMP_TABLE.NEXTVAL INTO PV_SEQ FROM DUAL;
      SELECT CASE
               WHEN PV_DAYEND = 31 THEN
                ''
               WHEN PV_DAYEND = 30 THEN
                ', NULL AS D31'
               WHEN PV_DAYEND = 29 THEN
                ', NULL AS D31, NULL AS D30'
               WHEN PV_DAYEND = 28 THEN
                ', NULL AS D31, NULL AS D30, NULL AS D29'
               ELSE
                ''
             END
        INTO V_COL1
        FROM DUAL;
    
      INSERT INTO AT_TEMP_DATE
        SELECT A.*, PV_SEQ, rownum
          FROM table(PKG_FUNCTION.table_listdate(PV_STARTDATE, PV_ENDDATE)) A;
    
      -- LAY DANH SACH COT DONG THEO THANG
      SELECT LISTAGG('A.D' || TO_CHAR(EXTRACT(DAY FROM A.CDATE)), ',') WITHIN GROUP(ORDER BY A.STT)
        INTO V_COL
        FROM AT_TEMP_DATE A
       WHERE A.REQUEST_ID = PV_SEQ;
    
      -- LAY DU LIEU PIVOT
      SELECT LISTAGG('''' || TO_CHAR(EXTRACT(DAY FROM A.CDATE)) || '''' ||
                     ' AS "D' || A.STT || '"',
                     ',') WITHIN GROUP(ORDER BY A.STT)
        INTO V_COL_V
        FROM AT_TEMP_DATE A
       WHERE A.REQUEST_ID = PV_SEQ;
    
      PV_SQL := '
        WITH CTE_WORKSIGN AS
         (SELECT A.EMPLOYEE_ID,
                 ' || V_COL || '
            FROM (SELECT T.EMPLOYEE_ID,
                         TO_NUMBER(TO_CHAR(T.WORKINGDAY, ''dd'')) AS DAY,
                         L.NAME_VN
                    FROM AT_WORKSIGN T
                     left join (select k.*
  from (select distinct w.employee_id,
               w.object_employee_id,
               w.effect_date,               
               RANK() OVER (PARTITION BY w.employee_id ORDER BY w.effect_date desc) AS  k
          from hu_working w
          WHERE W.IS_MISSION = -1
          AND W.EFFECT_DATE <= ''' || PV_ENDDATE ||
                ''' AND W.STATUS_ID = 447) k
            where  k.k = 1 ) l on l.employee_id = t.employee_id
              
                    LEFT JOIN AT_SHIFT L
                      ON T.SHIFT_ID = L.ID
                   WHERE ' || PV_EMP_OBJ || '
                   and T.WORKINGDAY BETWEEN ''' ||
                PV_STARTDATE || ''' AND ''' || PV_ENDDATE ||
                ''') PIVOT(MAX(NAME_VN) FOR DAY IN(' || V_COL_V ||
                ')) A)
        SELECT ROWNUM STT, TB.*
          FROM (SELECT E.ID            EMPLOYEE_ID,
                       E.EMPLOYEE_CODE,
                       E.FULLNAME_VN   VN_FULLNAME,
                       to_date(''' || P_START_DATE ||
                ''')  P_START_DATE ,
                       to_date(''' || P_END_DATE ||
                ''')   P_END_DATE ,
                       ' || P_PERIOD_ID ||
                ' P_PERIOD_ID,
                       ' || P_EMP_OBJ ||
                ' P_EMP_OBJ,
                       O.ID            ORG_ID,
                       O.NAME_VN       ORG_NAME,
                       O.ORG_PATH,
                       T.NAME_VN       TITLE_NAME,
                       K.NAME          STAFF_RANK_NAME,
                       ' || V_COL || '
                  FROM HU_EMPLOYEE E
                 INNER JOIN (SELECT E.EMPLOYEE_ID,
                                   E.TITLE_ID,
                                   E.ORG_ID,
                                   E.IS_3B,
                                   E.STAFF_RANK_ID,
                                   ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                              FROM HU_WORKING E
                             WHERE E.EFFECT_DATE <= ''' ||
                PV_ENDDATE || '''
                               AND E.STATUS_ID = 447
                               AND E.IS_MISSION = -1) W
                    ON E.ID = W.EMPLOYEE_ID
                   AND W.ROW_NUMBER = 1
                  inner JOIN CTE_WORKSIGN A
                    ON E.ID = A.EMPLOYEE_ID
                 INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(''' ||
                P_USERNAME || '''),' || P_ORG_ID || ',0)) O1
                    ON W.ORG_ID = O1.ORG_ID
                  LEFT JOIN HUV_ORGANIZATION O
                    ON W.ORG_ID = O.ID
                  LEFT JOIN HU_TITLE T
                    ON W.TITLE_ID = T.ID
                  LEFT JOIN HU_STAFF_RANK K
                    ON W.STAFF_RANK_ID = K.ID
                 WHERE E.CONTRACT_ID IS NOT NULL
                   AND (NVL(E.WORK_STATUS, 0) <> 257 OR
                       (E.WORK_STATUS = 257 AND
                       E.TER_LAST_DATE >= ''' ||
                PV_STARTDATE ||
                '''))
                 ORDER BY O.NAME_VN, E.EMPLOYEE_CODE) TB';
    
      INSERT INTO AT_STRSQL VALUES (SEQ_AT_STRSQL.NEXTVAL, PV_SQL);
      COMMIT;
    
      OPEN P_CUR FOR TO_CHAR(PV_SQL);
    
      OPEN P_CUR2 FOR
        SELECT M.ID, M.NAME NAME_VN
          FROM AT_TIME_MANUAL M
         WHERE M.CODE = 'RDT'
            OR M.CODE = 'RVS'
           AND M.ACTFLG = 'A';
      OPEN P_CUR3 FOR
        SELECT S.ID,
               S.CODE,
               S.NAME_VN,
               '[' || M.CODE || '] - ' || M.NAME MANUAL_NAME,
               CASE
                 WHEN S.IS_NOON = -1 THEN
                  'X'
                 ELSE
                  ''
               END IS_NOON,
               M1.NAME SUNDAY_NAME,
               SATURDAY.NAME_VN SATURDAY_NAME,
               S.HOURS_START,
               S.HOURS_STOP,
               S.HOURS_STAR_CHECKIN,
               S.HOURS_STAR_CHECKOUT,
               S.NOTE
          FROM AT_SHIFT S
          INNER JOIN (SELECT DISTINCT SOA.AT_SHIFT_ID FROM AT_SHIFT_ORG_ACCESS SOA
                             INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
                             ON SOA.ORG_ID = O1.ORG_ID) ORGS
            ON ORGS.AT_SHIFT_ID=S.ID
          --INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
            --ON S.ORG_ID = O1.ORG_ID
          LEFT JOIN AT_SHIFT SATURDAY
            ON S.SATURDAY = SATURDAY.ID
          LEFT JOIN AT_TIME_MANUAL M
            ON S.MANUAL_ID = M.ID
          LEFT JOIN AT_TIME_MANUAL M1
            ON S.SUNDAY = M1.ID
         WHERE S.ACTFLG = 'A'
         --ORDER BY S.CODE;
         UNION
         SELECT S.ID,
               S.CODE,
               S.NAME_VN,
               '[' || M.CODE || '] - ' || M.NAME MANUAL_NAME,
               CASE
                 WHEN S.IS_NOON = -1 THEN
                  'X'
                 ELSE
                  ''
               END IS_NOON,
               M1.NAME SUNDAY_NAME,
               SATURDAY.NAME_VN SATURDAY_NAME,
               S.HOURS_START,
               S.HOURS_STOP,
               S.HOURS_STAR_CHECKIN,
               S.HOURS_STAR_CHECKOUT,
               S.NOTE
          FROM AT_SHIFT S
         
          LEFT JOIN AT_SHIFT SATURDAY
            ON S.SATURDAY = SATURDAY.ID
          LEFT JOIN AT_TIME_MANUAL M
            ON S.MANUAL_ID = M.ID
          LEFT JOIN AT_TIME_MANUAL M1
            ON S.SUNDAY = M1.ID
         WHERE S.ACTFLG = 'A' AND S.CODE='OFF' OR S.CODE='WFH';
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
    
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.GETDATAFROMORG',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              null,
                              null,
                              null,
                              null);
  END;

  PROCEDURE CHECK_DELETE_SIGNWORK(P_ID IN NVARCHAR2) IS
  BEGIN
    INSERT INTO TEMP_SIGN VALUES (P_ID, 'CHECK_DELETE_SIGNWORK');
  END;

  PROCEDURE IMPORT_ENTITLEMENT_LEAVE(P_DOCXML IN CLOB,
                                     P_USER   IN NVARCHAR2,
                                     P_PERIOD IN NUMBER) IS
  
    v_DOCXML XMLTYPE;
    PV_YEAR  NUMBER;
    PV_MONTH NUMBER;
  BEGIN
    v_DOCXML := XMLTYPE.createXML(P_DOCXML);
  
    SELECT P.YEAR, P.MONTH
      INTO PV_YEAR, PV_MONTH
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD;
  
    DELETE AT_ENTITLEMENT_OUTSIDE_COMPANY S
     WHERE S.YEAR = PV_YEAR
       AND S.MONTH = PV_MONTH
       AND S.EMPLOYEE_ID IN
           (SELECT DOM.EMPLOYEE_CODE
              FROM XMLTABLE('/NewDataSet/Data' PASSING v_DOCXML COLUMNS
                            EMPLOYEE_CODE NUMBER PATH './EMPLOYEE_CODE') DOM);
  
    COMMIT;
  
    INSERT INTO AT_ENTITLEMENT_OUTSIDE_COMPANY
      (EMPLOYEE_ID,
       YEAR,
       MONTH,
       OUTSIDE_COMPANY,
       CREATED_LOG,
       CREATED_DATE)
      SELECT E.EMPLOYEE_CODE, PV_YEAR, PV_MONTH, E.LEAVE, P_USER, SYSDATE
        FROM XMLTABLE('/NewDataSet/Data' PASSING v_DOCXML COLUMNS
                      EMPLOYEE_CODE NUMBER PATH './EMPLOYEE_CODE',
                      LEAVE NUMBER PATH './LEAVE') E;
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.IMPORT_ENTITLEMENT_LEAVE',
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
                              NULL,
                              NULL,
                              NULL,
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

  PROCEDURE CALL_ENTITLEMENT_HOSE(P_USERNAME   VARCHAR2,
                                  P_ORG_ID     IN NUMBER,
                                  P_PERIOD_ID  IN NUMBER,
                                  P_ISDISSOLVE IN NUMBER) IS
    PV_FROMDATE    DATE;
    PV_ENDDATE     DATE;
    PV_REQUEST_ID  NUMBER;
    PV_YEAR        NUMBER;
    PV_SQL         CLOB;
    PV_TOTAL_P     NUMBER;
    PV_RESET_MONTH NUMBER;
    PV_MONTH       NUMBER;
    PV_MONTH_P     NUMBER;
    PV_YEAR_TN     NUMBER;
    PV_DAY_TN      NUMBER;
    PV_I           NUMBER;
    PV_CAL_I       NUMBER;
    PV_STRING      VARCHAR2(2000);
    PVC_ENDDATE    DATE;
    PV_PERIOD      NUMBER;
    PVC_CAL_I      NUMBER;
    PVC_YEAR       NUMBER;
    PVC_FROMDATE   DATE;
    PVC_MONTHDATE  NVARCHAR2(10);
    PV_REQUEST_ID1 NUMBER;
    PVC_MONTH      NUMBER;
    PV_CHEKDATE    DATE;
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
    SELECT TO_DATE('15/' || TO_CHAR(P.END_DATE, 'MM/yyyy'), 'dd/MM/yyyy'),
           P.START_DATE,
           P.END_DATE,
           P.YEAR,
           P.MONTH
      INTO PV_CHEKDATE, PV_FROMDATE, PV_ENDDATE, PV_YEAR, PV_MONTH
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
  
    SELECT t.year_p,
           T.TO_LEAVE_YEAR,
           ROUND(to_number(t.year_p) / 12, 2),
           T.YEAR_TN,
           T.DAY_TN
      INTO PV_TOTAL_P, PV_RESET_MONTH, PV_MONTH_P, PV_YEAR_TN, PV_DAY_TN
      FROM (select t.year_p,
                   TO_CHAR(T.TO_LEAVE_YEAR, 'MM') TO_LEAVE_YEAR,
                   T.YEAR_TN,
                   T.DAY_TN
              from AT_LIST_PARAM_SYSTEM t
             WHERE TO_CHAR(T.EFFECT_DATE_FROM, 'yyyy') <= PV_YEAR
               AND T.ACTFLG = 'A'
             ORDER BY T.EFFECT_DATE_FROM DESC) T
     WHERE ROWNUM = 1;
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG1 E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
  
    FOR PV_CAL_I IN PV_MONTH .. PV_MONTH + 1 LOOP
      IF PV_CAL_I = 13 THEN
        PVC_CAL_I := 1;
        PVC_YEAR  := PV_YEAR + 1;
      ELSE
        PVC_CAL_I := PV_CAL_I;
        PVC_YEAR  := PV_YEAR;
      END IF;
      PV_REQUEST_ID1 := SEQ_AT_REQUEST.NEXTVAL;
    
      SELECT TO_DATE('01/' || TO_CHAR(P.END_DATE, 'MM/yyyy'), 'dd/MM/yyyy'),
             ADD_MONTHS(TO_DATE('01/' || TO_CHAR(P.END_DATE, 'MM/yyyy'),
                                'dd/MM/yyyy'),
                        1) - 1,
             P.ID,
             TO_CHAR(P.END_DATE, 'yyyyMM'),
             P.MONTH
        INTO PVC_FROMDATE, PVC_ENDDATE, PV_PERIOD, PVC_MONTHDATE, PVC_MONTH
        FROM AT_PERIOD P
       WHERE P.YEAR = PVC_YEAR
         AND P.MONTH = PVC_CAL_I;
    
      INSERT INTO AT_CHOSEN_EMP_TEMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE,
         CONTRACT_TYPE,
         CONTRACT_DATE)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID1,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE,
                CT.CONTRACT_TYPE,
                CT.CONTRACT_DATE
           FROM HU_EMPLOYEE T
          INNER JOIN (SELECT E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE ASC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PVC_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
           LEFT JOIN (SELECT E.EMPLOYEE_ID,
                            O.CODE CONTRACT_TYPE,
                            E.START_DATE CONTRACT_DATE,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.START_DATE ASC) AS ROW_NUMBER
                       FROM HU_CONTRACT E
                      INNER JOIN HU_CONTRACT_TYPE S
                         ON S.ID = E.CONTRACT_TYPE_ID
                      INNER JOIN OT_OTHER_LIST O
                         ON O.ID = S.TYPE_ID
                      WHERE E.START_DATE <= PVC_ENDDATE
                        AND E.STATUS_ID = 447) CT
             ON T.ID = CT.EMPLOYEE_ID
            AND CT.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID
                       FROM AT_CHOSEN_ORG1 O
                      WHERE O.REQUEST_ID = PV_REQUEST_ID) O
             ON O.ORG_ID = W.ORG_ID
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PVC_FROMDATE)));
    
      DELETE FROM AT_ENTITLEMENT T
       WHERE EXISTS (SELECT a.ID
                FROM AT_ENTITLEMENT a
               INNER JOIN AT_CHOSEN_EMP_TEMP EMP
                  ON EMP.EMPLOYEE_ID = a.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID1
               WHERE T.EMPLOYEE_ID = EMP.EMPLOYEE_ID
                 AND a.YEAR = PVC_YEAR
                 AND a.MONTH = PVC_CAL_I)
         and t.YEAR = PVC_YEAR
         AND t.MONTH = PVC_CAL_I;
    
      INSERT INTO AT_ENTITLEMENT T
        (ID,
         EMPLOYEE_ID,
         YEAR,
         MONTH,
         PERIOD_ID,
         WORKING_TIME_HAVE,
         TOTAL_HAVE,
         CREATED_BY,
         CREATED_DATE,
         --PREV_HAVE,
         PREV_USED,
         EXPIREDATE,
         CUR_HAVE,
         CUR_USED,
         FUND,
         SPECIAL,
         CUR_HAVE1,
         CUR_HAVE2,
         CUR_HAVE3,
         CUR_HAVE4,
         CUR_HAVE5,
         CUR_HAVE6,
         CUR_HAVE7,
         CUR_HAVE8,
         CUR_HAVE9,
         CUR_HAVE10,
         CUR_HAVE11,
         CUR_HAVE12,
         CUR_USED1,
         CUR_USED2,
         CUR_USED3,
         CUR_USED4,
         CUR_USED5,
         CUR_USED6,
         CUR_USED7,
         CUR_USED8,
         CUR_USED9,
         CUR_USED10,
         CUR_USED11,
         CUR_USED12,
         AL_TO_CASH,
         REASON,
         EXPIREDATE_NB,
         FIXADD,
         PAY_PREV_ENT,
         AL_T1,
         AL_T2,
         AL_T3,
         AL_T4,
         AL_T5,
         AL_T6,
         AL_T7,
         AL_T8,
         AL_T9,
         AL_T10,
         AL_T11,
         AL_T12,
         AL_ADD_T1,
         AL_ADD_T2,
         AL_ADD_T3,
         AL_ADD_T4,
         AL_ADD_T5,
         AL_ADD_T6,
         AL_ADD_T7,
         AL_ADD_T8,
         AL_ADD_T9,
         AL_ADD_T10,
         AL_ADD_T11,
         AL_ADD_T12,
         PREV_USED1,
         PREV_USED2,
         PREV_USED3,
         PREV_USED4,
         PREV_USED5,
         PREV_USED6,
         PREV_USED7,
         PREV_USED8,
         PREV_USED9,
         PREV_USED10,
         PREV_USED11,
         PREV_USED12,
         TOTAL_PAY_PREV_ENT,
         SUB_PREV_ENT,
         BALANCE_WORKING_TIME_3B,
         SENIORITYHAVE,
         TOTAL_HAVE1,
         SENIORITY,
         TIME_SENIORITY,
         MONTH_SENIORITY_CHANGE,
         TIME_SENIORITY_AFTER_CHANGE)
        SELECT SEQ_AT_ENTITLEMENT.NEXTVAL,
               EMP.EMPLOYEE_ID,
               PVC_YEAR,
               PVC_CAL_I,
               PV_PERIOD,
               EXTRACT(YEAR FROM EMP.JOIN_DATE),
               CASE
                 WHEN PVC_YEAR - EXTRACT(YEAR FROM EMP.JOIN_DATE) = 0 THEN
                  PV_TOTAL_P - CASE
                    WHEN EXTRACT(DAY FROM EMP.CONTRACT_DATE) >= 16 AND
                         EMP.CONTRACT_TYPE = 'HDTV' THEN
                     EXTRACT(MONTH FROM EMP.JOIN_DATE) + 1
                    ELSE
                     EXTRACT(MONTH FROM EMP.JOIN_DATE)
                  END
                 
                  + 1 + FN_CALL_TN(emp.employee_id,
                                   ADD_MONTHS(PVC_ENDDATE,
                                              NVL(B.ADJUST_MONTH_TN1, 0)),
                                   PV_YEAR_TN,
                                   PV_DAY_TN) + CASE
                    WHEN PVC_YEAR - EXTRACT(YEAR FROM EMP.TER_EFFECT_DATE) = 0 THEN
                     - (12 - EXTRACT(MONTH FROM EMP.TER_EFFECT_DATE) + CASE
                          WHEN EXTRACT(DAY FROM EMP.TER_EFFECT_DATE) >= 1 AND
                               EXTRACT(DAY FROM EMP.TER_EFFECT_DATE) <= 15 THEN
                           1
                          ELSE
                           0
                        END)
                    ELSE
                     0
                  END
                 ELSE
                  PV_TOTAL_P + FN_CALL_TN(emp.employee_id,
                                          ADD_MONTHS(PVC_ENDDATE,
                                                     NVL(B.ADJUST_MONTH_TN1, 0)),
                                          PV_YEAR_TN,
                                          PV_DAY_TN)
                 
                  + CASE
                    WHEN PVC_YEAR - EXTRACT(YEAR FROM EMP.TER_EFFECT_DATE) = 0 THEN
                     - (12 - EXTRACT(MONTH FROM EMP.TER_EFFECT_DATE) + CASE
                          WHEN EXTRACT(DAY FROM EMP.TER_EFFECT_DATE) >= 1 AND
                               EXTRACT(DAY FROM EMP.TER_EFFECT_DATE) <= 15 THEN
                           1
                          ELSE
                           0
                        END)
                    ELSE
                     0
                  END
               END,
               UPPER(P_USERNAME),
               SYSDATE,
               --PREV_HAVE,
               PREV_USED,
               EXPIREDATE,
               CUR_HAVE,
               CUR_USED,
               FUND,
               SPECIAL,
               CUR_HAVE1,
               CUR_HAVE2,
               CUR_HAVE3,
               CUR_HAVE4,
               CUR_HAVE5,
               CUR_HAVE6,
               CUR_HAVE7,
               CUR_HAVE8,
               CUR_HAVE9,
               CUR_HAVE10,
               CUR_HAVE11,
               CUR_HAVE12,
               CUR_USED1,
               CUR_USED2,
               CUR_USED3,
               CUR_USED4,
               CUR_USED5,
               CUR_USED6,
               CUR_USED7,
               CUR_USED8,
               CUR_USED9,
               CUR_USED10,
               CUR_USED11,
               CUR_USED12,
               AL_TO_CASH,
               REASON,
               EXPIREDATE_NB,
               FIXADD,
               PAY_PREV_ENT,
               AL_T1,
               AL_T2,
               AL_T3,
               AL_T4,
               AL_T5,
               AL_T6,
               AL_T7,
               AL_T8,
               AL_T9,
               AL_T10,
               AL_T11,
               AL_T12,
               AL_ADD_T1,
               AL_ADD_T2,
               AL_ADD_T3,
               AL_ADD_T4,
               AL_ADD_T5,
               AL_ADD_T6,
               AL_ADD_T7,
               AL_ADD_T8,
               AL_ADD_T9,
               AL_ADD_T10,
               AL_ADD_T11,
               AL_ADD_T12,
               PREV_USED1,
               PREV_USED2,
               PREV_USED3,
               PREV_USED4,
               PREV_USED5,
               PREV_USED6,
               PREV_USED7,
               PREV_USED8,
               PREV_USED9,
               PREV_USED10,
               PREV_USED11,
               PREV_USED12,
               TOTAL_PAY_PREV_ENT,
               SUB_PREV_ENT,
               BALANCE_WORKING_TIME_3B,
               SENIORITYHAVE,
               CASE
                 WHEN EMP.JOIN_DATE IS NULL THEN
                  0
                 WHEN PVC_YEAR - EXTRACT(YEAR FROM EMP.JOIN_DATE) = 0 THEN
                  PV_TOTAL_P - CASE
                    WHEN EXTRACT(DAY FROM EMP.CONTRACT_DATE) >= 16 AND
                         EMP.CONTRACT_TYPE = 'HDTV' THEN
                     EXTRACT(MONTH FROM EMP.JOIN_DATE) + 1
                    ELSE
                     EXTRACT(MONTH FROM EMP.JOIN_DATE)
                  END
                 
                  + 1 + CASE
                    WHEN PVC_YEAR - EXTRACT(YEAR FROM EMP.TER_EFFECT_DATE) = 0 THEN
                     - (12 - EXTRACT(MONTH FROM EMP.TER_EFFECT_DATE) + CASE
                          WHEN EXTRACT(DAY FROM EMP.TER_EFFECT_DATE) >= 1 AND
                               EXTRACT(DAY FROM EMP.TER_EFFECT_DATE) <= 15 THEN
                           1
                          ELSE
                           0
                        END)
                    ELSE
                     0
                  END
                 ELSE
                  PV_TOTAL_P
                 
                  + CASE
                    WHEN PVC_YEAR - EXTRACT(YEAR FROM EMP.TER_EFFECT_DATE) = 0 THEN
                     - (12 - EXTRACT(MONTH FROM EMP.TER_EFFECT_DATE) + CASE
                          WHEN EXTRACT(DAY FROM EMP.TER_EFFECT_DATE) >= 1 AND
                               EXTRACT(DAY FROM EMP.TER_EFFECT_DATE) <= 15 THEN
                           1
                          ELSE
                           0
                        END)
                    ELSE
                     0
                  END
               END,
               (CASE
                 WHEN TRUNC(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1,
                                                 EMP.JOIN_DATE)) / 12) > 0 AND
                      MOD(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1,
                                               EMP.JOIN_DATE)),
                          12) > 0 THEN
                  TRUNC(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1, EMP.JOIN_DATE)) / 12) ||
                  TO_CHAR(UNISTR(' n\0103m ')) || CASE
                    WHEN MOD(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE, EMP.JOIN_DATE)),
                             12) < 1 THEN
                     TO_CHAR(MOD(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1,
                                                      EMP.JOIN_DATE)),
                                 12),
                             '0D0')
                    ELSE
                     TO_CHAR(MOD(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1,
                                                      EMP.JOIN_DATE)),
                                 12))
                  END || TO_CHAR(UNISTR(' th\00E1ng'))
                 WHEN TRUNC(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1,
                                                 EMP.JOIN_DATE)) / 12) = 0 AND
                      MOD(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1,
                                               EMP.JOIN_DATE)),
                          12) > 0 THEN
                  CASE
                    WHEN MOD(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1,
                                                  EMP.JOIN_DATE)),
                             12) < 1 THEN
                     TO_CHAR(MOD(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1,
                                                      EMP.JOIN_DATE)),
                                 12),
                             '0D0')
                    ELSE
                     TO_CHAR(MOD(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1,
                                                      EMP.JOIN_DATE)),
                                 12))
                  END || TO_CHAR(UNISTR(' th\00E1ng'))
                 WHEN TRUNC(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1,
                                                 EMP.JOIN_DATE)) / 12) > 0 AND
                      MOD(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1,
                                               EMP.JOIN_DATE)),
                          12) = 0 THEN
                  TRUNC(TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1, EMP.JOIN_DATE)) / 12) ||
                  TO_CHAR(UNISTR(' n\0103m'))
                 ELSE
                  NULL
               END),
               TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1, EMP.JOIN_DATE)),
               NVL(B.ADJUST_MONTH_TN1, 0),
               TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1, EMP.JOIN_DATE)) +
               NVL(B.ADJUST_MONTH_TN1, 0)
          FROM AT_CHOSEN_EMP_TEMP EMP
          LEFT JOIN AT_ENTITLEMENT A
            ON A.EMPLOYEE_ID = EMP.EMPLOYEE_ID
           AND A.YEAR = PVC_YEAR
           AND A.MONTH = PVC_CAL_I - 1
          LEFT JOIN (SELECT ENT1.EMPLOYEE_ID,
                            SUM(ENT1.ADJUST_MONTH_TN) ADJUST_MONTH_TN1
                       FROM AT_DECLARE_ENTITLEMENT ENT1
                      WHERE ENT1.YEAR <= PVC_YEAR
                        AND EXISTS
                      (SELECT EMP.EMPLOYEE_ID
                               FROM AT_CHOSEN_EMP_TEMP EMP
                              WHERE EMP.EMPLOYEE_ID = ENT1.EMPLOYEE_ID
                                AND EMP.REQUEST_ID = PV_REQUEST_ID1)
                      GROUP BY ENT1.EMPLOYEE_ID) B
            ON B.EMPLOYEE_ID = EMP.EMPLOYEE_ID
         WHERE EMP.REQUEST_ID = PV_REQUEST_ID1;
    
      -- TINH  SO NGAY PHEP BI TRU NGOAI CO QUAN
      PV_SQL := '
      MERGE INTO AT_ENTITLEMENT A
     USING (SELECT  COM.EMPLOYEE_ID, COM.OUTSIDE_COMPANY
            FROM AT_ENTITLEMENT_OUTSIDE_COMPANY COM
            WHERE COM.YEAR = ' || PVC_YEAR || '
              AND COM.MONTH = ' || PVC_CAL_I || '
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP_TEMP EMP
             WHERE EMP.EMPLOYEE_ID = COM.EMPLOYEE_ID
             AND EMP.REQUEST_ID = ' || PV_REQUEST_ID1 ||
                ')) B
      ON (a.EMPLOYEE_ID = b.EMPLOYEE_ID)
    WHEN MATCHED THEN
      UPDATE SET A.AL_T' || PVC_CAL_I || ' = NVL(B.OUTSIDE_COMPANY,0)
    WHERE A.YEAR = ' || PVC_YEAR || '
       AND A.MONTH = ' || PVC_CAL_I || '
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP_TEMP EMP
             WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
             AND EMP.REQUEST_ID = ' || PV_REQUEST_ID1 || ')';
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
    
      --Update so phep tu bang cong thang qua
      PV_SQL := 'UPDATE AT_ENTITLEMENT ENT SET ENT.CUR_USED' ||
                EXTRACT(MONTH FROM PVC_ENDDATE) ||
                ' = NVL((SELECT SUM(T.DAY_NUM) FROM AT_LEAVESHEET T
                       INNER JOIN AT_TIME_MANUAL M
                       ON M.ID = T.MANUAL_ID
                   WHERE TO_CHAR(T.WORKINGDAY,''yyyyMM'') = ''' ||
                PVC_MONTHDATE || '''
                   AND CODE LIKE ''%P%''
                   AND T.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                   GROUP BY T.EMPLOYEE_ID ),0) + NVL(ENT.AL_T' ||
                EXTRACT(MONTH FROM PVC_ENDDATE) || ',0)
                WHERE ENT.YEAR = ' || PVC_YEAR || '
                AND ENT.MONTH = ' || PVC_CAL_I || '
                    AND EXISTS(SELECT EMP.EMPLOYEE_ID FROM AT_CHOSEN_EMP_TEMP EMP
                                WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                                AND EMP.REQUEST_ID = ' ||
                PV_REQUEST_ID1 || ')';
      /*INSERT INTO AT_STRSQL(ID,STRINGSQL)
      VALUES(PV_REQUEST_ID,PV_SQL); */
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
    
      FOR PV_I IN 1 .. PV_RESET_MONTH LOOP
        IF PV_STRING IS NOT NULL THEN
          PV_STRING := PV_STRING || ' + ' || 'NVL(A.CUR_USED' || PV_I ||
                       ',0)';
        ELSE
          PV_STRING := 'NVL(A.CUR_USED' || PV_I || ',0)';
        END IF;
      END LOOP;
      -- Tinh so ngay nghi phep tu dau nam den ngay reset p nam truoc
      PV_SQL := 'UPDATE AT_ENTITLEMENT A
     SET A.PREV_USED =  ' || PV_STRING || '
     WHERE A.YEAR =  ' || PVC_YEAR || '
       AND A.MONTH = ' || PVC_CAL_I || '
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP_TEMP EMP
             WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
             AND EMP.REQUEST_ID = ' || PV_REQUEST_ID1 || ')';
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
    
      -- Tinh so phep nam truoc con lai
      MERGE INTO AT_ENTITLEMENT A
      USING (SELECT ENT1.EMPLOYEE_ID, ENT1.PREV_HAVE
               FROM AT_ENTITLEMENT_PREV_HAVE ENT1
              WHERE ENT1.YEAR = PVC_YEAR
                AND EXISTS (SELECT EMP.EMPLOYEE_ID
                       FROM AT_CHOSEN_EMP_TEMP EMP
                      WHERE EMP.EMPLOYEE_ID = ENT1.EMPLOYEE_ID
                        AND EMP.REQUEST_ID = PV_REQUEST_ID1)) B
      ON (a.EMPLOYEE_ID = b.EMPLOYEE_ID)
      WHEN MATCHED THEN
        UPDATE
           SET A.PREV_HAVE = CASE
                               WHEN NVL(B.PREV_HAVE, 0) < 0 THEN
                                0
                               ELSE
                                NVL(B.PREV_HAVE, 0)
                             END
         WHERE A.YEAR = PVC_YEAR
           AND A.MONTH = PVC_CAL_I
           AND EXISTS (SELECT EMP.EMPLOYEE_ID
                  FROM AT_CHOSEN_EMP_TEMP EMP
                 WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                   AND EMP.REQUEST_ID = PV_REQUEST_ID1);
    
      -- TINH LAI SO PHEP NAM TRUOC DA NGHI
      UPDATE AT_ENTITLEMENT A
         SET A.PREV_USED = CASE
                             WHEN NVL(A.PREV_HAVE, 0) = 0 THEN
                              0
                             WHEN NVL(A.PREV_HAVE, 0) < NVL(A.PREV_USED, 0) THEN
                              A.PREV_HAVE
                             ELSE
                              A.PREV_USED
                           END,
             A.SENIORITY_EDIT = (CASE
                                  WHEN TRUNC(A.TIME_SENIORITY_AFTER_CHANGE / 12) > 0 AND
                                       MOD(A.TIME_SENIORITY_AFTER_CHANGE, 12) > 0 THEN
                                   TRUNC(A.TIME_SENIORITY_AFTER_CHANGE / 12) ||
                                   TO_CHAR(UNISTR(' n\0103m ')) || CASE
                                     WHEN MOD(A.TIME_SENIORITY_AFTER_CHANGE, 12) < 1 THEN
                                      TO_CHAR(MOD(A.TIME_SENIORITY_AFTER_CHANGE,
                                                  12),
                                              '0D0')
                                     ELSE
                                      TO_CHAR(MOD(A.TIME_SENIORITY_AFTER_CHANGE,
                                                  12))
                                   END || TO_CHAR(UNISTR(' th\00E1ng'))
                                  WHEN TRUNC(A.TIME_SENIORITY_AFTER_CHANGE / 12) = 0 AND
                                       MOD(A.TIME_SENIORITY_AFTER_CHANGE, 12) > 0 THEN
                                   CASE
                                     WHEN MOD(A.TIME_SENIORITY_AFTER_CHANGE, 12) < 1 THEN
                                      TO_CHAR(MOD(A.TIME_SENIORITY_AFTER_CHANGE,
                                                  12),
                                              '0D0')
                                     ELSE
                                      TO_CHAR(MOD(A.TIME_SENIORITY_AFTER_CHANGE,
                                                  12))
                                   END || TO_CHAR(UNISTR(' th\00E1ng'))
                                  WHEN TRUNC(A.TIME_SENIORITY_AFTER_CHANGE / 12) > 0 AND
                                       MOD(A.TIME_SENIORITY_AFTER_CHANGE, 12) = 0 THEN
                                   TRUNC(A.TIME_SENIORITY_AFTER_CHANGE / 12) ||
                                   TO_CHAR(UNISTR(' n\0103m'))
                                  ELSE
                                   NULL
                                END)
       WHERE A.YEAR = PVC_YEAR
         AND A.MONTH = PVC_CAL_I
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP_TEMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID1);
    
      -- Tinh so phep nam truoc con lai su dung v? duoc reset vao theo thang thiet lap
    
      IF PVC_MONTH > PV_RESET_MONTH THEN
        UPDATE AT_ENTITLEMENT A
           SET A.PREVTOTAL_HAVE = 0
         WHERE A.YEAR = PVC_YEAR
           AND A.MONTH = PVC_CAL_I
           AND EXISTS (SELECT EMP.EMPLOYEE_ID
                  FROM AT_CHOSEN_EMP_TEMP EMP
                 WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                   AND EMP.REQUEST_ID = PV_REQUEST_ID1);
      ELSE
        UPDATE AT_ENTITLEMENT A
           SET A.PREVTOTAL_HAVE = NVL(A.PREV_HAVE, 0) - NVL(A.PREV_USED, 0)
         WHERE A.YEAR = PVC_YEAR
           AND A.MONTH = PVC_CAL_I
           AND EXISTS (SELECT EMP.EMPLOYEE_ID
                  FROM AT_CHOSEN_EMP_TEMP EMP
                 WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                   AND EMP.REQUEST_ID = PV_REQUEST_ID1);
      END IF;
    
      --Tinh toan lai so phep da sd
      UPDATE AT_ENTITLEMENT ENT
         SET ENT.CUR_USED             = NVL(ENT.CUR_USED1, 0) +
                                        NVL(ENT.CUR_USED2, 0) +
                                        NVL(ENT.CUR_USED3, 0) +
                                        NVL(ENT.CUR_USED4, 0) +
                                        NVL(ENT.CUR_USED5, 0) +
                                        NVL(ENT.CUR_USED6, 0) +
                                        NVL(ENT.CUR_USED7, 0) +
                                        NVL(ENT.CUR_USED8, 0) +
                                        NVL(ENT.CUR_USED9, 0) +
                                        NVL(ENT.CUR_USED10, 0) +
                                        NVL(ENT.CUR_USED11, 0) +
                                        NVL(ENT.CUR_USED12, 0) -
                                        NVL(ENT.PREV_USED, 0),
             ENT.SENIORITYHAVE        = FN_CALL_TN(ENT.employee_id,
                                                   ADD_MONTHS(PVC_ENDDATE,
                                                              NVL(ENT.MONTH_SENIORITY_CHANGE,
                                                                  0)),
                                                   PV_YEAR_TN,
                                                   PV_DAY_TN),
             ENT.TIME_OUTSIDE_COMPANY = NVL(ENT.AL_T1, 0) +
                                        NVL(ENT.AL_T2, 0) +
                                        NVL(ENT.AL_T3, 0) +
                                        NVL(ENT.AL_T4, 0) +
                                        NVL(ENT.AL_T5, 0) +
                                        NVL(ENT.AL_T6, 0) +
                                        NVL(ENT.AL_T7, 0) +
                                        NVL(ENT.AL_T8, 0) +
                                        NVL(ENT.AL_T9, 0) +
                                        NVL(ENT.AL_T10, 0) +
                                        NVL(ENT.AL_T11, 0) +
                                        NVL(ENT.AL_T12, 0)
       WHERE ENT.YEAR = PVC_YEAR
         AND ENT.MONTH = PVC_CAL_I
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP_TEMP EMP
               WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID1);
    
      --Tinh toan lai so phep con lai
      IF PV_RESET_MONTH >= PVC_CAL_I THEN
        UPDATE AT_ENTITLEMENT ENT
           SET ENT.CUR_HAVE = NVL(ENT.PREV_HAVE, 0) + NVL(ENT.TOTAL_HAVE, 0) -
                              NVL(ENT.CUR_USED, 0) - NVL(ENT.PREV_USED, 0)
         WHERE ENT.YEAR = PVC_YEAR
           AND ENT.MONTH = PVC_CAL_I
           AND EXISTS (SELECT EMP.EMPLOYEE_ID
                  FROM AT_CHOSEN_EMP_TEMP EMP
                 WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                   AND EMP.REQUEST_ID = PV_REQUEST_ID1);
      ELSE
        UPDATE AT_ENTITLEMENT ENT
           SET ENT.CUR_HAVE = NVL(ENT.TOTAL_HAVE, 0) - NVL(ENT.CUR_USED, 0)
         WHERE ENT.YEAR = PVC_YEAR
           AND ENT.MONTH = PVC_CAL_I
           AND EXISTS (SELECT EMP.EMPLOYEE_ID
                  FROM AT_CHOSEN_EMP_TEMP EMP
                 WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                   AND EMP.REQUEST_ID = PV_REQUEST_ID1);
      
      END IF;
      IF PVC_YEAR = 2019 AND PVC_CAL_I = 6 THEN
        MERGE INTO AT_ENTITLEMENT A
        USING (SELECT P.EMPLOYEE_ID,
                      P.PREV_HAVE_2018,
                      P.CUR_USED6_2018,
                      P.PREV_USED_2018,
                      P.PREVTOTAL_HAVE_2018,
                      P.CUR_USED_2018
                 FROM AT_ENTITLEMENT_2018 P
                WHERE EXISTS (SELECT EMP.EMPLOYEE_ID
                         FROM AT_CHOSEN_EMP_TEMP EMP
                        WHERE EMP.EMPLOYEE_ID = P.EMPLOYEE_ID
                          AND EMP.REQUEST_ID = PV_REQUEST_ID1)) B
        ON (a.EMPLOYEE_ID = b.EMPLOYEE_ID)
        WHEN MATCHED THEN
          UPDATE
             SET A.PREV_HAVE      = NVL(B.PREV_HAVE_2018, 0),
                 A.CUR_USED6      = NVL(B.CUR_USED6_2018, 0),
                 A.PREV_USED      = NVL(B.PREV_USED_2018, 0),
                 A.PREVTOTAL_HAVE = NVL(B.PREVTOTAL_HAVE_2018, 0),
                 A.CUR_USED       = NVL(B.CUR_USED_2018, 0),
                 A.CUR_HAVE       = NVL(A.CUR_HAVE, 0) +
                                    NVL(B.PREVTOTAL_HAVE_2018, 0) +
                                    NVL(B.PREV_USED_2018, 0) -
                                    NVL(B.CUR_USED6_2018, 0) +
                                    NVL(A.TIME_OUTSIDE_COMPANY, 0)
          -- A.TOTAL_HAVE = NVL(A.TOTAL_HAVE, 0) - NVL(B.CUR_USED6_2018, 0)
           WHERE A.YEAR = PVC_YEAR
             AND A.MONTH = PVC_CAL_I
             AND EXISTS
           (SELECT EMP.EMPLOYEE_ID
                    FROM AT_CHOSEN_EMP_TEMP EMP
                   WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                     AND EMP.REQUEST_ID = PV_REQUEST_ID1);
      END IF;
      DELETE AT_CHOSEN_EMP_TEMP E WHERE E.REQUEST_ID = PV_REQUEST_ID1;
      COMMIT;
      PV_REQUEST_ID1 := 0;
      PV_STRING      := NULL;
    END LOOP;
  
    DELETE AT_CHOSEN_ORG1 A WHERE A.REQUEST_ID = PV_REQUEST_ID;
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      DELETE AT_CHOSEN_EMP_TEMP E WHERE E.REQUEST_ID = PV_REQUEST_ID1;
      DELETE AT_CHOSEN_ORG1 A WHERE A.REQUEST_ID = PV_REQUEST_ID;
      COMMIT;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_AT_ANNUAL.CALL_ENTITLEMENT_HOSE',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              null,
                              null,
                              null,
                              null);
    
  END;

  PROCEDURE AT_ENTITLEMENT_PREV_HAVE(P_USERNAME   VARCHAR2,
                                     P_ORG_ID     IN NUMBER,
                                     P_PERIOD_ID  IN NUMBER,
                                     P_ISDISSOLVE IN NUMBER) IS
    PV_FROMDATE   DATE;
    PV_ENDDATE    DATE;
    PV_REQUEST_ID NUMBER;
    PV_YEAR       NUMBER;
    PV_MONTH      NUMBER;
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
    SELECT P.START_DATE, P.END_DATE, EXTRACT(YEAR FROM P.END_DATE), p.month
      INTO PV_FROMDATE, PV_ENDDATE, PV_YEAR, PV_MONTH
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
  
    INSERT INTO AT_CHOSEN_EMP
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       TER_EFFECT_DATE,
       USERNAME,
       REQUEST_ID,
       JOIN_DATE,
       JOIN_DATE_STATE)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              CASE
                WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                 T.TER_EFFECT_DATE + 1
                ELSE
                 NULL
              END TER_EFFECT_DATE,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              T.JOIN_DATE,
              T.JOIN_DATE_STATE
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_ENDDATE
                      AND E.STATUS_ID = 447
                      AND E.IS_3B = 0) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
           ON O.ORG_ID = W.ORG_ID
        WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  
    MERGE INTO AT_ENTITLEMENT_PREV_HAVE A
    USING (SELECT ENT1.EMPLOYEE_ID, ENT1.TOTAL_HAVE, ENT1.YEAR
             FROM AT_ENTITLEMENT ENT1
            WHERE ENT1.YEAR = PV_YEAR
              AND ENT1.MONTH = 12
              AND EXISTS
            (SELECT EMP.EMPLOYEE_ID
                     FROM AT_CHOSEN_EMP EMP
                    WHERE EMP.EMPLOYEE_ID = ENT1.EMPLOYEE_ID)) B
    ON (a.EMPLOYEE_ID = b.EMPLOYEE_ID AND A.YEAR = PV_YEAR + 1)
    WHEN MATCHED THEN
      UPDATE
         SET A.PREV_HAVE = NVL(B.TOTAL_HAVE, 0)
       WHERE A.YEAR = PV_YEAR + 1
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID)
    WHEN NOT MATCHED THEN
      INSERT
        (A.EMPLOYEE_ID, A.YEAR, A.PREV_HAVE)
      VALUES
        (B.EMPLOYEE_ID, PV_YEAR + 1, B.TOTAL_HAVE);
  
    MERGE INTO AT_ENTITLEMENT A
    USING (SELECT ENT1.EMPLOYEE_ID, ENT1.TOTAL_HAVE
             FROM AT_ENTITLEMENT ENT1
            WHERE ENT1.YEAR = PV_YEAR
              AND ENT1.MONTH = 12
              AND EXISTS
            (SELECT EMP.EMPLOYEE_ID
                     FROM AT_CHOSEN_EMP EMP
                    WHERE EMP.EMPLOYEE_ID = ENT1.EMPLOYEE_ID)) B
    ON (a.EMPLOYEE_ID = b.EMPLOYEE_ID)
    WHEN MATCHED THEN
      UPDATE
         SET A.TOTAL_HAVE     = NVL(B.TOTAL_HAVE, 0),
             A.PREVTOTAL_HAVE = CASE
                                  WHEN NVL(B.TOTAL_HAVE, 0) < 0 THEN
                                   0
                                  ELSE
                                   NVL(B.TOTAL_HAVE, 0)
                                END
      
       WHERE A.YEAR = PV_YEAR + 1
         AND A.MONTH = 1
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID);
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.AT_ENTITLEMENT_PREV_HAVE',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              null,
                              null,
                              null,
                              null);
    
  END;

  /*
  PROCEDURE GET_CCT_ORIGIN(P_USERNAME      IN NVARCHAR2,
                           P_ORG_ID        IN NUMBER,
                           P_ISDISSOLVE    IN NUMBER,
                           P_EMPLOYEE_CODE IN NVARCHAR2,
                           P_EMPLOYEE_NAME IN NVARCHAR2,
                           P_ORG_NAME      IN NVARCHAR2,
                           P_TITLE_NAME    IN NVARCHAR2,
                           P_PERIOD_ID     IN NUMBER,
                           P_TERMINATE     IN NUMBER,
                           P_CUR           OUT CURSOR_TYPE) IS
    PV_STARTDATE DATE;
    PV_ENDDATE   DATE;
  BEGIN
  
    IF P_PERIOD_ID IS NULL OR P_PERIOD_ID = 0 THEN
      PV_STARTDATE := TO_DATE('01/01/2016', 'dd/MM/yyyy');
      PV_ENDDATE   := TO_DATE('31/01/2016', 'dd/MM/yyyy');
    ELSE
      SELECT P.START_DATE, P.END_DATE
        INTO PV_STARTDATE, PV_ENDDATE
        FROM AT_PERIOD P
       WHERE P.ID = P_PERIOD_ID;
    END IF;
  
    PKG_COMMON_LIST.INSERT_CHOSEN_ORG(P_USERNAME, P_ORG_ID, P_ISDISSOLVE);
  
    OPEN P_CUR FOR
      SELECT ROWNUM             TT,
             EE.ID              EMPLOYEE_ID,
             EE.EMPLOYEE_CODE,
             EE.FULLNAME_VN     VN_FULLNAME,
             O.NAME_VN          ORG_NAME,
             O.DESCRIPTION_PATH ORG_DESC,
             TI.NAME_VN         TITLE_NAME,
             S.NAME             STAFF_RANK_NAME,
             A.D1,
             A.D2,
             A.D3,
             A.D4,
             A.D5,
             A.D6,
             A.D7,
             A.D8,
             A.D9,
             A.D10,
             A.D11,
             A.D12,
             A.D13,
             A.D14,
             A.D15,
             A.D16,
             A.D17,
             A.D18,
             A.D19,
             A.D20,
             A.D21,
             A.D22,
             A.D23,
             A.D24,
             A.D25,
             A.D26,
             A.D27,
             A.D28,
             A.D29,
             A.D30,
             A.D31
        FROM (SELECT T.EMPLOYEE_ID,
                     T.ORG_ID,
                     T.TITLE_ID,
                     TO_NUMBER(TO_CHAR(T.WORKINGDAY, 'dd')) AS DAY,
                     NVL(L.CODE, NULL) CODE
                FROM AT_TIME_TIMESHEET_ORIGIN T
               INNER JOIN SE_CHOSEN_ORG CHOSEN
                  ON CHOSEN.ORG_ID = T.ORG_ID
                 AND CHOSEN.USERNAME = UPPER(P_USERNAME)
                LEFT JOIN AT_TIME_MANUAL L
                  ON T.MANUAL_ID = L.ID
               WHERE T.WORKINGDAY >= PV_STARTDATE
                 AND T.WORKINGDAY <= PV_ENDDATE)
      PIVOT(MAX(CODE)
         FOR DAY IN(1 AS D1,
                    2 AS D2,
                    3 AS D3,
                    4 AS D4,
                    5 AS D5,
                    6 AS D6,
                    7 AS D7,
                    8 AS D8,
                    9 AS D9,
                    10 AS D10,
                    11 AS D11,
                    12 AS D12,
                    13 AS D13,
                    14 AS D14,
                    15 AS D15,
                    16 AS D16,
                    17 AS D17,
                    18 AS D18,
                    19 AS D19,
                    20 AS D20,
                    21 AS D21,
                    22 AS D22,
                    23 AS D23,
                    24 AS D24,
                    25 AS D25,
                    26 AS D26,
                    27 AS D27,
                    28 AS D28,
                    29 AS D29,
                    30 AS D30,
                    31 AS D31)) A
       INNER JOIN HU_EMPLOYEE EE
          ON EE.ID = A.EMPLOYEE_ID
        LEFT JOIN HU_ORGANIZATION O
          ON O.ID = A.ORG_ID
        LEFT JOIN HU_TITLE TI
          ON TI.ID = A.TITLE_ID
        LEFT JOIN HU_STAFF_RANK S
          ON S.ID = EE.STAFF_RANK_ID
       WHERE (P_EMPLOYEE_CODE IS NULL OR
             (P_EMPLOYEE_CODE IS NOT NULL AND
             EE.EMPLOYEE_CODE LIKE '%' || P_EMPLOYEE_CODE || '%'))
         AND (P_EMPLOYEE_NAME IS NULL OR
             (P_EMPLOYEE_NAME IS NOT NULL AND
             UPPER(EE.FULLNAME_VN) LIKE
             '%' || UPPER(P_EMPLOYEE_NAME) || '%'))
         AND (P_ORG_NAME IS NULL OR
             (P_ORG_NAME IS NOT NULL AND
             UPPER(O.NAME_VN) LIKE '%' || UPPER(P_ORG_NAME) || '%'))
         AND (P_TITLE_NAME IS NULL OR
             (P_TITLE_NAME IS NOT NULL AND
             UPPER(TI.NAME_VN) LIKE '%' || UPPER(P_TITLE_NAME) || '%'))
         AND (NVL(EE.WORK_STATUS, 0) <> 257 OR
             (NVL(EE.WORK_STATUS, 0) = 257 AND
             EE.TER_LAST_DATE >= PV_STARTDATE));
  
  END;
  
  
  
  PROCEDURE GET_SUMMARY_NB(P_USERNAME        IN NVARCHAR2,
                           P_ORG_ID          IN NUMBER,
                           P_ISDISSOLVE      IN NUMBER,
                           P_PAGE_INDEX      IN NUMBER,
                           P_EMPLOYEE_CODE   IN VARCHAR2,
                           P_PAGE_SIZE       IN NUMBER,
                           P_PERIOD_ID       IN NUMBER,
                           P_EMPLOYEE_NAME   IN NVARCHAR2,
                           P_ORG_NAME        IN NVARCHAR2,
                           P_TITLE_NAME      IN NVARCHAR2,
                           P_STAFF_RANK_NAME IN NVARCHAR2,
                           P_CUR             OUT CURSOR_TYPE,
                           P_CURCOUNR        OUT CURSOR_TYPE) IS
    PV_STARTDATE DATE;
    PV_ENDDATE   DATE;
  BEGIN
    SELECT P.START_DATE, P.END_DATE
      INTO PV_STARTDATE, PV_ENDDATE
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
  
    OPEN P_CUR FOR
      SELECT *
        FROM (SELECT ROWNUM                 TT,
                     A.ID,
                     EE.ID                  EMPLOYEE_ID,
                     EE.EMPLOYEE_CODE,
                     EE.FULLNAME_VN         VN_FULLNAME,
                     O.NAME_VN              ORG_NAME,
                     O.DESCRIPTION_PATH     ORG_DESC,
                     TI.NAME_VN             TITLE_NAME,
                     S.NAME                 STAFF_RANK_NAME,
                     A.D1,
                     A.D2,
                     A.D3,
                     A.D4,
                     A.D5,
                     A.D6,
                     A.D7,
                     A.D8,
                     A.D9,
                     A.D10,
                     A.D11,
                     A.D12,
                     A.D13,
                     A.D14,
                     A.D15,
                     A.D16,
                     A.D17,
                     A.D18,
                     A.D19,
                     A.D20,
                     A.D21,
                     A.D22,
                     A.D23,
                     A.D24,
                     A.D25,
                     A.D26,
                     A.D27,
                     A.D28,
                     A.D29,
                     A.D30,
                     A.D31,
                     A.TOTAL_FACTOR1,
                     A.TOTAL_FACTOR1_5,
                     A.TOTAL_FACTOR2,
                     A.TOTAL_FACTOR2_7,
                     A.TOTAL_FACTOR3,
                     A.TOTAL_FACTOR3_9,
                     A.TOTAL_FACTOR_CONVERT
                FROM AT_TIME_TIMESHEET_NB A
               INNER JOIN HU_EMPLOYEE EE
                  ON EE.ID = A.EMPLOYEE_ID
               INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, P_ISDISSOLVE)) O1
                  ON A.ORG_ID = O1.ORG_ID
                LEFT JOIN HU_ORGANIZATION O
                  ON O.ID = A.ORG_ID
                LEFT JOIN HU_TITLE TI
                  ON TI.ID = EE.TITLE_ID
                LEFT JOIN HU_STAFF_RANK S
                  ON S.ID = EE.STAFF_RANK_ID
               WHERE A.PERIOD_ID = P_PERIOD_ID
                 AND (P_EMPLOYEE_CODE IS NULL OR
                     (P_EMPLOYEE_CODE IS NOT NULL AND
                     EE.EMPLOYEE_CODE LIKE '%' || P_EMPLOYEE_CODE || '%'))
                 AND (P_EMPLOYEE_NAME IS NULL OR
                     (P_EMPLOYEE_NAME IS NOT NULL AND
                     UPPER(EE.FULLNAME_VN) LIKE
                     '%' || UPPER(P_EMPLOYEE_NAME) || '%'))
                 AND (P_ORG_NAME IS NULL OR
                     (P_ORG_NAME IS NOT NULL AND
                     UPPER(O.NAME_VN) LIKE '%' || UPPER(P_ORG_NAME) || '%'))
                 AND (P_TITLE_NAME IS NULL OR
                     (P_TITLE_NAME IS NOT NULL AND
                     UPPER(TI.NAME_VN) LIKE
                     '%' || UPPER(P_TITLE_NAME) || '%'))
                 AND (P_STAFF_RANK_NAME IS NULL OR
                     (P_STAFF_RANK_NAME IS NOT NULL AND
                     UPPER(S.NAME) LIKE
                     '%' || UPPER(P_STAFF_RANK_NAME) || '%'))
                 AND (NVL(EE.WORK_STATUS, 0) <> 257 OR
                     (NVL(EE.WORK_STATUS, 0) = 257 AND
                     EE.TER_LAST_DATE >= PV_STARTDATE))) C
       WHERE C.TT < (P_PAGE_INDEX * P_PAGE_SIZE) + 1
         AND C.TT >= ((P_PAGE_INDEX - 1) * P_PAGE_SIZE) + 1;
    OPEN P_CURCOUNR FOR
      SELECT COUNT(*) TOTAL
        FROM AT_TIME_TIMESHEET_NB A
       INNER JOIN HU_EMPLOYEE EE
          ON EE.ID = A.EMPLOYEE_ID
       INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, P_ISDISSOLVE)) O
          ON A.ORG_ID = O.ORG_ID
        LEFT JOIN HU_STAFF_RANK S
          ON S.ID = EE.STAFF_RANK_ID
        LEFT JOIN HU_TITLE TI
          ON TI.ID = EE.TITLE_ID
        LEFT JOIN HU_ORGANIZATION O
          ON O.ID = A.ORG_ID
       WHERE A.PERIOD_ID = P_PERIOD_ID
         AND (P_EMPLOYEE_CODE IS NULL OR
             (P_EMPLOYEE_CODE IS NOT NULL AND
             EE.EMPLOYEE_CODE LIKE '%' || P_EMPLOYEE_CODE || '%'))
         AND (P_EMPLOYEE_NAME IS NULL OR
             (P_EMPLOYEE_NAME IS NOT NULL AND
             UPPER(EE.FULLNAME_VN) LIKE
             '%' || UPPER(P_EMPLOYEE_NAME) || '%'))
         AND (P_ORG_NAME IS NULL OR
             (P_ORG_NAME IS NOT NULL AND
             UPPER(O.NAME_VN) LIKE '%' || UPPER(P_ORG_NAME) || '%'))
         AND (P_TITLE_NAME IS NULL OR
             (P_TITLE_NAME IS NOT NULL AND
             UPPER(TI.NAME_VN) LIKE '%' || UPPER(P_TITLE_NAME) || '%'))
         AND (P_STAFF_RANK_NAME IS NULL OR
             (P_STAFF_RANK_NAME IS NOT NULL AND
             UPPER(S.NAME) LIKE '%' || UPPER(P_STAFF_RANK_NAME) || '%'))
         AND (NVL(EE.WORK_STATUS, 0) <> 257 OR
             (NVL(EE.WORK_STATUS, 0) = 257 AND
             EE.TER_LAST_DATE >= PV_STARTDATE));
  END;
  
  PROCEDURE GET_SUMMARY_RICE(P_USERNAME        IN NVARCHAR2,
                             P_ORG_ID          IN NUMBER,
                             P_ISDISSOLVE      IN NUMBER,
                             P_PAGE_INDEX      IN NUMBER,
                             P_EMPLOYEE_CODE   IN VARCHAR2,
                             P_PAGE_SIZE       IN NUMBER,
                             P_PERIOD_ID       IN NUMBER,
                             P_EMPLOYEE_NAME   IN VARCHAR2,
                             P_ORG_NAME        IN NVARCHAR2,
                             P_TITLE_NAME      IN NVARCHAR2,
                             P_STAFF_RANK_NAME IN NVARCHAR2,
                             P_CUR             OUT CURSOR_TYPE,
                             P_CURCOUNR        OUT CURSOR_TYPE) IS
    PV_STARTDATE DATE;
    PV_ENDDATE   DATE;
  BEGIN
  
    SELECT P.START_DATE, P.END_DATE
      INTO PV_STARTDATE, PV_ENDDATE
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
  
    OPEN P_CUR FOR
      SELECT C.*
        FROM (SELECT ROWNUM               TT,
                     A.ID,
                     EE.ID                EMPLOYEE_ID,
                     EE.EMPLOYEE_CODE,
                     EE.FULLNAME_VN       VN_FULLNAME,
                     O.NAME_VN            ORG_NAME,
                     O.DESCRIPTION_PATH   ORG_DESC,
                     TI.NAME_VN           TITLE_NAME,
                     S.NAME               STAFF_RANK_NAME,
                     A.D1,
                     A.D2,
                     A.D3,
                     A.D4,
                     A.D5,
                     A.D6,
                     A.D7,
                     A.D8,
                     A.D9,
                     A.D10,
                     A.D11,
                     A.D12,
                     A.D13,
                     A.D14,
                     A.D15,
                     A.D16,
                     A.D17,
                     A.D18,
                     A.D19,
                     A.D20,
                     A.D21,
                     A.D22,
                     A.D23,
                     A.D24,
                     A.D25,
                     A.D26,
                     A.D27,
                     A.D28,
                     A.D29,
                     A.D30,
                     A.D31,
                     A.NDAY_RICE,
                     A.TOTAL_RICE_PRICE,
                     A.TOTAL_RICE_DECLARE,
                     A.RICE_EDIT,
                     A.TOTAL_RICE
                FROM AT_TIME_TIMESHEET_RICE A
               INNER JOIN HU_EMPLOYEE EE
                  ON EE.ID = A.EMPLOYEE_ID
               INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, P_ISDISSOLVE)) O1
                  ON EE.ORG_ID = O1.ORG_ID
                LEFT JOIN HU_ORGANIZATION O
                  ON O.ID = A.ORG_ID
                LEFT JOIN HU_TITLE TI
                  ON TI.ID = EE.TITLE_ID
                LEFT JOIN HU_STAFF_RANK S
                  ON S.ID = EE.STAFF_RANK_ID
               WHERE A.PERIOD_ID = P_PERIOD_ID
                 AND (P_EMPLOYEE_CODE IS NULL OR
                     (P_EMPLOYEE_CODE IS NOT NULL AND
                     EE.EMPLOYEE_CODE LIKE '%' || P_EMPLOYEE_CODE || '%'))
                 AND (P_EMPLOYEE_NAME IS NULL OR
                     (P_EMPLOYEE_NAME IS NOT NULL AND
                     UPPER(EE.FULLNAME_VN) LIKE
                     '%' || UPPER(P_EMPLOYEE_NAME) || '%'))
                 AND (P_ORG_NAME IS NULL OR
                     (P_ORG_NAME IS NOT NULL AND
                     O.NAME_VN LIKE '%' || P_ORG_NAME || '%'))
                 AND (P_TITLE_NAME IS NULL OR
                     (P_TITLE_NAME IS NOT NULL AND
                     TI.NAME_VN LIKE '%' || P_TITLE_NAME || '%'))
                 AND (P_STAFF_RANK_NAME IS NULL OR
                     (P_STAFF_RANK_NAME IS NOT NULL AND
                     LOWER(S.NAME) LIKE
                     '%' || LOWER(P_STAFF_RANK_NAME) || '%'))
                 AND (NVL(EE.WORK_STATUS, 0) <> 257 OR
                     (NVL(EE.WORK_STATUS, 0) = 257 AND
                     EE.TER_LAST_DATE >= PV_STARTDATE))) C
       WHERE C.TT < (P_PAGE_INDEX * P_PAGE_SIZE) + 1
         AND C.TT >= ((P_PAGE_INDEX - 1) * P_PAGE_SIZE) + 1;
    OPEN P_CURCOUNR FOR
      SELECT COUNT(*) TOTAL
        FROM AT_TIME_TIMESHEET_RICE A
       INNER JOIN HU_EMPLOYEE EE
          ON EE.ID = A.EMPLOYEE_ID
       INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, P_ISDISSOLVE)) O1
          ON EE.ORG_ID = O1.ORG_ID
        LEFT JOIN HU_STAFF_RANK S
          ON S.ID = EE.STAFF_RANK_ID
        LEFT JOIN HU_TITLE TI
          ON TI.ID = EE.TITLE_ID
        LEFT JOIN HU_ORGANIZATION O
          ON O.ID = A.ORG_ID
       WHERE A.PERIOD_ID = P_PERIOD_ID
         AND ((P_EMPLOYEE_CODE IS NULL OR
             (P_EMPLOYEE_CODE IS NOT NULL AND
             EE.EMPLOYEE_CODE LIKE '%' || P_EMPLOYEE_CODE || '%')) OR
             (P_EMPLOYEE_CODE IS NULL OR
             (P_EMPLOYEE_CODE IS NOT NULL AND
             EE.FULLNAME_VN LIKE '%' || P_EMPLOYEE_CODE || '%')))
         AND (P_ORG_NAME IS NULL OR
             (P_ORG_NAME IS NOT NULL AND
             O.NAME_VN LIKE '%' || P_ORG_NAME || '%'))
         AND (P_TITLE_NAME IS NULL OR
             (P_TITLE_NAME IS NOT NULL AND
             TI.NAME_VN LIKE '%' || P_TITLE_NAME || '%'))
         AND (P_STAFF_RANK_NAME IS NULL OR
             (P_STAFF_RANK_NAME IS NOT NULL AND
             S.NAME LIKE '%' || P_STAFF_RANK_NAME || '%'))
         AND (NVL(EE.WORK_STATUS, 0) <> 257 OR
             (NVL(EE.WORK_STATUS, 0) = 257 AND
             EE.TER_LAST_DATE >= PV_STARTDATE));
  END;
  */
  -- DONG KY CONG
  PROCEDURE CLOSEDOPEN_PERIOD(P_USERNAME   NVARCHAR2,
                              P_ORG_ID     IN NUMBER,
                              P_ISDISSOLVE IN NUMBER,
                              P_STATUS     IN NUMBER,
                              P_PERIOD_ID  IN NUMBER) IS
  BEGIN
  
    PKG_COMMON_LIST.INSERT_CHOSEN_ORG(P_USERNAME, P_ORG_ID, P_ISDISSOLVE);
  
    UPDATE AT_ORG_PERIOD PO
       SET PO.STATUSCOLEX = P_STATUS
     WHERE (PO.ID, PO.ORG_ID) IN
           (SELECT PO.ID, PO.ORG_ID
              FROM AT_PERIOD P
             INNER JOIN AT_ORG_PERIOD PO
                ON P.ID = PO.PERIOD_ID
             INNER JOIN SE_CHOSEN_ORG O
                ON O.ORG_ID = PO.ORG_ID
               AND o.USERNAME = upper(P_USERNAME)
             WHERE P.ID = P_PERIOD_ID);
  END;
  /*
  
  
  PROCEDURE INSERT_TIME_CARD(P_TIMEID     IN NVARCHAR2,
                             P_TERMINALID IN NUMBER,
                             P_VALTIME    IN DATE,
                             P_USERNAME   IN NVARCHAR2) AS
  BEGIN
    MERGE INTO AT_SWIPE_DATA T
    USING (SELECT P_TIMEID TIMEID,
                  P_TERMINALID TERMINAL_ID,
                  P_VALTIME VALTIME,
                  TRUNC(P_VALTIME) WORKINGDAY
             FROM DUAL) D
    ON (T.ITIME_ID = D.TIMEID AND T.TERMINAL_ID = D.TERMINAL_ID AND TO_CHAR(T.VALTIME, 'yyyyMMdd HH24MI') = TO_CHAR(D.VALTIME, 'yyyyMMdd HH24MI'))
    WHEN NOT MATCHED THEN
      INSERT
        (ID, ITIME_ID, TERMINAL_ID, VALTIME, WORKINGDAY)
      VALUES
        (SEQ_AT_SWIPE_DATA.NEXTVAL,
         P_TIMEID,
         P_TERMINALID,
         P_VALTIME,
         D.WORKINGDAY);
  END;
  
  PROCEDURE INSERT_TIME_CARD_AUTO(P_TIMEID      IN NUMBER,
                                  P_TERMINAL_ID IN NUMBER,
                                  P_VALTIME     IN DATE,
                                  P_USERNAME    IN NVARCHAR2) AS
   PV_MAX NUMBER;
  BEGIN
    SELECT NVL((SELECT MAX(ID) MAXID FROM AT_SWIPE_DATA T), 0) + 1
      INTO PV_MAX
      FROM DUAL;
  
    MERGE INTO AT_SWIPE_DATA T
    USING (SELECT P_TIMEID TIMEID,
                  P_VALTIME VALTIME,
                  TRUNC(P_VALTIME) WORKINGDAY
             FROM DUAL) D
    ON (T.ITIME_ID = D.TIMEID AND TO_CHAR(T.VALTIME, 'yyyyMMdd HH24MI') = TO_CHAR(D.VALTIME, 'yyyyMMdd HH24MI'))
    WHEN NOT MATCHED THEN
      INSERT
        (ID,
         ITIME_ID,
         VALTIME,
         WORKINGDAY,
         TERMINAL_ID,
         CREATED_BY,
         CREATED_LOG,
         CREATED_DATE)
      VALUES
        (PV_MAX,
         P_TIMEID,
         P_VALTIME,
         D.WORKINGDAY,
         P_TERMINAL_ID,
         P_USERNAME,
         P_USERNAME,
         SYSDATE)
    WHEN MATCHED THEN
      UPDATE
         SET TERMINAL_ID   = P_TERMINAL_ID,
             MODIFIED_BY   = P_USERNAME,
             MODIFIED_LOG  = P_USERNAME,
             MODIFIED_DATE = SYSDATE;
  END;
  
  
  
  
  PROCEDURE UPDATE_LEAVESHEET_DAILY(P_STARTDATE IN DATE,
                                    P_ENDDATE   IN DATE,
                                    P_USERNAME  IN VARCHAR2) AS
    PV_START      NUMBER;
    PV_END        NUMBER;
    PV_START_CHAR VARCHAR2(100);
  BEGIN
    PV_START      := TO_CHAR(P_STARTDATE, 'yyyymmdd');
    PV_END        := TO_CHAR(P_ENDDATE, 'yyyymmdd');
    PV_START_CHAR := TO_CHAR(P_STARTDATE, 'yyyymm');
  
    INSERT INTO AT_TIME_TIMESHEET_DAILY_TEMP
      SELECT E.ID,
             E.EMPLOYEE_ID,
             ORG_ID,
             TITLE_ID,
             E.WORKINGDAY,
             SHIFT_CODE,
             LEAVE_CODE,
             LATE,
             COMEBACKOUT,
             WORKDAY_OT,
             WORKDAY_NIGHT,
             TYPE_DAY,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             VALIN1,
             VALIN2,
             VALIN3,
             VALIN4,
             DECISION_ID,
             PA_OBJECT_SALARY_ID,
             SHIFT_ID,
             EE.MANUAL_ID,
             LEAVE_ID,
             WORKINGHOUR,
             WORKINGHOUR_SHIFT,
             NUMBER_SWIPE,
             E.WORKING_MEAL
        FROM AT_TIME_TIMESHEET_DAILY E
       INNER JOIN (SELECT E.EMPLOYEE_ID,
                          TO_DATE(PV_START_CHAR || E.QUARTER, 'yyyymmdd') WORKINGDAY,
                          E.QUANTITY_SOLD MANUAL_ID
                     FROM (SELECT *
                             FROM AT_WORKSIGN_DATE_TEMP T UNPIVOT INCLUDE NULLS(QUANTITY_SOLD FOR QUARTER IN(D1 AS '01',
                                                                                                             D2 AS '02',
                                                                                                             D3 AS '03',
                                                                                                             D4 AS '04',
                                                                                                             D5 AS '05',
                                                                                                             D6 AS '06',
                                                                                                             D7 AS '07',
                                                                                                             D8 AS '08',
                                                                                                             D9 AS '09',
                                                                                                             D10 AS '10',
                                                                                                             D11 AS '11',
                                                                                                             D12 AS '12',
                                                                                                             D13 AS '13',
                                                                                                             D14 AS '14',
                                                                                                             D15 AS '15',
                                                                                                             D16 AS '16',
                                                                                                             D17 AS '17',
                                                                                                             D18 AS '18',
                                                                                                             D19 AS '19',
                                                                                                             D20 AS '20',
                                                                                                             D21 AS '21',
                                                                                                             D22 AS '22',
                                                                                                             D23 AS '23',
                                                                                                             D24 AS '24',
                                                                                                             D25 AS '25',
                                                                                                             D26 AS '26',
                                                                                                             D27 AS '27',
                                                                                                             D28 AS '28',
                                                                                                             D29 AS '29',
                                                                                                             D30 AS '30',
                                                                                                             D31 AS '31'))) E
                    WHERE PV_START_CHAR || E.QUARTER >= PV_START
                      AND PV_START_CHAR || E.QUARTER <= PV_END) EE
          ON E.EMPLOYEE_ID = EE.EMPLOYEE_ID
         AND E.WORKINGDAY = EE.WORKINGDAY
       WHERE E.WORKINGDAY >= P_STARTDATE
         AND E.WORKINGDAY <= P_ENDDATE;
  
    DELETE AT_TIME_TIMESHEET_DAILY E
     WHERE E.WORKINGDAY >= P_STARTDATE
       AND E.WORKINGDAY <= P_ENDDATE
       AND E.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_WORKSIGN_DATE_TEMP);
  
    INSERT INTO AT_TIME_TIMESHEET_DAILY
      SELECT ID,
             EMPLOYEE_ID,
             ORG_ID,
             TITLE_ID,
             WORKINGDAY,
             SHIFT_CODE,
             LEAVE_CODE,
             LATE,
             COMEBACKOUT,
             WORKDAY_OT,
             WORKDAY_NIGHT,
             TYPE_DAY,
             CREATED_DATE,
             CREATED_BY,
             CREATED_LOG,
             MODIFIED_DATE,
             MODIFIED_BY,
             MODIFIED_LOG,
             VALIN1,
             VALIN2,
             VALIN3,
             VALIN4,
             DECISION_ID,
             PA_OBJECT_SALARY_ID,
             SHIFT_ID,
             MANUAL_ID,
             LEAVE_ID,
             WORKINGHOUR,
             WORKINGHOUR_SHIFT,
             NUMBER_SWIPE,
             E.WORKING_MEAL
        FROM AT_TIME_TIMESHEET_DAILY_TEMP E;
  
  END;
  
  PROCEDURE CAL_TIME_TIMESHEET_MONTHLY(P_USERNAME   VARCHAR2,
                                       P_PERIOD_ID  IN NUMBER,
                                       P_ORG_ID     IN NUMBER,
                                       P_ISDISSOLVE IN NUMBER) IS
    PV_FROMDATE   DATE;
    PV_TODATE     DATE;
    PV_YEAR       NUMBER;
    PV_SQL        CLOB;
    PV_REQUEST_ID NUMBER;
    PV_WORKING_STANDARD NUMBER:= 26;
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
    SELECT P.START_DATE, P.END_DATE, EXTRACT(YEAR FROM P.END_DATE), P.PERIOD_STANDARD
      INTO PV_FROMDATE, PV_TODATE, PV_YEAR, PV_WORKING_STANDARD
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O
        WHERE EXISTS (SELECT 1
                 FROM AT_ORG_PERIOD OP
                WHERE OP.PERIOD_ID = P_PERIOD_ID
                  AND OP.ORG_ID = O.ORG_ID
                  AND OP.STATUSCOLEX = 1));
  
    -- insert emp can tinh toan
    INSERT INTO AT_CHOSEN_EMP
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       PA_OBJECT_SALARY_ID,
       USERNAME,
       REQUEST_ID,
       TER_EFFECT_DATE,
       JOIN_DATE,
       JOIN_DATE_STATE,
       EXPIREDATE_ENT)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              T.PA_OBJECT_SALARY_ID,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              CASE
                WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                 T.TER_EFFECT_DATE + 1
                ELSE
                 NULL
              END TER_EFFECT_DATE,
              T.JOIN_DATE,
              T.JOIN_DATE_STATE,
              TO_DATE(TO_CHAR(NVL(EE.EXPIREDATE_ENT,
                                  NVL(E_PARAM.TO_LEAVE_YEAR,
                                      TO_DATE('3103' || PV_YEAR, 'ddmmyyyy'))),
                              'ddmm') || PV_YEAR,
                      'ddmmyyyy')
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_TODATE
                      AND E.STATUS_ID = 447
                      AND E.IS_3B = 0) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
           ON O.ORG_ID = W.ORG_ID
         LEFT JOIN (SELECT E.EMPLOYEE_ID,
                          MAX(TO_DATE('01' ||
                                      TO_CHAR(E.START_MONTH_EXTEND, '00') ||
                                      E.YEAR_ENTITLEMENT,
                                      'ddmmyyyy')) EXPIREDATE_ENT
                     FROM AT_DECLARE_ENTITLEMENT E
                    WHERE E.START_MONTH_EXTEND IS NOT NULL
                      AND E.START_MONTH_EXTEND > 0
                      AND E.YEAR_ENTITLEMENT IS NOT NULL
                      AND E.YEAR_ENTITLEMENT = PV_YEAR
                      AND E.YEAR_ENTITLEMENT >= 1900
                    GROUP BY E.EMPLOYEE_ID) EE
           ON W.EMPLOYEE_ID = EE.EMPLOYEE_ID
         LEFT JOIN (SELECT *
                     FROM (SELECT E.TO_LEAVE_YEAR
                             FROM AT_LIST_PARAM_SYSTEM E
                            WHERE E.EFFECT_DATE_FROM <= PV_TODATE
                              AND E.TO_LEAVE_YEAR IS NOT NULL
                              AND E.ACTFLG = 'A')
                    WHERE ROWNUM = 1) E_PARAM
           ON 1 = 1
        WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  
    INSERT INTO AT_CHOSEN_EMP_CLEAR
      (EMPLOYEE_ID, REQUEST_ID)
      (SELECT EMPLOYEE_ID, PV_REQUEST_ID
         FROM (SELECT A.*,
                      ROW_NUMBER() OVER(PARTITION BY A.EMPLOYEE_ID ORDER BY A.EFFECT_DATE DESC, A.ID DESC) AS ROW_NUMBER
                 FROM HU_WORKING A
                WHERE A.STATUS_ID = 447
                  AND A.EFFECT_DATE <= PV_TODATE
                  AND A.IS_3B = 0) C
        INNER JOIN HU_EMPLOYEE EE
           ON C.EMPLOYEE_ID = EE.ID
          AND C.ROW_NUMBER = 1
        WHERE (NVL(EE.WORK_STATUS, 0) <> 257 OR
              (EE.WORK_STATUS = 257 AND EE.TER_LAST_DATE >= PV_FROMDATE)));
  
    ---------------------------------------------------------------------------------
    -- XOA BO DU LIEU CU TRONG BANG TAM
    ---------------------------------------------------------------------------------
    DELETE AT_TIME_TIMESHEET_DAILY_TEM T
    \* WHERE T.WORKINGDAY >= PV_FROMDATE
       AND T.WORKINGDAY <= PV_TODATE
       AND T.EMPLOYEE_ID IN (SELECT EE.EMPLOYEE_ID FROM AT_CHOSEN_EMP EE)*\;
  
    ---------------------------------------------------------------------------------
    -- INSERT DU LIEU MOI CAN TINH TRONG BANG TAM
    ---------------------------------------------------------------------------------
    INSERT INTO AT_TIME_TIMESHEET_DAILY_TEM
      (EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       WORKINGDAY,
       SHIFT_CODE,
       LEAVE_CODE,
       LATE,
       COMEBACKOUT,
       WORKING_MEAL,
       WORKDAY_OT,
       MANUAL_ID,
       SHIFT_ID,
       VALIN1,
       VALIN2,
       VALIN3,
       VALIN4,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG,
       PA_OBJECT_SALARY_ID)
      SELECT T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             WORKINGDAY,
             SHIFT_CODE,
             LEAVE_CODE,
             LATE,
             COMEBACKOUT,
             WORKING_MEAL,
             WORKDAY_OT,
             MANUAL_ID,
             SHIFT_ID,
             VALIN1,
             VALIN2,
             VALIN3,
             VALIN4,
             CREATED_DATE,
             CREATED_BY,
             CREATED_LOG,
             MODIFIED_DATE,
             MODIFIED_BY,
             MODIFIED_LOG,
             EE.PA_OBJECT_SALARY_ID
        FROM AT_CHOSEN_EMP EE
        LEFT JOIN AT_TIME_TIMESHEET_DAILY T
          ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
         AND (EE.TER_EFFECT_DATE IS NULL OR
             (EE.TER_EFFECT_DATE IS NOT NULL AND
             T.WORKINGDAY < EE.TER_EFFECT_DATE))
         AND T.WORKINGDAY >= EE.JOIN_DATE
         AND T.WORKINGDAY >= PV_FROMDATE
         AND T.WORKINGDAY <= PV_TODATE;
  
    -- XOA DU LIEU CU CAN TINH TRONG BANG TONG HOP
    ---------------------------------------------------------------------------------
    DELETE AT_TIME_TIMESHEET_MONTHLY T
     WHERE T.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
       AND T.PERIOD_ID = P_PERIOD_ID;
    ---------------------------------------------------------------------------------
    -- INSERT DU LIEU VAO BANG TONG HOP CONG
    ---------------------------------------------------------------------------------
    INSERT INTO AT_TIME_MONTHLY_TEMP
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       DECISION_ID,
       SALARY_ID,
       DECISION_START,
       DECISION_END,
       PA_OBJECT_SALARY_ID,
       PERIOD_ID,
       FROM_DATE,
       END_DATE,
       LATE,
       COMEBACKOUT,
       WORKING_MEAL,
       WORKING_X,
       WORKING_F,
       WORKING_E,
       WORKING_A,
       WORKING_H,
       WORKING_D,
       WORKING_C,
       WORKING_T,
       WORKING_Q,
       WORKING_N,
       WORKING_P,
       WORKING_L,
       WORKING_R,
       --WORKING_S,
       WORKING_B,
       WORKING_K,
       WORKING_J,
       WORKING_TS,
       WORKING_O,
       WORKING_V,
       WORKING_ADD,
       WORKING_STANDARD,
       REQUEST_ID)
      \*WITH MAX_SALARY AS
       (SELECT T.ORG_ID,
               T.TITLE_ID,
               T.EMPLOYEE_ID,
               T.ID,
               T.START_DATE,
               T.STAFF_RANK_ID,
               T.END_DATE
          FROM (SELECT E.*
                  FROM (SELECT W.ORG_ID,
                               W.EMPLOYEE_ID,
                               W.ID,
                               W.TITLE_ID,
                               W.STAFF_RANK_ID,
                               W.EFFECT_DATE START_DATE,
                               PV_TODATE END_DATE,
                               ROW_NUMBER() OVER(PARTITION BY W.EMPLOYEE_ID ORDER BY W.EFFECT_DATE DESC, W.ID DESC) RN
                          FROM HU_WORKING W
                         WHERE W.STATUS_ID = 447
                           AND W.IS_3B = 0
                           AND W.IS_WAGE = -1
                           AND W.EFFECT_DATE <= PV_TODATE) E
                 WHERE E.RN = 1) T)*\
       WITH MAX_WORKING AS
       (SELECT T.ORG_ID,
               T.TITLE_ID,
               T.EMPLOYEE_ID,
               T.ID,
               T.START_DATE,
               T.STAFF_RANK_ID,
               T.END_DATE
          FROM (SELECT E.*
                  FROM (SELECT W.ORG_ID,
                               W.EMPLOYEE_ID,
                               W.ID,
                               W.TITLE_ID,
                               W.STAFF_RANK_ID,
                               W.EFFECT_DATE START_DATE,
                               PV_TODATE END_DATE,
                               ROW_NUMBER() OVER(PARTITION BY W.EMPLOYEE_ID ORDER BY W.EFFECT_DATE DESC, W.ID DESC) RN
                          FROM HU_WORKING W
                         WHERE W.STATUS_ID = 447
                           AND W.IS_3B = 0
                           AND W.IS_WAGE = 0
                           AND W.EFFECT_DATE <= PV_TODATE) E
                 WHERE E.RN = 1) T)
      SELECT SEQ_AT_TIME_MONTHLY_TEMP.NEXTVAL,
             T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             T.STAFF_RANK_ID,
             T.DECISION_ID,
             T.SALARY_ID,
             CASE
               WHEN PV_FROMDATE > T.START_DATE THEN
                PV_FROMDATE
               ELSE
                T.START_DATE
             END START_DATE,
             T.END_DATE,
             T.PA_OBJECT_SALARY_ID,
             P_PERIOD_ID,
             PV_FROMDATE,
             PV_TODATE,
             LATE,
             COMEBACKOUT,
             WORKING_MEAL,
             CASE
               WHEN WORKING_X = 0 THEN
                NULL
               ELSE
                WORKING_X
             END WORKING_X,
             CASE
               WHEN WORKING_F = 0 THEN
                NULL
               ELSE
                WORKING_F
             END WORKING_F,
             CASE
               WHEN WORKING_E = 0 THEN
                NULL
               ELSE
                WORKING_E
             END WORKING_E,
             CASE
               WHEN WORKING_A = 0 THEN
                NULL
               ELSE
                WORKING_A
             END WORKING_A,
             CASE
               WHEN WORKING_H = 0 THEN
                NULL
               ELSE
                WORKING_H
             END WORKING_H,
             CASE
               WHEN WORKING_D = 0 THEN
                NULL
               ELSE
                WORKING_D
             END WORKING_D,
             CASE
               WHEN WORKING_C = 0 THEN
                NULL
               ELSE
                WORKING_C
             END WORKING_C,
             CASE
               WHEN WORKING_T = 0 THEN
                NULL
               ELSE
                WORKING_T
             END WORKING_T,
             CASE
               WHEN WORKING_Q = 0 THEN
                NULL
               ELSE
                WORKING_Q
             END WORKING_Q,
             CASE
               WHEN WORKING_N = 0 THEN
                NULL
               ELSE
                WORKING_N
             END WORKING_N,
             CASE
               WHEN WORKING_P = 0 THEN
                NULL
               ELSE
                WORKING_P
             END WORKING_P,
             CASE
               WHEN WORKING_L = 0 THEN
                NULL
               ELSE
                WORKING_L
             END WORKING_L,
             CASE
               WHEN WORKING_R = 0 THEN
                NULL
               ELSE
                WORKING_R
             END WORKING_R,
             \*CASE
               WHEN WORKING_S = 0 THEN
                NULL
               ELSE
                WORKING_S
             END WORKING_S,*\
             CASE
               WHEN WORKING_B = 0 THEN
                NULL
               ELSE
                WORKING_B
             END WORKING_B,
             CASE
               WHEN WORKING_K = 0 THEN
                NULL
               ELSE
                WORKING_K
             END WORKING_K,
             CASE
               WHEN WORKING_J = 0 THEN
                NULL
               ELSE
                WORKING_J
             END WORKING_J,
             CASE
               WHEN WORKING_TS = 0 THEN
                NULL
               ELSE
                WORKING_TS
             END WORKING_TS,
             CASE
               WHEN WORKING_O = 0 THEN
                NULL
               ELSE
                WORKING_O
             END WORKING_O,
             CASE
               WHEN WORKING_V = 0 THEN
                NULL
               ELSE
                WORKING_V
             END WORKING_V,
             CASE
               WHEN WORKING_ADD = 0 THEN
                NULL
               ELSE
                WORKING_ADD
             END WORKING_ADD,
             PV_WORKING_STANDARD,
             PV_REQUEST_ID
        FROM (SELECT A.EMPLOYEE_ID,
                     EE.ORG_ID,
                     W.TITLE_ID,
                     W.ID DECISION_ID,
                     W.START_DATE,
                     W.END_DATE,
                     W.STAFF_RANK_ID,
                     A.PA_OBJECT_SALARY_ID,
                     S.ID SALARY_ID,
                     SUM(LATE) LATE,
                     SUM(COMEBACKOUT) COMEBACKOUT,
                     SUM(WORKING_MEAL) WORKING_MEAL,
                     \*map ky hieu AT_FML voi collumn trong AT_TIME_TIMESHEET_DAILY*\
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'X' THEN
                            1
                           WHEN (F.CODE = 'X' OR F2.CODE = 'X') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_X, --==Ngay di lam
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'OFF' THEN
                            1
                           WHEN (F.CODE = 'OFF' OR F2.CODE = 'OFF') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_F, --==OFF
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'LT' THEN
                            1
                           WHEN (F.CODE = 'LT' OR F2.CODE = 'LT') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_L, --==Ngay Le Tet
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'DD' THEN
                            1
                           WHEN (F.CODE = 'DD' OR F2.CODE = 'DD') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_D, --==Ngay di duong
                     SUM(CASE
                           WHEN F.CODE = 'TS' THEN
                            1
                           ELSE
                            0
                         END) WORKING_TS, --== Nghi thai san
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'O' THEN
                            1
                           WHEN (F.CODE = 'O' OR F2.CODE = 'O') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_O, --==nghi om
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'P' THEN
                            1
                           WHEN (F.CODE = 'P' OR F2.CODE = 'P') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_P, --==nghi phep
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'B' THEN
                            1
                           WHEN (F.CODE = 'B' OR F2.CODE = 'B') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_B, --==nghi bu
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'CT' THEN
                            1
                           WHEN (F.CODE = 'CT' OR F2.CODE = 'CT') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_C, --== cong tac
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'E' THEN
                            1
                           WHEN (F.CODE = 'E' OR F2.CODE = 'E') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_E,
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'DH' THEN
                            1
                           WHEN (F.CODE = 'DH' OR F2.CODE = 'DH') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_A, --==nghi di hoc
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'VR' THEN
                            1
                           WHEN (F.CODE = 'VR' OR F2.CODE = 'VR') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_V, --==Nghi viec rieng
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'BCTS' THEN
                            1
                           WHEN (F.CODE = 'BCTS' OR F2.CODE = 'BCTS') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_H, --==Nghi bien chung thai san
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'KT' THEN
                            1
                           WHEN (F.CODE = 'KT' OR F2.CODE = 'KT') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_Q, --==Nghi kham thai
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'VS' THEN
                            1
                           WHEN (F.CODE = 'VS' OR F2.CODE = 'VS') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_N, --==Lao dong Nam nghi vo sinh
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'KH' THEN
                            1
                           WHEN (F.CODE = 'KH' OR F2.CODE = 'KH') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_R, --==Nghi ket hon
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'TG' THEN
                            1
                           WHEN (F.CODE = 'TG' OR F2.CODE = 'TG') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_T, --==Nghi tang gia
                     \*SUM(CASE
                       WHEN F.ID = F2.ID AND M.CODE = 'TG' THEN
                        1
                       WHEN (F.CODE = 'TG' OR F2.CODE = 'TG') AND F.ID <> F2.ID THEN
                        .5
                       ELSE
                        0
                     END) WORKING_S,*\ --==Nghi tang
  
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'NKL' THEN
                            1
                           WHEN (F.CODE = 'NKL' OR F2.CODE = 'NKL') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_K, --==Nghi KHONG huong luong
                     SUM(CASE
                           WHEN F.ID = F2.ID AND F.CODE = 'NCL' THEN
                            1
                           WHEN (F.CODE = 'NCL' OR F2.CODE = 'NCL') AND F.ID <> F2.ID THEN
                            .5
                           ELSE
                            0
                         END) WORKING_J, --==Nghi co huong luong
                     0 WORKING_ADD
                FROM AT_TIME_TIMESHEET_DAILY_TEM A
               INNER JOIN (SELECT E.*
                             FROM (SELECT W.ORG_ID,
                                          W.EMPLOYEE_ID,
                                          W.ID,
                                          W.TITLE_ID,
                                          W.STAFF_RANK_ID,
                                          W.EFFECT_DATE START_DATE,
                                          PV_TODATE END_DATE,
                                          ROW_NUMBER() OVER(PARTITION BY W.EMPLOYEE_ID ORDER BY W.EFFECT_DATE DESC, W.ID DESC) RN
                                     FROM HU_WORKING W
                                    WHERE W.STATUS_ID = 447
                                      AND W.IS_3B = 0
                                      AND W.IS_MISSION = -1
                                      AND W.EFFECT_DATE <= PV_TODATE) E
                            WHERE E.RN = 1) W
                  ON A.EMPLOYEE_ID = W.EMPLOYEE_ID
               INNER JOIN (SELECT E.*
                             FROM (SELECT W.ID,
                                          W.EMPLOYEE_ID,
                                          ROW_NUMBER() OVER(PARTITION BY W.EMPLOYEE_ID ORDER BY W.EFFECT_DATE DESC, W.ID DESC) RN
                                     FROM HU_WORKING W
                                    WHERE W.STATUS_ID = 447
                                      AND W.IS_3B = 0
                                      AND W.IS_WAGE = -1
                                      AND W.EFFECT_DATE <= PV_TODATE) E
                            WHERE E.RN = 1) S
                  ON A.EMPLOYEE_ID = S.EMPLOYEE_ID
               INNER JOIN AT_CHOSEN_EMP EE
                  ON W.EMPLOYEE_ID = EE.EMPLOYEE_ID
                LEFT JOIN AT_TIME_MANUAL M
                  ON A.MANUAL_ID = M.ID
                LEFT JOIN AT_FML F
                  ON M.MORNING_ID = F.ID
                LEFT JOIN AT_FML F2
                  ON M.AFTERNOON_ID = F2.ID
                LEFT JOIN AT_SHIFT S
                  ON A.SHIFT_ID = S.ID
               GROUP BY A.EMPLOYEE_ID,
                        W.ID,
                        S.ID,
                        W.START_DATE,
                        W.END_DATE,
                        EE.ORG_ID,
                        W.TITLE_ID,
                        W.STAFF_RANK_ID,
                        A.PA_OBJECT_SALARY_ID) T;
  
    ---------------------------------------------------------------------------------
    -- INSERT DU LIEU VAO BANG TONG HOP CONG
  
    --------------------------------------------------------------------------------------
    -- AP DUNG CONG THUC TINH CHO CAC COT TREN BANG CONG TONG HOP
    --------------------------------------------------------------------------------------
    FOR CUR_ITEM IN (SELECT *
                       FROM AT_TIME_FORMULAR T
                      WHERE T.TYPE IN (2)
                        AND T.STATUS = 1
                      ORDER BY T.FORMULAR_ID) LOOP
  
      PV_SQL := 'UPDATE AT_TIME_MONTHLY_TEMP T SET ' ||
                CUR_ITEM.FORMULAR_CODE || '= NVL((' ||
                CUR_ITEM.FORMULAR_VALUE || '),0)';
  
      EXECUTE IMMEDIATE PV_SQL;
  
    END LOOP;
   \* update AT_TIME_MONTHLY_TEMP t
       set t.working_da =
           (select ROUND(sum(NVL(ap.hours, 0)) / 8, 2)
              from at_project_assign ap
             where ap.employee_id = t.employee_id
               and ap.workingday between t.from_date and t.end_date);*\
    INSERT INTO AT_TIME_TIMESHEET_MONTHLY
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       DECISION_ID,
       SALARY_ID,
       DECISION_START,
       DECISION_END,
       PA_OBJECT_SALARY_ID,
       PERIOD_ID,
       FROM_DATE,
       END_DATE,
       WORKING_X,
       WORKING_F,
       WORKING_E,
       WORKING_A,
       WORKING_H,
       WORKING_D,
       WORKING_C,
       WORKING_T,
       WORKING_Q,
       WORKING_N,
       WORKING_P,
       WORKING_L,
       WORKING_R,
       WORKING_S,
       WORKING_B,
       WORKING_K,
       WORKING_J,
       WORKING_TS,
       WORKING_O,
       WORKING_V,
       WORKING_ADD,
       TOTAL_W_SALARY,
       TOTAL_W_NOSALARY,
       TOTAL_WORKING,
       WORKING_DA,
       LATE,
       COMEBACKOUT,
       WORKING_MEAL,
       WORKING_STANDARD,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG)
      SELECT SEQ_AT_TIME_TIMESHEET_MONTHLY.NEXTVAL,
             T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             T.STAFF_RANK_ID,
             T.DECISION_ID,
             T.SALARY_ID,
             T.DECISION_START,
             T.DECISION_END,
             T.PA_OBJECT_SALARY_ID,
             T.PERIOD_ID,
             T.FROM_DATE,
             T.END_DATE,
             T.WORKING_X,
             T.WORKING_F,
             T.WORKING_E,
             T.WORKING_A,
             T.WORKING_H,
             T.WORKING_D,
             T.WORKING_C,
             T.WORKING_T,
             T.WORKING_Q,
             T.WORKING_N,
             T.WORKING_P,
             T.WORKING_L,
             T.WORKING_R,
             T.WORKING_S,
             T.WORKING_B,
             T.WORKING_K,
             T.WORKING_J,
             T.WORKING_TS,
             T.WORKING_O,
             T.WORKING_V,
             T.WORKING_ADD,
             T.TOTAL_W_SALARY,
             T.TOTAL_W_NOSALARY,
             T.TOTAL_WORKING,
             T.WORKING_DA,
             T.LATE,
             T.COMEBACKOUT,
             T.WORKING_MEAL,
             P.PERIOD_STANDARD,
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME),
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME)
        FROM AT_TIME_MONTHLY_TEMP T
        INNER JOIN AT_PERIOD P
          ON T.PERIOD_ID = P.ID;
  
    DELETE AT_TIME_TIMESHEET_DAILY E
     WHERE E.WORKINGDAY >= PV_FROMDATE
       AND E.WORKINGDAY <= PV_TODATE
       AND E.EMPLOYEE_ID NOT IN
           (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);
  
   \* DELETE AT_TIME_TIMESHEET_ORIGIN E
     WHERE E.WORKINGDAY >= PV_FROMDATE
       AND E.WORKINGDAY <= PV_TODATE
       AND E.EMPLOYEE_ID NOT IN
           (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);*\
  
    DELETE AT_TIME_TIMESHEET_MONTHLY E
     WHERE E.PERIOD_ID = P_PERIOD_ID
       AND E.EMPLOYEE_ID NOT IN
           (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);
    COMMIT;
    CALL_ENTITLEMENT(P_USERNAME, P_ORG_ID, P_PERIOD_ID, P_ISDISSOLVE);
    COMMIT;
    CALL_ENTITLEMENT_NB(P_USERNAME, P_ORG_ID, P_PERIOD_ID, P_ISDISSOLVE);
    COMMIT;
    \*INSERT_INS_CHANGE(P_USERNAME, P_ORG_ID, P_PERIOD_ID, P_ISDISSOLVE);*\
     EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_MONTHLY',
                              SQLERRM || '_' || DBMS_UTILITY.format_error_backtrace,
                              P_ORG_ID,
                              P_PERIOD_ID,
                              P_USERNAME,
                              PV_REQUEST_ID,
                              PV_SQL);
  END;
  PROCEDURE INSERT_INS_CHANGE(P_USERNAME   IN VARCHAR2,
                              P_ORG_ID     IN NUMBER,
                              P_PERIOD_ID  IN NUMBER,
                              P_ISDISSOLVE IN NUMBER) AS
    PV_FROMDATE   DATE;
    PV_TODATE     DATE;
    PV_YEAR       NUMBER;
    PV_REQUEST_ID NUMBER;
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    -----------------------------------------------------------------
    -- LAY NGAY BAT DAU KET THUC CUA KY CONG
    -----------------------------------------------------------------
    SELECT P.START_DATE, P.END_DATE, EXTRACT(YEAR FROM P.END_DATE)
      INTO PV_FROMDATE, PV_TODATE, PV_YEAR
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
  
    ----------------------------------------------------------------
    -- Insert org can tinh toan
    ----------------------------------------------------------------
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O
        WHERE EXISTS (SELECT 1
                 FROM AT_ORG_PERIOD OP
                WHERE OP.PERIOD_ID = P_PERIOD_ID
                  AND OP.ORG_ID = O.ORG_ID
                  AND OP.STATUSCOLEX = 1));
    ----------------------------------------------------------------
    -- 1. insert employee can tinh toan
    ----------------------------------------------------------------
    INSERT INTO AT_CHOSEN_EMP
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       PA_OBJECT_SALARY_ID,
       USERNAME,
       REQUEST_ID,
       TER_EFFECT_DATE,
       JOIN_DATE,
       JOIN_DATE_STATE)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              T.PA_OBJECT_SALARY_ID,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              CASE
                WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                 T.TER_EFFECT_DATE + 1
                ELSE
                 NULL
              END TER_EFFECT_DATE,
              T.JOIN_DATE,
              T.JOIN_DATE_STATE
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_TODATE
                      AND E.STATUS_ID = 447
                      AND E.IS_3B = 0) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
           ON O.ORG_ID = W.ORG_ID
        WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  
    --------------------------------------------------------------------
    -- 2.XOA NV DUOC TONG HOP TRONG THANG
    --------------------------------------------------------------------
    DELETE INS_CHANGE C
     WHERE C.PERIOD_CHANGE = P_PERIOD_ID
       AND C.EMPLOYEE_ID IN (SELECT EE.EMPLOYEE_ID FROM AT_CHOSEN_EMP EE);
  
    --------------------------------------------------------------------------
    -- 3.INSERT BANG TAM NHUNG BAN GHI CO THANG BIEN DONG <= THANG TONG HOP CONG
    --------------------------------------------------------------------------
    INSERT INTO INS_CHANGE_TEMP
      (EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       ORG_ID_INS,
       OLDSALARY,
       NEWSALARY,
       ISBHXH,
       ISBHYT,
       ISBHTN,
       CHANGE_TYPE,
       EFFECTDATE,
       CHANGE_MONTH,
       RETURN_DATEBHXH,
       RETURN_DATEBHYT,
       NOTE,
       CLTFRMMONTH,
       CLTTOMONTH,
       CLTBHXH,
       CLTBHYT,
       CLTBHTN,
       REPFRMMONTH,
       REPTOMONTH,
       REPBHXH,
       REPBHYT,
       REPBHTN,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG,
       TER_PKEY,
       PERIOD_CHANGE)
      SELECT C.EMPLOYEE_ID,
             C.ORG_ID,
             C.TITLE_ID,
             C.ORG_ID_INS,
             C.OLDSALARY,
             C.NEWSALARY,
             C.ISBHXH,
             C.ISBHYT,
             C.ISBHTN,
             C.CHANGE_TYPE,
             C.EFFECTDATE,
             C.CHANGE_MONTH,
             C.RETURN_DATEBHXH,
             C.RETURN_DATEBHYT,
             C.NOTE,
             C.CLTFRMMONTH,
             C.CLTTOMONTH,
             C.CLTBHXH,
             C.CLTBHYT,
             C.CLTBHTN,
             C.REPFRMMONTH,
             C.REPTOMONTH,
             C.REPBHXH,
             C.REPBHYT,
             C.REPBHTN,
             C.CREATED_DATE,
             C.CREATED_BY,
             C.CREATED_LOG,
             C.MODIFIED_DATE,
             C.MODIFIED_BY,
             C.MODIFIED_LOG,
             C.TER_PKEY,
             C.PERIOD_CHANGE
        FROM AT_CHOSEN_EMP EE
       INNER JOIN INS_CHANGE C
          ON C.EMPLOYEE_ID = EE.EMPLOYEE_ID
         AND C.CHANGE_MONTH <= PV_FROMDATE
       INNER JOIN (SELECT MAX(CM.ID) ID, CM.EMPLOYEE_ID
                     FROM INS_CHANGE CM
                    WHERE CM.CHANGE_MONTH < PV_FROMDATE
                    GROUP BY CM.EMPLOYEE_ID) M
          ON C.ID = M.ID;
  
    ------------------------------------------------------------------
    -- 4. INSERT DU LIEU HOP LE TU BANG CONG TONG HOP VAO BANG TAM
    ------------------------------------------------------------------
  
    INSERT INTO AT_INS_TIMESHEET_MONTHLY_TEMP
      (EMPLOYEE_ID, PERIOD_ID, TOTAL_W_SALARY, WORKING_TS)
      SELECT T.EMPLOYEE_ID,
             T.PERIOD_ID,
             SUM(T.TOTAL_W_SALARY) TOTAL_W_SALARY,
             SUM(T.WORKING_TS) WORKING_TS
        FROM AT_CHOSEN_EMP EE
       INNER JOIN AT_TIME_TIMESHEET_MONTHLY T
          ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
         AND T.PERIOD_ID = P_PERIOD_ID
       GROUP BY T.EMPLOYEE_ID, T.PERIOD_ID;
  
    ---------------------------------------------------------------------------
    -- 5. DAY DU LIEU VAO BANG INS_CHANGE T? SINH BIEN DONG GIAM
    ---------------------------------------------------------------------------
    INSERT INTO INS_CHANGE
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       ORG_ID_INS,
       OLDSALARY,
       NEWSALARY,
       ISBHXH,
       ISBHYT,
       ISBHTN,
       CHANGE_TYPE,
       EFFECTDATE,
       CHANGE_MONTH,
       RETURN_DATEBHXH,
       RETURN_DATEBHYT,
       NOTE,
       CLTFRMMONTH,
       CLTTOMONTH,
       CLTBHXH,
       CLTBHYT,
       CLTBHTN,
       REPFRMMONTH,
       REPTOMONTH,
       REPBHXH,
       REPBHYT,
       REPBHTN,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG,
       TER_PKEY,
       PERIOD_CHANGE)
      SELECT SEQ_INS_CHANGE.NEXTVAL,
             C.EMPLOYEE_ID,
             C.ORG_ID,
             C.TITLE_ID,
             C.ORG_ID_INS,
             C.NEWSALARY OLDSALARY,
             0 NEWSALARY,
             C.ISBHXH,
             C.ISBHYT,
             C.ISBHTN,
             CASE
               WHEN IT.WORKING_TS >= 12 THEN -- SINH BIEN DONG NGH THAI SAN
                4
               WHEN IT.TOTAL_W_SALARY < 12 THEN -- SINH BIEN DONG NGHI KHONG LUONG
                5
             END CHANGE_TYPE,
             PV_FROMDATE,
             PV_FROMDATE,
             C.RETURN_DATEBHXH,
             C.RETURN_DATEBHYT,
             TO_CHAR(UNISTR('Bi\1EBFn \0111\1ED9ng t\1EF1 sinh t\1EEB ph\1EA7n t\1ED5ng h\1EE3p c\00F4ng')),
             C.CLTFRMMONTH,
             C.CLTTOMONTH,
             C.CLTBHXH,
             C.CLTBHYT,
             C.CLTBHTN,
             C.REPFRMMONTH,
             C.REPTOMONTH,
             C.REPBHXH,
             C.REPBHYT,
             C.REPBHTN,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             C.TER_PKEY,
             P_PERIOD_ID
        FROM INS_CHANGE_TEMP C
       INNER JOIN AT_INS_TIMESHEET_MONTHLY_TEMP IT
          ON IT.EMPLOYEE_ID = C.EMPLOYEE_ID
       WHERE (IT.WORKING_TS >= 12 OR IT.TOTAL_W_SALARY < 12)
         AND C.CHANGE_TYPE NOT IN (4, 5);
  
    ---------------------------------------------------------------------------
    -- 5. DAY DU LIEU VAO BANG INS_CHANGE T? SINH BIEN DONG TANG
    ---------------------------------------------------------------------------
    INSERT INTO INS_CHANGE
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       ORG_ID_INS,
       OLDSALARY,
       NEWSALARY,
       ISBHXH,
       ISBHYT,
       ISBHTN,
       CHANGE_TYPE,
       EFFECTDATE,
       CHANGE_MONTH,
       RETURN_DATEBHXH,
       RETURN_DATEBHYT,
       NOTE,
       CLTFRMMONTH,
       CLTTOMONTH,
       CLTBHXH,
       CLTBHYT,
       CLTBHTN,
       REPFRMMONTH,
       REPTOMONTH,
       REPBHXH,
       REPBHYT,
       REPBHTN,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG,
       TER_PKEY,
       PERIOD_CHANGE)
      SELECT SEQ_INS_CHANGE.NEXTVAL,
             C.EMPLOYEE_ID,
             C.ORG_ID,
             C.TITLE_ID,
             C.ORG_ID_INS,
             0 OLDSALARY,
             (W.SAL_BASIC + NVL(A.AMOUNT, 0)) NEWSALARY,
             C.ISBHXH,
             C.ISBHYT,
             C.ISBHTN,
             CASE
               WHEN C.CHANGE_TYPE = 4 THEN -- SINH BIEN DONG NGH THAI SAN DI LAM TRO LAI
                64
               WHEN C.CHANGE_TYPE = 5 THEN -- SINH BIEN DONG NGHI KHONG LUONG DI LAM TRO LAI
                9
             END CHANGE_TYPE,
             PV_FROMDATE,
             PV_FROMDATE,
             C.RETURN_DATEBHXH,
             C.RETURN_DATEBHYT,
             TO_CHAR(UNISTR('Bi\1EBFn \0111\1ED9ng t\1EF1 sinh t\1EEB ph\1EA7n t\1ED5ng h\1EE3p c\00F4ng')),
             C.CLTFRMMONTH,
             C.CLTTOMONTH,
             C.CLTBHXH,
             C.CLTBHYT,
             C.CLTBHTN,
             C.REPFRMMONTH,
             C.REPTOMONTH,
             C.REPBHXH,
             C.REPBHYT,
             C.REPBHTN,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             C.TER_PKEY,
             P_PERIOD_ID
        FROM INS_CHANGE_TEMP C
       INNER JOIN AT_INS_TIMESHEET_MONTHLY_TEMP IT
          ON IT.EMPLOYEE_ID = C.EMPLOYEE_ID
        LEFT JOIN HU_WORKING_MAX W
          ON C.EMPLOYEE_ID = W.EMPLOYEE_ID
        LEFT JOIN HU_WORKING_ALLOW A
          ON W.ID = A.HU_WORKING_ID
         AND A.IS_INSURRANCE = -1
       WHERE IT.TOTAL_W_SALARY > 12
         AND C.CHANGE_TYPE IN (4, 5);
  END;
  --- QUAN LY NGAY NGHI PHEP
  
  -- QUAN LY NGHI BU
  
  -- LICH SU NGHI
  PROCEDURE MANAGEMENT_HISTORY_LEAVE(P_EMPLOYEE_ID IN NUMBER,
                                     P_FROM_DATE   IN DATE,
                                     P_TO_DATE     IN DATE,
                                     P_OUT         OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_OUT FOR
      SELECT E.ID AS EMPLOYEE_ID,
             E.EMPLOYEE_CODE AS EMPLOYEE_CODE,
             E.FULLNAME_VN AS EMPLOYEE_NAME,
             TO_CHAR(PR.REGDATE, 'DD/MM/RRRR') AS REG_DATE,
             TO_CHAR(PR.FROM_DATE, 'DD/MM/RRRR') AS FROM_DATE,
             TO_CHAR(PR.TO_DATE, 'DD/MM/RRRR') AS TO_DATE,
             TO_CHAR(PR.FROM_HOUR, 'DD/MM/RRRR') AS FROM_HOUR,
             TO_CHAR(PR.TO_HOUR, 'DD/MM/RRRR') AS TO_HOUR,
             TM.ID AS SIGN_ID,
             TM.CODE AS SIGN_CODE,
             TM.CODE || ' - ' || TM.NAME AS SIGN_NAME,
             TM.NOTE AS NOTE,
             TO_CHAR(PA.APPROVE_DATE, 'DD/MM/RRRR') AS APPROVE_DATE,
             PA.APPROVE_STATUS
        FROM AT_PORTAL_REG PR
       INNER JOIN HU_EMPLOYEE E
          ON PR.ID_EMPLOYEE = E.ID
       INNER JOIN AT_TIME_MANUAL TM
          ON PR.ID_SIGN = TM.ID
       INNER JOIN AT_PORTAL_APP PA
          ON PR.ID_REGGROUP = PA.ID_REGGROUP
      --INNER JOIN AT_FML F ON TM.MORNING_ID=F.ID
       WHERE PR.ID_EMPLOYEE = P_EMPLOYEE_ID
         AND PR.STATUS = 2
         AND PR.SVALUE = 'LEAVE'
            --AND TM.MORNING_ID IN (255,251)
         AND PR.REGDATE >= P_FROM_DATE
         AND PR.REGDATE <= P_TO_DATE
       ORDER BY PR.REGDATE;
  END;
  
  PROCEDURE GETMACHINES(P_USERNAME  IN VARCHAR2,
                        P_ORGID     IN NUMBER,
                        P_FROM_DATE IN DATE,
                        P_TO_DATE   IN DATE,
                        P_OUT       OUT CURSOR_TYPE) IS
  
  BEGIN
    OPEN P_OUT FOR
      SELECT TD.ID,
             E.ID AS EMPLOYEE_ID,
             E.EMPLOYEE_CODE AS EMPLOYEE_CODE,
             E.FULLNAME_VN AS VN_FULLNAME,
             T.NAME_VN AS TITLE_NAME,
             O.NAME_VN AS ORG_NAME,
             O.DESCRIPTION_PATH AS ORG_DESC,
             S.NAME AS STAFF_RANK_NAME,
             O.ID AS ORG_ID,
             TO_CHAR(TD.WORKINGDAY, 'DD/MM/RRRR') AS WORKINGDAY,
             TD.SHIFT_ID AS SHIFT_ID,
             TD.SHIFT_CODE AS SHIFT_CODE,
             TM.CODE AS MANUAL_CODE,
             TD.LATE AS LATE,
             TD.WORKINGHOUR AS WORKINGHOUR,
             TO_CHAR(TD.VALIN1, 'DD/MM/RRRR') AS SHIFTIN,
             TO_CHAR(TD.VALIN2, 'DD/MM/RRRR') AS SHIFTBACKOUT,
             TO_CHAR(TD.VALIN3, 'DD/MM/RRRR') AS SHIFTBACKIN,
             TO_CHAR(TD.VALIN4, 'DD/MM/RRRR') AS SHIFTOUT,
             TD.COMEBACKOUT AS COMEBACKOUT,
             CASE
               WHEN FMLM.IS_LEAVE = 0 THEN
                ROUND(NVL(TD.WORKINGHOUR_SHIFT, 0) / 2, 1)
               ELSE
                0
             END + CASE
               WHEN FMLA.IS_LEAVE = 0 THEN
                ROUND(NVL(TD.WORKINGHOUR_SHIFT, 0) / 2, 1)
               ELSE
                0
             END SALARIED_HOUR,
             CASE
               WHEN FMLM.IS_LEAVE = -1 THEN
                ROUND(NVL(TD.WORKINGHOUR_SHIFT, 0) / 2, 1)
               ELSE
                0
             END + CASE
               WHEN FMLA.IS_LEAVE = -1 THEN
                ROUND(NVL(TD.WORKINGHOUR_SHIFT, 0) / 2, 1)
               ELSE
                0
             END NOTSALARIED_HOUR
        FROM AT_TIME_TIMESHEET_MACHINET TD
        LEFT JOIN HU_EMPLOYEE E
          ON E.ID = TD.EMPLOYEE_ID
        LEFT JOIN HU_TITLE T
          ON T.ID = E.TITLE_ID
        LEFT JOIN HU_ORGANIZATION O
          ON O.ID = E.ORG_ID
        LEFT JOIN HU_STAFF_RANK S
          ON S.ID = E.STAFF_RANK_ID
        LEFT JOIN AT_TIME_MANUAL TM
          ON TD.SHIFT_MANUAL_ID = TM.ID
        LEFT JOIN AT_FML FMLM
          ON TM.MORNING_ID = FMLM.ID
        LEFT JOIN AT_FML FMLA
          ON TM.AFTERNOON_ID = FMLA.ID
       WHERE td.workingday >= P_FROM_DATE
         and td.workingday <= P_TO_DATE
         AND E.ORG_ID IN
             (SELECT * FROM TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORGID)))
       ORDER BY E.ID, TD.WORKINGDAY;
  END;
  */
  PROCEDURE IMPORT_SWIPE_DATA(P_XML IN CLOB, P_USERNAME IN VARCHAR2) IS
    P_MINDATE           DATE;
    P_MAXDATE           DATE;
    P_ACCOUNT_SHORTNAME NVARCHAR2(250);
  BEGIN
  
    INSERT INTO AT_SWIPE_DATA_IMPORT_TEMP
      (itime_id, workingday,TERMINAL_ID, valtime, ACCOUNT_SHORTNAME)
      SELECT T.ITIME_ID,
             TO_DATE(T.WORKINGDAY, 'dd/MM/yyyy') WORKINGDAY,
             T.TERMINAL_ID,
             TO_DATE(T.WORKINGDAY || ' ' || T.VALTIME, 'dd/MM/yyyy HH24:MI') VALTIME,
             ACCOUNT_SHORTNAME
        FROM XMLTABLE('/NewDataSet/DATA' PASSING xmltype(P_XML) COLUMNS
                      --describe columns and path to them:
                      ITIME_ID varchar2(20) PATH './ITIME_ID',
                      WORKINGDAY varchar2(20) PATH './WORKINGDAY',
                      TERMINAL_ID NUMBER PATH './TERMINAL_ID',
                      ACCOUNT_SHORTNAME varchar2(500) PATH
                      './ORG_CHECK_IN_NAME',
                      VALTIME varchar2(20) PATH './VALTIME') T;
    --Delete data cu
    DELETE AT_SWIPE_DATA AT
     WHERE EXISTS (SELECT T.ITIME_ID
              FROM AT_SWIPE_DATA_IMPORT_TEMP T
             WHERE T.ITIME_ID = AT.ITIME_ID
               AND T.TERMINAL_ID = AT.TERMINAL_ID            
               AND T.WORKINGDAY = AT.WORKINGDAY);
  
    INSERT INTO AT_SWIPE_DATA
      (ID,
       ITIME_ID,
       VALTIME,
       WORKINGDAY,
       CREATED_BY,
       MODIFIED_BY,
       EMPLOYEE_ID,
       MACHINE_TYPE,
       ACCOUNT_SHORTNAME,
       TERMINAL_ID,
       NOTE)
      SELECT SEQ_AT_SWIPE_DATA.NEXTVAL,
             DD.ITIME_ID,
             DD.VALTIME,
             DD.WORKINGDAY,
             P_USERNAME,
             P_USERNAME,
             EMPLOYEE_ID,
             MACHINE_TYPE,
             ACCOUNT_SHORTNAME,
             TERMINAL_ID,
             N'Import d\1EEF li\1EC7u qu\1EB9t th\1EBB'
        FROM (SELECT DISTINCT D.ITIME_ID,
                              D.VALTIME,
                              D.WORKINGDAY,
                              /*PKG_FUNCTION.GET_EMP_BY_ATT_CODE(D.ITIME_ID,
                              6920,
                              D.WORKINGDAY) AS EMPLOYEE_ID,*/
                              NVL(E.ID, 0) EMPLOYEE_ID,
                              6920 MACHINE_TYPE,
                              (CASE
                                WHEN D.ACCOUNT_SHORTNAME IS NULL THEN
                                 D.ACCOUNT_SHORTNAME
                                ELSE
                                 E.ORG_CHECK_IN_NAME
                              END) AS ACCOUNT_SHORTNAME,
                              D.TERMINAL_ID
                FROM AT_SWIPE_DATA_IMPORT_TEMP D
                LEFT JOIN (SELECT EE.ID,
                                 EE.ITIME_ID,
                                 O.NAME_VN AS ORG_CHECK_IN_NAME
                            FROM HU_EMPLOYEE EE
                            LEFT JOIN HU_ORGANIZATION O
                              ON O.ID = EE.ORG_ID
                           WHERE NVL(EE.IS_KIEM_NHIEM, 0) = 0) E
                  ON D.ITIME_ID = E.ITIME_ID) DD;
  
    SELECT MIN(T.WORKINGDAY), MAX(T.WORKINGDAY)
      INTO P_MINDATE, P_MAXDATE
      FROM AT_SWIPE_DATA_IMPORT_TEMP T;
  
    DELETE AT_DATA_INOUT AT
     WHERE EXISTS (SELECT T.ITIME_ID
              FROM AT_SWIPE_DATA_IMPORT_TEMP T
             WHERE T.ITIME_ID = AT.ITIME_ID
               AND T.WORKINGDAY = AT.WORKINGDAY);
  
    INSERT INTO AT_DATA_INOUT S
      (S.ID,
       S.EMPLOYEE_ID,
       S.WORKINGDAY,
       S.VALIN1,
       S.VALIN2,
       S.VALIN3,
       S.VALIN4,
       S.VALIN5,
       S.VALIN6,
       S.VALIN7,
       S.VALIN8,
       S.VALIN9,
       S.VALIN10,
       S.VALIN11,
       S.VALIN12,
       S.VALIN13,
       S.VALIN14,
       S.VALIN15,
       S.ITIME_ID,
       S.CREATED_DATE,
       S.CREATED_BY,
       S.CREATED_LOG)
      SELECT SEQ_AT_DATA_INOUT.NEXTVAL,
             A.EMPLOYEE_ID,
             A.WORKINGDAY,
             A.VAL1,
             A.VAL2,
             A.VAL3,
             A.VAL4,
             A.VAL5,
             A.VAL6,
             A.VAL7,
             A.VAL8,
             A.VAL9,
             A.VAL10,
             A.VAL11,
             A.VAL12,
             A.VAL13,
             A.VAL14,
             A.VAL15,
             A.ITIME_ID,
             SYSDATE,
             P_USERNAME,
             P_USERNAME
        FROM (SELECT *
                FROM (SELECT T.ITIME_ID,
                             E.ID EMPLOYEE_ID,
                             T.WORKINGDAY,
                             T.VALTIME,
                             ROW_NUMBER() OVER(PARTITION BY T.ITIME_ID, T.WORKINGDAY, E.ID ORDER BY T.ITIME_ID, T.VALTIME ASC) AS STT
                        FROM AT_SWIPE_DATA_IMPORT_TEMP T
                       INNER JOIN HU_EMPLOYEE E
                          ON T.ITIME_ID = E.ITIME_ID) PIVOT(MAX(VALTIME) FOR STT IN(1 AS VAL1,
                                                                                    2 AS VAL2,
                                                                                    3 AS VAL3,
                                                                                    4 AS VAL4,
                                                                                    5 AS VAL5,
                                                                                    6 AS VAL6,
                                                                                    7 AS VAL7,
                                                                                    8 AS VAL8,
                                                                                    9 AS VAL9,
                                                                                    10 AS
                                                                                    VAL10,
                                                                                    11 AS
                                                                                    VAL11,
                                                                                    12 AS
                                                                                    VAL12,
                                                                                    13 AS
                                                                                    VAL13,
                                                                                    14 AS
                                                                                    VAL14,
                                                                                    15 AS
                                                                                    VAL15,
                                                                                    16 AS
                                                                                    VAL16,
                                                                                    17 AS
                                                                                    VAL17,
                                                                                    18 AS
                                                                                    VAL18,
                                                                                    19 AS
                                                                                    VAL19,
                                                                                    20 AS
                                                                                    VAL20))) A;
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.IMPORT_SWIPE_DATA',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
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
      ROLLBACK;
  END;

  /*
  PROCEDURE IMPORT_SWIPE_DATA_AUTO(P_XML      IN CLOB,
                                   P_USERNAME IN VARCHAR2,
                                   P_OUT      OUT CURSOR_TYPE) IS
    P_MINDATE DATE;
    P_MAXDATE DATE;
  BEGIN
  
    INSERT INTO TEMP_SQL (NAME, USERNAME) VALUES (P_XML, P_USERNAME);
  
    INSERT INTO AT_SWIPE_DATA_IMPORT_TEMP
      (itime_id, workingday, valtime)
      SELECT T.ITIME_ID,
             TO_DATE(SUBSTR(T.VALTIME, 1, 10), 'dd/MM/yyyy') WORKINGDAY,
             TO_DATE(T.VALTIME,
                     'dd/MM/yyyy HH:MI ' || SUBSTR(T.VALTIME, -2, 2)) VALTIME
        FROM XMLTABLE('/NewDataSet/DATA' PASSING xmltype(P_XML) COLUMNS
                      --describe columns and path to them:
                      ITIME_ID varchar2(20) PATH './ID',
                      VALTIME varchar2(20) PATH './VAL') T;
    --Delete data cu
    DELETE AT_SWIPE_DATA AT
     WHERE EXISTS (SELECT T.ITIME_ID
              FROM AT_SWIPE_DATA_IMPORT_TEMP T
             WHERE T.ITIME_ID = AT.ITIME_ID
               AND T.WORKINGDAY = AT.WORKINGDAY);
  
    INSERT INTO AT_SWIPE_DATA
      (ID, ITIME_ID, VALTIME, WORKINGDAY, CREATED_BY, MODIFIED_BY)
      SELECT SEQ_AT_SWIPE_DATA.NEXTVAL,
             D.ITIME_ID,
             D.VALTIME,
             D.WORKINGDAY,
             P_USERNAME,
             P_USERNAME
        FROM AT_SWIPE_DATA_IMPORT_TEMP D;
  
    SELECT MIN(T.WORKINGDAY), MAX(T.WORKINGDAY)
      INTO P_MINDATE, P_MAXDATE
      FROM AT_SWIPE_DATA_IMPORT_TEMP T;
  
    DELETE AT_DATA_INOUT AT
     WHERE EXISTS (SELECT T.ITIME_ID
              FROM AT_SWIPE_DATA_IMPORT_TEMP T
             WHERE T.ITIME_ID = AT.ITIME_ID
               AND T.WORKINGDAY = AT.WORKINGDAY);
  
    INSERT INTO AT_DATA_INOUT S
      (S.ID,
       S.EMPLOYEE_ID,
       S.WORKINGDAY,
       S.VALIN1,
       S.VALIN2,
       S.VALIN3,
       S.VALIN4,
       S.VALIN5,
       S.VALIN6,
       S.VALIN7,
       S.VALIN8,
       S.VALIN9,
       S.VALIN10,
       S.VALIN11,
       S.VALIN12,
       S.VALIN13,
       S.VALIN14,
       S.VALIN15,
       S.ITIME_ID,
       S.CREATED_DATE,
       S.CREATED_BY,
       S.CREATED_LOG)
      SELECT SEQ_AT_DATA_INOUT.NEXTVAL,
             A.EMPLOYEE_ID,
             A.WORKINGDAY,
             A.VAL1,
             A.VAL2,
             A.VAL3,
             A.VAL4,
             A.VAL5,
             A.VAL6,
             A.VAL7,
             A.VAL8,
             A.VAL9,
             A.VAL10,
             A.VAL11,
             A.VAL12,
             A.VAL13,
             A.VAL14,
             A.VAL15,
             A.ITIME_ID,
             SYSDATE,
             P_USERNAME,
             P_USERNAME
        FROM (SELECT *
                FROM (SELECT T.ITIME_ID,
                             E.ID EMPLOYEE_ID,
                             T.WORKINGDAY,
                             T.VALTIME,
                             ROW_NUMBER() OVER(PARTITION BY T.ITIME_ID, T.WORKINGDAY, E.ID ORDER BY T.ITIME_ID, T.VALTIME ASC) AS STT
                        FROM AT_SWIPE_DATA_IMPORT_TEMP T
                       INNER JOIN HU_EMPLOYEE E
                          ON T.ITIME_ID = E.ITIME_ID)
              PIVOT(MAX(VALTIME)
                 FOR STT IN(1 AS VAL1,
                           2 AS VAL2,
                           3 AS VAL3,
                           4 AS VAL4,
                           5 AS VAL5,
                           6 AS VAL6,
                           7 AS VAL7,
                           8 AS VAL8,
                           9 AS VAL9,
                           10 AS VAL10,
                           11 AS VAL11,
                           12 AS VAL12,
                           13 AS VAL13,
                           14 AS VAL14,
                           15 AS VAL15,
                           16 AS VAL16,
                           17 AS VAL17,
                           18 AS VAL18,
                           19 AS VAL19,
                           20 AS VAL20))) A;
  
    OPEN P_OUT FOR
      SELECT 1 FROM DUAL;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.IMPORT_SWIPE_DATA',
                              SQLERRM || '_' || DBMS_UTILITY.format_error_backtrace,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
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
      OPEN P_OUT FOR
        SELECT 0 FROM DUAL;
      ROLLBACK;
  END;
  */
  PROCEDURE IMPORT_TIMESHEET_CTT(P_XML       IN CLOB,
                                 P_USERNAME  IN VARCHAR2,
                                 P_PERIOD_ID IN NUMBER,
                                 P_CUR       OUT CURSOR_TYPE) IS
    P_STARTDATE    DATE;
    P_ENDDATE      DATE;
    P_TEMP         NUMBER;
    V_COL_V        CLOB;
    PV_SEQ         NUMBER;
    PV_SQL         CLOB;
    PV_XML         CURSOR_TYPE;
    PV_IMPORT      NUMBER;
    PV_COUNT       NUMBER := 0;
    PV_SEQ_ID      NUMBER;
    PV_DAY_NUM     NUMBER := 0;
    PV_COUNT_CODE  NUMBER := 0;
    PV_COUNT_ERROR NUMBER := 0;
    PV_MANUAL_ID   NUMBER := 0;
  BEGIN
    SELECT P.START_DATE, P.END_DATE
      INTO P_STARTDATE, P_ENDDATE
      FROM AT_PERIOD P
     WHERE p.ID = P_PERIOD_ID;
    SELECT SEQ_ORG_TEMP_TABLE.NEXTVAL INTO PV_SEQ FROM DUAL;
  
    INSERT INTO AT_TEMP_DATE
      SELECT A.*, PV_SEQ, rownum
        FROM table(PKG_FUNCTION.table_listdate(P_STARTDATE, P_ENDDATE)) A;
  
    -- LAY DU LIEU PIVOT
    SELECT LISTAGG('D' || A.STT || ' ' || ' AS ' || '''' ||
                   TO_CHAR(A.CDATE, 'DD') || '''',
                   ',') WITHIN GROUP(ORDER BY A.STT)
      INTO V_COL_V
      FROM AT_TEMP_DATE A
     WHERE A.REQUEST_ID = PV_SEQ;
  
    --Xoa row trong AT_TIMESHEET_CTT_ERROR
    DELETE FROM AT_TIMESHEET_CTT_ERROR;
    COMMIT;
    --UNPIVOT COLUMNS TO ROWS
    INSERT INTO XML_TABLE_TEMP
      (EMPLOYEE_ID,
       D1,
       DA1,
       D2,
       DA2,
       D3,
       DA3,
       D4,
       DA4,
       D5,
       DA5,
       D6,
       DA6,
       D7,
       DA7,
       D8,
       DA8,
       D9,
       DA9,
       D10,
       DA10,
       D11,
       DA11,
       D12,
       DA12,
       D13,
       DA13,
       D14,
       DA14,
       D15,
       DA15,
       D16,
       DA16,
       D17,
       DA17,
       D18,
       DA18,
       D19,
       DA19,
       D20,
       DA20,
       D21,
       DA21,
       D22,
       DA22,
       D23,
       DA23,
       D24,
       DA24,
       D25,
       DA25,
       D26,
       DA26,
       D27,
       DA27,
       D28,
       DA28,
       D29,
       DA29,
       D30,
       DA30,
       D31,
       DA31)
      SELECT EMPLOYEE_ID,
             D1,
             TO_DATE(DA1, 'dd/MM/yyyy'),
             D2,
             TO_DATE(DA2, 'dd/MM/yyyy'),
             D3,
             TO_DATE(DA3, 'dd/MM/yyyy'),
             D4,
             TO_DATE(DA4, 'dd/MM/yyyy'),
             D5,
             TO_DATE(DA5, 'dd/MM/yyyy'),
             D6,
             TO_DATE(DA6, 'dd/MM/yyyy'),
             D7,
             TO_DATE(DA7, 'dd/MM/yyyy'),
             D8,
             TO_DATE(DA8, 'dd/MM/yyyy'),
             D9,
             TO_DATE(DA9, 'dd/MM/yyyy'),
             D10,
             TO_DATE(DA10, 'dd/MM/yyyy'),
             D11,
             TO_DATE(DA11, 'dd/MM/yyyy'),
             D12,
             TO_DATE(DA12, 'dd/MM/yyyy'),
             D13,
             TO_DATE(DA13, 'dd/MM/yyyy'),
             D14,
             TO_DATE(DA14, 'dd/MM/yyyy'),
             D15,
             TO_DATE(DA15, 'dd/MM/yyyy'),
             D16,
             TO_DATE(DA16, 'dd/MM/yyyy'),
             D17,
             TO_DATE(DA17, 'dd/MM/yyyy'),
             D18,
             TO_DATE(DA18, 'dd/MM/yyyy'),
             D19,
             TO_DATE(DA19, 'dd/MM/yyyy'),
             D20,
             TO_DATE(DA20, 'dd/MM/yyyy'),
             D21,
             TO_DATE(DA21, 'dd/MM/yyyy'),
             D22,
             TO_DATE(DA22, 'dd/MM/yyyy'),
             D23,
             TO_DATE(DA23, 'dd/MM/yyyy'),
             D24,
             TO_DATE(DA24, 'dd/MM/yyyy'),
             D25,
             TO_DATE(DA25, 'dd/MM/yyyy'),
             D26,
             TO_DATE(DA26, 'dd/MM/yyyy'),
             D27,
             TO_DATE(DA27, 'dd/MM/yyyy'),
             D28,
             TO_DATE(DA28, 'dd/MM/yyyy'),
             D29,
             TO_DATE(DA29, 'dd/MM/yyyy'),
             D30,
             TO_DATE(DA30, 'dd/MM/yyyy'),
             D31,
             TO_DATE(DA31, 'dd/MM/yyyy')
        FROM XMLTABLE('/NewDataSet/DATA' PASSING xmltype(P_XML) COLUMNS
                      EMPLOYEE_ID NUMBER PATH './E_ID',
                      D1 NUMBER PATH './D1',
                      DA1 NVARCHAR2(16) PATH './DA1',
                      D2 NUMBER PATH './D2',
                      DA2 NVARCHAR2(16) PATH './DA2',
                      D3 NUMBER PATH './D3',
                      DA3 NVARCHAR2(16) PATH './DA3',
                      D4 NUMBER PATH './D4',
                      DA4 NVARCHAR2(16) PATH './DA4',
                      D5 NUMBER PATH './D5',
                      DA5 NVARCHAR2(16) PATH './DA5',
                      D6 NUMBER PATH './D6',
                      DA6 NVARCHAR2(16) PATH './DA6',
                      D7 NUMBER PATH './D7',
                      DA7 NVARCHAR2(16) PATH './DA7',
                      D8 NUMBER PATH './D8',
                      DA8 NVARCHAR2(16) PATH './DA8',
                      D9 NUMBER PATH './D9',
                      DA9 NVARCHAR2(16) PATH './DA9',
                      D10 NUMBER PATH './D10',
                      DA10 NVARCHAR2(16) PATH './DA10',
                      D11 NUMBER PATH './D11',
                      DA11 NVARCHAR2(16) PATH './DA11',
                      D12 NUMBER PATH './D12',
                      DA12 NVARCHAR2(16) PATH './DA12',
                      D13 NUMBER PATH './D13',
                      DA13 NVARCHAR2(16) PATH './DA13',
                      D14 NUMBER PATH './D14',
                      DA14 NVARCHAR2(16) PATH './DA14',
                      D15 NUMBER PATH './D15',
                      DA15 NVARCHAR2(16) PATH './DA15',
                      D16 NUMBER PATH './D16',
                      DA16 NVARCHAR2(16) PATH './DA16',
                      D17 NUMBER PATH './D17',
                      DA17 NVARCHAR2(16) PATH './DA17',
                      D18 NUMBER PATH './D18',
                      DA18 NVARCHAR2(16) PATH './DA18',
                      D19 NUMBER PATH './D19',
                      DA19 NVARCHAR2(16) PATH './DA19',
                      D20 NUMBER PATH './D20',
                      DA20 NVARCHAR2(16) PATH './DA20',
                      D21 NUMBER PATH './D21',
                      DA21 NVARCHAR2(16) PATH './DA21',
                      D22 NUMBER PATH './D22',
                      DA22 NVARCHAR2(16) PATH './DA22',
                      D23 NUMBER PATH './D23',
                      DA23 NVARCHAR2(16) PATH './DA23',
                      D24 NUMBER PATH './D24',
                      DA24 NVARCHAR2(16) PATH './DA24',
                      D25 NUMBER PATH './D25',
                      DA25 NVARCHAR2(16) PATH './DA25',
                      D26 NUMBER PATH './D26',
                      DA26 NVARCHAR2(16) PATH './DA26',
                      D27 NUMBER PATH './D27',
                      DA27 NVARCHAR2(16) PATH './DA27',
                      D28 NUMBER PATH './D28',
                      DA28 NVARCHAR2(16) PATH './DA28',
                      D29 NUMBER PATH './D29',
                      DA29 NVARCHAR2(16) PATH './DA29',
                      D30 NUMBER PATH './D30',
                      DA30 NVARCHAR2(16) PATH './DA30',
                      D31 NUMBER PATH './D31',
                      DA31 NVARCHAR2(16) PATH './DA31');
    --INSERT OR UPDATE IN AT_LEAVESHEET & AT_LEAVESHEET_DETAIL
    FOR DATA IN (SELECT *
                   FROM XML_TABLE_TEMP UNPIVOT((MANUAL_ID, LEAVE_DAY) FOR A IN((D1, DA1) AS 'M1',
                                                                               (D2, DA2) AS 'M2',
                                                                               (D3, DA3) AS 'M3',
                                                                               (D4, DA4) AS 'M4',
                                                                               (D5, DA5) AS 'M5',
                                                                               (D6, DA6) AS 'M6',
                                                                               (D7, DA7) AS 'M7',
                                                                               (D8, DA8) AS 'M8',
                                                                               (D9, DA9) AS 'M9',
                                                                               (D10, DA10) AS
                                                                               'M10',
                                                                               (D11, DA11) AS
                                                                               'M11',
                                                                               (D12, DA12) AS
                                                                               'M12',
                                                                               (D13, DA13) AS
                                                                               'M13',
                                                                               (D14, DA14) AS
                                                                               'M14',
                                                                               (D15, DA15) AS
                                                                               'M15',
                                                                               (D16, DA16) AS
                                                                               'M16',
                                                                               (D17, DA17) AS
                                                                               'M17',
                                                                               (D18, DA18) AS
                                                                               'M18',
                                                                               (D19, DA19) AS
                                                                               'M19',
                                                                               (D20, DA20) AS
                                                                               'M20',
                                                                               (D21, DA21) AS
                                                                               'M21',
                                                                               (D22, DA22) AS
                                                                               'M22',
                                                                               (D23, DA23) AS
                                                                               'M23',
                                                                               (D24, DA24) AS
                                                                               'M24',
                                                                               (D25, DA25) AS
                                                                               'M25',
                                                                               (D26, DA26) AS
                                                                               'M26',
                                                                               (D27, DA27) AS
                                                                               'M27',
                                                                               (D28, DA28) AS
                                                                               'M28',
                                                                               (D29, DA29) AS
                                                                               'M29',
                                                                               (D30, DA30) AS
                                                                               'M30',
                                                                               (D31, DA31) AS
                                                                               'M31'))) LOOP
      --Get error rows
      IF DATA.MANUAL_ID IS NOT NULL THEN
        INSERT INTO AT_TIMESHEET_CTT_ERROR E
          (SELECT E.EMPLOYEE_CODE, DATA.LEAVE_DAY
             FROM AT_LEAVESHEET DT
            INNER JOIN HU_EMPLOYEE E
               ON E.ID = DT.EMPLOYEE_ID
            WHERE DT.EMPLOYEE_ID = DATA.EMPLOYEE_ID
              AND TO_DATE(DATA.LEAVE_DAY, 'dd-MON-yy') BETWEEN
                  DT.LEAVE_FROM AND DT.LEAVE_TO --vi gia tri khi RUN la '01-AUG-19' nen phai format 'dd-MON-yy' moi xuat ra 01/08/2019
              AND DT.MANUAL_ID <> DATA.MANUAL_ID
              AND (DT.IMPORT IS NULL OR DT.IMPORT <> -1));
      ELSE
        --Neu manual_ID null kiem tra xem co ton tai du lieu goc hay ko
        SELECT COUNT(1)
          INTO PV_COUNT_ERROR
          FROM AT_LEAVESHEET DT
         WHERE DT.EMPLOYEE_ID = DATA.EMPLOYEE_ID
           AND TO_DATE(DATA.LEAVE_DAY, 'dd-MON-yy') BETWEEN DT.LEAVE_FROM AND
               DT.LEAVE_TO --vi gia tri khi RUN la '01-AUG-19' nen phai format 'dd-MON-yy' moi xuat ra 01/08/2019
           AND (DT.IMPORT IS NULL OR DT.IMPORT <> -1);
        IF PV_COUNT_ERROR > 0 THEN
          --co du lieu goc => insert error
          INSERT INTO AT_TIMESHEET_CTT_ERROR E
            (SELECT E.EMPLOYEE_CODE, DATA.LEAVE_DAY
               FROM AT_LEAVESHEET DT
              INNER JOIN HU_EMPLOYEE E
                 ON E.ID = DT.EMPLOYEE_ID
              WHERE DT.EMPLOYEE_ID = DATA.EMPLOYEE_ID
                AND TO_DATE(DATA.LEAVE_DAY, 'dd-MON-yy') BETWEEN
                    DT.LEAVE_FROM AND DT.LEAVE_TO --vi gia tri khi RUN la '01-AUG-19' nen phai format 'dd-MON-yy' moi xuat ra 01/08/2019
                AND (DT.IMPORT IS NULL OR DT.IMPORT <> -1));
        END IF;
      END IF;
      --xoa cac dong co ky hieu nghi la X ,MAC dinh xet cung
      -- TNG-499
      DELETE AT_LEAVESHEET_DETAIL DEL_LEAVEDT
       WHERE DEL_LEAVEDT.EMPLOYEE_ID = DATA.EMPLOYEE_ID
         AND DEL_LEAVEDT.MANUAL_ID = 25 --DANG XET CUNG.25 LA NGAY CONG DI LAM
         AND DEL_LEAVEDT.LEAVESHEET_ID IN
             (SELECT ID
                FROM AT_LEAVESHEET
               WHERE ID = PV_COUNT
                 AND IMPORT = -1);
      DELETE AT_LEAVESHEET DEL_LEAVE
       WHERE DEL_LEAVE.ID = PV_COUNT
         AND DEL_LEAVE.MANUAL_ID = 25 --DANG XET CUNG.25 LA NGAY CONG DI LAM
         AND IMPORT = -1;
      --Check exist leave_day
      BEGIN
        SELECT NVL(DT.ID, 0)
          INTO PV_COUNT
          FROM AT_LEAVESHEET DT
         WHERE DT.EMPLOYEE_ID = DATA.EMPLOYEE_ID
           AND TO_DATE(DATA.LEAVE_DAY, 'dd-MON-yy') BETWEEN DT.LEAVE_FROM AND
               DT.LEAVE_TO; --vi gia tri khi RUN la '01-AUG-19' nen phai format 'dd-MON-yy' moi xuat ra 01/08/2019
      
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          PV_COUNT := -1; --khong co id
      END;
      --Get day_num by manual_id
      BEGIN
        SELECT CASE
                 WHEN M.CODE IN
                      ('L', 'OFF', 'X', '0X', '0.5X', '0.75X', '1.5X') THEN
                  0
                 WHEN M.CODE LIKE '%X/%' THEN
                  NVL(AFTERNOON.VALUE_RATE, 0)
                 WHEN M.CODE LIKE '%/2' THEN --them 2 dong nay,neu ky hieu
                  NVL(MORNING.VALUE_RATE, 0) --nghi /2 thi roi case nay
                 WHEN M.CODE NOT LIKE '%X/%' THEN
                  NVL(MORNING.VALUE_RATE, 0) + NVL(AFTERNOON.VALUE_RATE, 0)
                 ELSE
                  0
               END DAY_NUM
          INTO PV_DAY_NUM
          FROM AT_TIME_MANUAL M
          LEFT JOIN AT_TIME_MANUAL_RATE MORNING
            ON MORNING.ID = M.MORNING_RATE_ID
          LEFT JOIN AT_TIME_MANUAL_RATE AFTERNOON
            ON AFTERNOON.ID = M.AFTERNOON_RATE_ID
         WHERE M.ID = DATA.MANUAL_ID;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          PV_DAY_NUM := -1;
      END;
      --Exist => update, not Exist => insert
      IF PV_COUNT > 0 THEN
        IF DATA.MANUAL_ID IS NULL OR DATA.MANUAL_ID = '' THEN
          DELETE FROM AT_LEAVESHEET_DETAIL DT
           WHERE DT.EMPLOYEE_ID = DATA.EMPLOYEE_ID
             AND DT.LEAVE_DAY = TO_DATE(DATA.LEAVE_DAY, 'dd-MON-yy')
             AND DT.LEAVESHEET_ID IN
                 (SELECT ID
                    FROM AT_LEAVESHEET
                   WHERE ID = PV_COUNT
                     AND IMPORT = -1);
          DELETE FROM AT_LEAVESHEET T
           WHERE T.ID = PV_COUNT
             AND T.IMPORT = -1;
        ELSE
          BEGIN
            --neu loai nghi la X/P thi ko lay ID cua X/P ma lay ID cua P
            /*SELECT M.ID
            INTO PV_MANUAL_ID
            FROM AT_TIME_MANUAL M
            WHERE M.CODE = (SELECT F.CODE
            FROM AT_TIME_MANUAL T
            LEFT JOIN AT_FML F
            ON T.AFTERNOON_ID = F.ID
            WHERE T.ID = DATA.MANUAL_ID);*/
            SELECT O.ID
              INTO PV_MANUAL_ID
              FROM AT_TIME_MANUAL T
             INNER JOIN AT_TIME_MANUAL O
                ON O.AFTERNOON_ID = T.AFTERNOON_ID
             where T.ID = DATA.MANUAL_ID
               and rownum = 1;
            -- PV_MANUAL_ID := DATA.MANUAL_ID;
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
              PV_MANUAL_ID := 0;
          END;
        
          UPDATE AT_LEAVESHEET_DETAIL DT
             SET DT.MANUAL_ID     = PV_MANUAL_ID,
                 DT.DAY_NUM       = PV_DAY_NUM,
                 DT.STATUS_SHIFT = CASE
                                     WHEN PV_DAY_NUM = '0.5' THEN
                                      '1'
                                     ELSE
                                      '0'
                                   END,
                 DT.MODIFIED_DATE = SYSDATE,
                 DT.MODIFIED_BY   = P_USERNAME,
                 DT.MODIFIED_LOG  = P_USERNAME
           WHERE DT.EMPLOYEE_ID = DATA.EMPLOYEE_ID
             AND DT.LEAVE_DAY = TO_DATE(DATA.LEAVE_DAY, 'dd-MON-yy')
             AND DT.LEAVESHEET_ID IN
                 (SELECT ID
                    FROM AT_LEAVESHEET
                   WHERE ID = PV_COUNT
                     AND IMPORT = -1);
          UPDATE AT_LEAVESHEET L
             SET L.MANUAL_ID     = PV_MANUAL_ID,
                 L.DAY_NUM       = PV_DAY_NUM,
                 L.MODIFIED_DATE = SYSDATE,
                 L.MODIFIED_BY   = P_USERNAME,
                 L.MODIFIED_LOG  = P_USERNAME
           WHERE L.ID = PV_COUNT
             AND IMPORT = -1;
        END IF;
      ELSE
        --Check cac ky hieu ko can insert
        SELECT COUNT(1)
          INTO PV_COUNT_CODE
          FROM AT_TIME_MANUAL T
         WHERE T.ID = DATA.MANUAL_ID
           AND T.CODE NOT IN
               ('L', 'OFF', 'X', '0X', '0.5X', '0.75X', '1.5X');
        IF PV_COUNT_CODE > 0 THEN
          PV_SEQ_ID := SEQ_AT_LEAVESHEET.NEXTVAL;
          BEGIN
            --neu loai nghi la X/P thi ko lay ID cua X/P ma lay ID cua P
            /*SELECT M.ID
            INTO PV_MANUAL_ID
            FROM AT_TIME_MANUAL M
            WHERE M.CODE = (SELECT F.CODE
            FROM AT_TIME_MANUAL T
            LEFT JOIN AT_FML F
            ON T.AFTERNOON_ID = F.ID
            WHERE T.ID = DATA.MANUAL_ID);*/
          
            SELECT O.ID
              INTO PV_MANUAL_ID
              FROM AT_TIME_MANUAL T
             INNER JOIN AT_TIME_MANUAL O
                ON O.AFTERNOON_ID = T.AFTERNOON_ID
             where T.ID = DATA.MANUAL_ID
               and rownum = 1;
            -- PV_MANUAL_ID := DATA.MANUAL_ID;
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
              PV_MANUAL_ID := 0;
          END;
          INSERT INTO AT_LEAVESHEET
            (ID,
             EMPLOYEE_ID,
             LEAVE_FROM,
             LEAVE_TO,
             MANUAL_ID,
             DAY_NUM,
             CREATED_DATE,
             CREATED_BY,
             CREATED_LOG,
             MODIFIED_DATE,
             MODIFIED_BY,
             MODIFIED_LOG,
             IMPORT,
             STATUS)
          VALUES
            (PV_SEQ_ID,
             DATA.EMPLOYEE_ID,
             TO_DATE(DATA.LEAVE_DAY, 'dd-MON-yy'),
             TO_DATE(DATA.LEAVE_DAY, 'dd-MON-yy'),
             PV_MANUAL_ID,
             PV_DAY_NUM,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             -1,
             1);
        
          INSERT INTO AT_LEAVESHEET_DETAIL
            (ID,
             LEAVESHEET_ID,
             EMPLOYEE_ID,
             LEAVE_DAY,
             MANUAL_ID,
             DAY_NUM,
             STATUS_SHIFT,
             CREATED_DATE,
             CREATED_BY,
             CREATED_LOG,
             MODIFIED_DATE,
             MODIFIED_BY,
             MODIFIED_LOG)
          VALUES
            (SEQ_AT_LEAVESHEET_DETAIL.NEXTVAL,
             PV_SEQ_ID,
             DATA.EMPLOYEE_ID,
             TO_DATE(DATA.LEAVE_DAY, 'dd-MON-yy'),
             PV_MANUAL_ID,
             PV_DAY_NUM,
             CASE WHEN PV_DAY_NUM = '0.5' THEN '1' ELSE '0' END,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             SYSDATE,
             P_USERNAME,
             P_USERNAME);
        END IF;
      END IF;
    END LOOP;
  
    --Check xem co row nao error khong
    SELECT COUNT(1) INTO PV_COUNT_ERROR FROM AT_TIMESHEET_CTT_ERROR;
    IF PV_COUNT_ERROR <= 0 THEN
      INSERT INTO AT_WORKSIGN_DATE_TEMP
        (ID,
         EMPLOYEE_ID,
         PERIOD_ID,
         D1,
         D2,
         D3,
         D4,
         D5,
         D6,
         D7,
         D8,
         D9,
         D10,
         D11,
         D12,
         D13,
         D14,
         D15,
         D16,
         D17,
         D18,
         D19,
         D20,
         D21,
         D22,
         D23,
         D24,
         D25,
         D26,
         D27,
         D28,
         D29,
         D30,
         D31)
        SELECT SEQ_AT_WORKSIGN_DATE_TEMP.NEXTVAL,
               EMPLOYEE_ID,
               P_PERIOD_ID,
               D1,
               D2,
               D3,
               D4,
               D5,
               D6,
               D7,
               D8,
               D9,
               D10,
               D11,
               D12,
               D13,
               D14,
               D15,
               D16,
               D17,
               D18,
               D19,
               D20,
               D21,
               D22,
               D23,
               D24,
               D25,
               D26,
               D27,
               D28,
               D29,
               D30,
               D31
          FROM XMLTABLE('/NewDataSet/DATA' PASSING xmltype(P_XML) COLUMNS
                        EMPLOYEE_ID NUMBER PATH './E_ID',
                        D1 NUMBER PATH './D1',
                        D2 NUMBER PATH './D2',
                        D3 NUMBER PATH './D3',
                        D4 NUMBER PATH './D4',
                        D5 NUMBER PATH './D5',
                        D6 NUMBER PATH './D6',
                        D7 NUMBER PATH './D7',
                        D8 NUMBER PATH './D8',
                        D9 NUMBER PATH './D9',
                        D10 NUMBER PATH './D10',
                        D11 NUMBER PATH './D11',
                        D12 NUMBER PATH './D12',
                        D13 NUMBER PATH './D13',
                        D14 NUMBER PATH './D14',
                        D15 NUMBER PATH './D15',
                        D16 NUMBER PATH './D16',
                        D17 NUMBER PATH './D17',
                        D18 NUMBER PATH './D18',
                        D19 NUMBER PATH './D19',
                        D20 NUMBER PATH './D20',
                        D21 NUMBER PATH './D21',
                        D22 NUMBER PATH './D22',
                        D23 NUMBER PATH './D23',
                        D24 NUMBER PATH './D24',
                        D25 NUMBER PATH './D25',
                        D26 NUMBER PATH './D26',
                        D27 NUMBER PATH './D27',
                        D28 NUMBER PATH './D28',
                        D29 NUMBER PATH './D29',
                        D30 NUMBER PATH './D30',
                        D31 NUMBER PATH './D31');
    
      --Delete data cu
      DELETE AT_TIME_TIMESHEET_DAILY E
       WHERE E.WORKINGDAY >= P_STARTDATE
         AND E.WORKINGDAY <= P_ENDDATE
         AND EXISTS (SELECT EMPLOYEE_ID
                FROM AT_WORKSIGN_DATE_TEMP
               WHERE EMPLOYEE_ID = E.EMPLOYEE_ID);
    
      PV_SQL := '
               INSERT INTO AT_TIME_TIMESHEET_DAILY
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         WORKINGDAY,
         CREATED_DATE,
         CREATED_BY,
         CREATED_LOG,
         MODIFIED_DATE,
         MODIFIED_BY,
         MODIFIED_LOG,
         MANUAL_ID)
        SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
               T.EMPLOYEE_ID,
               T.ORG_ID,
               T.TITLE_ID,
               T.CDATE,
               SYSDATE,
               ''' || P_USERNAME || ''',
               ''' || P_USERNAME || ''',
               SYSDATE,
               ''' || P_USERNAME || ''',
               ''' || P_USERNAME || ''',
               T.MANUAL_ID
          FROM (SELECT E.EMPLOYEE_ID,
                       E.ORG_ID,
                       E.TITLE_ID,
                       D.CDATE,
                       T.MANUAL_ID
                  FROM (SELECT E.ID AS EMPLOYEE_ID, E.ORG_ID, E.TITLE_ID
                          FROM HU_EMPLOYEE E
                         WHERE EXISTS (SELECT EMPLOYEE_ID
                                  FROM AT_WORKSIGN_DATE_TEMP
                                 WHERE EMPLOYEE_ID = E.ID)) E
                 INNER JOIN (SELECT *
                              FROM AT_WORKSIGN_DATE_TEMP UNPIVOT(MANUAL_ID FOR DAY IN(' ||
                V_COL_V ||
                '))) T
                    ON E.EMPLOYEE_ID = T.EMPLOYEE_ID
                 INNER JOIN (SELECT CDATE, TO_CHAR(CDATE, ''dd'') AS DAY
                              FROM TABLE(TABLE_LISTDATE(''' ||
                P_STARTDATE || ''', ''' || P_ENDDATE || '''))) D
                    ON D.DAY = T.DAY) T
      ';
      INSERT INTO AT_STRSQL VALUES (SEQ_AT_STRSQL.NEXTVAL, PV_SQL);
    
      EXECUTE IMMEDIATE PV_SQL;
      UPDATE AT_TIME_TIMESHEET_DAILY A
         SET A.OBJECT_ATTENDANCE = NVL((SELECT OBJECT_ATTENDANCE
                                         FROM (SELECT E.OBJECT_ATTENDANCE,
                                                      E.EFFECT_DATE
                                                 FROM HU_WORKING E
                                                WHERE E.EMPLOYEE_ID =
                                                      A.EMPLOYEE_ID
                                                  AND E.EFFECT_DATE <=
                                                      P_ENDDATE
                                                  AND E.STATUS_ID = 447
                                                  AND E.IS_WAGE = 0
                                                  AND E.IS_3B = 0
                                                ORDER BY E.EFFECT_DATE DESC) T
                                        WHERE ROWNUM = 1),
                                       0)
       WHERE A.WORKINGDAY BETWEEN P_STARTDATE AND P_ENDDATE;
    
      DELETE AT_TEMP_DATE A WHERE A.REQUEST_ID = PV_SEQ;
    END IF;
    --==import cong khong c? gio cong nen phai su dung ky hieu
    /* UPDATE AT_TIME_TIMESHEET_DAILY D
     SET D.WORKING_MEAL =
         (SELECT SUM(CASE
                       WHEN F.ID = F2.ID AND F.CODE ='X' THEN
                        1
                       WHEN F.ID = F2.ID AND F.CODE = 'CT' THEN
                        1
                       WHEN F.ID = F2.ID AND F.CODE = 'DH' THEN
                        1
                       ELSE
                        0
                     END)
            FROM AT_TIME_TIMESHEET_DAILY A
            LEFT JOIN AT_TIME_MANUAL M
              ON A.MANUAL_ID = M.ID
            LEFT JOIN AT_FML F
              ON M.MORNING_ID = F.ID
            LEFT JOIN AT_FML F2
              ON M.AFTERNOON_ID = F2.ID
           WHERE D.ID = A.ID)
    WHERE D.WORKINGDAY >= P_STARTDATE
     AND D.WORKINGDAY <= P_ENDDATE
     AND EXISTS (SELECT EMPLOYEE_ID
          FROM AT_WORKSIGN_DATE_TEMP T
         WHERE T.EMPLOYEE_ID = D.EMPLOYEE_ID);*/
    OPEN P_CUR FOR
      SELECT T.EMPLOYEE_CODE, TO_CHAR(T.LEAVE_DAY, 'dd/MM/yyyy') LEAVE_DAY
        FROM AT_TIMESHEET_CTT_ERROR T;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.IMPORT_TIMESHEET_CTT',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              P_USERNAME,
                              P_PERIOD_ID);
      ROLLBACK;
  END;

  /*
    PROCEDURE CAL_TIMESHEET_DAILY (P_USERNAME   IN NVARCHAR2,
                                     P_ORG_ID     IN NUMBER,
                                     P_PERIOD_ID  IN NUMBER,
                                     P_ISDISSOLVE IN NUMBER) IS
    PV_ENDDATE DATE;
    PV_FROMDATE DATE;
    PV_REQUEST_ID NUMBER;
    BEGIN
      SELECT T.START_DATE, T.END_DATE
      INTO PV_FROMDATE, PV_ENDDATE
      FROM AT_PERIOD T
      WHERE T.ID = P_PERIOD_ID;
      PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
      --==xac dinh danh sach nv de xu ly cong
      \*IF P_LST_EMPLOYEE IS NOT NULL THEN--==Neu co check ds nv se tong hop cong cho danh sach nv nay thoi
        INSERT INTO AT_CHOSEN_EMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE)
         (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE
           FROM HU_EMPLOYEE T
          INNER JOIN (SELECT E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          WHERE INSTR(',' || P_LST_EMPLOYEE || ',', ',' || T.ID || ',') > 0);
      ELSE*\
      INSERT INTO AT_CHOSEN_ORG E
        (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
           FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                      P_ORG_ID,
                                      P_ISDISSOLVE)) O
          WHERE EXISTS (SELECT 1
                   FROM AT_ORG_PERIOD OP
                  WHERE OP.PERIOD_ID = P_PERIOD_ID
                    AND OP.ORG_ID = O.ORG_ID
                    AND OP.STATUSCOLEX = 1));
  
      -- insert emp can tinh toan
      INSERT INTO AT_CHOSEN_EMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE
           FROM HU_EMPLOYEE T
          INNER JOIN (SELECT E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
             ON O.ORG_ID = W.ORG_ID
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
      --END IF;
  
          INSERT INTO AT_CAL_INOUT_TEMP
        (ID,
         EMPLOYEE_ID,
         WORKINGDAY,
         SHIFT_ID,
         SHIFT_CODE,
         SHIFT_MANUAL_ID,
         SHIFT_HOURS_START,
         SHIFT_HOURS_STOP,
         VALTIME1,
         VALTIME2,
         REQUEST_ID,
         ITIME_ID)
        SELECT SEQ_AT_CAL_INOUT_TEMP.NEXTVAL,
               T.EMPLOYEE_ID,
               WSIGN.WORKINGDAY,
               WSIGN.SHIFT_ID,
               WSIGN.SHIFT_CODE,
               WSIGN.SHIFT_MANUAL_ID,
               WSIGN.HOURS_START,
               WSIGN.HOURS_STOP,
               WSIGN.VALTIME,
               WSIGN.VALTIME_OUT,
               PV_REQUEST_ID,
               T.ITIME_ID
          FROM AT_CHOSEN_EMP T
         INNER JOIN (SELECT TT.*,
                            HU.ID EMPLOYEE_ID,
                            CASE
                              WHEN ATS.HOURS_START IS NOT NULL THEN
                               TO_DATE(TO_CHAR(TT.WORKINGDAY, 'yyyymmdd') ||
                                       TO_CHAR(ATS.HOURS_START, 'HH24MI'),
                                       'yyyymmddHH24MI')
                            END HOURS_START,
                            CASE
                              WHEN ATS.HOURS_STOP IS NOT NULL THEN
                               TO_DATE(TO_CHAR(TT.WORKINGDAY, 'yyyymmdd') ||
                                       TO_CHAR(ATS.HOURS_STOP, 'HH24MI'),
                                       'yyyymmddHH24MI')
                            END HOURS_STOP,
                            ATS.CODE SHIFT_CODE,
                            ATS.MANUAL_ID SHIFT_MANUAL_ID
                       FROM (SELECT T.ITIME_ID,
                                    T.TERMINAL_ID,
                                    T.WORKINGDAY,
                                    T.VALTIME,
                                    FN_SET_SHIFT(extract(hour from
                                                         cast(T.VALTIME as
                                                              timestamp))) AS SHIFT_ID,
                                    CASE T.STT
                                      WHEN 1 THEN
                                       IO.VALIN2
                                      WHEN 3 THEN
                                       IO.VALIN4
                                      ELSE
                                       NULL
                                    END AS VALTIME_OUT
                               FROM (SELECT T.ITIME_ID,
                                            T.TERMINAL_ID,
                                            T.WORKINGDAY,
                                            T.VALTIME,
                                            ROW_NUMBER() OVER(PARTITION BY T.ITIME_ID, T.WORKINGDAY ORDER BY T.VALTIME) AS STT
                                       from AT_SWIPE_DATA T
                                      WHERE T.WORKINGDAY >= PV_FROMDATE
                                        AND T.WORKINGDAY <= PV_ENDDATE) T
                                      INNER JOIN AT_DATA_INOUT IO
                                        ON IO.WORKINGDAY = T.WORKINGDAY
                                        AND IO.ITIME_ID = T.ITIME_ID) TT
                      INNER JOIN HU_EMPLOYEE HU
                         ON HU.ITIME_ID = TT.ITIME_ID
                      INNER JOIN AT_SHIFT ATS
                         ON ATS.ID = TT.SHIFT_ID
                      ) WSIGN
            ON WSIGN.EMPLOYEE_ID = T.EMPLOYEE_ID;
  
  
    INSERT INTO AT_TIME_TIMESHEET_MACHINE_TEMP M
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         WORKINGDAY,
         SHIFT_ID,
         SHIFT_CODE,
         SHIFT_MANUAL_ID,
         SHIFT_HOURS_START,
         SHIFT_HOURS_STOP,
         WORKINGHOUR,
         VALIN1,
         VALIN2,
         VALIN3,
         VALIN4,
         CREATED_DATE,
         CREATED_BY,
         REQUEST_ID)
        SELECT SEQ_AT_TIME_MACHINE_TEMP.NEXTVAL,
               T.EMPLOYEE_ID,
               T.ORG_ID,
               T.TITLE_ID,
               T.STAFF_RANK_ID,
               WSIGN.WORKINGDAY,
               WSIGN.SHIFT_ID,
               WSIGN.SHIFT_CODE,
               WSIGN.SHIFT_MANUAL_ID,
               WSIGN.SHIFT_HOURS_START,
               WSIGN.SHIFT_HOURS_STOP,
               CASE
                 WHEN WSIGN.SHIFT_HOURS_START >= WSIGN.VALTIME1 THEN
                  ROUND((WSIGN.VALTIME2 - WSIGN.SHIFT_HOURS_START) * 24, 2)
                 ELSE
                  ROUND((WSIGN.VALTIME2 - WSIGN.VALTIME1) * 24, 2)
               END AS WORKINGHOUR,
               WSIGN.VALTIME1,
               WSIGN.VALTIME2,
               WSIGN.VALTIME3,
               WSIGN.VALTIME4,
               SYSDATE,
               UPPER(P_USERNAME),
               PV_REQUEST_ID
          FROM AT_CHOSEN_EMP T
         INNER JOIN (SELECT *
                       FROM AT_CAL_INOUT_TEMP WSIGN
                      WHERE WSIGN.IS_OT IS NULL) WSIGN
            ON T.EMPLOYEE_ID = WSIGN.EMPLOYEE_ID;
  
      --Tinh lai so gio
      UPDATE AT_TIME_TIMESHEET_MACHINE_TEMP T
         SET T.WORKINGHOUR = (TRUNC(T.WORKINGHOUR)) + CASE
                               WHEN MOD(T.WORKINGHOUR, 1) >= 0.67 THEN
                                1
                               WHEN MOD(T.WORKINGHOUR, 1) >= 0.25 THEN
                                0.5
                               ELSE
                                0
                             END;
  
      DELETE FROM AT_TIME_TIMESHEET_DAILY D
       WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
         AND D.WORKINGDAY >= PV_FROMDATE
         AND D.WORKINGDAY <= PV_ENDDATE;
  
      DELETE FROM AT_TIME_TIMESHEET_ORIGIN D
       WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
         AND D.WORKINGDAY >= PV_FROMDATE
         AND D.WORKINGDAY <= PV_ENDDATE;
  
      INSERT INTO AT_TIME_TIMESHEET_DAILY T
        (T.ID,
         T.EMPLOYEE_ID,
         T.ORG_ID,
         T.TITLE_ID,
         T.WORKINGDAY,
         --T.SHIFT_CODE,
         T.WORKINGHOUR,
         --T.WORKINGHOUR_SHIFT,
         --T.NUMBER_SWIPE,
         --T.SHIFT_ID,
         --T.LEAVE_CODE,
         T.MANUAL_ID,
         \*T.LEAVE_ID,
         T.LATE,
         T.COMEBACKOUT,
         T.VALIN1,
         T.VALIN2,
         T.VALIN3,
         T.VALIN4,*\
         T.CREATED_DATE,
         T.CREATED_BY,
         T.CREATED_LOG,
         T.MODIFIED_DATE,
         T.MODIFIED_BY,
         T.MODIFIED_LOG)
        SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
               W.EMPLOYEE_ID,
               W.ORG_ID,
               W.TITLE_ID,
               W.WORKINGDAY,
               --W.SHIFT_CODE,
               W.WORKINGHOUR,
               --W.WORKINGHOUR_SHIFT,
               --W.NUMBER_SWIPE,
               --W.SHIFT_ID,
               --W.LEAVE_CODE,
               W.MANUAL_ID,
               \*W.LEAVE_ID,
               W.LATE,
               W.COMEBACKOUT,
               W.VALIN1,
               W.VALIN2,
               W.VALIN3,
               W.VALIN4,*\
               SYSDATE,
               UPPER(P_USERNAME),
               UPPER(P_USERNAME),
               SYSDATE,
               UPPER(P_USERNAME),
               UPPER(P_USERNAME)
          FROM (SELECT W.EMPLOYEE_ID,
                       W.ORG_ID,
                       W.TITLE_ID,
                       W.WORKINGDAY,
                       --W.SHIFT_CODE,
                       SUM(W.WORKINGHOUR) AS WORKINGHOUR,
                       --W.SHIFT_ID,
                       CASE
                         WHEN SUM(W.WORKINGHOUR) >= 6 THEN
                          1 --anh xa theo id danh muc KHCong
                         WHEN SUM(W.WORKINGHOUR) >= 4 THEN
                          3
                         WHEN SUM(W.WORKINGHOUR) >= 2 THEN
                          2
                         ELSE
                          NULL
                       END AS MANUAL_ID
                  FROM AT_TIME_TIMESHEET_MACHINE_TEMP W
                 GROUP BY W.EMPLOYEE_ID, W.WORKINGDAY, W.ORG_ID, W.TITLE_ID) W;
      -- start autogentimesheet
      INSERT ALL INTO AT_TIME_TIMESHEET_DAILY
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         WORKINGDAY,
         WORKINGHOUR,
         MANUAL_ID,
         CREATED_DATE,
         CREATED_BY,
         CREATED_LOG,
         MODIFIED_DATE,
         MODIFIED_BY,
         MODIFIED_LOG)
      VALUES
        (SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         WORKINGDAY,
         WORKINGHOUR,
         MANUAL_ID,
         SYSDATE,
         UPPER(P_USERNAME),
         UPPER(P_USERNAME),
         SYSDATE,
         UPPER(P_USERNAME),
         UPPER(P_USERNAME)) INTO At_Time_Timesheet_Origin
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         WORKINGDAY,
         WORKINGHOUR,
         MANUAL_ID,
         CREATED_DATE,
         CREATED_BY,
         CREATED_LOG,
         MODIFIED_DATE,
         MODIFIED_BY,
         MODIFIED_LOG)
      VALUES
        (Seq_At_Time_Timesheet_Origin.NEXTVAL,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         WORKINGDAY,
         WORKINGHOUR,
         MANUAL_ID,
         SYSDATE,
         UPPER(P_USERNAME),
         UPPER(P_USERNAME),
         SYSDATE,
         UPPER(P_USERNAME),
         UPPER(P_USERNAME))
        select W.EMPLOYEE_ID,
               W.ORG_ID,
               W.TITLE_ID,
               W.WORKINGDAY,
               W.WORKINGHOUR,
               W.MANUAL_ID
          FROM (SELECT CHOSEN_EMP.EMPLOYEE_ID,
                       CHOSEN_EMP.ORG_ID,
                       CHOSEN_EMP.TITLE_ID,
                       DATERANGES.CDATE AS WORKINGDAY,
                       8 AS WORKINGHOUR,
                       CASE
                         WHEN FN_GET_CN_START(DATERANGES.CDATE) = 0 THEN
                          22
                         WHEN (select count(*)
                                 from AT_HOLIDAY HOLIDAY
                                WHERE HOLIDAY.WORKINGDAY = DATERANGES.CDATE) > 0 THEN
                          35
                         ELSE
                          1
                       END AS MANUAL_ID
                  FROM AT_CHOSEN_EMP CHOSEN_EMP
                  join HU_EMPLOYEE HU_EMP
                    ON HU_EMP.ID = CHOSEN_EMP.EMPLOYEE_ID
                 cross join Table(TABLE_LISTDATE(PV_FROMDATE, PV_ENDDATE)) DATERANGES
                 where HU_EMP.AUTOGENTIMESHEET = -1) W;
    END;
  
    PROCEDURE PRI_AUTO_SHIFT(P_USERNAME   IN NVARCHAR2,
                             P_ORG_ID     IN NUMBER,
                             P_PERIOD_ID  IN NUMBER,
                             P_ISDISSOLVE IN NUMBER) IS
    PV_ENDDATE DATE;
    PV_FROMDATE DATE;
    PV_REQUEST_ID NUMBER;
    BEGIN
      SELECT T.START_DATE, T.END_DATE
      INTO PV_FROMDATE, PV_ENDDATE
      FROM AT_PERIOD T
      WHERE T.ID = P_PERIOD_ID;
      PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
      INSERT INTO AT_CHOSEN_ORG E
        (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
           FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                      P_ORG_ID,
                                      P_ISDISSOLVE)) O
          WHERE EXISTS (SELECT 1
                   FROM AT_ORG_PERIOD OP
                  WHERE OP.PERIOD_ID = P_PERIOD_ID
                    AND OP.ORG_ID = O.ORG_ID
                    AND OP.STATUSCOLEX = 1));
  
      -- insert emp can tinh toan
      INSERT INTO AT_CHOSEN_EMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE
           FROM HU_EMPLOYEE T
           --==CHI THAO TAC TREN DS NV VIEN SETUP CA TU DONG
           INNER JOIN AT_WORKSIGN ATW
             ON T.ID = ATW.EMPLOYEE_ID
             AND ATW.PERIOD_ID = P_PERIOD_ID
             AND ATW.CREATED_LOG ='AUTO'
           --==
          INNER JOIN (SELECT E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
             ON O.ORG_ID = W.ORG_ID
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  --==KHI THIET LAP LAI SE XOA NHUNG CA AUTO
      DELETE FROM AT_WORKSIGN T
       WHERE T.CREATED_LOG = 'AUTO'
         AND T.PERIOD_ID = P_PERIOD_ID
      AND EXISTS(
        SELECT (1) FROM AT_CHOSEN_EMP E WHERE E.EMPLOYEE_ID = T.EMPLOYEE_ID);
  
      INSERT INTO AT_WORKSIGN T
        (T.ID,
         T.EMPLOYEE_ID,
         T.WORKINGDAY,
         T.SHIFT_ID,
         T.PERIOD_ID,
         T.CREATED_DATE,
         T.CREATED_BY,
         T.CREATED_LOG)
        SELECT SEQ_AT_WORKSIGN.NEXTVAL,
               EE.EMPLOYEE_ID,
               C.CDATE,
               EE.SHIFT_ID SHIFT_ID,
               P_PERIOD_ID,
               SYSDATE,
               UPPER(P_USERNAME),
               'AUTO'
          FROM (SELECT T.EMPLOYEE_ID,
                       CASE
                         WHEN T.EFFECT_DATE_FROM > PV_FROMDATE THEN
                          T.EFFECT_DATE_FROM
                         ELSE
                          PV_FROMDATE
                       END START_DELETE,
                       CASE
                         WHEN T.EFFECT_DATE_TO < PV_ENDDATE THEN
                          T.EFFECT_DATE_TO
                         ELSE
                          PV_ENDDATE
                       END END_DELETE,
                       S.ID SHIFT_ID
                  FROM AT_SIGNDEFAULT T
                 INNER JOIN AT_CHOSEN_EMP EE
                    ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
                 INNER JOIN AT_SHIFT S
                    ON T.SINGDEFAULE = S.ID
                 WHERE T.ACTFLG = 'A'
                   AND S.ACTFLG = 'A'
                   AND T.EFFECT_DATE_FROM <= PV_ENDDATE
                   AND NVL(T.EFFECT_DATE_TO, PV_FROMDATE) >= PV_FROMDATE) EE
         CROSS JOIN TABLE(TABLE_LISTDATE(PV_FROMDATE, PV_ENDDATE)) C
         WHERE TO_CHAR(C.CDATE, 'DY') <> 'SUN'
           AND NOT EXISTS
         (SELECT H.ID FROM AT_HOLIDAY H WHERE H.WORKINGDAY = C.CDATE);
  
  
    END;
  
    FUNCTION FN_CALL_TN(P_EMPLOYEE_ID IN NUMBER,
                        P_ENDDATE IN DATE,
                        P_YEAR_TN NUMBER,
                        P_DAY_TN  NUMBER)
      RETURN NUMBER AS
      PRAGMA AUTONOMOUS_TRANSACTION;
      RTNVALUE NUMBER;
      PV_MONTH_TN NUMBER;
      PV_JOIN_DATE DATE;
    BEGIN
  
    SELECT E.JOIN_DATE INTO PV_JOIN_DATE
    FROM HU_EMPLOYEE E
    WHERE E.ID =P_EMPLOYEE_ID;
  
    SELECT ROUND(MONTHS_BETWEEN(P_ENDDATE + 1,PV_JOIN_DATE)/12,2)
           INTO PV_MONTH_TN
    FROM DUAL;
  
    SELECT CASE WHEN  PV_MONTH_TN < P_YEAR_TN THEN
                       0
                WHEN  PV_MONTH_TN >= P_YEAR_TN  AND  PV_MONTH_TN < P_YEAR_TN*2 THEN
                       P_DAY_TN
                WHEN  PV_MONTH_TN >= P_YEAR_TN*2  AND  PV_MONTH_TN < P_YEAR_TN*3 THEN
                       P_DAY_TN*2
                WHEN  PV_MONTH_TN >= P_YEAR_TN*3  AND  PV_MONTH_TN < P_YEAR_TN*4 THEN
                       P_DAY_TN*3
                WHEN  PV_MONTH_TN >= P_YEAR_TN*4  AND  PV_MONTH_TN < P_YEAR_TN*5 THEN
                       P_DAY_TN*4
                WHEN  PV_MONTH_TN >= P_YEAR_TN*5  AND  PV_MONTH_TN < P_YEAR_TN*6 THEN
                       P_DAY_TN*5
                WHEN  PV_MONTH_TN >= P_YEAR_TN*6  AND  PV_MONTH_TN < P_YEAR_TN*7 THEN
                       P_DAY_TN*6
                WHEN  PV_MONTH_TN >= P_YEAR_TN*7  AND  PV_MONTH_TN < P_YEAR_TN*8 THEN
                       P_DAY_TN*7
           END
    INTO RTNVALUE
    FROM DUAL;
  
  
  
      RETURN RTNVALUE;
    EXCEPTION
      WHEN OTHERS THEN
        RETURN 0;
        SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                                'PKG_ATTENDANCE_BUSINESS.FN_CALL_TN',
                                SQLERRM || '_' ||
                                DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                                NULL,
                                NULL);
  
    END FN_CALL_TN;
  */

  /*IN DON DANG KY NGHI*/
  PROCEDURE PRINT_DONPHEP(P_ID          IN NUMBER,
                          P_EMPLOYEE_ID IN NUMBER,
                          P_DATE_TIME   IN DATE,
                          P_CUR         OUT CURSOR_TYPE) IS
    PV_PHEP_APP       NUMBER;
    PHEP_NGHI_NAM     NUMBER;
    PV_PHEP_CHE_DO    NUMBER;
    PV_PHEP_THAM_NIEN NUMBER;
    PV_PHEP_CONLAI    NUMBER;
    PV_PHEP_DA_NGHI   NUMBER;
    PV_PHEP_QUYDOI    NUMBER;
  BEGIN
  
    SELECT SUM(CASE
                 WHEN F.ID = F2.ID AND F.CODE = 'P' THEN
                  1
                 WHEN (F.CODE = 'P' OR F2.CODE = 'P') AND F.ID <> F2.ID THEN
                  .5
                 ELSE
                  0
               END)
      INTO PV_PHEP_APP
      FROM At_Leavesheet_Detail A
      LEFT JOIN AT_TIME_MANUAL M
        ON A.MANUAL_ID = M.ID
      LEFT JOIN AT_FML F
        ON M.MORNING_ID = F.ID
      LEFT JOIN AT_FML F2
        ON M.AFTERNOON_ID = F2.ID
     WHERE A.EMPLOYEE_ID = P_EMPLOYEE_ID
       AND TO_CHAR(A.Leave_Day, 'yyyy') = TO_CHAR(P_DATE_TIME, 'yyyy')
       AND M.CODE LIKE '%P%';
  
    SELECT SUM(NVL(PR.NVALUE, 0))
      INTO PHEP_NGHI_NAM
      FROM AT_PORTAL_REG PR
     INNER JOIN AT_TIME_MANUAL M
        ON M.ID = PR.ID_SIGN
     WHERE PR.ID_EMPLOYEE = P_EMPLOYEE_ID
       AND TO_CHAR(PR.FROM_DATE, 'yyyy') = TO_CHAR(P_DATE_TIME, 'yyyy')
       AND PR.STATUS NOT IN (1, 2)
       AND (M.MORNING_ID = 251 OR M.AFTERNOON_ID = 251)
       AND PR.SVALUE = 'LEAVE'
       AND M.CODE like '%P%'
       AND (PR.ID_REGGROUP <> P_ID OR P_ID = 0);
  
    SELECT TEMP.PHEP_CHE_DO,
           TEMP.PHEP_THAM_NIEN,
           TEMP.PHEP_CONLAI,
           TEMP.PHEP_DA_NGHI,
           TEMP.PHEP_QUYDOI
      INTO PV_PHEP_CHE_DO,
           PV_PHEP_THAM_NIEN,
           PV_PHEP_CONLAI,
           PV_PHEP_DA_NGHI,
           PV_PHEP_QUYDOI
      FROM (SELECT NVL(TOTAL_HAVE1, 0) AS PHEP_CHE_DO,
                   NVL(EN.SENIORITYHAVE, 0) AS PHEP_THAM_NIEN,
                   NVL(EN.PREVTOTAL_HAVE, 0) AS PHEP_CONLAI,
                   NVL(PHEP_NGHI_NAM, 0) + NVL(PV_PHEP_APP, 0) AS PHEP_DA_NGHI,
                   NVL(EN.TIME_OUTSIDE_COMPANY, 0) AS PHEP_QUYDOI
              FROM AT_ENTITLEMENT EN
             WHERE EN.YEAR = TO_CHAR(P_DATE_TIME, 'yyyy')
               AND EN.MONTH <= TO_CHAR(P_DATE_TIME, 'MM')
               AND EN.EMPLOYEE_ID = P_EMPLOYEE_ID
             ORDER BY EN.YEAR, EN.MONTH DESC) TEMP
     WHERE ROWNUM = 1;
  
    OPEN P_CUR FOR
      SELECT NVL(E.EMPLOYEE_CODE, '') AS MASO_NV,
             NVL(E.FULLNAME_VN, '') AS HO_TEN_NV,
             NVL(HOV.NAME_VN, '') AS DON_VI_NV,
             NVL(HTITLE.NAME_EN, '') AS CHUC_DANH_NV,
             NVL(HAP.NVALUE, 0) AS TONG_NGAY_NGHI,
             TO_CHAR(HAP.FROM_DATE, 'dd/MM/YYYY') AS START_DATE,
             TO_CHAR(HAP.TO_DATE, 'dd/MM/YYYY') AS END_DATE,
             NVL(ATTM.DAYIN_KH, 0) AS NGAY_TRONG_KEHOACH,
             NVL(ATTM.DAYOUT_KH, 0) AS NGAY_NGOAI_KEHOACH,
             NVL(ATTM.REASON, '') AS LY_DO_NGHI,
             UPPER(NVL(AT_MANUAL.NAME, '')) AS LOAI_NGHI_UPPER,
             NVL(AT_MANUAL.NAME, '') AS LOAI_NGHI_LOWER,
             NVL(PV_PHEP_CHE_DO, 0) + NVL(PV_PHEP_THAM_NIEN, 0) +
             NVL(PV_PHEP_CONLAI, 0) AS PHEP_HIENCO,
             NVL(PV_PHEP_CHE_DO, 0) AS PHEP_CHEDO,
             NVL(PV_PHEP_THAM_NIEN, 0) AS PHEP_THAMNIEN,
             NVL(PV_PHEP_CONLAI, 0) AS PHEP_CONLAI,
             NVL(PV_PHEP_DA_NGHI, 0) AS PHEP_DANGHI,
             NVL(PV_PHEP_QUYDOI, 0) AS PHEP_QUYDOI,
             TO_CHAR(UNISTR('ng\00E0y ')) || TO_CHAR(SYSDATE, 'DD') || ', ' ||
             TO_CHAR(UNISTR('th\00E1ng ')) || TO_CHAR(SYSDATE, 'MM') || ', ' ||
             TO_CHAR(UNISTR('n\0103m ')) || TO_CHAR(SYSDATE, 'YYYY') AS NGAY_XUAT_BC
        FROM AT_PORTAL_REG ATTM
       INNER JOIN HU_EMPLOYEE E
          ON ATTM.ID_EMPLOYEE = E.ID
        LEFT JOIN HUV_ORGANIZATION HOV
          ON HOV.ID = E.ORG_ID
        LEFT JOIN HU_TITLE HTITLE
          ON HTITLE.ID = E.TITLE_ID
        LEFT JOIN HUV_AT_PORTAL HAP
          ON HAP.ID = ATTM.ID_REGGROUP
        LEFT JOIN AT_TIME_MANUAL AT_MANUAL
          ON AT_MANUAL.ID = ATTM.ID_SIGN
       WHERE ATTM.ID = P_ID;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.PRINT_DONPHEP',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                              P_ID);
  END;

  PROCEDURE CALL_ENTITLEMENT_TNG(P_USERNAME   VARCHAR2,
                                 P_ORG_ID     IN NUMBER,
                                 P_PERIOD_ID  IN NUMBER,
                                 P_ISDISSOLVE IN NUMBER) IS
  
    PV_FROMDATE    DATE;
    PV_ENDDATE     DATE;
    PV_MONTH       NUMBER;
    PV_YEAR        NUMBER;
    PV_I           NUMBER;
    PV_REQUEST_ID  NUMBER;
    PV_CAL_I       NUMBER;
    PVC_YEAR       NUMBER;
    PVC_CAL_I      NUMBER;
    PV_CHEKDATE    DATE;
    PVC_ENDDATE    DATE;
    PVC_FROMDATE   DATE;
    PV_PERIOD      NUMBER;
    PVC_MONTHDATE  NVARCHAR2(10);
    PVC_MONTH      NUMBER;
    PV_SQL         CLOB;
    PV_REQUEST_ID1 NUMBER;
  BEGIN
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG1 E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
  
    SELECT TO_DATE('15/' || TO_CHAR(P.END_DATE, 'MM/yyyy'), 'dd/MM/yyyy'),
           P.START_DATE,
           P.END_DATE,
           P.YEAR,
           P.MONTH
      INTO PV_CHEKDATE, PV_FROMDATE, PV_ENDDATE, PV_YEAR, PV_MONTH
      FROM AT_PERIOD P
     WHERE P.ID = P_PERIOD_ID;
    FOR PV_CAL_I IN PV_MONTH .. PV_MONTH + 2 LOOP
    
      IF PV_CAL_I = 13 THEN
        PVC_CAL_I := 1;
        PVC_YEAR  := PV_YEAR + 1;
      END IF;
    
      IF PV_CAL_I = 14 THEN
        PVC_CAL_I := 2;
        PVC_YEAR  := PV_YEAR + 1;
      END IF;
    
      IF PV_CAL_I <= 12 THEN
        PVC_CAL_I := PV_CAL_I;
        PVC_YEAR  := PV_YEAR;
      END IF;
    
      PV_REQUEST_ID1 := SEQ_AT_REQUEST.NEXTVAL;
    
      SELECT P.START_DATE,
             P.END_DATE,
             P.ID,
             TO_CHAR(P.END_DATE, 'yyyyMM'),
             P.MONTH
        INTO PVC_FROMDATE, PVC_ENDDATE, PV_PERIOD, PVC_MONTHDATE, PVC_MONTH
        FROM AT_PERIOD P
       WHERE P.YEAR = PVC_YEAR
         AND P.MONTH = PVC_CAL_I;
    
      INSERT INTO AT_CHOSEN_EMP_TEMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE,
         CONTRACT_TYPE,
         CONTRACT_DATE,
         QP_YEAR,
         TN_YEAR,
         TN_DAY,
         QP_MONTH,
         TO_LEAVE_YEAR)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID1,
                /*T.JOIN_DATE,*/
                T.SENIORITY_DATE JOIN_DATE,
                T.JOIN_DATE_STATE,
                CT.CONTRACT_TYPE,
                CT.CONTRACT_DATE,
                FN_GET_PHEP('YEAR_P', W.ORG_ID, PVC_YEAR),
                FN_GET_PHEP('YEAR_TN', W.ORG_ID, PVC_YEAR),
                FN_GET_PHEP('DAY_TN', W.ORG_ID, PVC_YEAR),
                ROUND(FN_GET_PHEP('YEAR_P', W.ORG_ID, PVC_YEAR) / 12, 2) + CASE
                  WHEN TO_CHAR(T.SENIORITY_DATE, 'yyyyMM') <
                       TO_CHAR(T.JOIN_DATE, 'yyyyMM') AND
                       TO_CHAR(T.JOIN_DATE, 'yyyyMM') =
                       TO_CHAR(PVC_ENDDATE, 'yyyyMM') THEN
                   CASE
                     WHEN TO_CHAR(T.SENIORITY_DATE, 'DD') > 15 THEN
                      TO_CHAR(ADD_MONTHS(T.JOIN_DATE,
                                         -1 - TO_NUMBER(TO_CHAR(T.SENIORITY_DATE,
                                                                'MM'))),
                              'MM') *
                      ROUND(FN_GET_PHEP('YEAR_P', W.ORG_ID, PVC_YEAR) / 12, 2)
                     ELSE
                     
                      TO_CHAR(ADD_MONTHS(T.JOIN_DATE,
                                         -TO_NUMBER(TO_CHAR(T.SENIORITY_DATE,
                                                            'MM'))),
                              'MM') *
                      ROUND(FN_GET_PHEP('YEAR_P', W.ORG_ID, PVC_YEAR) / 12, 2)
                   END
                  ELSE
                   0
                END IF,
                FN_GET_PHEP('TO_LEAVE_YEAR', W.ORG_ID, PVC_YEAR)
           FROM HU_EMPLOYEE T
          INNER JOIN (SELECT E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE ASC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PVC_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_WAGE = 0
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
           LEFT JOIN (SELECT E.EMPLOYEE_ID,
                            O.CODE CONTRACT_TYPE,
                            E.START_DATE CONTRACT_DATE,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.START_DATE ASC) AS ROW_NUMBER
                       FROM HU_CONTRACT E
                      INNER JOIN HU_CONTRACT_TYPE S
                         ON S.ID = E.CONTRACT_TYPE_ID
                      INNER JOIN OT_OTHER_LIST O
                         ON O.ID = S.TYPE_ID
                      WHERE E.START_DATE <= PVC_ENDDATE
                        AND E.STATUS_ID = 447) CT
             ON T.ID = CT.EMPLOYEE_ID
            AND CT.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID
                       FROM AT_CHOSEN_ORG1 O
                      WHERE O.REQUEST_ID = PV_REQUEST_ID) O
             ON O.ORG_ID = W.ORG_ID
          WHERE T.JOIN_DATE <= PVC_ENDDATE
            AND NVL(T.IS_KIEM_NHIEM, 0) = 0
            AND (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PVC_FROMDATE)));
    
      DELETE FROM AT_ENTITLEMENT T
       WHERE EXISTS (SELECT a.ID
                FROM AT_ENTITLEMENT a
               INNER JOIN AT_CHOSEN_EMP_TEMP EMP
                  ON EMP.EMPLOYEE_ID = a.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID1
               WHERE T.EMPLOYEE_ID = EMP.EMPLOYEE_ID
                 AND a.YEAR = PVC_YEAR
                 AND a.MONTH = PVC_CAL_I)
         and t.YEAR = PVC_YEAR
         AND t.MONTH = PVC_CAL_I;
    
      INSERT INTO AT_ENTITLEMENT T
        (ID,
         EMPLOYEE_ID,
         YEAR,
         MONTH,
         WORKING_TIME_HAVE,
         TOTAL_HAVE1,
         CREATED_BY,
         CREATED_DATE,
         PREV_HAVE,
         PREV_USED,
         CUR_HAVE,
         CUR_USED,
         CUR_HAVE1,
         CUR_HAVE2,
         CUR_HAVE3,
         CUR_HAVE4,
         CUR_HAVE5,
         CUR_HAVE6,
         CUR_HAVE7,
         CUR_HAVE8,
         CUR_HAVE9,
         CUR_HAVE10,
         CUR_HAVE11,
         CUR_HAVE12,
         CUR_USED1,
         CUR_USED2,
         CUR_USED3,
         CUR_USED4,
         CUR_USED5,
         CUR_USED6,
         CUR_USED7,
         CUR_USED8,
         CUR_USED9,
         CUR_USED10,
         CUR_USED11,
         CUR_USED12,
         PREV_USED1,
         PREV_USED2,
         PREV_USED3,
         PREV_USED4,
         PREV_USED5,
         PREV_USED6,
         PREV_USED7,
         PREV_USED8,
         PREV_USED9,
         PREV_USED10,
         PREV_USED11,
         PREV_USED12,
         TO_LEAVE_YEAR,
         QP_YEAR,
         TN_YEAR,
         TN_DAY,
         SENIORITY,
         SENIORITYHAVE,
         ADJUST_MONTH_TN,
         EXPIREDATE,
         PERIOD_ID,
         QP_MONTH)
        SELECT SEQ_AT_ENTITLEMENT.NEXTVAL,
               EMP.EMPLOYEE_ID,
               PVC_YEAR,
               PVC_CAL_I,
               EXTRACT(YEAR FROM EMP.JOIN_DATE),
               CASE
                 WHEN TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1, EMP.JOIN_DATE)) >= 12 THEN
                  NVL(EMP.QP_YEAR, 0)
                 WHEN TO_CHAR(EMP.JOIN_DATE, 'dd') > 15 and
                      TO_CHAR(EMP.JOIN_DATE, 'yyyyMM') =
                      TO_CHAR(PVC_ENDDATE, 'yyyyMM') THEN
                  NVL(TOTAL_HAVE1, 0) /*+ NVL(B.ADJUST_MONTH_TN1, 0)*/
                 ELSE
                  NVL(TOTAL_HAVE1, 0) + NVL(EMP.QP_MONTH, 0) /*+
                                                                                                                                                                                                                                    NVL(B.ADJUST_MONTH_TN1, 0)*/
               END,
               UPPER(P_USERNAME),
               SYSDATE,
               A.PREV_HAVE,
               A.PREV_USED,
               A.CUR_HAVE,
               A.CUR_USED,
               A.CUR_HAVE1,
               A.CUR_HAVE2,
               A.CUR_HAVE3,
               A.CUR_HAVE4,
               A.CUR_HAVE5,
               A.CUR_HAVE6,
               A.CUR_HAVE7,
               A.CUR_HAVE8,
               A.CUR_HAVE9,
               A.CUR_HAVE10,
               A.CUR_HAVE11,
               A.CUR_HAVE12,
               A.CUR_USED1,
               A.CUR_USED2,
               A.CUR_USED3,
               A.CUR_USED4,
               A.CUR_USED5,
               A.CUR_USED6,
               A.CUR_USED7,
               A.CUR_USED8,
               A.CUR_USED9,
               A.CUR_USED10,
               A.CUR_USED11,
               A.CUR_USED12,
               A.PREV_USED1,
               A.PREV_USED2,
               A.PREV_USED3,
               A.PREV_USED4,
               A.PREV_USED5,
               A.PREV_USED6,
               A.PREV_USED7,
               A.PREV_USED8,
               A.PREV_USED9,
               A.PREV_USED10,
               A.PREV_USED11,
               A.PREV_USED12,
               EMP.TO_LEAVE_YEAR,
               EMP.QP_YEAR,
               EMP.TN_YEAR,
               EMP.TN_DAY,
               TRUNC(MONTHS_BETWEEN(PVC_ENDDATE + 1, EMP.JOIN_DATE)),
               FN_CALL_TN(EMP.EMPLOYEE_ID,
                          PVC_ENDDATE,
                          EMP.TN_YEAR,
                          EMP.TN_DAY),
               NVL(B.ADJUST_MONTH_TN1, 0),
               CASE
                 WHEN FN_GET_EXPIREDATE(EMP.ORG_ID, PVC_YEAR) <> '0' THEN
                  TO_DATE(FN_GET_EXPIREDATE(EMP.ORG_ID, PVC_YEAR) || '/' ||
                          PVC_YEAR,
                          'dd/MM/yyyy')
                 ELSE
                  NULL
               END,
               PV_PERIOD,
               EMP.QP_MONTH
          FROM AT_CHOSEN_EMP_TEMP EMP
          LEFT JOIN AT_ENTITLEMENT A
            ON A.EMPLOYEE_ID = EMP.EMPLOYEE_ID
           AND A.YEAR = PVC_YEAR
           AND A.MONTH = PVC_CAL_I - 1
          LEFT JOIN (SELECT ENT1.EMPLOYEE_ID,
                            SUM(ENT1.ADJUST_MONTH_TN) ADJUST_MONTH_TN1
                       FROM AT_DECLARE_ENTITLEMENT ENT1
                      WHERE TO_CHAR(ENT1.START_DATE, 'yyyyMM') <=
                            TO_CHAR(PVC_ENDDATE, 'yyyyMM')
                        AND (ENT1.END_DATE IS NULL OR
                             TO_CHAR(ENT1.END_DATE, 'yyyyMM') >=
                             TO_CHAR(PVC_ENDDATE, 'yyyyMM'))
                        AND EXISTS
                      (SELECT EMP.EMPLOYEE_ID
                               FROM AT_CHOSEN_EMP_TEMP EMP
                              WHERE EMP.EMPLOYEE_ID = ENT1.EMPLOYEE_ID
                                AND EMP.REQUEST_ID = PV_REQUEST_ID1)
                      GROUP BY ENT1.EMPLOYEE_ID) B
            ON B.EMPLOYEE_ID = EMP.EMPLOYEE_ID
         WHERE EMP.REQUEST_ID = PV_REQUEST_ID1;
    
      --Update so phep tu bang cong thang qua
      PV_SQL := 'UPDATE AT_ENTITLEMENT ENT SET ENT.CUR_USED' ||
                EXTRACT(MONTH FROM PVC_ENDDATE) ||
                ' = NVL((SELECT SUM(D.DAY_NUM)
                  FROM AT_LEAVESHEET L
                  INNER JOIN OT_OTHER_LIST O
                     ON O.ID = L.STATUS
                     AND O.TYPE_ID = 7
                     AND O.CODE = ''A''
                    INNER JOIN AT_LEAVESHEET_DETAIL D
                     ON D.LEAVESHEET_ID = L.ID
                    INNER JOIN AT_TIME_MANUAL M
                     ON M.ID = D.MANUAL_ID
                   WHERE TO_CHAR(D.LEAVE_DAY,''yyyyMM'') = ''' ||
                PVC_MONTHDATE || '''
                   AND M.CODE = ''P''
                   AND D.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                   GROUP BY D.EMPLOYEE_ID ),0) + NVL(ENT.AL_T' ||
                EXTRACT(MONTH FROM PVC_ENDDATE) || ',0)
                WHERE ENT.YEAR = ' || PVC_YEAR || '
                AND ENT.MONTH = ' || PVC_CAL_I || '
                    AND EXISTS(SELECT EMP.EMPLOYEE_ID FROM AT_CHOSEN_EMP_TEMP EMP
                                WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                                AND EMP.REQUEST_ID = ' ||
                PV_REQUEST_ID1 || ')';
      /* INSERT INTO AT_STRSQL(ID,STRINGSQL)
      VALUES(PV_REQUEST_ID,PV_SQL);*/
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
    
      /* UPDATE AT_ENTITLEMENT A
        SET A.PREV_USED  = FN_GET_PREV_USED(NVL(A.TO_LEAVE_YEAR, 1), A.ID),
            A.TOTAL_HAVE = NVL(A.TOTAL_HAVE1, 0) + NVL(A.SENIORITYHAVE, 0)
      WHERE A.YEAR = PVC_YEAR
        AND A.MONTH = PVC_CAL_I
        AND EXISTS (SELECT EMP.EMPLOYEE_ID
               FROM AT_CHOSEN_EMP_TEMP EMP
              WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                AND EMP.REQUEST_ID = PV_REQUEST_ID1);*/
    
      -- Tinh so phep nam truoc con lai
      MERGE INTO AT_ENTITLEMENT A
      USING (SELECT ENT1.EMPLOYEE_ID, ENT1.PREV_HAVE
               FROM AT_ENTITLEMENT_PREV_HAVE ENT1
              WHERE ENT1.YEAR = PVC_YEAR
                AND EXISTS (SELECT EMP.EMPLOYEE_ID
                       FROM AT_CHOSEN_EMP_TEMP EMP
                      WHERE EMP.EMPLOYEE_ID = ENT1.EMPLOYEE_ID
                        AND EMP.REQUEST_ID = PV_REQUEST_ID1)) B
      ON (a.EMPLOYEE_ID = b.EMPLOYEE_ID)
      WHEN MATCHED THEN
        UPDATE
           SET A.PREV_HAVE = CASE
                               WHEN NVL(B.PREV_HAVE, 0) < 0 THEN
                                0
                               ELSE
                                NVL(B.PREV_HAVE, 0)
                             END
         WHERE A.YEAR = PVC_YEAR
           AND A.MONTH = PVC_CAL_I
           AND EXISTS (SELECT EMP.EMPLOYEE_ID
                  FROM AT_CHOSEN_EMP_TEMP EMP
                 WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                   AND EMP.REQUEST_ID = PV_REQUEST_ID1);
    
      -- TINH LAI SO PHEP NAM TRUOC DA NGHI
      UPDATE AT_ENTITLEMENT A
         SET A.PREV_USED = CASE
                             WHEN NVL(A.PREV_HAVE, 0) = 0 THEN
                              0
                             WHEN NVL(A.PREV_HAVE, 0) < NVL(A.PREV_USED, 0) THEN
                              A.PREV_HAVE
                             ELSE
                              A.PREV_USED
                           END
       WHERE A.YEAR = PVC_YEAR
         AND A.MONTH = PVC_CAL_I
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP_TEMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID1);
    
      -- TINH LAI SO PHEP NAM TRUOC CON LAI DUOC SU DUNG
      UPDATE AT_ENTITLEMENT A
         SET A.PREVTOTAL_HAVE = CASE
                                  WHEN A.TO_LEAVE_YEAR < PVC_CAL_I THEN
                                   0
                                  ELSE
                                   CASE
                                     WHEN NVL(A.PREV_HAVE, 0) < =
                                          NVL(A.PREV_USED, 0) THEN
                                      0
                                     ELSE
                                      NVL(A.PREV_HAVE, 0) - NVL(A.PREV_USED, 0)
                                   END
                                END
       WHERE A.YEAR = PVC_YEAR
         AND A.MONTH = PVC_CAL_I
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP_TEMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID1);
    
      -- Tinh so ngay nghi tinh cho thang cu
      PV_SQL := 'UPDATE AT_ENTITLEMENT A
     SET A.PREV_USED' || EXTRACT(MONTH FROM PVC_ENDDATE) ||
                ' = CASE WHEN ' || EXTRACT(MONTH FROM PVC_ENDDATE) ||
                ' > A.TO_LEAVE_YEAR THEN
                         0
                     ELSE
                       CASE WHEN A.CUR_USED' ||
                EXTRACT(MONTH FROM PVC_ENDDATE) ||
                ' <= NVL(PREVTOTAL_HAVE,0) THEN
                                 A.CUR_USED' ||
                EXTRACT(MONTH FROM PVC_ENDDATE) || '
                            WHEN A.CUR_USED' ||
                EXTRACT(MONTH FROM PVC_ENDDATE) || ' >= NVL(PREVTOTAL_HAVE,0) THEN
                          NVL(PREVTOTAL_HAVE,0)
                      ELSE
                        0
                     END
                   END
     WHERE A.YEAR =  ' || PVC_YEAR || '
       AND A.MONTH = ' || PVC_CAL_I || '
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP_TEMP EMP
             WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
             AND EMP.REQUEST_ID = ' || PV_REQUEST_ID1 || ')';
    
      INSERT INTO AT_STRSQL (ID, STRINGSQL) VALUES (PV_REQUEST_ID, PV_SQL);
    
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
    
      PV_SQL := 'UPDATE AT_ENTITLEMENT A
     SET A.CUR_USED' || EXTRACT(MONTH FROM PVC_ENDDATE) ||
                ' =  A.CUR_USED' || EXTRACT(MONTH FROM PVC_ENDDATE) ||
                ' -  A.PREV_USED' || EXTRACT(MONTH FROM PVC_ENDDATE) || '
     WHERE A.YEAR =  ' || PVC_YEAR || '
       AND A.MONTH = ' || PVC_CAL_I || '
       AND EXISTS (SELECT EMP.EMPLOYEE_ID
              FROM AT_CHOSEN_EMP_TEMP EMP
             WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
             AND EMP.REQUEST_ID = ' || PV_REQUEST_ID1 || ')';
    
      INSERT INTO AT_STRSQL (ID, STRINGSQL) VALUES (PV_REQUEST_ID, PV_SQL);
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL);
    
      --Tinh toan lai so phep da sd
      UPDATE AT_ENTITLEMENT ENT
         SET ENT.CUR_USED = NVL(ENT.CUR_USED1, 0) + NVL(ENT.CUR_USED2, 0) +
                            NVL(ENT.CUR_USED3, 0) + NVL(ENT.CUR_USED4, 0) +
                            NVL(ENT.CUR_USED5, 0) + NVL(ENT.CUR_USED6, 0) +
                            NVL(ENT.CUR_USED7, 0) + NVL(ENT.CUR_USED8, 0) +
                            NVL(ENT.CUR_USED9, 0) + NVL(ENT.CUR_USED10, 0) +
                            NVL(ENT.CUR_USED11, 0) + NVL(ENT.CUR_USED12, 0) /* +
                                                                                        NVL(ENT.PREV_USED, 0)*/
       WHERE ENT.YEAR = PVC_YEAR
         AND ENT.MONTH = PVC_CAL_I
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP_TEMP EMP
               WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID1);
    
      --Tinh toan lai so phep da sd nam cu
      UPDATE AT_ENTITLEMENT ENT
         SET ENT.PREV_USED = CASE
                               WHEN NVL(ENT.PREV_USED1, 0) +
                                    NVL(ENT.PREV_USED2, 0) +
                                    NVL(ENT.PREV_USED3, 0) +
                                    NVL(ENT.PREV_USED4, 0) +
                                    NVL(ENT.PREV_USED5, 0) +
                                    NVL(ENT.PREV_USED6, 0) +
                                    NVL(ENT.PREV_USED7, 0) +
                                    NVL(ENT.PREV_USED8, 0) +
                                    NVL(ENT.PREV_USED9, 0) +
                                    NVL(ENT.PREV_USED10, 0) +
                                    NVL(ENT.PREV_USED11, 0) +
                                    NVL(ENT.PREV_USED12, 0) <= ENT.PREV_HAVE THEN
                                NVL(ENT.PREV_USED1, 0) +
                                NVL(ENT.PREV_USED2, 0) +
                                NVL(ENT.PREV_USED3, 0) +
                                NVL(ENT.PREV_USED4, 0) +
                                NVL(ENT.PREV_USED5, 0) +
                                NVL(ENT.PREV_USED6, 0) +
                                NVL(ENT.PREV_USED7, 0) +
                                NVL(ENT.PREV_USED8, 0) +
                                NVL(ENT.PREV_USED9, 0) +
                                NVL(ENT.PREV_USED10, 0) +
                                NVL(ENT.PREV_USED11, 0) +
                                NVL(ENT.PREV_USED12, 0)
                               ELSE
                                ENT.PREV_HAVE
                             END
       WHERE ENT.YEAR = PVC_YEAR
         AND ENT.MONTH = PVC_CAL_I
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP_TEMP EMP
               WHERE EMP.EMPLOYEE_ID = ENT.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID1);
    
      -- TINH LAI SO PHEP NAM TRUOC CON LAI DUOC SU DUNG
      UPDATE AT_ENTITLEMENT A
         SET A.PREVTOTAL_HAVE = CASE
                                  WHEN A.TO_LEAVE_YEAR < PVC_CAL_I THEN
                                   0
                                  ELSE
                                  /* NVL(A.PREV_HAVE, 0) - NVL(A.PREV_USED, 0)*/
                                   CASE
                                     WHEN NVL(A.PREV_HAVE, 0) < =
                                          NVL(A.PREV_USED, 0) THEN
                                      0
                                     ELSE
                                      NVL(A.PREV_HAVE, 0) - NVL(A.PREV_USED, 0)
                                   END
                                END
       WHERE A.YEAR = PVC_YEAR
         AND A.MONTH = PVC_CAL_I
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP_TEMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID1);
    
      -- phep con lai
      UPDATE AT_ENTITLEMENT A
         SET A.TOTAL_HAVE = NVL(A.TOTAL_HAVE1, 0) + NVL(A.SENIORITYHAVE, 0) +
                            NVL(A.PREVTOTAL_HAVE, 0) +
                            NVL(A.ADJUST_MONTH_TN, 0) - NVL(A.CUR_USED, 0)
       WHERE A.YEAR = PVC_YEAR
         AND A.MONTH = PVC_CAL_I
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP_TEMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID1);
    
      UPDATE AT_ENTITLEMENT A
         SET A.CUR_HAVE = NVL(A.TOTAL_HAVE1, 0) + NVL(A.ADJUST_MONTH_TN, 0) -
                          NVL(A.CUR_USED, 0)
       WHERE A.YEAR = PVC_YEAR
         AND A.MONTH = PVC_CAL_I
         AND EXISTS (SELECT EMP.EMPLOYEE_ID
                FROM AT_CHOSEN_EMP_TEMP EMP
               WHERE EMP.EMPLOYEE_ID = A.EMPLOYEE_ID
                 AND EMP.REQUEST_ID = PV_REQUEST_ID1);
    
      DELETE AT_CHOSEN_EMP_TEMP E WHERE E.REQUEST_ID = PV_REQUEST_ID1;
      COMMIT;
      PV_REQUEST_ID1 := 0;
    
    END LOOP;
    DELETE AT_CHOSEN_ORG1 A WHERE A.REQUEST_ID = PV_REQUEST_ID;
  EXCEPTION
    WHEN OTHERS THEN
    
      DELETE AT_CHOSEN_EMP_TEMP E WHERE E.REQUEST_ID = PV_REQUEST_ID1;
      DELETE AT_CHOSEN_ORG1 A WHERE A.REQUEST_ID = PV_REQUEST_ID;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CALL_ENTITLEMENT_TNG',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              null,
                              null,
                              null,
                              null);
  END;

  PROCEDURE CAL_TIMETIMESHEET_OT(P_USERNAME   NVARCHAR2,
                                 P_ORG_ID     IN NUMBER,
                                 P_PERIOD_ID  IN NUMBER,
                                 P_ISDISSOLVE IN NUMBER,
                                 P_EMP_OBJ    IN NUMBER) IS
    PV_FROMDATE   DATE;
    PV_ENDDATE    DATE;
    PV_REQUEST_ID NUMBER;
    PV_SQL        CLOB;
  BEGIN
  
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    -- TONG HOP DU LIEU TU DANG KY LAM THEM VS KHAI BAO LAM THEM RA BANG LAM THEM TONG HOP
    SELECT CASE
             WHEN AP.FROMDATE_BEFOREMONTH = -1 THEN
              TO_DATE(AP.FROMDATE_PERIOD || '/' ||
                      TO_CHAR(TO_DATE('01/' || A.MONTH || '/' || A.YEAR,
                                      'DD/MM/YYYY') - 1,
                              'MM/YYYY'),
                      'DD/MM/YYYY')
             ELSE
              TO_DATE(AP.FROMDATE_PERIOD || '/' || A.MONTH || '/' || A.YEAR,
                      'DD/MM/YYYY')
           END START_DATE,
           CASE
             WHEN AP.TODATE_ENDMONTH = -1 THEN
              LAST_DAY(TO_DATE('01/' || A.MONTH || '/' || A.YEAR,
                               'DD/MM/YYYY'))
             ELSE
              TO_DATE(AP.TODATE_PERIOD || '/' || A.MONTH || '/' || A.YEAR,
                      'DD/MM/YYYY')
           END END_DATE
      INTO PV_FROMDATE, PV_ENDDATE
      FROM AT_PERIOD A
     INNER JOIN AT_TIME_PERIOD AP
        ON AP.ID =
           PKG_FUNCTION.GET_TIME_PERIOD_ID(A.MONTH, A.YEAR, P_EMP_OBJ)
     WHERE A.ID = P_PERIOD_ID;
  
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
  
    -- insert emp can tinh toan
    INSERT INTO AT_CHOSEN_EMP E
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       USERNAME,
       REQUEST_ID,
       OBJECT_EMPLOYEE_ID)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              W.OBJECT_EMPLOYEE_ID
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.STAFF_RANK_ID,
                          E.IS_3B,
                          S.LEVEL_STAFF,
                          E.OBJECT_EMPLOYEE_ID,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_ENDDATE
                      AND E.STATUS_ID = 447
                      AND E.IS_WAGE = 0
                      AND E.IS_3B = 0) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
           ON O.ORG_ID = W.ORG_ID
        WHERE NVL(T.IS_KIEM_NHIEM, 0) = 0
          AND W.OBJECT_EMPLOYEE_ID = P_EMP_OBJ
          AND (NVL(T.WORK_STATUS, 0) <> 257 OR
               (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  
    DELETE FROM AT_TIME_TIMESHEET_OT T
     WHERE EXISTS (SELECT a.ID
              FROM AT_TIME_TIMESHEET_OT a
             INNER JOIN AT_CHOSEN_EMP EMP
                ON EMP.EMPLOYEE_ID = a.EMPLOYEE_ID
               AND EMP.REQUEST_ID = PV_REQUEST_ID
             WHERE T.EMPLOYEE_ID = EMP.EMPLOYEE_ID
               AND A.PERIOD_ID = P_PERIOD_ID)
       AND t.PERIOD_ID = P_PERIOD_ID;
  
    INSERT INTO AT_TIME_TIMESHEET_OT
      (ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       PERIOD_ID,
       TOTAL_FACTOR1,
       TOTAL_FACTOR1_5,
       TOTAL_FACTOR1_8,
       TOTAL_FACTOR2,
       TOTAL_FACTOR2_1,
       TOTAL_FACTOR2_7,
       TOTAL_FACTOR3,
       TOTAL_FACTOR3_9,
       OT_DAY,
       OT_NIGHT,
       OT_WEEKEND_DAY,
       OT_WEEKEND_NIGHT,
       OT_HOLIDAY_DAY,
       OT_HOLIDAY_NIGHT,
       TOTAL_NB1,
       TOTAL_NB1_5,
       TOTAL_NB1_8,
       TOTAL_NB2,
       TOTAL_NB2_1,
       TOTAL_NB2_7,
       TOTAL_NB3,
       TOTAL_NB3_9,
       NEW_OT_DAY,
       NEW_OT_NIGHT,
       NEW_OT_WEEKEND_DAY,
       NEW_OT_WEEKEND_NIGHT,
       NEW_OT_HOLIDAY_DAY,
       NEW_OT_HOLIDAY_NIGHT,
       NUMBER_FACTOR_CP,
       SALARY_ID,
       EMP_OBJ_ID,
       FROM_DATE,
       END_DATE,
       CREATED_DATE,
       CREATED_BY,
       MODIFIED_DATE,
       MODIFIED_BY)
      SELECT SEQ_AT_TIME_TIMESHEET_OT.NEXTVAL,
             B.EMPLOYEE_ID,
             B.ORG_ID,
             B.TITLE_ID,
             P_PERIOD_ID,
             B.OT_1,
             B.OT_15,
             B.OT_18,
             B.OT_2,
             B.OT_21,
             B.OT_27,
             B.OT_3,
             B.OT_39,
             B.OT_DAY,
             B.OT_NIGHT,
             B.OT_WEEKEND_DAY,
             B.OT_WEEKEND_NIGHT,
             B.OT_HOLIDAY_DAY,
             B.OT_HOLIDAY_NIGHT,
             B.OT_NB_1,
             B.OT_NB_15,
             B.OT_NB_18,
             B.OT_NB_2,
             B.OT_NB_21,
             B.OT_NB_27,
             B.OT_NB_3,
             B.OT_NB_39,
             B.NEW_OT_DAY,
             B.NEW_OT_NIGHT,
             B.NEW_OT_WEEKEND_DAY,
             B.NEW_OT_WEEKEND_NIGHT,
             B.NEW_OT_HOLIDAY_DAY,
             B.NEW_OT_HOLIDAY_NIGHT,
             B.NUMBER_FACTOR_CP,
             B.SALARY_ID,
             B.OBJECT_EMPLOYEE_ID,
             PV_FROMDATE,
             PV_ENDDATE,
             SYSDATE,
             P_USERNAME,
             SYSDATE,
             P_USERNAME
        FROM (SELECT A.EMPLOYEE_ID,
                     A.ORG_ID,
                     A.TITLE_ID,
                     NVL(SUM(A.OT_1), 0) OT_1,
                     NVL(SUM(A.OT_15), 0) OT_15,
                     NVL(SUM(A.OT_18), 0) OT_18,
                     NVL(SUM(A.OT_2), 0) OT_2,
                     NVL(SUM(A.OT_21), 0) OT_21,
                     NVL(SUM(A.OT_27), 0) OT_27,
                     NVL(SUM(A.OT_3), 0) OT_3,
                     NVL(SUM(A.OT_39), 0) OT_39,
                     NVL(SUM(A.OT_DAY), 0) OT_DAY,
                     NVL(SUM(A.OT_NIGHT), 0) OT_NIGHT,
                     NVL(SUM(A.OT_WEEKEND_DAY), 0) OT_WEEKEND_DAY,
                     NVL(SUM(A.OT_WEEKEND_NIGHT), 0) OT_WEEKEND_NIGHT,
                     NVL(SUM(A.OT_HOLIDAY_DAY), 0) OT_HOLIDAY_DAY,
                     NVL(SUM(A.OT_HOLIDAY_NIGHT), 0) OT_HOLIDAY_NIGHT,
                     NVL(SUM(A.OT_NB_1), 0) OT_NB_1,
                     NVL(SUM(A.OT_NB_15), 0) OT_NB_15,
                     NVL(SUM(A.OT_NB_18), 0) OT_NB_18,
                     NVL(SUM(A.OT_NB_2), 0) OT_NB_2,
                     NVL(SUM(A.OT_NB_21), 0) OT_NB_21,
                     NVL(SUM(A.OT_NB_27), 0) OT_NB_27,
                     NVL(SUM(A.OT_NB_3), 0) OT_NB_3,
                     NVL(SUM(A.OT_NB_39), 0) OT_NB_39,
                     NVL(SUM(A.NEW_OT_DAY), 0) NEW_OT_DAY,
                     NVL(SUM(A.NEW_OT_NIGHT), 0) NEW_OT_NIGHT,
                     NVL(SUM(A.NEW_OT_WEEKEND_DAY), 0) NEW_OT_WEEKEND_DAY,
                     NVL(SUM(A.NEW_OT_WEEKEND_NIGHT), 0) NEW_OT_WEEKEND_NIGHT,
                     NVL(SUM(A.NEW_OT_HOLIDAY_DAY), 0) NEW_OT_HOLIDAY_DAY,
                     NVL(SUM(A.NEW_OT_HOLIDAY_NIGHT), 0) NEW_OT_HOLIDAY_NIGHT,
                     NVL(SUM(A.NUMBER_FACTOR_CP), 0) NUMBER_FACTOR_CP,
                     A.SALARY_ID,
                     A.OBJECT_EMPLOYEE_ID
                FROM (SELECT OT.EMPLOYEE_ID,
                             EMP.TITLE_ID,
                             EMP.ORG_ID,
                             W.ID SALARY_ID,
                             EMP.OBJECT_EMPLOYEE_ID,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_100, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_1,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_150, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_15,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_180, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_18,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_200, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_2,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_210, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_21,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_270, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_27,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_300, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_3,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_390, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_39,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6923 THEN
                                   NVL(OT.TOTAL_OT_TT, 0) +
                                   NVL(OT.HESO_OT_100, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_100, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_NB_1,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6923 THEN
                                   NVL(OT.TOTAL_OT_TT, 0) +
                                   NVL(OT.HESO_OT_150, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_150, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_NB_15,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6923 THEN
                                   NVL(OT.TOTAL_OT_TT, 0) +
                                   NVL(OT.HESO_OT_180, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_180, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_NB_18,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6923 THEN
                                   NVL(OT.TOTAL_OT_TT, 0) +
                                   NVL(OT.HESO_OT_200, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_200, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_NB_2,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6923 THEN
                                   NVL(OT.TOTAL_OT_TT, 0) +
                                   NVL(OT.HESO_OT_210, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_210, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_NB_21,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6923 THEN
                                   NVL(OT.TOTAL_OT_TT, 0) +
                                   NVL(OT.HESO_OT_270, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_270, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_NB_27,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6923 THEN
                                   NVL(OT.TOTAL_OT_TT, 0) +
                                   NVL(OT.HESO_OT_300, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_300, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_NB_3,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6923 THEN
                                   NVL(OT.TOTAL_OT_TT, 0) +
                                   NVL(OT.HESO_OT_390, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6923 THEN
                                      NVL(OT.TOTAL_OT_TT, 0) +
                                      NVL(OT.HESO_OT_390, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_NB_39,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND
                                          OT.SIGN_ID IS NOT NULL AND OT.SIGN_ID <> 1 AND
                                          OT.SIGN_ID <> 2 THEN
                                      NVL(OT.TOTAL_DAY_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_DAY,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND
                                          OT.SIGN_ID IS NOT NULL AND OT.SIGN_ID <> 1 AND
                                          OT.SIGN_ID <> 2 THEN
                                      NVL(OT.TOTAL_NIGHT_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_NIGHT,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND
                                          OT.SIGN_ID IS NULL OR OT.SIGN_ID = 1 THEN
                                      NVL(OT.TOTAL_DAY_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_WEEKEND_DAY,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND
                                          OT.SIGN_ID IS NULL OR OT.SIGN_ID = 1 THEN
                                      NVL(OT.TOTAL_NIGHT_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_WEEKEND_NIGHT,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND OT.SIGN_ID = 2 THEN
                                      NVL(OT.TOTAL_DAY_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_HOLIDAY_DAY,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                0
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE <= WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND OT.SIGN_ID = 2 THEN
                                      NVL(OT.TOTAL_NIGHT_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END OT_HOLIDAY_NIGHT,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6922 AND
                                       OT.SIGN_ID IS NOT NULL AND OT.SIGN_ID <> 1 AND
                                       OT.SIGN_ID <> 2 THEN
                                   NVL(OT.TOTAL_DAY_TT, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND
                                          OT.SIGN_ID IS NOT NULL AND OT.SIGN_ID <> 1 AND
                                          OT.SIGN_ID <> 2 THEN
                                      NVL(OT.TOTAL_DAY_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END NEW_OT_DAY,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6922 AND
                                       OT.SIGN_ID IS NOT NULL AND OT.SIGN_ID <> 1 AND
                                       OT.SIGN_ID <> 2 THEN
                                   NVL(OT.TOTAL_NIGHT_TT, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND
                                          OT.SIGN_ID IS NOT NULL AND OT.SIGN_ID <> 1 AND
                                          OT.SIGN_ID <> 2 THEN
                                      NVL(OT.TOTAL_NIGHT_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END NEW_OT_NIGHT,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6922 AND
                                       OT.SIGN_ID IS NULL OR OT.SIGN_ID = 1 THEN
                                   NVL(OT.TOTAL_DAY_TT, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND
                                          OT.SIGN_ID IS NULL OR OT.SIGN_ID = 1 THEN
                                      NVL(OT.TOTAL_DAY_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END NEW_OT_WEEKEND_DAY,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6922 AND
                                       OT.SIGN_ID IS NULL OR OT.SIGN_ID = 1 THEN
                                   NVL(OT.TOTAL_NIGHT_TT, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND
                                          OT.SIGN_ID IS NULL OR OT.SIGN_ID = 1 THEN
                                      NVL(OT.TOTAL_NIGHT_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END NEW_OT_WEEKEND_NIGHT,
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6922 AND OT.SIGN_ID = 2 THEN
                                   NVL(OT.TOTAL_DAY_TT, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND OT.SIGN_ID = 2 THEN
                                      NVL(OT.TOTAL_DAY_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END NEW_OT_HOLIDAY_DAY,
                             
                             CASE
                               WHEN WK.EFFECT_DATE IS NULL THEN
                                CASE
                                  WHEN OT.OT_TYPE_ID = 6922 AND OT.SIGN_ID = 2 THEN
                                   NVL(OT.TOTAL_NIGHT_TT, 0)
                                  ELSE
                                   0
                                END
                               ELSE
                                CASE
                                  WHEN OT.REGIST_DATE > WK.EFFECT_DATE THEN
                                   CASE
                                     WHEN OT.OT_TYPE_ID = 6922 AND OT.SIGN_ID = 2 THEN
                                      NVL(OT.TOTAL_NIGHT_TT, 0)
                                     ELSE
                                      0
                                   END
                                  ELSE
                                   0
                                END
                             END NEW_OT_HOLIDAY_NIGHT,
                             CASE
                               WHEN OT.OT_TYPE_ID = 6924 THEN
                                NVL(OT.TOTAL_OT_TT, 0)
                               ELSE
                                0
                             END NUMBER_FACTOR_CP
                        FROM AT_OT_REGISTRATION OT
                       INNER JOIN AT_CHOSEN_EMP EMP
                          ON EMP.EMPLOYEE_ID = OT.EMPLOYEE_ID
                         AND EMP.REQUEST_ID = PV_REQUEST_ID
                       INNER JOIN (SELECT E.ID,
                                         E.EMPLOYEE_ID,
                                         ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                                    FROM HU_WORKING E
                                    LEFT JOIN HU_STAFF_RANK S
                                      ON E.STAFF_RANK_ID = S.ID
                                   WHERE E.EFFECT_DATE <= PV_ENDDATE
                                     AND E.STATUS_ID = 447
                                     AND E.IS_WAGE = -1
                                     AND E.IS_3B = 0) W
                          ON OT.EMPLOYEE_ID = W.EMPLOYEE_ID
                         AND W.ROW_NUMBER = 1
                        LEFT JOIN (SELECT E.ID,
                                         E.EMPLOYEE_ID,
                                         E.EFFECT_DATE - 1 EFFECT_DATE,
                                         ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                                    FROM HU_WORKING E
                                    LEFT JOIN HU_STAFF_RANK S
                                      ON E.STAFF_RANK_ID = S.ID
                                   WHERE E.EFFECT_DATE BETWEEN PV_FROMDATE AND
                                         PV_ENDDATE
                                     AND E.STATUS_ID = 447
                                     AND E.IS_WAGE = -1
                                     AND E.IS_3B = 0) WK
                          ON OT.EMPLOYEE_ID = WK.EMPLOYEE_ID
                         AND WK.ROW_NUMBER = 1
                       WHERE OT.STATUS = 1
                         AND NVL(OT.IS_DELETED, 0) = 0
                         AND (TRUNC(OT.REGIST_DATE) BETWEEN PV_FROMDATE AND
                             PV_ENDDATE)) A
               GROUP BY A.EMPLOYEE_ID,
                        A.ORG_ID,
                        A.TITLE_ID,
                        A.SALARY_ID,
                        A.OBJECT_EMPLOYEE_ID) B;
    --EXECUTE IMMEDIATE PV_SQL;
    --anhvn,HSV-636 TONG HOP LAM NGOAI GIO
  UPDATE AT_TIME_TIMESHEET_OT OT
   SET OT.COUNT_OT_SUPPORT =
       (SELECT NVL((SELECT SUM(LEAST((NVL(R.TOTAL_OT_TT, 0) +
                                    NVL(R.HESO_OT_100, 0) +
                                    NVL(R.HESO_OT_150, 0) +
                                    NVL(R.HESO_OT_180, 0) +
                                    NVL(R.HESO_OT_200, 0) +
                                    NVL(R.HESO_OT_210, 0) +
                                    NVL(R.HESO_OT_270, 0) +
                                    NVL(R.HESO_OT_300, 0) +
                                    NVL(R.HESO_OT_390, 0)) / 8,
                                    1))
                     FROM AT_OT_REGISTRATION R
                    WHERE R.EMPLOYEE_ID = OT.EMPLOYEE_ID
                    AND (TRUNC(R.REGIST_DATE) BETWEEN PV_FROMDATE AND
                             PV_ENDDATE)
                      AND R.IS_DELETED = 0
                      AND R.STATUS = 1
                      AND R.OTHER_ORG = 1),
                   0)
          FROM DUAL)
   WHERE EXISTS (SELECT a.ID
              FROM AT_TIME_TIMESHEET_OT a
             INNER JOIN AT_CHOSEN_EMP EMP
                ON EMP.EMPLOYEE_ID = a.EMPLOYEE_ID
               AND EMP.REQUEST_ID = PV_REQUEST_ID
             WHERE OT.EMPLOYEE_ID = EMP.EMPLOYEE_ID
               AND A.PERIOD_ID = P_PERIOD_ID)
       AND OT.PERIOD_ID = P_PERIOD_ID;
   -------
    DELETE AT_CHOSEN_ORG E WHERE E.REQUEST_ID = PV_REQUEST_ID;
    DELETE AT_CHOSEN_EMP E WHERE E.REQUEST_ID = PV_REQUEST_ID;
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      DELETE AT_CHOSEN_ORG E WHERE E.REQUEST_ID = PV_REQUEST_ID;
      DELETE AT_CHOSEN_EMP E WHERE E.REQUEST_ID = PV_REQUEST_ID;
    
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CAL_TIMETIMESHEET_OT',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              null,
                              null,
                              null,
                              null);
  END;

  PROCEDURE INPORT_NB(P_DOCXML    IN CLOB,
                      P_USER      IN NVARCHAR2,
                      P_PERIOD_ID NUMBER) IS
  
    v_DOCXML XMLTYPE;
  BEGIN
    v_DOCXML := XMLTYPE.createXML(P_DOCXML);
  
    MERGE INTO AT_COMPENSATORY_EDIT D
    USING (SELECT E.ID
             FROM XMLTABLE('/NewDataSet/DATA' PASSING v_DOCXML COLUMNS
                           EMPLOYEE_CODE NVARCHAR2(20) PATH
                           './EMPLOYEE_CODE') DOM
            INNER JOIN HU_EMPLOYEE E
               ON E.EMPLOYEE_CODE = DOM.EMPLOYEE_CODE
              AND NVL(E.IS_KIEM_NHIEM, 0) = 0) C
    ON (D.EMPLOYEE_ID = C.ID)
    WHEN MATCHED THEN
      UPDATE
         SET d.Check_Delete = 'Updated' DELETE
       WHERE D.EMPLOYEE_ID = C.ID
         AND D.PERIOD_ID = P_PERIOD_ID
         and d.Check_Delete = 'Updated';
    commit;
  
    INSERT INTO AT_COMPENSATORY_EDIT
      (EMPLOYEE_ID, PERIOD_ID, COMPENSATORY_EDIT, created_date, created_by)
      SELECT E.ID, P_PERIOD_ID, DOM.COMPENSATORY_EDIT, SYSDATE, P_USER
        FROM XMLTABLE('/NewDataSet/DATA' PASSING v_DOCXML COLUMNS
                      EMPLOYEE_CODE NUMBER PATH './EMPLOYEE_CODE',
                      COMPENSATORY_EDIT NUMBER PATH './COMPENSATORY_EDIT') DOM
       INNER JOIN HU_EMPLOYEE E
          ON E.EMPLOYEE_CODE = DOM.EMPLOYEE_CODE
         AND NVL(E.IS_KIEM_NHIEM, 0) = 0;
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_INS_BUSINESS.INPORT_MANAGER_SUN_CARE',
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
                              NULL,
                              NULL,
                              NULL,
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

  PROCEDURE INPORT_NB_PREV(P_DOCXML IN CLOB,
                           P_USER   IN NVARCHAR2,
                           P_YEAR   NUMBER) IS
  
    v_DOCXML XMLTYPE;
  BEGIN
    v_DOCXML := XMLTYPE.createXML(P_DOCXML);
  
    MERGE INTO AT_COMPENSATORY_PREV D
    USING (SELECT E.ID
             FROM XMLTABLE('/NewDataSet/DATA' PASSING v_DOCXML COLUMNS
                           EMPLOYEE_CODE NVARCHAR2(20) PATH
                           './EMPLOYEE_CODE') DOM
            INNER JOIN HU_EMPLOYEE E
               ON E.EMPLOYEE_CODE = DOM.EMPLOYEE_CODE
              AND NVL(E.IS_KIEM_NHIEM, 0) = 0) C
    ON (D.EMPLOYEE_ID = C.ID)
    WHEN MATCHED THEN
      UPDATE
         SET d.Check_Delete = 'Updated' DELETE
       WHERE D.EMPLOYEE_ID = C.ID
         AND D.YEAR = P_YEAR
         and d.Check_Delete = 'Updated';
    commit;
  
    INSERT INTO AT_COMPENSATORY_PREV
      (EMPLOYEE_ID, YEAR, PREV_HAVE, created_date, created_by)
      SELECT E.ID, P_YEAR, DOM.PREV_HAVE, SYSDATE, P_USER
        FROM XMLTABLE('/NewDataSet/DATA' PASSING v_DOCXML COLUMNS
                      EMPLOYEE_CODE NUMBER PATH './EMPLOYEE_CODE',
                      PREV_HAVE NUMBER PATH './PREV_HAVE') DOM
       INNER JOIN HU_EMPLOYEE E
          ON E.EMPLOYEE_CODE = DOM.EMPLOYEE_CODE
         AND NVL(E.IS_KIEM_NHIEM, 0) = 0;
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_INS_BUSINESS.INPORT_MANAGER_SUN_CARE',
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
                              NULL,
                              NULL,
                              NULL,
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

  /*
    PROCEDURE CAL_TIMETIMESHEET_OT(P_USERNAME   NVARCHAR2,
                                   P_ORG_ID     IN NUMBER,
                                   P_PERIOD_ID  IN NUMBER,
                                   P_ISDISSOLVE IN NUMBER) IS
      PV_FROMDATE    DATE;
      PV_ENDDATE     DATE;
      PV_MAX_PAYOT   NUMBER;
      PV_LEVEL_STAFF NUMBER;
      PV_HOUR_CAL_OT NUMBER;
      PV_REQUEST_ID  NUMBER;
      PV_MINUS_ALLOW NUMBER := 50;
    BEGIN
  
      PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
  
      -- TONG HOP DU LIEU TU DANG KY LAM THEM VS KHAI BAO LAM THEM RA BANG LAM THEM TONG HOP
      SELECT P.START_DATE, P.END_DATE
        INTO PV_FROMDATE, PV_ENDDATE
        FROM AT_PERIOD P
       WHERE P.ID = P_PERIOD_ID;
  
      -- Insert org can tinh toan
      INSERT INTO AT_CHOSEN_ORG E
        (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
           FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                      P_ORG_ID,
                                      P_ISDISSOLVE)) O
          WHERE EXISTS (SELECT 1
                   FROM AT_ORG_PERIOD OP
                  WHERE OP.PERIOD_ID = P_PERIOD_ID
                    AND OP.ORG_ID = O.ORG_ID
                    AND OP.STATUSCOLEX = 1));
  
      -- insert emp can tinh toan
      INSERT INTO AT_CHOSEN_EMP E
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         USERNAME,
         REQUEST_ID)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                UPPER(P_USERNAME),
                PV_REQUEST_ID
           FROM HU_EMPLOYEE T
          INNER JOIN (SELECT E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.STAFF_RANK_ID,
                            E.IS_3B,
                            S.LEVEL_STAFF,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        AND E.STATUS_ID = 447
                        AND E.IS_WAGE = 0
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID FROM AT_CHOSEN_ORG O) O
             ON O.ORG_ID = W.ORG_ID
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
  
      INSERT INTO AT_CHOSEN_EMP_CLEAR
        (EMPLOYEE_ID, REQUEST_ID)
        (SELECT EMPLOYEE_ID, PV_REQUEST_ID
           FROM (SELECT A.*,
                        ROW_NUMBER() OVER(PARTITION BY A.EMPLOYEE_ID ORDER BY A.EFFECT_DATE DESC, A.ID DESC) AS ROW_NUMBER
                   FROM HU_WORKING A
                  WHERE A.STATUS_ID = 447
                    AND A.EFFECT_DATE <= PV_ENDDATE
                    AND A.IS_3B = 0) C
          INNER JOIN HU_EMPLOYEE EE
             ON C.EMPLOYEE_ID = EE.ID
            AND C.ROW_NUMBER = 1
          WHERE (NVL(EE.WORK_STATUS, 0) <> 257 OR
                (EE.WORK_STATUS = 257 AND EE.TER_LAST_DATE >= PV_FROMDATE)));
  
      --1. TONG HOP GIO LAM THEM KHI DANG KY LAM THEM
  
      SELECT S.HOUR_MAX_OT, S.LEVEL_STAFF, S.HOUR_CAL_OT
        INTO PV_MAX_PAYOT, PV_LEVEL_STAFF, PV_HOUR_CAL_OT
        FROM (SELECT T.HOUR_MAX_OT, K.LEVEL_STAFF, T.HOUR_CAL_OT
                FROM AT_LIST_PARAM_SYSTEM T
                LEFT JOIN HU_STAFF_RANK K
                  ON T.RANK_PAY_OT = K.ID
               WHERE T.ACTFLG = 'A'
                 AND T.EFFECT_DATE_FROM <= PV_ENDDATE
               ORDER BY ROW_NUMBER() OVER(ORDER BY T.EFFECT_DATE_FROM DESC)) S
       WHERE ROWNUM = 1;
  
      -- INSERT AT_REGISTER_OT_TEMP;
      INSERT INTO AT_REGISTER_OT_TEMP
        (ID,
         EMPLOYEE_ID,
         WORKINGDAY,
         FROM_HOUR,
         TO_HOUR,
         BREAK_HOUR,
         NOTE,
         CREATED_DATE,
         CREATED_BY,
         CREATED_LOG,
         MODIFIED_DATE,
         MODIFIED_BY,
         MODIFIED_LOG,
         TYPE_OT,
         HOUR,
         HS_OT,
         TYPE_INPUT,
         IS_NB,
         WORKING_ID,
         WORKING_START,
         WORKING_END,
         VAL_IN,
         VAL_OUT)
        WITH CTE_WORKING AS
         (SELECT T.ORG_ID,
                 T.TITLE_ID,
                 T.EMPLOYEE_ID,
                 T.ID,
                 T.START_DATE,
                 T.SAL_BASIC,
                 T.STAFF_RANK_ID,
                 T.COST_SUPPORT,
                 T.PERCENT_SALARY,
                 NVL(LAG(T.START_DATE - 1)
                     OVER(PARTITION BY T.EMPLOYEE_ID ORDER BY T.START_DATE DESC,
                          T.ID DESC),
                     PV_ENDDATE) END_DATE
            FROM (SELECT *
                    FROM (SELECT W.ORG_ID,
                                 W.EMPLOYEE_ID,
                                 W.ID,
                                 W.TITLE_ID,
                                 W.STAFF_RANK_ID,
                                 W.EFFECT_DATE START_DATE,
                                 NVL(W.SAL_BASIC, 0) * NVL(W.PERCENT_SALARY, 0) / 100 SAL_BASIC,
                                 NVL(W.COST_SUPPORT, 0) *
                                 NVL(W.PERCENT_SALARY, 0) / 100 COST_SUPPORT,
                                 NVL(W.PERCENT_SALARY, 0) PERCENT_SALARY,
                                 ROW_NUMBER() OVER(PARTITION BY W.EMPLOYEE_ID ORDER BY W.EFFECT_DATE DESC, W.ID DESC) RN
                            FROM HU_WORKING W
                           WHERE W.STATUS_ID = 447
                             AND W.IS_3B = 0
                             AND W.EFFECT_DATE <= PV_FROMDATE)
                   WHERE RN = 1
                  UNION ALL
                  SELECT W.ORG_ID,
                         W.EMPLOYEE_ID,
                         W.ID,
                         W.TITLE_ID,
                         W.STAFF_RANK_ID,
                         W.EFFECT_DATE,
                         NVL(W.SAL_BASIC, 0) * NVL(W.PERCENT_SALARY, 0) / 100 SAL_BASIC,
                         NVL(W.COST_SUPPORT, 0) * NVL(W.PERCENT_SALARY, 0) / 100 COST_SUPPORT,
                         NVL(W.PERCENT_SALARY, 0) PERCENT_SALARY,
                         ROW_NUMBER() OVER(PARTITION BY W.EMPLOYEE_ID ORDER BY W.EFFECT_DATE DESC, W.ID DESC) RN
                    FROM HU_WORKING W
                   WHERE W.STATUS_ID = 447
                     AND W.IS_3B = 0
                     AND W.IS_WAGE = -1
                     AND W.EFFECT_DATE > PV_FROMDATE
                     AND W.EFFECT_DATE <= PV_ENDDATE) T)
        SELECT SEQ_AT_REGISTER_OT_TEMP.NEXTVAL,
               EMPLOYEE_ID,
               WORKINGDAY,
               FROM_HOUR,
               TO_HOUR,
               BREAK_HOUR,
               NOTE,
               CREATED_DATE,
               CREATED_BY,
               CREATED_LOG,
               MODIFIED_DATE,
               MODIFIED_BY,
               MODIFIED_LOG,
               TYPE_OT,
               ROUND(PKG_ATTENDANCE_FUNTION.WORKINGHOUR_OT(C.FROM_HOUR,
                                                           C.TO_HOUR,
                                                           C.VAL_MIN,
                                                           C.VAL_MAX),
                     2),
               HS_OT,
               TYPE_INPUT,
               IS_NB,
               WORKING_ID,
               CASE
                 WHEN WORKING_START < PV_FROMDATE THEN
                  PV_FROMDATE
                 ELSE
                  WORKING_START
               END,
               WORKING_END,
               VAL_MIN,
               VAL_MAX
  
          FROM (SELECT OT.EMPLOYEE_ID,
                       WORKINGDAY,
                       FROM_HOUR,
                       CASE
                         WHEN TO_CHAR(OT.FROM_HOUR, 'yyyymmdd') <>
                              TO_CHAR(OT.TO_HOUR, 'yyyymmdd') THEN
                          TO_HOUR
                         WHEN OT.FROM_HOUR > OT.TO_HOUR THEN
                          TO_HOUR + 1
                         ELSE
                          TO_HOUR
                       END TO_HOUR,
                       BREAK_HOUR,
                       NOTE,
                       CREATED_DATE,
                       CREATED_BY,
                       CREATED_LOG,
                       MODIFIED_DATE,
                       MODIFIED_BY,
                       MODIFIED_LOG,
                       TYPE_OT,
                       HOUR,
                       HS_OT,
                       TYPE_INPUT,
                       CASE
                         WHEN NVL(OT.TYPE_INPUT, 0) = -1 THEN
                          (SELECT INOUT.VALTIME
                             FROM (SELECT INOUT.*,
                                          ROW_NUMBER() OVER(PARTITION BY INOUT.ITIME_ID, INOUT.WORKINGDAY ORDER BY INOUT.VALTIME) AS ROW_NUMBER
                                     FROM AT_SWIPE_DATA INOUT) INOUT
                            WHERE INOUT.WORKINGDAY IS NOT NULL
                              AND INOUT.ITIME_ID = EE.ITIME_ID
                              AND INOUT.WORKINGDAY = OT.WORKINGDAY
                              AND INOUT.ROW_NUMBER = 1)
                         ELSE
                          FROM_HOUR
                       END VAL_MIN,
                       CASE
                         WHEN NVL(OT.TYPE_INPUT, 0) = -1 THEN
                          CASE
                            WHEN TO_CHAR(OT.FROM_HOUR, 'yyyymmdd') <>
                                 TO_CHAR(OT.TO_HOUR, 'yyyymmdd') THEN
                             (SELECT MAX(E.VALTIME)
                                FROM AT_SWIPE_DATA E
                               WHERE E.WORKINGDAY IS NOT NULL
                                 AND E.ITIME_ID = EE.ITIME_ID
                                 AND E.WORKINGDAY <= OT.WORKINGDAY + 1
                                 AND E.WORKINGDAY >= OT.WORKINGDAY
                                 AND E.VALTIME <=
                                     (OT.TO_HOUR + PV_MINUS_ALLOW / 60 / 24)
                                 AND E.VALTIME >
                                     NVL((SELECT INOUT.VALTIME
                                           FROM (SELECT INOUT.*,
                                                        ROW_NUMBER() OVER(PARTITION BY INOUT.ITIME_ID, INOUT.WORKINGDAY ORDER BY INOUT.VALTIME) AS ROW_NUMBER
                                                   FROM AT_SWIPE_DATA INOUT) INOUT
                                          WHERE INOUT.WORKINGDAY IS NOT NULL
                                            AND INOUT.ITIME_ID = EE.ITIME_ID
                                            AND INOUT.WORKINGDAY = OT.WORKINGDAY
                                            AND INOUT.ROW_NUMBER = 1),
                                         '1/jan/1900'))
                            WHEN OT.FROM_HOUR > OT.TO_HOUR THEN
                             (SELECT MAX(E.VALTIME)
                                FROM AT_SWIPE_DATA E
                               WHERE E.WORKINGDAY IS NOT NULL
                                 AND E.ITIME_ID = EE.ITIME_ID
                                 AND E.WORKINGDAY <= OT.WORKINGDAY + 1
                                 AND E.WORKINGDAY >= OT.WORKINGDAY
                                 AND E.VALTIME <=
                                     (OT.TO_HOUR + 1 + PV_MINUS_ALLOW / 60 / 24)
                                 AND E.VALTIME >
                                     NVL((SELECT INOUT.VALTIME
                                           FROM (SELECT INOUT.*,
                                                        ROW_NUMBER() OVER(PARTITION BY INOUT.ITIME_ID, INOUT.WORKINGDAY ORDER BY INOUT.VALTIME) AS ROW_NUMBER
                                                   FROM AT_SWIPE_DATA INOUT) INOUT
                                          WHERE INOUT.WORKINGDAY IS NOT NULL
                                            AND INOUT.ITIME_ID = EE.ITIME_ID
                                            AND INOUT.WORKINGDAY = OT.WORKINGDAY
                                            AND INOUT.ROW_NUMBER = 1),
                                         '1/jan/1900'))
                            ELSE
                             (SELECT MAX(E.VALTIME)
                                FROM (SELECT INOUT.*,
                                             ROW_NUMBER() OVER(PARTITION BY INOUT.ITIME_ID, INOUT.WORKINGDAY ORDER BY INOUT.VALTIME) AS ROW_NUMBER
                                        FROM AT_SWIPE_DATA INOUT) E
                               WHERE E.WORKINGDAY IS NOT NULL
                                 AND E.ITIME_ID = EE.ITIME_ID
                                 AND E.WORKINGDAY = OT.WORKINGDAY
                                 AND E.ROW_NUMBER > 1)
                          END
                         ELSE
                          CASE
                            WHEN TO_CHAR(OT.FROM_HOUR, 'yyyymmdd') <>
                                 TO_CHAR(OT.TO_HOUR, 'yyyymmdd') THEN
                             TO_HOUR
                            WHEN OT.FROM_HOUR > OT.TO_HOUR THEN
                             TO_HOUR + 1
                            ELSE
                             TO_HOUR
                          END
                       END VAL_MAX,
                       OT.IS_NB,
                       W.ID WORKING_ID,
                       W.START_DATE WORKING_START,
                       W.END_DATE WORKING_END
                  FROM AT_REGISTER_OT OT
                 INNER JOIN AT_CHOSEN_EMP EE
                    ON OT.EMPLOYEE_ID = EE.EMPLOYEE_ID
                 INNER JOIN CTE_WORKING W
                    ON OT.EMPLOYEE_ID = W.EMPLOYEE_ID
                   AND W.START_DATE <= OT.WORKINGDAY
                   AND W.END_DATE >= OT.WORKINGDAY
                 WHERE OT.WORKINGDAY >= PV_FROMDATE
                   AND OT.WORKINGDAY <= PV_ENDDATE) C;
  
      UPDATE AT_REGISTER_OT_TEMP T
         SET T.HOUR = CASE
                        WHEN NVL(T.HOUR, 0) >= NVL(PV_HOUR_CAL_OT, 0) THEN
                         NVL(T.HOUR, 0)
                        ELSE
                         0
                      END;
  
      -- KHOI TAO DDU LIEU
      INSERT INTO AT_TIME_TIMESHEET_OT_TEMP
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         PERIOD_ID,
         FROM_DATE,
         END_DATE,
         D1,
         D2,
         D3,
         D4,
         D5,
         D6,
         D7,
         D8,
         D9,
         D10,
         D11,
         D12,
         D13,
         D14,
         D15,
         D16,
         D17,
         D18,
         D19,
         D20,
         D21,
         D22,
         D23,
         D24,
         D25,
         D26,
         D27,
         D28,
         D29,
         D30,
         D31,
         D1_1,
         D1_15,
         D1_2,
         D1_27,
         D1_3,
         D1_39,
         D2_1,
         D2_15,
         D2_2,
         D2_27,
         D2_3,
         D2_39,
         D3_1,
         D3_15,
         D3_2,
         D3_27,
         D3_3,
         D3_39,
         D4_1,
         D4_15,
         D4_2,
         D4_27,
         D4_3,
         D4_39,
         D5_1,
         D5_15,
         D5_2,
         D5_27,
         D5_3,
         D5_39,
         D6_1,
         D6_15,
         D6_2,
         D6_27,
         D6_3,
         D6_39,
         D7_1,
         D7_15,
         D7_2,
         D7_27,
         D7_3,
         D7_39,
         D8_1,
         D8_15,
         D8_2,
         D8_27,
         D8_3,
         D8_39,
         D9_1,
         D9_15,
         D9_2,
         D9_27,
         D9_3,
         D9_39,
         D10_1,
         D10_15,
         D10_2,
         D10_27,
         D10_3,
         D10_39,
         D11_1,
         D11_15,
         D11_2,
         D11_27,
         D11_3,
         D11_39,
         D12_1,
         D12_15,
         D12_2,
         D12_27,
         D12_3,
         D12_39,
         D13_1,
         D13_15,
         D13_2,
         D13_27,
         D13_3,
         D13_39,
         D14_1,
         D14_15,
         D14_2,
         D14_27,
         D14_3,
         D14_39,
         D15_1,
         D15_15,
         D15_2,
         D15_27,
         D15_3,
         D15_39,
         D16_1,
         D16_15,
         D16_2,
         D16_27,
         D16_3,
         D16_39,
         D17_1,
         D17_15,
         D17_2,
         D17_27,
         D17_3,
         D17_39,
         D18_1,
         D18_15,
         D18_2,
         D18_27,
         D18_3,
         D18_39,
         D19_1,
         D19_15,
         D19_2,
         D19_27,
         D19_3,
         D19_39,
         D20_1,
         D20_15,
         D20_2,
         D20_27,
         D20_3,
         D20_39,
         D21_1,
         D21_15,
         D21_2,
         D21_27,
         D21_3,
         D21_39,
         D22_1,
         D22_15,
         D22_2,
         D22_27,
         D22_3,
         D22_39,
         D23_1,
         D23_15,
         D23_2,
         D23_27,
         D23_3,
         D23_39,
         D24_1,
         D24_15,
         D24_2,
         D24_27,
         D24_3,
         D24_39,
         D25_1,
         D25_15,
         D25_2,
         D25_27,
         D25_3,
         D25_39,
         D26_1,
         D26_15,
         D26_2,
         D26_27,
         D26_3,
         D26_39,
         D27_1,
         D27_15,
         D27_2,
         D27_27,
         D27_3,
         D27_39,
         D28_1,
         D28_15,
         D28_2,
         D28_27,
         D28_3,
         D28_39,
         D29_1,
         D29_15,
         D29_2,
         D29_27,
         D29_3,
         D29_39,
         D30_1,
         D30_15,
         D30_2,
         D30_27,
         D30_3,
         D30_39,
         D31_1,
         D31_15,
         D31_2,
         D31_27,
         D31_3,
         D31_39,
         CREATED_DATE,
         CREATED_BY,
         REQUEST_ID,
         TOTAL_FACTOR1,
         TOTAL_FACTOR1_5,
         TOTAL_FACTOR2,
         TOTAL_FACTOR2_7,
         TOTAL_FACTOR3,
         TOTAL_FACTOR3_9,
         TOTAL_FACTOR_CONVERT,
         NUMBER_FACTOR_PAY,
         NUMBER_FACTOR_CP)
        SELECT SEQ_AT_TIME_OT_TEMP.NEXTVAL,
               A.EMPLOYEE_ID,
               A.ORG_ID,
               A.TITLE_ID,
               A.STAFF_RANK_ID,
               P_PERIOD_ID,
               PV_FROMDATE,
               PV_ENDDATE,
               NVL(A.D1_1, 0) + NVL(A.D1_15, 0) + NVL(A.D1_2, 0) +
               NVL(A.D1_27, 0) + NVL(A.D1_3, 0) + NVL(A.D1_39, 0) D1,
               NVL(A.D2_1, 0) + NVL(A.D2_15, 0) + NVL(A.D2_2, 0) +
               NVL(A.D2_27, 0) + NVL(A.D2_3, 0) + NVL(A.D2_39, 0) D2,
               NVL(A.D3_1, 0) + NVL(A.D3_15, 0) + NVL(A.D3_2, 0) +
               NVL(A.D3_27, 0) + NVL(A.D3_3, 0) + NVL(A.D3_39, 0) D3,
               NVL(A.D4_1, 0) + NVL(A.D4_15, 0) + NVL(A.D4_2, 0) +
               NVL(A.D4_27, 0) + NVL(A.D4_3, 0) + NVL(A.D4_39, 0) D4,
               NVL(A.D5_1, 0) + NVL(A.D5_15, 0) + NVL(A.D5_2, 0) +
               NVL(A.D5_27, 0) + NVL(A.D5_3, 0) + NVL(A.D5_39, 0) D5,
               NVL(A.D6_1, 0) + NVL(A.D6_15, 0) + NVL(A.D6_2, 0) +
               NVL(A.D6_27, 0) + NVL(A.D6_3, 0) + NVL(A.D6_39, 0) D6,
               NVL(A.D7_1, 0) + NVL(A.D7_15, 0) + NVL(A.D7_2, 0) +
               NVL(A.D7_27, 0) + NVL(A.D7_3, 0) + NVL(A.D7_39, 0) D7,
               NVL(A.D8_1, 0) + NVL(A.D8_15, 0) + NVL(A.D8_2, 0) +
               NVL(A.D8_27, 0) + NVL(A.D8_3, 0) + NVL(A.D8_39, 0) D8,
               NVL(A.D9_1, 0) + NVL(A.D9_15, 0) + NVL(A.D9_2, 0) +
               NVL(A.D9_27, 0) + NVL(A.D9_3, 0) + NVL(A.D9_39, 0) D9,
               NVL(A.D10_1, 0) + NVL(A.D10_15, 0) + NVL(A.D10_2, 0) +
               NVL(A.D10_27, 0) + NVL(A.D10_3, 0) + NVL(A.D10_39, 0) D10,
               NVL(A.D11_1, 0) + NVL(A.D11_15, 0) + NVL(A.D11_2, 0) +
               NVL(A.D11_27, 0) + NVL(A.D11_3, 0) + NVL(A.D11_39, 0) D11,
               NVL(A.D12_1, 0) + NVL(A.D12_15, 0) + NVL(A.D12_2, 0) +
               NVL(A.D12_27, 0) + NVL(A.D12_3, 0) + NVL(A.D12_39, 0) D12,
               NVL(A.D13_1, 0) + NVL(A.D13_15, 0) + NVL(A.D13_2, 0) +
               NVL(A.D13_27, 0) + NVL(A.D13_3, 0) + NVL(A.D13_39, 0) D13,
               NVL(A.D14_1, 0) + NVL(A.D14_15, 0) + NVL(A.D14_2, 0) +
               NVL(A.D14_27, 0) + NVL(A.D14_3, 0) + NVL(A.D14_39, 0) D14,
               NVL(A.D15_1, 0) + NVL(A.D15_15, 0) + NVL(A.D15_2, 0) +
               NVL(A.D15_27, 0) + NVL(A.D15_3, 0) + NVL(A.D15_39, 0) D15,
               NVL(A.D16_1, 0) + NVL(A.D16_15, 0) + NVL(A.D16_2, 0) +
               NVL(A.D16_27, 0) + NVL(A.D16_3, 0) + NVL(A.D16_39, 0) D16,
               NVL(A.D17_1, 0) + NVL(A.D17_15, 0) + NVL(A.D17_2, 0) +
               NVL(A.D17_27, 0) + NVL(A.D17_3, 0) + NVL(A.D17_39, 0) D17,
               NVL(A.D18_1, 0) + NVL(A.D18_15, 0) + NVL(A.D18_2, 0) +
               NVL(A.D18_27, 0) + NVL(A.D18_3, 0) + NVL(A.D18_39, 0) D18,
               NVL(A.D19_1, 0) + NVL(A.D19_15, 0) + NVL(A.D19_2, 0) +
               NVL(A.D19_27, 0) + NVL(A.D19_3, 0) + NVL(A.D19_39, 0) D19,
               NVL(A.D20_1, 0) + NVL(A.D20_15, 0) + NVL(A.D20_2, 0) +
               NVL(A.D20_27, 0) + NVL(A.D20_3, 0) + NVL(A.D20_39, 0) D20,
               NVL(A.D21_1, 0) + NVL(A.D21_15, 0) + NVL(A.D21_2, 0) +
               NVL(A.D21_27, 0) + NVL(A.D21_3, 0) + NVL(A.D21_39, 0) D21,
               NVL(A.D22_1, 0) + NVL(A.D22_15, 0) + NVL(A.D22_2, 0) +
               NVL(A.D22_27, 0) + NVL(A.D22_3, 0) + NVL(A.D22_39, 0) D22,
               NVL(A.D23_1, 0) + NVL(A.D23_15, 0) + NVL(A.D23_2, 0) +
               NVL(A.D23_27, 0) + NVL(A.D23_3, 0) + NVL(A.D23_39, 0) D23,
               NVL(A.D24_1, 0) + NVL(A.D24_15, 0) + NVL(A.D24_2, 0) +
               NVL(A.D24_27, 0) + NVL(A.D24_3, 0) + NVL(A.D24_39, 0) D24,
               NVL(A.D25_1, 0) + NVL(A.D25_15, 0) + NVL(A.D25_2, 0) +
               NVL(A.D25_27, 0) + NVL(A.D25_3, 0) + NVL(A.D25_39, 0) D25,
               NVL(A.D26_1, 0) + NVL(A.D26_15, 0) + NVL(A.D26_2, 0) +
               NVL(A.D26_27, 0) + NVL(A.D26_3, 0) + NVL(A.D26_39, 0) D26,
               NVL(A.D27_1, 0) + NVL(A.D27_15, 0) + NVL(A.D27_2, 0) +
               NVL(A.D27_27, 0) + NVL(A.D27_3, 0) + NVL(A.D27_39, 0) D27,
               NVL(A.D28_1, 0) + NVL(A.D28_15, 0) + NVL(A.D28_2, 0) +
               NVL(A.D28_27, 0) + NVL(A.D28_3, 0) + NVL(A.D28_39, 0) D28,
               NVL(A.D29_1, 0) + NVL(A.D29_15, 0) + NVL(A.D29_2, 0) +
               NVL(A.D29_27, 0) + NVL(A.D29_3, 0) + NVL(A.D29_39, 0) D29,
               NVL(A.D30_1, 0) + NVL(A.D30_15, 0) + NVL(A.D30_2, 0) +
               NVL(A.D30_27, 0) + NVL(A.D30_3, 0) + NVL(A.D30_39, 0) D30,
               NVL(A.D31_1, 0) + NVL(A.D31_15, 0) + NVL(A.D31_2, 0) +
               NVL(A.D31_27, 0) + NVL(A.D31_3, 0) + NVL(A.D31_39, 0) D31,
               A.D1_1,
               A.D1_15,
               A.D1_2,
               A.D1_27,
               A.D1_3,
               A.D1_39,
               A.D2_1,
               A.D2_15,
               A.D2_2,
               A.D2_27,
               A.D2_3,
               A.D2_39,
               A.D3_1,
               A.D3_15,
               A.D3_2,
               A.D3_27,
               A.D3_3,
               A.D3_39,
               A.D4_1,
               A.D4_15,
               A.D4_2,
               A.D4_27,
               A.D4_3,
               A.D4_39,
               A.D5_1,
               A.D5_15,
               A.D5_2,
               A.D5_27,
               A.D5_3,
               A.D5_39,
               A.D6_1,
               A.D6_15,
               A.D6_2,
               A.D6_27,
               A.D6_3,
               A.D6_39,
               A.D7_1,
               A.D7_15,
               A.D7_2,
               A.D7_27,
               A.D7_3,
               A.D7_39,
               A.D8_1,
               A.D8_15,
               A.D8_2,
               A.D8_27,
               A.D8_3,
               A.D8_39,
               A.D9_1,
               A.D9_15,
               A.D9_2,
               A.D9_27,
               A.D9_3,
               A.D9_39,
               A.D10_1,
               A.D10_15,
               A.D10_2,
               A.D10_27,
               A.D10_3,
               A.D10_39,
               A.D11_1,
               A.D11_15,
               A.D11_2,
               A.D11_27,
               A.D11_3,
               A.D11_39,
               A.D12_1,
               A.D12_15,
               A.D12_2,
               A.D12_27,
               A.D12_3,
               A.D12_39,
               A.D13_1,
               A.D13_15,
               A.D13_2,
               A.D13_27,
               A.D13_3,
               A.D13_39,
               A.D14_1,
               A.D14_15,
               A.D14_2,
               A.D14_27,
               A.D14_3,
               A.D14_39,
               A.D15_1,
               A.D15_15,
               A.D15_2,
               A.D15_27,
               A.D15_3,
               A.D15_39,
               A.D16_1,
               A.D16_15,
               A.D16_2,
               A.D16_27,
               A.D16_3,
               A.D16_39,
               A.D17_1,
               A.D17_15,
               A.D17_2,
               A.D17_27,
               A.D17_3,
               A.D17_39,
               A.D18_1,
               A.D18_15,
               A.D18_2,
               A.D18_27,
               A.D18_3,
               A.D18_39,
               A.D19_1,
               A.D19_15,
               A.D19_2,
               A.D19_27,
               A.D19_3,
               A.D19_39,
               A.D20_1,
               A.D20_15,
               A.D20_2,
               A.D20_27,
               A.D20_3,
               A.D20_39,
               A.D21_1,
               A.D21_15,
               A.D21_2,
               A.D21_27,
               A.D21_3,
               A.D21_39,
               A.D22_1,
               A.D22_15,
               A.D22_2,
               A.D22_27,
               A.D22_3,
               A.D22_39,
               A.D23_1,
               A.D23_15,
               A.D23_2,
               A.D23_27,
               A.D23_3,
               A.D23_39,
               A.D24_1,
               A.D24_15,
               A.D24_2,
               A.D24_27,
               A.D24_3,
               A.D24_39,
               A.D25_1,
               A.D25_15,
               A.D25_2,
               A.D25_27,
               A.D25_3,
               A.D25_39,
               A.D26_1,
               A.D26_15,
               A.D26_2,
               A.D26_27,
               A.D26_3,
               A.D26_39,
               A.D27_1,
               A.D27_15,
               A.D27_2,
               A.D27_27,
               A.D27_3,
               A.D27_39,
               A.D28_1,
               A.D28_15,
               A.D28_2,
               A.D28_27,
               A.D28_3,
               A.D28_39,
               A.D29_1,
               A.D29_15,
               A.D29_2,
               A.D29_27,
               A.D29_3,
               A.D29_39,
               A.D30_1,
               A.D30_15,
               A.D30_2,
               A.D30_27,
               A.D30_3,
               A.D30_39,
               A.D31_1,
               A.D31_15,
               A.D31_2,
               A.D31_27,
               A.D31_3,
               A.D31_39,
               TRUNC(SYSDATE),
               UPPER(P_USERNAME),
               PV_REQUEST_ID,
               CASE
                 WHEN OT.FACTOR1 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR1
               END FACTOR1,
               CASE
                 WHEN OT.FACTOR1_5 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR1_5
               END FACTOR1_5,
               CASE
                 WHEN OT.FACTOR2 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR2
               END FACTOR2,
               CASE
                 WHEN OT.FACTOR2_7 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR2_7
               END FACTOR2_7,
               CASE
                 WHEN OT.FACTOR3 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR3
               END FACTOR3,
               CASE
                 WHEN OT.FACTOR3_9 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR3_9
               END FACTOR3_9,
               CASE
                 WHEN OT.TOTAL_FACTOR_CONVERT = 0 THEN
                  NULL
                 ELSE
                  OT.TOTAL_FACTOR_CONVERT
               END TOTAL_FACTOR_CONVERT,
               CASE
                 WHEN NVL(A.STAFF_RANK_LEVEL, 0) > PV_LEVEL_STAFF THEN
                  CASE
                    WHEN NVL(OT.TOTAL_FACTOR_CONVERT, 0) <= PV_MAX_PAYOT THEN
                     NVL(OT.TOTAL_FACTOR_CONVERT, 0)
                    WHEN NVL(OT.TOTAL_FACTOR_CONVERT, 0) > PV_MAX_PAYOT THEN
                     PV_MAX_PAYOT
                  END
               END,
               (NVL(OT.TOTAL_FACTOR_CONVERT, 0) -
               NVL(CASE
                      WHEN NVL(A.STAFF_RANK_LEVEL, 0) > PV_LEVEL_STAFF THEN
                       CASE
                         WHEN NVL(OT.TOTAL_FACTOR_CONVERT, 0) <= PV_MAX_PAYOT THEN
                          NVL(OT.TOTAL_FACTOR_CONVERT, 0)
                         WHEN NVL(OT.TOTAL_FACTOR_CONVERT, 0) > PV_MAX_PAYOT THEN
                          PV_MAX_PAYOT
                       END
                    END,
                    0)) + NVL(NB.TOTAL_FACTOR_CONVERT, 0)
          FROM (SELECT T.EMPLOYEE_ID,
                       EE.ORG_ID,
                       EE.TITLE_ID,
                       EE.STAFF_RANK_ID,
                       EE.STAFF_RANK_LEVEL,
                       TO_CHAR(T.WORKINGDAY, 'dd') || '_' || CASE
                         WHEN T.HS_OT = 4236 THEN -- 1
                          '1'
                         WHEN T.HS_OT = 4237 THEN -- 1.5
                          '15'
                         WHEN T.HS_OT = 4238 THEN -- 2
                          '2'
                         WHEN T.HS_OT = 4239 THEN -- 2.7
                          '27'
                         WHEN T.HS_OT = 4240 THEN -- 3
                          '3'
                         WHEN T.HS_OT = 4241 THEN -- 3.9
                          '39'
                       END AS DAY,
                       CASE
                         WHEN NVL(T.IS_NB, 0) = 0 THEN
                          T.HOUR
                         ELSE
                          0
                       END HOUR
                  FROM AT_REGISTER_OT_TEMP T
                 INNER JOIN AT_CHOSEN_EMP EE
                    ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID) T
        PIVOT(SUM(HOUR)
           FOR DAY IN('01_1' AS D1_1,
                      '01_15' AS D1_15,
                      '01_2' AS D1_2,
                      '01_27' AS D1_27,
                      '01_3' AS D1_3,
                      '01_39' AS D1_39,
                      '02_1' AS D2_1,
                      '02_15' AS D2_15,
                      '02_2' AS D2_2,
                      '02_27' AS D2_27,
                      '02_3' AS D2_3,
                      '02_39' AS D2_39,
                      '03_1' AS D3_1,
                      '03_15' AS D3_15,
                      '03_2' AS D3_2,
                      '03_27' AS D3_27,
                      '03_3' AS D3_3,
                      '03_39' AS D3_39,
                      '04_1' AS D4_1,
                      '04_15' AS D4_15,
                      '04_2' AS D4_2,
                      '04_27' AS D4_27,
                      '04_3' AS D4_3,
                      '04_39' AS D4_39,
                      '05_1' AS D5_1,
                      '05_15' AS D5_15,
                      '05_2' AS D5_2,
                      '05_27' AS D5_27,
                      '05_3' AS D5_3,
                      '05_39' AS D5_39,
                      '06_1' AS D6_1,
                      '06_15' AS D6_15,
                      '06_2' AS D6_2,
                      '06_27' AS D6_27,
                      '06_3' AS D6_3,
                      '06_39' AS D6_39,
                      '07_1' AS D7_1,
                      '07_15' AS D7_15,
                      '07_2' AS D7_2,
                      '07_27' AS D7_27,
                      '07_3' AS D7_3,
                      '07_39' AS D7_39,
                      '08_1' AS D8_1,
                      '08_15' AS D8_15,
                      '08_2' AS D8_2,
                      '08_27' AS D8_27,
                      '08_3' AS D8_3,
                      '08_39' AS D8_39,
                      '09_1' AS D9_1,
                      '09_15' AS D9_15,
                      '09_2' AS D9_2,
                      '09_27' AS D9_27,
                      '09_3' AS D9_3,
                      '09_39' AS D9_39,
                      '10_1' AS D10_1,
                      '10_15' AS D10_15,
                      '10_2' AS D10_2,
                      '10_27' AS D10_27,
                      '10_3' AS D10_3,
                      '10_39' AS D10_39,
                      '11_1' AS D11_1,
                      '11_15' AS D11_15,
                      '11_2' AS D11_2,
                      '11_27' AS D11_27,
                      '11_3' AS D11_3,
                      '11_39' AS D11_39,
                      '12_1' AS D12_1,
                      '12_15' AS D12_15,
                      '12_2' AS D12_2,
                      '12_27' AS D12_27,
                      '12_3' AS D12_3,
                      '12_39' AS D12_39,
                      '13_1' AS D13_1,
                      '13_15' AS D13_15,
                      '13_2' AS D13_2,
                      '13_27' AS D13_27,
                      '13_3' AS D13_3,
                      '13_39' AS D13_39,
                      '14_1' AS D14_1,
                      '14_15' AS D14_15,
                      '14_2' AS D14_2,
                      '14_27' AS D14_27,
                      '14_3' AS D14_3,
                      '14_39' AS D14_39,
                      '15_1' AS D15_1,
                      '15_15' AS D15_15,
                      '15_2' AS D15_2,
                      '15_27' AS D15_27,
                      '15_3' AS D15_3,
                      '15_39' AS D15_39,
                      '16_1' AS D16_1,
                      '16_15' AS D16_15,
                      '16_2' AS D16_2,
                      '16_27' AS D16_27,
                      '16_3' AS D16_3,
                      '16_39' AS D16_39,
                      '17_1' AS D17_1,
                      '17_15' AS D17_15,
                      '17_2' AS D17_2,
                      '17_27' AS D17_27,
                      '17_3' AS D17_3,
                      '17_39' AS D17_39,
                      '18_1' AS D18_1,
                      '18_15' AS D18_15,
                      '18_2' AS D18_2,
                      '18_27' AS D18_27,
                      '18_3' AS D18_3,
                      '18_39' AS D18_39,
                      '19_1' AS D19_1,
                      '19_15' AS D19_15,
                      '19_2' AS D19_2,
                      '19_27' AS D19_27,
                      '19_3' AS D19_3,
                      '19_39' AS D19_39,
                      '20_1' AS D20_1,
                      '20_15' AS D20_15,
                      '20_2' AS D20_2,
                      '20_27' AS D20_27,
                      '20_3' AS D20_3,
                      '20_39' AS D20_39,
                      '21_1' AS D21_1,
                      '21_15' AS D21_15,
                      '21_2' AS D21_2,
                      '21_27' AS D21_27,
                      '21_3' AS D21_3,
                      '21_39' AS D21_39,
                      '22_1' AS D22_1,
                      '22_15' AS D22_15,
                      '22_2' AS D22_2,
                      '22_27' AS D22_27,
                      '22_3' AS D22_3,
                      '22_39' AS D22_39,
                      '23_1' AS D23_1,
                      '23_15' AS D23_15,
                      '23_2' AS D23_2,
                      '23_27' AS D23_27,
                      '23_3' AS D23_3,
                      '23_39' AS D23_39,
                      '24_1' AS D24_1,
                      '24_15' AS D24_15,
                      '24_2' AS D24_2,
                      '24_27' AS D24_27,
                      '24_3' AS D24_3,
                      '24_39' AS D24_39,
                      '25_1' AS D25_1,
                      '25_15' AS D25_15,
                      '25_2' AS D25_2,
                      '25_27' AS D25_27,
                      '25_3' AS D25_3,
                      '25_39' AS D25_39,
                      '26_1' AS D26_1,
                      '26_15' AS D26_15,
                      '26_2' AS D26_2,
                      '26_27' AS D26_27,
                      '26_3' AS D26_3,
                      '26_39' AS D26_39,
                      '27_1' AS D27_1,
                      '27_15' AS D27_15,
                      '27_2' AS D27_2,
                      '27_27' AS D27_27,
                      '27_3' AS D27_3,
                      '27_39' AS D27_39,
                      '28_1' AS D28_1,
                      '28_15' AS D28_15,
                      '28_2' AS D28_2,
                      '28_27' AS D28_27,
                      '28_3' AS D28_3,
                      '28_39' AS D28_39,
                      '29_1' AS D29_1,
                      '29_15' AS D29_15,
                      '29_2' AS D29_2,
                      '29_27' AS D29_27,
                      '29_3' AS D29_3,
                      '29_39' AS D29_39,
                      '30_1' AS D30_1,
                      '30_15' AS D30_15,
                      '30_2' AS D30_2,
                      '30_27' AS D30_27,
                      '30_3' AS D30_3,
                      '30_39' AS D30_39,
                      '31_1' AS D31_1,
                      '31_15' AS D31_15,
                      '31_2' AS D31_2,
                      '31_27' AS D31_27,
                      '31_3' AS D31_3,
                      '31_39' AS D31_39)) A
          LEFT JOIN (SELECT OT.EMPLOYEE_ID,
                            NVL(OT.FACTOR1, 0) FACTOR1,
                            NVL(OT.FACTOR1_5, 0) FACTOR1_5,
                            NVL(OT.FACTOR2, 0) FACTOR2,
                            NVL(OT.FACTOR2_7, 0) FACTOR2_7,
                            NVL(OT.FACTOR3, 0) FACTOR3,
                            NVL(OT.FACTOR3_9, 0) FACTOR3_9,
                            ROUND(NVL(OT.FACTOR1, 0) * 1 +
                                  NVL(OT.FACTOR1_5, 0) * 1.5 +
                                  NVL(OT.FACTOR2, 0) * 2 +
                                  NVL(OT.FACTOR2_7, 0) * 2.7 +
                                  NVL(OT.FACTOR3, 0) * 3 +
                                  NVL(OT.FACTOR3_9, 0) * 3.9,
                                  2) TOTAL_FACTOR_CONVERT
                       FROM (SELECT T.EMPLOYEE_ID,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4236 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR1,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4237 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR1_5,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4238 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR2,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4239 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR2_7,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4240 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR3,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4241 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR3_9
                               FROM AT_REGISTER_OT_TEMP T
                              WHERE NVL(T.IS_NB, 0) = 0
                              GROUP BY T.EMPLOYEE_ID) OT) OT
            ON A.EMPLOYEE_ID = OT.EMPLOYEE_ID
          LEFT JOIN (SELECT OT.EMPLOYEE_ID,
                            ROUND(NVL(OT.FACTOR1, 0) * 1 +
                                  NVL(OT.FACTOR1_5, 0) * 1.5 +
                                  NVL(OT.FACTOR2, 0) * 2 +
                                  NVL(OT.FACTOR2_7, 0) * 2.7 +
                                  NVL(OT.FACTOR3, 0) * 3 +
                                  NVL(OT.FACTOR3_9, 0) * 3.9,
                                  2) TOTAL_FACTOR_CONVERT
                       FROM (SELECT T.EMPLOYEE_ID,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4236 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR1,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4237 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR1_5,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4238 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR2,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4239 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR2_7,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4240 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR3,
                                    SUM(NVL(CASE
                                              WHEN T.HS_OT = 4241 AND
                                                   NVL(T.HOUR, 0) * 60 >=
                                                   NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                                               T.HOUR
                                            END,
                                            0)) FACTOR3_9
                               FROM AT_REGISTER_OT_TEMP T
                              WHERE NVL(T.IS_NB, 0) = -1
                              GROUP BY T.EMPLOYEE_ID) OT) NB
            ON A.EMPLOYEE_ID = NB.EMPLOYEE_ID;
  
      UPDATE AT_TIME_TIMESHEET_OT_TEMP OT
         SET OT.CONGHIBU = CASE
                             WHEN NVL(OT.NUMBER_FACTOR_CP, 0) > 0 OR
                                  NVL(OT.BACKUP_MONTH_BEFORE, 0) > 0 THEN
                              ROUND((NVL(OT.NUMBER_FACTOR_CP, 0) +
                                    NVL(OT.BACKUP_MONTH_BEFORE, 0)) / 8,
                                    2)
                             ELSE
                              0
                           END;
  
      --DELETE AT_TIMESHEET_OT_DTL_TEMP;
      -- KHOI TAO DDU LIEU
      INSERT INTO AT_TIMESHEET_OT_DTL_TEMP
        (ID,
         EMPLOYEE_ID,
         PERIOD_ID,
         FROM_DATE,
         END_DATE,
         D1,
         D2,
         D3,
         D4,
         D5,
         D6,
         D7,
         D8,
         D9,
         D10,
         D11,
         D12,
         D13,
         D14,
         D15,
         D16,
         D17,
         D18,
         D19,
         D20,
         D21,
         D22,
         D23,
         D24,
         D25,
         D26,
         D27,
         D28,
         D29,
         D30,
         D31,
         CREATED_DATE,
         CREATED_BY,
         TOTAL_FACTOR1,
         TOTAL_FACTOR1_5,
         TOTAL_FACTOR2,
         TOTAL_FACTOR2_7,
         TOTAL_FACTOR3,
         TOTAL_FACTOR3_9,
         TOTAL_FACTOR_CONVERT,
         NUMBER_FACTOR_PAY,
         WORKING_ID)
        WITH CTE_OT AS
         (SELECT T.EMPLOYEE_ID,
                 T.WORKING_ID,
                 T.WORKING_START,
                 T.WORKING_END,
                 SUM(NVL(CASE
                           WHEN T.HS_OT = 4236 AND
                                NVL(EE.STAFF_RANK_LEVEL, 0) > PV_LEVEL_STAFF AND
                                NVL(T.HOUR, 0) * 60 >= NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                            T.HOUR
                         END,
                         0)) FACTOR1,
                 SUM(NVL(CASE
                           WHEN T.HS_OT = 4237 AND
                                NVL(EE.STAFF_RANK_LEVEL, 0) > PV_LEVEL_STAFF AND
                                NVL(T.HOUR, 0) * 60 >= NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                            T.HOUR
                         END,
                         0)) FACTOR1_5,
                 SUM(NVL(CASE
                           WHEN T.HS_OT = 4238 AND
                                NVL(EE.STAFF_RANK_LEVEL, 0) > PV_LEVEL_STAFF AND
                                NVL(T.HOUR, 0) * 60 >= NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                            T.HOUR
                         END,
                         0)) FACTOR2,
                 SUM(NVL(CASE
                           WHEN T.HS_OT = 4239 AND
                                NVL(EE.STAFF_RANK_LEVEL, 0) > PV_LEVEL_STAFF AND
                                NVL(T.HOUR, 0) * 60 >= NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                            T.HOUR
                         END,
                         0)) FACTOR2_7,
                 SUM(NVL(CASE
                           WHEN T.HS_OT = 4240 AND
                                NVL(EE.STAFF_RANK_LEVEL, 0) > PV_LEVEL_STAFF AND
                                NVL(T.HOUR, 0) * 60 >= NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                            T.HOUR
                         END,
                         0)) FACTOR3,
                 SUM(NVL(CASE
                           WHEN T.HS_OT = 4241 AND
                                NVL(EE.STAFF_RANK_LEVEL, 0) > PV_LEVEL_STAFF AND
                                NVL(T.HOUR, 0) * 60 >= NVL(PV_HOUR_CAL_OT, 0) * 60 THEN
                            T.HOUR
                         END,
                         0)) FACTOR3_9
            FROM AT_REGISTER_OT_TEMP T
           INNER JOIN AT_CHOSEN_EMP EE
              ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
           WHERE NVL(T.IS_NB, 0) = 0
           GROUP BY T.EMPLOYEE_ID,
                    T.WORKING_ID,
                    T.WORKING_START,
                    T.WORKING_END)
        SELECT SEQ_AT_TIME_OT_TEMP.NEXTVAL,
               A.EMPLOYEE_ID,
               P_PERIOD_ID,
               WORKING_START,
               WORKING_END,
               A.D1,
               A.D2,
               A.D3,
               A.D4,
               A.D5,
               A.D6,
               A.D7,
               A.D8,
               A.D9,
               A.D10,
               A.D11,
               A.D12,
               A.D13,
               A.D14,
               A.D15,
               A.D16,
               A.D17,
               A.D18,
               A.D19,
               A.D20,
               A.D21,
               A.D22,
               A.D23,
               A.D24,
               A.D25,
               A.D26,
               A.D27,
               A.D28,
               A.D29,
               A.D30,
               A.D31,
               TRUNC(SYSDATE),
               UPPER(P_USERNAME),
               CASE
                 WHEN OT.FACTOR1 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR1
               END FACTOR1,
               CASE
                 WHEN OT.FACTOR1_5 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR1_5
               END FACTOR1_5,
               CASE
                 WHEN OT.FACTOR2 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR2
               END FACTOR2,
               CASE
                 WHEN OT.FACTOR2_7 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR2_7
               END FACTOR2_7,
               CASE
                 WHEN OT.FACTOR3 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR3
               END FACTOR3,
               CASE
                 WHEN OT.FACTOR3_9 = 0 THEN
                  NULL
                 ELSE
                  OT.FACTOR3_9
               END FACTOR3_9,
               CASE
                 WHEN OT.TOTAL_FACTOR_CONVERT = 0 THEN
                  NULL
                 ELSE
                  OT.TOTAL_FACTOR_CONVERT
               END TOTAL_FACTOR_CONVERT,
               CASE
                 WHEN NVL(A.STAFF_RANK_LEVEL, 0) > PV_LEVEL_STAFF THEN
                  CASE
                    WHEN NVL(OT.TOTAL_FACTOR_CONVERT, 0) +
                         NVL(OT.TOTAL_FACTOR_CONVERT_BEFORE, 0) <= PV_MAX_PAYOT THEN
                     NVL(OT.TOTAL_FACTOR_CONVERT, 0)
                    WHEN NVL(OT.TOTAL_FACTOR_CONVERT, 0) +
                         NVL(OT.TOTAL_FACTOR_CONVERT_BEFORE, 0) > PV_MAX_PAYOT THEN
                     PV_MAX_PAYOT - NVL(OT.TOTAL_FACTOR_CONVERT_BEFORE, 0)
                  END
               END,
               OT.WORKING_ID
          FROM (SELECT T.EMPLOYEE_ID,
                       EE.STAFF_RANK_ID,
                       EE.STAFF_RANK_LEVEL,
                       T.WORKING_ID,
                       TO_NUMBER(TO_CHAR(T.WORKINGDAY, 'dd')) AS DAY,
                       SUM(CASE
                             WHEN MOD(NVL(T.HOUR, 0) * 60, 60) >= 1 AND
                                  NVL(T.HOUR, 0) * 60 > NVL(PV_HOUR_CAL_OT, 0) * 60 AND
                                  MOD(NVL(T.HOUR, 0) * 60, 60) < 30 THEN
                              TRUNC(NVL(T.HOUR, 0)) + 30 / 60
                             WHEN MOD(NVL(T.HOUR, 0) * 60, 60) > 30 AND
                                  NVL(T.HOUR, 0) * 60 > NVL(PV_HOUR_CAL_OT, 0) * 60 AND
                                  MOD(NVL(T.HOUR, 0) * 60, 60) <= 60 THEN
                              TRUNC(NVL(T.HOUR, 0)) + 1
                             WHEN NVL(T.HOUR, 0) * 60 > 30 THEN
                              TRUNC(NVL(T.HOUR, 0))
                             ELSE
                              0
                           END) HOUR
                  FROM AT_REGISTER_OT_TEMP T
                 INNER JOIN AT_CHOSEN_EMP EE
                    ON T.EMPLOYEE_ID = EE.EMPLOYEE_ID
                 GROUP BY T.EMPLOYEE_ID,
                          T.WORKING_ID,
                          EE.STAFF_RANK_ID,
                          EE.STAFF_RANK_LEVEL,
                          T.WORKINGDAY) T
        PIVOT(SUM(HOUR)
           FOR DAY IN(1 AS D1,
                      2 AS D2,
                      3 AS D3,
                      4 AS D4,
                      5 AS D5,
                      6 AS D6,
                      7 AS D7,
                      8 AS D8,
                      9 AS D9,
                      10 AS D10,
                      11 AS D11,
                      12 AS D12,
                      13 AS D13,
                      14 AS D14,
                      15 AS D15,
                      16 AS D16,
                      17 AS D17,
                      18 AS D18,
                      19 AS D19,
                      20 AS D20,
                      21 AS D21,
                      22 AS D22,
                      23 AS D23,
                      24 AS D24,
                      25 AS D25,
                      26 AS D26,
                      27 AS D27,
                      28 AS D28,
                      29 AS D29,
                      30 AS D30,
                      31 AS D31)) A
          LEFT JOIN (SELECT OT.EMPLOYEE_ID,
                            OT.WORKING_ID,
                            OT.WORKING_START,
                            OT.WORKING_END,
                            NVL(OT.FACTOR1, 0) FACTOR1,
                            NVL(OT.FACTOR1_5, 0) FACTOR1_5,
                            NVL(OT.FACTOR2, 0) FACTOR2,
                            NVL(OT.FACTOR2_7, 0) FACTOR2_7,
                            NVL(OT.FACTOR3, 0) FACTOR3,
                            NVL(OT.FACTOR3_9, 0) FACTOR3_9,
                            ROUND(NVL(OT.FACTOR1, 0) * 1 +
                                  NVL(OT.FACTOR1_5, 0) * 1.5 +
                                  NVL(OT.FACTOR2, 0) * 2 +
                                  NVL(OT.FACTOR2_7, 0) * 2.7 +
                                  NVL(OT.FACTOR3, 0) * 3 +
                                  NVL(OT.FACTOR3_9, 0) * 3.9,
                                  2) TOTAL_FACTOR_CONVERT,
                            NVL((SELECT ROUND(NVL(OT1.FACTOR1, 0) * 1 +
                                             NVL(OT1.FACTOR1_5, 0) * 1.5 +
                                             NVL(OT1.FACTOR2, 0) * 2 +
                                             NVL(OT1.FACTOR2_7, 0) * 2.7 +
                                             NVL(OT1.FACTOR3, 0) * 3 +
                                             NVL(OT1.FACTOR3_9, 0) * 3.9,
                                             2) TOTAL_FACTOR_CONVERT
                                  FROM CTE_OT OT1
                                 WHERE OT1.WORKING_END = OT.WORKING_START - 1
                                   AND OT1.EMPLOYEE_ID = OT.EMPLOYEE_ID),
                                0) TOTAL_FACTOR_CONVERT_BEFORE
                       FROM CTE_OT OT) OT
            ON A.EMPLOYEE_ID = OT.EMPLOYEE_ID
           AND A.WORKING_ID = OT.WORKING_ID;
  
      DELETE AT_REGISTER_OT OT
       WHERE OT.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP EE)
         AND OT.WORKINGDAY >= PV_FROMDATE
         AND OT.WORKINGDAY <= PV_ENDDATE;
  
      -- XOA DU LIEU CU TRUOC KHI TINH
      DELETE FROM AT_TIMESHEET_OT_DTL D
       WHERE D.PERIOD_ID = P_PERIOD_ID
         AND D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O);
  
      -- XOA DU LIEU CU TRUOC KHI TINH
      DELETE FROM AT_TIME_TIMESHEET_OT D
       WHERE D.EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP O)
         AND D.PERIOD_ID = P_PERIOD_ID;
  
      INSERT INTO AT_TIME_TIMESHEET_OT
        (ID,
         EMPLOYEE_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         PERIOD_ID,
         TOTAL_FACTOR1,
         TOTAL_FACTOR1_5,
         TOTAL_FACTOR2,
         TOTAL_FACTOR2_7,
         TOTAL_FACTOR3,
         TOTAL_FACTOR3_9,
         TOTAL_FACTOR_CONVERT,
         NUMBER_FACTOR_PAY,
         NUMBER_FACTOR_CP,
         CONGHIBU,
         CREATED_DATE,
         CREATED_BY,
         CREATED_LOG,
         D1,
         D2,
         D3,
         D4,
         D5,
         D6,
         D7,
         D8,
         D9,
         D10,
         D11,
         D12,
         D13,
         D14,
         D15,
         D16,
         D17,
         D18,
         D19,
         D20,
         D21,
         D22,
         D23,
         D24,
         D25,
         D26,
         D27,
         D28,
         D29,
         D30,
         D31,
         D1_1,
         D1_15,
         D1_2,
         D1_27,
         D1_3,
         D1_39,
         D2_1,
         D2_15,
         D2_2,
         D2_27,
         D2_3,
         D2_39,
         D3_1,
         D3_15,
         D3_2,
         D3_27,
         D3_3,
         D3_39,
         D4_1,
         D4_15,
         D4_2,
         D4_27,
         D4_3,
         D4_39,
         D5_1,
         D5_15,
         D5_2,
         D5_27,
         D5_3,
         D5_39,
         D6_1,
         D6_15,
         D6_2,
         D6_27,
         D6_3,
         D6_39,
         D7_1,
         D7_15,
         D7_2,
         D7_27,
         D7_3,
         D7_39,
         D8_1,
         D8_15,
         D8_2,
         D8_27,
         D8_3,
         D8_39,
         D9_1,
         D9_15,
         D9_2,
         D9_27,
         D9_3,
         D9_39,
         D10_1,
         D10_15,
         D10_2,
         D10_27,
         D10_3,
         D10_39,
         D11_1,
         D11_15,
         D11_2,
         D11_27,
         D11_3,
         D11_39,
         D12_1,
         D12_15,
         D12_2,
         D12_27,
         D12_3,
         D12_39,
         D13_1,
         D13_15,
         D13_2,
         D13_27,
         D13_3,
         D13_39,
         D14_1,
         D14_15,
         D14_2,
         D14_27,
         D14_3,
         D14_39,
         D15_1,
         D15_15,
         D15_2,
         D15_27,
         D15_3,
         D15_39,
         D16_1,
         D16_15,
         D16_2,
         D16_27,
         D16_3,
         D16_39,
         D17_1,
         D17_15,
         D17_2,
         D17_27,
         D17_3,
         D17_39,
         D18_1,
         D18_15,
         D18_2,
         D18_27,
         D18_3,
         D18_39,
         D19_1,
         D19_15,
         D19_2,
         D19_27,
         D19_3,
         D19_39,
         D20_1,
         D20_15,
         D20_2,
         D20_27,
         D20_3,
         D20_39,
         D21_1,
         D21_15,
         D21_2,
         D21_27,
         D21_3,
         D21_39,
         D22_1,
         D22_15,
         D22_2,
         D22_27,
         D22_3,
         D22_39,
         D23_1,
         D23_15,
         D23_2,
         D23_27,
         D23_3,
         D23_39,
         D24_1,
         D24_15,
         D24_2,
         D24_27,
         D24_3,
         D24_39,
         D25_1,
         D25_15,
         D25_2,
         D25_27,
         D25_3,
         D25_39,
         D26_1,
         D26_15,
         D26_2,
         D26_27,
         D26_3,
         D26_39,
         D27_1,
         D27_15,
         D27_2,
         D27_27,
         D27_3,
         D27_39,
         D28_1,
         D28_15,
         D28_2,
         D28_27,
         D28_3,
         D28_39,
         D29_1,
         D29_15,
         D29_2,
         D29_27,
         D29_3,
         D29_39,
         D30_1,
         D30_15,
         D30_2,
         D30_27,
         D30_3,
         D30_39,
         D31_1,
         D31_15,
         D31_2,
         D31_27,
         D31_3,
         D31_39,
         FROM_DATE,
         END_DATE,
         BACKUP_MONTH_BEFORE,
         GHINHAN_OT)
        SELECT SEQ_AT_TIME_TIMESHEET_OT.NEXTVAL,
               EMPLOYEE_ID,
               ORG_ID,
               TITLE_ID,
               STAFF_RANK_ID,
               PERIOD_ID,
               TOTAL_FACTOR1,
               TOTAL_FACTOR1_5,
               TOTAL_FACTOR2,
               TOTAL_FACTOR2_7,
               TOTAL_FACTOR3,
               TOTAL_FACTOR3_9,
               TOTAL_FACTOR_CONVERT,
               NUMBER_FACTOR_PAY,
               NUMBER_FACTOR_CP,
               CONGHIBU,
               CREATED_DATE,
               CREATED_BY,
               CREATED_LOG,
               CASE
                 WHEN NVL(D1, 0) = 0 THEN
                  NULL
                 ELSE
                  D1
               END D1,
               CASE
                 WHEN NVL(D2, 0) = 0 THEN
                  NULL
                 ELSE
                  D2
               END D2,
               CASE
                 WHEN NVL(D3, 0) = 0 THEN
                  NULL
                 ELSE
                  D3
               END D3,
               CASE
                 WHEN NVL(D4, 0) = 0 THEN
                  NULL
                 ELSE
                  D4
               END D4,
               CASE
                 WHEN NVL(D5, 0) = 0 THEN
                  NULL
                 ELSE
                  D5
               END D5,
               CASE
                 WHEN NVL(D6, 0) = 0 THEN
                  NULL
                 ELSE
                  D6
               END D6,
               CASE
                 WHEN NVL(D7, 0) = 0 THEN
                  NULL
                 ELSE
                  D7
               END D7,
               CASE
                 WHEN NVL(D8, 0) = 0 THEN
                  NULL
                 ELSE
                  D8
               END D8,
               CASE
                 WHEN NVL(D9, 0) = 0 THEN
                  NULL
                 ELSE
                  D9
               END D9,
               CASE
                 WHEN NVL(D10, 0) = 0 THEN
                  NULL
                 ELSE
                  D10
               END D10,
               CASE
                 WHEN NVL(D11, 0) = 0 THEN
                  NULL
                 ELSE
                  D11
               END D11,
               CASE
                 WHEN NVL(D12, 0) = 0 THEN
                  NULL
                 ELSE
                  D12
               END D12,
               CASE
                 WHEN NVL(D13, 0) = 0 THEN
                  NULL
                 ELSE
                  D13
               END D13,
               CASE
                 WHEN NVL(D14, 0) = 0 THEN
                  NULL
                 ELSE
                  D14
               END D14,
               CASE
                 WHEN NVL(D15, 0) = 0 THEN
                  NULL
                 ELSE
                  D15
               END D15,
               CASE
                 WHEN NVL(D16, 0) = 0 THEN
                  NULL
                 ELSE
                  D16
               END D16,
               CASE
                 WHEN NVL(D17, 0) = 0 THEN
                  NULL
                 ELSE
                  D17
               END D17,
               CASE
                 WHEN NVL(D18, 0) = 0 THEN
                  NULL
                 ELSE
                  D18
               END D18,
               CASE
                 WHEN NVL(D19, 0) = 0 THEN
                  NULL
                 ELSE
                  D19
               END D19,
               CASE
                 WHEN NVL(D20, 0) = 0 THEN
                  NULL
                 ELSE
                  D20
               END D20,
               CASE
                 WHEN NVL(D21, 0) = 0 THEN
                  NULL
                 ELSE
                  D21
               END D21,
               CASE
                 WHEN NVL(D22, 0) = 0 THEN
                  NULL
                 ELSE
                  D22
               END D22,
               CASE
                 WHEN NVL(D23, 0) = 0 THEN
                  NULL
                 ELSE
                  D23
               END D23,
               CASE
                 WHEN NVL(D24, 0) = 0 THEN
                  NULL
                 ELSE
                  D24
               END D24,
               CASE
                 WHEN NVL(D25, 0) = 0 THEN
                  NULL
                 ELSE
                  D25
               END D25,
               CASE
                 WHEN NVL(D26, 0) = 0 THEN
                  NULL
                 ELSE
                  D26
               END D26,
               CASE
                 WHEN NVL(D27, 0) = 0 THEN
                  NULL
                 ELSE
                  D27
               END D27,
               CASE
                 WHEN NVL(D28, 0) = 0 THEN
                  NULL
                 ELSE
                  D28
               END D28,
               CASE
                 WHEN NVL(D29, 0) = 0 THEN
                  NULL
                 ELSE
                  D29
               END D29,
               CASE
                 WHEN NVL(D30, 0) = 0 THEN
                  NULL
                 ELSE
                  D30
               END D30,
               CASE
                 WHEN NVL(D31, 0) = 0 THEN
                  NULL
                 ELSE
                  D31
               END D31,
               D1_1,
               D1_15,
               D1_2,
               D1_27,
               D1_3,
               D1_39,
               D2_1,
               D2_15,
               D2_2,
               D2_27,
               D2_3,
               D2_39,
               D3_1,
               D3_15,
               D3_2,
               D3_27,
               D3_3,
               D3_39,
               D4_1,
               D4_15,
               D4_2,
               D4_27,
               D4_3,
               D4_39,
               D5_1,
               D5_15,
               D5_2,
               D5_27,
               D5_3,
               D5_39,
               D6_1,
               D6_15,
               D6_2,
               D6_27,
               D6_3,
               D6_39,
               D7_1,
               D7_15,
               D7_2,
               D7_27,
               D7_3,
               D7_39,
               D8_1,
               D8_15,
               D8_2,
               D8_27,
               D8_3,
               D8_39,
               D9_1,
               D9_15,
               D9_2,
               D9_27,
               D9_3,
               D9_39,
               D10_1,
               D10_15,
               D10_2,
               D10_27,
               D10_3,
               D10_39,
               D11_1,
               D11_15,
               D11_2,
               D11_27,
               D11_3,
               D11_39,
               D12_1,
               D12_15,
               D12_2,
               D12_27,
               D12_3,
               D12_39,
               D13_1,
               D13_15,
               D13_2,
               D13_27,
               D13_3,
               D13_39,
               D14_1,
               D14_15,
               D14_2,
               D14_27,
               D14_3,
               D14_39,
               D15_1,
               D15_15,
               D15_2,
               D15_27,
               D15_3,
               D15_39,
               D16_1,
               D16_15,
               D16_2,
               D16_27,
               D16_3,
               D16_39,
               D17_1,
               D17_15,
               D17_2,
               D17_27,
               D17_3,
               D17_39,
               D18_1,
               D18_15,
               D18_2,
               D18_27,
               D18_3,
               D18_39,
               D19_1,
               D19_15,
               D19_2,
               D19_27,
               D19_3,
               D19_39,
               D20_1,
               D20_15,
               D20_2,
               D20_27,
               D20_3,
               D20_39,
               D21_1,
               D21_15,
               D21_2,
               D21_27,
               D21_3,
               D21_39,
               D22_1,
               D22_15,
               D22_2,
               D22_27,
               D22_3,
               D22_39,
               D23_1,
               D23_15,
               D23_2,
               D23_27,
               D23_3,
               D23_39,
               D24_1,
               D24_15,
               D24_2,
               D24_27,
               D24_3,
               D24_39,
               D25_1,
               D25_15,
               D25_2,
               D25_27,
               D25_3,
               D25_39,
               D26_1,
               D26_15,
               D26_2,
               D26_27,
               D26_3,
               D26_39,
               D27_1,
               D27_15,
               D27_2,
               D27_27,
               D27_3,
               D27_39,
               D28_1,
               D28_15,
               D28_2,
               D28_27,
               D28_3,
               D28_39,
               D29_1,
               D29_15,
               D29_2,
               D29_27,
               D29_3,
               D29_39,
               D30_1,
               D30_15,
               D30_2,
               D30_27,
               D30_3,
               D30_39,
               D31_1,
               D31_15,
               D31_2,
               D31_27,
               D31_3,
               D31_39,
               FROM_DATE,
               END_DATE,
               BACKUP_MONTH_BEFORE,
               GHINHAN_OT
          FROM AT_TIME_TIMESHEET_OT_TEMP E;
  
      INSERT INTO AT_REGISTER_OT
        (ID,
         EMPLOYEE_ID,
         WORKINGDAY,
         FROM_HOUR,
         TO_HOUR,
         BREAK_HOUR,
         NOTE,
         CREATED_DATE,
         CREATED_BY,
         CREATED_LOG,
         MODIFIED_DATE,
         MODIFIED_BY,
         MODIFIED_LOG,
         TYPE_OT,
         HOUR,
         HS_OT,
         TYPE_INPUT,
         IS_NB,
         WORKING_ID,
         VAL_IN,
         VAL_OUT)
        SELECT SEQ_AT_REGISTER_OT.NEXTVAL,
               EMPLOYEE_ID,
               WORKINGDAY,
               FROM_HOUR,
               TO_HOUR,
               BREAK_HOUR,
               NOTE,
               CREATED_DATE,
               CREATED_BY,
               CREATED_LOG,
               MODIFIED_DATE,
               MODIFIED_BY,
               MODIFIED_LOG,
               TYPE_OT,
               HOUR,
               HS_OT,
               TYPE_INPUT,
               IS_NB,
               WORKING_ID,
               VAL_IN,
               VAL_OUT
          FROM AT_REGISTER_OT_TEMP OT
         WHERE EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP EE);
  
      INSERT INTO AT_TIMESHEET_OT_DTL
        SELECT *
          FROM AT_TIMESHEET_OT_DTL_TEMP OT
         WHERE EMPLOYEE_ID IN (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP EE);
  
      DELETE AT_TIME_TIMESHEET_OT E
       WHERE E.PERIOD_ID = P_PERIOD_ID
         AND E.EMPLOYEE_ID NOT IN
             (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);
  
      DELETE AT_TIMESHEET_OT_DTL E
       WHERE E.PERIOD_ID = P_PERIOD_ID
         AND E.EMPLOYEE_ID NOT IN
             (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);
    EXCEPTION
      WHEN OTHERS THEN
  
        DELETE AT_TIME_TIMESHEET_OT E
       WHERE E.PERIOD_ID = P_PERIOD_ID
         AND E.EMPLOYEE_ID NOT IN
             (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);
  
      DELETE AT_TIMESHEET_OT_DTL E
       WHERE E.PERIOD_ID = P_PERIOD_ID
         AND E.EMPLOYEE_ID NOT IN
             (SELECT EMPLOYEE_ID FROM AT_CHOSEN_EMP_CLEAR);
        SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                                'PKG_ATTENDANCE_BUSINESS.CAL_TIMETIMESHEET_OT',
                                SQLERRM || '_' ||
                                DBMS_UTILITY.format_error_backtrace,
                                null,
                                null,
                                null,
                                null);
    END;
  */

  FUNCTION FN_GET_PHEP(P_WCODE  IN NVARCHAR2 DEFAULT '',
                       P_ORG_ID IN NUMBER DEFAULT 0,
                       P_YEAR   IN NUMBER DEFAULT 0) RETURN NUMBER AS
    PRAGMA AUTONOMOUS_TRANSACTION;
    PV_RESULT NUMBER;
    PV_SQL    CLOB;
    PV_ORG_ID NUMBER;
    PV_COUNT  NUMBER;
  BEGIN
  
    SELECT O.ID2
      INTO PV_ORG_ID
      FROM HU_ORGANIZATION_V O
     WHERE O.ID = P_ORG_ID;
  
    SELECT COUNT(*)
      INTO PV_COUNT
      FROM AT_LIST_PARAM_SYSTEM T
     WHERE T.ORG_ID = PV_ORG_ID;
  
    IF PV_COUNT > 0 THEN
    
      IF P_WCODE = 'TO_LEAVE_YEAR' OR P_WCODE = 'EFFECT_DATE_TO_NB' THEN
      
        PV_SQL := 'SELECT NVL((SELECT TO_LEAVE_YEAR
                FROM (select TO_CHAR(T.TO_LEAVE_YEAR, ''MM'') TO_LEAVE_YEAR
                        from AT_LIST_PARAM_SYSTEM t
                       WHERE TO_CHAR(T.EFFECT_DATE_FROM, ''yyyy'') <= :P_YEAR
                         AND T.ORG_ID = :PV_ORG_ID
                         AND T.ACTFLG = ''A''
                       ORDER BY T.EFFECT_DATE_FROM DESC) T
               WHERE ROWNUM = 1),0)
              FROM DUAL';
      
        /* INSERT INTO AT_STRSQL(ID,STRINGSQL)
        VALUES(1,PV_SQL); */
      
        EXECUTE IMMEDIATE TO_CHAR(PV_SQL)
          INTO PV_RESULT
          USING P_YEAR, PV_ORG_ID;
      
      ELSE
      
        PV_SQL := 'SELECT NVL((SELECT "' || P_WCODE || '"
                FROM (select "' || P_WCODE || '"
                        from AT_LIST_PARAM_SYSTEM t
                       WHERE TO_CHAR(T.EFFECT_DATE_FROM, ''yyyy'') <= :P_YEAR
                         AND T.ORG_ID = :PV_ORG_ID
                         AND T.ACTFLG = ''A''
                       ORDER BY T.EFFECT_DATE_FROM DESC) T
               WHERE ROWNUM = 1),0)
              FROM DUAL';
      
        /*INSERT INTO AT_STRSQL(ID,STRINGSQL)
        VALUES(1,PV_SQL); */
      
        EXECUTE IMMEDIATE TO_CHAR(PV_SQL)
          INTO PV_RESULT
          USING P_YEAR, PV_ORG_ID;
      
      END IF;
    
    ELSE
      IF P_WCODE = 'TO_LEAVE_YEAR' OR P_WCODE = 'EFFECT_DATE_TO_NB' THEN
        PV_SQL := 'SELECT NVL((SELECT TO_LEAVE_YEAR
                FROM (select TO_CHAR(T.TO_LEAVE_YEAR, ''MM'') TO_LEAVE_YEAR
                        from AT_LIST_PARAM_SYSTEM t
                       WHERE TO_CHAR(T.EFFECT_DATE_FROM, ''yyyy'') <= :P_YEAR
                         AND T.ACTFLG = ''A''
                         AND T.ORG_ID = -1
                       ORDER BY T.EFFECT_DATE_FROM DESC) T
               WHERE ROWNUM = 1),0)
              FROM DUAL';
      
        /*INSERT INTO AT_STRSQL(ID,STRINGSQL)
                   VALUES(1,PV_SQL);
        */
        EXECUTE IMMEDIATE TO_CHAR(PV_SQL)
          INTO PV_RESULT
          USING P_YEAR;
      
      ELSE
        PV_SQL := 'SELECT NVL((SELECT "' || P_WCODE || '"
                FROM (select "' || P_WCODE || '"
                        from AT_LIST_PARAM_SYSTEM t
                       WHERE TO_CHAR(T.EFFECT_DATE_FROM, ''yyyy'') <= :P_YEAR
                         AND T.ACTFLG = ''A''
                         AND T.ORG_ID = -1
                       ORDER BY T.EFFECT_DATE_FROM DESC) T
               WHERE ROWNUM = 1),0)
              FROM DUAL';
      
        /* INSERT INTO AT_STRSQL(ID,STRINGSQL)
        VALUES(1,PV_SQL); */
      
        EXECUTE IMMEDIATE TO_CHAR(PV_SQL)
          INTO PV_RESULT
          USING P_YEAR;
      END IF;
    
    END IF;
  
    RETURN PV_RESULT;
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.FN_GET_PHEP',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                              P_WCODE,
                              P_ORG_ID,
                              P_YEAR,
                              NULL,
                              NULL,
                              NULL,
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
      RETURN 0;
  END;

  FUNCTION FN_GET_PREV_USED(PV_RESET_MONTH IN NUMBER DEFAULT 1,
                            PV_ID          IN NUMBER) RETURN NUMBER AS
    PRAGMA AUTONOMOUS_TRANSACTION;
    PV_RESULT NUMBER;
    PV_STRING NVARCHAR2(2000);
    PV_I      NUMBER;
    PV_SQL    CLOB;
  BEGIN
    FOR PV_I IN 1 .. PV_RESET_MONTH LOOP
      IF PV_STRING IS NOT NULL THEN
        PV_STRING := PV_RESULT || ' + ' || 'NVL(A.CUR_USED' || PV_I ||
                     ',0)';
      ELSE
        PV_STRING := 'NVL(A.CUR_USED' || PV_I || ',0)';
      END IF;
    END LOOP;
  
    PV_SQL := 'SELECT NVL((SELECT *
                FROM (select ' || PV_STRING || '
                        from AT_ENTITLEMENT A
                       WHERE A.ID = :PV_ID) T
               WHERE ROWNUM = 1),0)
              FROM DUAL';
  
    /*  INSERT INTO AT_STRSQL(ID,STRINGSQL)
    VALUES(1,PV_SQL); */
  
    EXECUTE IMMEDIATE TO_CHAR(PV_SQL)
      INTO PV_RESULT
      USING PV_ID;
  
    RETURN PV_RESULT;
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.FN_GET_PREV_USED',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                              PV_RESET_MONTH,
                              1,
                              NULL,
                              NULL,
                              NULL,
                              NULL,
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
      RETURN 0;
  END;

  FUNCTION FN_GET_EXPIREDATE(P_ORG_ID IN NUMBER DEFAULT 0,
                             P_YEAR   IN NUMBER DEFAULT 0) RETURN NVARCHAR2 AS
    PRAGMA AUTONOMOUS_TRANSACTION;
    PV_RESULT NVARCHAR2(10);
    PV_SQL    CLOB;
    PV_ORG_ID NUMBER;
    PV_COUNT  NUMBER;
  BEGIN
  
    SELECT O.ID2
      INTO PV_ORG_ID
      FROM HU_ORGANIZATION_V O
     WHERE O.ID = P_ORG_ID;
  
    SELECT COUNT(*)
      INTO PV_COUNT
      FROM AT_LIST_PARAM_SYSTEM T
     WHERE T.ORG_ID = PV_ORG_ID;
  
    IF PV_COUNT > 0 THEN
    
      PV_SQL := 'SELECT NVL((SELECT TO_LEAVE_YEAR
                FROM (select TO_CHAR(T.TO_LEAVE_YEAR, ''dd/MM'') TO_LEAVE_YEAR
                        from AT_LIST_PARAM_SYSTEM t
                       WHERE TO_CHAR(T.EFFECT_DATE_FROM, ''yyyy'') <= :P_YEAR
                         AND T.ORG_ID = :PV_ORG_ID
                         AND T.ACTFLG = ''A''
                       ORDER BY T.EFFECT_DATE_FROM DESC) T
               WHERE ROWNUM = 1),0)
              FROM DUAL';
    
      /* INSERT INTO AT_STRSQL(ID,STRINGSQL)
      VALUES(1,PV_SQL); */
    
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL)
        INTO PV_RESULT
        USING P_YEAR, PV_ORG_ID;
    
    ELSE
    
      PV_SQL := 'SELECT NVL((SELECT TO_LEAVE_YEAR
                FROM (select TO_CHAR(T.TO_LEAVE_YEAR, ''dd/MM'') TO_LEAVE_YEAR
                        from AT_LIST_PARAM_SYSTEM t
                       WHERE TO_CHAR(T.EFFECT_DATE_FROM, ''yyyy'') <= :P_YEAR
                         AND T.ACTFLG = ''A''
                         AND T.ORG_ID = -1
                       ORDER BY T.EFFECT_DATE_FROM DESC) T
               WHERE ROWNUM = 1),0)
              FROM DUAL';
      /* INSERT INTO AT_STRSQL(ID,STRINGSQL)
      VALUES(1,PV_SQL); */
    
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL)
        INTO PV_RESULT
        USING P_YEAR;
    
    END IF;
  
    RETURN NVL(PV_RESULT, '');
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.FN_GET_OT',
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
                              NULL,
                              NULL,
                              NULL,
                              NULL);
      RETURN 0;
  END;

  FUNCTION DATE_TO_DATE_SUM(P_FROM_DATE IN DATE, P_TO_DATE IN DATE)
    RETURN NVARCHAR2 IS
    PV_DATE   DATE;
    PV_EX     NVARCHAR2(1);
    PV_C      NUMBER;
    PV_I      NUMBER;
    PV_RESULT NVARCHAR2(32000);
  
  BEGIN
    PV_RESULT := '';
    PV_DATE   := P_FROM_DATE;
    PV_C      := P_TO_DATE - P_FROM_DATE;
    FOR PV_I IN 0 .. PV_C LOOP
      PV_EX := ',';
      IF PV_I = PV_C THEN
        PV_EX := '';
      END IF;
    
      PV_RESULT := PV_RESULT || 'SUM(A.D' || '' ||
                   TO_NUMBER(TO_CHAR(TO_DATE(PV_DATE, 'DD/MM/RRRR'), 'DD')) || '' || ')' ||
                   ' AS ' || '"D' || TO_NUMBER(TO_CHAR(PV_DATE, 'DD')) || '"' ||
                   PV_EX;
      PV_DATE   := PV_DATE + 1;
    END LOOP;
    RETURN PV_RESULT;
  END;

  FUNCTION DATE_TO_DATE(P_FROM_DATE IN DATE, P_TO_DATE IN DATE)
    RETURN NVARCHAR2 IS
    PV_DATE   DATE;
    PV_EX     NVARCHAR2(1);
    PV_C      NUMBER;
    PV_I      NUMBER;
    PV_RESULT NVARCHAR2(32000);
  
  BEGIN
    PV_RESULT := '';
    PV_DATE   := P_FROM_DATE;
    PV_C      := P_TO_DATE - P_FROM_DATE;
    FOR PV_I IN 0 .. PV_C LOOP
      PV_EX := ',';
      IF PV_I = PV_C THEN
        PV_EX := '';
      END IF;
    
      PV_RESULT := PV_RESULT || '''' ||
                   TO_CHAR(TO_DATE(PV_DATE, 'DD/MM/RRRR'), 'DD') || '''' ||
                   ' AS ' || '"D' || TO_NUMBER(TO_CHAR(PV_DATE, 'DD')) || '"' ||
                   PV_EX;
      PV_DATE   := PV_DATE + 1;
    END LOOP;
    RETURN PV_RESULT;
  END;

  FUNCTION DATE_TO_DATE_SELECT(P_FROM_DATE IN DATE, P_TO_DATE IN DATE)
    RETURN NVARCHAR2 IS
    PV_DATE   DATE;
    PV_EX     NVARCHAR2(1);
    PV_C      NUMBER;
    PV_I      NUMBER;
    PV_RESULT NVARCHAR2(32000);
  
  BEGIN
    PV_RESULT := '';
    PV_DATE   := P_FROM_DATE;
    PV_C      := P_TO_DATE - P_FROM_DATE;
    FOR PV_I IN 0 .. PV_C LOOP
      PV_EX := ',';
      IF PV_I = PV_C THEN
        PV_EX := '';
      END IF;
    
      PV_RESULT := PV_RESULT || 'A.D' || '' ||
                   TO_NUMBER(TO_CHAR(TO_DATE(PV_DATE, 'DD/MM/RRRR'), 'DD')) || '' ||
                   ' AS ' || '"D' || TO_NUMBER(TO_CHAR(PV_DATE, 'DD')) || '"' ||
                   PV_EX;
      PV_DATE   := PV_DATE + 1;
    END LOOP;
    RETURN PV_RESULT;
  END;

  FUNCTION DATE_TO_DATE_SELECT1(P_FROM_DATE IN DATE, P_TO_DATE IN DATE)
    RETURN NVARCHAR2 IS
    PV_DATE   DATE;
    PV_EX     NVARCHAR2(1);
    PV_C      NUMBER;
    PV_I      NUMBER;
    PV_RESULT NVARCHAR2(32000);
  
  BEGIN
    PV_RESULT := '';
    PV_DATE   := P_FROM_DATE;
    PV_C      := P_TO_DATE - P_FROM_DATE;
    FOR PV_I IN 0 .. PV_C LOOP
      PV_EX := ',';
      IF PV_I = PV_C THEN
        PV_EX := '';
      END IF;
    
      PV_RESULT := PV_RESULT || ' D' ||
                   TO_NUMBER(TO_CHAR(TO_DATE(PV_DATE, 'DD/MM/RRRR'), 'DD')) ||
                   PV_EX;
      PV_DATE   := PV_DATE + 1;
    END LOOP;
    RETURN PV_RESULT;
  END;

  FUNCTION FN_CALL_DEDUCT(P_MIN_LATE       IN NUMBER,
                          P_MIN_EARLY      IN NUMBER,
                          P_MIN_LATE_EARLY IN NUMBER,
                          P_ORG_ID         IN NUMBER,
                          P_DATE           IN DATE) RETURN NUMBER AS
    PRAGMA AUTONOMOUS_TRANSACTION;
    PV_RESUILT        NUMBER;
    PV_COUNT          NUMBER;
    PV_ORG_ID         NUMBER;
    PV_TYPE           NUMBER;
    PV_DATE           DATE;
    PV_DATE1          DATE;
    PV_DATE2          DATE;
    PV_COUNT1         NUMBER;
    PV_COUNT2         NUMBER;
    PV_RESUILT1       NUMBER;
    PV_RESUILT2       NUMBER;
    PV_MIN_LATE_EARLY NUMBER;
  BEGIN
  
    PV_MIN_LATE_EARLY := NVL(P_MIN_LATE, 0) + NVL(P_MIN_EARLY, 0);
  
    SELECT O.ID2
      INTO PV_ORG_ID
      FROM HU_ORGANIZATION_V O
     WHERE O.ID = P_ORG_ID;
  
    SELECT COUNT(*)
      INTO PV_COUNT1
      FROM AT_SETUP_EXCHANGE T
     WHERE T.ORG_ID = PV_ORG_ID
       AND T.EFFECT_DATE <= P_DATE
       AND T.ACTFLG = 'A'
       AND T.TYPE_EXCHANGE = 4132;
  
    SELECT COUNT(*)
      INTO PV_COUNT2
      FROM AT_SETUP_EXCHANGE T
     WHERE T.ORG_ID = PV_ORG_ID
       AND T.EFFECT_DATE <= P_DATE
       AND T.ACTFLG = 'A'
       AND T.TYPE_EXCHANGE = 4133;
  
    IF NVL(P_MIN_LATE, 0) <> 0 THEN
      SELECT COUNT(*)
        INTO PV_COUNT
        FROM AT_SETUP_EXCHANGE T
       WHERE T.ORG_ID = PV_ORG_ID
         AND T.EFFECT_DATE <= P_DATE
         AND T.ACTFLG = 'A'
         AND T.TYPE_EXCHANGE = 4131;
    
      IF PV_COUNT > 0 THEN
        SELECT T.EFFECT_DATE
          INTO PV_DATE
          FROM (SELECT T.EFFECT_DATE
                  FROM AT_SETUP_EXCHANGE T
                 WHERE T.ORG_ID = PV_ORG_ID
                   AND T.EFFECT_DATE <= P_DATE
                   AND T.ACTFLG = 'A'
                   AND T.TYPE_EXCHANGE = 4131) T
         WHERE ROWNUM = 1;
      
        BEGIN
          SELECT T.NUMBER_DATE
            INTO PV_RESUILT
            FROM AT_SETUP_EXCHANGE T
           WHERE T.EFFECT_DATE = PV_DATE
             AND T.TYPE_EXCHANGE = 4131
             AND T.ORG_ID = PV_ORG_ID
             AND P_MIN_LATE BETWEEN T.FROM_MINUTE AND T.TO_MINUTE
             AND T.ACTFLG = 'A';
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            PV_RESUILT := 0;
        END;
      ELSE
        PV_RESUILT := 0;
      END IF;
    END IF;
  
    IF NVL(P_MIN_EARLY, 0) <> 0 THEN
      SELECT COUNT(*)
        INTO PV_COUNT1
        FROM AT_SETUP_EXCHANGE T
       WHERE T.ORG_ID = PV_ORG_ID
         AND T.EFFECT_DATE <= P_DATE
         AND T.ACTFLG = 'A'
         AND T.TYPE_EXCHANGE = 4132;
    
      IF PV_COUNT1 > 0 THEN
        SELECT T.EFFECT_DATE
          INTO PV_DATE1
          FROM (SELECT T.EFFECT_DATE
                  FROM AT_SETUP_EXCHANGE T
                 WHERE T.ORG_ID = PV_ORG_ID
                   AND T.EFFECT_DATE <= P_DATE
                   AND T.ACTFLG = 'A'
                   AND T.TYPE_EXCHANGE = 4132) T
         WHERE ROWNUM = 1;
      
        BEGIN
          SELECT T.NUMBER_DATE
            INTO PV_RESUILT1
            FROM AT_SETUP_EXCHANGE T
           WHERE T.EFFECT_DATE = PV_DATE1
             AND T.TYPE_EXCHANGE = 4132
             AND T.ORG_ID = PV_ORG_ID
             AND P_MIN_LATE BETWEEN T.FROM_MINUTE AND T.TO_MINUTE
             AND T.ACTFLG = 'A';
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            PV_RESUILT1 := 0;
        END;
      ELSE
        PV_RESUILT1 := 0;
      END IF;
    END IF;
  
    IF NVL(P_MIN_LATE, 0) + NVL(P_MIN_EARLY, 0) <> 0 THEN
      SELECT COUNT(*)
        INTO PV_COUNT2
        FROM AT_SETUP_EXCHANGE T
       WHERE T.ORG_ID = PV_ORG_ID
         AND T.EFFECT_DATE <= P_DATE
         AND T.ACTFLG = 'A'
         AND T.TYPE_EXCHANGE = 4133;
    
      IF PV_COUNT2 > 0 THEN
        SELECT T.EFFECT_DATE
          INTO PV_DATE2
          FROM (SELECT T.EFFECT_DATE
                  FROM AT_SETUP_EXCHANGE T
                 WHERE T.ORG_ID = PV_ORG_ID
                   AND T.EFFECT_DATE <= P_DATE
                   AND T.ACTFLG = 'A'
                   AND T.TYPE_EXCHANGE = 4133) T
         WHERE ROWNUM = 1;
      
        BEGIN
          SELECT T.NUMBER_DATE
            INTO PV_RESUILT2
            FROM AT_SETUP_EXCHANGE T
           WHERE T.EFFECT_DATE = PV_DATE2
             AND T.TYPE_EXCHANGE = 4133
             AND NVL(P_MIN_LATE, 0) + NVL(P_MIN_EARLY, 0) BETWEEN
                 T.FROM_MINUTE AND T.TO_MINUTE
             AND T.ACTFLG = 'A'
             AND T.ORG_ID = PV_ORG_ID;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            PV_RESUILT2 := 0;
        END;
      ELSE
        PV_RESUILT2 := 0;
      END IF;
    END IF;
  
    PV_RESUILT := NVL(PV_RESUILT, 0) + NVL(PV_RESUILT1, 0) +
                  NVL(PV_RESUILT2, 0);
    RETURN PV_RESUILT;
  EXCEPTION
    WHEN OTHERS THEN
      RETURN 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.FN_CALL_DEDUCT',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                              NULL,
                              NULL);
    
  END FN_CALL_DEDUCT;

  FUNCTION FN_GET_EXPIREDATE_NB(P_ORG_ID IN NUMBER DEFAULT 0,
                                P_YEAR   IN NUMBER DEFAULT 0)
    RETURN NVARCHAR2 AS
    PRAGMA AUTONOMOUS_TRANSACTION;
    PV_RESULT NVARCHAR2(10);
    PV_SQL    CLOB;
    PV_ORG_ID NUMBER;
    PV_COUNT  NUMBER;
  BEGIN
  
    SELECT O.ID2
      INTO PV_ORG_ID
      FROM HU_ORGANIZATION_V O
     WHERE O.ID = P_ORG_ID;
  
    SELECT COUNT(*)
      INTO PV_COUNT
      FROM AT_LIST_PARAM_SYSTEM T
     WHERE T.ORG_ID = PV_ORG_ID;
  
    IF PV_COUNT > 0 THEN
    
      PV_SQL := 'SELECT NVL((SELECT EFFECT_DATE_TO_NB
                FROM (select CASE WHEN NVL(T.NO_EFFECT_NB,0) = 0 THEN
                TO_CHAR(T.EFFECT_DATE_TO_NB, ''dd/MM'')
                ELSE
                  NULL
                END   EFFECT_DATE_TO_NB
                        from AT_LIST_PARAM_SYSTEM t
                       WHERE TO_CHAR(T.EFFECT_DATE_FROM, ''yyyy'') <= :P_YEAR
                         AND T.ORG_ID = :PV_ORG_ID
                         AND T.ACTFLG = ''A''
                       ORDER BY T.EFFECT_DATE_FROM DESC) T
               WHERE ROWNUM = 1),0)
              FROM DUAL';
    
      /* INSERT INTO AT_STRSQL(ID,STRINGSQL)
      VALUES(1,PV_SQL); */
    
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL)
        INTO PV_RESULT
        USING P_YEAR, PV_ORG_ID;
    
    ELSE
    
      PV_SQL := 'SELECT NVL((SELECT EFFECT_DATE_TO_NB
                FROM (select CASE WHEN NVL(T.NO_EFFECT_NB,0) = 0 THEN
                TO_CHAR(T.EFFECT_DATE_TO_NB, ''dd/MM'')
                ELSE
                  NULL
                END EFFECT_DATE_TO_NB
                        from AT_LIST_PARAM_SYSTEM t
                       WHERE TO_CHAR(T.EFFECT_DATE_FROM, ''yyyy'') <= :P_YEAR
                         AND T.ACTFLG = ''A''
                         AND T.ORG_ID = -1
                       ORDER BY T.EFFECT_DATE_FROM DESC) T
               WHERE ROWNUM = 1),0)
              FROM DUAL';
      /* INSERT INTO AT_STRSQL(ID,STRINGSQL)
      VALUES(1,PV_SQL); */
    
      EXECUTE IMMEDIATE TO_CHAR(PV_SQL)
        INTO PV_RESULT
        USING P_YEAR;
    
    END IF;
  
    RETURN NVL(PV_RESULT, '');
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.FN_GET_EXPIREDATE_NB',
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
                              NULL,
                              NULL,
                              NULL,
                              NULL);
      RETURN 0;
  END;

  FUNCTION FN_GET_PERIOD_STANDARD(P_ORG_ID    IN NUMBER DEFAULT 0,
                                  P_PERIOD_ID IN NUMBER DEFAULT 0,
                                  P_OBJECT_ID IN NUMBER) RETURN NUMBER AS
    PRAGMA AUTONOMOUS_TRANSACTION;
    PV_RESULT NUMBER;
    PV_ORG_ID NUMBER;
  BEGIN
    SELECT O.ID2
      INTO PV_ORG_ID
      FROM HU_ORGANIZATION_V O
     WHERE O.ID = P_ORG_ID;
  
    SELECT T.PERIOD_STANDARD
      INTO PV_RESULT
      FROM PA_WORK_STANDARD T
     WHERE T.ORG_ID = PV_ORG_ID
       AND T.PERIOD_ID = P_PERIOD_ID
       AND T.OBJECT_ID = P_OBJECT_ID
       AND T.ACTFLG = 'A';
  
    RETURN PV_RESULT;
  
  EXCEPTION
    WHEN OTHERS THEN
      RETURN 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.FN_GET_PERIOD_STANDARD',
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
      RETURN 0;
  END;
  FUNCTION FN_CHECK_EXIST_EMP(P_EMP_CODE IN NVARCHAR2, P_DATE IN DATE)
    RETURN NUMBER AS
    PRAGMA AUTONOMOUS_TRANSACTION;
    P_COUNT NUMBER;
  
  BEGIN
    SELECT COUNT(ID)
      INTO P_COUNT
      FROM HU_EMPLOYEE E
     WHERE E.EMPLOYEE_CODE = P_EMP_CODE
       AND E.IS_KIEM_NHIEM IS NULL
       And (E.WORK_STATUS <> 257 Or
           (E.WORK_STATUS = 257 And E.TER_LAST_DATE >= P_DATE));
    IF P_COUNT = 0 THEN
      RETURN 0;
    ELSE
      RETURN 1;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      RETURN - 1;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.FN_CHECK_EXIST_EMP',
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
  PROCEDURE IMPORT_DATA_REGISTER_CO_OUT( --P_XML      IN CLOB,
                                        --P_USERNAME IN VARCHAR2,
                                        P_REQUEST_ID IN NUMBER,
                                        /* P_OUT        OUT NUMBER*/
                                        P_RESULT OUT CURSOR_TYPE) IS
    PV_CHECK NUMBER := 0;
  BEGIN
    SELECT COUNT(*)
      INTO PV_CHECK
      FROM AT_DESCRIPTION_ERROR_TMP P
     WHERE P.REQUEST_ID = P_REQUEST_ID
       AND P.FLAG_ERROR <> 0;
    IF PV_CHECK = 0 THEN
      DELETE AT_DESCRIPTION_ERROR_TMP a WHERE a.REQUEST_ID = P_REQUEST_ID;
    END IF;
    OPEN P_RESULT FOR
      SELECT *
        FROM AT_DESCRIPTION_ERROR_TMP
       WHERE REQUEST_ID = P_REQUEST_ID;
    DELETE AT_DESCRIPTION_ERROR_TMP a WHERE a.REQUEST_ID = P_REQUEST_ID;
  END;
  PROCEDURE IMPORT_DATA_REGISTER_CO(P_XML        IN CLOB,
                                    P_USERNAME   IN VARCHAR2,
                                    P_REQUEST_ID IN NUMBER,
                                    P_OUT        OUT NUMBER
                                    /*P_RESULT   OUT CURSOR_TYPE*/) IS
    PV_SEQ            NUMBER;
    PV_SEQ_DES        NUMBER;
    PV_SEQ_AT_LS      NUMBER;
    P_COUNT           NUMBER;
    P_STATUS_SHIFT_PH NUMBER;
    P_STATUS_SHIFT_CT NUMBER;
    P_DAY_NUM         NUMBER;
    v_DOCXML          XMLTYPE;
    v_DATA            clob;
    v_INDEX           NUMBER := 0;
    v_FLAG_ERROR      NUMBER := 0;
    PV_CHECKNUM       NUMBER;
  BEGIN
    SELECT XMLTYPE.createXML(P.Value), p.value
      INTO v_DOCXML, v_DATA
      FROM AD_REQUEST_PARAMETERS P
     WHERE P.REQUEST_ID = P_REQUEST_ID
       AND P.SEQUENCE = 1;
    pkg_hcm_system.PRU_UPDATE_LOG_IN_REQUEST(P_REQUEST_ID,
                                             'Khoi tao tham so');
    --insert into temp(text) values (v_DATA);
    --v_DOCXML := XMLTYPE.createXML(P_XML);
    SELECT SEQ_AT_XMLDATA_TMP.NEXTVAL INTO PV_SEQ FROM DUAL;
    INSERT INTO AT_XMLDATA_TMP VALUES (PV_SEQ, v_DOCXML);
    COMMIT;
    -- HANDLE IMPORT
    pkg_hcm_system.PRU_UPDATE_LOG_IN_REQUEST(P_REQUEST_ID,
                                             'Bat dau import du lieu');
    FOR A IN (SELECT E.ID                   EMPLOYEE_ID,
                     TMP.MANUAL_ID,
                     TMP.MANUAL_NAME,
                     TMP.NOTE,
                     TMP.LEAVE_DAY          AS LEAVE_DAY,
                     TMP.STATUS_SHIFT_VALUE,
                     TMP.EMPLOYEE_CODE,
                     CASE
                       WHEN NVL(TM.ORDER_W,0) <>0 THEN
                         TMP.FROM_LOCATION
                       ELSE
                         NULL
                     END FROM_LOCATION,
                     CASE
                       WHEN NVL(TM.ORDER_W,0) <>0 THEN
                         TMP.TO_LOCATION
                       ELSE
                         NULL
                     END TO_LOCATION,
                     CASE
                       WHEN NVL(TM.ORDER_W,0) <>0 THEN
                         TMP.NUMBER_KILOMETER
                       ELSE
                         NULL
                     END NUMBER_KILOMETER,
                     TMP.STT,
                     CASE
                       WHEN TM.Code='CT' THEN
                         TMP.PROVINCE_ID
                       ELSE
                         NULL
                     END PROVINCE_ID
                FROM AT_XMLDATA_TMP T,
                     XMLTABLE('/DocumentElement/Table1' PASSING T.XML_DATA
                              COLUMNS "STT" NUMBER PATH 'STT',
                              "EMPLOYEE_CODE" NVARCHAR2(500) PATH
                              'EMPLOYEE_CODE',
                              "MANUAL_NAME" NVARCHAR2(500) PATH 'MANUAL_NAME',
                              "MANUAL_ID" NUMBER PATH 'MANUAL_ID',
                              "FROM_LOCATION" NVARCHAR2(500) PATH 'FROM_LOCATION',
                              "TO_LOCATION" NVARCHAR2(500) PATH 'TO_LOCATION',
                              "NUMBER_KILOMETER" NVARCHAR2(500) PATH 'NUMBER_KILOMETER',
                              "LEAVE_DAY" NVARCHAR2(500) PATH 'LEAVE_DAY',
                              "STATUS_SHIFT_VALUE" NUMBER PATH
                              'STATUS_SHIFT_VALUE',
                              "NOTE" NVARCHAR2(1000) PATH 'NOTE',
                              "PROVINCE_ID" NUMBER PATH 'PROVINCE_ID') TMP
               INNER JOIN HU_EMPLOYEE E
                  ON E.EMPLOYEE_CODE = TMP.EMPLOYEE_CODE
                 AND E.IS_KIEM_NHIEM IS NULL
               LEFT JOIN AT_TIME_MANUAL TM
               	  ON TM.ID = TMP.MANUAL_ID
               WHERE T.ID = PV_SEQ) LOOP
      --CHECK TUNG ROW
      v_INDEX    := v_INDEX + 1;
      PV_SEQ_DES := SEQ_AT_DESCRIPTION_ERROR_TMP.NEXTVAL;
      INSERT INTO AT_DESCRIPTION_ERROR_TMP
        (ID,
         STT,
         NOTE,
         EMPLOYEE_CODE,
         MANUAL_ID,
         LEAVE_DAY,
         REQUEST_ID,
         NUMBER_KILOMETER,
         PROVINCE_ID,
         FLAG_ERROR)
      VALUES
        (PV_SEQ_DES,
         v_INDEX,
         A.NOTE,
         A.EMPLOYEE_CODE,
         A.MANUAL_ID,
         A.LEAVE_DAY,
         P_REQUEST_ID,
         A.NUMBER_KILOMETER,
         A.PROVINCE_ID,
         0);
      IF PKG_FUNCTION.IsDate(TRIM(NVL(A.LEAVE_DAY, '')), 'DD/MM/RRRR') = 0 THEN
        UPDATE AT_DESCRIPTION_ERROR_TMP T
           SET T.LEAVE_DAY  = TO_CHAR(UNISTR('Ng\00E0y ngh\1EC9 kh\00F4ng \0111\00FAng \0111\1ECBnh d\1EA1ng  ')) ||
                              A.LEAVE_DAY,
               T.FLAG_ERROR = 1
         WHERE T.ID = PV_SEQ_DES;
        v_FLAG_ERROR := 1;
      ELSE
        IF FN_CHECK_EXIST_EMP(TRIM(A.EMPLOYEE_CODE),
                              TO_DATE(A.LEAVE_DAY, 'DD/MM/RRRR')) IN
           (0, -1) THEN
          UPDATE AT_DESCRIPTION_ERROR_TMP T
             SET T.EMPLOYEE_CODE = TO_CHAR(UNISTR('M\00E3 nh\00E2n vi\00EAn: ')) ||
                                   TO_CHAR(A.EMPLOYEE_CODE) ||
                                   TO_CHAR(UNISTR(' kh\00F4ng t\1ED3n t\1EA1i trong h\1EC7 th\1ED1ng, ')),
                 T.FLAG_ERROR    = 1
           WHERE T.ID = PV_SEQ_DES;
          v_FLAG_ERROR := 1;
        END IF;
        IF A.NUMBER_KILOMETER IS NOT NULL THEN
          IF PKG_FUNCTION.IS_NUMBER(A.NUMBER_KILOMETER)=0 THEN
              UPDATE AT_DESCRIPTION_ERROR_TMP T
                 SET T.NUMBER_KILOMETER  = TO_CHAR(UNISTR('Pha\0309i nh\00E2\0323p s\00F4\0301')),
                     T.FLAG_ERROR = 1
               WHERE T.ID = PV_SEQ_DES;
              v_FLAG_ERROR := 1;
          END IF;
        END IF;
        IF PKG_ATTENDANCE_LIST.FNC_CHECK_LEAVE_EXITS(TRIM(A.EMPLOYEE_CODE),
                                                     TRIM(A.LEAVE_DAY),
                                                     TRIM(A.MANUAL_ID),
                                                     A.STATUS_SHIFT_VALUE) > 0 THEN
          UPDATE AT_DESCRIPTION_ERROR_TMP T
             SET T.LEAVE_DAY  = TO_CHAR(UNISTR('Ng\00E0y ngh\1EC9 kh\00F4ng h\1EE3p l\1EC7 ')) ||
                                A.LEAVE_DAY,
                 T.FLAG_ERROR = 1
           WHERE T.ID = PV_SEQ_DES;
          v_FLAG_ERROR := 1;
        END IF;
        IF PKG_ATTENDANCE_LIST.FNC_CHECK_LEAVE_SHEET(TRIM(A.EMPLOYEE_CODE),
                                                     TRIM(A.LEAVE_DAY),
                                                     A.STATUS_SHIFT_VALUE) = -1 THEN
          UPDATE AT_DESCRIPTION_ERROR_TMP T
             SET T.LEAVE_DAY  = T.LEAVE_DAY || chr(10) ||
                                TO_CHAR(UNISTR('Ng\00E0y c\00F3 ki\1EC3u c\00F4ng l\00E0 0.5, 1.5 kh\00F4ng \0111\01B0\1EE3c \0111\0103ng k\00FD \0111\1EA7u ca, cu\1ED1i ca, ')),
                 T.FLAG_ERROR = 1
           WHERE T.ID = PV_SEQ_DES;
          v_FLAG_ERROR := 1;
        END IF;
        IF A.MANUAL_ID = 5 AND
           PKG_AT_LIST.FNC_GET_INFO_PHEPNAM(TRIM(A.EMPLOYEE_CODE),
                                            TO_DATE(A.LEAVE_DAY,
                                                    'DD/MM/RRRR'),
                                            PKG_ATTENDANCE_LIST.FNC_CHECK_LEAVE_SHEET(TRIM(A.EMPLOYEE_CODE),
                                                                                      TRIM(A.LEAVE_DAY),
                                                                                      (CASE WHEN
                                                                                       A.STATUS_SHIFT_VALUE IS NULL THEN 0 ELSE
                                                                                       A.STATUS_SHIFT_VALUE END))) = 1 THEN
          UPDATE AT_DESCRIPTION_ERROR_TMP T
             SET T.LEAVE_DAY  = T.LEAVE_DAY || chr(10) ||
                                TO_CHAR(UNISTR('M\00E3 nh\00E2n vi\00EAn: ')) ||
                                TO_CHAR(A.EMPLOYEE_CODE) ||
                                TO_CHAR(UNISTR(' \0111\00E3 v\01B0\1EE3t qu\00E1 s\1ED1 ph\00E9p qui \0111\1ECBnh, vui l\00F2ng \0111i\1EC1u ch\1EC9nh l\1EA1i d\1EEF li\1EC7u, ')),
                 T.FLAG_ERROR = 1
           WHERE T.ID = PV_SEQ_DES;
          v_FLAG_ERROR := 1;
        END IF;
        IF A.MANUAL_ID = 6 AND
           PKG_AT_LIST.FNC_GET_INFO_NGHIBU(TRIM(A.EMPLOYEE_CODE),
                                           TO_DATE(A.LEAVE_DAY, 'DD/MM/RRRR'),
                                           PKG_ATTENDANCE_LIST.FNC_CHECK_LEAVE_SHEET(TRIM(A.EMPLOYEE_CODE),
                                                                                     TRIM(A.LEAVE_DAY),
                                                                                     (CASE WHEN
                                                                                      A.STATUS_SHIFT_VALUE IS NULL THEN 0 ELSE
                                                                                      A.STATUS_SHIFT_VALUE END))) = 1 THEN
        
          UPDATE AT_DESCRIPTION_ERROR_TMP T
             SET T.LEAVE_DAY  = T.LEAVE_DAY || chr(10) ||
                                TO_CHAR(UNISTR('M\00E3 nh\00E2n vi\00EAn: ')) ||
                                TO_CHAR(A.EMPLOYEE_CODE) ||
                                TO_CHAR(UNISTR(' \0111\00E3 v\01B0\1EE3t qu\00E1 s\1ED1 ph\00E9p qui \0111\1ECBnh, vui l\00F2ng \0111i\1EC1u ch\1EC9nh l\1EA1i d\1EEF li\1EC7u, ')),
                 T.FLAG_ERROR = 1
           WHERE T.ID = PV_SEQ_DES;
          v_FLAG_ERROR := 1;
        END IF;
      END IF;
      --COMMIT;
      BEGIN
        --END CHECK
        SELECT SEQ_AT_LEAVESHEET.NEXTVAL,
               CASE
                 WHEN A.STATUS_SHIFT_VALUE IS NULL THEN
                  0
                 ELSE
                  A.STATUS_SHIFT_VALUE
               END
          INTO PV_SEQ_AT_LS, P_STATUS_SHIFT_PH
          FROM DUAL;
      
        SELECT PKG_ATTENDANCE_LIST.FNC_CHECK_LEAVE_SHEET(A.EMPLOYEE_CODE,
                                                         TRIM(A.LEAVE_DAY),
                                                         P_STATUS_SHIFT_PH)
          INTO P_DAY_NUM
          FROM DUAL;
        pkg_hcm_system.PRU_UPDATE_LOG_IN_REQUEST(P_REQUEST_ID,
                                                 'row data:' ||
                                                 '[EMPLOYEE_CODE]:' ||
                                                 A.EMPLOYEE_CODE ||
                                                 '[MANUAL_NAME]: ' ||
                                                 A.MANUAL_NAME ||
                                                 '[LEAVE_DAY]:' ||
                                                 A.LEAVE_DAY);
        --IF v_FLAG_ERROR=0 THEN
        SELECT COUNT(*)
          INTO P_COUNT
          FROM AT_LEAVESHEET_DETAIL_TMP L
         WHERE TRUNC(L.LEAVE_DAY) =
               TRUNC(TO_DATE(A.LEAVE_DAY, 'DD/MM/RRRR'))
           AND L.EMPLOYEE_ID = A.EMPLOYEE_ID;
        IF P_COUNT = 0 THEN
          INSERT INTO AT_LEAVESHEET_TMP
            (ID,
             EMPLOYEE_ID,
             MANUAL_ID,
             NOTE,
             DAY_NUM,
             LEAVE_FROM,
             LEAVE_TO,
             STATUS,
             IS_APP,
             IMPORT,
             CREATED_DATE,
             CREATED_BY,
             CREATED_LOG,
             MODIFIED_DATE,
             MODIFIED_BY,
             MODIFIED_LOG,
             FROM_LOCATION,
             TO_LOCATION,
             NUMBER_KILOMETER,
             PROVINCE_ID,
             REQUEST_ID)
          VALUES
            (PV_SEQ_AT_LS,
             A.EMPLOYEE_ID,
             A.MANUAL_ID,
             A.NOTE,
             P_DAY_NUM,
             TO_DATE(A.LEAVE_DAY, 'DD/MM/RRRR'),
             TO_DATE(A.LEAVE_DAY, 'DD/MM/RRRR'),
             1,
             -1,
             -1,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             SYSDATE,
             P_USERNAME,
             P_USERNAME,
             A.FROM_LOCATION,
             A.TO_LOCATION,
             TO_NUMBER(A.NUMBER_KILOMETER),
             A.PROVINCE_ID,
             P_REQUEST_ID);
          --COMMIT;
          SELECT CASE
                   WHEN A.STATUS_SHIFT_VALUE IS NULL THEN
                    -1
                   ELSE
                    A.STATUS_SHIFT_VALUE
                 END
            INTO P_STATUS_SHIFT_CT
            FROM DUAL;
          INSERT INTO AT_LEAVESHEET_DETAIL_TMP
            (ID,
             LEAVESHEET_ID,
             EMPLOYEE_ID,
             LEAVE_DAY,
             MANUAL_ID,
             DAY_NUM,
             STATUS_SHIFT,
             CREATED_DATE,
             CREATED_BY,
             CREATED_LOG,
             MODIFIED_DATE,
             MODIFIED_BY,
             MODIFIED_LOG,
             REQUEST_ID)
            SELECT SEQ_AT_LEAVESHEET_DETAIL.NEXTVAL,
                   AL.ID,
                   AL.EMPLOYEE_ID,
                   AL.LEAVE_FROM,
                   AL.MANUAL_ID,
                   AL.DAY_NUM,
                   P_STATUS_SHIFT_CT STATUS_SHIFT,
                   SYSDATE,
                   P_USERNAME,
                   P_USERNAME,
                   SYSDATE,
                   P_USERNAME,
                   P_USERNAME,
                   P_REQUEST_ID
              FROM AT_LEAVESHEET_TMP AL
             WHERE AL.id = PV_SEQ_AT_LS;
        END IF;
      EXCEPTION
        WHEN OTHERS THEN
          pkg_hcm_system.PRU_UPDATE_LOG_IN_REQUEST(P_REQUEST_ID,
                                                   'row data ERROR:' ||
                                                   '[EMPLOYEE_CODE]:' ||
                                                   A.EMPLOYEE_CODE ||
                                                   '[MANUAL_NAME]: ' ||
                                                   A.MANUAL_NAME ||
                                                   'CODE ERROR: ' ||
                                                   SQLERRM || '_' ||
                                                   DBMS_UTILITY.format_error_backtrace);
      END;
    
    --COMMIT;
    END LOOP;
    SELECT COUNT(*)
      INTO P_COUNT
      FROM AT_DESCRIPTION_ERROR_TMP P
     WHERE P.REQUEST_ID = P_REQUEST_ID
       AND P.FLAG_ERROR <> 0;
    IF P_COUNT > 0 THEN
      --ROLLBACK;
      P_OUT := 4;
    ELSE
      --COMMIT;
      P_OUT := 3;
      INSERT INTO  AT_LEAVESHEET
        SELECT *
          FROM AT_LEAVESHEET_TMP P
         WHERE P.REQUEST_ID = P_REQUEST_ID;
      INSERT INTO AT_LEAVESHEET_DETAIL
        SELECT *
          FROM AT_LEAVESHEET_DETAIL_TMP P
         WHERE P.REQUEST_ID = P_REQUEST_ID;
    END IF;
    DELETE FROM AT_LEAVESHEET_DETAIL_TMP WHERE REQUEST_ID = P_REQUEST_ID;
    DELETE FROM AT_LEAVESHEET_TMP WHERE REQUEST_ID = P_REQUEST_ID;
    --DELETE AT_DESCRIPTION_ERROR_TMP WHERE ID = PV_SEQ_DES;
    DELETE AT_XMLDATA_TMP WHERE ID = PV_SEQ;
    -- P_OUT := 3;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT := 4;
      DELETE FROM AT_LEAVESHEET_DETAIL_TMP WHERE REQUEST_ID = P_REQUEST_ID;
      DELETE FROM AT_LEAVESHEET_TMP WHERE REQUEST_ID = P_REQUEST_ID;
      -- DELETE AT_DESCRIPTION_ERROR_TMP WHERE REQUEST_ID=P_REQUEST_ID;
      DELETE AT_XMLDATA_TMP WHERE ID = PV_SEQ;
      pkg_hcm_system.PRU_UPDATE_LOG_IN_REQUEST(P_REQUEST_ID,
                                               'XAY RA LOI:' || SQLERRM || '_' ||
                                               DBMS_UTILITY.format_error_backtrace);
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.IMPORT_DATA_REGISTER_CO',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              NULL);
  END;

  PROCEDURE ENUM_NODE(n DBMS_XMLDOM.DOMNode, P_NAME NVARCHAR2) IS
    P_CHN DBMS_XMLDOM.DOMNode;
    nl    dbms_xmldom.DOMNodeList;
    P_N   DBMS_XMLDOM.DOMNode;
  BEGIN
    nl := dbms_xmldom.getChildNodes(n);
    FOR I IN 0 .. DBMS_XMLDOM.GETLENGTH(NL) LOOP
      P_CHN := DBMS_XMLDOM.ITEM(NL, I);
      IF DBMS_XMLDOM.GETNODETYPE(P_CHN) = 1 THEN
        PKG_ATTENDANCE_BUSINESS.ENUM_NODE(P_CHN,
                                          P_NAME ||
                                          DBMS_XMLDOM.GETNODENAME(P_CHN) || '/');
      END IF;
    END LOOP;
  
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.ENUM_NODE',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              NULL);
  END;

  PROCEDURE IMPORT_DATA_INOUT(P_DATA      IN CLOB,
                              P_USER      IN NVARCHAR2,    
                              P_TER_ID In Number,
                              P_FROM_DATE IN DATE,
                              P_END_DATE  IN DATE,
                              P_OUT       OUT CURSOR_TYPE) IS
    v_DOCXML XMLTYPE;
  BEGIN
    v_DOCXML := XMLTYPE.createXML(P_DATA);
    DELETE AT_SWIPE_DATA S
     WHERE S.WORKINGDAY BETWEEN P_FROM_DATE AND P_END_DATE
     And S.TERMINAL_ID=P_TER_ID And Nvl(S.DITRE_VESOM,0)<>1;
    --OPEN P_OUT FOR
    INSERT INTO TEMP (TEXT) VALUES (P_DATA);
    INSERT INTO AT_SWIPE_DATA
      (ID,
       ITIME_ID,
       VALTIME,
       TERMINAL_ID,
       CREATED_DATE,
       CREATED_BY,
       WORKINGDAY,
       MACHINE_TYPE,
       EMPLOYEE_ID,
       account_shortname)
      SELECT SEQ_AT_SWIPE_DATA.NEXTVAL,
             T.Itime_Id,
             TO_DATE(Trim(DOM.WORKING_DAY)||' '||Trim(DOM.VALTIME), 'YYYY/MM/DD HH24:MI'),
             P_TER_ID,
             SYSDATE + 1,
             'AUTO',
             TRUNC(TO_DATE(DOM.WORKING_DAY, 'YYYY/MM/DD')),
             6920,
             T.ID,
             DOM.account_shortname
        FROM XMLTABLE('/NewDataSet/Table' PASSING v_DOCXML COLUMNS
                      EMPLOYEE_CODE NVARCHAR2(6) PATH './EMPLOYEE_CODE',
                      VALTIME NVARCHAR2(50) PATH './VALTIME',
                      WORKING_DAY NVARCHAR2(50) PATH './WORKINGDAY',
                      MACHINE_TYPE NVARCHAR2(20) PATH './MACHINE_TYPE',
                      MACHINE_CODE NVARCHAR2(20) PATH './MACHINE_CODE',
                      account_shortname NVARCHAR2(20) PATH './account_shortname',
                      CHECKTYPE NVARCHAR2(2) PATH './CHECKTYPE') DOM
        Inner JOIN HU_EMPLOYEE T
          ON T.ITIME_ID = DOM.EMPLOYEE_CODE;
    
    OPEN P_OUT FOR
      SELECT 1 FROM DUAL;
  EXCEPTION
    WHEN OTHERS THEN
      OPEN P_OUT FOR
        SELECT 0 FROM DUAL;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.IMPORT_DATA_INOUT',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              NULL);
  END;

  PROCEDURE IMPORT_DATA_INOUT_CHECKINOUT(P_DATA        IN CLOB,
                                         P_TERMINAL_ID IN NUMBER,
                                         P_USER        IN NVARCHAR2,
                                         P_FROM_DATE   IN DATE,
                                         P_END_DATE    IN DATE,
                                         P_OUT         OUT CURSOR_TYPE) IS
    v_DOCXML XMLTYPE;
  BEGIN
    v_DOCXML := XMLTYPE.createXML(P_DATA);
    DELETE AT_SWIPE_DATA S
     WHERE S.WORKINGDAY BETWEEN P_FROM_DATE AND P_END_DATE
       AND S.TERMINAL_ID = P_TERMINAL_ID;
    --OPEN P_OUT FOR
    INSERT INTO AT_SWIPE_DATA
      (ID,
       ITIME_ID,
       VALTIME,
       TERMINAL_ID,
       CREATED_DATE,
       CREATED_BY,
       WORKINGDAY,
       MACHINE_TYPE,
       EMPLOYEE_ID)
      SELECT SEQ_AT_SWIPE_DATA.NEXTVAL,
             T.Itime_Id,
             DOM.VALTIME,
             P_TERMINAL_ID,
             SYSDATE + 1,
             'AUTO',
             TRUNC(TO_DATE(DOM.WORKINGDAY, 'dd/mm/yyyy HH24:MI:SS')),
             6920,
             T.ID
        FROM XMLTABLE('/NewDataSet/Table' PASSING v_DOCXML COLUMNS
                      EMPLOYEE_CODE NVARCHAR2(6) PATH './EMPLOYEE_CODE',
                      WORKINGDAY NVARCHAR2(50) PATH './WORKINGDAY',
                      VALTIME NVARCHAR2(50) PATH './VALTIME',
                      CHECKTYPE NVARCHAR2(2) PATH './CHECKTYPE') DOM
        LEFT JOIN HU_EMPLOYEE T
          ON T.ITIME_ID = DOM.EMPLOYEE_CODE;
    INSERT INTO TEMP (TEXT) VALUES (P_DATA);
    OPEN P_OUT FOR
      SELECT 1 FROM DUAL;
  EXCEPTION
    WHEN OTHERS THEN
      OPEN P_OUT FOR
        SELECT 0 FROM DUAL;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.IMPORT_DATA_INOUT_CHECKINOUT',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              NULL);
  END;

  PROCEDURE GET_OTHER_LIST_DSVM(P_USERNAME   IN NVARCHAR2,
                                P_ORG_ID     IN NUMBER,
                                P_START_DATE IN date,
                                P_END_DATE   IN date,
                                P_ISDISSOLVE IN NUMBER,
                                P_CUR        OUT CURSOR_TYPE,
                                P_CURCOUNR   OUT CURSOR_TYPE,
                                P_CURDATA    OUT CURSOR_TYPE) IS
  BEGIN
  
    OPEN P_CUR FOR
      SELECT OT.ID, OT.TYPE_ID, OT.NAME_VN, OT.STATUS
        FROM OT_OTHER_LIST OT
       WHERE OT.ACTFLG = 'A'
         AND OT.TYPE_CODE = 'DTVS';
    OPEN P_CURCOUNR FOR
      SELECT OT.ID, OT.TYPE_ID, OT.NAME_VN, OT.STATUS
        FROM OT_OTHER_LIST OT
       WHERE OT.ACTFLG = 'A'
         AND OT.TYPE_CODE = 'LTTDTVS';
    OPEN P_CURDATA FOR
      SELECT ROWNUM STT, TB.*
        FROM (SELECT E.ID            EMPLOYEE_ID,
                     E.EMPLOYEE_CODE,
                     E.FULLNAME_VN   VN_FULLNAME,
                     O.ID            ORG_ID,
                     O.NAME_VN       ORG_NAME,
                     O.ORG_PATH,
                     T.NAME_VN       TITLE_NAME,
                     K.NAME          STAFF_RANK_NAME
                FROM HU_EMPLOYEE E
               INNER JOIN (SELECT E.EMPLOYEE_ID,
                                 E.TITLE_ID,
                                 E.ORG_ID,
                                 E.IS_3B,
                                 E.STAFF_RANK_ID,
                                 ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                            FROM HU_WORKING E
                           WHERE E.EFFECT_DATE <= P_END_DATE
                             AND E.STATUS_ID = 447
                             AND E.IS_3B = 0) W
                  ON E.ID = W.EMPLOYEE_ID
                 AND W.ROW_NUMBER = 1
               INNER JOIN TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME), P_ORG_ID, NVL(P_ISDISSOLVE, 0))) O1
                  ON W.ORG_ID = O1.ORG_ID
                LEFT JOIN HUV_ORGANIZATION O
                  ON W.ORG_ID = O.ID
                LEFT JOIN HU_TITLE T
                  ON W.TITLE_ID = T.ID
                LEFT JOIN HU_STAFF_RANK K
                  ON W.STAFF_RANK_ID = K.ID
               WHERE E.CONTRACT_ID IS NOT NULL
                 AND (NVL(E.WORK_STATUS, 0) <> 257 OR
                     (E.WORK_STATUS = 257 AND
                     E.TER_LAST_DATE >= P_START_DATE))
               ORDER BY O.NAME_VN, E.EMPLOYEE_CODE) TB;
  END;
--anhvn
PROCEDURE CAL_TIME_TIMESHEET_BY_EMP(P_USERNAME   IN NVARCHAR2,
                                       P_DELETE_ALL IN NUMBER := 1,
                                       P_DATE  IN DATE,
                                       P_EMPLIST    IN CLOB,
                                       P_OUT        OUT NUMBER) IS
    PV_DATE    DATE;
    /*PV_ENDDATE     DATE;*/
    PV_SQL         CLOB;
    PV_REQUEST_ID  NUMBER;
    PV_MINUS_ALLOW NUMBER := 50;
    PV_SUNDAY      DATE; --Lay ngay chu nhat trong thang
    /*PV_TEST1       NUMBER;*/
   /* PV_TEST2       NUMBER;*/
    /*PV_CHECK       NUMBER;*/
    PV_DEL_ALL     NUMBER;
    /*PV_CHECKNV     NUMBER;*/
    PV_TG_BD_CA    DATE;
    PV_TG_KT_CA    DATE;
    PV_TG_BD_NGHI  DATE;
    PV_TG_KT_NGHI  DATE;
    PV_IN_MIN      DATE;
    PV_OUT_MAX     DATE;
    PV_TRAN_INDX   NUMBER;
    PV_SQL_CRE_TB  CLOB;
    PV_TBL_NAME    NVARCHAR2(50);
    --PV_TOKEN       NVARCHAR2(40);
  BEGIN
   /* SELECT standard_hash(TO_CHAR(P_ORG_ID) || P_USERNAME ||
                         TO_CHAR(P_PERIOD_ID),
                         'MD5')
      INTO PV_TOKEN
      FROM DUAL;
  
    SELECT COUNT(*)
      INTO PV_CHECK
      FROM user_tables UT
     WHERE UPPER(UT.TABLE_NAME) = UPPER(PV_TOKEN);
  
    IF PV_CHECK > 0 THEN
      PV_SQL_CRE_TB := '
            DROP TABLE ' || 'TBL' || PV_TOKEN || '
    ';
      --COMMIT;
      EXECUTE IMMEDIATE PV_SQL_CRE_TB;
    END IF;*/
  
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    PV_DEL_ALL    := P_DELETE_ALL;
    PV_TRAN_INDX  := ABS(REMAINDER(PV_REQUEST_ID, 15));
  
    PV_DATE := P_DATE;
 /*   PV_ENDDATE  := to_date(P_TO_DATE, 'dd/mm/yyyy');*/
  
    /*SELECT P.START_DATE, P.END_DATE
     INTO PV_FROMDATE, PV_ENDDATE
     FROM AT_PERIOD P
    WHERE P.ID = P_PERIOD_ID;*/
  
    -- Insert org can tinh toan
    /*INSERT INTO AT_CHOSEN_ORG_TMP E --temp
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
    COMMIT;*/
  
    /*INSERT INTO AT_CHOSEN_EMP_CLEAR_TMP -- temp
      (EMPLOYEE_ID, REQUEST_ID)
      SELECT DISTINCT S.EMPLOYEE_ID, PV_REQUEST_ID
        FROM AT_TIME_TIMESHEET_MACHINET S
       INNER JOIN (SELECT ORG_ID
                     FROM AT_CHOSEN_ORG_TMP O
                    WHERE O.REQUEST_ID = PV_REQUEST_ID) O
          ON O.ORG_ID = S.ORG_ID
       WHERE (S.CREATED_BY = 'AUTO')
         AND S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
    COMMIT;
  
    SELECT COUNT(1)
      INTO PV_CHECKNV
      FROM AT_TIME_TIMESHEET_MACHINET S
     INNER JOIN (SELECT ORG_ID
                   FROM AT_CHOSEN_ORG_TMP O
                  WHERE O.REQUEST_ID = PV_REQUEST_ID) O
        ON O.ORG_ID = S.ORG_ID
     WHERE S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
  
    SELECT COUNT(1) INTO PV_CHECK FROM AT_CHOSEN_EMP_CLEAR;
    IF (PV_CHECK = 0 AND PV_CHECKNV = 0) OR P_DELETE_ALL <> 0 THEN
      PV_DEL_ALL := 1;
    END IF;*/
  
    /*IF PV_DEL_ALL = 1 THEN*/
      --==TINH LAI TAT CA CA NV
      -- insert emp can tinh toan
      IF P_EMPLIST IS NOT NULL THEN
      INSERT INTO AT_CHOSEN_EMP_TMP --temp
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE,
         DECISION_ID,
         OBJECT_ATTENDANCE)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE,
                W.ID DECISION_ID,
                W.OBJECT_ATTENDANCE
           FROM HU_EMPLOYEE T
          INNER JOIN (SELECT E.ID,
                            E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            E.OBJECT_ATTENDANCE,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_DATE
                       /* and e.OBJECT_EMPLOYEE_ID = P_OBJ_EMP_ID*/
                        AND E.STATUS_ID = 447
                        AND E.IS_WAGE = 0
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          /*INNER JOIN (SELECT ORG_ID
                       FROM AT_CHOSEN_ORG_TMP O
                      WHERE O.REQUEST_ID = PV_REQUEST_ID) O
             ON O.ORG_ID = W.ORG_ID*/
          WHERE INSTR(',' || P_EMPLIST || ',', ',' || T.ID || ',') > 0
              AND (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_DATE)));
     END IF;
    /*ELSE
      --==CHI TINH NV AUTO
      -- insert emp can tinh toan
      INSERT INTO AT_CHOSEN_EMP_TMP
        (EMPLOYEE_ID,
         ITIME_ID,
         ORG_ID,
         TITLE_ID,
         STAFF_RANK_ID,
         STAFF_RANK_LEVEL,
         TER_EFFECT_DATE,
         USERNAME,
         REQUEST_ID,
         JOIN_DATE,
         JOIN_DATE_STATE,
         DECISION_ID,
         OBJECT_ATTENDANCE)
        (SELECT T.ID,
                T.ITIME_ID,
                W.ORG_ID,
                W.TITLE_ID,
                W.STAFF_RANK_ID,
                W.LEVEL_STAFF,
                CASE
                  WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                   T.TER_EFFECT_DATE + 1
                  ELSE
                   NULL
                END TER_EFFECT_DATE,
                UPPER(P_USERNAME),
                PV_REQUEST_ID,
                T.JOIN_DATE,
                T.JOIN_DATE_STATE,
                W.ID DECISION_ID,
                W.OBJECT_ATTENDANCE
           FROM HU_EMPLOYEE T
          INNER JOIN AT_CHOSEN_EMP_CLEAR_TMP C --==CHI TINH NV AUTO
             ON T.ID = C.EMPLOYEE_ID
            AND C.REQUEST_ID = PV_REQUEST_ID
          INNER JOIN (SELECT E.ID,
                            E.EMPLOYEE_ID,
                            E.TITLE_ID,
                            E.ORG_ID,
                            E.IS_3B,
                            E.STAFF_RANK_ID,
                            S.LEVEL_STAFF,
                            E.OBJECT_ATTENDANCE,
                            ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                       FROM HU_WORKING E
                       LEFT JOIN HU_STAFF_RANK S
                         ON E.STAFF_RANK_ID = S.ID
                      WHERE E.EFFECT_DATE <= PV_ENDDATE
                        and e.OBJECT_EMPLOYEE_ID = P_OBJ_EMP_ID
                        AND E.STATUS_ID = 447
                        AND E.IS_WAGE = 0
                        AND E.IS_3B = 0) W
             ON T.ID = W.EMPLOYEE_ID
            AND W.ROW_NUMBER = 1
          INNER JOIN (SELECT ORG_ID
                       FROM AT_CHOSEN_ORG_TMP O
                      WHERE O.REQUEST_ID = PV_REQUEST_ID) O
             ON O.ORG_ID = W.ORG_ID
          WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
    END IF;*/
    COMMIT;
  
    --RETURN;
    --==Start Chuc nang bang cong goc
    --==insert data v?o bang tam de tinh toan
    /*UPDATE AT_SWIPE_DATA S
    SET S.EMPLOYEE_ID = NVL((SELECT ID
                              FROM HU_EMPLOYEE E
                             WHERE E.ITIME_ID = S.ITIME_ID
                               AND NVL(E.IS_KIEM_NHIEM, 0) = 0),
                            0);*/
    INSERT INTO AT_SWIPE_DATA_TMP
      SELECT SW.*, PV_REQUEST_ID
        FROM AT_SWIPE_DATA SW
       INNER JOIN AT_CHOSEN_EMP_TMP E
          ON E.EMPLOYEE_ID = SW.EMPLOYEE_ID
         AND E.REQUEST_ID = PV_REQUEST_ID
       WHERE SW.WORKINGDAY =PV_DATE;
    COMMIT;
    INSERT INTO AT_WORKSIGN_TMP
      SELECT WS.*, PV_REQUEST_ID
        FROM AT_WORKSIGN WS
       INNER JOIN AT_CHOSEN_EMP_TMP E
          ON E.EMPLOYEE_ID = WS.EMPLOYEE_ID
         AND E.REQUEST_ID = PV_REQUEST_ID
       WHERE WS.WORKINGDAY =PV_DATE;
    COMMIT;
    INSERT INTO AT_LEAVESHEET_DETAIL_TMP
      SELECT D.ID,
             D.LEAVESHEET_ID,
             D.EMPLOYEE_ID,
             D.LEAVE_DAY,
             D.MANUAL_ID,
             D.DAY_NUM,
             D.STATUS_SHIFT,
             D.SHIFT_ID,
             D.CREATED_DATE,
             D.CREATED_BY,
             D.CREATED_LOG,
             D.MODIFIED_DATE,
             D.MODIFIED_BY,
             D.MODIFIED_LOG,
             PV_REQUEST_ID,
             D.OLD_LEAVE,
             D.REASON_LEAVE
        FROM AT_LEAVESHEET_DETAIL D
        JOIN AT_LEAVESHEET L
          ON L.ID = D.LEAVESHEET_ID
       WHERE D.LEAVE_DAY =PV_DATE
         AND L.STATUS = 1;
    COMMIT;
    INSERT INTO AT_TIME_TIMESHEET_MACHINE_TMP M --temp
      (ID,
       /*OBJ_EMP_ID,*/
      /* PERIOD_ID,*/
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       WORKINGDAY,
       SHIFT_ID,
       SHIFT_CODE,
       CREATED_DATE,
       --CREATED_BY,
       REQUEST_ID,
       VALIN1,
       VALOUT1,
       TIMEVALIN,
       TIMEVALOUT,
       TIMEIN_REALITY, --GIO VAO TT
       TIMEOUT_REALITY, -- GIO RA THUC TE
       OBJECT_ATTENDANCE,
       OBJECT_ATTENDANCE_CODE,
       WORK_HOUR,
       HOURS_STOP,
       HOURS_START,
       START_MID_HOURS,
       END_MID_HOURS,
       LATE_HOUR,
       EARLY_HOUR,
       SHIFT_DAY,
       HOURS_STAR_CHECKIN,
       HOURS_STAR_CHECKOUT,
       NOTE,
       --MIN_ON_LEAVE,
       MANUAL_ID,
       STATUS_SHIFT,
       MANUAL_CODE,
       MORNING_RATE,
       AFTERNOON_RATE,
       DAY_NUM,
       STATUS_SHIFT_NAME,
       CREATED_BY
       )
      SELECT SEQ_AT_TIME_TIMESHEET_MACHINET.NEXTVAL,
            /* P_OBJ_EMP_ID,*/
             /*P_PERIOD_ID,*/
             T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             T.STAFF_RANK_ID,
             WS.WORKINGDAY,
             WS.SHIFT_ID,
             SH.CODE SHIFT_CODE,
             SYSDATE,
             --P_USERNAME,
             PV_REQUEST_ID,
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_IN.VALTIME
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       0,
                                       PV_REQUEST_ID)
             END,
             
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_OUT.VALTIME
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       1,
                                       PV_REQUEST_ID)
             END,
             
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_IN.VALTIME
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       0,
                                       PV_REQUEST_ID)
             END,
             
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_OUT.VALTIME
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       1,
                                       PV_REQUEST_ID)
             END,
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_IN.VALTIME
               ELSE
                I.TIMEIN_REALITY
             END,
             
             CASE
               WHEN SH.CODE = 'OFF' THEN
                DATA_OUT.VALTIME
               ELSE
                I.TIMEOUT_REALITY
             END,
             T.OBJECT_ATTENDANCE,
             OL.CODE OBJECT_ATTENDANCE_CODE,
             SH.WORK_HOUR,
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STOP, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_START, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.START_MID_HOURS, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.END_MID_HOURS, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             CASE
               WHEN SH.LATE_HOUR IS NOT NULL THEN
                TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                        TO_CHAR(SH.LATE_HOUR, 'HH24:MI'),
                        'DD/MM/RRRR HH24:MI')
               ELSE
                NULL
             END,
             CASE
               WHEN SH.EARLY_HOUR IS NOT NULL THEN
                TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                        TO_CHAR(SH.EARLY_HOUR, 'HH24:MI'),
                        'DD/MM/RRRR HH24:MI')
               ELSE
                NULL
             END,
             SH.SHIFT_DAY,
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STAR_CHECKIN, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STAR_CHECKOUT, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             I.NOTE,
             --LEAVE.DAY_NUM,
             LEAVE.MANUAL_ID,
             LEAVE.STATUS_SHIFT,
             FM.CODE,
             MR.VALUE_RATE,
             AF.VALUE_RATE,
             LEAVE.DAY_NUM,
             CASE
               WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 0 THEN
                TO_CHAR(UNISTR(''))
               WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 1 THEN
                TO_CHAR(UNISTR('\0110\00E2\0300u ca'))
               WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 2 THEN
                TO_CHAR(UNISTR('cu\00F4\0301i ca'))
             END,
             CASE
               WHEN WS.EMPLOYEE_ID || TO_CHAR(WS.WORKINGDAY, 'DDMMYYYY') IN
                    (SELECT T.EMPLOYEE_ID ||
                            TO_CHAR(T.WORKING_DAY, 'DDMMYYYY')
                       FROM AT_TIMESHEET_MACHINET_IMPORT T
                      WHERE T.EMPLOYEE_ID = WS.EMPLOYEE_ID
                        AND T.WORKING_DAY = WS.WORKINGDAY) THEN
                P_USERNAME
               ELSE
                CAST('AUTO' AS NVARCHAR2(20))
             END  
        FROM AT_WORKSIGN_TMP WS
       INNER JOIN AT_CHOSEN_EMP_TMP T
          ON T.EMPLOYEE_ID = WS.EMPLOYEE_ID
         AND T.REQUEST_ID = PV_REQUEST_ID
        LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                          SW.WORKING_DAY,
                          SW.TIMEIN_REALITY,
                          SW.TIMEOUT_REALITY,
                          SW.NOTE,
                          ROW_NUMBER() OVER(PARTITION BY SW.EMPLOYEE_ID, SW.WORKING_DAY ORDER BY SW.TIMEIN_REALITY, SW.TIMEOUT_REALITY) AS ROW_NUMBER
                     FROM AT_TIMESHEET_MACHINET_IMPORT SW
                    WHERE SW.WORKING_DAY =PV_DATE
                   
                   ) I
          ON I.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND I.WORKING_DAY = WS.WORKINGDAY
         AND I.ROW_NUMBER = 1
        LEFT JOIN AT_SHIFT SH
          ON WS.SHIFT_ID = SH.ID
        LEFT JOIN AT_LEAVESHEET_DETAIL_TMP LEAVE
          ON WS.EMPLOYEE_ID = LEAVE.EMPLOYEE_ID
         AND LEAVE.REQUEST_ID = PV_REQUEST_ID
         AND TRUNC(WS.WORKINGDAY) = TRUNC(LEAVE.LEAVE_DAY)
        LEFT JOIN AT_TIME_MANUAL FM
          ON FM.ID = LEAVE.MANUAL_ID
        LEFT JOIN AT_TIME_MANUAL_RATE MR
          ON MR.ID = FM.MORNING_RATE_ID
        LEFT JOIN AT_TIME_MANUAL_RATE AF
          ON AF.ID = FM.AFTERNOON_RATE_ID
        LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                          SW.WORKINGDAY,
                          MIN(SW.VALTIME) AS VALTIME
                     FROM AT_SWIPE_DATA_TMP SW
                    WHERE SW.REQUEST_ID = PV_REQUEST_ID
                    GROUP BY SW.EMPLOYEE_ID, SW.WORKINGDAY) DATA_IN
          ON DATA_IN.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND DATA_IN.WORKINGDAY = WS.WORKINGDAY
        LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                          SW.WORKINGDAY,
                          MAX(SW.VALTIME) AS VALTIME
                     FROM AT_SWIPE_DATA_TMP SW
                    WHERE SW.REQUEST_ID = PV_REQUEST_ID
                    GROUP BY SW.EMPLOYEE_ID, SW.WORKINGDAY) DATA_OUT
          ON DATA_OUT.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND DATA_OUT.WORKINGDAY = WS.WORKINGDAY
        LEFT JOIN OT_OTHER_LIST OL
          ON T.OBJECT_ATTENDANCE = OL.ID
       WHERE WS.REQUEST_ID = PV_REQUEST_ID;
  
    --PHAN CHIA TRAN DE TINH TOAN
    PV_TBL_NAME   := TRIM('AT_TIME_TIMESHEET_MACHINE_TEMP' ||
                          TRIM(TO_CHAR(PV_REQUEST_ID)));                      
                          
    PV_SQL_CRE_TB := '
        CREATE TABLE ' || PV_TBL_NAME || '
        AS (SELECT * FROM AT_TIME_TIMESHEET_MACHINE_TMP WHERE 0=1)
    ';
    --INSERT INTO TEMP(TEXT)VALUES(PV_SQL_CRE_TB);
    EXECUTE IMMEDIATE PV_SQL_CRE_TB;
     
    PV_SQL_CRE_TB := '
       INSERT INTO ' || PV_TBL_NAME || '
       SELECT * FROM AT_TIME_TIMESHEET_MACHINE_TMP WHERE REQUEST_ID=' ||
                     PV_REQUEST_ID || '
    ';
    EXECUTE IMMEDIATE PV_SQL_CRE_TB;
    COMMIT;      
    
    --================================================================================
  
    --insert into AT_TIME_TIMESHEET_MACHINE_TEMP_1 select * from AT_TIME_TIMESHEET_MACHINE_TEMP;
    --== ap dung cong thuc
    FOR CUR_ITEM IN (SELECT *
                       FROM AT_TIME_FORMULAR T
                      WHERE T.TYPE IN (5)
                        AND T.STATUS = 1
                      ORDER BY T.ORDERBY) LOOP
      PV_SQL := 'UPDATE ' || PV_TBL_NAME || '  T SET ' ||
                CUR_ITEM.FORMULAR_CODE || '= NVL((' ||
                CUR_ITEM.FORMULAR_VALUE || '),NULL)   WHERE  REQUEST_ID = ' ||
                PV_REQUEST_ID || ' ';
      /* INSERT INTO TEMP1
        (TMP, WCODE)
      VALUES
        (PV_SQL, 'CUR_ITEM.FORMULAR_CODE');*/    
      BEGIN
        EXECUTE IMMEDIATE PV_SQL;
        COMMIT;
      EXCEPTION
        WHEN OTHERS THEN
          INSERT INTO TEMP1
            (TMP, WCODE)
          VALUES
            (PV_SQL, 'CUR_ITEM.FORMULAR_CODE');
          COMMIT;
          CONTINUE;
      END;
    END LOOP;
    -- XOA DU LIEU CU TRUOC KHI TINH
    DELETE FROM AT_TIME_TIMESHEET_MACHINET D
     WHERE D.WORKINGDAY =PV_DATE;
      /* AND D.ORG_ID IN (SELECT P.ORG_ID
                          FROM AT_CHOSEN_ORG_TMP P
                         WHERE P.REQUEST_ID = PV_REQUEST_ID);*/
    COMMIT;
    --==Insert thong tin da tin vao table that
    DELETE FROM AT_TIME_TIMESHEET_MACHINET T
     WHERE T.REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_TIME_TIMESHEET_MACHINE_TMP T
     WHERE T.REQUEST_ID = PV_REQUEST_ID;
    PV_SQL := '
        INSERT INTO AT_TIME_TIMESHEET_MACHINET
        SELECT  p.* FROM ' || PV_TBL_NAME ||
              ' P WHERE REQUEST_ID=' || PV_REQUEST_ID || '
    ';
    --INSERT INTO TEMP(TEXT)VALUES(PV_SQL_CRE_TB);
    EXECUTE IMMEDIATE PV_SQL;
    COMMIT;
    
     UPDATE AT_TIME_TIMESHEET_MACHINET T
       SET T.STATUS = (CASE WHEN (T.SHIFT_ID IS NULL OR T.SHIFT_ID = 1 OR T.SHIFT_ID = 2) THEN
                            ''
                       ELSE
                          CASE
                            WHEN (NVL(T.MIN_LATE,0) <> 0 OR NVL(T.MIN_EARLY,0) <> 0) THEN
                                CASE WHEN (NVL(T.MIN_LATE,0) <> 0 AND NVL(T.MIN_EARLY,0) <> 0) THEN
                                     'DITRE_VESOM'
                                ELSE
                                  CASE WHEN NVL(T.MIN_LATE,0) <> 0 THEN
                                     'DITRE'
                                  WHEN NVL(T.MIN_EARLY,0) <> 0 THEN
                                      'VESOM'
                                  END  
                                END                          
                            WHEN (T.TIMEIN_REALITY IS NULL AND T.TIMEOUT_REALITY IS NULL AND T.TIMEVALOUT IS NULL AND T.TIMEVALIN IS NULL) THEN
                                 'KHONGQT'
                            WHEN (T.TIMEIN_REALITY IS NULL OR T.TIMEOUT_REALITY IS NULL OR T.TIMEVALOUT IS NULL OR T.TIMEVALIN IS NULL) THEN
                            'THIEUQT'                       
                          ELSE
                           ''
                        END
                       END)                      
     WHERE T.REQUEST_ID = PV_REQUEST_ID;                       
     COMMIT;
    
    PV_SQL := '
        INSERT INTO AT_TIME_TIMESHEET_MACHINE_TMP
        SELECT  p.* FROM ' || PV_TBL_NAME ||
              ' P WHERE REQUEST_ID=' || PV_REQUEST_ID || '
    ';
  
    EXECUTE IMMEDIATE PV_SQL;
  
    PV_SQL_CRE_TB := '
            DROP TABLE ' || PV_TBL_NAME || '
    ';
    --INSERT INTO TEMP(TEXT)VALUES(PV_SQL_CRE_TB);
    COMMIT;
    EXECUTE IMMEDIATE PV_SQL_CRE_TB;
    /*INSERT INTO AT_TIME_TIMESHEET_MACHINET T
    SELECT DISTINCT P.*
      FROM AT_TIME_TIMESHEET_MACHINE_TEMP P
     WHERE P.REQUEST_ID = PV_REQUEST_ID;*/
  
    --==End Chuc nang bang cong goc
    --==Start Chuc nang tong hop cong
    DELETE FROM AT_TIME_TIMESHEET_DAILY D
     WHERE D.EMPLOYEE_ID IN
           (SELECT EMPLOYEE_ID
              FROM At_Chosen_Emp_Tmp O
             WHERE O.REQUEST_ID = PV_REQUEST_ID)
       AND EXISTS
     (SELECT D.ID
              FROM AT_TIME_TIMESHEET_DAILY D_CUR
             WHERE D_CUR.EMPLOYEE_ID = D.EMPLOYEE_ID
               AND D_CUR.WORKINGDAY = D.WORKINGDAY
               AND (D_CUR.CREATED_BY = 'AUTO' OR P_DELETE_ALL <> 0))
       AND D.WORKINGDAY = PV_DATE;
  
    INSERT INTO AT_TIME_TIMESHEET_DAILY T
      (ID,
       T.EMPLOYEE_ID,
       T.ORG_ID,
       T.TITLE_ID,
       T.WORKINGDAY,
       T.SHIFT_CODE,
       T.SHIFT_ID,
       T.WORKINGHOUR,
       T.MANUAL_ID,
       T.CREATED_DATE,
       T.CREATED_BY,
       T.CREATED_LOG,
       T.MODIFIED_DATE,
       T.MODIFIED_BY,
       T.MODIFIED_LOG,
       /* VALIN1,
       VALIN2,
       VALIN3,
       VALIN4,
       VALIN5,
       VALIN6,
       VALIN7,
       VALIN8,
       VALIN9,
       VALIN10,
       VALIN11,
       VALIN12,
       VALIN13,
       VALIN14,
       VALIN15,
       VALIN16,
       VALOUT1,
       VALOUT2,
       VALOUT3,
       VALOUT4,
       VALOUT5,
       VALOUT6,
       VALOUT7,
       VALOUT8,
       VALOUT9,
       VALOUT10,
       VALOUT11,
       VALOUT12,
       VALOUT13,
       VALOUT14,
       VALOUT15,
       VALOUT16,*/
       T.TIMEVALIN,
       TIMEVALOUT,
       OBJECT_ATTENDANCE,
       MIN_IN_WORK,
       MIN_DEDUCT_WORK,
       MIN_ON_LEAVE,
       MIN_LATE,
       MIN_EARLY,
       MIN_OUT_WORK_DEDUCT,
       MIN_LATE_EARLY,
       MIN_OUT_WORK,
       MIN_DEDUCT)
      SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
             M.EMPLOYEE_ID,
             M.ORG_ID,
             M.TITLE_ID,
             M.WORKINGDAY,
             M.SHIFT_CODE,
             M.SHIFT_ID,
             CASE
               WHEN NVL(L.EMPLOYEE_ID, 0) > 0 AND L.Leave_Day IS NOT NULL THEN
                0 --==UU TIEN XET DANG KY NGHI
               ELSE
                CASE
                  WHEN O.CODE = 'KCC' THEN
                   (SELECT WORK_HOUR FROM AT_SHIFT WHERE CODE = 'HC')
                  WHEN M.TIMEVALIN IS NOT NULL THEN --==CO GIO VAO-RA SE LAY CA DI LAM [X]
                   (SELECT WORK_HOUR FROM AT_SHIFT WHERE CODE = 'HC')
                  ELSE
                   0 --==K CO GIO VAO-RA LAY NGHI K LY DO
                END
             END WORKINGHOUR,
             M.SHIFT_TYPE_ID AS MANUAL_ID,
             
             SYSDATE,
             'AUTO',
             UPPER(P_USERNAME),
             SYSDATE,
             UPPER(P_USERNAME),
             UPPER(P_USERNAME),
             /* VALIN1,
             VALIN2,
             VALIN3,
             VALIN4,
             VALIN5,
             VALIN6,
             VALIN7,
             VALIN8,
             VALIN9,
             VALIN10,
             VALIN11,
             VALIN12,
             VALIN13,
             VALIN14,
             VALIN15,
             VALIN16,
             VALOUT1,
             VALOUT2,
             VALOUT3,
             VALOUT4,
             VALOUT5,
             VALOUT6,
             VALOUT7,
             VALOUT8,
             VALOUT9,
             VALOUT10,
             VALOUT11,
             VALOUT12,
             VALOUT13,
             VALOUT14,
             VALOUT15,
             VALOUT16,*/
             M.TIMEVALIN,
             M.TIMEVALOUT,
             M.OBJECT_ATTENDANCE,
             M.MIN_IN_WORK,
             M.MIN_DEDUCT_WORK,
             M.MIN_ON_LEAVE,
             M.MIN_LATE,
             M.MIN_EARLY,
             M.MIN_OUT_WORK_DEDUCT,
             M.MIN_LATE_EARLY,
             M.MIN_OUT_WORK,
             M.MIN_DEDUCT
        FROM AT_TIME_TIMESHEET_MACHINE_TMP M
        LEFT JOIN AT_LEAVESHEET_DETAIL_TMP L
          ON M.EMPLOYEE_ID = L.EMPLOYEE_ID
         AND M.WORKINGDAY = L.LEAVE_DAY
        LEFT JOIN AT_SHIFT SH
          ON SH.ID = M.SHIFT_ID
        LEFT JOIN OT_OTHER_LIST O
          ON M.OBJECT_ATTENDANCE = O.ID
       WHERE M.REQUEST_ID = PV_REQUEST_ID
         AND NOT EXISTS (SELECT D.ID
                FROM AT_TIME_TIMESHEET_DAILY D
               WHERE D.EMPLOYEE_ID = M.EMPLOYEE_ID
                 AND D.WORKINGDAY = M.WORKINGDAY);
  
    INSERT INTO TEMP1
      (TMP, WCODE, EXEDATE, TYPE)
    VALUES
      ('INSERT INTO AT_CHOSEN_EMP', '12', SYSDATE, 400);
    COMMIT;
    --Cham cong chi tiet hang ngay
    /* INSERT INTO AT_TIME_TIMESHEET_DAILY T
    (T.ID,
     T.EMPLOYEE_ID,
     T.ORG_ID,
     T.TITLE_ID,
     T.WORKINGDAY,
     T.MANUAL_ID,
     T.CREATED_DATE,
     T.CREATED_BY,
     T.CREATED_LOG,
     T.MODIFIED_DATE,
     T.MODIFIED_BY,
     T.MODIFIED_LOG)
    SELECT SEQ_AT_TIME_TIMESHEET_DAILY.NEXTVAL,
           EMP.EMPLOYEE_ID,
           EMP.ORG_ID,
           EMP.TITLE_ID,
           H.WORKINGDAY,
           23, --==L
           SYSDATE,
           'AUTO',
           UPPER(P_USERNAME),
           SYSDATE,
           UPPER(P_USERNAME),
           UPPER(P_USERNAME)
      FROM AT_CHOSEN_EMP_TMP EMP,
           (SELECT DISTINCT H.WORKINGDAY
              FROM AT_HOLIDAY H
             WHERE H.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE) H
     WHERE H.WORKINGDAY >= EMP.JOIN_DATE and emp.request_id=PV_REQUEST_ID
       AND
          -- EMP.EMPLOYEE_ID NOT IN (SELECT T.EMPLOYEE_ID FROM AT_TIME_TIMESHEET_DAILY T WHERE T.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE );
           NOT EXISTS (SELECT D.ID
              FROM AT_TIME_TIMESHEET_DAILY D
             WHERE D.EMPLOYEE_ID = EMP.EMPLOYEE_ID
               AND D.WORKINGDAY = H.WORKINGDAY);*/
  
    DELETE FROM AT_CHOSEN_ORG_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_CHOSEN_EMP_CLEAR_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_CHOSEN_EMP_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_SWIPE_DATA_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_WORKSIGN_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_TIME_TIMESHEET_MACHINE_TMP
     WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_LEAVESHEET_DETAIL_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    --DELETE FROM AT_CHOSEN_EMP_CLEAR_TMP
    P_OUT := 3;
  EXCEPTION
    WHEN OTHERS THEN
    
      P_OUT := 4;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_BY_EMP',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              NULL,
                              NULL,
                              /*P_ORG_ID,
                              P_PERIOD_ID,*/
                              P_USERNAME,
                              PV_REQUEST_ID,
                              PV_TBL_NAME,
                              PV_SQL);
      PV_TBL_NAME := TRIM('AT_TIME_TIMESHEET_MACHINE_TEMP' ||
                          TRIM(TO_CHAR(PV_REQUEST_ID)));
      BEGIN
        PV_SQL_CRE_TB := '
            DROP TABLE ' || PV_TBL_NAME || '
       ';
        --INSERT INTO TEMP(TEXT)VALUES(PV_SQL_CRE_TB);
        --COMMIT;
        EXECUTE IMMEDIATE PV_SQL_CRE_TB;
      EXCEPTION
        WHEN OTHERS THEN
          P_OUT := 4;
      END;
      DELETE FROM AT_CHOSEN_ORG_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_CHOSEN_EMP_CLEAR_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_CHOSEN_EMP_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_SWIPE_DATA_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_WORKSIGN_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_TIME_TIMESHEET_MACHINE_TMP
       WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_LEAVESHEET_DETAIL_TMP
       WHERE REQUEST_ID = PV_REQUEST_ID;
      COMMIT;
  END;
  
  PROCEDURE GET_PERIOD_USER(P_USER IN NVARCHAR2,
                            P_OUT OUT CURSOR_TYPE,
                            P_OUT1 OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_OUT FOR
      SELECT LEVEL A1 
        FROM DUAL
       WHERE LEVEL > 2018
     CONNECT BY LEVEL <= EXTRACT (YEAR FROM SYSDATE) + 1;
    
    OPEN P_OUT1 FOR
      SELECT A.MONTH A2,
             A.YEAR A1,
             A.YEAR || A.MONTH A3,
             A.ID A4
        FROM AT_PERIOD A;
  END;
  
  PROCEDURE IMPORT_OT(P_DOCXML IN CLOB,P_USERNAME IN NVARCHAR2,P_LOG IN NVARCHAR2) IS
  
   v_DOCXML XMLTYPE;
  BEGIN
    v_DOCXML := XMLTYPE.createXML(P_DOCXML);
    FOR ITEM IN ( SELECT 
             E.ID,
             DOM.EMPLOYEE_CODE,
             DOM.ID_PERIOD,
             DOM.OT_DAY,
             DOM.OT_NIGHT,
             DOM.OT_WEEKEND_DAY,
             DOM.OT_WEEKEND_NIGHT,
             DOM.OT_HOLIDAY_DAY,
             DOM.OT_HOLIDAY_NIGHT,
             DOM.NEW_OT_DAY,
             DOM.NEW_OT_NIGHT,
             DOM.NEW_OT_WEEKEND_DAY,
             DOM.NEW_OT_WEEKEND_NIGHT,
             DOM.NEW_OT_HOLIDAY_DAY,
             DOM.NEW_OT_HOLIDAY_NIGHT,
             DOM.NUMBER_FACTOR_CP,
             DOM.TOTAL_FACTOR1,
             DOM.TOTAL_FACTOR1_5,
             DOM.TOTAL_FACTOR1_8,
             DOM.TOTAL_FACTOR2,
             DOM.TOTAL_FACTOR2_1,
             DOM.TOTAL_FACTOR2_7,
             DOM.TOTAL_FACTOR3,
             DOM.TOTAL_FACTOR3_9,
             DOM.TOTAL_NB1,
             DOM.TOTAL_NB1_5,
             DOM.TOTAL_NB1_8,
             DOM.TOTAL_NB2,
             DOM.TOTAL_NB2_1,
             DOM.TOTAL_NB2_7,
             DOM.TOTAL_NB3,
             DOM.TOTAL_NB3_9,
             DOM.IS_LOCK,
             DOM.COUNT_OT_SUPPORT,
             E.ORG_ID
        FROM XMLTABLE('/NewDataSet/Import_THOT' PASSING v_DOCXML COLUMNS                      
                      EMPLOYEE_CODE NVARCHAR2(10) PATH './EMPLOYEE_CODE',
                      ID_PERIOD NUMBER PATH './ID_PERIOD',
                      OT_DAY NUMBER PATH './OT_DAY',
                      OT_NIGHT NUMBER PATH './OT_NIGHT',
                      OT_WEEKEND_DAY NUMBER PATH './OT_WEEKEND_DAY',
                      OT_WEEKEND_NIGHT NUMBER PATH './OT_WEEKEND_NIGHT',
                      OT_HOLIDAY_DAY NUMBER PATH './OT_HOLIDAY_DAY',
                      OT_HOLIDAY_NIGHT NUMBER PATH './OT_HOLIDAY_NIGHT',
                      NEW_OT_DAY NUMBER PATH './NEW_OT_DAY',
                      NEW_OT_NIGHT NUMBER PATH './NEW_OT_NIGHT',
                      NEW_OT_WEEKEND_DAY NUMBER PATH './NEW_OT_WEEKEND_DAY',
                      NEW_OT_WEEKEND_NIGHT NUMBER PATH './NEW_OT_WEEKEND_NIGHT',
                      NEW_OT_HOLIDAY_DAY NUMBER PATH './NEW_OT_HOLIDAY_DAY',
                      NEW_OT_HOLIDAY_NIGHT NUMBER PATH './NEW_OT_HOLIDAY_NIGHT',
                      NUMBER_FACTOR_CP NUMBER PATH './NUMBER_FACTOR_CP',
                      TOTAL_FACTOR1 NUMBER PATH './TOTAL_FACTOR1',
                      TOTAL_FACTOR1_5 NUMBER PATH './TOTAL_FACTOR1_5',
                      TOTAL_FACTOR1_8 NUMBER PATH './TOTAL_FACTOR1_8',
                      TOTAL_FACTOR2 NUMBER PATH './TOTAL_FACTOR2',
                      TOTAL_FACTOR2_1 NUMBER PATH './TOTAL_FACTOR2_1',
                      TOTAL_FACTOR2_7 NUMBER PATH './TOTAL_FACTOR2_7',
                      TOTAL_FACTOR3 NUMBER PATH './TOTAL_FACTOR3',
                      TOTAL_FACTOR3_9 NUMBER PATH './TOTAL_FACTOR3_9',
                      TOTAL_NB1 NUMBER PATH './TOTAL_NB1',
                      TOTAL_NB1_5 NUMBER PATH './TOTAL_NB1_5',
                      TOTAL_NB1_8 NUMBER PATH './TOTAL_NB1_8',
                      TOTAL_NB2 NUMBER PATH './TOTAL_NB2',
                      TOTAL_NB2_1 NUMBER PATH './TOTAL_NB2_1',
                      TOTAL_NB2_7 NUMBER PATH './TOTAL_NB2_7',
                      TOTAL_NB3 NUMBER PATH './TOTAL_NB3',
                      TOTAL_NB3_9 NUMBER PATH './TOTAL_NB3_9',
                      IS_LOCK NUMBER PATH './IS_LOCK',
                      COUNT_OT_SUPPORT NUMBER PATH './COUNT_OT_SUPPORT') DOM
        INNER JOIN HU_EMPLOYEE E
          ON E.EMPLOYEE_CODE = DOM.EMPLOYEE_CODE) LOOP
    
    DELETE FROM AT_TIME_TIMESHEET_OT B WHERE B.PERIOD_ID = ITEM.ID_PERIOD
                                         AND B.EMPLOYEE_ID = ITEM.ID;
    INSERT INTO AT_TIME_TIMESHEET_OT
      (ID,
       EMPLOYEE_ID,
       PERIOD_ID,
       OT_DAY,
       OT_NIGHT,
       OT_WEEKEND_DAY,
       OT_WEEKEND_NIGHT,
       OT_HOLIDAY_DAY,
       OT_HOLIDAY_NIGHT,
       NEW_OT_DAY,
       NEW_OT_NIGHT,
       NEW_OT_WEEKEND_DAY,
       NEW_OT_WEEKEND_NIGHT,
       NEW_OT_HOLIDAY_DAY,
       NEW_OT_HOLIDAY_NIGHT,
       NUMBER_FACTOR_CP,
       TOTAL_FACTOR1,
       TOTAL_FACTOR1_5,
       TOTAL_FACTOR1_8,
       TOTAL_FACTOR2,
       TOTAL_FACTOR2_1,
       TOTAL_FACTOR2_7,
       TOTAL_FACTOR3,
       TOTAL_FACTOR3_9,
       TOTAL_NB1,
       TOTAL_NB1_5,
       TOTAL_NB1_8,
       TOTAL_NB2,
       TOTAL_NB2_1,
       TOTAL_NB2_7,
       TOTAL_NB3,
       TOTAL_NB3_9,
       IS_LOCK,
       ORG_ID,
       COUNT_OT_SUPPORT,
       
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG,
       MODIFIED_DATE,
       MODIFIED_BY,
       MODIFIED_LOG
       )
       VALUES(
       SEQ_AT_TIME_TIMESHEET_OT.NEXTVAL,
       ITEM.ID,
       ITEM.ID_PERIOD,
       ITEM.OT_DAY,
       ITEM.OT_NIGHT,
       ITEM.OT_WEEKEND_DAY,
       ITEM.OT_WEEKEND_NIGHT,
       ITEM.OT_HOLIDAY_DAY,
       ITEM.OT_HOLIDAY_NIGHT,
       ITEM.NEW_OT_DAY,
       ITEM.NEW_OT_NIGHT,
       ITEM.NEW_OT_WEEKEND_DAY,
       ITEM.NEW_OT_WEEKEND_NIGHT,
       ITEM.NEW_OT_HOLIDAY_DAY,
       ITEM.NEW_OT_HOLIDAY_NIGHT,
       ITEM.NUMBER_FACTOR_CP,
       ITEM.TOTAL_FACTOR1,
       ITEM.TOTAL_FACTOR1_5,
       ITEM.TOTAL_FACTOR1_8,
       ITEM.TOTAL_FACTOR2,
       ITEM.TOTAL_FACTOR2_1,
       ITEM.TOTAL_FACTOR2_7,
       ITEM.TOTAL_FACTOR3,
       ITEM.TOTAL_FACTOR3_9,
       ITEM.TOTAL_NB1,
       ITEM.TOTAL_NB1_5,
       ITEM.TOTAL_NB1_8,
       ITEM.TOTAL_NB2,
       ITEM.TOTAL_NB2_1,
       ITEM.TOTAL_NB2_7,
       ITEM.TOTAL_NB3,
       ITEM.TOTAL_NB3_9,
       ITEM.IS_LOCK,
       ITEM.ORG_ID,
       ITEM.COUNT_OT_SUPPORT,
       SYSDATE,
       P_USERNAME,
       P_LOG,
       SYSDATE,
       P_USERNAME,
       P_LOG
       ); 
   END LOOP ;    
     
  EXCEPTION    
    WHEN OTHERS THEN
      ROLLBACK;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_HU_IPROFILE_EMPLOYEE.IMPORT_NV',
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
                              NULL,
                              NULL,
                              NULL,
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
  
  PROCEDURE EXPORT_QLKT(P_CUR OUT CURSOR_TYPE) IS
  BEGIN
    OPEN P_CUR FOR
      SELECT O.ID, O.NAME_VN FROM OT_OTHER_LIST O WHERE O.TYPE_ID = 2238;
  EXCEPTION
    WHEN OTHERS THEN
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.IMPORT_OT',
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
                              NULL,
                              NULL,
                              NULL,
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
  PROCEDURE DELETE_AT_WORKSIGN_FROM_AT_SIGNDEFAULT(P_EMPLOYEE_ID IN NUMBER,
                                  P_FROMDATE   IN DATE,
                                  P_ENDATE     IN DATE,
                                  P_OUT        OUT NUMBER) IS
  BEGIN
    DELETE AT_WORKSIGN S
    WHERE S.EMPLOYEE_ID = P_EMPLOYEE_ID
    AND S.WORKINGDAY BETWEEN TRUNC(P_FROMDATE) AND TRUNC(P_ENDATE);
    P_OUT:=3;
  EXCEPTION
    WHEN OTHERS THEN
      P_OUT:=0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.DELETE_AT_WORKSIGN_FROM_AT_SIGNDEFAULT',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                              P_EMPLOYEE_ID,
                              P_FROMDATE,
                              P_ENDATE);                                
  END;       
  PROCEDURE INSERT_GEN_ATWORKSIGN(P_USERNAME   IN VARCHAR2,
                                P_ORG_ID     IN NUMBER,
                                P_ISDISSOLVE IN NUMBER,
                                P_EMPLOYEE_ID IN CLOB,
                                P_YEAR        IN NUMBER,
                                P_CALENDAR_ID IN NUMBER,
                                P_OUT        OUT NUMBER) IS
    PV_FROMDATE   DATE;
    PV_ENDDATE    DATE;
    PV_REQUEST_ID NUMBER;
    PV_SQL        CLOB;
    PV_TBL        VARCHAR2(50);
    PV_TBL_ORG    VARCHAR2(50);
    PV_COUNT      NUMBER;
    PV_CHECK      NUMBER;
  BEGIN
    PV_CHECK:=0;
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    PV_FROMDATE   := TO_DATE('01/01/'||P_YEAR,'DD/MM/RRRR');
    PV_ENDDATE    := TO_DATE('31/12/'||P_YEAR,'DD/MM/RRRR');
    
    -- KIEM TRA DANH SACH NHAN VIEN CO TON TAI TRONG BANG CONG GOC HAY KHONG
    BEGIN 
      SELECT COUNT(ID)
      INTO PV_COUNT 
      FROM AT_TIME_TIMESHEET_MACHINET S
      WHERE TRUNC(S.WORKINGDAY) BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE)
      AND INSTR(',' || P_EMPLOYEE_ID || ',', ',' || S.EMPLOYEE_ID || ',') > 0;
    EXCEPTION 
      WHEN NO_DATA_FOUND THEN
      PV_COUNT:=0;
    END;
    
    IF PV_COUNT > 0 THEN
      PV_CHECK := PV_CHECK + 1;
    END IF;
    -- KIEM TRA DANH SACH NHAN VIEN CO DANG KY NGHI HAY KHONG
    BEGIN 
      SELECT COUNT(ID)
      INTO PV_COUNT 
      FROM AT_LEAVESHEET_DETAIL S
      WHERE TRUNC(S.LEAVE_DAY)  BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE)
      AND INSTR(',' || P_EMPLOYEE_ID || ',', ',' || S.EMPLOYEE_ID || ',') > 0;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
      PV_COUNT:=0;
    END;
    IF PV_COUNT > 0 THEN
      PV_CHECK := PV_CHECK + 1;
    END IF;
    -- KIEM TRA DANH SACH NHAN VIEN CO DANG KY LAM THEM HAY KHONG
    BEGIN 
      SELECT COUNT(ID)
      INTO PV_COUNT 
      FROM AT_OT_REGISTRATION S
      WHERE TRUNC(S.REGIST_DATE) BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE)
      AND INSTR(',' || P_EMPLOYEE_ID || ',', ',' || S.EMPLOYEE_ID || ',') > 0;
    EXCEPTION 
      WHEN NO_DATA_FOUND THEN
      PV_COUNT:=0;
    END;
    IF PV_COUNT > 0 THEN
      PV_CHECK := PV_CHECK + 1;
    END IF;
    -- KIEM TRA PV_CHECK NEU LON HON 0 THI KHONG TINH TOAN NUA   
    IF PV_CHECK > 0 THEN
      P_OUT:= 4;
      RETURN;
    END IF;

    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG E
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O
       /*WHERE EXISTS (SELECT 1
        FROM AT_ORG_PERIOD OP
       WHERE OP.PERIOD_ID = P_PERIOD_ID
         AND OP.ORG_ID = O.ORG_ID
         AND OP.STATUSCOLEX = 1)*/
       );
  
    -- insert emp can tinh toan
    INSERT INTO AT_CHOSEN_EMP
      (EMPLOYEE_ID,
       ITIME_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       STAFF_RANK_LEVEL,
       USERNAME,
       REQUEST_ID,
       JOIN_DATE,
       WORK_STATUS)
      (SELECT T.ID,
              T.ITIME_ID,
              W.ORG_ID,
              W.TITLE_ID,
              W.STAFF_RANK_ID,
              W.LEVEL_STAFF,
              UPPER(P_USERNAME),
              PV_REQUEST_ID,
              T.JOIN_DATE,
              T.WORK_STATUS
         FROM HU_EMPLOYEE T
        INNER JOIN (SELECT E.EMPLOYEE_ID,
                          E.TITLE_ID,
                          E.ORG_ID,
                          E.IS_3B,
                          E.STAFF_RANK_ID,
                          S.LEVEL_STAFF,
                          ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                     FROM HU_WORKING E
                     LEFT JOIN HU_STAFF_RANK S
                       ON E.STAFF_RANK_ID = S.ID
                    WHERE E.EFFECT_DATE <= PV_ENDDATE
                      AND E.STATUS_ID = 447
                      AND E.IS_MISSION = -1) W
           ON T.ID = W.EMPLOYEE_ID
          AND W.ROW_NUMBER = 1
        INNER JOIN AT_CHOSEN_ORG O
           ON O.ORG_ID = W.ORG_ID
          AND O.REQUEST_ID = PV_REQUEST_ID
        WHERE INSTR(',' || P_EMPLOYEE_ID || ',', ',' || W.EMPLOYEE_ID || ',') > 0
          AND T.WORK_STATUS IS NOT NULL 
          AND (NVL(T.WORK_STATUS, 0) <> 257 OR
              (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE))
          AND T.ID NOT IN
              (SELECT A.EMPLOYEE_ID
                 FROM AT_WORKSIGN A
                WHERE A.WORKINGDAY BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE)
                  AND NVL(A.IS_IMPORT, 0) = 1
                GROUP BY A.EMPLOYEE_ID));
  -- THEM MOI VAO BANG GAN LICH LAM VIEC CHO NHAN VIEN
  INSERT INTO AT_ASSIGNEMP_CALENDAR
  (ID,
  EMPLOYEE_ID,
  YEAR,
  CALENDAR_ID,
  CREATED_DATE,
  CREATED_BY,
  CREATED_LOG,
  MODIFIED_DATE,
  MODIFIED_BY,
  MODIFIED_LOG)
  (SELECT SEQ_AT_ASSIGNEMP_CALENDAR.NEXTVAL,
          S.EMPLOYEE_ID,
          P_YEAR,
          P_CALENDAR_ID,
          SYSDATE,
          UPPER(P_USERNAME),
          UPPER(P_USERNAME),         
          SYSDATE,
          UPPER(P_USERNAME),
          UPPER(P_USERNAME)          
  FROM AT_CHOSEN_EMP S
  WHERE S.REQUEST_ID = PV_REQUEST_ID);

    --INSERT CA MAC DINH
    MERGE INTO AT_WORKSIGN WS
    USING (SELECT
             EE.EMPLOYEE_ID,
             C.CDATE,
             CASE
               WHEN C.CDATE IN
                    (SELECT H.WORKINGDAY
                       FROM AT_HOLIDAY H
                      WHERE H.YEAR = P_YEAR
                        AND NVL(H.OFFDAY,0) <> -1) THEN
                (SELECT ID FROM AT_SHIFT WHERE CODE = 'L')
               WHEN C.CDATE IN
                    (SELECT H.WORKINGDAY
                       FROM AT_HOLIDAY H
                      WHERE H.YEAR = P_YEAR
                      AND NVL(H.OFFDAY,0) = -1) THEN
                (SELECT ID FROM AT_SHIFT WHERE CODE = 'OFF')
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 6 THEN
                EE.SUNDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 5 THEN
                EE.SATURDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 4 THEN
                EE.FRIDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 3 THEN
                EE.THURSDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 2 THEN
                EE.WEDNESDAY
               WHEN MOD(TO_CHAR(C.CDATE, 'J'), 7) = 1 THEN
                EE.TUEDAY
               ELSE
                EE.MONDAY
             END SHIFT_ID,
             NULL PERIOD_ID,
             SYSDATE CREATED_DATE,
             'AUTO' CREATED_BY, --==BAT BUOC DE AUTO DE PHAN BIET IMPORT
             UPPER(P_USERNAME) CREATED_LOG
        FROM (SELECT EE.EMPLOYEE_ID,
                     CASE
                       WHEN T.FROMDATE_EFFECT > PV_FROMDATE THEN
                        T.FROMDATE_EFFECT
                       ELSE
                        PV_FROMDATE
                     END START_DELETE,
                     CASE
                       WHEN T.TODATE_EFFECT < PV_ENDDATE THEN
                        T.TODATE_EFFECT
                       ELSE
                        PV_ENDDATE
                     END END_DELETE,
                     CASE
                       WHEN TRUNC(EE.JOIN_DATE) > PV_FROMDATE THEN
                        TRUNC(EE.JOIN_DATE)
                       ELSE
                        PV_FROMDATE
                     END GEN_FROM_DATE, 
                     PV_ENDDATE GEN_END_DATE,
                     NVL(T.SIGNID_MON, 0) MONDAY,
                     NVL(T.SIGNID_TUE, 0) TUEDAY,
                     NVL(T.SIGNID_WED, 0) WEDNESDAY,
                     NVL(T.SIGNID_THU, 0) THURSDAY,
                     NVL(T.SIGNID_FRI, 0) FRIDAY,
                     NVL(T.SIGNID_SAT, 0) SATURDAY,
                     NVL(T.SIGNID_SUN, 0) SUNDAY,
                     EE.JOIN_DATE,
                     EE.WORK_STATUS                     
                FROM AT_SIGNDEFAULT_ORG T
               INNER JOIN AT_CHOSEN_EMP EE
                 ON EE.REQUEST_ID = PV_REQUEST_ID
               WHERE T.ACTFLG = 'A'
                 AND T.ID = P_CALENDAR_ID
                 AND INSTR(',' || P_EMPLOYEE_ID || ',', ',' || EE.EMPLOYEE_ID || ',') >0) EE
       CROSS JOIN TABLE(TABLE_LISTDATE(EE.GEN_FROM_DATE, EE.GEN_END_DATE)) C
        LEFT JOIN HU_TERMINATE R
          ON EE.EMPLOYEE_ID = R.EMPLOYEE_ID
       WHERE (NVL(EE.WORK_STATUS,0) = 258 OR 
             ((NVL(EE.WORK_STATUS,0) = 257 OR NVL(EE.WORK_STATUS,0) = 256) 
             AND TRUNC(R.TER_DATE) BETWEEN TRUNC(EE.GEN_FROM_DATE) AND TRUNC(EE.GEN_END_DATE)))
         AND C.CDATE >= TRUNC(EE.GEN_FROM_DATE)
         AND C.CDATE <= TRUNC(EE.GEN_END_DATE)
         AND (C.CDATE <= R.LAST_DATE OR R.LAST_DATE IS NULL)
         AND TRUNC(C.CDATE) >= TRUNC(EE.JOIN_DATE)) H
    ON (WS.WORKINGDAY = H.CDATE AND WS.EMPLOYEE_ID = H.EMPLOYEE_ID)
  WHEN MATCHED THEN
    UPDATE SET WS.SHIFT_ID = H.SHIFT_ID,
               WS.MODIFIED_DATE = SYSDATE,
               WS.MODIFIED_BY = UPPER(P_USERNAME),
               WS.MODIFIED_LOG = UPPER(P_USERNAME)
  WHEN NOT MATCHED THEN
    INSERT
      (ID,
       EMPLOYEE_ID,
       WORKINGDAY,
       SHIFT_ID,
       PERIOD_ID,
       CREATED_DATE,
       CREATED_BY,
       CREATED_LOG)       
    VALUES
      (SEQ_AT_WORKSIGN.NEXTVAL,
       H.EMPLOYEE_ID,
       H.CDATE,
       H.SHIFT_ID,
       NULL,
       H.CREATED_DATE,
       H.CREATED_BY,
       H.CREATED_LOG      
      );
      
     P_OUT := 3;
     
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
      P_OUT := 0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSSINESS.INSERT_GEN_ATWORKSIGN',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              P_USERNAME, 
                              P_ORG_ID,
                              P_ISDISSOLVE,
                              P_EMPLOYEE_ID,
                              P_YEAR,
                              P_CALENDAR_ID,
                              P_OUT);
    
  END INSERT_GEN_ATWORKSIGN;
  
 PROCEDURE DELETE_GEN_ATWORKSIGN(P_ID         IN VARCHAR2,
                                 P_YEAR       IN NUMBER,
                                 P_OUT        OUT NUMBER) IS
  PV_CHECK            NUMBER; 
  PV_COUNT            NUMBER;  
  PV_EMPLOYEE_ID      CLOB;
  PV_FROMDATE         DATE;
  PV_ENDDATE          DATE;                          
  BEGIN
    
    PV_CHECK:=0;
    
    SELECT LISTAGG(S.EMPLOYEE_ID,',') WITHIN GROUP(ORDER BY S.EMPLOYEE_ID)
    INTO PV_EMPLOYEE_ID
    FROM AT_ASSIGNEMP_CALENDAR S
    WHERE INSTR(',' || P_ID || ',', ',' || S.ID || ',') > 0
      AND S.YEAR = P_YEAR;
    
    PV_FROMDATE   := TO_DATE('01/01/'||P_YEAR,'DD/MM/RRRR');
    PV_ENDDATE    := TO_DATE('31/12/'||P_YEAR,'DD/MM/RRRR');
    
    -- KIEM TRA DANH SACH NHAN VIEN CO TON TAI TRONG BANG CONG GOC HAY KHONG
    BEGIN 
      SELECT COUNT(ID)
      INTO PV_COUNT 
      FROM AT_TIME_TIMESHEET_MACHINET S
      WHERE TRUNC(S.WORKINGDAY) BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE)
      AND INSTR(',' || PV_EMPLOYEE_ID || ',', ',' || S.EMPLOYEE_ID || ',') > 0;
    EXCEPTION 
      WHEN NO_DATA_FOUND THEN
      PV_COUNT:=0;
    END;
    
    IF PV_COUNT > 0 THEN
      PV_CHECK := PV_CHECK + 1;
    END IF;
    -- KIEM TRA DANH SACH NHAN VIEN CO DANG KY NGHI HAY KHONG
    BEGIN 
      SELECT COUNT(ID)
      INTO PV_COUNT 
      FROM AT_LEAVESHEET_DETAIL S
      WHERE TRUNC(S.LEAVE_DAY)  BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE)
      AND INSTR(',' || PV_EMPLOYEE_ID || ',', ',' || S.EMPLOYEE_ID || ',') > 0;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
      PV_COUNT:=0;
    END;
    IF PV_COUNT > 0 THEN
      PV_CHECK := PV_CHECK + 1;
    END IF;
    -- KIEM TRA DANH SACH NHAN VIEN CO DANG KY LAM THEM HAY KHONG
    BEGIN 
      SELECT COUNT(ID)
      INTO PV_COUNT 
      FROM AT_OT_REGISTRATION S
      WHERE TRUNC(S.REGIST_DATE) BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE)
      AND INSTR(',' || PV_EMPLOYEE_ID || ',', ',' || S.EMPLOYEE_ID || ',') > 0;
    EXCEPTION 
      WHEN NO_DATA_FOUND THEN
      PV_COUNT:=0;
    END;
    IF PV_COUNT > 0 THEN
      PV_CHECK := PV_CHECK + 1;
    END IF;
    -- KIEM TRA PV_CHECK NEU LON HON 0 THI KHONG TINH TOAN NUA   
    IF PV_CHECK > 0 THEN
      P_OUT:= 4;
      RETURN;
    END IF;
       
    DELETE AT_ASSIGNEMP_CALENDAR S
    WHERE INSTR(',' || P_ID || ',', ',' || S.ID || ',') > 0
      AND S.YEAR = P_YEAR;
         
    DELETE AT_WORKSIGN S
    WHERE INSTR(',' || PV_EMPLOYEE_ID || ',', ',' || S.EMPLOYEE_ID || ',') > 0
    AND TRUNC(S.WORKINGDAY) BETWEEN TRUNC(PV_FROMDATE) AND TRUNC(PV_ENDDATE);
    
    P_OUT:=3;
    COMMIT;
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
      P_OUT:=0;
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.DELETE_GEN_ATWORKSIGN',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                              P_ID);                                
  END DELETE_GEN_ATWORKSIGN;   
   
   PROCEDURE GET_SWIPE_DATA_IMPORT(P_USERNAME    IN VARCHAR2,
                                   P_ORG_ID      IN NUMBER,
                                   P_IS_DISSOLVE IN NUMBER,
                                   P_OUT         OUT CURSOR_TYPE,
                                   P_OUT2        OUT CURSOR_TYPE) IS
   BEGIN
     OPEN P_OUT FOR
       SELECT OT.ID, OT.NAME_VN
         FROM HU_ORGANIZATION OT
        INNER JOIN TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID, P_IS_DISSOLVE)) CHOSEN
           ON OT.ID = CHOSEN.ORG_ID
        WHERE OT.ACTFLG = 'A';
     OPEN P_OUT2 FOR
       SELECT AT.TERMINAL_NAME NAME, AT.TERMINAL_CODE CODE, AT.ID
         FROM AT_TERMINALS AT
        INNER JOIN TABLE(TABLE_ORG_RIGHT(P_USERNAME, P_ORG_ID, P_IS_DISSOLVE)) CHOSEN
           ON AT.ORG_ID = CHOSEN.ORG_ID
        WHERE AT.ACTFLG = 'A';
   EXCEPTION
     WHEN OTHERS THEN
       SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                               'PKG_ATTENDANCE_BUSINESS.GET_SWIPE_DATA_IMPORT',
                               SQLERRM || '_' ||
                               DBMS_UTILITY.FORMAT_ERROR_BACKTRACE,
                               P_USERNAME,
                               P_ORG_ID,
                               P_IS_DISSOLVE);
   END GET_SWIPE_DATA_IMPORT;
   
  
  PROCEDURE CAL_TIME_TIMESHEET_ALL_AUTO IS
    
  
    P_USERNAME   NVARCHAR2(255);
    P_ORG_ID     NUMBER;
    P_PERIOD_ID  NUMBER;
    P_ISDISSOLVE NUMBER;
    P_FROM_DATE  nvarchar2(255);
    P_TO_DATE    nvarchar2(255);
    P_EMPLIST    CLOB;
    P_REQUEST_ID NUMBER;
  
    PV_FROMDATE    DATE;
    PV_ENDDATE     DATE;
    PV_SQL         CLOB;
    PV_REQUEST_ID  NUMBER;
    PV_MINUS_ALLOW NUMBER := 50;
    PV_SUNDAY      DATE; 
    PV_TEST1       NUMBER;
    PV_TEST2       NUMBER;
    PV_CHECK       NUMBER;
    PV_CHECKNV     NUMBER;
    PV_TG_BD_CA    DATE;
    PV_TG_KT_CA    DATE;
    PV_TG_BD_NGHI  DATE;
    PV_TG_KT_NGHI  DATE;
    PV_IN_MIN      DATE;
    PV_OUT_MAX     DATE;
    PV_TRAN_INDX   NUMBER;
    PV_SQL_CRE_TB  CLOB;
    PV_TBL_NAME    NVARCHAR2(50);
  BEGIN
    
    P_USERNAME := 'ADMIN';
    P_ORG_ID := 1;
    P_ISDISSOLVE := 0;
  
 
    PV_REQUEST_ID := SEQ_AT_REQUEST.NEXTVAL;
    PV_TRAN_INDX  := ABS(REMAINDER(PV_REQUEST_ID, 15)); 
    PV_FROMDATE := TRUNC(SYSDATE);
    PV_ENDDATE  := TRUNC(SYSDATE);
   
    -- Insert org can tinh toan
    INSERT INTO AT_CHOSEN_ORG_TMP E --temp
      (SELECT ORG_ID, UPPER(P_USERNAME), PV_REQUEST_ID
         FROM TABLE(TABLE_ORG_RIGHT(UPPER(P_USERNAME),
                                    P_ORG_ID,
                                    P_ISDISSOLVE)) O);
    COMMIT;
  
    INSERT INTO AT_CHOSEN_EMP_CLEAR_TMP -- temp
      (EMPLOYEE_ID, REQUEST_ID)
      SELECT DISTINCT S.EMPLOYEE_ID, PV_REQUEST_ID
        FROM AT_TIME_TIMESHEET_MACHINET S
       INNER JOIN (SELECT ORG_ID
                     FROM AT_CHOSEN_ORG_TMP O
                    WHERE O.REQUEST_ID = PV_REQUEST_ID) O
          ON O.ORG_ID = S.ORG_ID
       WHERE (S.CREATED_BY = 'AUTO')
         AND S.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE;
    COMMIT;
    
    --==TINH LAI TAT CA CA NV
            -- insert emp can tinh toan
            INSERT INTO AT_CHOSEN_EMP_TMP --temp
              (EMPLOYEE_ID,
               ITIME_ID,
               ORG_ID,
               TITLE_ID,
               STAFF_RANK_ID,
               STAFF_RANK_LEVEL,
               TER_EFFECT_DATE,
               USERNAME,
               REQUEST_ID,
               JOIN_DATE,
               JOIN_DATE_STATE,
               DECISION_ID,
               OBJECT_ATTENDANCE,
               OBJECT_LABOR)
              (SELECT T.ID,
                      T.ITIME_ID,
                      W.ORG_ID,
                      W.TITLE_ID,
                      W.STAFF_RANK_ID,
                      W.LEVEL_STAFF,
                      CASE
                        WHEN T.TER_EFFECT_DATE IS NOT NULL THEN
                         T.TER_EFFECT_DATE + 1
                        ELSE
                         NULL
                      END TER_EFFECT_DATE,
                      UPPER(P_USERNAME),
                      PV_REQUEST_ID,
                      T.JOIN_DATE,
                      T.JOIN_DATE_STATE,
                      W.ID DECISION_ID,
                      W.OBJECT_ATTENDANCE,
                      W.OBJECT_EMPLOYEE_ID
                 FROM HU_EMPLOYEE T
                INNER JOIN (SELECT E.ID,
                                  E.EMPLOYEE_ID,
                                  E.TITLE_ID,
                                  E.ORG_ID,
                                  E.IS_3B,
                                  E.STAFF_RANK_ID,
                                  S.LEVEL_STAFF,
                                  E.OBJECT_ATTENDANCE,
                                  E.OBJECT_EMPLOYEE_ID,
                                  ROW_NUMBER() OVER(PARTITION BY E.EMPLOYEE_ID ORDER BY E.EFFECT_DATE DESC, E.CREATED_DATE DESC) AS ROW_NUMBER
                             FROM HU_WORKING E
                             LEFT JOIN HU_STAFF_RANK S
                               ON E.STAFF_RANK_ID = S.ID
                            WHERE E.EFFECT_DATE <= PV_ENDDATE
                              AND E.STATUS_ID = 447
                              AND E.IS_WAGE = 0
                              AND E.IS_3B = 0) W
                   ON T.ID = W.EMPLOYEE_ID
                  AND W.ROW_NUMBER = 1
                INNER JOIN (SELECT ORG_ID
                             FROM AT_CHOSEN_ORG_TMP O
                            WHERE O.REQUEST_ID = PV_REQUEST_ID) O
                   ON O.ORG_ID = W.ORG_ID
                WHERE (NVL(T.WORK_STATUS, 0) <> 257 OR
                      (T.WORK_STATUS = 257 AND T.TER_LAST_DATE >= PV_FROMDATE)));
    COMMIT;
  
    INSERT INTO AT_SWIPE_DATA_TMP
      SELECT SW.*, PV_REQUEST_ID
        FROM AT_SWIPE_DATA SW
       INNER JOIN AT_CHOSEN_EMP_TMP E
          ON E.EMPLOYEE_ID = SW.EMPLOYEE_ID
         AND E.REQUEST_ID = PV_REQUEST_ID
       WHERE SW.WORKINGDAY BETWEEN PV_FROMDATE -1 AND PV_ENDDATE + 1;
    COMMIT;
    INSERT INTO AT_WORKSIGN_TMP
      SELECT WS.*,
             PV_REQUEST_ID
        FROM AT_WORKSIGN WS
       INNER JOIN AT_CHOSEN_EMP_TMP E
          ON E.EMPLOYEE_ID = WS.EMPLOYEE_ID
         AND E.REQUEST_ID = PV_REQUEST_ID
       INNER JOIN HU_EMPLOYEE EMP
          ON EMP.ID = WS.EMPLOYEE_ID
       WHERE WS.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
         AND (EMP.TER_LAST_DATE IS NULL OR
             (EMP.TER_LAST_DATE IS NOT NULL AND EMP.TER_LAST_DATE >= WS.WORKINGDAY));
    COMMIT;
    INSERT INTO AT_LEAVESHEET_DETAIL_TMP
      SELECT D.ID,
             D.LEAVESHEET_ID,
             D.EMPLOYEE_ID,
             D.LEAVE_DAY,
             D.MANUAL_ID,
             D.DAY_NUM,
             D.STATUS_SHIFT,
             D.SHIFT_ID,
             D.CREATED_DATE,
             D.CREATED_BY,
             D.CREATED_LOG,
             D.MODIFIED_DATE,
             D.MODIFIED_BY,
             D.MODIFIED_LOG,
             PV_REQUEST_ID,
             D.OLD_LEAVE,
             D.REASON_LEAVE
        FROM AT_LEAVESHEET_DETAIL D
        JOIN AT_LEAVESHEET L
          ON L.ID = D.LEAVESHEET_ID       
         INNER JOIN AT_CHOSEN_EMP_TMP E
          ON E.EMPLOYEE_ID = L.EMPLOYEE_ID
         AND E.REQUEST_ID = PV_REQUEST_ID 
       WHERE D.LEAVE_DAY BETWEEN PV_FROMDATE AND PV_ENDDATE
         AND L.STATUS = 1;
    COMMIT;
    INSERT INTO AT_TIME_TIMESHEET_MACHINE_TMP M --temp
      (ID,
       OBJ_EMP_ID,
       PERIOD_ID,
       EMPLOYEE_ID,
       ORG_ID,
       TITLE_ID,
       STAFF_RANK_ID,
       WORKINGDAY,
       SHIFT_ID,
       SHIFT_CODE,
       CREATED_DATE,
       --CREATED_BY,
       REQUEST_ID,
       VALIN1,
       VALOUT1,
       TIMEVALIN,
       TIMEVALOUT,
       TIMEIN_REALITY, --GIO VAO TT
       TIMEOUT_REALITY, -- GIO RA THUC TE
       OBJECT_ATTENDANCE,
       OBJECT_ATTENDANCE_CODE,
       WORK_HOUR,
       HOURS_STOP,
       HOURS_START,
       START_MID_HOURS,
       END_MID_HOURS,
       LATE_HOUR,
       EARLY_HOUR,
       SHIFT_DAY,
       HOURS_STAR_CHECKIN,
       HOURS_STAR_CHECKOUT,
       NOTE,
       --MIN_ON_LEAVE,
       MANUAL_ID,
       STATUS_SHIFT,
       MANUAL_CODE,
       MORNING_RATE,
       AFTERNOON_RATE,
       DAY_NUM,
       STATUS_SHIFT_NAME,
       CREATED_BY,
       ORG_ACCOUNTING,
       SHIFT_HOURS_START,
       SHIFT_HOURS_STOP,
       TOMORROW_SHIFT   
       )
      SELECT SEQ_AT_TIME_TIMESHEET_MACHINET.NEXTVAL,
             T.OBJECT_LABOR,
             P_PERIOD_ID,
             T.EMPLOYEE_ID,
             T.ORG_ID,
             T.TITLE_ID,
             T.STAFF_RANK_ID,
             WS.WORKINGDAY,
             WS.SHIFT_ID,
             SH.CODE SHIFT_CODE,
             SYSDATE,
             --P_USERNAME,
             PV_REQUEST_ID,
             CASE
               WHEN SH.CODE IN( 'OFF','L') THEN
                DATA_IN.VALTIME
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       0,
                                       PV_REQUEST_ID)
             END,
             
             CASE
               WHEN SH.CODE IN( 'OFF','L') THEN
                DATA_OUT.VALTIME
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       1,
                                       PV_REQUEST_ID)
             END,
             --gio vao goc
             CASE
               WHEN SH.CODE IN( 'OFF','L') THEN
                --DATA_IN.VALTIME
                NULL
               ELSE
                PA_FUNC.FN_GET_VALTIME_1(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       0,
                                       PV_REQUEST_ID)
             END,
             --gio ra goc
             CASE
               WHEN SH.CODE IN('OFF','L') THEN
                --DATA_OUT.VALTIME  
                NULL
               ELSE
                PA_FUNC.FN_GET_VALTIME_1(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       1,
                                       PV_REQUEST_ID)
             END,
              --gio vao thuc te
             CASE
               WHEN SH.CODE IN( 'OFF','L') THEN
                --DATA_IN.VALTIME 
                NULL
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       0,
                                       PV_REQUEST_ID)
             END,
              --gio ra thuc te
             CASE
               WHEN SH.CODE IN( 'OFF','L') THEN
                --DATA_OUT.VALTIME 
                NULL
               ELSE
                PA_FUNC.FN_GET_VALTIME(T.EMPLOYEE_ID,
                                       WS.WORKINGDAY,
                                       WS.SHIFT_ID,
                                       T.OBJECT_ATTENDANCE,
                                       CASE
                                         WHEN LEAVE.EMPLOYEE_ID IS NOT NULL THEN
                                          NVL(LEAVE.STATUS_SHIFT, 0)
                                         ELSE
                                          3
                                       END,
                                       1,
                                       PV_REQUEST_ID)
             END,
             
             T.OBJECT_ATTENDANCE,
             OL.CODE OBJECT_ATTENDANCE_CODE,
             --SH.WORK_HOUR,
             SH.SHIFT_HOUR,
             CASE WHEN SH.CODE='L' OR SH.CODE='OFF' THEN
                  NULL
             ELSE
                  TO_DATE(TO_CHAR(CASE WHEN NVL(SH.IS_HOURS_STOP,0) <> 0 THEN WS.WORKINGDAY+1 ELSE WS.WORKINGDAY END, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STOP, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI')
             END HOURS_STOP, -- GIO KET THUC CA
             CASE WHEN SH.CODE='L' OR SH.CODE='OFF' THEN
                  NULL
             ELSE
                  TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_START, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI')
             END HOURS_START, -- GIO BAT DAU CA
             CASE WHEN SH.CODE='L' OR SH.CODE='OFF' THEN
                  NULL
             ELSE
                  TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.START_MID_HOURS, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI')
             END,
             CASE WHEN SH.CODE='L' OR SH.CODE='OFF' THEN
                  NULL
             ELSE
                  TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.END_MID_HOURS, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI')
             END,
             CASE
               WHEN SH.LATE_HOUR IS NOT NULL THEN
                TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                        TO_CHAR(SH.LATE_HOUR, 'HH24:MI'),
                        'DD/MM/RRRR HH24:MI')
               ELSE
                NULL
             END,
             CASE
               WHEN SH.EARLY_HOUR IS NOT NULL THEN
                TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                        TO_CHAR(SH.EARLY_HOUR, 'HH24:MI'),
                        'DD/MM/RRRR HH24:MI')
               ELSE
                NULL
             END,
             SH.SHIFT_DAY,
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STAR_CHECKIN, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             TO_DATE(TO_CHAR(WS.WORKINGDAY, 'DD/MM/RRRR ') ||
                     TO_CHAR(SH.HOURS_STAR_CHECKOUT, 'HH24:MI'),
                     'DD/MM/RRRR HH24:MI'),
             I.NOTE,
             --LEAVE.DAY_NUM,
             CASE
               WHEN SH.CODE = 'OFF' OR SH.CODE = 'L' THEN
                NULL
               ELSE
                LEAVE.MANUAL_ID
             END,
             /*LEAVE.MANUAL_ID,*/
             LEAVE.STATUS_SHIFT,
             CASE
               WHEN SH.CODE = 'OFF' OR SH.CODE = 'L' THEN
                NULL
               ELSE
                FM.CODE
             END,
             /*FM.CODE,*/
             MR.VALUE_RATE,
             AF.VALUE_RATE,
             LEAVE.DAY_NUM,
             CASE
               WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 0 THEN
                TO_CHAR(UNISTR(''))
               WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 1 THEN
                TO_CHAR(UNISTR('\0110\00E2\0300u ca'))
               WHEN NVL(LEAVE.STATUS_SHIFT, 0) = 2 THEN
                TO_CHAR(UNISTR('cu\00F4\0301i ca'))
             END,
             CASE
               WHEN WS.EMPLOYEE_ID || TO_CHAR(WS.WORKINGDAY, 'DDMMYYYY') IN
                    (SELECT T.EMPLOYEE_ID ||
                            TO_CHAR(T.WORKING_DAY, 'DDMMYYYY')
                       FROM AT_TIMESHEET_MACHINET_IMPORT T
                      WHERE T.EMPLOYEE_ID = WS.EMPLOYEE_ID
                        AND T.WORKING_DAY = WS.WORKINGDAY) THEN
                P_USERNAME
               ELSE
                CAST('AUTO' AS NVARCHAR2(20))
             END,
             PKG_FUNCTION.ORG_WORKING_MAX(T.EMPLOYEE_ID, WS.WORKINGDAY),
              PKG_FUNCTION.GET_TIME_OT_COEFF_OVER(WS.WORKINGDAY,'START') SHIFT_HOURS_START, -- GIO BAT DAU CUA KHUNG GIO LAM DEM
              PKG_FUNCTION.GET_TIME_OT_COEFF_OVER(WS.WORKINGDAY,'STOP') SHIFT_HOURS_STOP, -- GIO KET THUC CUA KHUNG GIO LAM DEM   
             CASE
               WHEN SH.IS_HOURS_STOP = -1 THEN 
                 -1
               ELSE
                 0
              END         
        FROM AT_WORKSIGN_TMP WS
       INNER JOIN AT_CHOSEN_EMP_TMP T
          ON T.EMPLOYEE_ID = WS.EMPLOYEE_ID
         AND T.REQUEST_ID = PV_REQUEST_ID
        LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                          SW.WORKING_DAY,
                          SW.TIMEIN_REALITY,
                          SW.TIMEOUT_REALITY,
                          SW.NOTE,
                          ROW_NUMBER() OVER(PARTITION BY SW.EMPLOYEE_ID, SW.WORKING_DAY ORDER BY SW.TIMEIN_REALITY, SW.TIMEOUT_REALITY) AS ROW_NUMBER
                     FROM AT_TIMESHEET_MACHINET_IMPORT SW
                    WHERE SW.WORKING_DAY BETWEEN PV_FROMDATE AND PV_ENDDATE
                   
                   ) I
          ON I.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND I.WORKING_DAY = WS.WORKINGDAY
         AND I.ROW_NUMBER = 1
        LEFT JOIN AT_SHIFT SH
          ON WS.SHIFT_ID = SH.ID
        LEFT JOIN AT_LEAVESHEET_DETAIL_TMP LEAVE
          ON WS.EMPLOYEE_ID = LEAVE.EMPLOYEE_ID
         AND LEAVE.REQUEST_ID = PV_REQUEST_ID
         AND TRUNC(WS.WORKINGDAY) = TRUNC(LEAVE.LEAVE_DAY)
        LEFT JOIN AT_TIME_MANUAL FM
          ON FM.ID = LEAVE.MANUAL_ID
        LEFT JOIN AT_TIME_MANUAL_RATE MR
          ON MR.ID = FM.MORNING_RATE_ID
        LEFT JOIN AT_TIME_MANUAL_RATE AF
          ON AF.ID = FM.AFTERNOON_RATE_ID
        LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                          SW.WORKINGDAY,
                          MIN(SW.VALTIME) AS VALTIME
                     FROM AT_SWIPE_DATA_TMP SW
                    WHERE SW.REQUEST_ID = PV_REQUEST_ID
                    GROUP BY SW.EMPLOYEE_ID, SW.WORKINGDAY) DATA_IN
          ON DATA_IN.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND DATA_IN.WORKINGDAY = WS.WORKINGDAY
        LEFT JOIN (SELECT SW.EMPLOYEE_ID,
                          SW.WORKINGDAY,
                          MAX(SW.VALTIME) AS VALTIME
                     FROM AT_SWIPE_DATA_TMP SW
                    WHERE SW.REQUEST_ID = PV_REQUEST_ID
                    GROUP BY SW.EMPLOYEE_ID, SW.WORKINGDAY) DATA_OUT
          ON DATA_OUT.EMPLOYEE_ID = T.EMPLOYEE_ID
         AND DATA_OUT.WORKINGDAY = WS.WORKINGDAY
        LEFT JOIN OT_OTHER_LIST OL
          ON T.OBJECT_ATTENDANCE = OL.ID
       WHERE WS.REQUEST_ID = PV_REQUEST_ID;
  
    --PHAN CHIA TRAN DE TINH TOAN
    PV_TBL_NAME   := TRIM('AT_TIME_TIMESHEET_MACHINE_TEMP' ||
                          TRIM(TO_CHAR(PV_REQUEST_ID)));                      
                          
    PV_SQL_CRE_TB := '
        CREATE TABLE ' || PV_TBL_NAME || '
        AS (SELECT * FROM AT_TIME_TIMESHEET_MACHINE_TMP WHERE 0=1)
    ';
    --INSERT INTO TEMP(TEXT)VALUES(PV_SQL_CRE_TB);
    EXECUTE IMMEDIATE PV_SQL_CRE_TB;
     
    PV_SQL_CRE_TB := '
       INSERT INTO ' || PV_TBL_NAME || '
       SELECT * FROM AT_TIME_TIMESHEET_MACHINE_TMP WHERE REQUEST_ID=' ||
                     PV_REQUEST_ID || '
    ';
    EXECUTE IMMEDIATE PV_SQL_CRE_TB;
    COMMIT;      
    
    --================================================================================
  
    --== ap dung cong thuc
    FOR CUR_ITEM IN (SELECT *
                       FROM AT_TIME_FORMULAR T
                      WHERE T.TYPE IN (5)
                        AND T.STATUS = 1
                      ORDER BY T.ORDERBY) LOOP
      PV_SQL := 'UPDATE ' || PV_TBL_NAME || '  T SET ' ||
                CUR_ITEM.FORMULAR_CODE || ' = CASE WHEN NVL((' || CUR_ITEM.FORMULAR_VALUE || '),0) <= 0 THEN 
                                                     0 
                                                 ELSE NVL((' || CUR_ITEM.FORMULAR_VALUE || '),0) 
                                              END   
                 WHERE  REQUEST_ID = ' || PV_REQUEST_ID || ' ';
       INSERT INTO TEMP1
        (TMP, WCODE)
      VALUES
        (PV_SQL, 'CUR_ITEM.FORMULAR_CODE1');    
      BEGIN
        EXECUTE IMMEDIATE PV_SQL;
        COMMIT;
      EXCEPTION
        WHEN OTHERS THEN
          INSERT INTO TEMP1
            (TMP, WCODE)
          VALUES
            (PV_SQL, 'CUR_ITEM.FORMULAR_CODE');
          COMMIT;
          CONTINUE;
      END;
    END LOOP;
    -- XOA DU LIEU CU TRUOC KHI TINH
    DELETE FROM AT_TIME_TIMESHEET_MACHINET D
     WHERE D.WORKINGDAY BETWEEN PV_FROMDATE AND PV_ENDDATE
       AND D.Employee_Id IN (SELECT P. EMPLOYEE_ID
                          FROM AT_CHOSEN_EMP_TMP P
                         WHERE P.REQUEST_ID = PV_REQUEST_ID);
    COMMIT;
    --==Insert thong tin da tin vao table that
    DELETE FROM AT_TIME_TIMESHEET_MACHINET T
     WHERE T.REQUEST_ID = PV_REQUEST_ID;

    PV_SQL := '
        INSERT INTO AT_TIME_TIMESHEET_MACHINET
        SELECT  p.* FROM ' || PV_TBL_NAME ||
              ' P WHERE REQUEST_ID=' || PV_REQUEST_ID || '
    ';
    --INSERT INTO TEMP(TEXT)VALUES(PV_SQL_CRE_TB);
    --INSERT INTO SQL_TEST(SQL_TEXT,CREATE_DATE) VALUES(PV_SQL,SYSDATE);
    EXECUTE IMMEDIATE PV_SQL;
    COMMIT;
    UPDATE AT_TIME_TIMESHEET_MACHINET T
    SET T.MIN_NIGHT = (CASE WHEN NVL(T.MIN_NIGHT,0) <> 0 THEN
                           ROUND(T.MIN_NIGHT,0)||'.'||ROUND(((T.MIN_NIGHT) - ROUND((T.MIN_NIGHT),0))*60,0)
                       END)
    WHERE T.REQUEST_ID = PV_REQUEST_ID;
    
    PV_SQL_CRE_TB := '
            DROP TABLE ' || PV_TBL_NAME || '
    ';
    --INSERT INTO TEMP(TEXT)VALUES(PV_SQL_CRE_TB);
    COMMIT;
    EXECUTE IMMEDIATE PV_SQL_CRE_TB;
  
    DELETE FROM AT_CHOSEN_ORG_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_CHOSEN_EMP_CLEAR_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_CHOSEN_EMP_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_SWIPE_DATA_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_WORKSIGN_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_TIME_TIMESHEET_MACHINE_TMP
     WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_LEAVESHEET_DETAIL_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
    DELETE FROM AT_CHOSEN_EMP_CLEAR_TMP WHERE REQUEST_ID = PV_REQUEST_ID;           
  EXCEPTION
    WHEN OTHERS THEN
    
      SYS_WRITE_EXCEPTION_LOG(SQLCODE,
                              'PKG_ATTENDANCE_BUSINESS.CAL_TIME_TIMESHEET_ALL_AUTO',
                              SQLERRM || '_' ||
                              DBMS_UTILITY.format_error_backtrace,
                              P_ORG_ID,
                              P_PERIOD_ID,
                              P_USERNAME,
                              PV_REQUEST_ID,
                              PV_TBL_NAME,
                              PV_SQL);
                              
      DELETE FROM AT_CHOSEN_ORG_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_CHOSEN_EMP_CLEAR_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_CHOSEN_EMP_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_SWIPE_DATA_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_WORKSIGN_TMP WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_TIME_TIMESHEET_MACHINE_TMP
       WHERE REQUEST_ID = PV_REQUEST_ID;
      DELETE FROM AT_LEAVESHEET_DETAIL_TMP
       WHERE REQUEST_ID = PV_REQUEST_ID;                        
                              
      PV_TBL_NAME := TRIM('AT_TIME_TIMESHEET_MACHINE_TEMP' ||
                          TRIM(TO_CHAR(PV_REQUEST_ID)));
      
       PV_SQL_CRE_TB := '
            DROP TABLE ' || PV_TBL_NAME || '
       ';
        EXECUTE IMMEDIATE PV_SQL_CRE_TB;                    

      COMMIT;
  END;
 
                             
END PKG_ATTENDANCE_BUSINESS;
