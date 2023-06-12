<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAtToxicLeaveEmp.ascx.vb"
    Inherits="Attendance.ctrlAtToxicLeaveEmp" %>
<%@ Import Namespace="Common" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="200px" Scrolling="Y" >
               <asp:HiddenField ID="hidEmp" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />
                <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" />
               <table class="table-form">
                    <tr>
                        <td class="lb" style="width: 180px">
                            <asp:Label runat="server" ID="lbEmployeeCode" Text="Mã nhân viên"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="130px" AutoPostBack="true">
                                <ClientEvents OnKeyPress="OnKeyPress" /> 
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                                runat="server" ErrorMessage="Bạn phải chọn Nhân viên " ToolTip="Bạn phải chọn Nhân viên"> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="width: 180px">
                            <asp:Label runat="server" ID="lbEmployeeName" Text="Họ tên"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="lb" style="width: 180px">
                            <asp:Label runat="server" ID="lbOrgName" Text="Đơn vị"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
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
                   <tr>
                       <td class="lb">
                           <%# Translate("Hiệu lực")%><span class="lbReq">*</span>
                       </td>
                       <td>
                           <tlk:RadTextBox ID="txtFromEffect1" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromEffect1"
                                runat="server" ErrorMessage="Bạn phải nhập Hiệu lực 1 " ToolTip="Bạn phải nhập Hiệu lực 1"> 
                            </asp:RequiredFieldValidator>
                       </td>
                       <td class="lb">
                           <%# Translate("Phép hưởng")%><span class="lbReq">*</span>
                       </td>
                       <td>
                           <tlk:RadNumericTextBox ID="ntxtToxicLeave1" runat="server" SkinID="Decimal">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ntxtToxicLeave1"
                                runat="server" ErrorMessage="Bạn phải nhập Phép hưởng 1" ToolTip="Bạn phải nhập Phép hưởng 1"> 
                            </asp:RequiredFieldValidator>
                       </td>
                   </tr>
                   <tr style="display: none">
                       <td class="lb">
                           <%# Translate("Hiệu lực 2")%>
                       </td>
                       <td>
                           <tlk:RadTextBox ID="txtFromEffect2" runat="server">
                            </tlk:RadTextBox>
                       </td>
                       <td class="lb">
                           <%# Translate("Phép hưởng 2")%>
                       </td>
                       <td>
                           <tlk:RadNumericTextBox ID="ntxtToxicLeave2" runat="server"  SkinID="Decimal">
                            </tlk:RadNumericTextBox>
                       </td>
                   </tr>
                   <tr style="display: none">
                       <td class="lb">
                           <%# Translate("Hiệu lực 3")%>
                       </td>
                       <td>
                           <tlk:RadTextBox ID="txtFromEffect3" runat="server">
                            </tlk:RadTextBox>
                       </td>
                       <td class="lb">
                           <%# Translate("Phép hưởng 3")%>
                       </td>
                       <td>
                           <tlk:RadNumericTextBox ID="ntxtToxicLeave3" runat="server"  SkinID="Decimal">
                            </tlk:RadNumericTextBox>
                       </td>
                   </tr>
                   <tr style="display: none">
                       <td class="lb">
                           <%# Translate("Hiệu lực 4")%>
                       </td>
                       <td>
                           <tlk:RadTextBox ID="txtFromEffect4" runat="server">
                            </tlk:RadTextBox>
                       </td>
                       <td class="lb">
                           <%# Translate("Phép hưởng 4")%>
                       </td>
                       <td>
                           <tlk:RadNumericTextBox ID="ntxtToxicLeave4" runat="server" SkinID="Decimal">
                            </tlk:RadNumericTextBox>
                       </td>
                   </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEffectDate">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdEffectDate"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvDate" ControlToValidate="rdEffectDate" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực đã tồn tại. %>"
                                ToolTip="<%$ Translate: Ngày hiệu lực đã tồn tại. %>"></asp:CustomValidator>
                        </td>
                         <td class="lb">
                           <%# Translate("Ngày kết thúc")%>
                       </td>
                       <td>
                          <tlk:RadDatePicker runat="server" ID="rdExpireDate">
                            </tlk:RadDatePicker>
                       </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mô tả")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="rdNote" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" 
                style="margin-top: 11px">
                <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,NOTE,ORG_NAME,TITLE_NAME,EMPLOYEE_NAME,FROM_EFFECT1,TOXICLEAVE1,TOXICLEAVE2,FROM_EFFECT2,FROM_EFFECT3,TOXICLEAVE3,TOXICLEAVE4,FROM_EFFECT4,EFFECT_DATE,EXPIRE_DATE,NOTE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ & tên nhân viên %>" DataField="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="250px"
                                SortExpression="ORG_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="300px"
                                SortExpression="TITLE_NAME">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Hiệu lực %>" DataField="FROM_EFFECT1" UniqueName="FROM_EFFECT1"
                                SortExpression="FROM_EFFECT1">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Phép hưởng %>" DataField="TOXICLEAVE1" UniqueName="TOXICLEAVE1"
                                SortExpression="TOXICLEAVE1">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime" UniqueName="EFFECT_DATE"
                                SortExpression="EFFECT_DATE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="80px" />
                                </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime" UniqueName="EXPIRE_DATE"
                                SortExpression="EXPIRE_DATE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="80px" />
                                </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" UniqueName="NOTE"
                                SortExpression="NOTE" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlAtToxicLeaveEmp_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAtToxicLeaveEmp_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAtToxicLeaveEmp_RadPane2';
        var validateID = 'MainContent_ctrlAtToxicLeaveEmp_valSum';
        var oldSize = $('#' + pane1ID).height();
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
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                var grid = $find("<%=rgDanhMuc.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
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
