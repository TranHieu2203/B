<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMessageBox.ascx.vb"
    Inherits="Common.ctrlMessageBox" %>
<tlk:RadWindow runat="server" ID="rwMessage" Height="150px" Width="320px" Behaviors="Close"
    VisibleStatusbar="false" Modal="true" EnableViewState="false" Title="Cảnh báo">
    <ContentTemplate>
        <p>
            <p style="padding: 5px 15px 5px 15px; height: 40px">
                <asp:Label ID="lblMessage" runat="server"></asp:Label></p>
        </p>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <div style="margin: 0px 10px 10px 10px; text-align: center;">
                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: YES %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: NO %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
            </div>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>
