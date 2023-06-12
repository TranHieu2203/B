<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLeaveCTRegistrationNewEdit.ascx.vb"
    Inherits="Attendance.ctrlLeaveCTRegistrationNewEdit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidValid" runat="server" />
<asp:HiddenField ID="hidStatus" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<asp:Label runat="server" ID="lbStatus" ForeColor="Red"></asp:Label>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Orientation="Horizontal" Width="100%">
    <tlk:RadPane ID="RadPane1" runat="server" Height="100%" Scrolling="Y">
        <div style="width: 100%; float: left;">
            <div style="width: 65%; float: left;">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
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
                            <%# Translate("Họ tên")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnID" ReadOnly="true" Visible="false">
                            </tlk:RadNumericTextBox>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_Name" ReadOnly="true">
                            </tlk:RadTextBox>
                            <%--<tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>--%>
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
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_Code" ReadOnly="true">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_id" Visible="False">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reEMPLOYEE_CODE" ControlToValidate="rtEmployee_Code"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn mã nhân viên %>"
                                ToolTip="<%$ Translate: Chưa chọn mã nhân viên %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Chức danh")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtTitle_Name" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="rtTitle_Id" ReadOnly="true" Visible="False">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr style ="display:none">
                        <td colspan="4">
                            <b style="color: red">
                                <%# Translate("Thông tin phép năm")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr style ="display:none">
                        <td class="lb">
                            <%# Translate("Phép chế độ")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnCUR_HAVE" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Phép cũ chuyển qua")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnPREV_HAVE" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr style ="display:none">
                        <td class="lb">
                            <%# Translate("Phép theo tháng làm việc")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnCUR_HAVE_INMONTH" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Phép cũ đã nghỉ")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnCUR_USED" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr style ="display:none">
                        <td class="lb">
                            <%# Translate("Phép đã sử dụng")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtCUR_USED_INMONTH" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Phép cũ còn hiệu lực")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnPREVTOTAL_HAVE" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr style ="display:none">
                        <td class="lb">
                            <%# Translate("Phép bù đang có")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnCUR_DANGKY" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Phép thâm niên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnSENIORITYHAVE" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr style ="display:none">
                        <td class="lb">
                            <%# Translate("Phép bù đã sử dụng")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtCUR_HAVE" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Quỹ phép còn lại")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnBALANCE" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr style ="display:none">
                        <td class="lb">
                            <%# Translate("Số phép điều chỉnh")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rnChange" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b style="color: red">
                                <%# Translate("Thông tin đăng ký")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr >
                        <td class="lb">
                            <%# Translate("Loại đăng ký")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cbMANUAL_ID" Width="250px" DataTextField="NAME_VN"
                                DataValueField="ID" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Số ngày đăng ký")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnDAY_NUM" ReadOnly="true" Width="50px">
                            </tlk:RadNumericTextBox>
                            <%--<tlk:RadNumericTextBox runat="server" ID="RadNumericTextBox1" ReadOnly="true" BorderWidth="0" Width="75px">
                            </tlk:RadNumericTextBox>--%>
                            <asp:Label runat="server" ID="Label1" Text="<%$ Translate: Số con %>"></asp:Label>
                            <tlk:RadNumericTextBox runat="server" ID="rnN_SON" Width="50px" MinValue="1" AutoPostBack="true" CausesValidation="false">
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
                            <tlk:RadDatePicker runat="server" ID="rdLEAVE_TO" CausesValidation="false" AutoPostBack="true">
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
                            <%# Translate("Lý do đăng ký")%>
                            <span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox runat="server" ID="rtNote" CausesValidation="false" rtNOTE="MultiLine" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reNOTE" ControlToValidate="rtNote"
                                runat="server" ErrorMessage="<%$ Translate: Chưa nhập lý do nghỉ %>"
                                ToolTip="<%$ Translate: Chưa nhập lý do nghỉ  %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnIS_APP" Value="0" Visible="false"></tlk:RadNumericTextBox></td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnSTATUS" Value="0" Visible="false"></tlk:RadNumericTextBox></td>
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
                            <tlk:RadComboBox runat="server" ID="cbPROVINCE_ID" Width="200px" DataTextField="NAME_VN" 
                                DataValueField="ID" CausesValidation="false"  Visible="false">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 35%; float: left; margin-top: 35px; margin-left: -100px;">
                <table onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td colspan="4">
                            <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="500px" Height="345px" ReadOnly="true">
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

<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function clientButtonClicking(sender, args) {
            //if (args.get_item().get_commandName() == "CANCEL") {
            //    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlLeaveRegistration');
            //    args.set_cancel(true);
            //}
        }
        function OpenInNewTab(url) {
            window.location.href = url;
        }
        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }
        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
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
        function valueChange(sender, args) {
        }

        function IsBlock() {
            $("#divLeaveDetail").css("display", "block");
        }
        function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);            winH = $(window).height() - 230;            winW = $(window).width() - 90;            $("#ctl00_MainContent_ctrlLeaveCTRegistrationNewEdit_RadSplitter3").stop().animate({ height: winH - 30, width: winW }, 0);            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlLeaveCTRegistrationNewEdit_RadPane1").stop().animate({ height: winH - 30, width: winW }, 0);            Sys.Application.add_load(SizeToFitMain);
        }        SizeToFitMain();        $(document).ready(function () {
            SizeToFitMain();
        });        $(window).resize(function () {
            SizeToFitMain();
        });
    </script>
</tlk:RadCodeBlock>
