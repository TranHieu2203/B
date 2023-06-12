<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_Request_PortalNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlRC_Request_PortalNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />

<style type="text/css">
    /*.ages
    {
        width: 39% !important;
        float: left;
    }
    .LevelLanguage
    {
        width: 44% !important;
        float: left;
    }
    .ages span
    {
        width: 100% !important;
    }
    .LevelLanguage div
    {
        width: 100% !important;
    }*/
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="680px" Orientation="Horizontal" Scrolling="None">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" >
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Width="100%" Height="100%">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <tlk:RadMultiPage ID="mitiple" runat="server" Width="100%">
            <tlk:RadPageView ID="rpvID" runat="server" Selected="true">
                <fieldset>
                    <legend>
                        <%# Translate("Thông tin chung")%>
                    </legend>
                    <table class="table-form">
                         <tr>
                            <td class="lb">
                                <%# Translate("Mã YCTD")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtCode_YCTD" runat="server" ReadOnly="true">
                                </tlk:RadTextBox>            
                            </td>
                            <td class="lb">
                                <%# Translate("Tên YCTD")%>
                            </td>
                            <td colspan="3">
                                <tlk:RadTextBox ID="txtName_YCTD" runat="server" Width="100%" ReadOnly="true">
                                </tlk:RadTextBox>     
                            </td>                            
                        </tr>
                         <tr>
                            <td class="lb">
                                <%# Translate("Trong định biên")%>
                            </td>
                            <td>
                                <asp:RadioButton ID="chkIn" runat="server"  AutoPostBack="true" CausesValidation="false" GroupName="INOUT"/>           
                            </td>
                            <td class="lb">
                                <%# Translate("Ngoài định biên")%>
                            </td>
                            <td>
                                <asp:RadioButton ID="chkOut" runat="server" AutoPostBack="true" CausesValidation="false" GroupName="INOUT"/>                                 
                            </td>                            
                        </tr>
                        
                        <tr>
                            <td class="lb">
                                <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" AutoPostBack="true">
                                    <ClientEvents OnKeyPress="OnKeyPress" />
                                </tlk:RadTextBox>
                                <tlk:RadButton ID="btnFindOrg" runat="server" SkinID="ButtonView" CausesValidation="false" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOrgName"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập phòng ban %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="lb" style="width: 150px">
                                <%# Translate("Vị trí tuyển dụng")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboTitle" runat="server" AutoPostBack="true" CausesValidation="false">
                                </tlk:RadComboBox>
                                <asp:CustomValidator ID="cusTitle" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn chức danh %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn chức danh %>" ClientValidationFunction="cusTitle">
                                </asp:CustomValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Nhóm vị trí TD")%>
                            </td>
                            <td >                              
                                <tlk:RadTextBox ID="txtGroupPos" runat="server" ReadOnly="True" Width="130px">
                                </tlk:RadTextBox>
                            </td>

                            <td class="lb" style="display: none">
                                <%# Translate("Địa điểm làm việc")%>
                            </td>
                            <td style="display: none">
                                <tlk:RadComboBox runat="server" ID="cbolocationWork">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                           <td class="lb">
                                <%# Translate("Người yêu cầu")%><span class="lbReq">*</span>
                           </td>                         
                           <td>
                                <tlk:RadTextBox ID="txtPersonRequest" runat="server" ReadOnly="True" Width="130px">
                                </tlk:RadTextBox>
                                <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindEmployee" runat="server" SkinID="ButtonView"
                                    CausesValidation="false" Enabled="false">
                                </tlk:RadButton>  
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtPersonRequest"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Người yêu cầu %>" ToolTip="<%$ Translate: Bạn phải chọn Người yêu cầu %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày gửi yêu cầu")%><span class="lbReq">*</span>
                           </td>   
                            <td>
                                <tlk:RadDatePicker ID="rdSendDate" runat="server" AutoPostBack="true" CausesValidation="false" DataFormatString="{0:dd/MM/yyyy}" >
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdSendDate"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày gửi kế hoạch %>"
                                    ToolTip="<%$ Translate: Bạn phải nhập Ngày gửi kế hoạch %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày cần đáp ứng")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdExpectedJoinDate" runat="server">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdExpectedJoinDate"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày đi làm dự kiến %>"
                                    ToolTip="<%$ Translate: Bạn phải nhập Ngày đi làm dự kiến %>"> 
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="rdExpectedJoinDate"
                                    ControlToCompare="rdSendDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày đi làm dự kiến phải lớn hơn Ngày gửi yêu cầu %>"
                                    ToolTip="<%$ Translate: Ngày đi làm dự kiến phải lớn hơn Ngày gửi yêu cầu %>"></asp:CompareValidator>
                            </td>
                            <td>
                            </td>
                            <td style="display: none">
                                <asp:CheckBox ID="chkIsSupport" runat="server" Text="<%$ Translate: TNG hỗ trợ triển khai %>" />
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Hình thức tuyển dụng")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboContractType" runat="server">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Nơi làm việc")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboWorkingPlace" runat="server">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Mức lương")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rntxtSalary" runat="server" AutoPostBack="true" SkinID="Money">
                                </tlk:RadNumericTextBox>     
                            </td>        

                            <td class="lb"  style="display: none">
                                <%# Translate("Tính chất tuyển dụng")%>
                            </td>
                            <td style="display: none">
                                <tlk:RadComboBox ID="cboRecruitProperty" runat="server">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="lb">
                                <%# Translate("Số lượng hiện có")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="txtCurrentNumber" ReadOnly="true" runat="server">
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Số lượng định biên")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="txtPayrollLimit" ReadOnly="true" runat="server">
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Số lượng còn lại")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="txtDifferenceNumber" ReadOnly="true" runat="server">
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="lb">
                                <%# Translate("Lý do tuyển dụng")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboRecruitReason" runat="server" AutoPostBack="true" CausesValidation="false">
                                </tlk:RadComboBox>
                                <asp:CustomValidator ID="cusRecruitReason" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Lý do tuyển dụng%>"
                                    ToolTip="<%$ Translate: Bạn phải chọn Lý do tuyển dụng %>" ClientValidationFunction="cusRecruitReason">
                                </asp:CustomValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Số lượng cần tuyển")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rntxtRecruitNumber" runat="server" NumberFormat-DecimalDigits="1"
                                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0"
                                    MaxValue="100" AutoPostBack="true">
                                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                                </tlk:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rntxtRecruitNumber"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số lượng cần tuyển %>"
                                    ToolTip="<%$ Translate: Bạn phải nhập Số lượng cần tuyển %>"> 
                                </asp:RequiredFieldValidator>                              
                            </td>
                            <td>
                            </td>
                            <td style="width: 150px">
                                <asp:CheckBox ID="chkIsOver" runat="server" Text="<%$ Translate: Vượt định biên %>" AutoPostBack="true"
                                    Checked="false" />
                            </td>
                        </tr>

                        <tr id="TuyenThayThe" runat="server" CausesValidation="false">     
                            <td></td>
                            <td colspan="4">
                                <asp:Label runat="server" ID="lblMessage" Text="Danh sách nhân viên tuyển thay thế" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr id="RgTuyenThayThe" runat="server" >
                            <td></td>                          
                            <td rowspan="4" colspan="5">
                                <tlk:RadGrid ID="rgEmployee" AllowPaging="false"  runat="server" EditMode="InPlace"  Height="205px" Width="765px">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView  AllowPaging="false" AllowCustomPaging="false" DataKeyNames="ID"  ClientDataKeyNames="" CommandItemDisplay="Top" >                                       
                                    <CommandItemStyle Height="25px" />
                                    <CommandItemTemplate >
                                      <div style="padding: 2px 0 0 0">
                                        <div style="float: left">
                                              <tlk:RadButton Width="72px" ID="btnEmployee" runat="server" Text="Thêm" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                                CausesValidation="false" CommandName="FindEmployee" TabIndex="3">
                                                </tlk:RadButton>
                                        </div>
                                        <div style="float: right">
                                            <tlk:RadButton Width="70px" ID="btnDeleteEmp" runat="server" Text="Xóa" CausesValidation="false" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                                CommandName="DeleteEmployee" TabIndex="3">
                                            </tlk:RadButton>
                                        </div>
                                      </div>
                                    </CommandItemTemplate>
                                  <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                                        ReadOnly="true" />
                                    <tlk:GridBoundColumn HeaderText="Mã NV" DataField="EMPLOYEE_CODE" HeaderStyle-Width="100px"
                                        ReadOnly="true" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="Họ tên" DataField="FULLNAME_VN" UniqueName="FULLNAME_VN"
                                        HeaderStyle-Width="150px" ReadOnly="true" SortExpression="FULLNAME_VN" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridDateTimeColumn HeaderText="Ngày vào công ty" DataField="JOIN_DATE"  UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE" HeaderStyle-Width="150px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </tlk:GridDateTimeColumn>
                                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                        HeaderStyle-Width="150px" ReadOnly="true" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Center" />
                                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME_VN" UniqueName="TITLE_NAME_VN"
                                        HeaderStyle-Width="150px" ReadOnly="true" SortExpression="TITLE_NAME_VN" ItemStyle-HorizontalAlign="Center" />                  
                                </Columns>
                                </MasterTableView>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ClientSettings>
                                       <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </tlk:RadGrid>
                             </td>
                        </tr>
                        <tr>
                           <td></td>    
                        </tr>
                        <tr>
                           <td></td>  
                        </tr>
                        <tr>
                           <td></td>    
                        </tr>
                        <tr>
                           <td></td>  
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Yêu cầu máy tính")%>
                            </td>
                            <td >
                                <asp:CheckBox ID="chkRequestComputer" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td class="lb">
                                <%# Translate("Diễn giải chi tiết")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtRecruitReason" runat="server" TextMode="MultiLine" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>
                            </td>
                            <td class="lb">                                
                                <%# Translate("Chọn người được thay thế")%>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>
                            </td>
                            <td colspan="5">
                                <tlk:RadListBox ID="lstEmployee" runat="server" Width="100%" Height="100px">
                                </tlk:RadListBox>
                            </td>
                        </tr>                     
                    </table>
                </fieldset>
                <fieldset>
                    <legend>
                        <%# Translate("Chi tiết yêu cầu tuyển dụng")%>
                    </legend>
                    <table class="table-form">
                    <tr>
                            <td class="lb">
                                <%# Translate("Trình độ học vấn")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboLearningLevel" runat="server">
                                </tlk:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboLearningLevel"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Trình độ học vấn %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn Trình độ học vấn %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="lb">
                                <%# Translate("Trình độ chuyên môn")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboQualification" runat="server">
                                </tlk:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="cboQualification"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nghiệp vụ chuyên môn %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn Nghiệp vụ chuyên môn %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Trình độ chuyên môn khác")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtQualificationOthers" runat="server" TextMode="MultiLine" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Độ tuổi từ")%><span class="lbReq"></span>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rntxtAgeFrom" runat="server" NumberFormat-DecimalDigits="1"
                                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="18"
                                    MaxValue="100">
                                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                                </tlk:RadNumericTextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rntxtAgeFrom"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Độ tuổi từ %>" ToolTip="<%$ Translate: Bạn phải nhập Độ tuổi từ %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Độ tuổi đến")%><span class="lbReq"></span>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rntxtAgeTo" runat="server" NumberFormat-DecimalDigits="1"
                                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="18"
                                    MaxValue="100">
                                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                                </tlk:RadNumericTextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="rntxtAgeTo"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Độ tuổi đến %>" ToolTip="<%$ Translate: Bạn phải nhập Độ tuổi đến %>"> 
                                </asp:RequiredFieldValidator>--%>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rntxtAgeTo"
                                    ControlToCompare="rntxtAgeFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Độ tuổi đến phải lớn hơn Độ tuổi từ %>"
                                    ToolTip="<%$ Translate: Độ tuổi đến phải lớn hơn Độ tuổi từ %>"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Ngoại ngữ")%><br>
                            </td>
                            <td style="width: 1px">
                                <tlk:RadComboBox ID="cboLanguage" runat="server">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Trình độ ngoại ngữ")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboLanguageLevel" runat="server">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Điểm ngoại ngữ")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="txtScores" runat="server" SkinID="Decimal" AutoPostBack="false">
                                </tlk:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Khả năng ngoại ngữ")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtForeignAbility" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Số năm kinh nghiệm tối thiểu")%>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox ID="rntxtExperienceNumber" runat="server" NumberFormat-DecimalDigits="1"
                                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0"
                                    MaxValue="100">
                                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Ưu tiên giới tính")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboGenderPriority" runat="server">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="display: none">
                                <%# Translate("Trình độ tin học văn phòng")%>
                            </td>
                            <td style="display: none">
                                <tlk:RadComboBox ID="cboComputerLevel" runat="server">
                                </tlk:RadComboBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Trình độ tin học")%>
                            </td>
                            <td colspan="3">
                                <tlk:RadTextBox ID="txtComputerAppLevel" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Mô tả công việc")%><span class="lbReq">*</span>
                            </td>
                            <td colspan="5" >
                                <tlk:RadTextBox ID="txtDescription" runat="server" TextMode="MultiLine" SkinID="Textbox1023" Height="43px"  Width="100%">
                                </tlk:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtDescription"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Mô tả công việc %>"
                                    ToolTip="<%$ Translate: Bạn phải nhập Mô tả công việc %>">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="display: none">
                                <%# Translate("Yêu cầu chính")%>
                            </td>
                            <td colspan="5" style="display: none">
                                <tlk:RadTextBox ID="txtMainTask" runat="server" TextMode="MultiLine" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="lb" style="display: none;">
                                <%# Translate("Đính kèm mô tả")%>
                            </td>
                            <td style="display: none;">
                                <tlk:RadButton ID="btnUploadFileDescription" runat="server" Text="Tải lên" CausesValidation="false">
                                </tlk:RadButton>
                                <asp:HyperLink ID="hypFile" Visible="false" Target="_blank" runat="server"></asp:HyperLink>
                                <asp:HiddenField ID="hddFile" runat="server" />
                                <asp:LinkButton ID="btnDeleteFile" runat="server" Visible="false" CausesValidation="false"
                                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa tệp tin này');">Xóa</asp:LinkButton>
                            </td>
                            <td class="lb">
                                <%# Translate("Kỹ năng đặc biệt")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboSpecialSkills" runat="server">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Mức độ ưu tiên")%>
                            </td>
                            <td colspan="3">
                                <tlk:RadTextBox ID="txtRequestExperience" runat="server" TextMode="MultiLine" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Đính kèm tập tin")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                                </tlk:RadTextBox>
                                <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                                </tlk:RadTextBox>
                                <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false"
                                    TabIndex="3" />
                                <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                                    CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <%# Translate("Yêu cầu khác")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtRequestOther" runat="server" TextMode="MultiLine" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="lb">
                                <%# Translate("Ghi chú")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>    
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusContractType(oSrc, args) {
            var cbo = $find("<%# cboContractType.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusRecruitReason(oSrc, args) {
            var cbo = $find("<%# cboRecruitReason.ClientID %>");
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
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
        }

        function OnValueChanged(sender, args) {

        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }       

        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlRC_Request_PortalNewEdit_txtOrgName') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
         var winH;
        var winW;

         function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() - 210;
            winW = $(window).width() - 90;
            $("#ctl00_MainContent_ctrlRC_Request_PortalNewEdit_RadSplitter3").stop().animate({ height: winH, width: winW }, 0);
             $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRC_Request_PortalNewEdit_RadPane2").stop().animate({ height: winH-30 }, 0);

            
            Sys.Application.add_load(SizeToFitMain);
        }

        SizeToFitMain();

        $(document).ready(function () {
            SizeToFitMain();
        });
        $(window).resize(function () {
            SizeToFitMain();
        });
    </script>
</tlk:RadCodeBlock>
