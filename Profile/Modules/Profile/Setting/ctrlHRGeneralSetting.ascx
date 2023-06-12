<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHRGeneralSetting.ascx.vb"
    Inherits="Profile.ctrlHRGeneralSetting" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:Panel ID="radProbation" runat="server" CssClass="Pane">
            <fieldset  style="display:none">
                <legend>
                    <%# Translate("Thiết lập hồ sơ")%><span class="lbReq">*</span>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp" runat="server" Text="<%$ Translate: Lương cơ bản = Hệ số bậc lương * Mức lương vùng bảo hiểm%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_1" runat="server" Text="<%$ Translate: Ngày bắt đầu = Ngày kết thúc HĐ kề trước + 1%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_2" runat="server" Text="<%$ Translate: Load data loại hợp đồng theo thiết lập quy trình hợp đồng%>"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset  style="display:none">
                <legend>

                    <%# Translate("Thiết lập tuyển dụng")%><span class="lbReq">*</span>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="ckh_RC_Over_Rank" runat="server" Text="<%$ Translate: Cảnh báo vượt rank lương theo vị trí định biên%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="ckh_RC_Sal_Budget_Exceeded" runat="server" Text="<%$ Translate: Cảnh báo vượt ngân sách lương theo bộ phận%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="ckh_PersonRe_TCTD" runat="server" Text="<%$ Translate: TBP phân công YCTD cho người phụ trách%>"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
             <fieldset  style="display:none">
                <legend>

                    <%# Translate("Thiết lập hệ thống")%><span class="lbReq">*</span>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb9">
                            <asp:Label ID="Label1" runat="server" Text="Mã xác thực đăng ký User"></asp:Label>
                            <tlk:RadTextBox runat="server" ID="txtCodeXacThuc"  Width="260px" />
                            <%--<asp:CheckBox ID="CheckBox1" runat="server" Text="<%$ Translate: Cho phép chỉnh sửa thông tin portal (hồ sơ,nhân thân)%>"/>--%>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>

                    <%# Translate("Thiết lập Portal")%><span class="lbReq">*</span>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkPortalAllowChange" runat="server" Text="<%$ Translate: Cho phép chỉnh sửa thông tin portal (hồ sơ,nhân thân)%>"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
             <fieldset>
                <legend>

                    <%# Translate("Thiết lập Chấm công")%>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkAdvanceLeave" runat="server" Text="<%$ Translate: Số phép ứng tối đa     %>"/>
                            <tlk:RadNumericTextBox ID="ntxtAdvanceLeave" runat="server" SkinID ="Decimal" Width="40px"></tlk:RadNumericTextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

    </script>
    <style>
        .Pane {
            width: 33%;
            float: left;
        }
      
    </style>
</tlk:RadCodeBlock>
