<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlUserException.ascx.vb" Inherits="Common.ctrlUserException" %>

<%@ Register Src="../ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    #ctl00_MainContent_ctrlUserException_ctrlOrg_trvOrg {
        height: 90% !important;
    }
    #ctl00_MainContent_ctrlUserException_ctrlOrg_trvOrgPostback{
        display: none;
    }
    #ctl00_MainContent_ctrlUserException_rgGrid{

    }
</style>
<asp:HiddenField ID="hidID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="250" Width="300px" Scrolling="None">
        <%--<tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" Height="40%" SkinID="GridSingleSelect">
            <ClientSettings EnablePostBackOnRowClick="true">
            </ClientSettings>
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tài khoản %>" DataField="USERNAME"
                        UniqueName="USERNAME" SortExpression="USERNAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME"
                        UniqueName="FULLNAME" SortExpression="FULLNAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Công ty %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
        <common:ctrlorganization id="orgLoca" runat="server" />--%>
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="200%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="100%" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" Height="100%" SkinID="GridSingleSelect">
                    <ClientSettings EnablePostBackOnRowClick="true">
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tài khoản %>" DataField="USERNAME"
                                UniqueName="USERNAME" SortExpression="USERNAME" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME"
                                UniqueName="FULLNAME" SortExpression="FULLNAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Công ty %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
                <common:ctrlorganization id="ctrlOrg" runat="server" AutoPostBack="true" />
            </tlk:RadPane>
        </tlk:RadSplitter>

    </tlk:RadPane>

    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" MinHeight="26" Height="1px">
                <tlk:RadTabStrip runat="server" ID="rtabTab" Orientation="HorizontalTop" SelectedIndex="0">
                    <%-- <Tabs>
                        <tlk:RadTab Text="<%$ Translate: Phân quyền chức năng %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Phân quyền sơ đồ tổ chức %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Phân quyền báo cáo %>">
                        </tlk:RadTab>
                    </Tabs>--%>
                </tlk:RadTabStrip>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <asp:PlaceHolder ID="TabView" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>