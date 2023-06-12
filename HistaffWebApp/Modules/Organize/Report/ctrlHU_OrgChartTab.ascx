<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_OrgChartTab.ascx.vb" Inherits="Profile.ctrlHU_OrgChartTab" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPanel" runat="server" MinHeight="30" Height="30px" Scrolling="None">
        <tlk:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" CausesValidation="false">
            <Tabs>
                <tlk:RadTab runat="server" PageViewID="rpvOrgChart" Text="<%$ Translate: Sơ đồ tổ chức %>"
                    Selected="True">
                </tlk:RadTab>
                <tlk:RadTab runat="server" PageViewID="rpvJobPosition" Text="<%$ Translate: Vị trí công việc %>">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None" Width="100%">
        <tlk:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Width="100%">
            <tlk:RadPageView ID="rpvOrgChart" runat="server">
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvJobPosition" runat="server">
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
</tlk:RadSplitter>
