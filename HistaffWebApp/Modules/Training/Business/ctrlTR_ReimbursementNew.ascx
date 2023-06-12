<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ReimbursementNew.ascx.vb"
    Inherits="Training.ctrlTR_ReimbursementNew" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="270px" Scrolling="None">
      
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidEmployeeID" />
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
                <td></td>

                <td>
                    <asp:CheckBox ID="chkTerminate" runat="server" Text="Nhân viên nghỉ việc" />
                </td>
            </tr>
            <tr>
                <td class="lb" >
                    <%# Translate("Ngày bắt đầu cam kết")%>
                </td>
                <td >
                    <tlk:RadDatePicker ID="rdCOMMIT_START_T_SEARCH" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb" >
                    <%# Translate("Đến ngày")%>
                </td>
                <td >
                    <tlk:RadDatePicker ID="rdCOMMIT_START_E_SEARCH" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSeachEmployee">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" >
                    <%# Translate("Ngày kết thúc cam kết")%>
                </td>
                <td >
                    <tlk:RadDatePicker ID="rdCOMMIT_END_T_SEARCH" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td >
                    <tlk:RadDatePicker ID="rdCOMMIT_END_E_SEARCH" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td></td>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                        Text="<%$ Translate: Tìm kiếm %>">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin xủ lý bồi hoàn")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkWithTer" runat="server" Text="Theo ngày nghỉ việc" AutoPostBack="true" OnCheckedChanged="chkWithTer_CheckChanged"/>
                </td>
                <td class="lb">
                    <%# Translate("Ngày chốt")%>
                </td>
                <td >
                    <tlk:RadDatePicker ID="rdCLOSING_DATE" runat="server" AutoPostBack="true">
                    </tlk:RadDatePicker>
                </td>
            </tr>
             <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin điền nhanh ghi chú")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tháng bồi hoàn")%>
                </td>
                <td>
                    <tlk:RadMonthYearPicker runat="server" ID="rdMONTH_PERIOD" TabIndex="4" Culture="en-US">
                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="MM/yyyy">
                        </DateInput>
                    </tlk:RadMonthYearPicker>
                    <%--<tlk:RadTextBox runat="server" ID="rdMONTH_PERIOD">
                    </tlk:RadTextBox>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="472px">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <tlk:RadButton ID="btnDienNhanh" runat="server"  CausesValidation="false"
                        Text="<%$ Translate: Điền nhanh %>">
                    </tlk:RadButton>
                </td>
            </tr>

        </table>
        <tlk:RadGrid ID="rgMain" runat="server" AllowPaging="True" Height="53%" AllowSorting="True"
            AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID"
                ClientDataKeyNames="ID,MONTH_PERIOD,REIMBURSE_REMARK,IS_LOCK_NAME,TER_DATE,CONVERED_TIME,COMMIT_END,MONEY_COMMIT,IS_LOCK">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đã khóa %>" DataField="IS_LOCK_NAME"
                        UniqueName="IS_LOCK_NAME" SortExpression="IS_LOCK_NAME" HeaderStyle-Width="50px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="80px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                        UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" HeaderStyle-Width="150px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        SortExpression="TITLE_NAME" HeaderStyle-Width="150px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" HeaderStyle-Width="250px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày nghỉ việc %>" DataField="TER_DATE"
                        UniqueName="TER_DATE" SortExpression="TER_DATE" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" UniqueName="YEAR"
                        SortExpression="YEAR" HeaderStyle-Width="50px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chương trình đào tạo %>" DataField="TR_PROGRAM_NAME"
                        UniqueName="TR_PROGRAM_NAME" SortExpression="TR_PROGRAM_NAME" HeaderStyle-Width="100px"/>

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="TR_PROGRAM_START_DATE"
                        UniqueName="TR_PROGRAM_START_DATE" SortExpression="TR_PROGRAM_START_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" />

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="TR_PROGRAM_END_DATE"
                        UniqueName="TR_PROGRAM_END_DATE" SortExpression="TR_PROGRAM_END_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" />


                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tiền cam kết %>" DataField="MONEY_COMMIT"
                        UniqueName="MONEY_COMMIT" SortExpression="MONEY_COMMIT" DataFormatString="{0:N0}" HeaderStyle-Width="100px"/>

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số ngày cam kết %>" DataField="CONVERED_TIME"
                        UniqueName="CONVERED_TIME" SortExpression="CONVERED_TIME" HeaderStyle-Width="100px"/>

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày cam kết %>" DataField="COMMIT_START"
                        UniqueName="COMMIT_START" SortExpression="COMMIT_START" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" />

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày cam kết %>" DataField="COMMIT_END"
                        UniqueName="COMMIT_END" SortExpression="COMMIT_END" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" />

                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày chốt bồi hoàn %>" DataField="CLOSING_DATE"
                        UniqueName="CLOSING_DATE" SortExpression="CLOSING_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số ngày còn cam kết %>" DataField="REIMBURSE_TIME"
                        UniqueName="REIMBURSE_TIME" SortExpression="REIMBURSE_TIME" HeaderStyle-Width="100px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tiền bồi hoàn %>" DataField="MONEY_REIMBURSE"
                        UniqueName="MONEY_REIMBURSE" SortExpression="MONEY_REIMBURSE" DataFormatString="{0:N0}" HeaderStyle-Width="100px"/>

                    <tlk:GridBoundColumn HeaderText="<%$ Translate:Tháng bồi hoàn %>" DataField="MONTH_PERIOD"
                        UniqueName="MONTH_PERIOD" SortExpression="MONTH_PERIOD" HeaderStyle-Width="100px" />

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REIMBURSE_REMARK"
                        UniqueName="REIMBURSE_REMARK" SortExpression="REIMBURSE_REMARK" HeaderStyle-Width="250px"/>
                </Columns>
            </MasterTableView>

        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindReimbursement" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusCourse(oSrc, args) {
            var cbo = $find("<%# cboCourse.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT" || item.get_commandName() == "EXPORT_TEMPLATE") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                //ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                //ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        //        function ResizeSplitter() {
        //            setTimeout(function () {
        //                var splitter = $find("<%= RadSplitter3.ClientID%>");
        //                var pane = splitter.getPaneById('<%= LeftPane.ClientID %>');
        //                var height = pane.getContentElement().scrollHeight;
        //                splitter.set_height(splitter.get_height() + pane.get_height() - height);
        //                pane.set_height(height);
        //            }, 200);
        //        }

        //        // Hàm khôi phục lại Size ban đầu cho Splitter
        //        function ResizeSplitterDefault() {
        //            var splitter = $find("<%= RadSplitter3.ClientID%>");
        //            var pane = splitter.getPaneById('<%= LeftPane.ClientID %>');
        //            if (oldSize == 0) {
        //                oldSize = pane.getContentElement().scrollHeight;
        //            } else {
        //                var pane2 = splitter.getPaneById('<%= MainPane.ClientID %>');
        //                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
        //                pane.set_height(oldSize);
        //                pane2.set_height(splitter.get_height() - oldSize - 1);
        //            }
        //        }
    </script>
</tlk:RadCodeBlock>
