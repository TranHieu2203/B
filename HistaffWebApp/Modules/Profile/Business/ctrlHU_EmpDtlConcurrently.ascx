<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlConcurrently.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlConcurrently" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" AllowFilteringByColumn="true"
            Height="100%">
            <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS,EXPIRE_DATE_CON" ClientDataKeyNames="ID,EMPLOYEE_ID,STATUS,EXPIRE_DATE_CON,ORG_ID_DESC,ORG_CON,TITLE_CON">
                        <Columns>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                             <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE_CON"  HeaderStyle-Width="150px"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE_CON" UniqueName="EFFECT_DATE_CON"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE_CON"  HeaderStyle-Width="150px"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE_CON" UniqueName="EXPIRE_DATE_CON"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Phòng ban kiêm nhiệm" DataField="ORG_CON_NAME"
                                SortExpression="ORG_CON_NAME" UniqueName="ORG_CON_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="Vị trí kiêm nhiệm" DataField="TITLE_CON_NAME"
                                SortExpression="TITLE_CON_NAME" UniqueName="TITLE_CON_NAME" HeaderStyle-Width="150px" />
                            <%--<tlk:GridBoundColumn HeaderText="Chức vụ kiêm nhiệm" DataField="TITLE_CON_NAME1"
                                SortExpression="TITLE_CON_NAME1" UniqueName="TITLE_CON_NAME1" HeaderStyle-Width="150px" />--%>
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK"
                                SortExpression="REMARK" UniqueName="REMARK" HeaderStyle-Width="150px" />
                           
                            <%--<tlk:GridBoundColumn HeaderText="Phụ cấp kiêm nhiệm" DataField="ALLOW_MONEY_NUMBER"  HeaderStyle-Width="150px"
                                SortExpression="ALLOW_MONEY_NUMBER" UniqueName="ALLOW_MONEY_NUMBER" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="Người ký quyết định kiêm nhiệm 1" DataField="SIGN_NAME"
                                SortExpression="SIGN_NAME" UniqueName="SIGN_NAME" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="Chức danh quyết định kiêm nhiệm 1" DataField="SIGN_TITLE_NAME"
                                SortExpression="SIGN_TITLE_NAME" UniqueName="SIGN_TITLE_NAME" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="Người ký quyết định kiêm nhiệm 2" DataField="SIGN_NAME2"
                                SortExpression="SIGN_NAME2" UniqueName="SIGN_NAME2" HeaderStyle-Width="120px" />  
                            <tlk:GridBoundColumn HeaderText="Chức danh quyết định kiêm nhiệm 2" DataField="SIGN_TITLE_NAME2"
                                SortExpression="SIGN_TITLE_NAME2" UniqueName="SIGN_TITLE_NAME2" HeaderStyle-Width="120px" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày thôi kiêm nhiệm" DataField="EFFECT_DATE_STOP"  HeaderStyle-Width="150px"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE_STOP" UniqueName="EFFECT_DATE_STOP"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Người ký quyết định thôi kiêm nhiệm 1" DataField="SIGN_NAME_STOP"
                                SortExpression="SIGN_NAME_STOP" UniqueName="SIGN_NAME_STOP" HeaderStyle-Width="120px" /> 
                            <tlk:GridBoundColumn HeaderText="Chức danh quyết định thôi kiêm nhiệm 1" DataField="SIGN_TITLE_NAME_STOP"
                                SortExpression="SIGN_TITLE_NAME_STOP" UniqueName="SIGN_TITLE_NAME_STOP" HeaderStyle-Width="120px" />  
                            <tlk:GridBoundColumn HeaderText="Người ký quyết định thôi kiêm nhiệm 2" DataField="SIGN_NAME_STOP2"
                                SortExpression="SIGN_NAME_STOP2" UniqueName="SIGN_NAME_STOP2" HeaderStyle-Width="120px" />  
                            <tlk:GridBoundColumn HeaderText="Chức danh quyết định thôi kiêm nhiệm 2" DataField="SIGN_TITLE_NAME_STOP2"
                                SortExpression="SIGN_TITLE_NAME_STOP2" UniqueName="SIGN_TITLE_NAME_STOP2" HeaderStyle-Width="120px" /> --%>
                        </Columns>
                    </MasterTableView>
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
        //            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlDiscipline_RadSplitter2');
        //        }

    </script>
</tlk:RadScriptBlock>
