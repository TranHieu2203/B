<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ProgramResult.ascx.vb"
    Inherits="Training.ctrlTR_ProgramResult" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidProgramID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="170px" Scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chương trình đào tạo")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã khóa đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCourseCode" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Khóa đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCourseName" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                
                <td class="lb">
                    <%# Translate("Loại đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCourseType">
                    </tlk:RadTextBox>
                </td>
                <td rowspan="2" style="padding-left:50px">
                    Cách thức cập nhật kết quả theo Loại đào tạo:<br />
                    - Loại "Tham gia": Nhập cột Kết quả đạt?<br />
                    - Loại "Thi xếp loại": Nhập 3 cột: Điểm, Kết quả đạt?, Xếp loại<br />
                    - Loại "Đánh giá": Nhập 3 cột: Đánh giá đến hạn 1, 2, 3<br />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thời gian tổ chức đào tạo từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdToDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên bằng cấp/chứng chỉ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCERTIFICATE_NAME" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>

            <tr style="display:none">
                <td class="lb">
                    <%# Translate("Tên chương trình")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtProgramName" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="display:none">
                <td class="lb">
                    <%# Translate("Hình thức")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtHinhThuc">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Kết quả đào tạo")%>
                    </b>
                </td>
            </tr>
            
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowMultiRowEdit="true" AllowSorting="false">
            <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,TR_RANK_ID,INSERT_HSNV" ClientDataKeyNames="ID,EMPLOYEE_ID,TR_RANK_ID,INSERT_HSNV" EditMode="InPlace">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã học viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" ReadOnly="true" HeaderStyle-Width="70px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                        ReadOnly="true" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" ReadOnly="true" HeaderStyle-Width="150px" />
                        
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ReadOnly="true" HeaderStyle-Width="150px" />
                    <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Thời lượng học thực tế  %>" DataField="DURATION"
                        UniqueName="DURATION" SortExpression="DURATION" DataFormatString="{0:n0}" HeaderStyle-Width="70px"
                        ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Tham dự cuối khóa %>" DataField="IS_END"
                        UniqueName="IS_END" SortExpression="IS_END" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Có dự thi %>" DataField="IS_EXAMS"
                        UniqueName="IS_EXAMS" SortExpression="IS_EXAMS" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />--%>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Kết quả đạt? %>" DataField="IS_REACH"
                        UniqueName="IS_REACH" SortExpression="IS_REACH" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />
                    <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm chuẩn TOEIC %>" DataField="TOIEC_BENCHMARK"
                        UniqueName="TOIEC_BENCHMARK" SortExpression="TOIEC_BENCHMARK" DataFormatString="{0:n0}"
                        HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" />--%>
                    <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm đầu vào %>" DataField="TOIEC_SCORE_IN"
                        UniqueName="TOIEC_SCORE_IN" SortExpression="TOIEC_SCORE_IN" DataFormatString="{0:n0}"
                        HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm đầu ra %>" DataField="TOIEC_SCORE_OUT"
                        UniqueName="TOIEC_SCORE_OUT" SortExpression="TOIEC_SCORE_OUT" DataFormatString="{0:n0}"
                        HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm tiến bộ %>" DataField="INCREMENT_SCORE"
                        UniqueName="INCREMENT_SCORE" SortExpression="INCREMENT_SCORE" DataFormatString="{0:n0}"
                        HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />--%>
                    <tlk:GridTemplateColumn UniqueName="TR_RANK_NAME" HeaderText="<%$ Translate: Xếp loại %>"
                        SortExpression="TR_RANK_NAME" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "TR_RANK_NAME")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <tlk:RadComboBox runat="server" ID="cboRank" Width="100px">
                            </tlk:RadComboBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm thi lại %>" DataField="RETEST_SCORE"
                        UniqueName="RETEST_SCORE" SortExpression="RETEST_SCORE" DataFormatString="{0:n0}"
                        HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Right" />--%>
                    <%--<tlk:GridTemplateColumn UniqueName="RETEST_RANK_ID" HeaderText="<%$ Translate: Xếp loại thi lại %>"
                        SortExpression="RETEST_RANK_ID" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "RETEST_RANK_ID")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <tlk:RadComboBox runat="server" ID="cboRank2" Width="100px">
                            </tlk:RadComboBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>--%>
                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú thi lại %>" DataField="RETEST_REMARK"
                        UniqueName="RETEST_REMARK" SortExpression="RETEST_REMARK" />--%>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm số %>" DataField="FINAL_SCORE"
                        UniqueName="FINAL_SCORE" SortExpression="FINAL_SCORE" DataFormatString="{0:n2}"
                        HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Right" />

                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Đánh giá đến hạn 1 %>" DataField="COMMENT_1"
                        UniqueName="COMMENT_1" SortExpression="COMMENT_1" HeaderStyle-Width="150px" />
                        
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Đánh giá đến hạn 2 %>" DataField="COMMENT_2"
                        UniqueName="COMMENT_2" SortExpression="COMMENT_2" HeaderStyle-Width="150px" />
                        
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Đánh giá đến hạn 3 %>" DataField="COMMENT_3"
                        UniqueName="COMMENT_3" SortExpression="COMMENT_3" HeaderStyle-Width="150px" />
                    <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Vắng mặt có lý do %>" DataField="ABSENT_REASON"
                        UniqueName="ABSENT_REASON" SortExpression="ABSENT_REASON" HeaderStyle-Width="70px"
                        ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Vắng mặt không lý do %>" DataField="ABSENT_UNREASON"
                        UniqueName="ABSENT_UNREASON" SortExpression="ABSENT_UNREASON" HeaderStyle-Width="70px"
                        ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đã nhận chứng chỉ %>" DataField="IS_CERTIFICATE"
                        UniqueName="IS_CERTIFICATE" SortExpression="IS_CERTIFICATE" HeaderStyle-Width="70px"
                        ItemStyle-HorizontalAlign="Center" />--%>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp chứng nhận %>" DataField="CERT_DATE"
                        UniqueName="CERT_DATE" SortExpression="CERT_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        HeaderStyle-Width="120px" />
                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Số chứng chỉ %>" DataField="CERTIFICATE_NO"
                        UniqueName="CERTIFICATE_NO" SortExpression="CERTIFICATE_NO" HeaderStyle-Width="100px" />--%>
                    <%--<tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày nhận chứng chỉ %>" DataField="CER_RECEIVE_DATE"
                        UniqueName="CER_RECEIVE_DATE" SortExpression="CER_RECEIVE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        HeaderStyle-Width="120px" />--%>
                    <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Số cam kết đào tạo %>" DataField="COMMIT_NO"
                        UniqueName="COMMIT_NO" SortExpression="COMMIT_NO" ReadOnly="true" HeaderStyle-Width="70px"
                        ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày bắt đầu cam kết %>" DataField="COMMIT_STARTDATE"
                        UniqueName="COMMIT_STARTDATE" SortExpression="COMMIT_STARTDATE" DataFormatString="{0:dd/MM/yyyy}"
                        HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc cam kết %>" DataField="COMMIT_ENDDATE"
                        UniqueName="COMMIT_ENDDATE" SortExpression="COMMIT_ENDDATE" DataFormatString="{0:dd/MM/yyyy}"
                        HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Số tháng cam kết làm việc %>" DataField="COMMIT_WORKMONTH"
                        UniqueName="COMMIT_WORKMONTH" SortExpression="COMMIT_WORKMONTH" HeaderStyle-Width="100px"
                        ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Bồi hoàn lệ phí đào tạo? %>" DataField="IS_REFUND_FEE"
                        UniqueName="IS_REFUND_FEE" SortExpression="IS_REFUND_FEE" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="70px" />--%>
                    <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Trừ lương? %>" DataField="IS_RESERVES"
                        UniqueName="IS_RESERVES" SortExpression="IS_RESERVES" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="70px" />--%>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" />
                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Đã cấp chứng nhận %>" DataField="INSERT_HSNV_STATUS"
                        ReadOnly="true" HeaderStyle-Width="150px" />--%>
                     <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đã cấp chứng nhận %>" DataField="INSERT_HSNV_STATUS"
                        UniqueName="INSERT_HSNV_STATUS" SortExpression="INSERT_HSNV_STATUS" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="70px"  ReadOnly = "true"/>
                    <tlk:GridTemplateColumn DataField="ATTACH_FILE" UniqueName="ATTACH_FILE" HeaderText="<%$ Translate: File đính kèm %>"
                        SortExpression="ATTACH_FILE" Visible="false">
                        <ItemTemplate>
                            <%--<%# Eval("ATTACH_FILE") %>--%>
                            <%--<asp:HyperLink ID="AttachFile" runat="server"></asp:HyperLink>--%>
                            <%--<tlk:RadTextBox ID="_FileName" runat="server" CausesValidation="false" Text='<%# Eval("ATTACH_FILE") %>'
                                ReadOnly="true" BorderStyle="None" /></tlk:RadButton>--%>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="DownloadFile" CommandArgument='<%# Eval("ATTACH_FILE") %>'
                                Font-Underline="true"><%# Eval("ATTACH_FILE") %></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <tlk:RadTextBox ID="_FileName" runat="server" CausesValidation="false" Width="50%"
                                Text='<%# Eval("ATTACH_FILE") %>' ReadOnly="true" />
                            <tlk:RadButton runat="server" Text="Upload" ID="btnUpload" CommandName="UploadFile"
                                CommandArgument='<%# Eval("EMPLOYEE_ID") %>'>
                            </tlk:RadButton>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="150px" />
            <ClientSettings EnablePostBackOnRowClick="true">
                <%--<Scrolling FrozenColumnsCount="4" />--%>
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </tlk:RadGrid>
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
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
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

        function OpenNew() {
            debugger;
            var listID = '';
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            var masterTable = $find("<%= rgData.ClientID %>").get_masterTableView();
            var row = masterTable.get_selectedItems();
            for (var i = 0; i < row.length; i++) {
                var Insert_HSNV = masterTable.get_selectedItems()[i].getDataKeyValue('INSERT_HSNV');
                if (Insert_HSNV == 1)
                    return 1;
                var CheckID = masterTable.get_selectedItems()[i].getDataKeyValue('ID');
                if (CheckID == null)
                    return 3;
                listID = listID + masterTable.get_selectedItems()[i].getDataKeyValue('ID') + ',';
            }
            var ProgramID = $get("<%= hidProgramID.ClientID %>").value;
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ConfirmCertificate&group=Business&PROGRAM_ID=' + ProgramID + '&LIST_ID=' + listID + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            oWindow.center();
            return 2;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "SEND_TRAINING_TO_PROFILE_ENABLE") {
                bCheck = OpenNew();
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%# Translate("Tồn tại nhân viên đã được cập nhật thông tin sang HSNV") %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 3) {
                    m = '<%# Translate("Vui lòng cập nhật kết quả cho học viên trước khi cấp chứng nhận") %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                args.set_cancel(true);
            }
        }

        function OnClientClose(oWnd, args) {
            $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
        }

    </script>
</tlk:RadCodeBlock>
