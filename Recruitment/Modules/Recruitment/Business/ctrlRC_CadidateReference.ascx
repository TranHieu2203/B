<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CadidateReference.ascx.vb" Inherits="Recruitment.ctrlRC_CadidateReference" %>
<%@ Register Src="../Shared/ctrlRC_CanBasicInfo.ascx" TagName="ctrlRC_CanBasicInfo"
    TagPrefix="Recruitment" %>
<asp:HiddenField ID="hidReference" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Orientation="Horizontal" Style="height: 100%;
    width: 100%; overflow: Auto;">
    <tlk:RadPane ID="RadPane2" runat="server" Height="50px" Scrolling="None">
        <Recruitment:ctrlRC_CanBasicInfo runat="server" ID="ctrlRC_CanBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPaneLeft" runat="server" Height="150px">
        <%--   <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />--%>
        <table class="table-form" style="padding-left: 32px">
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtHoTen">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="text-align: left">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChucDanh" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td style="text-align: left" class="lb">
                    <%# Translate("Đơn vị công tác")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtDonViCongTac">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Địa chỉ liên hệ")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtDiaChiLienHe">
                    </tlk:RadTextBox>
                </td>
                <td style="text-align: left" class="lb">
                    <%# Translate("Số điện thoại")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="txtSoDienThoai" MinValue="0">
                        <NumberFormat AllowRounding="false" GroupSeparator="" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" style="text-align: left">
                   
                </td>
                <td>
                   
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="100%">
        <tlk:RadGrid ID="rgReference" runat="server" AllowMultiRowSelection="true" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CANDIDATE_ID">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FULLNAME" HeaderText="<%$ Translate:   Họ và Tên %>"
                        UniqueName="FULLNAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TITLE_NAME" HeaderText="<%$ Translate: Chức danh%>"
                        UniqueName="TITLE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="WORK_UNIT" HeaderText="<%$ Translate: Đơn vị công tác%>"
                        UniqueName="WORK_UNIT">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ADDRESS_CONTACT" HeaderText="<%$ Translate: Địa chỉ liên hệ%>"
                        UniqueName="ADDRESS_CONTACT">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="PHONENUMBER" HeaderText="<%$ Translate: Số điện thoại%>"
                        UniqueName="PHONENUMBER">
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function RaiseCommand(sender, eventArgs) {
            var item = eventArgs.get_commandName()
            if (item == "ExportToExcel") {
                enableAjax = false;
            }
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter2.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPaneLeft.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter2.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPaneLeft.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%# RadPane3.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - 103 - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
