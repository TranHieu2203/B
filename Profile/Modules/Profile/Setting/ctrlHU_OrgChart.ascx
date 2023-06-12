<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_OrgChart.ascx.vb" Inherits="Profile.ctrlHU_OrgChart" %>
<style type="text/css">
    #btnSaveImage
    {
        display: none;
    }
    .RadUpload .ruFakeInput
    {
        display: none;
    }
    .RadUpload .ruBrowse
    {
        width: 120px !important;
        _width: 120px !important;
        width: 120px;
        _width: 120px;
        background-position: 0 -46px !important;
    }
    .hide
    {
        display: none !important;
    }
    .btnChooseImage
    {
        margin-left: -5px;
    }
    .ruInputs
    {
        width: 0px;
        text-align: center;
    }
.NodeTree1 > td,
.NodeJOB1 > td,
.NodeNhomDuAn1 > td {
    overflow: hidden;    
    padding: 5px 13px 5px;
}
.NodeTree1:hover,
.NodeJOB1:hover {
    background-color: #c0e6f8;
}
.d-flex {
    display: flex;    
}
.m-1
{
    padding-left: 16px !important;    
}
td.NodeNhomDuAn {
    padding-left: 20px !important;
	background-color: transparent;
    background-image: url("/Static/Images/ftePlan.gif");
    background-repeat: no-repeat;
    background-position: left;
}

td.NodeJOB {
    padding-left: 20px !important;
	background-color: transparent;
    background-image: url("/Static/Images/vcard.png");
    background-repeat: no-repeat;
    background-position: left;
}

td.NodeTree {
    padding-left: 20px !important;
	background-color: transparent;
    background-image: url("/Static/Images/folder.gif");
    background-repeat: no-repeat;
    background-position: left;
}
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
<tlk:RadPane ID="RadPane3" runat="server" Height="40px" Scrolling="None" Width="100%">
        <table  class="table-form">
            <tr>
                <td><asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị các đơn vị giải thể %>" AutoPostBack="True" ScrollHeight="100%" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
<tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        
        <tlk:RadTreeList Height="100%" RenderMode="Lightweight" ID="orgTreeList" runat="server" ParentDataKeyNames="PARENT_ID" DataKeyNames="ID" AutoGenerateColumns="false" CommandItemDisplay="Top">
    <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="true" />
    <ExportSettings>
        <Pdf PageLeftMargin="20" PageRightMargin="20"></Pdf>
    </ExportSettings>
        <Columns> 
            <tlk:TreeListBoundColumn HeaderStyle-Width="400px" DataField="ORG_NAME" UniqueName="ORG_NAME" HeaderText="<%$ Translate: Đơn vị %>"></tlk:TreeListBoundColumn>
            <tlk:TreeListBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataType="System.Decimal" HeaderStyle-Width="100px" DataField="JOB_CNT" UniqueName="JOB_CNT" HeaderText="<%$ Translate: Job Position %>"></tlk:TreeListBoundColumn>
            <tlk:TreeListBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataType="System.Decimal" HeaderStyle-Width="100px" DataField="YTD_FTE" UniqueName="YTD_FTE" HeaderText="<%$ Translate: Số lượng vị trí %>"></tlk:TreeListBoundColumn>
            <tlk:TreeListBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataType="System.Decimal" HeaderStyle-Width="100px" DataField="PLAN_FTE" UniqueName="PLAN_FTE" HeaderText="<%$ Translate: Số lượng vị trí kế hoạch %>"></tlk:TreeListBoundColumn>
            <tlk:TreeListBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataType="System.Decimal" HeaderStyle-Width="100px" DataField="VS_PLAN_FTE" UniqueName="VS_PLAN_FTE" HeaderText="<%$ Translate: Chênh lệch %>"></tlk:TreeListBoundColumn>
            <tlk:TreeListBoundColumn HeaderStyle-Width="160px" DataField="OWNER_NAME" UniqueName="OWNER_NAME" HeaderText="<%$ Translate: Owner %>"></tlk:TreeListBoundColumn>
            <tlk:TreeListBoundColumn Display="false" HeaderStyle-Width="160px" DataField="IS_JOB" UniqueName="IS_JOB" HeaderText="<%$ Translate: Owner %>"></tlk:TreeListBoundColumn>
            <tlk:TreeListBoundColumn Display="false" HeaderStyle-Width="160px" DataField="NHOMDUAN" UniqueName="NHOMDUAN" HeaderText="<%$ Translate: Owner %>"></tlk:TreeListBoundColumn>
        </Columns>
        <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
        </tlk:RadTreeList>

        
</tlk:RadPane>
</tlk:RadSplitter>
    <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    <asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>