<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAllowance.ascx.vb"
    Inherits="Payroll.ctrlAllowance" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidEmp" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane4" runat="server" Width="250px" Scrolling="None">
    <fieldset style="padding:0">
    <legend>Thông tin tìm kiếm:</legend>
<table class="table-form">
            <tr>
                <td style="min-width:70px" class="lb">
                    Nhân viên
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="140px">
                    </tlk:RadTextBox>                        
                    
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
                <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking=clientButtonClicking >
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="150px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTennhanvien" runat="server">
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
                        <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME_VN,ALLOWANCE_TYPE,ALLOWANCE_TYPE_NAME,AMOUNT,EFFECT_DATE,EXP_DATE,ACTFLG,REMARK">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="CHK" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Mã nhân viên %>"
                                        DataField="EMPLOYEE_ID" SortExpression="EMPLOYEE_ID" UniqueName="EMPLOYEE_ID" Display="False" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Mã nhân viên %>"
                                        DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Họ và tên %>"
                                        DataField="FULLNAME_VN" SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" />
                                    <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Loại phụ cấp %>"
                                        DataField="ALLOWANCE_TYPE_NAME" SortExpression="ALLOWANCE_TYPE_NAME" UniqueName="ALLOWANCE_TYPE_NAME" />
                                        <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Tiền phụ cấp %>"
                                        DataField="AMOUNT" SortExpression="AMOUNT" UniqueName="AMOUNT" />
                                        <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Ngày hiệu lực %>"
                                        DataField="EFFECT_DATE" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE" />
                                        <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Ngày hết hiệu lực %>"
                                        DataField="EXP_DATE" SortExpression="EXP_DATE" UniqueName="EXP_DATE" />
                                        <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Trạng thái %>"
                                        DataField="ACTFLG" SortExpression="ACTFLG" UniqueName="ACTFLG" />
                                        <tlk:GridBoundColumn HeaderStyle-Width="100px" HeaderText="<%$ Translate: Ghi chú %>"
                                        DataField="REMARK" SortExpression="REMARK" UniqueName="REMARK" />
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
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
