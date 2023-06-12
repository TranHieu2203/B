<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrltreelist.ascx.vb"
    Inherits="Profile.ctrltreelist" %>
<style>
    .disable-event
    {
        pointer-events: none;
            cursor: none;
        }
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
.custom-column{
    padding-left: 15px;
}
td.NodeTree {
    padding-left: 20px !important;
	background-color: transparent;
    background-image: url("/Static/Images/folder.gif");
    background-repeat: no-repeat;
    background-position: left;
}       
#MainContent_ctrltreelist_cbDissolve, #MainContent_ctrltreelist_chkUyBan{
    margin-top: 7px;
}
#MainContent_ctrltreelist_chkUyBan{
    margin-left: 20px;
}
</style>


<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Visible="false" Scrolling="None">
                <tlk:RadToolBar ID="tbarOrg" runat="server"  OnClientButtonClicking="clientButtonClicking"/>
                <asp:ValidationSummary ID="valSum" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadTreeList RenderMode="Lightweight" EditMode="InPlace"  ID="rgOrgTitle" runat="server" OnNeedDataSource="RadGrid_NeedDataSource" AutoPostBack="true" 
                    ParentDataKeyNames="PARENT_ID" DataKeyNames="ID" AutoGenerateColumns="false" AllowSorting="true" OnUpdateCommand="orgTreeList_UpdateCommand" 
                    OnInsertCommand="orgTreeList_InsertCommand" Height=95% AllowMultiItemSelection ="false">
                    <ClientSettings Selecting-AllowItemSelection="true" >
                        <Selecting AllowItemSelection="True" AllowToggleSelection="true"/>
                    </ClientSettings>
                    <Columns>
                         <tlk:TreeListTemplateColumn DataField="NAME_VN" UniqueName="NAME_VN" HeaderStyle-Width="250px" HeaderText="Tên tiếng Việt <span style='color: red'>*</span>">
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
                         <tlk:TreeListTemplateColumn DataField="NAME_EN" UniqueName="NAME_EN" HeaderText="Tên tiếng Anh" HeaderStyle-Width="250px"  >
                            <ItemTemplate>
                                <%# Eval("NAME_EN")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtENG" Text='<%# Bind("NAME_EN")%>' runat="server" MaxLength = "100"></asp:TextBox>
                            </EditItemTemplate>
                                <%--<HeaderStyle Width="250px"></HeaderStyle>--%>
                        </tlk:TreeListTemplateColumn>
                        <tlk:TreeListTemplateColumn DataField="CODE" UniqueName="CODE" HeaderText="Tên viết tắt <span style='color: red'>*</span>" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Eval("CODE")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" Text='<%# Bind("CODE")%>' runat="server" MaxLength = "100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="Tên viết tắt không được trống!"
                                ></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </tlk:TreeListTemplateColumn>
                        <tlk:TreeListTemplateColumn DataField="COST_CENTER_CODE" UniqueName="COST_CENTER_CODE" HeaderText="Mã chi phí" HeaderStyle-Width="150px" Visible="false">
                            <ItemTemplate>
                                <%# Eval("COST_CENTER_CODE")%>
                            </ItemTemplate>
                           <%-- <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" Text='<%# Bind("COST_CENTER_CODE")%>' runat="server" MaxLength = "30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3" ErrorMessage="Mã chi phí không được trống"
                                ></asp:RequiredFieldValidator>
                            </EditItemTemplate>--%>
                        </tlk:TreeListTemplateColumn>
                        <tlk:TreeListBoundColumn DataField="ORG_LEVEL_NAME" UniqueName="ORG_LEVEL_NAME" HeaderText="Cấp" ReadOnly ="true" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center"></tlk:TreeListBoundColumn>
                        <tlk:TreeListCheckBoxColumn DataField="UY_BAN" UniqueName="UY_BAN" HeaderText="Ủy ban" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></tlk:TreeListCheckBoxColumn>
                        <tlk:TreeListDateTimeColumn DataField = "FOUNDATION_DATE" UniqueName="FOUNDATION_DATE" HeaderText="Ngày thành lập" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></tlk:TreeListDateTimeColumn>
                        <tlk:TreeListBoundColumn DataField="ACTFLG" UniqueName="ACTFLG" HeaderText="Trạng thái" HeaderStyle-Width="150px" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></tlk:TreeListBoundColumn>
                        <tlk:TreeListBoundColumn DataField="FILENAME" UniqueName="FILENAME" HeaderText="File đính kèm" HeaderStyle-Width="150px" ReadOnly="true"></tlk:TreeListBoundColumn>
                        <tlk:TreeListButtonColumn UniqueName="DowloadCommandColumn" Text="Dowload" CommandName="Dowload" HeaderText="Tải" ImageUrl="~/Static/Images/Icons/16/icon_dowloadFile.png"  ButtonType="ImageButton"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="40px"></tlk:TreeListButtonColumn>
                       <tlk:TreeListButtonColumn UniqueName="ViewCommandColumn" Text="View" CommandName="View" HeaderText="View" ImageUrl="~/Static/Images/Icons/16/ViewImgOrg.png"   ButtonType="ImageButton" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px"></tlk:TreeListButtonColumn>
                        <%--<tlk:TreeListEditCommandColumn UniqueName="InsertCommandColumn" AddRecordText="<%$ Translate: Thêm mới %>" HeaderStyle-ForeColor="#ffffff" HeaderStyle-CssClass="disable-event"
                        EditText="<%$ Translate: Sửa %>" ShowAddButton="true" ShowEditButton="true" HeaderStyle-Width="140px" 
                        InsertText="<%$ Translate: Lưu %>" UpdateText="<%$ Translate: Lưu %>" CancelText="<%$ Translate: Hủy %>" 
                        ItemStyle-HorizontalAlign ="Center"></tlk:TreeListEditCommandColumn>--%>
                        
                        <tlk:TreeListBoundColumn Display="false" DataField="ID" UniqueName="ID" HeaderText=""></tlk:TreeListBoundColumn>    
                         <tlk:TreeListBoundColumn Display="false" HeaderStyle-Width="160px" DataField="COLOR" UniqueName="COLOR" HeaderText="<%$ Translate: Owner %>"></tlk:TreeListBoundColumn>
                        <tlk:TreeListBoundColumn Display="false" HeaderStyle-Width="160px" DataField="STATUS_NAME" UniqueName="STATUS_NAME"></tlk:TreeListBoundColumn>
                       <tlk:TreeListBoundColumn Display="false" HeaderStyle-Width="160px" DataField="ORG_LEVEL" UniqueName="ORG_LEVEL"></tlk:TreeListBoundColumn>
                    </Columns>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" ScrollHeight = 550px ></Scrolling>
                    </ClientSettings>
            </tlk:RadTreeList>
                <asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị các đơn vị giải thể %>" AutoPostBack="True" />

                <asp:CheckBox ID="chkUyBan" runat="server" Text="<%$ Translate: Xem cơ cấu ủy ban %>" AutoPostBack="True" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function clientButtonClicking(sender, args) {
            //if (args.get_item().get_commandName() == "NEXT") {
            //    window.open('/Default.aspx?mid=Organize&fid=ctrlHU_Organization&group=Business', "_self");
            //}
        }
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