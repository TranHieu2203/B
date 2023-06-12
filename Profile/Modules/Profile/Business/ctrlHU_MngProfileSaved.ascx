<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_MngProfileSaved.ascx.vb"
    Inherits="Profile.ctrlHU_MngProfileSaved" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<%--<link href="/Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />--%>
<style>
    #ctl00_MainContent_ctrlHU_MngProfileSaved_rgEmployeeList_GridData{
        height: 81% !important;
    }
</style>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <table class="table-form padding-10">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbFromDate" runat="server" Text="Ngày vào công ty từ" />
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbToDate" runat="server" Text="Đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>

                        <td colspan="2">
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                        </td>
                        <td style="padding-left: 20px;">
                            <tlk:RadButton runat="server" Text="Tìm" ID="btnSearch" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                        <%--  <td >
                            <button type="button" class="btn btn-info btn-sm" data-toggle="modal" data-target="#myModal">In Process</button>
                        </td>--%>
                    </tr>
                    <tr style="visibility: hidden;">
                        <td class="lb">
                            <asp:Label ID="lbGhiChu" runat="server" Text="Ghi chú"></asp:Label>
                        </td>
                        <td colspan="4">
                            <tlk:RadTextBox ID="txtGhiChu" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgEmployeeList" runat="server" Height="100%" EnableHeaderContextMenu="true">
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,ORG_DESC,IMAGE" ClientDataKeyNames="ID,EMPLOYEE_CODE,IMAGE" >
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="TITLE_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_ABBR" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="FULLNAME_VN" UniqueName="FULLNAME_VN"
                                SortExpression="FULLNAME_VN" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                SortExpression="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME_VN" UniqueName="TITLE_NAME_VN"
                                SortExpression="TITLE_NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="SUBMIT_PROFILE" UniqueName="SUBMIT_PROFILE"
                                SortExpression="SUBMIT_PROFILE" HeaderStyle-Width="130px" Visible="true" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <%--<HeaderStyle Width="120px" />--%>
                </tlk:RadGrid>
            </tlk:RadPane>
          
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="900" Height="600px"
            OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
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

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_EmployeeMng_RadSplitter3');
        //        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenEditWindow(states) {
            var empId = $find('<%= rgEmployeeList.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_MngProfileSavedNewEdit&group=Business&emp=' + empId + '', "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30);
            oWindow.center(); */
        }

        function OpenInsertWindow() {
            var empId = $find('<%= rgEmployeeList.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=' + empId + '&state=' + states, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30);
            oWindow.center(); */
        }

        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == 'CREATE') {

                var bCheck = $find('<%= rgEmployeeList.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }
                else if (bCheck > 1) {
                    var m1 = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                        var n1 = noty({ text: m1, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n1.options.id); }, 10000);
                        args.set_cancel(true);
                    }
                    else {
                        OpenEditWindow("Edit");
                        args.set_cancel(true);
                    }
            } else if (item.get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgEmployeeList.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }
                else if (bCheck > 1) {
                    var m1 = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                        var n1 = noty({ text: m1, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n1.options.id); }, 10000);
                        args.set_cancel(true);
                    }
                    else {
                        OpenEditWindow("Edit");
                        args.set_cancel(true);
                    }

            }

        if (item.get_commandName() == "EXPORT") {
            enableAjax = false;
        }

        if (item.get_commandName() == "PRINT") {
            enableAjax = false;
        }

    }

    function OnClientClose(oWnd, args) {
        postBack(oWnd.get_navigateUrl());
    }

    function postBack(url) {
        var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(url); //Making ajax request with the argument
        }

        function btnPrintSupportClick(sender, args) {
            var bCheck = $find('<%= rgEmployeeList.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }
            //            if (bCheck > 1) {
            //                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
            //                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
            //                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            //                args.set_cancel(true);
            //                return;
            //            }
            enableAjax = false;
        }

    </script>
    <%--<script src="/Scripts/jquery-3.2.1.min.js"></script>
     
    <script src="/Scripts/bootstrap.min.js"></script>--%>
</tlk:RadScriptBlock>
