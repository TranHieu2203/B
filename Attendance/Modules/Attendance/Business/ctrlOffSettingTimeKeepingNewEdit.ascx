<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOffSettingTimeKeepingNewEdit.ascx.vb"
    Inherits="Attendance.ctrlOffSettingTimeKeepingNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height ="20%">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCode" runat="server" Text="Loại chế độ"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTypeBT" runat="server">
                    </tlk:RadComboBox>

                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="cboTypeBT" runat="server"
                        ErrorMessage="Bạn phải nhập loại nghĩ." ToolTip="Bạn phải nhập loại nghĩ."> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbFromDate" runat="server" Text="Từ ngày"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdFromDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdFromDate"
                        runat="server" ErrorMessage="Bạn phải nhập từ ngày." ToolTip="Bạn phải nhập từ ngày."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbToDate" runat="server" Text="Đến ngày"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdToDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdToDate"
                        runat="server" ErrorMessage="Bạn phải nhập đến ngày." ToolTip="Bạn phải nhập đến ngày."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbNumber" runat="server" Text="Số phút"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="txtNumber">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNumber"
                        runat="server" ErrorMessage="Bạn phải nhập số phút." ToolTip="Bạn phải nhập số phút."> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbREMARK" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtREMARK" Width="100%" SkinID="Textbox1023" TextMode="MultiLine"
                        runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
         <tlk:RadGrid ID="rgEmployee" AllowPaging="true" AllowMultiRowEdit="true" runat="server" PageSize="50" 
        Height="100%">
        <groupingsettings casesensitive="false" />
        <mastertableview editmode="InPlace" allowpaging="true" allowcustompaging="true"
            datakeynames="ID,EMPLOYEE_CODE,ORG_ID,TITLE_ID" clientdatakeynames="ID,EMPLOYEE_ID,FULLNAME_VN,TITLE_ID,ORG_ID,TITLE_NAME,ORG_NAME,EMPLOYEE_CODE"
            commanditemdisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnEmployee" runat="server" Text="Chọn tất cả nhân viên"
                                CausesValidation="false" CommandName="FindEmployee" TabIndex="3">
                            </tlk:RadButton> 
                            <%--<tlk:RadButton Width="150px" ID="RadButton1" runat="server" Text="Chọn nhân viên từ import"
                                CausesValidation="false" CommandName="FindEmployeeImport" TabIndex="3">
                            </tlk:RadButton>--%>                                                     
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmp" runat="server" Text="Xóa"
                                CausesValidation="false" CommandName="DeleteEmployee" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                   <%-- <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="MSNV" DataField="EMPLOYEE_CODE"
                        ReadOnly="true" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="FULLNAME_VN" UniqueName="FULLNAME_VN"
                        ReadOnly="true" SortExpression="FULLNAME_VN" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        ReadOnly="true" SortExpression="ORG_NAME" />
                        <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        ReadOnly="true" SortExpression="TITLE_NAME" />--%>
                </Columns>
            </mastertableview>
        <headerstyle horizontalalign="Center" />
        <clientsettings>
                <Selecting AllowRowSelect="True" />
            </clientsettings>
    </tlk:RadGrid>
    <//tlk:RadPane>   
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            registerOnfocusOut('ctl00_MainContent_RadSplitter1');
        });
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
        function clientButtonClicking(sender, args) {
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
        }
        
    </script>
</tlk:RadCodeBlock>
