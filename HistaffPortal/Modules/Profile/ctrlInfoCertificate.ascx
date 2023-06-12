<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInfoCertificate.ascx.vb"
    Inherits="Profile.ctrlInfoCertificate" %>
<%@ Import Namespace="Common" %>
<style type="text/css">
  /*.rgDataDiv
  {
      height: 100px !important
      }*/

</style>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidFamilyID" runat="server" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<div >
    <table class="table-form" style="display:none">
        <tr>
            <td class="lb" style="width: 150px">
                <%# Translate("Thời gian đào tạo từ tháng")%>
                <span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadMonthYearPicker ID="rdFromDate" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                    DateInput-DateFormat="dd/MM/yyyy">
                </tlk:RadMonthYearPicker>
                <asp:RequiredFieldValidator ID="rqFromDate" ControlToValidate="rdFromDate" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải chọn từ tháng %>" ToolTip="<%$ Translate: Bạn phải chọn từ tháng %>">
                </asp:RequiredFieldValidator>
            </td>
            <td class="lb">
                <%# Translate("Đến tháng")%>
            </td>
            <td>
                <tlk:RadMonthYearPicker ID="rdToDate" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                    DateInput-DateFormat="dd/MM/yyyy">
                </tlk:RadMonthYearPicker>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Tên trường")%>
                <span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtSchool">
                </tlk:RadTextBox>
                <asp:RequiredFieldValidator ID="rqSchool" ControlToValidate="txtSchool" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải chọn tên trường %>" ToolTip="<%$ Translate: Bạn phải chọn tên trường %>">
                </asp:RequiredFieldValidator>
            </td>
            <td class="lb">
                <asp:Label runat="server" ID="lbChuyenNganh" Text="Chuyên ngành"></asp:Label>
            </td>
            <td>
                <tlk:RadTextBox ID="txtChuyenNganh" SkinID="Textbox250" runat="server">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <asp:Label runat="server" ID="lbLevel" Text="Trình độ"></asp:Label>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbLevel">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <asp:Label runat="server" ID="lbSorce" Text="Điểm số"></asp:Label>
            </td>
            <td>
                <tlk:RadNumericTextBox runat="server" ID="rtxtScore">
                </tlk:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <asp:Label runat="server" ID="lbContent" Text="Nội dung đào tạo"></asp:Label>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtContent">
                </tlk:RadTextBox>
            </td>
            <td class="lb">
                <asp:Label runat="server" ID="lbTrainingType" Text="Loại hình đào tạo"></asp:Label>
            </td>
            <td>
                <tlk:RadTextBox ID="txtTraining_Name" runat="server">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <asp:Label runat="server" ID="lbTrainingForm" Text="Hình thức đào tạo"></asp:Label>
            </td>
            <td>
                <tlk:RadComboBox ID="cboTrainingForm" runat="server">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <asp:Label runat="server" ID="lbBangCap" Text="Loại chứng chỉ"></asp:Label>
            </td>
            <td>
                <tlk:RadComboBox ID="cboBangCap" runat="server" Width="160px" AutoPostBack="true"
                    CausesValidation="false">
                </tlk:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Mã số chứng chỉ")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtCode_certificate">
                </tlk:RadTextBox>
            </td>
            <td>
            </td>
            <td>
                <asp:Label runat="server" ID="lbRenew" Text="Cần gia hạn"></asp:Label>
                <asp:CheckBox ID="is_Renew" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Hiệu lực chứng chỉ từ")%>
                <span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadDatePicker runat="server" ID="rdEffectFrom">
                </tlk:RadDatePicker>
                <asp:RequiredFieldValidator ID="rqEffectFrom" ControlToValidate="rdEffectFrom" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải chọn ngày hiệu lực %>" ToolTip="<%$ Translate: Bạn phải chọn ngày hiệu lực %>">
                </asp:RequiredFieldValidator>
            </td>
            <td class="lb">
                <%# Translate("Hiệu lực chứng chỉ đến")%>
                <span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadDatePicker runat="server" ID="rdEffectTo">
                </tlk:RadDatePicker>
                <asp:RequiredFieldValidator ID="rqEffectTo" ControlToValidate="rdEffectTo" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải chọn ngày hiệu lực %>" ToolTip="<%$ Translate: Bạn phải chọn ngày hiệu lực %>">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Xếp loại tốt nghiệp")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtResultTrain">
                </tlk:RadTextBox>
            </td>
            <td class="lb">
                <%# Translate("Năm tốt nghiệp")%>
                <span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadNumericTextBox ID="txtYear" runat="server" NumberFormat-DecimalDigits="1"
                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                    MaxValue="9999" SkinID="Number" CausesValidation="false">
                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                </tlk:RadNumericTextBox>
                <asp:RequiredFieldValidator ID="rqYear" ControlToValidate="txtYear" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải chọn tên trường %>" ToolTip="<%$ Translate: Bạn phải chọn tên trường %>">
                </asp:RequiredFieldValidator>
            </td>
            <td style="display: none" class="lb">
                <%# Translate("Tập tin đính kèm")%>
            </td>
            <td style="display: none">
                <tlk:RadTextBox ID="txtUploadFile" runat="server">
                </tlk:RadTextBox>
            </td>
            <td style="display: none">
                <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false" />
                <tlk:RadButton runat="server" ID="btnDownload" Text="<%$ Translate: Tải xuống%>"
                    CausesValidation="false" OnClientClicked="rbtClicked" EnableViewState="false">
                </tlk:RadButton>
            </td>
            <td style="display: none" class="lb">
                <%# Translate("Ngày nhận bằng")%>
            </td>
            <td style="display: none">
                <tlk:RadDatePicker runat="server" ID="rdDayGra">
                </tlk:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Ghi chú")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtGhichu" SkinID="Textbox1023">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td class="lb" style="display: none">
                <%# Translate("Ghi chú")%>
            </td>
            <td style="display: none">
                <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr style="visibility: hidden">
            <td class="lb">
                <tlk:RadTextBox ID="txtRemindLink" runat="server">
                </tlk:RadTextBox>
            </td>
        </tr>
    </table>
    <div style="display:none">
    <tlk:RadGrid PageSize="50" ID="rgCetificateEdit" runat="server" Height="500px" Width="100%">
        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_NAME,SPECIALIZED_TRAIN,TYPE_TRAIN_NAME,RESULT_TRAIN,CERTIFICATE,RECEIVE_DEGREE_DATE,EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,FK_PKEY,FORM_TRAIN_ID,TYPE_TRAIN_ID,CERTIFICATE_ID,IS_RENEWED,STATUS"
            ClientDataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_NAME,SPECIALIZED_TRAIN,TYPE_TRAIN_NAME,RESULT_TRAIN,CERTIFICATE,RECEIVE_DEGREE_DATE,EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,FK_PKEY,FORM_TRAIN_ID,TYPE_TRAIN_ID,CERTIFICATE_ID,IS_RENEWED,STATUS"
            Caption="<%$ Translate: Thông tin chỉnh sửa %>">
            <Columns>
                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                </tlk:GridClientSelectColumn>
                <%--<tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Wrap="False" Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>--%>
                <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Display="false">
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="Từ tháng" UniqueName="FROM_DATE"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                    Visible="true" DataFormatString="{0:MM/yyyy}">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="Tới tháng" UniqueName="TO_DATE"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:MM/yyyy}"
                    CurrentFilterFunction="EqualTo" Visible="true">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridBoundColumn DataField="NAME_SHOOLS" HeaderText="Tên trường" UniqueName="NAME_SHOOLS"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="true">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="SPECIALIZED_TRAIN" HeaderText="Chuyên ngành" UniqueName="SPECIALIZED_TRAIN"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="true">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="TYPE_TRAIN_NAME" HeaderText="Loại hình đào tạo" UniqueName="TYPE_TRAIN_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="true">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="FORM_TRAIN_NAME" HeaderText="Hình thức đào tạo" UniqueName="FORM_TRAIN_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="true">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="CODE_CERTIFICATE" HeaderText="Mã số chứng chỉ" UniqueName="CODE_CERTIFICATE"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="true">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="Loại chứng chỉ" UniqueName="CERTIFICATE"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="true">
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECTIVE_DATE_FROM"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                    UniqueName="EFFECTIVE_DATE_FROM">
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EFFECTIVE_DATE_TO"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                    UniqueName="EFFECTIVE_DATE_TO">
                </tlk:GridDateTimeColumn>
                   <tlk:GridBoundColumn DataField="RENEWED_NAME" HeaderText="Cần gia hạn"
                        UniqueName="RENEWED_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="YEAR_GRA" HeaderText="Năm tốt nghiệp" UniqueName="YEAR_GRA"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="true">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="LEVEL_NAME" HeaderText="Trình độ" UniqueName="LEVEL_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="SCORE" HeaderText="Điểm số" UniqueName="SCORE" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="CONTENT_TRAIN" HeaderText="Nội dung đào tạo" UniqueName="CONTENT_TRAIN"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="RESULT_TRAIN" HeaderText="Xếp loại tốt nghiệp" UniqueName="RESULT_TRAIN"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="REMARK" HeaderText="Ghi chú" UniqueName="REMARK"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME" UniqueName="STATUS_NAME"
                    SortExpression="STATUS_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="TRUE">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="UPLOAD_FILE" UniqueName="UPLOAD_FILE"
                    SortExpression="UPLOAD_FILE" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="FILE_NAME" UniqueName="FILE_NAME"
                    SortExpression="FILE_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
        </div>

    <tlk:RadGrid PageSize="50" ID="rgCetificate" runat="server"  Width="100%">
        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_NAME,SPECIALIZED_TRAIN,TYPE_TRAIN_NAME,RESULT_TRAIN,CERTIFICATE,RECEIVE_DEGREE_DATE,EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,FORM_TRAIN_ID,TYPE_TRAIN_ID,CERTIFICATE_ID,IS_RENEWED,LEVEL_ID,LEVEL_NAME,CERTIFICATE_CODE,NOTE,POINT_LEVEL,CONTENT_LEVEL"
            ClientDataKeyNames="ID,EMPLOYEE_ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_NAME,SPECIALIZED_TRAIN,TYPE_TRAIN_NAME,RESULT_TRAIN,CERTIFICATE,RECEIVE_DEGREE_DATE,EFFECTIVE_DATE_FROM,EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,FORM_TRAIN_ID,TYPE_TRAIN_ID,CERTIFICATE_ID,IS_RENEWED,LEVEL_ID,LEVEL_NAME,CERTIFICATE_CODE,NOTE,POINT_LEVEL,CONTENT_LEVEL">
            <Columns>
                <%--<tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Wrap="False" Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>--%>
                <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Display="false">
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="Từ tháng" UniqueName="FROM_DATE"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                    Visible="true" DataFormatString="{0:MM/yyyy}">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="Tới tháng" UniqueName="TO_DATE"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:MM/yyyy}"
                    CurrentFilterFunction="EqualTo" Visible="true">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
                </tlk:GridDateTimeColumn>
                <tlk:GridBoundColumn DataField="CERTIFICATE_NAME" HeaderText="Tên bằng cấp/Chứng chỉ" UniqueName="CERTIFICATE_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    >
                </tlk:GridBoundColumn>

                <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="Loại bằng cấp/ chứng chỉ" UniqueName="CERTIFICATE"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="YEAR_GRA" HeaderText="Năm tốt nghiệp" UniqueName="YEAR_GRA"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="true">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="POINT_LEVEL" HeaderText="Điểm số" UniqueName="POINT_LEVEL"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    >
                </tlk:GridBoundColumn>
                
                <tlk:GridBoundColumn DataField="RESULT_TRAIN" HeaderText="Xếp loại tốt nghiệp" UniqueName="RESULT_TRAIN"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="true">
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECTIVE_DATE_FROM"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                    UniqueName="EFFECTIVE_DATE_FROM">
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EFFECTIVE_DATE_TO"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                    UniqueName="EFFECTIVE_DATE_TO">
                </tlk:GridDateTimeColumn>
                <%--<tlk:GridBoundColumn DataField="NAME_SHOOLS" HeaderText="Tên trường" UniqueName="NAME_SHOOLS"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                </tlk:GridBoundColumn>--%>
                <tlk:GridBoundColumn DataField="SPECIALIZED_TRAIN" HeaderText="Chuyên ngành" UniqueName="SPECIALIZED_TRAIN"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                </tlk:GridBoundColumn>
                <%--<tlk:GridBoundColumn DataField="TYPE_TRAIN_NAME" HeaderText="Loại hình đào tạo" UniqueName="TYPE_TRAIN_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                </tlk:GridBoundColumn>--%>
                <tlk:GridBoundColumn DataField="FORM_TRAIN_NAME" HeaderText="Hình thức đào tạo" UniqueName="FORM_TRAIN_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                </tlk:GridBoundColumn>
                
                <%--<tlk:GridBoundColumn DataField="CERTIFICATE_CODE" HeaderText="Mã chứng chỉ" UniqueName="CERTIFICATE_CODE"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                </tlk:GridBoundColumn>--%>
                
                
                <%--<tlk:GridBoundColumn DataField="RENEWED_NAME" HeaderText="Cần gia hạn" UniqueName="RENEWED_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                </tlk:GridBoundColumn>--%>
                
                <tlk:GridBoundColumn DataField="LEVEL_NAME" HeaderText="Trình độ" UniqueName="LEVEL_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="TRAIN_PLACE_NAME" HeaderText="Nơi đào tạo" UniqueName="TRAIN_PLACE_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="true">
                </tlk:GridBoundColumn>
                
                <tlk:GridBoundColumn DataField="CONTENT_LEVEL" HeaderText="Nội dung đào tạo" UniqueName="CONTENT_LEVEL"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="true">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn DataField="NOTE" HeaderText="Ghi chú" UniqueName="NOTE"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
                <%--  <tlk:GridBoundColumn DataField="RENEWED_NAME" HeaderText="Cần gia hạn" UniqueName="RENEWED_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                </tlk:GridBoundColumn>--%>
                <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="UPLOAD_FILE" UniqueName="UPLOAD_FILE"
                    SortExpression="UPLOAD_FILE" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="FILE_NAME" UniqueName="FILE_NAME"
                    SortExpression="FILE_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    Visible="false">
                </tlk:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <Common:ctrlUpload ID="ctrlUpload1" runat="server" />
    <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function clientButtonClicking(sender, args) {

            }

            var enableAjax = true;
            function onRequestStart(sender, eventArgs) {
                eventArgs.set_enableAjax(enableAjax);
                enableAjax = true;
            }

            function OnClientSelectedIndexChanged(sender, eventArgs) {
                var id = sender.get_id();
                var cbo;

            }

            function clearSelectRadcombo(cbo) {
                if (cbo) {
                    cbo.clearItems();
                    cbo.clearSelection();
                    cbo.set_text('');
                }
            }
            function clearSelectRadtextbox(cbo) {
                if (cbo) {
                    cbo.clear();
                }
            }

            function OnClientItemsRequesting(sender, eventArgs) {

            }
            function rbtClicked(sender, eventArgs) {
                enableAjax = false;
            }
            function rgCetificateEditRadGridDeSelecting() { }
            function rgCetificateRadGridDeSelecting() { }
            function rgCetificateEditOnClientRowSelected() { }
            function rgCetificateEditRadGridSelecting() { }
            function rgCetificateOnClientRowSelected() { }
            function rgCetificateRadGridSelecting() { }
            var winH;
            var winW;

            function SizeToFitMain() {
                Sys.Application.remove_load(SizeToFitMain);
                winH = $(window).height() - 210;
                winW = $(window).width() - 90;
                $("#ctl00_MainContent_ctrlInfoCertificate_rgCetificate").stop().animate({ height: winH }, 0);
                $("#ctl00_MainContent_ctrlInfoCertificate_rgCetificate_GridData").stop().animate({ height: winH-94 }, 0);
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
</div>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
