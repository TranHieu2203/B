<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TransferTripartiteNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_TransferTripartiteNewEdit" %>
<tlk:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<asp:HiddenField ID="hidEmp" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidDirect" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidStaffRank" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMassTransferSalary" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
            ValidationGroup="" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chuyển đi")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" SkinID="Readonly" ReadOnly="true"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên %>" ToolTip="<%$ Translate: Bạn phải chọn nhân viên %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtStaffRankOld" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgNameOld" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>

                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleNameOld" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleGroupOld" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại tờ trình")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionTypeOld" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số tờ trình")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNoOld" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDateOld" runat="server" SkinID="Readonly">
                        <DateInput Enabled="false">
                        </DateInput>
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDateOld" runat="server" SkinID="Readonly">
                        <DateInput Enabled="false">
                        </DateInput>
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thang lương")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalGroup" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm lương")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalLevel" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Bậc lương")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalRank" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lương cơ bản")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalBasic" runat="server" SkinID="Money">
                        <ClientEvents OnValueChanged="OnClientValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("% hưởng lương")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPercentSalary" runat="server" SkinID="Number">
                        <ClientEvents OnValueChanged="OnClientValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí hỗ trợ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostSupport" runat="server" SkinID="Money">
                        <ClientEvents OnValueChanged="OnClientValueChanged" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <tlk:RadGrid PageSize="50" ID="rgAllowCur" runat="server" Height="200px" Width="100%"
                        SkinID="GridNotPaging">
                        <MasterTableView>
                            <Columns>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                                    SortExpression="ALLOWANCE_LIST_NAME" UniqueName="ALLOWANCE_LIST_NAME" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                                    SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                                    ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đóng bảo hiểm %>" DataField="IS_INSURRANCE"
                                    SortExpression="IS_INSURRANCE" UniqueName="IS_INSURRANCE" HeaderStyle-Width="100px">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>
                            </Columns>
                            <HeaderStyle Width="100px" />
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chuyển đến")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" SkinID="Readonly" ReadOnly="true" />
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="reqOrgName" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalOrgName" ControlToValidate="txtOrgName" runat="server" ErrorMessage="<%$ Translate: Đơn vị không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Đơn vị không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTitle" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqTitle" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn chức danh %>"
                        ToolTip="<%$ Translate: Bạn phải chọn chức danh  %>" ControlToValidate="cboTitle">
                    </asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="cvalTitle" ControlToValidate="cboTitle" runat="server" ErrorMessage="<%$ Translate: Chức danh không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Chức danh không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleGroup" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại tờ trình")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDecisionType" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDecisionType" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại tờ trình %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại tờ trình  %>" ControlToValidate="cboDecisionType">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalDecisionType" ControlToValidate="cboDecisionType" runat="server" ErrorMessage="<%$ Translate: Loại tờ trình không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Loại tờ trình không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Số tờ trình")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Cấp nhân sự")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboStaffRank" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqStaffRank" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn cấp nhân sự %>"
                        ToolTip="<%$ Translate: Bạn phải chọn cấp nhân sự  %>" ControlToValidate="cboStaffRank">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalStaffRank" ControlToValidate="cboStaffRank" runat="server" ErrorMessage="<%$ Translate: Cấp nhân sự không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Cấp nhân sự không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                        <DateInput CausesValidation="false">
                        </DateInput>
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusExistEffectDate" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"
                        ToolTip="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>"
                        ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Quản lý trực tiếp")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtDirectManager" Width="130px" SkinID="Readonly"
                        ReadOnly="true" />
                    <tlk:RadButton runat="server" ID="btnFindDirect" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin phê duyệt")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trạng thái")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqStatus" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn trạng thái %>"
                        ToolTip="<%$ Translate: Bạn phải chọn trạng thái  %>" ControlToValidate="cboStatus">
                    </asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="cusStatus" ControlToValidate="cboStatus" runat="server" ErrorMessage="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày ký")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Người ký")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignName" runat="server" SkinID="Readonly" ReadOnly="true"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSign" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignTitle" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" Width="88%" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindDirect" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function cusDecisionType(oSrc, args) {
            var cbo = $find("<%# cboDecisionType.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }


        function cusStaffRank(oSrc, args) {
            var cbo = $find("<%# cboStaffRank.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
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

        function clientButtonClicking(sender, args) {
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }

        function OnDateSelected(sender, e) {
            var datePicker = $find("<%= rdEffectDate.ClientID %>");
            var date = datePicker.get_selectedDate();
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboSalGroup.ClientID %>':
                    cbo = $find('<%= cboSalLevel.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboSalRank.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    break;
                case '<%= cboSalLevel.ClientID %>':
                    cbo = $find('<%= cboSalRank.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    break;
                case '<%= cboSalRank.ClientID %>':
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    var item = eventArgs.get_item();
                    if (item) {
                        cbo.set_value(item.get_attributes().getAttribute("SALARY_BASIC"));
                    }
                    break;
                case '<%= cboTitle.ClientID %>':
                    cbo = $find('<%= txtTitleGroup.ClientID %>');
                    clearSelectRadtextbox(cbo);
                    var item = eventArgs.get_item();
                    if (item) {
                        cbo.set_value(item.get_attributes().getAttribute("GROUP_NAME"));
                    }
                    break;
                default:
                    break;
            }
        }


        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboSalGroup.ClientID %>':
                    cbo = $find('<%= rdEffectDate.ClientID %>');
                    var date = cbo.get_selectedDate();
                    if (date) {
                        var day = cbo.get_selectedDate().getDate();
                        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                        var month = months[cbo.get_selectedDate().getMonth()];
                        var year = cbo.get_selectedDate().getFullYear();
                        value = day + "/" + month + "/" + year;
                    }
                    break;
                case '<%= cboSalLevel.ClientID %>':
                    cbo = $find('<%= cboSalGroup.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboSalRank.ClientID %>':
                    cbo = $find('<%= cboSalLevel.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboTitle.ClientID %>':
                    value = $get("<%= hidOrg.ClientID %>").value;
                    break;
                default:
                    break;
            }

            if (!value) {
                value = null;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;

        }

        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }

        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }


        function OnClientValueChanged(sender, args) {

        }

        function RedirectTerminate(empID) {
            document.location.href = 'Dialog.aspx?mid=Profile&fid=ctrlHU_Terminate3BNewEdit&group=Business&noscroll=1&FormType=3&empid=' + empID;
        }
    </script>
</tlk:RadScriptBlock>
