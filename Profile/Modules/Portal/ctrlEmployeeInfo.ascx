<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlEmployeeInfo.ascx.vb"
    Inherits="Profile.ctrlEmployeeInfo" %>
<div class="account_info">
    <table class="table-form">
        <tr>
            <td style="width: 200px">
                <asp:Label ID="lblEMPLOYEE_CODE" runat="server"></asp:Label>
            </td>
            <td rowspan="3">
                <tlk:RadBinaryImage ID="rbiEmployeeImage" runat="server" AutoAdjustImageControlSize="true"
                    Width="50px" Height="60px" ResizeMode="Fill" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px" rowspan="2" colspan="2">
                <asp:Label ID="lblFULLNAME" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</div>
