<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Request.ascx.vb"
    Inherits="Training.ctrlTR_Request" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntYear" runat="server" NumberFormat-DecimalDigits="0"
                                NumberFormat-GroupSeparator="" ShowSpinButtons="true">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboStatus">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS_ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="STATUS_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="COM_DESC" UniqueName="COM_DESC" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_DESC" UniqueName="ORG_DESC" Visible="false" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" HeaderStyle-Width="90px"/>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã YCĐT %>" DataField="REQUEST_CODE"
                                SortExpression="REQUEST_CODE" UniqueName="REQUEST_CODE" HeaderStyle-Width="90px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người yêu cầu %>" DataField="SENDER_NAME"
                                SortExpression="REQUEST_SENDER" UniqueName="SENDER_NAME" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh người yêu cầu %>" DataField="SENDER_TITLE_NAME" SortExpression="SENDER_TITLE_NAME"
                                UniqueName="SENDER_TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Email người gửi yêu cầu %>" DataField="SENDER_EMAIL"
                                SortExpression="SENDER_EMAIL" UniqueName="SENDER_EMAIL" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại người gửi yêu cầu %>"
                                DataField="SENDER_MOBILE" SortExpression="SENDER_MOBILE" UniqueName="SENDER_MOBILE" HeaderStyle-Width="120px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày gửi yêu cầu %>" DataField="REQUEST_DATE"
                                SortExpression="REQUEST_DATE" UniqueName="REQUEST_DATE" HeaderStyle-Width="110px" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo %>" DataField="COURSE_NAME"
                                SortExpression="COURSE_NAME" UniqueName="COURSE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo khác %>" DataField="OTHER_COURSE" SortExpression="OTHER_COURSE"
                                UniqueName="OTHER_COURSE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="TRAIN_FORM"
                                SortExpression="FORM_NAME" UniqueName="TRAIN_FORM" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mục tiêu đào tạo %>" DataField="TARGET_TRAIN"
                                SortExpression="TARGET_TRAIN" UniqueName="TARGET_TRAIN" HeaderStyle-Width="250px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung đào tạo %>" DataField="CONTENT"
                                SortExpression="CONTENT" UniqueName="CONTENT" HeaderStyle-Width="250px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kế hoạch đào tạo %>" DataField="TR_PLAN_NAME"
                                SortExpression="TR_PLAN_NAME" UniqueName="TR_PLAN_NAME" HeaderStyle-Width="250px" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Công ty %>" DataField="COM_NAME"
                                SortExpression="COM_NAME" UniqueName="COM_NAME" HeaderStyle-Width="250px" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="250px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian dự kiến từ %>" DataField="EXPECTED_DATE"
                                SortExpression="EXPECTED_DATE" UniqueName="EXPECTED_DATE" HeaderStyle-Width="110px" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian dự kiến đến %>" DataField="EXPECT_DATE_TO"
                                SortExpression="EXPECT_DATE_TO" UniqueName="EXPECT_DATE_TO" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Thời gian bắt đầu %>" DataField="START_DATE"
                                SortExpression="START_DATE" UniqueName="START_DATE" HeaderStyle-Width="110px" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trung tâm đào tạo %>" DataField="CENTERS"
                                SortExpression="CENTERS" UniqueName="CENTERS" HeaderStyle-Width="250px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị chủ trì đào tạo %>" DataField="UNIT_NAME"
                                SortExpression="UNIT_NAME" UniqueName="UNIT_NAME" HeaderStyle-Width="250px" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Giảng viên %>" DataField="TEACHERS"
                                SortExpression="TEACHERS" UniqueName="TEACHERS" />
                           <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa điểm %>" DataField="VENUE" SortExpression="VENUE"
                                UniqueName="VENUE" />--%>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí dự kiến %>" DataField="EXPECTED_COST"
                                SortExpression="EXPECTED_COST" UniqueName="EXPECTED_COST" DataFormatString="{0:N0}" />
                            
                           
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số học viên dự kiến %>" DataField="TRAINER_NUMBER"
                                SortExpression="TRAINER_NUMBER" UniqueName="TRAINER_NUMBER" DataFormatString="{0:N0}" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Cam kết đào tạo %>" DataField="TR_COMMIT"
                                SortExpression="TR_COMMIT" UniqueName="TR_COMMIT" AllowFiltering="false"/>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Bằng cấp/Chứng chỉ đạt được %>" DataField="CERTIFICATE"
                                SortExpression="CERTIFICATE" UniqueName="CERTIFICATE" AllowFiltering="false"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi đào tạo %>" DataField="TR_PLACE" SortExpression="TR_PLACE"
                                UniqueName="TR_PLACE" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không duyệt %>" DataField="REJECT_REASON"
                                SortExpression="REJECT_REASON" UniqueName="REJECT_REASON" />
                        </Columns>
                        <HeaderStyle Width="150px" />
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
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {
            window.open('/Default.aspx?mid=Training&fid=ctrlTR_RequestNewEdit&group=Business', "_self"); /*
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
            window.open('/Default.aspx?mid=Training&fid=ctrlTR_RequestNewEdit&group=Business&editable=true&ID=' + id, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            return 2;
        }

        function gridRowDblClick(sender, eventArgs) {
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Training&fid=ctrlTR_RequestNewEdit&group=Business&ID=' + id, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            return 0;
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
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == "NEXT") {
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
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
