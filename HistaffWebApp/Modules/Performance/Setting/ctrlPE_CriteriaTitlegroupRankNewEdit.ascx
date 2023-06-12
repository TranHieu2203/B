<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_CriteriaTitlegroupRankNewEdit.ascx.vb"
    Inherits="Performance.ctrlPE_CriteriaTitlegroupRankNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin tiêu chí đánh giá")%>
                    <hr />
                </td>
            </tr>
            <tr>
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
                    <%# Translate("Nhóm chức danh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitleGroup" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboTitleGroup" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn chưa chọn nhóm chức danh. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tiêu chí đánh giá")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbCriteria" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cbCriteria" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn chưa chọn Tiêu chí đánh giá. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtNote" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin thang điểm đánh giá")%>
                    <hr />
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <%# Translate("Giá trị từ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtRankFrom" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>     
                </td>
                <td class="lb">
                     <%# Translate("Giá trị đến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtRankTo" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                     <%# Translate("Mức điểm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtPoint" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>   
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtDESCRIPTION" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <tlk:RadGrid ID="rgCriteria" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        PageSize="1000" AllowSorting="false" AllowMultiRowSelection="false" CellSpacing="0"
                        GridLines="Vertical" Height="120px">
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle Visible="false" />
                        <MasterTableView DataKeyNames="ID,CRITERIA_TITLEGROUP_ID,RANK_FROM,RANK_TO,POINT,DESCRIPTION"
                            ClientDataKeyNames="ID,CRITERIA_TITLEGROUP_ID,RANK_FROM,RANK_TO,POINT,DESCRIPTION"
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
                                <tlk:GridBoundColumn DataField="CRITERIA_TITLEGROUP_ID" Visible="false" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Giá trị từ %>" DataField="RANK_FROM"
                                    SortExpression="RANK_FROM" UniqueName="RANK_FROM" HeaderStyle-Width="120px" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Giá trị đến %>" DataField="RANK_TO"
                                    SortExpression="RANK_TO" UniqueName="RANK_TO" HeaderStyle-Width="120px" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức điểm %>" DataField="POINT"
                                    SortExpression="POINT" UniqueName="POINT" HeaderStyle-Width="120px" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="DESCRIPTION"
                                    SortExpression="DESCRIPTION" UniqueName="DESCRIPTION"  />
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
        </table>
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
