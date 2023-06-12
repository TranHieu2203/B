<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_TitleCourse.ascx.vb"
    Inherits="Training.ctrlTR_TitleCourse" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="180px" Width="100%" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb1">
                            <asp:Label ID="Label2" runat="server" Text="Nhóm chức danh"></asp:Label>
                            
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTitleGroup" runat="server" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqTitleGroup" ControlToValidate="cboTitleGroup"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nhóm chức danh %>" ToolTip="<%$ Translate: Bạn phải chọn nhóm chức danh %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb1">
                            <asp:Label ID="Label3" runat="server" Text="Nhóm chức danh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTitle" runat="server" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbCode" runat="server" Text="Nhóm chức danh"></asp:Label>
                            
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtGroupTitle" runat="server" CausesValidation="false" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label1" runat="server" Text="Chức danh"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTitle" runat="server" CausesValidation="false" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <b>
                                <%# Translate("Thiết lập khóa đào tạo theo chuẩn chức danh")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboCourse" runat="server" AutoPostBack="false" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboCourse"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập khóa đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập khóa đào tạo %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEffectDate">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsCheck" runat="server" Text="<%$ Translate: Khóa học bắt buộc %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mô tả")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="472px" Height="43px" />
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <%-- <b>
                    <%# Translate("Danh sách khóa đào tạo theo chức danh")%></b>
                <hr />--%>
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" SkinID="GridSingleSelect" Scrolling="Y">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,TR_COURSE_REMARK,TR_COURSE_ID,TR_COURSE_CODE,TR_COURSE_NAME,TITLE_GROUP_NAME,TITLE_GROUP_ID,HU_TITLE_NAME,HU_TITLE_ID,IS_CHECK,EFFECT_DATE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="TR_COURSE_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chức danh %>" DataField="TITLE_GROUP_NAME"
                                SortExpression="TITLE_GROUP_NAME" UniqueName="TITLE_GROUP_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="HU_TITLE_NAME"
                                SortExpression="HU_TITLE_NAME" UniqueName="HU_TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã khóa đào tạo %>" DataField="TR_COURSE_CODE"
                                SortExpression="TR_COURSE_CODE" UniqueName="TR_COURSE_CODE" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Bắt buộc %>" AllowFiltering="false"
                                DataField="IS_CHECK" SortExpression="IS_CHECK" UniqueName="IS_CHECK" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên khóa đào tạo %>" DataField="TR_COURSE_NAME"
                                SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" >
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="TR_COURSE_REMARK"
                                SortExpression="TR_COURSE_REMARK" UniqueName="TR_COURSE_REMARK" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == "EXPORT_TEMPLATE") {
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

    </script>
</tlk:RadCodeBlock>
