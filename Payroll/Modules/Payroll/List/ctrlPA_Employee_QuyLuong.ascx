<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Employee_QuyLuong.ascx.vb"
    Inherits="Payroll.ctrlPA_Employee_QuyLuong" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
           <%-- <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarSalaryGroups" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>--%>
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="Label5" runat="server" Text="Năm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboYearSearch" AutoPostBack="true"></tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label6" runat="server" Text="kỳ lương"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPeriodSearch"></tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label2" runat="server" Text="Đơn vị quỹ lương" AutoPostBack="true"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboDonVi"></tlk:RadComboBox>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
                    <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                        <table class="table-form">
                            <tr>
                                <td class="item-head" colspan="6">
                                    <%# Translate("Danh sách nhân viên chưa gán")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="ntxtNotEmp" runat="server" ReadOnly="true"></tlk:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                        <tlk:RadGrid ID="rgNotEmp" runat="server" Height="93%" AllowSorting="false">
                            <MasterTableView DataKeyNames="ID" AllowPaging="false">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                                        UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" HeaderStyle-Width="180px" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
                                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" HeaderStyle-Width="180px" />
                                </Columns>
                            </MasterTableView>
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        </tlk:RadGrid>
                    </tlk:RadPane>
                    <tlk:RadPane ID="RadPane5" runat="server" Scrolling="None" Width="50px">
                        <table class="table-form">
                            <tr style="height: 50px">
                            </tr>
                            <tr>
                                <td>
                                    <tlk:RadButton ID="btnInsert" runat="server" Font-Bold="true" Text=">" OnClientClicking="btnInsert_Click">
                                    </tlk:RadButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <tlk:RadButton ID="btnDelete" runat="server" Font-Bold="true" Text="<" OnClientClicking="btnDelete_Click"
                                        CausesValidation="false">
                                    </tlk:RadButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <tlk:RadButton ID="btnInsertALL" runat="server" Font-Bold="true" Text=">>">
                                    </tlk:RadButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <tlk:RadButton ID="btnDeleteALL" runat="server" Font-Bold="true" Text="<<" CausesValidation="false">
                                    </tlk:RadButton>
                                </td>
                            </tr>
                        </table>
                    </tlk:RadPane>
                    <tlk:RadPane ID="RadPane6" runat="server" Scrolling="None">
                        <table class="table-form">
                            <tr>
                                <td class="item-head" colspan="6">
                                    <%# Translate("Danh sách nhân viên đã gán")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="ntxtEmp" runat="server" ReadOnly="true"></tlk:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                        <tlk:RadGrid ID="rgEmp" runat="server" Height="93%" AllowSorting="false">
                            <MasterTableView DataKeyNames="ID" AllowPaging="false">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="90px" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                                        UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" HeaderStyle-Width="150px" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" HeaderStyle-Width="180px" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
                                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" HeaderStyle-Width="150px" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị quỹ lương %>" DataField="DONVI_QUYLUONG_NAME"
                                        UniqueName="DONVI_QUYLUONG_NAME" SortExpression="DONVI_QUYLUONG_NAME" HeaderStyle-Width="150px" />
                                </Columns>
                            </MasterTableView>
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        </tlk:RadGrid>
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT" || item.get_commandName() == "EXPORT_TEMP") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= MainPane.ClientID%>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= MainPane.ClientID%>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }

        function btnInsert_Click(sender, args) {
            var bCheck = $find('<%# rgNotEmp.ClientID%>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }

        }

        function btnDelete_Click(sender, args) {
            var bCheck = $find('<%# rgEmp.ClientID%>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }

        }

    </script>
</tlk:RadCodeBlock>
