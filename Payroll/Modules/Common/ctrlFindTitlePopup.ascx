<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindTitlePopup.ascx.vb"
    Inherits="Common.ctrlFindTitlePopup" %>
<%@ Register Src="ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="None" VisibleStatusbar="false"
    Height="400px" Width="600" Modal="true" Title="<%$ Translate: VIEW_TITLE_POSITION_POPUP %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <p>
                <tlk:RadGrid ID="rgTitle" runat="server" Height="300px">
                    <MasterTableView DataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Title_Code %>" DataField="CODE" SortExpression="CODE"
                                UniqueName="CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Title_Name_VN %>" DataField="NAME_VN"
                                SortExpression="NAME_VN" UniqueName="NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Title_Name_EN %>" DataField="NAME_EN"
                                SortExpression="NAME_EN" UniqueName="NAME_EN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Title_Global_Name %>" DataField="GLOBAL_NAME"
                                SortExpression="GLOBAL_NAME" UniqueName="GLOBAL_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Title_WORKLEVEL_Name %>" DataField="WORKLEVEL_NAME"
                                SortExpression="WORKLEVEL_NAME" UniqueName="WORKLEVEL_NAME" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </p>
            <div style="margin: 0px 10px 10px 10px; text-align: center;">
                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: SELECT %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: CANCEL %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
            </div>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>
