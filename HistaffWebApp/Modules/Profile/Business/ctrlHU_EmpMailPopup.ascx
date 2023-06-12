<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpMailPopup.ascx.vb"
    Inherits="Profile.ctrlHU_EmpMailPopup" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="70px" Scrolling="None">
                <div style="width:100%;margin:0 auto;text-align:center; padding-top:15px; font-weight:500;">
                    <h2>Danh sách nhân viên trùng Email</h2>
                </div>
                <table class="table-form">
                    <tr>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE"
                        ClientDataKeyNames="ID,EMPLOYEE_CODE" AllowFilteringByColumn="false">
                        <Columns>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="FULLNAME_VN"
                                SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME_VN"
                                SortExpression="TITLE_NAME_VN" UniqueName="TITLE_NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="200px" />  
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="WORK_STATUS_NAME"
                                SortExpression="WORK_STATUS_NAME" UniqueName="WORK_STATUS_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày thôi việc" DataField="TER_EFFECT_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="TER_EFFECT_DATE" UniqueName="TER_EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Email" DataField="WORK_EMAIL"
                                SortExpression="WORK_EMAIL" UniqueName="WORK_EMAIL" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>         
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
