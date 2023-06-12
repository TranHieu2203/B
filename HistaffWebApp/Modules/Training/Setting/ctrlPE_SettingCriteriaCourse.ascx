<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_SettingCriteriaCourse.ascx.vb"
    Inherits="Training.ctrlPE_SettingCriteriaCourse" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="400px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboCourse"  CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqCourse" ControlToValidate="cboCourse"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Khóa đào tạo %>"
                                ToolTip="<%$ Translate: Chưa chọn Khóa đào tạo %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Thang điểm quy đổi")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnScale_Point" SkinID="decimal">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Hiệu lực từ ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEffectFrom" CausesValidation="false">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqEffectFrom" ControlToValidate="rdEffectFrom"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Hiệu lực từ ngày %>"
                                ToolTip="<%$ Translate: Chưa chọn Hiệu lực từ ngày %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEffectTo" CausesValidation="false">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Height="70" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RegularExpressionValidator ID="reqRemark" runat="server" ErrorMessage="Thông tin nhập liệu có chứa mã html"
                                ControlToValidate="txtRemark" ValidationExpression="^(?!.*<[^>]+>).*">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b style="color: red">
                                <%# Translate("Thông tin tiêu chí đánh giá")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Tiêu chí")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboCriteria" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                            <%--<asp:RequiredFieldValidator ID="reqCriteria" ControlToValidate="cboCriteria"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Tiêu chí %>"
                                ToolTip="<%$ Translate: Chưa chọn Tiêu chí %>"> </asp:RequiredFieldValidator>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Tỷ trọng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnRatio" SkinID="decimal">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Mức độ hữu ích")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnPointMax">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="4" colspan="7">
                            <tlk:RadGrid ID="rgCriteria" AllowPaging="true"  runat="server" EditMode="InPlace" Height="150px" Scrolling="None" Width="800px">
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView  AllowPaging="false" AllowCustomPaging="false" DataKeyNames="ID,CRITERIA_NAME,CRITERIA_ID,RATIO,POINT_MAX"  
                                                    ClientDataKeyNames="ID,CRITERIA_NAME,CRITERIA_ID,RATIO,POINT_MAX" CommandItemDisplay="Top" >                                       
                                <CommandItemStyle Height="25px" />
                                <CommandItemTemplate >
                                    <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton Width="72px" ID="btnAdd" runat="server" Text="Thêm" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                        CausesValidation="false" CommandName="Add" TabIndex="3">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: right">
                                        <tlk:RadButton Width="70px" ID="btnDelete" runat="server" Text="Xóa" CausesValidation="false" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                            CommandName="Delete" TabIndex="3">
                                        </tlk:RadButton>
                                    </div>
                                    </div>
                                </CommandItemTemplate>
                                <Columns>
                                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                                    ReadOnly="true" />
                                <tlk:GridBoundColumn HeaderText="Tên tiêu chí" DataField="CRITERIA_NAME" HeaderStyle-Width="155px"
                                    ReadOnly="true" UniqueName="CRITERIA_NAME" SortExpression="CRITERIA_NAME" ItemStyle-HorizontalAlign="Center" />
                                <tlk:GridBoundColumn HeaderText="Tỷ trọng" DataField="RATIO" UniqueName="RATIO"
                                    HeaderStyle-Width="155px" ReadOnly="true" SortExpression="RATIO" ItemStyle-HorizontalAlign="Center" />
                                <tlk:GridBoundColumn HeaderText="Mức độ hữu ích" DataField="POINT_MAX" UniqueName="POINT_MAX"
                                    HeaderStyle-Width="155px" ReadOnly="true" SortExpression="POINT_MAX" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            </MasterTableView>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </tlk:RadGrid>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,TR_CRITERIA_GROUP_NAME,TR_CRITERIA_GROUP_ID,TR_COURSE_NAME,TR_COURSE_ID,EFFECT_FROM,EFFECT_TO,REMARK,SCALE_POINT">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Khóa đào tạo" DataField="TR_COURSE_NAME" SortExpression="TR_COURSE_NAME"
                                UniqueName="TR_COURSE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Nhóm tiêu chí" DataField="TR_CRITERIA_GROUP_NAME" SortExpression="TR_CRITERIA_GROUP_NAME"
                                UniqueName="TR_CRITERIA_GROUP_NAME" HeaderStyle-Width="430px"/>
                            <tlk:GridBoundColumn HeaderText="Thang điểm quy đổi" DataField="SCALE_POINT" SortExpression="SCALE_POINT"
                                UniqueName="SCALE_POINT" HeaderStyle-Width="90px"/>
                            <tlk:GridBoundColumn DataField="EFFECT_FROM" ReadOnly="true" HeaderText="Hiệu lực từ ngày"
                                SortExpression="EFFECT_FROM" UniqueName="EFFECT_FROM" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="130px" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn DataField="EFFECT_TO" ReadOnly="true" HeaderText="Hiệu lực đến ngày"
                                SortExpression="EFFECT_TO" UniqueName="EFFECT_TO" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="130px" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <ClientEvents OnGridCreated="GridCreated" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPE_SettingCriteriaCourse_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPE_SettingCriteriaCourse_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPE_SettingCriteriaCourse_RadPane2';
        var validateID = 'MainContent_ctrlPE_SettingCriteriaCourse_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

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
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var l = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var k = noty({ text: l, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(k.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>