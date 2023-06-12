<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_StocksTransaction.ascx.vb"
    Inherits="Profile.ctrlHU_StocksTransaction" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .text-center{
        text-align: center !important;
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
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%" AllowPaging="True" AllowFilteringByColumn="true"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true"  EnablePostBackOnRowClick="true">
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
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
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" ItemStyle-CssClass="text-center" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" 
                                HeaderStyle-Width="100px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Họ và tên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="Mã hồ sơ" DataField="STOCK_CODE" ItemStyle-HorizontalAlign="Center" SortExpression="STOCK_CODE" UniqueName="STOCK_CODE" HeaderStyle-Width="100px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Ngày giao dịch" DataField="TRADE_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="TRADE_DATE" UniqueName="TRADE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="true" ShowSortIcon="false" HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
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
                            <tlk:GridBoundColumn HeaderText="Ngày thanh toán" DataField="PAY_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="PAY_DATE" UniqueName="PAY_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="true" ShowSortIcon="false" HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="File đính kèm" DataField="FILE_NAME" SortExpression="FILE_NAME" UniqueName="FILE_NAME" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE" SortExpression="NOTE" UniqueName="NOTE" HeaderStyle-Width="300px"/>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
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
            } else if (item.get_commandName() == "CREATE") {
                var items = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems();
                if (items.length > 0) {
                    var id = $find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                    window.open('/Default.aspx?mid=Profile&fid=ctrlHU_StocksTransactionNewEdit&group=Business&ID=' + id + '', "_self");
                    args.set_cancel(true);
                    return;
                }
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_StocksTransactionNewEdit&group=Business&ID=' + 0 + '', "_self");
                args.set_cancel(true);
                return;
            }
        }
        function gridRowDblClick(sender, eventArgs) {
            var items = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems();
            if (items.length > 0) {
                var id = $find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_StocksTransactionNewEdit&group=Business&ID=' + id + '', "_self");
                args.set_cancel(true);
                return;
            }
        }
    </script>
</tlk:RadCodeBlock>
