<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_StoreDTTD.ascx.vb"
    Inherits="Payroll.ctrlPA_StoreDTTD" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" TabIndex="12" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                       <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="160px" AutoPostBack="true"
                                MaxLength="80" runat="server" ToolTip="">
                            </tlk:RadComboBox>
                        </td>
                        <td style="text-align: right;width:164px">
                            <%# Translate("Đối tượng nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboEmpObj" runat="server"  AutoPostBack="true"
                                >
                            </tlk:RadComboBox>
                        </td>  
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" SkinID="Readonly" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" SkinID="Readonly" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
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
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID"
                        ClientDataKeyNames="ID" EditMode="InPlace">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Cửa hàng" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                             <tlk:GridBoundColumn HeaderText="Nhãn hàng" DataField="BRAND_NAME"
                                SortExpression="BRAND_NAME" UniqueName="BRAND_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Doanh thu thực đạt" DataField="TARGET_ACTUAL"
                                SortExpression="TARGET_ACTUAL" UniqueName="TARGET_ACTUAL" HeaderStyle-Width="100px"  ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Chỉ tiêu doanh thu" DataField="TARGET_PLAN"
                                SortExpression="TARGET_PLAN" UniqueName="TARGET_PLAN" HeaderStyle-Width="100px"  ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Doanh thu tối thiểu" DataField="REVENUE_MIN"
                                SortExpression="REVENUE_MIN" UniqueName="REVENUE_MIN" HeaderStyle-Width="100px"  ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Mức hưởng Trường ca" DataField="BENEFIT_TRCA"
                                SortExpression="BENEFIT_TRCA" UniqueName="BENEFIT_TRCA" HeaderStyle-Width="100px"  ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Mức hưởng QLCH" DataField="BENEFIT_QLCH"
                                SortExpression="BENEFIT_QLCH" UniqueName="BENEFIT_QLCH" HeaderStyle-Width="100px"  ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE"
                                SortExpression="NOTE" UniqueName="NOTE" HeaderStyle-Width="200px"  ReadOnly="true" />
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
