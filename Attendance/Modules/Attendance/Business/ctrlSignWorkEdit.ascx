<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSignWorkEdit.ascx.vb"
    Inherits="Attendance.ctrlSignWorkEdit" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane5" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="100%">
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin nhân viên")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Vị trí công việc")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>

            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin thay đổi")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kỳ Công:")%>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbPeroidName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày:")%>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbStartDate"></asp:Label>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày:")%>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbEndDate"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <hr />
                </td>
            </tr>
            <tr runat="server" id="tr1">
                <td>
                    <asp:Label runat="server" ID="lbSTT1"></asp:Label>
                    <asp:Label runat="server" ID="lbDay1"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode1"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit1"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode1" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr2">
                <td>
                    <asp:Label runat="server" ID="lbSTT2"></asp:Label>
                    <asp:Label runat="server" ID="lbDay2"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode2"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit2"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode2" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr3">
                <td>
                    <asp:Label runat="server" ID="lbSTT3"></asp:Label>
                    <asp:Label runat="server" ID="lbDay3"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode3"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit3"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode3" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr4">
                <td>
                    <asp:Label runat="server" ID="lbSTT4"></asp:Label>
                    <asp:Label runat="server" ID="lbDay4"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode4"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit4"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode4" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr5">
                <td>
                    <asp:Label runat="server" ID="lbSTT5"></asp:Label>
                    <asp:Label runat="server" ID="lbDay5"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode5"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit5"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode5" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr6">
                <td>
                    <asp:Label runat="server" ID="lbSTT6"></asp:Label>
                    <asp:Label runat="server" ID="lbDay6"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode6"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit6"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode6" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr7">
                <td>
                    <asp:Label runat="server" ID="lbSTT7"></asp:Label>
                    <asp:Label runat="server" ID="lbDay7"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode7"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit7"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode7" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr8">
                <td>
                    <asp:Label runat="server" ID="lbSTT8"></asp:Label>
                    <asp:Label runat="server" ID="lbDay8"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode8"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit8"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode8" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr9">
                <td>
                    <asp:Label runat="server" ID="lbSTT9"></asp:Label>
                    <asp:Label runat="server" ID="lbDay9"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode9"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit9"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode9" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr10">
                <td>
                    <asp:Label runat="server" ID="lbSTT10"></asp:Label>
                    <asp:Label runat="server" ID="lbDay10"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode10"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit10"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode10" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr11">
                <td>
                    <asp:Label runat="server" ID="lbSTT11"></asp:Label>
                    <asp:Label runat="server" ID="lbDay11"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode11"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit11"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode11" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr12">
                <td>
                    <asp:Label runat="server" ID="lbSTT12"></asp:Label>
                    <asp:Label runat="server" ID="lbDay12"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode12"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit12"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode12" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr13">
                <td>
                    <asp:Label runat="server" ID="lbSTT13"></asp:Label>
                    <asp:Label runat="server" ID="lbDay13"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode13"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit13"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode13" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr14">
                <td>
                    <asp:Label runat="server" ID="lbSTT14"></asp:Label>
                    <asp:Label runat="server" ID="lbDay14"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode14"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit14"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode14" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr15">
                <td>
                    <asp:Label runat="server" ID="lbSTT15"></asp:Label>
                    <asp:Label runat="server" ID="lbDay15"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode15"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit15"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode15" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr16">
                <td>
                    <asp:Label runat="server" ID="lbSTT16"></asp:Label>
                    <asp:Label runat="server" ID="lbDay16"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode16"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit16"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode16" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr17">
                <td>
                    <asp:Label runat="server" ID="lbSTT17"></asp:Label>
                    <asp:Label runat="server" ID="lbDay17"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode17"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit17"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode17" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr18">
                <td>
                    <asp:Label runat="server" ID="lbSTT18"></asp:Label>
                    <asp:Label runat="server" ID="lbDay18"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode18"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit18"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode18" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr19">
                <td>
                    <asp:Label runat="server" ID="lbSTT19"></asp:Label>
                    <asp:Label runat="server" ID="lbDay19"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode19"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit19"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode19" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr20">
                <td>
                    <asp:Label runat="server" ID="lbSTT20"></asp:Label>
                    <asp:Label runat="server" ID="lbDay20"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode20"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit20"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode20" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr21">
                <td>
                    <asp:Label runat="server" ID="lbSTT21"></asp:Label>
                    <asp:Label runat="server" ID="lbDay21"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode21"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit21"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode21" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr22">
                <td>
                    <asp:Label runat="server" ID="lbSTT22"></asp:Label>
                    <asp:Label runat="server" ID="lbDay22"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode22"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit22"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode22" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr23">
                <td>
                    <asp:Label runat="server" ID="lbSTT23"></asp:Label>
                    <asp:Label runat="server" ID="lbDay23"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode23"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit23"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode23" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr24">
                <td>
                    <asp:Label runat="server" ID="lbSTT24"></asp:Label>
                    <asp:Label runat="server" ID="lbDay24"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode24"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit24"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode24" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr25">
                <td>
                    <asp:Label runat="server" ID="lbSTT25"></asp:Label>
                    <asp:Label runat="server" ID="lbDay25"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode25"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit25"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode25" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr26">
                <td>
                    <asp:Label runat="server" ID="lbSTT26"></asp:Label>
                    <asp:Label runat="server" ID="lbDay26"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode26"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit26"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode26" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr27">
                <td>
                    <asp:Label runat="server" ID="lbSTT27"></asp:Label>
                    <asp:Label runat="server" ID="lbDay27"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode27"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit27"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode27" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr28">
                <td>
                    <asp:Label runat="server" ID="lbSTT28"></asp:Label>
                    <asp:Label runat="server" ID="lbDay28"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode28"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit28"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode28" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr29">
                <td>
                    <asp:Label runat="server" ID="lbSTT29"></asp:Label>
                    <asp:Label runat="server" ID="lbDay29"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode29"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit29"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode29" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr30">
                <td>
                    <asp:Label runat="server" ID="lbSTT30"></asp:Label>
                    <asp:Label runat="server" ID="lbDay30"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode30"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit30"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode30" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr runat="server" id="tr31">
                <td>
                    <asp:Label runat="server" ID="lbSTT31"></asp:Label>
                    <asp:Label runat="server" ID="lbDay31"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lbCode31"></asp:Label>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbDayEdit31"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCode31" AutoPostBack="true"></tlk:RadComboBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
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

        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }

        function clientButtonClicking(sender, args) {
        }
    </script>
</tlk:RadCodeBlock>

