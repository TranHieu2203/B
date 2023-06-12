<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrltreelist.ascx.vb"
    Inherits="Profile.ctrltreelist" %>
<style>
    .disable-event
    {
        pointer-events: none;
        cursor: none;
        display: none
    }
    .RadInput_Metro a 
    {
        background: transparent no-repeat url(Static/Images/Toolbar/calendar.png) !important;
        background: transparent no-repeat url(Static/Images/Toolbar/calendar.png) !important;
        padding: 0px;
        margin-top: 2px;
    }
    .RadPicker .RadInputError:after{
        right: 27px;
        transform: translateY(-5px);
    }
    
    .rtlREdit input.disable-inupt{
        /*pointer-events: none;*/
        background-color: khaki;
        border: 1px solid #767676;
    }
    
    input.disable-inupt{
        /*pointer-events: none;*/
        background-color: khaki;
        border: 1px solid #767676;
    }
    .inactive-row > td 
    {
        padding: 3px 13px 3px !important;    
    }
    td.NodeNhomDuAn {
        padding-left: 20px !important;
	    background-color: transparent;
        background-image: url("/Static/Images/ftePlan.gif");
        background-repeat: no-repeat;
        background-position: left;
    }
    
    .RadPicker.RadPicker_Metro > .RadInput.RadInput_Metro {
        width: 100px;
        float: left;
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
    .RadTreeList tr.rtlHeader th {
      padding: 3px 0 2px !important;
    } 
</style>


<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward" Visible = "false">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOrg" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
            <asp:ValidationSummary ID="valSum" runat="server" />
                <tlk:RadTreeList RenderMode="Lightweight" EditMode="InPlace"  ID="rgOrgTitle" runat="server" OnNeedDataSource="RadGrid_NeedDataSource" AutoPostBack="true" 
                    ParentDataKeyNames="PARENT_ID" DataKeyNames="ID" AutoGenerateColumns="false" AllowSorting="true" OnUpdateCommand="orgTreeList_UpdateCommand" 
                    OnInsertCommand="orgTreeList_InsertCommand" Height=90% AllowMultiItemSelection ="false">
                    <ClientSettings Selecting-AllowItemSelection="true" >
                        <Selecting AllowItemSelection="True"/>
                    </ClientSettings>
                    <Columns>
                         <tlk:TreeListTemplateColumn DataField="NAME_VN" UniqueName="NAME_VN" HeaderText="<%$ Translate: Tổ chức <span style='color: red'>*</span> %>">
                            <ItemTemplate>
                                <%# Eval("NAME_VN")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" CssClass='<%# Bind("STATUS_NAME")%>' Width="320px" Text='<%# Bind("NAME_VN")%>' runat="server" MaxLength = "100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Bạn phải nhập Tên tổ chức" ToolTip="Bạn phải nhập Tên tổ chức"
                                ></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                                <%--<HeaderStyle Width="250px"></HeaderStyle>--%>
                        </tlk:TreeListTemplateColumn>
                         <tlk:TreeListTemplateColumn DataField="NAME_EN" UniqueName="NAME_EN" HeaderText="<%$ Translate: Tên tiếng Anh %>">
                            <ItemTemplate>
                                <%# Eval("NAME_EN")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TxtENG" CssClass='<%# Bind("STATUS_NAME")%>' Width="320px" Text='<%# Bind("NAME_EN")%>' runat="server" MaxLength = "100"></asp:TextBox>
                            </EditItemTemplate>
                                <%--<HeaderStyle Width="250px"></HeaderStyle>--%>
                        </tlk:TreeListTemplateColumn>
                        <tlk:TreeListTemplateColumn DataField="CODE" UniqueName="CODE" 
                            HeaderText="<%$ Translate: Mã <span style='color: red'>*</span> %>" HeaderStyle-Width="130px">
                            <ItemTemplate>
                                <%# Eval("CODE")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" CssClass='<%# Bind("STATUS_NAME")%>' Width="100px" Text='<%# Bind("CODE")%>' runat="server" MaxLength = "100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="Bạn phải nhập Mã" ToolTip="Bạn phải nhập Mã"
                                ></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </tlk:TreeListTemplateColumn>
                        <tlk:TreeListTemplateColumn DataField="COST_CENTER_CODE" UniqueName="COST_CENTER_CODE" HeaderStyle-CssClass="text-align-left" HeaderText="<%$ Translate: Mã chi phí <span style='color: red'>*</span> %>" HeaderStyle-Width="100px">
                            <ItemTemplate>
                                <%# Eval("COST_CENTER_CODE")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" Text='<%# Bind("COST_CENTER_CODE")%>' Width="70px" runat="server" MaxLength = "30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3" ErrorMessage="Bạn phải nhập Mã chi phí" ToolTip="Bạn phải nhập Mã chi phí"
                                ></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </tlk:TreeListTemplateColumn>
                        <tlk:TreeListNumericColumn DataField="CAP" UniqueName="CAP" HeaderText="<%$ Translate: Cấp %>" ReadOnly ="true" HeaderStyle-CssClass="text-align-right" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right"></tlk:TreeListNumericColumn>
                        <tlk:TreeListCheckBoxColumn DataField="NHOMDUAN" UniqueName="NHOMDUAN" HeaderText="<%$ Translate: Hội đồng %>" HeaderStyle-CssClass="text-align-center" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center"></tlk:TreeListCheckBoxColumn>
                        <tlk:TreeListDateTimeColumn DataField = "EFFECTDATE" UniqueName="EFFECTDATE" HeaderText="<%$ Translate: Ngày hiệu lực %>" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center"></tlk:TreeListDateTimeColumn>
<%--                        <tlk:TreeListBoundColumn DataField="ACTFLG" UniqueName="ACTFLG" HeaderText="<%$ Translate: Trạng thái %>" HeaderStyle-Width="120px" ReadOnly="true"></tlk:TreeListBoundColumn>--%>
                        <tlk:TreeListEditCommandColumn UniqueName="InsertCommandColumn" AddRecordText="<%$ Translate: Thêm mới %>" HeaderStyle-ForeColor="#ffffff" HeaderStyle-CssClass="disable-event"
                        ItemStyle-CssClass="tool-group"
                        EditText="<%$ Translate: Sửa %>" ShowAddButton="true" ShowEditButton="true" HeaderStyle-Width="90px" 
                        InsertText="<%$ Translate: Lưu %>" UpdateText="<%$ Translate: Lưu %>" CancelText="<%$ Translate: Hủy %>" 
                        ItemStyle-HorizontalAlign ="Center"></tlk:TreeListEditCommandColumn>
                        
                        <tlk:TreeListBoundColumn Display="false" DataField="ID" UniqueName="ID" HeaderText=""></tlk:TreeListBoundColumn>
                        <tlk:TreeListBoundColumn Display="false" HeaderStyle-Width="160px" DataField="COLOR" UniqueName="COLOR" HeaderText="<%$ Translate: Owner %>"></tlk:TreeListBoundColumn>
                        <tlk:TreeListBoundColumn Display="false" HeaderStyle-Width="160px" DataField="STATUS_NAME" UniqueName="STATUS_NAME"></tlk:TreeListBoundColumn>
                        <%--<tlk:TreeListBoundColumn Display="false" HeaderStyle-Width="160px" DataField="NHOMDUAN" UniqueName="NHOMDUAN1" HeaderText="<%$ Translate: Owner %>"></tlk:TreeListBoundColumn>--%>
                        <%--<tlk:TreeListButtonColumn UniqueName="DeleteCommandColumn" Text="Delete" CommandName="Delete" HeaderStyle-Width="60px"></tlk:TreeListButtonColumn>--%>
                    </Columns>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" ScrollHeight = 550px ></Scrolling>
                    </ClientSettings>
            </tlk:RadTreeList>
        <asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị các đơn vị giải thể %>"
            AutoPostBack="True" CssClass="align-checkbox"/>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<script>
    $(document).ready(function () {

    })
</script>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
