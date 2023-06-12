<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpInfomations.ascx.vb"
    Inherits="Profile.ctrlHU_EmpInfomations" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Profile.ProfileCommon" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <%--<tlk:RadToolBar ID="tbarAssetMngs" runat="server" OnClientButtonClicking="clientButtonClicking" />--%>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100%">
                <tlk:RadGrid PageSize="50" ID="rgAssetMng" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" EditMode="InPlace" ClientDataKeyNames="ID,IMAGE,MOBILE_PHONE,BIRTH_DATE,FULLNAME_VN,TITLE_NAME_VN">
                        <Columns>
                           <%-- <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBinaryImageColumn HeaderText ="Hình ảnh" DataField ="IMAGE_BINARY" UniqueName ="IMAGE_BINARY"
                            ImageHeight="80px" ImageWidth="80px" ResizeMode="Fit" DataAlternateTextField="IMAGE_BINARY"
                            DataAlternateTextFormatString="Image of {0}">
                                 <HeaderStyle Width="100px" />
                                
                            </tlk:GridBinaryImageColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên nhân viên %>" DataField="FULLNAME_VN"
                                SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME_VN"
                                SortExpression="TITLE_NAME_VN" UniqueName="TITLE_NAME_VN">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="BIRTH_DATE" UniqueName="BIRTH_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày tham gia hội sở %>" DataField="JOIN_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="JOIN_DATE" UniqueName="JOIN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại %>" DataField="MOBILE_PHONE"
                                SortExpression="MOBILE_PHONE" UniqueName="MOBILE_PHONE">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            --%>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move" Modal="true"
            ShowContentDuringLoad="false" OnClientClose="popupclose">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_AssetMng_RadSplitter3');
        }

        function CheckValidate() {
            var bCheck = $find('<%= rgAssetMng.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            return 0;
        }
        function OpenNew() {
            var extented = '';
            var bCheck = $find('<%= rgAssetMng.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck > 0) {
                gUID = $find('<%= rgAssetMng.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                if (gUID)
                    extented = '&IsHasInfoEmp=true&gUID=' + gUID;
            }

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_AssetMngNewEdit&group=Business' + extented, "_self");

            /*window.open('/Default.aspx?mid=Profile&fid=ctrlHU_AssetMngNewEdit&group=Business', "_self");
            oWindow.setSize(900, 500);
            oWindow.center(); */
        }
        function OpenEdit() {
            var id = $find('<%= rgAssetMng.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var item = $find('<%= rgAssetMng.ClientID%>').get_masterTableView().get_selectedItems()[0];
            var cellValue = $(item.get_cell("STATUS_ID")).text();

            if (cellValue != '<%= HU_ASSET_MNG_STATUS.ASSET_WAIT %>') {
                var m = '<%= Translate("Không thể thực hiện thao tác sửa với bản ghi này.") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                args.set_cancel(true);
                return;
            }

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_AssetMngNewEdit&group=Business&gUID=' + id + '', "_self"); /*
            oWindow.setSize(900, 500);
            oWindow.center(); */
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'CREATE') {
                var bCheck = $find('<%= rgAssetMng.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                    return;
                }

                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgAssetMng.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                    return;
                }
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                    return;
                }

                OpenEdit();
                args.set_cancel(true);
            }
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEdit();
        }

        var enableAjax = true;
        var oldSize = 0;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgAssetMng.ClientID %>").get_masterTableView().rebind();
            }

        }

    </script>
</tlk:RadCodeBlock>
