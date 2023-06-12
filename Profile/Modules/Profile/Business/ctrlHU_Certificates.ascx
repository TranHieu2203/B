<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Certificates.ascx.vb"
    Inherits="Profile.ctrlHU_Certificates" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .hide-button input{
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
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWorkings" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="Tìm" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" AllowMultiRowSelection="true"
                    Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,TO_DATE_1, FROM_DATE_1,FROM_DATE,TO_DATE, YEAR_GRA, NAME_SHOOLS, FORM_TRAIN_ID, SPECIALIZED_TRAIN, TYPE_TRAIN_ID, RESULT_TRAIN, CERTIFICATE, RECEIVE_DEGREE_DATE, EFFECTIVE_DATE_FROM, EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,IS_RENEWED,CERTIFICATE_ID,TYPE_TRAIN_NAME,LEVEL_ID,POINT_LEVEL,CONTENT_LEVEL,NOTE,CERTIFICATE_CODE,TYPE_TRAIN_NAME,ORG_DESC"
                        ClientDataKeyNames="ID,TO_DATE_1,FROM_DATE_1, FROM_DATE,TO_DATE, YEAR_GRA, NAME_SHOOLS, FORM_TRAIN_ID, SPECIALIZED_TRAIN, TYPE_TRAIN_ID, RESULT_TRAIN, CERTIFICATE, RECEIVE_DEGREE_DATE, EFFECTIVE_DATE_FROM, EFFECTIVE_DATE_TO,UPLOAD_FILE,FILE_NAME,IS_RENEWED,CERTIFICATE_ID,TYPE_TRAIN_NAME,LEVEL_ID,POINT_LEVEL,CONTENT_LEVEL,NOTE,CERTIFICATE_CODE,TYPE_TRAIN_NAME">
                        <NoRecordsTemplate>
                            Không có bản ghi nào
                        </NoRecordsTemplate>
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Display="false" Visible="false" ></tlk:GridBoundColumn>

                            
                            <tlk:GridBoundColumn DataField="EMPLOYEE_CODE" HeaderText="Mã nhân viên"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="EMPLOYEE_NAME" HeaderText="Tên nhân viên"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" HeaderStyle-Width="150px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="ORG_NAME" HeaderText="Phòng ban"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" HeaderStyle-Width="200px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="TITLE_NAME" HeaderText="Vị trí công việc"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" HeaderStyle-Width="250px">
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
       
                            <tlk:GridBoundColumn DataField="FROM_DATE" HeaderText="Thời gian đào tạo từ"
                                UniqueName="FROM_DATE" SortExpression="FROM_DATE" DataFormatString="{0:MM/yyyy}" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn DataField="TO_DATE" HeaderText="Thời gian đào tạo đến"
                                UniqueName="TO_DATE" SortExpression="TO_DATE" DataFormatString="{0:MM/yyyy}" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>

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
                            <tlk:GridBoundColumn HeaderText="Hiệu lực chứng chỉ từ" DataField="EFFECTIVE_DATE_FROM"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                                UniqueName="EFFECTIVE_DATE_FROM" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Hiệu lực chứng chỉ đến" DataField="EFFECTIVE_DATE_TO"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                                UniqueName="EFFECTIVE_DATE_TO" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            
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
                            
                            <%--<tlk:GridCheckBoxColumn DataField="IS_MAJOR" HeaderText="Chuyên môn cao nhất" AllowFiltering="false"
                                UniqueName="IS_MAJOR" SortExpression="IS_MAJOR">
                            </tlk:GridCheckBoxColumn>--%>
                            <tlk:GridButtonColumn UniqueName="DowloadCommandColumn" Text="Download" CommandName="Dowload" HeaderText="Tải" 
                                ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"  ButtonType="ImageButton"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="40px">
                            </tlk:GridButtonColumn>
                            <tlk:GridButtonColumn UniqueName="ViewCommandColumn" Text="View" CommandName="View" HeaderText="View" 
                                ImageUrl="~/Static/Images/Icons/16/ViewImgOrg.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            </tlk:GridButtonColumn>
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1100"
            Height="620" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

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


        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenNew() {
            //var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_CertificateNewEdit&group=Business&noscroll=1', "rwPopup");
            /*window.open('/Default.aspx?mid=Profile&fid=ctrlHU_CertificateNewEdit&group=Business&noscroll=1', "_self");*/
            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_CertificateNewEdit&group=Business&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.center);
            oWindow.setSize(1100, 620);
        }
        function gridRowDblClick(sender, eventArgs) {
            OpenEdit();
        }
        function OpenEdit() {
            debugger;
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            //var owindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_CertificateNewEdit&group=Business&noscroll=1&ID=' + id, "rwPopup");
            /*window.open('/Default.aspx?mid=Profile&fid=ctrlHU_CertificateNewEdit&group=Business&noscroll=1&ID=' + id, "_self");*/
            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_CertificateNewEdit&group=Business&noscroll=1&ID=' + id, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.center);
            oWindow.setSize(1100, 620);
            return 2;
        }

        function OnClientButtonClicking(sender, args) {
            var m;
            var bCheck;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }

                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "NEXT") {
                enableAjax = false;
            }
        }

        function gridRowDblClick(sender, eventArgs) {
            if (OpenEdit(true) == 0) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }
            if (OpenEdit(true) == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW)%>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }
            args.set_cancel(true);
        }

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $get("<%= btnSearch.ClientId %>").click();
            }

        }

        function getUrlVars() {
            var vars = {};
            var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
                vars[key] = value;
            });
            return vars;
        }

        function OnClientClose(oWnd, args) {
            $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
        }

    </script>
</tlk:RadCodeBlock>
