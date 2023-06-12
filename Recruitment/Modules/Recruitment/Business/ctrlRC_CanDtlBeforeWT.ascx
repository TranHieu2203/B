<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CanDtlBeforeWT.ascx.vb"
    Inherits="Recruitment.ctrlRC_CanDtlBeforeWT" %>
<%@ Register Src="../Shared/ctrlRC_CanBasicInfo.ascx" TagName="ctrlRC_CanBasicInfo"
    TagPrefix="Recruitment" %>
<asp:HiddenField ID="hidEmpBeforeWTID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Orientation="Horizontal" Style="height: 100%;
    width: 100%; overflow: Auto;">
    <tlk:RadPane ID="RadPane2" runat="server" Height="50px" Scrolling="None">
        <recruitment:ctrlrc_canbasicinfo runat="server" id="ctrlRC_CanBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPaneLeft" runat="server" Height="250px">
        <%--    <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />--%>
        <table class="table-form" style="padding-left: 32px">
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Từ tháng/năm")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromdate" DateInput-DisplayDateFormat="MM/yyyy" runat="server" AutoPostBack="true">
                    </tlk:RadDatePicker>
                  <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdFromdate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày vào. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày vào %>"> </asp:RequiredFieldValidator>
                </td>
                <td style="text-align: left" class="lb">
                    <%# Translate("Đến tháng/năm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTodate" runat="server" DateInput-DisplayDateFormat="MM/yyyy" AutoPostBack="true">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdTodate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày đến. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày đến %>"> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cval_EffectDate_ExpireDate" runat="server" ErrorMessage="<%$ Translate:  Ngày nghỉ phải lớn hơn ngày vào. %>"
                        ToolTip="<%$ Translate: Ngày nghỉ phải lớn hơn ngày vào. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID ="lblThamNiem" Text ="Thâm niên" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtThamNien" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Tên công ty")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtOrgname" Width ="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrgname"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên công ty %>" ToolTip="<%$ Translate: Bạn phải nhập tên công ty %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Địa chỉ công ty ")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtOrgAddress" Width ="100%">
                    </tlk:RadTextBox>
                   <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOrgAddress"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập địa chỉ công ty %>"
                        ToolTip="<%$ Translate: Bạn phải nhập địa chỉ công ty %>"> </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                 <td style="text-align: left" class="lb" >
                    <%# Translate("Chức danh")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtTitlename" Width="100%">
                    </tlk:RadTextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtTitlename"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập chức danh %>" ToolTip="<%$ Translate: Bạn phải nhập chức danh %>"> </asp:RequiredFieldValidator>
                </td>
                <%--<td style="text-align: left" class="lb">
                    <%# Translate("Số điện thoại")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPhone">
                    </tlk:RadTextBox>
                </td>
                --%>
            </tr>
            
            <tr>
               
                <td style="text-align: left" class="lb">
                    <%# Translate("Mô tả công việc")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtWork" Width="100%">
                    </tlk:RadTextBox>
                </td>
                </tr>
            <tr>
                <td style="text-align: left" class="lb">
                    <%# Translate("Lý do nghỉ việc")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtReasonLeave" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            
            <tr>
                <td class="lb" style="text-align: left;display:none">
                    <%# Translate("QLTT")%>
                </td>
                <td style="display:none">
                    <tlk:RadTextBox ID="txtDirectManager" runat="server" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="text-align: left">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
             <tr style="display:none">
                <td class="lb" style="text-align: left">
                    <%# Translate("Mức lương")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnSalary" runat="server" MinValue="0" NumberFormat-DecimalDigits="2" NumberFormat-GroupSizes="3">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="100%">
        <tlk:RadGrid ID="rgCandidateBeforeWT" runat="server" AutoGenerateColumns="False" PageSize="50"
            AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="True" CellSpacing="0"
            GridLines="None" Height="100%">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID">
                <CommandItemSettings ExportToPdfText="Export to PDF" />
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                </RowIndicatorColumn>
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                </ExpandCollapseColumn>
                <Columns>
                    <tlk:GridClientSelectColumn HeaderStyle-Width="40px">
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="Candidate_ID" HeaderText="Candidate_ID" UniqueName="Candidate_ID"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ORG_NAME" HeaderText="<%$ Translate : Tên công ty %>"
                        UniqueName="ORG_NAME" Visible="True">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <%--<tlk:GridBoundColumn DataField="ORG_PHONE" HeaderText="<%$ Translate : Điện thoại công ty %>"
                        UniqueName="ORG_PHONE" Visible="True">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>--%>
                  <tlk:GridBoundColumn DataField="ORG_ADDRESS" HeaderText="<%$ Translate : Địa chỉ công ty %>"
                        UniqueName="ORG_ADDRESS" Visible="false">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FROMDATE" HeaderText="<%$ Translate : Từ tháng/năm %>"
                        DataFormatString="{0:MM/yyyy}" UniqueName="FROMDATE" Visible="True">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TODATE" HeaderText="<%$ Translate : Đến tháng/năm %>"
                        DataFormatString="{0:MM/yyyy}" UniqueName="TODATE" Visible="True">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SENORITY" HeaderText="<%$ Translate : Thâm niên %>"
                        UniqueName="SENORITY" Visible="True">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TITLE_NAME" HeaderText="<%$ Translate : Chức danh %>"
                        UniqueName="TITLE_NAME" Visible="True">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                 <tlk:GridBoundColumn DataField="DIRECT_MANAGER" HeaderText="<%$ Translate : QLTT %>"
                        UniqueName="DIRECT_MANAGER" Visible="false">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                   <tlk:GridBoundColumn DataField="WORK" HeaderText="<%$ Translate : Mô tả công việc %>"
                        UniqueName="WORK" Visible="True">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="REASON_LEAVE" HeaderText="<%$ Translate : Lý do nghỉ việc %>"
                        UniqueName="REASON_LEAVE" Visible="True">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="REMARK" HeaderText="<%$ Translate : Ghi chú %>" UniqueName="REMARK"
                        Visible="True">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn DataField="SALARY" HeaderText="<%$ Translate : Mức lương %>" UniqueName="SALARY"
                        Visible="false" DataFormatString="{0:#,##0}">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnablePostBackOnRowClick="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function RaiseCommand(sender, eventArgs) {
            var item = eventArgs.get_commandName()
            if (item == "ExportToExcel") {
                enableAjax = false;
            }
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter2.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPaneLeft.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter2.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPaneLeft.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%# RadPane3.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - 103 - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
