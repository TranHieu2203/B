<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsHealthImport.ascx.vb"
    Inherits="Insurance.ctrlInsHealthImport" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="250" Width="300px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="130px" Scrolling="None">
                <div style="display: none;">
                    <tlk:RadTextBox ID="txtID" Text="0" runat="server">
                    </tlk:RadTextBox>
                </div>
                <style>
                    .td-padding td 
                    { 
                        padding:2px 5px;
                    }
                </style>
                <div>
                    <fieldset style="width:auto; height:auto">
                        <legend>
						    <%# Translate("Thông tin tìm kiếm")%>
					    </legend>
                        <table class="td-padding" width="100%">
                        <tr>
                            <td class="lb">
                                <%# Translate("Mã nhân viên")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtEMPLOYEEID_SEARCH" MaxLength="250" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Đơn vị BH")%>
                            </td>
                            <td>
                                <tlk:RadComboBox DataValueField="ID" DataTextField="NAME" ID="ddlINSORG" runat="server"
                                    TabIndex="2" ToolTip="<%$ Translate: Đơn vị BH %>" AutoPostBack="false" EmptyMessage="<%$ Translate: Đơn vị BH %>">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                            </td>
                            <td>
                                <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFind" TabIndex="8" runat="server"
                                    Text="<%$ Translate: FIND %>" ValidationGroup="Search" SkinID="ButtonFind" OnClick="btnFIND_Click">
                                </tlk:RadButton>
                            </td>
                        </tr>
                    </table>
                    </fieldset>					    
                </div>     
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar3" runat="server">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
            </tlk:RadPane>
            <%--<tlk:RadSplitBar ID="RadSplitBar2" runat="server">
            </tlk:RadSplitBar>--%>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgGridData" runat="server" Height="100%" AllowPaging="True" AllowSorting="True"
                    CellSpacing="0" ShowStatusBar="true" AllowMultiRowSelection="true" GridLines="None"
                    PageSize="20" AutoGenerateColumns="false" AllowFilteringByColumn="true">
                    <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_CODE,VN_FULLNAME,ORG_NAME,INS_ORG_NAME,INS_ORG_ID,EMPID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="90px"
                                FilterControlWidth="60%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                                UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME" HeaderStyle-Width="150px"
                                FilterControlWidth="80%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" HeaderStyle-Width="170px" FilterControlWidth="80%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị bảo hiểm %>" DataField="INS_ORG_NAME"
                                UniqueName="INS_ORG_NAME" SortExpression="INS_ORG_NAME" HeaderStyle-Width="200px"
                                FilterControlWidth="80%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị bảo hiểm %>" DataField="INS_ORG_ID"
                                UniqueName="INS_ORG_ID" SortExpression="INS_ORG_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thâm niên BH %>" DataField="SENIORITY_INSURANCE"
                                UniqueName="SENIORITY_INSURANCE" SortExpression="SENIORITY_INSURANCE" HeaderStyle-Width="90px"
                                FilterControlWidth="60%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHXH - Số sổ %>" DataField="SOCIAL_NUMBER"
                                UniqueName="SOCIAL_NUMBER" SortExpression="SOCIAL_NUMBER" HeaderStyle-Width="130px"
                                FilterControlWidth="80%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHXH - Tình trạng sổ %>" DataField="SOCIAL_STATUS"
                                UniqueName="SOCIAL_STATUS" SortExpression="SOCIAL_STATUS" HeaderStyle-Width="110px"
                                FilterControlWidth="70%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHXH - Ngày cấp %>" DataFormatString="{0:dd/MM/yyyy}"
                                DataField="SOCIAL_GRANT_DATE" UniqueName="SOCIAL_GRANT_DATE" SortExpression="SOCIAL_GRANT_DATE"
                                HeaderStyle-Width="90px" FilterControlWidth="60%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHXH - Số lưu trữ %>" DataField="SOCIAL_SAVE_NUMBER"
                                UniqueName="SOCIAL_SAVE_NUMBER" SortExpression="SOCIAL_SAVE_NUMBER" HeaderStyle-Width="130px"
                                FilterControlWidth="80%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHYT - Số thẻ %>" DataField="HEALTH_NUMBER"
                                UniqueName="HEALTH_NUMBER" SortExpression="HEALTH_NUMBER" HeaderStyle-Width="130px"
                                FilterControlWidth="80%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHYT - Tình trạng thẻ %>" DataField="HEALTH_STATUS"
                                UniqueName="HEALTH_STATUS" SortExpression="HEALTH_STATUS" HeaderStyle-Width="110px"
                                FilterControlWidth="70%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHYT - Hiệu lực Từ ngày %>" DataFormatString="{0:dd/MM/yyyy}"
                                DataField="HEALTH_EFFECT_FROM_DATE" UniqueName="HEALTH_EFFECT_FROM_DATE" SortExpression="HEALTH_EFFECT_FROM_DATE"
                                HeaderStyle-Width="100px" FilterControlWidth="60%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHYT - Hiệu lực Đến ngày %>" DataFormatString="{0:dd/MM/yyyy}"
                                DataField="HEALTH_EFFECT_TO_DATE" UniqueName="HEALTH_EFFECT_TO_DATE" SortExpression="HEALTH_EFFECT_TO_DATE"
                                HeaderStyle-Width="100px" FilterControlWidth="60%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHYT - Ngày nhận %>" DataFormatString="{0:dd/MM/yyyy}"
                                DataField="HEALTH_RECEIVE_DATE" UniqueName="HEALTH_RECEIVE_DATE" SortExpression="HEALTH_RECEIVE_DATE"
                                HeaderStyle-Width="100px" FilterControlWidth="60%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHYT - Người nhận %>" DataField="HEALTH_RECEIVER"
                                UniqueName="HEALTH_RECEIVER" SortExpression="HEALTH_RECEIVER" HeaderStyle-Width="90px"
                                FilterControlWidth="60%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi khám chữa bệnh %>" DataField="HEALTH_AREA_INS"
                                UniqueName="HEALTH_AREA_INS" SortExpression="HEALTH_AREA_INS" HeaderStyle-Width="250px"
                                FilterControlWidth="80%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMPID" UniqueName="EMPID"
                                SortExpression="EMPID" Visible="false" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            OnClientBeforeClose="OnClientBeforeClose" OnClientClose="popupclose" Height="600px"
            EnableShadow="true" Behaviors="Close, Maximize" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        //------------------------for import
        function OpenImport(sender, eventArgs) {
            var programCode = "IMPORT_ASP_NET";
            var oWindow = radopen('Dialog.aspx?mid=Common&fid=ctrlViewImport&noscroll=1&programCode=' + programCode, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            return 0;
        }

        function popupclose(oWnd, args) {
            //window.location.reload();
        }
        function OnClientBeforeClose(sender, eventArgs) {
            if (!confirm("Bạn có muốn đóng màn hình không?")) {
                //if cancel is clicked prevent the window from closing
                args.set_cancel(true);
            }
        }
        function OnClientClose(oWnd, args) {
            oWnd = $find('<%#popupId %>');
            oWnd.setSize(screen.width - 250, screen.height - 300);
            oWnd.remove_close(OnClientClose);
            var arg = args.get_argument();
            if (arg) {
                postBack(arg);
            }
        }
        //-----------------

    </script>
</tlk:RadCodeBlock>
