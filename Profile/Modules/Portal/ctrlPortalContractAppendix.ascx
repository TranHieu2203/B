<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalContractAppendix.ascx.vb"
    Inherits="Profile.ctrlPortalContractAppendix" %>
<tlk:RadGrid ID="rgGrid" runat="server"  AllowFilteringByColumn="true" Scrolling="both">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME1"
                UniqueName="ORG_NAME1" SortExpression="ORG_NAME1" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />--%>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số HĐLĐ %>" DataField="CONTRACT_NO"
                UniqueName="CONTRACT_NO" SortExpression="CONTRACT_NO" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hợp đồng %>" DataField="CONTRACTTYPE_NAME"
                UniqueName="CONTRACTTYPE_NAME" SortExpression="CONTRACTTYPE_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số phụ lục hợp đồng %>" DataField="APPEND_NUMBER"
                UniqueName="APPEND_NUMBER" SortExpression="APPEND_NUMBER" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại phụ lục hợp đồng %>" DataField="APPEND_TYPE_NAME"
                UniqueName="APPEND_TYPE_NAME" SortExpression="APPEND_TYPE_NAME" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="START_DATE" UniqueName="START_DATE"
                ReadOnly="true" SortExpression="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                ReadOnly="true" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày ký %>" DataField="SIGN_DATE" UniqueName="SIGN_DATE"
                ReadOnly="true" SortExpression="SIGN_DATE" DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh người ký %>" DataField="SIGN_TITLE_NAME"
                UniqueName="SIGN_TITLE_NAME" SortExpression="SIGN_TITLE_NAME" />

            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Loại quyết định %>" DataField="DECISION_TYPE_NAME"
                UniqueName="DECISION_TYPE_NAME" SortExpression="DECISION_TYPE_NAME" />--%>
            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Người ký %>" DataField="SIGN_NAME"
                UniqueName="SIGN_NAME" SortExpression="SIGN_NAME" />--%>
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
            $("#ctl00_MainContent_ctrlPortalContractAppendix_rgGrid").stop().animate({ height: winH }, 0);
            $("#ctl00_MainContent_ctrlPortalContractAppendix_rgGrid_GridData").stop().animate({ height: winH -80}, 0);
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