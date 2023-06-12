<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ConcurrentlyNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_ConcurrentlyNewEdit" %>
<style>
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_ConcurrentlyNewEdit_LeftPane{
        background: #f2f2f2;
    }
    .content-box{
        background: #ffffff;
        border:none !important;
        box-shadow: 0px 0px 6px rgba(0 0 0 / 15%);
    }
    html .RadGrid {
        background-color: #f2f2f2 !important;
        border: none;
    }
    .RadToolBar_Metro .rtbOuter,.brackcrum {
        background-color: #f2f2f2;
    }
    .item-head{
        text-align: center;
        color: #6a6a6a !important;
        margin-bottom: 13px;
    }
    /*div.RadToolBar .rtbUL{
        text-align: right !important;
    }*/
</style>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidEmpCode" runat="server" />
<asp:HiddenField ID="hidOrgName" runat="server" />
<asp:HiddenField ID="hdQuanLyTrucTiep" runat="server" />
<asp:HiddenField ID="hidOrgCOn" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidSign2" runat="server" />
<asp:HiddenField ID="hidOrgId" runat="server" />
<asp:HiddenField ID="hidSignStop" runat="server" />
<asp:HiddenField ID="hidSignStop2" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCon" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Width="100%">
        <div style="width: 100%">
            <div style="width: 50%; float: left;background-color: #f2f2f2;" class="content-wrapper">
                <fieldset class="content-box">
                    <p class="item-head" style="text-align: left;"><%# Translate("Thông tin chính")%></p> 
                    <table  style="height: 245px; width:100%;" class="table-form">
                        <tr>
                            <td style="text-align: left; width:15%;" class="lb" >
                                <%# Translate("Mã nhân viên")%>
                                <span class="lbReq">*</span>
                            </td>
                            <td style="width:35%;" class="control3">
                                <tlk:RadTextBox ID="txtEmpCode" Width="133px" runat="server"  AutoPostBack="true">
                                    <ClientEvents OnKeyPress="OnKeyPress" />
                                </tlk:RadTextBox>
                                <tlk:RadButton ID="btnFindEmp" runat="server" SkinID="ButtonView"
                                    CausesValidation="false" onkeydown="return (event.keyCode!=13)">
                                </tlk:RadButton>
                                <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmpCode" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                            </td>
                            <td class="lb" style="text-align: left; width:15%;">
                                <%# Translate("Đơn vị công tác")%>
                            </td>
                            <td style="width:35%;" class="control3">
                                <tlk:RadToolTip ID="EmpOrgNameToolTip" runat="server" Width="100" Height="10" Position="BottomRight" 
                                    Text='' TargetControlID="txtEmpOrgName">
                                </tlk:RadToolTip>
                                <tlk:RadTextBox ID="txtEmpOrgName" runat="server" ReadOnly="True" >
                                </tlk:RadTextBox>
                                <tlk:RadButton runat="server" ID="btnOrgId" Width="8px" SkinID="ButtonView" CausesValidation="false" Visible="false"/>
                            </td>
                        </tr>
                        <tr>
                          
                            <td style="text-align: left" class="lb">
                                <%# Translate("Họ và tên")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtEmpName" runat="server" ReadOnly="true">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Vị trí công việc")%>
                            </td>
                            <td>
                                
                                <%--<tlk:RadComboBox ID="cboTitleId" runat="server"
                                    CausesValidation="false">
                                </tlk:RadComboBox>--%>
                                 <tlk:RadTextBox ID="txtEmpTitle" runat="server" ReadOnly="True">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Cấp bậc (WL)")%>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboStaffRank" runat="server" Enabled="false" >
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset class="content-box">
                    <p class="item-head" style="text-align: left;"><%# Translate("Thông tin phê duyệt kiêm nhiệm")%></p>
                    <table onkeydown="return (event.keyCode!=13)" class="table-form" style="height: 245px">
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Số quyết định")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtCON_NO" runat="server">
                                </tlk:RadTextBox>
                                <asp:RequiredFieldValidator ID="reqCON_NO" ControlToValidate="txtCON_NO" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập Số quyết định. %>" ToolTip="<%$ Translate: Bạn phải nhập Số quyết định. %>"> 
                                </asp:RequiredFieldValidator>
                                <%--<asp:CustomValidator ID="curDecisionNo" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn số quyết định %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn số quyết định %>">
                                </asp:CustomValidator>
                                <asp:CustomValidator ID="CusDecisionNoSame" runat="server" ErrorMessage="<%$ Translate: Mã số bị trùng  %>"
                                    ToolTip="<%$ Translate: Mã số bị trùng %>">
                                </asp:CustomValidator>--%>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Ngày ký")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdSIGN_DATE" runat="server">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdSIGN_DATE" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải chọn Ngày ký. %>" ToolTip="<%$ Translate: Bạn phải chọn Ngày ký. %>"> 
                                </asp:RequiredFieldValidator>
                                <%-- <asp:CustomValidator ID="curStartDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Ngày ký %>"
                                    ToolTip="<%$ Translate: Bạn phải chọn Ngày ký %>">
                                </asp:CustomValidator>--%>
                            </td>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Trạng thái của quyết định")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboStatus" runat="server" CausesValidation="False">
                                </tlk:RadComboBox>
                                <asp:RequiredFieldValidator ID="reqStatus" ControlToValidate="cboStatus" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải chọn Trạng thái của quyết định. %>" ToolTip="<%$ Translate: Bạn phải chọn Trạng thái của quyết định. %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        
                        <tr style="display:none">
                            <td class="lb" style="text-align: left;">
                                <%# Translate("Người ký 2")%><%--<span class="lbReq">*</span>--%>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtSIGN2" ReadOnly="true" Width="128px" />
                                <tlk:RadButton runat="server" ID="btnSIGN2" SkinID="ButtonView" CausesValidation="false" />
                                <%-- <asp:RequiredFieldValidator ID="reqApplyName1" ControlToValidate="txtSIGN2" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập người phê duyệt. %>" ToolTip="<%$ Translate: Bạn phải nhập người phê duyệt. %>"> 
                                </asp:RequiredFieldValidator>--%>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Chức danh người ký 2")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSIGN_TITLE2" ReadOnly="True" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Ghi chú")%>
                            </td>
                            <td colspan="3">
                                <tlk:RadTextBox ID="txtRemark" runat="server" Width="100%">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="text-align: left;">
                                <asp:Label ID="Label1" runat="server" Text="Người ký"></asp:Label>
                                <%--<%# Translate("Người ký")%>--%>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtSIGN" ReadOnly="true" Width="128px"  />
                                <tlk:RadButton runat="server" ID="btnSIGN" SkinID="ButtonView" CausesValidation="false" />
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSIGN" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập người phê duyệt. %>" ToolTip="<%$ Translate: Bạn phải nhập người phê duyệt. %>"> 
                                </asp:RequiredFieldValidator>--%>
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                            </td>
                            <td class="lb" style="text-align: left">
                                <asp:Label ID="Label2" runat="server" Text="Vị trí công việc người ký"></asp:Label>
                                <%--<%# Translate("Chức danh người ký")%>--%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSIGN_TITLE" ReadOnly="True" runat="server">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div style="width: 50%; float: left;background-color: #f2f2f2;">
                <fieldset class="content-box">
                    <p class="item-head" style="text-align: left;"><%# Translate("Thông tin kiêm nhiệm")%></p> 
                    <table onkeydown="return (event.keyCode!=13)" class="table-form" style="height: 245px">
                        <tr>
                            <td style="text-align: left; width: 15%;" class="lb">
                                <%# Translate("Đơn vị kiêm nhiệm")%>
                                <span class="lbReq">*</span>
                            </td>
                            <td style="width: 35%;">
                                <tlk:RadToolTip ID="ORG_CONNameToolTip" runat="server" Width="100" Height="10" Position="BottomRight"
                                    Text='' TargetControlID="txtORG_CONName">
                                </tlk:RadToolTip>
                                <tlk:RadTextBox runat="server" ID="txtORG_CONName" Width="88%" ReadOnly="true" />
                                <tlk:RadButton runat="server" ID="btnORG_CON"  Width="10%" SkinID="ButtonView" CausesValidation="false" />
                                <%--<asp:RequiredFieldValidator ID="reqOrgName" ControlToValidate="txtORG_CONName" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập đơn vị kiêm nhiệm. %>" ToolTip="<%$ Translate: Bạn phải nhập đơn vị kiêm nhiệm. %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td style="text-align: left; width: 15%;" class="lb">
                                <%# Translate("Vị trí kiêm nhiệm")%>
                                <span class="lbReq">*</span>
                            </td>
                            <td style="width: 35%;">
                                <tlk:RadComboBox runat="server" ID="cboTITLE_CON" AutoPostBack="true" CausesValidation="false" Width="100%"
                                Filter="Contains"
                                HighlightTemplatedItems="true">
                                    <HeaderTemplate>
                                    <table style="width: 1070px" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 570px;">
                                                Tên vị trí
                                            </td>
                                            <td style="width: 250px;">
                                                Master
                                            </td>
                                                <td style="width: 250px;">
                                                Interim
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="width: 1070px" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 570px;">
                                                <%# Eval("NAME")%>
                                            </td>
                                            <td style="width: 250px;">
                                                <%# Eval("MASTER_NAME")%>
                                            </td>
                                            <td style="width: 250px;">
                                                <%# Eval("INTERIM_NAME")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="lb" style="text-align: left;">
                                <asp:Label runat="server" ID="lbJobLevel" Text="Cấp bậc (WL)"></asp:Label><span class="lbReq">*</span>
                            </td>
                            <td class="control3">
                                <tlk:RadComboBox runat="server" ID="cboJobLevel" Width="100%">
                                </tlk:RadComboBox>
                                <%--<asp:RequiredFieldValidator ID="reqJobLevel" ControlToValidate="cboJobLevel"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Cấp bậc (WL) %>" ToolTip="<%$ Translate: Bạn phải chọn Cấp bậc (WL) %>"> </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb3">
                                <asp:Label runat="server" ID="lbDirectManager" Text="Vị trí QLTT"></asp:Label>
                                <span class="lbReq"></span>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server"  Width="100%" ID="txtDirectManagerPosition" ReadOnly="true" />
                            </td>
                            <td class="lb3">
                                <asp:Label runat="server" ID="lbmanager" Text="Quản lý trực tiếp"></asp:Label>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server"  Width="100%" ID="txtDirectManager" ReadOnly="true"></tlk:RadTextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="lb">
                                <%# Translate("Quản lý trực tiếp tại đơn vị kiêm nhiệm") %>
                                <span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtQuanLyTrucTiep" Width="130px" ReadOnly="true" />
                                <tlk:RadButton runat="server" ID="btnQuanLyTrucTiep" SkinID="ButtonView" CausesValidation="false" />
                                <%--<asp:RequiredFieldValidator ID="rqQuanLyTrucTiep" ControlToValidate="txtQuanLyTrucTiep" runat="server"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập quản lý trực tiếp tại đơn vị kiêm nhiệm. %>" 
                                    ToolTip="<%$ Translate: Bạn phải nhập quản lý trực tiếp tại đơn vị kiêm nhiệm. %>"> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="lb">
                                <%# Translate("Chức danh QLTT") %>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtChucDanhQLTT" Width="130px" ReadOnly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td style="display: none">
                                <%# Translate("Phụ cấp kiêm nhiệm")%>
                            </td>
                            <td style="display: none">
                                <tlk:RadNumericTextBox ID="rntxtALLOW_MONEY" MinValue="0" runat="server">
                                    <NumberFormat DecimalDigits="0" ZeroPattern="n" />
                                </tlk:RadNumericTextBox>
                            </td>
                            <td></td>
                            <td style="display: none">
                                <asp:CheckBox ID="chkALLOW" runat="server" Text="<%$ Translate: Đơn vị kiêm nhiệm trả phụ cấp? %>"
                                    AutoPostBack="false" />
                            </td>
                        </tr>
                        <tr >
                            <td style="text-align: left" class="lb">
                                <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdEFFECT_DATE_CON" Width="100%" runat="server" AutoPostBack="true">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdEFFECT_DATE_CON"
                                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>"
                                    ToolTip="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>"> </asp:RequiredFieldValidator>
                            </td>
                            <%--<td style="text-align: left" class="lb">
                                <%# Translate("Ngày hết hiệu lực")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdEXPIRE_DATE_CON" Width="100%" runat="server">
                                </tlk:RadDatePicker>
                                <asp:CustomValidator ID="cval_EffectDate_ExpireDate" runat="server" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực. %>"
                                    ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực. %>">
                                </asp:CustomValidator>
                            </td>--%>
                        </tr>
                        <tr>
                            <%--<td style="text-align: left; display: none;" class="lb">
                                <%# Translate("Chức vụ kiêm nhiệm")%>
                            </td>
                            <td colspan="3" style="display: none">
                                <tlk:RadComboBox ID="cboChucVu" runat="server"  Width="100%" AutoPostBack ="true"
                                    CausesValidation="false">
                                </tlk:RadComboBox>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="text-align: left; display: none;" class="lb">
                                <%# Translate("Số tiền")%><span class="lbReq">*</span>
                            </td>
                            <td style="display: none;">
                                <tlk:RadNumericTextBox ID="rdMonney" MinValue="0" runat="server"  SkinID="Money">
                                </tlk:RadNumericTextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdMonney"
                                    runat="server" ErrorMessage="Bạn phải nhập Số tiền " ToolTip="Bạn phải nhập Số tiền "> 
                                </asp:RequiredFieldValidator>--%>
                            </td>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Tập tin đính kèm")%>
                            </td>
                            <td>
                                <div>
                                    <tlk:RadTextBox ID="txtUploadFile" Width="100%" runat="server" ReadOnly="true">
                                    </tlk:RadTextBox>
                                    <tlk:RadTextBox ID="txtUploadFile_Link" runat="server" Visible="false" Width ="80%"></tlk:RadTextBox>
                                </div>
                            </td>
                            <td colspan="2">
                                <tlk:RadButton runat="server" ID="btnUploadFile" Text="<%$ Translate: Đăng %>"
                                    CausesValidation="false" />
                                <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải %>"
                                    CausesValidation="false" OnClientClicked="rbtClicked">
                                </tlk:RadButton>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsChuyen" runat="server" Text="<%$ Translate: Tạo nhân viên mới %>"
                                    AutoPostBack="false" Style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="lb">
                                 <%# Translate("Diễn giải")%>
                            </td>
                            <td colspan="5">
                                <tlk:RadTextBox ID="txtBasic" runat="server" SkinID="Textbox1023" Width="100%">
                                </tlk:RadTextBox>
                             </td>
                        </tr>
                        <tr></tr>
                    </table>
                </fieldset>
                <fieldset class="content-box">
                    <p class="item-head" style="text-align: left;"><%# Translate("Thông tin thôi kiêm nhiệm")%></p>
                    <table onkeydown="return (event.keyCode!=13)" class="table-form" style="height: 245px">
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Số quyết định")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtConNoStop" runat="server" ReadOnly="True">
                                </tlk:RadTextBox>
                                <asp:RequiredFieldValidator ID="reqConNoStop" ControlToValidate="txtConNoStop" runat="server" Enabled="false"
                                    ErrorMessage="<%$ Translate: Bạn phải nhập Số quyết định. %>" ToolTip="<%$ Translate: Bạn phải nhập Số quyết định. %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Ngày ký")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdSIGN_DATE_STOP" runat="server" Enabled="false">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator ID="reqSIGN_DATE_STOP" ControlToValidate="rdSIGN_DATE_STOP" runat="server" Enabled="false"
                                    ErrorMessage="<%$ Translate: Bạn phải chọn Ngày ký. %>" ToolTip="<%$ Translate: Bạn phải chọn Ngày ký. %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left" class="lb">
                                <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdEFFECT_DATE_STOP" runat="server" AutoPostBack="true" Enabled="false">
                                </tlk:RadDatePicker>
                                <asp:RequiredFieldValidator ID="reqEFFECT_DATE_STOP" ControlToValidate="rdEFFECT_DATE_STOP" runat="server" Enabled="false"
                                    ErrorMessage="<%$ Translate: Bạn phải chọn Ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải chọn Ngày hiệu lực. %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="lb" style="text-align: left">
                                <%# Translate("Trạng thái của quyết định")%><span class="lbReq">*</span>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboSTATUS_STOP" runat="server" CausesValidation="False" Enabled="false">
                                </tlk:RadComboBox>
                                <asp:RequiredFieldValidator ID="reqSTATUS_STOP" ControlToValidate="cboSTATUS_STOP" runat="server" Enabled="false"
                                    ErrorMessage="<%$ Translate: Bạn phải chọn Trạng thái của quyết định. %>" ToolTip="<%$ Translate: Bạn phải chọn Trạng thái của quyết định. %>"> 
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="text-align: left;">
                                <asp:Label ID="Label3" runat="server" Text="Người ký"></asp:Label>
                            </td>
                            <td>
                                <tlk:RadTextBox runat="server" ID="txtSIGN_ID_STOP" ReadOnly="true" Width="128px"  Enabled="false"/>
                                <tlk:RadButton runat="server" ID="btnSIGN_ID_STOP" SkinID="ButtonView" CausesValidation="false" Enabled="false"/>
                            </td>
                            <td class="lb" style="text-align: left">
                                <asp:Label ID="Label4" runat="server" Text="Vị trí công việc người ký"></asp:Label>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtSIGN_TITLE_STOP" ReadOnly="True" runat="server" Enabled="false">
                                </tlk:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </div>
        <div style="float: left; width: 100%">
            <tlk:RadGrid ID="rgConcurrently" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                Height="270px" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings AllowColumnsReorder="True" EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling UseStaticHeaders="true" AllowScroll="True" />
                    <Resizing AllowColumnResize="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,STATUS">
                    <Columns>
                    </Columns>
                </MasterTableView>
            </tlk:RadGrid>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindQuanLyTT" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrgCon" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign2" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSignStop" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSignStop2" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmployeeGoc" runat="server"></asp:PlaceHolder>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">


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
            if (args.get_item().get_commandName() == 'CANCEL') {
                //getRadWindow().close(null);
                //args.set_cancel(true);
                /*window.close(this);*/
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "REJECT") {
                enableAjax = false;
            }
            //if (args.get_item().get_commandName() == 'EDIT') {
            //    enableAjax = false;
            //}
        }
        function getFileName(box, id, name) {
            var gotopage = "/GetFileMLG.aspx?fp=" + box + "&fid=" + id + "&fname=" + name;
            window.open(gotopage);
        }
        function OpenEditTransfer(e) {
        }
        function OnClientItemsRequesting(sender, eventArgs) {

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
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_ConcurrentlyNewEdit_txtEmpCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>