<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_FRAME_SALARY.ascx.vb"
    Inherits="Payroll.ctrlPA_FRAME_SALARY" %>
<style type="text/css">
    #btnSaveImage {
        display: none;
    }

    .RadUpload .ruFakeInput {
        display: none;
    }

    .RadUpload .ruBrowse {
        width: 120px !important;
        _width: 120px !important;
        width: 120px;
        _width: 120px;
        background-position: 0 -46px !important;
    }

    .hide {
        display: none !important;
    }

    .btnChooseImage {
        margin-left: -5px;
    }

    .ruInputs {
        width: 0px;
        text-align: center;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="400px">
        <tlk:RadTreeView ID="treeOrgFunction" runat="server" CausesValidation="False" Height="93%">
        </tlk:RadTreeView>
        <asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị khung hệ số lương ngừng áp dụng %>"
            AutoPostBack="True" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidParentID" runat="server" />
        <asp:HiddenField ID="hidRepresentative" runat="server" />
        <tlk:RadToolBar ID="tbarOrgFunctions" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbParent_Name" runat="server" Text="Khung lương cha"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtParent_Name" runat="server" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbNameVN" runat="server" Text="Tên tiếng việt (VN)"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNameVN" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="Bạn phải nhập Tên đơn vị" ToolTip="Bạn phải nhập Tên đơn vị"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbNameEN" runat="server" Text="Tên tiếng anh (EN)"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNameEN" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNameVN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Tên đơn vị %>"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>

            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCode" runat="server" Text="Mã khung lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                     <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Mã khung lương %>">
                    </asp:RequiredFieldValidator>
                </td>

                <td class="lb">
                    <asp:Label ID="lblNextCode" runat="server" Text="Mã khung lương kế tiếp"></asp:Label>
                    <%--<span class="lbReq" runat="server" id="Asterrik_NextCode" visible="false">*</span>--%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNextCode" runat="server" ReadOnly="true" SkinID="Textbox50">
                    </tlk:RadTextBox>
                    <%--<asp:RequiredFieldValidator ID="reqNextCode" ControlToValidate="txtNextCode" runat="server" Enabled="false"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã khung lương kết tiếp %>" ToolTip="<%$ Translate:  Bạn phải nhập mã khung lương kết tiếp %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
           

            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lblCoefficient" Text="Hệ số lương"></asp:Label>
                    <span class="lbReq" runat="server" id="Asterrik_Coefficient" visible="false">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="txtCoefficient" ReadOnly="true"  SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqCoefficient" ControlToValidate="txtCoefficient" runat="server" Enabled="false"
                        ErrorMessage="<%$ Translate: Bạn phải nhập hệ số lương %>" ToolTip="<%$ Translate:  Bạn phải nhập hệ số lương %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label8" Text="Số tháng nâng lương"></asp:Label>
                    <span class="lbReq" runat="server" id="Asterrik_Promote_month" visible="false">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="txtPromote_month" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="ReqPromote_month" ControlToValidate="txtPromote_month" runat="server" Enabled="false"
                        ErrorMessage="<%$ Translate: Bạn phải nhập số tháng nâng lương %>" ToolTip="<%$ Translate:  Bạn phải nhập số tháng nâng lương %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <asp:Label ID="lbREMARK" runat="server" Text="Ghi chú"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="4">
                    <tlk:RadTextBox ID="txtREMARK" runat="server" SkinID="Textbox9999" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>

            <tr>
                <td></td>
                <td>
                    <asp:RadioButton runat="server" class="rdCheck" GroupName="level" ID="chkIslevel1" AutoPostBack="True"/>
                    <asp:Label ID="lbOrgChart" runat="server" Text="Ngạch lương"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:RadioButton runat="server" class="rdCheck" GroupName="level" ID="chkIslevel2" AutoPostBack="True"/>
                    <asp:Label ID="Label1" runat="server" Text="Mã, nhóm lương"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="display:none">
                    <asp:Button ID="button" runat="server" CausesValidation="false" Text="Test"  />

                </td>
                <td></td>
                <td>
                    <asp:RadioButton runat="server" class="rdCheck" GroupName="level" ID="chkIslevel3" AutoPostBack="True"/>
                    <asp:Label ID="Label2" runat="server" Text="Bậc lương"></asp:Label>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="rscriptblock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function btnDownFile_Clicking(sender, args) {
            enableAjax = false;
        }

        function validationFailed(sender, args) {
            sender.deleteFileInputAt(0);
            var message = '<%=Translate("Dung lượng ảnh phải < 4MB") %>';
            var notify104126 = noty({ text: message, dismissQueue: true, type: 'error' });
            setTimeout(function () { $.noty.close(notify104126.options.id); }, 2000);
        }
       $(function () {
            var chkIslevel1 = $("[id*=chkIslevel1]");
            var chkIslevel2 = $("[id*=chkIslevel2]");
            var chkIslevel3 = $("[id*=chkIslevel3]");
            chkIslevel1.dblclick(function () {
                if ($(this).is(":checked")) {
                    chkIslevel1.removeAttr("checked");
                }
            });
            chkIslevel2.dblclick(function () {
                if ($(this).is(":checked")) {
                    chkIslevel2.removeAttr("checked");
                }
            });
             chkIslevel3.dblclick(function () {
                if ($(this).is(":checked")) {
                    chkIslevel3.removeAttr("checked");
                    document.getElementById("<%= button.ClientID %>").click();
                }
            });
        }); 
    </script>
</tlk:RadScriptBlock>
