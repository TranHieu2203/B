<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlApproveSetupEmp.ascx.vb"
    Inherits="Common.ctrlApproveSetupEmp" %>
<%@ Register Src="../ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter runat="server" ID="splitFull" Width="100%" Height="100%">
    <tlk:RadPane runat="server" ID="rpnLeftFull" Width="350" Scrolling="None">
        <tlk:RadSplitter runat="server" ID="spliterTreeEmp" Orientation="Horizontal" Width="100%"
            Height="100%">
            <tlk:RadPane runat="server" ID="rpnTreeOrgTop" Scrolling="None">
                <Common:ctrlOrganization runat="server" ID="ctrlOrg" />
            </tlk:RadPane>
            <tlk:RadPane runat="server" ID="rpnTreeOrgBottom" Scrolling="None">
                <tlk:RadGrid PageSize=50 runat="server" ID="rgEmp" SkinID="GridSingleSelect" Width="100%" Height="100%">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="2" />
                        <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="false" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Mã NV %>' DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderStyle-Width="45">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Họ tên %>' DataField="FULLNAME_VN"
                                UniqueName="FULLNAME_VN" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderStyle-Width="70">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Phòng ban %>' DataField="ORG_NAME"
                                UniqueName="ORG_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderStyle-Width="120">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Vị trí công việc %>' DataField="TITLE_NAME_VN"
                                UniqueName="TITLE_NAME_VN" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderStyle-Width="120">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Loại nhân viên %>' DataField="EMP_STATUS"
                                UniqueName="EMP_STATUS" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderStyle-Width="120">
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="rpnRightFull" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="145px">
                <tlk:RadToolBar runat="server" ID="tbarDetail" Width="100%" OnClientButtonClicking="tbarDetail_ClientButtonClicking">
                </tlk:RadToolBar>
                <asp:ValidationSummary runat="server" ID="valSum" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Quy trình")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadComboBox runat="server" ID="cboApproveProcess" Width="250px">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="rqApproveProcess" ControlToValidate="cboApproveProcess" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn quy trình. %>" ToolTip="<%$ Translate: Bạn phải chọn quy trình. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Template áp dụng")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadComboBox runat="server" ID="cboApproveTemplate" Width="250px">
                            </tlk:RadComboBox>
                            <asp:CustomValidator ID="cvalApproveTemplate" ControlToValidate="cboApproveTemplate" runat="server" ErrorMessage="<%$ Translate: Template phê duyệt không tồn tại hoặc đã ngừng áp dụng. %>"
                                ToolTip="<%$ Translate: Template phê duyệt không tồn tại hoặc đã ngừng áp dụng. %>">
                            </asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="reqApproveTemplate" ControlToValidate="cboApproveTemplate" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn Template phê duyệt. %>" ToolTip="<%$ Translate: Bạn phải chọn Template phê duyệt. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Áp dụng từ ngày")%>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdFromDate">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator runat="server" ID="reqFromDate" ControlToValidate="rdFromDate"
                                ErrorMessage='<%$ Translate: Chưa nhập Từ ngày %>'></asp:RequiredFieldValidator>
                            <asp:CustomValidator runat="server" ID="cvalCheckDateExist" ErrorMessage='<%$ Translate: Đã có thiết lập áp dụng vào khoảng thời gian bạn chọn. %>'></asp:CustomValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdToDate">
                            </tlk:RadDatePicker>
                            <asp:CustomValidator runat="server" ID="cvalFromDateToDate" ErrorMessage='<%$ Translate: Từ ngày lớn hơn đến ngày. %>'></asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 runat="server" ID="rgDetail" Width="1010px" Height="100%" SkinID="GridSingleSelect">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                        <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="false" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" GroupsDefaultExpanded="true">
                        <GroupByExpressions>
                            <tlk:GridGroupByExpression>
                                <GroupByFields>
                                    <tlk:GridGroupByField FieldName="PROCESS_NAME" />
                                </GroupByFields>
                                <SelectFields>
                                    <tlk:GridGroupByField FieldName="PROCESS_NAME" HeaderText="Quy trình" />
                                </SelectFields>
                            </tlk:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Template %>' DataField="TEMPLATE_NAME"
                                UniqueName="TEMPLATE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Email thông báo %>' DataField="REQUEST_EMAIL"
                                UniqueName="REQUEST_EMAIL">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Áp dụng từ ngày %>' DataField="FROM_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="FROM_DATE">
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Áp dụng đến ngày %>' DataField="TO_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="TO_DATE">
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock runat="server" ID="radScriptBlock">
    <script type="text/javascript">
        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlApproveSetupEmp_splitFull');
        }

        var oldSize = 145;
        function tbarDetail_ClientButtonClicking(s, e) {

            if (e.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }

            switch (e.get_item().get_commandName()) {
                case 'CREATE':
                    if ($find('<%= rgEmp.ClientID %>').get_masterTableView().get_selectedItems().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn nhân viên cần thiết lập', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        e.set_cancel(true);
                        break;
                    }
                    break;
                case 'EDIT':
                    if ($find('<%= rgDetail.ClientID %>').get_masterTableView().get_selectedItems().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn bản ghi nào! Không thể thực hiện thao tác này', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        e.set_cancel(true);
                        break;
                    }
                    break;
                case 'DELETE':
                    if ($find('<%= rgDetail.ClientID %>').get_masterTableView().get_selectedItems().length == 0) {
                        var n = noty({ text: 'Bạn chưa chọn dòng cần xóa', dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                        e.set_cancel(true);
                        break;
                    }

                    break;
            }

        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane3.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 1000);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane3.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;

            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadScriptBlock>
