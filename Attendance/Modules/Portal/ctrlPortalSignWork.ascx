<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalSignWork.ascx.vb"
    Inherits="Attendance.ctrlPortalSignWork" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hiStartDate" runat="server" />
<asp:HiddenField ID="hiEndDate" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" >
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="TopPanel" runat="server" MinHeight="130" Height="82px" Scrolling="None">
                <table class="table-form" style="padding-top: 8px;">
                    <tr>
                        <td></td>
                        <td>
                            <asp:RadioButton runat="server" ID="chkDefault" GroupName="grType"  Text="<%$ Translate: Ca làm việc %>" />
                        </td>
                        <td></td>
                        <td>
                            <asp:RadioButton runat="server" ID="chkSign" GroupName="grType"  Text="<%$ Translate: Ca đăng ký %>" />
                        </td>
                        <td class="lb" >
                            <label id="lbStatus1" runat="server"><%# Translate("Trạng thái")%></label>
                            
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboStatus1">
                            </tlk:RadComboBox>
                        </td>
                        <td></td>
                        <td></td>
                         <td style="width: 60px; text-align: right">
                             
                            <%# Translate("Chú thích :")%>
                        </td>
                        <td style="width: 50px;">
                            <p style="border: 1px solid #000000; background-color: #ff0000; height: 17px; margin: 0 auto;"></p>
                        </td>
                        <td style="text-align: left">
                            <%# Translate("Ngày lễ, tết")%>
                        </td>
                        <td style="width: 50px;">
                            <p style="border: 1px solid #000000; background-color: rgb(158 230 114); height: 17px; margin: 0 auto;"></p>
                        </td>
                        <td style="text-align: left">
                            <%# Translate("Ca thay đổi")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" runat="server" SkinID="dDropdownList" AutoPostBack="true"
                                Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td style="width: 100px; text-align: right">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriodId" runat="server" SkinID="dDropdownList" AutoPostBack="true"
                                Width="150px">
                            </tlk:RadComboBox>
                        </td>
                        <td style=" text-align: right">
                            <%--<%# Translate("Đối tượng nhân viên")%>--%>
                            <label>Đối tượng nhân viên</label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboEmpObj" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td></td>
                        <td>
                            <tlk:RadButton ID="btnSearchEmp" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                       
                        <td style="width: 15px"></td>
                        <td style="width: 50px;">
                            <p style="border: 1px solid #000000; background-color: #ffff00; height: 17px; margin: 0 auto;"></p>
                        </td>
                        <td style="text-align: left">
                            <%# Translate("Ngày thứ 7, CN")%>
                        </td>
                    </tr>
                    
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgSignWork" runat="server" Height="100%">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,STATUS,ID_REGGROUP" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,STATUS,ID_REGGROUP">
                        <Columns>
                            <tlk:GridClientSelectColumn Visible="false" UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                UniqueName="STATUS_NAME" HeaderStyle-Width="100px" SortExpression="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phê duyệt %>" DataField="APPROVER_NAME"
                                UniqueName="APPROVER_NAME" HeaderStyle-Width="100px" SortExpression="APPROVER_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                                UniqueName="VN_FULLNAME" HeaderStyle-Width="120px" SortExpression="VN_FULLNAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" HeaderStyle-Width="150px" SortExpression="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                SortExpression="ORG_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridTemplateColumn HeaderText="D1" AllowFiltering="false" Visible="false" UniqueName="D1">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D1") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D2" AllowFiltering="false" Visible="false" UniqueName="D2">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D2")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D3" AllowFiltering="false" Visible="false" UniqueName="D3">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D3")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D4" AllowFiltering="false" Visible="false" UniqueName="D4">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D4")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D5" AllowFiltering="false" Visible="false" UniqueName="D5">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D5")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D6" AllowFiltering="false" Visible="false" UniqueName="D6">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D6") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D7" AllowFiltering="false" Visible="false" UniqueName="D7">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D7")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D8" AllowFiltering="false" Visible="false" UniqueName="D8">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D8")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D9" AllowFiltering="false" Visible="false" UniqueName="D9">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D9")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D10" AllowFiltering="false" Visible="false" UniqueName="D10">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D10")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D11" AllowFiltering="false" Visible="false" UniqueName="D11">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D11")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D12" AllowFiltering="false" Visible="false" UniqueName="D12">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D12") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D13" AllowFiltering="false" Visible="false" UniqueName="D13">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D13")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D14" AllowFiltering="false" Visible="false" UniqueName="D14">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D14")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D15" AllowFiltering="false" Visible="false" UniqueName="D15">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D15") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D16" AllowFiltering="false" Visible="false" UniqueName="D16">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D16") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D17" AllowFiltering="false" Visible="false" UniqueName="D17">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D17")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D18" AllowFiltering="false" Visible="false" UniqueName="D18">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D18")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D19" AllowFiltering="false" Visible="false" UniqueName="D19">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D19")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D20" AllowFiltering="false" Visible="false" UniqueName="D20">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D20")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D21" AllowFiltering="false" Visible="false" UniqueName="D21">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D21")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D22" AllowFiltering="false" Visible="false" UniqueName="D22">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D22")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D23" AllowFiltering="false" Visible="false" UniqueName="D23">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D23")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D24" AllowFiltering="false" Visible="false" UniqueName="D24">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D24")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D25" AllowFiltering="false" Visible="false" UniqueName="D25">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D25")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D26" AllowFiltering="false" Visible="false" UniqueName="D26">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D26")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D27" AllowFiltering="false" Visible="false" UniqueName="D27">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D27")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D28" AllowFiltering="false" Visible="false" UniqueName="D28">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D28")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D29" AllowFiltering="false" Visible="false" UniqueName="D29">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D29") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D30" AllowFiltering="false" Visible="false" UniqueName="D30">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D30") %>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn HeaderText="D31" AllowFiltering="false" Visible="false" UniqueName="D31">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("D31")%>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                         
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không phê duyệt %>" DataField="REASON"
                                UniqueName="REASON" HeaderStyle-Width="100px" SortExpression="REASON" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EDIT') {
                OpenEditWindow();
                args.set_cancel(true);
            }
            if (item.get_commandName() == "EXPORT") {
                var grid = $find("<%=rgSignWork.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                // ResizeSplitter();
            } else if (item.get_commandName() == "DELETE") {
                enableAjax = false;

            }
            else {

            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        var winH;
        var winW;

        function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() - 210;
            winW = $(window).width() - 90;
            $("#ctl00_MainContent_ctrlPortalSignWork_RadSplitter1").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlPortalSignWork_RadSplitter3").stop().animate({ height: winH, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPortalSignWork_MainPane").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlPortalSignWork_rgSignWork").stop().animate({ height: winH - 118, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPortalSignWork_RadPane1").stop().animate({ height: winH, width: winW }, 0);

            Sys.Application.add_load(SizeToFitMain);
        }

        SizeToFitMain();

        $(document).ready(function () {
            SizeToFitMain();
            $('#ctl00_MainContent_ctrlPortalSignWork_rgSignWork_ctl00 tbody tr td').each(function () {
                var content = $(this).text().trim();
                if (content.indexOf('(HASVALUE)') > -1) {
                    if ($(this).attr('Style') == null) {
                        $(this).attr('Style', 'color:rgb(26, 178, 76) ; font-weight : 800 !important;');
                    } else {
                        $(this).attr('Style', $(this).attr('Style') + 'color:rgb(26, 178, 76) ; font-weight : 800 !important;');
                    }
                    $(this).text(content.replace('(HASVALUE)', ''));
                }

            })
        });
        $(window).resize(function () {
            SizeToFitMain();
        });

    </script>
</tlk:RadScriptBlock>
