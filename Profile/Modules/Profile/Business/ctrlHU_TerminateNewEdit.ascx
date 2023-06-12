<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TerminateNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_TerminateNewEdit" %>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarTerminate" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidTitleID" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:HiddenField ID="hiSalbasic" runat="server" />
        <asp:HiddenField ID="hidWorkStatus" runat="server" />
        <asp:HiddenField ID="hidCheckDelete" runat="server" />
        <asp:HiddenField ID="Hid_IsEnter" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin chung")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEmployeeCode" Text="MSNV"></asp:Label>
                    <span style="color: red">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" AutoPostBack="true" Width="130px">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải nhập nhân viên." ToolTip="Bạn phải nhập nhân viên."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEmployeeName" Text="Họ tên nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbOrgName" Text="Phòng ban"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <%--<tr>
                
                <td class="lb">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtStaffRankName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>--%>
            <%--<td class="lb">
                    <%# Translate("Lương cơ bản")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalBasic" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>--%>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTitleName" Text="Vị trí công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>

                <td class="lb">
                    <asp:Label runat="server" ID="lbJoinDateState" Text="Ngày vào làm"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdJoinDateState" runat="server" Enabled="False" ReadOnly="true">
                    </tlk:RadDatePicker>
                </td>
                <%--
                <td class="lb">
                    <%# Translate("Chi phí hỗ trợ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostSupport" runat="server" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadNumericTextBox>
                </td>--%>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSeniority" Text="Thâm niên công tác"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSeniority" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <%--<td class="lb">
                    <asp:Label runat ="server" ID ="Label1" Text ="Số hợp đồng" ></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContractNo" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>--%>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbContractNo" Text="Hợp đồng hiện tại"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContractNo" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbContractEffectDate" Text="Từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractEffectDate" runat="server" Enabled="False">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbContractExpireDate" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractExpireDate" runat="server" Enabled="False">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label2" Text="Ngày nghỉ việc"></asp:Label><span style="color: red"> *</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTerDate" runat="server" AutoPostBack="true" DateInput-CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdTerDate" runat="server"
                        ErrorMessage="Bạn phải nhập Ngày nghỉ việc." ToolTip="Bạn phải nhập Ngày nghỉ việc."> </asp:RequiredFieldValidator>

                    <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Ngày thôi việc phải lớn hơn ngày nộp đơn."
                        ToolTip="Ngày thôi việc phải lớn hơn ngày nộp đơn.">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Ngày thôi việc không nhỏ hơn ngày vào công ty."
                        ToolTip="Ngày thôi việc không nhỏ hơn ngày vào công ty.">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEffectDate" Text="Ngày hiệu lực QĐ nghỉ việc"> </asp:Label><span style="color: red"> *</span>

                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực." ToolTip="Bạn phải nhập ngày hiệu lực."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="display: none">
                    <asp:Label runat="server" ID="lbLastDate" Text="Ngày làm việc cuối cùng"></asp:Label>
                </td>
                <td  style="display: none">
                    <tlk:RadDatePicker ID="rdLastDate" runat="server" AutoPostBack="false" DateInput-CausesValidation="false">
                    </tlk:RadDatePicker>
                    <%--<asp:RequiredFieldValidator ID="reqLastDate" ControlToValidate="rdLastDate" runat="server"
                        ErrorMessage="Bạn phải nhập Ngày làm việc cuối cùng." ToolTip="Bạn phải nhập Ngày làm việc cuối cùng."> </asp:RequiredFieldValidator>--%>

                    <%--<asp:CustomValidator ID="cval_LastDate_SendDate" runat="server" ErrorMessage="Ngày làm việc cuối cùng phải lớn hơn ngày nghỉ việc."
                        ToolTip="Ngày làm việc cuối cùng phải lớn hơn ngày nghỉ việc.">
                    </asp:CustomValidator>--%>
                    <%--<asp:CustomValidator ID="cval_LastDate_JoinDate" runat="server" ErrorMessage="Ngày làm việc cuối cùng không nhỏ hơn ngày vào công ty."
                        ToolTip="Ngày làm việc cuối cùng không nhỏ hơn ngày vào công ty.">
                    </asp:CustomValidator>--%>
                </td>
            </tr>

            <tr style="visibility: hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin nghỉ việc")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSendDate" Text="Ngày nộp đơn"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSendDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CustomValidator ID="cvaldpSendDate" runat="server" ErrorMessage="Ngày nộp đơn không nhỏ hơn ngày vào công ty."
                        ToolTip="Ngày nộp đơn không nhỏ hơn ngày vào công ty.">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTerReason" Text="Loại nghỉ việc"></asp:Label>
                    <span style="color: red">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboTerReason" runat="server" Width="100%">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboTerReason"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại nghỉ việc. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại nghỉ việc %>"> </asp:RequiredFieldValidator>
                </td>
                <%--<td>
                    <tlk:RadDatePicker ID="rdApprovalDate" runat="server" AutoPostBack="true" DateInput-CausesValidation="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdApprovalDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày được phê duyệt nghỉ. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày được phê duyệt nghỉ %>"> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvaldpApproveDate" runat="server" ErrorMessage="<%$ Translate: Ngày được phê duyệt nghỉ phải lớn hơn ngày nộp đơn. %>"
                        ToolTip="<%$ Translate: Ngày được phê duyệt nghỉ phải lớn hơn ngày nộp đơn. %>">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvaldpApproveDateJoinDate" runat="server" ErrorMessage="<%$ Translate: Ngày phê duyệt không nhỏ hơn ngày vào công ty. %>"
                        ToolTip="<%$ Translate: Ngày phê duyệt không nhỏ hơn ngày vào công ty. %>">
                    </asp:CustomValidator>
                </td>--%>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTerReasonDetail" Text="Lý do nghỉ việc"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtTerReasonDetail" runat="server" SkinID="TextBox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label16" ></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="cbIsAllowForTer" Text="Tính trợ cấp thôi việc" runat="server" />
                </td>
                <td class="lb">
                     <asp:Label runat="server" ID="Label17" ></asp:Label>
               </td>
                <td>
                    <asp:CheckBox ID="chkIs_Job_loss_Allowance" Text="Tính trợ cấp mất việc" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lb"></td>
                <td>
                    <asp:CheckBox ID="cbIsBlackList" runat="server" Text="Danh sách đen" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbReason_BlackList" Visible="false" Text="Lý do danh sách đen"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtReason_BlackList" runat="server" Visible="false" SkinID="TextBox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            
            <tr style="display: none">
                <td class="lb">
                    <asp:Label runat="server" ID="Label3" Text="Truy thu BHYT"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTruyThu_BHYT" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboTerReason"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại nghỉ việc. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại nghỉ việc %>"> </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <%--<tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin bàn giao")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <tlk:RadGrid PageSize="50" ID="rgHandoverContent" runat="server" Height="200px" Width="90%" SkinID="GridNotPaging"
                        AllowMultiRowEdit="true">
                        <MasterTableView DataKeyNames="ID,TERMINATE_ID,IS_FINISH,CONTENT_ID,CONTENT_NAME,EMPLOYEE_ID" ClientDataKeyNames="ID,TERMINATE_ID,IS_FINISH,CONTENT_ID,CONTENT_NAME,EMPLOYEE_ID"
                            EditMode="InPlace">
                            <Columns>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung bàn giao %>" DataField="CONTENT_NAME"
                                    SortExpression="CONTENT_NAME" UniqueName="CONTENT_NAME" ReadOnly="true" />
                                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Hoàn thành %>" DataField="IS_FINISH"
                                    SortExpression="IS_FINISH" UniqueName="IS_FINISH">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridCheckBoxColumn>
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbRemark" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="TextBox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>--%>
            <%-- START HSV khong su dung cai nay  />--%>
            <tr style="display: none">
                <td colspan="6">
                    <b>
                        <%# Translate("Danh sách công nợ")%></b>
                    <hr />
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label runat="server" ID="lbDebtType" Text="Tên loại công nợ"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDebtType" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDebtMoney" Text="Số tiền"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtDebtMoney" MinValue="0" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDebtStatus" Text="Trạng thái"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDebtStatus" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <asp:Label runat="server" ID="lbDebtNote" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtDebtNote" runat="server" SkinID="TextBox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td colspan="6">
                    <tlk:RadGrid PageSize="50" ID="rgDebt" runat="server" Height="250px" Width="100%" SkinID="GridNotPaging"
                        AllowMultiRowEdit="true" AllowMultiRowSelection="false">
                        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="True">
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <MasterTableView EditMode="PopUp" DataKeyNames="DEBT_TYPE_ID,DEBT_TYPE_NAME,MONEY,ID,DEBT_STATUS,DEBT_STATUS_NAME,REMARK" ClientDataKeyNames="DEBT_TYPE_ID,DEBT_TYPE_NAME,MONEY,ID,DEBT_STATUS,DEBT_STATUS_NAME,REMARK" CommandItemDisplay="Top" AllowAutomaticInserts="true">
                            <CommandItemStyle Height="28px" />
                            <CommandItemTemplate>
                                <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnAddDebt" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                            CommandName="btnAddDebt"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Thêm %>" OnClientClicking="btnAddDebtsOnClientClicking">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: right">
                                        <tlk:RadButton ID="btnDeleteDebts" runat="server"
                                            CommandName="btnDeleteDebts"
                                            Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Xóa %>" OnClientClicking="btnDeleteDebtsOnClientClicking">
                                        </tlk:RadButton>
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên loại công nợ %>" DataField="DEBT_TYPE_NAME"
                                    SortExpression="DEBT_TYPE_NAME" UniqueName="DEBT_TYPE_NAME" ReadOnly="true" />
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="MONEY"
                                    SortExpression="MONEY" UniqueName="MONEY" DataFormatString="{0:n0}"
                                    ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="DEBT_STATUS_NAME"
                                    SortExpression="DEBT_STATUS_NAME" UniqueName="DEBT_STATUS_NAME" ReadOnly="true" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                    SortExpression="REMARK" UniqueName="REMARK" ReadOnly="true" />
                            </Columns>
                            <HeaderStyle Width="100px" />
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Phê duyệt")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDecisionType" Text="Loại quyết định"></asp:Label>
                    <span style="color: red">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDecisionType" AutoPostBack="true" CausesValidation="false" runat="server" Enabled="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDecisionType" ControlToValidate="cboDecisionType" runat="server"
                        ErrorMessage="Bạn phải chọn loại quyết định." ToolTip="Bạn phải chọn loại quyết định."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDecisionNo" Text="Số quyết định"></asp:Label><span style="color: red"> *</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqDecisionNo" ControlToValidate="txtDecisionNo" runat="server"
                        ErrorMessage="Bạn phải nhập số quyết định." ToolTip="Bạn phải nhập số quyết định."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Số thông báo"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNotifyNo" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbStatus" Text="Trạng thái"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqStatus" ControlToValidate="cboStatus" runat="server"
                        ErrorMessage="Bạn phải chọn trạng tháỉ phê duyệt." ToolTip="Bạn phải chọn trạng tháỉ phê duyệt."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSignDate" Text="Ngày ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label4" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" Enabled="true" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải tập tin%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSignerName" Text="Người ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerName" runat="server" ReadOnly="true" Width="130px" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSinger" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSignerTitle" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerTitle" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            xPos = $find("<%# RadSplitter3.ClientID%>")._element.control._element.scrollLeft;
            yPos = $find("<%# RadSplitter3.ClientID%>")._element.control._element.scrollTop;
        }
        function EndRequestHandler(sender, args) {
            $find("<%# RadSplitter3.ClientID%>")._element.control._element.scrollLeft = xPos;
            $find("<%# RadSplitter3.ClientID%>")._element.control._element.scrollTop = yPos;
        }
        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
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
            if (args.get_item().get_commandName() == "UNLOCK") {
                enableAjax = false;
            }

        }

        <%--function btnDeleteReasonClick(sender, args) {
            var bCheck = $find('<%# rgHandoverContent.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                args.set_cancel(true);
            }
        }--%>

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function btnDeleteDebtsOnClientClicking(sender, args) {

        }
        function btnAddDebtsOnClientClicking(sender, args) {
            var grid = $find("<%# rgDebt.ClientID%>");
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnKeyPress(sender, eventArgs) 
        { 
           var c = eventArgs.get_keyCode(); 
           if (c == 13) 
           { 
             document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";   
           }      
        } 
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_TerminateNewEdit_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
