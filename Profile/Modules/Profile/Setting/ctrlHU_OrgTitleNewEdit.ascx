<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_OrgTitleNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_OrgTitleNewEdit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidDecisionID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidSignID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="20px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOrg" runat="server"  />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="360px">
       <table class="table-form">
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin chức danh")%>
                    </b>
                    <hr />
                </td>
            </tr>
             <tr>

            <td class="lb">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td style="width: 300px">
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" Width="250px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqOrgCode" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Phòng ban không được để trống %>" ToolTip="<%$ Translate: Phòng ban không được để trống %>"> 
                    </asp:RequiredFieldValidator>
                </td>

                </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Mã chức danh cha")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtParentCode" runat="server" SkinID="Readonly" ReadOnly="true"
                        Width="250px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindTitle" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="ReqParentCode" ControlToValidate="txtParentCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn chức danh cha %>" ToolTip="<%$ Translate: Bạn phải chọn chức danh cha %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên chức danh cha")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtParentName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                </td>
                <td>
                    <tlk:RadTextBox ID="txtParentID" runat="server" Visible="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
        <tlk:RadGrid ID="rgTitle" AllowPaging="false" AllowMultiRowEdit="true" runat="server"
            Height="100%">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView EditMode="InPlace" AllowPaging="false" AllowCustomPaging="false"
               DataKeyNames="ID,NAME_VN,CODE" ClientDataKeyNames="NAME_VN" CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="150px" ID="btnTitle" runat="server" Text="<%$ Translate: Chọn tất cả chức danh %>"
                                CausesValidation="false" CommandName="FindTitle">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right; background-color: Red">
                            <tlk:RadButton Width="100px" ID="btnDeleteTitle" runat="server" Text="<%$ Translate: Xóa %>"
                                CausesValidation="false" CommandName="DeleteTitle">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã chức danh %>" 
                        ReadOnly="true" UniqueName="CODE" SortExpression="CODE" DataField="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chức danh %>" DataField="NAME_VN"
                        UniqueName="NAME_VN" ReadOnly="true" SortExpression="NAME_VN"  />
                  

                    <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" ReadOnly="true" SortExpression="ORG_NAME" />--%>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
   
    </tlk:RadPane>
</tlk:RadSplitter>
