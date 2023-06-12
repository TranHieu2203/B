<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_PortalPayslip.ascx.vb"
    Inherits="Payroll.ctrlPA_PortalPayslip" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<table class="table-form">
    <tr>
        <td class="lb">
            <%# Translate("Năm")%>
        </td>
        <td>
            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                TabIndex="12" Width="80px">
            </tlk:RadComboBox>
        </td>
        <td class="lb">
            <%# Translate("Kỳ lương")%>
        </td>
        <td>
            <tlk:RadComboBox ID="rcboPeriod" runat="server" AutoPostBack="false">
            </tlk:RadComboBox>
        </td>
        <td>
            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Xem %>" SkinID="ButtonFind">
            </tlk:RadButton>
        </td>
        <td>
            <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải phiếu lương %>" OnClientClicking="clientButtonClicking">
            </tlk:RadButton>
        </td>
    </tr>
</table>
<table style="width: 98%" border="0" cellpadding="0" cellspacing="0" class="show-data ui-widget ui-widget-content">
    <tr>
        <td style="padding-left: 50px">
            <asp:Label ID="Label23" runat="server"></asp:Label>
        </td>
        <td>
            <h2>THÔNG TIN LƯƠNG</h2>
        </td>
    </tr>
</table>
<br />
<div class="all-pl">
    <div class="all-l">
            <tlk:RadGrid ID="rgMain" runat="server" Height="390px" Width="100%" AllowPaging="False" Skin="Office2007"
                    AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText="" DataField="COLUMN1"
                                UniqueName="COLUMN1" SortExpression="COLUMN1" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign ="Center"
                                ItemStyle-Width="30px"/>
                            <tlk:GridBoundColumn DataField="COLUMN2"
                                UniqueName="COLUMN2" SortExpression="COLUMN2" HeaderStyle-Width="140px" HeaderStyle-HorizontalAlign ="Center"
                                ItemStyle-Width="140px" />
                            <tlk:GridBoundColumn DataField="COLUMN3" HeaderStyle-HorizontalAlign ="Left"
                                UniqueName="COLUMN3" SortExpression="COLUMN3"
                                HeaderStyle-Width="200px" ItemStyle-Width="200px" />
                        </Columns>
                    </MasterTableView>                        
                </tlk:RadGrid>
    </div>
    <div class="all-r">
       <%-- <img src="../../Static/Images/logo_luong.png" />--%>
       
    </div>
</div>
<style>
    .all-pl {
    float: left;
    width: 100%;
    min-height: 500px;
    }

    .all-l {
    padding-left: 50px;
    font-size: larger;
    border: 1px;
    width: 50%;
    float: left;
    }

    .all-r {
    float: right;
    width: 45%;
    }

        .all-r img {
        width: 100%;
        margin-bottom: 50px;
        }
    #WindowMainRegion {
        min-height:500px;
    }
</style>
<script type="text/javascript">

    function popupclose(sender, args) {
        var m;
        var arg = args.get_argument();
        if (arg == '1') {
            m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
            var n = noty({ text: m, dismissQueue: true, type: 'success' });
            setTimeout(function () { $.noty.close(n.options.id); }, 5000);

        }

    }

    var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }

    function clientButtonClicking(sender, args) {
        enableAjax = false;
    }
</script>
