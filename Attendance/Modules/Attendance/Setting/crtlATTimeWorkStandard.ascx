<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="crtlATTimeWorkStandard.ascx.vb"
    Inherits="Attendance.crtlATTimeWorkStandard" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    td.paddingzero {
        padding-left: 0px !important;
    }
</style>
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:ValidationSummary ID="valSum" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
     
    <tlk:radpane id="MainPane" runat="server" scrolling="None">
        <tlk:radsplitter id="RadSplitter2" runat="server" width="100%" height="100%" orientation="Horizontal">
            <tlk:radpane id="RadPane3" runat="server" height="35px" scrolling="None">
                <tlk:radtoolbar id="tbarOT" runat="server" onclientbuttonclicking="clientButtonClicking" />
            </tlk:radpane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="275px" Scrolling="None">
                <table class="table-form">
                <tr>
                     <td class="lb">
                         <%# Translate("Năm hiệu lực")%><span class="lbReq">*</span>
                     </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtYear"></tlk:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtYear"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập Năm hiệu lực. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                    <td class="lb">
                            <%# Translate("Đơn vị")%><span class="lbReq">*</span>
                     </td>
                    <td>
                       <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" SkinID="Readonly" ReadOnly="true" />
                       <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    </td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOrgName"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập đơn vị. %>"></asp:RequiredFieldValidator>
                    <td></td>
                   <%-- <td class="lb">
                         <%# Translate("Theo nhân viên")%>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkEmployee" runat="server" AutoPostBack="true" onclick="NotCheck(this.id)" />
                    </td>
                     <td class="lb" style="width: 200px">
                    <asp:Label ID="lbEmployeeCode" runat="server" Text="<%$ Translate: Mã nhân viên %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode"  runat="server" AutoPostBack="true" Width="130px">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                </td>--%>
                </tr>
                <tr>
                     <td class="lb">
                            <%# Translate("Đối tượng nhân viên")%><span class="lbReq">*</span>
                     </td>
                    <td>
                        <tlk:RadComboBox ID="cboObjEmployee" runat="server"></tlk:RadComboBox>
                    </td>
                     <td>
                        <%--<tlk:RadTextBox runat="server" ID="RadTextBox1"></tlk:RadTextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboObjEmployee"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập đối tượng nhân viên. %>"></asp:RequiredFieldValidator>
                    </td>
                     <td class="lb">
                            <%# Translate("Môi trường làm việc")%><span class="lbReq">*</span>
                     </td>
                    <td>
                        <tlk:RadComboBox ID="cboObjAttendant" runat="server"></tlk:RadComboBox>
                    </td>
                    <td>
                       <%-- <tlk:RadTextBox runat="server" ID="RadTextBox2"></tlk:RadTextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboObjAttendant"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập môi trường làm việc. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb"></td>
                    <td></td>
                    <td class="lb" style="width: 200px">
                  
                    </td>
                    <td>
                      <%--  <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                        </tlk:RadTextBox>--%>
                    </td>

                </tr>
                <tr>
                    <td class="lb"></td>
                    <td></td>
                    <td class="lb"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="lb"></td>
                    <td>
                        <asp:CheckBox ID="chkNotT7" runat="server" Text="Không tính thứ 7" />
                    </td>
                    <td class="lb"></td>
                    <td>
                        <asp:CheckBox ID="chkNotCN" runat="server" Text="Không tính chủ nhật"/>
                    </td>

                    <td class="lb"></td>
                    <td></td>
                    <%--<td class="lb">
                    <asp:Label ID="lbOrg_Name" runat="server" Text="<%$ Translate: Đơn vị %>"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtOrg_Name" runat="server" ReadOnly="True" SkinID="ReadOnly">
                        </tlk:RadTextBox>
                    </td>--%>
                </tr>
                <tr>
                <td class="lb"></td>
                <td>
                    <asp:CheckBox ID="chkNotNT7" runat="server" Text="Không tính 1/2 thứ 7" />
                </td>
                <td class="lb"></td>
                <td>
                    <asp:CheckBox ID="chkNot2T7" runat="server" Text="Không tính 2 thứ 7 trong tháng"/>
                </td>

                <td class="lb"></td>
                <td></td>

                <%--<td class="lb" style="width: 200px">
                 <asp:Label ID="lbTITLE" runat="server" Text="<%$ Translate: Vị trí công việc %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>--%>
                  
            </tr>
            <tr>
                <td class="lb"></td>
                <td>
                    <tlk:RadTextBox Width="50px" runat="server" ID="txtTC"></tlk:RadTextBox>
                    <%# Translate("Trừ công trong tháng")%>
                </td>
                <td class="lb"></td>
                <td>
                    <tlk:RadTextBox Width="50px" runat="server" ID="txtCMD"></tlk:RadTextBox>
                    <%# Translate("Công mặc định")%>
                </td>
            </tr>
            <tr>
                <td class="lb"></td>
                <td colspan ="9">
                     &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                     &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                     &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    <tlk:RadButton runat="server" ID="btnCal" CausesValidation="false" Text="  Khởi tạo  " />
                </td>
                <td class="lb"></td>
                <td></td>
            </tr>  
            <tr> 
                 <td class="lb">
                    <%# Translate("Hệ số quy đổi")%>
                     <br />
                     <tlk:RadNumericTextBox runat="server" ID="rdConversionFator" Width="75px" SkinID="Number"></tlk:RadNumericTextBox>
                </td>
                <%--<td>
                    
                </td>--%>
                <%--<td class="lb"></td>--%>
                <td colspan="9">
                    <tlk:RadGrid ID="rgCal" runat="server" Height="80px" Width="810px" Style="overflow: auto;">
                        <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID" AllowCustomPaging="false" AllowPaging="false">
                            <Columns>
                                <tlk:GridTemplateColumn HeaderText="<%$ Translate: T1 %>" SortExpression="T1"
                                    UniqueName="T1" DataField="T1">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T1" SkinID="Decimal" runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>   
                                    <ItemStyle HorizontalAlign="Center" />  
                                     <ItemStyle CssClass="paddingzero" />                               
                                </tlk:GridTemplateColumn>
                                 <tlk:GridTemplateColumn HeaderText="<%$ Translate: T2 %>" SortExpression="T2"
                                    UniqueName="T2">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T2" SkinID="Decimal"  runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                     <ItemStyle CssClass="paddingzero" />
                                </tlk:GridTemplateColumn>
                                 <tlk:GridTemplateColumn HeaderText="<%$ Translate: T3 %>" SortExpression="T3"
                                    UniqueName="T3">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T3" SkinID="Decimal"  runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                     <ItemStyle CssClass="paddingzero" />
                                </tlk:GridTemplateColumn>
                                 <tlk:GridTemplateColumn HeaderText="<%$ Translate: T4 %>" SortExpression="T4"
                                    UniqueName="T4">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T4" SkinID="Decimal"  runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                     <ItemStyle CssClass="paddingzero" />
                                </tlk:GridTemplateColumn>
                                 <tlk:GridTemplateColumn HeaderText="<%$ Translate: T5 %>" SortExpression="T5"
                                    UniqueName="T5">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T5" SkinID="Decimal"  runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                     <ItemStyle CssClass="paddingzero" />
                                </tlk:GridTemplateColumn>
                                 <tlk:GridTemplateColumn HeaderText="<%$ Translate: T6 %>" SortExpression="T6"
                                    UniqueName="T6">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T6" SkinID="Decimal"  runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                     <ItemStyle CssClass="paddingzero" />
                                </tlk:GridTemplateColumn>
                                 <tlk:GridTemplateColumn HeaderText="<%$ Translate: T7 %>" SortExpression="T7"
                                    UniqueName="T7">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T7" SkinID="Decimal" runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                     <ItemStyle CssClass="paddingzero" />
                                </tlk:GridTemplateColumn>
                                 <tlk:GridTemplateColumn HeaderText="<%$ Translate: T8 %>" SortExpression="T8"
                                    UniqueName="T8">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T8" SkinID="Decimal" runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                     <ItemStyle CssClass="paddingzero" />
                                </tlk:GridTemplateColumn>
                                 <tlk:GridTemplateColumn HeaderText="<%$ Translate: T9 %>" SortExpression="T9"
                                    UniqueName="T9">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T9" SkinID="Decimal" runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                     <ItemStyle CssClass="paddingzero" />
                                </tlk:GridTemplateColumn>
                                 <tlk:GridTemplateColumn HeaderText="<%$ Translate: T10 %>" SortExpression="T10"
                                    UniqueName="T10">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T10" SkinID="Decimal" runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                     <ItemStyle CssClass="paddingzero" />
                                </tlk:GridTemplateColumn>
                                 <tlk:GridTemplateColumn HeaderText="<%$ Translate: T11 %>" SortExpression="T11"
                                    UniqueName="T11">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T11" SkinID="Decimal"  runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                     <ItemStyle CssClass="paddingzero" />
                                </tlk:GridTemplateColumn>
                                 <tlk:GridTemplateColumn HeaderText="<%$ Translate: T12 %>" SortExpression="T12"
                                    UniqueName="T12">
                                    <ItemTemplate>
                                        <tlk:RadNumericTextBox ID="T12" SkinID="Decimal" runat="server"
                                            CausesValidation="false" Width="100%">
                                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                        </tlk:RadNumericTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                     <ItemStyle CssClass="paddingzero" />
                                </tlk:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                </td>
            </tr>
           </table>    
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC,EMP_ORG_DESC" ClientDataKeyNames="ID,YEAR,OBJ_EMPLOYEE_ID,OBJ_ATTENDANT_ID,IS_EDIT,ORG_NAME,ORG_ID,EMPLOYEE_CODE,EMPLOYEE_ID,EMPLOYEE_NAME,EMP_ORG_NAME,TITLE_NAME,EMPLOYEE_CODE,IS_EMPLOYEE,CONVERSION_FACTOR,IS_NOT_CAL_SUN,IS_NOT_CAL_SAT,IS_NOT_CAL_2_SAT,IS_NOT_CAL_HALF_SAT,MINUS_MONTHLY_AT,DEFAULT_AT">
                        <Columns>
                           <%-- <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm hiệu lực%>" DataField="YEAR" HeaderStyle-Width="80px"
                                UniqueName="YEAR" SortExpression="YEAR" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị%>" DataField="ORG_NAME" HeaderStyle-Width="120px"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng nhân viên%>" DataField="OBJ_EMPLOYEE_NAME" HeaderStyle-Width="100px"
                                UniqueName="OBJ_EMPLOYEE_NAME" SortExpression="OBJ_EMPLOYEE_NAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Môi trường làm việc%>" DataField="OBJ_ATTENDANT_NAME" HeaderStyle-Width="100px"
                                UniqueName="OBJ_ATTENDANT_NAME" SortExpression="OBJ_ATTENDANT_NAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>

                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên%>" DataField="EMPLOYEE_CODE" HeaderStyle-Width="100px"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ & tên NV%>" DataField="EMPLOYEE_NAME" HeaderStyle-Width="100px"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban NV%>" DataField="EMP_ORG_NAME" HeaderStyle-Width="100px"
                                UniqueName="EMP_ORG_NAME" SortExpression="EMP_ORG_NAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc NV%>" DataField="TITLE_NAME" HeaderStyle-Width="100px"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số quy đổi%>" DataField="CONVERSION_FACTOR"
                                UniqueName="CONVERSION_FACTOR" SortExpression="CONVERSION_FACTOR" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T1%>" DataField="WORKSTANDARD_M1"
                                UniqueName="WORKSTANDARD_M1" SortExpression="WORKSTANDARD_M1" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T2%>" DataField="WORKSTANDARD_M2"
                                UniqueName="WORKSTANDARD_M2" SortExpression="WORKSTANDARD_M2" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T3%>" DataField="WORKSTANDARD_M3"
                                UniqueName="WORKSTANDARD_M3" SortExpression="WORKSTANDARD_M3" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T4%>" DataField="WORKSTANDARD_M4"
                                UniqueName="WORKSTANDARD_M4" SortExpression="WORKSTANDARD_M4" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T5%>" DataField="WORKSTANDARD_M5"
                                UniqueName="WORKSTANDARD_M5" SortExpression="WORKSTANDARD_M5" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T6%>" DataField="WORKSTANDARD_M6"
                                UniqueName="WORKSTANDARD_M6" SortExpression="WORKSTANDARD_M6" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T7%>" DataField="WORKSTANDARD_M7"
                                UniqueName="WORKSTANDARD_M7" SortExpression="WORKSTANDARD_M7" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T8%>" DataField="WORKSTANDARD_M8"
                                UniqueName="WORKSTANDARD_M8" SortExpression="WORKSTANDARD_M8" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T9%>" DataField="WORKSTANDARD_M9"
                                UniqueName="WORKSTANDARD_M9" SortExpression="WORKSTANDARD_M9" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T10%>" DataField="WORKSTANDARD_M10"
                                UniqueName="WORKSTANDARD_M10" SortExpression="WORKSTANDARD_M10" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T11%>" DataField="WORKSTANDARD_M11"
                                UniqueName="WORKSTANDARD_M11" SortExpression="WORKSTANDARD_M11" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: T12%>" DataField="WORKSTANDARD_M12"
                                UniqueName="WORKSTANDARD_M12" SortExpression="WORKSTANDARD_M12" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>--%>
                        </Columns>
                        <HeaderStyle Width="40px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
       </tlk:radsplitter>
    </tlk:radpane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_crtlATTimeWorkStandard_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_crtlATTimeWorkStandard_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_crtlATTimeWorkStandard_RadPane2';
        var validateID = 'MainContent_crtlATTimeWorkStandard_valSum';
        var oldSize = $('#' + pane1ID).height();

        var enableAjax = true;

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    $('#' + txtBoxName + ',' + '#' + txtBoxTitle).css("width", "100%");
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgData.ClientID%>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule', 0, 0, 7);
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_crtlATTimeWorkStandard_rdFromDate_dateInput').val('');
            $('#ctl00_MainContent_crtlATTimeWorkStandard_rdToDate_dateInput').val('');
        }

        function setDisplayValue(sender, args) {
            sender.set_displayValue(sender.get_value());
        }


        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_crtlATTimeWorkStandard_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);

        function NotCheck(checkbox) {
        }

    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
