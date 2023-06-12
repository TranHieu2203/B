<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_WorkInfo.ascx.vb"
    Inherits="Profile.ctrlHU_WorkInfo" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .hide-button input{
        display: none;
    }
</style>
<%--<link href="/Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />--%>
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
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <table class="table-form padding-10">
                    <tr>
                        <td class="lb">
                             <asp:Label ID="lbFromDate" runat="server" Text="Ngày bắt đầu từ"/>
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
                    <ClientSettings >
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        <%--<ClientEvents OnCommand="ValidateFilter" />--%>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,FILE_NAME,UPLOAD_FILE,ORG_DESC" ClientDataKeyNames="ID,FIlE_NAME,UPLOAD_FILE,ORG_DESC">
                        <Columns>
                           <%-- <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="TITLE_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_ABBR" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="FULLNAME_VN"
                                UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<Chức danh" DataField="TITLE_NAME_VN"
                                UniqueName="TITLE_NAME_VN" SortExpression="TITLE_NAME_VN" />
                            
                            <tlk:GridBoundColumn HeaderText="Cấp nhân sự" DataField="STAFF_RANK_NAME"
                                UniqueName="STAFF_RANK_NAME" SortExpression="STAFF_RANK_NAME">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="Ngày vào công ty" DataField="JOIN_DATE"
                                UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Ngày vào tập đoàn" DataField="JOIN_DATE_STATE"
                                UniqueName="JOIN_DATE_STATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE_STATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="WORK_STATUS_NAME"
                                UniqueName="WORK_STATUS_NAME" SortExpression="WORK_STATUS_NAME" HeaderStyle-Width="130px"
                                Visible="true" />
                            <tlk:GridBoundColumn HeaderText="Nhóm máu" DataField="NHOM_MAU"
                                UniqueName="NHOM_MAU" SortExpression="NHOM_MAU">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </tlk:GridBoundColumn>--%>
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <%--<HeaderStyle Width="120px" />--%>                    
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None" Height="45px" Visible ="false">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbTemplatePrint" runat="server" Text="Biễu mẫu hỗ trợ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" Width="400px" ID="cboPrintSupport">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnPrintSupport" runat="server" Text="Hỗ trợ in"
                                OnClientClicking="btnPrintSupportClick" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
        </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1100"
            Height="550" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        function <%=ClientID%>_OnClientClose(oWnd, args) {
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(<%=ClientID%>_OnClientClose);
            var arg = args.get_argument();
            if(arg == null)
            {
                arg = new Object();
                arg.ID = 'Cancel';
            }
            if (arg) {
                var ajaxManager = $find("<%= AjaxManagerId %>");
                ajaxManager.ajaxRequest("<%= ClientID %>_PopupPostback:" + arg.ID);
            }
        }
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


        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {
            /*window.open('/Default.aspx?mid=Profile&fid=ctrlHU_WorkInfoNewEdit&group=Business&noscroll=1', "_self");*/


            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_WorkInfoNewEdit&group=Business&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.center);
            oWindow.setSize(1100, 550);
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgEmployeeList.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgEmployeeList.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            /*window.open('/Default.aspx?mid=Profile&fid=ctrlHU_WorkInfoNewEdit&group=Business&noscroll=1&ID=' + id, "_self");*/


            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_WorkInfoNewEdit&group=Business&noscroll=1&ID=' + id, "rwPopup");
            var pos = $("html").offset();
            var desiredHeight = $(window).height() - 50;
            oWindow.moveTo(pos.center);
            oWindow.setSize(1100, 550);

            return 2;
        }

        function OnClientButtonClicking(sender, args) {
            var m;
            var bCheck;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }

                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "NEXT"||args.get_item().get_commandName() == "IMPORT") {
                enableAjax = false;
            }
        }

        function gridRowDblClick(sender, eventArgs) {
            if (OpenEdit(true) == 0) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }
            if (OpenEdit(true) == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW)%>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }
        }

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $get("<%= btnSearch.ClientId %>").click();
            }

        }

        function getUrlVars() {
            var vars = {};
            var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
                vars[key] = value;
            });
            return vars;
        }

        function OnClientClose(oWnd, args) {
            $find("<%= rgEmployeeList.ClientID %>").get_masterTableView().rebind();
        }

    </script>
    <%--<script src="/Scripts/jquery-3.2.1.min.js"></script>
     
    <script src="/Scripts/bootstrap.min.js"></script>--%>
</tlk:RadScriptBlock>
