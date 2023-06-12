<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalSalary.ascx.vb"
    Inherits="Profile.ctrlPortalSalary" %>
<tlk:RadGrid PageSize="50" ID="rgSalary" runat="server"  AllowFilteringByColumn="true" 
    Scrolling="X">
    <MasterTableView DataKeyNames="ID,EFFECT_DATE,EMPLOYEE_ID" ClientDataKeyNames="ID,EFFECT_DATE,EMPLOYEE_ID">
        <Columns>
            <%--   <tlk:GridNumericColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE"
                SortExpression="EMPLOYEE_CODE" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME"
                SortExpression="EMPLOYEE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                SortExpression="ORG_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                SortExpression="TITLE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE" UniqueName="EFFECT_DATE"
                DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Nhóm lương" DataField="SAL_TYPE_NAME" UniqueName="SAL_TYPE_NAME"
                SortExpression="SAL_TYPE_NAME" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Biểu thuế" DataField="TAX_TABLE_Name" UniqueName="TAX_TABLE_Name"
                SortExpression="TAX_TABLE_Name" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Thang lương" DataField="SAL_GROUP_NAME" UniqueName="SAL_GROUP_NAME"
                SortExpression="SAL_GROUP_NAME" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridBoundColumn>
            <tlk:GridNumericColumn HeaderText="Hệ số" DataField="FACTORSALARY" UniqueName="FACTORSALARY"
                SortExpression="FACTORSALARY" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="Lương cơ bản" DataField="SAL_BASIC" UniqueName="SAL_BASIC"
                DataFormatString="{0:n0}" SortExpression="SAL_BASIC" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="% Lương được hưởng" DataField="PERCENTSALARY"
                UniqueName="PERCENTSALARY" SortExpression="PERCENTSALARY" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="Thưởng hiệu quả công việc" DataField=""
                UniqueName="" SortExpression="" ShowFilterIcon="true"
                DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="Tổng Lương" DataField="SAL_TOTAL" UniqueName="SAL_TOTAL"
                SortExpression="SAL_TOTAL" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="Chi phí hỗ trợ khác" DataField="COST_SUPPORT"
                UniqueName="COST_SUPPORT" SortExpression="COST_SUPPORT" ShowFilterIcon="true"
                DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="OTHERSALARY1" DataField="OTHERSALARY1" UniqueName="OTHERSALARY1"
                SortExpression="OTHERSALARY1" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="OTHERSALARY2" DataField="OTHERSALARY2" UniqueName="OTHERSALARY2"
                SortExpression="OTHERSALARY2" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="OTHERSALARY3" DataField="OTHERSALARY3" UniqueName="OTHERSALARY3"
                SortExpression="OTHERSALARY3" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="OTHERSALARY4" DataField="OTHERSALARY4" UniqueName="OTHERSALARY4"
                SortExpression="OTHERSALARY4" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="OTHERSALARY5" DataField="OTHERSALARY5" UniqueName="OTHERSALARY5"
                SortExpression="OTHERSALARY5" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridNumericColumn HeaderText="Trạng thái" DataField="STATUS_NAME" UniqueName="STATUS_NAME"
                SortExpression="STATUS_NAME" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO" UniqueName="DECISION_NO"
                SortExpression="DECISION_NO" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Cấp nhân sự" DataField="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME"
                SortExpression="STAFF_RANK_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Ngạch lương" DataField="SAL_LEVEL_NAME" UniqueName="SAL_LEVEL_NAME"
                SortExpression="SAL_LEVEL_NAME" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Bậc lương" DataField="SAL_RANK_NAME" UniqueName="SAL_RANK_NAME"
                SortExpression="SAL_RANK_NAME" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridBoundColumn>
            <tlk:GridNumericColumn HeaderText="Khoản bổ sung" DataField="" UniqueName=""
                SortExpression="" ShowFilterIcon="true" DataFormatString="{0:n0}">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridNumericColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày ký" DataField="SIGN_DATE" UniqueName="SIGN_DATE"
                DataFormatString="{0:dd/MM/yyyy}" SortExpression="SIGN_DATE" ShowFilterIcon="true"
                FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Người ký" DataField="SIGN_NAME" UniqueName="SIGN_NAME"
                SortExpression="SIGN_NAME" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Chức danh người ký" DataField="SIGN_TITLE" UniqueName="SIGN_TITLE"
                SortExpression="SIGN_TITLE" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
            </tlk:GridBoundColumn>--%>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>

<div style="display:none">
<tlk:RadGrid PageSize="50" ID="rgAllow" runat="server" Height="200px" >
    <MasterTableView Caption="<%$ Translate: Phụ cấp theo tờ trình/QĐ %>">
        <Columns>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                SortExpression="ALLOWANCE_LIST_NAME" UniqueName="ALLOWANCE_LIST_NAME" />
            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
            </tlk:GridNumericColumn>
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                DataFormatString="{0:dd/MM/yyyy}" />
            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đóng bảo hiểm %>" DataField="IS_INSURRANCE"
                SortExpression="IS_INSURRANCE" UniqueName="IS_INSURRANCE" HeaderStyle-Width="100px">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </tlk:GridCheckBoxColumn>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>
    </div>
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
            $("#ctl00_MainContent_ctrlPortalSalary_rgSalary").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlPortalSalary_rgSalary_GridData").stop().animate({ height: winH-80 }, 0);
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