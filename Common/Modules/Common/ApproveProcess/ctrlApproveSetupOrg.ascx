<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlApproveSetupOrg.ascx.vb"
    Inherits="Common.ctrlApproveSetupOrg" %>
<%@ Register Src="../ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<style type="text/css">
    #ctl00_MainContent_ctrlApproveSetupOrg_rgDetail_ctl00
    {
        width: 100% !important;
    }
</style>
<tlk:RadSplitter runat="server" ID="splitFull" Width="100%" Height="100%">
    <tlk:RadPane runat="server" ID="paneLeftFull" Width="250" MaxWidth="300">
        <common:ctrlorganization runat="server" id="ctrlOrg" />
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="paneRightFull" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="220px" Scrolling="None">
                <tlk:RadToolBar runat="server" ID="tbarDetail" Width="100%" OnClientButtonClicking="OnClientButtonClicking" />
                <asp:ValidationSummary runat="server" ID="valSummaryVal" />
                <asp:Panel runat="server" ID="pnlDetail" Enabled="false">
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Quy trình")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboApproveProcess" Width="250px">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Hiệu lực từ ngày")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdFromDate">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator runat="server" ID="reqFromDate" ControlToValidate="rdFromDate"
                                    ErrorMessage='<%$ Translate: Chưa nhập Hiệu lực từ ngày %>'></asp:RequiredFieldValidator>
                                <%--<asp:CustomValidator runat="server" ID="cvalCheckDateExist" ErrorMessage='<%$ Translate: Đã có thiết lập áp dụng vào khoảng thời gian bạn chọn. %>'></asp:CustomValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Đến ngày")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker runat="server" ID="rdToDate">
                                </tlk:RadDatePicker>
                                <asp:CompareValidator runat="server" ID="compareFromDateToDate" ErrorMessage="<%$ Translate: Từ ngày lớn hơn đến ngày. %>"
                                    ToolTip="<%$ Translate: Từ ngày lớn hơn đến ngày. %>" ControlToCompare="rdFromDate"
                                    ControlToValidate="rdToDate" Operator="GreaterThan" Type="Date">
                                </asp:CompareValidator>
                            </td>
                            <td class="lb" id="lbKieuCong" runat="server">
                                <%# Translate("Loại phép")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboKieuCong">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Template áp dụng")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboApproveTemplate"  Width="250px">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Cấp chức danh")%>
                            </td>
                            <td>
                                <tlk:RadComboBox rendermode="Lightweight" ID="cboPosition" AutoPostBack="true" CausesValidation="false"
                                    runat="server" CheckBoxes="true" Filter="StartsWith" MarkFirstMatch="True" EnableCheckAllItemsCheckBox="true"
                                    Width="100%" Label="">
                                    <Items>
                                    </Items>
                                    <Localization CheckAllString="Chọn tất cả" AllItemsCheckedString="Tất cả cấp chức danh"
                                        ItemsCheckedString="Cấp chức danh đã chọn" />
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Chức danh")%>
                            </td>
                            <td>
                                <tlk:RadComboBox rendermode="Lightweight" ID="cboTitle" AutoPostBack="true" CausesValidation="false"
                                    runat="server" CheckBoxes="true" Filter="StartsWith" MarkFirstMatch="True" EnableCheckAllItemsCheckBox="true"
                                    Width="100%" Label="">
                                    <Items>
                                    </Items>
                                    <Localization CheckAllString="Chọn tất cả" AllItemsCheckedString="Tất cả chức danh" 
                                        ItemsCheckedString="Chức danh đã chọn" />
                                </tlk:RadComboBox>                              
                            </td>
                            <td class="lb" style="display: none">
                                <%# Translate("Kế hoạch nghỉ")%>
                            </td>
                            <td style="display: none">
                                <tlk:RadComboBox runat="server" ID="cboLeavePlan" Width="150px">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="display: none">
                                <%# Translate("Số giờ từ")%>
                            </td>
                            <td style="display: none">
                                <tlk:RadNumericTextBox runat="server" ID="rntxtFromHour" MinValue="0" Width="150px"
                                    DataType="System.Decimal">
                                    <NumberFormat DecimalDigits="2" AllowRounding="false" DecimalSeparator="." KeepNotRoundedValue="true" />
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb" style="display: none">
                                <%# Translate("đến")%>
                            </td>
                            <td style="display: none">
                                <tlk:RadNumericTextBox runat="server" ID="rntxtToHour" MinValue="0" Width="150px">
                                    <NumberFormat DecimalDigits="2" AllowRounding="false" DecimalSeparator="." KeepNotRoundedValue="true" />
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("CC mail trong khi duyệt")%>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtCCMailAccepting"  Width="250px">
                                </tlk:RadTextBox>
                            </td>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                ErrorMessage="Vui lòng nhập email hợp lệ tại trường CC mail trong khi duyệt."
                                ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                ControlToValidate="txtCCMailAccepting">
                            </asp:RegularExpressionValidator>
                            <td class="lb">
                                <%# Translate("Số ngày ĐK trễ cho phép")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox runat="server" ID="txtFromHour" MinValue="0">
                                    <NumberFormat DecimalDigits="2" AllowRounding="false" DecimalSeparator="." KeepNotRoundedValue="true" />
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb" id="lbFromDay" runat="server">
                                <%# Translate("Số ngày ĐK từ")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox runat="server" ID="rntxtFromDay" MinValue="0">
                                    <NumberFormat DecimalDigits="2" AllowRounding="false" DecimalSeparator="." KeepNotRoundedValue="true" />
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("CC mail khi được duyệt")%>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtCCMailAccepted"  Width="250px">
                                </tlk:RadTextBox>
                            </td>
                            <asp:RegularExpressionValidator ID="emailValidator" runat="server" Display="Dynamic"
                                ErrorMessage="Vui lòng nhập email hợp lệ tại trường CC mail khi được duyệt."
                                ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                ControlToValidate="txtCCMailAccepted">
                            </asp:RegularExpressionValidator>
                            <td class="lb">
                                <%# Translate("Số ngày PD trễ cho phép")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox runat="server" ID="txtToHour" MinValue="0">
                                    <NumberFormat DecimalDigits="2" AllowRounding="false" DecimalSeparator="." KeepNotRoundedValue="true" />
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb" id="lbToDay" runat="server">
                                <%# Translate("Số ngày ĐK đến")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox runat="server" ID="rntxtToDay" MinValue="0">
                                    <NumberFormat DecimalDigits="2" AllowRounding="false" DecimalSeparator="." KeepNotRoundedValue="true" />
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:CheckBox id="chkIgnoreapprovelvl" runat="server"/> <%# Translate("Bỏ qua cấp phê duyệt không tồn tại")%>
                            </td>
                            <td></td>
                            <td>
                                <asp:CheckBox id="ChkDB" runat="server" Text= "Vượt định biên"/> 
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="Y">
                <tlk:RadGrid PageSize="50" runat="server" ID="rgDetail" Height="100%" SkinID="GridSingleSelect">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                        <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="false" />
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" GroupsDefaultExpanded="true">
                        <GroupByExpressions>
                            <tlk:GridGroupByExpression>
                                <GroupByFields>
                                    <tlk:GridGroupByField FieldName="PROCESS_NAME" />
                                </GroupByFields>
                                <SelectFields>
                                    <tlk:GridGroupByField FieldName="PROCESS_NAME" HeaderText="Quy trình" />
                                </SelectFields>
                            </tlk:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Template %>' DataField="TEMPLATE_NAME"
                                UniqueName="TEMPLATE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày hiệu lực %>' DataField="FROM_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="FROM_DATE">
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày kết thúc %>' DataField="TO_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="TO_DATE">
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Chức danh %>' DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Nhóm chức danh %>' DataField="GROUP_TITLE_NAME"
                                UniqueName="GROUP_TITLE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: CC mail trong khi duyệt %>' DataField="MAIL_ACCEPTING"
                                UniqueName="MAIL_ACCEPTING">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: CC mail trong khi được duyệt %>' DataField="MAIL_ACCEPTED"
                                UniqueName="MAIL_ACCEPTED">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText='<%$ Translate: Số ngày DK trễ cho phép  %>' DataField="FROM_HOUR"
                                UniqueName="FROM_HOUR">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText='<%$ Translate: Số ngày PD trễ cho phép %>' DataField="TO_HOUR" UniqueName="TO_HOUR">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Loại phép %>' DataField="SIGN_NAME"
                                UniqueName="SIGN_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText='<%$ Translate:Số ngày ĐK từ %>' DataField="FROM_DAY"
                                UniqueName="FROM_DAY">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText='<%$ Translate: Số ngày ĐK đến %>' DataField="TO_DAY" UniqueName="TO_DAY">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Email thông báo %>' DataField="REQUEST_EMAIL"
                                UniqueName="REQUEST_EMAIL">
                            </tlk:GridBoundColumn>
                            <tlk:GridCheckBoxColumn HeaderText='<%$ Translate: Bỏ qua cấp phê duyệt không tồn tại %>' DataField="IS_IGNORE_APPROVE_LEVEL"
                                UniqueName="IS_IGNORE_APPROVE_LEVEL" AllowFiltering="false" ShowFilterIcon="false">
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn HeaderText='<%$ Translate: Vượt định biên %>' DataField="IS_OVER_LIMIT"
                                UniqueName="IS_OVER_LIMIT" AllowFiltering="false" ShowFilterIcon="false">
                            </tlk:GridCheckBoxColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<common:ctrlupload id="ctrlUpload1" runat="server" />
<tlk:RadScriptBlock runat="server" ID="radScriptBlock">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlApproveSetupOrg_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlApproveSetupOrg_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlApproveSetupOrg_RadPane2';
        var validateID = 'MainContent_ctrlApproveSetupOrg_valSummaryVal';
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

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDetail');
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == "NEXT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }

            switch (args.get_item().get_commandName()) {
                case 'CREATE':
                    if ($find('<%= ctrlOrg.TreeClientID %>').get_selectedNodes().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn phòng ban cần thiết lập', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        args.set_cancel(true);
                    }
                    break;
                case 'EDIT':
                    if ($find('<%= rgDetail.ClientID %>').get_masterTableView().get_selectedItems().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn bản ghi nào! Không thể thực hiện thao tác này', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        args.set_cancel(true);
                    }
                    break;
                case 'DELETE':
                    if ($find('<%= rgDetail.ClientID %>').get_masterTableView().get_selectedItems().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn dòng cần xóa', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        args.set_cancel(true);
                    }

                    break;
            }
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
    </script>
</tlk:RadScriptBlock>
