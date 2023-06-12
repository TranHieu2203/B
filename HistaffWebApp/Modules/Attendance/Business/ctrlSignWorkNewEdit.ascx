<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlWorkschedule.ascx.vb"
    Inherits="Attendance.ctrlSignWorkNewEdit" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="80px" Scrolling="Y">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td>
                    <%# Translate("Ca làm việc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSign" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboSign"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa chọn ca. %>" ToolTip="<%$ Translate: Bạn chưa chọn ca. %>"> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusCboSign" ControlToValidate="cboSign" runat="server" ErrorMessage="<%$ Translate: Ca làm việc không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Ca làm việc không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromdate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqFromdate" ControlToValidate="rdFromdate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn chưa nhập từ ngày. %>" ToolTip="<%$ Translate: Bạn chưa nhập từ ngày. %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEnddate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdEnddate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập đến ngày. %>" ToolTip="<%$ Translate: Bạn chưa nhập đến ngày. %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgWorkschedule" AllowPaging="false" runat="server"
            Width="100%" Height="100%">
            <MasterTableView AllowPaging="false" AllowCustomPaging="false" DataKeyNames="ID,EMPLOYEE_CODE,ORG_DESC"
                ClientDataKeyNames="ID,EMPLOYEE_CODE" CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnFindEmployee" runat="server" Text="<%$ Translate: Chọn nhân viên %>"
                                CausesValidation="false" CommandName="FindEmployee">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmployee" runat="server" Text="<%$ Translate: Xóa nhân viên %>"
                                CausesValidation="false" CommandName="DeleteEmployee">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                        UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

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
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }
        
    </script>
</tlk:RadCodeBlock>
