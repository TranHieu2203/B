<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlViewImage.ascx.vb"
    Inherits="Profile.ctrlViewImage" %>
<style type="text/css">
    #ctrlViewImage_rbiEmployeeImage {
        display: block;
        margin-left: auto;
        margin-right: auto;
        width: auto;
        height: 100%;
    }
</style>

    <tlk:RadBinaryImage ID="rbiEmployeeImage" runat="server" AutoAdjustImageControlSize="false" 
            ResizeMode="Fill" />
