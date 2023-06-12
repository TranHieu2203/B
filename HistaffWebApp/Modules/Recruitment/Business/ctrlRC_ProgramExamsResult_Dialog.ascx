<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramExamsResult_Dialog.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramExamsResult_Dialog" %>
<%@ Import Namespace="Common" %>
<%@ Register Src="ctrlRC_ProgramExamsResult.ascx" TagName="ctrlRC_ProgramExamsResult"
    TagPrefix="Recruitment" %><%@ Register Src="ctrlRC_ProgramInterviewResult.ascx" TagName="ctrlRC_ProgramInterviewResult"
    TagPrefix="Recruitment" %>
<asp:HiddenField ID="hidProgramID" runat="server" />
<asp:HiddenField ID="hidCandidate_ID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane5" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Width="100px" Height="210px">
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

                <td class="lb" style="display: none">
                    <%# Translate("Mã tuyển dụng")%>:
                </td>
                <td style="display: none">
                    <asp:Label ID="lblCode" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Tên công việc")%>:
                </td>
                <td style="display: none">
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
                    <%# Translate("Số lượng cần tuyển")%>:
                </td>
                <td>
                    <asp:Label ID="lblRequestNumber" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>

            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hồ sơ đã nhận")%>:
                </td>
                <td>
                    <asp:Label ID="lblRecordreceived" runat="server" Text="" Font-Bold="true"></asp:Label>
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
                    <tlk:RadButton ID="cmdExportExcel" runat="server" CausesValidation="false" Text="<%$ Translate: Xuất file excel%>" ToolTip="Xuất danh sách đề nghị ký HĐLĐ thử việc" OnClientClicked="OpenEditTransfer" Visible="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin ứng viên")%>
                    </b>
                    <hr />
                </td>

            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%>:
                </td>
                <td>
                    <asp:Label ID="lblIDNV" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>

                <td class="lb">
                    <%# Translate("Họ tên")%>:
                </td>
                <td>
                    <asp:Label ID="lblFullName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giới tính")%>:
                </td>
                <td>
                    <asp:Label ID="lblGENDER" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>

                <td class="lb">
                    <%# Translate("Ngày sinh")%>:
                </td>
                <td>
                    <asp:Label ID="lblBIRTH_DATE" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
   <tlk:RadPane ID="RadPane2" runat="server" MinWidth="200" Width="730px" Scrolling="None">
        <fieldset style="height: 90%">
            <legend>
                <%# Translate("Thông tin kết quả thi tuyển")%></legend>
            <table class="table-form" width="100%">
                <tr>
                    <td>
                        <%# Translate("Vòng thi")%>:
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
                        <tlk:RadNumericTextBox ID="txtMarks" runat="server" AutoPostBack="true" NumberFormat-DecimalDigits="1"
                            NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="0"
                            MaxValue="1000">
                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                           <ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />
                        </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMarks"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập điểm thi. %>"
                            Text="<%$ Translate: Bạn phải nhập điểm thi. %>" ToolTip="<%$ Translate: Bạn phải nhập điểm thi. %>"> </asp:RequiredFieldValidator>
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
                <tr>
                    <td>
                        <%# Translate("Kết quả")%>:
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox ID="txtResult" runat="server" ReadOnly="true" Width="250px" Font-Bold="true"
                            BorderWidth="0">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
            <div>
                <asp:Label ID="lblAVG" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label>
            </div>
        </fieldset>
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
        function clientButtonClicking(sender, args) {
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
        }
        function displayDecimalFormat(sender, args) {
            sender.set_textBoxValue(sender.get_value().toString());
        }
    </script>
</tlk:RadCodeBlock>
