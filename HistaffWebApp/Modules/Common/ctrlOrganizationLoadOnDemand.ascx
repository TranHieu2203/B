<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOrganizationLoadOnDemand.ascx.vb"
    Inherits="Common.ctrlOrganizationLoadOnDemand" %>
<script type="text/javascript">
    function myFunction() {
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
    }
    </script>
<tlk:RadTreeView ID="trvOrg" runat="server" CausesValidation="false" Height="93%"
    Width="100%">
</tlk:RadTreeView>
<tlk:RadTreeView ID="trvOrgPostback" runat="server" CausesValidation="false" Height="93%"
    Width="100%">
</tlk:RadTreeView>
<asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị các đơn vị cũ? %>"
    AutoPostBack="True" />
