<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_BlackList.ascx.vb"
    Inherits="Profile.ctrlHU_BlackList" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarTerminates" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="68px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                        <asp:Label ID="lbFromSend" runat="server" Text="Ngày nộp đơn từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromSend" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                         <asp:Label ID="lbToSend" runat="server" Text="Đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToSend" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                         <asp:Label ID="lbFromLast" runat="server" Text="Ngày làm việc cuối cùng từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromLast" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                         <asp:Label ID="lbToLast" runat="server" Text="Đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToLast" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton runat="server" Text="Tìm" ID="btnSearch" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgTerminate" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,STATUS_CODE,IS_NOHIRE">
                        <Columns>
                       <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="STATUS_CODE" Visible="false" />
                            <tlk:GridBoundColumn DataField="IS_NOHIRE" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_DESC" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                                SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số CMND %>" DataField="ID_NO" SortExpression="ID_NO"
                                UniqueName="ID_NO" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày vào %>" DataField="JOIN_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="JOIN_DATE" UniqueName="JOIN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị/Phòng ban %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                           <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày nộp đơn %>" DataField="SEND_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="SEND_DATE" UniqueName="SEND_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                                 <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày thôi việc %>" DataField="EFFECT_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày làm việc cuối %>" DataField="LAST_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="LAST_DATE" UniqueName="LAST_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>--%>
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
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
        registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_BlackList_RadSplitter3');
    }

    var enableAjax = true;
    function onRequestStart(sender, eventArgs)
    {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }

    function clientButtonClicking(sender, args)
    {
        var bCheck;
        var n;
        var m;

        if (args.get_item().get_commandName() == "DELETE")
        {
            var bCheck = $find('<%= rgTerminate.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
            {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                args.set_cancel(true);
            }
        }

        if (args.get_item().get_commandName() == "EXPORT")
        {
            enableAjax = false;
        }
    }

</script>
