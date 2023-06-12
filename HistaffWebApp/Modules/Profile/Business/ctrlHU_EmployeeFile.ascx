<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmployeeFile.ascx.vb"
    Inherits="Profile.ctrlHU_EmployeeFile" %>
<tlk:RadSplitter runat="server" ID="splitFull" Orientation="Horizontal" Width="100%"
    Height="100%">
    <tlk:RadPane runat="server" ID="paneTop" Height="33px" Scrolling="None">
        <tlk:RadToolBar runat="server" ID="tbarDetail" OnClientButtonClicking="ClientButtonClicking">
        </tlk:RadToolBar>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="paneBottom" Height="150px">
        &nbsp;
        <table class="table-form">
            <tr>
                <asp:HiddenField ID="hidID" runat="server" />
                <td class="lb" style="width: 90px">
                    <%# Translate("Loại văn bản")%><span class="lbReq">*</span>
                </td>
                <td style="vertical-align: top">
                    <tlk:RadComboBox runat="server" ID="cboFileType" CausesValidation="false" AutoPostBack="true">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqType" ControlToValidate="cboFileType" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập loại văn bản. %>" ToolTip="<%$ Translate: Bạn phải nhập loại văn bản. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 90px">
                    <%# Translate("Số lượng")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtSoLuong">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" style="width: 90px">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdEffectDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rqEffect" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 90px">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdExpireDate">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 90px">
                    <%# Translate("Tên văn bản")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFileName">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rqName" ControlToValidate="txtFileName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên văn bản. %>" ToolTip="<%$ Translate: Bạn phải nhập tên văn bản. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 90px">
                    <%# Translate("Số hiệu văn bản")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFileNo">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 90px">
                    <%# Translate("Nơi ban hành")%>
                </td>
                <td colspan="3" style="vertical-align: top">
                    <tlk:RadTextBox runat="server" ID="txtEffectLocation" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 90px">
                    <%# Translate("Ghi chú")%>
                </td>
                <td style="vertical-align: top" colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtNote" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="vertical-align: top">
                    <%# Translate("Tập tin:")%><span class="lbReq">*</span>
                </td>
                <td style="width: 300px; vertical-align: top" colspan="3">
                    <asp:Label runat="server" ID="lblFileName"></asp:Label>
                    <tlk:RadAsyncUpload runat="server" ID="rupFileAttatch" MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                        Width="220px" OnClientValidationFailed="ClientValidationFalse" MaxFileSize="4096000">
                        <Localization Select="<%$ Translate: Chọn tệp %>" />
                        <Localization Remove="<%$ Translate: Xóa %>" />
                        <Localization Cancel="<%$ Translate: Hủy bỏ %>" />
                    </tlk:RadAsyncUpload>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="paneGrid" Scrolling="None">
        <tlk:RadGrid runat="server" ID="rgAttatchFiles" Width="100%" Height="100%" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,NAME_VN, FILE_NAME, NOTE,EFFECT_DATE,EXPIRE_DATE,FILE_NO,EFFECT_LOCATION,FILE_NUMBER">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" SortExpression="ID" AutoPostBackOnFilter="true"
                        ShowFilterIcon="false" UniqueName="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số hiệu văn bản %>" DataField="FILE_NO"
                        SortExpression="FILE_NO" AutoPostBackOnFilter="true" ShowFilterIcon="false" UniqueName="FILE_NO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên văn bản %>" DataField="NAME_VN"
                        SortExpression="NAME_VN" AutoPostBackOnFilter="true" ShowFilterIcon="false" UniqueName="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại văn bản %>" DataField="FILE_TYPE_NAME"
                        SortExpression="FILE_TYPE_NAME" ShowFilterIcon="false" UniqueName="FILE_TYPE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số lượng %>" DataField="FILE_NUMBER"
                        SortExpression="FILE_NUMBER" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        UniqueName="FILE_NUMBER">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực%>" DataField="EFFECT_DATE"
                        AutoPostBackOnFilter="true" ShowFilterIcon="false" AndCurrentFilterFunction="Contains"
                        SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="EFFECT_DATE" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                        AutoPostBackOnFilter="true" ShowFilterIcon="false" AndCurrentFilterFunction="Contains"
                        SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="EXPIRE_DATE">
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi ban hành %>" DataField="EFFECT_LOCATION"
                        SortExpression="EFFECT_LOCATION" ShowFilterIcon="false" UniqueName="EFFECT_LOCATION"
                        AutoPostBackOnFilter="true" AndCurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tệp %>" DataField="FILE_NAME"
                        SortExpression="FILE_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        UniqueName="FILE_NAME" />
                    <tlk:GridTemplateColumn HeaderText="" ShowFilterIcon="false">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <a href='<%# "/GetFileManager.aspx?fid=" & Eval("ID").ToString() %>' target="_blank">
                                <img src="/Static/Images/Icons/16/download.png" alt='<%# Translate("Download file") %>'
                                    style="border: 0" />&nbsp; Mở file </a>
                        </ItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" AutoPostBackOnFilter="true"
                        ShowFilterIcon="false" AndCurrentFilterFunction="Contains" SortExpression="NOTE"
                        UniqueName="NOTE">
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<div style="display: none">
    <asp:Button runat="server" ID="btnSubmit" />
</div>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script language="javascript" type="text/javascript">
        var isUploading = false;

        function ClientButtonClicking(sender, args) {

            var itemValue = args.get_item().get_commandName();
            if (itemValue == 'SAVE') {
                args.set_cancel(true);
                var isHasFile = $('.ruUploadProgress').length != 0;

                $('#<%# btnSubmit.ClientID %>').click();
                ResizeSplitter();
                return;
            }
            else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }

            if (itemValue == 'DELETE' || itemValue == 'EDIT') {
                var masterTable = $find('<%# rgAttatchFiles.ClientID %>').get_masterTableView();
                var gridSelectCount = 0;
                if (masterTable != null) {
                    gridSelectCount = masterTable.get_selectedItems().length;
                }

                if (gridSelectCount == 0) {
                    var msg = '<%# Translate("Bạn chưa chọn File cần xử lý") %>';
                    var n = noty({ text: msg, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            }

        }

        function ClientValidationFalse(sender, args) {
            sender.deleteAllFileInputs();
            isUploading = false;
            var msg = '<%# Translate("File bạn chọn có dung lượng > 4 MB.") %>';
            var n = noty({ text: msg, dismissQueue: true, type: 'warning' });
            setTimeout(function () { $.noty.close(n.options.id); }, 5000);
        }

        var enableAjax = true;
        var oldSize = 0;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%# splitFull.ClientID%>");
                var pane = splitter.getPaneById('<%# paneBottom.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height + 35);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%# splitFull.ClientID%>");
            var pane = splitter.getPaneById('<%# paneBottom.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%# paneGrid.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize + 35);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadScriptBlock>
