<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFrameSalaryTree.ascx.vb"
    Inherits="Common.ctrlFrameSalaryTree" %>
<tlk:RadTreeView ID="trvFrameSalarys" runat="server" CausesValidation="false" Height="93%"
    Width="100%">
</tlk:RadTreeView>
<tlk:RadTreeView ID="trvFrameSalaryPostback" runat="server" CausesValidation="false" Height="93%"
    Width="100%">
</tlk:RadTreeView>
<div style="display:none">
    <asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị các đơn vị đã giải thể? %>"
        AutoPostBack="True" />
    <div style="width: 10px; height: 10px; background-color: Yellow; float: right;">
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
