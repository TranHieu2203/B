<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Stocks.ascx.vb"
    Inherits="Profile.ctrlHU_Stocks" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    #ctl00_MainContent_ctrlHU_Stocks_rnMonth_wrapper{
        width: 110px!important;
    }
    #ctl00_MainContent_ctrlHU_Stocks_rnPercent_wrapper{
        width: 145px!important;
    }
    #ctl00_MainContent_ctrlHU_Stocks_rnTime_wrapper{
        width: 110px!important;
    }
    .text-center{
        text-align: center !important;
    }
</style>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="30px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="165px">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbCode" runat="server" Text="Mã hồ sơ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCode" runat="server" ReadOnly="true" SkinID="Readonly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbTitle" runat="server" Text="Vị trí công việc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTileName" runat="server" ReadOnly="true" SkinID="Readonly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbPercent" runat="server" Text="Chỉ số chiết khấu"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnPercent" runat="server">
                            </tlk:RadNumericTextBox> %
                            <asp:RequiredFieldValidator ID="reqPercent" ControlToValidate="rnPercent" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập Chỉ số chiết khấu. %>" ToolTip="<%$ Translate: Bạn phải nhập Chỉ số chiết khấu. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="text-align: left">
                            <asp:Label ID="lbNote" runat="server" Text="Ghi chú"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbEmployeeCode" runat="server" Text="Mã nhân viên"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployeeCode" runat="server" ReadOnly="true" SkinID="Readonly"  Width="132px">
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false" Width="40px">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbDateState" runat="server" Text="Ngày nhận việc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDateState" runat="server" ReadOnly="true" SkinID="Readonly">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbMonth" runat="server" Text="Thời gian TT thỏa thuận"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnMonth" runat="server" SkinID="Decimal" >
                            </tlk:RadNumericTextBox> (tháng)
                            <asp:RequiredFieldValidator ID="reqMonth" ControlToValidate="rnMonth" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian TT thỏa thuận. %>" 
                                ToolTip="<%$ Translate: Bạn phải nhập Thời gian TT thỏa thuận. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td rowspan="3" colspan="2">
                            <tlk:RadTextBox ID="txtNote" runat="server" Width="100%" Height="85px" SkinID="Textbox1023">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbEmployeeName" runat="server" Text="Họ và tên"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="true" SkinID="Readonly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbEffectedDate" runat="server" Text="Ngày hiệu lực"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEffectedDate" runat="server" AutoPostBack="true">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqEffectedDate" ControlToValidate="rdEffectedDate" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>" 
                                ToolTip="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbTime" runat="server" Text="Số lần TT thỏa thuận"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnTime" runat="server" >
                            </tlk:RadNumericTextBox> (lần)
                            <asp:RequiredFieldValidator ID="reqTime" ControlToValidate="rnTime" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập Số lần TT thỏa thuận. %>" 
                                ToolTip="<%$ Translate: Bạn phải nhập Số lần TT thỏa thuận. %>"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbLocation" runat="server" Text="Công ty"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtLocation" runat="server" ReadOnly="true" SkinID="Readonly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbStockType" runat="server" Text="Loại cổ phiếu"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboStockType" runat="server" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqStockType" ControlToValidate="cboStockType"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại cổ phiếu. %>"
                                ToolTip="<%$ Translate: Bạn phải chọn Loại cổ phiếu. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbStockDeal" runat="server" Text="Số lượng CP TT"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnStockDeal" runat="server">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="reqStockDeal" ControlToValidate="rnStockDeal" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập Số lượng CP TT. %>" 
                                ToolTip="<%$ Translate: Bạn phải nhập Số lượng CP TT. %>"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbOrgName" runat="server" Text="Phòng ban"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="true" SkinID="Readonly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbPayType" runat="server" Text="Hình thức chi trả"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPayType" runat="server" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqPayType" ControlToValidate="cboPayType"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Hình thức chi trả. %>"
                                ToolTip="<%$ Translate: Bạn phải chọn Hình thức chi trả. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbMoneyDeal" runat="server" Text="Số tiền quy đổi thỏa thuận"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnMoneyDeal" runat="server">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="reqMoneyDeal" ControlToValidate="rnMoneyDeal" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập Số tiền quy đổi thỏa thuận. %>" 
                                ToolTip="<%$ Translate: Bạn phải nhập Số tiền quy đổi thỏa thuận. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbUploadFile" Text="<%$ Translate: File đính kèm %>"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                            </tlk:RadTextBox>
                            <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false"
                                TabIndex="3" />
                            <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                                CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%" AllowPaging="True" AllowFilteringByColumn="true"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true"  EnablePostBackOnRowClick="true">
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,CODE,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_ID,ORG_NAME,ORG_DESC,ORG_CODE,LOCATION_ID,LOCATION_NAME,TITLE_ID,TITLE_NAME,STATE_DATE,EFFECTED_DATE,STOCKS_TYPE,STOCKS_TYPE_NAME,PAY_TYPE,PAY_TYPE_NAME,STOCK_DEAL,MONEY_DEAL,PERCENT,MONTH,TIME,NOTE,FILE_NAME,UPLOAD_FILE_NAME,CREATED_DATE"
                        ClientDataKeyNames="ID,EMPLOYEE_ID,CODE,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_ID,ORG_NAME,ORG_DESC,ORG_CODE,LOCATION_ID,LOCATION_NAME,TITLE_ID,TITLE_NAME,STATE_DATE,EFFECTED_DATE,STOCKS_TYPE,STOCKS_TYPE_NAME,PAY_TYPE,PAY_TYPE_NAME,STOCK_DEAL,MONEY_DEAL,PERCENT,MONTH,TIME,NOTE,FILE_NAME,UPLOAD_FILE_NAME,CREATED_DATE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã hồ sơ" DataField="CODE" ItemStyle-HorizontalAlign="Center" SortExpression="CODE" UniqueName="CODE" HeaderStyle-Width="100px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" ItemStyle-CssClass="text-center" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" 
                                HeaderStyle-Width="100px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Họ và tên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="Công ty" DataField="LOCATION_NAME" SortExpression="LOCATION_NAME" UniqueName="LOCATION_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="Ngày nhận việc" DataField="STATE_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="STATE_DATE" UniqueName="STATE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="true" ShowSortIcon="false" HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Ngày hiệu lực" DataField="EFFECTED_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECTED_DATE" UniqueName="EFFECTED_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="true" ShowSortIcon="false" HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Loại cổ phiếu" DataField="STOCKS_TYPE_NAME" SortExpression="STOCKS_TYPE_NAME" UniqueName="STOCKS_TYPE_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="Hình thức chi trả" DataField="PAY_TYPE_NAME" SortExpression="PAY_TYPE_NAME" UniqueName="PAY_TYPE_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="Số lượng cổ phiếu thỏa thuận" DataField="STOCK_DEAL" 
                                            DataFormatString="{0:n0}" SortExpression="STOCK_DEAL" UniqueName="STOCK_DEAL" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="Số tiền quy đổi thỏa thuận" DataField="MONEY_DEAL" 
                                            DataFormatString="{0:n0}" SortExpression="MONEY_DEAL" UniqueName="MONEY_DEAL" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="Chỉ số chiết khấu" DataField="PERCENT" 
                                            DataFormatString="{0:n0}" SortExpression="PERCENT" UniqueName="PERCENT" HeaderStyle-Width="100px" 
                                ItemStyle-CssClass="text-center" />
                            <tlk:GridBoundColumn HeaderText="Thời gian thanh toán thỏa thuận" DataField="MONTH" 
                                            DataFormatString="{0:n2}" SortExpression="MONTH" UniqueName="MONTH" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="Số lần thanh toán thỏa thuận" DataField="TIME"  
                                            DataFormatString="{0:n0}" SortExpression="TIME" UniqueName="TIME" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="File đính kèm" DataField="FILE_NAME" SortExpression="FILE_NAME" UniqueName="FILE_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE" SortExpression="NOTE" UniqueName="NOTE" HeaderStyle-Width="400px"/>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
