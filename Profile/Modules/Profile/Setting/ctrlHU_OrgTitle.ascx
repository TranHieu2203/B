<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_OrgTitle.ascx.vb"
    Inherits="Profile.ctrlHU_OrgTitle" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOrgTitles" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgOrgTitle" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    AllowFilteringByColumn="true" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,TITLE_ID,ACTFLG,NAME_VN">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Mã chức danh" DataField="CODE"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" SortExpression="CODE"
                                UniqueName="CODE" />
                            <tlk:GridBoundColumn HeaderText="Tên chức danh" DataField="NAME_VN"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" SortExpression="NAME_VN"
                                UniqueName="NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="Nhóm chức danh" DataField="TITLE_GROUP_NAME"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" SortExpression="TITLE_GROUP_NAME"
                                UniqueName="TITLE_GROUP_NAME" />
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="ACTFLG"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" SortExpression="ACTFLG"
                                UniqueName="ACTFLG" />--%>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
