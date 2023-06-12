<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Competency_RadarGapsBetweenPos.ascx.vb"
    Inherits="Profile.ctrlHU_Competency_RadarGapsBetweenPos" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="70px" Scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField runat="server" ID="hidTitleID" />
        <asp:HiddenField runat="server" ID="hidEmpID" />
        <asp:HiddenField runat="server" ID="hidPeriodID" />
        <asp:HiddenField runat="server" ID="hidYear" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" Width="130px"
                        ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Họ tên ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh hiện tại")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh mong muốn")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitleName" SkinID="dDropdownList" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                        Text="<%$ Translate: Tìm kiếm %>" OnClientClicked="OnClientClicked">
                        
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <div id="container" style="min-width: 50%; max-width: 50%; height: 400px; margin: 0 auto;
            float: left">
        </div>
        <div id="container1" style="min-width: 50%; max-width: 50%; height: 400px; margin: 0 auto;
            float: right">
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            Height="550px" EnableShadow="true" Behaviors="Close, Move" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

        function OnClientClicked(sender, args) {
            enableAjax = false;
        }

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
        function InIEvent() {
                
                var container = $('#container').highcharts({
                    credits: {
                        enabled: false
                    },
                    chart: {
                        polar: true,
                        type: 'line',
                        style: {
                            fontFamily: 'Arial'
                               }
                    },

                    title: {
                        text: '<%=title %>'
                    },

                     pane: {
                        size: '90%'
                    },

                    xAxis: {
                        categories: [<%=categories %>],
                        tickmarkPlacement: 'on',
                        lineWidth: 0
                    },

                    yAxis: {
                        gridLineInterpolation: 'polygon',
                        lineWidth: 0,
                        min: 0
                    },

                    tooltip: {
                        shared: true,
                        pointFormat: '<span style="color:{series.color}">{series.name}: <b>{point.y:,.0f}</b><br/>'
                    },
                    legend: {
                        align: 'center',
                        verticalAlign: 'top',
                        y: 30,
                        layout: 'horizontal'
                    },

                    series: [{
                        name: '<%=name1 %>',
                        data: [<%=dataset1 %>],
                        color: '#33FF99',
                        pointPlacement: 'on'
                    }, 
		            {
		                name: '<%=name3 %>',
		                data: [<%=dataset3 %>],
		                color: '#0066FF',
		                pointPlacement: 'on'
		            }]

                });

                 var container1 =  $('#container1').highcharts({
                    credits: {
                        enabled: false
                    },
                    chart: {
                        polar: true,
                        type: 'line',
                        style: {
                            fontFamily: 'Arial'
                               }
                    },

                    title: {
                        text: '<%=title2 %>'
                    },

                     pane: {
                        size: '90%'
                    },

                    xAxis: {
                        categories: [<%=categories2 %>],
                        tickmarkPlacement: 'on',
                        lineWidth: 0
                    },

                    yAxis: {
                        gridLineInterpolation: 'polygon',
                        lineWidth: 0,
                        min: 0
                    },

                    tooltip: {
                        shared: true,
                        pointFormat: '<span style="color:{series.color}">{series.name}: <b>{point.y:,.0f}</b><br/>'
                    },
                    legend: {
                        align: 'center',
                        verticalAlign: 'top',
                        y: 30,
                        layout: 'horizontal'
                    },

                    series: [{
                        name: '<%=name1 %>',
                        data: [<%=dataset2_1 %>],
                        color: '#33FF99',
                        pointPlacement: 'on'
                    }, 
		            {
		                name: '<%=name3 %>',
		                data: [<%=dataset2_3 %>],
		                color: '#0066FF',
		                pointPlacement: 'on'
		            }]
                });
            }   
            $(document).ready(InIEvent);
    </script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>
</tlk:RadCodeBlock>
