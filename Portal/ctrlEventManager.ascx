<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlEventManager.ascx.vb"
    Inherits="Portal.ctrlEventManager" %>
<div style="margin: 7px; margin-right: 30px; position: absolute; right: 0px">
    Thông tin được hiển thị <span style="background-color: Orange">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
</div>
<tlk:RadToolBar ID="rtbMain" runat="server">
</tlk:RadToolBar>
<asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
<tlk:RadGrid ID="rGrid" runat="server">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
            <tlk:GridBoundColumn DataField="ID" Visible="false" />
            <tlk:GridBoundColumn DataField="IS_SHOW" Visible="false" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: TITLE %>" DataField="TITLE" UniqueName="TITLE"
                SortExpression="TITLE" ShowFilterIcon="false" FilterControlWidth="200px" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: ADD_TIME %>" DataField="ADD_TIME"
                UniqueName="ADD_TIME" DataFormatString="{0:dd/MM/yyyy}" SortExpression="ADD_TIME"
                ShowFilterIcon="false" FilterControlWidth="100px" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains">
                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
