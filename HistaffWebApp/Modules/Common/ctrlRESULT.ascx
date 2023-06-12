<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRESULT.ascx.vb"
    Inherits="Common.ctrlRESULT" %>
<style type="text/css">
	    #ctrlRESULT_rtbLog {
		    height: 6500px !important;
            width: 100% !important; 
            padding: 0px 0px 0px 0px; 
            overflow:hidden;
	    }
	     
    </style>
<%@ Register Src="ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<tlk:RadSplitter runat="server" ID="RadSplitter1" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPaneGrid" runat="server" Scrolling="Both" Width="100%">
        <tlk:RadTextBox ID="rtbLog" runat="server" SkinID="Textbox1023" Rows="40" TextMode="MultiLine" Width="100%" Height="570px">
        </tlk:RadTextBox>
    </tlk:RadPane>   
</tlk:RadSplitter>
<script type="text/javascript">
    var enableAjax = true;
    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();        
    }
    //mandatory for the RadWindow dialogs functionality
    function getRadWindow() {
        if (window.radWindow) {
            return window.radWindow;
        }
        if (window.frameElement && window.frameElement.radWindow) {
            return window.frameElement.radWindow;
        }
        return null;
    }

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }        
</script>
