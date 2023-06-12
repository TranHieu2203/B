<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Job_Position.ascx.vb"
    Inherits="Profile.ctrlHU_Job_Position" %>
    <%@ Import Namespace = "Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="130px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server">
        </tlk:RadToolBar>
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Tên công việc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboJob" runat="server" Filter="Contains">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqBank" ControlToValidate="cboJob" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn tên công việc %>" ToolTip="<%$ Translate:  Bạn phải chọn tên công việc %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusBank" runat="server" ErrorMessage="<%$ Translate: Tên công việc không tồn tại hoặc đã ngừng áp dụng %>"
                        ToolTip="<%$ Translate: Tên công việc không tồn tại hoặc đã ngừng áp dụng%>" ControlToValidate="cboJob" ></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên tiếng Việt")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên tiếng Việt %>" ToolTip="<%$ Translate:  Bạn phải nhập tên tiếng Việt %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên tiếng Anh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameEN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtNameEN" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên tiếng Anh %>" ToolTip="<%$ Translate: Bạn phải nhập tên tiếng Anh %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgBankBranchs" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="JOB_NAME,NAME,NAME_EN,JOB_ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="<%$ Translate: ID %>" UniqueName="ID"
                        Visible="false" />
                    <tlk:GridBoundColumn DataField="JOB_ID" HeaderText="<%$ Translate: JOB_ID %>" UniqueName="JOB_ID"
                        Visible="false" />
                    <tlk:GridBoundColumn DataField="JOB_NAME" HeaderText="<%$ Translate: Tên công việc %>"
                        UniqueName="JOB_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NAME" HeaderText="<%$ Translate: Tên tiếng Việt %>"
                        UniqueName="NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NAME_EN" HeaderText="<%$ Translate: Tên tiếng Anh %>"
                        UniqueName="NAME_EN">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ACTFLG" HeaderText="<%$ Translate: Trạng thái %>">
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
             </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">

    function ValidateFilter(sender, eventArgs) {
        var val = eventArgs.get_commandArgument().split("|")[1];
        if (validateHTMLText(val) || validateSQLText(val)) {
            eventArgs.set_cancel(true);
        }
    }

    function GridCreated(sender, eventArgs) {
        registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_JobPosition_RadSplitter3');
    }

    function cusBank(oSrc, args) {
        var cbo = $find("<%# cboJob.ClientID %>");
        args.IsValid = (cbo.get_value().length != 0);
    }

    var enableAjax = true;
    var oldSize = 0;
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
    // Resize when save success
    function ResizeSplitterSaveSuccess() {
        setTimeout(function () {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
            var height = pane.getContentElement().scrollHeight;
            splitter.set_height(splitter.get_height() + pane.get_height() - 152);
            pane.set_height(152);
        }, 200);
    }

    // Hàm khôi phục lại Size ban đầu cho Splitter
    function ResizeSplitterDefault() {
        var splitter = $find("<%= RadSplitter3.ClientID%>");
        var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
        if (oldSize == 0) {
            oldSize = pane.getContentElement().scrollHeight;
        } else {
            var pane2 = splitter.getPaneById('<%= RadPane1.ClientID %>');
            splitter.set_height(splitter.get_height() + pane.get_height() - 152);
            pane.set_height(152);
            pane2.set_height(splitter.get_height() - 152 - 1);
        }
    }
</script>
</tlk:RadCodeBlock>
