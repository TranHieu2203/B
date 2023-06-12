<%@ Page Title="Access Limit" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="AccessLimit.aspx.vb" Inherits="HistaffWebApp.AccessLimit" ViewStateMode="Enabled" %>

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
    <tlk:RadWindow runat="server" ID="rwAccessLimit" VisibleOnPageLoad ="true" VisibleStatusbar="false" Width="420px"
        Height="150px" EnableShadow="true" Behaviors="Close" Modal="true"  Title="<%$ Translate: Thông báo%>">
        <ContentTemplate>
            <div id="browser" style="padding: 10px;">
                <%# Translate("Số lượng người truy cập vượt quá giới hạn cho phép. Bạn hãy click vào ")%> <a style ="text-decoration: underline" href ="<%=ReturnUrl %>"><%# Translate("đường dẫn sau")%></a><%# Translate(" để quay lại.")%>
            </div>
        </ContentTemplate>
    </tlk:RadWindow>
    <tlk:RadAjaxLoadingPanel runat="server" ID="LoadingPanel">
    </tlk:RadAjaxLoadingPanel>
</asp:Content>
