<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlINS_ReportList.ascx.vb"
    Inherits="Insurance.ctrlINS_ReportList" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
<tlk:RadPane ID="LeftPane" runat="server" Scrolling="None" Width="300px">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="75px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" />
                <table class="table-form padding-10" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <label id="lblFromDate" runat="server" visible="false"><%# Translate("Từ ngày")%></label>
                            <label id="lblMonth" runat="server"><%# Translate("Tháng")%></label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server" Visible="false"></tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqFromdate" ControlToValidate="rdFromDate" Enabled="false"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải Từ ngày %>" 
                                ToolTip="<%$ Translate: Bạn phải nhập Từ ngày. %>"> 
                            </asp:RequiredFieldValidator>
                            <tlk:RadComboBox ID="cboMonth" runat="server" Width="60px"></tlk:RadComboBox>
                            <asp:CustomValidator ID="customValMonth" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn tháng. %>"
                                ToolTip="<%$ Translate: Bạn phải chọn tháng. %>" 
                                ClientValidationFunction="cusMonth">
                            </asp:CustomValidator>                            
                        </td>
                        <td class="lb">
                            <label id="lblToDate" runat="server" visible="false"><%# Translate("Đến ngày")%></label>
                            <label id="lblYear" runat="server"><%# Translate("Năm")%></label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server" Visible="false"></tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqToDate" ControlToValidate="rdToDate" Enabled="false"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải Đến ngày %>" 
                                ToolTip="<%$ Translate: Bạn phải nhập Đến ngày. %>"> 
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="compareValDate" runat="server" Enabled="false"
                                ToolTip="<%$ Translate: Từ ngày phải nhỏ hơn Đến ngày %>"
                                ErrorMessage="<%$ Translate: Từ ngày phải nhỏ hơn Đến ngày %>" Type="Date" Operator="GreaterThan"
                                ControlToCompare="rdFromDate" ControlToValidate="rdToDate"></asp:CompareValidator>
                            <tlk:RadComboBox ID="cboYear" runat="server" Width="60px"></tlk:RadComboBox>
                            <asp:CustomValidator ID="customValYear" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn năm. %>"
                                ToolTip="<%$ Translate: Bạn phải chọn năm. %>" 
                                ClientValidationFunction="cusYear">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    Height="100%" AllowSorting="True" AllowMultiRowSelection="false" AllowFilteringByColumn="false">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME">
                        <Columns>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã báo cáo %>" DataField="CODE"
                                UniqueName="CODE" SortExpression="CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên báo cáo %>" DataField="NAME"
                                UniqueName="NAME" SortExpression="NAME" />                            
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlINS_ReportList_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlINS_ReportList_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlINS_ReportList_RadPane2';
        var validateID = 'MainContent_ctrlINS_ReportList_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

        $(document).ready(function () {
            registerOnfocusOut(splitterID);        
        });

        function cusMonth(oSrc, args) {
            var cbo = $find("<%# cboMonth.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusYear(oSrc, args) {
            var cbo = $find("<%# cboYear.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');                    
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);                                      
                }
                enableAjax = false;
            }
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        $(document).ready(function () {
            var masterTable = $find("<%=rgDanhMuc.ClientID%>").get_masterTableView();
            masterTable.selectItem(0)
        });
    </script>
</tlk:RadCodeBlock>