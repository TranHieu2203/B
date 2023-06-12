<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHu_Contract_Procedure.ascx.vb"
    Inherits="Profile.ctrlHu_Contract_Procedure" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidTITLE_GROUP_ID" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOrgName" runat="server" Text="Đơn vị"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" SkinID="Readonly" ReadOnly="true" />
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn đơn vị. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbExpireDateOld" runat="server" Text="Ngày hiệu lực"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td class="borderRight">
                    <tlk:RadDatePicker ID="rdeffectdate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label7" Text="Hợp đồng 1"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboHD1" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label9" Text="Hợp đồng 2"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboHD2" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label12" Text="Hợp đồng 3"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboHD3" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label10" Text="Hợp đồng 4"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboHD4" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label11" Text="Hợp đồng 5"></asp:Label>

                </td>
                <td>
                    <tlk:RadComboBox ID="cboHD5" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label13" Text="Hợp đồng 6"></asp:Label>

                </td>
                <td>
                    <tlk:RadComboBox ID="cboHD6" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label8" Text="Mô tả"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtnote" runat="server" Width="100%" SkinID="Textbox1023">
                    </tlk:RadTextBox>
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
            <MasterTableView DataKeyNames="ID,ORG_ID,ORG_NAME,HD_1,HD_1_NAME,HD_2,HD_2_NAME,HD_3,HD_3_NAME,HD_4,HD_4_NAME,HD_5,HD_5_NAME,HD_6,HD_6_NAME,EFFECT_DATE,NOTE" ClientDataKeyNames="ID,ORG_ID,ORG_NAME,HD_1,HD_1_NAME,HD_2,HD_2_NAME,HD_3,HD_3_NAME,HD_4,HD_4_NAME,HD_5,HD_5_NAME,HD_6,HD_6_NAME,EFFECT_DATE,NOTE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Bộ phận" DataField="ORG_NAME" HeaderStyle-Width="100px"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Hợp đồng thứ 1" DataField="HD_1_NAME" HeaderStyle-Width="150px"
                        UniqueName="HD_1_NAME" SortExpression="HD_1_NAME" />
                    <tlk:GridBoundColumn HeaderText="Hợp đồng thứ 2" DataField="HD_2_NAME" HeaderStyle-Width="150px"
                        UniqueName="HD_2_NAME" SortExpression="HD_2_NAME" />
                    <tlk:GridBoundColumn HeaderText="Hợp đồng thứ 3" DataField="HD_3_NAME" HeaderStyle-Width="150px"
                        UniqueName="HD_3_NAME" SortExpression="HD_3_NAME" />
                    <tlk:GridBoundColumn HeaderText="Hợp đồng thứ 4" DataField="HD_4_NAME" HeaderStyle-Width="150px"
                        UniqueName="HD_4_NAME" SortExpression="HD_4_NAME" />
                    <tlk:GridBoundColumn HeaderText="Hợp đồng thứ 5" DataField="HD_5_NAME" HeaderStyle-Width="150px"
                        UniqueName="HD_5_NAME" SortExpression="HD_5_NAME" />
                    <tlk:GridBoundColumn HeaderText="Hợp đồng thứ 6" DataField="HD_6_NAME" HeaderStyle-Width="150px"
                        UniqueName="HD_6_NAME" SortExpression="HD_6_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" HeaderStyle-Width="150px"
                        ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                        DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />

                    <tlk:GridBoundColumn HeaderText="Mô tả" DataField="NOTE"
                        UniqueName="NOTE" SortExpression="NOTE" />
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

        var splitterID = 'ctl00_MainContent_ctrlHu_Contract_Procedure_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHu_Contract_Procedure_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHu_Contract_Procedure_RadPane2';
        var validateID = 'MainContent_ctrlHu_Contract_Procedure_valSum';
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

    </script>
</tlk:RadCodeBlock>
