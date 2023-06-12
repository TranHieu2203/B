<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlActionLog.ascx.vb"
    Inherits="Common.ctrlActionLog" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%" orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="rtbActionLog" runat="server">
        </tlk:RadToolBar>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="150px">
        <Common:ctrlMessageBox ID="MessageBox" runat="server" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="7">
                        <%# Translate("Tìm")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm chức năng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboFunctionGroup" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Tên chức năng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboFunctionName" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Thao tác")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboActionName" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Platform")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPlatform" runat="server">
                        <Items>
                            <tlk:radcomboboxitem runat="server" text="" Value="0" />
                            <tlk:radcomboboxitem runat="server" text="App" Value="1"/>
                            <tlk:radcomboboxitem runat="server" text="Portal" Value="2" />
                            <tlk:radcomboboxitem runat="server" text="Mobile" Value="3" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm người dùng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboUserGroup" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Tài khoản")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUser" runat="server" MaxLength="255">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Địa chỉ IP")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtIP" runat="server" MaxLength="255">
                    </tlk:RadTextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDateTimePicker ID="rdFromDate" runat="server">
                        <DateInput DateFormat="dd/MM/yyyy hh:mm tt">
                        </DateInput>
                        <Calendar CultureInfo="vi-VN">
                            <SpecialDays>
                                <tlk:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                </tlk:RadCalendarDay>
                            </SpecialDays>
                        </Calendar>
                    </tlk:RadDateTimePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDateTimePicker ID="rdToDate" runat="server">
                        <DateInput DateFormat="dd/MM/yyyy hh:mm tt">
                        </DateInput>
                        <Calendar CultureInfo="vi-VN">
                            <SpecialDays>
                                <tlk:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                </tlk:RadCalendarDay>
                            </SpecialDays>
                        </Calendar>
                    </tlk:RadDateTimePicker>
                </td>
                <td class="lb">
                    <%# Translate("Tên máy")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtComputerName" runat="server" MaxLength="255">
                    </tlk:RadTextBox>
                </td>
                <td style="padding-left: 10px;">
                    <tlk:RadButton ID="btnFIND" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgActionLog" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="Id" ReadOnly="true" Visible="false" HeaderText="<%$ Translate:Id %>"
                        SortExpression="Id" />
                    <tlk:GridBoundColumn DataField="Username" ReadOnly="true" HeaderText="<%$ Translate:Tên tài khoản %>"
                        SortExpression="Username" />
                    <tlk:GridBoundColumn DataField="Fullname" ReadOnly="true" HeaderText="<%$ Translate:Họ và tên %>"
                        SortExpression="Fullname" />
                    <tlk:GridBoundColumn DataField="Email" ReadOnly="true" HeaderText="<%$ Translate:Email %>"
                        SortExpression="Email" />
                    <tlk:GridBoundColumn DataField="Mobile" ReadOnly="true" HeaderText="<%$ Translate:Mobile %>"
                        SortExpression="Mobile" />
                    <tlk:GridBoundColumn DataField="ViewGroup" ReadOnly="true" HeaderText="<%$ Translate:Nhóm chức năng %>"
                        SortExpression="ViewGroup" />
                    <tlk:GridBoundColumn DataField="ViewDescription" ReadOnly="true" HeaderText="<%$ Translate:Tên chức năng %>"
                        SortExpression="ViewDescription" />
                    <tlk:GridBoundColumn DataField="ActionName" ReadOnly="true" HeaderText="<%$ Translate:Thao tác %>"
                        SortExpression="ActionName" />

                    
                    <%--<tlk:GridBoundColumn DataField="ActionDate" ReadOnly="true" HeaderText="<%$ Translate:Ngày thao tác %>"
                        SortExpression="ActionDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />--%>

                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày thao tác %>" DataField="ActionDate" AllowFiltering="false"
                        UniqueName="ActionDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" SortExpression="ActionDate" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>


                    <tlk:GridBoundColumn DataField="ObjectName" ReadOnly="true" HeaderText="<%$ Translate:Bảng dữ liệu %>"
                        SortExpression="ObjectName" />
                    <tlk:GridTemplateColumn ItemStyle-VerticalAlign="Middle" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                        <HeaderTemplate>
                            <%# Translate("Nội dung thao tác")%>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tlk:RadButton ID="btnView" runat="server" SkinID="ButtonView" AutoPostBack="false"
                                OnClientClicking="OpenView" CommandArgument='<%# Eval("ID") %>' />
                        </ItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn DataField="IP" ReadOnly="true" HeaderText="<%$ Translate:Địa chỉ IP %>"
                        SortExpression="IP" />
                    <tlk:GridBoundColumn DataField="ComputerName" ReadOnly="true" HeaderText="<%$ Translate:Tên máy %>"
                        SortExpression="ComputerName" />
                    <tlk:GridBoundColumn DataField="PLATFORM" ReadOnly="true" HeaderText="<%$ Translate:Platform %>"
                        SortExpression="PLATFORM" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:radsplitter>
<tlk:radwindowmanager id="rwmMain" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            Height="500px" EnableShadow="true" Behaviors="Close, Maximize" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:radwindowmanager>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<script type="text/javascript">
    var enableAjax = true;
    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (item.get_commandName() == "EXPORT") {
            enableAjax = false;
        }
    }

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }

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
        registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_RadSplitter1');
    }

    function OpenView(s, e) {
        $find("<%=rwPopup.ClientID %>").show();
        var id = e.get_commandArgument();
        radopen('Dialog.aspx?mid=Common&fid=ctrlCheckActionChange&group=Secure&noscroll=1&ID=' + id, "rwPopup");
        e.set_cancel(true);
    }


</script>
