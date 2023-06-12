<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CompetencyEmp.ascx.vb"
    Inherits="Profile.ctrlHU_CompetencyEmp" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="90px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidTitleID" />
        <asp:HiddenField runat="server" ID="hidEmpID" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" Width="130px"
                        ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Họ tên ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" Height="100%" AllowMultiRowEdit="true"
            SkinID="GridNotPaging">
            <MasterTableView DataKeyNames="TITLE_ID,EMPLOYEE_ID,COMPETENCY_ID" ClientDataKeyNames="TITLE_ID,TITLE_NAME"
                EditMode="InPlace">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm năng lực %>" DataField="COMPETENCY_GROUP_NAME"
                        UniqueName="COMPETENCY_GROUP_NAME" SortExpression="COMPETENCY_GROUP_NAME" ReadOnly="true">
                        <HeaderStyle Width="35%" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năng lực %>" DataField="COMPETENCY_NAME"
                        UniqueName="COMPETENCY_NAME" SortExpression="COMPETENCY_NAME" ReadOnly="true">
                        <HeaderStyle Width="35%" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức năng lực chuẩn %>" DataField="LEVEL_NUMBER_STANDARD_NAME"
                        UniqueName="LEVEL_NUMBER_STANDARD_NAME" SortExpression="LEVEL_NUMBER_STANDARD_NAME"
                        AllowFiltering="false" ReadOnly="true">
                        <HeaderStyle Width="15%" />
                    </tlk:GridBoundColumn>
                    <tlk:GridTemplateColumn UniqueName="LEVEL_NUMBER_EMP_NAME" HeaderText="<%$ Translate: Mức năng lực cá nhân%>"
                        SortExpression="LEVEL_NUMBER_EMP_NAME">
                        <HeaderStyle Width="15%" />
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "LEVEL_NUMBER_EMP_NAME")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <tlk:RadComboBox runat="server" ID="cboLevelNumber" Width="120px">
                                <Items>
                                    <tlk:RadComboBoxItem Value="" Text="" />
                                    <tlk:RadComboBoxItem Value="1" Text="1" />
                                    <tlk:RadComboBoxItem Value="2" Text="2" />
                                    <tlk:RadComboBoxItem Value="3" Text="3" />
                                    <tlk:RadComboBoxItem Value="4" Text="4" />
                                </Items>
                            </tlk:RadComboBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            Height="550px" EnableShadow="true" Behaviors="Close, Move" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function POPUP(value) {
            var oWindow = radopen(value, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center();
            oWindow.center(); */
            return false;
        }

        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
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
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
