<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlTrainingOutCompany.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlTrainingOutCompany" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Orientation="Horizontal" Scrolling="None">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
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

                            
                            <tlk:GridBoundColumn DataField="EMPLOYEE_CODE" HeaderText="Mã nhân viên" Visible="false"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="EMPLOYEE_NAME" HeaderText="Tên nhân viên" Visible="false"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="ORG_NAME" HeaderText="Phòng ban" Visible="false"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="TITLE_NAME" HeaderText="Chức danh" Visible="false"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="Loại bằng cấp/ chứng chỉ"
                                UniqueName="CERTIFICATE" SortExpression="CERTIFICATE">
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn DataField="CERTIFICATE_GROUP_NAME" HeaderText="Nhóm chứng chỉ"
                                UniqueName="CERTIFICATE_GROUP_NAME" SortExpression="CERTIFICATE_GROUP_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="CERTIFICATE_TYPE_NAME" HeaderText="Loại chứng chỉ"
                                UniqueName="CERTIFICATE_TYPE_NAME" SortExpression="CERTIFICATE_TYPE_NAME">
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn DataField="CERTIFICATE_NAME" HeaderText="Tên bằng cấp/ chứng chỉ"
                                UniqueName="CERTIFICATE_NAME" SortExpression="CERTIFICATE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn DataField="FROM_DATE" HeaderText="Thời gian đào tạo từ"
                                UniqueName="FROM_DATE" ShowFilterIcon="false" DataFormatString="{0:MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn DataField="TO_DATE" HeaderText="Thời gian đào tạo đến"
                                UniqueName="TO_DATE" ShowFilterIcon="false" DataFormatString="{0:MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridBoundColumn DataField="LEVEL_NAME" HeaderText="Trình độ"
                                UniqueName="LEVEL_NAME" SortExpression="LEVEL_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="SPECIALIZED_TRAIN" HeaderText="Chuyên ngành"
                                UniqueName="SPECIALIZED_TRAIN" SortExpression="SPECIALIZED_TRAIN">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="MAJOR_NAME" HeaderText="Trình độ chuyên môn"
                                UniqueName="MAJOR_NAME" SortExpression="MAJOR_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="YEAR_GRA" HeaderText="Năm tốt nghiệp" UniqueName="YEAR_GRA"
                                ShowFilterIcon="false" SortExpression="YEAR_GRA">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="RESULT_TRAIN" HeaderText="Xếp loại tốt nghiệp" UniqueName="RESULT_TRAIN"
                                ShowFilterIcon="false" SortExpression="RESULT_TRAIN">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn DataField="POINT_LEVEL" HeaderText="Điểm số" UniqueName="POINT_LEVEL"
                                ShowFilterIcon="false" SortExpression="POINT_LEVEL" DataFormatString="{0:N1}">
                            </tlk:GridNumericColumn>
                            <tlk:GridDateTimeColumn HeaderText="Hiệu lực chứng chỉ từ" DataField="EFFECTIVE_DATE_FROM"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                                UniqueName="EFFECTIVE_DATE_FROM">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Hiệu lực chứng chỉ đến" DataField="EFFECTIVE_DATE_TO"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                                UniqueName="EFFECTIVE_DATE_TO">
                            </tlk:GridDateTimeColumn>
                            
                            <tlk:GridBoundColumn DataField="CONTENT_LEVEL" HeaderText="Nội dung đào tạo"
                                UniqueName="CONTENT_LEVEL" SortExpression="CONTENT_LEVEL">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="GRADUATE_SCHOOL_NAME" HeaderText="Trường đào tạo"
                                UniqueName="GRADUATE_SCHOOL_NAME" SortExpression="GRADUATE_SCHOOL_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="FORM_TRAIN_NAME" HeaderText="Hình thức đào tạo"
                                UniqueName="FORM_TRAIN_NAME" SortExpression="FORM_TRAIN_NAME">
                            </tlk:GridBoundColumn>
                            <%--<tlk:GridBoundColumn DataField="TRAIN_PLACE_NAME" HeaderText="Nơi đào tạo"
                                UniqueName="TRAIN_PLACE_NAME" SortExpression="TRAIN_PLACE_NAME">
                            </tlk:GridBoundColumn>--%>
                            <tlk:GridBoundColumn DataField="NOTE" HeaderText="Ghi chú"
                                UniqueName="NOTE" SortExpression="NOTE">
                            </tlk:GridBoundColumn>
                            <tlk:GridCheckBoxColumn DataField="IS_MAIN" HeaderText="Là bằng chính" AllowFiltering="false"
                                UniqueName="IS_MAIN" SortExpression="IS_MAIN">
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn DataField="IS_MAJOR" HeaderText="Chuyên môn cao nhất" AllowFiltering="false"
                                UniqueName="IS_MAJOR" SortExpression="IS_MAJOR">
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridButtonColumn UniqueName="DowloadCommandColumn" Text="Dowload" CommandName="Dowload" HeaderText="Tải" 
                                ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"  ButtonType="ImageButton"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="40px">
                            </tlk:GridButtonColumn>
                            <tlk:GridButtonColumn UniqueName="ViewCommandColumn" Text="View" CommandName="View" HeaderText="View" 
                                ImageUrl="~/Static/Images/Icons/16/ViewImgOrg.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            </tlk:GridButtonColumn>
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
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane4.ClientID%>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane4.ClientID%>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane4.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }

        function <%=ClientID%>_OnClientClose(oWnd, args) {
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(<%=ClientID%>_OnClientClose);
            var arg = args.get_argument();
            if (arg == null) {
                arg = new Object();
                arg.ID = 'Cancel';
            }
            if (arg) {
                var ajaxManager = $find("<%= AjaxManagerId %>");
                ajaxManager.ajaxRequest("<%= ClientID %>_PopupPostback:" + arg.ID);
            }
        }

    </script>
</tlk:RadCodeBlock>
