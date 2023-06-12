<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_RegisterNo.ascx.vb"
    Inherits="Recruitment.ctrlRC_RegisterNo" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="none">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="20" ID="rgData" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,ORG_DESC_REQUEST,ORG_DESC_PERFORM" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm khai báo %>" DataField="YEAR"
                                SortExpression="YEAR" UniqueName="YEAR" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tuyển dụng %>" DataField="CODE"
                                SortExpression="CODE" UniqueName="CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phiếu tuyển dụng %>" DataField="REQUIRED_VOTES_NAME"
                                SortExpression="REQUIRED_VOTES_NAME" UniqueName="REQUIRED_VOTES_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị tuyển dụng %>" DataField="ORG_REQUEST_NAME"
                                SortExpression="ORG_REQUEST_NAME" UniqueName="ORG_REQUEST_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: SL định biến %>" DataField="NUMBER_PLAN"
                                SortExpression="NUMBER_PLAN" UniqueName="NUMBER_PLAN" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: SL lao động hiện tại %>" DataField="NUMBER_NOW"
                                SortExpression="NUMBER_NOW" UniqueName="NUMBER_NOW" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: SL cần tuyển dụng %>" DataField="NUMBER_MUST"
                                SortExpression="NUMBER_MUST" UniqueName="NUMBER_MUST" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày tạo mã tuyển dụng %>" DataField="RC_CREATE_DATE"
                                SortExpression="RC_CREATE_DATE" UniqueName="RC_CREATE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểm tra định biên %>" DataField="RC_CHECK_NAME"
                                SortExpression="RC_CHECK_NAME" UniqueName="RC_CHECK_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: SL tuyển dụng cho 1 mã chi tiết %>"
                                DataField="NUMBER_REQUIRED_DETAIL" SortExpression="NUMBER_REQUIRED_DETAIL" UniqueName="NUMBER_REQUIRED_DETAIL" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số mã tuyển dụng chi tiết %>" DataField="ID_REQUIRED_DETAIL"
                                SortExpression="ID_REQUIRED_DETAIL" UniqueName="ID_REQUIRED_DETAIL" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Thời gian cần tuyển %>" DataField="TIME_REQUIRED"
                                SortExpression="TIME_REQUIRED" UniqueName="TIME_REQUIRED" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hình tuyển dụng %>" DataField="TYPE_REQUIRED_NAME"
                                SortExpression="TYPE_REQUIRED_NAME" UniqueName="TYPE_REQUIRED_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị thực hiện tuyển dụng %>" DataField="ORG_PERFROM_NAME"
                                SortExpression="ORG_PERFROM_NAME" UniqueName="ORG_PERFROM_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tuổi từ %>" DataField="FROM_AGE"
                                SortExpression="FROM_AGE" UniqueName="FROM_AGE" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tới tuổi %>" DataField="TO_AGE"
                                SortExpression="TO_AGE" UniqueName="TO_AGE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                                SortExpression="GENDER_NAME" UniqueName="GENDER_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ %>" DataField="LEARMING_LEVEL_NAME"
                                SortExpression="LEARMING_LEVEL_NAME" UniqueName="LEARMING_LEVEL_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chuyển ngành %>" DataField="MAJOR_NAME"
                                SortExpression="MAJOR_NAME" UniqueName="MAJOR_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kinh nghiệm %>" DataField="EXPERIENCE"
                                SortExpression="EXPERIENCE" UniqueName="EXPERIENCE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kỹ năng %>" DataField="SKILLS" SortExpression="SKILLS"
                                UniqueName="SKILLS" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Môn thi 1 %>" DataField="EXAM_1"
                                SortExpression="EXAM_1" UniqueName="EXAM_1" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Môn thi 2 %>" DataField="EXAM_2"
                                SortExpression="EXAM_2" UniqueName="EXAM_2" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Môn thi 3 %>" DataField="EXAM_3"
                                SortExpression="EXAM_3" UniqueName="EXAM_3" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
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
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {
            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_RegisterNoNewEdit&group=Business&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(1200, 500);
            oWindow.center();
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_RegisterNoNewEdit&group=Business&noscroll=1&ID=' + id, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(1200, 500);
            oWindow.center();
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
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'DELETE') {
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

    </script>
</tlk:RadCodeBlock>
