<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlFile.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlFile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<asp:HiddenField ID="hidHuFileID" runat="server" />
<asp:HiddenField ID="txtFileNameSys" runat="server" />
<asp:HiddenField ID="txtFileNameDL" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="70px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPaneLeft" runat="server" Height="210px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 130px">
                    <asp:Label ID="lbFullName" runat="server" Text="Tên văn bản"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFullName">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqFullname" ControlToValidate="txtFullName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên văn bản %>" ToolTip="<%$ Translate: Bạn phải nhập Tên văn bản  %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                 <asp:Label ID="lbNumberCode" runat="server" Text="Số hiệu ban hành"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtNumberCode" SkinID="Textbox15">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbradAsynceUpload1" runat="server" Text="Tệp tin"></asp:Label>
                </td>
                <td style="padding-left: 12px;">
                    <tlk:RadAsyncUpload Width="120px" Height="20px" runat="server" ID="_radAsynceUpload1"
                        ControlObjectsVisibility="None" OnClientFileUploaded="fileUploaded1" OnClientValidationFailed="validationFailed"
                        EnableAjaxSkinRendering="true" MaxFileSize="4096000" CssClass="btnChooseImage"
                        HideFileInput="True" DisablePlugins="True" MaxFileInputsCount="1" MultipleFileSelection="Disabled">
                        <Localization Select="<%$ Translate: Chọn file %>" />
                    </tlk:RadAsyncUpload>
                    <tlk:RadTextBox ID="txtFileNameVN" runat="server" ReadOnly="true" Style="border: none;">
                    </tlk:RadTextBox>
                    <%--  <asp:LinkButton ID="txtFileName" runat="server" OnClientClick="txtFileName_Click('<%# "txtDownload_Click(""" & Eval("ID").ToString() &  """ , """ &   Eval("FILENAME").ToString() & """, """ &   Eval("FILENAME_SYS").ToString() & """)" %>')">
                    </asp:LinkButton>--%>
                </td>
                <td class="lb" style="width: 130px">
                      <asp:Label ID="lbSign" runat="server" Text="Người ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSign">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbAdress" runat="server" Text="Cơ quan ban hành"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtAdress" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbFromDate" runat="server" Text="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdFromDate">
                        <DateInput ID="DateInput1" onkeydown="_rdFromDate_OnKeyDown(event)" runat="server">
                            <ClientEvents OnFocus="_rdFromDate_OnFocus" />
                        </DateInput>
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                   <asp:Label ID="lbToDate" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdToDate">
                        <DateInput ID="DateInput2" onkeydown="_rdToDate_OnKeyDown(event)" runat="server">
                            <ClientEvents OnFocus="_rdToDate_OnFocus" />
                        </DateInput>
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="compareFromDate_DeductToDate" runat="server" ErrorMessage="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>"
                        ToolTip="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>" ControlToValidate="rdToDate"
                        ControlToCompare="rdFromDate" Operator="GreaterThanEqual" Type="Date">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                  <asp:Label ID="lbNote" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtNote" SkinID="Textbox1023" Width="100%" />
                </td>
            </tr>
        </table>
        <div style="display: none;">
            <asp:Button runat="server" ID="btnSaveFile" CausesValidation="false" />
            <asp:Button runat="server" ID="btnExportFile" CausesValidation="false" />
            <asp:Button runat="server" ID="btnDownload" CausesValidation="false" />
        </div>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="20" ID="rgHuFile" runat="server" AllowMultiRowSelection="true"
            Height="100%">
            <MasterTableView DataKeyNames="ID,TYPE_ID,NAME,NUMBER_CODE,ADDRESS,FROM_DATE,TO_DATE,FILENAME,REMARK,ACTFLG,FILENAME_SYS,SIGN_PERSON"
                ClientDataKeyNames="ID,TYPE_ID,NAME,NUMBER_CODE,ADDRESS,FROM_DATE,TO_DATE,FILENAME,REMARK,ACTFLG,FILENAME_SYS,SIGN_PERSON">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                  <tlk:GridClientSelectColumn HeaderStyle-Width="40px">
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="NUMBER_CODE" HeaderText="Số hiệu văn bản"
                        UniqueName="NUMBER_CODE">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NAME" HeaderText="Tên văn bản"
                        UniqueName="NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SIGN_PERSON" HeaderText="Người ký"
                        UniqueName="SIGN_PERSON">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FILENAME" HeaderText="Tên file"
                        Visible="true" UniqueName="FILENAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="Ngày hiệu lực"
                        UniqueName="FROM_DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="Ngày hết hiệu lực"
                        UniqueName="TO_DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn DataField="ADDRESS" HeaderText="Nơi ban hành"
                        UniqueName="ADDRESS" EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="REMARK" HeaderText="Ghi chú" UniqueName="REMARK"
                        EmptyDataText="">
                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    </tlk:GridBoundColumn>
                    <%--<asp:LinkButton ID ="LinkButton1" runat="server" OnClientClick="download(<%$ Eval("FILENAME_SYS").ToString() %>)" Text="Tải tệp">
                    </asp:LinkButton>   --%>
                    <%--<tlk:GridHyperLinkColumn UniqueName="FILENAME_SYS" 
                            FilterControlAltText="Filter column column" DataNavigateUrlFields="FILENAME_SYS" 
                            Text="Tải tệp" >
                    </tlk:GridHyperLinkColumn>--%>
                 <tlk:GridTemplateColumn DataField="ID" HeaderText="Tải tệp" UniqueName="ID">
                        <ItemTemplate>
                            <asp:LinkButton ID="txtDownload" runat="server" Text=' Tải tệp' CausesValidation="false"
                                OnClientClick='<%# "txtDownload_Click(""" & Eval("ID").ToString() &  """ , """ &   Eval("FILENAME").ToString() & """, """ &   Eval("FILENAME_SYS").ToString() & """)" %>'></asp:LinkButton>
                        </ItemTemplate>
                    </tlk:GridTemplateColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
            <ClientSettings EnablePostBackOnRowClick="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<script type="text/javascript">
    function _rdFromDate_OnFocus(sender, args) {
        sender.get_owner().showPopup();
    }
    function _rdFromDate_OnKeyDown(e) {
        if (e.keyCode == 9) {
            $find("<%= rdFromDate.ClientID %>").hidePopup();
        }
    }
    function _rdToDate_OnFocus(sender, args) {
        sender.get_owner().showPopup();
    }
    function _rdToDate_OnKeyDown(e) {
        if (e.keyCode == 9) {
            $find("<%= rdToDate.ClientID %>").hidePopup();
        }
    }
    var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
    function fileUploaded1(sender, args) {
        $get('<%= btnSaveFile.ClientID %>').click();
    }
    function validationFailed(sender, args) {
        sender.deleteFileInputAt(0);
        var message = '<%=Translate("Dung lượng ảnh phải < 4MB") %>';
        var notify104126 = noty({ text: message, dismissQueue: true, type: 'error' });
        setTimeout(function () { $.noty.close(notify104126.options.id); }, 2000);
    }

    function txtDownload_Click(val1, val2, val3) {
        document.getElementById('<%= hidHuFileID.ClientID %>').value = val1;
        document.getElementById('<%= txtFileNameSys.ClientID %>').value = val3;
        document.getElementById('<%= txtFileNameDL.ClientID %>').value = val2;
        //$("#txtDownloadFile").val(value);
        //alert($("#txtDownloadFile").val());
        enableAjax = false;
        $get('<%= btnDownload.ClientID %>').click();
    }

    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (item.get_commandName() == "EXPORT") {
            enableAjax = false;
        }
    }
</script>
