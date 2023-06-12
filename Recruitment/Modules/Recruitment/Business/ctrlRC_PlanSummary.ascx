<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_PlanSummary.ascx.vb"
    Inherits="Recruitment.ctrlRC_PlanSummary" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" OnClientButtonClicking="clientButtonClicking" runat="server"/>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <table  class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <%--<tlk:RadNumericTextBox ID="rntxtYear" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>--%>
                            <tlk:RadComboBox ID="cboYear" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm kiếm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%">
                    <MasterTableView>
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME">
                                <HeaderStyle Width="250px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí tuyển dụng %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng hiện tại %>" DataField="SL_HIENTAI"
                                SortExpression="SL_HIENTAI" UniqueName="SL_HIENTAI" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng đã tuyển %>" DataField="SL_DATUYEN"
                                SortExpression="SL_DATUYEN" UniqueName="SL_DATUYEN" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số lượng dự báo %>" DataField="SL_DUBAO"
                                SortExpression="SL_DUBAO" UniqueName="SL_DUBAO" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 1 %>" DataField="SL_T1" SortExpression="SL_T1"
                                UniqueName="SL_T1" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 2 %>" DataField="SL_T2" SortExpression="SL_T2"
                                UniqueName="SL_T2" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 3 %>" DataField="SL_T3" SortExpression="SL_T3"
                                UniqueName="SL_T3" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 4 %>" DataField="SL_T4" SortExpression="SL_T4"
                                UniqueName="SL_T4" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 5 %>" DataField="SL_T5" SortExpression="SL_T5"
                                UniqueName="SL_T5" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 6 %>" DataField="SL_T6" SortExpression="SL_T6"
                                UniqueName="SL_T6" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 7 %>" DataField="SL_T7" SortExpression="SL_T7"
                                UniqueName="SL_T7" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 8 %>" DataField="SL_T8" SortExpression="SL_T8"
                                UniqueName="SL_T8" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 9 %>" DataField="SL_T9" SortExpression="SL_T9"
                                UniqueName="SL_T9" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 10 %>" DataField="SL_T10"
                                SortExpression="SL_T10" UniqueName="SL_T10" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 11 %>" DataField="SL_T11"
                                SortExpression="SL_T11" UniqueName="SL_T11" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tháng 12 %>" DataField="SL_T12"
                                SortExpression="SL_T12" UniqueName="SL_T12" />
                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />
                        </Columns>
                        <HeaderStyle Width="70px" />
                        <ItemStyle Width="70px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
        }

    </script>
</tlk:RadCodeBlock>
