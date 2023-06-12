﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_TranningRecord.ascx.vb"
    Inherits="Training.ctrlTR_TranningRecord" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="66px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Khóa đào tạo")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboCourse">
                            </tlk:RadComboBox>
                        </td>
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
                    </tr>
                    <tr>
                        <td class="lb" style="width: 180px">
                            <asp:Label runat="server" ID="lbEmployee" Text="Mã NV"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployee" runat="server" Width="130px" AutoPostBack="true">
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <asp:Checkbox ID="chkIsTer" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc%>" CausesValidation="false">
                            </asp:Checkbox>
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
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo %>" DataField="TR_COURSE_NAME"
                                SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="FROM_DATE"
                                SortExpression="FROM_DATE" UniqueName="FROM_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="TO_DATE"
                                SortExpression="TO_DATE" UniqueName="TO_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung %>" DataField="CONTENT"
                                SortExpression="CONTENT" UniqueName="CONTENT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mục tiêu %>" DataField="TARGET_TRAIN"
                                SortExpression="TARGET_TRAIN" UniqueName="TARGET_TRAIN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi đào tạo %>" DataField="VENUE"
                                SortExpression="VENUE" UniqueName="VENUE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trung tâm đào tạo %>" DataField="Centers_NAME"
                                SortExpression="Centers_NAME" UniqueName="Centers_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="IS_REACH"
                                UniqueName="IS_REACH" SortExpression="IS_REACH"  />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="IS_REACHED"
                                UniqueName="IS_REACHED" SortExpression="IS_REACHED"   Visible="false"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm số %>" DataField="FINAL_SCORE"
                                SortExpression="FINAL_SCORE" UniqueName="FINAL_SCORE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp loại %>" DataField="TR_RANK_NAME"
                                SortExpression="TR_RANK_NAME" UniqueName="TR_RANK_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đánh giá đến hạn 1 %>" DataField="COMMENT_1"
                                SortExpression="COMMENT_1" UniqueName="COMMENT_1" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đánh giá đến hạn 2 %>" DataField="COMMENT_2"
                                SortExpression="COMMENT_2" UniqueName="COMMENT_2" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đánh giá đến hạn 3 %>" DataField="COMMENT_3"
                                SortExpression="COMMENT_3" UniqueName="COMMENT_3" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Bằng cấp/Chứng chỉ %>" DataField="CERTIFICATE_NAME"
                                SortExpression="CERTIFICATE_NAME" UniqueName="CERTIFICATE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày cấp bằng cấp chứng chỉ %>" DataField="CERTIFICATE_DATE"
                                SortExpression="CERTIFICATE_DATE" UniqueName="CERTIFICATE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số cam kết %>" DataField="COMMIT_NO"
                                SortExpression="COMMIT_NO" UniqueName="COMMIT_NO" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tiền cam kết %>" DataField="MONEY_COMMIT"
                                SortExpression="MONEY_COMMIT" UniqueName="MONEY_COMMIT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tháng cam kết %>" DataField="COMMIT_WORK"
                                SortExpression="COMMIT_WORK" UniqueName="COMMIT_WORK" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bắt đầu cam kết %>" DataField="COMMIT_START"
                                SortExpression="COMMIT_START" UniqueName="COMMIT_START" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc cam kết %>" DataField="COMMIT_END"
                                SortExpression="COMMIT_END" UniqueName="COMMIT_END" DataFormatString="{0:dd/MM/yyyy}" />
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
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
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