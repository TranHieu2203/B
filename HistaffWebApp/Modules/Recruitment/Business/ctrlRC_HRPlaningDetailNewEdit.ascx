<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_HRPlaningDetailNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlRC_HRPlaningDetailNewEdit" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="100px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <b><%# Translate("Năm đinh biên")%></b>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbYear"></asp:Label>
                </td>
                <td class="lb">
                    <b><%# Translate("Phiên bản")%></b>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbVersion"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <b><%# Translate("Ngày hiệu lực")%></b>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbEffectDate"></asp:Label>
                </td>
                <td class="lb">
                    <b><%# Translate("Phòng ban")%></b>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbOrgName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <b><%# Translate("Định biên nhanh")%></b>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnAutoPlaning" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
                <td>
                    <tlk:RadButton ID="btnAutoFill" runat="server" SkinID="Button" CausesValidation="false" 
                        Text="<%$ Translate: Điền nhanh %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="300px">
        <tlk:RadGrid ID="rgData" AllowPaging="false" AllowMultiRowEdit="true" runat="server">
            <MasterTableView DataKeyNames="ORG_ID,TITLE_ID,TITLE_NAME,MONTH_1,MONTH_2,MONTH_3,MONTH_4,MONTH_5,MONTH_6,MONTH_7,MONTH_8,MONTH_9,MONTH_10,MONTH_11,MONTH_12" 
                ClientDataKeyNames="ORG_ID,TITLE_ID,TITLE_NAME,MONTH_1,MONTH_2,MONTH_3,MONTH_4,MONTH_5,MONTH_6,MONTH_7,MONTH_8,MONTH_9,MONTH_10,MONTH_11,MONTH_12" FilterItemStyle-HorizontalAlign="Center" EditMode="InPlace" 
                AllowPaging="false">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: SL hiện tại  %>" DataField="CURRENT_COUNT"
                                SortExpression="CURRENT_COUNT" UniqueName="CURRENT_COUNT" AllowFiltering="false" AllowSorting="false"/>

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 1 %>" AllowFiltering="false" 
                                SortExpression="MONTH_1" DataField="MONTH_1" UniqueName="MONTH_1" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_1" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 2 %>" AllowFiltering="false" 
                                SortExpression="MONTH_2" DataField="MONTH_2" UniqueName="MONTH_2" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_2" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 3 %>" AllowFiltering="false" 
                                SortExpression="MONTH_3" DataField="MONTH_3" UniqueName="MONTH_3" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_3" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 4 %>" AllowFiltering="false" 
                                SortExpression="MONTH_4" DataField="MONTH_4" UniqueName="MONTH_4" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_4" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 5 %>" AllowFiltering="false" 
                                SortExpression="MONTH_5" DataField="MONTH_5" UniqueName="MONTH_5" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_5" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 6 %>" AllowFiltering="false" 
                                SortExpression="MONTH_6" DataField="MONTH_6" UniqueName="MONTH_6" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_6" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 7 %>" AllowFiltering="false" 
                                SortExpression="MONTH_7" DataField="MONTH_7" UniqueName="MONTH_7" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_7" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 8 %>" AllowFiltering="false" 
                                SortExpression="MONTH_8" DataField="MONTH_8" UniqueName="MONTH_8" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_8" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 9 %>" AllowFiltering="false" 
                                SortExpression="MONTH_9" DataField="MONTH_9" UniqueName="MONTH_9" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_9" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 10 %>" AllowFiltering="false" 
                                SortExpression="MONTH_10" DataField="MONTH_10" UniqueName="MONTH_10" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_10" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 11 %>" AllowFiltering="false" 
                                SortExpression="MONTH_11" DataField="MONTH_11" UniqueName="MONTH_11" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_11" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: 12 %>" AllowFiltering="false" 
                                SortExpression="MONTH_12" DataField="MONTH_12" UniqueName="MONTH_12" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <tlk:RadNumericTextBox ID="MONTH_12" SkinID="Number" runat="server"
                                        CausesValidation="false" Width="100%" NumberFormat-GroupSeparator=",">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                        </Columns>
                        <HeaderStyle Width="150px" />
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
        function setDisplayValue(sender, args) {
            sender.set_displayValue(sender.get_value());
        }
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                //ResizeSplitter();
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
                var pane = splitter.getPaneById('<%= RadPane2.ClientID%>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height("420");
            }, 200);
        }
        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane2.ClientID%>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height("420");
            }
        }
        function CloseWindow() {
            var oWindow = GetRadWindow();
            if (oWindow) oWindow.close(1);
        }
    </script>
</tlk:RadCodeBlock>
