<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindAssetPopup.ascx.vb"
    Inherits="Profile.ctrlFindAssetPopup" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="None" VisibleStatusbar="false"
    Height="400px" Width="600px" Modal="true" AssetDeclare="<%$ Translate: VIEW_AssetDeclare_POSITION_POPUP %>"
    OnClientClose="popupclose">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <p>
                <tlk:RadGrid PageSize=50 ID="rgAssetDeclare" runat="server" Height="300px" AutoGenerateColumns="False"
                    AllowPaging="True">
                    <MasterTableView DataKeyNames="ID,CODE,NAME">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhóm tài sản %>" DataField="GROUP_CODE"
                                SortExpression="GROUP_CODE" UniqueName="GROUP_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tài sản %>" DataField="NAME"
                                SortExpression="NAME" UniqueName="NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm tài sản %>" DataField="GROUP_NAME"
                                SortExpression="GROUP_NAME" UniqueName="GROUP_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </p>
            <div style="margin: 0px 10px 10px 10px; text-align: right;">
                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
            </div>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">
        function popupclose(sender, args) {
            $get("<%= btnNO.ClientId %>").click();
        }
    </script>
</tlk:RadScriptBlock>
