<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TitleConcurrent.ascx.vb"
    Inherits="Profile.ctrlHU_TitleConcurrent" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupDirect" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupLevel" runat="server"></asp:PlaceHolder>
<asp:HiddenField runat="server" ID="hidReload" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="180px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" ValidationGroup="TitleConcurrent" />
                <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="TitleConcurrent" />
                <table class="table-form">
                    <tr>
                        <td class="lb" >
                            <asp:Label ID="lbEmployeeCode" runat="server" Text="Tên nhân viên"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                         <asp:HiddenField ID="hidEmployee" runat="server" />
                            <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" Width="130px"
                                ReadOnly="True">
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                                Width="40px">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                                runat="server" ErrorMessage="Bạn phải nhập nhân viên." ToolTip="Bạn phải nhập nhân viên."> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbOrgName2" runat="server" Text="Đơn vị"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <asp:HiddenField ID="hidOrgID" runat="server" />
                            <tlk:RadTextBox runat="server" ID="txtOrgName2" ReadOnly="true" />
                            <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                            <asp:RequiredFieldValidator ValidationGroup="TitleConcurrent" ID="RequiredFieldValidator1"
                                ControlToValidate="txtOrgName2" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>"
                                ToolTip="Bạn phải chọn đơn vị">
                            </asp:RequiredFieldValidator>
                        </td>
                       
                    </tr>
                    <tr>
                     <td class="lb" style="width: 150px">
                            <asp:Label ID="lbTitle" runat="server" Text="Chức danh"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboTitle" SkinID="LoadDemand" OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                            <asp:CustomValidator ValidationGroup="TitleConcurrent" ID="cusTitle" runat="server"
                                ErrorMessage="Bạn phải chọn Chức danh" ToolTip="Bạn phải chọn Chức danh" ClientValidationFunction="cusTitle">
                            </asp:CustomValidator>
                        </td>
                        <td>
                            <asp:Label ID="lbDecisionNo" runat="server" Text="Số quyết định"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtDecisionNo" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbEffectDate" runat="server" Text="Ngày hiệu lực"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                                ErrorMessage="Bạn phải nhập ngày hiệu lực." ToolTip="Bạn phải nhập ngày hiệu lực."
                                ValidationGroup="TitleConcurrent"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cusExistEffectDate" runat="server" ErrorMessage="Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất"
                                ToolTip="Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất" ValidationGroup="TitleConcurrent">
                            </asp:CustomValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbExpireDate" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                                Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" ErrorMessage="Ngày kết thúc phải lớn hơn ngày hiệu lực"
                                ToolTip="Ngày kết thúc phải lớn hơn ngày hiệu lực" ValidationGroup="TitleConcurrent"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbNote" runat="server" Text="Ghi chú"></asp:Label>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtNote" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" Height="250px" AllowSorting="True" AllowMultiRowSelection="true"
                    Width="99.8%">
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="True">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="NAME,EFFECT_DATE,EXPIRE_DATE,NOTE,ORG_ID,ORG_NAME,TITLE_ID,DECISION_NO,EMPLOYEE_ID,EMPLOYEE_NAME,EMPLOYEE_CODE">
                        <Columns>
                           <%--  <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_NAME" Visible="false" />
                            <tlk:GridBoundColumn DataField="TITLE_ID" Visible="false" />
                              <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                SortExpression="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="NAME" UniqueName="NAME"
                                SortExpression="NAME" />
                                <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO" UniqueName="DECISION_NO"
                                SortExpression="DECISION_NO" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                                UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE"
                                UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" />
                            <tlk:GridDateTimeColumn HeaderText="Ghi chú" DataField="NOTE" UniqueName="NOTE"
                                SortExpression="NOTE" />--%>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        var oldSize = 0;

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

        function ResizeSplitter() {
        }

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboTitle.ClientID %>':
                    value = $get("<%= hidOrgID.ClientID %>").value;
                    break;
                default:
                    break;
            }

            if (!value) {
                value = 0;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;
            context["value"] = sender.get_value();

        }

    </script>
</tlk:RadCodeBlock>
