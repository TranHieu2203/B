<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHu_Org_Pause.ascx.vb"
    Inherits="Profile.ctrlHu_Org_Pause" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidTITLE_GROUP_ID" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOrgName" runat="server" Text="Bộ phận/CH"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />

                </td>
                <td class="lb">
                    <asp:Label ID="Label5" runat="server" Text="Mã BP/CH"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtcode" SkinID="Readonly" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lblFromdate" runat="server" Text="Từ ngày"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td class="borderRight">
                    <tlk:RadDatePicker ID="rdFromdate" runat="server"  Width="100%">
                    </tlk:RadDatePicker>
                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdeffectdate" runat="server"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực" ToolTip="Bạn phải nhập ngày hiệu lực">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label ID="Label9" runat="server" Text="Đến ngày"></asp:Label><span
                        class="lbReq"></span>
                </td>
                <td class="borderRight">
                    <tlk:RadDatePicker ID="rdTodate" runat="server" >
                    </tlk:RadDatePicker>
                </td>
                
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label10" runat="server" Text="Số ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnSoNgay" SkinID="Readonly" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    
                </td>
                <td>
                    <asp:CheckBox  runat="server" ID="chkIs_pause" Text="Ngưng hoạt động"/> 
                </td>
                <td class="lb">
                    
                </td>
                <td>
                    <asp:CheckBox  runat="server" ID="chkIs_OT" Text="Ngưng DK OT"/> 
                </td>
            </tr>

        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="false">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,ORG_ID,ORG_CODE,ORG_NAME,EFFECT_FROM,EFFECT_TO,DAY_NUM,IS_PAUSE,IS_OT">
                <Columns>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHu_Org_Pause_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHu_Org_Pause_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHu_Org_Pause_RadPane2';
        var validateID = 'MainContent_ctrlHu_Org_Pause_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;
        var Page_Validators = new Array();

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
                //if (!Page_ClientValidate(""))
                //    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain');
                //else
                //    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
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
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }



        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlHu_Org_Pause_txtOrgName') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
