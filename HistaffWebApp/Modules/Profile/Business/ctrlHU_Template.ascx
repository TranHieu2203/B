<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Template.ascx.vb"
    Inherits="Profile.ctrlHU_Template" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Loại biểu mẫu động")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTemplateType" runat="server" AutoPostBack="true" CausesValidation="false"
                        Width="305px">
                    </tlk:RadComboBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Width="500px">
                <tlk:RadGrid PageSize=50 ID="rgTemplate" SkinID="GridSingleSelect" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,NAME,CODE" CommandItemDisplay="Top">
                        <CommandItemStyle Height="28px" />
                        <CommandItemTemplate>
                            <div style="padding: 2px 0 0 0">
                                <div style="float: left">
                                    <tlk:RadButton ID="btnImportFile" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/import1.png"
                                        CausesValidation="false" Text="<%$ Translate: Nhập biểu mẫu %>" CommandName="ImportFile">
                                    </tlk:RadButton>
                                </div>
                            </div>
                        </CommandItemTemplate>
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã biểu mẫu %>" DataField="CODE" SortExpression="CODE"
                                UniqueName="CODE">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên biểu mẫu %>" DataField="NAME"
                                SortExpression="NAME" UniqueName="NAME" />
                            <tlk:GridTemplateColumn UniqueName="LINK_DOWN" HeaderText="" ShowFilterIcon="false"
                                AllowFiltering="false">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="70px" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="linkDownload" runat="server" Text="Tải tệp" Style="text-decoration: underline !important"
                                        NavigateUrl='<%# "/GetFile.aspx?id=TEMPLATE&folderName=" & Eval("FOLDER_NAME").ToString() & "&fid=" & Eval("CODE").ToString()%>'>
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%" AllowMultiRowEdit="true" AllowMultiRowSelection="true"
                    SkinID="GridNotPaging">
                    <MasterTableView DataKeyNames="ID,TEMPLATE_TYPE_ID,CODE" CommandItemDisplay="Top"
                        EditMode="InPlace">
                        <CommandItemStyle Height="28px" />
                        <CommandItemTemplate>
                            <div style="padding: 2px 0 0 0">
                                <div style="float: left">
                                    <tlk:RadButton ID="btnEditMerge" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/edit.png"
                                        CausesValidation="false" Width="70px" Text="<%$ Translate: Sửa %>" CommandName="EditMerge" Enabled="false">
                                    </tlk:RadButton>
                                    <tlk:RadButton ID="btnSaveMerge" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/disk_blue.png"
                                        CausesValidation="false" Width="70px" Text="<%$ Translate: Lưu %>" CommandName="SaveMerge"
                                        Enabled="false">
                                    </tlk:RadButton>
                                    <tlk:RadButton ID="btnCancelMerge" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/disk_blue_error.png"
                                        CausesValidation="false" Width="70px" Text="<%$ Translate: Hủy %>" CommandName="CancelMerge"
                                        Enabled="false">
                                    </tlk:RadButton>
                                </div>
                            </div>
                        </CommandItemTemplate>
                        <Columns>
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Merge field %>" DataField="CODE"
                                SortExpression="CODE" UniqueName="CODE" ReadOnly="true">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCode" runat="server" ReadOnly="true" Style="border: none; background: inherit;
                                        font: 12px Arial, 'Segoe UI' , Verdana, Tahoma, 'Segoe UI' , Times; font-weight: bold" Text='<%# Eval("CODE").ToString() %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </tlk:GridTemplateColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NAME" SortExpression="NAME"
                                UniqueName="NAME" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" MaxFileInput="1" AllowedExtensions="doc,docx"
    isMultiple="Disabled" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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
            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_RadPaneMain');
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            var m;
            var n;

            if (args.get_item().get_commandName() == "IMPORT") {

            }
        }

        function getUrlVars() {
            var vars = {};
            var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
                vars[key] = value;
            });
            return vars;
        }

    </script>
</tlk:RadCodeBlock>
