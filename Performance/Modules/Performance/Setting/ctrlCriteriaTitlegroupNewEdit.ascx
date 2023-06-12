<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCriteriaTitlegroupNewEdit.ascx.vb"
    Inherits="Performance.ctrlCriteriaTitlegroupNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="4">
                    <%# Translate("Thông tin tiêu chí đánh giá")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhãn hàng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboBrandID" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm chức danh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitleGroup" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboTitleGroup" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn chưa nhập nhóm chức danh. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdEffectDate" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn chưa nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="4">
                    <%# Translate("Thông tin tiêu chí đánh giá")%>
                    <hr />
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <%# Translate("Tiêu chí")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbCriteria" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>        
                </td>
                 <td class="lb">
                    <asp:CheckBox ID="chkCriteria" runat="server" Enabled="false" />
                </td>
                <td>
                     <%# Translate("Tiêu chí tổng hợp")%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tỷ trọng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rdRatio" runat="server" SkinID="Decimal"></tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgCriteria" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        PageSize="1000" AllowSorting="false" AllowMultiRowSelection="false" CellSpacing="0"
                        GridLines="Vertical" Height="100%">
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle Visible="false" />
                        <MasterTableView DataKeyNames="ID,CRITERIA_ID,RATIO"
                            ClientDataKeyNames="ID,CRITERIA_ID,RATIO"
                            CommandItemDisplay="Top">
                            <CommandItemStyle Height="25px" />
                            <CommandItemTemplate>
                                <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnInsertCriteria" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Thêm %>" CommandName="InsertCriteria" ValidationGroup="grpAddAllow">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: right">
                                        <tlk:RadButton ID="btnDeleteCriteria" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Xóa %>" CommandName="DeleteCriteria">
                                        </tlk:RadButton>
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridBoundColumn DataField="ID" Visible="false" />
                                <tlk:GridBoundColumn DataField="CRITERIA_ID" Visible="false" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiêu chí %>" DataField="CRITERIA_NAME"
                                    SortExpression="CRITERIA_NAME" UniqueName="CRITERIA_NAME" />
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Tiêu chí tổng hợp %>" DataField="IS_CHECK"
                                    SortExpression="IS_CHECK" UniqueName="IS_CHECK" AllowFiltering="false" ShowFilterIcon="false">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỷ trọng %>" DataField="RATIO"
                                    SortExpression="RATIO" UniqueName="RATIO" />
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
    </tlk:RadPane>

</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;

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
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
