<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ApproveEmployee_Edit.ascx.vb"
    Inherits="Profile.ctrlHU_ApproveEmployee_Edit" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .hide-button input {
        display: none;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="40px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWorkings" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Lý do không phê duyệt")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%" MaxLength="250">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 500px;"></td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS,EMPLOYEE_CODE,EMPLOYEE_NAME,MARITAL_STATUS,MARITAL_STATUS_NAME,PER_ADDRESS,PER_PROVINCE_NAME,PER_DISTRICT_NAME,PER_WARD_NAME,NAV_ADDRESS,NAV_PROVINCE_NAME,NAV_DISTRICT_NAME,NAV_WARD_NAME,ID_NO,ID_DATE,ID_PLACE_NAME,ID_PLACE,
                    EXPIRE_DATE_IDNO,CONTACT_PER,RELATION_PER_CTR,RELATION_PER_CTR_NAME,CONTACT_PER_MBPHONE,VILLAGE,HOME_PHONE,MOBILE_PHONE,WORK_EMAIL,PER_EMAIL,PERSON_INHERITANCE,BANK_NO,BANK_ID,BANK_NAME,BANK_BRANCH_NAME,FILE_ADDRESS,FILE_BANK,UPLOAD_FILE_ADDRESS,UPLOAD_FILE_BANK,IMAGE,UPLOAD_IMAGE,FILE_CMND,FILE_CMND_BACK,UPLOAD_FILE_CMND,UPLOAD_FILE_CMND_BACK,UPLOAD_FILE_OTHER,FILE_OTHER,MAJOR_NAME,GRADUATE_SCHOOL_NAME,GRADUATION_YEAR"
                        ClientDataKeyNames="ID,STATUS,EMPLOYEE_CODE,EMPLOYEE_NAME,MARITAL_STATUS,MARITAL_STATUS_NAME,PER_ADDRESS,PER_PROVINCE_NAME,PER_DISTRICT_NAME,PER_WARD_NAME,NAV_ADDRESS,NAV_PROVINCE_NAME,NAV_DISTRICT_NAME,NAV_WARD_NAME,ID_NO,ID_DATE,ID_PLACE_NAME,ID_PLACE,
                    EXPIRE_DATE_IDNO,CONTACT_PER,RELATION_PER_CTR,RELATION_PER_CTR_NAME,CONTACT_PER_MBPHONE,VILLAGE,HOME_PHONE,MOBILE_PHONE,WORK_EMAIL,PER_EMAIL,PERSON_INHERITANCE,BANK_NO,BANK_ID,BANK_NAME,BANK_BRANCH_NAME,FILE_ADDRESS,FILE_BANK,UPLOAD_FILE_ADDRESS,UPLOAD_FILE_BANK,IMAGE,UPLOAD_IMAGE,FILE_CMND,FILE_CMND_BACK,UPLOAD_FILE_CMND,UPLOAD_FILE_CMND_BACK,UPLOAD_FILE_OTHER,FILE_OTHER,MAJOR_NAME,GRADUATE_SCHOOL_NAME,GRADUATION_YEAR">
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
                            <tlk:GridButtonColumn UniqueName="ViewCommandColumnImage" Text="ViewImage" CommandName="ViewImage" HeaderText="Hình hồ sơ" 
                                ImageUrl="~/Static/Images/Icons/16/ViewImgOrg.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                            </tlk:GridButtonColumn>
                            <tlk:GridButtonColumn UniqueName="DowloadCommandColumnImage" Text="DownloadImage" CommandName="DowloadImage" HeaderText="Tải hình hồ sơ" 
                                ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"  ButtonType="ImageButton"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="104px">
                            </tlk:GridButtonColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tình trạng hôn nhân %>" DataField="MARITAL_STATUS_NAME"
                                UniqueName="MARITAL_STATUS_NAME" SortExpression="MARITAL_STATUS_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tôn giáo %>" DataField="RELIGION_NAME"
                                UniqueName="RELIGION_NAME" SortExpression="RELIGION_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Dân tộc %>" DataField="NATIVE_NAME"
                                UniqueName="NATIVE_NAME" SortExpression="NATIVE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số CMND %>" DataField="ID_NO"
                                UniqueName="ID_NO" SortExpression="ID_NO">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn UniqueName="ID_DATE" HeaderText="<%$ Translate: Ngày cấp %>"
                                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="ID_DATE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn UniqueName="EXPIRE_DATE_IDNO" HeaderText="<%$ Translate: Ngày hết hiệu lực %>" 
                                ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="EXPIRE_DATE_IDNO" CurrentFilterFunction="EqualTo">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi cấp %>" DataField="ID_PLACE_NAME"
                                UniqueName="ID_PLACE_NAME" SortExpression="ID_PLACE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú CMND/CCCD %>" DataField="NOTE_CHANGE_CMND"
                                UniqueName="NOTE_CHANGE_CMND" SortExpression="NOTE_CHANGE_CMND">
                            </tlk:GridBoundColumn>
                            
                            <tlk:GridButtonColumn UniqueName="DowloadCommandColumnCMND" Text="DownloadCMND mặt trước" CommandName="DowloadCMND" HeaderText="Tải mặt trước" 
                                ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"  ButtonType="ImageButton"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="75px">
                            </tlk:GridButtonColumn>

                            <tlk:GridButtonColumn UniqueName="ViewCommandColumnCMND" Text="ViewCMND mặt trước" CommandName="ViewCMND" HeaderText="Xem mặt trước" 
                                ImageUrl="~/Static/Images/Icons/16/ViewImgOrg.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="75px">
                            </tlk:GridButtonColumn>

                            <tlk:GridButtonColumn UniqueName="DowloadCommandColumnCMNDBack" Text="DownloadCMNDBack mặt sau" CommandName="DowloadCMNDBack" HeaderText="Tải mặt sau" 
                                ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"  ButtonType="ImageButton"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="75px">
                            </tlk:GridButtonColumn>
                            <tlk:GridButtonColumn UniqueName="ViewCommandColumnCMNDBack" Text="ViewCMNDBack mặt sau" CommandName="ViewCMNDBack" HeaderText="Xem mặt sau" 
                                ImageUrl="~/Static/Images/Icons/16/ViewImgOrg.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="75px">
                            </tlk:GridButtonColumn>



                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú thay đổi CMND %>" DataField="CMND_NOTE_CHANGE"
                                UniqueName="CMND_NOTE_CHANGE" SortExpression="CMND_NOTE_CHANGE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người liên hệ %>" DataField="CONTACT_PER"
                                UniqueName="CONTACT_PER" SortExpression="CONTACT_PER">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mối quan hệ %>" DataField="RELATION_PER_CTR_NAME"
                                UniqueName="RELATION_PER_CTR_NAME" SortExpression="RELATION_PER_CTR_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại người liên hệ %>" DataField="CONTACT_PER_MBPHONE"
                                UniqueName="CONTACT_PER_MBPHONE" SortExpression="CONTACT_PER_MBPHONE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ liên hệ %>" DataField="ADDRESS_PER_CTR"
                                UniqueName="ADDRESS_PER_CTR" SortExpression="ADDRESS_PER_CTR">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số hộ khẩu %>" DataField="NO_HOUSEHOLDS"
                                UniqueName="NO_HOUSEHOLDS" SortExpression="NO_HOUSEHOLDS">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ thường trú %>" DataField="PER_ADDRESS"
                                UniqueName="PER_ADDRESS" SortExpression="PER_ADDRESS">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thành phố %>" DataField="PER_PROVINCE_NAME"
                                UniqueName="PER_PROVINCE_NAME" SortExpression="PER_PROVINCE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Quận huyện %>" DataField="PER_DISTRICT_NAME"
                                UniqueName="PER_DISTRICT_NAME" SortExpression="PER_DISTRICT_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xã phường %>" DataField="PER_WARD_NAME"
                                UniqueName="PER_WARD_NAME" SortExpression="PER_WARD_NAME">
                            </tlk:GridBoundColumn>
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ liên lạc %>" DataField="NAV_ADDRESS"
                                UniqueName="NAV_ADDRESS" SortExpression="NAV_ADDRESS">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thành phố %>" DataField="NAV_PROVINCE_NAME"
                                UniqueName="NAV_PROVINCE_NAME" SortExpression="NAV_PROVINCE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Quận huyện %>" DataField="NAV_DISTRICT_NAME"
                                UniqueName="NAV_DISTRICT_NAME" SortExpression="NAV_DISTRICT_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xã phường %>" DataField="NAV_WARD_NAME"
                                UniqueName="NAV_WARD_NAME" SortExpression="NAV_WARD_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridButtonColumn UniqueName="DowloadCommandColumnAddress" Text="DownloadAddress" CommandName="DowloadAddress" HeaderText="Tải" 
                                ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"  ButtonType="ImageButton"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="40px">
                            </tlk:GridButtonColumn>
                            <tlk:GridButtonColumn UniqueName="ViewCommandColumnAddress" Text="ViewAddress" CommandName="ViewAddress" HeaderText="View" 
                                ImageUrl="~/Static/Images/Icons/16/ViewImgOrg.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            </tlk:GridButtonColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại cố định %>" DataField="HOME_PHONE"
                                UniqueName="HOME_PHONE" SortExpression="HOME_PHONE">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại di động %>" DataField="MOBILE_PHONE"
                                UniqueName="MOBILE_PHONE" SortExpression="MOBILE_PHONE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Email công ty %>" DataField="WORK_EMAIL" HeaderStyle-Width="200px"
                                UniqueName="WORK_EMAIL" SortExpression="WORK_EMAIL">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Email cá nhân %>" DataField="PER_EMAIL" HeaderStyle-Width="200px"
                                UniqueName="PER_EMAIL" SortExpression="PER_EMAIL">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên người thụ hưởng %>" DataField="PERSON_INHERITANCE"
                                UniqueName="PERSON_INHERITANCE" SortExpression="PERSON_INHERITANCE">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Số TK chuyển khoản %>" DataField="BANK_NO"
                                UniqueName="BANK_NO" SortExpression="BANK_NO">
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngân hàng %>" DataField="BANK_NAME"
                                UniqueName="BANK_NAME" SortExpression="BANK_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chi nhánh ngân hàng %>" DataField="BANK_BRANCH_NAME"
                                UniqueName="BANK_BRANCH_NAME" SortExpression="BANK_BRANCH_NAME">
                            </tlk:GridBoundColumn>   
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ tin học cơ bản %>" DataField="COMPUTER_RANK_NAME" Visible="false"
                                UniqueName="COMPUTER_RANK_NAME" SortExpression="COMPUTER_RANK_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại chứng chỉ %>" DataField="COMPUTER_MARK_NAME" Visible="false"
                                UniqueName="COMPUTER_MARK_NAME" SortExpression="COMPUTER_MARK_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ tin học ứng dụng %>" DataField="COMPUTER_CERTIFICATE" Visible="false"
                                UniqueName="COMPUTER_CERTIFICATE" SortExpression="COMPUTER_CERTIFICATE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngoại ngữ %>" DataField="LANGUAGE_NAME" Visible="false"
                                UniqueName="LANGUAGE_NAME" SortExpression="LANGUAGE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ ngoại ngữ %>" DataField="LANGUAGE_LEVEL_NAME" Visible="false"
                                UniqueName="LANGUAGE_LEVEL_NAME" SortExpression="LANGUAGE_LEVEL_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm số/Xếp loại %>" DataField="LANGUAGE_MARK" Visible="false"
                                UniqueName="LANGUAGE_MARK" SortExpression="LANGUAGE_MARK">
                            </tlk:GridBoundColumn>     
                            
                            <tlk:GridButtonColumn UniqueName="DowloadCommandColumnBank" Text="DownloadBank" CommandName="DowloadBank" HeaderText="Tải" 
                                ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"  ButtonType="ImageButton"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="45px">
                            </tlk:GridButtonColumn>
                            <tlk:GridButtonColumn UniqueName="ViewCommandColumnBank" Text="ViewBank" CommandName="ViewBank" HeaderText="View" 
                                ImageUrl="~/Static/Images/Icons/16/ViewImgOrg.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="45px">
                            </tlk:GridButtonColumn>
                            
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Sổ hộ chiếu %>" DataField="PASS_NO"
                                UniqueName="PASS_NO" SortExpression="PASS_NO">
                            </tlk:GridBoundColumn>    
                            <tlk:GridBoundColumn UniqueName="PASS_DATE" HeaderText="<%$ Translate: Ngày cấp %>" DataFormatString="{0:dd/MM/yyyy}" 
                                DataField="PASS_DATE" SortExpression="PASS_DATE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn UniqueName="PASS_EXPIRE" HeaderText="<%$ Translate: Ngày hết hạn %>" DataFormatString="{0:dd/MM/yyyy}" 
                                DataField="PASS_EXPIRE" SortExpression="PASS_EXPIRE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi cấp %>" DataField="PASS_PLACE"
                                UniqueName="PASS_PLACE" SortExpression="PASS_PLACE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Visa %>" DataField="VISA"
                                UniqueName="VISA" SortExpression="VISA">
                            </tlk:GridBoundColumn>    
                            <tlk:GridBoundColumn UniqueName="VISA_DATE" HeaderText="<%$ Translate: Ngày cấp %>" DataFormatString="{0:dd/MM/yyyy}" 
                                DataField="VISA_DATE" SortExpression="VISA_DATE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn UniqueName="VISA_EXPIRE" HeaderText="<%$ Translate: Ngày hết hạn %>" DataFormatString="{0:dd/MM/yyyy}" 
                                DataField="VISA_EXPIRE" SortExpression="VISA_EXPIRE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi cấp %>" DataField="VISA_PLACE"
                                UniqueName="VISA_PLACE" SortExpression="VISA_PLACE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số sổ lao động %>" DataField="BOOK_NO"
                                UniqueName="BOOK_NO" SortExpression="BOOK_NO">
                            </tlk:GridBoundColumn>    
                            <tlk:GridBoundColumn UniqueName="BOOK_DATE" HeaderText="<%$ Translate: Ngày cấp %>" DataFormatString="{0:dd/MM/yyyy}" 
                                DataField="BOOK_DATE" SortExpression="BOOK_DATE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn UniqueName="BOOK_EXPIRE" HeaderText="<%$ Translate: Ngày hết hạn %>" DataFormatString="{0:dd/MM/yyyy}" 
                                DataField="BOOK_EXPIRE" SortExpression="BOOK_EXPIRE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi cấp %>" DataField="SSLD_PLACE"
                                UniqueName="SSLD_PLACE" SortExpression="SSLD_PLACE">
                            </tlk:GridBoundColumn>
                            
                            
                            <tlk:GridButtonColumn UniqueName="DowloadCommandColumnOther" Text="DowloadOther" CommandName="DowloadOther" HeaderText="Tải" 
                                ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"  ButtonType="ImageButton"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="40px">
                            </tlk:GridButtonColumn>
                            <tlk:GridButtonColumn UniqueName="ViewCommandColumnOther" Text="ViewOther" CommandName="ViewOther" HeaderText="View" 
                                ImageUrl="~/Static/Images/Icons/16/ViewImgOrg.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            </tlk:GridButtonColumn>   
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ văn hóa %>" DataField="ACADEMY_NAME"
                                UniqueName="ACADEMY_NAME" SortExpression="ACADEMY_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ học vấn %>" DataField="LEARNING_LEVEL_NAME"
                                UniqueName="LEARNING_LEVEL_NAME" SortExpression="LEARNING_LEVEL_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chuyên môn %>" DataField="MAJOR_NAME"
                                UniqueName="MAJOR_NAME" SortExpression="MAJOR_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trường học %>" DataField="GRADUATE_SCHOOL_NAME"
                                UniqueName="GRADUATE_SCHOOL_NAME" SortExpression="GRADUATE_SCHOOL_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm tốt nghiệp %>" DataField="GRADUATION_YEAR"
                                UniqueName="GRADUATION_YEAR" SortExpression="GRADUATION_YEAR">
                            </tlk:GridBoundColumn>--%>
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
