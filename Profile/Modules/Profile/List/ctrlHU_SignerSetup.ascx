<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_SignerSetup.ascx.vb"
    Inherits="Profile.ctrlHU_SignerSetup" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidSinger" runat="server" />
<asp:HiddenField ID="hidJoinDate" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="200px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Ngày hiệu lực"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdEffectDate"></tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="Bạn phải chọn ngày hiệu lực" ToolTip="Bạn phải chọn ngày hiệu lực">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Người ký")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên %>" ToolTip="<%$ Translate: Bạn phải chọn nhân viên %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Vị trí công việc")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTitle" SkinID="Readonly" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Phòng ban")%><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" SkinID="Readonly" ReadOnly="True">
                        <%--<ClientEvents OnKeyPress="OnKeyPress" />--%>
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrgName" runat="server"
                        ErrorMessage="Bạn phải chọn Phòng ban" ToolTip="Bạn phải chọn Phòng ban">
                    </asp:RequiredFieldValidator>
                    <tlk:RadButton ID="btnFindOrg" runat="server" SkinID="ButtonView" CausesValidation="false" />

                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label2" Text="Tên chức năng"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboFunction" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboFunction" runat="server"
                        ErrorMessage="Bạn phải chọn chức năng" ToolTip="Bạn phải chọn chức năng">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label3" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtRemark" Width="100%"></tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label10" Text="Căn cứ 1"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtBase1" Width="100%"></tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label11" Text="Căn cứ 2"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtBase2" Width="100%"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label4" Text="Loại thiết lập"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboSetupType" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Localization-CheckAllString="All">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label5" Text="Căn cứ giấy ủy quyền"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtAuthorBase" Width="100%"></tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label12" Text="Căn cứ 3"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtBase3" Width="100%"></tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label13" Text="Căn cứ 4"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtBase4" Width="100%"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Ngày hiệu lực giấy ủy quyền"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdAuthorEffectDate">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label7" Text="Đơn vị đại diện"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtAuthorDeputy" Width="100%"></tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label14" Text="Căn cứ 5"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtBase5" Width="100%"></tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label15" Text="Căn cứ 6"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtBase6" Width="100%"></tlk:RadTextBox>
                </td>
            </tr>
             <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label8" Text="Cấp ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdNgayCap">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label9" Text="Giấy chứng nhận kinh doanh số"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtKdNo" Width="100%"></tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID"
                ClientDataKeyNames="ID,EFFECT_DATE,CER_BUS_RESG_EFFECT_DATE,CER_BUS_RESG,FUNC_ID,FUNC_NAME,SIGNER_ID,SIGNER_NAME,SETUP_TYPE_ID,SETUP_TYPE_NAME,ORG_DESC,
                REMARK,SIGNER_TITLE_ID,SIGNER_TITLE_NAME,ACTFLG,BASE_AUTHOR,AUTHOR_EFFECT_DATE,DEPUTY_AUTHOR,ORG_ID,ORG_NAME,JOIN_DATE,
                BASE1, BASE2, BASE3, BASE4, BASE5, BASE6">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                        HeaderStyle-Width="130px" UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                    <tlk:GridBoundColumn HeaderText="Tên chức năng" DataField="FUNC_NAME"
                        HeaderStyle-Width="150px" UniqueName="FUNC_NAME" SortExpression="FUNC_NAME" />

                    <tlk:GridBoundColumn HeaderText="Loại thiết lập" DataField="SETUP_TYPE_NAME"
                        HeaderStyle-Width="150px" UniqueName="SETUP_TYPE_NAME" SortExpression="SETUP_TYPE_NAME" />
                    
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME"
                        HeaderStyle-Width="150px" UniqueName="ORG_NAME" SortExpression="ORG_NAME" />

                    <tlk:GridBoundColumn HeaderText="Người ký" DataField="SIGNER_NAME"
                        HeaderStyle-Width="150px" UniqueName="SIGNER_NAME" SortExpression="SIGNER_NAME" />

                    <tlk:GridBoundColumn HeaderText="Vị trí công việc người ký" DataField="SIGNER_TITLE_NAME"
                        HeaderStyle-Width="150px" UniqueName="SIGNER_TITLE_NAME" SortExpression="SIGNER_TITLE_NAME" />

                    <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK"
                        HeaderStyle-Width="200px" UniqueName="REMARK" SortExpression="REMARK" />

                    <tlk:GridBoundColumn HeaderText="Căn cứ giấy ủy quyền" DataField="BASE_AUTHOR"
                        HeaderStyle-Width="150px" UniqueName="BASE_AUTHOR" SortExpression="BASE_AUTHOR" />

                    <tlk:GridBoundColumn HeaderText="Ngày hiệu lực giấy ủy quyền" DataField="AUTHOR_EFFECT_DATE"
                        HeaderStyle-Width="130px" UniqueName="AUTHOR_EFFECT_DATE" SortExpression="AUTHOR_EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />

                    <tlk:GridBoundColumn HeaderText="Đơn vị đại diện" DataField="DEPUTY_AUTHOR"
                        HeaderStyle-Width="150px" UniqueName="DEPUTY_AUTHOR" SortExpression="DEPUTY_AUTHOR" />
                    
                    <tlk:GridBoundColumn HeaderText="Ngày cấp" DataField="CER_BUS_RESG_EFFECT_DATE"
                        HeaderStyle-Width="130px" UniqueName="CER_BUS_RESG_EFFECT_DATE" SortExpression="CER_BUS_RESG_EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                    <tlk:GridBoundColumn HeaderText="Giấy chứng nhận kinh doanh số" DataField="CER_BUS_RESG"
                        HeaderStyle-Width="150px" UniqueName="CER_BUS_RESG" SortExpression="CER_BUS_RESG" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_SignerSetup_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_SignerSetup_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_SignerSetup_RadPane2';
        var validateID = 'MainContent_ctrlHU_SignerSetup_valSum';
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
                    var l = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var k = noty({ text: l, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(k.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else if (args.get_item().get_commandName() == "EXPORT_TEMPLATE") {
                enableAjax = false;
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
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_SignerSetup_txtOrgName') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
