<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_WorkStandard.ascx.vb"
    Inherits="Payroll.ctrlPA_WorkStandard" %>
<%@ Import Namespace="Framework.UI" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="185px" Scrolling="None">
                <tlk:RadToolBar ID="tbarSalaryTypes" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
                    CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên công ty")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtCongty">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Đối tượng lao động")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboLabor" runat="server" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqLabor" ControlToValidate="cboLabor" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn đối tượng lao động. %>" ToolTip="<%$ Translate: Bạn phải chọn đối tượng lao động. %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Năm")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rntxtYEAR" Width="60px" Value="1" ShowSpinButtons="True"
                                ReadOnly="true" CausesValidation="false" MinValue="2015" AutoPostBack="True">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtYEAR"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập năm. %>" ToolTip="<%$ Translate: Bạn phải nhập năm. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Kỳ lương")%></label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" runat="server" CausesValidation="false">
                            </tlk:RadComboBox>
                        </td>
                        <td id="Td1" class="lb" runat="server">
                            <label id="lbOrders">
                                <%# Translate("Ngày công chế độ")%></label>
                        </td>
                        <td id="Td2" runat="server">
                            <tlk:RadTextBox ID="txtWordStandard" runat="server" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtWordStandard"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày công chế độ. %>"
                                ToolTip="<%$ Translate: Ngày công chế độ. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="YEAR,ORG_ID,ORG_NAME,PERIOD_ID,OBJECT_ID,OBJECT_NAME,PERIOD_STANDARD,PERIOD_NAME,ACTFLG,REMARK">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Công ty/Đơn vị %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng lao động %>" DataField="OBJECT_NAME"
                                SortExpression="OBJECT_NAME" UniqueName="OBJECT_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" SortExpression="YEAR"
                                UniqueName="YEAR" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kỳ lương %>" DataField="PERIOD_NAME"
                                SortExpression="PERIOD_NAME" UniqueName="PERIOD_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Ngày công chế độ %>" DataField="PERIOD_STANDARD"
                                SortExpression="PERIOD_STANDARD" UniqueName="PERIOD_STANDARD">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                                SortExpression="ACTFLG" UniqueName="ACTFLG" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <ClientEvents OnGridCreated="GridCreated" />
                    </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>    
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlPA_Period_RadSplitter1';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Period_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Period_RadPane2';
        var enableAjax = true;
        var oldSize = $('#' + pane1ID).height();
//        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                //ResizeSplitter();
                ResizeSplitter(splitterID, pane1ID, pane2ID, oldSize, 'rgData');
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        
    </script>
</tlk:RadCodeBlock>
