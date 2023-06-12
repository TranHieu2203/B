<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_Program.ascx.vb"
    Inherits="Recruitment.ctrlRC_Program" %>
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
            <tlk:RadPane ID="RadPane3" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
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
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdToDate"
                                ControlToCompare="rdFromDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"
                                ToolTip="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"></asp:CompareValidator>
                        </td>
                        <td class="lb" style="display:none">
                            <%# Translate("Đợt tuyển dụng")%>
                        </td>
                        <td style="display:none">
                            <tlk:RadComboBox ID="cboRecPeriod" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboStatus" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                        </td>
                         <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="lb" style="display:none">
                            <%# Translate("Loại tuyển dụng")%>
                        </td>
                        <td style="display:none">
                            <tlk:RadComboBox ID="cboRecType" runat="server">
                                <Items>
                                    <tlk:RadComboBoxItem Text="-- Tất cả --" Value="" Selected="true" />
                                    <tlk:RadComboBoxItem Text="Trong kế hoạch" Value="REC_TYPE1" />
                                    <tlk:RadComboBoxItem Text="Ngoài kế hoạch" Value="REC_TYPE2" />
                                </Items>
                            </tlk:RadComboBox>
                        </td>
                        <td>
                        </td>
                       
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowPaging="true" AllowSorting="true"  SkinID="GridSingleSelect">
                    <ClientSettings EnableRowHoverStyle="true" >
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,STATUS_ID,EXAMP_COUNT" FilterItemStyle-HorizontalAlign="Center">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã YCTD %>" DataField="CODE_YCTD"
                                SortExpression="CODE_YCTD" UniqueName="CODE_YCTD" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người yêu cầu %>" DataField="PER_REQUEST"
                                SortExpression="PER_REQUEST" UniqueName="PER_REQUEST" />


                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày gửi yêu cầu %>" DataField="SEND_DATE"
                                SortExpression="SEND_DATE" UniqueName="SEND_DATE" HeaderStyle-Width="100px" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày đáp ứng %>" DataField="EXPECTED_JOIN_DATE"
                                SortExpression="EXPECTED_JOIN_DATE" UniqueName="EXPECTED_JOIN_DATE" HeaderStyle-Width="120px" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tuyển dụng %>" DataField="CODE"
                                SortExpression="CODE" UniqueName="CODE" HeaderStyle-Width="150px"/>--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Công ty %>" DataField="ORG_NAME_CTY"
                                SortExpression="ORG_NAME_CTY" UniqueName="ORG_NAME_CTY" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ban/Phòng %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí tuyển dụng %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số lượng yêu cầu %>" DataField="REQUEST_NUMBER"
                                SortExpression="REQUEST_NUMBER" UniqueName="REQUEST_NUMBER" AllowFiltering="false"
                                HeaderStyle-Width="90px" />



                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Diễn giải chi tiết %>" DataField="RECRUIT_REASON_NAME"
                                SortExpression="RECRUIT_REASON_NAME" UniqueName="RECRUIT_REASON_NAME" />




                           <%-- <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: TNG Hỗ trợ %>" DataField="IS_SUPPORT"
                                UniqueName="IS_SUPPORT" SortExpression="IS_SUPPORT" HeaderStyle-Width="70px" />--%>
                             <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: TNG Hỗ trợ %>" DataField="SUPPORT_NAME"
                                SortExpression="SUPPORT_NAME" UniqueName="SUPPORT_NAME" />--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hồ sơ đã nhận %>" DataField="CANDIDATE_COUNT"
                                SortExpression="CANDIDATE_COUNT" UniqueName="CANDIDATE_COUNT" AllowFiltering="false"
                                HeaderStyle-Width="90px" />


                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số lượng đã tuyển %>" DataField="CANDIDATE_RECEIVED"
                                SortExpression="CANDIDATE_RECEIVED" UniqueName="CANDIDATE_RECEIVED" AllowFiltering="false"
                                HeaderStyle-Width="90px" />
                            <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng trong kế hoạch %>" DataField="CANDIDATE_REQUEST"
                                SortExpression="CANDIDATE_REQUEST" UniqueName="CANDIDATE_REQUEST" AllowFiltering="false"
                                HeaderStyle-Width="90px" />--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" HeaderStyle-Width="100px" />
                            <%-- <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Trong ngân sách? %>" DataField="IS_IN_PLAN"
                                UniqueName="IS_IN_PLAN" SortExpression="IS_IN_PLAN" HeaderStyle-Width="70px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày bắt đầu tuyển dụng %>" DataField="RECRUIT_START"
                                SortExpression="RECRUIT_START" UniqueName="RECRUIT_START" HeaderStyle-Width="100px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc nhận hồ sơ %>" DataField="RECEIVE_END"
                                SortExpression="RECEIVE_END" UniqueName="RECEIVE_END" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do tuyển dụng chi tiết %>" DataField="RECRUIT_REASON"
                                SortExpression="RECRUIT_REASON" UniqueName="RECRUIT_REASON" />--%>
                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="STAGE_ID" DataField="STAGE_ID" UniqueName="STAGE_ID"
                                SortExpression="STAGE_ID" Visible="false" />
                        </Columns>
                        <HeaderStyle Width="150px" />
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnExams" runat="server" Text="<%$ Translate: Tạo vòng phỏng vấn %>"
                                OnClientClicking="btnExamsClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnCandidate" runat="server" Text="<%$ Translate: Khai báo ứng viên %>"
                                OnClientClicking="btnCandidateClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        
                        <td>
                            <tlk:RadButton ID="btnSchedule" runat="server" Text="<%$ Translate: Lên lịch phỏng vấn %>"
                                OnClientClicking="btnScheduleClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnResult" runat="server" Text="<%$ Translate: Cập nhật kết quả %>"
                                OnClientClicking="btnResultClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnTransfer" runat="server" Text="<%$ Translate: Chuyển sang HSNS %>"
                                OnClientClicking="btnTransferClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
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
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_ProgrammNewEdit&group=Business', "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
        }
        function gridRowDblClick(sender, eventArgs) {
            OpenEdit();
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_ProgrammNewEdit&group=Business&ID=' + id, "_self"); /*
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

        function btnCandidateClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            var COUNT_EXAM = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EXAMP_COUNT');
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck > 1) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (COUNT_EXAM == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_EXISTS_EXAMS) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            OpenInNewTab('Default.aspx?mid=Recruitment&fid=ctrlRC_CandidateList&group=Business&PROGRAM_ID=' + id)
        }
        function btnExamsClick(sender, args) {

            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck > 1) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramExams&group=Business&PROGRAM_ID=' + id); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
        }

        function btnScheduleClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck > 1) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            OpenInNewTab('Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramSchedule&group=Business&PROGRAM_ID=' + id)
        }
        function btnResultClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck > 1) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramUResult&group=Business&PROGRAM_ID=' + id); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
        }
        function btnTransferClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck > 1) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_CandidateTransferList&group=Business&PROGRAM_ID=' + id); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
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
