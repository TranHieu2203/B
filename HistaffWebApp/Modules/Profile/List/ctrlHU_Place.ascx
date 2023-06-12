<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Place.ascx.vb"
    Inherits="Profile.ctrlHU_Place" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPanel" runat="server" MinHeight="30" Height="30px" Scrolling="None">
        <tlk:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" CausesValidation="false">
            <Tabs>
                <tlk:RadTab runat="server" PageViewID="rpvNations" Text="<%$ Translate: Quốc gia %>"
                    Selected="True">
                </tlk:RadTab>
                <tlk:RadTab runat="server" PageViewID="rpvCity" Text="<%$ Translate: Tỉnh thành %>">
                </tlk:RadTab>
                <tlk:RadTab runat="server" PageViewID="rpvDistrict" Text="<%$ Translate: Quận huyện %>">
                </tlk:RadTab>
                <tlk:RadTab runat="server" PageViewID="rpvWard" Text="<%$ Translate: Xã phường %>">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Width="100%">
            <tlk:RadPageView ID="rpvNations" runat="server">
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvProvince" runat="server" Width="100%">
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvDistrict" runat="server" Width="100%">
            </tlk:RadPageView>
            <tlk:RadPageView ID="rpvWard" runat="server" Width="100%">
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
</tlk:RadSplitter>
