<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTreelistEmp.ascx.vb"
    Inherits="Profile.ctrlTreelistEmp" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOrg" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadTreeList RenderMode="Lightweight" EditMode="PopUp"  ID="rgOrgTitle" runat="server" 
                    OnNeedDataSource="RadGrid_NeedDataSource" AutoPostBack="true" 
                    ParentDataKeyNames="PARENT_ID" DataKeyNames="ID" AutoGenerateColumns="false" AllowSorting="true" 
                     Height=90% AllowMultiItemSelection ="true" SelectedItemStyle-ForeColor="#000">
                    <ClientSettings Selecting-AllowItemSelection="True">
                        <Selecting AllowItemSelection="True" />
                    </ClientSettings>
                   <Columns>
                         <tlk:TreeListTemplateColumn DataField="NAME_VN" UniqueName="NAME_VN" HeaderText="Tên tiếng Việt">
                            <ItemTemplate>
                                <%# Eval("NAME_VN")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" Text='<%# Bind("NAME_VN")%>' runat="server" MaxLength = "100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Tên tiếng Việt không được trống!"
                                ></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                                <%--<HeaderStyle Width="250px"></HeaderStyle>--%>
                        </tlk:TreeListTemplateColumn>
                         <tlk:TreeListTemplateColumn DataField="NAME_EN" UniqueName="NAME_EN" HeaderText="Tên tiếng Anh" HeaderStyle-Width="120px">
                            <ItemTemplate>
                                <%# Eval("NAME_EN")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtENG" Text='<%# Bind("NAME_EN")%>' runat="server" MaxLength = "100"></asp:TextBox>
                            </EditItemTemplate>
                                <%--<HeaderStyle Width="250px"></HeaderStyle>--%>
                        </tlk:TreeListTemplateColumn>
                        <tlk:TreeListTemplateColumn DataField="CODE" UniqueName="CODE" HeaderText="Tên viết tắt" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <%# Eval("CODE")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" Text='<%# Bind("CODE")%>' runat="server" MaxLength = "100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="Tên viết tắt không được trống!"
                                ></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </tlk:TreeListTemplateColumn>
                        <tlk:TreeListTemplateColumn DataField="COST_CENTER_CODE" UniqueName="COST_CENTER_CODE" HeaderText="Mã chi phí" HeaderStyle-Width="120px">
                            <ItemTemplate>
                                <%# Eval("COST_CENTER_CODE")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" Text='<%# Bind("COST_CENTER_CODE")%>' runat="server" MaxLength = "30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3" ErrorMessage="Mã chi phí không được trống"
                                ></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </tlk:TreeListTemplateColumn>
                        <tlk:TreeListNumericColumn DataField="NLEVEL" UniqueName="NLEVEL" HeaderText="Cấp" ReadOnly ="true" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Right"></tlk:TreeListNumericColumn>
                        <tlk:TreeListCheckBoxColumn DataField="GROUPPROJECT" UniqueName="GROUPPROJECT" HeaderText="Hội đồng" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Right"></tlk:TreeListCheckBoxColumn>
                        <tlk:TreeListDateTimeColumn DataField = "EFFECT_DATE" UniqueName="EFFECT_DATE" HeaderText="Ngày hiệu lực" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center"></tlk:TreeListDateTimeColumn>
                        <%--<tlk:TreeListBoundColumn DataField="STATUS_NAME" UniqueName="STATUS_NAME" HeaderText="Trạng thái" HeaderStyle-Width="150px" ReadOnly="true"></tlk:TreeListBoundColumn>--%>
                       <tlk:TreeListBoundColumn DataField="TITLE_NAME" UniqueName="TITLE_NAME" HeaderText="Vị trí GDBP" HeaderStyle-Width="200px" ReadOnly="true"></tlk:TreeListBoundColumn>
                       <tlk:TreeListBoundColumn DataField="POSITION_NAME" UniqueName="POSITION_NAME" HeaderText="Giám Đốc BP" HeaderStyle-Width="150px" ReadOnly="true"></tlk:TreeListBoundColumn>
                       <tlk:TreeListBoundColumn DataField="FILENAME" UniqueName="FILENAME" HeaderText="Tên file" HeaderStyle-Width="150px" ReadOnly="true"></tlk:TreeListBoundColumn>
                        <tlk:TreeListBoundColumn Display="false" DataField="ID" UniqueName="ID" HeaderText=""></tlk:TreeListBoundColumn>    
                         <tlk:TreeListBoundColumn Display="false" HeaderStyle-Width="160px" DataField="COLOR" UniqueName="COLOR" HeaderText="<%$ Translate: Owner %>"></tlk:TreeListBoundColumn>
                        <tlk:TreeListButtonColumn UniqueName="DowloadCommandColumn" Text="Dowload" CommandName="Dowload" ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px"></tlk:TreeListButtonColumn>
                       <tlk:TreeListButtonColumn UniqueName="ViewCommandColumn" Text="View" CommandName="View" ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px"></tlk:TreeListButtonColumn>
                   </Columns>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" ScrollHeight = 550px></Scrolling>
                    </ClientSettings>
            </tlk:RadTreeList>
        <asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị các đơn vị giải thể %>"
            AutoPostBack="True" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function <%=ClientID%>_OnClientClose(oWnd, args) {
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(<%=ClientID%>_OnClientClose);
            var arg = args.get_argument();
            if(arg == null)
            {
                arg = new Object();
                arg.ID = 'Cancel';
            }
            if (arg) {
                var ajaxManager = $find("<%= AjaxManagerId %>");
                ajaxManager.ajaxRequest("<%= ClientID %>_PopupPostback:" + arg.ID);
            }
        }
</script>
</tlk:RadCodeBlock>
<style type="text/css">
td.NodeNhomDuAn {
    padding-left: 20px !important;
	background-color: transparent;
    background-image: url("/Static/Images/ftePlan.gif");
    background-repeat: no-repeat;
    background-position: left;
}

td.NodeJOB {
    padding-left: 20px !important;
	background-color: transparent;
    background-image: url("/Static/Images/vcard.png");
    background-repeat: no-repeat;
    background-position: left;
}

td.NodeTree {
    padding-left: 20px !important;
	background-color: transparent;
    background-image: url("/Static/Images/folder.gif");
    background-repeat: no-repeat;
    background-position: left;
}
</style>
