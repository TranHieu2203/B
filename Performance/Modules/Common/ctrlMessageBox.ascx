<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMessageBox.ascx.vb" Inherits="Common.ctrlMessageBox" %>

<tlk:RadWindow runat="server" ID="rwMessage"  Behaviors="Close"
    MaxWidth="500px"
    MinWidth="300px"
    MinHeight="200px" 
    VisibleStatusbar="false"
    AutoSizeBehaviors="Width, Height"
    AutoSize="true"
    Modal="true" 
    EnableViewState="false" >
    <ContentTemplate>
        <p>
            <p style="padding:10px 15px 10px 15px;"><asp:Label ID="lblMessage" runat="server"></asp:Label></p>
        </p>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <div style="margin: 0px 10px 10px 10px; text-align:center ;">
                <asp:LinkButton  ID="LinkButton1" CssClass="button action blue" runat="server" OnClick="btnOk_Click" >
                <span class="label">OK</span>
                </asp:LinkButton>
                <asp:LinkButton  ID="LinkButton2" CssClass="button action red" runat="server" OnClick="btnCancel_Click">
                <span class="label">Cancel</span>
                </asp:LinkButton>
            </div>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>