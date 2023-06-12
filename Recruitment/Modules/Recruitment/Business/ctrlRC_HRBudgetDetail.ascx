<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_HRBudgetDetail.ascx.vb"
    Inherits="Recruitment.ctrlRC_HRBudgetDetail" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField runat="server" ID="hidYearPLID" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:radsplitbar id="RadSplitBar1" runat="server" collapsemode="Forward">
    </tlk:radsplitbar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="100px" Scrolling="None">
                <table class="table-form">
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
                            <b><%# Translate("Chức danh")%></b>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTitles" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <b><%# Translate("Rank lương")%></b>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnRankSal" runat="server" SkinID="Money" >
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
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowPaging="true" AllowSorting="true" AllowMultiRowEdit="true">
                    <MasterTableView DataKeyNames="ID,YEAR_PLAN_ID,ORG_ID,TITLE_ID,MONTH_1,MONTH_2,MONTH_3,MONTH_4,MONTH_5,MONTH_6,MONTH_7,MONTH_8,MONTH_9,MONTH_10,MONTH_11,MONTH_12,RANK_SAL" 
                        ClientDataKeyNames="ID,YEAR_PLAN_ID,ORG_ID,TITLE_ID,MONTH_1,MONTH_2,MONTH_3,MONTH_4,MONTH_5,MONTH_6,MONTH_7,MONTH_8,MONTH_9,MONTH_10,MONTH_11,MONTH_12,RANK_SAL" FilterItemStyle-HorizontalAlign="Center" EditMode="InPlace">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>

                            <tlk:GridBoundColumn DataField="ID" Visible="false" ReadOnly="true"/>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" ReadOnly="true" />

                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 1 %>"
                                SortExpression="MONTH_1" DataField="MONTH_1" UniqueName="MONTH_1" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 2 %>"
                                SortExpression="MONTH_2" DataField="MONTH_2" UniqueName="MONTH_2" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 3 %>"
                                SortExpression="MONTH_3" DataField="MONTH_3" UniqueName="MONTH_3" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 4 %>"
                                SortExpression="MONTH_4" DataField="MONTH_4" UniqueName="MONTH_4" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 5 %>"
                                SortExpression="MONTH_5" DataField="MONTH_5" UniqueName="MONTH_5" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 6 %>"
                                SortExpression="MONTH_6" DataField="MONTH_6" UniqueName="MONTH_6" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 7 %>"
                                SortExpression="MONTH_7" DataField="MONTH_7" UniqueName="MONTH_7" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 8 %>"
                                SortExpression="MONTH_8" DataField="MONTH_8" UniqueName="MONTH_8" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 9 %>"
                                SortExpression="MONTH_9" DataField="MONTH_9" UniqueName="MONTH_9" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 10 %>"
                                SortExpression="MONTH_10" DataField="MONTH_10" UniqueName="MONTH_10" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 11 %>"
                                SortExpression="MONTH_11" DataField="MONTH_11" UniqueName="MONTH_11" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: 12 %>"
                                SortExpression="MONTH_12" DataField="MONTH_12" UniqueName="MONTH_12" DataFormatString="{0:N0}" HeaderStyle-Width="70px" ReadOnly="true">
                            </tlk:GridNumericColumn>
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Rank lương %>"
                                SortExpression="RANK_SAL" DataField="RANK_SAL" UniqueName="RANK_SAL" ColumnGroupName="GeneralInformation" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "RANK_SAL", "{0:##,##0#}")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tlk:RadNumericTextBox ID="RANK_SAL" SkinID="Money" runat="server"
                                        CausesValidation="false" Width="100%">
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </tlk:RadNumericTextBox>
                                </EditItemTemplate>
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
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function setDisplayValue(sender, args) {
            sender.set_displayValue(sender.get_value().toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1."));
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'DELETE') {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            }
        }

    </script>
</tlk:RadCodeBlock>
