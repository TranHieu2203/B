<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CommiteeNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_CommiteeNewEdit" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<style type="text/css">
    .ctl00_MainContent_ctrlHU_CommiteeNewEdit_RadTextBox1 {
        height: 100px;
    }
</style>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidSigner" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="400px">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <div style="width: 100%; float: left;">
            <div style="width: 100%; float: left;">
                <table class="table-form">
                    <tr>
                        <td colspan="6">
                            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <b style="color: red">
                                <%# Translate("Thông tin thành lập hội đồng")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboYear">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="rqcboYear" ControlToValidate="cboYear"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn năm %>"
                                ToolTip="<%$ Translate: Chưa chọn năm %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Số quyết định")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtDeciosionNo">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày thành lập")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdFromdate" Width="100%">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdFromdate"
                                runat="server" ErrorMessage="<%$ Translate: Chưa nhập Ngày thành lập %>"
                                ToolTip="<%$ Translate: Chưa nhập Ngày thành lập %>"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>

                        <td class="lb">
                            <%# Translate("Người quyết đinh")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtSignerCode" AutoPostBack="true" Width="81%">
                                <ClientEvents OnKeyPress="OnKeyPress" />
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFingSigner" runat="server" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="rqSigner" ControlToValidate="txtSignerCode"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn ngươi quyết định %>"
                                ToolTip="<%$ Translate: Chưa chọn ngươi quyết định %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Họ tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtSignerName" ReadOnly="true" SkinID="ReadOnly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày giải thể")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdToDate" Width="100%">
                            </tlk:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="rdFromDate" ControlToCompare="rdToDate"
                                Operator="LessThanEqual" Type="Date" runat="server" ErrorMessage="<%$ Translate: Ngày giải thể phải nhỏ hơn ngày thành lập %>"
                                ToolTip="<%$ Translate: Ngày giải thể phải nhỏ hơn ngày thành lập %>"> </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên hội đồng")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtName" runat="server" SkinID="Textbox1023" Height="40" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName"
                                runat="server" ErrorMessage="<%$ Translate: Chưa nhập Tên hội đồng %>"
                                ToolTip="<%$ Translate: Chưa nhập Tên hội đồng %>"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mục đích thành lập")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtTarget" runat="server" SkinID="Textbox1023" Height="40" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtTarget"
                                runat="server" ErrorMessage="<%$ Translate: Chưa nhập Mục đích thành lập %>"
                                ToolTip="<%$ Translate: Chưa nhập Mục đích thành lập %>"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Height="40" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <b style="color: red">
                                <%# Translate("Danh sách hội đồng hiện tại")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Chức danh hội đồng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboCommiteTitle" CausesValidation="false">
                            </tlk:RadComboBox>
                        </td>
                        <td></td>
                        <td></td>
                        <td class="lb">
                            <asp:CheckBox runat="server" ID="chkOutside" Text="Cán bộ ngoài công ty" AutoPostBack="true" CausesValidation="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã CBCNV")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtEmployeeCode" AutoPostBack="true" Width="81%">
                                <ClientEvents OnKeyPress="OnKeyPress" />
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindEmp" runat="server" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên CBCNV")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtEmployeName" ReadOnly="true" SkinID="ReadOnly">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="txtMaThe" ReadOnly="true" SkinID="ReadOnly" Visible="false">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên CBCNV")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtEmpOutsideName" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Đơn vị")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" SkinID="ReadOnly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Chức danh hiện tại")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtCurrentTitle" ReadOnly="true" SkinID="ReadOnly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Đơn vị bên ngoài")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtOrgOutsideName" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" AllowPaging="true" runat="server" Height="100%" Scrolling="None">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowPaging="false" AllowCustomPaging="false" DataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_NAME" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_NAME" CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="72px" ID="btnAdd" runat="server" Text="Thêm" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                CausesValidation="false" CommandName="Add" TabIndex="3">
                            </tlk:RadButton>
                            <tlk:RadButton Width="102px" ID="btnDetail" runat="server" Text="Chi tiết" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/refresh.png"
                                CausesValidation="false" CommandName="Detail" TabIndex="4" OnClientClicked="btnResultHistoryClick" AutoPostBack="false">
                            </tlk:RadButton>
                            <tlk:RadButton Width="112px" ID="btnExport" runat="server" Text="Xuất file mẫu" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/export1.png"
                                CausesValidation="false" CommandName="Export" OnClientClicked="btnExportTemplate" TabIndex="5">
                            </tlk:RadButton>
                            <tlk:RadButton Width="112px" ID="btnImport" runat="server" Text="Nhập file mẫu" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/import1.png"
                                CausesValidation="false" CommandName="Import" TabIndex="6">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="70px" ID="btnDelete" runat="server" Text="Xóa" CausesValidation="false" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                CommandName="Delete" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                        ReadOnly="true" />
                    <tlk:GridBoundColumn HeaderText="Mã CBCNV" DataField="EMPLOYEE_CODE"
                        ReadOnly="true" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="130px"/>
                    <tlk:GridBoundColumn HeaderText="Mã thẻ" DataField="MATHE_NAME" HeaderStyle-Width="100px"
                        ReadOnly="true" UniqueName="MATHE_NAME" SortExpression="MATHE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Tên CBCNV" DataField="EMPLOYEE_NAME" HeaderStyle-Width="120px"
                        ReadOnly="true" UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" HeaderStyle-Width="120px"
                        ReadOnly="true" UniqueName="ORG_NAME" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Chức danh hiện tại" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        HeaderStyle-Width="130px" ReadOnly="true" SortExpression="TITLE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Chức danh hội đồng" DataField="COMMITEE_TITLE_NAME" UniqueName="COMMITEE_TITLE_NAME"
                        HeaderStyle-Width="130px" ReadOnly="true" SortExpression="COMMITEE_TITLE_NAME" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSigner" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_CommiteeNewEdit_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_CommiteeNewEdit_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_CommiteeNewEdit_RadPane2';
        var validateID = 'MainContent_ctrlHU_CommiteeNewEdit_valSum';
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



        function btnResultHistoryClick(sender, args) {
            var lID = document.getElementById("<%= hidID.ClientID %>").value;
            if (lID == null || lID == 0) {
                m = '<%# Translate("Bạn cần lưu trước khi sử dụng chức năng này") %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            else {
                var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_CommiteeDetail&group=Business&ID=' + lID, "rwPopup");
                var pos = $("html").offset();
                oWindow.moveTo(pos.left, pos.top);
                oWindow.maximize();
            }
        }
        function btnExportTemplate(sender, args) {
            enableAjax = false;
        }
        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
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
            debugger;
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
