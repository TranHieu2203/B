<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Salary_QuyLuong.ascx.vb"
    Inherits="Payroll.ctrlPA_Salary_QuyLuong" %>
<%@ Import Namespace="Framework.UI" %>
    <link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="220px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryGroups" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="Năm"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboYear" AutoPostBack="true"></tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="Đơn vị lương"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboDonVi"></tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label3" runat="server" Text="Kỳ lương"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboPeriod"></tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label4" runat="server" Text="Số tiền"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="ntxtSalary" SkinID="Money"></tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lbRemark"  Text ="Ghi chú"></asp:Label>
                </td>
                <td colspan="15">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin tìm kiếm")%></b>
                    <hr />
                </td>
            </tr>
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
                <td>
                    <tlk:RadButton runat="server" Text="Tìm" ID="btnSearch" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,PERIOD_ID,DONVI_QUYLUONG_ID,SALARY,REMARK,YEAR">
                <Columns>
                       <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Mã đơn vị" DataField="CODE" SortExpression="CODE"
                        UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="Đơn vị quỹ lương" DataField="DONVI_QUYLUONG_NAME" SortExpression="DONVI_QUYLUONG_NAME"
                        UniqueName="DONVI_QUYLUONG_NAME" />
                    <tlk:GridNumericColumn HeaderText="Số tiền" DataField="SALARY" SortExpression="SALARY"
                        UniqueName="SALARY" DataFormatString="{0:N0}" />
                    <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" SortExpression="REMARK"
                        UniqueName="REMARK" />
                    <tlk:GridBoundColumn HeaderText="Kỳ lương" DataField="PERIOD_NAME"
                        SortExpression="PERIOD_NAME" UniqueName="PERIOD_NAME" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
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
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
