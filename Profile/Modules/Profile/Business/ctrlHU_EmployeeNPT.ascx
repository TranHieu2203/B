<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmployeeNPT.ascx.vb"
    Inherits="Profile.ctrlHU_EmployeeNPT" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbFromDate" runat="server" Text="Ngày vào làm từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbToDate" runat="server" Text="Đến ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="Tìm" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgEmpNPT" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="0" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="EMPLOYEE_ID,ID,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_NAME,FULLNAME,RELATION_NAME,BIRTH_DATE,TAXTATION,DEDUCT_FROM,DEDUCT_TO,ORG_DESC"
                        ClientDataKeyNames="EMPLOYEE_ID,ID,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_NAME,FULLNAME,RELATION_NAME,BIRTH_DATE,TAXTATION,DEDUCT_FROM,DEDUCT_TO">

                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="Ban/ Phòng" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="200px" />  
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Mối quan hệ" DataField="RELATION_NAME"
                                SortExpression="RELATION_NAME" UniqueName="RELATION_NAME" />
                            <tlk:GridBoundColumn HeaderText="Họ và tên người thân" DataField="FULLNAME"
                                SortExpression="FULLNAME" UniqueName="FULLNAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="Ngày sinh" DataField="BIRTH_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="BIRTH_DATE" UniqueName="BIRTH_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridCheckBoxColumn HeaderText="Đối tượng giảm trừ" DataField="IS_DEDUCT"
                                SortExpression="IS_DEDUCT" UniqueName="IS_DEDUCT" HeaderStyle-Width="70px" AllowFiltering="false" />
                            <tlk:GridBoundColumn HeaderText="Mã số thuế" DataField="TAXTATION"
                                SortExpression="TAXTATION" UniqueName="TAXTATION" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="Giảm trừ Từ ngày" DataField="DEDUCT_FROM"
                                ItemStyle-HorizontalAlign="Center" SortExpression="DEDUCT_FROM" UniqueName="DEDUCT_FROM"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Giảm trừ Đến ngày" DataField="DEDUCT_TO"
                                ItemStyle-HorizontalAlign="Center" SortExpression="DEDUCT_TO" UniqueName="DEDUCT_TO"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
