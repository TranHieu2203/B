<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCanDtlFamily.ascx.vb"
    Inherits="Recruitment.ctrlCanDtlFamily" %>
<%@ Register Src="../Shared/ctrlCanBasicInfo.ascx" TagName="ctrlCanBasicInfo" TagPrefix="Recruitment" %>
<asp:HiddenField ID="hidFamilyID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="70px" Scrolling="None">
        <Recruitment:ctrlCanBasicInfo runat="server" ID="ctrlCanBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPaneLeft" runat="server" Height="220px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" style="padding-left: 32px">
            <tr>
                <td class="lb">
                    <%# Translate("Họ tên")%><span class="lbReq"> *</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFullName">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqFullname" ControlToValidate="txtFullName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập họ tên %>" ToolTip="<%$ Translate: Bạn phải nhập họ tên  %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Quan hệ")%><span class="lbReq"> *</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboRelationship" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalRelationship" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn quan hệ %>"
                        ToolTip="<%$ Translate: Bạn phải chọn quan hệ %>" ClientValidationFunction="cvalRelationship">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày sinh (dd/mm/yyyy)")%><span class="lbReq"> *</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtDay" Width="40px" MinValue="1" MaxValue="31"
                        NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MaxLength="2">
                    </tlk:RadNumericTextBox>
                    &nbsp;&nbsp;&nbsp;
                    <tlk:RadNumericTextBox runat="server" ID="rntxtMonth" Width="40px" MinValue="1" MaxValue="12"
                        NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" MaxLength="2">
                    </tlk:RadNumericTextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" Width="50px" MinValue="1910"
                        NumberFormat-GroupSeparator="" NumberFormat-DecimalDigits="0" MaxLength="4">
                    </tlk:RadNumericTextBox>
                    <asp:CustomValidator ID="cvalBirthDate" runat="server">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Số CMND")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtIDNO" SkinID="Textbox15">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cvalIDNO" runat="server" ErrorMessage="<%$ Translate: Số CMND không được phép trùng %>"
                        ToolTip="<%$ Translate: Số CMND không được phép trùng %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nghề nghiệp")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtJob">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("ĐV công tác")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCompany">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkIsDeduct" AutoPostBack="true" Text="<%$ Translate : Đối tượng giảm trừ  %>" />
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkIsDied" Text="<%$ Translate : Đã mất  %>" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày giảm trừ")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductFrom">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày kết thúc")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductTo">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="compareDeductTo_DeductFrom" runat="server" ErrorMessage="<%$ Translate: Ngày kết thúc giảm trừ phải lớn hơn ngày bắt đầu %>"
                        ToolTip="<%$ Translate: Ngày kết thúc giảm trừ phải lớn hơn ngày bắt đầu %>"
                        ControlToValidate="rdDeductTo" ControlToCompare="rdDeductFrom" Operator="GreaterThanEqual"
                        Type="Date">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td colspan="3">
                    <asp:CheckBox runat="server" ID="chkIsAlien" AutoPostBack="true" Text="<%$ Translate : Thân nhân là người nước ngoài%>" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa chỉ")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtAdress" TextMode="MultiLine" Width="100%" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgFamily" runat="server" AllowMultiRowSelection="true" Height="100%" AllowPaging="true" PageSize="50">
            <MasterTableView DataKeyNames="ID">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn HeaderStyle-Width="40px">
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="Candidate_ID" HeaderText="Candidate_ID" UniqueName="Candidate_ID"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="IS_DEDUCT" HeaderText="IS_DEDUCT" UniqueName="IS_DEDUCT"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_DAY" HeaderText="BIRTH_DAY" UniqueName="BIRTH_DAY"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_MONTH" HeaderText="BIRTH_MONTH" UniqueName="BIRTH_MONTH"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_YEAR" HeaderText="BIRTH_YEAR" UniqueName="BIRTH_YEAR"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="IS_WORK_OPPOSITE" HeaderText="IS_WORK_OPPOSITE" UniqueName="IS_WORK_OPPOSITE"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="RELATION_ID" HeaderText="RELATION_ID" UniqueName="RELATION_ID"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ADDRESS" HeaderText="ADDRESS" UniqueName="ADDRESS"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="IS_ALIEN" HeaderText="IS_ALIEN" UniqueName="IS_ALIEN"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="RELATION_NAME" HeaderText="<%$ Translate : Quan hệ %>"
                        UniqueName="RELATION_NAME" Visible="True">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FULLNAME" HeaderText="<%$ Translate : Họ và tên%>"
                        UniqueName="FULLNAME" Visible="True">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridTemplateColumn UniqueName="BIRTH_DATE" HeaderText="<%$ Translate : Ngày sinh%>"
                        ReadOnly="True" Resizable="False">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblBirthDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn DataField="ID_NO" HeaderText="<%$ Translate : CMND%>" UniqueName="ID_NO"
                        Visible="True" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridTemplateColumn UniqueName="IS_DEDUCT" HeaderText="<%$ Translate : Giảm trừ%>"
                        ReadOnly="True" Resizable="False">
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIS_DEDUCT" runat="server" Checked='<%# Eval("IS_DEDUCT") %>'
                                Enabled="false" />
                        </ItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn DataField="DEDUCT_FROM" HeaderText="<%$ Translate : Ngày giảm trừ%>"
                        UniqueName="DEDUCT_FROM" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="DEDUCT_TO" HeaderText="<%$ Translate : Ngày kết thúc %>"
                        UniqueName="DEDUCT_TO" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridTemplateColumn UniqueName="IS_DIED" HeaderText="<%$ Translate : Đã mất %>"
                        ReadOnly="true" Resizable="False">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIS_DIED" runat="server" Checked='<%# Eval("IS_DIED") %>' Enabled="false" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn DataField="JOB" HeaderText="<%$ Translate : Nghề nghiệp %>"
                        UniqueName="JOB" Visible="True" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="COMPANY" HeaderText="<%$ Translate : Đơn vị công tác %>"
                        UniqueName="COMPANY" Visible="True" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnablePostBackOnRowClick="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cvalRelationship(oSrc, args) {
            var cbo = $find("<%# cboRelationship.ClientID%>");
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
