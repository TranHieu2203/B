<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_EvaluateCourse.ascx.vb"
    Inherits="Training.ctrlTR_EvaluateCourse
    " %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidValid" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" >
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane11" runat="server" Scrolling="None" Height="30px">
                <table class="table-form">
                    <tr>
                        <td class="item-head" colspan="6" style="width: 50%; border-right: 2px,solid, #ff0000">
                            <%# Translate("Danh sách khóa đào tạo đăng ký")%>
                        </td>
                        <td style="min-width: 5px"></td>
                        <td class="item-head" colspan="6" style="width: 30%">
                            <%# Translate("Kết quả đánh giá")%>
                        </td>

                        <td class="item-head" colspan="6" style="width: 10%">
                            <%# Translate("Điểm trung bình")%>
                        </td>
                        <td class="item-head" colspan="6" style="width: 10%">
                            <tlk:RadTextBox runat="server" ID="txtDTB" Width="100%" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>

            <tlk:RadPane ID="RadPane5" runat="server" Scrolling="None">
                <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
                    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                        <tlk:RadGrid ID="rgCourse" runat="server" Height="100%" Width="100%" AllowPaging="true"
                            AllowMultiRowSelection="false">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                                <ClientEvents OnRowDblClick="gridRowDblClick" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID,IS_LOCK,ASSESMENT_ID" ClientDataKeyNames="ID">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã khóa đào tạo %>" DataField="TR_PROGRAM_CODE"
                                        SortExpression="TR_PROGRAM_CODE" UniqueName="TR_PROGRAM_CODE" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" SortExpression="YEAR"
                                        UniqueName="YEAR" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo %>" DataField="TR_COURSE_NAME"
                                        SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="TRAIN_FIELD"
                                        SortExpression="TRAIN_FIELD" UniqueName="TRAIN_FIELD" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="TRAIN_FORM_NAME"
                                        SortExpression="TRAIN_FORM_NAME" UniqueName="TRAIN_FORM_NAME" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tính chất nhu cầu %>" DataField="PROPERTIES_NEED_NAME"
                                        SortExpression="PROPERTIES_NEED_NAME" UniqueName="PROPERTIES_NEED_NAME" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung đào tạo %>" DataField="CONTENT"
                                        SortExpression="CONTENT" UniqueName="CONTENT" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại đào tạo %>" DataField="TR_TYPE_NAME"
                                        SortExpression="TR_TYPE_NAME" UniqueName="TR_TYPE_NAME" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="START_DATE"
                                        SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="END_DATE"
                                        SortExpression="END_DATE" UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số lượng học viên %>" DataField="STUDENT_NUMBER"
                                        SortExpression="STUDENT_NUMBER" UniqueName="STUDENT_NUMBER" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi phí đào tạo %>" DataField="COST_TOTAL"
                                        SortExpression="COST_TOTAL" UniqueName="COST_TOTAL" DataFormatString="{0:N0}" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trung tâm đào tạo %>" DataField="Centers_NAME"
                                        SortExpression="Centers_NAME" UniqueName="Centers_NAME" Visible="false">
                                    </tlk:GridBoundColumn>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi đào tạo %>" DataField="VENUE" SortExpression="VENUE"
                                        UniqueName="VENUE" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                        UniqueName="REMARK" Visible="false" />
                                    <tlk:GridBoundColumn DataField="STATUS_ASSESMENT"
                                        HeaderText="<%$ Translate: Trạng thái %>" SortExpression="STATUS_ASSESMENT"
                                        UniqueName="STATUS_ASSESMENT" />
                                    <tlk:GridBoundColumn DataField="IS_LOCK_TEXT"
                                        HeaderText="<%$ Translate: Đã khóa%>" SortExpression="IS_LOCK_TEXT"
                                        UniqueName="IS_LOCK_TEXT"  HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
                    </tlk:RadPane>

                    <tlk:RadPane ID="RadPane6" runat="server" Scrolling="None" Width="5px">
                    </tlk:RadPane>

                    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="Y" Width="100%" Height="100%">
                        <%--<tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">--%>
                        <tlk:RadGrid ID="rgEvaluate" runat="server" Height="250px"  Width="100%" AllowPaging="true"
                            AllowMultiRowSelection="false" AllowMultiRowEdit="true">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                                <ClientEvents OnRowDblClick="gridRowDblClick" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,POINT_ASS,REMARK,CRI_COURSE_ID,TR_CRITERIA_ID,TR_CRITERIA_POINT_MAX"
                                EditMode="InPlace">
                                <Columns>
                                    <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                                </tlk:GridClientSelectColumn>--%>
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tiêu chí %>" DataField="TR_CRITERIA_CODE"
                                        UniqueName="TR_CRITERIA_CODE" SortExpression="TR_CRITERIA_CODE" ReadOnly="true" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiêu chí %>" DataField="TR_CRITERIA_NAME"
                                        UniqueName="TR_CRITERIA_NAME" SortExpression="TR_CRITERIA_NAME" ReadOnly="true" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tỷ trọng %>" DataField="TR_CRITERIA_RATIO"
                                        UniqueName="TR_CRITERIA_RATIO" SortExpression="TR_CRITERIA_RATIO" DataFormatString="{0:n2}"
                                        ReadOnly="true" HeaderStyle-Width="50px" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức độ hữu ích %>" DataField="TR_CRITERIA_POINT_MAX"
                                        UniqueName="TR_CRITERIA_POINT_MAX" SortExpression="TR_CRITERIA_POINT_MAX" DataFormatString="{0:n0}"
                                        ReadOnly="true" />
                                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm đánh giá %>" DataField="POINT_ASS"
                                        UniqueName="POINT_ASS" SortExpression="POINT_ASS" DataFormatString="{0:n0}" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ý kiến chung %>" DataField="REMARK" UniqueName="REMARK"
                                        SortExpression="REMARK" />
                                </Columns>

                            </MasterTableView>
                        </tlk:RadGrid>
                           <%-- </tlk:RadPane>--%>
                        <tlk:RadSplitter ID="RadSplitter5" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
                            <tlk:RadPane ID="RadPane7" runat="server" Scrolling="None" Width="100%">
                                <table class="table-form">
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <%# Translate("Điều bạn tâm đắc và ứng dụng kiến thúc trong khóa học vào công việc gì?")%><span class="lbReq">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <tlk:RadTextBox runat="server" ID="txtNote1" Width="100%">
                                            </tlk:RadTextBox>
                                            <asp:RequiredFieldValidator ID="reqNote1" ControlToValidate="txtNote1"
                                                runat="server" ErrorMessage="<%$ Translate: Không được để trống %>" ToolTip="<%$ Translate: Không được để trống %>">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <%# Translate("Với người dẫn dắt khóa học này, anh/chị có thể chia sẻ hoặc đóng góp để giúp chúng tôi cải thiện hơn.")%><span class="lbReq">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <tlk:RadTextBox runat="server" ID="txtNote2" Width="100%">
                                            </tlk:RadTextBox>
                                            <asp:RequiredFieldValidator ID="reqNote2" ControlToValidate="txtNote2"
                                                runat="server" ErrorMessage="<%$ Translate: Không được để trống %>" ToolTip="<%$ Translate: Không được để trống %>">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <%# Translate("Với khóa học này, để có trải nghiệm người học tuyệt vời hơn nữa, bạn mong muốn.")%><span class="lbReq">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <tlk:RadTextBox runat="server" ID="txtNote3" Width="100%">
                                            </tlk:RadTextBox>
                                            <asp:RequiredFieldValidator ID="reqNote3" ControlToValidate="txtNote3"
                                                runat="server" ErrorMessage="<%$ Translate: Không được để trống %>" ToolTip="<%$ Translate: Không được để trống %>">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <%# Translate("Ý kiến khác.")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <tlk:RadTextBox runat="server" ID="txtNote4" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </tlk:RadPane>
                        </tlk:RadSplitter>

                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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
         var winH;
        var winW;

         function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() - 210;
            winW = $(window).width() - 90;
            $("#ctl00_MainContent_ctrlTR_EvaluateCourse_RadSplitter1").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlTR_EvaluateCourse_RadSplitter3").stop().animate({ height: winH, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlTR_EvaluateCourse_MainPane").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlTR_EvaluateCourse_rgCourse").stop().animate({ height: winH-76 }, 0);
             $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlTR_EvaluateCourse_RadPane5").stop().animate({ height: winH - 76 }, 0);
             $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlTR_EvaluateCourse_RadPane3").stop().animate({ height: winH - 76 }, 0);
             $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlTR_EvaluateCourse_RadPane2").stop().animate({ height: winH-76 }, 0);
             $("#RAD_SPLITTER_ctl00_MainContent_ctrlTR_EvaluateCourse_RadSplitter3").stop().animate({ height: winH }, 0);
             $("#ctl00_MainContent_ctrlTR_EvaluateCourse_rgEvaluate").stop().animate({ height: winH-250 }, 0);
             $("#ctl00_MainContent_ctrlTR_EvaluateCourse_rgEvaluate_GridData").stop().animate({ height: winH -330}, 0);
             
            //
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
