<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_Stage.ascx.vb"
    Inherits="Recruitment.ctrlRC_Stage" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="260px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width:130px">
                            <%# Translate("Năm tuyển dụng")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntYear" runat="server" NumberFormat-DecimalDigits="0"
                                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                                MaxValue="9999">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm %>">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnCostallocate" runat="server" Text="<%$ Translate: Phân bổ chi phí tuyển dụng %>"
                                OnClientClicking="btnCostallocateClick" AutoPostBack="false" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="item-head" colspan="4">
                            <%# Translate("Thông tin đợt tuyển dụng")%>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <%--  <fieldset>--%>
                            <%--      <legend>
                                    <%# Translate("Thông tin đợt tuyển dụng")%></legend>--%>
                            <table>
                                <tr>
                                    <td class="lb" style="width:130px">
                                        <%# Translate("Đơn vị khai thác")%><span class="lbReq">*</span>
                                    </td>
                                    <td colspan="3">
                                        <tlk:RadTextBox ID="txtOrganizationName" runat="server" Width="100%"
                                            ReadOnly="true">
                                        </tlk:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Tên đợt tuyển dụng")%><span class="lbReq">*</span>
                                    </td>
                                    <td colspan="3">
                                        <tlk:RadTextBox ID="txtTitle" runat="server" Width="100%">
                                        </tlk:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTitle"
                                            runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên đợt tuyển dụng. %>"
                                            Text="<%$ Translate: Bạn phải nhập tên đợt tuyển dụng. %>" ToolTip="<%$ Translate: Bạn phải nhập tên đợt tuyển dụng. %>"> </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Ngày bắt đầu")%><span class="lbReq">*</span>
                                    </td>
                                    <td>
                                        <tlk:RadDatePicker ID="dateStart" runat="server">
                                            <DateInput DateFormat="dd/MM/yyyy">
                                            </DateInput>
                                        </tlk:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="dateStart" runat="server"
                                            ErrorMessage="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>"> </asp:RequiredFieldValidator>
                                    </td>
                                    <td class="lb">
                                        <%# Translate("Ngày kết thúc")%><span class="lbReq">*</span>
                                    </td>
                                    <td>
                                        <tlk:RadDatePicker ID="dateEnd" runat="server">
                                            <DateInput DateFormat="dd/MM/yyyy">
                                            </DateInput>
                                        </tlk:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="reqEndDate" ControlToValidate="dateEnd" runat="server"
                                            ErrorMessage="<%$ Translate: Bạn phải nhập ngày kết thúc. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày kết thúc. %>"> </asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="dateEnd"
                                            Type="Date" ControlToCompare="dateStart" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày bắt đầu %>"
                                            ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày bắt đầu %>"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Nguồn tuyển dụng")%>
                                    </td>
                                    <td>
                                        <tlk:RadComboBox ID="cbbSourceOfRec" runat="server" AutoPostBack="false">
                                        </tlk:RadComboBox>
                                    </td>
                                    <td class="lb">
                                        <%# Translate("Chi phí dự kiến")%>
                                    </td>
                                    <td>
                                        <tlk:RadNumericTextBox ID="rntCostEstimate" runat="server" SkinID="Money" ReadOnly="true">
                                        </tlk:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Ghi chú")%>
                                    </td>
                                    <td colspan="3">
                                        <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                                        </tlk:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                            <%--    </fieldset>--%>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="false" CellSpacing="0" GridLines="None"
                    Height="100%">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn HeaderStyle-Width="40px" UniqueName="ClientSelectColumn">
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Visible="False">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị khai thác %>" DataField="ORGANIZATIONNAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đợt tuyển dụng %>" DataField="TITLE" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="STARTDATE"
                                HeaderStyle-Width="120px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="ENDDATE"
                                HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm tuyển dụng %>" DataField="YEAR"
                                HeaderStyle-Width="90px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí dự kiến %>" DataField="COSTESTIMATE"
                                AllowFiltering="false" HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nguồn tuyển dụng %>" DataField="SOURCE_NAME" />
                            <tlk:GridBoundColumn DataField="SOURCE_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" />
                        </Columns>
                        <HeaderStyle Width="150px" />
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


        function btnCostallocateClick(sender, args) {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck > 1) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_Costallocate&group=Business&Stage_ID=' + id, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.center, pos.middle);
            //            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            oWindow.setSize(800, 500);
        }

        function OnClientClose(oWnd, args) {
            //            DeselectAllInGrid();
            window.location.reload(true);
        }

        function DeselectAllInGrid() {
            var grid = $find("<%=rgData.ClientID %>");

            if (grid) {
                var MasterTable = grid.get_masterTableView();
                var Rows = MasterTable.get_dataItems();
                for (var i = 0; i < Rows.length; i++) {
                    var row = Rows[i];
                    row.set_selected(false);
                }
            }
        }

    </script>
</tlk:RadCodeBlock>
