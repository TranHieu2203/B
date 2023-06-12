CREATE OR REPLACE PACKAGE PKG_RECRUITMENT_EXPORT IS
  TYPE CURSOR_TYPE IS REF CURSOR;
  PROCEDURE GET_TEMPLATE_MAIL(P_CODE IN NVARCHAR2,
                              P_TYPE IN NVARCHAR2,
                              P_OUT  OUT CURSOR_TYPE);
  PROCEDURE GET_MAILCC_DIRECT_HR(P_EMP_ID IN NUMBER,
                                 P_USER_ID IN NUMBER,
                                 P_OUT OUT NVARCHAR2);
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
                                 P_CUR12 OUT CURSOR_TYPE);
  PROCEDURE GET_INS_HEATH_IMPORT(P_CUR1 OUT CURSOR_TYPE,
                                 P_CUR2 OUT CURSOR_TYPE,
                                 P_CUR3 OUT CURSOR_TYPE);
  PROCEDURE GET_OFFERLETTER_IMPORT(P_USERNAME IN NVARCHAR2,
                                   P_CUR      OUT CURSOR_TYPE,
                                   P_CUR1     OUT CURSOR_TYPE,
                                   P_CUR2     OUT CURSOR_TYPE,
                                   P_CUR3     OUT CURSOR_TYPE,
                                   P_CUR4     OUT CURSOR_TYPE,
                                   P_CUR5     OUT CURSOR_TYPE,
                                   P_CUR6     OUT CURSOR_TYPE,
                                   P_CUR7     OUT CURSOR_TYPE);
end PKG_RECRUITMENT_EXPORT;
