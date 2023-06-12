<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Travel.ascx.vb"
    Inherits="Profile.ctrlHU_Travel" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmp" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="230px" Scrolling="None">
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="item-head" colspan="6">
                            <b>
                                <%# Translate("Thông tin NV")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb" style="width: 200px">
                            <asp:Label ID="lbEmployeeCode" runat="server" Text="<%$ Translate: Mã NV %>"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="130px" AutoPostBack="false">
                                <ClientEvents OnKeyPress="OnKeyPress" />
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                                Width="40px">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập NV. %>" ToolTip="<%$ Translate: Bạn phải nhập NV. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="width: 200px">
                            <asp:Label ID="lbEmployeeName" runat="server" Text="<%$ Translate: Tên NV %>"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb" style="width: 200px">
                            <asp:Label ID="lbTITLE" runat="server" Text="<%$ Translate: Vị trí công việc %>"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTitleName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbOrg_Name" runat="server" Text="<%$ Translate: Đơn vị %>"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Translate: Mã thẻ %>"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtMaThe" runat="server" ReadOnly="True" SkinID="ReadOnly">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="item-head" colspan="6">
                            <b>
                                <%# Translate("Thông tin tàu xe") %></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="Label3" runat="server" Text="<%$ Translate: Từ ngày %>"></asp:Label><span
                                class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdFromDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Từ ngày. %>" ToolTip="<%$ Translate: Bạn phải nhập Từ ngày. %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label4" runat="server" Text="<%$ Translate: Đến ngày %>"></asp:Label><span
                                class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdToDate"
                                Type="Date" ControlToCompare="rdFromDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"
                                ToolTip="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"></asp:CompareValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label2" runat="server" Text="<%$ Translate: Số ngày đi đường %>"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnDaynum" runat="server" SkinID="Decimal" NumberFormat-AllowRounding="false">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnDaynum"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số ngày đi đường. %>" ToolTip="<%$ Translate: Bạn phải nhập Số ngày đi đường. %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="Label5" runat="server" Text="<%$ Translate: Nơi đến %>"></asp:Label>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtPlaceName" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Translate: Chi phí %>"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnMoney" runat="server" SkinID="Decimal" NumberFormat-AllowRounding="false">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbRemark" runat="server" Text="<%$ Translate: Ghi chú %>"></asp:Label>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtRemark" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,TITLE_NAME,ORG_NAME,MATHE_NAME,FROM_DATE,TO_DATE,DAY_NUM,PLACE_NAME,MONEY,REMARK">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã NV" DataField="EMPLOYEE_CODE" HeaderStyle-Width="100px"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Tên NV" DataField="EMPLOYEE_NAME" HeaderStyle-Width="100px"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" HeaderStyle-Width="140px"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                                HeaderStyle-Width="130px" SortExpression="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Mã thẻ" DataField="MATHE_NAME" UniqueName="MATHE_NAME" HeaderStyle-Width="100px"
                                SortExpression="MATHE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Từ ngày" DataField="FROM_DATE" UniqueName="FROM_DATE"
                                HeaderStyle-Width="100px" SortExpression="FROM_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="Đến ngày" DataField="TO_DATE" UniqueName="TO_DATE"
                                HeaderStyle-Width="100px" SortExpression="TO_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <tlk:GridNumericColumn HeaderText="Số ngày đi đường" DataField="DAY_NUM" UniqueName="DAY_NUM"
                                HeaderStyle-Width="100px" SortExpression="DAY_NUM" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="Nơi đến" DataField="PLACE_NAME" UniqueName="PLACE_NAME"
                                HeaderStyle-Width="150px" SortExpression="PLACE_NAME" />
                            <tlk:GridNumericColumn HeaderText="Chi phí" DataField="MONEY" UniqueName="MONEY"
                                HeaderStyle-Width="100px" SortExpression="MONEY" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" UniqueName="REMARK"
                                HeaderStyle-Width="150px" SortExpression="REMARK" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                var rows = $find('<%= rgMain.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {

            } else if (args.get_item().get_commandName() == "EDIT") {
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var l = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var k = noty({ text: l, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(k.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else {

            }
        }
        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_Travel_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
