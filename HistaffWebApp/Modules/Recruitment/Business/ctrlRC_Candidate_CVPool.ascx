<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_Candidate_CVPool.ascx.vb"
    Inherits="Recruitment.ctrlRC_Candidate_CVPool" %>
<%@ Import Namespace="Common" %>
<style type="text/css">
    .form-table td
    {
        padding: 5px;
    }

    fieldset
    {
        padding: 0.35em 0.625em 0.75em !important;
        margin: 0 8px !important;
        border: 1px solid #c0c0c0 !important;
    }

        fieldset legend
        {
            border-bottom: none !important;
            width: inherit !important;
            margin-bottom: 0px !important;
        }

    .RadTabStripTop_Office2007 .rtsLevel1
    {
        position: fixed;
        z-index: 100;
    }

    .lb
    {
        width: 115px;
    }

    .msg-error
    {
        margin-left: 420px;
        font-family: "Segoe UI", Arial, Verdana, Tahoma, Times;
        padding: 0px 33px 0px 0px;
        border-radius: 5px;
        text-align: center;
        position: fixed;
        left: 50%;
        top: 50%; /* margin-right: -184px; */
        margin-top: -267px;
        overflow: hidden;
        z-index: 99999;
    }

    .AutoHeight
    {
        height: auto !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="ToolbarPane" runat="server" Height="32px" Scrolling="None" visible="false">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="DetailPane" runat="server" Width="100%" CssClass="AutoHeight">
        <tlk:RadPanelBar runat="server" ID="RadPanelBar1" Width="100%" ExpandMode="MultipleExpandedItems"
            OnClientItemClicking="HeightGridClick">
            <Items>
                <tlk:RadPanelItem Expanded="true" Text="<%$ Translate: Tìm kiếm cơ bản %>">
                    <ContentTemplate>
                        <table class="table-form" id="table_01">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Mã ứng viên")%>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hidProgramID" runat="server" />
                                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="100%">
                                    </tlk:RadTextBox>
                                </td>
                                <td class="lb">
                                    <%# Translate("Họ và tên")%>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" Width="100%">
                                    </tlk:RadTextBox>
                                </td>
                                <td class="lb" style="display:none">
                                    <%# Translate("Trạng thái ứng viên")%>
                                </td>
                                <td style="display:none">
                                    <tlk:RadComboBox runat="server" ID="cboEmpStatus">
                                    </tlk:RadComboBox>
                                </td>
                                <td class="lb">
                                    <%# Translate("Giới tính")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox runat="server" ID="cboGender">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </tlk:RadPanelItem>
                <tlk:RadPanelItem Text="<%$ Translate:  Tìm kiếm nâng cao %>">
                    <ContentTemplate>
                        <table class="table-form" id="table_02">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Tình trạng hôn nhân")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox ID="cboTinhTrangHN" runat="server">
                                    </tlk:RadComboBox>
                                </td>
                                <td class="lb">
                                    <%# Translate("Quốc gia")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox ID="cboNation" runat="server">
                                    </tlk:RadComboBox>
                                </td>
                                <td class="lb">
                                    <%# Translate("Nơi sinh")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox runat="server" ID="cboProvince">
                                    </tlk:RadComboBox>
                                </td>
                                <td class="lb">
                                    <%# Translate("Quốc tịch")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox runat="server" ID="cboNationality">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Ngày sinh (Từ ngày)")%>
                                </td>
                                <td>
                                    <tlk:RadDatePicker ID="rdBirthDateFrom" runat="server">
                                    </tlk:RadDatePicker>
                                </td>
                                <td class="lb">
                                    <%# Translate("Ngày sinh (Đến ngày)")%>
                                </td>
                                <td>
                                    <tlk:RadDatePicker ID="rdBirthDateTo" runat="server">
                                    </tlk:RadDatePicker>
                                </td>
                                <td class="lb">
                                    <%# Translate("Tỉnh/TP")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox runat="server" ID="cboNav_Province">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="9">
                                    <b></b>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Trình độ văn hóa")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox runat="server" ID="cboTrinhDoVanHoa">
                                    </tlk:RadComboBox>
                                </td>
                                <td class="lb">
                                    <%# Translate("Trình độ học vấn")%>
                                </td>
                                <td >
                                    <tlk:RadComboBox runat="server" ID="cboTrinhDoHocVan">
                                    </tlk:RadComboBox>
                                </td>
                                <td class="lb" style="display:none">
                                    <%# Translate("Trình độ chuyên môn")%>
                                </td>
                                <td style="display:none">
                                    <tlk:RadComboBox runat="server" ID="cboTrinhDoChuyenMon">
                                    </tlk:RadComboBox>
                                </td>
                                <td class="lb">
                                    <%# Translate("Chuyên nghành")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox ID="cboChuyenNganh" runat="server">
                                    </tlk:RadComboBox>
                                </td>
                                <td class="lb" style="display:none">
                                    <%# Translate("Trường học")%>
                                </td>
                                <td style="display:none">
                                    <tlk:RadComboBox ID="cboTruongHoc" runat="server">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                
                                <td class="lb" style="display:none">
                                    <%# Translate("Bằng cấp")%>
                                </td>
                                <td style="display:none">
                                    <tlk:RadComboBox ID="cboBangCap" runat="server">
                                    </tlk:RadComboBox>
                                </td>
                                <td class="lb" style="display:none">
                                    <%# Translate("Xếp loại")%>
                                </td>
                                <td style="display:none">
                                    <tlk:RadComboBox ID="cboXepLoai" runat="server">
                                    </tlk:RadComboBox>
                                </td>
                                <td class="lb" style="display:none">
                                    <%# Translate("Trình độ tin học")%>
                                </td>
                                <td style="display:none">
                                    <tlk:RadComboBox ID="cboTrinhDoTinHoc" runat="server">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                            <tr style="display:none">
                                <td class="lb">
                                    <%# Translate("Trình độ ngoại ngữ")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox ID="cboTrinhDoNgoaiNgu" runat="server">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="9">
                                    <b></b>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Chiều cao từ (cm)")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtChieuCaoTu" runat="server" Width="100%">
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td class="lb">
                                    <%# Translate("Chiều cao đến (cm)")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtChieuCaoDen" runat="server" Width="100%">
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td class="lb">
                                    <%# Translate("Cân nặng từ (kg)")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtCanNangTu" runat="server" Width="100%">
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td class="lb">
                                    <%# Translate("Cân nặng đến (kg)")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox ID="txtCanNangDen" runat="server" Width="100%">
                                    </tlk:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Loại sức khỏe")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox ID="cboLoaiSucKhoe" runat="server">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </tlk:RadPanelItem>
            </Items>
        </tlk:RadPanelBar>
    </tlk:RadPane>
    <tlk:RadPane ID="DetailPaneSearch" runat="server" BorderStyle="None" MinHeight="35"
        MaxHeight="35" Height="35px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                        Text="<%$ Translate: Tìm %>">
                    </tlk:RadButton>
                </td>
                <td>
                    <tlk:RadButton ID="btnLuu" runat="server"  CausesValidation="false" Width="80px"
                        Text="<%$ Translate: Lưu %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPaneData" runat="server" Scrolling="None" Width="100%" Height="560px">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true"
            AllowMultiRowEdit="True" AllowMultiRowSelection="True" CellSpacing="0" GridLines="None">
            <MasterTableView DataKeyNames="CANDIDATE_ID,CANDIDATE_CODE,STATUS_ID" ClientDataKeyNames="CANDIDATE_ID,CANDIDATE_CODE,STATUS_ID"
                FilterItemStyle-HorizontalAlign="Center">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="CANDIDATE_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CANDIDATE_CODE"
                        SortExpression="CANDIDATE_CODE" UniqueName="CANDIDATE_CODE">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên ứng viên %>" DataField="FULL_NAME_VN"
                        SortExpression="FULL_NAME_VN" UniqueName="FULL_NAME_VN" />


                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                        SortExpression="GENDER_NAME" UniqueName="GENDER_NAME" HeaderStyle-Width="80px" />

                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Nguồn ứng viên %>" DataField="RC_SOURCE_REC_NAME"
                        SortExpression="RC_SOURCE_REC_NAME" UniqueName="RC_SOURCE_REC_NAME" />

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên công ty %>" DataField="ORG_NAME"
                        SortExpression="ORG_NAME" UniqueName="ORG_NAME" />


                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí mong muốn 1 %>" DataField="POS_1"
                        SortExpression="POS_1" UniqueName="POS_1" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí mong muốn 2 %>" DataField="POS_2"
                        SortExpression="POS_2" UniqueName="POS_2" />
                    <tlk:GridNumericColumn HeaderText="Mức lương mong muốn " DataField="SAL_MM" UniqueName="SAL_MM"
                        SortExpression="SAL_MM" ShowFilterIcon="true" DataFormatString="{0:n0}" />


                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME_VN"
                        SortExpression="TITLE_NAME_VN" UniqueName="TITLE_NAME_VN" />--%>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày chỉnh sửa %>" DataField="MODIFIED_DATE"
                        SortExpression="MODIFIED_DATE" UniqueName="MODIFIED_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái tuyển dụng %>" DataField="CANDIDATE_STATUS_NAME"
                        SortExpression="CANDIDATE_STATUS_NAME" UniqueName="CANDIDATE_STATUS_NAME" />
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
                <HeaderStyle Width="150px" />
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnRowDblClick="gridRowDblClick" />
            </ClientSettings>
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<%--<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            OnClientBeforeClose="OnClientBeforeClose" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>--%>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenInsertWindow() {
            
            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&state=New&noscroll=1&reload=1&FormType=0', "rwPopup");
            var pos = $("html").offset();
            var desiredHeight = $(window).height() - 50;
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), desiredHeight);
        }
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "SAVE") {
            }
            else if (item.get_commandName() == "PRINT" || item.get_commandName() == "UNLOCK") {
                enableAjax = false;
            }
            else if (item.get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            }
            else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow("Edit");
                    // OpenEditWindow("Normal");
                    args.set_cancel(true);
                }

            }
            else if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'PRINT' || args.get_item().get_commandName() == 'UNLOCK' || args.get_item().get_commandName() == 'PREVIOUS' || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }

        }
        function OpenEditWindow(states) {
            var empId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('CANDIDATE_CODE');
            var gUId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('CANDIDATE_ID');

            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&gUID=' + gUId + '&Can=' +
            empId + '&state=' + states + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            var desiredHeight = $(window).height() - 50;
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), desiredHeight);

        }
        function OpenEditTransfer(sender, args) {
            enableAjax = false;
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseWindow() {
            var oWindow = GetRadWindow();
            if (oWindow) oWindow.close(1);
        }

        //Function này để hủy postback khi nhập lương cơ bản rồi enter 
        //Lỗi này chỉ gặp ở trình duyệt chrome và ie.
        function OnKeyBSPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                eventArgs.set_cancel(true);
            }
        }

        function gridRowDblClick(sender, eventArgs) {
            //OpenViewProfile("Normal");
            OpenEditWindow("Normal");
        }

        function OpenViewProfile(states) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var gUId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('CANDIDATE_ID');
            var empId = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('CANDIDATE_CODE');
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_CVPoolDtl&group=Business&gUID=' + gUId + '&Can=' +
            empId + '&state=' + states + '', "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            return 2;
        }

        function OnClientBeforeClose(sender, eventArgs) {
            var arg = eventArgs.get_argument();
            if (!arg) {
                if (!confirm("Bạn có muốn đóng màn hình không?")) {
                    //if cancel is clicked prevent the window from closing
                    args.set_cancel(true);
                }
            }
        }

        function OnClientClose(oWnd, args) {
            $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
        }

        function HeightGrid() {
            var itemHeight_01 = $('#table_01').height();
            var itemHeight_02 = $('#table_02').height();
            var availHeight = $(window).height();
            var difference = availHeight - itemHeight_01 - itemHeight_02 - 35 - 20;
            $('#RadPaneData').css('height', difference + 'px');
            //alert(difference);
        }

        function HeightGridClick(sender, args) {
            HeightGrid();
            //  enumerateChildItems(args.get_item());
            //  $('#RadPaneData').css('height', 230 + 'px');
        }

        //        function enumerateChildItems(myitem) {
        //            for (var i = 0; i < myitem.get_items().get_count(); i++) {
        //                myitem.get_items().getItem(i).expand()
        //                enumerateChildItems(myitem.get_items().getItem(i));
        //            }
        //        }yText("Tìm kiếm cơ bản");
        //        var item = panelBar.find

        //        panelItem.disable();

        //        $('#table_01').click(function () { HeightGrid(); });

    </script>
</tlk:RadScriptBlock>
