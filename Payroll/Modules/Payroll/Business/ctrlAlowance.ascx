<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAlowance.ascx.vb"
    Inherits="Payroll.ctrlAlowance" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane4" runat="server" Width="250px" Scrolling="None">
  
        <fieldset style="padding:0">
    <legend>Thông tin tìm kiếm:</legend>
<table class="table-form">
            <tr>
                <td style="min-width:90px" class="lb">
                    Nhân viên
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="140px">
                    </tlk:RadTextBox>                        
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhân viên %>" ToolTip="<%$ Translate: Bạn phải chọn Nhân viên %>"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    NV nghỉ việc
                </td>
                <td>
                    <tlk:RadButton ID="chkNhanvienghiviec" AutoPostBack="false" ToggleType="CheckBox"
                        ButtonType="ToggleButton" runat="server" Text="" CausesValidation="False">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
  </fieldset>
        
   
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None" Height="100%">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPaneTop" runat="server" Height="30px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="125px">          
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTennhanvien" runat="server">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox ID="txtEmployid" runat="server" Visible="false">
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false" Text="...">
                    </tlk:RadButton>
                        </td>
                          <td class="lb">
                            <%# Translate("Phụ cấp")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPhucap" runat="server" SkinID="dDropdownList" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
               
                    <tr>
                        <td class="lb">
                            <%# Translate("Số tiền")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtSotien" runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="dpTungay" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                          <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="dpDenngay" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
               
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtGhichu" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                   
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPaneBotton" runat="server" Scrolling="None">
                <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
           
                
                    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                        <tlk:RadGrid ID="rgData" runat="server" SkinID="GridSingleSelect" Height="100%">
                  <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                <%--    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="EMPLOYEE_ID,EFFECT_DATE,EXP_DATE,ACTFLG">
                        <Columns>
                         <tlk:GridClientSelectColumn UniqueName="CHK" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                             
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_ID"
                                SortExpression="EMPLOYEE_ID" UniqueName="EMPLOYEE_ID">
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="EFFECT_DATE"
                                SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc%>" DataField="EXP_DATE"
                                SortExpression="EXP_DATE" UniqueName="EXP_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                             
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                                SortExpression="ACTFLG" UniqueName="ACTFLG" HeaderStyle-Width="50px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                SortExpression="REMARK" UniqueName="REMARK" HeaderStyle-Width="50px" />
                        </Columns>
                    </MasterTableView>--%>
                </tlk:RadGrid>
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<script type="text/javascript">

    function clientButtonClicking(sender, args) {
        if (args.get_item().get_commandName() == 'EXPORT') {
            enableAjax = false;
        }
    }
    var enableAjax = true;
    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
</script>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>