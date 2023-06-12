<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Allowance.ascx.vb"
    Inherits="Profile.ctrlHU_Allowance" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    #ctl00_MainContent_ctrlHU_Allowance_ctrlOrg_trvOrgPostback {
        height: 64% !important;
    }
</style>
<asp:HiddenField runat="server" ID="hidEmployeeID" />
<asp:HiddenField runat="server" ID="hidEmployee_SID" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:PlaceHolder ID="phEMPR" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phEMPRS" runat="server"></asp:PlaceHolder>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="320px" Scrolling="None">
        <table class="table-form" onkeydown="return (event.keyCode!=13)" Width="300px">
            <tr>
                <td class="lb">
                    <%# Translate("Nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSeachEmployee" Width="188px">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFrom" runat="server" Width="194px">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTo" runat="server" Width="194px">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Khoản phụ cấp")%>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboLPC" runat="server" width="190px">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkNewEmp" runat="server" Text="<%$ Translate: Tìm mới nhất %>" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                        Text="<%$ Translate: Tìm %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
        <hr />
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Height="140px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtEmployeeName" AutoPostBack="true" >
                                <ClientEvents OnKeyPress="OnKeyPress" />
                                </tlk:RadTextBox>
                            <tlk:RadButton runat="server" ID="btnFindR" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeName"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Vị trí công việc")%><span class="lbReq"></span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTitleName" runat="server" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                         <td class="lb">
                            <%# Translate("Số tiền")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtAmount" runat="server" SkinID="Money1">
                            </tlk:RadNumericTextBox>
                            <%--AutoPostBack="true" CausesValidation="false"--%>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="lb">
                            <%# Translate("Khoản phụ cấp")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboALLOWANCE_LIST" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboALLOWANCE_LIST"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn phụ cấp. %>" ToolTip="<%$ Translate: Bạn phải chọn phụ cấp. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbUpload" runat="server" Text="Tập tin đính kèm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                            </tlk:RadTextBox>
                            <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                                TabIndex="3" />
                            <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                                CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb"></td>
                        <td style="display:none">
                            <tlk:RadButton runat="server" ID="chk_Is_Deduct" ToggleType="CheckBox" ButtonType="ToggleButton"
                                Enabled="false" CausesValidation="false" Text=" <%$ Translate: Khoản trừ %>"
                                AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEFFECT_DATE" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdEFFECT_DATE"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày hiệu lực %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày hết hiệu lực")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEXPIRE_DATE" runat="server">
                            </tlk:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực %>"
                                ErrorMessage="<%$ Translate: Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực %>"
                                Type="Date" Operator="GreaterThan" ControlToCompare="rdEFFECT_DATE" ControlToValidate="rdEXPIRE_DATE"></asp:CompareValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox runat="server" ID="txtRemark" Width="100%" SkinID="Textbox1023">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                                          
                       <%-- <td class="lb">
                            <%# Translate("Hệ số")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtFactor" runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                        </td>--%>
                 
                   <%-- <tr>
                        <td class="lb">
                            <%# Translate("% thuế tạm tính")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnPercentPIT" SkinID="Decimal" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Thuế tạm tính")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnAmountPIT" SkinID="Money1" ReadOnly="true">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>--%>                    
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50"
                    Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true"  >
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,PERCENT_PIT,AMOUNT_PIT,
                    EMPLOYEE_NAME,ALLOWANCE_LIST_NAME,ALLOWANCE_LIST_ID,AMOUNT,
                    FACTOR,EXPIRE_DATE,EFFECT_DATE,EMPLOYEE_SIGNER_NAME,FILE_NAME,UPLOAD_FILE,
                    EMPLOYEE_SIGNER_SR_NAME,REMARK,EMPLOYEE_SIGNER_TITLE_NAME,DATE_SIGNER,EMPLOYEE_SIGNER_ID,ORG_DESC,ORG_NAME1,ORG_NAME2,ORG_NAME3,PAYROLL,STAFF_RANK_NAME,TITLE_NAME_VN,TITLE_JOB_NAME,LEARNING_LEVEL_NAME,DETAIL_ADD,DETAIL_ADD_NAME,IS_DEDUCT">
                        <Columns>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindReimbursement" runat="server"></asp:PlaceHolder>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlHU_Allowance_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Allowance_RadPane2';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Allowance_RadPane1';
        var validateID = 'MainContent_ctrlHU_Allowance_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgMain.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
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
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            }else if (args.get_item().get_commandName() == "EXPORT_TEMPLATE") {
                enableAjax = false;
            }
            else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
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

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>
