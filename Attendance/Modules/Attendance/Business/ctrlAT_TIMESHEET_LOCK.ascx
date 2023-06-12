<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_TIMESHEET_LOCK.ascx.vb"
    Inherits="Attendance.ctrlAT_TIMESHEET_LOCK" %>
<style>
    .cheb {
        padding-right: 10px;
    }
</style>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidEmpId" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="220px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrg"  AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                </td>
                <td>
                    <%# Translate("Theo nhân viên")%>
                </td>
                <td>
                    <tlk:RadButton ID="chkIsEMP" AutoPostBack="true" Text="" CausesValidation="false" ToggleType="CheckBox"
                        ButtonType="ToggleButton" runat="server" TabIndex="4">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr id="hide1" runat="server">
                <td class="lb">
                    <%# Translate("Mã NV")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtEmpCode" AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false" Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmpCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Họ và tên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFullName" ReadOnly="true" SkinID="ReadOnly"></tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" SkinID="ReadOnly"></tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Vị trí công việc")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTitle" ReadOnly="true" SkinID="ReadOnly"></tlk:RadTextBox>
                </td>

            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Năm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" CausesValidation="false" AutoPostBack="true"
                        TabIndex="12" Width="160px">
                    </tlk:RadComboBox>
                    
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label10" Text="Đối tượng nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server"  AutoPostBack="true" CausesValidation="false" ID="rcOBJECT_EMPLOYEE">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqFromDate" ControlToValidate="rdFromDate" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập từ ngày. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdToDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                
                <td class="lb">
                    <%# Translate("Kỳ công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" AutoPostBack="true" Width="160px" runat="server">
                    </tlk:RadComboBox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboPeriod"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn kỳ công. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn kỳ công. %>"> </asp:RequiredFieldValidator>--%>
                </td>
                <td></td>
                <td>
                    <tlk:RadButton ID="rcIsLeave" AutoPostBack="false"  Text="Khóa đăng ký nghỉ/Phép" CausesValidation="false" ToggleType="CheckBox"
                        ButtonType="ToggleButton" runat="server" Checked="true">
                    </tlk:RadButton>
                </td>
                <td></td>
                <td>
                    <tlk:RadButton ID="rcIsOvertime" AutoPostBack="false"  Text="Khóa đăng ký làm thêm" CausesValidation="false" ToggleType="CheckBox"
                        ButtonType="ToggleButton" runat="server"  Checked="true">
                    </tlk:RadButton>
                </td>
                <td></td>
                <td>
                    <tlk:RadButton ID="rcIsDMVS" AutoPostBack="false"  Text=" Khóa đăng ký bổ sung quẹt thẻ" CausesValidation="false" ToggleType="CheckBox"
                        ButtonType="ToggleButton" runat="server"  Checked="true">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat ="server" ID="txtNote" Width="100%"></tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID,ORG_DESC,ORG_DESC_TIMESHEET" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_NAME,EMPLOYEE_CODE,ORG_ID,ORG_NAME,TITLE_ID,TITLE_NAME,REMARK,IS_DMVS,IS_LEAVE,IS_OVERTIME,FROM_DATE,TO_DATE,ORG_NAME_TIMESHEET">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME_TIMESHEET" UniqueName="ORG_NAME_TIMESHEET"
                        SortExpression="ORG_NAME_TIMESHEET" HeaderStyle-Width="200px" />
                    <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME"
                        UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban nhân viên" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME" HeaderStyle-Width="200px" />
                    <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        SortExpression="TITLE_NAME" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="Từ ngày" DataField="FROM_DATE" UniqueName="FROM_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        SortExpression="FROM_DATE" HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" />
                    <tlk:GridBoundColumn HeaderText="Đến ngày" DataField="TO_DATE" UniqueName="TO_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        SortExpression="TO_DATE" HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" />
                   <tlk:GridCheckBoxColumn HeaderText="Khóa đăng ký nghỉ/phép" DataField="IS_LEAVE" UniqueName="IS_LEAVE"
                        SortExpression="IS_LEAVE" HeaderStyle-Width="100px" />
                    <tlk:GridCheckBoxColumn HeaderText="Khóa đăng ký làm thêm" DataField="IS_OVERTIME" UniqueName="IS_OVERTIME"
                        SortExpression="IS_OVERTIME" HeaderStyle-Width="100px" />
                    <tlk:GridCheckBoxColumn HeaderText="Khóa đăng ký bổ sung quẹt thẻ" DataField="IS_DMVS" UniqueName="IS_DMVS"
                        SortExpression="IS_DMVS" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="Mô tả" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" />
                </Columns>
                <HeaderStyle Width="120px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
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
        function OnKeyPress(sender, eventArgs) 
        { 
           var c = eventArgs.get_keyCode(); 
           if (c == 13) 
           { 
             document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
    </script>
</tlk:RadCodeBlock>
