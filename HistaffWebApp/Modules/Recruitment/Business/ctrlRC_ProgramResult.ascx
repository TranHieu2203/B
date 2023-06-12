<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramResult.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramResult" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidProgramID" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="HiddenField1" runat="server" />
<asp:HiddenField ID="HiddenField2" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="180px" Scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6">
                  
                        <%# Translate("Thông tin chương trình tuyển dụng")%>
                  
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày gửi yêu cầu")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSendDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Mã tuyển dụng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tên công việc")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtJobName" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Vị trí tuyển dụng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng cần tuyển")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRequestNumber" runat="server" ReadOnly="True" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chọn môn thi")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboExams" runat="server" CausesValidation="false" AutoPostBack="true">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Thang điểm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPointLadder" runat="server" SkinID="number" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Điểm đạt")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPointPass" runat="server" SkinID="number" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <tlk:RadButton ButtonType="ToggleButton" ToggleType="CheckBox" runat="server" CausesValidation="false"
                        ID="chkIsPV" Text="<%$ Translate: Phỏng vấn? %>" Enabled="false">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true" AllowMultiRowEdit="true" AllowSorting="false">
            <MasterTableView DataKeyNames="ID,CANDIDATE_ID,STATUS_ID" EditMode="InPlace">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CANDIDATE_CODE"
                        UniqueName="CANDIDATE_CODE" SortExpression="CANDIDATE_CODE" ReadOnly="true" HeaderStyle-Width="70px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="CANDIDATE_NAME"
                        UniqueName="CANDIDATE_NAME" SortExpression="CANDIDATE_NAME" ReadOnly="true" HeaderStyle-Width="120px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                        UniqueName="GENDER_NAME" SortExpression="GENDER_NAME" ReadOnly="true" HeaderStyle-Width="60px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                        UniqueName="BIRTH_DATE" SortExpression="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        HeaderStyle-Width="100px" ReadOnly="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại liên lạc %>" DataField="MOBILE_PHONE"
                        UniqueName="MOBILE_PHONE" SortExpression="MOBILE_PHONE" ReadOnly="true" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CMND %>" DataField="ID_NO" UniqueName="ID_NO"
                        SortExpression="ID_NO" ReadOnly="true" HeaderStyle-Width="100px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thi %>" DataField="SCHEDULE_DATE"
                        UniqueName="SCHEDULE_DATE" SortExpression="SCHEDULE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        HeaderStyle-Width="100px" ReadOnly="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Kết quả  %>" DataField="POINT_RESULT"
                        UniqueName="POINT_RESULT" SortExpression="POINT_RESULT" DataFormatString="{0:n0}"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="70px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhận xét %>" DataField="COMMENT_INFO"
                        UniqueName="COMMENT_INFO" SortExpression="COMMENT_INFO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thông tin đánh giá %>" DataField="ASSESSMENT_INFO"
                        UniqueName="ASSESSMENT_INFO" SortExpression="ASSESSMENT_INFO" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đạt %>" DataField="IS_PASS" UniqueName="IS_PASS"
                        SortExpression="IS_PASS" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái ứng viên %>" DataField="STATUS_NAME"
                        UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" ReadOnly="true" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="150px" />
            <ClientSettings>
                <Scrolling FrozenColumnsCount="5" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Height="50px" Scrolling="None">
        <table  class="table-form">
            <tr>
                <td>
                    <tlk:RadButton ID="btnThiDat" runat="server" Text="<%$ Translate: Ứng viên đạt %>"
                        OnClientClicking="btnThiDatClick">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnKhongThiDat" runat="server" Text="<%$ Translate: Ứng viên không đạt %>"
                        OnClientClicking="btnKhongThiDatClick">
                    </tlk:RadButton>
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
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }

            if (args.get_item().get_commandName() == 'THIDAT' ||
            args.get_item().get_commandName() == 'THIKHONGDAT' ||
            args.get_item().get_commandName() == 'SAVE') {
                var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
        }

        function btnThiDatClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }
        }
        function btnKhongThiDatClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }
        }

    </script>
</tlk:RadCodeBlock>
