<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ProgramClassStudent.ascx.vb"
    Inherits="Training.ctrlTR_ProgramClassStudent" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidClassID" runat="server" />
<asp:HiddenField runat="server" ID="hidProgramID" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane5" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="145px">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin lớp học")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên lớp")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian học từ")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEndDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <%--<tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin tìm kiếm")%>
                    <hr />
                </td>
            </tr>--%>
            <tr style="display:none">
                <td>
                    <%# Translate("Chọn đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Width="100%" ReadOnly="true" />
                </td>
                <td colspan="3">
                    <tlk:RadButton runat="server" ID="btnChoose" SkinID="ButtonView" CausesValidation="false" />
                    <%--</td>
                <td>
                    <%# Translate("Loại hợp đồng")%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td colspan="2">
                    <tlk:RadListBox runat="server" ID="lstPositions" Width="100%" Height="100px" CheckBoxes="true"
                        OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                    </tlk:RadListBox>
                </td>
                <td colspan="3">
                    <tlk:RadListBox ID="lstContractType" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging"
                        CheckBoxes="true" runat="server" Width="100%" Height="100px">
                    </tlk:RadListBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
                <td>--%>
                    <asp:CheckBox ID="chkIsPlan" runat="server" Text="<%$ Translate: Tìm kiếm theo yêu cầu đào tạo %>"
                        AutoPostBack="true" CausesValidation="false" />
                    <%--</td>
                <td>--%>
                    <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="item-head" colspan="6">
                            <%# Translate("Danh sách nhân viên chưa xếp lớp")%>
                        </td>
                    </tr>
                </table>
                <tlk:RadGrid ID="rgCanNotSchedule" runat="server" Height="93%" AllowSorting="false" PageSize="50">
                    <MasterTableView DataKeyNames="EMPLOYEE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                                UniqueName="BIRTH_DATE" SortExpression="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                                UniqueName="GENDER_NAME" SortExpression="GENDER_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" HeaderStyle-Width="180px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hợp đồng %>" DataField="CONTRACT_TYPE_NAME"
                                UniqueName="CONTRACT_TYPE_NAME" SortExpression="CONTRACT_TYPE_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số CMND %>" DataField="ID_NO" UniqueName="ID_NO"
                                SortExpression="ID_NO" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="ID_DATE"
                                UniqueName="ID_DATE" SortExpression="ID_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Width="50px">
                <table class="table-form">
                    <tr style="height: 25px">
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnDelete" runat="server" Font-Bold="true" Text="<--" OnClientClicking="btnDelete_Click"
                                CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnInsert" runat="server" Font-Bold="true" Text="-->" OnClientClicking="btnInsert_Click">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="item-head" colspan="6">
                            <%# Translate("Danh sách nhân viên đã xếp lớp")%>
                        </td>
                    </tr>
                </table>
                <tlk:RadGrid ID="rgCanSchedule" runat="server" Height="93%" AllowSorting="false" PageSize="50">
                    <MasterTableView DataKeyNames="EMPLOYEE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                                UniqueName="BIRTH_DATE" SortExpression="BIRTH_DATE" DataFormatString="{0:dd/mm/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                                UniqueName="GENDER_NAME" SortExpression="GENDER_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" HeaderStyle-Width="180px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" HeaderStyle-Width="180px" />
                           <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hợp đồng %>" DataField="CONTRACT_TYPE_NAME"
                                UniqueName="CONTRACT_TYPE_NAME" SortExpression="CONTRACT_TYPE_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số CMND %>" DataField="ID_NO" UniqueName="ID_NO"
                                SortExpression="ID_NO" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="ID_DATE"
                                UniqueName="ID_DATE" SortExpression="ID_DATE" DataFormatString="{0:dd/mm/yyyy}" />--%>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Email công ty %>" DataField="WORK_EMAIL"
                                UniqueName="WORK_EMAIL" SortExpression="WORK_EMAIL" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Email cá nhân %>" DataField="PER_EMAIL"
                                UniqueName="PER_EMAIL" SortExpression="PER_EMAIL " HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Gửi mail thông báo %>" DataField="SEND_MAIL_STATUS"
                                UniqueName="SEND_MAIL_STATUS" SortExpression="SEND_MAIL_STATUS" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
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

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                //                getRadWindow().close(null);
                //                args.set_cancel(true);
                window.history.back();
            }
            if (args.get_item().get_commandName() == 'SAVE') {
                window.history.back();
            }
            if (args.get_item().get_commandName() == 'PRINT_STUDENT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'PRINT_COMPLETE') {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadCodeBlock>
