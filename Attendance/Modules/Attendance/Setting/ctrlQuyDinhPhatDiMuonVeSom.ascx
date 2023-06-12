<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlQuyDinhPhatDiMuonVeSom.ascx.vb"
    Inherits="Attendance.ctrlQuyDinhPhatDiMuonVeSom" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="200px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã quy định ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rdTxtCode" SkinID="Textbox15" runat="server">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="rdTxtCode" runat="server" ErrorMessage="<%$ Translate: Mã quy định đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã quy định đã tồn tại. %>">
                    </asp:CustomValidator>
                </td>
                <td>
                    <%# Translate("Mức 1 >=")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtMuc1" SkinID="Textbox15" Width="50px" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(Phút)")%>
                </td>
                <td class="lb">
                    <tlk:RadComboBox ID="rcbLoaiPhat1" SkinID="Textbox15" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtGiatri1" SkinID="Textbox15" Width="100px" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="2" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(VND/Ngày công)")%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên quy định")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rdTxtName" MaxLength="1000" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <%# Translate("Mức 2 >=")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtMuc2" SkinID="Textbox15" Width="50px" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(Phút)")%>
                </td>
                <td class="lb">
                    <tlk:RadComboBox ID="rcbLoaiPhat2" SkinID="Textbox15" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtGiatri2" SkinID="Textbox15" Width="100px" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="2" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(VND/Ngày công)")%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Mức 3 >=")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtMuc3" SkinID="Textbox15" Width="50px" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(Phút)")%>
                </td>
                <td class="lb">
                    <tlk:RadComboBox ID="rcbLoaiPhat3" SkinID="Textbox15" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtGiatri3" SkinID="Textbox15" Width="100px" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="2" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(VND/Ngày công)")%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Mức 4 >=")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtMuc4" SkinID="Textbox15" Width="50px" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(Phút)")%>
                </td>
                <td class="lb">
                    <tlk:RadComboBox ID="rcbLoaiPhat4" SkinID="Textbox15" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtGiatri4" SkinID="Textbox15" Width="100px" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="2" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(VND/Ngày công)")%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                </td>
                <td>
                    <%# Translate("Mức 5 từ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtMuc5" SkinID="Textbox15" Width="50px" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(Phút)")%>
                </td>
                <td class="lb">
                    <tlk:RadComboBox ID="rcbLoaiPhat5" SkinID="Textbox15" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtGiatri5" SkinID="Textbox15" Width="100px" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="2" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(VND/Ngày công)")%>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,MUC1,LOAIPHAT1,GIATRI1,MUC2,LOAIPHAT2,GIATRI2,MUC3,LOAIPHAT3,GIATRI3,MUC4,LOAIPHAT4,GIATRI4,MUC5,LOAIPHAT5,GIATRI5,ACTFLG">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã quy định %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên quy định %>" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức 1 >= (phút) %>" DataField="MUC1"
                        UniqueName="MUC1" SortExpression="MUC1" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: loai phạt 1 %>" DataField="LOAIPHAT1_NAME"
                        UniqueName="LOAIPHAT1_NAME" SortExpression="LOAIPHAT1_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: giá trị 1 (VND/Ngày)%>" DataField="GIATRI1"
                        DataFormatString="{0:n0}" UniqueName="GIATRI1" SortExpression="GIATRI1" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức 2 >= (phút) %>" DataField="MUC2"
                        UniqueName="MUC2" SortExpression="MUC2" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: loai phạt 2 %>" DataField="LOAIPHAT2_NAME"
                        UniqueName="LOAIPHAT2_NAME" SortExpression="LOAIPHAT2_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: giá trị 2 (VND/Ngày)%>" DataField="GIATRI2"
                        DataFormatString="{0:n0}" UniqueName="GIATRI2" SortExpression="GIATRI2" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức 3 >= (phút) %>" DataField="MUC3"
                        UniqueName="MUC3" SortExpression="MUC3" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: loai phạt 3 %>" DataField="LOAIPHAT3_NAME"
                        UniqueName="LOAIPHAT3_NAME" SortExpression="LOAIPHAT3_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: giá trị 3 (VND/Ngày)%>" DataField="GIATRI3"
                        DataFormatString="{0:n0}" UniqueName="GIATRI3" SortExpression="GIATRI3" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức 4 >= (phút) %>" DataField="MUC4"
                        UniqueName="MUC4" SortExpression="MUC4" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: loai phạt 4 %>" DataField="LOAIPHAT4_NAME"
                        UniqueName="LOAIPHAT4_NAME" SortExpression="LOAIPHAT4_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: giá trị 4 (VND/Ngày)%>" DataField="GIATRI4"
                        DataFormatString="{0:n0}" UniqueName="GIATRI4" SortExpression="GIATRI4" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức 5 >= (phút) %>" DataField="MUC5"
                        UniqueName="MUC5" SortExpression="MUC5" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: loai phạt 5 %>" DataField="LOAIPHAT5_NAME"
                        UniqueName="LOAIPHAT5_NAME" SortExpression="LOAIPHAT5_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: giá trị 5 (VND/Ngày)%>" DataField="GIATRI5"
                        DataFormatString="{0:n0}" UniqueName="GIATRI5" SortExpression="GIATRI5" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                        <HeaderStyle Width="70px" />
                    </tlk:GridBoundColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
