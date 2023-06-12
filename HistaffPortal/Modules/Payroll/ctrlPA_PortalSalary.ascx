<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_PortalSalary.ascx.vb"
    Inherits="Payroll.ctrlPA_PortalSalary" %>
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
            <tlk:RadComboBox ID="rcboPeriod" runat="server" AutoPostBack="true">
            </tlk:RadComboBox>
        </td>
        <td>
            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Xem %>" SkinID="ButtonFind" Visible="false">
            </tlk:RadButton>
        </td>
        <td>
            <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải phiếu lương %>" OnClientClicking="clientButtonClicking" Visible="false">
            </tlk:RadButton>
        </td>
    </tr>
</table>

<br />
<table >
    <tr>
        <td colspan="2" style="text-align:center">
            <h2>THÔNG TIN LƯƠNG</h2>
        </td>
    </tr>
    <tr></tr>
    <tr>
        <td >
            <tlk:RadGrid ID="rgMain" runat="server" Height="1100px" Width="350px" AllowPaging="False" Skin="Office2007" BorderStyle="None" CssClass="RemoveBorders"
                    AllowMultiRowSelection="true" Enabled="false" >
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText="" DataField="COLUMN1"
                                UniqueName="COLUMN1" SortExpression="COLUMN1" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign ="Center"
                                ItemStyle-Width="30px"/>
                            <tlk:GridBoundColumn DataField="COLUMN2"
                                UniqueName="COLUMN2" SortExpression="COLUMN2" HeaderStyle-Width="140px" HeaderStyle-HorizontalAlign ="Center"
                                ItemStyle-Width="140px" />
                            <tlk:GridBoundColumn DataField="COLUMN3" HeaderStyle-HorizontalAlign ="Left" DataFormatString = "{0:#,##0.##}"
                                UniqueName="COLUMN3" SortExpression="COLUMN3"
                                HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                            <tlk:GridBoundColumn DataField="IS_BOLD" HeaderStyle-HorizontalAlign ="Left"
                                UniqueName="IS_BOLD" SortExpression="IS_BOLD"
                                HeaderStyle-Width="50px" ItemStyle-Width="50px" Visible="false"/>
                            <tlk:GridBoundColumn DataField="COLOR" HeaderStyle-HorizontalAlign ="Left"
                                UniqueName="COLOR" SortExpression="COLOR"
                                HeaderStyle-Width="50px" ItemStyle-Width="50px" Visible="false"/>
                        </Columns>
                    </MasterTableView>                        
                </tlk:RadGrid>
        </td>
        <td>
            <tlk:RadGrid ID="rgMain1" runat="server" Height="1100px" Width="350px"  AllowPaging="False" Skin="Office2007" BorderStyle="None" CssClass="RemoveBorders"
                    AllowMultiRowSelection="true" Enabled="false" >
                    <ClientSettings EnableRowHoverStyle="true" >
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText="" DataField="COLUMN1"
                                UniqueName="COLUMN1" SortExpression="COLUMN1" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign ="Center"
                                ItemStyle-Width="30px"/>
                            <tlk:GridBoundColumn DataField="COLUMN2"
                                UniqueName="COLUMN2" SortExpression="COLUMN2" HeaderStyle-Width="140px" HeaderStyle-HorizontalAlign ="Center"
                                ItemStyle-Width="140px" />
                            <tlk:GridBoundColumn DataField="COLUMN3" HeaderStyle-HorizontalAlign ="Left" DataFormatString = "{0:#,##0.##}"
                                UniqueName="COLUMN3" SortExpression="COLUMN3"
                                HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                             <tlk:GridBoundColumn DataField="IS_BOLD" HeaderStyle-HorizontalAlign ="Left"
                                UniqueName="IS_BOLD" SortExpression="IS_BOLD"
                                HeaderStyle-Width="50px" ItemStyle-Width="50px" Visible="false"/>
                            <tlk:GridBoundColumn DataField="COLOR" HeaderStyle-HorizontalAlign ="Left"
                                UniqueName="COLOR" SortExpression="COLOR"
                                HeaderStyle-Width="50px" ItemStyle-Width="50px" Visible="false"/>
                        </Columns>
                    </MasterTableView>                        
                </tlk:RadGrid>
        </td>
    </tr>
</table>
<%--<div class="all-pl">
    <div class="all-l">
            
    </div>
    <div class="all-r">
       <%-- <img src="../../Static/Images/logo_luong.png" />
       
    </div>
</div>--%>
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
    div.RemoveBorders .rgHeader,
div.RemoveBorders th.rgResizeCol,
div.RemoveBorders .rgFilterRow td
{
	border-width:0 0 1px 0; /*top right bottom left*/
}

div.RemoveBorders .rgRow td,
div.RemoveBorders .rgAltRow td,
div.RemoveBorders .rgEditRow td,
div.RemoveBorders .rgFooter td
{
	border-width:0;
	padding-left:7px; /*needed for row hovering and selection*/
}

div.RemoveBorders .rgGroupHeader td,
div.RemoveBorders .rgFooter td
{
	padding-left:7px;
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
