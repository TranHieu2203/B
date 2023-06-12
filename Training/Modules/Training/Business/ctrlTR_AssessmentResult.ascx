<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_AssessmentResult.ascx.vb"
    Inherits="Training.ctrlTR_AssessmentResult" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="LeftPane" runat="server" Height="200px">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Năm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" MinValue="1900" MaxLength="2999" AutoPostBack="true"
                        CausesValidation="false">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chương trình đào tạo")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCourse" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td style="width: 100px"></td>
                <td style="width: 200px"></td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trung tâm")%>
                </td>
                <td colspan="6">
                    <tlk:RadTextBox runat="server" ID="txtCenters" Width="100%" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giảng viên")%>
                </td>
                <td colspan="6">
                    <tlk:RadTextBox runat="server" ID="txtLectures" Width="100%" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEndDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nơi đào tạo")%>
                </td>
                <td colspan="6">
                    <tlk:RadTextBox runat="server" ID="txtAddress" Width="100%" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane11" runat="server" Scrolling="None" Height="30px">
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6" style="width: 50%; border-right: 2px,solid, #ff0000">
                    <%# Translate("Danh sách nhân viên")%>
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
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true"
                    CellSpacing="0" GridLines="None">
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="EMPLOYEE_ID,IS_LOCK,EMPLOYEE_CODE,ASSESMENT_ID">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="EMPLOYEE_CODE" HeaderText="<%$ Translate: Mã nhân viên %>"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="70px" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_NAME" HeaderText="<%$ Translate: Tên nhân viên%>"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn DataField="ORG_NAME"
                                HeaderText="<%$ Translate: Phòng ban%>" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn DataField="TITLE_NAME"
                                HeaderText="<%$ Translate: Chức danh%>" SortExpression="TITLE_NAME"
                                UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn DataField="STATUS"
                                HeaderText="<%$ Translate: Trạng thái %>" SortExpression="STATUS"
                                UniqueName="STATUS" />
                            <tlk:GridBoundColumn DataField="IS_LOCK_TEXT"
                                HeaderText="<%$ Translate: Đã khóa%>" SortExpression="IS_LOCK_TEXT"
                                UniqueName="IS_LOCK_TEXT"  HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </tlk:RadGrid>
            </tlk:RadPane>
            <%--   <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Width="10px">
            </tlk:RadPane>--%>
            <tlk:RadPane ID="RadPane3" runat="server">
                <table class="table-form">
                    <tr>
                        <td>
                            <tlk:RadGrid ID="rgResult" runat="server" Height="190px" PageSize="50" AllowPaging="true"
                                CellSpacing="0" GridLines="None" AllowSorting="false" AllowMultiRowSelection="false"
                                AllowMultiRowEdit="true">
                                <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,POINT_ASS,REMARK,CRI_COURSE_ID,TR_CRITERIA_ID,TR_CRITERIA_POINT_MAX"
                                    EditMode="InPlace">
                                    <%--      <GroupByExpressions>
                                        <tlk:GridGroupByExpression>
                                            <SelectFields>
                                                <tlk:GridGroupByField FieldName="TR_CRITERIA_GROUP_NAME" HeaderText="<%$ Translate: Nhóm tiêu chí %>">
                                                </tlk:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <tlk:GridGroupByField FieldName="TR_CRITERIA_GROUP_NAME" SortOrder="Ascending"></tlk:GridGroupByField>
                                            </GroupByFields>
                                        </tlk:GridGroupByExpression>
                                    </GroupByExpressions>--%>
                                    <Columns>
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tiêu chí %>" DataField="TR_CRITERIA_CODE"
                                            UniqueName="TR_CRITERIA_CODE" SortExpression="TR_CRITERIA_CODE" ReadOnly="true"
                                            HeaderStyle-Width="15%" />
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiêu chí %>" DataField="TR_CRITERIA_NAME"
                                            UniqueName="TR_CRITERIA_NAME" SortExpression="TR_CRITERIA_NAME" ReadOnly="true" />
                                        <tlk:GridNumericColumn HeaderText="<%$ Translate: Tỷ trọng %>" DataField="TR_CRITERIA_RATIO"
                                            UniqueName="TR_CRITERIA_RATIO" SortExpression="TR_CRITERIA_RATIO" DataFormatString="{0:n2}"
                                            ReadOnly="true" HeaderStyle-Width="15%" />
                                        <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức độ hữu ích %>" DataField="TR_CRITERIA_POINT_MAX"
                                            UniqueName="TR_CRITERIA_POINT_MAX" SortExpression="TR_CRITERIA_POINT_MAX" DataFormatString="{0:n0}"
                                            ReadOnly="true" HeaderStyle-Width="15%" />
                                        <tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm đánh giá %>" DataField="POINT_ASS"
                                            UniqueName="POINT_ASS" SortExpression="POINT_ASS" DataFormatString="{0:n0}" HeaderStyle-Width="15%" />
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ý kiến chung %>" DataField="REMARK" UniqueName="REMARK"
                                            SortExpression="REMARK" />
                                    </Columns>
                                </MasterTableView>
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                            </tlk:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <%# Translate("Điều bạn tâm đắc và ứng dụng kiến thúc trong khóa học vào công việc gì?")%>
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
                            <%# Translate("Với người dẫn dắt khóa học này, anh/chị có thể chia sẻ hoặc đóng góp để giúp chúng tôi cải thiện hơn.")%>
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
                            <%# Translate("Với khóa học này, để có trải nghiệm người học tuyệt vời hơn nữa, bạn mong muốn.")%>
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


        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }

        function clientButtonClicking(sender, args) {

            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == 'PRINT_STUDENT') {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == 'PRINT_COMPLETE') {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadCodeBlock>
