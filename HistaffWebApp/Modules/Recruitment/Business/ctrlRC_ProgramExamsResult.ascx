<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramExamsResult.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramExamsResult" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hdProgramID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="600px" Scrolling="None">
        <fieldset style="height: 90%; padding-right: 5px">
            <legend></legend>
            <%-- <%# Translate("Danh sách ứng viên")%>--%>
            <%# Translate("Danh sách ứng viên")%>
            <hr />
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
                        <tlk:GridBoundColumn DataField="Email" Visible="false" />
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
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <HeaderStyle HorizontalAlign="Center" Width="150px" />
            </tlk:RadGrid>
        </fieldset>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" MinWidth="200" Width="730px" 
        Scrolling="None">
        <fieldset  style="height: 90%">
            <legend></legend>
            <%--    <%# Translate("Thông tin kết quả thi tuyển")%>--%>
            <table class="table-form" width="100%">
                <tr>
                    <td class="item-head" colspan="6">
                        <%# Translate("Thông tin kết quả thi tuyển")%>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>
                        <%# Translate("Môn thi")%>:
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox ID="lblExamName" runat="server" ReadOnly="true" Width="250px" Font-Bold="true"
                            BorderWidth="0">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%# Translate("Thang điểm")%>:
                    </td>
                    <td>
                        <tlk:RadTextBox ID="lblPointLadder" runat="server" ReadOnly="true" Width="250px"
                            Font-Bold="true" BorderWidth="0">
                        </tlk:RadTextBox>
                    </td>
                    <td>
                        <%# Translate("Điểm đạt")%>:
                    </td>
                    <td>
                        <tlk:RadTextBox ID="lblPointPass" runat="server" ReadOnly="true" Width="250px" Font-Bold="true"
                            BorderWidth="0">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%# Translate("Điểm thi")%><span class="lbReq">*</span>:
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="txtMarks" runat="server" NumberFormat-DecimalDigits="1"
                            NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="0"
                            MaxValue="1000">
                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                            <ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />
                        </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMarks"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập điểm thi. %>" Text="<%$ Translate: Bạn phải nhập điểm thi. %>"
                            ToolTip="<%$ Translate: Bạn phải nhập điểm thi. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <%# Translate("Người coi thi")%>:
                    </td>
                    <td>
                        <asp:Label ID="lblProctor" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: text-top">
                        <%# Translate("Nhận xét")%>:
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtComment" runat="server" SkinID="Textbox1023" Width="250px">
                        </tlk:RadTextBox>
                    </td>
                    <td style="vertical-align: text-top">
                        <%# Translate("Đánh giá")%>:
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtAssessment" runat="server" SkinID="Textbox1023" Width="250px">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
            <div>
                <asp:Label ID="lblAVG" runat="server" Font-Size="12px"></asp:Label>
            </div>
            <tlk:RadToolBar ID="tbarMain" runat="server" />
            <tlk:RadGrid ID="rgData" runat="server" Height="230px" AllowMultiRowEdit="false"
                AllowSorting="false">
                <MasterTableView DataKeyNames="ID" SkinID="GridSingleSelect" ClientDataKeyNames="ID,EXAM_NAME,SCHEDULE_DATE,POINT_LADDER, POINT_PASS, POINT_RESULT,COMMENT_INFO,ASSESSMENT_INFO">
                    <Columns>
                        <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        </tlk:GridClientSelectColumn>
                        <tlk:GridBoundColumn DataField="ID" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Môn thi %>" DataField="EXAM_NAME"
                            UniqueName="EXAM_NAME" SortExpression="EXAM_NAME" ReadOnly="true" HeaderStyle-Width="200px" />
                        <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thi %>" DataField="SCHEDULE_DATE"
                            UniqueName="SCHEDULE_DATE" SortExpression="SCHEDULE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                            HeaderStyle-Width="70px" ReadOnly="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </tlk:GridDateTimeColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm thi %>" DataField="POINT_RESULT"
                            ItemStyle-HorizontalAlign="Right" UniqueName="POINT_RESULT" SortExpression="POINT_RESULT"
                            ReadOnly="true" HeaderStyle-Width="40px" />
                        <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đạt %>" DataField="ISPASS" HeaderStyle-Width="30px"
                            UniqueName="ISPASS" SortExpression="ISPASS" ShowFilterIcon="true" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhận xét %>" DataField="COMMENT_INFO"
                            SortExpression="COMMENT_INFO" UniqueName="COMMENT_INFO" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Đánh giá %>" DataField="ASSESSMENT_INFO"
                            SortExpression="ASSESSMENT_INFO" UniqueName="ASSESSMENT_INFO" />
                    </Columns>
                </MasterTableView>
            </tlk:RadGrid>
        </fieldset>
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

        function clientSendMailButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == "SENDMAIL") {
                bCheck = $find('<%# gridCadidate.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
        }

        function displayDecimalFormat(sender, args) {
            sender.set_textBoxValue(sender.get_value().toString());
        }

    </script>
</tlk:RadCodeBlock>
