<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlWorking.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlWorking" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal" SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" AllowFilteringByColumn="true" Height="100%">
            <MasterTableView DataKeyNames="ID,ORG_DESC">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="Loại quyết định" DataField="DECISION_TYPE_NAME"
                        UniqueName="DECISION_TYPE_NAME" SortExpression="DECISION_TYPE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Bộ phận" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Loại hình lao động" DataField="OBJECT_LABOR_TITLE" Visible="false"
                        UniqueName="OBJECT_LABOR_TITLE" SortExpression="OBJECT_LABOR_TITLE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Người quản lý trực tiếp" DataField="DIRECT_MANAGER_NAME"
                        UniqueName="DIRECT_MANAGER_NAME" SortExpression="DIRECT_MANAGER_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                        UniqueName="DECISION_NO" SortExpression="DECISION_NO">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE"
                        UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="Đơn vị ký quyết định " DataField="SIGN_ORG_NAME" Visible="false"
                        UniqueName="SIGN_ORG_NAME" SortExpression="SIGN_ORG_NAME">
                    </tlk:GridBoundColumn>
                   <tlk:GridBoundColumn HeaderText="Người ký" DataField="SIGN_NAME" Visible="false"
                        UniqueName="SIGN_NAME" SortExpression="SIGN_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Chức danh người ký" DataField="SIGN_TITLE_NAME" Visible="false"
                        UniqueName="SIGN_TITLE_NAME" SortExpression="SIGN_TITLE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Loại hình lao động" DataField="OBJECT_LABORNAME"
                        UniqueName="OBJECT_LABORNAME" SortExpression="OBJECT_LABORNAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Đối tượng chấm công" DataField="OBJECT_ATTENDANCE_NAME"
                        UniqueName="OBJECT_ATTENDANCE_NAME" SortExpression="OBJECT_ATTENDANCE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Đối tượng nhân viên" DataField="OBJECT_EMPLOYEE_NAME"
                        UniqueName="OBJECT_EMPLOYEE_NAME" SortExpression="OBJECT_EMPLOYEE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Thời gian làm việc" DataField="OBJECT_ATTENDANT_NAME"
                        UniqueName="OBJECT_ATTENDANT_NAME" SortExpression="OBJECT_ATTENDANT_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Nơi làm việc" DataField="WORK_PLACE_NAME"
                        UniqueName="WORK_PLACE_NAME" SortExpression="WORK_PLACE_NAME">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE"
                        UniqueName="NOTE" SortExpression="NOTE">
                    </tlk:GridBoundColumn>
                </Columns>
                <HeaderStyle Width="160px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>