<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalTraining.ascx.vb"
    Inherits="Profile.ctrlPortalTraining" %>
<tlk:RadGrid PageSize="20" ID="rgTraining" runat="server" Height="350px" AllowFilteringByColumn="true"
    Scrolling="both">
    <MasterTableView DataKeyNames="ID,ORG_ID,EMPLOYEE_ID,EMPLOYEE_CODE" ClientDataKeyNames="ID,ORG_ID,EMPLOYEE_ID">
        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="STATUS_CODE" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridTemplateColumn HeaderText="Đơn vị" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
                            </tlk:GridTemplateColumn>
                            <tlk:GridDateTimeColumn HeaderText="Thời gian bắt đầu" DataField="START_DATE" ItemStyle-HorizontalAlign="Center"
                                SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Thời gian kết thúc" DataField="EXPIRE_DATE" ItemStyle-HorizontalAlign="Center"
                                SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" SortExpression="TITLE_NAME"
                                UniqueName="TITLE_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày cấp văn bằng chứng chỉ" DataField="DEGREE_DATE" ItemStyle-HorizontalAlign="Center"
                                SortExpression="DEGREE_DATE" UniqueName="DEGREE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Chương trình đào tạo" DataField="PROGRAM_TRAINING" SortExpression="PROGRAM_TRAINING"
                                UniqueName="PROGRAM_TRAINING" />
                            <tlk:GridBoundColumn HeaderText="Hình thức đào tạo" DataField="TRAINNING_NAME" SortExpression="TRAINNING_NAME"
                                UniqueName="TRAINNING_NAME" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị đào tạo" DataField="UNIT" SortExpression="UNIT" UniqueName="UNIT" />
                            <tlk:GridBoundColumn HeaderText="Bằng cấp/chứng chỉ" DataField="CERTIFICATE" SortExpression="CERTIFICATE"
                                UniqueName="CERTIFICATE" />
                            <tlk:GridBoundColumn HeaderText="Địa điểm" DataField="LOCATION" SortExpression="LOCATION" UniqueName="LOCATION" />
                            <tlk:GridBoundColumn HeaderText="Chi phí" DataField="COST" SortExpression="COST" UniqueName="COST" />
                            <tlk:GridBoundColumn HeaderText="Kết quả đào tạo" DataField="RESULT_TRAIN" SortExpression="RESULT_TRAIN"
                                UniqueName="RESULT_TRAIN" />
                            <tlk:GridDateTimeColumn HeaderText="Thời hạn văn bằng chứng chỉ" DataField="DEGREE_EXPIRE_DATE" ItemStyle-HorizontalAlign="Center"
                                SortExpression="DEGREE_EXPIRE_DATE" UniqueName="DEGREE_EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" SortExpression="REMARK" UniqueName="REMARK" />--%>
         </Columns>                    
    </MasterTableView>
</tlk:RadGrid>