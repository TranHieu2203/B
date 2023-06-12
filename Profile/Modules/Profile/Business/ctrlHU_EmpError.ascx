<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpError.ascx.vb"
    Inherits="Profile.ctrlHU_EmpError" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="45px" Scrolling="None">
                <table class="table-form" style="padding-top: 8px;">
                    <tr>
                        <td style="width: 60px; text-align: right">
                            <%# Translate("Chú thích :")%>
                        </td>
                        <td style="width: 70px;">
                            <p style="border: 1px solid #000000; background-color: yellow; height: 17px; margin: 0 auto;"></p>
                        </td>
                        <td style="text-align: left">
                            <%# Translate("Trùng số CMND")%>
                        </td>
                        <td style="width: 15px"></td>
                        <td style="width: 70px;">
                            <p style="border: 1px solid #000000; background-color: green; height: 17px; margin: 0 auto;"></p>
                        </td>
                        <td style="text-align: left">
                            <%# Translate("Trùng họ tên")%>
                        </td>

                        <td style="width: 15px"></td>
                        <td style="width: 70px;">
                             <p style="border: 1px solid #000000; background-color: #ff0000; height: 17px; margin: 0 auto;"></p>
                        </td>
                        <td style="text-align: left">
                            <%# Translate("Trùng cả CMND và họ tên")%>
                        </td>
                    </tr>
                </table>

            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgEmployeeList" runat="server" Height="100%" EnableHeaderContextMenu="true">
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                        <%--<ClientEvents OnRowDblClick="gridRowDblClick" />--%>
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        <%--<ClientEvents OnCommand="ValidateFilter" />--%>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_CODE">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="FULLNAME_VN"
                                UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày sinh" DataField="BIRTH_DATE"
                                UniqueName="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="BIRTH_DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Giới tính" DataField="GENDER"
                                UniqueName="GENDER" SortExpression="GENDER" />
                            <tlk:GridBoundColumn HeaderText="Số CMNN" DataField="ID_NO"
                                UniqueName="ID_NO" SortExpression="ID_NO" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME_VN"
                                UniqueName="TITLE_NAME_VN" SortExpression="TITLE_NAME_VN" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày nhận việc" DataField="JOIN_DATE"
                                UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Ngày nghỉ việc" DataField="TER_EFFECT_DATE"
                                UniqueName="TER_EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="TER_EFFECT_DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Blacklist" DataField="WORK_STATUS_NAME"
                                UniqueName="WORK_STATUS_NAME" SortExpression="WORK_STATUS_NAME" HeaderStyle-Width="130px"
                                Visible="true" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <%--<HeaderStyle Width="120px" />--%>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var oWnd = GetRadWindow();
        });
        //mandatory for the RadWindow dialogs functionality
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            oWindow.setSize($(window).width(), "370");
            return oWindow;
        }
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadScriptBlock>
