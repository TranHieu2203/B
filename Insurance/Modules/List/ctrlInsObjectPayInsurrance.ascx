<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsObjectPayInsurrance.ascx.vb"
    Inherits="Insurance.ctrlInsObjectPayInsurrance" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="100%" Scrolling="none">
                <tlk:RadToolBar ID="tbarOtherLists" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                <table class="table-form" style="height: 100%">
                    <tr>
                        <td style="vertical-align: top">
                            <tlk:RadListBox rendermode="Lightweight" ID="radLstInsurrance" runat="server" CheckBoxes="false"
                                showcheckall="false" Width="300px" Height="150px" AutoPostBack="true">
                                <Items>
                                    <tlk:RadListBoxItem Text="Bảo hiểm xã hội" Value="BHXH" />
                                    <tlk:RadListBoxItem Text="Bảo hiểm y tế" Value="BHYT" />
                                    <tlk:RadListBoxItem Text="Bảo hiểm thất nghiệp" Value="BHTN" />
                                    <tlk:RadListBoxItem Text="BHTNLD-BNN" Value="BHTNLD_BNN" />
                                </Items>
                            </tlk:RadListBox>
                        </td>

                        <td style="vertical-align: top">
                            <tlk:RadListBox rendermode="Lightweight" ID="radLstContact" runat="server" CheckBoxes="true"
                                showcheckall="true" Width="300px" Height="150px">
                            </tlk:RadListBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }       
    </script>
</tlk:RadCodeBlock>
