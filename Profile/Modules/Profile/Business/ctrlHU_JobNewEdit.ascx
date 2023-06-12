<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_JobNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_JobNewEdit" %>
    <%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="HiddenField1" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<style type="text/css">
    div.RadComboBox_Office2007
    {
        height: 22px;
    }
    
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_JobNewEdit_RadPane1
    {
        overflow: hidden !important;
    }
    #ctl00_MainContent_ctrlHU_JobNewEdit_orgTreeList_ctl03_FUNCTION_NAMEEditor_Input
    {
        border: none !important;
    }
    .lb
    {
        padding-right:30px !important;
    }
    #ctl00_MainContent_ctrlHU_JobNewEdit_RadEditor1{
        width: 750px !important;
    }
    #ctl00_MainContent_ctrlHU_JobNewEdit_orgTreeList_ctl03_FUNCTION_NAMEEditor 
    {
        width: 88px !important;
    }
    .RadTreeList.RadTreeList_Metro .RadComboBox.RadComboBox_Metro 
    {
        width: 88px !important;
    }
    @media only screen and (max-width: 1400px) {
      #ctl00_MainContent_ctrlHU_JobNewEdit_RadEditor1,
      #ctl00_MainContent_ctrlHU_JobNewEdit_RadEditor2,
      #ctl00_MainContent_ctrlHU_JobNewEdit_RadEditor3 {
        width: 500px !important;
      }
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

    td.NodeTree {
        padding-left: 20px !important;
	    background-color: transparent;
        background-image: url("/Static/Images/folder.gif");
        background-repeat: no-repeat;
        background-position: left;
    }

    td.NodeFolderContent {
        padding-left: 20px !important;
	    background-color: transparent;
        background-image: url("/Static/Images/folder-content.gif");
        background-repeat: no-repeat;
        background-position: left;
    }

    td.NodeFileContent {
        padding-left: 20px !important;
	    background-color: transparent;
        background-image: url("/Static/Images/file-content.gif");
        background-repeat: no-repeat;
        background-position: left;
    }
    .rwTitleWrapper
    {
        height: 30px !important;
    }
    .RadWindow .rwIcon{
        transform: translateY(-67%)
    }
    .RadWindow .rwTitle{
        line-height: 25px;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="32px">
        <tlk:RadToolBar ID="tbarMain" runat="server"/>
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="100%">
    <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Vertical">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="50%">
        <table class="table-form">
            <tr>
                <td class="lb text-align-left">
                    <%# Translate("Mã")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="ReadOnly" ReadOnly="true" Width="250px">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã công việc đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã công việc đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="lb text-align-left">
                    <%# Translate("Tên tiếng Việt")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNamVN" runat="server" Width="250px">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                        ControlToValidate="txtNamVN" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="lb text-align-left">
                    <%# Translate("Tên tiếng Anh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNamEN" runat="server" Width="250px">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ID="reqNamEN" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                        ControlToValidate="txtNamEN" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr style="display: none;">
                <td class="lb text-align-left">
                    <%# Translate("Nhóm cấp bậc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboPhanLoai" AutoPostBack="true" CausesValidation="false" Width="250px">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr style="display:none">
                <td class="lb text-align-left">
                    <%# Translate("Cấp bậc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboJobband" AutoPostBack="true" CausesValidation="false" Width="250px">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb text-align-left">
                    <%# Translate("Lĩnh vực")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboJobFamily" AutoPostBack="true" CausesValidation="false" Width="250px">
                    </tlk:RadComboBox>
                </td>
            </tr>
        </table>
        <br />
        <table class="table-form">
            <tr>
                <td colspan="5">
                        <tlk:RadTreeList Width="100%" RenderMode="Lightweight" EditMode="InPlace" ID="orgTreeList" runat="server" OnNeedDataSource="orgTreeList_NeedDataSource" AutoPostBack="true" 
                        AllowSorting="true" ParentDataKeyNames="PARENT_ID" DataKeyNames="ID" AutoGenerateColumns="false" OnUpdateCommand="RadTreeList1_UpdateCommand" OnInsertCommand="RadTreeList1_InsertCommand" OnDeleteCommand="RadTreeList1_DeleteCommand">
                            <Columns>
                                <tlk:TreeListEditCommandColumn 
                                    UniqueName="InsertCommandColumn" 
                                    UpdateText="<%$ Translate: Lưu %>" 
                                    CancelText="<%$ Translate: Hủy %>" 
                                    InsertText="<%$ Translate: Lưu %>" 
                                    AddRecordText="<%$ Translate: Thêm %>" 
                                    EditText="Sửa"
                                    ShowEditButton="true" 
                                    HeaderStyle-Width="90px" 
                                    ItemStyle-HorizontalAlign="Center">
                                </tlk:TreeListEditCommandColumn>
                                <tlk:TreeListBoundColumn DataField="NAME" UniqueName="NAME" HeaderText="<%$ Translate: Chức năng <span style='color: red'>*</span>%>"></tlk:TreeListBoundColumn>
                                <tlk:TreeListBoundColumn DataField="NAME_EN" UniqueName="NAME_EN" HeaderText="<%$ Translate: Chức năng (Eng) <span style='color: red'>*</span>%>">
                                </tlk:TreeListBoundColumn>
                                <tlk:TreeListBoundColumn Display="false" DataField="ID" UniqueName="ID" HeaderText="ID"></tlk:TreeListBoundColumn>
                                <tlk:TreeListBoundColumn Display="false" DataField="PARENT_ID" UniqueName="PARENT_ID" HeaderText="PARENT_ID"></tlk:TreeListBoundColumn>
                                <tlk:TreeListBoundColumn Display="false" DataField="FUNCTION_ID" UniqueName="FUNCTION_ID" HeaderText="Function"></tlk:TreeListBoundColumn>
                                <tlk:TreeListBoundColumn DataField="FUNCTION_NAME" UniqueName="FUNCTION_NAME" 
                                    HeaderStyle-Width="100px"
                                    HeaderText="<%$ Translate: Thước đo %>"></tlk:TreeListBoundColumn>
                                <tlk:TreeListButtonColumn UniqueName="DeleteCommandColumn" 
                                    Text="<%$ Translate: Xóa %>" 
                                    CommandName="Delete" 
                                    ConfirmText="Bạn chắc chắn muốn xóa bản ghi?"
                                    HeaderStyle-Width="50px">
                                </tlk:TreeListButtonColumn>
                            </Columns>
                        </tlk:RadTreeList>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <table class="table-form">
            <tr>
                <td>
                    <p>
                    <%# Translate("Mô tả công việc")%>
                    </p>
                    <tlk:RadEditor RenderMode="Lightweight" ID="RadEditor1" runat="server" EditModes="Design"
                        ToolTip="Mô tả công việc"
                        Width="100%" ToolsFile="/Static/ToolsFile.xml" Height="300px">
                        <Content><span style="font-family: Arial; font-size: 12px;">  Mục đích...</span><br /><br /><br /><br /><br /><span style="font-family: Arial; font-size: 12px;">  Nhiệm vụ chính...</span><br /><br /><br /><br /><br /><span style="font-family: Arial; font-size: 12px;">  Yêu cầu...</span></Content>
                    </tlk:RadEditor>
                    <hr />
                    <br />
                </td>
            </tr>
            <tr style="display:none;">
                <td>
                    <p>
                        <%# Translate("Mô tả công việc")%>
                    </p>
                    <tlk:RadEditor RenderMode="Lightweight" ID="RadEditor2" runat="server" 
                        EmptyMessage="Mô tả..."
                        EditModes="Design" Width="100%" ToolsFile="/Static/ToolsFile.xml" Height="300px">
                    </tlk:RadEditor>
                    <hr />
                    <br />
                </td>
            </tr>
            <tr style="display:none;">
                <td>
                    <p>
                        <%# Translate("Yêu cầu công việc")%>
                    </p>
                    <tlk:RadEditor RenderMode="Lightweight" ID="RadEditor3" runat="server" EditModes="Design" 
                        EmptyMessage="Yêu cầu.."
                        Width="100%" ToolsFile="/Static/ToolsFile.xml" Height="300px">
                    </tlk:RadEditor>
                </td>
            </tr>
        </table>
        <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    </tlk:RadPane>
</tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="pgFindMultiEmp" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
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

        function OnClientValueChanged(sender, args) {

        }

        function setDisplayValue(sender, args) {
            sender.set_displayValue(sender.get_value());
        }

        function OnClientClicking(sender, eventArgs) {
            console.log('ac')
            enableAjax = false; ;
        }

    </script>
</tlk:RadCodeBlock>
