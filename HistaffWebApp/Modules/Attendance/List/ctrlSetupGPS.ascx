<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSetupGPS.ascx.vb"
    Inherits="Attendance.ctrlSetupGPS" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hiOrgdID" runat="server" />


<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>

    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Visible="false">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên địa điểm")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtNameVN" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNameVN"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Tên địa điểm. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Địa chỉ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtaddress" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtaddress"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Địa chỉ. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Lat (Vĩ độ)")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtLatVD" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtLatVD"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Lat (Vĩ độ). %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Long (Kinh độ)")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtLongKD" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtLongKD"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Long (Kinh độ). %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label2" runat="server" Text="Bán kính (chấm công)"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtRadius" MinValue="0" runat="server" AutoPostBack="false">
                            </tlk:RadNumericTextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtRadius"
                                runat="server" ErrorMessage="Bạn phải nhập Bán kính (chấm công)" ToolTip="Bạn phải nhập Bán kính (chấm công)"> 
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgDanhMuc" runat="server" Height="100%" AllowPaging="True" AllowFilteringByColumn="true"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                         <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC,ORG_NAME" ClientDataKeyNames="ID,IP,NAME,ADDRESS,LAT_VD,LONG_KD,RADIUS,ORG_DESC,ORG_NAME">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                SortExpression="ORG_NAME">
                            </tlk:GridBoundColumn>


                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên địa điểm %>" DataField="NAME"
                                UniqueName="NAME" SortExpression="NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ %>" DataField="ADDRESS"
                                UniqueName="ADDRESS" SortExpression="ADDRESS" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lat (Vĩ độ) %>" DataField="LAT_VD"
                                UniqueName="LAT_VD" SortExpression="LAT_VD" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Long (Kinh độ) %>" DataField="LONG_KD"
                                UniqueName="LONG_KD" SortExpression="LONG_KD" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Bán kính (chấm công) %>" DataField="RADIUS"
                                UniqueName="RADIUS" SortExpression="RADIUS" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                                UniqueName="ACTFLG" SortExpression="ACTFLG" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
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

<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else if (args.get_item().get_commandName() == "CREATE") {
                var Orgid = $("#<%=hiOrgdID.ClientID%>").val();
                var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlSetupGPSNewEdit&group=List&noscroll=1&reload=1&FormType=0', "rwPopup");
                var pos = $("html").offset();
                oWindow.moveTo(pos.center);
                oWindow.maximize();
                args.set_cancel(true);
            }
            else if (args.get_item().get_commandName() == "EDIT") {
                if ($find('<%# rgDanhMuc.ClientID%>').get_masterTableView().get_selectedItems().length < 1) {
                    return;
                }

                var id = $find('<%# rgDanhMuc.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                OpenInsertWindow(id);
                args.set_cancel(true);
                return;
            }
            else {
                ResizeSplitterDefault();
            }
        }
        function OnClientClose(oWnd, args) {
            $find("<%= rgDanhMuc.ClientID %>").get_masterTableView().rebind();
        }
        function OpenInsertWindow(Id) {
            var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlSetupGPSNewEdit&group=List&ID=' + Id + '&noscroll=1&reload=1&FormType=0', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.center);
            oWindow.maximize();
        }
        function gridRowDblClick(sender, eventArgs) {
            var id = $find('<%# rgDanhMuc.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlSetupGPSNewEdit&group=List&ID=' + id + '&noscroll=1&reload=1&FormType=0', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.center);
            oWindow.maximize();
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }
        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            <%--var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }--%>
        }
    </script>
</tlk:RadCodeBlock>
