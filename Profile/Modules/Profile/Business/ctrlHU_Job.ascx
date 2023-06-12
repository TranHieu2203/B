<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Job.ascx.vb"
    Inherits="Profile.ctrlHU_Job" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
                  <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick"  />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,COLOR,ACTFLG" ClientDataKeyNames="ID">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã công việc %>" DataField="CODE" HeaderStyle-Width="50px"
                                SortExpression="CODE" UniqueName="CODE" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng Việt %>" DataField="NAME_VN" HeaderStyle-Width="70px"
                                SortExpression="NAME_VN" UniqueName="NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng Anh %>" DataField="NAME_EN" HeaderStyle-Width="70px"
                                SortExpression="NAME_EN" UniqueName="NAME_EN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm cấp bậc %>" DataField="PHAN_LOAI_NAME" HeaderStyle-Width="70px"
                                SortExpression="PHAN_LOAI_NAME" UniqueName="PHAN_LOAI_NAME" />--%>
                           <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Thang công việc %>" DataField="JOB_BAND_NAME" HeaderStyle-Width="70px"
                                SortExpression="JOB_BAND_NAME" UniqueName="JOB_BAND_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm công việc %>" DataField="JOB_FAMILY_NAME" HeaderStyle-Width="70px"
                                SortExpression="JOB_FAMILY_NAME" UniqueName="JOB_FAMILY_NAME" />--%>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Mục đích %>" DataField="PURPOSE" HeaderStyle-Width="120px"
                                SortExpression="PURPOSE" UniqueName="PURPOSE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Yêu cầu %>" DataField="REQUEST" HeaderStyle-Width="120px"
                                SortExpression="REQUEST" UniqueName="REQUEST" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Yêu cầu công việc %>" DataField="NOTE" HeaderStyle-Width="120px"
                                SortExpression="NOTE" UniqueName="NOTE" />--%>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG" HeaderStyle-Width="50px"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />--%>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
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
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_JobNewEdit&group=Business', "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;

            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_JobNewEdit&group=Business&ID=' + id, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            return 2;
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }

                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'DELETE' ||
                args.get_item().get_commandName() == 'APROVE' ||
                args.get_item().get_commandName() == 'REJECT') {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
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
        function gridRowDblClick(sender, eventArgs) {
        /*OpenEdit();*/
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_JobNewEdit&group=Business&ID=' + id + '&isView=1', "_self");
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
