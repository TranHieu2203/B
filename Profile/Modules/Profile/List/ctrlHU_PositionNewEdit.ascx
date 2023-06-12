<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_PositionNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_PositionNewEdit" %>
<style type="text/css">
    .lbl
    {
        padding-left: 40px !important;
    }
            .NodeFTEPlan
        {
            padding-left: 20px;
            background-color: transparent;
            background-image: url("/Static/Images/ftePlan.gif");
            background-repeat: no-repeat;
        }
        
        .NodeGroup
        {
            padding-left: 20px;
            background-color: transparent;
            background-image: url("/Static/Images/group.png");
            background-repeat: no-repeat;
        }
        
        .NodeFolder
        {
            padding-left: 20px;
            background-color: transparent;
            background-image: url("/Static/Images/folder.gif");
            background-repeat: no-repeat;
        }
        .borderRight
        {
            border-right: 1px solid #C1C1C1;
        }
        .table_content{
            display: flex;
        }
        .rtWrapperContent{
            color: white;
        }
        .RadToolBar .rtbOuter {
            padding: 2px;
            border-width: 1px;
            border-style: solid;
            float: right;
        }
         .QLPD{
             position: relative;
             width: 250px;
         }
         #ctrlHU_PositionNewEdit_btnDeleteQLGT{
            position: absolute;
            right: 31px;
            top: 1px;
            color: black;
            border: unset;
            background-color: unset !important;
         }
         #ctrlHU_PositionNewEdit_txtQLPD{
            padding-right: 25px;
         }
         #ctrlHU_PositionNewEdit_btnDeleteQLGT .rbDecorated{
            background-color: unset !important;
            padding: 1px 3px;
            color: #595959;
            font-weight: bold;
         }
         #ctrlHU_PositionNewEdit_btnDeleteQLGT .rbDecorated:hover{
            color: #262626;
         }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="1500px" Height="100%" Scrolling="Both">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="Both" Width="1500px">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <asp:HiddenField ID="hidOrgIdOld" runat="server" />
        <asp:HiddenField ID="hidEmp" runat="server" />
        <asp:HiddenField ID="hidQLPD" runat="server" />
        <asp:HiddenField ID="hidPos" runat="server" />
        <asp:HiddenField ID="hidIsPlanBefore" runat="server" />
        <%--<tlk:RadToolBar ID="tbarOrgFunctions" runat="server" />--%>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <div class="table_content">
        <table class="table-form" style="width: 450px;">
            <tr>
                <td class="lbl">
                    <%# Translate("Ngày áp dụng")%><span class="lbReq">*</span>
                </td>
                <td class="borderRight" style="width: 280px">
                    <tlk:RadDatePicker ID="txtEffective_Date" runat="server" Width="250px">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEffective_Date"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày sử dụng %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày sử dụng %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Mã vị trí")%><span class="lbReq">*</span>
                </td>
                <td class="borderRight">
                    <tlk:RadTextBox ID="txtMaVT" runat="server" Width="250px" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                     <asp:CustomValidator ID="cvalMaVT" ControlToValidate="txtMaVT" runat="server" ErrorMessage="<%$ Translate: Mã vị trí đã tồn tại %>"
                        ToolTip="<%$ Translate: Mã vị trí đã tồn tại %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Công việc")%><span class="lbReq">*</span>
                </td>
                <td class="borderRight">
                    <tlk:RadComboBox ID="cboJobFamily" AutoPostBack="true" runat="server" CausesValidation="false"
                        OnSelectedIndexChanged="cboJobFamily_SelectedIndexChanged" Width="250px">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqJobFamily" ControlToValidate="cboJobFamily" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã công việc %>" ToolTip="<%$ Translate: Bạn phải nhập mã công việc %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td class="borderRight">
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" Width="220px" >
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false" AccessKey="">
                    </tlk:RadButton> 
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboJobFamily" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tổ chức %>" ToolTip="<%$ Translate: Bạn phải nhập Tổ chức %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lbl">
                     <%# Translate("Nhóm cấp bậc")%><span class="lbReq">*</span>
                </td>
                <td class="borderRight">
                    <tlk:RadComboBox ID="cboTitleGroup" runat="server" Enabled="false" SkinID="dDropdownList" AutoPostBack="false" CausesValidation="false"  Width="250px">
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ID="cusTitleGroup" runat="server" ErrorMessage="Bạn phải chọn nhóm cấp bậc"
                        ToolTip="Bạn phải chọn nhóm cấp bậc" ClientValidationFunction="cusTitleGroup">
                    </asp:CustomValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Thời gian làm việc")%>
                </td>
                <td class="borderRight">
                    <tlk:RadComboBox ID="cboWorkingTime" runat="server" CausesValidation="false" Width="250px">
                    </tlk:RadComboBox>
                </td>
            </tr>
           <%-- <tr>
                <td class="lbl">
                    <%# Translate("Cấp bậc")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTCV" runat="server" Width="250px" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>--%>
            <tr>
                <td class="lbl">
                    <%# Translate("Trưởng đơn vị")%>
                </td>
                <td class="borderRight">
                    <asp:CheckBox ID="cbTDV" runat="server" Text="" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Kế hoạch")%>
                </td>
                <td class="borderRight">
                    <asp:CheckBox ID="cbPlan" runat="server" Text="" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Non Physical")%>
                </td>
                <td class="borderRight">
                    <asp:CheckBox ID="cbNonPhysical" runat="server" Text="" AutoPostBack="true"/>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Vị trí quản lý trực tiếp")%><span class="lbReq" ID="sStarQLTT" runat="server"> *</span>
                </td>
                <td class="borderRight">
                    <tlk:RadTextBox ID="txtQLTT" runat="server" Width="220px" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqQLTT" ControlToValidate="txtQLTT" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Vị trí quản lý trực tiếp %>" ToolTip="<%$ Translate: Bạn phải nhập Vị trí quản lý trực tiếp %>">
                    </asp:RequiredFieldValidator>
                </td>
               
               
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Quản lý trực tiếp")%>
                </td>
                <td class="borderRight">
                     <tlk:RadTextBox ID="txtEmpQltt" runat="server" ReadOnly="true" SkinID="ReadOnly" Width="250px">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Vị trí quản lý gián tiếp")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td class="borderRight">
                    <div class="QLPD">
                        <tlk:RadTextBox ID="txtQLPD" runat="server" Width="220px" ReadOnly="true" SkinID="ReadOnly">
                        </tlk:RadTextBox>
                        <tlk:RadButton ID="btnDeleteQLGT" runat="server" CausesValidation="false" Text="X">
                        </tlk:RadButton>
                        <tlk:RadButton ID="btnQLPD" runat="server" SkinID="ButtonView" CausesValidation="false">
                        </tlk:RadButton>
                    </div>
                    <%--<asp:RequiredFieldValidator ID="reqQLPD" ControlToValidate="txtQLPD" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập quản lý gián tiếp %>" ToolTip="<%$ Translate: Bạn phải nhập quản lý gián tiếp %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Quản lý gián tiếp")%>
                </td>
                 <td class="borderRight">
                     <tlk:RadTextBox ID="txtEmpQlgt" runat="server" ReadOnly="true" SkinID="ReadOnly" Width="250px">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Master")%>
                </td>
                <td class="borderRight">
                    <tlk:RadTextBox ID="txtMaster" runat="server" Width="250px" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    <%# Translate("Interim")%>
                </td>
                <td class="borderRight">
                    <tlk:RadTextBox ID="txtInterim" runat="server" Width="250px" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <%--<tr>
                <td class="lbl">
                    <%# Translate("Mã chi phí")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMaCP" runat="server" Width="250px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqMaCP" ControlToValidate="txtMaCP" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã chi phí %>" ToolTip="<%$ Translate: Bạn phải nhập mã chi phí %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>--%>
            <%--<tr>
                <td class="lbl">
                    <%# Translate("Địa điểm làm việc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbbDDLV" runat="server" Width="250px">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDDLV" ControlToValidate="cbbDDLV" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập địa điểm %>" ToolTip="<%$ Translate: Bạn phải nhập địa điểm %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>--%>
            <tr>
                <td class="lbl">
                    <%# Translate("Trạng thái")%>
                </td>
                <td class="borderRight">
                    <tlk:RadComboBox ID="cboStatus" runat="server" Enabled="false" Width="250px" SkinID="ReadOnly" style="background-color:#F0E68C !important">
                        <Items>
                            <tlk:RadComboBoxItem runat="server" Text="Áp dụng" Value="-1" Selected="true" />
                            <tlk:RadComboBoxItem runat="server" Text="Ngừng áp dụng" Value="0" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lbl">
                    <asp:Label runat="server" ID="Label4" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td class="borderRight">
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" Enabled="true" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải tập tin%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtRemindLinkJD" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Tên tiếng Việt")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTenVT" runat="server" Width="130px" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Tên tiếng Anh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTCVE" runat="server" Width="130px" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Mô tả công việc")%>
                </td>
                <td>
                    <tlk:RadEditor RenderMode="Lightweight" runat="server" ID="txtSpec" SkinID="DefaultSetOfTools"
                        Height="200px" ToolsFile="/Static/ToolsFile.xml">
                        <ImageManager ViewPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            UploadPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            DeletePaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            EnableAsyncUpload="true"></ImageManager>
                    </tlk:RadEditor>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Yêu cầu công việc")%>
                </td>
                <td>
                    <tlk:RadEditor RenderMode="Lightweight" runat="server" ID="txtYC" SkinID="DefaultSetOfTools"
                        Height="200px" ToolsFile="/Static/ToolsFile.xml">
                        <ImageManager ViewPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            UploadPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            DeletePaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            EnableAsyncUpload="true"></ImageManager>
                    </tlk:RadEditor>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="borderRight" style="height: 270px"></td>
            </tr>
        </table>
        <div style="width: 1040px;">
            <table class="table-form" style="width: 1035px;">
                <tr>
                    <td class="item-head" colspan="6">
                        <%# Translate("Mô tả công việc:")%>
                    </td>
                </tr>
                <tr>
                    <td class="item-head" colspan="2">
                        <span>I. Mục đích công việc</span>
                    </td>
                    <td class="item-head" colspan="2">
                        <span>II. Tiêu chuẩn vị trí</span>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("1")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtJOB_TARGET_1" runat="server" Width="370px" >
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("Trình độ chuyên môn")%>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboTDCM" runat="server" CausesValidation="false" Width="370px">
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("2")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtJOB_TARGET_2" runat="server" Width="370px" >
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("Chuyên ngành đào tạo")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtMAJOR" runat="server"  Width="370px">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("3")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtJOB_TARGET_3" runat="server" Width="370px" >
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("Kinh nghiệm làm việc")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtWORK_EXP" runat="server"  Width="370px" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("4")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtJOB_TARGET_4" runat="server" Width="370px" >
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("Trình độ ngoại ngữ")%>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboLANGUAGE" runat="server" CausesValidation="false" Width="370px">
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("5")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtJOB_TARGET_5" runat="server" Width="370px" >
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("Trình độ tin học/phần mềm")%>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboCOMPUTER" runat="server" CausesValidation="false" Width="370px">
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("6")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtJOB_TARGET_6" runat="server" Width="370px" >
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 130px;">
                        <%# Translate("Các kỹ năng hỗ trợ")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtSUPPORT_SKILL" runat="server"   Width="370px">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
            <table class="table-form">
                <tr>
                    <td class="item-head" colspan="6">
                        <%# Translate("III.1 Quan hệ tương tác")%>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px">
                        <%# Translate("Nội bộ")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtINTERNAL_1" runat="server" width="370px" >
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 130px">
                        <%# Translate("Bên ngoài")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtOUTSIDE_1" runat="server" width="370px" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px">
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtINTERNAL_2" runat="server" width="370px" >
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 130px">
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtOUTSIDE_2" runat="server" width="370px" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px">
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtINTERNAL_3" runat="server" width="370px" >
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 130px">
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtOUTSIDE_3" runat="server" width="370px" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
            <table class="table-form">
                <tr>
                    <td class="item-head" colspan="6">
                        <%# Translate("III.2.1 Trách nhiệm")%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px"></td>
                    <td style="width: 220px; text-align: center;">
                        <%# Translate("Trách nhiệm")%>
                    </td>
                    <td style="width: 430px; text-align: center;">
                        <%# Translate("Trách nhiệm cụ thể")%>
                    </td>
                    <td style="width: 220px; text-align: center;">
                        <%# Translate("Kết quả đầu ra")%>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px; text-align:right;">
                        <%# Translate("1")%>
                    </td>
                    <td style="width: 220px;">
                        <tlk:RadTextBox ID="txtRESPONSIBILITY_1" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                    <td style="width: 430px; ">
                        <tlk:RadTextBox ID="txtDETAIL_RESPONSIBILITY_1" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                    <td style="width: 220px; ">
                        <tlk:RadTextBox ID="txtOUT_RESULT_1" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px; text-align:right;">
                        <%# Translate("2")%>
                    </td>
                    <td style="width: 220px;">
                        <tlk:RadTextBox ID="txtRESPONSIBILITY_2" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                    <td style="width: 430px; ">
                        <tlk:RadTextBox ID="txtDETAIL_RESPONSIBILITY_2" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                    <td style="width: 220px; ">
                        <tlk:RadTextBox ID="txtOUT_RESULT_2" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px; text-align:right;">
                        <%# Translate("3")%>
                    </td>
                    <td style="width: 220px;">
                        <tlk:RadTextBox ID="txtRESPONSIBILITY_3" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                    <td style="width: 430px;">
                        <tlk:RadTextBox ID="txtDETAIL_RESPONSIBILITY_3" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                    <td style="width: 220px; ">
                        <tlk:RadTextBox ID="txtOUT_RESULT_3" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px; text-align:right;">
                        <%# Translate("4")%>
                    </td>
                    <td style="width: 220px;">
                        <tlk:RadTextBox ID="txtRESPONSIBILITY_4" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                    <td style="width: 430px; ">
                        <tlk:RadTextBox ID="txtDETAIL_RESPONSIBILITY_4" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                    <td style="width: 220px; ">
                        <tlk:RadTextBox ID="txtOUT_RESULT_4" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px; text-align:right;">
                        <%# Translate("5")%>
                    </td>
                    <td style="width: 220px;">
                        <tlk:RadTextBox ID="txtRESPONSIBILITY_5" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                    <td style="width: 430px; ">
                        <tlk:RadTextBox ID="txtDETAIL_RESPONSIBILITY_5" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                    <td style="width: 220px;">
                        <tlk:RadTextBox ID="txtOUT_RESULT_5" runat="server" width="100%" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
            <table class="table-form">
                <tr>
                    <td class="item-head" colspan="6">
                        <%# Translate("III.2.2 Quyền hạn")%>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px; text-align:right;">
                        <%# Translate("1")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtPERMISSION_1" runat="server" width="880px" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px; text-align:right;">
                        <%# Translate("2")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtPERMISSION_2" runat="server" width="880px" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px; text-align:right;">
                        <%# Translate("3")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtPERMISSION_3" runat="server" width="880px" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px; text-align:right;">
                        <%# Translate("4")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtPERMISSION_4" runat="server" width="880px" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px; text-align:right;">
                        <%# Translate("5")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtPERMISSION_5" runat="server" width="880px" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 130px; text-align:right;">
                        <%# Translate("6")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtPERMISSION_6" runat="server" width="880px" >
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
            <table class="table-form" style="display: none">
                <tr>
                    <td class="item-head" style="width: 200px;">
                        <%# Translate("IV-File đính kèm")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtUploadJD" ReadOnly="true" runat="server">
                        </tlk:RadTextBox>
                        <tlk:RadTextBox ID="txtUploadFileJD" runat="server" Visible="false">
                        </tlk:RadTextBox>
                        <tlk:RadButton runat="server" ID="btnUploadFileJD" SkinID="ButtonView" CausesValidation="false"
                            TabIndex="3" Enabled="true" />
                        <tlk:RadButton ID="btnDownloadJD" runat="server" Text="<%$ Translate: Tải tập tin%>"
                            CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                        </tlk:RadButton>
                    </td>
                </tr>
            </table>
        </div>
        </div>
        <div style="display: none;">
            <asp:Button runat="server" ID="btnSaveImage" CausesValidation="false" />
            <asp:Button runat="server" ID="btnSaveFile" CausesValidation="false" />
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<common:ctrlupload id="ctrlUpload2" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindQLPD" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                e.preventDefault();
                if (e.target.id === 'ctrlHU_PositionNewEdit_txtQLTT') {
                    $find("<%=btnFindEmployee.ClientID %>").click();
                }
                else if (e.target.id === 'ctrlHU_PositionNewEdit_txtQLPD') {
                    $find("<%=btnQLPD.ClientID %>").click();
                }
                else {
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
