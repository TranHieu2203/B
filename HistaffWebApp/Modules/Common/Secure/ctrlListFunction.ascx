<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlListFunction.ascx.vb"
    Inherits="Common.ctrlListFunction" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="120px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMainToolBar" runat="server">
        </tlk:RadToolBar>
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table>
            <td valign="top">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã chức năng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtFunctionID" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reqFunctionID" runat="server" ErrorMessage="<%$ Translate: Chưa nhập mã chức năng %>"
                                ToolTip="<%$ Translate: Chưa nhập mã chức năng %>" ControlToValidate="txtFunctionID"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidatorFunctionID" runat="server" ToolTip="<%$ Translate: Mã chức năng đã tồn tại %>"
                                ErrorMessage="<%$ Translate: Mã chức năng đã tồn tại %>"></asp:CustomValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên chức năng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtFunctionName" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reqFunctionName" runat="server" ErrorMessage="<%$ Translate: Chưa nhập tên chức năng %>"
                                ToolTip="<%$ Translate: Chưa nhập tên chức năng %>" ControlToValidate="txtFunctionName"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkSigner" Text="Thiết lập người ký" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Nhóm chức năng")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboFunctionGroup">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên phân hệ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboModule">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="ChkClient" Text="Admin khách hàng" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="min-width: 40px"></td>
            <td valign="top">
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin tìm kiếm")%>
                    </legend>
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Tên phân hệ")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboModuleFind">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Nhóm chức năng")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboFunctionGroupFind">
                                </tlk:RadComboBox>
                            </td>
                            <td style="padding-left: 20px;">
                            <tlk:RadButton runat="server" Text="Tìm" ID="btnSearch" SkinID="ButtonFind" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" >
        <tlk:RadGrid PageSize="500" ID="rgListFunctions" runat="server" Height="100%">
            <MasterTableView ClientDataKeyNames="ID,NAME,MODULE_ID,FID,FUNCTION_GROUP_ID,SETUP_SIGNER,ADMIN_HR,ACTFLG" DataKeyNames="ID,ACTFLG">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã chức năng %>" DataField="FID"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="FID"
                        SortExpression="FID" ReadOnly="true" HeaderStyle-Width="200px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chức năng %>" DataField="NAME"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="NAME"
                        SortExpression="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chức năng %>" DataField="FUNCTION_GROUP_NAME"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="FUNCTION_GROUP_NAME"
                        SortExpression="FUNCTION_GROUP_NAME" ReadOnly="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phân hệ %>" DataField="MODULE_NAME"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="MODULE_NAME"
                        SortExpression="MODULE_NAME" ReadOnly="true" />
                    <tlk:GridBoundColumn DataField="ACTFLG" HeaderText="<%$ Translate: Trạng thái %>">
                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="7%" />
                        <HeaderStyle Width="7%" />
                    </tlk:GridBoundColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate:Thiết lập người ký %>" DataField="SETUP_SIGNER"
                        AllowFiltering="false" UniqueName="SETUP_SIGNER"
                        SortExpression="SETUP_SIGNER" ReadOnly="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate:Admin HR %>" DataField="ADMIN_HR"
                        AllowFiltering="false" UniqueName="ADMIN_HR"
                        SortExpression="ADMIN_HR" ReadOnly="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<script type="text/javascript">
    var enableAjax = true;
    var oldSize = 0;
    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
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

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }

    // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
    function ResizeSplitter() {
        setTimeout(function () {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
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
