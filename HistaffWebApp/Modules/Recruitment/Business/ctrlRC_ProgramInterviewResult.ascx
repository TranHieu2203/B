<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramInterviewResult.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramInterviewResult" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hdProgramID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="600px" Scrolling="None">
        <fieldset style="height: 90%; padding-right: 5px">
            <legend></legend>
            <tlk:RadButton ID="cmdSendEmail" runat="server" CausesValidation="false" Text="<%$ Translate: Gửi thư cảm ơn %>">
            </tlk:RadButton>
            <br />
            <br />
            <tlk:RadGrid ID="gridCadidate" runat="server" Height="300px" AllowMultiRowEdit="false"
                OnSelectedIndexChanged="gridCadidate_SelectedIndexChanged" AllowSorting="false">
                <MasterTableView DataKeyNames="ID" SkinID="GridSingleSelect" ClientDataKeyNames="ID">
                    <Columns>
                        <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        </tlk:GridClientSelectColumn>
                        <tlk:GridBoundColumn DataField="ID" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="Code"
                            UniqueName="Code" SortExpression="Code" ReadOnly="true" HeaderStyle-Width="70px" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FullName" UniqueName="FullName"
                            SortExpression="FullName" ReadOnly="true" HeaderStyle-Width="120px" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="Gender"
                            UniqueName="Gender" SortExpression="Gender" ReadOnly="true" HeaderStyle-Width="60px" />
                        <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="DOB"
                            UniqueName="DOB" SortExpression="DOB" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px"
                            ReadOnly="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </tlk:GridDateTimeColumn>
                    </Columns>
                </MasterTableView><ClientSettings>
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <HeaderStyle HorizontalAlign="Center" Width="150px" />
            </tlk:RadGrid></fieldset>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" MinWidth="200" Width="730px" Scrolling="None">
        <fieldset style="height: 90%">
            <legend></legend>
            <table class="table-form" width="100%">
                <tr>
                    <td style="width: 120px">
                        <%# Translate("Môn phỏng vấn")%>:
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox ID="lblExamName_Interview" runat="server" ReadOnly="true" Width="200px"
                            Font-Bold="true" BorderWidth="0">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%# Translate("Người phỏng vấn")%>:
                    </td>
                    <td>
                        <asp:Label ID="lblProctor" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td>
                        <%# Translate("Kết quả phỏng vấn")%>:
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cbbStatus" runat="server">
                            <Items>
                                <tlk:RadComboBoxItem runat="server" Selected="True" Text="Chưa có kết quả" Value="-1" />
                                <tlk:RadComboBoxItem runat="server" Text="Không đạt phỏng vấn" Value="0" />
                                <tlk:RadComboBoxItem runat="server" Text="Đạt phỏng vấn" Value="1" />
                            </Items>
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: text-top" colspan="2">
                        <%# Translate("Nhận xét")%>:
                    </td>
                    <td style="vertical-align: text-top" colspan="2">
                        <%# Translate("Đánh giá")%>:
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <tlk:RadTextBox ID="txtComment" runat="server" SkinID="Textbox1023" Width="300px">
                        </tlk:RadTextBox>
                    </td>
                    <td colspan="2">
                        <tlk:RadTextBox ID="txtAssessment" runat="server" SkinID="Textbox1023" Width="300px">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
            <tlk:RadToolBar ID="tbarMain" runat="server" />
            <tlk:RadGrid ID="rgDataInterview" runat="server" Height="230px" AllowMultiRowEdit="false"
                AllowSorting="false">
                <MasterTableView DataKeyNames="ID" SkinID="GridSingleSelect" ClientDataKeyNames="ID,EXAM_NAME,ISPASS,IS_PASS,COMMENT_INFO,ASSESSMENT_INFO">
                    <Columns>
                        <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        </tlk:GridClientSelectColumn>
                        <tlk:GridBoundColumn DataField="ID" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Môn phỏng vấn %>" DataField="EXAM_NAME"
                            UniqueName="EXAM_NAME" SortExpression="EXAM_NAME" ReadOnly="true" HeaderStyle-Width="200px" />
                        <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thi %>" DataField="SCHEDULE_DATE"
                            UniqueName="SCHEDULE_DATE" SortExpression="SCHEDULE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                            HeaderStyle-Width="70px" ReadOnly="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </tlk:GridDateTimeColumn>
                        <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đạt %>" DataField="ISPASS" HeaderStyle-Width="30px"
                            UniqueName="ISPASS" SortExpression="ISPASS" ShowFilterIcon="true" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhận xét %>" DataField="COMMENT_INFO"
                            SortExpression="COMMENT_INFO" UniqueName="COMMENT_INFO" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Đánh giá %>" DataField="ASSESSMENT_INFO"
                            SortExpression="ASSESSMENT_INFO" UniqueName="ASSESSMENT_INFO" />
                    </Columns>
                </MasterTableView></tlk:RadGrid></fieldset>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


    </script>
</tlk:RadCodeBlock>
