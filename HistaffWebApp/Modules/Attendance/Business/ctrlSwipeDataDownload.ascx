<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSwipeDataDownload.ascx.vb"
    Inherits="Attendance.ctrlSwipeDataDownload" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .hide-button input{
        display: none;
    }
</style>
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="hidempid1" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="50px" Scrolling="None">
                <table class="table-form" >
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbTimekeepingType" runat="server" Text="Loại chấm công"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboTimekeepingType" Width="100%" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbTerminal" runat="server" Text="Chấm công tại"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboTerminal" Width="100%" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" >
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb" style="display:none">
                            <asp:Label ID="lbMachine_Type" runat="server" Text="Hệ thống chấm công"></asp:Label>
                        </td>
                        <td style="display:none"> 
                            <tlk:RadComboBox runat="server" ID="cbMachine_Type">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbStartDate" runat="server" Text="Từ ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdStartDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbEndDate" runat="server" Text="Đến ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEndDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearchEmp" runat="server" Text="Tìm kiếm">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="lb">
                            <asp:Label runat="server" ID="lbEmployeeCode" Text="Mã nhân viên"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployeeCode" runat="server" AutoPostBack="true">
                                <ClientEvents OnKeyPress="OnKeyPress" />
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbEmployeeName" Text="Họ tên nhân viên"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbTitleName" Text="Vị trí công việc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTitleName" runat="server" SkinID="Readonly" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rglSwipeDataDownload" runat="server" Height="100%" AllowSorting="True"
                    AllowMultiRowSelection="true" AllowPaging="True" AutoGenerateColumns="False">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView Width="100%" TableLayout="Fixed" DataKeyNames="ITIME_ID,ORG_DESC,IMAGE_GPS,PLACE_ID,LONGITUDE,LATITUDE,UPLOAD_IMAGE_GPS" ClientDataKeyNames="VALTIME,ITIME_ID,IMAGE_GPS,PLACE_ID,LONGITUDE,LATITUDE,UPLOAD_IMAGE_GPS">
                        <Columns>
                            <%--<tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Loại máy" DataField="TERMINAL_CODE" SortExpression="TERMINAL_CODE"
                                UniqueName="TERMINAL_CODE">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="Hệ thống chấm công" DataField="MACHINE_TYPE_NAME" SortExpression="MACHINE_TYPE_NAME"
                                UniqueName="MACHINE_TYPE_NAME">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Mã CC" DataField="ITIME_ID_S" SortExpression="ITIME_ID_S"
                                UniqueName="ITIME_ID_S">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="Ngày quẹt thẻ" DataField="WORKINGDAY"
                                SortExpression="WORKINGDAY" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}"
                                UniqueName="WORKINGDAY">
                                <HeaderStyle Width="200px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn AllowFiltering="false" HeaderText="Thời gian quẹt thẻ"
                                DataField="VALTIME" UniqueName="VALTIME" DataFormatString="{0:HH:mm}" DataType="System.DateTime"
                                PickerType="TimePicker" SortExpression="VALTIME">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>--%>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<common:ctrlupload id="ctrlUpload" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<common:ctrlupload id="ctrlUpload1" runat="server" />
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
            var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
            var height = pane.getContentElement().scrollHeight;
            splitter.set_height(splitter.get_height() + pane.get_height() - height);
            pane.set_height(height);
        }, 200);
    }
    // Hàm khôi phục lại Size ban đầu cho Splitter
    function ResizeSplitterDefault() {
        var splitter = $find("<%= RadSplitter3.ClientID%>");
        var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
        if (oldSize == 0) {
            oldSize = pane.getContentElement().scrollHeight;
        } else {
            var pane2 = splitter.getPaneById('<%= RadPane1.ClientID %>');
            splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
            pane.set_height(oldSize);
            pane2.set_height(splitter.get_height() - oldSize - 1);
        }
    }

    function OnKeyPress(sender, eventArgs) {
        
        <%--var c = eventArgs.get_keyCode();
        debugger;
        if (c == 13) {
            document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
        }--%>
    }
    window.addEventListener('keydown', function (e) {
        //if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
        //    if (e.target.id !== 'ctl00_MainContent_ctrlSwipeDataDownload_txtEmployeeCode') {
        //        e.preventDefault();
        //        return false;
        //    }
        //}
    }, true);
</script>
