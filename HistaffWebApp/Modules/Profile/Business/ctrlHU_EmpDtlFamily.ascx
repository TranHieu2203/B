<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlFamily.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlFamily" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    #ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_rgFamily_ctl00_ctl02_ctl02_FilterCheckBox_IS_DEDUCT
    {
        display: none;
    }
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_RightPane
    {
        height:0px !important ;
    }
    .rgFilterRow td {
        text-align: center;
    }
    #ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_rgFamily_ctl00_ctl02_ctl02_FilterCheckBox_IS_OWNER
    {
        display: none;
    }
</style>
<asp:HiddenField ID="hidFamilyID" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>

    <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgFamily" runat="server" AllowFilteringByColumn="true"
            Height="100%">
            <MasterTableView DataKeyNames="ID"
                ClientDataKeyNames="ID">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridBoundColumn DataField="RELATION_NAME" HeaderText="<%$ Translate: Quan hệ %>"
                        UniqueName="RELATION_NAME" Visible="True" SortExpression="RELATION_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FULLNAME" HeaderText="<%$ Translate: Họ và tên%>"
                        UniqueName="FULLNAME" Visible="True" SortExpression="FULLNAME" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    
                    <tlk:GridDateTimeColumn UniqueName="BIRTH_DATE" HeaderText="<%$ Translate: Ngày sinh%>"
                        ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="BIRTH_DATE" SortExpression="BIRTH_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn DataField="ID_NO" HeaderText="<%$ Translate: CMND %>" UniqueName="ID_NO"
                        Visible="True" EmptyDataText="" SortExpression="ID_NO" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TAXTATION" HeaderText="<%$ Translate: Mã thuế %>" UniqueName="TAXTATION"
                        Visible="True" EmptyDataText="" SortExpression="TAXTATION" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    
                    <tlk:GridCheckBoxColumn DataField="IS_DEDUCT" UniqueName="IS_DEDUCT" HeaderText="<%$ Translate: Thuộc đối tượng giảm trừ%>"
                        SortExpression="IS_DEDUCT" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        >
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridDateTimeColumn DataField="DEDUCT_REG" HeaderText="<%$ Translate: Ngày đăng ký giảm trừ%>"
                        UniqueName="DEDUCT_REG" Visible="true" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_REG" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn DataField="DEDUCT_FROM" HeaderText="<%$ Translate: Ngày giảm trừ%>"
                        UniqueName="DEDUCT_FROM" Visible="true" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_FROM" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn DataField="DEDUCT_TO" HeaderText="<%$ Translate: Ngày kết thúc %>"
                        UniqueName="DEDUCT_TO" Visible="true" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_TO" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridCheckBoxColumn DataField="IS_OWNER" UniqueName="IS_OWNER" HeaderText="<%$ Translate: Là chủ hộ%>"
                        SortExpression="IS_OWNER" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        >
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_CODE" HeaderText="<%$ Translate: Giấy khai sinh%>"
                        UniqueName="BIRTH_CODE" Visible="True" SortExpression="BIRTH_CODE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="QUYEN" HeaderText="<%$ Translate: Quyển số%>"
                        UniqueName="QUYEN" Visible="True" SortExpression="QUYEN" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_NATION_NAME" HeaderText="<%$ Translate: Quốc tịch%>"
                        UniqueName="BIRTH_NATION_NAME" Visible="True" SortExpression="BIRTH_NATION_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_PROVINCE_NAME" HeaderText="<%$ Translate: Tỉnh/TP Nơi sinh%>"
                        UniqueName="BIRTH_PROVINCE_NAME" Visible="True" SortExpression="BIRTH_PROVINCE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_DISTRICT_NAME" HeaderText="<%$ Translate: Quận/Huyện Nơi sinh%>"
                        UniqueName="BIRTH_DISTRICT_NAME" Visible="True" SortExpression="BIRTH_DISTRICT_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BIRTH_WARD_NAME" HeaderText="<%$ Translate: Phường/Xã Nơi sinh%>"
                        UniqueName="BIRTH_WARD_NAME" Visible="True" SortExpression="BIRTH_WARD_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                </Columns>
                <%--<HeaderStyle Width="100px" />--%>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">


        function ValidateFilter(sender, eventArgs) {

        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter();
                else
                    ResizeSplitterDefault();
            }
            else if (item.get_commandName() == "NEW") {
                enableAjax = false;
            }
            else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }
        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_RadSplitter2');
        //        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_rdBirthDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_rdDeductReg_dateInput').val('');
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_rdDeductFrom_dateInput').val('');
            $('#ctl00_MainContent_ctrlHU_EmpDtl_ctrlHU_EmpDtlFamily_rdDeductTo_dateInput').val('');
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter2.ClientID%>");
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter2.ClientID%>");
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane4.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {

            }
        }

        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }
        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }
        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {


            }

            if (!value) {
                value = 0;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;
            context["value"] = sender.get_value();

        }

    </script>
</tlk:RadScriptBlock>
