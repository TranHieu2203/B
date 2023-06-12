<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsManagerSunCareNewEdit.ascx.vb"
    Inherits="Insurance.ctrlInsManagerSunCareNewEdit" %>
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
                        <%# Translate("Thông tin chế độ bảo hiểm")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEMPLOYEE_CODE" Width="130px" SkinID="ReadOnly" runat="server"
                        Enabled="false">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqrcboCHANGE_TYPE" ControlToValidate="txtEMPLOYEE_CODE"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chưa chọn nhân viên. %>"></asp:RequiredFieldValidator>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Họ và tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEMPLOYEE_NAME" SkinID="ReadOnly" runat="server" Enabled="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Ngày sinh")%>
                    
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNgaySinh" SkinID="ReadOnly" runat="server" Enabled="false">
                    </tlk:RadTextBox>
                    
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Số CMND")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCMND" SkinID="ReadOnly" runat="server" Enabled="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtORG_NAME" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE_NAME" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Năm")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNam" Width="130px"  runat="server" SkinID="Number" MaxLength="4" MinValue="1888" MaxValue="2999">
                    </tlk:RadTextBox>
                    
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Số HĐ bảo hiểm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboHDbaohiem" AutoPostBack="true" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Đơn vị bảo hiểm")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDVBaoHiem" SkinID="ReadOnly" runat="server" Enabled="false">
                    </tlk:RadTextBox>
                    
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Giá trị HĐ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtGiaTri_HD" SkinID="ReadOnly" runat="server"  Enabled="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("HĐ từ ngày")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdHDTuNgay" SkinID="ReadOnly" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                    
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("HĐ đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdHDDenNgay" SkinID="ReadOnly" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Chương trình bảo hiểm")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCTBH"  AutoPostBack="true" runat="server" CausesValidation="false" >
                    </tlk:RadComboBox>
                    
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Số tiền")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtSoTien" SkinID="ReadOnly" runat="server" Enabled="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 120px">
                    <%# Translate("BH người thân")%>
                </td>
                <td>
                    <asp:CheckBox ID="chkCheckBHNguoiThan" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Họ tên thân nhân")%>

                </td>
                <td>
                    <tlk:RadComboBox ID="rcfamily_name"  AutoPostBack="true" runat="server" CausesValidation="false" >
                    </tlk:RadComboBox>
                    
                    
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Mối quan hệ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="RadTextBox4" SkinID="ReadOnly" runat="server" Enabled="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Ngày sinh")%>

                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgaySinhTN" SkinID="ReadOnly" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                    
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Số CMND")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCMNDTN" SkinID="ReadOnly" runat="server" Enabled="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đối tượng chi trả")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDTCT" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày tham gia")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgaythamGia" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                </td>
            </tr>
           
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpSTART_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Số tiền bảo hiểm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="RadNumericTextBox1" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                
            </tr>
            
            <tr>
                <td class="lb">
                    <%# Translate("Ngày báo giảm")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpEND_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực %>"
                        ErrorMessage="<%$ Translate: Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực %>"
                        Type="Date" Operator="GreaterThan" ControlToCompare="dpSTART_DATE" ControlToValidate="dpEND_DATE"></asp:CompareValidator>
                </td>

                <td class="lb">
                    <%# Translate("Số tiền Hoàn lại")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmCOST" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày nhận tiền")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="RadDatePicker1" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Người nhận tiền")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="RadNumericTextBox2" runat="server">
                    </tlk:RadTextBox>
                </td>
                
            </tr>
            <tr style="display:none">
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
                        <%# Translate("Thông tin Claim bảo hiểm")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Ngày khám bệnh")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgayKhamBenh" runat="server" TabIndex="2">
                            </tlk:RadDatePicker>
                </td>
                
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Tên bệnh")%>
                    
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="RadDatePicker2" runat="server" TabIndex="2" Width="100%">
                            </tlk:RadTextBox>
                </td>
                
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Số tiền YC bồi thường")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="RadTextBox5" runat="server" TabIndex="2">
                            </tlk:RadTextBox>
                </td>
                
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Số tiền được bồi thường")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="RadTextBox6" runat="server" TabIndex="2">
                            </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 150px">
                    <%# Translate("Ngày buồi thường")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="RadDatePicker3" runat="server" TabIndex="2">
                            </tlk:RadDatePicker>
                </td>
                
            </tr>
            <tr >
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="RadTextBox7" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
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
                                <tlk:GridBoundColumn DataField="SUN_CARE_ID" Visible="false" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày khám bệnh %>" DataField="EXAMINE_DATE"
                                    SortExpression="EXAMINE_DATE" UniqueName="EXAMINE_DATE" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên bệnh %>" DataField="DISEASE_NAME"
                                    SortExpression="DISEASE_NAME" UniqueName="DISEASE_NAME" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền YC bồi thường %>" DataField="AMOUNT_OF_CLAIMS"
                                    SortExpression="AMOUNT_OF_CLAIMS" UniqueName="AMOUNT_OF_CLAIMS" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền được bồi thường %>" DataField="AMOUNT_OF_COMPENSATION"
                                    SortExpression="AMOUNT_OF_COMPENSATION" UniqueName="AMOUNT_OF_COMPENSATION" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                               <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bồi thường %>" DataField="COMPENSATION_DATE"
                                    SortExpression="COMPENSATION_DATE" UniqueName="COMPENSATION_DATE" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE"
                                    SortExpression="NOTE" UniqueName="NOTE" />
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
