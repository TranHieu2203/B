<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCandidateResultNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlCandidateResultNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgId" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<asp:HiddenField ID="hidYear" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMassTransferSalary" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã ứng viên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCandidateCode" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCandidateName" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày sinh")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdBirthDate" Enabled="false" DateInput-Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Môn thi 1")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam1Name">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày thi")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdExam1Date">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Kết quả")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam1Result">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:Label ID="lbExam1Reach" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Môn thi 2")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam2Name">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày thi")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdExam2Date">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Kết quả")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam2Result">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:Label ID="lbExam2Reach" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Môn thi 3")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam3Name">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày thi")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdExam3Date">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Kết quả")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam3Result">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:Label ID="lbExam3Reach" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <%# Translate("Kết quả vòng 1")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboRound1Status">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Môn thi 4")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam4Name">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày thi")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdExam4Date">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Kết quả")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam4Result">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:Label ID="lbExam4Reach" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Môn thi 5")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam5Name">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày thi")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdExam5Date">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Kết quả")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtExam5Result">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:Label ID="lbExam5Reach" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <%# Translate("Kết quả vòng 2")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboRound2Status">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhận xét")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtExamComment" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi Chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtExamRemark" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
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


        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

        
    </script>
</tlk:RadCodeBlock>
