<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalCommend.ascx.vb"
    Inherits="Profile.ctrlPortalCommend" %>
<tlk:RadGrid PageSize="50" ID="rgCommend" runat="server" Height="350px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="DECISION_NO,EFFECT_DATE,COMMEND_LEVEL_NAME,COMMEND_TYPE_NAME,REMARK,MONEY,YEAR,COMMEND_PAY_NAME,SIGNER_NAME,COMMEND_TITLE_NAME,NOTE">
        <Columns>
            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                UniqueName="DECISION_NO" SortExpression="DECISION_NO" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                ShowFilterIcon="true">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp khen thưởng %>" DataField="COMMEND_LEVEL_NAME"
                UniqueName="COMMEND_LEVEL_NAME" SortExpression="COMMEND_LEVEL_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức khen thưởng %>" DataField="COMMEND_TYPE_NAME"
                UniqueName="COMMEND_TYPE_NAME" SortExpression="COMMEND_TYPE_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do khen thưởng %>" DataField="REMARK"
                UniqueName="REMARK" SortExpression="REMARK" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức thưởng %>" DataField="MONEY" UniqueName="MONEY"
                SortExpression="MONEY" DataFormatString="{0:###,###,###,##0}" ShowFilterIcon="true">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR"
                UniqueName="YEAR" SortExpression="YEAR" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức trả thưởng %>" DataField="COMMEND_PAY_NAME"
                UniqueName="COMMEND_PAY_NAME" SortExpression="COMMEND_PAY_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người ký %>" DataField="SIGNER_NAME"
                UniqueName="SIGNER_NAME" SortExpression="SIGNER_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>--%>
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
            $("#ctl00_MainContent_ctrlPortalCommend_rgCommend").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlPortalCommend_rgCommend_GridData").stop().animate({ height: winH -80 }, 0);
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