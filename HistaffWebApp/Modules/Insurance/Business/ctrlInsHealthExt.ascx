<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsHealthExt.ascx.vb"
    Inherits="Insurance.ctrlInsHealthExt" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" Width="300px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="120px" Scrolling="None">
                <div style="display: none;">
                    <tlk:RadTextBox ID="txtID" Text="0" runat="server">
                    </tlk:RadTextBox>
                </div>
                <div>
                    <fieldset style="width:auto; height:auto">
                        <legend>
						    <%# Translate("Thông tin tìm kiếm")%>
					    </legend>
                        <table with="100%">
                           <tr>
                                <td style="padding-bottom:5px">
                                    <%# Translate("Mã nhân viên")%>
                                </td>
                                <td style="padding-bottom:5px">
                                    <tlk:RadTextBox ID="txtEMPLOYEE_ID" MaxLength="250" runat="server">
                                    </tlk:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom:5px">
                                    <%# Translate("Nhân viên nghỉ việc")%>                            
                                </td>
                                <td style="padding-bottom:5px">
                                    <tlk:RadButton ID="chkEmployeeTerminate" AutoPostBack="false" ToggleType="CheckBox"  ButtonType="ToggleButton" runat="server">
                                    </tlk:RadButton>  
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom:5px">                                                     
                                </td>
                                <td style="padding-bottom:5px">                            
                                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFind" TabIndex="8" runat="server"
                                        Text="<%$ Translate: FIND %>" ValidationGroup="Search" SkinID="ButtonFind" OnClick="btnFIND_Click">
                                    </tlk:RadButton>
                                </td>
                            </tr>
                        </table>
                    </fieldset>					    
                </div>                               
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar3" runat="server">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="txtEFFECTIVE_FROM_DATE" TabIndex="4">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqtxtEFFECTIVE_FROM_DATE" ControlToValidate="txtEFFECTIVE_FROM_DATE"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Từ ngày. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Từ ngày. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="txtEFFECTIVE_TO_DATE" TabIndex="5">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqtxtEFFECTIVE_TO_DATE" ControlToValidate="txtEFFECTIVE_TO_DATE"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Đến ngày. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Đến ngày. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar2" runat="server">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgGridData" runat="server" Height="100%" AllowPaging="True" AllowSorting="True"
                    CellSpacing="0" ShowStatusBar="true" AllowMultiRowSelection="true" GridLines="None"
                    PageSize="20" AutoGenerateColumns="false" AllowFilteringByColumn="true">
                    <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_CODE,VN_FULLNAME,ORG_NAME,INS_ORG_NAME,INS_ORG_ID,EMPID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID%>" DataField="ID" UniqueName="ID"
                                SortExpression="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"  HeaderStyle-Width="90px" FilterControlWidth="60%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                                UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME"   HeaderStyle-Width="150px" FilterControlWidth="80%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME"   HeaderStyle-Width="170px" FilterControlWidth="80%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị bảo hiểm %>" DataField="INS_ORG_NAME"
                                UniqueName="INS_ORG_NAME" SortExpression="INS_ORG_NAME"   HeaderStyle-Width="230px" FilterControlWidth="80%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số thẻ BHYT %>" DataField="HEALTH_NUMBER"
                                UniqueName="HEALTH_NUMBER" SortExpression="HEALTH_NUMBER" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hiệu lực từ ngày %>" DataFormatString="{0:dd/MM/yyyy}"
                                DataField="health_effect_from_date" UniqueName="health_effect_from_date" SortExpression="health_effect_from_date"   HeaderStyle-Width="100px" FilterControlWidth="60%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hiệu lực đến ngày %>" DataFormatString="{0:dd/MM/yyyy}"
                                DataField="health_effect_to_date" UniqueName="health_effect_to_date" SortExpression="health_effect_to_date"   HeaderStyle-Width="100px" FilterControlWidth="60%" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị bảo hiểm %>" DataField="INS_ORG_ID"
                                UniqueName="INS_ORG_ID" SortExpression="INS_ORG_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMPID" UniqueName="EMPID"
                                SortExpression="EMPID" Visible="false" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />