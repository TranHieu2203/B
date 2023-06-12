<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalTrainingOutCompany_Edit.ascx.vb"
    Inherits="Profile.ctrlPortalTrainingOutCompany_Edit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidProcessTrainID" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<div style="overflow: auto">
    <table class="table-form">
        <tr>
            <td class="lb" style="width: 200px">
                <%# Translate("Từ tháng")%> <%--<span class="lbReq">*</span>--%>
            </td>
            <td>
                <tlk:RadMonthYearPicker ID="rdTuThang" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                    DateInput-DateFormat="dd/MM/yyyy">
                </tlk:RadMonthYearPicker>
                <%--<asp:RequiredFieldValidator ID="rqTuThang" ControlToValidate="rdTuThang" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải nhập từ tháng. %>" ToolTip="<%$ Translate: Bạn phải nhập từ tháng. %>"> </asp:RequiredFieldValidator>--%>
            </td>
            <td class="lb" style="width: 200px">
                <%# Translate("Đến tháng")%>
            </td>
            <td>
                <tlk:RadMonthYearPicker ID="rdToiThang" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                    DateInput-DateFormat="dd/MM/yyyy">
                </tlk:RadMonthYearPicker>
                <asp:CustomValidator ID="cvalToiThang" runat="server" ErrorMessage="<%$ Translate: Từ tháng không đươc lớn hơn tới tháng. %>"
                    ToolTip="<%$ Translate: Từ tháng không đươc lớn hơn tới tháng. %>">
                </asp:CustomValidator>
                <%--<asp:RequiredFieldValidator ID="rqToiThang" ControlToValidate="rdToiThang" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tới tháng. %>" ToolTip="<%$ Translate: Bạn phải nhập tới tháng. %>"> </asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Năm tốt nghiệp")%>
                <%--<span class="lbReq">*</span>--%>
            </td>
            <td>
                <tlk:RadNumericTextBox runat="server" ID="rntGraduateYear" NumberFormat-DecimalDigits="0"
                    NumberFormat-GroupSizes="4" MinValue="1990" MaxValue="9999">
                </tlk:RadNumericTextBox>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntGraduateYear"
                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập năm tốt nghiệp. %>"
                    ToolTip="<%$ Translate: Bạn phải nhập năm tốt nghiệp. %>"> </asp:RequiredFieldValidator>--%>
            </td>
            <td class="lb">
                <%# Translate("Tên trường")%>
                <%--<span class="lbReq">*</span>--%>
            </td>
            <td colspan="3">
                <tlk:RadTextBox ID="txtTrainingSchool" runat="server" MaxLength="255">
                </tlk:RadTextBox>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTrainingSchool"
                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên trường. %>" ToolTip="<%$ Translate: Bạn phải nhập tên trường. %>"> </asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Hình thức đào tạo")%>
            </td>
            <td>
                <tlk:RadComboBox ID="cboTrainingForm" runat="server">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Chuyên ngành")%>
            </td>
            <td>
                <tlk:RadTextBox ID="txtChuyenNganh" SkinID="Textbox250" runat="server" MaxLength="255">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Kết quả đào tạo")%>
            </td>
            <td>
                <tlk:RadTextBox ID="txtKetQua" SkinID="Textbox250" runat="server" MaxLength="255">
                </tlk:RadTextBox>
            </td>
            <td class="lb">
                <%# Translate("Bằng cấp/Chứng chỉ")%>
            </td>
            <td>
                <tlk:RadTextBox ID="txtBangCap" SkinID="Textbox250" runat="server" MaxLength="255">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Ngày hiệu lực chứng chỉ")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdFrom" runat="server">
                </tlk:RadDatePicker>
            </td>
            <td class="lb">
                <%# Translate("Ngày hết hiệu lực chứng chỉ")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdTo" runat="server">
                </tlk:RadDatePicker>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày hiệu lực chứng chỉ phải nhỏ hơn ngày hết hiệu lực chứng chỉ %>"
                    ErrorMessage="<%$ Translate: Ngày hiệu lực chứng chỉ phải nhỏ hơn ngày hết hiệu lực chứng chỉ %>"
                    Type="Date" Operator="GreaterThan" ControlToCompare="rdFrom" ControlToValidate="rdTo"></asp:CompareValidator>
            </td>
        </tr>
    </table>
    <tlk:RadGrid PageSize=50 ID="rgTrainingOutCompanyEdit" runat="server" Height="250px" Width="99%">
        <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_ID,SPECIALIZED_TRAIN,RESULT_TRAIN,CERTIFICATE,
                          EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,STATUS,FK_PKEY,REASON_UNAPROVE" 
                          ClientDataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_ID,SPECIALIZED_TRAIN,RESULT_TRAIN,CERTIFICATE,
                          EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,STATUS,FK_PKEY,REASON_UNAPROVE" Caption="<%$ Translate: Thông tin chỉnh sửa %>">
            <Columns>
                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                </tlk:GridClientSelectColumn>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="<%$ Translate: Từ tháng%>"
                    UniqueName="FROM_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                    Visible="true" DataFormatString="{0:MM/yyyy}">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="<%$ Translate: Tới tháng%>"
                    UniqueName="TO_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:MM/yyyy}"
                    CurrentFilterFunction="EqualTo" Visible="true">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridNumericColumn HeaderText="<%$ Translate: Năm tốt nghiệp %>" DataField="YEAR_GRA"
                    UniqueName="YEAR_GRA" SortExpression="YEAR_GRA" ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </tlk:GridNumericColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên trường %>" DataField="NAME_SHOOLS"
                    UniqueName="NAME_SHOOLS" SortExpression="NAME_SHOOLS" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="FORM_TRAIN_NAME"
                    UniqueName="FORM_TRAIN_NAME" SortExpression="FORM_TRAIN_NAME" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Chuyên ngành %>" DataField="SPECIALIZED_TRAIN"
                    UniqueName="SPECIALIZED_TRAIN" SortExpression="SPECIALIZED_TRAIN" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả đào tạo %>" DataField="RESULT_TRAIN"
                    UniqueName="RESULT_TRAIN" SortExpression="RESULT_TRAIN" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Bằng cấp/chứng chỉ %>" DataField="CERTIFICATE"
                    UniqueName="CERTIFICATE" SortExpression="CERTIFICATE" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE_FROM"
                    UniqueName="EFFECTIVE_DATE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EFFECTIVE_DATE_TO"
                    UniqueName="EFFECTIVE_DATE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái phê duyệt%>" DataField="STATUS_NAME"
                    UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                  <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không phê duyệt %>" DataField="REASON_UNAPROVE"
                    UniqueName="REASON_UNAPROVE" SortExpression="REASON_UNAPROVE">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <br />
    <tlk:RadGrid PageSize=50 ID="rgTrainingOutCompany" runat="server" Height="250px" Width="99%">
        <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_ID,SPECIALIZED_TRAIN,RESULT_TRAIN,CERTIFICATE,
                            EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO " ClientDataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_ID,SPECIALIZED_TRAIN,RESULT_TRAIN,CERTIFICATE,
                            EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO" Caption="<%$ Translate: Thông tin hiện tại %>">
            <Columns>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Wrap="False" Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                 <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="<%$ Translate: Từ tháng%>"
                    UniqueName="FROM_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                    Visible="true" DataFormatString="{0:MM/yyyy}">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="<%$ Translate: Tới tháng%>"
                    UniqueName="TO_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:MM/yyyy}"
                    CurrentFilterFunction="EqualTo" Visible="true">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridNumericColumn HeaderText="<%$ Translate: Năm tốt nghiệp %>" DataField="YEAR_GRA"
                    UniqueName="YEAR_GRA" SortExpression="YEAR_GRA" ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </tlk:GridNumericColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên trường %>" DataField="NAME_SHOOLS"
                    UniqueName="NAME_SHOOLS" SortExpression="NAME_SHOOLS" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="FORM_TRAIN_NAME"
                    UniqueName="FORM_TRAIN_NAME" SortExpression="FORM_TRAIN_NAME" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Chuyên ngành %>" DataField="SPECIALIZED_TRAIN"
                    UniqueName="SPECIALIZED_TRAIN" SortExpression="SPECIALIZED_TRAIN" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả đào tạo %>" DataField="RESULT_TRAIN"
                    UniqueName="RESULT_TRAIN" SortExpression="RESULT_TRAIN" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Bằng cấp/chứng chỉ %>" DataField="CERTIFICATE"
                    UniqueName="CERTIFICATE" SortExpression="CERTIFICATE" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE_FROM"
                    UniqueName="EFFECTIVE_DATE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EFFECTIVE_DATE_TO"
                    UniqueName="EFFECTIVE_DATE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function clientButtonClicking(sender, args)
            {
                
            }

        </script>
    </tlk:RadCodeBlock>
</div>

<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />