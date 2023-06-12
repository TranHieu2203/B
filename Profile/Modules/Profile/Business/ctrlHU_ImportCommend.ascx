<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ImportCommend.ascx.vb"
    Inherits="Profile.ctrlHU_ImportCommend" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField runat="server" ID="hidOrg" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="300" Width="300px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter4" runat="server" Orientation="Horizontal" Width="100%"
            Height="100%">
            <tlk:RadPane ID="Pane1" runat="server" Width="100%" Height="50%" Scrolling="None">
                <tlk:RadSplitter ID="RadSplitter5" runat="server" Orientation="Horizontal" Width="100%"
                    Height="100%">
                    <tlk:RadPane ID="RadPane3" runat="server" Width="100%" Height="33px" Scrolling="None">
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <b>
                                        <%# Translate("Đối tượng")%></b>
                                </td>
                                <td>
                                    <tlk:RadComboBox ID="cboCommendObj" runat="server" AutoPostBack="True" CausesValidation="False">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </tlk:RadPane>
                    <tlk:RadPane ID="RadPane6" runat="server" Width="100%" Scrolling="None">
                        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="RadPane1" runat="server" Width="100%" Height="50%" Scrolling="None">
                <tlk:RadSplitter ID="RadSplitter6" runat="server" Orientation="Horizontal" Width="100%"
                    Height="100%">
                    <tlk:RadPane ID="RadPane8" runat="server" Width="100%" Height="55px" Scrolling="None">
                        <table class="table-form">
                            <tr>
                                <td>
                                    <b>
                                        <%# Translate("Ngày xét thưởng")%></b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <tlk:RadDatePicker ID="rdCommend" runat="server" Width="100%" AutoPostBack="True">
                                    </tlk:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                    </tlk:RadPane>
                    <tlk:RadPane ID="RadPane9" runat="server" Width="50%" Scrolling="None">
                        <tlk:RadGrid ID="rgvCommendList" runat="server" Width="100%" CellSpacing="0" GridLines="None"
                            AutoGenerateColumns="False" Height="100%" AllowMultiRowSelection="True">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                            <MasterTableView ClientDataKeyNames="ID,CODE,NAME" DataKeyNames="ID">
                                <Columns>
                                    <tlk:GridTemplateColumn Reorderable="False" Resizable="False" ShowFilterIcon="False"
                                        ShowSortIcon="False" UniqueName="CheckBoxTemplateColumn">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ImportList_IS_SELECTED" runat="server" AutoPostBack="True" OnCheckedChanged="rgvCommendList_CheckBoxRowSelection" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="ImportList_HEADER_IS_SELECTED" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="rgvCommendList_HeaderCheckBoxSelection" />
                                        </HeaderTemplate>
                                        <HeaderStyle Width="30px" />
                                    </tlk:GridTemplateColumn>
                                    <tlk:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false">
                                    </tlk:GridBoundColumn>
                                    <tlk:GridBoundColumn AllowFiltering="False" AllowSorting="false" DataField="CODE"
                                        Groupable="False" HeaderText="<%$ Translate: Mã %>" ReadOnly="true" Reorderable="False"
                                        Resizable="False" ShowSortIcon="False" UniqueName="CODE" Visible="True">
                                        <HeaderStyle Width="60px" />
                                    </tlk:GridBoundColumn>
                                    <tlk:GridBoundColumn AllowFiltering="False" AllowSorting="false" DataField="NAME"
                                        Groupable="False" HeaderText="<%$ Translate: Hình thức khen thưởng %>" ReadOnly="true"
                                        Reorderable="False" Resizable="False" ShowSortIcon="False" UniqueName="NAME"
                                        Visible="True">
                                    </tlk:GridBoundColumn>
                                    <tlk:GridBoundColumn DataField="DATA_TYPE" UniqueName="DATA_TYPE" Visible="false">
                                    </tlk:GridBoundColumn>
                                </Columns>
                                <PagerStyle Visible="True" />
                            </MasterTableView>
                            <PagerStyle Visible="True" />
                        </tlk:RadGrid>
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane runat="server" ID="RightPane" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane runat="server" ID="RadPane5" Scrolling="None" Height="60px">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
                <tlk:RadTabStrip runat="server" ID="tabImportSalary" SelectedIndex="0" MultiPageID="pageImportSalary">
                    <Tabs>
                        <tlk:RadTab Text='<%$ Translate: Danh sách nhân viên %>' Selected="True">
                        </tlk:RadTab>
                        <tlk:RadTab Text='<%$ Translate: Dữ liệu đã nhập %>'>
                        </tlk:RadTab>
                    </Tabs>
                </tlk:RadTabStrip>
            </tlk:RadPane>
            <tlk:RadPane runat="server" ID="RadPane7" Scrolling="None" Height="100%">
                <tlk:RadMultiPage runat="server" ID="pageImportSalary" SelectedIndex="0" Height="100%">
                    <tlk:RadPageView ID="RadPageView1" runat="server">
                        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
                            <tlk:RadPane runat="server" ID="RadPane2" Scrolling="None" Height="40%">
                                <tlk:RadGrid ID="rgvEmployees" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    Height="100%" AllowSorting="True" AllowMultiRowSelection="True" CellSpacing="0"
                                    GridLines="None" AllowFilteringByColumn="True" HierarchyLoadMode="ServerOnDemand">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                    <MasterTableView DataKeyNames="EMPLOYEE_ID" ClientDataKeyNames="EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME_VN,ORG_ID,ORG_NAME,ID_NO">
                                        <Columns>
                                            <tlk:GridClientSelectColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px"
                                                ItemStyle-HorizontalAlign="Center" UniqueName="cbCheck">
                                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </tlk:GridClientSelectColumn>
                                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" UniqueName="EMPLOYEE_ID" Visible="false">
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="EMPLOYEE_CODE" HeaderText="<%$ Translate: Mã nhân viên %>"
                                                UniqueName="EMPLOYEE_CODE">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="FULLNAME_VN" HeaderText="<%$ Translate: Tên nhân viên %>"
                                                UniqueName="FULLNAME_VN">
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="ID_NO" HeaderText="<%$ Translate: CMND %>" UniqueName="ID_NO">
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="ORG_ID" HeaderText="<%$ Translate: Mã phòng ban %>"
                                                UniqueName="ORG_ID">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </tlk:GridBoundColumn>
                                            <tlk:GridBoundColumn DataField="ORG_NAME" HeaderText="<%$ Translate: Tên phòng ban %>"
                                                UniqueName="ORG_NAME">
                                                <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                            </tlk:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </tlk:RadGrid>
                            </tlk:RadPane>
                            <tlk:RadSplitBar ID="RadSplitBar3" runat="server" CollapseMode="Forward">
                            </tlk:RadSplitBar>
                            <tlk:RadPane runat="server" ID="RadPane4" Scrolling="None" Height="60%">
                                <tlk:RadButton runat="server" ID="btnSelect" AutoPostBack="True" Height="35px" Width="50px"
                                    Text="Chọn">
                                </tlk:RadButton>
                                &nbsp;&nbsp;<tlk:RadButton runat="server" ID="btnDeSelect" Text="Bỏ Chọn" AutoPostBack="True"
                                    Height="35px" Width="50px">
                                </tlk:RadButton>
                                <tlk:RadGrid runat="server" ID="rgvDataPrepare" GridLines="None" AutoGenerateColumns="False"
                                    CellSpacing="0" GroupingEnabled="False" AllowPaging="False" AllowMultiRowEdit="True"
                                    AllowMultiRowSelection="True" Height="88%">
                                    <ClientSettings AllowExpandCollapse="False" AllowGroupExpandCollapse="False">
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                    <%-- <MasterTableView DataKeyNames="EMPLOYEE_ID">
                                        <Columns>
                                        </Columns>
                                        <PagerStyle Visible="False" />
                                    </MasterTableView>--%>
                                </tlk:RadGrid>
                            </tlk:RadPane>
                        </tlk:RadSplitter>
                    </tlk:RadPageView>
                    <tlk:RadPageView ID="RadPageView2" runat="server" Height="100%">
                        <table class="table-form">
                            <tr>
                                <td>
                                    <b>
                                        <%# Translate("Ngày xét thưởng")%></b>
                                </td>
                                <td>
                                    <tlk:RadDatePicker ID="rdCommendView" runat="server" Width="100%" AutoPostBack="True">
                                    </tlk:RadDatePicker>
                                </td>
                                <td>
                                    <tlk:RadButton ID="btnShow" runat="server" Text="<%$ Translate: Xem %>">
                                    </tlk:RadButton>
                                </td>
                            </tr>
                        </table>
                        <tlk:RadGrid runat="server" ID="rgvDataImported" GridLines="None" ShowStatusBar="True"
                            AutoGenerateColumns="False" CellSpacing="0" Height="100%" AllowFilteringByColumn="True"
                            AllowPaging="True" AllowSorting="True" PageSize="100000">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>
                            <%--<MasterTableView DataKeyNames="EMPLOYEE_ID">
                                <Columns>
                                </Columns>
                                <PagerStyle Visible="True" />
                            </MasterTableView>--%>
                        </tlk:RadGrid>
                    </tlk:RadPageView>
                </tlk:RadMultiPage>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EDIT') {
                OpenEdit();
                args.set_cancel(true);
            }

            if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

      
    </script>
</tlk:RadCodeBlock>
