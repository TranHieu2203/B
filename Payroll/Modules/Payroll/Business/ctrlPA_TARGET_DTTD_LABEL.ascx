<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_TARGET_DTTD_LABEL.ascx.vb"
    Inherits="Payroll.ctrlPA_TARGET_DTTD_LABEL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%">
   <%-- <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>--%>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true" TabIndex="12" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                         <td class="lb">
                            <%# Translate("Kỳ công")%><span class="lbReq"></span>
                        </td>
                        <td>
                           <tlk:RadComboBox ID="cboPeriod" runat="server" SkinID="dDropdownList" >
                           </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Nhãn hàng")%><span class="lbReq"></span>
                        </td>
                        <td>
                           <tlk:RadComboBox ID="cboBrand" runat="server"  >
                           </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgDelegacy" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true" AllowMultiRowEdit="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID"
                        ClientDataKeyNames="ID" >
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <%--<tlk:GridBoundColumn HeaderText="Năm" DataField="YEAR"
                                SortExpression="YEAR" UniqueName="YEAR" HeaderStyle-Width="60px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Tháng" DataField="MONTH"
                                SortExpression="MONTH" UniqueName="MONTH" HeaderStyle-Width="150px" ReadOnly="true" />--%>
                            <tlk:GridBoundColumn HeaderText="Tháng Target/DTTĐ" DataField="SALEMONTH_NAME"
                                SortExpression="SALEMONTH_NAME" UniqueName="SALEMONTH_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Nhãn hàng" DataField="BRAND_NAME"
                                SortExpression="BRAND_NAME" UniqueName="BRAND_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Chỉ tiêu doanh số" DataField="TARGETBRAND"
                                SortExpression="TARGETBRAND" UniqueName="TARGETBRAND" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Tổng doanh thu" DataField="TOTALREVENUE"
                                SortExpression="TOTALREVENUE" UniqueName="TOTALREVENUE" HeaderStyle-Width="60px"  ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE"
                                SortExpression="NOTE" UniqueName="NOTE" HeaderStyle-Width="150px" ReadOnly="true" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:radsplitter>
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function OpenNew() {
            //var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_WorkInfoNewEdit&group=Business&noscroll=1', "rwPopup");
            window.open('/Default.aspx?mid=Payroll&fid=ctrlPA_ADDTAXNewEdit&group=Business&noscroll=1', "_self");
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgDelegacy.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgDelegacy.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            //var owindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_WorkInfoNewEdit&group=Business&noscroll=1&ID=' + id, "rwPopup");
            window.open('/Default.aspx?mid=Payroll&fid=ctrlPA_ADDTAXNewEdit&group=Business&noscroll=1&ID=' + id, "_self");
            return 2;
        }
        function OnClientButtonClicking(sender, args) {
            var m;
            var bCheck;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }

                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "NEXT"||args.get_item().get_commandName() == "IMPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "CHECK") {
                
                enableAjax = false;
            }
        }
        function gridRowDblClick(sender, eventArgs) {
            if (OpenEdit(true) == 0) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }
            if (OpenEdit(true) == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW)%>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }
            args.set_cancel(true);
        }
        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $get("<%= btnSearch.ClientId %>").click();
            }

        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:radcodeblock>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
