<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_MngProfileSavedNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_MngProfileSavedNewEdit" %>
<tlk:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<asp:HiddenField ID="hidIDEmp" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="110px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
            ValidationGroup="" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin nhân viên")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 150px">
                    <%# Translate("Mã số nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCode" Enabled="false">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tên")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtName" Enabled="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Enabled="false">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Vị trí công việc")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTitleName" Enabled="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgEmployee" AllowPaging="true" AllowMultiRowEdit="true" runat="server"
            PageSize="50" Height="100%">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="true" AllowCustomPaging="true" DataKeyNames="ID,DOCUMENT_NAME,TYPE_DOCUMENT_NAME,MUST_HAVE,DATE_SUBMIT,IS_SUBMITED,REMARK,ALLOW_UPLOAD_FILE"
                ClientDataKeyNames="ID,DOCUMENT_NAME,TYPE_DOCUMENT_NAME,MUST_HAVE,DATE_SUBMIT,IS_SUBMITED,REMARK,ALLOW_UPLOAD_FILE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="ID" DataField="ID" ReadOnly="true" UniqueName="ID"
                        SortExpression="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Tên tài liệu" DataField="DOCUMENT_NAME" ReadOnly="true"
                        UniqueName="DOCUMENT_NAME" SortExpression="DOCUMENT_NAME" />
                    <tlk:GridBoundColumn HeaderText="Loại tài liệu" DataField="TYPE_DOCUMENT_NAME" UniqueName="TYPE_DOCUMENT_NAME"
                        ReadOnly="true" SortExpression="TYPE_DOCUMENT_NAME" />
                    <tlk:GridCheckBoxColumn HeaderText="Là tài liệu bắt buộc" DataField="MUST_HAVE" ReadOnly="true"
                        UniqueName="MUST_HAVE" SortExpression="MUST_HAVE" AllowFiltering="false" HeaderStyle-Width="50px" />
                    <tlk:GridCheckBoxColumn HeaderText="Cho phép upload file" DataField="ALLOW_UPLOAD_FILE"
                        ReadOnly="true" UniqueName="ALLOW_UPLOAD_FILE" SortExpression="ALLOW_UPLOAD_FILE"
                        AllowFiltering="false" HeaderStyle-Width="50px" />
                    <%-- <tlk:GridCheckBoxColumn HeaderText="Cho phép upload file" DataField="ALLOW_UPLOAD_FILE"
                        ReadOnly="true" UniqueName="ALLOW_UPLOAD_FILE" SortExpression="ALLOW_UPLOAD_FILE"
                        AllowFiltering="false" HeaderStyle-Width="50px" />--%>
                    <%--  <tlk:GridDateTimeColumn HeaderText="Ngày nộp" DataField="DATE_SUBMIT" ItemStyle-HorizontalAlign="Center"
                        SortExpression="DATE_SUBMIT" UniqueName="DATE_SUBMIT" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-Width="200px" />
                    <tlk:GridCheckBoxColumn HeaderText="Đã nộp" DataField="" UniqueName="IS_SUBMITED"
                        SortExpression="IS_SUBMITED" AllowFiltering="false"  HeaderStyle-Width="50px"/>--%>
                    <tlk:GridTemplateColumn HeaderText="Ngày nộp" DataField="DATE_SUBMIT" UniqueName="DATE_SUBMIT"
                        HeaderStyle-Width="200px">
                        <EditItemTemplate>
                            <tlk:RadDatePicker runat="server" ID="rdDateSubmit">
                            </tlk:RadDatePicker>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Đã nộp" DataField="IS_SUBMITED" AllowFiltering="false"
                        UniqueName="IS_SUBMITED" HeaderStyle-Width="50px">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkSubmited" />
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Ghi chú" DataField="REMARK" UniqueName="REMARK">
                        <EditItemTemplate>
                            <tlk:RadTextBox runat="server" ID="rtRemark" Width="175px">
                            </tlk:RadTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn DataField="UPLOAD_FILE" UniqueName="UPLOAD_FILE" HeaderText="<%$ Translate: File đính kèm %>"
                        SortExpression="UPLOAD_FILE">
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbtnUpload" runat="server" CommandName="DownloadFile" CommandArgument='<%# Eval("UPLOAD_FILE") %>'
                                Font-Underline="true"><%# Eval("UPLOAD_FILE") %></asp:LinkButton>
                            <%-- <tlk:RadTextBox ID="_FileName" runat="server" CausesValidation="false" Width="50%"
                                Text='<%# Eval("UPLOAD_FILE") %>' ReadOnly="true" />--%>
                            <tlk:RadButton runat="server" Text="Upload" ID="btnUpload" CommandName="UploadFile"
                                CommandArgument='<%# Eval("ID") %>'>
                            </tlk:RadButton>
                            <tlk:RadButton runat="server" Text="Delete" ID="btnDelete" CommandName="DeleteFile"
                                CommandArgument='<%# Eval("ID") %>'>
                            </tlk:RadButton>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_MngProfileSavedNewEdit_LeftPane');
        });



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

        function clientButtonClicking(sender, args) {
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'Export') {
                enableAjax = false;
            } if (args.get_item().get_commandName() == 'CANCEL') {
                enableAjax = false;
            }
        }
        function btnExportClicking(sender, args) {
            enableAjax = false;
        }
        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {

                default:
                    break;
            }
        }

        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

    </script>
</tlk:RadCodeBlock>
