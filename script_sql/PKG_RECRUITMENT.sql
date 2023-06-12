CREATE OR REPLACE PACKAGE PKG_RECRUITMENT IS

  TYPE CURSOR_TYPE IS REF CURSOR;
  PROCEDURE CHECK_EXIST_REQUEST(P_ORG_ID             IN NUMBER,
                                P_TITLE_ID           IN NUMBER,
                                P_SEND_DATE          IN DATE,
                                P_EXPECTED_JOIN_DATE IN DATE,
                                P_OUT                OUT NUMBER);
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
                                      p_out                 OUT NUMBER);
  PROCEDURE delete_rc_candidate_trainning(p_id IN NUMBER, p_out OUT NUMBER);
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
                                      P_OUT                 OUT NUMBER);
  PROCEDURE get_combobox(p_combobox_code IN VARCHAR2,
                         p_cur           OUT cursor_type);
  PROCEDURE Get_candidate_trainning(p_candidate_code IN VARCHAR2,
                                    P_CUR            OUT CURSOR_TYPE);
  PROCEDURE GET_PLAN_SUMMARY(P_YEAR       IN VARCHAR,
                             P_USERNAME   IN VARCHAR,
                             P_ORGID      IN NUMBER,
                             P_ISDISSOLVE IN NUMBER,
                             P_CUR        OUT CURSOR_TYPE);

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
                                 PV_CUR18 OUT CURSOR_TYPE);
  PROCEDURE UPDATE_REMARK_REJECT(P_ID            IN NUMBER,
                                 P_REMARK_REJECT IN NVARCHAR2,
                                 P_OUT           OUT NUMBER);
  PROCEDURE UPDATE_REQUEST_REMARK_REJECT(P_ID            IN NUMBER,
                                         P_REMARK_REJECT IN NVARCHAR2,
                                         P_OUT           OUT NUMBER);
  PROCEDURE GET_TOTAL_EMPLOYEE_BY_TITLEID(P_ORGID   IN NUMBER,
                                          P_TITLEID IN NUMBER,
                                          P_USER    IN NVARCHAR2,
                                          P_OUT     OUT NUMBER);

  PROCEDURE STAGE_GET_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);

  PROCEDURE STAGE_GETLIST_BYFILTER(P_ORG_ID     IN NUMBER,
                                   P_STAGE_YEAR IN NUMBER,
                                   P_CUR        OUT CURSOR_TYPE);

  PROCEDURE STAGE_GETDATA_COMBOBOX(P_ORG_ID IN NUMBER,
                                   P_CUR    OUT CURSOR_TYPE);

  PROCEDURE STAGE_UPDATE_COSTESTIMATE(P_STAGE_ID IN NUMBER,
                                      P_OUT      OUT NUMBER);

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
                         P_OUT              OUT NUMBER);

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
                         P_OUT              OUT NUMBER);

  PROCEDURE DELETE_STAGE(P_ID IN NVARCHAR2, P_OUT OUT NUMBER);

  PROCEDURE COSTALLOCATE_GET_BY_STAGE(P_RC_STAGE_ID IN NUMBER,
                                      P_CUR         OUT CURSOR_TYPE);

  PROCEDURE ADDNEW_COSTALLOCATE(P_RC_STAGE_ID IN NUMBER,
                                P_ORG_ID      IN NUMBER,
                                P_COSTAMOUNT  IN NUMBER,
                                P_REMARK      IN NVARCHAR2,
                                P_USERNAME    IN NVARCHAR2,
                                P_CREATED_LOG IN NVARCHAR2,
                                P_OUT         OUT NUMBER);

  PROCEDURE UPDATE_COSTALLOCATE(P_ID            IN NUMBER,
                                P_RC_STAGE_ID   IN NUMBER,
                                P_ORG_ID        IN NUMBER,
                                P_COSTAMOUNT    IN NUMBER,
                                P_REMARK        IN NVARCHAR2,
                                P_USERNAME      IN NVARCHAR2,
                                P_MODIFY_BY_LOG IN NVARCHAR2,
                                P_OUT           OUT NUMBER);

  PROCEDURE DELETE_COSTALLOCATE(P_ID IN NVARCHAR2, P_OUT OUT NUMBER);

  PROCEDURE UPDATE_MANNING_ORG(P_ID          IN NUMBER,
                               P_NAME        IN NVARCHAR2,
                               P_EFFECT_DATE IN DATE,
                               P_NOTE        IN NVARCHAR2,
                               P_STATUS      IN NUMBER,
                               P_OUT         OUT NUMBER);

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
                               P_OUT         OUT NUMBER);

  PROCEDURE ADDNEW_MANNING_TITLE(P_ORG_ID         IN NUMBER,
                                 P_MANNING_ORG_ID IN NUMBER,
                                 P_OUT            OUT NUMBER);

  PROCEDURE GET_MANNING_ORG_ID(P_OUT OUT NUMBER);
  PROCEDURE DELETE_RECORD_IMPORT(P_ID IN CLOB, P_OUT OUT NUMBER);
  PROCEDURE UPDATE_NEW_MANNING_TITLE(P_ID       IN NUMBER,
                                     P_NEW_MAN  IN NUMBER,
                                     P_NOTE     IN NVARCHAR2,
                                     P_MOBILIZE IN NUMBER,
                                     P_OUT      OUT NUMBER);

  PROCEDURE UPDATE_SUMMARY_MANNING_ORG(P_MANNING_ORG_ID IN NUMBER,
                                       P_OUT            OUT NUMBER);
  PROCEDURE UPDATE_SUMMARY_MANNING_TITLE(P_MANNING_ORG_ID IN NUMBER,
                                         P_OUT            OUT NUMBER);

  PROCEDURE DELETE_MANNING(P_ID             IN NVARCHAR2,
                           P_MANNING_ORG_ID IN NUMBER,
                           P_OUT            OUT NUMBER);

  PROCEDURE GETCOUNT_EMPWORKING(P_TITLE IN NUMBER, P_OUT OUT NUMBER);

  PROCEDURE GETLIST_MANNING(P_ORG_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);

  PROCEDURE GETLIST_MANNING_BY_NAME(P_MANNING_ORG_ID IN NUMBER,
                                    P_ORG_ID         IN NUMBER,
                                    P_YEAR           IN NUMBER,
                                    P_CUR            OUT CURSOR_TYPE);

  PROCEDURE GET_OLD_MANNING(P_EFFECT_DATE_NEW IN DATE, P_OUT OUT NUMBER);

  PROCEDURE GET_CURRENT_MANNING_TITLE(P_ORG_ID   IN NUMBER,
                                      P_TITLE_ID IN NUMBER,
                                      P_CUR      OUT CURSOR_TYPE);
  PROCEDURE GET_CURRENT_MANNING_TITLE1(P_ORG_ID   IN NUMBER,
                                       P_TITLE_ID IN NUMBER,
                                       P_DATE     IN DATE,
                                       P_CUR      OUT CURSOR_TYPE);

  PROCEDURE LOAD_INFO_MANNING_EMPBYORG(P_ORG   IN NUMBER,
                                       P_TITLE IN NUMBER,
                                       P_CUR   OUT CURSOR_TYPE);

  PROCEDURE EXPORT_DATA_TEMPLATE_MANNING(P_ORG_ID  IN NVARCHAR2,
                                         P_TITLE   IN NUMBER,
                                         p_CUR_OUT OUT CURSOR_TYPE);

  PROCEDURE LOAD_COMBOBOX_YEAR(P_ORG_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);

  PROCEDURE LOAD_YEAR_PLANREG(P_ORG_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);

  PROCEDURE LOAD_COMBOBOX_LISTMANNINGNAME(P_ORG_ID IN NUMBER,
                                          P_YEAR   IN NUMBER,
                                          P_CUR    OUT CURSOR_TYPE);

  PROCEDURE XUAT_TO_TRINH(P_STAGE_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);

  PROCEDURE COST_GETLIST_BYFILTER(P_ORG_ID IN NUMBER,
                                  P_CUR    OUT CURSOR_TYPE);

  PROCEDURE COST_GET_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);

  PROCEDURE ADDNEW_COST(P_RC_STAGE_ID    IN NUMBER,
                        P_ORG_ID         IN NUMBER,
                        P_SOURCEOFREC_ID IN NVARCHAR2,
                        P_COSTESTIMATE   IN NUMBER,
                        P_COSTREALITY    IN NUMBER,
                        P_REMARK         IN NVARCHAR2,
                        P_USERNAME       IN NVARCHAR2,
                        P_CREATED_LOG    IN NVARCHAR2,
                        P_OUT            OUT NUMBER);

  PROCEDURE UPDATE_COST(P_ID             IN NUMBER,
                        P_RC_STAGE_ID    IN NUMBER,
                        P_ORG_ID         IN NUMBER,
                        P_SOURCEOFREC_ID IN NVARCHAR2,
                        P_COSTESTIMATE   IN NUMBER,
                        P_COSTREALITY    IN NUMBER,
                        P_REMARK         IN NVARCHAR2,
                        P_USERNAME       IN NVARCHAR2,
                        P_MODIFY_BY_LOG  IN NVARCHAR2,
                        P_OUT            OUT NUMBER);

  PROCEDURE DELETE_COST(P_ID IN NVARCHAR2, P_OUT OUT NUMBER);

  PROCEDURE COST_COSTALLOCATE_GET_BY_COST(P_RC_COST_ID IN NUMBER,
                                          P_CUR        OUT CURSOR_TYPE);

  PROCEDURE COST_UPDATE_COSTREALITY(P_COST_ID IN NUMBER, P_OUT OUT NUMBER);

  PROCEDURE ADDNEW_COST_COSTALLOCATE(P_RC_COST_ID  IN NUMBER,
                                     P_ORG_ID      IN NUMBER,
                                     P_COSTAMOUNT  IN NUMBER,
                                     P_REMARK      IN NVARCHAR2,
                                     P_USERNAME    IN NVARCHAR2,
                                     P_CREATED_LOG IN NVARCHAR2,
                                     P_OUT         OUT NUMBER);

  PROCEDURE UPDATE_COST_COSTALLOCATE(P_ID            IN NUMBER,
                                     P_RC_COST_ID    IN NUMBER,
                                     P_ORG_ID        IN NUMBER,
                                     P_COSTAMOUNT    IN NUMBER,
                                     P_REMARK        IN NVARCHAR2,
                                     P_USERNAME      IN NVARCHAR2,
                                     P_MODIFY_BY_LOG IN NVARCHAR2,
                                     P_OUT           OUT NUMBER);

  PROCEDURE DELETE_COST_COSTALLOCATE(P_ID IN NVARCHAR2, P_OUT OUT NUMBER);

  PROCEDURE CANDIDATE_RESULT_GETBYFILTER(P_PROGRAM_ID       IN NUMBER,
                                         P_PROGRAM_EXAMS_ID IN NUMBER,
                                         P_CUR              OUT CURSOR_TYPE);

  PROCEDURE CANDIDATE_LIST_GETBYPROGRAM(P_PROGRAM_ID IN NUMBER,
                                        P_CUR        OUT CURSOR_TYPE);

  PROCEDURE CANDIDATE_GET_AVERAGE_MARKS(P_PROGRAM_ID   IN NUMBER,
                                        P_CANDIDATE_ID IN NUMBER,
                                        P_OUT          OUT NUMBER);

  PROCEDURE EXAMS_GETBYCANDIDATE(P_PROGRAM_ID   IN NUMBER,
                                 P_CANDIDATE_ID IN NUMBER,
                                 P_IS_PV        IN NUMBER,
                                 P_CUR          OUT CURSOR_TYPE);

  PROCEDURE UPDATE_CANDIDATE_RESULT(P_ID              IN NUMBER,
                                    P_POINT_RESULT    IN NUMBER,
                                    P_COMMENT_INFO    IN NVARCHAR2,
                                    P_ASSESSMENT_INFO IN NVARCHAR2,
                                    P_IS_PASS         IN NUMBER,
                                    P_OUT             OUT NUMBER);

  PROCEDURE DELETE_CANDIDATE_RESULT(P_ID IN NVARCHAR2, P_OUT OUT NUMBER);

  PROCEDURE UPDATE_CANDIDATE_STATUS(P_ID          IN NUMBER,
                                    P_STATUS_CODE IN NVARCHAR2,
                                    P_OUT         OUT NUMBER);

  PROCEDURE EXPORT_RECRUITMENT_NEEDS(P_ID         IN NUMBER,
                                     P_DinhBien   IN NUMBER,
                                     P_HienCo     IN NUMBER,
                                     P_SLCanTuyen IN NUMBER,
                                     p_CUR_OUT    OUT CURSOR_TYPE);
  PROCEDURE EXPORT_RC_NEEDS_BYLISTID(P_ID      IN NVARCHAR2,
                                     p_CUR_OUT OUT CURSOR_TYPE);
  --p_CUR_OUT2     OUT CURSOR_TYPE);    

  PROCEDURE EXPORT_DE_NGHI_THU_VIEC(P_PROGRAM_ID             IN NUMBER,
                                    table_CELL_EXAMS_MAPPING OUT CURSOR_TYPE,
                                    table_CELL_MAPPING       OUT CURSOR_TYPE,
                                    table_INFO               OUT CURSOR_TYPE,
                                    table_DETAIL             OUT CURSOR_TYPE);

  PROCEDURE AUTO_GENERATE_PRO_EXAMS(P_ORG_ID     IN NUMBER,
                                    P_TITLE_ID   IN NUMBER,
                                    P_PROGRAM_ID IN NUMBER,
                                    P_OUT        OUT NUMBER);

  PROCEDURE GET_PROGRAM_SCHCEDULE_LIST(P_PROGRAM_ID IN NUMBER,
                                       P_CUR        OUT CURSOR_TYPE);
  PROCEDURE GET_PROGRAM_SCHCEDULE_LIST_ALL (P_PROGRAM_SCHEDULE_ID IN NVARCHAR2,
                                            P_PROGRAM_ID IN NUMBER,
                                       P_CUR        OUT CURSOR_TYPE);
  PROCEDURE GET_SCHCEDULE_CAN_BY_STATUS(P_PROGRAM_ID          IN NUMBER,
                                        P_PROGRAM_SCHEDULE_ID IN NUMBER,
                                        P_CUR                 OUT CURSOR_TYPE);

  PROCEDURE GET_CANDIDATE_NOT_SCHEDULE(P_PROGRAM_ID IN NUMBER,
                                       P_SCHEDULE   IN NUMBER,
                                       P_CUR        OUT CURSOR_TYPE);

  PROCEDURE CHECK_EXAMS_PRO_SCHEDULE_CAN(P_PROGRAM_SCHEDULE_ID IN NUMBER,
                                         P_CANDIDATE_ID        IN NUMBER,
                                         P_OUT                 OUT NUMBER);

  PROCEDURE ADDNEW_CAN_PRO_SCHEDULE(P_CANDIDATE_ID        IN NUMBER,
                                    P_PROGRAM_SCHEDULE_ID IN NUMBER,
                                    P_PROGRAM_EXAMS_ID    IN NUMBER,
                                    P_OUT                 OUT NUMBER);

  PROCEDURE ADDNEW_PRO_SCHEDULE(P_PROGRAM_ID    IN NUMBER,
                                P_EMPLOYEE_ID   IN NUMBER,
                                P_SCHEDULE_DATE IN DATE,
                                P_EXAMS_PLACE   IN NVARCHAR2,
                                P_NOTE          IN NVARCHAR2,
                                P_TIME          IN NVARCHAR2,
                                P_ID_EXAM          IN NVARCHAR2,
                                P_USERNAME      IN NVARCHAR2,
                                P_CREATED_LOG   IN NVARCHAR2,
                                P_OUT           OUT NUMBER);

  PROCEDURE UPDATE_PRO_SCHEDULE(P_ID            IN NUMBER,
                                P_EMPLOYEE_ID   IN NUMBER,
                                P_SCHEDULE_DATE IN DATE,
                                P_EXAMS_PLACE   IN NVARCHAR2,
                                P_NOTE          IN NVARCHAR2,
                                P_TIME          IN NVARCHAR2,
                                P_ID_EXAM          IN NVARCHAR2,
                                P_USERNAME      IN NVARCHAR2,
                                P_MODIFY_BY_LOG IN NVARCHAR2,
                                P_OUT           OUT NUMBER);
  PROCEDURE GET_TOPID_PRO_SCHEDULE(P_PROGRAM_ID IN NUMBER,
                                   P_OUT        OUT NUMBER);

  PROCEDURE UPDATE_PRO_SCHEDULE_ID(P_ID IN NUMBER, P_OUT OUT NUMBER);

  PROCEDURE DELETE_PRO_SCHEDULE_CAN_ISNULL(P_OUT OUT NUMBER);

  PROCEDURE GET_PRO_SCHEDULE_BYID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);

  PROCEDURE DELETE_PRO_SCHEDULE_CAN(P_PRO_SCHEDULE_ID IN NUMBER,
                                    P_CANDIDATE_ID    IN NUMBER,
                                    P_OUT             OUT NUMBER);

  PROCEDURE GET_ALL_EXAMS_BYPRO(P_PROGRAM_ID  IN NUMBER,
                                P_SCHEDULE_ID IN NUMBER,
                                P_CUR         OUT CURSOR_TYPE);

  PROCEDURE MANNING_ORG_GET_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);

  PROCEDURE GET_TITLE_IN_PLAN(P_ORGID   IN NUMBER,
                              P_IS_PLAN IN NUMBER,
                              P_LANG    IN VARCHAR,
                              P_CUR     OUT CURSOR_TYPE);

  PROCEDURE CHECK_REQUEST_NOT_APPROVE(P_ID IN NVARCHAR2, P_OUT OUT NUMBER);

  PROCEDURE PLAN_GET_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);
  PROCEDURE Get_Email_Candidate(P_ID IN NVARCHAR2, P_OUT OUT nvarchar2);
  PROCEDURE GET_INFO_CADIDATE(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);
  PROCEDURE GET_PROGRAM_EXAMS(P_PROGRAMID IN NUMBER,
                              P_ORGID     IN NUMBER,
                              P_TILID     IN NUMBER,
                              P_CUR       OUT CURSOR_TYPE);
  PROCEDURE GET_MAIL_COMPANY(P_EMPID IN NUMBER, P_CUR OUT NVARCHAR2);
  PROCEDURE Get_Email_Employee(P_ID IN NVARCHAR2, P_OUT OUT nvarchar2);
  PROCEDURE IMPORT_DINH_BIEN(P_USER   IN NVARCHAR2,
                             P_DOCXML IN NCLOB,
                             P_OUT    OUT NUMBER);

  PROCEDURE GET_GROUP_TITLE_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);
  PROCEDURE GET_INF_EMP_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);
  PROCEDURE GET_HR_PLANINGS_DEFAULT(IS_BLANK IN NUMBER,
                                    P_CUR    OUT CURSOR_TYPE);
  PROCEDURE AUTO_GEN_CODE_RC(ORG_ID       IN NUMBER,
                             DATE_REQUEST IN DATE,
                             P_CUR        OUT CURSOR_TYPE);

  PROCEDURE GET_NUM_PLANNING_DETAIL_BY_MONTH(P_ORG_ID       IN NUMBER,
                                             P_TITLE_ID     IN NUMBER,
                                             P_DATE_REQUEST IN DATE,
                                             P_CUR          OUT CURSOR_TYPE);

  PROCEDURE GET_TITLE_BY_ID(P_ID IN NUMBER, P_CUR OUT CURSOR_TYPE);

  PROCEDURE GET_CANDIDATE_NOT_SCHEDULE_1(P_PROGRAM_ID IN NUMBER,
                                         P_SCHEDULE   IN NUMBER,
                                         P_EXAMS_ID   IN NUMBER,
                                         P_CUR        OUT CURSOR_TYPE);

  PROCEDURE UPDATE_PROGRAM_CANDIDATE_STATUS(P_ID          IN NUMBER,
                                            P_STATUS_CODE IN NVARCHAR2,
                                            P_OUT         OUT NUMBER);

  PROCEDURE CANDIDATE_LIST_GETBYPROGRAM_BY_EXAMS(P_PROGRAM_ID IN NUMBER,
                                                 P_EXAMS_ID   IN NUMBER,
                                                 P_CUR        OUT CURSOR_TYPE);

  PROCEDURE EXAMS_GETBYCANDIDATE_BY_PCS_ID(P_PSC_ID IN NUMBER,
                                           P_IS_PV  IN NUMBER,
                                           P_CUR    OUT CURSOR_TYPE);

  PROCEDURE DELETE_RC_PROGRAM_SCHEDULE(P_ID IN NVARCHAR2, P_OUT OUT NUMBER);

  PROCEDURE GET_SCHCEDULE_CAN_BY_SCHCEDULEID(P_PROGRAM_ID          IN NUMBER,
                                             P_PROGRAM_SCHEDULE_ID IN NUMBER,
                                             P_CUR                 OUT CURSOR_TYPE);
  PROCEDURE UPDATE_APPROVED_REQUEST_PORTAL(P_ID  IN VARCHAR2,
                                           P_OUT OUT NUMBER);
 PROCEDURE GET_REQUESTPORTAL_APPROVE(P_EMPLOYEE_APPROVED  IN NUMBER,
                                     P_IS_APPROVED        IN NUMBER,
                                     P_USERNAME           IN VARCHAR2,
                                     P_ORG_ID             IN NUMBER,                        
                                     P_OUT                OUT CURSOR_TYPE);  
                                     
  PROCEDURE DELETE_SALARY_ALLOWANCE_CANDIDATE(RC_PROGRAM_ID IN NUMBER, RC_CANDIDATE_ID IN NUMBER, P_OUT OUT NUMBER);    
  PROCEDURE GET_SE_USER_BY_IS_RC(P_IS_RC IN NUMBER,
                                    P_CUR  OUT CURSOR_TYPE);                                     
END;
