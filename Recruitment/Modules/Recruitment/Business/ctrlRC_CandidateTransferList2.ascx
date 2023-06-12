<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CandidateTransferList2.ascx.vb"
    Inherits="Recruitment.ctrlRC_CandidateTransferList2" %>
<%@ Import Namespace="Common" %>
<p>
    <br />
</p>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
       <tlk:RadGrid ID="rgCandidateList" runat="server" AutoGenerateColumns="False" AllowPaging="True" EnablePostBackOnRowClick="true"
                Height="400px" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings AllowColumnsReorder="True"  EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling UseStaticHeaders="true" AllowScroll="True" />
                    <Resizing AllowColumnResize="true" />
                </ClientSettings>
            <MasterTableView DataKeyNames="ID,CANDIDATE_CODE" ClientDataKeyNames="ID,CANDIDATE_CODE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CANDIDATE_CODE"
                        UniqueName="CANDIDATE_CODE" SortExpression="CANDIDATE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME_VN"
                        UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                        UniqueName="GENDER_NAME" SortExpression="GENDER_NAME" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                        UniqueName="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="BIRTH_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CMND %>" DataField="ID_NO" UniqueName="ID_NO"
                        SortExpression="ID_NO" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="ID_DATE"
                        UniqueName="ID_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="ID_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi sinh %>" DataField="BIRTH_PLACE_NAME"
                        UniqueName="BIRTH_PLACE_NAME" SortExpression="BIRTH_PLACE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate:Địa chỉ hiện tại %>" DataField="PER_ADDRESS"
                        UniqueName="PER_ADDRESS" SortExpression="PER_ADDRESS" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate:Số điện thoại %>" DataField="CONTACT_MOBILE"
                        UniqueName="CONTACT_MOBILE" SortExpression="CONTACT_MOBILE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate:Thông tin đánh giá %>" DataField="ASSESS_NAME"
                        UniqueName="ASSESS_NAME" SortExpression="ASSESS_NAME" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Người phỏng vấn %>" DataField="ASSESSOR"
                        UniqueName="ASSESSOR" SortExpression="ASSESSOR" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="100px" />
            <ItemStyle Width="100px" />
           </tlk:RadGrid>
            
    </tlk:RadPane>
             
          <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
               <tlk:RadGrid ID="rgResult" runat="server" Height="400px">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="CANDIDATE_CODE"
                        UniqueName="CANDIDATE_CODE" SortExpression="CANDIDATE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME_VN"
                        UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên môn thi %>" DataField="EXAMS_NAME"
                        UniqueName="EXAMS_NAME" SortExpression="EXAMS_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thang điểm %>" DataField="POINT_LADDER"
                        UniqueName="POINT_LADDER" SortExpression="POINT_LADDER" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm đạt %>" DataField="POINT_PASS" UniqueName="POINT_PASS"
                        SortExpression="POINT_PASS" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="POINT_RESULT" UniqueName="POINT_RESULT"
                        SortExpression="POINT_RESULT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate:Nhận xét %>" DataField="COMMENT_INFO"
                        UniqueName="COMMENT_INFO" SortExpression="COMMENT_INFO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate:Thông tin đánh giá %>" DataField="ASSESSMENT_INFO"
                        UniqueName="ASSESSMENT_INFO" SortExpression="ASSESSMENT_INFO" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Người phỏng vấn %>" DataField="EMPLOYEE_NAME"
                        UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="100px" />
            <ItemStyle Width="100px" />
           </tlk:RadGrid>
          </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
            <asp:HiddenField ID="hidOrg" runat="server" />
        <asp:HiddenField ID="hidTitle" runat="server" />
        <asp:HiddenField ID="hidProgramID" runat="server" />
        <tlk:RadTabStrip ID="rtabCandidateTransfer"  runat="server"
            CausesValidation="false" MultiPageID="RadMultiPage1" AutoPostBack="false">
            <Tabs>
                <tlk:RadTab runat="server" ID="rtIdElect" PageViewID="rpvElect" Text="<%$ Translate: Xác nhận trúng tuyển %>" Selected="True">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtIdResult" PageViewID="rpvResult" Text="<%$ Translate: Kết quả môn thi %>">
                </tlk:RadTab>
                <tlk:RadTab runat="server" ID="rtIdAspiration" PageViewID="rpvAspiration" Text="<%$ Translate: Nguyện vọng %>">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
           <tlk:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server" Width="100%" >
            
            <tlk:RadPageView ID="rpvElect" runat="server" Width="100%">
       </tlk:RadPageView>
            <tlk:RadPageView ID="rpvResult" runat="server" Width="100%">
  

     
            </tlk:RadPageView>
          <tlk:RadPageView ID="rpvAspiration" runat="server" Width="100%">
           <tlk:RadGrid ID="rgAspiration" runat="server" Height="400px">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian làm việc %>" DataField="TIME_WORK"
                        UniqueName="TIME_WORK" SortExpression="TIME_WORK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi làm việc %>" DataField="PLACE_WORK"
                        UniqueName="PLACE_WORK" SortExpression="PLACE_WORK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhận việc từ ngày %>" DataField="RECEIVE_FROM"
                        UniqueName="RECEIVE_FROM" SortExpression="RECEIVE_FROM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhận việc đến ngày %>" DataField="RECEIVE_TO"
                        UniqueName="RECEIVE_TO" SortExpression="RECEIVE_TO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thử việc từ ngày %>" DataField="PROBATION_FROM" UniqueName="PROBATION_FROM"
                        SortExpression="PROBATION_FROM" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thử việc đến ngày %>" DataField="PROBATION_TO" UniqueName="PROBATION_TO"
                        SortExpression="PROBATION_TO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bắt đầu làm việc %>" DataField="STARTDATE_WORK"
                        UniqueName="STARTDATE_WORK" SortExpression="STARTDATE_WORK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức lương thử việc %>" DataField="PROBATION_SALARY"
                        UniqueName="PROBATION_SALARY" SortExpression="PROBATION_SALARY" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Mức lương chính thức %>" DataField="OFFICAL_SALARY"
                        UniqueName="OFFICAL_SALARY" SortExpression="OFFICAL_SALARY" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đề nghị khác %>" DataField="OTHER_SUGGESTIONS"
                        UniqueName="OTHER_SUGGESTIONS" SortExpression="OTHER_SUGGESTIONS" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="100px" />
            <ItemStyle Width="100px" />
           </tlk:RadGrid>
            </tlk:RadPageView>
            </tlk:RadMultiPage>
    </tlk:RadPane>
  
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">



        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadScriptBlock>
