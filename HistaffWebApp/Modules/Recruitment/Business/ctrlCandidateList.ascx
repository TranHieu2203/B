<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCandidateList.ascx.vb"
    Inherits="Recruitment.ctrlCandidateList" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
            <MasterTableView DataKeyNames="ID,CANDIDATE_CODE,STATUS_ID,PLANNING_ID,PLAN_NAME,Email,STATUSMAILTHANKS,STATUSMAILRECRUITMENT,ORG_ID,ORG_NAME,PLAN_TITLE_NAME"
                ClientDataKeyNames="ID,CANDIDATE_CODE,STATUS_ID,PLANNING_ID,PLAN_NAME,Email,STATUSMAILTHANKS,STATUSMAILRECRUITMENT,ORG_ID,ORG_NAME">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />                    
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" UniqueName="YEAR"
                        SortExpression="YEAR" HeaderStyle-Width="70px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên đợt %>" DataField="PLAN_NAME"
                        UniqueName="PLAN_NAME" SortExpression="PLAN_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí tuyển dụng %>" DataField="PLAN_TITLE_NAME"
                        UniqueName="PLAN_TITLE_NAME" SortExpression="PLAN_TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CANDIDATE_CODE"
                        UniqueName="CANDIDATE_CODE" SortExpression="CANDIDATE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên ứng viên %>" DataField="FULLNAME_VN"
                        UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                        UniqueName="BIRTH_DATE" SortExpression="BIRTH_DATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                        UniqueName="GENDER_NAME" SortExpression="GENDER_NAME" />
                   <tlk:GridBoundColumn HeaderText="<%$ Translate: Email %>" DataField="EMAIL"
                        UniqueName="EMAIL" SortExpression="EMAIL" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CMT %>" DataField="ID_NO" UniqueName="ID_NO"
                        SortExpression="ID_NO" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="ID_DATE"
                        UniqueName="ID_DATE" SortExpression="ID_DATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ chuyên môn %>" DataField="HIGHEST_LEVEL_NAME"
                        UniqueName="HIGHEST_LEVEL_NAME" SortExpression="HIGHEST_LEVEL_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số năm kinh nghiệm %>" DataField="YEAR_EXP"
                        UniqueName="YEAR_EXP" SortExpression="YEAR_EXP" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái ứng viên %>" DataField="STATUS_NAME"
                        UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái gửi email cảm ơn %>" DataField="STATUSMAILTHANKS_NAME_VN"
                        UniqueName="STATUSMAILTHANKS_NAME_VN" SortExpression="STATUSMAILTHANKS_NAME_VN" />
                         <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái gửi email trúng tuyển %>" DataField="STATUSMAILRECRUITMENT_NAME_VN"
                        UniqueName="STATUSMAILRECRUITMENT_NAME_VN" SortExpression="STATUSMAILRECRUITMENT_NAME_VN" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="200px" />
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết ứng viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        function OpenEditWindow(states) {
            var CanId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('CANDIDATE_CODE');
            var plan = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PLANNING_ID');

            window.open('/Default.aspx?mid=Recruitment&fid=ctrlCanDtl&group=Business&plan=' + plan + '&Can=' + CanId + '&state=' + states + '', "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */

        }

        function OpenInsertWindow() {
            var plan = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PLANNING_ID');

            if (!plan) {
                var m = '<%# Translate("Bạn chưa chọn yêu cầu tuyển dụng") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlCanDtl&group=Business&state=New&reload=1&plan=' + plan, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                else {
                    OpenEditWindow("Edit");
                }
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientClose(oWnd, args) {
            $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
        }


    </script>
</tlk:RadScriptBlock>
