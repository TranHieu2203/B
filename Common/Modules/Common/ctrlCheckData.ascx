<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCheckData.ascx.vb"
    Inherits="Common.ctrlCheckData" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="30px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="230px">
                <tlk:RadToolBar ID="tbarContracts" runat="server" />
                <tlk:RadTabStrip ID="rtabRecruitmentInfo" runat="server" MultiPageID="RadMultiPage1"
                    BackColor="#88B4E9" AutoPostBack="false" CausesValidation="false" EnableEmbeddedSkins="false">
                    <Tabs>
                        <tlk:RadTab runat="server" ID="rtIdSelect" PageViewID="rpvSelect" Text="<%$ Translate: Select %>"
                            Selected="true">
                        </tlk:RadTab>
                        <tlk:RadTab runat="server" ID="rtIdUpdate" PageViewID="rpvUpdate" Text="<%$ Translate: Update%>">
                        </tlk:RadTab>
                    </Tabs>
                </tlk:RadTabStrip>
                <tlk:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server" Style="margin-top: 15px"
                    Width="100%" ScrollBars="None">
                    <tlk:RadPageView ID="rpvSelect" runat="server" Selected="true">
                        <table class="table-form">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtSqlExcute" runat="server" CausesValidation="false" Wrap="true" 
                                        TextMode="MultiLine" Rows="10" Width="700px"></asp:TextBox>
                                       

                                </td>
                                <td>
                                    <tlk:RadButton ID="btnExcute"  CausesValidation="false" runat="server" Style="margin-left: 10px" Text="<%$ Translate: Select %>" >
                                    </tlk:RadButton>
                                </td>
                            </tr>
                        </table>
                    </tlk:RadPageView>
                    <tlk:RadPageView ID="rpvUpdate" runat="server">
                    <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                        <table class="table-form">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtSqlUpdate" runat="server" CausesValidation="false" Wrap="true" 
                                        TextMode="MultiLine" Rows="10" Width="700px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtSqlUpdate"
                                        runat="server" ErrorMessage="<%$ Translate: Vui lòng nhập câu lệnh %>" ToolTip="<%$ Translate: Vui lòng nhập câu lệnh %>">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <tlk:RadButton ID="btnUpdate" runat="server" Style="margin-left: 10px" Text="<%$ Translate: Update %>" >
                                    </tlk:RadButton>
                                </td>
                            </tr>
                        </table>
                    </tlk:RadPageView>
                </tlk:RadMultiPage>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgCheckData" runat="server" Height="100%" AllowFilteringByColumn="true"  AllowPaging="True" CssClass="MyCustomClass"
                    AllowSorting="True" AllowMultiRowSelection="True">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView>
                        <Columns>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == "REJECT") {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadCodeBlock>
