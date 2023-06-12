<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CommendCollectDetail.ascx.vb"
    Inherits="Profile.ctrlHU_CommendCollectDetail" %>
<%@ Import Namespace="Common" %>
<tlk:RadGrid ID="rgCommend" runat="server" Height="99%" AllowPaging="true" PageSize="50">
    <ClientSettings EnableRowHoverStyle="true">
        <Selecting AllowRowSelect="true" />
    </ClientSettings>
    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
            </tlk:GridClientSelectColumn>
            <tlk:GridBoundColumn DataField="ID" Visible="false" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng %>" DataField="Commend_OBJ_NAME"
                SortExpression="Commend_OBJ_NAME" UniqueName="Commend_OBJ_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="OBJ_ORG_NAME"
                SortExpression="OBJ_ORG_NAME" UniqueName="OBJ_ORG_NAME" Visible="false" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên nhân viên %>" DataField="EMPLOYEE_NAME"
                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị nhân viên %>" DataField="ORG_NAME"
                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh nhân viên %>" DataField="TITLE_NAME"
                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp khen thưởng %>" DataField="Commend_LEVEL_NAME"
                SortExpression="Commend_LEVEL_NAME" UniqueName="Commend_LEVEL_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Danh hiệu khen thưởng %>" DataField="COMMEND_TITLE_NAME"
                SortExpression="COMMEND_TITLE_NAME" UniqueName="COMMEND_TITLE_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do %>" DataField="REMARK" SortExpression="REMARK"
                UniqueName="REMARK" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức khen thưởng %>" DataField="Commend_TYPE_NAME"
                SortExpression="Commend_TYPE_NAME" UniqueName="Commend_TYPE_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức thưởng %>" DataField="MONEY"
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" SortExpression="MONEY"
                UniqueName="MONEY" />
            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                SortExpression="ORG_DESC" Visible="false" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức trả thưởng %>" DataField="COMMEND_PAY_NAME"
                SortExpression="COMMEND_PAY_NAME" UniqueName="COMMEND_PAY_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nguồn chi %>" DataField="POWER_PAY_NAME"
                SortExpression="POWER_PAY_NAME" UniqueName="POWER_PAY_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                ItemStyle-HorizontalAlign="Center" SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
        </Columns>
        <HeaderStyle Width="130px" />
    </MasterTableView>
</tlk:RadGrid>
<asp:HiddenField ID="hidEmp" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
    </script>
</tlk:RadCodeBlock>
