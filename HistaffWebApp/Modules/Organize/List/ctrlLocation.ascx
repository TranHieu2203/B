<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLocation.ascx.vb"
    Inherits="Profile.ctrlLocation" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupDirect" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupLevel" runat="server"></asp:PlaceHolder>
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="PaneToolBar" runat="server" Scrolling="None" Height="33px">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="PaneGrid" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgLocation" runat="server" AllowFilteringByColumn="True" AllowMultiRowSelection="True"
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0"
            GridLines="None" Height="100%" PageSize="50" ShowStatusBar="True">
            <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling UseStaticHeaders="true" AllowScroll="True" />
                <Resizing AllowColumnResize="true" />
                <ClientEvents OnRowDblClick="gridRowDblClick" />
            </ClientSettings>
            <MasterTableView CommandItemDisplay="None" EditMode="InPlace" DataKeyNames="ID,LOCATION_VN_NAME,LOCATION_EN_NAME,ADDRESS,PHONE, ACTFLG" ClientDataKeyNames="ID">
                <CommandItemSettings ExportToPdfText="Export to PDF" />
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                </RowIndicatorColumn>
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                </ExpandCollapseColumn>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                          HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                     </tlk:GridClientSelectColumn>

                    <tlk:GridBoundColumn DataField="ID"  HeaderStyle-Width="0px" Visible="true">
                    </tlk:GridBoundColumn>

                    <tlk:GridBoundColumn DataField="LOCATION_VN_NAME" FilterControlAltText="Filter LOCATION_VN_NAME column"
                        HeaderText="<%$ Translate: Tên đơn vị %>" ReadOnly="true" SortExpression="LOCATION_VN_NAME" 
                        UniqueName="LOCATION_VN_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CODE" FilterControlAltText="Filter CODE"
                        HeaderText="<%$ Translate: Mã công ty %>" ReadOnly="true" SortExpression="CODE"
                        UniqueName="CODE">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="LOCATION_SHORT_NAME" FilterControlAltText="Filter LOCATION_SHORT_NAME column"
                        HeaderText="<%$ Translate: Tên viết tắt %>" ReadOnly="true" SortExpression="LOCATION_SHORT_NAME"
                        UniqueName="LOCATION_SHORT_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NAME_VN" FilterControlAltText="Filter NAME_VN column"
                        HeaderText="<%$ Translate: Tên tiếng việt %>" ReadOnly="true" SortExpression="NAME_VN"
                        UniqueName="NAME_VN">
                        <HeaderStyle HorizontalAlign="Center"   Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="LOCATION_EN_NAME" FilterControlAltText="Filter LOCATION_EN_NAME column"
                        HeaderText="<%$ Translate: Tên tiếng anh %>" ReadOnly="true" SortExpression="LOCATION_EN_NAME"
                        UniqueName="LOCATION_EN_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="REGION_NAME" FilterControlAltText="Filter REGION_NAME column"
                        HeaderText="<%$ Translate: Vùng %>" ReadOnly="true" SortExpression="REGION_NAME"
                        UniqueName="REGION_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ADDRESS" FilterControlAltText="Filter ADDRESS column"
                        HeaderText="<%$ Translate: Địa chỉ theo GPKD %>" ReadOnly="true" SortExpression="ADDRESS"
                        UniqueName="ADDRESS">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="PHONE" FilterControlAltText="Filter PHONE column"
                        HeaderText="<%$ Translate: Số điện thoại %>" ReadOnly="true" SortExpression="PHONE"
                        UniqueName="PHONE">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="WORK_ADDRESS" FilterControlAltText="Filter WORK_ADDRESS column"
                        HeaderText="<%$ Translate: Địa điểm làm việc %>" ReadOnly="true" SortExpression="WORK_ADDRESS"
                        UniqueName="WORK_ADDRESS">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="PROVINCE_NAME" FilterControlAltText="Filter PROVINCE_NAME column"
                        HeaderText="<%$ Translate: Tỉnh/Thành phố %>" ReadOnly="true" SortExpression="PROVINCE_NAME"
                        UniqueName="PROVINCE_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="DISTRICT_NAME" FilterControlAltText="Filter DISTRICT_NAME column"
                        HeaderText="<%$ Translate: Quận/ Huyện %>" ReadOnly="true" SortExpression="DISTRICT_NAME"
                        UniqueName="DISTRICT_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="WARD_NAME" FilterControlAltText="Filter WARD_NAME column"
                        HeaderText="<%$ Translate: Xã/Phường %>" ReadOnly="true" SortExpression="WARD_NAME"
                        UniqueName="WARD_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="INS_LIST_NAME" FilterControlAltText="Filter INS_LIST_NAME column"
                        HeaderText="<%$ Translate: Đơn vị đóng BH %>" ReadOnly="true" SortExpression="INS_LIST_NAME"
                        UniqueName="INS_LIST_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>


                    <tlk:GridBoundColumn DataField="ACCOUNT_NUMBER" FilterControlAltText="Filter ACCOUNT_NUMBER column"
                        HeaderText="<%$ Translate: Số tài khoản %>" ReadOnly="true" SortExpression="ACCOUNT_NUMBER"
                        UniqueName="ACCOUNT_NUMBER">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BANK_NAME" FilterControlAltText="Filter NAME_VN column"
                        HeaderText="<%$ Translate: Ngân hàng %>" ReadOnly="true" SortExpression="BANK_NAME"
                        UniqueName="BANK_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BANK_BRANCH_NAME" FilterControlAltText="Filter BANK_BRANCH_NAME column"
                        HeaderText="<%$ Translate: Chi nhánh ngân hàng %>" ReadOnly="true" SortExpression="BANK_BRANCH_NAME"
                        UniqueName="BANK_BRANCH_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TAX_CODE" FilterControlAltText="Filter TAX_CODE column"
                        HeaderText="<%$ Translate: Mã số thuế %>" ReadOnly="true" SortExpression="TAX_CODE"
                        UniqueName="TAX_CODE">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CHANGE_TAX_CODE" FilterControlAltText="Filter CHANGE_TAX_CODE column"
                        HeaderText="<%$ Translate: Lần thay đổi MST thứ %>" ReadOnly="true" SortExpression="CHANGE_TAX_CODE"
                        UniqueName="CHANGE_TAX_CODE">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TAX_DATE" FilterControlAltText="Filter TAX_DATE column"
                        HeaderText="<%$ Translate: Ngày cấp MST %>" ReadOnly="true" SortExpression="TAX_DATE"
                        HeaderStyle-Width="70px" UniqueName="TAX_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>

                     <tlk:GridBoundColumn DataField="EMP_LAW_NAME" FilterControlAltText="Filter EMP_LAW_NAME column"
                        HeaderText="<%$ Translate: Người đại diện pháp luật, %>" ReadOnly="true" SortExpression="EMP_LAW_NAME"
                        UniqueName="EMP_LAW_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn DataField="EMP_SIGNCONTRACT_NAME" FilterControlAltText="Filter EMP_SIGNCONTRACT_NAME column"
                        HeaderText="<%$ Translate: Người đại diện ký HĐLĐ %>" ReadOnly="true" SortExpression="EMP_SIGNCONTRACT_NAME"
                        UniqueName="EMP_SIGNCONTRACT_NAME">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn DataField="BUSINESS_NUMBER" FilterControlAltText="Filter BUSINESS_NUMBER column"
                        HeaderText="<%$ Translate: Số giấy phép kinh doanh %>" ReadOnly="true" SortExpression="BUSINESS_NUMBER"
                        UniqueName="BUSINESS_NUMBER">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn DataField="BUSINESS_REG_DATE" FilterControlAltText="Filter BUSINESS_REG_DATE column"
                        HeaderText="<%$ Translate: Ngày đăng ký %>" ReadOnly="true" SortExpression="BUSINESS_REG_DATE"
                        UniqueName="BUSINESS_REG_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn DataField="WEBSITE" FilterControlAltText="Filter WEBSITE column"
                        HeaderText="<%$ Translate: Website %>" ReadOnly="true" SortExpression="WEBSITE"
                        UniqueName="WEBSITE">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn DataField="FAX" FilterControlAltText="Filter FAX column"
                        HeaderText="<%$ Translate: Fax %>" ReadOnly="true" SortExpression="FAX"
                        UniqueName="FAX">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>

                    <tlk:GridBoundColumn DataField="NOTE" FilterControlAltText="Filter NOTE column"
                        HeaderText="<%$ Translate: Ghi chú %>" ReadOnly="true" SortExpression="NOTE"
                        UniqueName="NOTE">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ACTFLG" FilterControlAltText="Filter ACTFLG column"
                        HeaderText="<%$ Translate: Trạng thái %>" ReadOnly="true" SortExpression="ACTFLG"
                        HeaderStyle-Width="70px" UniqueName="ACTFLG">
                        <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                    </tlk:GridBoundColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
        </tlk:RadGrid>
    </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:radsplitter>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindEmployee_Contract" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindOrganization" runat="server"></asp:PlaceHolder>
<tlk:radwindowmanager id="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1350"
            Height="550" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:radwindowmanager>
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (item.get_commandName() == "CREATE") {
                var oWindow = radopen('Dialog.aspx?mid=Organize&fid=ctrlLocationNewEdit&group=list', "rwPopup");
                var pos = $("html").offset();
                oWindow.moveTo(pos.center);
                oWindow.setSize(1350, 550);
                args.set_cancel(true);
            }
            if (item.get_commandName() == "EDIT") {
                var bCheck = $find('<%= rgLocation.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    var id = $find('<%# rgLocation.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                    var oWindow = radopen('Dialog.aspx?mid=Organize&fid=ctrlLocationNewEdit&group=list&ID=' + id, "rwPopup");
                    var pos = $("html").offset();
                    oWindow.moveTo(pos.center);
                    oWindow.setSize(1350, 550);
                }
                args.set_cancel(true);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        <%--function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboDistrict.ClientID %>':
                    cbo = $find('<%= cboProvince.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboWard.ClientID %>':
                    cbo = $find('<%= cboDistrict.ClientID %>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }
            if (!value) {
                value = 0;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;
            context["value"] = sender.get_value();
        }--%>

        <%--function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboProvince.ClientID %>':
                    cbo = $find('<%= cboDistrict.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboWard.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                default:
                    break;
            }
        }--%>

        //function clearSelectRadcombo(cbo) {
        //    if (cbo) {
        //        cbo.clearItems();
        //        cbo.clearSelection();
        //        cbo.set_text('');
        //    }
        //}

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

        function gridRowDblClick(sender, eventArgs) {
            var id = $find('<%# rgLocation.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Organize&fid=ctrlLocationNewEdit&group=list&ID=' + id, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.center);
            oWindow.setSize(1350, 550);
        }

        function OnClientClose(oWnd, args) {
            $find("<%= rgLocation.ClientID %>").get_masterTableView().rebind();
        }

    </script>
</tlk:radcodeblock>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
