<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_WelfareMngAuto.ascx.vb"
    Inherits="Profile.ctrlHU_WelfareMngAuto" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWelfareMngs" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="200px" Scrolling="None">
                <table class="table-form" style="margin-top:12px">
                    <tr>
                        <td class="lb">
                            <%# Translate("Loại phúc lợi")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboWelfareType" runat="server" AutoPostBack="True" TabIndex="11"
                                CausesValidation="False" Width="200px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên phúc lợi")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server" ReadOnly="true" Width="200px">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Thâm niên (tháng)")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtSENIORITY" SkinID="Number" MaxLength="38" runat="server"
                                ReadOnly="true" Width="200px">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số tiền")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtMONEY" MinValue="1" MaxLength="38" runat="server"
                                ReadOnly="true" Width="200px">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tuổi con nhỏ từ")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtCHILD_OLD_FROM" MinValue="0" MaxLength="38" runat="server"
                                ReadOnly="true" Width="200px">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tuổi con nhỏ đến")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtCHILD_OLD_TO" MinValue="0" MaxLength="38" runat="server"
                                ReadOnly="true" Width="200px">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Giới tính")%>
                        </td>
                        <td>
                            <tlk:RadListBox ID="lstbGender" CheckBoxes="true" runat="server" Height="80px" Width="200px"
                                Enabled="false" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                            </tlk:RadListBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại hợp đồng")%>
                        </td>
                        <td>
                            <tlk:RadListBox ID="lstCONTRACT_TYPE" CheckBoxes="true" runat="server" Height="80px"
                                Enabled="false" Width="200px" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                            </tlk:RadListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdSTART_DATE" ReadOnly="true" DateInput-ReadOnly="true"
                                DatePopupButton-Visible="false" Width="200px">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày hết hiệu lực")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEND_DATE" ReadOnly="true" DateInput-ReadOnly="true"
                                DatePopupButton-Visible="false" Width="200px">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100%">
                <tlk:RadGrid PageSize="50" ID="rgWelfareMng" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID" EditMode="InPlace" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                ReadOnly="true" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                ReadOnly="true" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                                ReadOnly="true" ItemStyle-HorizontalAlign="Center" UniqueName="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                ReadOnly="true" UniqueName="TITLE_NAME" HeaderStyle-Width="200px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                ReadOnly="true" UniqueName="ORG_NAME" HeaderStyle-Width="200px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng số con %>" DataField="COUNT_CHILD"
                                ReadOnly="true" UniqueName="COUNT_CHILD" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                                ReadOnly="true" UniqueName="GENDER_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Thâm niên (tháng) %>" DataField="SENIORITY"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" UniqueName="SENIORITY"
                                DataType="System.UInt64" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Loại HĐLĐ %>" DataField="CONTRACT_TYPE_NAME"
                                ReadOnly="true" UniqueName="CONTRACT_TYPE_NAME">
                                <HeaderStyle Width="250px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="MONEY" UniqueName="MONEY"
                                DataFormatString="{0:N0}">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số tiền %>" DataField="MONEYTOTAL"
                                UniqueName="MONEYTOTAL" DataFormatString="{0:N0}">
                            </tlk:GridNumericColumn>
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move" Modal="true"
            ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin quản lý phúc l%>"
            OnClientClose="popupclose">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_WelfareMngAuto_RadSplitter3');
        }

        function CheckValidate() {
            var bCheck = $find('<%= rgWelfareMng.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            return 0;
        }
        function OpenNew() {
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_WelfareMngNewEdit&group=Business', "_self"); /*
            oWindow.setSize(900, 500);
            oWindow.center(); */
        }
        function OpenEdit() {
            var id = $find('<%= rgWelfareMng.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_WelfareMngNewEdit&group=Business&gUID=' + id + '', "_self"); /*
            oWindow.setSize(900, 500);
            oWindow.center(); */
            //            var pos = $("html").offset();
            //            oWindow.moveTo(pos.left, pos.top);
            //            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
        }
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EDIT') {
                OpenEdit();
                args.set_cancel(true);
            }
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }
        var enableAjax = true;
        var oldSize = 0;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgWelfareMng.ClientID %>").get_masterTableView().rebind();
            }

        }

        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }
    </script>
</tlk:RadCodeBlock>
