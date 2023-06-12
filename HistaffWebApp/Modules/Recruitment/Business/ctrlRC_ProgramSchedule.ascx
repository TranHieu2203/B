<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramSchedule.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramSchedule" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="150px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:HiddenField ID="hidOrg" runat="server" />
        <asp:HiddenField ID="hidTitle" runat="server" />
        <asp:HiddenField ID="hidProgramID" runat="server" />
        <asp:Panel ID="Panel1" runat="server">
            <table class="table-form padding-10">
                <tr>
                    <td class="item-head" colspan="8">
                    
                            <%# Translate("Thông tin chương trình tuyển dụng")%>
                   
                        <hr />
                    </td>
                </tr>
                <tr>
                        <td class="lb">
                            <%# Translate("Mã YCTD")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblCode" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Vị trí tuyển dụng")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblTitleName" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Phòng ban tuyển dụng")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblOrgName" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        
                        
                        <td class="lb" style="display : none">
                            <%# Translate("Tên công việc")%>:
                        </td>
                        <td style="display : none">
                            <asp:Label ID="lblJobName" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb" style="display : none">
                            <%# Translate("Yêu cầu kinh nghiệm")%>:
                        </td>
                        <td style="display : none">
                            <asp:Label ID="lblExperienceRequired" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="lb">
                            <%# Translate("Ngày YCTD")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblSendDate" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày đáp ứng")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblRespone" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb">
                            <%# Translate("Số lượng cần tuyển")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblRequestNumber" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="lb" >
                            <%# Translate("Hồ sơ đã nhận")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblprofileReceive" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb" >
                            <%# Translate("Số lượng đã tuyển")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblQuantityHasRecruitment" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb" style="display : none">
                            <%# Translate("Trạng thái yêu cầu")%>:
                        </td>
                        <td style="display : none">
                            <asp:Label ID="lblStatusRequest" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="lb" style="display : none">
                            <%# Translate("Lý do tuyển dụng")%>:
                        </td>
                        <td style="display : none">
                            <asp:Label ID="lblReasonRecruitment" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td >
                        <td class="lb" style="display : none">
                            <%# Translate("Yêu cầu khác")%>:
                        </td>
                        <td colspan="3" style="display : none">
                            <asp:Label ID="lblOtherRequest" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
            </table>
        </asp:Panel>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
            <MasterTableView DataKeyNames="ID,CANDIDATE_COUNT" ClientDataKeyNames="ID,CANDIDATE_COUNT">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thi %>" DataField="SCHEDULE_DATE" HeaderStyle-Width="120px"
                        UniqueName="SCHEDULE_DATE" SortExpression="SCHEDULE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian thực hiện%>" DataField="TIME"
                        UniqueName="TIME" SortExpression="TIME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vòng tuyển dụng%>" DataField="EXAMS_NAME"
                        UniqueName="EXAMS_NAME" SortExpression="EXAMS_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: SL UV đã lên lịch %>" DataField="CANDIDATE_COUNT" HeaderStyle-Width="200px"
                        UniqueName="CANDIDATE_COUNT" SortExpression="CANDIDATE_COUNT" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên ứng viên %>" DataField="NAMES"
                                SortExpression="NAMES" UniqueName="NAMES">
                                <HeaderStyle Width="200px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phỏng vấn %>" DataField="EMPLOYEE_NAME" HeaderStyle-Width="200px"
                        UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Địa điểm phỏng vấn %>" DataField="EXAMS_PLACE" HeaderStyle-Width="200px"
                        UniqueName="EXAMS_PLACE" SortExpression="EXAMS_PLACE" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" HeaderStyle-Width="200px"
                        UniqueName="NOTE" SortExpression="NOTE" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Height="50px" Scrolling="None">
        <table class="table-form" style="margin-left: auto; margin-right: auto; display: table;">
            <tr>

                <td >
                    <tlk:RadButton CausesValidation="false" OnClientClicked="OpenEditTransfer"  ID="btnDSPV" runat="server" Text="<%$ Translate: Danh sách phỏng vấn %>"
                        >
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton CausesValidation="false" OnClientClicked="OpenEditTransfer"  ID="btnEmailNPV" runat="server" Text="<%$ Translate: Gửi Email NPV %>"
                       >
                    </tlk:RadButton>
                </td>
                <td >
                    <tlk:RadButton CausesValidation="false" OnClientClicked="OpenEditTransfer"  ID="btnEmail1" runat="server" Text="<%$ Translate: Gửi Email 1 %>">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton CausesValidation="false" OnClientClicked="OpenEditTransfer"  ID="btnEmail2" runat="server" Text="<%$ Translate: Gửi Email 2 %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            OnClientBeforeClose="OnClientBeforeClose" Modal="true" ShowContentDuringLoad="false"
            Title="<%$ Translate: Thông tin lên lịch thi tuyển %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        function OpenEditWindow(states) {
            var gUId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            var programID = $get("<%=hidProgramID.ClientID %>").value;

            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramScheduleNewEdit&group=Business&PROGRAM_ID='
            + programID + '&SCHEDULE_ID=' + gUId + '', "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */

        }

        function OnClientBeforeClose(sender, eventArgs) {
            var arg = eventArgs.get_argument();
            if (!arg) {
                if (!confirm("Bạn có muốn đóng màn hình không?")) {
                    //if cancel is clicked prevent the window from closing
                    eventArgs.set_cancel(true);
                }
            }
        }

        function OpenInsertWindow() {
            var programID = $get("<%=hidProgramID.ClientID %>").value;
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_ProgramScheduleNewEdit&group=Business&PROGRAM_ID='
            + programID + '', "_self"); /*
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
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow("Edit");
                    args.set_cancel(true);
                }

            } else if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'PRINT' || args.get_item().get_commandName() == 'UNLOCK' || args.get_item().get_commandName() == 'PREVIOUS') {
                enableAjax = false;
            }
        }
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenEditTransfer(sender, args) {
            enableAjax = false;
        }

        function OnClientClose(oWnd, args) {
            window.location.reload(true);
        }

        function postBack(url) {
            var ajaxManager = $find("<%# AjaxManagerId %>");
            ajaxManager.ajaxRequest(url); //Making ajax request with the argument
        }

    </script>
</tlk:RadScriptBlock>
