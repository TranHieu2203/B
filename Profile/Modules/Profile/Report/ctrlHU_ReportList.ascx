<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ReportList.ascx.vb"
    Inherits="Profile.ctrlHU_ReportList" %>
<%@ Import Namespace="Common" %>
<%--<script src="../../../Scripts/highcharts/highcharts.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/highcharts-3d.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/exporting.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/expor-data.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/offline-exporting.js" type="text/javascript"></script>--%>
<%--<script type="text/javascript" src="../../../Scripts/jquery-3.3.1.min.js"></script>--%>
<style type="text/css">
    .label1 {
        padding-right: 5px;
        padding-left: 5px;
        vertical-align: middle;
    }

    #container3_col1, #container3_col2, #container3_col3, #container3_col4, #container3_col5, #container3_col6 {
        height: 5%;
        margin: 0 auto;
        position: absolute;
        z-index: 1;
        text-align: right;
        left: -1.5%;
        top: 14%
    }

    #container_3_col1, #container_3_col2, #container_3_col3, #container_3_col4, #container_3_col5, #container_3_col6 {
        width: 50%;
        height: 50%;
        margin: 0 auto;
        position: relative
    }
</style>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" Scrolling="None" Width="300px">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <%--<tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>--%>
            <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
                <table class="table-form padding-10" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td>
                            <span class="label1">
                                <%# Translate("Tên báo cáo:")%>
                            </span>
                        </td>
                        <td colspan="3">
                            <tlk:RadComboBox ID="cboType" runat="server" AutoPostBack="true" CausesValidation="false"
                                SkinID="dDropdownList" Width="100%" DropDownAutoWidth="Disabled">
                            </tlk:RadComboBox>
                        </td>
                        <td id="tdLbGender" runat="server">
                             <span class="label1">
                                <%# Translate("Giới tính:")%>
                            </span>
                        </td>
                        <td id="tdCboGender" runat="server">
                            <tlk:RadComboBox ID="cboGender" runat="server" AutoPostBack="true" CausesValidation="false"
                                SkinID="dDropdownList" Width="100%">
                            </tlk:RadComboBox>
                        </td>
                        <td id="tdLbAge" runat="server">
                             <span class="label1">
                                <%# Translate("Độ tuổi lao động:")%>
                            </span>
                        </td>
                        <td id="tdCboAge" runat="server">
                            <tlk:RadComboBox ID="cboDoTuoi" runat="server" AutoPostBack="true" CausesValidation="false"
                                SkinID="dDropdownList">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdLbLearningLevel" runat="server">
                            <span class="label1">
                                <%# Translate("Trình độ học vấn:")%>
                            </span>
                        </td>
                        <td id="tdCboLearningLevel" runat="server">
                            <tlk:RadComboBox ID="cboTDHocVan" runat="server" AutoPostBack="true" CausesValidation="false"
                                SkinID="dDropdownList">
                            </tlk:RadComboBox>
                        </td>
                        <td id="tdLbWorkPlace" runat="server">
                            <span class="label1">
                                <%# Translate("Nơi làm việc:")%>
                            </span>
                        </td>
                        <td id="tdCboWorkPlace" runat="server">
                            <tlk:RadComboBox ID="cboNoiLamViec" runat="server" AutoPostBack="true" CausesValidation="false"
                                SkinID="dDropdownList">
                            </tlk:RadComboBox>
                        </td>
                        <td id="tdLbMonthYear" runat="server">
                            <span class="label1">
                                <label id="lblYear" runat="server"><%# Translate("NĂM:")%></label>
                            </span>
                        </td>
                        <td id="tdCboMonthYear" runat="server">
                            <tlk:RadComboBox ID="cboYear" runat="server" SkinID="dDropdownList" Width="60px" DropDownAutoWidth="Disabled">
                            </tlk:RadComboBox>
                            
                            <span class="label1">
                                <label id="lblMonthMin" runat="server"><%# Translate("THÁNG:")%></label>
                            </span>
                            <tlk:RadComboBox ID="cboMonth" runat="server" SkinID="dDropdownList" Width="60px">
                            </tlk:RadComboBox>
                        </td>
                        <td id="tdLbJobBand" runat="server">
                            <asp:Label ID="lbJobBand" runat="server" CssClass="label1"><%# Translate("Bậc lao động:")%></asp:Label>
                        </td>
                        <td id="tdCboJobBand" runat="server">
                            <tlk:RadComboBox ID="cboJobBand" runat="server" SkinID="dDropdownList">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdLbReportType" runat="server">
                            <span class="label1">
                                <label id="lblReportType" runat="server"><%# Translate("Kiểu báo cáo:")%></label>
                            </span>
                        </td>
                        <td id="tdCboReportType" runat="server">
                            <tlk:RadComboBox ID="cboReportType" runat="server" AutoPostBack="true" CausesValidation="false"
                                SkinID="dDropdownList">
                            </tlk:RadComboBox> 
                        </td>
                        <td id="tdLbExp" runat="server">
                            <span class="label1">
                                <label id="Label1" runat="server"><%# Translate("Thâm niên lao động:")%></label>
                            </span>
                        </td>
                        <td id="tdCboExp" runat="server">
                            <tlk:RadComboBox ID="cboThamNien" runat="server" AutoPostBack="true" CausesValidation="false"
                                SkinID="dDropdownList">
                            </tlk:RadComboBox> 
                        </td>
                        <td> 
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <div id="container_3" style="height: 100%; margin: 0 auto;position:relative">
                    
                <div id="container3" style="height: 5%;
                                            margin: 0 auto;
                                            position: absolute;
                                            z-index: 1;
                                            text-align: right;
                                            left: -0.5%;
                                            top: 6%;">
                    <tlk:RadImageButton ID="btnData" runat="server" Height="15px" ToolTip="Xuất file" OnClientClicked="btnDataClick">
                        <Image Url="/Static/Images/Toolbar/export1.png" />
                    </tlk:RadImageButton>
                </div>
                <div id="container" style="height: 100%; margin: 0 auto">
                </div>
                </div>

                <div id="container2" style="height: 100%; margin: 0 auto; display: flex;flex-wrap: wrap;overflow-y: scroll;">
                    <div id="container_3_col1">
                    
                <div id="container3_col1" >
                    <tlk:RadImageButton ID="btnDataCol1" runat="server" Height="15px" ToolTip="Xuất file" OnClientClicked="btnDataClick">
                        <Image Url="/Static/Images/Toolbar/export1.png" />
                    </tlk:RadImageButton>
                </div>
                    <div id="col1" style="height:100%;margin:0 auto">
                    </div>
                </div>
                    <div id="container_3_col2">
                    
                <div id="container3_col2">
                    <tlk:RadImageButton ID="btnDataCol2" runat="server" Height="15px" ToolTip="Xuất file" OnClientClicked="btnDataClick">
                        <Image Url="/Static/Images/Toolbar/export1.png" />
                    </tlk:RadImageButton>
                </div>
                    <div id="col2" style="height:100%;margin:0 auto">
                    </div>
                </div>
                    <div id="container_3_col3">
                    
                <div id="container3_col3">
                    <tlk:RadImageButton ID="btnDataCol3" runat="server" Height="15px" ToolTip="Xuất file" OnClientClicked="btnDataClick">
                        <Image Url="/Static/Images/Toolbar/export1.png" />
                    </tlk:RadImageButton>
                </div>
                    <div id="col3" style="height:100%;margin:0 auto">
                    </div>
                </div>
                    <div id="container_3_col4">
                    
                <div id="container3_col4">
                    <tlk:RadImageButton ID="btnDataCol4" runat="server" Height="15px" ToolTip="Xuất file" OnClientClicked="btnDataClick">
                        <Image Url="/Static/Images/Toolbar/export1.png" />
                    </tlk:RadImageButton>
                </div>
                    <div id="col4" style="height:100%;margin:0 auto">
                    </div>
                </div>
                    <div id="container_3_col5">
                    
                <div id="container3_col5">
                    <tlk:RadImageButton ID="btnDataCol5" runat="server" Height="15px" ToolTip="Xuất file" OnClientClicked="btnDataClick">
                        <Image Url="/Static/Images/Toolbar/export1.png" />
                    </tlk:RadImageButton>
                </div>
                    <div id="col5" style="height:100%;margin:0 auto">
                    </div>
                </div>
                    <div id="container_3_col6">
                    
                <div id="container3_col6">
                    <tlk:RadImageButton ID="btnDataCol6" runat="server" Height="15px" ToolTip="Xuất file" OnClientClicked="btnDataClick">
                        <Image Url="/Static/Images/Toolbar/export1.png" />
                    </tlk:RadImageButton>
                </div>
                    <div id="col6" style="height:100%;margin:0 auto">
                    </div>
                </div>
                </div>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function InIEvent() {
            $(function () {
                setTimeout(document.getElementById("container3").style.width = $("#container").width() + "px", 2000);
                setTimeout(document.getElementById("container3_col1").style.width = $("#col1").width() + "px", 2000);
                setTimeout(document.getElementById("container3_col2").style.width = $("#col2").width() + "px", 2000);
                setTimeout(document.getElementById("container3_col3").style.width = $("#col3").width() + "px", 2000);
                setTimeout(document.getElementById("container3_col4").style.width = $("#col4").width() + "px", 2000);
                setTimeout(document.getElementById("container3_col5").style.width = $("#col5").width() + "px", 2000);
                setTimeout(document.getElementById("container3_col6").style.width = $("#col6").width() + "px", 2000);
                $('#container_3').show();
                $('#container2').hide();
                var r = '<%=type %>';
                var k = '<%=smonth %>';
                if (r == 'pie') {
                    // Create the chart
                    $('#container').highcharts({
                        credits: {
                            enabled: false
                        },
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie',
                            options3d: {
                                enabled: true,
                                alpha: 45,
                                beta: 0
                            },
                            style: {
                                fontFamily: 'sans-serif'
                            }
                        },
                        tooltip: {
                            pointFormat: '<b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                depth: 60,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.percentage:.1f}%'
                                },
                                showInLegend: true
                            }
                        },
                        legend: {
                            enabled: true,
                            align: 'right',
                            verticalAlign: 'right',
                            y: 25,
                            x: -10,
                            padding: 0,
                            itemMarginTop: 0,
                            itemMarginBottom: 0,
                            itemStyle: {
                                fontSize: '12px',
                                fontWeight: 'normal',
                                color: '#767676'
                            },
                            layout: 'vertical'
                        },
                        exporting: {
                            sourceWidth: 900,
                            sourceHeight: 300,
                            // scale: 2 (default)
                            chartOptions: {
                                subtitle: null
                            },
                        },
                        series: [<%=series %>],
                        title: {
                            text:'<span style="font-weight: bold;color: #767676;font-size:12px;"><%=title %></span>'
                        },
                        colors: [<%=colors %>]
                    });
                }
                else if (r == 'column') {
                    // Create the chart
                    $('#container').highcharts({
                        credits: {
                            enabled: false
                        },
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'column',
                            style: {
                                fontFamily: 'sans-serif'
                            }
                        },
                        xAxis: {
                            categories: <%=category %>,
                            labels: {
                                skew3d: true,
                                style: {
                                    fontSize: '16px'
                                }
                            }
                        },
                        yAxis: {
                            title: {
                                text: null
                            },
                            stackLabels: {
                                enabled: true,
                                style: {
                                    fontWeight: 'bold',
                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                }
                            }
                        },
                        plotOptions: {
                            column: {
                                depth: 25
                            }
                        },
                        plotOptions: {
                            column: {
                                stacking: 'normal',
                                dataLabels: {
                                    enabled: true,
                                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                                }
                            }
                        },
                        exporting: {
                            sourceWidth: 900,
                            sourceHeight: 300,
                            // scale: 2 (default)
                            chartOptions: {
                                subtitle: null
                            }
                        },
                        series: [<%=series %>],
                        title: {
                            text:'<span style="font-weight: bold;color: #767676;font-size:12px;"><%=title %></span>'
                        },
                        colors: [<%=colors %>]

                    });
                } else if (r == 'column1') {
                    // Create the chart
                    $('#container').highcharts({
                        credits: {
                            enabled: false
                        },
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'column'
                        },
                        xAxis: {
                            categories:  <%=category %>,
                            labels: {
                                skew3d: true,
                                style: {
                                    fontSize: '16px'
                                }
                            }
                        },
                        yAxis: {
                            title: {
                                text: null
                            },
                            stackLabels: {
                                enabled: false,
                                style: {
                                    fontWeight: 'bold',
                                    shadow: false,
                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                }
                            }
                        },
                        plotOptions: {
                            column: {
                                depth: 25
                            }
                        },
                        plotOptions: {
                            column: {
                                dataLabels: {
                                    enabled: true,
                                    style: {
                                        textShadow: false,
                                    },
                                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'gray'
                                }
                            }
                        },
                        exporting: {
                            sourceWidth: 900,
                            sourceHeight: 300,
                            // scale: 2 (default)
                            chartOptions: {
                                subtitle: null
                            }
                        },
                        series: [<%=series %>],
                        title: {
                            text:'<span style="font-weight: bold;color: #767676;font-size:12px;"><%=title %></span>'
                        },
                        colors: [<%=colors %>]

                    });
                } else if (r == 'multi') {
                    $('#container_3').hide();
                    $('#container2').show();

                    // COL 1
                    $('#col1').highcharts({
                        credits: {
                            enabled: false
                        },
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie',
                            options3d: {
                                enabled: true,
                                alpha: 45,
                                beta: 0
                            },
                            style: {
                                fontFamily: 'sans-serif'
                            }
                        },
                        tooltip: {
                            pointFormat: '<b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                depth: 20,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.percentage:.1f}%'
                                },
                                showInLegend: true
                            }
                        },
                        legend: {
                            enabled: false,
                            align: 'right',
                            verticalAlign: 'right',
                            y: 25,
                            x: -10,
                            padding: 0,
                            itemMarginTop: 0,
                            itemMarginBottom: 0,
                            itemStyle: {
                                fontSize: '12px',
                                fontWeight: 'normal',
                                color: '#767676'
                            },
                            layout: 'vertical'
                        },
                        exporting: {
                            sourceWidth: 900,
                            sourceHeight: 300,
                            // scale: 2 (default)
                            chartOptions: {
                                subtitle: null
                            }
                        },
                        series: [{
                            name: '<%=name %>',
                            colorByPoint: true,
                            data: [<%=data %>]
                        }],
                        title: {
                            text: '<span style="font-weight: bold;color: #767676;font-size:12px;">Số lượng nhân sự</span>'
                        },
                        colors: ['#F1C40F', '#E67E22', '#95A5A6', '#E74C3C', '#ECF0F1', '#9B59B6', '#2FCC71', '#3498DB', '#1ABC9C']
                    });

                    // COL 2
                    $('#col2').highcharts({
                        credits: {
                            enabled: false
                        },
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie',
                            options3d: {
                                enabled: true,
                                alpha: 45,
                                beta: 0
                            },
                            style: {
                                fontFamily: 'sans-serif'
                            }
                        },
                        tooltip: {
                            pointFormat: '<b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                depth: 20,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.percentage:.1f}%'
                                },
                                showInLegend: true
                            }
                        },
                        legend: {
                            enabled: false,
                            align: 'right',
                            verticalAlign: 'right',
                            y: 25,
                            x: -10,
                            padding: 0,
                            itemMarginTop: 0,
                            itemMarginBottom: 0,
                            itemStyle: {
                                fontSize: '12px',
                                fontWeight: 'normal',
                                color: '#767676'
                            },
                            layout: 'vertical'
                        },
                        exporting: {
                            sourceWidth: 900,
                            sourceHeight: 300,
                            // scale: 2 (default)
                            chartOptions: {
                                subtitle: null
                            }
                        },
                        series: [{
                            name: '<%=name2 %>',
                            colorByPoint: true,
                            data: [<%=data2 %>]
                        }],
                        title: {
                            text: '<span style="font-weight: bold;color: #767676;font-size:12px;">Bậc lao động</span>'
                        },
                        colors: ['#F1C40F', '#E67E22', '#95A5A6', '#E74C3C', '#ECF0F1', '#9B59B6', '#2FCC71', '#3498DB', '#1ABC9C']
                    });

                    // COL 3
                    $('#col3').highcharts({
                        credits: {
                            enabled: false
                        },
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie',
                            style: {
                                fontFamily: 'sans-serif'
                            }
                        },
                        xAxis: {
                            categories: Highcharts.getOptions().lang.shortMonths,
                            labels: {
                                skew3d: true,
                                style: {
                                    fontSize: '16px'
                                }
                            }
                        },
                        yAxis: {
                            title: {
                                text: null
                            },
                            stackLabels: {
                                enabled: false,
                                style: {
                                    fontWeight: 'bold',
                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                }
                            }
                        },
                        plotOptions: {
                            column: {
                                depth: 25
                            }
                        },
                        plotOptions: {
                            column: {
                                dataLabels: {
                                    enabled: true,
                                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                                }
                            }
                        },
                        exporting: {
                            sourceWidth: 900,
                            sourceHeight: 300,
                            // scale: 2 (default)
                            chartOptions: {
                                subtitle: null
                            }
                        },
                        series: [<%=series_tnct %>],
                        title: {
                            text: '<span style="font-weight: bold;color: #767676;font-size:12px;">Thâm niên công tác</span>'
                        },
                        colors: ['#dba510', '#00536b']

                    });

                    // COL 4
                    $('#col4').highcharts({
                        credits: {
                            enabled: false
                        },
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'column',
                            style: {
                                fontFamily: 'sans-serif'
                            }
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                depth: 20,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.percentage:.1f}%'
                                },
                                showInLegend: true
                            }
                        },
                        xAxis: {
                            categories:  <%=category_work_place %>,
                            labels: {
                                skew3d: true,
                                style: {
                                    fontSize: '16px'
                                }
                            }
                        },
                        yAxis: {
                            title: {
                                text: null
                            },
                            stackLabels: {
                                enabled: false,
                                style: {
                                    fontWeight: 'bold',
                                    shadow: false,
                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                }
                            }
                        },
                        plotOptions: {
                            column: {
                                depth: 25
                            }
                        },
                        plotOptions: {
                            column: {
                                dataLabels: {
                                    enabled: true,
                                    style: {
                                        textShadow: false,
                                    },
                                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'gray'
                                }
                            }
                        },
                        exporting: {
                            sourceWidth: 900,
                            sourceHeight: 300,
                            // scale: 2 (default)
                            chartOptions: {
                                subtitle: null
                            }
                        },
                        series: [<%=series_work_place %>],
                        title: {
                            text: '<span style="font-weight: bold;color: #767676;font-size:12px;">Thống kê số lượng nhân viên tại nơi làm việc</span>'
                        },
                        colors: ['#F1C40F', '#E67E22', '#95A5A6', '#E74C3C', '#ECF0F1', '#9B59B6', '#2FCC71', '#3498DB', '#1ABC9C']
                    });

                    // COL 5
                    $('#col5').highcharts({
                        credits: {
                            enabled: false
                        },
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'column',
                            style: {
                                fontFamily: 'sans-serif'
                            }
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                depth: 20,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.percentage:.1f}%'
                                },
                                showInLegend: true
                            }
                        },
                        xAxis: {
                            categories:  <%=category_age %>,
                            labels: {
                                skew3d: true,
                                style: {
                                    fontSize: '16px'
                                }
                            }
                        },
                        yAxis: {
                            title: {
                                text: null
                            },
                            stackLabels: {
                                enabled: false,
                                style: {
                                    fontWeight: 'bold',
                                    shadow: false,
                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                }
                            }
                        },
                        plotOptions: {
                            column: {
                                depth: 25
                            }
                        },
                        plotOptions: {
                            column: {
                                dataLabels: {
                                    enabled: true,
                                    style: {
                                        textShadow: false,
                                    },
                                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'gray'
                                }
                            }
                        },
                        exporting: {
                            sourceWidth: 900,
                            sourceHeight: 300,
                            // scale: 2 (default)
                            chartOptions: {
                                subtitle: null
                            }
                        },
                        series: [<%=series_age %>],
                        title: {
                            text: '<span style="font-weight: bold;color: #767676;font-size:12px;">Thống kê lao động theo độ tuổi lao động</span>'
                        },
                        colors: ['#F1C40F', '#E67E22', '#95A5A6', '#E74C3C', '#ECF0F1', '#9B59B6', '#2FCC71', '#3498DB', '#1ABC9C']
                    });

                    // COL 6
                    $('#col6').highcharts({
                        credits: {
                            enabled: false
                        },
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'column',
                            style: {
                                fontFamily: 'sans-serif'
                            }
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                depth: 20,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.percentage:.1f}%'
                                },
                                showInLegend: true
                            }
                        },
                        xAxis: {
                            categories:  <%=category_learning_lv %>,
                            labels: {
                                skew3d: true,
                                style: {
                                    fontSize: '16px'
                                }
                            }
                        },
                        yAxis: {
                            title: {
                                text: null
                            },
                            stackLabels: {
                                enabled: false,
                                style: {
                                    fontWeight: 'bold',
                                    shadow: false,
                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                }
                            }
                        },
                        plotOptions: {
                            column: {
                                depth: 25
                            }
                        },
                        plotOptions: {
                            column: {
                                dataLabels: {
                                    enabled: true,
                                    style: {
                                        textShadow: false,
                                    },
                                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'gray'
                                }
                            }
                        },
                        exporting: {
                            sourceWidth: 900,
                            sourceHeight: 300,
                            // scale: 2 (default)
                            chartOptions: {
                                subtitle: null
                            }
                        },
                        series: [<%=series_learing_lv %>],
                        title: {
                            text: '<span style="font-weight: bold;color: #767676;font-size:12px;">Thống kê trình độ học vấn</span>'
                        },
                        colors: ['#F1C40F', '#E67E22', '#95A5A6', '#E74C3C', '#ECF0F1', '#9B59B6', '#2FCC71', '#3498DB', '#1ABC9C']
                    });

                }
            });
        }
        $(document).ready(InIEvent);
        function btnDataClick() {
            enableAjax = false;
        }
    </script>
</tlk:RadScriptBlock>