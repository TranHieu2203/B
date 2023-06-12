<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOnlineUser.ascx.vb"
    Inherits="Common.ctrlOnlineUser" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="rtbOnlineUser" runat="server">
        </tlk:RadToolBar>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgOnlineUser" runat="server" Height="100%">
            <MasterTableView>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="SessionID" Visible="false" />
                    <tlk:GridBoundColumn DataField="Username" ReadOnly="true" HeaderText="<%$ Translate:Tên tài khoản %>"
                        SortExpression="Username" />
                    <tlk:GridBoundColumn DataField="Fullname" ReadOnly="true" HeaderText="<%$ Translate:Họ và tên %>"
                        SortExpression="Fullname" />
                    <tlk:GridBoundColumn DataField="Email" ReadOnly="true" HeaderText="<%$ Translate:Email %>"
                        SortExpression="Email" />
                    <tlk:GridBoundColumn DataField="Mobile" ReadOnly="true" HeaderText="<%$ Translate:Mobile %>"
                        SortExpression="Mobile" />
                    <tlk:GridBoundColumn DataField="Location" ReadOnly="true" HeaderText="<%$ Translate:Location %>"
                        SortExpression="Location" />
                    <tlk:GridBoundColumn DataField="GroupNames" ReadOnly="true" HeaderText="<%$ Translate:Nhóm %>"
                        SortExpression="GroupNames" />
                    <tlk:GridBoundColumn DataField="LoginDate" ReadOnly="true" HeaderText="<%$ Translate:Ngày giờ đăng nhập %>"
                        SortExpression="LoginDate"  DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                    <tlk:GridBoundColumn DataField="LoginTime" ReadOnly="true" HeaderText="<%$ Translate:Số phút đăng nhập %>"
                        SortExpression="LoginTime" />
                    <tlk:GridBoundColumn DataField="LastAccessDate" ReadOnly="true" HeaderText="<%$ Translate:Ngày giờ sử dụng %>"
                        SortExpression="LastAccessDate"  DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                    <tlk:GridBoundColumn DataField="CurrentViewDesc" ReadOnly="true" HeaderText="<%$ Translate:Chức năng sử dụng %>"
                        SortExpression="CurrentViewDesc" />
                    <tlk:GridBoundColumn DataField="NoAccessTime" ReadOnly="true" HeaderText="<%$ Translate:Số phút không sử dụng %>"
                        SortExpression="NoAccessTime" />
                    <tlk:GridBoundColumn DataField="Status" ReadOnly="true" HeaderText="<%$ Translate:Trạng thái %>"
                        SortExpression="Status" />
                    <tlk:GridBoundColumn DataField="IP" ReadOnly="true" HeaderText="<%$ Translate:Địa chỉ IP %>"
                        SortExpression="IP" />
                    <tlk:GridBoundColumn DataField="ComputerName" ReadOnly="true" HeaderText="<%$ Translate:Tên máy %>"
                        SortExpression="ComputerName" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
        <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    </tlk:RadPane>
</tlk:RadSplitter>

