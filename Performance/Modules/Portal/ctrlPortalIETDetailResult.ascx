﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalIETDetailResult.ascx.vb"
    Inherits="Performance.ctrlPortalIETDetailResult" %>
<%@ Import Namespace="Common" %>

<tlk:RadSplitter ID="RadSplitter3" runat="server" Height="100%" Orientation="Horizontal" Scrolling="None">
    <tlk:RadPane ID="RadPane1" runat="server" Height="39px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100%">
        <tlk:RadGrid PageSize="500" ID="rgMain" runat="server" Height="100%" AllowMultiRowEdit="true"
            AllowMultiRowSelection="true" >
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID,TARGET_TYPE_CODE,EMPLOYEE_ACTUAL" ClientDataKeyNames="ID,TARGET_TYPE_CODE,EMPLOYEE_ACTUAL" EditMode="InPlace" EditFormSettings-EditFormType="AutoGenerated">
                <CommandItemStyle Height="28px" />
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                        ReadOnly="true" />
                    <tlk:GridBoundColumn HeaderText="ID Chỉ số đo lường" DataField="KPI_ASSESSMENT" Visible="false"
                        ReadOnly="true" UniqueName="KPI_ASSESSMENT" SortExpression="KPI_ASSESSMENT"/>
                    <tlk:GridBoundColumn HeaderText="Chỉ số đo lường" DataField="KPI_ASSESSMENT_TEXT" HeaderStyle-Width="100px"
                        ReadOnly="true" UniqueName="KPI_ASSESSMENT_TEXT" SortExpression="KPI_ASSESSMENT_TEXT" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Công thức tính" DataField="DESCRIPTION" UniqueName="DESCRIPTION"
                        HeaderStyle-Width="100px" ReadOnly="true" SortExpression="DESCRIPTION" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Loại tiêu chí" DataField="GOAL_TYPE_NAME" UniqueName="GOAL_TYPE_NAME"
                        HeaderStyle-Width="100px" ReadOnly="true" SortExpression="GOAL_TYPE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Chỉ tiêu" DataField="TARGET" UniqueName="TARGET"
                        HeaderStyle-Width="100px" ReadOnly="true" SortExpression="TARGET" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Ngưỡng" DataField="TARGET_MIN" UniqueName="TARGET_MIN"
                        HeaderStyle-Width="100px" ReadOnly="true" SortExpression="TARGET_MIN" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Trọng số" DataField="RATIO" UniqueName="RATIO"
                        HeaderStyle-Width="100px" ReadOnly="true" SortExpression="RATIO" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Điểm chuẩn" DataField="BENCHMARK" UniqueName="BENCHMARK"
                        HeaderStyle-Width="100px" ReadOnly="true" SortExpression="BENCHMARK" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: NV thực hiện %>" UniqueName="EMPLOYEE_ACTUAL" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "EMPLOYEE_ACTUAL")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <tlk:RadTextBox ID="txtEMPLOYEE_ACTUAL" runat="server"
                                CausesValidation="false" Width="100%" AutoPostBack="true" OnTextChanged="txtEMPLOYEE_ACTUAL_TextChanged">
                            </tlk:RadTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Điểm NV %>" UniqueName="EMPLOYEE_POINT" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "EMPLOYEE_POINT")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <tlk:RadNumericTextBox ID="rnEMPLOYEE_POINT" runat="server" SkinID="Decimal"
                                CausesValidation="false" Width="100%" AutoPostBack="true" OnTextChanged="rnEMPLOYEE_POINT_TextChanged">
                            </tlk:RadNumericTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Ghi chú %>" UniqueName="NOTE" HeaderStyle-Width="300px">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "NOTE")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <tlk:RadTextBox ID="txtNOTE" runat="server"
                                CausesValidation="false" Width="100%" AutoPostBack="true" OnTextChanged="txtNOTE_TextChanged">
                            </tlk:RadTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>    
                </Columns>
                <HeaderStyle Width="120px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlPortalIETDetailResult_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPortalIETDetailResult_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPortalIETDetailResult_RadPane2';
        var validateID = 'MainContent_ctrlPortalIETDetailResult_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;
        function setDisplayValue(sender, args) {
            sender.set_displayValue(sender.get_value().toString());
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
            else if (args.get_item().get_commandName() == 'SAVE') {
                setTimeout(function () {
                    getRadWindow().close(null);
                    args.set_cancel(true);
                }, 200);
            }
        }
    </script>
</tlk:RadScriptBlock>
