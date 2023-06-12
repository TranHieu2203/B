<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDashboardHome.ascx.vb"
    Inherits="Profile.ctrlDashboardHome" %>
<link href="../../../Styles/userCustom.css" rel="stylesheet" type="text/css" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<script src="../../../Scripts/jquery-3.3.1.min.js"></script>
<script src="../../../Scripts/noty/jquery.noty.js"></script>
<script src="../../../Scripts/noty/layouts/center.js"></script>
<script src="../../../Scripts/noty/layouts/topCenter.js"></script>
<script src="../../../Scripts/noty/themes/default.js"></script>
<style>
    #RAD_SPLITTER_ctl00_MainContent_RadSplitter1 {
        border: none;
        background: #f9f9f9;
    }
    
    input[type="checkbox"] {
        margin: 0px;
        height: 16px;
        width: 19px;
        opacity: 0;
    }
    #ctrlDashboardHome_rgContract {
        background: #ffffff !important;
        border: none;
    }

    #RadPaneMain {
        border: none;
    }

    #RAD_SPLITTER_RadSplitter1 {
        border: none;
    }

    .RadGrid_Metro .rgAltRow {
        background: #ffffff !important;
    }

    .RadGrid_Metro .rgSelectedRow, .RadGrid_Metro .rgHoveredRow {
        background: #ededed !important;
        color: #000;
    }

        .RadGrid_Metro .rgSelectedCell a, .RadGrid_Metro .rgSelectedRow a {
            color: #2196f3 !important;
        }

    tr.rgFilterRow td {
        border-left: 1px solid #e7e7e7;
    }

        tr.rgFilterRow td:first-child {
            border: none;
        }
	.RadGrid_Metro .rgHeader, .RadGrid_Metro .rgHeader a {
		color: #8b8b8b !important;
	}
    
    .gridcenter{
        text-align:center !important;
    }
    .radpaneKind{
        position:absolute
    }
    #ctrlDashboardHome_cboKindRemind {
        width:135% !important
    }
    #RAD_SPLITTER_PANE_CONTENT_ctrlDashboardHome_RadPane2{
        overflow: unset!important;
    }
    #ctrlDashboardHome_rgContract_GridData{
        height: 80% !important;
    }
</style>
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%" orientation="Horizontal"
    skinid="Demo">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Width="100%"  Height="30px" CssClass="radpaneKind">
        <table class="table-form">
                <tr>
                    <td class="lb">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Translate: Loại nhắc nhở %>" />
                    </td>
                    <td colspan="2">
                        <tlk:RadComboBox ID="cboKindRemind" runat="server" CausesValidation="false" AutoPostBack="true" >
                        </tlk:RadComboBox>
                    </td>
                </tr>
            </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Width="100%">
        <tlk:radgrid id="rgContract" runat="server" height="100%" allowfilteringbycolumn="true">
            <mastertableview commanditemsettings-exporttoexceltext="Xuất dữ liệu"
                commanditemsettings-exporttocsvtext="Chuyển" commanditemsettings-exporttopdftext="Báo tăng thai sản đi làm lại" commanditemdisplay="Top"
                DataKeyNames="ORG_DESC"
                clientdatakeynames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,FULLNAME,REMIND_DATE,GENDER,REMIND_TYPE,REMIND_NAME,USERNAME,TITLE_NAME,ORG_NAME,JOIN_DATE,VALUE,LINK_POPUP,WORK_EMAIL">
                <commanditemtemplate>
                    <div style="padding: 5px 0 0 5px; display: flex; flex-direction: row-reverse">
                        <div style="margin-bottom: 5px;">
                            <tlk:radbutton id="btnSendMail" runat="server" icon-primaryiconurl="~/Static/Images/Toolbar/send_email.png"
                                causesvalidation="false" width="85px" text="<%$ Translate: Gửi mail %>" tooltip="MAIL"
                                commandname="SendMail">
                            </tlk:radbutton>
                        </div>
                        <div style="margin-bottom: 5px;">
                            <tlk:radbutton id="btnExport" runat="server" icon-primaryiconurl="~/Static/Images/Toolbar/export1.png"
                                causesvalidation="false" width="70px" text="<%$ Translate: Excel %>" tooltip="Export"
                                commandname="EXPORT" onclientclicking="SelectGridClick">
                            </tlk:radbutton>
                        </div>
                    </div>
                </commanditemtemplate>
                <commanditemsettings showaddnewrecordbutton="false" showexporttocsvbutton="false"
                    showrefreshbutton="false" showexporttoexcelbutton="true">
                </commanditemsettings>
                <columns>
                    <tlk:gridclientselectcolumn headerstyle-horizontalalign="Center" headerstyle-width="30px"
                        itemstyle-horizontalalign="Center" uniquename="cbStatus">
                        <headerstyle horizontalalign="Center" width="30px" />
                        <itemstyle horizontalalign="Center" />
                    </tlk:gridclientselectcolumn>
                    <tlk:gridtemplatecolumn uniquename="LINK_POPUP" allowfiltering="false" headertext="<%$ Translate: Liên kết nhanh %>">
                        <itemtemplate>
                            <asp:LinkButton ID="lbtnLink" runat="server" Text="Xem" Style="text-decoration: underline !important; color: Blue">
                            </asp:LinkButton>
                        </itemtemplate>
                        <itemstyle horizontalalign="Center" verticalalign="Middle" />
                        <headerstyle width="90px" />
                    </tlk:gridtemplatecolumn>
                    <tlk:gridboundcolumn datafield="REMIND_TYPE" visible="false" uniquename="REMIND_TYPE"
                        sortexpression="REMIND_TYPE">
                    </tlk:gridboundcolumn>
                    <tlk:gridboundcolumn headertext="<%$ Translate: Loại nhắc nhở %>" datafield="REMIND_NAME"
                        uniquename="REMIND_NAME" sortexpression="REMIND_NAME">
                        <headerstyle width="200px" />
                    </tlk:gridboundcolumn>
                    <tlk:gridboundcolumn headertext="<%$ Translate: Ngày hết hạn %>" datafield="REMIND_DATE"
                        uniquename="REMIND_DATE" sortexpression="REMIND_DATE" dataformatstring="{0:dd/MM/yyyy}" ItemStyle-CssClass="gridcenter">
                        <headerstyle width="100px" />
                    </tlk:gridboundcolumn>
                    <tlk:gridboundcolumn headertext="<%$ Translate: Mã nhân viên %>" datafield="EMPLOYEE_CODE"
                        uniquename="EMPLOYEE_CODE" sortexpression="EMPLOYEE_CODE"  ItemStyle-CssClass="gridcenter">
                        <headerstyle width="80px" />
                    </tlk:gridboundcolumn>
                    <tlk:gridboundcolumn headertext="<%$ Translate: Họ và tên %>" datafield="FULLNAME"
                        uniquename="FULLNAME" sortexpression="FULLNAME">
                    </tlk:gridboundcolumn>
                    <tlk:gridboundcolumn headertext="<%$ Translate: Phòng ban %>" datafield="ORG_NAME"
                        uniquename="ORG_NAME" sortexpression="ORG_NAME">
                    </tlk:gridboundcolumn>
                    <tlk:gridboundcolumn headertext="<%$ Translate: Vị trí công việc %>" datafield="TITLE_NAME"
                        uniquename="TITLE_NAME" sortexpression="TITLE_NAME">
                        <headerstyle width="250px" />
                    </tlk:gridboundcolumn>
                    <tlk:gridboundcolumn headertext="<%$ Translate: Email %>" datafield="WORK_EMAIL"
                        uniquename="WORK_EMAIL" sortexpression="WORK_EMAIL">
                    </tlk:gridboundcolumn>
                </columns>
            </mastertableview>
            <clientsettings>
                <clientevents oncommand="RaiseCommand" />
                <ClientEvents OnGridCreated="GridCreated" />
            </clientsettings>
        </tlk:radgrid>
    </tlk:radpane>
</tlk:radsplitter>
<tlk:radscriptblock id="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oWnd1;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function RaiseCommand(sender, eventArgs) {
            var item = eventArgs.get_commandName();
            if (item == "ExportToExcel") {
                enableAjax = false;
            }
        }

        function POPUP(value) {
            var RadWinManager = GetRadWindow().get_windowManager();
            RadWinManager.set_visibleTitlebar(true);
            var oWindow = RadWinManager.open(value);
            var pos = $("html").offset();
            oWindow.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close); //set the desired behavioursvar pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(parent.document.body.clientWidth, parent.document.body.clientHeight);
            setTimeout(function () { oWindow.setActive(true); }, 0);
            return false;
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function GetRadWindow() {
            var oWindow = null;

            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;

            return oWindow;
        }
        function SelectGridClick(sender, args) {
            var item = args.get_commandName();
            if (item == "SendMail") {
                var bCheck = $find('<%# rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var notifyMgs = Notify("Vui lòng chọn dữ liệu thao tác", "warning");
                    args.set_cancel(true);
                    enableAjax = true;
                }
                else {
                    enableAjax = false;
                }
            }
            else if (item == "EXPORT") {
                enableAjax = false;
            }
            else
                enableAjax = true;
        }

        function setGridCheckbox(elem) {
            if ($(elem).is(':checked')) {
                $(elem).prev('.tvc-grid-checkbox').addClass('checked');
            } else {
                $(elem).prev('.tvc-grid-checkbox').removeClass('checked');
            }
        }

        // on input change, change custom checkbox 
        $('body').on('change', '.rgMasterTable input[type="checkbox"]', function () {
            setGridCheckbox($(this));
        });

        function GridCreated() {
            // apply custom checkbox on page ready
            $('.rgMasterTable input[type="checkbox"]').before('<span class="tvc-grid-checkbox">');
        }
    </script>
</tlk:radscriptblock>
