<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CompetencyAssNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_CompetencyAssNewEdit" %>
<tlk:radsplitter id="RadSplitter3" runat="server" width="100%" height="100%" orientation="Horizontal">
    <tlk:radpane id="RadPane3" runat="server" height="30px" scrolling="None">
        <tlk:radtoolbar id="tbarMain" runat="server" />
    </tlk:radpane>
    <tlk:radpane id="RadPane1" runat="server" height="180px" scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidTitleID" />
        <asp:HiddenField runat="server" ID="hidEmpID" />
        <asp:HiddenField runat="server" ID="hidPeriodID" />
        <asp:HiddenField runat="server" ID="hidYear" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:radtextbox id="txtEmployeeCode" skinid="ReadOnly" runat="server" width="130px"
                        readonly="True">
                    </tlk:radtextbox>
                    <tlk:radbutton id="btnEmployee" skinid="ButtonView" runat="server" causesvalidation="false"
                        width="40px">
                    </tlk:radbutton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Họ tên ")%>
                </td>
                <td>
                    <tlk:radtextbox id="txtEmployeeName" runat="server" readonly="True" skinid="ReadOnly">
                    </tlk:radtextbox>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:radtextbox id="txtTitleName" runat="server" readonly="True" skinid="ReadOnly">
                    </tlk:radtextbox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:radtextbox id="txtOrgName" runat="server" readonly="True" skinid="ReadOnly">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="vertical-align: top">
                    <%# Translate("Cách đánh giá")%>
                </td>
                <td colspan="5">
                    <tlk:radtextbox id="txtRemark" runat="server" skinid="Textbox1023" width="100%" height="90px"
                        readonly="true">
                    </tlk:radtextbox>
                </td>
            </tr>
        </table>
    </tlk:radpane>
    <tlk:radpane id="RadPane2" runat="server" scrolling="None">
        <tlk:radgrid PageSize="50" id="rgMain" runat="server" height="100%" allowmultirowedit="true"
            skinid="GridNotPaging">
            <mastertableview datakeynames="TITLE_ID,EMPLOYEE_ID,COMPETENCY_ID,LEVEL_NUMBER_ASS,REMARK"
                clientdatakeynames="TITLE_ID,TITLE_NAME,LEVEL_NUMBER_ASS,REMARK" editmode="InPlace">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm năng lực %>" DataField="COMPETENCY_GROUP_NAME"
                        UniqueName="COMPETENCY_GROUP_NAME" SortExpression="COMPETENCY_GROUP_NAME" ReadOnly="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năng lực %>" DataField="COMPETENCY_NAME"
                        UniqueName="COMPETENCY_NAME" SortExpression="COMPETENCY_NAME" ReadOnly="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức năng lực chuẩn %>" DataField="LEVEL_NUMBER_STANDARD_NAME"
                        UniqueName="LEVEL_NUMBER_STANDARD_NAME" SortExpression="LEVEL_NUMBER_STANDARD_NAME"
                        AllowFiltering="false" ReadOnly="true">
                        <HeaderStyle Width="150px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridTemplateColumn UniqueName="LEVEL_NUMBER_ASS_NAME" HeaderText="<%$ Translate: Mức năng lực cá nhân%>"
                        SortExpression="LEVEL_NUMBER_ASS_NAME">
                        <HeaderStyle Width="150px" />
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "LEVEL_NUMBER_ASS_NAME")%>
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
                    <tlk:GridTemplateColumn UniqueName="REMARK" HeaderText="<%$ Translate: Diễn giải %>"
                        SortExpression="REMARK">
                        <HeaderStyle Width="200px" />
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "REMARK")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023">
                            </tlk:RadTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                </Columns>
            </mastertableview>
        </tlk:radgrid>
    </tlk:radpane>
</tlk:radsplitter>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:radwindowmanager id="RadWindowManager1" runat="server">
    <windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            Height="550px" EnableShadow="true" Behaviors="Close, Move" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </windows>
</tlk:radwindowmanager>
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
//            var item = args.get_item();
//            if (args.get_item().get_commandName() == 'CANCEL') {
//                getRadWindow().close(null);
//                args.set_cancel(true);
//            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

    </script>
</tlk:radcodeblock>
