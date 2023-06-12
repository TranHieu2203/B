<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlNewInOut.ascx.vb"
    Inherits="Attendance.ctrlNewInOut" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <table class="table-form"  onkeydown="return (event.keyCode!=13)">
            <tr>
                <td colspan="4">
                    <b>
                        <%# Translate("Thêm mới dữ liệu vào ra")%></b><hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Mã nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" SkinID="ReadOnly" ReadOnly="true" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" runat="server" CausesValidation="false" SkinID="ButtonView">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmployeeCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn chưa nhập mã nhân viên %>" ToolTip="<%$ Translate: Bạn chưa nhập mã nhân viên %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Tên nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" SkinID="ReadOnly" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh chính")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" SkinID="ReadOnly" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" SkinID="ReadOnly" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày/Giờ quẹt thẻ")%>
                </td>
                <td> 
                    <tlk:RadTimePicker ID="rdhour" runat="server">
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="reqFromdate" ControlToValidate="rdhour" runat="server"
                        ErrorMessage="<%$ Translate: Bạn chưa nhập giờ quẹt %>" ToolTip="<%$ Translate: Bạn chưa nhập giờ quẹt %>"> </asp:RequiredFieldValidator>
                </td>
                 <td class="lb">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td> 
                     <tlk:RadTextBox ID="txtCapNS" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdFromDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập Từ ngày %>" ToolTip="<%$ Translate: Bạn chưa nhập Từ ngày%>"> </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="rdFromDate" ControlToCompare="rdToDate"
                        Operator="LessThanEqual" Type="Date" runat="server" ErrorMessage="<%$ Translate: Từ ngày phải nhỏ hơn Đến ngày %>"
                        ToolTip="<%$ Translate: Từ ngày phải nhỏ hơn Đến ngày %>"> </asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdToDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            
        </table>
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
