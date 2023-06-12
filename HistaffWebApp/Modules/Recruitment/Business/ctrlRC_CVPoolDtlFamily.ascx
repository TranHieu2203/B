<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CVPoolDtlFamily.ascx.vb"
    Inherits="Recruitment.ctrlRC_CVPoolDtlFamily" %>
<%@ Register Src="../Shared/ctrlRC_CanBasicInfo.ascx" TagName="ctrlRC_CanBasicInfo"
    TagPrefix="Recruitment" %>
<asp:HiddenField ID="hidFamilyID" runat="server" />
<asp:HiddenField ID="hidCanID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="34px">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="33px" Scrolling="None">
        <Recruitment:ctrlRC_CanBasicInfo runat="server" ID="ctrlRC_CanBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPaneLeft" runat="server" Height="290px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" style="padding-left: 32px">
            <tr>
                <td class="lb">
                    <%# Translate("Họ tên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFullName">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqFullname" ControlToValidate="txtFullName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập họ tên %>" ToolTip="<%$ Translate: Bạn phải nhập họ tên  %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Quan hệ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboRelationship" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalRelationship" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn quan hệ %>"
                        ToolTip="<%$ Translate: Bạn phải chọn quan hệ %>" ClientValidationFunction="cvalRelationship">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày sinh (dd/mm/yyyy)")%><span class="lbReq">*</span>
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
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa chỉ thường trú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtPER_ADDRESS" Width="100%" />
                </td>
                <td class="lb">
                    <asp:CheckBox runat="server" ID="cbIsDied" Text="<%$ Translate : Đã mất  %>" />
                    -
                    <%# Translate("Ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="dtpDIED_DATE">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr style="border-top: 1px solid #CCC">
                <td class="lb">
                    <%# Translate("Số khai sinh")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="txtBIRTH_NO" />
                </td>
                <td class="lb">
                    <%# Translate("Quyển")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="txtBIRTH_BOOK" />
                </td>
                <td class="lb">
                    <%# Translate("Quốc tịch")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboBIRTH_NAT2">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nơi đăng ký: Quốc gia")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboBIRTH_NAT">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Tỉnh/Thành phố")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboBIRTH_PRO">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Quận/Huyện")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboBIRTH_DIS">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
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
                <td class="lb">
                    <%# Translate("Ngày cấp")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="dtpID_DATE">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Nơi cấp")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboID_PLACE">
                    </tlk:RadComboBox>
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
                    <%# Translate("Mã số thuế")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPIT_CODE">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cấp")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="dtpPIT_DATE">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("ĐV công tác")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCompany">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Địa chỉ CT")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtADDRESS">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbIsMLG" Text="<%$ Translate : Cùng công ty  %>" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtREMARK" TextMode="MultiLine" Width="100%" />
                </td>
                <td colspan="2">
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgFamily" runat="server" AllowMultiRowSelection="true" Height="100%">
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
                    <tlk:GridBoundColumn DataField="RELATION_ID" HeaderText="RELATION_ID" UniqueName="RELATION_ID"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="Candidate_ID" HeaderText="Candidate_ID" UniqueName="Candidate_ID"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="IS_DIED" HeaderText="IS_DIED" UniqueName="IS_DIED"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="IS_MLG" HeaderText="IS_MLG" UniqueName="IS_MLG" Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="DIED_DATE" HeaderText="DIED_DATE" UniqueName="DIED_DATE"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ADDRESS" HeaderText="ADDRESS" UniqueName="ADDRESS"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="REMARK" HeaderText="REMARK" UniqueName="REMARK" Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="PIT_CODE" HeaderText="PIT_CODE" UniqueName="PIT_CODE"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="PIT_DATE" HeaderText="PIT_DATE" UniqueName="PIT_DATE"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ID_DATE" HeaderText="ID_DATE" UniqueName="ID_DATE"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ID_PLACE" HeaderText="ID_PLACE" UniqueName="ID_PLACE"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="PER_ADDRESS" HeaderText="PER_ADDRESS" UniqueName="PER_ADDRESS"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_NO" HeaderText="BIRTH_NO" UniqueName="BIRTH_NO"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_BOOK" HeaderText="BIRTH_BOOK" UniqueName="BIRTH_BOOK"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_NAT" HeaderText="BIRTH_NAT" UniqueName="BIRTH_NAT"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_PRO" HeaderText="BIRTH_PRO" UniqueName="BIRTH_PRO"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_DIS" HeaderText="BIRTH_DIS" UniqueName="BIRTH_DIS"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_NAT2" HeaderText="BIRTH_NAT2" UniqueName="BIRTH_NAT2"
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
                    <tlk:GridBoundColumn DataField="BIRTH_DAY" HeaderText="<%$ Translate : Ngày sinh%>"
                        UniqueName="BIRTH_DAY">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_MONTH" HeaderText="<%$ Translate : Tháng sinh%>"
                        UniqueName="BIRTH_MONTH">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_YEAR" HeaderText="<%$ Translate : Năm sinh%>"
                        UniqueName="BIRTH_YEAR">
                    </tlk:GridBoundColumn>
                    <tlk:GridTemplateColumn UniqueName="BIRTH_DATE" HeaderText="<%$ Translate : Ngày sinh%>"
                        ReadOnly="True" Resizable="False" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblBirthDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn DataField="ID_NO" HeaderText="<%$ Translate : Số CMND%>" UniqueName="ID_NO"
                        Visible="True" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridTemplateColumn UniqueName="IS_DIED" HeaderText="<%$ Translate : Đã mất %>"
                        ReadOnly="true" Resizable="False" Visible="false">
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
                    <tlk:GridBoundColumn DataField="ADDRESS" HeaderText="<%$ Translate : Địa chỉ CT %>"
                        UniqueName="ADDRESS" Visible="True" EmptyDataText="">
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
                //ResizeSplitterDefault();
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
