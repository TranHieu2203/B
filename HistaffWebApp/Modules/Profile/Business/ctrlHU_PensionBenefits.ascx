<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_PensionBenefits.ascx.vb"
    Inherits="Profile.ctrlHU_PensionBenefits" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarTerminates" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="68px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbFromSend" runat="server" Text = "Ngày nộp đơn từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromSend" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbToSend" runat="server" Text = "Đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToSend" runat="server">
                            </tlk:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdToSend"
                                Type="Date" ControlToCompare="rdFromSend" Operator="GreaterThanEqual" ErrorMessage="Đến ngày nộp đơn phải lớn hơn Từ ngày nộp đơn"
                                ToolTip="Đến ngày nộp đơn phải lớn hơn Từ ngày nộp đơn"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbFromLast" runat="server" Text = "Ngày làm việc cuối cùng từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromLast" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">                         
                            <asp:Label ID="lbToLast" runat="server" Text = "Đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToLast" runat="server">
                            </tlk:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="rdToLast"
                                Type="Date" ControlToCompare="rdFromLast" Operator="GreaterThanEqual" ErrorMessage="Đến ngày làm việc cuối cùng phải lớn hơn Từ ngày làm việc cuối cùng"
                                ToolTip="Đến ngày làm việc cuối cùng phải lớn hơn Từ ngày làm việc cuối cùng"></asp:CompareValidator>
                        
                        </td>
                        <td>
                            <tlk:RadButton runat="server" Text="Tìm" ID="btnSearch" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgTerminate" runat="server" Height="100%" AllowMultiRowSelection="true" Width="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                          <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3"/>
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,TYPE_TERMINATE,CODE,ORG_DESC" ClientDataKeyNames="ID,STATUS_CODE,IS_NOHIRE,STATUS_ID">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" HeaderText="ID"/>
                            <tlk:GridBoundColumn DataField="STATUS_CODE" Visible="false" HeaderText="STATUS_CODE" />
                            <tlk:GridBoundColumn DataField="IS_NOHIRE" Visible="false" HeaderText="IS_NOHIRE" />
                            <tlk:GridBoundColumn DataField="ORG_DESC" Visible="false" HeaderText="ORG_DESC" />
                            <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                                SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày vào" DataField="JOIN_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="JOIN_DATE" UniqueName="JOIN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị/Phòng ban" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />                  
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày nộp đơn" DataField="SEND_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="SEND_DATE" UniqueName="SEND_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày nghỉ thực tế" DataField="LAST_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="LAST_DATE" UniqueName="LAST_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày phê duyệt nghỉ" DataField="APPROVAL_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="APPROVAL_DATE" UniqueName="APPROVAL_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="Số phép còn lại" DataField="REMAINING_LEAVE"
                                SortExpression="REMAINING_LEAVE" UniqueName="REMAINING_LEAVE" />
                            <tlk:GridBoundColumn HeaderText="Số ngày nghỉ bù còn lại" DataField="COMPENSATORY_LEAVE"
                                SortExpression="COMPENSATORY_LEAVE" UniqueName="COMPENSATORY_LEAVE" />
                            <tlk:GridBoundColumn HeaderText="Tiền thanh toán phép" DataField="PAYMENT_LEAVE"
                                SortExpression="PAYMENT_LEAVE" UniqueName="PAYMENT_LEAVE" DataFormatString="{0:N0}" />
                             <tlk:GridBoundColumn HeaderText="Lương trung bình 6 tháng" DataField="SALARYMEDIUM"
                                SortExpression="SALARYMEDIUM" UniqueName="SALARYMEDIUM" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="Thời gian tham gia BHTN" DataField="SALARYMEDIUM_LOSS"
                                SortExpression="SALARYMEDIUM_LOSS" UniqueName="SALARYMEDIUM_LOSS" />
                            <tlk:GridBoundColumn HeaderText="Số năm tính trợ cấp mất việc" DataField="YEARFORALLOW"
                                SortExpression="YEARFORALLOW" UniqueName="YEARFORALLOW" />
                            <tlk:GridBoundColumn HeaderText="Số tiền còn lại" DataField="MONEY_RETURN"
                                SortExpression="MONEY_RETURN" UniqueName="MONEY_RETURN" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="Phạt thời hạn báo trước" DataField="AMOUNT_VIOLATIONS"
                                SortExpression="AMOUNT_VIOLATIONS" UniqueName="AMOUNT_VIOLATIONS" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="Phạt chấm dứt trái luật" DataField="AMOUNT_WRONGFUL"
                                SortExpression="AMOUNT_WRONGFUL" UniqueName="AMOUNT_WRONGFUL" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="Tiền thanh toán nghỉ bù" DataField="COMPENSATORY_PAYMENT"
                                SortExpression="COMPENSATORY_PAYMENT" UniqueName="COMPENSATORY_PAYMENT" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="Trợ cấp thôi việc" DataField="ALLOWANCE_TERMINATE"
                                SortExpression="ALLOWANCE_TERMINATE" UniqueName="ALLOWANCE_TERMINATE" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="Chi phí đào tạo" DataField="TRAINING_COSTS"
                                SortExpression="TRAINING_COSTS" UniqueName="TRAINING_COSTS" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="Bồi thường khác" DataField="OTHER_COMPENSATION"
                                SortExpression="OTHER_COMPENSATION" UniqueName="OTHER_COMPENSATION" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>--%>
                        </Columns>
                        <HeaderStyle Width="120px" />
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
                            <tlk:RadComboBox runat="server" Width = "400px" ID="cboPrintSupport">
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
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" OnClientClose="popupclose"
            Width="1000" Height="600px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }
        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_SeveranceSettlement_RadSplitter1');
        }

        function OpenNew() {
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_SeveranceSettlementNewEdit&group=Business&FormType=0', "_self"); /*
            var pos = $("html").offset();
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
        }

        function OpenEdit() {
            var id = $find('<%= rgTerminate.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_SeveranceSettlementNewEdit&group=Business&FormType=1&ID=' + id, "_self"); /*
            var pos = $("html").offset();
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
        }

        function OpenView() {
            var id = $find('<%= rgTerminate.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_SeveranceSettlementNewEdit&group=Business&FormType=2&ID=' + id, "_self"); /*
            var pos = $("html").offset();
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
        }

        function gridRowDblClick(sender, eventArgs) {
            var bCheck = CheckValidate();
            var n;
            var m;
            if (bCheck != 1) {
                OpenEdit();
            }
            if (bCheck == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }

        }

        function clientButtonClicking(sender, args) {
            var bCheck;
            var n;
            var m;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'PRINT' || args.get_item().get_commandName() == 'KTK') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = CheckValidate();
                if (bCheck != 1) {
                    OpenEdit();
                }
                if (bCheck == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                }
                args.set_cancel(true);
            }

            if (args.get_item().get_commandName() == "DELETE") {
                bCheck = CheckValidate();

                if (bCheck == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }

                if (bCheck == 2) {
                    m = '<%= Translate("Bản ghi đang ở trạng thái phê duyệt không thể xóa. Thao tác thực hiện không thành công.") %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }

                if (bCheck == -1) {
                    m = '<%= Translate("Bản ghi đang ở trạng thái không phê duyệt không thể xóa. Thao tác thực hiện không thành công.") %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }
            }
            if (args.get_item().get_commandName() == "PRINT_BBBG" ||
            args.get_item().get_commandName() == "PRINT_BBTL" ||
            args.get_item().get_commandName() == "PRINT_QD") {
                var bCheck = $find('<%= rgTerminate.ClientID %>').get_masterTableView().get_selectedItems().length;
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
        }

        function CheckValidate() {
            var bCheck = $find('<%= rgTerminate.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }

            var status_code = $find('<%= rgTerminate.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_CODE');
            if (status_code == 1) {
                return 2;
            }

            var status_id = $find('<%= rgTerminate.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_ID');
            if (status_id == 264) {
                return -1;
            }
            return 0;
        }

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $get("<%= btnSearch.ClientId %>").click();
            }
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function btnPrintSupportClick(sender, args) {
            var bCheck = $find('<%= rgTerminate.ClientID %>').get_masterTableView().get_selectedItems().length;
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


    </script>
</tlk:RadCodeBlock>
