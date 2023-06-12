<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOrganization.ascx.vb"
    Inherits="Common.ctrlOrganization" %>
<style>
.marg-top-5{
    margin-top: 5px ;
}
</style>
<tlk:RadTreeView ID="trvOrg" runat="server" CausesValidation="false" Height="95%"
    Width="100%" CssClass="marg-top-5">
</tlk:RadTreeView>
<tlk:RadTreeView ID="trvOrgPostback" runat="server" CausesValidation="false" Height="95%"
    Width="100%" CssClass="marg-top-5">
</tlk:RadTreeView>
<div>
    <asp:CheckBox ID="chkViewCommitee" runat="server" Text="<%$ Translate:Xem Ủy ban%>"
            AutoPostBack="True" CausesValidation="false" Visible="false"/>
    <asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị các đơn vị đã giải thể? %>"
        AutoPostBack="True" />
    <div class="dissolution">
    </div>
</div>
<tlk:RadCodeBlock runat="server" ID="RadCodeBlock1">
    <script type="text/javascript">
        if (Telerik.Web.UI.RadTreeView != undefined) {
            Telerik.Web.UI.RadTreeView.prototype.saveClientState = function () {
                return "{\"expandedNodes\":" + this._expandedNodesJson +
                ",\"collapsedNodes\":" + this._collapsedNodesJson +
                ",\"logEntries\":" + this._logEntriesJson +
                ",\"selectedNodes\":" + this._selectedNodesJson +
                ",\"checkedNodes\":" + this._checkedNodesJson +
                ",\"scrollPosition\":" + Math.round(this._scrollPosition) + "}";
            }
        }
    </script>
</tlk:RadCodeBlock>
