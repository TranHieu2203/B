<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInformation.ascx.vb"
    Inherits="Profile.ctrlInformation" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server">
        <div class="text_welcome">
            <img src="../../Static/Images/logo_HiStaff_big.png" class="logo_wel" />
            <div>
                <h2 style="font-size: 30px !important;">Chào mừng bạn đến với Phần mềm quản trị tổng thể nguồn nhân lực</h2>
            </div>
            <div>
                </div>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<style>
    .logo_wel
    {
        width: 300px;
        margin-bottom: 20px;
    }
    
    .text_welcome
    {
        width: 100%;
        height: 350px;
        text-align: center;
        font-size: 30px;
        font-weight: bold;
        line-height: 50px;
        margin-top: 50px;
        color: #454545;
    }
    .brackcrum
    {
        display: none;
    }
</style>
