<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_Required_Votes.ascx.vb"
    Inherits="Recruitment.ctrlRC_Required_Votes" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="210px" Scrolling="None">
                <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã phiếu yêu cầu")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCode" SkinID="Readonly" runat="server">
                            </tlk:RadTextBox>
                            <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã kiểu công đã tồn tại. %>"
                                ToolTip="<%$ Translate: Mã kiểu công đã tồn tại. %>">
                            </asp:CustomValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Đơn vị đề nghị")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" Width="130px">
                            </tlk:RadTextBox>
                            <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                                CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOrgName"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày tạo phiếu")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDateCreatVotes" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdDateCreatVotes"
                                runat="server" ErrorMessage="<%$ Translate: Chưa chọn ngày tạo phiếu yêu cầu. %>"
                                ToolTip="<%$ Translate: Chưa chọn ngày tạo phiếu yêu cầu. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày phê duyệt phiếu tuyển dụng")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDateAppVotes" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày kết thúc tuyển dụng")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEND_DATE_VOTES" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái phiếu yêu cầu")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboACTFLG" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mô tả")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="20" ID="rgDanhMuc" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="CODE,ORG_ID,ORG_NAME,DATE_CREATE_VOTES,DATE_APP_VOTES,END_DATE_VOTES,ACTFLG,NOTE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã phiếu yêu cầu %>" DataField="CODE"
                                UniqueName="CODE" SortExpression="CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị đề nghị  %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="130px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày tạo phiếu %>" DataField="DATE_CREATE_VOTES"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DATE_CREATE_VOTES"
                                UniqueName="DATE_CREATE_VOTES">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày phê duyệt phiếu tuyển dụng %>"
                                DataField="DATE_APP_VOTES" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}"
                                SortExpression="DATE_APP_VOTES" UniqueName="DATE_APP_VOTES">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc tuyển dụng %>" DataField="END_DATE_VOTES"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="END_DATE_VOTES"
                                UniqueName="END_DATE_VOTES">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái phiếu yêu cầu %>" DataField="ACTFLG_NAME"
                                UniqueName="ACTFLG_NAME" SortExpression="ACTFLG_NAME">
                                <HeaderStyle Width="70px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                                SortExpression="NOTE" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
