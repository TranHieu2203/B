<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindOrgPopup.ascx.vb"
    Inherits="Common.ctrlFindOrgPopup" %>
<%@ Register Src="ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="None" VisibleStatusbar="false"
    Height="420" Width="400" Modal="true" Title="<%$ Translate: Tìm kiếm đơn vị %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" runat="server">
            <div style="height: 330px;">
                <common:ctrlorganization id="ctrlOrg1" runat="server" />
            </div>
            <div style="margin: 0px 10px 10px 10px; text-align: center;">
                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
            </div>
        </tlk:RadAjaxPanel>
        <tlk:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Enabled="true" runat="server" > </tlk:RadAjaxLoadingPanel>
    </ContentTemplate>
</tlk:RadWindow>
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