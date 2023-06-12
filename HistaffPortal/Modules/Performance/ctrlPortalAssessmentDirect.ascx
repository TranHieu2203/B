<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalAssessmentDirect.ascx.vb"
    Inherits="Performance.ctrlPortalAssessmentDirect" %>
<%@ Import Namespace="Common" %>
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<tlk:RadSplitter runat="server" ID="splitAll" Orientation="Horizontal" Width="100%"
    Height="100%">
<tlk:RadPane runat="server" ID="paneTop" Scrolling="None" Height="80px">
            <table class="table-form">
                <tr>
                   <td> <%# Translate("Năm đánh giá")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="txtYear" runat="server" NumberFormat-DecimalDigits="1"
                            NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                            MaxValue="2900" SkinID="Number" CausesValidation="false">
                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                        </tlk:RadNumericTextBox>
                    </td>
                    <td> <%# Translate("Trạng thái")%>
                    </td>
                    <td>
                        <tlk:RadComboBox id="cboStatus" runat="server"></tlk:RadComboBox>
                    </td>
                     <td>
                           <tlk:RadButton runat="server" ID="btnSearch" Text='<%$ Translate: Tìm kiếm %>'>
                            </tlk:RadButton>
                     </td>
                    
                </tr>
                </table>
                <table>
                <tr>
                    <td>
                    <tlk:RadTextBox runat="server" ID="txtNote" EmptyMessage='<%$ Translate: Ý kiến %>'
                        Width="300px">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnApprove" Text='<%$ Translate: Duyệt %>' ForeColor="Blue"
                        ValidationGroup="Approve" Width="60px">
                    </tlk:RadButton>
                    <%--<tlk:RadButton runat="server" ID="btnDeny" Text='<%$ Translate: Không duyệt %>' ForeColor="Red"
                        ValidationGroup="Approve" Enabled="false">
                    </tlk:RadButton>--%>
                    </td>
                </tr>
            </table>
    </tlk:RadPane>
</tlk:RadSplitter>

<tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="350px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID,PE_PERIO_YEAR,EMPLOYEE_ID,PE_PERIO_ID,PE_STATUS_ID,OBJECT_GROUP_ID"
        ClientDataKeyNames="ID,PE_PERIO_YEAR,EMPLOYEE_ID,PE_PERIO_ID,PE_PERIO_TYPE_ASS,PE_STATUS_ID,OBJECT_GROUP_ID">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
            </tlk:GridClientSelectColumn>
            <tlk:GridBoundColumn DataField="ID" Visible="false">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn DataField="PE_PERIO_TYPE_ASS" Visible="false">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn DataField="PE_PERIO_ID" Visible="false">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="EMPLOYEE_NAME"
                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                SortExpression="ORG_NAME" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm đánh giá %>" DataField="PE_PERIO_YEAR"
                UniqueName="PE_PERIO_YEAR" SortExpression="PE_PERIO_YEAR" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kỳ đánh giá %>" DataField="PE_PERIO_NAME"
                UniqueName="PE_PERIO_NAME" SortExpression="PE_PERIO_NAME" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu đánh giá %>" DataField="PE_PERIO_TYPE_ASS_NAME"
                UniqueName="PE_PERIO_TYPE_ASS_NAME" SortExpression="PE_PERIO_TYPE_ASS_NAME" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="PE_PERIO_START_DATE"
                UniqueName="PE_PERIO_START_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="PE_PERIO_START_DATE"
                ShowFilterIcon="true">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="PE_PERIO_END_DATE"
                UniqueName="PE_PERIO_END_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="PE_PERIO_END_DATE"
                ShowFilterIcon="true">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng điểm NV %>" DataField="RESULT_CONVERT_NV"
                UniqueName="RESULT_CONVERT_NV" SortExpression="RESULT_CONVERT_NV" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp hạng NV %>" DataField="CLASSIFICATION_NAME_NV"
                UniqueName="CLASSIFICATION_NAME_NV" SortExpression="CLASSIFICATION_NAME_NV" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng điểm đánh giá %>" DataField="RESULT_CONVERT"
                UniqueName="RESULT_CONVERT" SortExpression="RESULT_CONVERT" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp hạng %>" DataField="CLASSIFICATION_NAME"
                UniqueName="CLASSIFICATION_NAME" SortExpression="CLASSIFICATION_NAME" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="PE_STATUS_NAME"
                UniqueName="PE_STATUS_NAME" SortExpression="PE_STATUS_NAME" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đánh giá kết quả công việc %>"
                Visible="false">
                <ItemTemplate>
                    <%--   <asp:LinkButton ID="btnlnk" runat="server" Text="Đánh giá" OnClick="btnEditClick"
                        Style="text-decoration: underline !important; color: Blue">
                    </asp:LinkButton>--%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <HeaderStyle Width="90px" />
            </tlk:GridTemplateColumn>
            <tlk:GridButtonColumn HeaderText="Đánh giá kết quả công việc" Text="Đánh giá" Visible="false">
                <HeaderStyle Width="90px" />
                <ItemStyle Font-Underline="true" Wrap="false" Width="90px" ForeColor="Blue" />
            </tlk:GridButtonColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings>
        <Selecting AllowRowSelect="True" />
        <ClientEvents OnRowDblClick="gridRowDblClick" />
    </ClientSettings>
</tlk:RadGrid>
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
        var selectedID = -1;
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function btnEditClick(sender, args) {
            var Year = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PE_PERIO_YEAR');
            var PeriodId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PE_PERIO_ID');
            var TypeAssId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PE_PERIO_TYPE_ASS');
            var EmpId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            var status = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PE_STATUS_ID');
            var oWindow = radopen('Dialog.aspx?mid=Performance&fid=ctrlPE_Assess&group=Business&noscroll=1&Year=' + Year + '&PeriodId=' + PeriodId + '&TypeAssId=' + TypeAssId + '&EmpId=' + EmpId + '&staus=' + status, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
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
            var Year = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PE_PERIO_YEAR');
            var PeriodId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PE_PERIO_ID');
            var TypeAssId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PE_PERIO_TYPE_ASS');
            var EmpId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            var status = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('PE_STATUS_ID');
            OpenInNewTab('Default.aspx?mid=Performance&fid=ctrlPE_Assess&Year=' + Year + '&PeriodId=' + PeriodId + '&TypeAssId=' + TypeAssId + '&EmpId=' + EmpId + '&status=' + status)
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

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'PRINT') {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadCodeBlock>
