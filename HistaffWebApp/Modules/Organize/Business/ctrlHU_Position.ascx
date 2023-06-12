<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Position.ascx.vb"
    Inherits="Profile.ctrlHU_Position" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .rgFilterRow > td:first-child {
        background-position: 3px center;
    }
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Title_RadPane2 
    {
        padding-left: 6px;    
    }
</style>
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidOrgName" runat="server" />
<asp:HiddenField ID="hidCommittee" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="250" Width="300px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane5" runat="server" Height="35px">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Visible="false">
                <asp:ValidationSummary ID="valSum" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTitleGroup" runat="server">
                            </tlk:RadComboBox>
                            <asp:CustomValidator ID="cusTitleGroup" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm chức danh %>"
                                ToolTip="<%$ Translate: Bạn phải chọn Nhóm chức danh %>" ClientValidationFunction="cusTitleGroup">
                            </asp:CustomValidator><asp:CustomValidator ID="cvalTitleGroup" ControlToValidate="cboTitleGroup"
                                runat="server" ErrorMessage="<%$ Translate: Nhóm chức danh không tồn tại hoặc đã ngừng áp dụng. %>"
                                ToolTip="<%$ Translate: Nhóm chức danh không tồn tại hoặc đã ngừng áp dụng. %>">
                            </asp:CustomValidator><a href="Default.aspx?mid=Organize&fid=ctrlHU_TitleNewEdit&group=Business">Edit</a>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Readonly" ReadOnly="true">
                            </tlk:RadTextBox><asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Mã chức danh %>" ToolTip="<%$ Translate: Bạn phải nhập Mã chức danh %>">
                            </asp:RequiredFieldValidator><asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode"
                                runat="server" ErrorMessage="<%$ Translate: Mã chức danh đã tồn tại. %>" ToolTip="<%$ Translate: Mã chức danh đã tồn tại. %>">
                            </asp:CustomValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                                ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                        </td>
                        <td class="lb">
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtNameVN" runat="server">
                            </tlk:RadTextBox><asp:RequiredFieldValidator ID="reqNamVN" ControlToValidate="txtNameVN"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên chức danh %>" ToolTip="<%$ Translate: Bạn phải nhập Tên chức danh %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Height="70" Width="100%">
                            </tlk:RadTextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                                ControlToValidate="txtRemark" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                         <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,COLOR,ACTFLG,MASTER_NAME,INTERIM_NAME,ORG_DESC,MASTER,INTERIM" ClientDataKeyNames="ID, CODE,NAME_VN,JOB_NAME,
            ORG_NAME,IS_OWNER,LM_NAME,IS_NONPHYSICAL,REMARK,BOTH,ORG_DESC">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã %>" DataField="CODE" HeaderStyle-Width="80px"
                                HeaderStyle-CssClass="text-align-left"
                                HeaderTooltip="<%$ Translate: Mã %>"
                                UniqueName="CODE" SortExpression="CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng việt %>" DataField="NAME_VN"
                                HeaderStyle-CssClass="text-align-left"
                                HeaderTooltip="<%$ Translate: Tên tiếng việt %>"
                                UniqueName="NAME_VN" SortExpression="NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng anh %>" DataField="NAME_EN"
                                HeaderStyle-CssClass="text-align-left"
                                HeaderTooltip="<%$ Translate: Tên tiếng anh %>"
                                UniqueName="NAME_EN" SortExpression="NAME_EN" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm cấp bậc %>" DataField="TITLE_GROUP_NAME"
                                HeaderStyle-CssClass="text-align-left"
                                HeaderTooltip="<%$ Translate: Nhóm cấp bậc %>"
                                UniqueName="TITLE_GROUP_NAME" SortExpression="TITLE_GROUP_NAME" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Master %>" DataField="MASTER_NAME"
                                HeaderStyle-CssClass="text-align-left"
                                HeaderTooltip="<%$ Translate: Master %>"
                                UniqueName="MASTER_NAME" SortExpression="MASTER_NAME" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Interim %>" DataField="INTERIM_NAME"
                                HeaderStyle-CssClass="text-align-left"
                                HeaderTooltip="<%$ Translate: Interim %>"
                                UniqueName="INTERIM_NAME" SortExpression="INTERIM_NAME" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                HeaderStyle-CssClass="text-align-left"
                                HeaderTooltip="<%$ Translate: Tổ chức %>"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" />--%>
                            <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Trưởng  %>" DataField="IS_OWNER"
                                HeaderTooltip="<%$ Translate: Trưởng  %>"
                                HeaderStyle-Width="90px" UniqueName="IS_OWNER" SortExpression="IS_OWNER" AllowFiltering="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: QLTT %>" DataField="LM_CODE"
                                HeaderStyle-CssClass="text-align-left"
                                HeaderTooltip="<%$ Translate: Quản lý trực tiếp %>"
                                HeaderStyle-Width="90px" UniqueName="LM_CODE" SortExpression="LM_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: QLPD %>" DataField="CSM_CODE"
                                HeaderStyle-CssClass="text-align-left"
                                HeaderTooltip="<%$ Translate: Quản lý phê duyệt %>"
                                HeaderStyle-Width="90px" UniqueName="CSM_CODE" SortExpression="CSM_CODE" />--%>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Master %>" DataField="MASTER_NAME"
                                HeaderStyle-Width="100px" UniqueName="MASTER_NAME" SortExpression="MASTER_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Interim %>" DataField="INTERIM_NAME"
                                HeaderStyle-Width="100px" UniqueName="INTERIM_NAME" SortExpression="INTERIM_NAME" />--%>
                            <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Non Physical %>" DataField="IS_NONPHYSICAL"
                                HeaderStyle-Width="125px" UniqueName="IS_NONPHYSICAL" SortExpression="IS_NONPHYSICAL"
                                HeaderTooltip="<%$ Translate: Non Physical %>"
                                AllowFiltering="false" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Kế hoạch %>" DataField="IS_PLAN"
                                HeaderTooltip="<%$ Translate: Kế hoạch %>"
                                HeaderStyle-Width="115px" UniqueName="IS_PLAN" SortExpression="IS_PLAN" AllowFiltering="false" />--%>
                            <%--<tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE"
                                UniqueName="EFFECTIVE_DATE" AllowFiltering="true" >
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                                HeaderStyle-CssClass="text-align-left"
                                HeaderTooltip="<%$ Translate: Trạng thái %>"
                                UniqueName="ACTFLG" SortExpression="ACTFLG" />--%>

                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                                HeaderStyle-CssClass="text-align-left"
                                HeaderTooltip="<%$ Translate: Trạng thái %>"
                                UniqueName="ACTFLG" SortExpression="ACTFLG" />--%>
                        </Columns>
                    </MasterTableView><ClientSettings EnableRowHoverStyle="true">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid></tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:PlaceHolder ID="phUpdateManager" runat="server"></asp:PlaceHolder>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_Title_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Title_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Title_RadPane2';
        var validateID = 'MainContent_ctrlHU_Title_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

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
        function OnClientClose(oWnd, args) {
            $find("<%= rgMain.ClientID %>").get_masterTableView().rebind();
        }
        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgMain.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                debugger;
                if ($find('<%# rgMain.ClientID%>').get_masterTableView().get_selectedItems().length < 1) {
                    return;
                }

                var id = $find('<%# rgMain.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                OpenInsertWindow(id);
                args.set_cancel(true);
                return;
            } else if (args.get_item().get_commandName() == "CREATE") {
                var Orgid = $("#<%=hidOrgID.ClientID%>").val();
                var OrgName = $("#<%=hidOrgName.ClientID%>").val();
                var Committee = $("#<%=hidCommittee.ClientID%>").val();
                var oWindow = radopen('Dialog.aspx?mid=Organize&fid=ctrlHU_PositionNewEdit&group=Business&OrgID=' + Orgid + ' &OrgName=' + OrgName + ' &Committee=' + Committee + '&noscroll=1&reload=1&FormType=0', "rwPopup");
                var desiredHeight = $(window).height() - 50;
                oWindow.moveTo(pos.center);
                oWindow.maximize();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function cusTitleGroup(oSrc, args) {
        }
        function gridRowDblClick(sender, eventArgs) {
            debugger;
            var id = $find('<%# rgMain.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
        /*OpenInsertWindow(id);*/
            var oWindow = radopen('Dialog.aspx?mid=Organize&fid=ctrlHU_PositionNewEdit&group=Business&ID=' + id + '&noscroll=1&reload=1&FormType=0&isView=1', "rwPopup");
            var pos = $("html").offset();
            var desiredHeight = $(window).height() - 50;
            oWindow.moveTo(pos.center);
            oWindow.maximize();
        }
        function OpenInsertWindow(Id) {
            debugger;
            var oWindow = radopen('Dialog.aspx?mid=Organize&fid=ctrlHU_PositionNewEdit&group=Business&ID=' + Id + '&noscroll=1&reload=1&FormType=0', "rwPopup");
            var pos = $("html").offset();
            var desiredHeight = $(window).height() - 50;
            oWindow.moveTo(pos.center);
            oWindow.maximize();
            //oWindow.maximize();
        }
    </script>
</tlk:RadCodeBlock>
