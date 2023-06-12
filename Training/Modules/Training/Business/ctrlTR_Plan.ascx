<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Plan.ascx.vb"
    Inherits="Training.ctrlTR_Plan" %>
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
            <tlk:RadPane ID="RadPane4" runat="server" Height="33px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm tổ chức")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntYear" runat="server" NumberFormat-DecimalDigits="0"
                                NumberFormat-GroupSeparator="" ShowSpinButtons="true">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm kiếm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã kế hoạch đào tạo %>" DataField="TR_PLAN_CODE"
                                SortExpression="TR_PLAN_CODE" UniqueName="TR_PLAN_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm đào tạo %>" DataField="YEAR" HeaderStyle-Width="50px"
                                SortExpression="YEAR" UniqueName="YEAR" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng/Ban %>" DataField="ORG_NAME" HeaderStyle-Width="200px"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại kế hoạch %>" DataField="PLAN_TYPE_TEXT" HeaderStyle-Width="150px"
                                SortExpression="PLAN_TYPE_TEXT" UniqueName="PLAN_TYPE_TEXT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo %>" DataField="TR_COURSE_NAME"
                                SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tính chất nhu cầu %>" DataField="PROPERTIES_NEED_NAME"
                                SortExpression="PROPERTIES_NEED_NAME" UniqueName="PROPERTIES_NEED_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="TR_TRAIN_FORM_NAME"
                                SortExpression="TR_TRAIN_FORM_NAME" UniqueName="TR_TRAIN_FORM_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="TR_TRAIN_FIELD_NAME"
                                SortExpression="TR_TRAIN_FIELD_NAME" UniqueName="TR_TRAIN_FIELD_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại đào tạo %>" DataField="TR_TYPE_NAME"
                                SortExpression="TR_TYPE_NAME" UniqueName="TR_TYPE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung đào tạo %>" DataField="CONTENT"
                                SortExpression="CONTENT" UniqueName="CONTENT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mục tiêu %>" DataField="TARGET_TRAIN"
                                SortExpression="TARGET_TRAIN" UniqueName="TARGET_TRAIN" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Tháng tổ chức %>" DataField="Months_NAME"
                                SortExpression="Months_NAME" UniqueName="Months_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>--%>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Cam kết đào tạo %>" DataField="TR_COMMIT_TEXT"
                                SortExpression="TR_COMMIT_TEXT" UniqueName="TR_COMMIT_TEXT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Bằng cấp/Chứng chỉ đạt được %>" DataField="CERTIFICATE_TEXT"
                                SortExpression="CERTIFICATE_TEXT" UniqueName="CERTIFICATE_TEXT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đánh giá sau đào tạo %>" DataField="TR_AFTER_TRAIN_TEXT"
                                SortExpression="TR_AFTER_TRAIN_TEXT" UniqueName="TR_AFTER_TRAIN_TEXT" />--%>



                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Cam kết đào tạo %>" DataField="TR_COMMIT" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="TR_COMMIT" SortExpression="TR_COMMIT" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />

                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Bằng cấp/Chứng chỉ đạt được %>" DataField="CERTIFICATE" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="CERTIFICATE" SortExpression="CERTIFICATE" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />

                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đánh giá sau đào tạo %>" DataField="TR_AFTER_TRAIN" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="TR_AFTER_TRAIN" SortExpression="TR_AFTER_TRAIN" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />

                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chương trình đào tạo %>" DataField="NAME"
                                SortExpression="NAME" UniqueName="NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chương trình %>" DataField="TR_PROGRAM_GROUP_NAME"
                                SortExpression="TR_PROGRAM_GROUP_NAME" UniqueName="TR_PROGRAM_GROUP_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số lượng giảng viên %>" DataField="TEACHER_NUMBER"
                                SortExpression="TEACHER_NUMBER" UniqueName="TEACHER_NUMBER" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số lượng học viên %>" DataField="STUDENT_NUMBER"
                                SortExpression="STUDENT_NUMBER" UniqueName="STUDENT_NUMBER" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời lượng %>" DataField="DURATION"
                                SortExpression="DURATION" UniqueName="DURATION" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị thời lượng %>" DataField="TR_DURATION_UNIT_NAME"
                                SortExpression="TR_DURATION_UNIT_NAME" UniqueName="TR_DURATION_UNIT_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trung tâm đào tạo %>" DataField="Centers_NAME"
                                SortExpression="Centers_NAME" UniqueName="Centers_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị chủ trì đào tạo %>" DataField="UNIT_NAME"
                                SortExpression="UNIT_NAME" UniqueName="UNIT_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa điểm %>" DataField="VENUE" SortExpression="VENUE"
                                UniqueName="VENUE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng chi phí (VNĐ) %>" DataField="COST_TOTAL"
                                SortExpression="COST_TOTAL" DataFormatString="{0:N0}" UniqueName="COST_TOTAL" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí 1 học viên (VNĐ) %>" DataField="COST_OF_STUDENT"
                                SortExpression="COST_OF_STUDENT" DataFormatString="{0:N0}" UniqueName="COST_OF_STUDENT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng chi phí (USD) %>" DataField="COST_TOTAL_USD"
                                SortExpression="COST_TOTAL_USD" DataFormatString="{0:N0}" UniqueName="COST_TOTAL_USD" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí 1 học viên (USD) %>" DataField="COST_OF_STUDENT_USD"
                                SortExpression="COST_OF_STUDENT_USD" DataFormatString="{0:N0}" UniqueName="COST_OF_STUDENT_USD" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị tham gia %>" DataField="Departments_NAME"
                                SortExpression="Departments_NAME" UniqueName="Departments_NAME">
                                <HeaderStyle Width="300px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="Titles_NAME"
                                SortExpression="Titles_NAME" UniqueName="Titles_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Công việc liên quan %>" DataField="Work_inv_NAME"
                                SortExpression="Work_inv_NAME" UniqueName="Work_inv_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />--%>
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
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
       
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {          
             //window.open('/Default.aspx?mid=Training&fid=ctrlTR_PlanNewEdit&group=Business&noscroll=1', "_self");
            //var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_PlanNewEdit&group=Business&noscroll=1', "rwPopup");
            //var pos = $("html").offset();
            //oWindow.moveTo(pos.left, pos.top);
            //oWindow.setSize($(window).width(), $(window).height());
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;

            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Training&fid=ctrlTR_PlanNewEdit&group=Business&noscroll=1&ID=' + id, "_self"); 
            //var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_PlanNewEdit&group=Business&noscroll=1&ID=' + id, "rwPopup");
            //var pos = $("html").offset();
            //oWindow.moveTo(pos.left, pos.top);
            //oWindow.setSize($(window).width(), $(window).height());
            return 2;
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            //if (args.get_item().get_commandName() == 'CREATE') {
            //    OpenNew();
            //    args.set_cancel(true);
            //}
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
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == "EXPORT_TEMPLATE") {
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
        function gridRowDblClick(sender, eventArgs) {
            OpenEdit();
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
