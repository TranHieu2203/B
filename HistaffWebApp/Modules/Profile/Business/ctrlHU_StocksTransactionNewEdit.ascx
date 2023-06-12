<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_StocksTransactionNewEditTransactionNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_StocksTransactionNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    #ctl00_MainContent_ctrlHU_StocksTransactionNewEdit_rnMonth_wrapper{
        width: 110px!important;
    }
    #ctl00_MainContent_ctrlHU_StocksTransactionNewEdit_rnTradeMonth_wrapper{
        width: 110px!important;
    }
    #ctl00_MainContent_ctrlHU_StocksTransactionNewEdit_rnProbationMonth_wrapper{
        width: 110px!important;
    }
    #ctl00_MainContent_ctrlHU_StocksTransactionNewEdit_rnPercent_wrapper{
        width: 145px!important;
    }
    #ctl00_MainContent_ctrlHU_StocksTransactionNewEdit_rnTime_wrapper{
        width: 110px!important;
    }
    .text-center{
        text-align: center !important;
    }
    /*#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_StocksTransactionNewEdit_RadPane2{
        height: 100% !important;
    }*/
    .frame{
        position: relative;
        background: #f9f9f9;
        border-top-left-radius: 10px;
        border-top: solid #cccccc 2px;
        border-left: solid #cccccc 2px;
        border-right: solid #cccccc 2px;
        border-top-right-radius: 10px;
        margin-left: 3px;
    }
    .trans-detail{
        position: absolute;
        left: 18px;
        color: gray;
        margin-top: 4px;
        font-size: 14px!important;
    }
    #ctl00_MainContent_ctrlHU_StocksTransactionNewEdit_tbarMain, 
    #ctl00_MainContent_ctrlHU_StocksTransactionNewEdit_tbarMain .rtbRoundedCorners{
        border-top-left-radius: 10px;
        border-top-right-radius: 10px;
    }
    #ctl00_MainContent_ctrlHU_StocksTransactionNewEdit_btnDownload_input{
        padding: 0 2px;
    }
    .complete-status .status{
        color: #73B74F;
    }
    .incomplete-status .status{
        color: #DD5849;
    }
    .RadGrid_Metro tbody tr td {
        border-style: solid;
        border-width: 0 0 1px 1px;
        border-color: #f2f2f2 !important;
    }
</style>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidPayType" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server"  Orientation="Horizontal" Height="330px">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="Y" Height="330px">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCode" runat="server" Text="Mã hồ sơ"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCode" runat="server" CausesValidation="false" AutoPostback="True">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="cboCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Mã hồ sơ. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Mã hồ sơ. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTitle" runat="server" Text="Vị trí công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTileName" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbPercent" runat="server" Text="Chỉ số chiết khấu"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnPercent" runat="server" ReadOnly="true" BackColor="Khaki">
                    </tlk:RadNumericTextBox> %
                </td>
                <td class="lb" style="text-align: left">
                    <asp:Label ID="lbNote" runat="server" Text="Ghi chú"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbEmployeeCode" runat="server" Text="Mã nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDateState" runat="server" Text="Ngày nhận việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDateState" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbMonth" runat="server" Text="Thời gian TT thỏa thuận"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnMonth" runat="server"  ReadOnly="true" SkinID="Decimal" BackColor="Khaki">
                    </tlk:RadNumericTextBox> (tháng) 
                </td>
                <td rowspan="4" colspan="2">
                    <tlk:RadTextBox ID="txtNote" runat="server" Width="500px" Height="113px" SkinID="Textbox1023" ReadOnly="true" BackColor="Khaki">
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
                    <asp:Label ID="lbEffectedDate" runat="server" Text="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectedDate" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadDatePicker> 
                </td>
                <td class="lb">
                    <asp:Label ID="lbTime" runat="server" Text="Số lần TT thỏa thuận"></asp:Label> 
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTime" runat="server" ReadOnly="true" BackColor="Khaki">
                    </tlk:RadNumericTextBox> (lần) 
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
                    <asp:Label ID="lbStockType" runat="server" Text="Loại cổ phiếu"></asp:Label> 
                </td>
                <td>
                    <tlk:RadTextBox ID="txtStockType" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbStockDeal" runat="server" Text="Số lượng CP TT"></asp:Label> 
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnStockDeal" runat="server" ReadOnly="true" BackColor="Khaki">
                    </tlk:RadNumericTextBox> 
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
                    <asp:Label ID="lbPayType" runat="server" Text="Hình thức chi trả"></asp:Label> 
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPayType" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbMoneyDeal" runat="server" Text="Số tiền quy đổi thỏa thuận"></asp:Label> 
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnMoneyDeal" runat="server" ReadOnly="true" BackColor="Khaki">
                    </tlk:RadNumericTextBox> 
                </td>
            </tr>
        </table>
        <div class="frame">
            <p class="trans-detail">Chi tiết giao dịch:</p>
            <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
            <table class="table-form">
                <tr>
                    <td class="lb">
                        <asp:Label ID="Label1" runat="server" Text="Mã giao dịch"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtCode" runat="server" ReadOnly="true" SkinID="Readonly">
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbTradeMonth" runat="server" Text="Số tháng trong lần giao dịch"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rnTradeMonth" runat="server"  ReadOnly="true" BackColor="Khaki">
                        </tlk:RadNumericTextBox> (Tháng)
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbStockPrice" runat="server" Text="Giá CP tại thời điểm giao dịch"></asp:Label><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rnStockPrice" runat="server" AutoPostBack="true">
                        </tlk:RadNumericTextBox> 
                        <asp:RequiredFieldValidator ID="reqStockPrice" ControlToValidate="rnStockPrice" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Giá CP tại thời điểm giao dịch. %>" ToolTip="<%$ Translate: Bạn phải nhập Giá CP tại thời điểm giao dịch. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbStockLeft" runat="server" Text="Số CP trong thời gian còn lại"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rnStockLeft" runat="server"  ReadOnly="true" BackColor="Khaki">
                        </tlk:RadNumericTextBox> 
                    </td>
                    <td class="lb">
                        <asp:Label runat="server" ID="lbUploadFile" Text="<%$ Translate: File đính kèm %>"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server" Width="130px">
                        </tlk:RadTextBox>
                        <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                        </tlk:RadTextBox>
                        <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false"
                            TabIndex="3" />
                        <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>" Enabled="false"
                            CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                        </tlk:RadButton>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label ID="lbTradeDate" runat="server" Text="Ngày giao dịch"></asp:Label><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="rdTradeDate" runat="server">
                        </tlk:RadDatePicker>
                        <asp:RequiredFieldValidator ID="reqTradeDate" ControlToValidate="rdTradeDate" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập Ngày giao dịch. %>" 
                            ToolTip="<%$ Translate: Bạn phải nhập Ngày giao dịch. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:CheckBox ID="chkIsProbation" runat="server" AutoPostback="true" Text="<%$ Translate: Có thử việc %>" /><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rnProbationMonth" runat="server" AutoPostBack="True">
                        </tlk:RadNumericTextBox> (Tháng)
                        <asp:RequiredFieldValidator ID="reqProbationMonth" ControlToValidate="rnProbationMonth" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập Số tháng thử việc. %>" 
                            ToolTip="<%$ Translate: Bạn phải nhập Số tháng thử việc. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbStockFinalPrice" runat="server" Text="Giá CP sau chiết khấu"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rnStockFinalPrice" runat="server" AutoPostBack="True" ReadOnly="true" BackColor="Khaki">
                        </tlk:RadNumericTextBox> 
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbStockTotal" runat="server" Text="Tổng số CP"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rnStockTotal" runat="server" ReadOnly="true"  BackColor="Khaki">
                        </tlk:RadNumericTextBox> 
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label ID="lbPayType2" runat="server" Text="Thời điểm chi trả"></asp:Label><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboPayType2" runat="server" CausesValidation="false">
                        </tlk:RadComboBox>
                        <asp:RequiredFieldValidator ID="reqPayType2" ControlToValidate="cboPayType2"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Thời điểm chi trả. %>"
                            ToolTip="<%$ Translate: Bạn phải chọn Thời điểm chi trả. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label runat="server" ID="lbProbationPercent" Text=""><p style="margin: 0 auto;">% thử việc<span class="lbReq">*</span></p></asp:Label>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboProbationPercent" runat="server" CausesValidation="false" AutoPostback="True">
                        </tlk:RadComboBox>
                        <asp:RequiredFieldValidator ID="reqProbationPercent" ControlToValidate="cboProbationPercent"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn % thử việc. %>"
                            ToolTip="<%$ Translate: Bạn phải chọn % thử việc. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbProbationStock" runat="server" Text="Số CP trong thời gian thử việc"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rnProbationStock" runat="server" ReadOnly="true"  BackColor="Khaki">
                        </tlk:RadNumericTextBox> 
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbStockTotalRound" runat="server" Text="Tổng số CP đã làm tròn"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rnStockTotalRound" runat="server" ReadOnly="true"  BackColor="Khaki">
                        </tlk:RadNumericTextBox> 
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbStatus" runat="server" Text="Trạng thái"></asp:Label><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboStatus" runat="server" CausesValidation="false" AutoPostBack="true">
                        </tlk:RadComboBox>
                        <asp:RequiredFieldValidator ID="reqStatus" ControlToValidate="cboStatus"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Trạng thái. %>"
                            ToolTip="<%$ Translate: Bạn phải chọn Trạng thái. %>"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label ID="lbStockPay" runat="server" Text="Số CP thanh toán"></asp:Label><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rnStockPay" runat="server">
                        </tlk:RadNumericTextBox> 
                        <asp:RequiredFieldValidator ID="reqStockPay" ControlToValidate="rnStockPay"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số CP thanh toán. %>"
                            ToolTip="<%$ Translate: Bạn phải nhập Số CP thanh toán. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbPayDate" runat="server" Text="Ngày thanh toán"></asp:Label><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="rdPayDate" runat="server" >
                        </tlk:RadDatePicker>
                        <asp:RequiredFieldValidator ID="reqPayDate" ControlToValidate="rdPayDate" runat="server"
                            ErrorMessage="<%$ Translate: Bạn phải nhập Ngày thanh toán. %>" 
                            ToolTip="<%$ Translate: Bạn phải nhập Ngày thanh toán. %>"> </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label ID="lbNote2" runat="server" Text="Ghi chú"></asp:Label>
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox ID="txtNote2" runat="server" SkinID="Textbox1023" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <table class="table-form" style="margin-top: 0 !important">
            <tr>
                <td >
                    <tlk:RadSplitter ID="RadSplitter1" runat="server"  Orientation="Horizontal" Scrolling="None" >
                        <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100%">
                            <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Width="99%" Height="100%" AllowPaging="True" AllowFilteringByColumn="true" 
                                AllowSorting="True" AllowMultiRowSelection="true">
                                <ClientSettings EnableRowHoverStyle="true"  EnablePostBackOnRowClick="true">
                                </ClientSettings>
                                <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,STOCK_CODE,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_ID,ORG_NAME,ORG_DESC,ORG_CODE,LOCATION_ID,LOCATION_NAME,TITLE_ID,TITLE_NAME,STOCK_ID,CODE,TRADE_DATE,PAY_TYPE,PAY_TYPE_NAME,STOCK_PRICE,TRADE_MONTH,PROBATION_MONTHS,PROBATION_PERCENT,STOCK_FINAL_PRICE,PROBATION_STOCK,STOCK_LEFT,STOCK_TOTAL,STOCK_TOTAL_ROUND,STOCK_PAY,PAY_DATE,FILE_NAME,UPLOAD_FILE_NAME,NOTE,STATUS,STATUS_NAME,STATUS_CODE"
                                    ClientDataKeyNames="ID,EMPLOYEE_ID,STOCK_CODE,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_ID,ORG_NAME,ORG_DESC,ORG_CODE,LOCATION_ID,LOCATION_NAME,TITLE_ID,TITLE_NAME,STOCK_ID,CODE,TRADE_DATE,PAY_TYPE,PAY_TYPE_NAME,STOCK_PRICE,TRADE_MONTH,PROBATION_MONTHS,PROBATION_PERCENT,STOCK_FINAL_PRICE,PROBATION_STOCK,STOCK_LEFT,STOCK_TOTAL,STOCK_TOTAL_ROUND,STOCK_PAY,PAY_DATE,FILE_NAME,UPLOAD_FILE_NAME,NOTE,STATUS,STATUS_NAME,STATUS_CODE">
                                    <Columns>
                                        <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                        </tlk:GridClientSelectColumn>
                                        <tlk:GridBoundColumn DataField="ID" Visible="false" />
                                        <tlk:GridBoundColumn HeaderText="Mã giao dịch" DataField="CODE" ItemStyle-CssClass="text-center" SortExpression="CODE" UniqueName="CODE" HeaderStyle-Width="150px">
                                        </tlk:GridBoundColumn>
                                        <tlk:GridDateTimeColumn HeaderText="Ngày giao dịch" DataField="TRADE_DATE"
                                            ItemStyle-HorizontalAlign="Center" SortExpression="TRADE_DATE" UniqueName="TRADE_DATE"
                                            DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="true" ShowSortIcon="false" HeaderStyle-Width="100px">
                                        </tlk:GridDateTimeColumn>
                                        <tlk:GridBoundColumn HeaderText="Trạng thái" ItemStyle-CssClass="status" 
                                            DataField="STATUS_NAME" SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" HeaderStyle-Width="150px"/>
                                        <tlk:GridBoundColumn HeaderText="Thời điểm chi trả" DataField="PAY_TYPE_NAME" SortExpression="PAY_TYPE_NAME" UniqueName="PAY_TYPE_NAME" HeaderStyle-Width="100px"/>
                                        <tlk:GridBoundColumn HeaderText="Giá CP tại thời điểm GD" DataField="STOCK_PRICE" 
                                            DataFormatString="{0:n0}" SortExpression="STOCK_PRICE" UniqueName="STOCK_PRICE" HeaderStyle-Width="100px"/>
                                        <tlk:GridBoundColumn HeaderText="Số tháng trong lần GD" DataField="TRADE_MONTH" 
                                            DataFormatString="{0:n0}" SortExpression="TRADE_MONTH" UniqueName="TRADE_MONTH" HeaderStyle-Width="100px"/>
                                        <tlk:GridBoundColumn HeaderText="Có thử việc" DataField="PROBATION_MONTHS" SortExpression="PROBATION_MONTHS" 
                                            UniqueName="PROBATION_MONTHS" HeaderStyle-Width="100px"/>
                                        <tlk:GridBoundColumn HeaderText="% Thử việc" DataField="PROBATION_PERCENT" SortExpression="PROBATION_PERCENT" UniqueName="PROBATION_PERCENT" 
                                            ItemStyle-CssClass="text-center" HeaderStyle-Width="100px"/>
                                        <tlk:GridBoundColumn HeaderText="Giá CP sau chiết khấu" DataField="STOCK_FINAL_PRICE" 
                                            DataFormatString="{0:n0}" SortExpression="STOCK_FINAL_PRICE" UniqueName="STOCK_FINAL_PRICE" HeaderStyle-Width="100px"/>
                                        <tlk:GridBoundColumn HeaderText="Số CP trong thời gian TV" 
                                            DataFormatString="{0:n0}" DataField="PROBATION_STOCK" SortExpression="PROBATION_STOCK" UniqueName="PROBATION_STOCK" HeaderStyle-Width="100px"/>
                                        <tlk:GridBoundColumn HeaderText="Số CP trong thời gian còn lại" 
                                            DataFormatString="{0:n0}" DataField="STOCK_LEFT" SortExpression="STOCK_LEFT" UniqueName="STOCK_LEFT" HeaderStyle-Width="100px"/>
                                        <tlk:GridBoundColumn HeaderText="Tổng số cổ phiếu" DataField="STOCK_TOTAL" 
                                            DataFormatString="{0:n0}" SortExpression="STOCK_TOTAL" UniqueName="STOCK_TOTAL" HeaderStyle-Width="100px"/>
                                        <tlk:GridBoundColumn HeaderText="Tổng số CP đã làm tròn" DataField="STOCK_TOTAL_ROUND" 
                                            DataFormatString="{0:n0}" SortExpression="STOCK_TOTAL_ROUND" UniqueName="STOCK_TOTAL_ROUND" HeaderStyle-Width="100px"/>
                                        <tlk:GridBoundColumn HeaderText="Số CP thanh toán" DataField="STOCK_PAY" 
                                            DataFormatString="{0:n0}" SortExpression="STOCK_PAY" UniqueName="STOCK_PAY" HeaderStyle-Width="100px"/>
                                        <tlk:GridDateTimeColumn HeaderText="Ngày thanh toán" DataField="PAY_DATE"
                                            ItemStyle-HorizontalAlign="Center" SortExpression="PAY_DATE" UniqueName="PAY_DATE"
                                            DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="true" ShowSortIcon="false" HeaderStyle-Width="100px">
                                        </tlk:GridDateTimeColumn>
                                        <tlk:GridBoundColumn HeaderText="File đính kèm" DataField="FILE_NAME" SortExpression="FILE_NAME" UniqueName="FILE_NAME" HeaderStyle-Width="150px"/>
                                        <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE" SortExpression="NOTE" UniqueName="NOTE" HeaderStyle-Width="150px"/>
                                    </Columns>
                                </MasterTableView>
                            </tlk:RadGrid>
                        </tlk:RadPane>
                    </tlk:RadSplitter>
                </td>
            </tr>
        </table>
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
