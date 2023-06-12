<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_Manning.ascx.vb"
    Inherits="Recruitment.ctrlRC_Manning" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidID" Value="" runat="server" />
<asp:HiddenField ID="hidMannOrgId" Value="" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Height="100%" Width="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="Y">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="validationsummary"
                    DisplayMode="BulletList" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Height="270px" Scrolling="None">
                <fieldset>
                    <legend>
                        <%# Translate("Tìm kiếm")%></legend>
                    <table class="table-form">
                        <tr>
                            <td>
                                <%# Translate("Năm")%>:
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboYear" runat="server" AutoPostBack="true" CausesValidation="false">
                                </tlk:RadComboBox>
                            </td>
                            <td>
                                <%# Translate("Tên định biên")%>:
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboListManning" runat="server" AutoPostBack="true" CausesValidation="false">
                                </tlk:RadComboBox>
                            </td>
                            <td>
                                <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                    Text="<%$ Translate: Tìm %>">
                                </tlk:RadButton>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin định biên")%></legend>
                    <table class="table-form">
                        <tr>
                            <td>
                                <%# Translate("Đơn vị / Phòng ban")%>:
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblOrgName" runat="server" Font-Bold="true">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%# Translate("Tên định biên")%>:<span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="rbtManName" runat="server" Width="250px" MaxLength="50" ToolTip="<%$ Translate: RC_MANNING_NAME %>"
                                    Height="22" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rbtManName"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên định biên. %>"
                                    Text="<%$ Translate: Bạn phải nhập tên định biên. %>" ToolTip="<%$ Translate: Bạn phải nhập tên định biên. %>"> </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <%# Translate("Ngày hiệu lực")%>:<span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdEffectDate"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                                    Text="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"> </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%# Translate("Ghi chú")%>:
                            </td>
                            <td>
                                <tlk:RadTextBox ID="rbtNote" runat="server" Width="250px" MaxLength="50" ToolTip="<%$ Translate: RC_MANNING_NOTE %>"
                                    Height="22" />
                            </td>
                            <td>
                                <%# Translate("Phê duyệt")%>:
                            </td>
                            <td>
                                <asp:CheckBox ID="cbStatus" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%# Translate("Định biên cũ")%>:
                            </td>
                            <td>
                                <tlk:RadTextBox ID="rbtManningOld" runat="server" Width="250px" MaxLength="50" ToolTip="<%$ Translate: RC_MANNING_OLD %>"
                                    Height="22" Enabled="false" />
                            </td>
                            <td>
                                <%# Translate("Định biên mới")%>:
                            </td>
                            <td>
                                <tlk:RadTextBox ID="rbtManningNew" ClientIDMode="Static" runat="server" Width="250px"
                                    MaxLength="50" ToolTip="<%$ Translate: RC_MANNING_NEW %>" Height="22" Enabled="false">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%# Translate("S.Lượng NV hiện tại")%>:
                            </td>
                            <td>
                                <tlk:RadTextBox ID="rbtManningCurrentCount" ClientIDMode="Static" runat="server"
                                    Width="250px" MaxLength="50" ToolTip="<%$ Translate: RC_MANNING_CURRENT_COUNT %>"
                                    Height="22" Enabled="false" />
                            </td>
                            <td>
                                <%# Translate("Số lượng tăng giảm")%>:
                            </td>
                            <td>
                                <tlk:RadTextBox ID="rbtManningCountMobilize" ClientIDMode="Static" runat="server"
                                    Width="250px" MaxLength="50" ToolTip="<%$ Translate: RC_MANNING_COUNT_MOBILIZE %>"
                                    Height="22" Enabled="false" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgManning" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true" CellSpacing="0" GridLines="None"
                    Height="100%" Width="100%" AllowFilteringByColumn="true" AllowMultiRowEdit="True">
                    <ClientSettings >
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView DataKeyNames="ID,NEW_MANNING,CURRENT_MANNING,NOTE,NAME,EFFECT_DATE,OLD_MANNING,MOBILIZE_COUNT_MANNING" ClientDataKeyNames="NEW_MANNING,CURRENT_MANNING,NOTE,NAME,EFFECT_DATE,OLD_MANNING,MOBILIZE_COUNT_MANNING"
                        EditMode="InPlace" EditFormSettings-EditFormType="AutoGenerated">
                        <Columns>
                            <tlk:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false">
                            </tlk:GridBoundColumn>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="NAME" EmptyDataText="" FilterControlWidth="90%" HeaderText="<%$ Translate: Tên định biên %>"
                                SortExpression="NAME" UniqueName="NAME" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="ORG_NAME" FilterControlWidth="90%" HeaderText="<%$ Translate: Phòng ban %>"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="TITLE_GROUP" FilterControlWidth="90%" HeaderText="<%$ Translate: Nhóm chức danh %>"
                                SortExpression="TITLE_GROUP" UniqueName="TITLE_GROUP" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="TITLE_NAME" FilterControlWidth="90%" HeaderText="<%$ Translate: Chức danh %>"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="EFFECT_DATE" EmptyDataText="" FilterControlWidth="90%"
                                HeaderText="<%$ Translate: Ngày hiệu lực %>" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            </tlk:GridBoundColumn>
                            <%-- <tlk:GridBoundColumn DataField="EmployeeCount" EmptyDataText="" FilterControlWidth="90%"
                                HeaderText="<%$ Translate: SLNV hiện tại %>" SortExpression="EmployeeCount" UniqueName="EmployeeCount">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            </tlk:GridBoundColumn>--%>
                            <tlk:GridBoundColumn DataField="OLD_MANNING" EmptyDataText="" FilterControlWidth="90%"
                                HeaderText="<%$ Translate: Định biên cũ %>" SortExpression="OLD_MANNING" UniqueName="OLD_MANNING"
                                ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="CURRENT_MANNING" EmptyDataText="" HeaderText="<%$ Translate: SL.NV hiện tại %>"
                                SortExpression="CURRENT_MANNING" UniqueName="CURRENT_MANNING" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            </tlk:GridBoundColumn>

                            


                            <tlk:GridTemplateColumn HeaderText="Định biên mới" HeaderStyle-Width="170px" DataField="NEW_MANNING"
                                UniqueName="NEW_MANNING">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="txtNewManning" Text='<%# (Eval("NEW_MANNING"))%>' runat="server" SkinID="Number" >
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <%--<tlk:GridTemplateColumn DataField="NEW_MANNING" HeaderText="<%$ Translate: Định biên mới %>"
                                SortExpression="NEW_MANNING" UniqueName="NEW_MANNING" AutoPostBackOnFilter="true">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="txtNewManning" ShowSpinButtons="false" MinValue="0" DataType="Interger"
                                        runat="server" CausesValidation="false" Text='<%# (Eval("NEW_MANNING")) %>' Width="78px">
                                        <ClientEvents OnBlur="OnChanged" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>--%>





                            <%-- <tlk:GridNumericColumn HeaderText="<%$ Translate: Định biên mới %>" DataField="NEW_MANNING"
                                ItemStyle-HorizontalAlign="Right" SortExpression="NEW_MANNING"
                                UniqueName="NEW_MANNING">
                            <HeaderStyle HorizontalAlign="Center" Width="170px"  />
                            </tlk:GridNumericColumn>--%>
                            <tlk:GridBoundColumn DataField="MOBILIZE_COUNT_MANNING" EmptyDataText="" HeaderText="<%$ Translate: Số lượng tăng/giảm %>"
                                SortExpression="MOBILIZE_COUNT_MANNING" UniqueName="MOBILIZE_COUNT_MANNING" ReadOnly="true">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridTemplateColumn DataField="NOTE" HeaderText="<%$ Translate: Ghi chú %>" SortExpression="NOTE"
                                UniqueName="NOTE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <tlk:RadTextBox ID="txtNote" runat="server" CausesValidation="false" Text='<%# (Eval("NOTE")) %>'>
                                    </tlk:RadTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                           <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" SortExpression="NOTE"
                                UniqueName="NOTE" />--%>
                        </Columns>
                        <HeaderStyle Width="150px" />
                    </MasterTableView></tlk:RadGrid><Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
            </tlk:RadPane>
        </tlk:RadSplitter></tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server" OnAjaxRequest="RadAjaxPanel1_AjaxRequest">
</tlk:RadAjaxPanel>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


        function OnClientClose(oWnd, args) {
            postBack(oWnd.get_navigateUrl());
        }

        //        function OnFocus(index) {
        //            var grid = $find('<%# rgManning.ClientID%>').get_masterTableView();
        //            grid.clearSelectedItems();
        //            grid.get_dataItems()[index].set_selected(true);
        //        }

        function OnChanged(sender, eventArgs) {
            $find("<%= RadAjaxPanel1.ClientID%>").ajaxRequestWithTarget("<%= RadAjaxPanel1.UniqueID %>", sender.get_id());
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

    </script>
</tlk:RadScriptBlock>
