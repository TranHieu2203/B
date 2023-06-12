<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_DisciplineNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_DisciplineNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpCode" runat="server" />
<asp:HiddenField ID="hidEmp" runat="server" />
<asp:HiddenField ID="hidOrg_Name_2" runat="server" />
<asp:HiddenField ID="hidDecisionID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarDiscipline" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="300px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td colspan="6">
                    <b>
                        <asp:Label runat="server" ID="DisciplineInfo" Text="Thông tin kỷ luật"></asp:Label>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Năm kỷ luật")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                        TabIndex="12">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbViolationDate" Text=" Ngày vi phạm"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdViolationDate" runat="server" TabIndex="16">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDetectViolationDate" Text=" Ngày phát hiện vi phạm"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDetectViolationDate" runat="server" TabIndex="17">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbStatus">Trạng thái <span class="lbReq">*</span></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server" TabIndex="11">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqStatus" runat="server" ErrorMessage="Bạn phải chọn trạng thái"
                        ToolTip="Bạn phải chọn trạng thái" ControlToValidate="cboStatus">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusStatus" ControlToValidate="cboStatus" runat="server" ErrorMessage="Trạng thái không tồn tại hoặc đã ngừng áp dụng."
                        ToolTip="Trạng thái không tồn tại hoặc đã ngừng áp dụng.">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDisciplineObj" Text="Đối tượng"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDisciplineObj" runat="server" AutoPostBack="True" TabIndex="12"
                        CausesValidation="False">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDisciplineObj" runat="server" ErrorMessage="Bạn phải nhập đối tượng"
                        ToolTip="Bạn phải nhập đối tượng" ControlToValidate="cboDisciplineObj">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalDisciplineObj" ControlToValidate="cboDisciplineObj" runat="server" ErrorMessage="Đối tượng không tồn tại hoặc đã ngừng áp dụng."
                        ToolTip="Đối tượng không tồn tại hoặc đã ngừng áp dụng.">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDisciplineType" Text="Hình thức"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDisciplineType" runat="server" TabIndex="13" CausesValidation="False">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDisciplineType" runat="server" ErrorMessage="Bạn phải chọn hình thức kỷ luật"
                        ToolTip="Bạn phải chọn hình thức kỷ luật" ControlToValidate="cboDisciplineType">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalDisciplineType" ControlToValidate="cboDisciplineType" runat="server" ErrorMessage="Hình thức kỷ luật không tồn tại hoặc đã ngừng áp dụng."
                        ToolTip="Hình thức kỷ luật không tồn tại hoặc đã ngừng áp dụng.">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label2" Text="Cấp kỷ luật"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbolevel" runat="server" TabIndex="14" CausesValidation="False">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Bạn phải nhập cấp kỷ luật"
                        ToolTip="Bạn phải nhập cấp kỷ luật" ControlToValidate="cbolevel">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbNoteDiscipline" Text="Căn cứ biên bản kỷ luật"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtNoteDiscipline" runat="server" TabIndex="4" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDisciplineReasonDetail" Text="Lý do kỷ luật chi tiết"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtDisciplineReasonDetail" runat="server" TabIndex="15" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>

                <td class="lb">
                    <asp:Label runat="server" ID="lbEffectDate" Text="Ngày hiệu lực"></asp:Label>
                    <span class="lbReq">*</span>

                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server" TabIndex="2" AutoPostBack="true">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực." ToolTip="Bạn phải nhập ngày hiệu lực."> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbExpireDate" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server" TabIndex="3">
                    </tlk:RadDatePicker>
                    <asp:CustomValidator ID="cval_EffectDate_ExpireDate" runat="server" ErrorMessage="Ngày hết hiệu lực phải lớn hơn ngày hiệu lực."
                        ToolTip="Ngày hết hiệu lực phải lớn hơn ngày hiệu lực.">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDelDiscipline" Text=" Ngày xóa kỷ luật"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDelDisciplineDate" runat="server" TabIndex="10">
                    </tlk:RadDatePicker>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDecisionNo" Text="Số quyết định"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server" TabIndex="1">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqDecisionNo" ControlToValidate="txtDecisionNo"
                        runat="server" ErrorMessage="Bạn phải nhập số quyết định."
                        ToolTip="Bạn phải nhập số quyết định."> 
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusDecisionNo" runat="server" ErrorMessage="Số quyết định đã tồn tại"
                        ToolTip="Số quyết định đã tồn tại">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSignDate" Text="Ngày ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server" TabIndex="6">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtUpload" TabIndex="3" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="Tải xuống"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="5" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr style="visibility: hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSignerName" Text="Người ký"></asp:Label>
                </td>
                <td>
                    <%--<tlk:RadTextBox ID = "txtIDEmp" runat = "server" Visible="false"></tlk:RadTextBox>--%>
                    <tlk:RadTextBox ID="txtSignerName" Width="130px" runat="server" ReadOnly="true" SkinID="ReadOnly" TabIndex="7">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSinger" runat="server" SkinID="ButtonView" CausesValidation="false" TabIndex="8">
                    </tlk:RadButton>
                    <%--<asp:RequiredFieldValidator ID="reqSignerName" ControlToValidate="txtSignerName"
                        runat="server" ErrorMessage="Bạn phải nhập người ký." ToolTip="Bạn phải nhập người ký."> 
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSignerTitle" Text="Vị trí công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerTitle" runat="server" TabIndex="9" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbRemarkDiscipline" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemarkDiscipline" runat="server" TextMode="MultiLine" TabIndex="18"
                        Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <%--<tr>
                <td colspan="6">
                    <b>
                        <asp:Label runat="server" ID="DecisionInfo" Text="Thông tin nhân viên ngoài công ty"></asp:Label>
                    </b>
                    <hr />
                </td>
            </tr>--%>
            <%--<tr>
                <td class="lb" >
                    <asp:Label runat="server" ID="Label3" Text="Họ Tên nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFullnameText" runat="server" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb" >
                    <asp:Label runat="server" ID="Label4" Text="Phòng ban"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgText" runat="server" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb" >
                    <asp:Label runat="server" ID="Label5" Text="Vị trí công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleText" runat="server" >
                    </tlk:RadTextBox>
                </td>
                <td>
                    <tlk:RadButton ID="btnAddEmp" runat="server" Text="Thêm" Icon-PrimaryIconCssClass="rbAdd" CausesValidation="false">
                    </tlk:RadButton>
                </td>
            </tr>--%>
            <%--<tr>
                <td>
                </td>
                <td>
                    <tlk:RadButton ID="btnExport" runat="server" Text="Xuất file mẫu" Icon-PrimaryIconCssClass="rbDownload" CausesValidation="false" OnClientClicked="rbtClicked" >
                    </tlk:RadButton>
                </td>
                <td>
                </td>
                <td>
                    <tlk:RadButton ID="btnImport" runat="server" Text="Nhập file mẫu" Icon-PrimaryIconCssClass="rbUpload" CausesValidation="false" OnClientClicked="rbtClicked" >
                    </tlk:RadButton>
                </td>
            </tr>--%>
        </table>
    </tlk:RadPane>
    <tlk:RadPane runat="server" Scrolling="none" ID="PanelEmployee" Height="100%">
        <tlk:RadGrid PageSize="50" ID="rgEmployee" AllowPaging="false" AllowMultiRowEdit="true"
            runat="server" Height="100%" ShowFooter="True">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="false" AllowCustomPaging="false"
                DataKeyNames="HU_DISCIPLINE_ID,HU_EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME,TITLE_NAME,ORG_NAME,MONEY,INDEMNIFY_MONEY,NO_PROCESS,FULLNAME_TEXT" ClientDataKeyNames="HU_DISCIPLINE_ID,HU_EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME,TITLE_NAME,ORG_NAME,MONEY,INDEMNIFY_MONEY,NO_PROCESS,FULLNAME_TEXT"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnEmployee" runat="server" Text="Chọn nhân viên"
                                CausesValidation="false" CommandName="FindEmployee">
                            </tlk:RadButton>
                            <tlk:RadButton Width="100px" ID="btnQD" runat="server" Text="Tạo Quyết định" Style="display: none"
                                CausesValidation="false" CommandName="CreateQD">
                            </tlk:RadButton>
                            <tlk:RadButton Width="100px" ID="btnHSL" runat="server" Text="Tạo Hồ sơ lương" Style="display: none"
                                CausesValidation="false" CommandName="CreateHSL">
                            </tlk:RadButton>
                            <%--<tlk:RadButton Width="100px" ID="btnShare" runat="server" Text="Chia đều"
                                CausesValidation="false" CommandName="ShareEmployee">
                            </tlk:RadButton>
                            <tlk:RadButton Width="100px" ID="btnCalc" runat="server" Text="Tính toán"
                                CausesValidation="false" CommandName="CalcEmployee">
                            </tlk:RadButton>--%>
                        </div>
                        <div style="float: right;">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmp" runat="server" Text="Xóa"
                                CausesValidation="false" CommandName="DeleteEmployee">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                        ReadOnly="true" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" Aggregate="Count"
                        FooterText="Tổng: ">
                        <HeaderStyle Width="100px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Họ tên" DataField="FULLNAME" UniqueName="FULLNAME"
                        ReadOnly="true" SortExpression="FULLNAME">
                        <HeaderStyle Width="120px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        ReadOnly="true" SortExpression="ORG_NAME">
                        <HeaderStyle Width="200px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME"
                        ReadOnly="true" UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="120px" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
        <tlk:RadGrid PageSize="50" ID="rgOrg" AllowPaging="false" AllowMultiRowEdit="true"
            runat="server" Height="100%" ShowFooter="True">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="false" AllowCustomPaging="false"
                DataKeyNames="HU_DISCIPLINE_ID,ORG_ID,ORG_NAME" ClientDataKeyNames="HU_DISCIPLINE_ID,ORG_ID,ORG_NAME"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnOrg" runat="server" Text="Chọn phòng ban"
                                CausesValidation="false" CommandName="FindOrg">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right;">
                            <tlk:RadButton Width="100px" ID="btnDeleteOrg" runat="server" Text="Xóa"
                                CausesValidation="false" CommandName="DeleteOrg">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        ReadOnly="true" SortExpression="ORG_NAME">
                        <HeaderStyle Width="200px" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="120px" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server" OnAjaxRequest="RadAjaxPanel1_AjaxRequest">
</tlk:RadAjaxPanel>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <Common:ctrlUpload ID="ctrlUpload1" runat="server" />
    <script type="text/javascript">

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
            //                //var radWindow = $find('rwPopup');
            //                //radWindow.close();
            //                args.set_cancel(true);
            //            } 
        }

        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function RedirectTerminate(empID) {
            document.location.href = 'Dialog.aspx?mid=Profile&fid=ctrlHU_TerminateNewEdit&group=Business&noscroll=1&FormType=3&ter_reason=4350&empid=' + empID;
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

        //Tạo Quyết định
        function openQDtab(value) {
            if (value == 0) {
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business', "_blank");
            }
            else {
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business&empid=' + value, "_blank");
            }
        }

        //Tạo Hồ sơ lương
        function openHSLtab(value) {
            if (value == 0) {
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_WageNewEdit&group=Business', "_blank");
            }
            else {
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_WageNewEdit&group=Business&empid=' + value, "_blank");
            }
        }

        function OnClientChanged1(sender, eventArgs) {
            $find("<%= RadAjaxPanel1.ClientID%>").ajaxRequestWithTarget("<%= RadAjaxPanel1.UniqueID %>", sender.get_id());
        }

    </script>
</tlk:RadCodeBlock>
