<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMessageBoxTraining.ascx.vb"
    Inherits="Common.ctrlMessageBox" %>
<tlk:RadWindow runat="server" ID="rwMessage" Height="150px" Width="320px" Behaviors="Close"
    VisibleStatusbar="false" Modal="true" EnableViewState="false" Title="<%$ Translate: Cảnh báo %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <p>
            <p style="padding: 5px 15px 5px 15px; height: 40px">
                <asp:Label ID="lblMessage" runat="server"></asp:Label></p>
        </p>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>