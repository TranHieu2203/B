<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlApproveTemplate.ascx.vb"
    Inherits="Common.ctrlApproveTemplate" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
.RadToolbarDelete {
    float: right;
    margin-right: 15px !important;
}
</style>
<tlk:RadSplitter runat="server" ID="splitFull" Width="100%" Height="100%">
    <tlk:RadPane runat="server" ID="paneLeftFull" Width="400" MaxWidth="400" Scrolling="None">
        <tlk:RadSplitter runat="server" ID="splitLeftFull" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="32px" Scrolling="None">
                <tlk:RadToolBar runat="server" ID="tbarTemplate" OnClientButtonClicking="tbarTemplate_ClientButtonClicking"
                    Width="100%">
                </tlk:RadToolBar>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" runat="server" ID="rgTemplate" Height="100%" SkinID="GridSingleSelect">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Tên Template %>' DataField="TEMPLATE_NAME"
                                UniqueName="TEMPLATE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridTemplateColumn HeaderText='<%$ Translate: Đối tượng áp dụng %>' UniqueName="TEMPLATE_TYPE"
                                HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <%# If(Eval("TEMPLATE_TYPE") = "0", Translate("Đơn vị/Phòng ban"), Translate("Nhân viên"))%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Độ ưu tiên %>' DataField="TEMPLATE_ORDER"
                                UniqueName="TEMPLATE_ORDER" HeaderStyle-Width="50px">
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
                <div style="display: none">
                    <asp:Button runat="server" ID="btnReloadGrid" CausesValidation="false" />
                </div>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="paneRightFull" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane5" runat="server" Height="180px" Scrolling="None">
                <tlk:RadToolBar runat="server" ID="tbarTemplateDetail" Width="100%" OnClientButtonClicking="tbarTemplateDetail_ClientButtonClicking"
                    ValidationGroup="Detail">
                </tlk:RadToolBar>
                <asp:ValidationSummary runat="server" ID="valSum" />
                <asp:Panel runat="server" ID="pnlDetail" Enabled="false">
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Cấp phê duyệt")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox runat="server" ID="nntxtAppLevel" NumberFormat-DecimalDigits="1" MinValue="1"
                                    ValidationGroup="Detail" ShowSpinButtons="true" Width="60px">
                                </tlk:RadNumericTextBox>
                                <asp:CustomValidator runat="server" ID="cvalLevel" ErrorMessage='<%$ Translate: Cấp phê duyệt đã tồn tại. %>'></asp:CustomValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Loại phê duyệt")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboAppType" AutoPostBack="true" CausesValidation="false">
                                    <Items>
                                        <tlk:RadComboBoxItem runat="server" Text='<%$ Translate: Quản lý trực tiếp %>' Value="0"  />
                                        <tlk:RadComboBoxItem runat="server" Text='<%$ Translate: Chọn nhân viên %>' Value="1" />
                                        <tlk:RadComboBoxItem runat="server" Text='<%$ Translate: Quản lý gián tiếp %>' Value="2" />
                                        <tlk:RadComboBoxItem runat="server" Text='<%$ Translate: HR %>' Value="3" />
                                        <tlk:RadComboBoxItem runat="server" Text='<%$ Translate: Tổng giám đốc bộ phận %>' Value="4" />
                                        <tlk:RadComboBoxItem runat="server" Text='<%$ Translate: Quản lý trực tiếp của cấp phê duyệt trước đó %>' Value="5" />
                                        <tlk:RadComboBoxItem runat="server" Text='<%$ Translate: Vị trí công việc %>' Value="6"  />
                                    </Items>
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel runat="server" ID="pnlSelectEmp" GroupingText='<%$ Translate: Người phê duyệt %>'>
                                    <%# Translate("Nhân viên: ")%>
                                    <tlk:RadTextBox runat="server" ID="txtEmplloyeeCode" EmptyMessage="Mã NV" Width="60px"
                                        ReadOnly="true">
                                    </tlk:RadTextBox>
                                    <tlk:RadTextBox runat="server" ID="txtEmployeeName" EmptyMessage="Họ tên" Width="150px"
                                        ReadOnly="true">
                                    </tlk:RadTextBox>
                                    <span class="lbReq">*</span>
                                    <asp:RequiredFieldValidator runat="server" ID="reqEmployee" ControlToValidate="txtEmplloyeeCode"
                                        ErrorMessage='<%$ Translate: Chưa chọn nhân viên %>'></asp:RequiredFieldValidator>
                                    <tlk:RadButton runat="server" ID="btnSearchEmp" SkinID="ButtonView" CausesValidation="false">
                                    </tlk:RadButton>
                                    <asp:HiddenField runat="server" ID="hidEmployeeID" />
                                </asp:Panel>
                            </td>
                             <td colspan="4">
                                <asp:Panel runat="server" ID="pnlCapChucDanhPheDuyet" GroupingText='<%$ Translate: Vị Trí Công Việc Phê Duyệt %>'>
                                     <%# Translate("Vị trí: ")%>
                                    <%--<tlk:RadComboBox runat="server" ID="cboCapChucDanhPheDuyet" Enabled="false">
                                    </tlk:RadComboBox>--%>
                                    <tlk:RadComboBox runat="server" ID="cboCapChucDanhPheDuyet" Width="94%" AutoPostBack="true" NoWrap="false" 
                                            CausesValidation="false" Filter="Contains" HighlightTemplatedItems="true">
                                        <HeaderTemplate>
                                            <table style="width: 820px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 570px;">
                                                        Tên vị trí
                                                    </td>
                                                    <td style="width: 250px;">
                                                        Master
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 820px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 570px;">
                                                        <%# Eval("NAME_VN")%>
                                                    </td>
                                                    <td style="width: 250px;">
                                                        <%# Eval("MASTER_NAME")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </tlk:RadComboBox>
                                    <span class="lbReq">*</span>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="lb">
                                <%# Translate("Inform date")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox runat="server" ID="rntxtInformDate" ShowSpinButtons="true" MinValue="0"
                                    MaxValue="99" Width="50">
                                    <NumberFormat DecimalDigits="1" />
                                </tlk:RadNumericTextBox>
                                <%--                                <asp:CustomValidator runat="server" ID="valCustomInformDate" ErrorMessage='<%$ Translate: Chưa nhập Inform date. %>'></asp:CustomValidator>
                                --%>
                            </td>
                            <td class="lb">
                                <%# Translate("Inform Email")%>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtInformEmail" ValidationGroup="Detail">
                                </tlk:RadTextBox>
                                 <asp:RegularExpressionValidator Display="Dynamic" ID="cvalMailAddress" ControlToValidate="txtInformEmail" runat="server"
                                    ErrorMessage="<%$ Translate: ĐỊNH DẠNG EMAIL KHÔNG CHÍNH XÁC%>" ToolTip="<%$ Translate: ĐỊNH DẠNG EMAIL KHÔNG CHÍNH XÁC%>"
                                    ValidationExpression="^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" runat="server" ID="rgDetail" Height="100%" SkinID="GridSingleSelect">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                        <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="false" />
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,APP_TYPE" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Cấp phê duyệt %>' DataField="APP_LEVEL"
                                UniqueName="APP_LEVEL">
                                <ItemStyle HorizontalAlign="Right" /> 
                            </tlk:GridBoundColumn>
                            <tlk:GridTemplateColumn HeaderText='<%$ Translate: Người phê duyệt %>'>
                                <ItemTemplate>
                                    <%# If(Eval("APP_TYPE") = Decimal.Parse("0"), Translate("Quản lý trực tiếp"), Translate(""))%>
                                    <%# If(Eval("APP_TYPE") = Decimal.Parse("1"), Translate("Nhân viên"), Translate(""))%>
                                    <%# If(Eval("APP_TYPE") = Decimal.Parse("2"), Translate("Quản lý gián tiếp"), Translate(""))%>
                                    <%# If(Eval("APP_TYPE") = Decimal.Parse("3"), Translate("HR"), Translate(""))%>
                                    <%# If(Eval("APP_TYPE") = Decimal.Parse("4"), Translate("Tổng giám đốc bộ phận"), Translate(""))%>
                                    <%# If(Eval("APP_TYPE") = Decimal.Parse("5"), Translate("Quản lý trực tiếp của cấp phê duyệt trước đó"), Translate(""))%>
                                    <%# If(Eval("APP_TYPE") = Decimal.Parse("6"), Translate("Vị trí công việc"), Translate(""))%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Mã NV %>' DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Họ tên %>' DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME">
                            </tlk:GridBoundColumn>
                            <%--<tlk:GridBoundColumn HeaderText='<%$ Translate: Inform Date %>' DataField="INFORM_DATE"
                                UniqueName="INFORM_DATE">
                                <ItemStyle HorizontalAlign="Right" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Inform Email %>' DataField="INFORM_EMAIL"
                                UniqueName="INFORM_EMAIL">
                            </tlk:GridBoundColumn>--%>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phImportLogs" runat="server"></asp:PlaceHolder>
<tlk:RadWindowManager ID="rwmPopup" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="450px"
            OnClientClose="popupclose" Height="250px" EnableShadow="true" Behaviors="Close"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlApproveTemplate_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlApproveTemplate_RadPane5';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlApproveTemplate_RadPane4';
        var validateID = 'MainContent_ctrlApproveTemplate_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
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

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut(splitterID);
        //        }

        function tbarTemplateDetail_ClientButtonClicking(s, e) {
            if (e.get_item().get_commandName() == "EXPORT" || e.get_item().get_commandName() == "NEXT") {
                enableAjax = false;
            }
            if (e.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter();
                else
                    ResizeSplitterDefault();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
            switch (e.get_item().get_commandName()) {
                case 'EDIT':
                    if ($find('<%= rgDetail.ClientID %>').get_masterTableView().get_selectedItems().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn bản ghi nào! Không thể thực hiện thao tác này', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        e.set_cancel(true);
                    }
                    break;
                case 'DELETE':
                    if ($find('<%= rgDetail.ClientID %>').get_masterTableView().get_selectedItems().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn dòng cần xóa', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        e.set_cancel(true);
                        break;
                    }
                    break;
            }
        }

        function SelectEmp(s, e) {
            e.set_cancel(true);
        }

        function tbarTemplate_ClientButtonClicking(s, e) {
            switch (e.get_item().get_commandName()) {
                case "CREATE":
                    $find("<%=rwPopup.ClientID %>").show();
                    radopen('Dialog.aspx?mid=Common&fid=ctrlApproveTemplateAddEdit&noscroll=1&group=ApproveProcess', "rwPopup");
                    e.set_cancel(true);
                    break;
                case 'EDIT':
                    if ($find('<%= rgTemplate.ClientID %>').get_masterTableView().get_selectedItems().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn Template cần sửa', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        break;
                    }

                    var templateId = $find('<%= rgTemplate.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue("ID");

                    $find("<%=rwPopup.ClientID %>").show();
                    radopen('Dialog.aspx?mid=Common&fid=ctrlApproveTemplateAddEdit&group=ApproveProcess&noscroll=1&ID=' + templateId, "rwPopup");
                    e.set_cancel(true);
                    break;
                case 'DELETE':
                    if ($find('<%= rgTemplate.ClientID %>').get_masterTableView().get_selectedItems().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn Template cần xóa', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        break;
                    }
                    break;
            }
        }

        function popupclose(s, e) {
            if (e.get_argument() == '1') {
                $get('<%= btnReloadGrid.ClientID %>').click();
            }
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%=rgTemplate.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane5.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;

            } else {
                var pane2 = splitter.getPaneById('<%= RadPane4.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadScriptBlock>
