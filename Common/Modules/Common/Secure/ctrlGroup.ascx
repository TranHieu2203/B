<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGroup.ascx.vb"
    Inherits="Common.ctrlGroup" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="100" Width="300px">
        <tlk:RadListBox ID="lstGroup" runat="server" AutoPostBack="true" Height="100%" Width="100%">
        </tlk:RadListBox>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" MinHeight="26" Height="26px">
                <tlk:RadTabStrip runat="server" ID="rtabTab" Orientation="HorizontalTop" SelectedIndex="0">
                    <Tabs>
                        <tlk:RadTab Text="<%$ Translate: Phân quyền tài khoản %>" Visible="false">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Phân quyền chức năng %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Phân quyền sơ đồ tổ chức %>">
                        </tlk:RadTab>
                        <%--<tlk:RadTab Text="<%$ Translate: Phân quyền báo cáo %>">
                        </tlk:RadTab>--%>
                    </Tabs>
                </tlk:RadTabStrip>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <asp:PlaceHolder ID="TabView" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>