<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_WelfareList.ascx.vb"
    Inherits="Profile.ctrlHU_WelfareList" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="RadPane3" Width="100%" Height="100%" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="95%" Height="95%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="240px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWelfareLists" runat="server" />
                <asp:ValidationSummary Enable="false" Visible="false" ID="valSum" runat="server" />
                <table class="table-form">
                    <tr>
                        <td colspan="4">
                        </td>
                        <td>
                            <asp:Label ID="lbGender" runat="server" Text="Giới tính"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbContractType" runat="server" Text="Loại hợp đồng"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbName" runat="server" Text="Tên chế độ phúc lợi"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboName" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                            <asp:CustomValidator ID="cusName" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tên chế độ phúc lợi. %>"
                                ToolTip="<%$ Translate: Bạn phải chọn tên chế độ phúc lợi.%>" ClientValidationFunction="cusName">
                            </asp:CustomValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbMoney" runat="server" Text="Số tiền"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="nmMONEY" MinValue="1" MaxLength="38" runat="server">
                            </tlk:RadNumericTextBox>
                          <%--  <asp:RequiredFieldValidator Enable="false" ID="cvalMONEY" ControlToValidate="nmMONEY" runat="server"
                                ErrorMessage="Bạn phải nhập số tiền." ToolTip="Bạn phải nhập số tiền.">
                            </asp:RequiredFieldValidator>--%>
                        </td>
                        <td rowspan="5" style="vertical-align: top;">
                            <tlk:RadListBox ID="lstbGender" CheckBoxes="true" runat="server" Height="60px" Width="100px"
                                OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                            </tlk:RadListBox>
                        </td>
                        <td rowspan="5" style="vertical-align: top;">
                            <tlk:RadListBox ID="lstCONTRACT_TYPE" CheckBoxes="true" runat="server" Height="135px"
                                Width="290px">
                            </tlk:RadListBox>
                            <asp:CustomValidator ID="cvalContractType" runat="server" ErrorMessage="Loại hợp đồng không tồn tại hoặc đã ngừng áp dụng."
                                ToolTip="Loại hợp đồng không tồn tại hoặc đã ngừng áp dụng.">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbSENIORITY" runat="server" Text="Thâm niên (tháng)"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="nmSENIORITY" SkinID="Number" MaxLength="38" runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbDenSoThang" runat="server" Text="Đến số tháng "></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="nmDenSoThang" SkinID="Number" MaxLength="38" runat="server">
                            </tlk:RadNumericTextBox>
                          <%--  <asp:CompareValidator ID="CompareValidator2" runat="server" ToolTip="<%$ Translate: Nhập đến số tháng lớn hơn từ số tháng  %>"
                                ErrorMessage="<%$ Translate: Nhập đến số tháng lớn hơn từ số tháng  %>" Type="Integer"
                                Operator="GreaterThan" ControlToCompare="nmSENIORITY" ControlToValidate="nmDenSoThang"></asp:CompareValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbCHILD_OLD_FROM" runat="server" Text="Tuổi con nhỏ từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="nmCHILD_OLD_FROM" MinValue="0" MaxLength="38" runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbCHILD_OLD_TO" runat="server" Text="đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="nmCHILD_OLD_TO" MinValue="0" MaxLength="38" runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbSTART_DATE" runat="server" Text="Ngày hiệu lực"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="dpSTART_DATE">
                            </tlk:RadDatePicker>
                          <%--  <asp:CustomValidator ID="dpSTART_DATE_custom" runat="server" ErrorMessage="Bạn phải nhập ngày hiệu lực."
                                ToolTip="Bạn phải nhập ngày hiệu lực.">
                            </asp:CustomValidator>--%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="dpSTART_DATE"
                                runat="server" ErrorMessage="" ToolTip="Bạn phải nhập ngày hiệu lực.">
                            </asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbEND_DATE" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="dpEND_DATE">
                            </tlk:RadDatePicker>
                          <%--  <asp:CompareValidator Enable="false" ID="CompareValidator1" runat="server" ToolTip="Ngày hết hiệu lực phải lớn hơn ngày hiệu lực."
                                ErrorMessage="Ngày hết hiệu lực phải lớn hơn ngày hiệu lực." Type="Date" Operator="GreaterThan"
                                ControlToCompare="dpSTART_DATE" ControlToValidate="dpEND_DATE"></asp:CompareValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbGroupTitle" runat="server" Text="Nhóm chức danh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cbGroupTitle" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                        </td>
                        <td>
                            <tlk:RadButton ID="chkIS_AUTO" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton"
                                ValidateRequestMode="Disabled" ValidationGroup="a" runat="server" Text="Tự động áp dụng."
                                Visible="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb" style="display:none">
                            <asp:Label ID="lbCode" runat="server" Text="Mã chế độ phúc lợi"></asp:Label>
                        </td>
                        <td style="display:none">
                            <tlk:RadTextBox Enabled="false" ID="txtCode" SkinID="ReadOnly" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator Enabled="false" ID="reqCode" ControlToValidate="txtCode" runat="server"
                                CausesValidation="false" ErrorMessage="Bạn phải nhập mã chế độ phúc lợi." ToolTip="Bạn phải nhập mã chế độ phúc lợi.">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator Enabled="false" ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="Mã chế độ phúc lợi đã tồn tại."
                                ToolTip="Mã chế độ phúc lợi đã tồn tại.">
                            </asp:CustomValidator>
                            <asp:RegularExpressionValidator Enabled="false" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Mã không được chứa ký tự đặc biệt và khoảng trắng"
                                ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgWelfareList" runat="server" AllowPaging="True" Height="100%"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true"  EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE,NAME,CONTRACT_TYPE_NAME,CONTRACT_TYPE,GENDER,SENIORITY,MONEY,START_DATE,END_DATE,IS_AUTO,CHILD_OLD_FROM,CHILD_OLD_TO,ID_NAME,TITLE_GROUP_ID">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn uniquename="cbStatus" headertext="CheckBox" headerstyle-horizontalalign="center"
                        headerstyle-width="30px" itemstyle-horizontalalign="center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn datafield="id" visible="false" />
                    <tlk:GridBoundColumn headertext="Mã chế độ phúc lợi" datafield="code"
                        sortexpression="code" uniquename="code" />
                    <tlk:GridBoundColumn headertext="Tên chế độ phúc lợi" datafield="name"
                        sortexpression="name" uniquename="name">
                        <headerstyle width="200px" />
                    </tlk:gridboundcolumn>
                    <tlk:gridboundcolumn headertext="Loại hợp đồng" datafield="contract_type_name"
                        sortexpression="contract_type_name" uniquename="contract_type_name">
                        <headerstyle width="250px" />
                    </tlk:gridboundcolumn>
                    <tlk:gridboundcolumn headertext="Giới tính" datafield="gender_name"
                        sortexpression="gender_name" uniquename="gender_name" />
                    <tlk:gridnumericcolumn headertext="Thâm niên" datafield="seniority"
                        sortexpression="seniority" uniquename="seniority" />
                    <tlk:gridnumericcolumn headertext="Tuổi con nhỏ từ" datafield="child_old_from"
                        sortexpression="child_old_from" uniquename="child_old_from" />
                    <tlk:gridnumericcolumn headertext="Tuổi con nhỏ đến" datafield="child_old_to"
                        sortexpression="child_old_to" uniquename="child_old_to" />
                    <tlk:gridnumericcolumn headertext="Số tiền" datafield="money" sortexpression="money"
                        dataformatstring="{0:n0}" uniquename="money" />
                    <tlk:griddatetimecolumn headertext="Ngày bắt đầu" datafield="start_date"
                        dataformatstring="{0:dd/mm/yyyy}" sortexpression="start_date" uniquename="start_date" />
                    <tlk:griddatetimecolumn headertext="Ngày kết thúc" datafield="end_date"
                        dataformatstring="{0:dd/mm/yyyy}" sortexpression="end_date" uniquename="end_date" />
                    <tlk:gridcheckboxcolumn headertext="Tự động áp dụng" datafield="is_auto"
                        allowfiltering="false"  sortexpression="is_auto" uniquename="is_auto" />
                    <tlk:gridboundcolumn headertext="Trạng thái" datafield="actflg"
                        sortexpression="actflg" uniquename="actflg" />--%>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_WelfareList_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_WelfareList_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_WelfareList_RadPane2';
        var validateID = 'MainContent_ctrlHU_WelfareList_valSum';
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

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgWelfareList.ClientID %>').get_masterTableView().get_dataItems().length;
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
                    ResizeSplitter('ctl00_MainContent_ctrlHU_WelfareList_RadSplitter1', pane1ID, pane2ID, validateID, oldSize + 2000, 'rgWelfareList');
                else
                    ResizeSplitterDefault('ctl00_MainContent_ctrlHU_WelfareList_RadSplitter1', pane1ID, pane2ID, oldSize + 2000);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgWelfareList.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }
        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboName.ClientID%>':
                    cbo = $find('<%= cboName.ClientID%>');
                    clearSelectRadcombo(cbo);
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
                case '<%= cboName.ClientID%>':
                    cbo = $find('<%= cboName.ClientID%>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }

            if (!value) {
                value = 0;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;
            context["value"] = sender.get_value();

        }
        function cusName(oSrc, args) {
            var cbo = $find("<%# cboName.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
        }
     
         
    </script>
</tlk:RadCodeBlock>
