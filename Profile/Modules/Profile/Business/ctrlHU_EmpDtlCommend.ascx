<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlCommend.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlCommend" %>
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
                    <%--<tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                        UniqueName="DECISION_NO" SortExpression="DECISION_NO" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                   <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                        ShowFilterIcon="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        FilterControlWidth="100%">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="Cấp khen thưởng" DataField="COMMEND_LEVEL_NAME"
                        UniqueName="COMMEND_LEVEL_NAME" SortExpression="COMMEND_LEVEL_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Danh hiệu khen thưởng" DataField="COMMEND_TITLE_NAME"
                        UniqueName="COMMEND_TITLE_NAME" SortExpression="COMMEND_TITLE_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="30%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Năm" DataField="YEAR"
                        UniqueName="YEAR" SortExpression="YEAR" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="30%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Hình thức khen thưởng" DataField="COMMEND_TYPE_NAME"
                        UniqueName="COMMEND_TYPE_NAME" SortExpression="COMMEND_TYPE_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="30%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Hình thức trả thưởng" DataField="COMMEND_PAY_NAME"
                        UniqueName="COMMEND_PAY_NAME" SortExpression="COMMEND_PAY_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="30%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Mức thưởng" DataField="MONEY" UniqueName="MONEY"
                        SortExpression="MONEY" DataFormatString="{0:###,###,###,##0}" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" FilterControlWidth="100%">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Lý do khen thưởng" DataField="REMARK"
                        UniqueName="REMARK" SortExpression="REMARK" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Người ký" DataField="SIGNER_NAME"
                        UniqueName="SIGNER_NAME" SortExpression="SIGNER_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>--%>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
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

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlCommend_RadSplitter2');
        }

    </script>
</tlk:RadScriptBlock>
