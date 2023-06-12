<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_MutilChangeInfoNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_MutilChangeInfoNewEdit" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<%@ Import Namespace="Profile" %>
<asp:HiddenField ID="hidIDEmp" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidManager" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="396px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
            ValidationGroup="" />
        <table class="table-form">
            <%--<tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin chung")%>
                    <hr />
                </td>
            </tr>--%>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbEffectDate" runat="server" Text="Ngày hiệu lực"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server" AutoPostBack="true">
                        <DateInput CausesValidation="false">
                        </DateInput>
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusExistEffectDate" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"
                        ToolTip="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"> </asp:CustomValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <asp:Label ID="lbDecisionType" runat="server" Text="Loại quyết định"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDecisionType" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboDecisionType"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại quyết định %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại quyết định %>"> </asp:RequiredFieldValidator>
                </td>
                
                
            </tr>
            <tr>
                
                <td class="lb">
                    <asp:Label ID="lbSignDate" runat="server" Text="Ngày ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbStatus" runat="server" Text="Trạng thái"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                    </tlk:RadComboBox>
                </td>

            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi Chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtSDESC" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            </table>
           <table class="table-form">
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin điều chỉnh")%>
                    <hr />
                </td>
            </tr>
            <tr>

                <td class="">
                    <%--<asp:Label ID="lbManagerNew" runat="server" Text="Quản lý trực tiếp"></asp:Label>--%>
                    <asp:CheckBox ID="chkManagerNew" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged"  Text ="<%$ Translate: Thay đổi Quản lý trực tiếp %>" />
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtManagerNew" AutoPostBack="true" Width="130px" >
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rqManagerNew" ControlToValidate="txtManagerNew"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn quản lý trực tiếp %>" ToolTip="<%$ Translate: Bạn phải chọn quản lý trực tiếp %>"></asp:RequiredFieldValidator>
                    <tlk:RadButton runat="server" ID="btnFindDirect" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
               
            </tr>
            <tr>
                <td class="">
                    <%--<asp:Label ID="lbOrgName" runat="server" Text="Đơn vị"></asp:Label><span class="lbReq">*</span>--%>
                    <asp:CheckBox ID="chkOrgName" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" Text="<%$ Translate: Thay đổi đơn vị/Phòng ban %>" />
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="rqOrgName" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="">
                    <%--<asp:Label ID="lbTitle" runat="server" Text="Chức danh"></asp:Label><span class="lbReq">*</span>--%>
                    <asp:CheckBox ID="chkTitle" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" Text="<%$ Translate: Thay đổi chức danh %>" />
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTitle">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="rqTitle" ControlToValidate="cboTitle"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn chức danh %>" ToolTip="<%$ Translate: Bạn phải chọn chức danh %>"> </asp:RequiredFieldValidator>
                </td>

            </tr>
            <tr>
                <td class="">
                    <%--<asp:Label ID="Label3" runat="server" Text="Đối tượng công"></asp:Label><span
                        class="lbReq">*</span>--%>
                    <asp:CheckBox ID="chkObjectAttendant" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" Text="<%$ Translate: Thay đổi môi trường làm việc %>" />
                </td>
                <td>
                    <tlk:RadComboBox ID="cboObjectAttendant" runat="server" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="rqObjectAttendant" ControlToValidate="cboObjectAttendant"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Môi trường làm việc %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Môi trường làm việc %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="">
                    <%--<asp:Label ID="Label1" runat="server" Text="Đối tượng nhân viên"></asp:Label><span
                        class="lbReq">*</span>--%>
                    <asp:CheckBox ID="chkObjectEmp" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" Text="<%$ Translate: Thay đổi Đối tượng nhân viên %>" />
                </td>
                <td>
                    <tlk:RadComboBox ID="cboObjectEmp" runat="server" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="rqObjectEmp" ControlToValidate="cboObjectEmp"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đối tượng nhân viên %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Đối tượng nhân viên %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                
                <td class="">
                    <%--<asp:Label ID="Label2" runat="server" Text="Nơi làm việc"></asp:Label><span
                        class="lbReq">*</span>--%>
                    <asp:CheckBox ID="chkWorkPlace" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" Text="<%$ Translate: Thay đổi Nơi làm việc %>" />
                </td>
                <td>
                    <tlk:RadComboBox ID="cboWorkPlace" runat="server" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="rqWorkPlace" ControlToValidate="cboWorkPlace"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nơi làm việc %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Nơi làm việc %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="">
                    <%--<asp:Label ID="lbOBJECT_ATTENDANCE" runat="server" Text="Đối tượng chấm công"></asp:Label>--%>
                     <asp:CheckBox ID="chkOBJECT_ATTENDANCE" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" Text="<%$ Translate: Thay đổi Đối tượng chấm công %>" />
                </td>
                <td>
                    <tlk:RadComboBox ID="cbOBJECT_ATTENDANCE" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="rqOBJECT_ATTENDANCE" ControlToValidate="cbOBJECT_ATTENDANCE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Thay đổi Đối tượng chấm công %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Thay đổi Đối tượng chấm công %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="">
                    <%--<asp:Label ID="lbObjectLaborNew" runat="server" Text="Loại hình lao động"></asp:Label><span
                        class="lbReq" runat="server" id="spObjectLaborNew">*</span>--%>
                    <asp:CheckBox ID="chkObjectLaborNew" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" Text="<%$ Translate: Thay đổi Loại hình lao động %>" />
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboObjectLaborNew" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ID="cusObjectLaborNew" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đối tượng lao động %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Đối tượng lao động %>" ClientValidationFunction="cusObjectLaborNew">
                    </asp:CustomValidator>--%>

                     <asp:RequiredFieldValidator ID="reqObjectLabor" ControlToValidate="cboObjectLaborNew"
                    runat="server" ErrorMessage="Bạn phải chọn loại hình lao động" ToolTip="Bạn phải chọn loại hình lao động">
                </asp:RequiredFieldValidator>
                </td> 
            </tr>
            
          
            
            <tr style="visibility: hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFileAttach_Link" runat="server" ReadOnly="true" Visible="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
           
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgEmployee" AllowPaging="true" AllowMultiRowEdit="true" runat="server"
            PageSize="50" Height="100%">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="true" AllowCustomPaging="true" DataKeyNames="EMPLOYEE_ID,EMPLOYEE_NAME,TITLE_ID,ORG_ID,TITLE_NAME,ORG_NAME,EMPLOYEE_CODE,REMARK,OBJECT_ATTENDANCE_NAME,OBJECT_LABORNAME,OBJECT_ATTENDANCE,OBJECT_LABOR,OBJECT_ATTENDANT_ID,OBJECT_ATTENDANT_NAME,OBJECT_EMPLOYEE_ID,OBJECT_EMPLOYEE_NAME,WORK_PLACE_ID,WORK_PLACE_NAME,DIRECT_MANAGER"
                ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_NAME,TITLE_ID,ORG_ID,TITLE_NAME,ORG_NAME,EMPLOYEE_CODE,REMARK,OBJECT_ATTENDANCE_NAME,OBJECT_LABORNAME,OBJECT_ATTENDANCE,OBJECT_LABOR,OBJECT_ATTENDANT_ID,OBJECT_ATTENDANT_NAME,OBJECT_EMPLOYEE_ID,OBJECT_EMPLOYEE_NAME,WORK_PLACE_ID,WORK_PLACE_NAME,DIRECT_MANAGER"
                CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnEmployee" runat="server" Text="Chọn tất cả nhân viên"
                                CausesValidation="false" CommandName="FindEmployee" TabIndex="3">
                            </tlk:RadButton>
                            <%--<tlk:RadButton Width="150px" ID="RadButton1" runat="server" Text="Chọn nhân viên từ import"
                                CausesValidation="false" CommandName="FindEmployeeImport" TabIndex="3">
                            </tlk:RadButton>--%>
                        </div>
                        <%-- <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnExport" runat="server" Text="Xuất Excel"   
                                CausesValidation="false" CommandName="Export" TabIndex="3">
                            </tlk:RadButton>
                        </div>--%>
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
                    <tlk:GridBoundColumn HeaderText="MSNV" DataField="EMPLOYEE_CODE" ReadOnly="true"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME"
                        ReadOnly="true" SortExpression="EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        ReadOnly="true" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        ReadOnly="true" SortExpression="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Đối tượng chấm công" DataField="OBJECT_ATTENDANCE_NAME"
                        UniqueName="OBJECT_ATTENDANCE_NAME" ReadOnly="true" SortExpression="OBJECT_ATTENDANCE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Loại hình lao động" DataField="OBJECT_LABORNAME"
                        UniqueName="OBJECT_LABORNAME" ReadOnly="true" SortExpression="OBJECT_LABORNAME" />
                    <tlk:GridBoundColumn HeaderText="Môi trường làm việc" DataField="OBJECT_ATTENDANT_NAME" UniqueName="OBJECT_ATTENDANT_NAME"
                        ReadOnly="true" SortExpression="OBJECT_ATTENDANT_NAME" />
                    <tlk:GridBoundColumn HeaderText="Đối tượng nhân viên" DataField="OBJECT_EMPLOYEE_NAME" UniqueName="OBJECT_EMPLOYEE_NAME"
                        ReadOnly="true" SortExpression="OBJECT_EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Nơi làm việc" DataField="WORK_PLACE_NAME" UniqueName="WORK_PLACE_NAME"
                        ReadOnly="true" SortExpression="WORK_PLACE_NAME" />
                    <tlk:GridBoundColumn HeaderText="NHÂN VIÊN" DataField="EMPLOYEE_ID" UniqueName="EMPLOYEE_ID"
                        ReadOnly="true" SortExpression="EMPLOYEE_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="CHỨC DANH" DataField="TITLE_ID" UniqueName="TITLE_ID"
                        ReadOnly="true" SortExpression="TITLE_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="TÊN PHÒNG BAN" DataField="ORG_ID" UniqueName="ORG_ID"
                        ReadOnly="true" SortExpression="ORG_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="ID THIẾT LẬP" DataField="WELFARE_ID" UniqueName="WELFARE_ID"
                        ReadOnly="true" SortExpression="WELFARE_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="QUẢN LÝ TRỰC TIẾP" DataField="DIRECT_MANAGER" UniqueName="DIRECT_MANAGER"
                        ReadOnly="true" SortExpression="DIRECT_MANAGER" Visible="false" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSignManager" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            xPos = $find("<%# RadSplitter1.ClientID%>")._element.control._element.scrollLeft;
            yPos = $find("<%# RadSplitter1.ClientID%>")._element.control._element.scrollTop;
        }
        function EndRequestHandler(sender, args) {
            $find("<%# RadSplitter1.ClientID%>")._element.control._element.scrollLeft = xPos;
            $find("<%# RadSplitter1.ClientID%>")._element.control._element.scrollTop = yPos;
        }
        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_MutilChangeInfoNewEdit_LeftPane');
        });



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
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'Export') {
                enableAjax = false;
            }
        }
        function btnExportClicking(sender, args) {
            enableAjax = false;
        }
        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {

                default:
                    break;
            }
        }

        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function OnClientItemsRequesting() { }
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
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_MutilChangeInfoNewEdit_txtManagerNew' && e.target.id !=='ctl00_MainContent_ctrlHU_MutilChangeInfoNewEdit_txtOrgName') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
        
    </script>
</tlk:RadCodeBlock>
