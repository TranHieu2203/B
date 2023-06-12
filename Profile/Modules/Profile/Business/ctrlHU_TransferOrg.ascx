<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TransferOrg.ascx.vb"
    Inherits="Profile.ctrlHU_TransferOrg" %>
<%@ Import Namespace="Common" %>
<style type="text/css">
      .rwControlButtons{display:none!important;}
    #all
    {
        display: flex;
    }
    #left
    {
        width: 51%;
        float: left;
    }
    #right
    {
        width: 47%;
    }
    #table{margin-top:45px !important}
    .RadButton_Metro.rbSkinnedButton, .RadButton_Metro.rbVerticalButton, .RadButton_Metro .rbDecorated{background-color:#FFFFFF !important; }
    .rbAdd, .rbNext, .rbSave, .rbRefresh, .rbConfig{margin-left:6px !important} 
.NodeFTEPlan {
    padding-left: 20px;
	background-color: transparent;
    background-image: url("/Static/Images/ftePlan.gif");
    background-repeat: no-repeat;
}

.NodeGroup {
    padding-left: 20px;
	background-color: transparent;
    background-image: url("/Static/Images/group.png");
    background-repeat: no-repeat;
}

.NodeFolder {
    padding-left: 20px;
	background-color: transparent;
    background-image: url("/Static/Images/folder.gif");
    background-repeat: no-repeat;
}
</style>
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidClassID" runat="server" />
<asp:HiddenField ID="txtWorkLocation" runat="server" />
<asp:HiddenField ID="txtQtyPos" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="LeftPane" runat="server" Height="100px">
        <asp:ValidationSummary ID="valSum" runat="server" />
        <div id="all">
            <div id="left">
                <table class="table-form">
                    <tr>
                        <td>
                            <%# Translate("Tìm kiếm")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtTextbox1" Width="100%" Placeholder="Mã vị trí/Tên vị trí/Mã NV/Tên NV" />
                        </td>
                         <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# Translate("Ghế")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPositionFilter" CheckBoxes="true" Width="450px" EnableCheckAllItemsCheckBox="true">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# Translate("Đơn vị")%>
                        </td>
                        <td>
                            <tlk:RadDropDownTree RenderMode="Lightweight" runat="server" ID="cboOrgTree1" Width="450px"
                                DefaultMessage="Chọn đơn vị" DefaultValue="0" DataTextField="NAME" DataFieldID="ID"
                                DataFieldParentID="PARENT_ID" DataValueField="ID" AutoPostBack="true" CausesValidation="false" EnableFiltering="true">
                                <DropDownSettings Height="85%" CloseDropDownOnSelection="true"/>
                                <FilterSettings Filter="Contains" Highlight="Matches"/>
                                <DropDownNodeTemplate>
                                    <div class="<%# If(Eval("ID") < 1, "NodeFTEPlan", If(Eval("NHOMDUAN"), "NodeGroup", "NodeFolder")) %>">
                                        <span>
                                            <%# Eval("NAME")%>
                                        </span>
                                    </div>
                                </DropDownNodeTemplate>
                            </tlk:RadDropDownTree>
                        </td> 
                    </tr>
                </table>
            </div>
            <div id="right">
                <table class="table-form">
                    <tr>
                        <td>
                            <%# Translate("Tìm kiếm")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtTextbox2" Width="100%" Placeholder="Mã vị trí/Tên vị trí/Mã NV/Tên NV" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch2" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# Translate("Ghế")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboPositionFilter2" CheckBoxes="true" Width="450px" EnableCheckAllItemsCheckBox="true">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# Translate("Đơn vị")%>
                        </td>
                        <td>
                            <tlk:RadDropDownTree RenderMode="Lightweight" runat="server" ID="cboOrgTree2" Width="450px"
                                DefaultMessage="Chọn đơn vị" DefaultValue="0" DataTextField="NAME" DataFieldID="ID"
                                DataFieldParentID="PARENT_ID" DataValueField="ID" AutoPostBack="true" CausesValidation="false" EnableFiltering="true">
                                <DropDownSettings Height="85%" CloseDropDownOnSelection="true"/>
                                <FilterSettings Filter="Contains" Highlight="Matches"/>
                                <DropDownNodeTemplate>
                                    <div class="<%# If(Eval("ID") < 1, "NodeFTEPlan", If(Eval("NHOMDUAN"), "NodeGroup", "NodeFolder")) %>">
                                        <span>
                                            <%# Eval("NAME")%>
                                        </span>
                                    </div>
                                </DropDownNodeTemplate>
                            </tlk:RadDropDownTree>
                        </td> 
                    </tr>
                </table>
            </div>
        </div>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgLeft" runat="server" Height="100%" AllowSorting="false">
                    <MasterTableView DataKeyNames="ID,CODE,NAME_VN,NAME_EN,ORG_ID,MASTER,MASTER_NAME,INTERIM,INTERIM_NAME,ORG_NAME,MASTER_CODE,REMARK,HIRING_STATUS,CONCURRENT,IS_PLAN,IS_OWNER,JOB_ID,COST_CENTER,IS_NONPHYSICAL,COLOR,FLAG,TITLE_GROUP_ID,BOTH">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã vị trí %>" DataField="CODE" UniqueName="CODE"
                                SortExpression="CODE" HeaderStyle-Width="60px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="NAME_VN"
                                UniqueName="NAME_VN" SortExpression="NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Master %>" DataField="MASTER_NAME"
                                UniqueName="MASTER_NAME" SortExpression="MASTER_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Interim %>" DataField="INTERIM_NAME"
                                UniqueName="INTERIM_NAME" SortExpression="INTERIM_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Width="50px">
                <table class="table-form" id="table">
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnInsert" runat="server" ToolTip="Điều chuyển">
                                <Icon PrimaryIconCssClass="rbNext" />
                            </tlk:RadButton>
                            <tlk:RadButton ID="RadButton1" runat="server" Text="" Visible="false">
                            </tlk:RadButton>
                            <tlk:RadButton ID="RadButton2" runat="server" Text="" OnClientClicking="RadButton2_Click" Visible="true">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnNhanBan" runat="server" Font-Bold="true" ToolTip="Nhân bản">
                                <Icon PrimaryIconCssClass="rbAdd" />
                            </tlk:RadButton>
                            <tlk:RadButton ID="RadButton3" runat="server" Text="" OnClientClicking="RadButton3_Click" Visible="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnSave" runat="server" Font-Bold="true" ToolTip="Lưu">
                                <Icon PrimaryIconCssClass="rbSave" />
                            </tlk:RadButton> 
                        </td>
                    </tr>  
                    <tr>
                        <td>
                             <tlk:RadButton ID="btnUndo" runat="server" Font-Bold="true" ToolTip="Quay lại">
                                <Icon PrimaryIconCssClass="rbRefresh" />
                            </tlk:RadButton>
                        </td>
                    </tr>
                     <tr>
                        <td>
                             <tlk:RadButton ID="btnRecruitment" runat="server" Font-Bold="true" ToolTip="Tuyển dụng">
                                <Icon PrimaryIconCssClass="rbConfig" />
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgRight" runat="server" Height="100%" AllowSorting="false">
                    <MasterTableView DataKeyNames="ID,CODE,NAME_VN,NAME_EN,ORG_ID,MASTER,MASTER_NAME,INTERIM,INTERIM_NAME,ORG_NAME,MASTER_CODE,REMARK,HIRING_STATUS,CONCURRENT,IS_PLAN,IS_OWNER,JOB_ID,COST_CENTER,IS_NONPHYSICAL,COLOR,FLAG,TITLE_GROUP_ID,BOTH">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã vị trí %>" DataField="CODE" UniqueName="CODE"
                                SortExpression="CODE" HeaderStyle-Width="60px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc  %>" DataField="NAME_VN"
                                UniqueName="NAME_VN" SortExpression="NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Master %>" DataField="MASTER_NAME"
                                UniqueName="MASTER_NAME" SortExpression="MASTER_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Interim %>" DataField="INTERIM_NAME"
                                UniqueName="INTERIM_NAME" SortExpression="INTERIM_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move" Modal="true"
            ShowContentDuringLoad="false" OnClientClose="popupclose">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var oWindow;
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function btnInsert() {
            setTimeout(function () { $('#<%= RadButton2.ClientID %>').click(); }, 100);
        }

        function RadButton2_Click(sender, args) {

            oWindow = radopen('Dialog.aspx?mid=Organize&fid=ctrlHU_TransferOrgPopup&group=Business&Rerect=DK', "rwPopup");
            oWindow.setSize(300, 200);
            oWindow.center();
            args.set_cancel(true);

        }

        function btnNhanBan(sender, args) {
            setTimeout(function () { $('#<%= RadButton3.ClientID %>').click(); }, 100);
        }
        function RadButton3_Click(sender, args) {
            oWindow = radopen('Dialog.aspx?mid=Organize&fid=ctrlHU_TransferOrgPopup&group=Business', "rwPopup");
            oWindow.setSize(300, 200);
            oWindow.center();
            args.set_cancel(true);
        }

        function btnCancelClick() {
            var oArg = new Object();
            oArg.ID = 'Cancel';
            oWindow.close(oArg);
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

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgLeft.ClientID %>").get_masterTableView().rebind();
            }

        }
    </script>
</tlk:RadCodeBlock>
