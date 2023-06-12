<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramInterviewResult_Dialog.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramInterviewResult_Dialog" %>
<%@ Import Namespace="Common" %>
<%@ Register Src="ctrlRC_ProgramExamsResult.ascx" TagName="ctrlRC_ProgramExamsResult"
    TagPrefix="Recruitment" %><%@ Register Src="ctrlRC_ProgramInterviewResult.ascx" TagName="ctrlRC_ProgramInterviewResult"
    TagPrefix="Recruitment" %>
<asp:HiddenField ID="hidProgramID" runat="server" />
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
    </script>
</tlk:RadCodeBlock>
