<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortal_TranningRecord.ascx.vb"
    Inherits="Training.ctrlPortal_TranningRecord
    " %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidValid" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" >
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" MaxLength="12" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdFromDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Từ ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Từ ngày %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" MaxLength="12" runat="server"
                                ToolTip="" Width="150px">
                            </tlk:RadDatePicker>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdToDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đến ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Đến ngày %>"> </asp:RequiredFieldValidator>--%>
                        </td>

                        <td class="lb">
                            <%# Translate("Khóa đào tạo")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboCourse">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <asp:Checkbox ID="chkTran" runat="server" Text="<%$ Translate: Khóa đào tạo có cam kết%>" CausesValidation="false">
                            </asp:Checkbox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
               <tlk:RadGrid ID="rgMain" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="EMPLOYEE_ID, EMPLOYEE_CODE" ClientDataKeyNames="EMPLOYEE_ID">
                        <Columns>
                            <%--   <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>--%>
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
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
                                UniqueName="IS_REACHED" SortExpression="IS_REACHED"  Visible="false"/>
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
<Common:ctrlMessageBoxTraining ID="ctrlMessageBoxTraining" runat="server" /> 
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function clientButtonClicking(sender, args) {

        }

        function gridRowDblClick(sender, args) {

        }
        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }

        }
        var winH;
        var winW;

        function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() - 210;
            winW = $(window).width() - 90;
            $("#ctl00_MainContent_ctrlPortal_TranningRecord_RadSplitter1").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlPortal_TranningRecord_RadSplitter3").stop().animate({ height: winH, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPortal_TranningRecord_MainPane").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlPortal_TranningRecord_rgMain").stop().animate({ height: winH-76, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPortal_TranningRecord_RadPane2").stop().animate({ height: winH, width: winW }, 0);
            
            Sys.Application.add_load(SizeToFitMain);
        }

        SizeToFitMain();

        $(document).ready(function () {
            SizeToFitMain();
        });
        $(window).resize(function () {
            SizeToFitMain();
        });
    </script>
</tlk:RadCodeBlock>
