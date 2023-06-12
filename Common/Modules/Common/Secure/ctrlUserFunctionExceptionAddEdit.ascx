<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlUserFunctionExceptionAddEdit.ascx.vb"
    Inherits="Common.ctrlUserFunctionExceptionAddEdit" %>
<style>
    
    #ctl00_MainContent_ctrlUserException_ctrlOrg_trvOrgPostback{
        display: block;
    }
    #ctl00_MainContent_ctrlUserException_rgGrid{
        display: none;
    }
</style>
<asp:HiddenField ID="hidOrg" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <fieldset style="font-family: Verdana; font-size: 11px;" height="20%">
            <legend accesskey="P">Thông tin tìm kiếm</legend>
            <table class="table-form">
                <tr>
                    <td class="lb">
                        <%# Translate("Tên phân hệ")%>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboMODULE" runat="server">
                        </tlk:RadComboBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Nhóm chức năng")%>
                    </td>
                    <td>
                        <tlk:RadComboBox runat="server" ID="cboFunctionGroup">
                        </tlk:RadComboBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Tên chức năng")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtFUNCTION_NAME" runat="server">
                        </tlk:RadTextBox>
                    </td>

                    <td>
                        <tlk:RadButton ID="btnFIND" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                        </tlk:RadButton>
                    </td>
                </tr>
            </table>
        </fieldset>
        <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" Height="65%" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID" Caption="<%$ Translate: SECURE_FUNCTION_NOT_IN_User %>">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="ID" Visible="False" />
                    <tlk:GridBoundColumn DataField="NAME" HeaderText="<%$ Translate: Tên chức năng %>"
                        UniqueName="NAME" SortExpression="NAME" ShowFilterIcon="false" FilterControlWidth="200px"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                    <tlk:GridBoundColumn DataField="FID" HeaderText="<%$ Translate: Mã chức năng %>"
                        UniqueName="FID" SortExpression="FID" HeaderStyle-Width="150px" ShowFilterIcon="false"
                        FilterControlWidth="130px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" />
                    <tlk:GridBoundColumn DataField="FUNCTION_GROUP_NAME" HeaderText="<%$ Translate: Nhóm chức năng %>"
                        UniqueName="FUNCTION_GROUP_NAME" SortExpression="FUNCTION_GROUP_NAME" HeaderStyle-Width="80px"
                        ShowFilterIcon="false" FilterControlWidth="60px" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" />
                    <tlk:GridBoundColumn DataField="MODULE_NAME" HeaderText="<%$ Translate: Tên phân hệ %>"
                        UniqueName="MODULE_NAME" SortExpression="MODULE_NAME" HeaderStyle-Width="80px"
                        ShowFilterIcon="false" FilterControlWidth="60px" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
        <fieldset style="font-family: Verdana; font-size: 11px;" height="20%">
            <legend accesskey="P">Phân quyền ngoại lệ</legend>
            <table class="table-form">
                <tr>
                    <td class="lb" style="display: none">
                        <%--<asp:Label ID="lbOrgName" runat="server" Text="Phòng ban">--%>
                        <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                    </td>
                    <td style="display: none">
                        <tlk:RadTextBox runat="server" ID="txtOrgName" SkinID="Readonly" ReadOnly="true" />
                        <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />

                    </td>
                    <td class="lb">
                        <%# Translate("Cấp bậc")%><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadComboBox runat="server" ID="cboLocation" AutoPostBack="false" CheckBoxes="true" EnableCheckAllItemsCheckBox="true">
                        </tlk:RadComboBox>
                    </td>
                </tr>
            </table>
        </fieldset>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" Width="150px" runat="server">
        <fieldset>
            <legend accesskey="P">
                <%# Translate("PERMISION")%></legend>
            <table class="table-form">
                <tr>
                    <td>
                        <asp:CheckBox ID="cbALLOW_CREATE" Text="<%$ Translate: ALLOW_CREATE %>" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbALLOW_MODIFY" Text="<%$ Translate: ALLOW_MODIFY %>" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbALLOW_DELETE" Text="<%$ Translate: ALLOW_DELETE %>" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbALLOW_PRINT" Text="<%$ Translate: ALLOW_PRINT %>" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbALLOW_IMPORT" Text="<%$ Translate: ALLOW_IMPORT %>" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbALLOW_EXPORT" Text="<%$ Translate: ALLOW_EXPORT %>" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbALLOW_SPECIAL1" Text="<%$ Translate: ALLOW_SPECIAL1 %>" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbALLOW_SPECIAL2" Text="<%$ Translate: ALLOW_SPECIAL2 %>" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbALLOW_SPECIAL3" Text="<%$ Translate: ALLOW_SPECIAL3 %>" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbALLOW_SPECIAL4" Text="<%$ Translate: ALLOW_SPECIAL4 %>" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbALLOW_SPECIAL5" Text="<%$ Translate: ALLOW_SPECIAL5 %>" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkAll" Text="<%$ Translate: Tất cả %>" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                            AutoPostBack="true" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </tlk:RadPane>
</tlk:RadSplitter>

<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
