<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Signer.ascx.vb"
    Inherits="Profile.ctrlHU_Signer" %>
<asp:HiddenField ID="hidEmpID" runat="server" />
<style type="text/css">
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Signer_RadPane1
    {
        height: 35px !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Height ="5%">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="MainPanel" runat="server" Scrolling="None" Height ="25%">
                <asp:Panel ID="pnlEdit" runat="server" Visible="true" Height ="90%">
                    <fieldset>
                        <legend accesskey="I">
                            <asp:Label ID="lblViewTitle" runat="server"></asp:Label></legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <asp:Label ID ="lbCode" runat ="server" Text ="Mã người ký"></asp:Label><span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtCode" runat="server">
                                    </tlk:RadTextBox>
                                    <tlk:RadButton ID="btnFindEmp" runat="server" SkinID="ButtonView" CausesValidation="false">
                                    </tlk:RadButton>
                                  <%--  <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                                        ErrorMessage="<%$ Translate: Bạn phải nhập mã người ký %>" ToolTip="<%$ Translate: Bạn phải nhập mã người ký %>">
                                    </asp:RequiredFieldValidator>--%>
                                </td>
                                <td class="lb">
                                    <asp:Label ID ="lbTYPE_NAME" runat ="server" Text ="Tên người ký"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtTYPE_NAME" MaxLength="255" runat="server" Width="300px">
                                    </tlk:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label ID ="lbNAME_EN" runat ="server" Text ="Chức vụ người ký"></asp:Label> 
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtNAME_EN" MaxLength="255" runat="server" Width="300px">
                                    </tlk:RadTextBox>
                                </td>
                                <td class="lb">
                                    <asp:Label ID ="lbOrg_Name" runat ="server" Text ="Công ty"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="rtOrg_Name" MaxLength="255" runat="server" Width="300px">
                                    </tlk:RadTextBox>
                                    <tlk:RadTextBox ID ="rtORG_ID" runat ="server"  Visible ="false"  ></tlk:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <asp:Label ID ="lbRemark" runat ="server" Text ="Ghi chú"></asp:Label>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtRemark" MaxLength="255" runat="server" Width="100%">
                                    </tlk:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height ="70%">
                <tlk:RadGrid ID="rgData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    Height="95%" AllowSorting="True" AllowMultiRowSelection="true" AllowFilteringByColumn="true">
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID,SIGNER_ID" ClientDataKeyNames="ID,SIGNER_CODE,TITLE_NAME,REMARK,NAME,ORG_ID,ORG_NAME,SIGNER_ID">
                        <Columns>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function FilterMenuShowing() { return; }
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

        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {

        }
    </script>
</tlk:RadCodeBlock>
