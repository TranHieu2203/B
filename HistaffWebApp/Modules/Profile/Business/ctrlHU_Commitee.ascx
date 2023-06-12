<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Commitee.ascx.vb"
    Inherits="Profile.ctrlHU_Commitee" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    #ctl00_MainContent_ctrlHU_Commitee_RadPane3 {
        border-bottom-width: 0 !important;
    }
</style>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEMPLOYEE_ID" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidORG_ID" runat="server" />
<asp:HiddenField ID="hidSIGNER_ID" runat="server" />
<asp:HiddenField ID="hidEMPLOYEE_ORG_DESC" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="200px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td style="width:110px"></td>
                        <td style="width:170px"></td>
                        <td style="width:180px"></td>
                        <td style="width:200px"></td>
                        <td style="width:180px"></td>
                        <td style="width:300px"></td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label1" Text="Mã số nhân viên"></asp:Label>
                            <span style="color: red">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEMPLOYEE_CODE" runat="server" SkinID="Readonly" ReadOnly="true" Width="132" AutoPostBack="true">
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEMPLOYEE_CODE"
                                runat="server" ErrorMessage="Bạn phải chọn Nhân viên" ToolTip="Bạn phải chọn Nhân viên"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label3" Text="Họ và tên"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEMPLOYEE_NAME" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label6" Text="Cấp bậc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEMPLOYEE_LEVEL" runat="server" SkinID="Readonly" ReadOnly="true" Width="45%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label7" Text="Ngày quyết định"></asp:Label>
                            <span style="color: red">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFROM_DATE" runat="server"  AutoPostBack="true">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdFROM_DATE"
                                runat="server" ErrorMessage="Bạn phải nhập Ngày quyết định" ToolTip="Bạn phải nhập Ngày quyết định"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label4" Text="Vị trí công việc tại tập đoàn"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEMPLOYEE_TITLE" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label12" Text="Phòng ban tại đơn vị gốc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEMPLOYEE_ORG" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label9" Text="Số quyết định"></asp:Label>
                            <span style="color: red">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtDECISION_NO" runat="server">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtDECISION_NO"
                                runat="server" ErrorMessage="Bạn phải nhập Số quyết định" ToolTip="Bạn phải nhập Số quyết định"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label5" Text="Vị trí công việc tại HĐ, UB"></asp:Label>
                            <span style="color: red">*</span>
                        </td>
                        <td>
                                <tlk:RadComboBox runat="server" ID="cboTitle" width="490px" CausesValidation="false"
                                Filter="Contains"
                                HighlightTemplatedItems="true">
                                    <HeaderTemplate>
                                    <table style="width: 1070px" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 570px;">
                                                Tên vị trí
                                            </td>
                                            <td style="width: 250px;">
                                                Master
                                            </td>
                                                <td style="width: 250px;">
                                                Interim
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="width: 1070px" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 570px;">
                                                <%# Eval("NAME")%>
                                            </td>
                                            <td style="width: 250px;">
                                                <%# Eval("MASTER_NAME")%>
                                            </td>
                                            <td style="width: 250px;">
                                                <%# Eval("INTERIM_NAME")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboTitle"
                                runat="server" ErrorMessage="Bạn phải chọn Vị trí công việc tại HĐ, UB" ToolTip="Bạn phải chọn Vị trí công việc tại HĐ, UB"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label2" Text="Phòng ban tại HĐ, UB"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtORG_NAME" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label8" Text="Ngày thôi nhiệm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdTO_DATE" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label11" Text="Người ký"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtSIGNER_NAME" runat="server" SkinID="Readonly" ReadOnly="true" Width="172">
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindSigner" runat="server" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label10" Text="Ghi chú"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtREMARK" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <div style="height:30px">
                    <asp:CheckBox ID="chkIS_INACTIVE" Text="Hiển thị danh sách ngưng hoạt động" runat="server" AutoPostBack="true" />
                </div>
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="96%" Width="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true"> 
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC,EMPLOYEE_ORG_DESC"
                        ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,EMPLOYEE_TITLE,EMPLOYEE_LEVEL,EMPLOYEE_ORG,ORG_ID,ORG_ID_EMP,ORG_NAME,COMMITTE_POSITION,FROM_DATE,TO_DATE,DECISION_NO,REMARK,SIGNER_ID,SIGNER_NAME,EMPLOYEE_ORG_DESC,TITLE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Họ và tên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="Cấp bậc" DataField="EMPLOYEE_LEVEL"
                                SortExpression="EMPLOYEE_LEVEL" UniqueName="EMPLOYEE_LEVEL" HeaderStyle-Width="70px" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban tại HĐ, UB" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="350px" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc tại HĐ, UB" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="250px" />
                            <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                                SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
                            <tlk:GridBoundColumn HeaderText="Ngày quyết định" DataField="FROM_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="FROM_DATE" UniqueName="FROM_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Ngày thôi nhiệm" DataField="TO_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="TO_DATE" UniqueName="TO_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Phòng ban tại đơn vị gốc" DataField="EMPLOYEE_ORG"
                                SortExpression="EMPLOYEE_ORG" UniqueName="EMPLOYEE_ORG" HeaderStyle-Width="350px" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc tại tập đoàn" DataField="EMPLOYEE_TITLE"
                                SortExpression="EMPLOYEE_TITLE" UniqueName="EMPLOYEE_TITLE" HeaderStyle-Width="250px" />
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK"
                                SortExpression="REMARK" UniqueName="REMARK" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSigner" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlHU_Commitee_RadSplitter1';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Commitee_RadPane3';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Commitee_RadPane2';
        var validateID = 'MainContent_ctrlHU_Commitee_ValidationSummary1';
        var oldSize = $('#' + pane1ID).height();
        
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rgDataRadGridDeSelecting() { }
        function rgDataOnClientRowSelected() { }
        function rgDataRadGridSelecting() {}

        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenEditWindow(states) {
            var empId = $find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=' + empId + '&state=' + states, "_self");
        }
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == 'SAVE') {
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize + 45, 'rgData');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize + 45);
            } else {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize + 45);
            }
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
    </script>
</tlk:RadCodeBlock>