<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalWorking.ascx.vb"
    Inherits="Profile.ctrlPortalWorking" %>
<tlk:RadGrid PageSize="50" ID="rgWorking" runat="server" Height="100px"  Width="100%" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridBoundColumn HeaderText="Loại quyết định" DataField="DECISION_TYPE_NAME"
                UniqueName="DECISION_TYPE_NAME" SortExpression="DECISION_TYPE_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Bộ phận" DataField="ORG_NAME" UniqueName="ORG_NAME"
                SortExpression="ORG_NAME">
            </tlk:GridBoundColumn>
           <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Nơi làm việc" DataField="WORK_PLACE_NAME"
                UniqueName="WORK_PLACE_NAME" SortExpression="WORK_PLACE_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Người quản lý trực tiếp" DataField="DIRECT_MANAGER_NAME"
                UniqueName="DIRECT_MANAGER_NAME" SortExpression="DIRECT_MANAGER_NAME">
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}">
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE"
                UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}">
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE" Visible="false"
                UniqueName="NOTE" SortExpression="NOTE">
            </tlk:GridBoundColumn>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function SizeToFit(resize) { }
        $(window).resize(function () {
            SizeToFit(0);
        });
        var winH;
        var winW;

        function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() - 210;
            winW = $(window).width() - 90;
            $("#ctl00_MainContent_ctrlPortalWorking_rgWorking").stop().animate({ height: winH, width: winW }, 0);

            Sys.Application.add_load(SizeToFitMain);
        }

        SizeToFitMain();

        $(document).ready(function () {
            SizeToFitMain();
        });
        $(window).resize(function () {
            SizeToFitMain();
        });
    </script>
</tlk:RadCodeBlock>