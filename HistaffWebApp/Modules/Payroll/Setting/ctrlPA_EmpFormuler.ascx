<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_EmpFormuler.ascx.vb"
    Inherits="Payroll.ctrlPA_EmpFormuler" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="130px" Scrolling="None">
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmpCode" AutoPostBack="true" runat="server" Width="130px">
                                <ClientEvents OnKeyPress="OnKeyPress" /> 
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmpCode"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên. %>" ToolTip="<%$ Translate: Bạn phải chọn nhân viên. %>"></asp:RequiredFieldValidator>
                            <tlk:RadButton Width="100" ID="btnChooseEmployee" runat="server" CausesValidation="false"
                                SkinID="ButtonView">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Họ tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmpName" SkinID="ReadOnly" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbOrgName" runat="server" Text="Phòng ban "></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" AutoPostBack="true">
                                <ClientEvents OnKeyPress="OnKeyPress" />
                            </tlk:RadTextBox>
                            <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrgName"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                         <td class="lb">
                            <%# Translate("Chức danh")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboTiTle" AutoPostBack="true" CausesValidation="false"/>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="cboTiTle"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn Chức danh. %>" ToolTip="<%$ Translate: Bạn phải chọn Chức danh. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Nhóm nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboGroupEmp" />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="cboGroupEmp"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm nhân viên. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Đối tượng lương")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboObjSal" AutoPostBack="true" CausesValidation="false"/>
                        </td>
                    </tr>
                    <tr>
                         <td class="lb">
                            <%# Translate("Công thức lương")%> <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboForGroup"/>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboForGroup"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn Công thức lương. %>" ToolTip="<%$ Translate: Bạn phải chọn Công thức lương. %>"></asp:RequiredFieldValidator>
                        </td>

                        <td class="lb">
                            <%# Translate("Đối tượng nhân viên")%> <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboObjEmp"/>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboObjEmp"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn Đối tượng nhân viên. %>" ToolTip="<%$ Translate: Bạn phải chọn Đối tượng nhân viên. %>"></asp:RequiredFieldValidator>
                        </td>     
                    </tr>    
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%> <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEffect_date">
                            </tlk:RadDatePicker>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdEffect_date"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Tháng hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập Tháng hiệu lực. %>"></asp:RequiredFieldValidator>
                        </td>
                         <td class="lb">
                            <%# Translate("Ngày hết hiệu lực")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdExpire_date">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>             
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgWorkschedule" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_NAME,ORG_ID,EFFECT_DATE,TITLE_ID,EXPIRE_DATE,OBJ_SAL_ID,FOR_GROUP_ID,GROUP_EMPLOYEE_ID,OBJECT_EMPLOYEE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                    </asp:Label>
                                    <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                        RelativeTo="Element" Position="BottomCenter">
                                        <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                    </tlk:RadToolTip>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm nhân viên %>" DataField="GROUP_EMPLOYEE_NAME"
                                UniqueName="GROUP_EMPLOYEE_NAME" SortExpression="GROUP_EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng lương %>" DataField="OBJ_SAL_NAME"
                                UniqueName="OBJ_SAL_NAME" SortExpression="OBJ_SAL_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Công thức lương %>" DataField="FOR_GROUP_NAME"
                                UniqueName="FOR_GROUP_NAME" SortExpression="FOR_GROUP_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                              <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng nhân viên %>" DataField="OBJECT_EMPLOYEE_NAME"
                                UniqueName="OBJECT_EMPLOYEE_NAME" SortExpression="OBJECT_EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" >
                             <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE">
                                  <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlPA_EmpFormuler_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_EmpFormuler_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_EmpFormuler_RadPane2';
        var validateID = 'MainContent_ctrlPA_EmpFormuler_valSum';
        var oldSize = $('#' + pane1ID).height();

        var txtBoxName = 'ctl00_MainContent_ctrlPA_EmpFormuler_txtEmpName_wrapper';
        var txtBoxTitle = 'ctl00_MainContent_ctrlPA_EmpFormuler_txtTitle_wrapper';

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

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();

            //if (args.get_item().get_commandName() == 'EXPORT_TEMP') {
            //    enableAjax = false;
            //}

            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    $('#' + txtBoxName + ',' + '#' + txtBoxTitle).css("width", "100%");
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgWorkschedule.ClientID %>");
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
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule', 0, 0, 7);
        }

        function displayDecimalFormat(sender, args) {
            sender.set_textBoxValue(sender.get_value().toString());
        }
        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlPA_EmpFormuler_txtOrgName') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
