<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_PortalHTCHAssessmentResult.ascx.vb"
    Inherits="Performance.ctrlPE_PortalHTCHAssessmentResult" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<style>
    
    #RAD_SPLITTER_PANE_CONTENT_RadPaneMain {
        overflow: hidden !important;
    }
    #ctrlPE_PortalHTCHAssessmentResult_rgData{
        position:fixed;
        bottom:0;
    }
</style>


<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Orientation="Horizontal" Width="100%" Height="100%">
            <asp:HiddenField ID="hidID" runat="server" />
            <tlk:RadPane ID="RadPane2" runat="server" Height="30px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>

            <tlk:RadPane ID="RadPane1" runat="server" Height="100%" Scrolling="Y">
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <asp:Label runat="server" ID="lbStatus" ForeColor="Red"></asp:Label>
                <table class="table-form">
                    <tr>
                        <td colspan="4">
                            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b style="color: red">
                                <%# Translate("Thông tin nhân viên")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtEmployeeCode" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Họ tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtEmployeeName" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Chức danh")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtTitleName" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Phòng ban")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtYear" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ đánh giá")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtPeriod" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdStartDate" CausesValidation="false" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEndDate" CausesValidation="false" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Điểm mạnh")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtStrengthNote" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Điểm cải thiện")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtImproveNote" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Triển vọng")%>
                        </td>
                        <td colspan="7">
                            <tlk:RadTextBox runat="server" ID="txtProspect" CausesValidation="false" txtNote="MultiLine" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Nhãn đánh giá")%>
                        </td>
                        <td colspan="7">
                            <tlk:RadTextBox runat="server" ID="txtBranchEvaluate" CausesValidation="false" txtNote="MultiLine" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="7">
                            <tlk:RadTextBox runat="server" ID="txtRemark" CausesValidation="false" txtNote="MultiLine" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
                <tlk:RadGrid ID="rgData" AllowPaging="true" runat="server" EditMode="InPlace"  Height="50%" Scrolling="None" AllowMultiRowEdit="true"
                    AllowMultiRowSelection="true" >
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowPaging="false" AllowCustomPaging="false" DataKeyNames="ID,POINTS_ACTUAL,RESULT_ACTUAL,POINTS_FINAL,RATIO,NOTE,IS_CHECK"
                        ClientDataKeyNames="ID,POINTS_ACTUAL,RESULT_ACTUAL,POINTS_FINAL,RATIO,NOTE,IS_CHECK" CommandItemDisplay="Top" EditMode="InPlace"  EditFormSettings-EditFormType="AutoGenerated">
                        <CommandItemStyle Height="25px" />
                        <CommandItemTemplate>
                            <div style="padding: 2px 0 0 0">
                                <div style="float: left">
                                    <tlk:RadButton Width="100px" ID="btnCalculate" runat="server" Text="Tổng hợp" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/calculator.png"
                                        CausesValidation="false" CommandName="Calculate" TabIndex="3">
                                    </tlk:RadButton>
                                    <tlk:RadButton Width="102px" ID="btnSave" runat="server" Text="Ghi nhận KQ" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/import1.png"
                                        CausesValidation="false" CommandName="Save" TabIndex="4">
                                    </tlk:RadButton>
                                    <tlk:RadButton Width="112px" ID="btnChange" runat="server" Text="Quy đổi" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/calculator.png"
                                        CausesValidation="false" CommandName="Change" TabIndex="5">
                                    </tlk:RadButton>
                                </div>
                            </div>
                        </CommandItemTemplate>
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                                ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Tiêu chí đánh giá" DataField="CRITERIA_NAME" Visible="true"
                                ReadOnly="true" UniqueName="CRITERIA_NAME" SortExpression="CRITERIA_NAME" HeaderStyle-Width="160px"/>
                            <tlk:GridNumericColumn HeaderText="Tỷ trọng" DataField="RATIO" HeaderStyle-Width="100px"
                                ReadOnly="true" UniqueName="RATIO" SortExpression="RATIO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" />

                            <tlk:GridNumericColumn HeaderText="Kết quả tổng hợp" DataField="POINTS_ACTUAL" UniqueName="POINTS_ACTUAL"
                                HeaderStyle-Width="100px" ReadOnly="true" SortExpression="POINTS_ACTUAL" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" />

                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Kết quả import %>" UniqueName="RESULT_ACTUAL" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "RESULT_ACTUAL")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tlk:RadTextBox ID="txtRESULT_ACTUAL" runat="server" Width="100%">
                                    </tlk:RadTextBox>
                                </EditItemTemplate>
                            </tlk:GridTemplateColumn>

                            <%--<tlk:GridNumericColumn HeaderText="Kết quả import" DataField="RESULT_ACTUAL" UniqueName="RESULT_ACTUAL"
                            HeaderStyle-Width="100px" ReadOnly="false" SortExpression="RESULT_ACTUAL" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:N0}" />--%>

                            <tlk:GridNumericColumn HeaderText="Số điểm quy đổi" DataField="POINTS_FINAL" UniqueName="POINTS_FINAL"
                                HeaderStyle-Width="100px" ReadOnly="true" SortExpression="POINTS_FINAL" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" />

                            <tlk:GridBoundColumn HeaderText="Nội dung theo điểm quy đổi" DataField="NOTE" UniqueName="NOTE"
                                HeaderStyle-Width="120px" ReadOnly="true" SortExpression="NOTE" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
        <%--<div style="width: 100%;">
        </div>
        <div style="margin-top: 15px;">
        </div>--%>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
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
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
        }

        function OnClientClose(sender, args) {
            $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
