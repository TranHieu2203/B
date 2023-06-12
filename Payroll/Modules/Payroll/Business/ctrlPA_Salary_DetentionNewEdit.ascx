﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Salary_DetentionNewEdit.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryDetentionNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<style type="text/css">
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="20px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCommend" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RightPane" runat="server" Height="360px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbl1" Text="Năm"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYear1" SkinID="dDropdownList" AutoPostBack="true" CausesValidation="false" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboYear1" runat="server"
                        ErrorMessage="Bạn phải chọn Năm" ToolTip="Bạn phải chọn Năm">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Tháng tính lương"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriodT" SkinID="dDropdownList" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqPeriodT" ControlToValidate="cboPeriodT" runat="server"
                        ErrorMessage="Bạn phải chọn Tháng tính lương" ToolTip="Bạn phải chọn Tháng tính lương">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbl2" Text="Năm"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYear2" SkinID="dDropdownList" AutoPostBack="true" CausesValidation="false" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbPayMonth" Text="Tháng trả lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPayMonth" SkinID="dDropdownList" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbNote" Text="Ghi chú"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNote" SkinID="Textbox1023" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
        <tlk:RadGrid ID="rgEmployee" AllowPaging="false" AllowMultiRowEdit="true" runat="server" Height="100%">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" EditFormSettings-EditFormType="AutoGenerated" AllowPaging="false" AllowCustomPaging="false"
                DataKeyNames="EMPLOYEE_ID"
                ClientDataKeyNames="EMPLOYEE_ID"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnEmployee" runat="server" Text="Chọn nhân viên"
                                CausesValidation="false" CommandName="FindEmployee">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmp" runat="server" Text="Xóa" CausesValidation="false"
                                CommandName="DeleteEmployee">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="EMPLOYEE_ID" HeaderText="ID" UniqueName="EMPLOYEE_ID" SortExpression="EMPLOYEE_ID"
                        Visible="false" ReadOnly="true"/>
                    <tlk:GridBoundColumn DataField="EMPLOYEE_CODE" HeaderText="MSNV" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"
                        Visible="true" ReadOnly="true"/>
                    <tlk:GridBoundColumn DataField="FULLNAME_VN" HeaderText="Họ tên nhân viên" UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN"
                        Visible="true" ReadOnly="true"/>
                    <tlk:GridBoundColumn DataField="ORG_NAME" HeaderText="Phòng ban" UniqueName="ORG_NAME" SortExpression="ORG_NAME"
                        Visible="true" ReadOnly="true"/>
                    <tlk:GridBoundColumn DataField="TITLE_NAME" HeaderText="Vị trí công việc" UniqueName="TITLE_NAME" SortExpression="TITLE_NAME"
                        Visible="true" ReadOnly="true"/>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {

            }
        }
    </script>
</tlk:RadCodeBlock>
