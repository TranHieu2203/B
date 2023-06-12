<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalCompetencyCourse.ascx.vb"
    Inherits="Profile.ctrlPortalCompetencyCourse" %>
<%@ Import Namespace="Common" %>
<span class="title-dbportal">
    Khóa đào tạo theo chức danh</span>
<div class="boxdbPortal">
    <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="350px" AllowFilteringByColumn="true">
        <MasterTableView DataKeyNames="EMPLOYEE_ID" ClientDataKeyNames="EMPLOYEE_ID">
            <Columns>
                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                </tlk:GridClientSelectColumn>
                <tlk:GridBoundColumn DataField="ID" Visible="false" />
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm năng lực %>" DataField="COMPETENCY_GROUP_NAME"
                    UniqueName="COMPETENCY_GROUP_NAME" SortExpression="COMPETENCY_GROUP_NAME">
                    <HeaderStyle Width="230px" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Năng lực %>" DataField="COMPETENCY_NAME"
                    UniqueName="COMPETENCY_NAME" SortExpression="COMPETENCY_NAME">
                    <HeaderStyle Width="330px" />
                </tlk:GridBoundColumn>
                <tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm chuẩn theo chức danh %>" DataField="LEVEL_NUMBER_STANDARD_NAME"
                    UniqueName="LEVEL_NUMBER_STANDARD_NAME" SortExpression="LEVEL_NUMBER_STANDARD_NAME">
                    <HeaderStyle Width="170px" />
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                </tlk:GridNumericColumn>
                <tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm đánh giá hiện tại %>" DataField="LEVEL_NUMBER_ASS_NAME"
                    UniqueName="LEVEL_NUMBER_ASS_NAME" SortExpression="LEVEL_NUMBER_ASS_NAME">
                    <HeaderStyle Width="170px" />
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                </tlk:GridNumericColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa học cần tham gia %>" DataField="TR_COURSE_NAME"
                    UniqueName="TR_COURSE_NAME" SortExpression="TR_COURSE_NAME" />
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" />
            <ClientEvents OnRowDblClick="gridRowDblClick" />
        </ClientSettings>
    </tlk:RadGrid>
</div>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var selectedID = -1;
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEdit();
        }

        function OpenEdit() {
            //            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            //            if (bCheck == 0)
            //                return 0;
            //            if (bCheck > 1)
            //                return 1;
            //            var Year = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PE_PERIO_YEAR');
            //            var PeriodId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PE_PERIO_ID');
            //            var TypeAssId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PE_PERIO_TYPE_ASS');
            //            var EmpId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            //            //            var oWindow = radopen('Dialog.aspx?mid=Performance&fid=ctrlPE_Assess&group=Business&noscroll=1&Year=' + Year + '&PeriodId=' + PeriodId + '&TypeAssId=' + TypeAssId + '&EmpId=' + EmpId, "rwPopup");
            //            //            var pos = $("html").offset();
            //            //            oWindow.moveTo(pos.left, pos.top);
            //            //            oWindow.setSize($(window).width(), $(window).height());
            //            OpenInNewTab('Default.aspx?mid=Performance&fid=ctrlPE_Assess&Year=' + Year + '&PeriodId=' + PeriodId + '&TypeAssId=' + TypeAssId + '&EmpId=' + EmpId)
            //            return 2;
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
        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
</tlk:RadCodeBlock>
