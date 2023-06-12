CREATE OR REPLACE PACKAGE "PKG_ATTENDANCE_BUSINESS" IS

  TYPE CURSOR_TYPE IS REF CURSOR;
  /*PROCEDURE GET_WORKING_TYPE_BY_DATE(P_EMPLOYEE_ID IN NUMBER,
  P_DATE        IN DATE,
  P_OUT         OUT NUMBER);*/
  PROCEDURE GET_TOTAL_ACCUMULATIVE_OT(P_EMPLOYEE_ID IN NUMBER,
                                      P_DATE        IN DATE,
                                      P_OUT         OUT NUMBER);
  PROCEDURE DELETE_LOG_AT(P_ID IN NUMBER);
  PROCEDURE CALCULATOR_DAY(P_FROM_DATE   IN DATE,
                           P_TO_DATE     IN DATE,
                           P_EMPLOYEE_ID IN NUMBER,
                           P_TYPE_MANUAL IN NUMBER,
                           CUR           OUT CURSOR_TYPE);
  PROCEDURE CAL_DAY_LEAVE_OLD(P_FROM_DATE   IN DATE,
                              P_TO_DATE     IN DATE,
                              P_EMPLOYEE_ID IN NUMBER,
                              CUR           OUT CURSOR_TYPE);
  PROCEDURE GET_TOTAL_PHEPNAM(P_EMPLOYEE_ID   IN NUMBER,
                              P_YEAR          IN NUMBER,
                              P_TYPE_LEAVE_ID IN NUMBER,
                              P_CUR           OUT CURSOR_TYPE);
  PROCEDURE GET_TOTAL_PHEPBU(P_EMPLOYEE_ID   IN NUMBER,
                             P_YEAR          IN NUMBER,
                             P_TYPE_LEAVE_ID IN NUMBER,
                             P_CUR           OUT CURSOR_TYPE);
  PROCEDURE CALL_ENTITLEMENT_NB(P_USERNAME   IN NVARCHAR2,
                                P_ORG_ID     IN NUMBER,
                                P_PERIOD_ID  IN NUMBER,
                                P_ISDISSOLVE IN NUMBER);
  PROCEDURE IMPORT_AT_SWIPE_DATA(P_USER IN NVARCHAR2,
                                 P_DATA IN CLOB,
                                 P_CUR  OUT CURSOR_TYPE);

  /*PROCEDURE CALL_ENTITLEMENT(P_USERNAME   IN VARCHAR2,
  P_ORG_ID     IN NUMBER,
  P_PERIOD_ID  IN NUMBER,
  P_ISDISSOLVE IN NUMBER);*/

  PROCEDURE UPDATE_DATAINOUT(P_ITIMEID  IN NUMBER,
                             P_USERNAME IN NVARCHAR2,
                             P_FROMDATE IN DATE,
                             P_ENDDATE  IN DATE);
  PROCEDURE MANAGEMENT_TOTAL_COMPENSATORY(P_EMPLOYEE_ID IN NUMBER,
                                          
                                          P_DATE_TIME IN DATE,
                                          P_OUT       OUT CURSOR_TYPE);
  PROCEDURE MANAGEMENT_TOTAL_ENTITLEMENT(P_EMPLOYEE_ID   IN NUMBER,
                                         P_ID_PORTAL_REG IN NUMBER,
                                         P_DATE_TIME     IN DATE,
                                         P_OUT           OUT CURSOR_TYPE);
  PROCEDURE DELETE_WORKSIGN(P_EMPLOYEE_ID IN NVARCHAR2,
                            P_FROM        IN DATE,
                            P_TO          IN DATE);

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
                         P_CURCOUNR      OUT CURSOR_TYPE);
  PROCEDURE CAL_TIME_TIMESHEET_IN(P_USERNAME   IN NVARCHAR2,
                                  P_ORG_ID     IN NUMBER,
                                  P_PERIOD_ID  IN NUMBER,
                                  P_ISDISSOLVE IN NUMBER,
                                  P_DELETE_ALL IN NUMBER := 1,
                                  P_OBJ_EMP_ID IN NUMBER,
                                  P_FROM_DATE  IN nvarchar2,
                                  P_TO_DATE    IN nvarchar2,
                                  P_REQUEST_ID IN NUMBER,
                                  P_OUT        OUT NUMBER);

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
                                       P_OUT        OUT NUMBER);
  /*PROCEDURE CAL_TIME_TIMESHEET_ALL(P_USERNAME   IN NVARCHAR2,
                                   P_ORG_ID     IN NUMBER,
                                   P_PERIOD_ID  IN NUMBER,
                                   P_ISDISSOLVE IN NUMBER,
                                   P_DELETE_ALL IN NUMBER := 1);
  PROCEDURE CAL_TIME_TIMESHEET_HOSE(P_USERNAME   IN NVARCHAR2,
                                    P_ORG_ID     IN NUMBER,
                                    P_PERIOD_ID  IN NUMBER,
                                    P_ISDISSOLVE IN NUMBER,
                                    P_DELETE_ALL IN NUMBER := 1);*/
  /*PROCEDURE CAL_TIMESHEET_DAILY(P_USERNAME   IN NVARCHAR2,
                                P_ORG_ID     IN NUMBER,
                                P_PERIOD_ID  IN NUMBER,
                                P_ISDISSOLVE IN NUMBER,
                                P_DELETE_ALL IN NUMBER := 1,
                                P_REQUEST_ID IN NUMBER,
                                P_OUT        OUT NUMBER);*/

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
                    P_TERMINATE              IN NUMBER,
                    P_EMP_OBJ                in number,
                    P_START_DATE             in date,
                    P_END_DATE               in date,
                    P_CUR                    OUT CURSOR_TYPE,
                    P_CURCOUNR               OUT CURSOR_TYPE);
  PROCEDURE UPDATE_TIMESHEET_CTT(P_FROMDATE  IN DATE,
                                 P_TODATE    IN DATE,
                                 P_EMP_ID    IN NUMBER,
                                 P_MANUAL_ID IN NUMBER,
                                 P_PERIOD_ID IN NUMBER,
                                 P_USERNAME  IN VARCHAR2);

  PROCEDURE INSERT_CHOSEN_LOGORG(P_USERNAME   IN NVARCHAR2,
                                 P_ORGID      IN NUMBER,
                                 P_ISDISSOLVE IN NUMBER,
                                 P_ACTION_ID  IN NUMBER);

  PROCEDURE CAL_TIME_TIMESHEET_MONTHLY(P_USERNAME   IN VARCHAR2,
                                       P_PERIOD_ID  IN NUMBER,
                                       P_ORG_ID     IN NUMBER,
                                       P_ISDISSOLVE IN NUMBER,
                                       P_FROM_DATE  IN DATE,
                                       P_TO_DATE    IN DATE,
                                       P_OBJ_EMPLOYEE_ID IN NUMBER);

  PROCEDURE CAL_TIMESHEET_MONTHLY_HOSE(P_USERNAME   VARCHAR2,
                                       P_PERIOD_ID  IN NUMBER,
                                       P_ORG_ID     IN NUMBER,
                                       P_ISDISSOLVE IN NUMBER);
  PROCEDURE CALL_ENTITLEMENT(P_USERNAME   VARCHAR2,
                             P_ORG_ID     IN NUMBER,
                             P_PERIOD_ID  IN NUMBER,
                             P_ISDISSOLVE IN NUMBER);

  FUNCTION FN_CALL_TN(P_EMPLOYEE_ID IN NUMBER,
                      P_ENDDATE     IN DATE,
                      P_YEAR_TN     NUMBER,
                      P_DAY_TN      NUMBER) RETURN NUMBER;

  PROCEDURE GETSIGNDEFAULT(P_USERNAME   IN NVARCHAR2,
                           P_ORG_ID     IN NUMBER,
                           P_PERIOD_ID  IN NUMBER,
                           P_ISDISSOLVE IN NUMBER,
                           P_FROMDATE   IN DATE,
                           P_ENDATE     IN DATE,
                           P_CUR        OUT CURSOR_TYPE);
  PROCEDURE GETSIGNDEFAULT_FOREMP(P_EMPLOYEE_ID IN NUMBER,
                                 P_USERNAME    IN VARCHAR2,
                                 P_ORG_ID     IN NUMBER,
                                 P_PERIOD_ID  IN NUMBER,
                                 P_ISDISSOLVE IN NUMBER,
                                 P_FROMDATE   IN DATE,
                                 P_ENDATE     IN DATE,
                                 P_OUT        OUT NUMBER);
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
                           P_CURCOUNR        OUT CURSOR_TYPE);

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
                                 P_D31        IN NUMBER);

  PROCEDURE IMPORT_WORKSIGN_DATE(P_STARTDATE IN DATE,
                                 P_ENDDATE   IN DATE,
                                 P_USERNAME  IN VARCHAR2);

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
                           P_CUR3        OUT CURSOR_TYPE);
  PROCEDURE CHECK_DELETE_SIGNWORK(P_ID IN NVARCHAR2);

  PROCEDURE IMPORT_ENTITLEMENT_LEAVE(P_DOCXML IN CLOB,
                                     P_USER   IN NVARCHAR2,
                                     P_PERIOD IN NUMBER);

  PROCEDURE CALL_ENTITLEMENT_HOSE(P_USERNAME   VARCHAR2,
                                  P_ORG_ID     IN NUMBER,
                                  P_PERIOD_ID  IN NUMBER,
                                  P_ISDISSOLVE IN NUMBER);

  PROCEDURE AT_ENTITLEMENT_PREV_HAVE(P_USERNAME   VARCHAR2,
                                     P_ORG_ID     IN NUMBER,
                                     P_PERIOD_ID  IN NUMBER,
                                     P_ISDISSOLVE IN NUMBER);
  /*
  PROCEDURE CAL_TIME_TIMESHEET_ALL(P_USERNAME   IN NVARCHAR2,
                                   P_ORG_ID     IN NUMBER,
                                   P_PERIOD_ID  IN NUMBER,
                                   P_ISDISSOLVE IN NUMBER,
                                   P_DELETE_ALL IN NUMBER:= 0);
  
  PROCEDURE CAL_TIME_TIMESHEET_MACHINES(P_USERNAME   IN NVARCHAR2,
                                        P_ORG_ID     IN NUMBER,
                                        P_FROMDATE   IN DATE,
                                        P_ENDDATE    IN DATE,
                                        P_ISDISSOLVE IN NUMBER);
  
  PROCEDURE CAL_TIMETIMESHEET_NB(P_USERNAME   IN NVARCHAR2,
                                 P_ORG_ID     IN NUMBER,
                                 P_PERIOD_ID  IN NUMBER,
                                 P_ISDISSOLVE IN NUMBER);
  
  PROCEDURE CAL_TIME_TIMESHEET_RICE(P_USERNAME   IN NVARCHAR2,
                                    P_ORG_ID     IN NUMBER,
                                    P_PERIOD_ID  IN NUMBER,
                                    P_ISDISSOLVE IN NUMBER);
  
  PROCEDURE GETDATAFROMORG(P_USERNAME    IN NVARCHAR2,
                           P_ORG_ID      IN NUMBER,
                           P_ISDISSOLVE  IN NUMBER,
                           P_EXPORT_TYPE IN NUMBER,
                           P_PERIOD_ID   IN NUMBER,
                           P_CUR         OUT CURSOR_TYPE,
                           P_CUR2        OUT CURSOR_TYPE,
                           P_CUR3        OUT CURSOR_TYPE);
  
  PROCEDURE GET_CCT(P_USERNAME      IN NVARCHAR2,
                    P_ORG_ID        IN NUMBER,
                    P_ISDISSOLVE    IN NUMBER,
                    P_PAGE_INDEX    IN NUMBER,
                    P_EMPLOYEE_CODE IN NVARCHAR2,
                    P_EMPLOYEE_NAME IN NVARCHAR2,
                    P_ORG_NAME      IN NVARCHAR2,
                    P_TITLE_NAME    IN NVARCHAR2,
                    P_PAGE_SIZE     IN NUMBER,
                    P_PERIOD_ID     IN NUMBER,
                    P_TERMINATE     IN NUMBER,
                    P_CUR           OUT CURSOR_TYPE,
                    P_CURCOUNR      OUT CURSOR_TYPE);
  
  PROCEDURE GET_CCT_ORIGIN(P_USERNAME      IN NVARCHAR2,
                           P_ORG_ID        IN NUMBER,
                           P_ISDISSOLVE    IN NUMBER,
                           P_EMPLOYEE_CODE IN NVARCHAR2,
                           P_EMPLOYEE_NAME IN NVARCHAR2,
                           P_ORG_NAME      IN NVARCHAR2,
                           P_TITLE_NAME    IN NVARCHAR2,
                           P_PERIOD_ID     IN NUMBER,
                           P_TERMINATE     IN NUMBER,
                           P_CUR           OUT CURSOR_TYPE);
  
  
  
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
                           P_CURCOUNR        OUT CURSOR_TYPE);
  
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
                             P_CURCOUNR        OUT CURSOR_TYPE);
  
  */

  PROCEDURE CLOSEDOPEN_PERIOD(P_USERNAME   IN NVARCHAR2,
                              P_ORG_ID     IN NUMBER,
                              P_ISDISSOLVE IN NUMBER,
                              P_STATUS     IN NUMBER,
                              P_PERIOD_ID  IN NUMBER);
  /*
  
  PROCEDURE INSERT_TIME_CARD(P_TIMEID     IN NVARCHAR2,
                             P_TERMINALID IN NUMBER,
                             P_VALTIME    IN DATE,
                             P_USERNAME   IN NVARCHAR2);
  
  PROCEDURE INSERT_TIME_CARD_AUTO(P_TIMEID      IN NUMBER,
                                  P_TERMINAL_ID IN NUMBER,
                                  P_VALTIME     IN DATE,
                                  P_USERNAME    IN NVARCHAR2);
  
  
  
  
  
  PROCEDURE UPDATE_LEAVESHEET_DAILY(P_STARTDATE IN DATE,
                                    P_ENDDATE   IN DATE,
                                    P_USERNAME  IN VARCHAR2);
  
  
  PROCEDURE CAL_TIME_TIMESHEET_MONTHLY(P_USERNAME   IN VARCHAR2,
                                       P_PERIOD_ID  IN NUMBER,
                                       P_ORG_ID     IN NUMBER,
                                       P_ISDISSOLVE IN NUMBER);
  PROCEDURE INSERT_INS_CHANGE(P_USERNAME   IN VARCHAR2,
                              P_ORG_ID     IN NUMBER,
                              P_PERIOD_ID  IN NUMBER,
                              P_ISDISSOLVE IN NUMBER);
  PROCEDURE MANAGEMENT_TOTAL_ENTITLEMENT(P_EMPLOYEE_ID IN NUMBER,
                                         P_DATE_TIME   IN DATE,
                                         P_OUT         OUT CURSOR_TYPE);
  PROCEDURE MANAGEMENT_TOTAL_COMPENSATORY(P_EMPLOYEE_ID IN NUMBER,
                                          P_DATE_TIME   IN DATE,
                                          P_OUT         OUT CURSOR_TYPE);
  PROCEDURE MANAGEMENT_HISTORY_LEAVE(P_EMPLOYEE_ID IN NUMBER,
                                     P_FROM_DATE   IN DATE,
                                     P_TO_DATE     IN DATE,
                                     P_OUT         OUT CURSOR_TYPE);
  
  PROCEDURE GETMACHINES(P_USERNAME  IN VARCHAR2,
                        P_ORGID     IN NUMBER,
                        P_FROM_DATE IN DATE,
                        P_TO_DATE   IN DATE,
                        P_OUT       OUT CURSOR_TYPE);
  */
  PROCEDURE IMPORT_SWIPE_DATA(P_XML IN CLOB, P_USERNAME IN VARCHAR2);
  /*
  PROCEDURE IMPORT_SWIPE_DATA_AUTO(P_XML      IN CLOB,
                                   P_USERNAME IN VARCHAR2,
                                   P_OUT      OUT CURSOR_TYPE);
  */
  PROCEDURE IMPORT_TIMESHEET_CTT(P_XML       IN CLOB,
                                 P_USERNAME  IN VARCHAR2,
                                 P_PERIOD_ID IN NUMBER,
                                 P_CUR       OUT CURSOR_TYPE);
  /*
    PROCEDURE UPDATE_TIMESHEET_CTT(P_FROMDATE  IN DATE,
                                   P_TODATE    IN DATE,
                                   P_EMP_ID    IN NUMBER,
                                   P_MANUAL_ID IN NUMBER,
                                   P_USERNAME  IN VARCHAR2);
                                   
    FUNCTION FN_CALL_TN(P_EMPLOYEE_ID IN NUMBER,
                        P_ENDDATE IN DATE,
                        P_YEAR_TN NUMBER,
                        P_DAY_TN  NUMBER) RETURN NUMBER;                              
  */

  PROCEDURE PRINT_DONPHEP(P_ID          IN NUMBER,
                          P_EMPLOYEE_ID IN NUMBER,
                          P_DATE_TIME   IN DATE,
                          P_CUR         OUT CURSOR_TYPE);

  PROCEDURE CALL_ENTITLEMENT_TNG(P_USERNAME   VARCHAR2,
                                 P_ORG_ID     IN NUMBER,
                                 P_PERIOD_ID  IN NUMBER,
                                 P_ISDISSOLVE IN NUMBER);

  PROCEDURE CAL_TIMETIMESHEET_OT(P_USERNAME   IN NVARCHAR2,
                                 P_ORG_ID     IN NUMBER,
                                 P_PERIOD_ID  IN NUMBER,
                                 P_ISDISSOLVE IN NUMBER,
                                 P_EMP_OBJ    IN NUMBER);

  PROCEDURE INPORT_NB(P_DOCXML    IN CLOB,
                      P_USER      IN NVARCHAR2,
                      P_PERIOD_ID NUMBER);

  PROCEDURE INPORT_NB_PREV(P_DOCXML IN CLOB,
                           P_USER   IN NVARCHAR2,
                           P_YEAR   NUMBER);

  FUNCTION FN_GET_PHEP(P_WCODE  IN NVARCHAR2 DEFAULT '',
                       P_ORG_ID IN NUMBER DEFAULT 0,
                       P_YEAR   IN NUMBER DEFAULT 0) RETURN NUMBER;

  FUNCTION FN_GET_PREV_USED(PV_RESET_MONTH IN NUMBER DEFAULT 1,
                            PV_ID          IN NUMBER) RETURN NUMBER;

  FUNCTION FN_GET_EXPIREDATE(P_ORG_ID IN NUMBER DEFAULT 0,
                             P_YEAR   IN NUMBER DEFAULT 0) RETURN NVARCHAR2;

  FUNCTION DATE_TO_DATE(P_FROM_DATE IN DATE, P_TO_DATE IN DATE)
    RETURN NVARCHAR2;

  FUNCTION DATE_TO_DATE_SUM(P_FROM_DATE IN DATE, P_TO_DATE IN DATE)
    RETURN NVARCHAR2;

  FUNCTION DATE_TO_DATE_SELECT(P_FROM_DATE IN DATE, P_TO_DATE IN DATE)
    RETURN NVARCHAR2;

  FUNCTION DATE_TO_DATE_SELECT1(P_FROM_DATE IN DATE, P_TO_DATE IN DATE)
    RETURN NVARCHAR2;

  FUNCTION FN_CALL_DEDUCT(P_MIN_LATE       IN NUMBER,
                          P_MIN_EARLY      IN NUMBER,
                          P_MIN_LATE_EARLY IN NUMBER,
                          P_ORG_ID         IN NUMBER,
                          P_DATE           IN DATE) RETURN NUMBER;

  FUNCTION FN_GET_EXPIREDATE_NB(P_ORG_ID IN NUMBER DEFAULT 0,
                                P_YEAR   IN NUMBER DEFAULT 0)
    RETURN NVARCHAR2;
  FUNCTION FN_CHECK_EXIST_EMP(P_EMP_CODE IN NVARCHAR2, P_DATE IN DATE)
    RETURN NUMBER;
  PROCEDURE IMPORT_DATA_REGISTER_CO_OUT( --P_XML      IN CLOB,
                                        --P_USERNAME IN VARCHAR2,
                                        P_REQUEST_ID IN NUMBER,
                                        /* P_OUT        OUT NUMBER*/
                                        P_RESULT OUT CURSOR_TYPE);
  PROCEDURE IMPORT_DATA_REGISTER_CO(P_XML        IN CLOB,
                                    P_USERNAME   IN VARCHAR2,
                                    P_REQUEST_ID IN NUMBER,
                                    P_OUT        OUT NUMBER
                                    /*P_RESULT   OUT CURSOR_TYPE*/);

  PROCEDURE ENUM_NODE(n DBMS_XMLDOM.DOMNode, P_NAME NVARCHAR2);

  FUNCTION FN_GET_PERIOD_STANDARD(P_ORG_ID    IN NUMBER DEFAULT 0,
                                  P_PERIOD_ID IN NUMBER DEFAULT 0,
                                  P_OBJECT_ID IN NUMBER) RETURN NUMBER;

  PROCEDURE IMPORT_DATA_INOUT(P_DATA      IN CLOB,
                              P_USER      IN NVARCHAR2,     
                              P_TER_ID In Number,
                              P_FROM_DATE IN DATE,
                              P_END_DATE  IN DATE,
                              P_OUT       OUT CURSOR_TYPE);

  PROCEDURE IMPORT_DATA_INOUT_CHECKINOUT(P_DATA        IN CLOB,
                                         P_TERMINAL_ID IN NUMBER,
                                         P_USER        IN NVARCHAR2,
                                         P_FROM_DATE   IN DATE,
                                         P_END_DATE    IN DATE,
                                         P_OUT         OUT CURSOR_TYPE);

  PROCEDURE GET_OTHER_LIST_DSVM(P_USERNAME   IN NVARCHAR2,
                                P_ORG_ID     IN NUMBER,
                                P_START_DATE IN date,
                                P_END_DATE   IN date,
                                P_ISDISSOLVE IN NUMBER,
                                P_CUR        OUT CURSOR_TYPE,
                                P_CURCOUNR   OUT CURSOR_TYPE,
                                P_CURDATA    OUT CURSOR_TYPE);
                                
  PROCEDURE CAL_TIME_TIMESHEET_BY_EMP(P_USERNAME   IN NVARCHAR2,
                                       P_DELETE_ALL IN NUMBER := 1,
                                       P_DATE  IN DATE,
                                       P_EMPLIST    IN CLOB,
                                       P_OUT        OUT NUMBER);
  PROCEDURE GET_PERIOD_USER(P_USER IN NVARCHAR2,
                            P_OUT OUT CURSOR_TYPE,
                            P_OUT1 OUT CURSOR_TYPE);
  PROCEDURE IMPORT_OT(P_DOCXML IN CLOB,P_USERNAME IN NVARCHAR2,P_LOG IN NVARCHAR2);  
  PROCEDURE DELETE_AT_WORKSIGN_FROM_AT_SIGNDEFAULT(P_EMPLOYEE_ID IN NUMBER,
                                  P_FROMDATE   IN DATE,
                                  P_ENDATE     IN DATE,
                                  P_OUT        OUT NUMBER);     
  PROCEDURE INSERT_GEN_ATWORKSIGN(P_USERNAME   IN VARCHAR2,
                                P_ORG_ID     IN NUMBER,
                                P_ISDISSOLVE IN NUMBER,
                                P_EMPLOYEE_ID IN CLOB,
                                P_YEAR        IN NUMBER,
                                P_CALENDAR_ID IN NUMBER,
                                P_OUT        OUT NUMBER);     
  PROCEDURE DELETE_GEN_ATWORKSIGN(P_ID         IN VARCHAR2,
                                 P_YEAR       IN NUMBER,
                                 P_OUT        OUT NUMBER);    
  PROCEDURE GET_SWIPE_DATA_IMPORT(P_USERNAME    IN VARCHAR2,
                                  P_ORG_ID      IN NUMBER,
                                  P_IS_DISSOLVE IN NUMBER,
                                  P_OUT         OUT CURSOR_TYPE,
                                  P_OUT2        OUT CURSOR_TYPE);
                                  
   PROCEDURE CAL_TIME_TIMESHEET_ALL_AUTO;                               
                                  
END PKG_ATTENDANCE_BUSINESS;
