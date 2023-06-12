<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalDisciplineNewEditMng.ascx.vb"
    Inherits="Profile.ctrlPortalDisciplineNewEditMng" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpCode" runat="server" />
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
                        TabIndex="12" >
                    </tlk:RadComboBox>
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
                    <asp:Label runat="server" ID="lbRemarkDiscipline" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemarkDiscipline" runat="server" TextMode="MultiLine" TabIndex="18"
                        Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
           <tr>
                <td colspan="6">
                    <b>
                        <asp:Label runat="server" ID="DecisionInfo" Text="Thông tin quyết định"></asp:Label>
                    </b>
                    <hr />
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
                
            </tr>
            <tr>
                 <td class="lb">
                    <asp:Label runat="server" ID="lbStatus" Text="Trạng thái"></asp:Label>
                    <span class="lbReq">*</span>
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
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSignerName" Text="Người ký"></asp:Label>
                </td>
                <td>
                    <%--<tlk:RadTextBox ID = "txtIDEmp" runat = "server" Visible="false"></tlk:RadTextBox>--%>
                    <tlk:RadTextBox ID="txtSignerName" Width="130px" runat="server" ReadOnly="true" SkinID="ReadOnly" TabIndex="7">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSinger" runat="server" SkinID="ButtonView" CausesValidation="false" TabIndex="8" Enabled="false">
                    </tlk:RadButton>
                    <%--<asp:RequiredFieldValidator ID="reqSignerName" ControlToValidate="txtSignerName"
                        runat="server" ErrorMessage="Bạn phải nhập người ký." ToolTip="Bạn phải nhập người ký."> 
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSignerTitle" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerTitle" runat="server" TabIndex="9" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr >
                <td class="lb" style="display: none">
                    <asp:Label runat="server" ID="lbDisciplineReason" Text="Lý do kỷ luật"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadComboBox ID="cboDisciplineReason" runat="server" TabIndex="14" CausesValidation="False">
                    </tlk:RadComboBox>
                   <%-- <asp:RequiredFieldValidator ID="reqDisciplineReason" runat="server" ErrorMessage="Bạn phải nhập lý do kỷ luật"
                        ToolTip="Bạn phải nhập lý do kỷ luật" ControlToValidate="cboDisciplineReason">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalDisciplineReason" ControlToValidate="cboDisciplineReason" runat="server" ErrorMessage="Lý do kỷ luật không tồn tại hoặc đã ngừng áp dụng."
                        ToolTip="Lý do kỷ luật không tồn tại hoặc đã ngừng áp dụng.">
                    </asp:CustomValidator>--%>
                </td>
                
                
            </tr>
           
            <tr style="display: none">
                <td class="lb">
                    <asp:Label runat="server" ID="lbExplain" Text="Giải thích sự việc"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtExplain" runat="server" TextMode="MultiLine" Width="100%" TabIndex="18">
                    </tlk:RadTextBox>
                </td>
            </tr>
           
            <tr style="display: none">
                <td colspan="6">
                    <b>
                        <asp:Label runat="server" ID="lbCompensationInfo" Text="Thông tin bồi thường"></asp:Label>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:CheckBox ID="chkPhatTien" runat="server" Checked="false" CausesValidation="false" TextAlign="Right" AutoPostBack="true" />
                    <asp:Label runat="server" ID="lbMoney" Text="Số tiền phạt"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtMoney" runat="server" SkinID="Money" TabIndex="19" ClientEvents-OnBlur="OnClientChanged1">
                    </tlk:RadNumericTextBox>
                    <%--<asp:CustomValidator ID="cvalMoney" runat="server" ErrorMessage="Bạn phải nhập số tiền phải >= 0."
                        ToolTip="Bạn phải nhập số tiền phải >= 0.">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbIndemnifyMoney" Text="Tổng giá trị thiệt hại"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtIndemnifyMoney" runat="server" SkinID="Money" TabIndex="20" ClientEvents-OnBlur="OnClientChanged1">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%--<asp:Label runat="server" ID="lbPaidInMoeny" Text="Thanh toán tiền" Visible = "false"></asp:Label>--%>
                    <asp:Label runat="server" ID="lbPaidIMoeny" Text="Tổng số tiền phải nộp"></asp:Label>

                </td>
                <td>
                    <tlk:RadButton ID="PaidInMoeny" ButtonType="ToggleButton" runat="server" Visible="false"></tlk:RadButton>
                    <tlk:RadNumericTextBox ID="rnPaidIMoeny" runat="server" SkinID="Money" TabIndex="21" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label runat="server" ID="lbAmountPaidCash" Text="Số tiền đã nộp bằng tiền mặt"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rdAmountPaidCash" runat="server" SkinID="Money" TabIndex="22" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbAmountToPaid" Text="Số tiền còn phải nộp"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnAmountToPaid" runat="server" SkinID="Money" TabIndex="23" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                    <%--<asp:CustomValidator ID="cvalAmountToPaid" runat="server" ErrorMessage="Số tiền còn phải nộp phải nhỏ hơn tổng số tiền phải nộp."
                        ToolTip="Số tiền còn phải nộp phải nhỏ hơn tổng số tiền phải nộp.">
                    </asp:CustomValidator>  --%>              
                </td>
            </tr>
            <tr style="display: none">
                <td colspan="6">
                    <b>
                        <asp:CheckBox ID="chkDeductFromSalary" runat="server" Text="Trừ vào lương"
                            Checked="false" CausesValidation="false" TextAlign="Right" AutoPostBack="true" />
                    </b>
                    <hr />
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label runat="server" ID="lbAmountSalaryMonth" Text="Số tiền trừ trực tiếp vào tháng lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnAmountSalaryMonth" runat="server" SkinID="Money" TabIndex="24" Enabled="false">
                    </tlk:RadNumericTextBox>
                    <%-- <asp:CustomValidator ID="cvalAmountSalaryMonth" runat="server" ErrorMessage="Số tiền trừ trực tiếp vào lương phải nhỏ hơn số tiền còn phải nộp">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbPeriod" Text="Kỳ lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmYear" runat="server" AutoPostBack="true" Enabled="false" TabIndex="26"
                        MaxLength="4" MinValue="1900" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <tlk:RadComboBox ID="cboPeriod" runat="server" Enabled="false" TabIndex="25">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:CheckBox ID="chkAmountInMonth" runat="server" Checked="false" CausesValidation="false" AutoPostBack="true" Enabled="false" />
                    <asp:Label runat="server" ID="lbAmountInMonth" Text="Tỷ lệ trừ vào mỗi tháng lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnAmountInMonth" runat="server" SkinID="Money" TabIndex="27" Enabled="false">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%--<asp:Label runat="server" ID="lbPerformDiscipline" Text="Thời gian thi hành kỷ luật"></asp:Label>--%>
                    <asp:Label runat="server" ID="lbAmountDeductedMonth" Text="Số tiền trừ mỗi tháng"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox ID="txtPerformDiscipline" runat="server" TabIndex="28">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnAmountDeductedMonth" runat="server" SkinID="Money" TabIndex="29" Enabled="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>


            <tr style="display: none">
                <td class="lb"></td>
                <td></td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionName" runat="server" TabIndex="10" Visible="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="visibility: hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="display: none">

                <td class="lb">
                    <asp:Label runat="server" ID="lbDisciplineLevel" Text="Cấp kỷ luật"></asp:Label>
                    <asp:Label runat="server" ID="lbDecisionNo_Discipline" Text="Số quyết định"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo_Discipline" runat="server" TabIndex="4">
                    </tlk:RadTextBox>


                    <tlk:RadComboBox ID="cboDisciplineLevel" runat="server" TabIndex="12" CausesValidation="False">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDisciplineLevel" runat="server" ErrorMessage="Bạn phải nhập cấp kỷ luật"
                        ToolTip="Bạn phải nhập cấp kỷ luật" ControlToValidate="cboDisciplineLevel">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalDisciplineLevel" ControlToValidate="cboDisciplineLevel" runat="server" ErrorMessage="Cấp kỷ luật không tồn tại hoặc đã ngừng áp dụng."
                        ToolTip="Cấp kỷ luật không tồn tại hoặc đã ngừng áp dụng.">
                    </asp:CustomValidator>
                </td>

            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label runat="server" ID="lbRemark" Text="Hành vi vi phạm"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" TextMode="MultiLine" TabIndex="17"
                        Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>

        </table>
        <tlk:RadGrid PageSize="50" ID="rgEmployee" AllowPaging="false" AllowMultiRowEdit="true"
            runat="server" Height="100%" ShowFooter="True">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="false" AllowCustomPaging="false"
                DataKeyNames="HU_DISCIPLINE_ID,HU_EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME,TITLE_NAME,ORG_NAME,MONEY,INDEMNIFY_MONEY,NO_PROCESS" ClientDataKeyNames="HU_DISCIPLINE_ID,HU_EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME,TITLE_NAME,ORG_NAME,MONEY,INDEMNIFY_MONEY,NO_PROCESS"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnEmployee" runat="server" Text="Chọn nhân viên"
                                CausesValidation="false" CommandName="FindEmployee">
                            </tlk:RadButton>
                            <%--<tlk:RadButton Width="100px" ID="btnQD" runat="server" Text="Tạo Quyết định" style="display: none!important"
                                CausesValidation="false" CommandName="CreateQD">
                            </tlk:RadButton>
                            <tlk:RadButton Width="100px" ID="btnHSL" runat="server" Text="Tạo Hồ sơ lương" style="display: none!important"
                                CausesValidation="false" CommandName="CreateHSL">
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
                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                        ReadOnly="true" UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" UniqueName="ORG_NAME"
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
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server" OnAjaxRequest="RadAjaxPanel1_AjaxRequest">
</tlk:RadAjaxPanel>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <common:ctrlupload id="ctrlUpload1" runat="server" />
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

        function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);            winH = $(window).height() - 210;            winW = $(window).width() - 90;            $("#ctl00_MainContent_ctrlPortalDisciplineNewEditMng_RadPane2").stop().animate({ height: winH - 30, width: winW }, 0);            $("#ctl00_MainContent_ctl00_MainContent_Panel1Panel").stop().animate({ height: winH, width: winW }, 0);            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPortalDisciplineNewEditMng_RadPane2").stop().animate({ height: winH - 30, width: winW }, 0);            Sys.Application.add_load(SizeToFitMain);
        }        SizeToFitMain();        $(document).ready(function () {
            SizeToFitMain();
        });        $(window).resize(function () {
            SizeToFitMain();
        });
    </script>
</tlk:RadCodeBlock>
