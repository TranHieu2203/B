<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="crtlATCoeffOverTime.ascx.vb"
    Inherits="Attendance.crtlATCoeffOverTime" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                 <tlk:RadToolBar ID="rtbMain" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="300px" Scrolling="Y">
               
                <asp:ValidationSummary ID="valSum" runat="server"/>
                <table class="table-form">
                <tr>
                     <td class="lb">
                            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                     </td>
                    <td>
                        <tlk:RadDatePicker runat="server" ID="rdEffectDate">
                        </tlk:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdEffectDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy chọn ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                     <td class="item-head" colspan="6">
                            <%# Translate("Khung giờ làm việc ban đêm")%>
                     </td>
                </tr>
                <tr>
                    <td class="lb">
                            <%# Translate("Giờ bắt đầu")%><span class="lbReq">*</span>
                     </td>
                    <td>
                       <tlk:RadTimePicker ID="rtFromdate_NightHour" runat="server" ShowPopupOnFocus="true" DateInput-CausesValidation="false">
                    </tlk:RadTimePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rtFromdate_NightHour"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập Giờ bắt đầu. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                            <%# Translate("Giờ kết thúc")%><span class="lbReq">*</span>
                     </td>
                    <td>
                       <tlk:RadTimePicker ID="rtTodate_NightHour" runat="server" ShowPopupOnFocus="true" DateInput-CausesValidation="false"></tlk:RadTimePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rtTodate_NightHour"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập Giờ kết thúc. %>"></asp:RequiredFieldValidator>
                    </td>
                     <td class="lb">
                          <asp:CheckBox ID="chkIs_Tomorow" runat="server" CssClass="cheb" Text="Qua ngày hôm sau" />
                     </td>
                </tr>
                <tr>
                     <td class="item-head" colspan="6">
                            <%# Translate("Hỗ trợ giờ làm đêm")%>
                     </td>
                </tr>
                <tr>
                    <td class="lb">
                            <%# Translate("Hệ số đêm")%><span class="lbReq">*</span>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="rtxtNight_Coeff" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rtxtNight_Coeff"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập Hệ số đêm. %>"></asp:RequiredFieldValidator>
                    </td>

                    <td class="lb">
                            <%# Translate("OT trong tháng")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="rnOtMonth" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>

                    <td class="lb">
                            <%# Translate("OT trong năm")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="rnOtYear" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>

                </tr>
                <tr>
                    <td class="item-head" colspan="6">
                        <%# Translate("Hệ số OT ngày làm việc")%>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                            <%# Translate("OT ngày")%><span class="lbReq">*</span>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtWeekday_Coeff" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ntxtWeekday_Coeff"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập OT ngày. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtWeekday_Coeff_PIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số không chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtWeekday_Coeff_NonPIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                </tr>
                    <tr>
                    <td class="lb">
                            <%# Translate("OT đêm")%><span class="lbReq">*</span>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtNightWeekday_Coeff" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ntxtNightWeekday_Coeff"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập OT đêm. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtNightWeekday_Coeff_PIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số không chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtNightWeekday_Coeff_NonPIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="item-head" colspan="6">
                        <%# Translate("Hệ số OT ngày nghỉ hàng tuần")%>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                            <%# Translate("OT ngày")%><span class="lbReq">*</span>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtOffday_Coeff" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ntxtOffday_Coeff"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập OT ngày. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtOffday_Coeff_PIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số không chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtOffday_Coeff_NonPIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                </tr>
                    <tr>
                    <td class="lb">
                            <%# Translate("OT đêm")%><span class="lbReq">*</span>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtNightOffday_Coeff" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ntxtNightOffday_Coeff"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập OT đêm. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtNightOffday_Coeff_PIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số không chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtNightOffday_Coeff_NonPIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                </tr>


                    <tr>
                    <td class="item-head" colspan="6">
                        <%# Translate("Hệ số OT ngày nghỉ lễ")%>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                            <%# Translate("OT ngày")%><span class="lbReq">*</span>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtHoliday_Coeff" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="ntxtHoliday_Coeff"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập OT ngày. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtHoliday_Coeff_PIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số không chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtHoliday_Coeff_NonPIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                </tr>
                    <tr>
                    <td class="lb">
                            <%# Translate("OT đêm")%><span class="lbReq">*</span>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtNightHoliday_Coeff" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="ntxtNightHoliday_Coeff"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập OT đêm. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtNightHoliday_Coeff_PIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                            <%# Translate("Hệ số không chịu thuế")%>
                     </td>
                    <td>
                       <tlk:RadNumericTextBox ID="ntxtNightHoliday_Coeff_NonPIT" runat="server" SkinID="Decimal">
                           <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                           <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                       </tlk:RadNumericTextBox>
                    </td>
                </tr>

                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,FROMDATE_NIGHTHOUR,EFFECTDATE,TODATE_NIGHTHOUR,IS_TOMOROW,NIGHT_COEFF,WEEKDAY_COEFF,WEEKDAY_COEFF_PIT,WEEKDAY_COEFF_NONPIT,NIGHTWEEKDAY_COEFF,NIGHTWEEKDAY_COEFF_PIT,NIGHTWEEKDAY_COEFF_NONPIT,OFFDAY_COEFF,OFFDAY_COEFF_PIT,OFFDAY_COEFF_NONPIT,NIGHTOFFDAY_COEFF,NIGHTOFFDAY_COEFF_PIT,NIGHTOFFDAY_COEFF_NONPIT,HOLIDAY_COEFF,HOLIDAY_COEFF_PIT,HOLIDAY_COEFF_NONPIT,NIGHTHOLIDAY_COEFF,NIGHTHOLIDAY_COEFF_PIT,NIGHTHOLIDAY_COEFF_NONPIT,OT_MONTH,OT_YEAR">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực%>" DataField="EFFECTDATE"
                                UniqueName="EFFECTDATE" SortExpression="EFFECTDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Giờ bắt đầu làm đêm%>" DataField="FROMDATE_NIGHTHOUR"
                                UniqueName="FROMDATE_NIGHTHOUR" SortExpression="FROMDATE_NIGHTHOUR" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:HH:mm}">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Giờ kết thúc làm đêm%>" DataField="TODATE_NIGHTHOUR"
                                UniqueName="TODATE_NIGHTHOUR" SortExpression="TODATE_NIGHTHOUR" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:HH:mm}">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Qua ngày hôm sau %>" DataField="IS_TOMOROWNAME"
                                UniqueName="IS_TOMOROWNAME" SortExpression="IS_TOMOROWNAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số hỗ trợ làm đêm%>" DataField="NIGHT_COEFF"
                                UniqueName="NIGHT_COEFF" SortExpression="NIGHT_COEFF" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Hệ số ngày thường%>" DataField="WEEKDAY_COEFF"
                                UniqueName="WEEKDAY_COEFF" SortExpression="WEEKDAY_COEFF" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số ngày thường chịu thuế%>" DataField="WEEKDAY_COEFF_PIT" UniqueName="WEEKDAY_COEFF_PIT"
                                SortExpression="WEEKDAY_COEFF_PIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số ngày thường không chịu thuế%>" DataField="WEEKDAY_COEFF_NONPIT"
                                UniqueName="WEEKDAY_COEFF_NONPIT" SortExpression="WEEKDAY_COEFF_NONPIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số đêm thường%>" DataField="NIGHTWEEKDAY_COEFF"
                                UniqueName="NIGHTWEEKDAY_COEFF" SortExpression="NIGHTWEEKDAY_COEFF" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số đêm thường chịu thuế %>" DataField="NIGHTWEEKDAY_COEFF_PIT"
                                UniqueName="NIGHTWEEKDAY_COEFF_PIT" SortExpression="NIGHTWEEKDAY_COEFF_PIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số đêm thường không chịu thuế%>" DataField="NIGHTWEEKDAY_COEFF_NONPIT"
                                UniqueName="NIGHTWEEKDAY_COEFF_NONPIT" SortExpression="NIGHTWEEKDAY_COEFF_NONPIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Hệ số ngày nghỉ%>" DataField="OFFDAY_COEFF"
                                UniqueName="OFFDAY_COEFF" SortExpression="OFFDAY_COEFF" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số ngày nghỉ chịu thuế%>" DataField="OFFDAY_COEFF_PIT" UniqueName="OFFDAY_COEFF_PIT"
                                SortExpression="OFFDAY_COEFF_PIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số ngày nghỉ không chịu thuế%>" DataField="OFFDAY_COEFF_NONPIT"
                                UniqueName="OFFDAY_COEFF_NONPIT" SortExpression="OFFDAY_COEFF_NONPIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số đêm nghỉ%>" DataField="NIGHTOFFDAY_COEFF"
                                UniqueName="NIGHTOFFDAY_COEFF" SortExpression="NIGHTOFFDAY_COEFF" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số đêm nghỉ chịu thuế %>" DataField="NIGHTOFFDAY_COEFF_PIT"
                                UniqueName="NIGHTOFFDAY_COEFF_PIT" SortExpression="NIGHTOFFDAY_COEFF_PIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số đêm nghỉ không chịu thuế%>" DataField="NIGHTOFFDAY_COEFF_NONPIT"
                                UniqueName="NIGHTOFFDAY_COEFF_NONPIT" SortExpression="NIGHTOFFDAY_COEFF_NONPIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Hệ số ngày lễ%>" DataField="HOLIDAY_COEFF"
                                UniqueName="HOLIDAY_COEFF" SortExpression="HOLIDAY_COEFF" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số ngày lễ chịu thuế%>" DataField="HOLIDAY_COEFF_PIT" UniqueName="HOLIDAY_COEFF_PIT"
                                SortExpression="HOLIDAY_COEFF_PIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate:Hệ số ngày lễ không chịu thuế%>" DataField="HOLIDAY_COEFF_NONPIT"
                                UniqueName="HOLIDAY_COEFF_NONPIT" SortExpression="HOLIDAY_COEFF_NONPIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số đêm lễ%>" DataField="NIGHTHOLIDAY_COEFF" UniqueName="NIGHTHOLIDAY_COEFF"
                                SortExpression="NIGHTHOLIDAY_COEFF" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate:Hệ số đêm lễ chịu thuế%>" DataField="NIGHTHOLIDAY_COEFF_PIT"
                                UniqueName="NIGHTHOLIDAY_COEFF_PIT" SortExpression="NIGHTHOLIDAY_COEFF_PIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số đêm lễ không chịu thuế%>" DataField="NIGHTHOLIDAY_COEFF_NONPIT" UniqueName="NIGHTHOLIDAY_COEFF_NONPIT"
                                SortExpression="NIGHTHOLIDAY_COEFF_NONPIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: OT trong tháng%>" DataField="OT_MONTH" UniqueName="OT_MONTH"
                                SortExpression="OT_MONTH" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: OT trong năm%>" DataField="OT_YEAR" UniqueName="OT_YEAR"
                                SortExpression="OT_YEAR" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;

        var splitterID = 'ctl00_MainContent_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_crtlATCoeffOverTime_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_crtlATCoeffOverTime_RadPane2';
        var validateID = 'MainContent_crtlATCoeffOverTime_valSum';
        var oldSize = $('#' + pane1ID).height();

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
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgData.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
}

function onRequestStart(sender, eventArgs) {
    eventArgs.set_enableAjax(enableAjax);
    enableAjax = true;
}

function setDisplayValue(sender, args) {
    sender.set_displayValue(sender.get_value());
}
    </script>
</tlk:RadCodeBlock>
