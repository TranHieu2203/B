<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsModifyInfor.ascx.vb"
    Inherits="Insurance.ctrlInsModifyInfor" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="320" Width="330px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="180px" Scrolling="None">
                <div style="display: none;">
                    <tlk:RadTextBox ID="txtID" Text="0" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtEMPID" Text="0" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtOLD_SOCIAL_NUMBER1" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtOLD_NAME3" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtOLD_BIRTHDAY4" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtOLD_BIRTH_PLACE4" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtOLD_ID_NO5" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtOLD_ID_PLACE5" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtOLD_ID_DATE5" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtOLD_GENDER6" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtAREA_NM7" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtOLD_PER_ADDRESS" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtOLD_CON_ADDRESS" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <%--    '1	Điều chỉnh số sổ BHXH
                            '2	Điều chỉnh họ tên
                            '3	Điều chỉnh ngày sinh            
                            '4	Điều chỉnh số CMND
                            '5	Điều chỉnh giới tính
                            '6	Thay đổi nơi khám chữa bệnh
                            '7  Điều chỉnh nơi cấp CMND
                            '8  Điều chỉnh ngày cấp CMND
                            '9  Điều chỉnh nơi sinh
                            '10  Điều chỉnh địa chỉ thường trú
                            '11  Điều chỉnh địa chỉ liên hệ
                    --%>
                </div>
                <div>
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <%# Translate("Thông tin tìm kiếm")%>
                        </legend>
                        <table width="100%" class="td-padding">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Từ ngày")%>
                                </td>
                                <td>
                                    <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtFROMDATE"
                                        TabIndex="1">
                                    </tlk:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Đến ngày")%>
                                </td>
                                <td>
                                    <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtTODATE"
                                        TabIndex="2">
                                    </tlk:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Nhân viên")%>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtEMPLOYEEID_SEARCH" MaxLength="250" runat="server" TabIndex="3">
                                    </tlk:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Loại điều chỉnh")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox DataValueField="ID" DataTextField="NAME" ID="ddlMODIFIER_TYPE_SEARCH"
                                        runat="server" TabIndex="4" ToolTip="<%$ Translate: Loại điều chỉnh %>" AutoPostBack="true"
                                        EmptyMessage="<%$ Translate: Loại điều chỉnh %>">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkSTATUS" runat="server" Text='<%# Translate("Đã nghỉ việc")%>' />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFind" TabIndex="5" runat="server"
                                        Text="<%$ Translate: Tìm kiếm %>" ValidationGroup="Search" SkinID="ButtonFind"
                                        OnClick="btnFIND_Click">
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
            <tlk:RadPane ID="RadPane1" runat="server" Height="300px" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />    
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width: 100px;">
                            <%# Translate("MSNV")%><span class="lbReq">*</span>
                        </td>
                        <td style="width: 250px;">
                            <tlk:RadTextBox ID="txtEMPLOYEE_ID" runat="server"  ReadOnly="true">
                            </tlk:RadTextBox>
                            <tlk:RadButton EnableEmbeddedSkins="false" runat="server" ID="btnSearchEmp" SkinID="ButtonView"
                                TabIndex="5" CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqtxtEMPLOYEE_ID" ControlToValidate="txtEMPLOYEE_ID"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Nhân viên. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Nhân viên. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="width: 100px;">
                            <%# Translate("Họ & tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtFULLNAME" ReadOnly="true" runat="server" TabIndex="6">
                            </tlk:RadTextBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Loại điều chỉnh")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="ddlINS_MODIFIER_TYPE_ID" AutoPostBack="true" runat="server" skinId="Number"
                                TabIndex="7" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqddlINS_MODIFIER_TYPE_ID" ControlToValidate="ddlINS_MODIFIER_TYPE_ID"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Loại điều chỉnh. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Loại điều chỉnh. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Số sổ BHXH")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtSOCIAL_NUMBER" ReadOnly="true" runat="server" TabIndex="7">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Lý do điều chỉnh")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtREASON" MaxLength="500" Width="100%" runat="server" TabIndex="8">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Thông tin cũ")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtOLD_INFO" MaxLength="500" runat="server" Width="100%" TabIndex="9" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Thông tin mới")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtNEW_INFO" MaxLength="500" runat="server" TabIndex="10">
                            </tlk:RadTextBox>
                            <tlk:RadComboBox ID="ddlHEALTH_AREA_INS_ID" Width="405px" Visible="false" runat="server"
                                TabIndex="10">
                            </tlk:RadComboBox>
                            <tlk:RadDatePicker ID="txtBIRTH_DATE" DateInput-DisplayDateFormat="dd/MM/yyyy" Visible="false"
                                TabIndex="10" runat="server">
                            </tlk:RadDatePicker>
                            <tlk:RadDatePicker ID="txtID_DATE" DateInput-DisplayDateFormat="dd/MM/yyyy" Visible="false"
                                TabIndex="10" runat="server">
                            </tlk:RadDatePicker>
                            <asp:CheckBox ID="chkIsUpdate" runat="server" Text='<%# Translate("Có cập nhật HSNS")%>'
                                TabIndex="11" />
                            <asp:RequiredFieldValidator ID="reqtxtNEW_INFO" ControlToValidate="txtNEW_INFO" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Thông tin mới. %>" ToolTip="<%$ Translate: Bạn phải nhập Thông tin mới. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="trPerAddress" visible="false">
                        <td class="lb" style="width: 130px;">
                            <%# Translate("Địa chỉ thường trú")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtPerAddress" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <%--<td class="lb" style="width: 150px;">
                            <%# Translate("Quốc gia")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPer_Country" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>--%>
                    </tr>
                    <tr runat="server" id="trPerAddress1" visible="false">
                        <td class="lb">
                            <%# Translate("Thành phố")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPer_Province" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb" style="width: 160px;">
                            <%# Translate("Quận huyện")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPer_District" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb" style="width: 160px;">
                            <%# Translate("Xã phường")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPer_Ward" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trConAddress" visible="false">
                        <td class="lb">
                            <%# Translate("Địa chỉ liên lạc")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtContactAddress" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trConAddress1" visible="false">
                        <td class="lb">
                            <%# Translate("Thành phố")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboCon_Province" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Quận huyện")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboCon_District" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Xã phường")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboCon_Ward" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày điều chỉnh")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" ID="txtMODIFIER_DATE"
                                TabIndex="12" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqtxtMODIFIER_DATE" ControlToValidate="txtMODIFIER_DATE"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày điều chỉnh. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Ngày điều chỉnh. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtNote" Width="400px" runat="server" TabIndex="13">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar2" runat="server">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgGridData" runat="server" Height="95%" AllowPaging="True" AllowSorting="True"
                    CellSpacing="0" ShowStatusBar="true" AllowMultiRowSelection="true" GridLines="None"
                    PageSize="20" AutoGenerateColumns="false" AllowFilteringByColumn="true">
                    <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true" EnablePostBackOnRowClick="True">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPID,FULL_NAME,DEP_NAME,INS_MODIFIER_TYPE_ID,REASON,OLD_INFO,NEW_INFO,MODIFIER_DATE,NOTE">
                        <Columns>
                            <%-- <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn> --%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID%>" DataField="ID" UniqueName="ID"
                                SortExpression="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="60px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULL_NAME"
                                UniqueName="FULL_NAME" SortExpression="FULL_NAME" HeaderStyle-Width="140px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="DEP_NAME"
                                UniqueName="DEP_NAME" SortExpression="DEP_NAME" HeaderStyle-Width="160px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: INS_MODIFIER_TYPE_ID %>" DataField="INS_MODIFIER_TYPE_ID"
                                UniqueName="INS_MODIFIER_TYPE_ID" SortExpression="INS_MODIFIER_TYPE_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do %>" DataField="REASON" UniqueName="REASON"
                                SortExpression="REASON" Visible="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thông tin cũ %>" DataField="OLD_INFO"
                                UniqueName="OLD_INFO" SortExpression="OLD_INFO" Visible="true" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thông tin mới %>" DataField="NEW_INFO"
                                UniqueName="NEW_INFO" SortExpression="NEW_INFO" Visible="true" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày điều chỉnh %>" DataFormatString="{0:dd/MM/yyyy}"
                                DataField="MODIFIER_DATE" UniqueName="MODIFIER_DATE" SortExpression="MODIFIER_DATE"
                                HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                                SortExpression="NOTE" HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV%>" DataField="EMPID" UniqueName="EMPID"
                                SortExpression="EMPID" Visible="false" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                //PROVINCE                         
                case '<%= cboPer_Province.ClientID %>':
                    cbo = $find('<%= cboPer_District.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboPer_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboCon_Province.ClientID %>':
                    cbo = $find('<%= cboCon_District.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboCon_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                //DISTRICT                          
                case '<%= cboPer_District.ClientID %>':
                    cbo = $find('<%= cboPer_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboCon_District.ClientID %>':
                    cbo = $find('<%= cboCon_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                default:
                    break;
            }
        }

        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }
        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboPer_District.ClientID %>':
                    cbo = $find('<%= cboPer_Province.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboPer_Ward.ClientID %>':
                    cbo = $find('<%= cboPer_District.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboCon_District.ClientID %>':
                    cbo = $find('<%= cboCon_Province.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboCon_Ward.ClientID %>':
                    cbo = $find('<%= cboCon_District.ClientID %>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }

            if (!value) {
                value = 0;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;
            context["value"] = sender.get_value();

        }
    </script>
</tlk:RadCodeBlock>
