<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CVPoolDtlTraining.ascx.vb"
    Inherits="Recruitment.ctrlRC_CVPoolDtlTraining" %>
<%@ Register Src="../Shared/ctrlRC_CanBasicInfo.ascx" TagName="ctrlRC_CanBasicInfo"
    TagPrefix="Recruitment" %>
<asp:HiddenField ID="hidTrainSinger" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Orientation="Horizontal" Style="height: 100%;
    width: 100%; overflow: Auto;">
    <tlk:RadPane ID="RadPane2" runat="server" Height="50px" Scrolling="None">
        <Recruitment:ctrlRC_CanBasicInfo runat="server" ID="ctrlRC_CanBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling=None>
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPaneLeft" runat="server" Height="150px">
        <%--   <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />--%>
        <table class="table-form" style="padding-left: 32px">
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Tên trường")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSchoolName">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="text-align: left">
                    <%# Translate("Ngành học")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBranchName" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td style="text-align: left" class="lb">
                    <%# Translate("Trình độ")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtLevel">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Văn bằng/chứng chỉ")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCERTIFICATE">
                    </tlk:RadTextBox>
                </td>
                <td style="text-align: left" class="lb">
                    <%# Translate("Nội dung đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtContent">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="text-align: left">
                    <%# Translate("Hệ đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTraining" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromdate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td style="text-align: left" class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTodate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CustomValidator ID="cval_FromDate_ToDate" runat="server" ErrorMessage="<%$ Translate: Đến ngày phải lớn hơn từ ngày. %>"
                        ToolTip="<%$ Translate: Đến ngày phải lớn hơn từ ngày. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb" style="text-align: left">
                    <%# Translate("Năm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox MinValue="0" ID="rntxYeah" runat="server" DataType="System.Int32">
                        <NumberFormat DecimalDigits="0" KeepNotRoundedValue="True" KeepTrailingZerosOnFocus="True"
                            ZeroPattern="n" GroupSeparator="" GroupSizes="1" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="text-align: left">
                    <%# Translate("Xếp loại")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRank" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td style="text-align: left" class="lb">
                    <%# Translate("Chi phí")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxCost">
                        <NumberFormat DecimalDigits="0" ZeroPattern="n" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" style="text-align: left">
                    <%# Translate("Ghi chú")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRemark" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="100%">
        <tlk:RadGrid ID="rgSingleTranning" runat="server" AllowMultiRowSelection="true" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="SCHOOL_NAME,BRANCH_NAME,LEVEL,CERTIFICATE,CONTENT,TRAINNING,FROMDATE,TODATE,RANK,YEAR_GRADUATE,COST,REMARK">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SCHOOL_NAME" HeaderText="<%$ Translate:   Tên trường %>"
                        UniqueName="SCHOOL_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BRANCH_NAME" HeaderText="<%$ Translate: Ngành học%>"
                        UniqueName="BRANCH_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="LEVEL" HeaderText="<%$ Translate: Trình độ%>" UniqueName="LEVEL">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="<%$ Translate: Văn bằng/Chứng chỉ%>"
                        UniqueName="CERTIFICATE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CONTENT" HeaderText="<%$ Translate: Nội dung đào tạo%>"
                        UniqueName="CONTENT">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TRAINNING" HeaderText="<%$ Translate: Hệ ĐT%>" UniqueName="TRAINNING">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FROMDATE" HeaderText="<%$ Translate: Từ ngày%>" DataFormatString="{0:dd/MM/yyyy}"
                        UniqueName="FROMDATE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TODATE" HeaderText="<%$ Translate: Đến ngày%>" DataFormatString="{0:dd/MM/yyyy}"
                        UniqueName="TODATE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="RANK" HeaderText="<%$ Translate: Xếp loại%>" UniqueName="RANK">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="YEAR_GRADUATE" HeaderText="<%$ Translate: Năm tốt nghiệp%>"
                        UniqueName="YEAR_GRADUATE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="COST" HeaderText="<%$ Translate: Chi phí%>" UniqueName="COST">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="REMARK" HeaderText="<%$ Translate: Ghi chú%>" UniqueName="REMARK">
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
