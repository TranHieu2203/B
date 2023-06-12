<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMessageBoxTraining.ascx.vb"
    Inherits="Common.ctrlMessageBoxTraining" %>
<tlk:RadWindow runat="server" ID="rwMessage" Height="250px" Width="420px" Behaviors="Close"
    VisibleStatusbar="false" Modal="true" EnableViewState="false" Title="<%$ Translate: Thông tin chương trình đào tạo %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <p>
            <p style="padding: 5px 15px 5px 15px; height: 40px">
                <asp:Label ID="lblMessage" runat="server"></asp:Label></p>
        </p>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>