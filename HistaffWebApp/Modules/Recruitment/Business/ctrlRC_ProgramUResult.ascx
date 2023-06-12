<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramUResult.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramUResult" %>
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
                            <%# Translate("Mã YCTD")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblCode" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Vị trí tuyển dụng")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblTitleName" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Phòng ban tuyển dụng")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblOrgName" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        
                        
                        <td class="lb" style="display : none">
                            <%# Translate("Tên công việc")%>:
                        </td>
                        <td style="display : none">
                            <asp:Label ID="lblJobName" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb" style="display : none">
                            <%# Translate("Yêu cầu kinh nghiệm")%>:
                        </td>
                        <td style="display : none">
                            <asp:Label ID="lblExperienceRequired" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="lb">
                            <%# Translate("Ngày YCTD")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblSendDate" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày đáp ứng")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblRespone" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Số lượng cần tuyển")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblRequestNumber" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="lb" >
                            <%# Translate("Hồ sơ đã nhận")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblprofileReceive" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb" >
                            <%# Translate("Số lượng đã tuyển")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblQuantityHasRecruitment" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb" style="display : none">
                            <%# Translate("Trạng thái yêu cầu")%>:
                        </td>
                        <td style="display : none">
                            <asp:Label ID="lblStatusRequest" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb" style="display : none">
                            <%# Translate("Lý do tuyển dụng")%>:
                        </td>
                        <td style="display : none">
                            <asp:Label ID="lblReasonRecruitment" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td >
                        <td class="lb" style="display : none">
                            <%# Translate("Yêu cầu khác")%>:
                        </td>
                        <td colspan="3" style="display : none">
                            <asp:Label ID="lblOtherRequest" runat="server" Text="" Font-Bold="true"></asp:Label>
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
                            <td class="item-head" colspan="6">
                                <%# Translate("Cập nhật kết quả hàng loạt")%>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" >
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
       <%-- <table class="table-form">
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
        </table>--%>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgCanSchedule" runat="server" Height="380px" AllowMultiRowEdit="false" OnPreRender="rgCanSchedule_PreRender"
                AllowSorting="false">
                
                <MasterTableView DataKeyNames="ID,PSC_ID,IS_PV,EXAMS_ORDER,PRO_CAN_ID,STATUS_CODE,POINT_PASS" SkinID="GridSingleSelect" ClientDataKeyNames="ID,PSC_ID,IS_PV,EXAMS_ORDER,PRO_CAN_ID,STATUS_CODE,POINT_PASS">
                    <Columns>
                        <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        </tlk:GridClientSelectColumn>
                        <tlk:GridBoundColumn DataField="ID" Visible="false" />
                        <tlk:GridBoundColumn DataField="PSC_ID" Visible="false" />
                        <tlk:GridBoundColumn DataField="IS_PV" Visible="false" />
                        <tlk:GridBoundColumn DataField="Email" Visible="false" />
                        <tlk:GridBoundColumn DataField="STATUS_CODE" Visible="false" />
                        <tlk:GridBoundColumn DataField="EXAMS_ORDER" Visible="false" />
                        <tlk:GridBoundColumn DataField="POINT_PASS" Visible="false" />
                        <tlk:GridBoundColumn DataField="PRO_CAN_ID" Visible="false" />
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
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="ISPASS" HeaderStyle-Width="30px"
                            UniqueName="ISPASS" SortExpression="ISPASS" ShowFilterIcon="true" />

                        <tlk:GridButtonColumn HeaderText="<%$ Translate: Cập nhật kết quả %>" ButtonType="PushButton" Text="Chi tiết" UniqueName="DETAIL"
                               CommandName="DETAIL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px"  />
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <HeaderStyle HorizontalAlign="Center" Width="150px" />
            </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane6" runat="server" Height="50px" Visible="false">
        <table class="table-form">
            <tr>
                <td>
                    <tlk:RadButton CausesValidation="false" OnClientClicked="OpenEditTransfer" ID="btnInterviewList"
                        runat="server" Text="<%$ Translate: Danh sách phỏng vấn %>">
                    </tlk:RadButton>
                </td>
                <td>
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
        var splitterID = 'ctl00_MainContent_ctrlRC_ProgramUResult_RadSplitter3';

      <%--  function btnInsert_Click(sender, args) {
            var bCheck = $find('<%# rgCanNotSchedule.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }

        }--%>
        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }


        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
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
