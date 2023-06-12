<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TransferOrgPopup.ascx.vb"
    Inherits="Profile.ctrlHU_TransferOrgPopup" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidAssetID" runat="server" />
<style type="text/css">
    .lb{padding-left:20px !important}
     #RAD_SPLITTER_PANE_CONTENT_ctrlPortalTransferOrgPopup_LeftPane{height:112px !important}
     #RAD_SPLITTER_PANE_CONTENT_RadPaneMain{ overflow: hidden !important; }
     
    .btn 
    {
        margin-right: 10px;
        padding: 7px 12px;
        outline: none;
        border: none;
        color: #ffffff;
        border-radius: 3px;    
    }
    .btn-success {
        background-color: #28a745;
    }
    .btn-danger {
        background-color: #dc3545;   
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Height="100%" Orientation="Horizontal" Width="100%">
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
           <%-- <tr>
                <td class="lb">
                            <label id="lbLAddress" runat="server">
                                <%# Translate("Địa điểm")%></label> 
                        </td>
                <td>  
                     <tlk:radcombobox runat="server" id="cboAddress" Width="180px">
                    </tlk:radcombobox>
                </td>
            </tr>--%>
            <tr>
                <td class="lb">
                  <label id="lblQuality" runat="server">
                                <%# Translate("Số lượng")%></label> 
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnQuality" Value="1" runat="server" Width="180px" MinValue="1">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
   <tlk:RadPane ID="RadPane2" runat="server" MinHeight="50" Height="50px" Scrolling="None">
        <div style="display: flex; justify-content: flex-end; align-items: center; height: 100%;">
      <%--      <asp:HiddenField ID="hidSelected" runat="server" />
            <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                Font-Bold="true" CausesValidation="false">
            </tlk:RadButton>
            <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                Font-Bold="true" CausesValidation="false">
            </tlk:RadButton>--%>
            <input type="button" value="<%=Translate(" Tiếp tục ")%>" onclick="btnCancelClick(1)" class="btn btn-success" style="margin-right: 10px;"/>
            <input type="button" value="<%=Translate(" Hủy ")%>" onclick="btnCancelClick(0)" class="btn btn-danger" style="margin-right: 10px;"/>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" /> 
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
                var enableAjax = true;
                function onRequestStart(sender, eventArgs) {
                    eventArgs.set_enableAjax(enableAjax);
                    enableAjax = true;
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
        function btnCancelClick(type) {
            debugger;
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get a reference to the current RadWindow
            var oWnd; // = GetRadWindow();

            if (window.frameElement && window.frameElement.radWindow) {
                oWnd = window.frameElement.radWindow;
            } else if (window.radWindow) {
                oWnd = window.radWindow;
            }
            if (type == 1) {
                if (typeof ctrlHU_TransferOrgPopup_rnQuality != "undefined") {
                    $(window.parent.MainContent_ctrlHU_TransferOrg_txtQtyPos).val($("#<%=rnQuality.ClientID%>").val());
                } else {
                    $(window.parent.MainContent_ctrlHU_TransferOrg_txtQtyPos).val(0);
                }
                $(window.parent.MainContent_ctrlHU_TransferOrg_txtQtyPos).change();
               
                $(window.parent.ctl00_MainContent_ctrlHU_TransferOrg_RadButton1_input).click();
                oArg.ID = 'Cancel';
                //Close the RadWindow and send the argument to the parent page
                oWnd.close(oArg);
            } else {
                //alert('No');
                oArg.ID = 'Cancel';
                //Close the RadWindow and send the argument to the parent page
                oWnd.close(oArg);
            }

        }

    </script>
</tlk:RadCodeBlock>
