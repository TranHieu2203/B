<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ApproveCertificateEmployee.ascx.vb"
    Inherits="Profile.ctrlHU_ApproveCertificateEmployee" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWorkings" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="55px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbRemark" runat="server" Text="Lý do không phê duyệt"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%" MaxLength="250"
                                Rows="1" Height="38px">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 500px;">
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS,FILE_NAME,UPLOAD_FILE" ClientDataKeyNames="ID,STATUS,FILE_NAME,UPLOAD_FILE">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="Loại bằng cấp/ chứng chỉ" UniqueName="CERTIFICATE"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
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
                            <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Display="false">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="FROM_DATE" HeaderText="Thời gian đào tạo từ" UniqueName="FROM_DATE"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                Visible="true" DataFormatString="{0:MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="TO_DATE" HeaderText="Thời gian đào tạo đến" UniqueName="TO_DATE"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:MM/yyyy}"
                                CurrentFilterFunction="EqualTo" Visible="true">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="GRADUATE_SCHOOL_NAME" HeaderText="Trường đào tạo" UniqueName="GRADUATE_SCHOOL_NAME"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="MAJOR_NAME" HeaderText="Trình độ chuyên môn"
                                UniqueName="MAJOR_NAME" SortExpression="MAJOR_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="SPECIALIZED_TRAIN" HeaderText="Chuyên ngành" UniqueName="SPECIALIZED_TRAIN"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="LEVEL_NAME" HeaderText="Trình độ" UniqueName="LEVEL_NAME"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="POINT_LEVEL" HeaderText="Điểm số" UniqueName="POINT_LEVEL" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="CONTENT_LEVEL" HeaderText="Nội dung đào tạo" UniqueName="CONTENT_LEVEL"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="FORM_TRAIN_NAME" HeaderText="Hình thức đào tạo" UniqueName="FORM_TRAIN_NAME"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Hiệu lực chứng chỉ từ" DataField="EFFECTIVE_DATE_FROM"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                                UniqueName="EFFECTIVE_DATE_FROM" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Hiệu lực chứng chỉ đến" DataField="EFFECTIVE_DATE_TO"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                                UniqueName="EFFECTIVE_DATE_TO" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="RESULT_TRAIN" HeaderText="Xếp loại tốt nghiệp" UniqueName="RESULT_TRAIN"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="YEAR_GRA" HeaderText="Năm tốt nghiệp" UniqueName="YEAR_GRA"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="NOTE" HeaderText="Ghi chú" UniqueName="NOTE"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridCheckBoxColumn DataField="IS_MAIN" HeaderText="Là bằng chính" AllowFiltering="false"
                                UniqueName="IS_MAIN" SortExpression="IS_MAIN">
                            </tlk:GridCheckBoxColumn> 
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME" UniqueName="STATUS_NAME"
                                SortExpression="STATUS_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="TRUE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="UPLOAD_FILE" DataField="UPLOAD_FILE" UniqueName="UPLOAD_FILE"
                                SortExpression="UPLOAD_FILE" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="false">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="FILE_NAME" DataField="FILE_NAME" UniqueName="FILE_NAME"
                                SortExpression="FILE_NAME" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                Visible="false">
                            </tlk:GridBoundColumn>
                            <tlk:GridButtonColumn UniqueName="DowloadCommandColumn" Text="Download" CommandName="Dowload" HeaderText="Tải" 
                                ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"  ButtonType="ImageButton"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="40px">
                            </tlk:GridButtonColumn>
                            <tlk:GridButtonColumn UniqueName="ViewCommandColumn" Text="View" CommandName="View" HeaderText="View" 
                                ImageUrl="~/Static/Images/Icons/16/ViewImgOrg.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            </tlk:GridButtonColumn>--%>
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {

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
