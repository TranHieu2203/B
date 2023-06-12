<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlComitee.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlComitee" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal" SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" AllowFilteringByColumn="true" Height="100%">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                        SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban tại HĐ, UB" DataField="ORG_NAME"
                        SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="350px" />
                    <tlk:GridBoundColumn HeaderText="Vị trí công việc tại HĐ, UB" DataField="TITLE_NAME"
                        SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="250px" />
                    <tlk:GridDateTimeColumn HeaderText="Ngày quyết định" DataField="FROM_DATE"
                        ItemStyle-HorizontalAlign="Center" SortExpression="FROM_DATE" UniqueName="FROM_DATE"
                        DataFormatString="{0:dd/MM/yyyy}">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="Ngày thôi nhiệm" DataField="TO_DATE"
                        ItemStyle-HorizontalAlign="Center" SortExpression="TO_DATE" UniqueName="TO_DATE"
                        DataFormatString="{0:dd/MM/yyyy}">
                    </tlk:GridDateTimeColumn>
                    <%--<tlk:GridBoundColumn HeaderText="Phòng ban tại đơn vị gốc" DataField="EMPLOYEE_ORG"
                        SortExpression="EMPLOYEE_ORG" UniqueName="EMPLOYEE_ORG" HeaderStyle-Width="350px" />
                    <tlk:GridBoundColumn HeaderText="Vị trí công việc tại tập đoàn" DataField="EMPLOYEE_TITLE"
                        SortExpression="EMPLOYEE_TITLE" UniqueName="EMPLOYEE_TITLE" HeaderStyle-Width="250px" />--%>
                    <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME"
                        SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                    <%--<tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK"
                        SortExpression="REMARK" UniqueName="REMARK" />--%>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlComitee_RadSplitter2');
        //        }

    </script>
</tlk:RadScriptBlock>