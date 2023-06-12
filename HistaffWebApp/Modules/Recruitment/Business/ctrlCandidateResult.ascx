<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCandidateResult.ascx.vb"
    Inherits="Recruitment.ctrlCandidateResult" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="70px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <table class="table-form padding-10">
            <tr>
                <td class="lb">
                    <%# Translate("Năm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" SkinID="Number" ShowSpinButtons="true"
                        AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đợt tuyển dụng")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboPlanning" AutoPostBack="true">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Trạng thái ứng viên")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCandidateStatus" AutoPostBack="true">
                    </tlk:RadComboBox>
                </td>
                <td>
                <asp:CheckBox runat="server" ID="cbIsEnd" Text="<%$ Translate: Hiển thị đợt kết thúc %>" AutoPostBack=true />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
            <MasterTableView DataKeyNames="ID,CANDIDATE_CODE,STATUS_ID,PLANNING_ID,PLAN_NAME,ORG_ID" ClientDataKeyNames="ID,CANDIDATE_CODE,STATUS_ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" UniqueName="YEAR"
                        SortExpression="YEAR" HeaderStyle-Width="70px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên đợt %>" DataField="PLAN_NAME"
                        UniqueName="PLAN_NAME" SortExpression="PLAN_NAME" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí tuyển dụng %>" DataField="PLAN_TITLE_NAME"
                        UniqueName="PLAN_TITLE_NAME" SortExpression="PLAN_TITLE_NAME" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CANDIDATE_CODE"
                        UniqueName="CANDIDATE_CODE" SortExpression="CANDIDATE_CODE" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên ứng viên %>" DataField="FULLNAME_VN"
                        UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" HeaderStyle-Width="125px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                        UniqueName="BIRTH_DATE" SortExpression="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                        UniqueName="GENDER_NAME" SortExpression="GENDER_NAME" HeaderStyle-Width="70px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CMT %>" DataField="ID_NO" UniqueName="ID_NO"
                        SortExpression="ID_NO" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số năm kinh nghiệm %>" DataField="YEAR_EXP"
                        UniqueName="YEAR_EXP" SortExpression="YEAR_EXP" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Môn thi 1 %>" DataField="EXAM1_NAME"
                        UniqueName="EXAM1_NAME" SortExpression="EXAM1_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thi %>" DataField="EXAM1_DATE"
                        UniqueName="EXAM1_DATE" SortExpression="EXAM1_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm đạt %>" DataField="EXAM1_REACH"
                        UniqueName="EXAM1_REACH" SortExpression="EXAM1_REACH" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="EXAM1_RESULT"
                        UniqueName="EXAM1_RESULT" SortExpression="EXAM1_RESULT" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Môn thi 2 %>" DataField="EXAM2_NAME"
                        UniqueName="EXAM2_NAME" SortExpression="EXAM2_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thi %>" DataField="EXAM2_DATE"
                        UniqueName="EXAM2_DATE" SortExpression="EXAM2_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm đạt %>" DataField="EXAM2_REACH"
                        UniqueName="EXAM2_REACH" SortExpression="EXAM2_REACH" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="EXAM2_RESULT"
                        UniqueName="EXAM2_RESULT" SortExpression="EXAM2_RESULT" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Môn thi 3 %>" DataField="EXAM3_NAME"
                        UniqueName="EXAM3_NAME" SortExpression="EXAM3_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thi %>" DataField="EXAM3_DATE"
                        UniqueName="EXAM3_DATE" SortExpression="EXAM3_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm đạt %>" DataField="EXAM3_REACH"
                        UniqueName="EXAM3_REACH" SortExpression="EXAM3_REACH" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="EXAM3_RESULT"
                        UniqueName="EXAM3_RESULT" SortExpression="EXAM3_RESULT" HeaderStyle-Width="125px" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả vòng 1 %>" DataField="ROUND1_STATUS_NAME"
                        UniqueName="ROUND1_STATUS_NAME" SortExpression="ROUND1_STATUS_NAME" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Môn thi 4 %>" DataField="EXAM4_NAME"
                        UniqueName="EXAM4_NAME" SortExpression="EXAM4_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thi %>" DataField="EXAM4_DATE"
                        UniqueName="EXAM4_DATE" SortExpression="EXAM4_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm đạt %>" DataField="EXAM4_REACH"
                        UniqueName="EXAM4_REACH" SortExpression="EXAM4_REACH" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="EXAM4_RESULT"
                        UniqueName="EXAM4_RESULT" SortExpression="EXAM4_RESULT" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Môn thi 5 %>" DataField="EXAM5_NAME"
                        UniqueName="EXAM5_NAME" SortExpression="EXAM5_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thi %>" DataField="EXAM5_DATE"
                        UniqueName="EXAM5_DATE" SortExpression="EXAM5_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm đạt %>" DataField="EXAM5_REACH"
                        UniqueName="EXAM5_REACH" SortExpression="EXAM5_REACH" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="EXAM5_RESULT"
                        UniqueName="EXAM5_RESULT" SortExpression="EXAM5_RESULT" HeaderStyle-Width="125px" />
                         <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả vòng 2 %>" DataField="ROUND2_STATUS_NAME"
                        UniqueName="ROUND2_STATUS_NAME" SortExpression="ROUND2_STATUS_NAME" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhận xét %>" DataField="EXAM_COMMENT"
                        UniqueName="EXAM_COMMENT" SortExpression="EXAM_COMMENT" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái ứng viên %>" DataField="STATUS_NAME"
                        UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="EXAM_REMARK"
                        UniqueName="EXAM_REMARK" SortExpression="EXAM_REMARK" HeaderStyle-Width="125px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái gửi mail %>" DataField="STATUS_MAIL_NAME"
                        UniqueName="STATUS_MAIL_NAME" SortExpression="STATUS_MAIL_NAME" HeaderStyle-Width="125px" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Cập nhật kết quả %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        function OpenEditWindow(states) {
            var CanId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('CANDIDATE_CODE');
            var status = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_ID');
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlCandidateResultNewEdit&group=Business&Can=' + CanId + '&status=' + status + '', "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */

        }


        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow("Edit");
                    args.set_cancel(true);
                }

            }

            if (args.get_item().get_commandName() == 'PRINT_KQ') {
                enableAjax = false;
            }
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            }
        }

        function postBack(url) {
        }

    </script>
</tlk:RadScriptBlock>
