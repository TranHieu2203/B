<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_JobPosition.ascx.vb"
    Inherits="Profile.ctrlHU_JobPosition" %>
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
     #all{overflow: auto}
    #left{width:70%; float:left;}
    #right{width:30%!important; float:left} 
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_OrgChartTab_JobPosition_LeftPane{overflow:hidden !important}
    #ctl00_MainContent_ctrlHU_OrgChartTab_JobPosition_jobPosTreeList_rtlData{height:77% !important}
.NodeTree1 > td,
.NodeJOB1 > td,
.NodeNhomDuAn1 > td {
    overflow: hidden;    
    padding: 0px 13px 0px;
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

td.NodeFolderContent {
    padding-left: 20px !important;
	background-color: transparent;
    background-image: url("/Static/Images/folder-content.gif");
    background-repeat: no-repeat;
    background-position: left;
}

td.NodeFileContent {
    padding-left: 20px !important;
	background-color: transparent;
    background-image: url("/Static/Images/file-content.gif");
    background-repeat: no-repeat;
    background-position: left;
}
</style>
<%--<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server" MinWidth="260" Width="100%" Height="100%">
    <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
        <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="70%" Height="100%">--%>
<div id="all">
    <div id="left">
       <%-- <table class="table-form">
            <tr>
                <td>
                    <b>Job Position</b>
                </td>
            </tr>
        </table>--%>
        <tlk:RadTreeList Height="100%" EditMode="InPlace" RenderMode="Lightweight" ID="jobPosTreeList"
            runat="server" ParentDataKeyNames="PARENT_ID" DataKeyNames="ID" AutoGenerateColumns="false"
            OnUpdateCommand="jobPosTreeList_UpdateCommand" AutoPostBack="true" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="true" />
            <ExportSettings>
                <Pdf PageLeftMargin="20" PageRightMargin="20">
                </Pdf>
            </ExportSettings>
            <Columns>
                <tlk:TreeListBoundColumn HeaderStyle-Width="400px" DataField="ORG_NAME" UniqueName="ORG_NAME" ReadOnly="true" HeaderText="<%$ Translate: Đơn vị %>"></tlk:TreeListBoundColumn>
                <tlk:TreeListBoundColumn ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-HorizontalAlign="Center" DataType="System.Decimal" HeaderStyle-Width="80px"
                    DataField="LY_FTE" UniqueName="LY_FTE" HeaderText="<%$ Translate: SL năm trước %>">
                </tlk:TreeListBoundColumn>
                <tlk:TreeListBoundColumn ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-HorizontalAlign="Center" DataType="System.Decimal" HeaderStyle-Width="80px"
                    DataField="TOTAL_EMP" UniqueName="TOTAL_EMP" HeaderText="<%$ Translate: SL hiện tại %>"
                    ReadOnly="true">
                </tlk:TreeListBoundColumn>
                <tlk:TreeListBoundColumn ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-HorizontalAlign="Center" DataType="System.Decimal" HeaderStyle-Width="80px"
                    DataField="CHENH_LECH1" UniqueName="CHENH_LECH1" HeaderText="<%$ Translate: +/- chênh lệch %>"
                    ReadOnly="true">
                </tlk:TreeListBoundColumn>
                <tlk:TreeListBoundColumn ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-HorizontalAlign="Center" DataType="System.Decimal" HeaderStyle-Width="80px"
                    DataField="TOTAL_TITLE" UniqueName="TOTAL_TITLE" HeaderText="<%$ Translate: SL kế hoạch %>"
                    ReadOnly="true">
                </tlk:TreeListBoundColumn>
                <tlk:TreeListBoundColumn ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-HorizontalAlign="Center" DataType="System.Decimal" HeaderStyle-Width="80px"
                    DataField="CHENH_LECH2" UniqueName="CHENH_LECH2" HeaderText="<%$ Translate: +/- chênh lệch %>"
                    ReadOnly="true">
                </tlk:TreeListBoundColumn>
                <tlk:TreeListEditCommandColumn HeaderStyle-Width="40px" UniqueName="EditCommandColumn"
                    ShowAddButton="false" ItemStyle-HorizontalAlign="Center" EditText="<%$ Translate: Sửa %>"
                    UpdateText="<%$ Translate: Lưu %>" CancelText="<%$ Translate: Hủy %>">
                </tlk:TreeListEditCommandColumn>
                <tlk:TreeListBoundColumn Display="False" DataField="ID" UniqueName="ID" HeaderText=""
                    HeaderStyle-Width="1px">
                </tlk:TreeListBoundColumn>
                <tlk:TreeListBoundColumn Display="False" DataField="PARENT_ID" UniqueName="PARENT_ID" HeaderText=""
                    HeaderStyle-Width="1px">
                </tlk:TreeListBoundColumn>
                 <tlk:TreeListBoundColumn Display="false" HeaderStyle-Width="160px" DataField="IS_JOB" UniqueName="IS_JOB" HeaderText="<%$ Translate: Owner %>"></tlk:TreeListBoundColumn>
            <tlk:TreeListBoundColumn Display="false" HeaderStyle-Width="160px" DataField="NHOMDUAN" UniqueName="NHOMDUAN" HeaderText="<%$ Translate: Owner %>"></tlk:TreeListBoundColumn>
            </Columns>
            <ClientSettings> 
                <ClientEvents OnItemClick="onClick" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true">
                </Scrolling>
            </ClientSettings>
        </tlk:RadTreeList>
        <%--</tlk:RadPane>--%>
    </div>
    <%--     <tlk:RadPane ID="MainPane" runat="server" Scrolling="none">--%>
    <div id="right"> 
        <tlk:RadTreeList Height="100%" EditMode="InPlace" RenderMode="Lightweight" ID="jobChildTreeList"
            runat="server" ParentDataKeyNames="PARENT_ID" DataKeyNames="ID" AutoGenerateColumns="false">
            <Columns>
                <tlk:TreeListBoundColumn DataField="NAME" UniqueName="NAME" HeaderText="<%$ Translate: Tên chức năng %>"
                    ReadOnly="true">
                </tlk:TreeListBoundColumn>
                <tlk:TreeListBoundColumn DataField="FUNCTION_NAME" UniqueName="FUNCTION_NAME" HeaderText="<%$ Translate: Thang đo %>"
                    ReadOnly="true">
                </tlk:TreeListBoundColumn>
            </Columns>
            <ClientSettings>
                <Scrolling UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
        </tlk:RadTreeList>
        <%--        </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>--%>
    </div>
</div>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
       
        function onClick(sender, args) {
            args.get_item().selected = true;
            args.get_item().fireCommand("Select", "");
        }
        winH = $(window).height() - 120;
        $("#ctl00_MainContent_ctrlHU_OrgChartTab_JobPosition_jobPosTreeList").stop().animate({ height: winH }, 0);
    </script>
</tlk:RadScriptBlock>
