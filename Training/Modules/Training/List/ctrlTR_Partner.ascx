<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Partner.ascx.vb"
    Inherits="Training.ctrlTR_Partner" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidLectureID" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="20" ID="rgMain" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã đối tác  %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên đối tác %>" DataField="NAME"
                        UniqueName="NAME" SortExpression="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="FIELD"
                        UniqueName="FIELD" SortExpression="FIELD" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã số thuế %>" DataField="PIT_NO"
                        UniqueName="PIT_NO" SortExpression="PIT_NO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giấy phép ĐKHĐKD %>" DataField="BUSINESS_LICENSE_NO"
                        UniqueName="BUSINESS_LICENSE_NO" SortExpression="BUSINESS_LICENSE_NO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Website %>" DataField="WEBSITE" UniqueName="WEBSITE"
                        SortExpression="WEBSITE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Fax %>" DataField="FAX" UniqueName="FAX"
                        SortExpression="FAX" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại %>" DataField="PHONE_ORG"
                        UniqueName="PHONE_ORG" SortExpression="PHONE_ORG" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ %>" DataField="ADDRESS" UniqueName="ADDRESS"
                        SortExpression="ADDRESS" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên người đứng tên pháp lý %>" DataField="FULLNAME_SURROGATE"
                        UniqueName="FULLNAME_SURROGATE" SortExpression="FULLNAME_SURROGATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh người đứng tên pháp lý %>"
                        DataField="TITLE_SURROGATE" UniqueName="TITLE_SURROGATE" SortExpression="TITLE_SURROGATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại người đứng tên pháp lý %>"
                        DataField="PHONE_SURROGATE" UniqueName="PHONE_SURROGATE" SortExpression="PHONE_SURROGATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email người đứng tên pháp lý %>"
                        DataField="EMAIL_SURROGATE" UniqueName="EMAIL_SURROGATE" SortExpression="EMAIL_SURROGATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên người liên hệ %>" DataField="FULLNAME_CONTACT"
                        UniqueName="FULLNAME_CONTACT" SortExpression="FULLNAME_CONTACT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh người liên hệ %>" DataField="TITLE_CONTACT"
                        UniqueName="TITLE_CONTACT" SortExpression="TITLE_CONTACT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại người liên hệ %>" DataField="PHONE_CONTACT"
                        UniqueName="PHONE_CONTACT" SortExpression="PHONE_CONTACT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email người liên hệ %>" DataField="EMAIL_CONTACT"
                        UniqueName="EMAIL_CONTACT" SortExpression="EMAIL_CONTACT" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="900px"
            OnClientClose="OnClientClose" Height="640px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:PlaceHolder ID="phFindLecture" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OpenInsertWindow() {
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_PartnerNewEdit&group=List&FormType=0&noscroll=1', "rwPopup");
            oWindow.setSize(800, 700);
            oWindow.center();
        }

        function OpenEditWindow() {
            var id = $find('<%= rgMain.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            if (id > 0) {
                var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_PartnerNewEdit&group=List&VIEW=TRUE&FormType=0&ID=' + id + '&noscroll=1', "rwPopup");
                oWindow.setSize(800, 700);
                oWindow.center();
            }
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
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
        function OnClientClose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgMain.ClientID %>").get_masterTableView().rebind();
            }
        }
    </script>
</tlk:RadCodeBlock>
