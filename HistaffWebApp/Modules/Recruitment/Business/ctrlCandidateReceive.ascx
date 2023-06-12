<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCandidateReceive.ascx.vb"
    Inherits="Recruitment.ctrlCandidateReceive" %>
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
            <MasterTableView DataKeyNames="ID,CANDIDATE_CODE,STATUS_ID,JOIN_PLAN_DATE,PLANNING_ID,PLAN_NAME,PLAN_TITLE_NAME,ORG_ID,STATUSMAILRECRUITMENT,Email,ORG_NAME" ClientDataKeyNames="ID,CANDIDATE_CODE,STATUS_ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />
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
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày dự kiến %>" DataField="JOIN_PLAN_DATE"
                        UniqueName="JOIN_PLAN_DATE" SortExpression="JOIN_PLAN_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày vào %>" DataField="JOIN_DATE"
                        UniqueName="JOIN_DATE" SortExpression="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Email %>" HeaderStyle-Width="120px" DataField="EMAIL"
                        UniqueName="EMAIL" SortExpression="EMAIL" />                        
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thang lương hệ số %>" DataField="SAL_GROUP_NAME"
                        SortExpression="SAL_GROUP_NAME" UniqueName="SAL_GROUP_NAME" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngạch lương hệ số %>" DataField="SAL_LEVEL_NAME"
                        SortExpression="SAL_LEVEL_NAME" UniqueName="SAL_LEVEL_NAME" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Bậc lương hệ số %>" DataField="SAL_RANK"
                        SortExpression="SAL_RANK" UniqueName="SAL_RANK" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số lương hệ số %>" DataField="SAL_COEFFICIENT_NAME"
                        SortExpression="SAL_COEFFICIENT_NAME" UniqueName="SAL_COEFFICIENT_NAME" HeaderStyle-Width="150px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hưởng %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái ứng viên %>" DataField="STATUS_NAME"
                        UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" HeaderStyle-Width="125px" />
                 <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái gửi thư trúng tuyển %>" HeaderStyle-Width="125px" DataField="STATUSMAILRECRUITMENT_NAME_VN"
                        UniqueName="STATUSMAILRECRUITMENT_NAME_VN" SortExpression="STATUSMAILRECRUITMENT_NAME_VN" />
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
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Cập nhật thông tin tiếp nhận %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        function OpenEditWindow(states) {
            var CanId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('CANDIDATE_CODE');
            var status = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_ID');
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlCandidateReceiveNewEdit&group=Business&Can=' + CanId + '&status=' + status + '', "_self"); /*
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

            if (args.get_item().get_commandName() == 'PRINT_PASS') {
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

    </script>
</tlk:RadScriptBlock>
