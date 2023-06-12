<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramScheduleNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramScheduleNewEdit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidProgramID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane5" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="210px">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table>
            <tr>
                <td valign="top">
                    <table class="table-form">
                        <tr>
                            <td class="item-head" colspan="6">
                                <%# Translate("Thông tin lịch phỏng vấn/thi tuyển")%>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ban/Phòng")%>
                            </td>
                            <td>
                                <asp:Label ID="lblJobName" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            <td class="lb">
                                <%# Translate("Vị trí tuyển dụng")%>
                            </td>
                            <td>
                                <asp:Label ID="lblTitleName" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            <td class="lb">
                                <%# Translate("Số lượng cần tuyển")%>
                            </td>
                            <td>
                                <asp:Label ID="lblRequestNo" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngày thi")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdScheduleDate" runat="server">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdScheduleDate"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày thi %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày thi %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Người coi thi/phỏng vấn")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboUsher" runat="server">
                                </tlk:RadComboBox>
                                <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindUsher" runat="server" SkinID="ButtonView"
                                    CausesValidation="false">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Thời gian")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtTime" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Địa điểm thi")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtExamsPlace" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ghi chú")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="min-width: 40px">
                </td>
                <td valign="top">
                    <table class="table-form">
                        <tr>
                            <td class="item-head" colspan="6">
                                <%# Translate("Danh sách các vòng tuyển dụng")%>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--<tlk:RadListBox runat="server" ID="rlbExams" ItemType="Aspose.Cells.Drawing.RadioButton" CheckBoxes="true" Width="300px" Height="120px">
                                </tlk:RadListBox>--%>
                                <asp:RadioButtonList runat="server" ID="rlbExams" CausesValidation="false" AutoPostBack="true">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6" valign="top" style="width: 50%">
                    <%# Translate("Danh sách ứng viên chưa đặt lịch")%>
                </td>
                <td style="min-width: 50px">
                </td>
                <td class="item-head" colspan="6" valign="top" style="width: 50%">
                    <%# Translate("Danh sách ứng viên đã đặt lịch")%>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgCanNotSchedule" runat="server" Height="100%" AllowSorting="false">
                    <MasterTableView DataKeyNames="ID,PRO_CAN">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CODE"
                                UniqueName="CODE" SortExpression="CODE" HeaderStyle-Width="60px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME" UniqueName="FULLNAME"
                                SortExpression="FULLNAME" HeaderStyle-Width="90px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="DOB"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số CMND %>" DataField="IDNO" UniqueName="IDNO"
                                SortExpression="IDNO" HeaderStyle-Width="90px" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Width="50px">
                <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server" OnAjaxRequest="RadAjaxPanel1_AjaxRequest">
                </tlk:RadAjaxPanel>
                <tlk:RadCodeBlock ID="RadCodeBlock2" runat="server">
                    <script type="text/javascript">
                        function OnClientClick(sender, eventArgs) {
                            $find("<%= RadAjaxPanel1.ClientID%>").ajaxRequestWithTarget("<%= RadAjaxPanel1.UniqueID %>", sender.get_id());
                        }
                    </script>
                </tlk:RadCodeBlock>
                <table class="table-form">
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnDelete" runat="server" Font-Bold="true" Text="<--" OnClientClicked="OnClientClick"
                                AutoPostBack="false" CausesValidation="false">
                            </tlk:RadButton>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnInsert" runat="server" Font-Bold="true" Text="-->" OnClientClicked="OnClientClick"
                                AutoPostBack="false">
                            </tlk:RadButton>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgCanSchedule" runat="server" Height="100%" AllowSorting="false">
                    <MasterTableView DataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí tuyển dụng %>" DataField="VITRI"
                                UniqueName="VITRI" SortExpression="VITRI" HeaderStyle-Width="90px" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban tuyển dụng %>" DataField="PHONGBAN"
                                UniqueName="PHONGBAN" SortExpression="PHONGBAN" HeaderStyle-Width="90px" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CANDIDATE_CODE"
                                UniqueName="CANDIDATE_CODE" SortExpression="CANDIDATE_CODE" HeaderStyle-Width="60px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME_VN"
                                UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" HeaderStyle-Width="90px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số CMND %>" DataField="ID_NO" UniqueName="ID_NO"
                                SortExpression="ID_NO" HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại %>" DataField="CONTACT_MOBILE"
                                UniqueName="CONTACT_MOBILE" SortExpression="CONTACT_MOBILE" HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Email %>" DataField="PER_EMAIL" UniqueName="PER_EMAIL"
                                SortExpression="PER_EMAIL" HeaderStyle-Width="90px" Visible="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày phỏng vấn %>" DataField="SCHEDULE_DATE"
                                UniqueName="SCHEDULE_DATE" SortExpression="SCHEDULE_DATE" HeaderStyle-Width="90px"
                                Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phỏng vấn %>" DataField="NGUOIPV"
                                UniqueName="NGUOIPV" SortExpression="NGUOIPV" HeaderStyle-Width="90px" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa điểm phỏng vấn %>" DataField="EXAMS_PLACE"
                                UniqueName="EXAMS_PLACE" SortExpression="EXAMS_PLACE" HeaderStyle-Width="90px"
                                Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                                SortExpression="NOTE" HeaderStyle-Width="90px" Visible="false" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane6" runat="server" Height="50px">
        <table class="table-form">
            <tr>
                <td>
                    <tlk:RadButton CausesValidation="false" OnClientClicked="OpenEditTransfer" ID="btnInterviewList"
                        runat="server" Text="<%$ Translate: Danh sách phỏng vấn %>">
                    </tlk:RadButton>
                </td>
                <td style="display:none">
                    <tlk:RadButton CausesValidation="false" OnClientClicked="OpenEditTransfer" ID="btnSendMail"
                        runat="server" Text="<%$ Translate: Gửi mail %>">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton CausesValidation="false" OnClientClicked="OpenEditTransfer" ID="btnRecLetter"
                        runat="server" Text="<%$ Translate: Thư mời tuyển dụng %>" Visible="false">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton CausesValidation="false" OnClientClicked="OpenEditTransfer" ID="btnRecInterviewLetter"
                        runat="server" Text="<%$ Translate: Phiếu phỏng vấn tuyển dụng %>" Visible="false">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindUsher" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">


        function btnInsert_Click(sender, args) {
            var bCheck = $find('<%# rgCanNotSchedule.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }

        }

        function btnDelete_Click(sender, args) {
            var bCheck = $find('<%# rgCanSchedule.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }

        }

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


        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
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
