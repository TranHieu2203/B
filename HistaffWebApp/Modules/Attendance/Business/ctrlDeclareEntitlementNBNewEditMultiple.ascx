<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDeclareEntitlementNBNewEditMultiple.ascx.vb"
    Inherits="Attendance.ctrlDeclareEntitlementNBNewEditMultiple" %>
    <link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin thâm niên")%><hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Năm khai báo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtYear" MaxLength="4" SkinID="Number" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtYear"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Năm khai báo %>" ToolTip="<%$ Translate: Bạn phải nhập Năm khai báo %>"> </asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvyear" MinimumValue="1901" MaximumValue="2999" ControlToValidate="rntxtYear"
                        runat="server" ErrorMessage="<%$ Translate: Năm khai báo phải nằm trong khoảng từ 1901 đến 2999 %>"
                        ToolTip="<%$ Translate: Năm khai báo phải nằm trong khoảng từ 1901 đến 2999 %>"> </asp:RangeValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tháng điều chỉnh")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStartMonth" runat="server" SkinID="dDropdownList">
                        <Items>
                            <tlk:RadComboBoxItem Text="" Value="" />
                            <tlk:RadComboBoxItem Text="1" Value="1" />
                            <tlk:RadComboBoxItem Text="2" Value="2" />
                            <tlk:RadComboBoxItem Text="3" Value="3" />
                            <tlk:RadComboBoxItem Text="4" Value="4" />
                            <tlk:RadComboBoxItem Text="5" Value="5" />
                            <tlk:RadComboBoxItem Text="6" Value="6" />
                            <tlk:RadComboBoxItem Text="7" Value="7" />
                            <tlk:RadComboBoxItem Text="8" Value="8" />
                            <tlk:RadComboBoxItem Text="9" Value="9" />
                            <tlk:RadComboBoxItem Text="10" Value="10" />
                            <tlk:RadComboBoxItem Text="11" Value="11" />
                            <tlk:RadComboBoxItem Text="12" Value="12" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
                <td class="lb" style="width: 150px">
                    <%# Translate("Số tháng điều chỉnh")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtADJUST_MONTH_TN2" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do điều chỉnh")%>
                </td>
                <td colspan="6">
                    <tlk:RadTextBox ID="txtREMARK_TN" Width="100%" SkinID="Textbox1023" TextMode="MultiLine"
                        runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin phép")%><hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Gia hạn đến tháng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStartCur" runat="server" SkinID="dDropdownList">
                        <Items>
                            <tlk:RadComboBoxItem Text="" Value="" />
                            <tlk:RadComboBoxItem Text="1" Value="1" />
                            <tlk:RadComboBoxItem Text="2" Value="2" />
                            <tlk:RadComboBoxItem Text="3" Value="3" />
                            <tlk:RadComboBoxItem Text="4" Value="4" />
                            <tlk:RadComboBoxItem Text="5" Value="5" />
                            <tlk:RadComboBoxItem Text="6" Value="6" />
                            <tlk:RadComboBoxItem Text="7" Value="7" />
                            <tlk:RadComboBoxItem Text="8" Value="8" />
                            <tlk:RadComboBoxItem Text="9" Value="9" />
                            <tlk:RadComboBoxItem Text="10" Value="10" />
                            <tlk:RadComboBoxItem Text="11" Value="11" />
                            <tlk:RadComboBoxItem Text="12" Value="12" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Số phép được thanh toán")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmENT_PAY" runat="server" SkinID="Decimal" NumberFormat-DecimalDigits="2">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tháng điều chỉnh phép")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboMonth" runat="server" SkinID="dDropdownList">
                        <Items>
                            <tlk:RadComboBoxItem Text="" Value="" />
                            <tlk:RadComboBoxItem Text="1" Value="1" />
                            <tlk:RadComboBoxItem Text="2" Value="2" />
                            <tlk:RadComboBoxItem Text="3" Value="3" />
                            <tlk:RadComboBoxItem Text="4" Value="4" />
                            <tlk:RadComboBoxItem Text="5" Value="5" />
                            <tlk:RadComboBoxItem Text="6" Value="6" />
                            <tlk:RadComboBoxItem Text="7" Value="7" />
                            <tlk:RadComboBoxItem Text="8" Value="8" />
                            <tlk:RadComboBoxItem Text="9" Value="9" />
                            <tlk:RadComboBoxItem Text="10" Value="10" />
                            <tlk:RadComboBoxItem Text="11" Value="11" />
                            <tlk:RadComboBoxItem Text="12" Value="12" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số phép điều chỉnh")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtADJUST_ENTITLEMENT2" runat="server" SkinID="Custom"
                        NumberFormat-DecimalDigits="1">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Lý do điều chỉnh")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtREMARK_EN" Width="100%" SkinID="Textbox1023" TextMode="MultiLine"
                        runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin nghỉ bù")%><hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Gia hạn đến tháng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboMonth_Extension_NB" runat="server" SkinID="dDropdownList">
                        <Items>
                            <tlk:RadComboBoxItem Text="" Value="" />
                            <tlk:RadComboBoxItem Text="1" Value="1" />
                            <tlk:RadComboBoxItem Text="2" Value="2" />
                            <tlk:RadComboBoxItem Text="3" Value="3" />
                            <tlk:RadComboBoxItem Text="4" Value="4" />
                            <tlk:RadComboBoxItem Text="5" Value="5" />
                            <tlk:RadComboBoxItem Text="6" Value="6" />
                            <tlk:RadComboBoxItem Text="7" Value="7" />
                            <tlk:RadComboBoxItem Text="8" Value="8" />
                            <tlk:RadComboBoxItem Text="9" Value="9" />
                            <tlk:RadComboBoxItem Text="10" Value="10" />
                            <tlk:RadComboBoxItem Text="11" Value="11" />
                            <tlk:RadComboBoxItem Text="12" Value="12" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Số phép bù được thanh toán")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmCOM_PAY" runat="server" SkinID="Decimal" NumberFormat-DecimalDigits="2">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tháng điều chỉnh nghỉ bù")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSTART_MONTH_NB" runat="server" SkinID="dDropdownList">
                        <Items>
                            <tlk:RadComboBoxItem Text="" Value="" />
                            <tlk:RadComboBoxItem Text="1" Value="1" />
                            <tlk:RadComboBoxItem Text="2" Value="2" />
                            <tlk:RadComboBoxItem Text="3" Value="3" />
                            <tlk:RadComboBoxItem Text="4" Value="4" />
                            <tlk:RadComboBoxItem Text="5" Value="5" />
                            <tlk:RadComboBoxItem Text="6" Value="6" />
                            <tlk:RadComboBoxItem Text="7" Value="7" />
                            <tlk:RadComboBoxItem Text="8" Value="8" />
                            <tlk:RadComboBoxItem Text="9" Value="9" />
                            <tlk:RadComboBoxItem Text="10" Value="10" />
                            <tlk:RadComboBoxItem Text="11" Value="11" />
                            <tlk:RadComboBoxItem Text="12" Value="12" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số bù điều chỉnh")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmADJUST_NB" runat="server" SkinID="Custom" NumberFormat-DecimalDigits="2">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Lý do điều chỉnh")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtREMARK_NB" SkinID="Textbox1023" Width="100%" TextMode="MultiLine"
                        runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <b style="color: Red;">Chú ý: </b>
                    <br />
                    - Nhập số dương hệ thống sẽ (+)
                    <br />
                    - Nhập số âm hệ thống sẽ (-)
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgWorkschedule" AllowPaging="true" runat="server"
            Height="100%">
            <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE" ClientDataKeyNames="ID,EMPLOYEE_CODE"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnEmployee" runat="server" Text="<%$ Translate: Chọn nhân viên %>"
                                CausesValidation="false" CommandName="FindEmployee">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="10%"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                        UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME" HeaderStyle-Width="25%"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" HeaderStyle-Width="30%"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME" HeaderStyle-Width="35%"/>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                 <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlDeclareEntitlementNBNewEditMultiple_RadSplitter3';
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
            registerOnfocusOut(splitterID);
        }
        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        function clientButtonClicking(sender, args) {
//            if (args.get_item().get_commandName() == 'CANCEL') {
//                getRadWindow().close(null);
//                args.set_cancel(true);
//            }
        }
        
    </script>
</tlk:RadCodeBlock>
