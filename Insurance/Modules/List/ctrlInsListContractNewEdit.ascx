<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsListContractNewEdit.ascx.vb"
    Inherits="Insurance.ctrlInsListContractNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="4">
                        <%# Translate("Thông tin hợp đồng bảo hiểm sức khỏe")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Số HĐ bảo hiểm")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSoHD" Width="130px"  runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Năm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNam" runat="server"  SkinID="Number" MaxLength="4" MinValue="1888" MaxValue="2999" AutoPostBack="true" CausesValidation="false" >
                    </tlk:RadTextBox>
                     <asp:RequiredFieldValidator ID="reqtxtNam" ControlToValidate="txtNam" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn chưa nhập Năm. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị bảo hiểm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="ddlINS_ORG_ID" runat="server" TabIndex="2">
                            </tlk:RadComboBox>
                </td>
                
            </tr>
            <tr>

                <td class="lb">
                    <%# Translate("HĐ từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="txtTuNgay"  runat="server" AutoPostBack="true" CausesValidation="false" >
                    </tlk:RadDatePicker>                    
                    <asp:RequiredFieldValidator ID="reqtxtTuNgay" ControlToValidate="txtTuNgay" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn chưa nhập HĐ từ ngày. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("HĐ đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="txtDenNgay"  runat="server" AutoPostBack="true" CausesValidation="false" >
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqtxtDenNgay" ControlToValidate="txtDenNgay" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn chưa nhập HĐ đến ngày. %>"></asp:RequiredFieldValidator>
                     <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtDenNgay"
                                ControlToCompare="txtTuNgay" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Từ ngày phải lớn hơn đến ngày %>"
                                ToolTip="<%$ Translate: Từ ngày phải lớn hơn đến ngày %>"></asp:CompareValidator>--%>
                </td>
                
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giá trị HĐ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rdGiaTriHD" runat="server" >
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày mua")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="txtBuy_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                </td>
                
            </tr>
            <tr >
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
                        <%# Translate("Chương trình bảo hiểm")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Chương trình")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="ddlINS_PROGRAM" runat="server" TabIndex="2">
                            </tlk:RadComboBox>
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Số tiền")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtValue" SkinID="Money" runat="server" >
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <tlk:RadGrid ID="rgAllow" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        PageSize="1000" AllowSorting="false" AllowMultiRowSelection="false" CellSpacing="0"
                        GridLines="Vertical" Height="120px">
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <PagerStyle Visible="false" />
                        <MasterTableView DataKeyNames="ID,CONTRACT_INS_ID,INS_PROGRAM_ID,MONEY_INS"
                            ClientDataKeyNames="ID,CONTRACT_INS_ID,INS_PROGRAM_ID,MONEY_INS"
                            CommandItemDisplay="Top">
                            <CommandItemStyle Height="25px" />
                            <CommandItemTemplate>
                                <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnInsertAllowance" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Thêm %>" CommandName="InsertAllow" ValidationGroup="grpAddAllow">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: right">
                                        <tlk:RadButton ID="btnDeleteAllowance" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Xóa %>" CommandName="DeleteAllow"
                                           >
                                        </tlk:RadButton>
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridBoundColumn DataField="ID" Visible="false" />
                                <tlk:GridBoundColumn DataField="CONTRACT_INS_ID" Visible="false" />
                                <tlk:GridBoundColumn DataField="INS_PROGRAM_ID"  HeaderStyle-Width="1px"/>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Chương trình bảo hiểm %>" DataField="INS_PROGRAM_NAME"
                                    SortExpression="INS_PROGRAM_NAME" UniqueName="INS_PROGRAM_NAME" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="MONEY_INS"
                                    SortExpression="MONEY_INS" UniqueName="MONEY_INS" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                               
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
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (item.get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
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
