<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRegisterCONewEdit.ascx.vb"
    Inherits="Attendance.ctrlRegisterCONewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<style type="text/css">
    .ctl00_MainContent_ctrlRegisterCONewEdit_RadTextBox1 {
        height: 100px;
    }
</style>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="100%">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <div style="width: 100%;float: left;">
            <div style="width: 65%; float: left;">
                <table class="table-form" >
                    <tr>
                        <td colspan="4">
                            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b style="color: red">
                                <%# Translate("Thông tin nhân viên")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnID" ReadOnly="true" Visible="false">
                            </tlk:RadNumericTextBox>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_Code" AutoPostBack="true">
                                <ClientEvents OnKeyPress="OnKeyPress" />  
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Phòng ban")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtOrg_Name" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="rtOrg_id" ReadOnly="true" Visible="False">
                            </tlk:RadTextBox>
                        </td>
                       <%-- <td>
                            <tlk:RadTextBox runat="server" ID="RadTextBox2" ReadOnly="true" Width="100%" BorderWidth="0">
                            </tlk:RadTextBox>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Họ tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_Name"  ReadOnly="true">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_id" Visible="False">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reEMPLOYEE_CODE" ControlToValidate="rtEmployee_Code"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn mã nhân viên %>"
                                ToolTip="<%$ Translate: Chưa chọn mã nhân viên %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Vị trí công việc")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtTitle_Name" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="rtTitle_Id" ReadOnly="true" Visible="False">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr id="rwQuater1" runat="server">
                        <td colspan="4">
                            <b style="color: red">
                                <%# Translate("Thông tin phép năm")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr id="rwQuater2" runat="server">
                        <td class="lb">
                            <b>
                                <%# Translate("Quỹ phép còn lại:")%>
                            </b>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblQPCL" Font-Bold ="true"></asp:Label>
                        </td>
                    </tr>
                    <tr id="rwQuater3" runat="server">
                        <td class="lb">
                            <%--<%# Translate("Phép năm thâm niên")%>--%>
                            <asp:Label runat="server" ID="lblPNTN" Text ="Phép năm thâm niên"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnSENIORITYHAVE" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                           <%-- <%# Translate("Phép năm theo chế độ")%>--%>
                            <asp:Label runat="server" ID="lblPNTCD" Text ="Phép năm theo chế độ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnCUR_HAVE" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr id="rwQuater4" runat="server">
                        <td class="lb">
                            <%--<%# Translate("Phép cũ chuyển qua")%>--%>
                            <asp:Label runat="server" ID="lblPCCQ" Text ="Phép cũ chuyển qua"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnPREV_HAVE" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%--<%# Translate("Phép theo tháng làm việc")%>--%>
                            <asp:Label runat="server" ID="lblPTTLV" Text ="Phép theo tháng làm việc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnCUR_HAVE_INMONTH" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr id="rwQuater5" runat="server">
                        <td class="lb">
                            <%--<%# Translate("Phép cũ điều chỉnh")%>--%>
                            <asp:Label runat="server" ID="lblPCDC" Text ="Phép cũ điều chỉnh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnOldChange" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%--<%# Translate("Phép năm điều chỉnh")%>--%>
                            <asp:Label runat="server" ID="lblPNDC" Text ="Phép năm điều chỉnh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnChange" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <%--new--%>
                    <tr id="rwQuater6" runat="server" style="display: none">
                        <td class="lb">
                            <%--<%# Translate("Phép cũ đã thanh toán")%>--%>
                            <asp:Label runat="server" ID="lblPCDTT" Text ="Phép cũ đã thanh toán"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnOldLeave" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%--<%# Translate("Phép năm đã thanh toán")%>--%>
                            <asp:Label runat="server" ID="LBLPNDTT" Text ="Phép năm đã thanh toán"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnLeave" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr id="rwQuater7" runat="server">
                        <td class="lb">
                            <%--<%# Translate("Phép cũ đã nghỉ")%>--%>
                            <asp:Label runat="server" ID="lblPCDN" Text ="Phép cũ đã nghỉ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnCUR_USED" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%--<%# Translate("Phép năm đã sử dụng")%>--%>
                            <asp:Label runat="server" ID="PNDSD" Text ="Phép năm đã sử dụng"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtCUR_USED_INMONTH" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr id="rwQuater8" runat="server">
                        
                        <td class="lb">
                            <%--<%# Translate("Phép cũ còn hiệu lực")%>--%>
                            <asp:Label runat="server" ID="lblPCCHL" Text ="Phép cũ còn hiệu lực"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnPREVTOTAL_HAVE" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb" style="display: none">
                            <%--<%# Translate("Phép trừ do nghỉ RO")%>--%>
                            <asp:Label runat="server" ID="lblPTDNRO" Text ="Phép trừ do nghỉ RO"></asp:Label>
                        </td>
                        <td style="display: none">
                            <tlk:RadTextBox runat="server" ID="rnRO" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr id="rwQuater9" runat="server">
                        <td class="lb">
                            <%--<%# Translate("Phép nghỉ bù trong năm")%>--%>
                            <asp:Label runat="server" ID="lblPNBTN" Text ="Phép nghỉ bù trong năm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnCUR_DANGKY" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%--<%# Translate("Phép nghỉ bù đã sử dụng")%>--%>
                            <asp:Label runat="server" ID="lblPNBDSD" Text ="Phép nghỉ bù đã sử dụng"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtCUR_HAVE" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <%--<td class="lb">
                            <%# Translate("Quỹ phép còn lại")%>
                        </td>--%>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnBALANCE" ReadOnly="true" Visible ="false"> 
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr id="rwQuater10" runat="server">
                        <td class="lb">
                            <%--<%# Translate("Phép năm điều chỉnh")%>--%>
                            <asp:Label runat="server" ID="Label3" Text ="Phép điều chỉnh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rdChangeB" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b style="color: red">
                                <%# Translate("Thông tin đăng ký nghỉ/công tác")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Loại nghỉ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cbMANUAL_ID" Width="200px" DataTextField="NAME_VN"
                                DataValueField="ID" CausesValidation="false" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Số ngày đăng ký")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnDAY_NUM" ReadOnly="true" Width="50px">
                            </tlk:RadNumericTextBox>
                            <%--<tlk:RadNumericTextBox runat="server" ID="RadNumericTextBox1" ReadOnly="true" BorderWidth="0" Width="5px">
                            </tlk:RadNumericTextBox>--%>
                            <asp:Label runat="server" ID="Label1" Text="<%$ Translate: Số con %>"></asp:Label>
                            <tlk:RadNumericTextBox runat="server" ID="rnTS_SON" Width="50px" MinValue="1" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdLEAVE_FROM" CausesValidation="false" AutoPostBack="true">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdLEAVE_TO" CausesValidation="false" AutoPostBack="true" Width="110px">
                            </tlk:RadDatePicker>
                            <input id="btnDetail" value="<%# Translate("Chi tiết")%>" type="button" onclick="showDetail('')">
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdLEAVE_TO"
                                Type="Date" ControlToCompare="rdLEAVE_FROM" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc nghỉ phải lớn hơn ngày bắt đầu nghỉ %>"
                                ToolTip="<%$ Translate: Ngày kết thúc nghỉ phải lớn hơn ngày bắt đầu nghỉ %>"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lblLeaveTT_TS" Text="<%$ Translate: Ngày kết thúc thực tế %>"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdLeaveTT_TS" CausesValidation="false" AutoPostBack="true" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Lý do")%>
                            <span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox runat="server" ID="rtReason_leave" CausesValidation="false" rtNOTE="MultiLine" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reNOTE" ControlToValidate="rtReason_leave"
                                runat="server" ErrorMessage="<%$ Translate: Chưa nhập lý do nghỉ %>"
                                ToolTip="<%$ Translate: Chưa nhập lý do nghỉ  %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnIS_APP" Value="-1" Visible="false"></tlk:RadNumericTextBox></td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnSTATUS" Value="1" Visible="false"></tlk:RadNumericTextBox></td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox runat="server" ID="rtNote" CausesValidation="false" rtNOTE="MultiLine" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbFromLoc" Text="<%$ Translate: Di chuyển từ %>" Visible="false"></asp:Label>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox runat="server" ID="rtFROM_LOCATION" CausesValidation="false" Width="100%" Visible="false">
                            </tlk:RadTextBox>
                        </td>
                        </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbToLoc" Text="<%$ Translate: Di chuyển dến %>" Visible="false"></asp:Label>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox runat="server" ID="rtTO_LOCATION" CausesValidation="false" Width="100%" Visible="false">
                            </tlk:RadTextBox>
                        </td>
                        </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbNumKM" Text="<%$ Translate: Khoảng cách(km) %>" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnNUMBER_KILOMETER" CausesValidation="false" Visible="false">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbPROVINCEEMP_ID" Text="Tỉnh/TP công tác" Visible="false"></asp:Label>
                        </td>
                        <td >
                            <tlk:RadComboBox runat="server" ID="cbPROVINCE_ID" Width="200px"  DataTextField="NAME_VN" 
                                DataValueField="ID" CausesValidation="false"  Visible="false">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                </table>                 
            </div>
             <div style="width: 35%; float: left;margin-top:35px;margin-left:-140px;">
                    <table  onkeydown="return (event.keyCode!=13)">                       
                        <tr>                           
                            <td colspan="4">
                                <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="500px" Height="382px" ReadOnly="true">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </div>
        </div>
       
        <div id="divLeaveDetail" style="display: none">
            <tlk:RadGrid PageSize="500" runat="server" ID="rgData" AllowMultiRowEdit="true" Width="70%">
                <MasterTableView DataKeyNames="MANUAL_ID,STATUS_SHIFT,DAY_NUM,SHIFT_NAME,SHIFT_DAY,LEAVE_DAY,EMPLOYEE_ID"
                    ClientDataKeyNames="MANUAL_ID,STATUS_SHIFT,DAY_NUM,SHIFT_NAME,SHIFT_DAY,LEAVE_DAY,EMPLOYEE_ID"
                    EditMode="InPlace" EditFormSettings-EditFormType="AutoGenerated">
                    <CommandItemStyle Height="28px" />

                    <Columns>
                        <tlk:GridBoundColumn HeaderText='<%$ Translate: EMPLOYEE_ID %>' DataField="EMPLOYEE_ID" Visible="false"
                            UniqueName="EMPLOYEE_ID" SortExpression="EMPLOYEE_ID" ReadOnly="true"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="145px" />
                            <HeaderStyle Width="150px" />
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày nghỉ %>' DataField="LEAVE_DAY"
                            UniqueName="LEAVE_DAY" SortExpression="LEAVE_DAY" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="145px" />
                            <HeaderStyle Width="150px" />
                        </tlk:GridBoundColumn>

                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nghỉ%>" DataField="MANUAL_ID" AllowSorting="false"
                            ColumnGroupName="MANUAL_ID" UniqueName="MANUAL_ID" SortExpression="MANUAL_ID" ReadOnly="true" Visible="false" />

                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nghỉ%>" DataField="MANUAL_NAME" AllowSorting="false"
                            ColumnGroupName="MANUAL_NAME" UniqueName="MANUAL_NAME" SortExpression="MANUAL_NAME" ReadOnly="true" />

                        <%--<tlk:GridTemplateColumn HeaderText="Không nghỉ" HeaderStyle-Width="150px" UniqueName ="NON_LEAVE" >
                            <EditItemTemplate>
                                <asp:CheckBox Width ="25px" runat="server" ID="ckNON_LEAVE"
                                ReadOnly ="true" AutoPostBack ="true" CausesValidation="false" OnCheckedChanged ="ckNON_LEAVE_OnCheckedChanged" ></asp:CheckBox>                                       
                            </EditItemTemplate>
                        </tlk:GridTemplateColumn>--%>

                        <tlk:GridTemplateColumn HeaderText="Đầu ca/cuối ca" HeaderStyle-Width="150px" UniqueName="STATUS_SHIFT">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "STATUS_SHIFT_NAME")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <tlk:RadComboBox Width="125px" runat="server" ID="cbSTATUS_SHIFT"
                                    ReadOnly="true" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="cbSTATUS_SHIFT_SelectedIndexChanged">
                                </tlk:RadComboBox>
                            </EditItemTemplate>
                        </tlk:GridTemplateColumn>

                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Số ngày nghỉ%>" DataField="DAY_NUM" AllowSorting="false"
                            ColumnGroupName="DAY_NUM" UniqueName="DAY_NUM" SortExpression="DAY_NUM" ReadOnly="true" DataFormatString="{0:N2}" />

                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca làm việc%>" DataField="SHIFT_NAME" AllowSorting="false"
                            ColumnGroupName="SHIFT_NAME" UniqueName="SHIFT_NAME" SortExpression="SHIFT_NAME" ReadOnly="true" />

                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên NV%>" DataField="SHIFT_ID" AllowSorting="false"
                            ColumnGroupName="SHIFT_ID" UniqueName="SHIFT_ID" SortExpression="SHIFT_ID" ReadOnly="true" Visible="false" />

                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày công ca%>" DataField="SHIFT_DAY" AllowSorting="false"
                            ColumnGroupName="SHIFT_DAY" UniqueName="SHIFT_DAY" SortExpression="SHIFT_DAY" ReadOnly="true" DataFormatString="{0:N2}" />
                    </Columns>
                </MasterTableView>
            </tlk:RadGrid>
        </div>
    </tlk:RadPane>

</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlRegisterCONewEdit_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRegisterCONewEdit_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRegisterCONewEdit_RadPane2';
        var validateID = 'MainContent_ctrlRegisterCONewEdit_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

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

        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule');
                else
                    //                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                    ResizeSplitterDefault();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
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
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == 'CANCEL') {
                //  getRadWindow().close(null);
                //  args.set_cancel(true);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function showDetail(value) {
            if (value == "")
                if ($("#divLeaveDetail").css("display") == "block")
                    $("#divLeaveDetail").css("display", "none");
                else
                    $("#divLeaveDetail").css("display", "block");
            else
                $("#divLeaveDetail").css("display", value);
        }

        function IsBlock() {
            $("#divLeaveDetail").css("display", "block");
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
    </script>
</tlk:RadCodeBlock>
