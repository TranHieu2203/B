<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_StageCost.ascx.vb"
    Inherits="Recruitment.ctrlRC_StageCost" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="210px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="item-head" colspan="6">
                            <%# Translate("Thông tin chi phí tuyển dụng")%>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--  <fieldset>
                                <legend>
                                    <%# Translate("Thông tin chi phí tuyển dụng")%></legend>--%>
                            <table>
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Đợt tuyển dụng")%><span class="lbReq">*</span>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdIsChanged" runat="server" />
                                        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server" OnAjaxRequest="RadAjaxPanel1_AjaxRequest">
                                        </tlk:RadAjaxPanel>
                                        <tlk:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                            <script type="text/javascript">
                                                function OnClientSelectedIndexChanged(sender, eventArgs) {
                                                    var val = document.getElementById("<%= hdIsChanged.ClientID%>").value
                                                    if (val == 1) {
                                                        $find("<%= RadAjaxPanel1.ClientID%>").ajaxRequestWithTarget("<%= RadAjaxPanel1.UniqueID %>", sender.get_id());
                                                    }
                                                }
                                            </script>
                                        </tlk:RadCodeBlock>
                                        <tlk:RadComboBox ID="cbbStage" runat="server" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                                        </tlk:RadComboBox>
                                        <asp:RequiredFieldValidator ID="reqcbbStage" ControlToValidate="cbbStage" runat="server"
                                            ErrorMessage="<%$ Translate: Bạn phải chọn đợt tuyển dụng %>" ToolTip="<%$ Translate: Bạn phải chọn đợt tuyển dụng %>">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td class="lb">
                                        <%# Translate("Nguồn tuyển dụng")%><span class="lbReq">*</span>
                                    </td>
                                    <td>
                                        <tlk:RadComboBox ID="cbbSourceOfRec" runat="server">
                                        </tlk:RadComboBox>
                                        <asp:RequiredFieldValidator ID="reqcbbSourceOfRec" ControlToValidate="cbbSourceOfRec"
                                            runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nguồn tuyển dụng %>"
                                            ToolTip="<%$ Translate: Bạn phải chọn nguồn tuyển dụng %>">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Chi phí dự kiến")%>
                                    </td>
                                    <td>
                                        <tlk:RadNumericTextBox ID="rntCostEstimate" runat="server" SkinID="Money" ReadOnly="true">
                                        </tlk:RadNumericTextBox>
                                    </td>
                                    <td class="lb">
                                        <%# Translate("Chi phí thực tế")%>
                                    </td>
                                    <td>
                                        <tlk:RadNumericTextBox ID="rntCostReality" runat="server" SkinID="Money">
                                        </tlk:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Ghi chú")%>
                                    </td>
                                    <td colspan="3">
                                        <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                                        </tlk:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                            <%--   </fieldset>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnCostallocate" runat="server" Text="<%$ Translate: Phân bổ chi phí tuyển dụng %>"
                                OnClientClicking="btnCostallocateClick" AutoPostBack="false" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true"
                    SkinID="GridSingleSelect">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,RC_STAGE_ID, TITLE, COSTESTIMATE, COSTREALITY, REMARK, SOURCE_NAME, SOURCEOFREC_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đợt tuyển dụng %>" DataField="TITLE"
                                SortExpression="TITLE" UniqueName="TITLE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nguồn tuyển dụng %>" DataField="SOURCE_NAME"
                                SortExpression="SOURCE_NAME" UniqueName="SOURCE_NAME" />
                            <tlk:GridBoundColumn DataField="SOURCEOFREC_ID" UniqueName="SOURCEOFREC_ID" Visible="false" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí dự kiến %>" DataField="COSTESTIMATE"
                                SortExpression="COSTESTIMATE" UniqueName="COSTESTIMATE" AllowFiltering="false"
                                HeaderStyle-Width="90px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí thực tế %>" DataField="COSTREALITY"
                                SortExpression="COSTREALITY" UniqueName="COSTREALITY" AllowFiltering="false"
                                HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
                        </Columns>
                        <HeaderStyle Width="150px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="500px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {

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


        function btnCostallocateClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck > 1) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_StageCost_CostAllocate&group=Business&Cost_ID=' + id, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.center, pos.middle);
            //            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            oWindow.setSize(800, 500);
        }

        function OnClientClose(oWnd, args) {
            window.location.reload(true);
        }


    </script>
</tlk:RadCodeBlock>
