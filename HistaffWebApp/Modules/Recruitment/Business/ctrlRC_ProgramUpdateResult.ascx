<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramUpdateResult.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramUpdateResult" %>
<%@ Import Namespace="Common" %>
<%@ Register Src="ctrlRC_ProgramExamsResult.ascx" TagName="ctrlRC_ProgramExamsResult"
    TagPrefix="Recruitment" %><%@ Register Src="ctrlRC_ProgramInterviewResult.ascx" TagName="ctrlRC_ProgramInterviewResult"
    TagPrefix="Recruitment" %>
<asp:HiddenField ID="hidProgramID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Width="100px" Height="100px">
         <table class="table-form">
            <tr> 
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chương trình tuyển dụng")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%>:
                </td>
                <td>
                    <asp:Label ID="lblOrgName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Ngày gửi yêu cầu")%>:
                </td>
                <td>
                    <asp:Label ID="lblSendDate" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
               
                <td class="lb">
                    <%# Translate("Mã tuyển dụng")%>:
                </td>
                <td>
                    <asp:Label ID="lblCode" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                 <td class="lb" style="display : none">
                    <%# Translate("Tên công việc")%>:
                </td>
                <td style="display : none">
                    <asp:Label ID="lblJobName" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                </td>
               
            </tr>
            <tr>
                
                <td class="lb">
                    <%# Translate("Vị trí tuyển dụng")%>:
                </td>
                <td>
                    <asp:Label ID="txtTitleName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Hồ sơ đã nhận")%>:
                </td>
                <td>
                    <asp:Label ID="lblRecordreceived" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng cần tuyển")%>:
                </td>
                <td>
                    <asp:Label ID="lblRequestNumber" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng đã tuyển")%>:
                </td>
                <td>
                    <asp:Label ID="lblNumberHaveRecruit" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <%--TODO: DAIDM comment, an button xuat file vi dang loi--%>
                    <tlk:RadButton ID="cmdExportExcel"  runat="server" CausesValidation="false" Text="<%$ Translate: Xuất file excel%>" ToolTip="Xuất danh sách đề nghị ký HĐLĐ thử việc" OnClientClicked="OpenEditTransfer" Visible="false">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="DetailPane" runat="server" Height="480px">
        <tlk:RadTabStrip ID="rtabProfileInfo" runat="server" CausesValidation="false" MultiPageID="RadMultiPage1"
            AutoPostBack="false">
            <Tabs>
                <tlk:RadTab runat="server" ID="tabExamsResult" PageViewID="tabExamsResult" Text="<%$ Translate: Kết quả thi tuyển %>"
                    Selected="True">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="tabInterviewResult" PageViewID="tabInterviewResult" Text="<%$ Translate: Kết quả phỏng vấn %>">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
        <tlk:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server"  Width="100%"
            ScrollBars="Auto" Height="100%">
            <tlk:RadPageView ID="RadPageView1" runat="server" Width="100%">
                <Recruitment:ctrlRC_ProgramExamsResult runat="server" ID="ctrlRC_ProgramExamsResult" />
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvEmpTitleConcurrent" runat="server" Width="100%">
                <Recruitment:ctrlRC_ProgramInterviewResult runat="server" ID="ctrlRC_ProgramInterviewResult" />
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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

        function OpenEditTransfer(sender, args) {
            enableAjax = false;
        }

    </script>
</tlk:RadCodeBlock>
