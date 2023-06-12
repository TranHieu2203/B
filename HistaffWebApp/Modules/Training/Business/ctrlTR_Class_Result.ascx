<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Class_Result.ascx.vb"
    Inherits="Training.ctrlTR_Class_Result" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidProgramID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="170px" Scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table>
            <tr>
                <td valign="top">
        <table class="table-form">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chương trình đào tạo")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã khóa đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCourseCode" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Khóa đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCourseName" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                
                <td class="lb" style="display:none">
                    <%# Translate("Loại đào tạo")%>
                </td>
                <td style="display:none">
                    <tlk:RadTextBox runat="server" ID="txtCourseType">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thời gian tổ chức đào tạo từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdToDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên bằng cấp/chứng chỉ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCERTIFICATE_NAME" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>

            <tr style="display:none">
                <td class="lb">
                    <%# Translate("Tên chương trình")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtProgramName" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="display:none">
                <td class="lb">
                    <%# Translate("Hình thức")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtHinhThuc">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Kết quả đào tạo")%>
                    </b>
                </td>
            </tr>
            
        </table>
                    </td>
                <td style="min-width: 40px">
                </td>
                <td valign="top">
                    <table class="table-form">
                        <tr>
                            <td class="item-head" colspan="6">
                                <%# Translate("Danh sách lớp học")%>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList runat="server" ID="rlbExams" CausesValidation="false" AutoPostBack="true">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
            </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowMultiRowEdit="true" AllowSorting="false">
            <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,IS_EXAM,NOTE" ClientDataKeyNames="ID,EMPLOYEE_ID,IS_EXAM,NOTE" EditMode="InPlace">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã học viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" ReadOnly="true" HeaderStyle-Width="70px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                        ReadOnly="true" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" ReadOnly="true" HeaderStyle-Width="150px" />
                        
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ReadOnly="true" HeaderStyle-Width="150px" />
                   
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm số %>" DataField="IS_EXAM"
                        UniqueName="IS_EXAM" SortExpression="IS_EXAM" DataFormatString="{0:n0}"
                        HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Right" />

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="150px" />
            <ClientSettings EnablePostBackOnRowClick="true">
                <%--<Scrolling FrozenColumnsCount="4" />--%>
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </tlk:RadGrid>
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
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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

        function OpenNew() {
            var listID = '';
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            var masterTable = $find("<%= rgData.ClientID %>").get_masterTableView();
            var row = masterTable.get_selectedItems();
            for (var i = 0; i < row.length; i++) {
                var Insert_HSNV = masterTable.get_selectedItems()[i].getDataKeyValue('INSERT_HSNV');
                if (Insert_HSNV == 1)
                    return 1;
                listID = listID + masterTable.get_selectedItems()[i].getDataKeyValue('ID') + ',';
            }
            var ProgramID = $get("<%= hidProgramID.ClientID %>").value;
            var oWindow = radopen('Dialog.aspx?mid=Training&fid=ctrlTR_ConfirmCertificate&group=Business&PROGRAM_ID=' + ProgramID + '&LIST_ID=' + listID + '&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            oWindow.center();
            return 2;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "SEND_TRAINING_TO_PROFILE_ENABLE") {
                bCheck = OpenNew();
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%# Translate("Tồn tại nhân viên đã được cập nhật thông tin sang HSNV") %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                args.set_cancel(true);
            }
        }

        function OnClientClose(oWnd, args) {
            $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
        }

    </script>
</tlk:RadCodeBlock>
