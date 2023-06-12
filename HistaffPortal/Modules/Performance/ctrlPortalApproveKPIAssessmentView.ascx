<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalApproveKPIAssessmentView.ascx.vb"
    Inherits="Performance.ctrlPortalApproveKPIAssessmentView" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="hidGoalID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="400px" Scrolling="Y">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking"/>
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <div style="width: 100%; float: left;">
            <div style="width: 100%; float: left;">
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
                            <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_Code" ReadOnly="true">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_id" Visible="False">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Họ tên")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnID" ReadOnly="true" Visible="false">
                            </tlk:RadNumericTextBox>
                            <tlk:RadTextBox runat="server" ID="rtEmployee_Name" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Chức danh")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtTitle_Name" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="rtTitle_Id" ReadOnly="true" Visible="False">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Phòng ban")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="rtOrg_Name" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="rtOrg_id" ReadOnly="true" Visible="False">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="RadTextBox2" ReadOnly="true" Width="100%" BorderWidth="0">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboYear" Enabled="false">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ đánh giá")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPeriod" Enabled="false">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdStartDate" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEndDate" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày bắt đầu")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEffectDate" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Số tháng")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtMonthNumber" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Lý do đánh giá")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox runat="server" ID="txtGoal" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="7">
                            <tlk:RadTextBox runat="server" ID="txtNote" txtNote="MultiLine" Width="100%" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="float: none; clear: both;">
        </div>
        <div style="margin-top: 15px;">
            <tlk:RadGrid ID="rgData" AllowPaging="true" runat="server" Height="250px"
                Scrolling="None">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView AllowPaging="false" AllowCustomPaging="false" DataKeyNames="ID,KPI_ASSESSMENT_TEXT,IS_IN_DB,EMPLOYEE_POINT,RATIO"
                    ClientDataKeyNames="ID,KPI_ASSESSMENT_TEXT">
                    <Columns>
                        <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                        </tlk:GridClientSelectColumn>
                        <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                            ReadOnly="true" />
                        <tlk:GridBoundColumn HeaderText="ID Chỉ số đo lường" DataField="KPI_ASSESSMENT" Visible="false"
                            ReadOnly="true" UniqueName="KPI_ASSESSMENT" SortExpression="KPI_ASSESSMENT" />
                        <tlk:GridBoundColumn HeaderText="Chỉ số đo lường" DataField="KPI_ASSESSMENT_TEXT"
                            HeaderStyle-Width="100px" ReadOnly="true" UniqueName="KPI_ASSESSMENT_TEXT" SortExpression="KPI_ASSESSMENT_TEXT"
                            ItemStyle-HorizontalAlign="Center" />
                        <tlk:GridBoundColumn HeaderText="Đơn vị tính" DataField="UNIT_NAME" UniqueName="UNIT_NAME"
                            HeaderStyle-Width="75px" ReadOnly="true" SortExpression="UNIT_NAME" ItemStyle-HorizontalAlign="Center" />
                        <tlk:GridBoundColumn HeaderText="Tần suất đo" DataField="FREQUENCY_NAME" UniqueName="FREQUENCY_NAME"
                            HeaderStyle-Width="75px" ReadOnly="true" SortExpression="FREQUENCY_NAME" ItemStyle-HorizontalAlign="Center" />
                        <tlk:GridBoundColumn HeaderText="Công thức tính" DataField="DESCRIPTION" UniqueName="DESCRIPTION"
                            HeaderStyle-Width="75px" ReadOnly="true" SortExpression="DESCRIPTION" ItemStyle-HorizontalAlign="Center" />
                        <tlk:GridBoundColumn HeaderText="Nguồn đo" DataField="SOURCE_NAME" UniqueName="SOURCE_NAME"
                            HeaderStyle-Width="75px" ReadOnly="true" SortExpression="SOURCE_NAME" ItemStyle-HorizontalAlign="Center" />
                        <tlk:GridBoundColumn HeaderText="Loại tiêu chí" DataField="GOAL_TYPE_NAME" UniqueName="GOAL_TYPE_NAME"
                            HeaderStyle-Width="75px" ReadOnly="true" SortExpression="GOAL_TYPE_NAME" ItemStyle-HorizontalAlign="Center" />
                        <tlk:GridBoundColumn HeaderText="Chỉ tiêu" DataField="TARGET" UniqueName="TARGET"
                            HeaderStyle-Width="75px" ReadOnly="true" SortExpression="TARGET" ItemStyle-HorizontalAlign="Center" />
                        <tlk:GridBoundColumn HeaderText="Ngưỡng" DataField="TARGET_MIN" UniqueName="TARGET_MIN"
                            HeaderStyle-Width="75px" ReadOnly="true" SortExpression="TARGET_MIN" ItemStyle-HorizontalAlign="Center" />
                        <tlk:GridBoundColumn HeaderText="Trọng số" DataField="RATIO" UniqueName="RATIO" HeaderStyle-Width="75px"
                            ReadOnly="true" SortExpression="RATIO" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </MasterTableView>
                <HeaderStyle HorizontalAlign="Center" />
                <ClientSettings>
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </tlk:RadGrid>
        </div>
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
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
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

        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule');
                else
                //                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                    ResizeSplitterDefault();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            debugger;
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
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function OnClientClose(sender, args) {
            console.log($find("<%= rgData.ClientID %>").get_masterTableView());
            $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
    </script>
</tlk:RadCodeBlock>
