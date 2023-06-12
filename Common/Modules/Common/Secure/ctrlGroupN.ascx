<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGroupN.ascx.vb"
    Inherits="Common.ctrlGroupN" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="100" Width="300px">
        <tlk:RadListBox ID="lstGroup" runat="server" AutoPostBack="true" Height="95%" Width="100%">
        </tlk:RadListBox>
        <div>
            <asp:CheckBox ID="chkShowInActiveUser" runat="server" Text="<%$ Translate: Hiển thị tài khoản ngưng áp dụng %>"
                AutoPostBack="True"  CausesValidation="false"/>
        </div>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" MinHeight="26" Height="26px">
                <tlk:RadTabStrip runat="server" ID="rtabTab" Orientation="HorizontalTop" SelectedIndex="0">
                    <Tabs>
                        <tlk:RadTab Text="<%$ Translate: Thêm tài khoản vào nhóm %>">
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