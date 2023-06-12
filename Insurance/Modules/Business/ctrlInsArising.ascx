<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsArising.ascx.vb"
    Inherits="Insurance.ctrlInsArising" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:HiddenField runat="server" ID="hidID" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="300" Width="300px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="200px">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tháng báo BH")%>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker runat="server" ID="txtMONTH" TabIndex="5" Culture="en-US">
                                <DateInput ID="DateInput3" runat="server" DisplayDateFormat="MM/yyyy"></DateInput>
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại biến động")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox DataValueField="ID" DataTextField="NAME" ID="ddlINS_ARISING_TYPE_ID"
                                runat="server" TabIndex="2" ToolTip="<%$ Translate: Tên loại biến động khai báo %>"
                                AutoPostBack="true" EmptyMessage="<%$ Translate: Chọn Tên loại biến động khai báo %>">
                            </tlk:RadComboBox>
                            <%--<asp:RequiredFieldValidator ID="reqddlINS_ARISING_TYPE_ID" ControlToValidate="ddlINS_ARISING_TYPE_ID"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Loại biến động. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Loại biến động. %>"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="item-head">
                            <%# Translate("Thông tin tìm kiếm")%>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ tháng")%>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker runat="server" ID="txtFROMDATE" TabIndex="4" Culture="en-US">
                                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="MM/yyyy">
                                </DateInput>
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đơn vị BH")%>
                        </td>
                        <td>
                            <tlk:RadComboBox DataValueField="ID" DataTextField="NAME" ID="ddlINSORG" runat="server"
                                TabIndex="2" ToolTip="<%$ Translate: Đơn vị BH %>" AutoPostBack="false" EmptyMessage="<%$ Translate: Đơn vị BH %>">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Đến tháng")%>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker runat="server" ID="txtTODATE" TabIndex="5" Culture="en-US">
                                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="MM/yyyy"></DateInput>
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Nhóm biến động")%>
                        </td>
                        <td>
                            <tlk:RadComboBox DataValueField="CODE" DataTextField="NAME" ID="ddlGROUP_ARISING_TYPE_ID"
                                runat="server" TabIndex="3" EmptyMessage="<%$ Translate: Nhóm biến động %>" CheckBoxes="false"
                                EnableCheckAllItemsCheckBox="false" DropDownAutoWidth="Enabled" Height="100px">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb"></td>
                        <td>
                            <tlk:RadButton ID="btnFind" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                        <td></td>
                        <td style="display:none">
                            <tlk:RadButton ID="btnCreate" runat="server" Text="<%$ Translate: Tạo DL %>" SkinID="Button" Width="90px">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgGridData" runat="server" Height="100%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true" AllowFilteringByColumn="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="5" />
                        <Selecting AllowRowSelect="true" />
                        <Scrolling UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                    <MasterTableView TableLayout="Fixed" CommandItemDisplay="None" DataKeyNames="ID"
                        ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPID,FULL_NAME,DEP_NAME,TITLE_NAME,EFFECT_DATE,ARISING_TYPE_NAME,ARISING_GROUP_TYPE,NOTE,ORG_DESC,NEW_SAL,SALARY_HD,SALARY_PC,TITLE_BHXH_NAME">
                        <GroupByExpressions>
                            <tlk:GridGroupByExpression>
                                <SelectFields>
                                    <tlk:GridGroupByField FieldName="ARISING_GROUP_TYPE" HeaderText="Nhóm Biến động"></tlk:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <tlk:GridGroupByField FieldName="ARISING_GROUP_TYPE"></tlk:GridGroupByField>
                                </GroupByFields>
                            </tlk:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" FilterControlWidth="99%" HeaderStyle-Width="70px"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULL_NAME"
                                UniqueName="FULL_NAME" SortExpression="FULL_NAME" HeaderStyle-Width="150px" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nhân viên %>" DataField="EMP_STATUS_NAME"
                                UniqueName="EMP_STATUS_NAME" SortExpression="EMP_STATUS_NAME" HeaderStyle-Width="150px" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="DEP_NAME"
                                UniqueName="DEP_NAME" HeaderStyle-Width="150px" SortExpression="DEP_NAME" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            

                            <%--<tlk:GridTemplateColumn HeaderText="Đơn vị" DataField="DEP_NAME" SortExpression="ORG_NAME"
                                UniqueName="DEP_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEP_NAME")%>'>
                                </asp:Label>
                                <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                                    RelativeTo="Element" Position="BottomCenter">
                                <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                </tlk:RadToolTip>
                            </ItemTemplate>
                            </tlk:GridTemplateColumn>--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" HeaderStyle-Width="150px" SortExpression="TITLE_NAME"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí CV BHXH %>" DataField="TITLE_BHXH_NAME"
                                UniqueName="TITLE_BHXH_NAME" HeaderStyle-Width="150px" SortExpression="TITLE_BHXH_NAME"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />--%>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE"
                                HeaderStyle-Width="85px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tháng báo đúng %>" DataField="MONTH_CONFIRM" UniqueName="MONTH_CONFIRM" SortExpression="MONTH_CONFIRM"
                                HeaderStyle-Width="85px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />

                            <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến tháng %>" DataField="TO_MONTH_CONFIRM" UniqueName="TO_MONTH_CONFIRM" SortExpression="TO_MONTH_CONFIRM"
                                HeaderStyle-Width="85px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />--%>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lương cũ %>" DataFormatString="{0:N0}"
                                DataField="OLD_SAL" UniqueName="OLD_SAL" SortExpression="OLD_SAL" HeaderStyle-Width="80px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lương mới %>" DataFormatString="{0:N0}"
                                DataField="NEW_SAL" UniqueName="NEW_SAL" SortExpression="NEW_SAL" HeaderStyle-Width="80px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Lương hợp đồng %>" DataFormatString="{0:N0}"
                                DataField="SALARY_HD" UniqueName="SALARY_HD" SortExpression="SALARY_HD" HeaderStyle-Width="80px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />--%>
                           <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Phụ cấp %>" DataFormatString="{0:N0}"
                                DataField="SALARY_PC" UniqueName="SALARY_PC" SortExpression="SALARY_PC" HeaderStyle-Width="80px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do %>" DataField="REASONS" UniqueName="REASONS"
                                SortExpression="REASONS" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại biến động %>" DataField="ARISING_TYPE_NAME"
                                UniqueName="ARISING_TYPE_NAME" SortExpression="ARISING_TYPE_NAME" Visible="true" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị BH %>" DataField="U_INSURANCE_NAME"
                                UniqueName="U_INSURANCE_NAME" HeaderStyle-Width="300px" SortExpression="U_INSURANCE_NAME" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHXH %>" DataField="SI" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="SI" SortExpression="SI" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHYT %>" DataField="HI" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="HI" SortExpression="HI" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTN %>" DataField="UI" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="UI" SortExpression="UI" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTNLD-BNN %>" DataField="BHTNLD_BNN" DataType="System.Boolean"
                                ItemStyle-VerticalAlign="Middle" UniqueName="BHTNLD_BNN" SortExpression="BHTNLD_BNN" HeaderStyle-Width="40px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Sử dụng %>" DataField="ARISING_TYPE_ID"
                                UniqueName="ARISING_TYPE_ID" SortExpression="ARISING_TYPE_ID" Visible="false" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số sổ bảo hiểm %>" DataField="SOCIAL_NUMBER"
                                UniqueName="SOCIAL_NUMBER" HeaderStyle-Width="150px" SortExpression="SOCIAL_NUMBER"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />


                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV%>" DataField="EMPID" UniqueName="EMPID"
                                SortExpression="EMPID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID%>" DataField="ID" UniqueName="ID"
                                SortExpression="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID%>" DataField="ARISING_GROUP_TYPE"
                                UniqueName="ARISING_GROUP_TYPE" SortExpression="ARISING_GROUP_TYPE" Visible="false" />
                            <%--<tlk:GridTemplateColumn Display="false" HeaderText="<%$ Translate: Ghi chú %>" UniqueName="NOTE" SortExpression="NOTE" HeaderStyle-Width="200px">
                              <ItemTemplate>
                                <tlk:RadTextBox ID="rtbNote" runat="server" Text='<%# Bind("NOTE") %>'></tlk:RadTextBox>
                              </ItemTemplate>
                            </tlk:GridTemplateColumn>--%>
                        </Columns>
                    </MasterTableView>
                    <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
