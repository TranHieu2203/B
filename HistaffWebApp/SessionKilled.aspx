<%@ Page Title="Session Kill" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="SessionKilled.aspx.vb" Inherits="HistaffWebApp.SessionKilled" ViewStateMode="Enabled" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <tlk:RadAjaxManager runat="server" ID="RadAjaxManager1">
        <AjaxSettings>
            <tlk:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <tlk:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </tlk:AjaxSetting>
        </AjaxSettings>
    </tlk:RadAjaxManager>
    <tlk:RadWindow runat="server" ID="rwSessionKilled" VisibleOnPageLoad ="true" VisibleStatusbar="false" Width="420px"
        Height="150px" EnableShadow="true" Behaviors="Close" Modal="true"  Title="<%$ Translate: Thông báo%>">
        <ContentTemplate>
            <div id="browser" style="padding: 10px;">
                <%# Translate("Phiên làm việc của bạn đã bị ADMIN tạm dừng. Bạn hãy quay trở lại trang ")%> <a style ="text-decoration: underline" href ="/Account/Login.aspx"><%# Translate("Đăng nhập")%></a>
            </div>
        </ContentTemplate>
    </tlk:RadWindow>
    <tlk:RadAjaxLoadingPanel runat="server" ID="LoadingPanel">
    </tlk:RadAjaxLoadingPanel>
</asp:Content>
