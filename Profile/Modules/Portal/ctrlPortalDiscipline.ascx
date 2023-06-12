<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalDiscipline.ascx.vb"
    Inherits="Profile.ctrlPortalDiscipline" %>
<tlk:RadGrid PageSize="50" ID="rgDiscipline" runat="server" Height="350px" AllowFilteringByColumn="true" Scrolling="Both">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <%--<tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                UniqueName="DECISION_NO" SortExpression="DECISION_NO" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE"
                ShowFilterIcon="true">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
             <tlk:GridDateTimeColumn HeaderText="Ngày vi phạm" DataField="DISCIPLINE_DATE"
                UniqueName="DISCIPLINE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DISCIPLINE_DATE"
                ShowFilterIcon="true">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Cấp kỷ luật" DataField="DISCIPLINE_LEVEL_NAME"
                UniqueName="DISCIPLINE_LEVEL_NAME" SortExpression="DISCIPLINE_LEVEL_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Hình thức kỷ luật" DataField="DISCIPLINE_TYPE_NAME"
                UniqueName="DISCIPLINE_TYPE_NAME" SortExpression="DISCIPLINE_TYPE_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridNumericColumn HeaderText="Tiền phạt" DataField="MONEY" UniqueName="MONEY"
                SortExpression="MONEY" DataFormatString="{0:###,###,###,##0}" ShowFilterIcon="true">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridDateTimeColumn HeaderText="Thời gian thi hành kỷ luật" DataField="PERFORM_DATE"
                UniqueName="PERFORM_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="PERFORM_DATE"
                ShowFilterIcon="true">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>--%>
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
            $("#ctl00_MainContent_ctrlPortalDiscipline_rgDiscipline").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlPortalDiscipline_rgDiscipline_GridData").stop().animate({ height: winH -80 }, 0);
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