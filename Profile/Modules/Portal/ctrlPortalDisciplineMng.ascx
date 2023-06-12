<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalDisciplineMng.ascx.vb"
    Inherits="Profile.ctrlPortalDisciplineMng" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .rcbAutoWidthResizer .rcbScroll
    {
        width: 397px !important;
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
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarDisciplines" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="38px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực từ")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>

                        <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>

                    </tr>
                    <tr>
                        <td>
                        </td>
                        
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgDiscipline" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,STATUS_ID,SIGNER_NAME,SIGNER_TITLE,SIGN_DATE" ClientDataKeyNames="ID,EMPLOYEE_ID,STATUS_ID,SIGNER_NAME,SIGNER_TITLE,SIGN_DATE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Ban/Phòng %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" /> 
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                ItemStyle-HorizontalAlign="Center" SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng %>" DataField="DISCIPLINE_OBJ_NAME"
                                SortExpression="DISCIPLINE_OBJ_NAME" UniqueName="DISCIPLINE_OBJ_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do kỷ luật %>" DataField="DISCIPLINE_REASON_NAME"
                                SortExpression="DISCIPLINE_REASON_NAME" UniqueName="DISCIPLINE_REASON_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức kỷ luật %>" DataField="DISCIPLINE_TYPE_NAME"
                                SortExpression="DISCIPLINE_TYPE_NAME" UniqueName="DISCIPLINE_TYPE_NAME" />

                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm kỷ luật %>" DataField="YEAR"
                                SortExpression="YEAR" UniqueName="YEAR" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp kỷ luật %>" DataField="LEVEL_NAME"
                                SortExpression="LEVEL_NAME" UniqueName="LEVEL_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày vi phạm %>" DataField="VIOLATION_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="VIOLATION_DATE" UniqueName="VIOLATION_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />  
                            <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng giá trị thiệt hại %>" DataField="INDEMNIFY_MONEY" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}" SortExpression="INDEMNIFY_MONEY" UniqueName="INDEMNIFY_MONEY" />--%>
                           <%-- <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền phạt %>" DataField="MONEY" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}" SortExpression="MONEY" UniqueName="MONEY" />                            
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số tiền phải nộp %>" DataField="PAIDMONEY" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}" SortExpression="PAIDMONEY" UniqueName="PAIDMONEY" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền đã nộp %>" DataField="AMOUNT_PAID_CASH" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}" SortExpression="AMOUNT_PAID_CASH" UniqueName="AMOUNT_PAID_CASH" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền còn phải nộp %>" DataField="AMOUNT_TO_PAID" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}" SortExpression="AMOUNT_TO_PAID" UniqueName="AMOUNT_TO_PAID" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kỳ lương bắt đầu trừ %>" DataField="PERIOD_NAME"
                                ItemStyle-HorizontalAlign="Center" SortExpression="PERIOD_NAME" UniqueName="PERIOD_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tỷ lệ % trừ mỗi tháng %>" DataField="AMOUNT_IN_MONTH" ItemStyle-HorizontalAlign="Right"
                               SortExpression="AMOUNT_IN_MONTH" UniqueName="AMOUNT_IN_MONTH" DataFormatString="{0:n0}"/>  
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền trừ mỗi tháng %>" DataField="AMOUNT_DEDUCT_AMOUNT" ItemStyle-HorizontalAlign="Right"
                              SortExpression="AMOUNT_DEDUCT_AMOUNT" UniqueName="AMOUNT_DEDUCT_AMOUNT" DataFormatString="{0:n0}"/> --%> 
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                                SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />                               
                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Người ký %>" DataField="SIGNER_NAME"
                                SortExpression="SIGNER_NAME" UniqueName="SIGNER_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh người ký %>" DataField="SIGNER_TITLE"
                                SortExpression="SIGNER_TITLE" UniqueName="SIGNER_TITLE" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày ký %>" DataField="SIGN_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="SIGN_DATE" UniqueName="SIGN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />  
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người tạo %>" DataField="CREATED_BY"
                                SortExpression="CREATED_BY" UniqueName="CREATED_BY" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày tạo %>" DataField="CREATED_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="CREATED_DATE" UniqueName="CREATED_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" /> 
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người cập nhật %>" DataField="MODIFIED_BY"
                                SortExpression="MODIFIED_BY" UniqueName="MODIFIED_BY" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cập nhật %>" DataField="MODIFIED_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="MODIFIED_DATE" UniqueName="MODIFIED_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" /> 
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None" Height="1px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)" style="display:none">
                    <tr>
                        <td class="lb">
                            <%# Translate("Biễu mẫu hỗ trợ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" Width="400px" ID="cboPrintSupport">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnPrintSupport" runat="server" Text="<%$ Translate: Hỗ trợ in %>"
                                OnClientClicking="btnPrintSupportClick" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="popupclose" Height="550px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function OpenNew() {
            window.open('/Default.aspx?mid=Profile&fid=ctrlPortalDisciplineNewEditMng&FormType=0&noscroll=1', "_self")
            //            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlPortalDisciplineMngNewEdit&group=Business&FormType=0&noscroll=1', "rwPopup");
            //            var pos = $("html").offset();
            //            oWindow.setSize(1020, $(window).height());
            //            oWindow.center();
        }

        function OpenEdit() {
            var bCheck = $find('<%= rgDiscipline.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%= rgDiscipline.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Profile&fid=ctrlPortalDisciplineNewEditMng&noscroll=1&FormType=1&ID=' + id, "_self")
            //            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlPortalDisciplineMngNewEdit&group=Business&noscroll=1&FormType=1&ID=' + id, "rwPopup");
            //            var pos = $("html").offset();
            //            oWindow.setSize(1020, $(window).height());
            //            oWindow.center();
            //            return 2;
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                }
                if (bCheck == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                }

                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "PRINT_VPCT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "PRINT_BKS") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'DELETE' || args.get_item().get_commandName() == 'DEACTIVE') {
                bCheck = $find('<%= rgDiscipline.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }


            }
        }
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function gridRowDblClick(sender, eventArgs) {
            var bCheck = OpenEdit();
            if (bCheck == 0) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }
            if (bCheck == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }

        }

        function popupclose(sender, args) {
            var arg = args.get_argument();
            if (arg == '1') {
                var m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $get("<%= btnSearch.ClientId %>").click();
            }
        }


        function btnPrintSupportClick(sender, args) {
            var bCheck = $find('<%= rgDiscipline.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                args.set_cancel(true);
                return;
            }
            if (bCheck > 1) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                args.set_cancel(true);
                return;
            }
            enableAjax = false;
        }

        var winH;
            var winW;

            function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() - 210;
            winW = $(window).width() - 90;
            $("#ctl00_MainContent_ctrlPortalDisciplineMng_RadSplitter1").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlPortalDisciplineMng_RadSplitter3").stop().animate({ height: winH }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPortalDisciplineMng_MainPane").stop().animate({ height: winH }, 0);
           $("#ctl00_MainContent_ctrlPortalDisciplineMng_rgData").stop().animate({ height: winH-76 }, 0);
             $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPortalDisciplineMng_RadPane2").stop().animate({ height: winH - 72 }, 0);
             $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPortalDisciplineMng_LeftPane").stop().animate({ height: winH }, 0);
            
            Sys.Application.add_load(SizeToFitMain);
        }

            SizeToFitMain();

            $(document).ready(function () {
                SizeToFitMain();
            });
            $(window).resize(function () {
                SizeToFitMain();
            });

    </script>
</tlk:RadCodeBlock>
