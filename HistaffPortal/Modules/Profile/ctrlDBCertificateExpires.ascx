<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDBCertificateExpires.ascx.vb"
    Inherits="Profile.ctrlDBCertificateExpires" %>
<%@ Import Namespace="Common" %>
<tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="350px" AllowFilteringByColumn="false">
    <MasterTableView DataKeyNames="CERTIFICATE_ID"
        ClientDataKeyNames="CERTIFICATE_ID">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center" Visible="false">
            </tlk:GridClientSelectColumn>
            <tlk:GridBoundColumn DataField="ID" Visible="false" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Văng bằng/chứng chỉ %>" DataField="CERTIFICATE_NAME"
                UniqueName="CERTIFICATE_NAME" SortExpression="CERTIFICATE_NAME">
                <HeaderStyle Width="200px" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời hạn chứng chỉ %>" DataField="CERTIFICATE_DURATION"
                UniqueName="CERTIFICATE_DURATION" SortExpression="CERTIFICATE_DURATION">
                <HeaderStyle Width="100px" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp chứng chỉ %>" DataField="CER_RECEIVE_DATE"
                UniqueName="CER_RECEIVE_DATE" SortExpression="CER_RECEIVE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle Width="150px" />
                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
               <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hạn chứng chỉ %>" DataField="CER_RECEIVE_ENDDATE"
                UniqueName="CER_RECEIVE_ENDDATE" SortExpression="CER_RECEIVE_ENDDATE" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle Width="150px" />
                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings>
        <Selecting AllowRowSelect="True" />
        <ClientEvents OnRowDblClick="gridRowDblClick" />
    </ClientSettings>
</tlk:RadGrid>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var selectedID = -1;
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEdit();
        }

        function OpenEdit() {
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            }
        }
        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
</tlk:RadCodeBlock>
