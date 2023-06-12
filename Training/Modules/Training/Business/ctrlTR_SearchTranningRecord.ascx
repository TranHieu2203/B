<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_SearchTranningRecord.ascx.vb"
    Inherits="Training.ctrlTR_SearchTranningRecord" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane1" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" 
                    OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="66px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdFromDate">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdToDate">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtEmployee">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                           <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm kiếm %>">
                            </tlk:RadButton></td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="EMPLOYEE_ID, EMPLOYEE_CODE" ClientDataKeyNames="EMPLOYEE_ID">
                        <Columns>
                            <%--   <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>--%>
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân niên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="FULLNAME_VN"
                                SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME_VN"
                                SortExpression="TITLE_NAME_VN" UniqueName="TITLE_NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị/Bộ phận %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Công việc liên quan %>" DataField="WORK_INVOLVE"
                                SortExpression="WORK_INVOLVE" UniqueName="WORK_INVOLVE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo %>" DataField="TR_COURSE_NAME"
                                SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chương trình %>" DataField="TR_PROGRAM_NAME"
                                SortExpression="TR_PROGRAM_NAME" UniqueName="TR_PROGRAM_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chương trình %>" DataField="TR_PROGRAM_GROUP_NAME"
                                SortExpression="TR_PROGRAM_GROUP_NAME" UniqueName="TR_PROGRAM_GROUP_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="TR_TRAIN_FIELD_NAME"
                                SortExpression="TR_TRAIN_FIELD_NAME" UniqueName="TR_TRAIN_FIELD_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="TR_TRAIN_FORM_NAME"
                                SortExpression="TR_TRAIN_FORM_NAME" UniqueName="TR_TRAIN_FORM_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời lượng %>" DataField="DURATION"
                                SortExpression="DURATION" UniqueName="DURATION" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="START_DATE"
                                SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="END_DATE"
                                SortExpression="END_DATE" UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số buổi học trong giờ hành chính %>"
                                DataField="DURATION_HC" SortExpression="DURATION_HC" UniqueName="DURATION_HC" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số buổi học ngoài giờ hành chính %>"
                                DataField="DURATION_OT" SortExpression="DURATION_OT" UniqueName="DURATION_OT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số lượng học viên %>" DataField="NO_OF_STUDENT"
                                SortExpression="NO_OF_STUDENT" UniqueName="NO_OF_STUDENT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng chi phí thực tế (VNĐ) %>" DataField="COST_TOTAL"
                                SortExpression="COST_TOTAL" UniqueName="COST_TOTAL" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí 1 học viên (VNĐ) %>" DataField="COST_OF_STUDENT"
                                SortExpression="COST_OF_STUDENT" UniqueName="COST_OF_STUDENT" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng chi phí thực tế (USD) %>" DataField="COST_TOTAL_USD"
                                SortExpression="COST_TOTAL_USD" UniqueName="COST_TOTAL_USD" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí 1 học viên (USD) %>" DataField="COST_OF_STUDENT_USD"
                                SortExpression="COST_OF_STUDENT_USD" UniqueName="COST_OF_STUDENT_USD" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Có thi lại %>" DataField="IS_EXAMS"
                                SortExpression="IS_EXAMS" UniqueName="IS_EXAMS" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngôn ngữ %>" DataField="TR_LANGUAGE_NAME"
                                SortExpression="TR_LANGUAGE_NAME" UniqueName="TR_LANGUAGE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trung tâm đào tạo %>" DataField="TR_UNIT_NAME"
                                SortExpression="TR_UNIT_NAME" UniqueName="TR_UNIT_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung %>" DataField="CONTENT"
                                SortExpression="CONTENT" UniqueName="CONTENT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mục đích %>" DataField="TARGET_TRAIN"
                                SortExpression="TARGET_TRAIN" UniqueName="TARGET_TRAIN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa điểm tổ chức %>" DataField="VENUE"
                                SortExpression="VENUE" UniqueName="VENUE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm đào tạo %>" DataField="TOEIC_FINAL_SCORE"
                                SortExpression="TOEIC_FINAL_SCORE" UniqueName="TOEIC_FINAL_SCORE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="IS_REACH" SortExpression="IS_REACH"
                                UniqueName="IS_REACH" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp loại %>" DataField="RANK_NAME"
                                SortExpression="RANK_NAME" UniqueName="RANK_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Văn bằng/Chứng chỉ %>" DataField="CERTIFICATE_NO"
                                SortExpression="CERTIFICATE_NO" UniqueName="CERTIFICATE_NO" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời hạn chứng chỉ %>" DataField="CERTIFICATE_DATE"
                                SortExpression="CERTIFICATE_DATE" UniqueName="CERTIFICATE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày cấp chứng chỉ %>" DataField="CER_RECEIVE_DATE"
                                SortExpression="CER_RECEIVE_DATE" UniqueName="CER_RECEIVE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hạn chứng chỉ %>" DataField="CER_EXPIRED_DATE"
                                SortExpression="CER_EXPIRED_DATE" UniqueName="CER_EXPIRED_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số cam kết %>" DataField="COMITMENT_TRAIN_NO"
                                SortExpression="COMITMENT_TRAIN_NO" UniqueName="COMITMENT_TRAIN_NO" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian cam kết  %>" DataField="COMMIT_WORK"
                                SortExpression="COMMIT_WORK" UniqueName="COMMIT_WORK" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bắt đầu cam kết %>" DataField="COMITMENT_START_DATE"
                                SortExpression="COMITMENT_START_DATE" UniqueName="COMITMENT_START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc cam kết %>" DataField="COMITMENT_END_DATE"
                                SortExpression="COMITMENT_END_DATE" UniqueName="COMITMENT_END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
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
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_RequestNewEdit&group=Business&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_RequestNewEdit&group=Business&noscroll=1&ID=' + id, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
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