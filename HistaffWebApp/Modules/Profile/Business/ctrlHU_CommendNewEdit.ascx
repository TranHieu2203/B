<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CommendNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_CommendNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidDecisionID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidSignID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<style type="text/css">
    .borderRight
    {
        border-right: 1px solid #C1C1C1;
    }


    #ctl00_MainContent_ctrlHU_CommendNewEdit_btnUploadFile .rbPrimaryIcon
    {
        top: 5px;
    }

    #ctl00_MainContent_ctrlHU_CommendNewEdit_btnDownload
    {
        display: inline-block;
        margin-top: -1px;
    }

    .RadGrid_Metro .rgCommandCell
    {
        padding-bottom: 3px;
    }
    #ctl00_MainContent_ctrlHU_CommendNewEdit_btnUploadFile_input {
        height: 20px !important;
    }
    #ctl00_MainContent_ctrlHU_CommendNewEdit_btnDownload .rbDecorated {
        height: 17px !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="20px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCommend" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RightPane" runat="server" Height="360px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td style="width: 150px;"></td>
                <td style="width: 150px;"></td>
                <td style="width: 150px;"></td>
                <td style="width: 150px;"></td>
                <td style="width: 150px;"></td>
                <td style="width: 150px;"></td>
                <td style="width: 50px;"></td>
                <td style="width: 50px;"></td>
                <td style="width: 80px;"></td>
                <td style="width: 150px;"></td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <asp:Label runat="server" ID="lbCommendInfo" Text="Thông tin khen thưởng"></asp:Label>
                    <hr />
                </td>
                <td class="borderRight"></td>
                <td></td>
                <td class="item-head" colspan="2">
                    <asp:Label runat="server" ID="DecisionInfo" Text="Thông tin quyết định"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Năm")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="false"
                        TabIndex="12">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEffectDate" Text="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server" AutoPostBack="true" TabIndex="3">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực." ToolTip="Bạn phải nhập ngày hiệu lực."> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbYear" Text="Năm"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtYear" runat="server" TabIndex="3">
                        <ClientEvents OnKeyPress="keyPress" />
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtYear"
                        runat="server" ErrorMessage="Bạn phải nhập năm khen thưởng" ToolTip="Bạn phải nhập năm khen thưởng"> </asp:RequiredFieldValidator>
                </td>
                <td class="borderRight"></td>
                <td></td>
                <td>
                    <asp:Label runat="server" ID="lbDecisionNo" Text="Số quyết định"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server" TabIndex="3" AutoPostBack="false">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqDecisionNo" ControlToValidate="txtDecisionNo"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số quyết định %>" ToolTip="<%$ Translate: Bạn phải nhập Số quyết định %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbCommendObj" Text="Đối tượng"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCommendObj" runat="server" AutoPostBack="True" CausesValidation="False"
                        TabIndex="3">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalCommendObj" runat="server" ErrorMessage="Bạn phải nhập đối tượng."
                        ToolTip="Bạn phải nhập đối tượng."> </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbCommendType" Text="Hình thức khen thưởng"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCommendType" runat="server" CausesValidation="true" TabIndex="3" AutoPostBack="true">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_cboCommendType" ControlToValidate="cboCommendType"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Hình thức khen thưởng %>" ToolTip="<%$ Translate: Bạn phải chọn Hình thức khen thưởng %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbCommendPay" Text="Hình thức trả thưởng"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCommendPay" runat="server" CausesValidation="False" TabIndex="3">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusCommendPay" ControlToValidate="cboCommendPay" runat="server"
                        ErrorMessage="Hình thức trả thưởng không tồn tại hoặc đã ngừng áp dụng." ToolTip="Hình thức trả thưởng không tồn tại hoặc đã ngừng áp dụng.">
                    </asp:CustomValidator>
                </td>
                <td class="borderRight"></td>
                <td></td>
                <td>
                    <asp:Label runat="server" ID="lbSignDate" Text="Ngày ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server" TabIndex="3" AutoPostBack="true">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdEffectDate" runat="server"
                        Type="Date" ControlToCompare="rdSignDate" Operator="GreaterThanEqual" ErrorMessage="Ngày hiệu lực phải lớn hơn Ngày ký"
                        ToolTip="Ngày hiệu lực phải lớn hơn Ngày ký"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbPeriod" Text="Kỳ lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriod" runat="server" CausesValidation="False" TabIndex="3">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTAX" Text="Tính Thuế"></asp:Label>
                </td>
                <td>
                    <tlk:RadButton ToggleType="CheckBox" runat="server" ID="chkTAX" ButtonType="ToggleButton"
                        CausesValidation="false" AutoPostBack="true" TabIndex="3">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbPeriodTax" Text="Kỳ lương tính thuế"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriodTax" runat="server" CausesValidation="False" TabIndex="3">
                    </tlk:RadComboBox>
                </td>
                <td class="borderRight"></td>
                <td></td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbCommend_Title" Text="Danh hiệu khen thưởng"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCommend_Title" runat="server" TabIndex="3">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboCommend_Title"
                        runat="server" ErrorMessage="Bạn phải chọn Danh hiệu khen thưởng" ToolTip="Bạn phải chọn Danh hiệu khen thưởng"> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusCommend_Title" ControlToValidate="cboCommend_Title" runat="server"
                        ErrorMessage="Danh hiệu khen thưởng không tồn tại hoặc đã ngừng áp dụng." ToolTip="Danh hiệu khen thưởng không tồn tại hoặc đã ngừng áp dụng.">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbCommendLevel" Text="Cấp khen thưởng"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCommendLevel" runat="server" CausesValidation="False" TabIndex="3">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboCommendLevel"
                        runat="server" ErrorMessage="Bạn phải chọn Cấp khen thưởng" ToolTip="Bạn phải chọn Cấp khen thưởng"> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusCommendLevel" ControlToValidate="cboCommendLevel" runat="server"
                        ErrorMessage="Cấp khen thưởng không tồn tại hoặc đã ngừng áp dụng." ToolTip="Cấp khen thưởng không tồn tại hoặc đã ngừng áp dụng.">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbMoney" Text="Mức thưởng"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtMoney" runat="server" TabIndex="3">
                        <NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>
                    </tlk:RadNumericTextBox>
                    <asp:CustomValidator ID="cvalMoney" runat="server" ErrorMessage="Bạn phải nhập số tiền phải >= 0."
                        ToolTip="Bạn phải nhập số tiền phải >= 0.">
                    </asp:CustomValidator>
                </td>
                <td class="borderRight"></td>
                <td></td>
                <td>
                    <asp:Label runat="server" ID="lbStatus" Text="Trạng thái"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server" TabIndex="3">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="cboStatus"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn trạng thái %>" ToolTip="<%$ Translate: Bạn phải chọn trạng thái %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbRemark" Text="Nội dung khen thưởng"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="100%" TabIndex="3">
                    </tlk:RadTextBox>
                </td>
                <td class="borderRight"></td>
                <td></td>
                <td>
                    <asp:Label runat="server" ID="lbSignerName1" Text="Người Ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerName" Width="130px" runat="server" ReadOnly="true" TabIndex="3">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSinger" runat="server" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <asp:Label runat="server" ID="lbCommend_Detail" Text="Lý do khen thưởng"></asp:Label>
                </td>
                <td colspan="5" style="display: none">
                    <tlk:RadTextBox ID="txtCommend_Detail" runat="server" TextMode="MultiLine" Width="100%"
                        TabIndex="3">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbUploadFile" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="Tải xuống" CausesValidation="false"
                        OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
                <td class="borderRight"></td>
                <td></td>
                <td>
                    <asp:Label runat="server" ID="lbSignerTitle" Text="Vị trí công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignerTitle" runat="server" TabIndex="3" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="visibility: hidden">
                <td class="lb">
                    <asp:Label runat="server" ID="lbCommendContent" Text="Nội dung khen thưởng"></asp:Label>
                </td>
                <td colspan="7">
                    <tlk:RadTextBox ID="RadTextBox1" runat="server" TextMode="MultiLine" Width="100%"
                        TabIndex="3">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
        <tlk:RadGrid ID="rgEmployee" AllowPaging="false" AllowMultiRowEdit="true" runat="server" Height="390px">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" EditFormSettings-EditFormType="AutoGenerated" AllowPaging="false" AllowCustomPaging="false"
                DataKeyNames="HU_COMMEND_ID,HU_EMPLOYEE_ID,EMPLOYEE_CODE,ORG_ID,TITLE_ID,FULLNAME,TITLE_NAME,ORG_NAME,COMMEND_PAY,MONEY,COMMEND_PAY_NAME,GUID_ID,NO"
                ClientDataKeyNames="HU_COMMEND_ID,HU_EMPLOYEE_ID,EMPLOYEE_CODE,ORG_ID,TITLE_ID,FULLNAME,TITLE_NAME,ORG_NAME,COMMEND_PAY,MONEY,COMMEND_PAY_NAME,GUID_ID,NO"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="120px" ID="btnEmployee" runat="server" Text="Chọn nhân viên"
                                CausesValidation="false" CommandName="FindEmployee" TabIndex="3">
                            </tlk:RadButton>
                            <tlk:RadButton Width="120px" ID="btnExport" runat="server" Text="Xuất file mẫu" OnClientClicked="btnExportClicking"
                                CausesValidation="false" CommandName="Export" TabIndex="3" >
                            </tlk:RadButton>
                            <tlk:RadButton Width="120px" ID="btnImport" runat="server" Text="Nhập file mẫu"
                                CausesValidation="false" CommandName="Import" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmp" runat="server" Text="Xóa" CausesValidation="false"
                                CommandName="DeleteEmployee" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="GUID_ID" UniqueName="GUID_ID" SortExpression="GUID_ID"
                        Visible="false" ReadOnly="true" ColumnGroupName="WorkingOLD" />
                    <tlk:GridBoundColumn DataField="HU_EMPLOYEE_ID" UniqueName="HU_EMPLOYEE_ID" SortExpression="HU_EMPLOYEE_ID"
                        Visible="false" ReadOnly="true" ColumnGroupName="WorkingOLD" />
                    <tlk:GridBoundColumn DataField="HU_COMMEND_ID" UniqueName="HU_COMMEND_ID" SortExpression="HU_COMMEND_ID"
                        Visible="false" ReadOnly="true" ColumnGroupName="WorkingOLD" />
                    <tlk:GridBoundColumn DataField="ORG_ID" UniqueName="ORG_ID" SortExpression="ORG_ID"
                        Visible="false" ReadOnly="true" ColumnGroupName="WorkingOLD" />
                    <tlk:GridBoundColumn DataField="TITLE_ID" UniqueName="TITLE_ID" SortExpression="TITLE_ID"
                        Visible="false" ReadOnly="true" ColumnGroupName="WorkingOLD" />
                    <tlk:GridBoundColumn HeaderText="MSNV" DataField="EMPLOYEE_CODE" HeaderStyle-Width="100px"
                        ReadOnly="true" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="FULLNAME" UniqueName="FULLNAME"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="FULLNAME" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        HeaderStyle-Width="200px" ReadOnly="true" SortExpression="TITLE_NAME" />
                    <tlk:GridTemplateColumn HeaderText="Hình thức trả thưởng" HeaderStyle-Width="100px"
                        UniqueName="COMMEND_PAY">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "COMMEND_PAY_NAME")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <tlk:RadComboBox Width="181px" runat="server" ID="cbCommend_Pay" AutoPostBack="true" CausesValidation="false"
                                OnSelectedIndexChanged="cbCommend_Pay_SelectedIndexChanged">
                            </tlk:RadComboBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Mức thưởng" HeaderStyle-Width="100px" UniqueName="MONEY" DataField="MONEY"
                        ColumnGroupName="WorkingNEW">
                        <EditItemTemplate>
                            <tlk:RadNumericTextBox Width="130px" runat="server"
                                ID="rnMONEY" AutoPostBack="true" OnTextChanged="rnMONEY_TextChanged">
                            </tlk:RadNumericTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Ghi chú" HeaderStyle-Width="100px" UniqueName="NO" DataField="NO">
                        <EditItemTemplate>
                            <tlk:RadTextBox Width="160px" runat="server" ID="txtNote" AutoPostBack="true" OnTextChanged="txtNote_TextChanged">
                            </tlk:RadTextBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
        <tlk:RadGrid ID="rgOrg" AllowPaging="false" AllowMultiRowEdit="true" runat="server"
            Visible="false" Height="390px">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="false" AllowCustomPaging="false"
                DataKeyNames="ID,HU_COMMEND_ID,ORG_NAME,ORG_ID,COMMEND_PAY,MONEY,COMMEND_PAY_NAME,GUID_ID"
                ClientDataKeyNames="ID,HU_COMMEND_ID,ORG_NAME,ORG_ID,COMMEND_PAY,MONEY,COMMEND_PAY_NAME,GUID_ID"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnOrg" runat="server" Text="Chọn phòng ban" CausesValidation="false"
                                CommandName="FindOrg">
                            </tlk:RadButton>
                            <%--<tlk:RadButton Width="150px" ID="btnOrgImport" runat="server" Text="Chọn phòng ban từ import"
                                CausesValidation="false" CommandName="FindOrgImport">
                            </tlk:RadButton>--%>
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="100px" ID="btnDeleteOrg" runat="server" Text="Xóa" CausesValidation="false"
                                CommandName="DeleteOrg">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="GUID_ID" UniqueName="GUID_ID" SortExpression="GUID_ID"
                        Visible="false" />
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="HU_COMMEND_ID" UniqueName="HU_COMMEND_ID" SortExpression="HU_COMMEND_ID"
                        Visible="false" ReadOnly="true" ColumnGroupName="WorkingOLD" />
                    <tlk:GridBoundColumn DataField="ORG_ID" UniqueName="ORG_ID" SortExpression="ORG_ID"
                        Visible="false" ReadOnly="true" ColumnGroupName="WorkingOLD" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" HeaderStyle-Width="200px"
                        ReadOnly="true" UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                    <tlk:GridTemplateColumn HeaderText="Hình thức trả thưởng" HeaderStyle-Width="200px"
                        UniqueName="COMMEND_PAY">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "COMMEND_PAY_NAME")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <tlk:RadComboBox runat="server" ID="cbCommend_PayORG" AutoPostBack="true"
                                OnSelectedIndexChanged="cbCommend_PayORG_SelectedIndexChanged">
                            </tlk:RadComboBox>
                        </EditItemTemplate>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="Mức thưởng" HeaderStyle-Width="150px" UniqueName="MONEY"
                        ColumnGroupName="WorkingNEW">
                        <EditItemTemplate>
                            <tlk:RadNumericTextBox SkinID="Money" DataFormatString="{0:N2}" Width="150px" runat="server"
                                ID="rnMONEY_ORG" AutoPostBack="true" OnTextChanged="rnMONEY_ORG_TextChanged">
                            </tlk:RadNumericTextBox>
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
</tlk:RadSplitter>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<common:ctrlupload id="ctrlUpload2" runat="server" />
<common:ctrlupload id="ctrlUpload" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindEmployeeImport" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindOrgImport" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }
        //        $(document).ready(function () {
        //            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_CommendNewEdit_RadPane2');
        //        });

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
        }
        function btnExportClicking(sender, args) {
            enableAjax = false;
        }

        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>
