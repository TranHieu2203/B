<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CanDtlTraining.ascx.vb"
    Inherits="Recruitment.ctrlRC_CanDtlTraining" %>
<%@ Register Src="../Shared/ctrlRC_CanBasicInfo.ascx" TagName="ctrlRC_CanBasicInfo"
    TagPrefix="Recruitment" %>
<asp:HiddenField ID="hidCandidateCode" runat="server" />
<asp:HiddenField ID="hidSelectedtrainID" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Orientation="Horizontal" Scrolling="None">
    <tlk:RadPane ID="RadPane2" runat="server" Height="50px" Scrolling="None">
        <recruitment:ctrlrc_canbasicinfo runat="server" id="ctrlRC_CanBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="ToolbarPane" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 130px">
                    <asp:Label runat="server" ID="lbTuThang" Text="Từ tháng"></asp:Label>
                </td>
                <td>
                    <tlk:RadMonthYearPicker ID="rdTuThang" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                        DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadMonthYearPicker>
                    <asp:RequiredFieldValidator ID="rqTuThang" ControlToValidate="rdTuThang" runat="server"
                        ErrorMessage="Bạn phải nhập từ tháng." ToolTip="Bạn phải nhập từ tháng."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 200px">
                    <asp:Label runat="server" ID="lbToiThang" Text="Đến tháng"></asp:Label>
                </td>
                <td>
                    <tlk:RadMonthYearPicker ID="rdToiThang" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                        DateInput-DateFormat="dd/MM/yyyy" >
                    </tlk:RadMonthYearPicker>
                    <asp:CustomValidator ID="cvalToiThang" runat="server" ErrorMessage="Từ tháng không đươc lớn hơn tới tháng."
                        ToolTip="Từ tháng không đươc lớn hơn tới tháng.">
                    </asp:CustomValidator>
                    <%--<asp:RequiredFieldValidator ID="rqToiThang" ControlToValidate="rdToiThang" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tới tháng. %>" ToolTip="<%$ Translate: Bạn phải nhập tới tháng. %>"> </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTrainingSchool" Text="Tên trường"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTrainingSchool" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTrainingSchool"
                        runat="server" ErrorMessage="Bạn phải nhập tên trường." ToolTip="Bạn phải nhập tên trường."> </asp:RequiredFieldValidator>
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
                    <asp:Label runat="server" ID="Label1" Text="Trình độ"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLevelId" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label2" Text="Điểm số"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtxtPointLevel" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <asp:Label runat="server" ID="Label4" Text="Nội dung đào tạo"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rtxtContentLevel" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>

            <tr>
                 <td class="lb">
                    <asp:Label runat="server" ID="lbTrainingType" Text="Loại hình đào tạo"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTrainingType" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTrainingForm" Text="Hình thức đào tạo"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTrainingForm" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbBangCap" Text="Loại chứng chỉ"></asp:Label>
                </td>
                <td>
                     <tlk:RadComboBox ID="cboRemark" runat="server" Width="160px" AutoPostBack="true" CausesValidation="false">
                     </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label5" Text="Mã số chứng chỉ"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCertificateCode" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server"  ID="lbFrom" Text="Hiệu lực chứng chỉ từ"></asp:Label>
                </td>
                <td >
                    <tlk:RadDatePicker ID="rdFrom" AutoPostBack="true" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdFrom" 
                        runat="server" ErrorMessage="Bạn phải nhập ngày hiệu lực chứng chỉ." ToolTip="Bạn phải nhập ngày hiệu lực chứng chỉ."> </asp:RequiredFieldValidator>
                </td>
                 <%-- <td> 
                    <tlk:RadDatePicker ID="rdStartDate" runat="server" AutoPostBack="True">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdStartDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực chứng chỉ. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực chứng chỉ. %>"> </asp:RequiredFieldValidator>
                   <asp:CustomValidator ID="CompareStartDate" runat="server" ErrorMessage="<%$ Translate: Ngày bắt đầu phải lớn hơn ngày kết thúc gần nhất %>"
                        ToolTip="<%$ Translate: Ngày bắt đầu phải lớn hơn ngày kết thúc gần nhất %>">
                    </asp:CustomValidator>
                </td>--%>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTo" Text="Hiệu lực chứng chỉ đến"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTo" AutoPostBack="true" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdTo" 
                        runat="server" ErrorMessage="Bạn phải nhập ngày hết hiệu lực chứng chỉ." ToolTip="Bạn phải nhập ngày hết hiệu lực chứng chỉ."> </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="Ngày hiệu lực chứng chỉ phải nhỏ hơn ngày hết hiệu lực chứng chỉ"
                        ErrorMessage="Ngày hiệu lực chứng chỉ phải nhỏ hơn ngày hết hiệu lực chứng chỉ"
                        Type="Date" Operator="GreaterThan" ControlToCompare="rdFrom" ControlToValidate="rdTo"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbKetQua" Text="Xếp loại tốt nghiệp"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtKetQua" SkinID="Textbox250" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label3" Text="Cần gia hạn"></asp:Label>
                </td>
                 <td >
                    <asp:CheckBox ID="chkTerminate" runat="server" />
                 </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbGraduateYear" Text="Năm tốt nghiệp"></asp:Label>                  
                </td>
                <td>
                    <%--<tlk:RadNumericTextBox runat="server" ID="rntGraduateYear1" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSizes="4" MinValue="1990" MaxValue="9999">
                    </tlk:RadNumericTextBox>--%>

                     <tlk:RadNumericTextBox ID="rntGraduateYear" runat="server" NumberFormat-DecimalDigits="1"
                    NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                    MaxValue="9999" SkinID="Number" CausesValidation="false">
                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                </tlk:RadNumericTextBox>


                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntGraduateYear"
                        runat="server" ErrorMessage="Bạn phải nhập năm tốt nghiệp."
                        ToolTip="Bạn phải nhập năm tốt nghiệp."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbRemark" Text="Đính kèm tập tin"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRemark" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td colspan="3">
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <tlk:RadButton runat="server" ID="btnDownload" Text="Tải xuống"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>                
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" runat="server" Width="100%">
                    </tlk:RadTextBox>

                </td>
            </tr>
            <tr style="display:none">                
                <td class="lb">
                    <asp:Label runat="server" ID="lbReceiveDegree" Text="Ngày nhận bằng"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdReceiveDegree" runat="server">
                    </tlk:RadDatePicker>

                </td>
            </tr>
            <tr style="visibility:hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="RadPane4" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgEmployeeTrain" runat="server" AllowMultiRowSelection="true" Height="100%"
            AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID, FROM_DATE,TO_DATE, YEAR_GRA, NAME_SHOOLS, FORM_TRAIN_ID, SPECIALIZED_TRAIN, TYPE_TRAIN_ID, RESULT_TRAIN, CERTIFICATE, RECEIVE_DEGREE_DATE, EFFECTIVE_DATE_FROM, EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,IS_RENEWED,CERTIFICATE_ID,TYPE_TRAIN_NAME,LEVEL_ID,POINT_LEVEL,CONTENT_LEVEL,NOTE,CERTIFICATE_CODE,TYPE_TRAIN_NAME" 
            ClientDataKeyNames="ID, FROM_DATE,TO_DATE, YEAR_GRA, NAME_SHOOLS, FORM_TRAIN_ID, SPECIALIZED_TRAIN, TYPE_TRAIN_ID, RESULT_TRAIN, CERTIFICATE, RECEIVE_DEGREE_DATE, EFFECTIVE_DATE_FROM, EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,IS_RENEWED,CERTIFICATE_ID,TYPE_TRAIN_NAME,LEVEL_ID,POINT_LEVEL,CONTENT_LEVEL,NOTE,CERTIFICATE_CODE,TYPE_TRAIN_NAME">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Display="false"></tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="Từ tháng"
                        UniqueName="FROM_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        Visible="true" DataFormatString="{0:MM/yyyy}">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="Tới tháng"
                        UniqueName="TO_DATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:MM/yyyy}"
                        CurrentFilterFunction="EqualTo" Visible="true">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn DataField="NAME_SHOOLS" HeaderText="Tên trường"
                        UniqueName="NAME_SHOOLS" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SPECIALIZED_TRAIN" HeaderText="Chuyên ngành"
                        UniqueName="SPECIALIZED_TRAIN" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TYPE_TRAIN_NAME" HeaderText="Loại hình đào tạo"
                        UniqueName="TYPE_TRAIN_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FORM_TRAIN_NAME" HeaderText="Hình thức đào tạo"
                        UniqueName="FORM_TRAIN_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CERTIFICATE_CODE" HeaderText="Mã chứng chỉ"
                        UniqueName="CERTIFICATE_CODE" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="Loại chứng chỉ"
                        UniqueName="CERTIFICATE" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains">
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
                    <tlk:GridNumericColumn DataField="YEAR_GRA" HeaderText="Năm tốt nghiệp"
                        UniqueName="YEAR_GRA" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        Visible="true">
                    </tlk:GridNumericColumn>

                   <%-- <tlk:GridBoundColumn DataField="RESULT_TRAIN" HeaderText="Kết quả"
                        UniqueName="RESULT_TRAIN" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="Bằng cấp"
                        UniqueName="CERTIFICATE" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="RECEIVE_DEGREE_DATE" HeaderText="Ngày nhận bằng"
                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                        UniqueName="RECEIVE_DEGREE_DATE">
                    </tlk:GridBoundColumn>
                      <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="UPLOAD_FILE"
                        UniqueName="UPLOAD_FILE" SortExpression="UPLOAD_FILE" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false"></tlk:GridBoundColumn>
                         <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="FILE_NAME"
                        UniqueName="FILE_NAME" SortExpression="FILE_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false"></tlk:GridBoundColumn>--%>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane4.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }

    </script>
</tlk:RadCodeBlock>