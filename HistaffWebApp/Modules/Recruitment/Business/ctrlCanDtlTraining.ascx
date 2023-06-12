<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCanDtlTraining.ascx.vb"
    Inherits="Recruitment.ctrlCanDtlTraining" %>
<%@ Register Src="~/Modules/Recruitment/Shared/ctrlCanBasicInfo.ascx" TagName="ctrlCanBasicInfo"
    TagPrefix="Recruitment" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="ToolbarPane" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="70px" Scrolling="None">
        <Recruitment:ctrlCanBasicInfo runat="server" ID="ctrlCanBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="235px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <fieldset>
            <legend>
                <%# Translate("Đào tạo")%></legend>
            <table class="table-form">
                <tr>
                    <td class="lb" style="width: 110px">
                        <%# Translate("Từ tháng")%><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rnbFMonth" runat="server" Width="50px" MinValue="1" MaxValue="12"
                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4">
                        </tlk:RadNumericTextBox>/
                        <tlk:RadNumericTextBox ID="rnbFYear" runat="server" Width="50px" MinValue="1900"
                            MaxValue="2100" NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4">
                        </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="reqFMonth" ControlToValidate="rnbFMonth" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập tháng %>" ToolTip="<%$ Translate: Bạn phải nhập tháng %>">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="reqFYear" ControlToValidate="rnbFYear" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập năm %>" ToolTip="<%$ Translate: Bạn phải nhập năm %>">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb" style="width: 100px">
                        <%# Translate("Đến tháng")%> <span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rnbTMonth" runat="server" Width="50px" MinValue="1" MaxValue="12"
                            NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4">
                        </tlk:RadNumericTextBox>/
                        <tlk:RadNumericTextBox ID="rnbTYear" runat="server" Width="50px" MinValue="1900"
                            MaxValue="2100" NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="4">
                        </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="reqTMonth" ControlToValidate="rnbTMonth" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập tháng %>" ToolTip="<%$ Translate: Bạn phải nhập tháng %>">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="reqTYear" ControlToValidate="rnbTYear" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập năm %>" ToolTip="<%$ Translate: Bạn phải nhập năm %>">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 95px">
                        <%# Translate("Tên trường đào tạo")%>
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox ID="txtTrainingSchool" runat="server" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Hình thức đào tạo")%>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboTrainingForm" runat="server">
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Trình độ học vấn")%>
                        <span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboLearningLevel" runat="server">
                        </tlk:RadComboBox>
                        <asp:CustomValidator ID="cusLearningLevel" runat="server" ErrorMessage="<%$ Translate: Bạn chưa chọn trình độ học vấn %>"
                            ToolTip="<%$ Translate: Bạn chưa chọn trình độ học vấn %>" ClientValidationFunction="cusLearningLevel">
                        </asp:CustomValidator>
                    </td>
                    <td class="lb">
                        <%# Translate("Chuyên ngành")%>
                        <span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboMajor" runat="server">
                        </tlk:RadComboBox>
                        <asp:CustomValidator ID="cusMajor" runat="server" ErrorMessage="<%$ Translate: Bạn chưa chọn chuyên ngành %>"
                            ToolTip="<%$ Translate: Bạn chưa chọn chuyên ngành %>" ClientValidationFunction="cusMajor">
                        </asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Năm tốt nghiệp")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox runat="server" ID="rntGraduateYear" NumberFormat-DecimalDigits="0"
                            NumberFormat-GroupSizes="4" MinValue="1900" MaxValue="9999">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Xếp loại")%>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboMark" runat="server">
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Nội dung đào tạo")%>
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox ID="rtbTrainingContent" runat="server" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="RadPane4" Height="100%" Scrolling="None">
        <tlk:RadGrid ID="rgCandidateTrain" runat="server" AllowMultiRowSelection="true" Height="100%" AllowPaging="true" PageSize="50"
            AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="FMONTH, FYEAR, TMONTH, TYEAR, FROM_DATE, TO_DATE, SCHOOL_NAME, TRAINING_FORM, LEARNING_LEVEL, MAJOR, GRADUATE_YEAR, MARK, TRAINING_CONTENT">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FMONTH" HeaderText="FMONTH" UniqueName="FMONTH" Visible="false">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FYEAR" HeaderText="FYEAR" UniqueName="FYEAR" Visible="false">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TMONTH" HeaderText="TMONTH" UniqueName="TMONTH" Visible="false">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TYEAR" HeaderText="TYEAR" UniqueName="TYEAR" Visible="false">
                    </tlk:GridBoundColumn>
                    <tlk:GridTemplateColumn UniqueName="FROM_MONTH_YEAR" HeaderText="<%$ Translate : Từ tháng%>">
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblFromMonthYear" runat="server"></asp:Label>
                        </ItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn UniqueName="TO_MONTH_YEAR" HeaderText="<%$ Translate : Đến tháng%>">
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblToMonthYear" runat="server"></asp:Label>
                        </ItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn DataField="SCHOOL_NAME" HeaderText="<%$ Translate: Tên trường%>"
                        UniqueName="SCHOOL_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TRAINING_FORM" UniqueName="TRAINING_FORM" Visible="false">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TRAINING_FORM_NAME" HeaderText="<%$ Translate: Hình thức đào tạo%>"
                        UniqueName="TRAINING_FORM_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="LEARNING_LEVEL" HeaderText="<%$ Translate: Trình độ học vấn%>"
                        UniqueName="LEARNING_LEVEL" Visible="false">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="MAJOR" HeaderText="<%$ Translate: Chuyên ngành%>"
                        UniqueName="MAJOR" Visible="false">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="MAJOR_NAME" HeaderText="<%$ Translate: Chuyên ngành%>"
                        UniqueName="MAJOR_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="GRADUATE_YEAR" HeaderText="<%$ Translate: Năm tốt nghiệp%>"
                        UniqueName="GRADUATE_YEAR" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="EqualTo" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="MARK" HeaderText="Xếp loại" UniqueName="MARK" Visible="false">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="MARK_NAME" HeaderText="<%$ Translate: Xếp loại%>"
                        UniqueName="MARK_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TRAINING_CONTENT" HeaderText="<%$ Translate: Nội dung đào tạo%>"
                        UniqueName="TRAINING_CONTENT" Visible="false">
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusLearningLevel(oSrc, args) {
            var cbo = $find("<%# cboLearningLevel.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusMajor(oSrc, args) {
            var cbo = $find("<%# cboMajor.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
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
                var pane2 = splitter.getPaneById('<%# RadPane4.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - 70 - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - 35 - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
