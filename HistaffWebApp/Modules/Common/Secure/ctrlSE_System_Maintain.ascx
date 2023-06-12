<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSE_System_Maintain.ascx.vb"
    Inherits="Common.ctrlSE_System_Maintain" %>
<%@ Import Namespace="Common" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="180px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMainToolBar" runat="server">
        </tlk:RadToolBar>
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr >
                <td class="lb" >
                    <%# Translate("Số hợp đồng")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rtCONTRACT_MAINTAIN" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdEFFECT_DATE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số hợp đồng. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập số hợp đồng. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEFFECT_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEFFECT_DATE" ControlToValidate="rdEFFECT_DATE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td> 
                    <tlk:RadDatePicker ID="rdEXPIRE_TDATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdEXPIRE_TDATE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hết hiệu lực. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày hết hiệu lực. %>"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdEXPIRE_TDATE"
                        Type="Date" ControlToCompare="rdEFFECT_DATE" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày bắt đầu %>"
                        ToolTip="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày bắt đầu %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày ký")%><span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSIGN_DATE" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdSIGN_DATE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày ký. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày ký. %>"></asp:RequiredFieldValidator>--%>
                </td>
                
            </tr>
            <tr >
                <td class="lb" >
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" runat="server" Width="100%" SkinID="Textbox1023">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgListFunctions" runat="server" Height="100%">
            <MasterTableView ClientDataKeyNames="CONTRACT_MAINTAIN,EFFECT_DATE,EXPIRE_TDATE,SIGN_DATE,NOTE" DataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hợp đồng bảo trì %>" DataField="CONTRACT_MAINTAIN"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="CONTRACT_MAINTAIN"
                        SortExpression="CONTRACT_MAINTAIN" ReadOnly="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_TDATE"
                        UniqueName="EXPIRE_TDATE" SortExpression="EXPIRE_TDATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày ký %>" DataField="SIGN_DATE"
                        UniqueName="SIGN_DATE" SortExpression="SIGN_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="NOTE"
                        SortExpression="NOTE" ReadOnly="true">
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="450px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="200px" EnableShadow="true" Behaviors="Close, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<script type="text/javascript">
    var enableAjax = true;
    var oldSize = 0;
    function OnClientClose(oWnd, args) {
        var m;
        var arg = args.get_argument();
        //debugger;
        if (arg == '1') {
            var toolBar = $find("<%=rtbMainToolBar.ClientID%>");
            var button = toolBar.findItemByText("Lưu");
            button.click();
        }
    }
    function popupclose(sender, args) {
        var m;
        var arg = args.get_argument();
        if (arg == '1') {
            m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
            var n = noty({ text: m, dismissQueue: true, type: 'success' });
            setTimeout(function () { $.noty.close(n.options.id); }, 5000);
        }
    }
    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (item.get_commandName() == "EXPORT") {
            enableAjax = false;
        } else if (item.get_commandName() == "SAVE") {
            // Nếu nhấn nút SAVE thì resize
            ResizeSplitter();
        } else {
            // Nếu nhấn các nút khác thì resize default
            ResizeSplitterDefault();
        }
    }

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }

    // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
    function ResizeSplitter() {
        setTimeout(function () {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
            var height = pane.getContentElement().scrollHeight;
            splitter.set_height(splitter.get_height() + pane.get_height() - height);
            pane.set_height(height);
        }, 200);
    }

    // Hàm khôi phục lại Size ban đầu cho Splitter
    function ResizeSplitterDefault() {
        var splitter = $find("<%= RadSplitter3.ClientID%>");
        var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
        if (oldSize == 0) {
            oldSize = pane.getContentElement().scrollHeight;
        } else {

            var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
            splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
            pane.set_height(oldSize);
            pane2.set_height(splitter.get_height() - oldSize - 1);
        }
    }
</script>
