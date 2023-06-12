<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGeneralSetting.ascx.vb"
    Inherits="Profile.ctrlGeneralSetting" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:Panel ID="radProbation" runat="server" CssClass="Pane" Width="100%">
            <fieldset>
                <legend>
                    <%# Translate("Thiết lập hồ sơ")%><span class="lbReq">*</span>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp" runat="server" Text="<%$ Translate: Lương cơ bản = Hệ số bậc lương * Mức lương vùng bảo hiểm%>"/>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtName_YCTD" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td>
                            <tlk:RadTextBox ID="RadTextBox1" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_20" runat="server" Text="<%$ Translate: Ẩn thông tin công ty (mặc định không check)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_1" runat="server" Text="<%$ Translate: Ngày bắt đầu = Ngày kết thúc HĐ kề trước + 1%>"/>
                        </td>
                         <td>
                            <tlk:RadTextBox ID="RadTextBox2" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td>
                            <tlk:RadTextBox ID="RadTextBox3" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_21" runat="server" Text="<%$ Translate: Ẩn thông tin PB cấp 1 (mặc định không check)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_2" runat="server" Text="<%$ Translate: Load data loại hợp đồng theo thiết lập quy trình hợp đồng%>"/>
                        </td>
                         <td>
                            <tlk:RadTextBox ID="RadTextBox4" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td>
                            <tlk:RadTextBox ID="RadTextBox5" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_22" runat="server" Text="<%$ Translate: Ẩn thông tin PB cấp 2 (mặc định không check)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chk_Hide_ManagerHeathIns" runat="server" Text="<%$ Translate: Ẩn thông tin Bảo hiểm sức khỏe (App,Portal)%>"/>
                        </td>
                         <td>
                            <tlk:RadTextBox ID="RadTextBox6" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td>
                            <tlk:RadTextBox ID="RadTextBox7" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_23" runat="server" Text="<%$ Translate: Ẩn thông tin PB cấp 3 (mặc định không check)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_3" runat="server" Text="<%$ Translate: Ngày QĐ nghỉ việc = Ngày nghỉ việc%>"/>
                        </td>
                         <td>
                            <tlk:RadTextBox ID="RadTextBox8" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td>
                            <tlk:RadTextBox ID="RadTextBox9" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_24" runat="server" Text="<%$ Translate: Ẩn thông tin PB cấp 4 (mặc định không check)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_4" runat="server" Text="<%$ Translate: Tính thêm niên theo HSV%>"/>
                        </td>
                         <td>
                            <tlk:RadTextBox ID="RadTextBox10" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td>
                            <tlk:RadTextBox ID="RadTextBox11" runat="server" Width="100%" ReadOnly="true" BorderWidth="0">
                                </tlk:RadTextBox>     
                        </td>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_25" runat="server" Text="<%$ Translate: Ẩn thông tin PB cấp 5 (mặc định không check)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_5" runat="server" Text="<%$ Translate: Bỏ bắt buộc nhập Quận/huyện nơi sinh%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_6" runat="server" Text="<%$ Translate: Bỏ bắt buộc nhập Phường/xã nơi sinh%>"/>
                        </td>
                    </tr>

                    <%--==============================================--%>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_7" runat="server" Text="<%$ Translate: Ẩn thông tin người ký - Chức danh người ký (Hồ sơ lương)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_8" runat="server" Text="<%$ Translate: Ẩn thông tin người ký - Chức danh người ký (Hợp đồng)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_9" runat="server" Text="<%$ Translate: Ẩn thông tin người ký - Chức danh người ký (Phụ lục HĐ)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_10" runat="server" Text="<%$ Translate: Ẩn thông tin người ký - Chức danh người ký (Quá trình công tác)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_11" runat="server" Text="<%$ Translate: Ẩn thông tin người ký - Chức danh người ký (Khen thưởng)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_12" runat="server" Text="<%$ Translate: Ẩn thông tin người ký - Chức danh người ký (Kỷ luật)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_13" runat="server" Text="<%$ Translate: Ẩn thông tin người ký - Chức danh người ký (Kiêm nhiệm)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_14" runat="server" Text="<%$ Translate: Ẩn thông tin người ký - Chức danh người ký (Nghỉ việc)%>"/>
                        </td>
                    </tr>

                     <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_15" runat="server" Text="<%$ Translate: Ẩn Loại hình lao động%>"/>
                        </td>
                    </tr>

                     <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_16" runat="server" Text="<%$ Translate: Bỏ bắt buộc nhập Loại hình lao động%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_17" runat="server" Text="<%$ Translate: Ẩn ngày tính thâm niên%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_18" runat="server" Text="<%$ Translate: Ẩn Thang, Ngạch, Bậc lương%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_19" runat="server" Text="<%$ Translate: Sao chép quá trình công tác qua Kinh nghiệm làm việc%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkApp_ISHide_Image" runat="server" Text="<%$ Translate: Ẩn không hiển thị hình ảnh nhân viên trên dưới Danh Sách nhân viên%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkConfigTitle" runat="server" Text="<%$ Translate: Bỏ qua thiết lập chức danh theo phòng ban%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chk_IsHide_ObjectLaborNew" runat="server" Text="<%$ Translate: Ẩn Loại hình lao động %>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkQLTT" runat="server" Text="<%$ Translate: Bỏ qua phân quyền QLTT tại hai màn hình Quá trình công tác và Quá trình công tác hàng loạt %>"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>

                    <%# Translate("Thiết lập tuyển dụng")%><span class="lbReq">*</span>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="ckh_RC_Over_Rank" runat="server" Text="<%$ Translate: Cảnh báo vượt rank lương theo vị trí định biên%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="ckh_RC_Sal_Budget_Exceeded" runat="server" Text="<%$ Translate: Cảnh báo vượt ngân sách lương theo bộ phận%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="ckh_PersonRe_TCTD" runat="server" Text="<%$ Translate: TBP phân công YCTD cho người phụ trách%>"/>
                        </td>
                    </tr>

                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkRC_ID_NO" runat="server" Text="<%$ Translate: Bắt buộc nhập số CMND%>"/>
                        </td>
                    </tr>

                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkRC_PER_MAIL" runat="server" Text="<%$ Translate: Bắt buộc nhập Email cá nhân%>"/>
                        </td>
                    </tr>

                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkRC_MOB_PHONE" runat="server" Text="<%$ Translate: Bắt buộc nhập Điện thoại di động%>"/>
                        </td>
                    </tr>

                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkRC_LITERACY" runat="server" Text="<%$ Translate: Bắt buộc nhập Trình độ học vấn%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkYCTDPERORG" runat="server" Text="<%$ Translate: YCTD Portal: Mặc định phân quyền thấy phòng ban theo User login%>"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
             <fieldset>
                <legend>

                    <%# Translate("Thiết lập hệ thống")%><span class="lbReq">*</span>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb9">
                            <asp:Label ID="Label1" runat="server" Text="Mã xác thực đăng ký User"></asp:Label>
                            <tlk:RadTextBox runat="server" ID="txtCodeXacThuc"  Width="260px" />
                            <%--<asp:CheckBox ID="CheckBox1" runat="server" Text="<%$ Translate: Cho phép chỉnh sửa thông tin portal (hồ sơ,nhân thân)%>"/>--%>
                        </td>
                        <td class="lb9" style="padding-left: 70px;">
                            <asp:CheckBox ID="chkIsOrgExpand" runat="server" Text="<%$ Translate: Org level expand %>"/>
                        </td>
                        <td class="lb9">
                            <tlk:RadNumericTextBox runat="server" ID="rnOrgExpandLevel"  Width="50px" />
                            <asp:Label ID="Label7" runat="server" Text="(Mặc định load cây SĐTC đến level đã thiết lập)"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:Label ID="Label2" runat="server" Text="Số lượng Client công ty hoặc chi nhánh"></asp:Label>
                            <tlk:RadNumericTextBox runat="server" ID="txtSetup_Num_Org"  Width="50px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chk_hide_AD_User" runat="server" Text="<%$ Translate: Ẩn thông tin tài khoản liên kết AD User%>"/>
                        </td>
                        </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chk_Hide_button_reset" runat="server" Text="<%$ Translate: Ẩn tính năng reset (nút reset)%>"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>

                    <%# Translate("Thiết lập Portal")%><span class="lbReq">*</span>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkPortalAllowChange" runat="server" Text="<%$ Translate: Cho phép chỉnh sửa thông tin portal (hồ sơ,nhân thân)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="Chk_hs_ot" runat="server" Text="<%$ Translate: Ẩn hệ số OT ngoại lệ khi đăng ký OT%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:Label ID="Label3" runat="server" Text="Tiêu đề mypage"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtNamePage"  Width="450px" SkinID="Textbox1023" Height="40px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:Label ID="Label4" runat="server" Text="Thông báo sắp hết thời gian bảo trì hệ thống"></asp:Label>
                        </td>    
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtMaintenanceComingEnd"  Width="450px" SkinID="Textbox1023" Height="40px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:Label ID="Label5" runat="server" Text="Thông báo hết thời gian bảo trì hệ thống"></asp:Label>
                        </td>  
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtMaintenanceEnd"  Width="450px" SkinID="Textbox1023" Height="40px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:Label ID="Label6" runat="server" Text="Thông báo màn hình login"></asp:Label>
                        </td>   
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtNotifyLogin"  Width="450px" SkinID="Textbox1023" Height="40px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkIsNotLoginMaintenance" runat="server" Text="<%$ Translate: Xử lý chặn login khi hết hạn bảo trì (trừ User ADMIN)%>"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
             <fieldset>
                <legend>

                    <%# Translate("Thiết lập Chấm công")%>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkAdvanceLeave" runat="server" Text="<%$ Translate: Số phép ứng tối đa     %>"/>
                            <tlk:RadNumericTextBox ID="ntxtAdvanceLeave" runat="server" SkinID ="Decimal" Width="40px"></tlk:RadNumericTextBox>
                        </td>
                         <td class="lb9">
                              <%# Translate("Số phép tạm ứng tối đa trong năm     ")%>
                            <tlk:RadNumericTextBox ID="ntxtAdvanceLeaveTemp" runat="server" SkinID ="Decimal" Width="40px"></tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkCal_SENIORITY_By_HSV" runat="server" Text="<%$ Translate: Tính thâm niên theo HSV%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chk_Is_Load_DirectMng" runat="server" Text="<%$ Translate: Xếp ca làm việc portal (Load danh sách nhân viên có QLTT là User login)%>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkIsAuto" runat="server" Text="<%$ Translate: Đăng ký đi trễ, về sớm: Tự động điền Giờ bắt đầu và Giờ kết thúc ca%>"/>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkWorkingLessEqualWorkingStandard" runat="server" Text="<%$ Translate: Xếp ca portal: Ràng buộc công làm việc <= công chuẩn%>"/>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkBoundShift" runat="server" Text="<%$ Translate: Xếp ca portal: Ràng buộc số ca OFF trong bảng xếp ca tháng%>"/>
                            <tlk:RadTextBox ID="txtCompareOperatorBoundShift" runat="server" Width="40px" />
                            <tlk:RadNumericTextBox ID="rntxtBoundShift" runat="server" SkinID ="Decimal" Width="40px"></tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkGovermentComOT" runat="server" Text="<%$ Translate: Tính OT nghỉ bù theo hệ số nhà nước %>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkPortalSignWork" runat="server" Text="<%$ Translate: Bỏ xử lý quy trình phê duyệt ca làm việc %>"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb9">
                            <asp:CheckBox ID="chkPortalSignWorkDMCA" runat="server" Text="<%$ Translate: Bỏ qua phân quyền ca làm việc theo phòng ban %>"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

    </script>
    <style>
        .Pane {
            width: 33%;
            float: left;
        }
      
    </style>
</tlk:RadCodeBlock>
