<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLeaveHistory.ascx.vb"
    Inherits="Attendance.ctrlLeaveHistory" %>
<tlk:RadSplitter runat="server" ID="splitAll" Orientation="Horizontal" Width="100%"
    Height="100%">
    <tlk:RadPane runat="server" ID="paneTop" Scrolling="None" Height="50px">
        <div style="display: block; padding: 5px">
            <table class="table-form">
                <tr>
                    <td class="lb">
                        <%# Translate("Đăng ký từ ngày")%>
                    </td>
                    <td>
                        <tlk:RadDatePicker runat="server" ID="rdFromDateSearch">
                        </tlk:RadDatePicker>
                    </td>
                    <td class="lb">
                        <%# Translate("đến")%>
                    </td>
                    <td>
                        <tlk:RadDatePicker runat="server" ID="rdToDateSearch">
                        </tlk:RadDatePicker>
                    </td>
                    <td>
                        <tlk:RadButton runat="server" ID="btnSearch" Text='<%$ Translate: Tìm kiếm %>'>
                        </tlk:RadButton>
                    </td>
                </tr>
            </table>
        </div>
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="RadPane1" Height="320px">
        <tlk:RadGrid PageSize="50" runat="server" ID="rgData" Width="99%" Height="250px">
            <MasterTableView ClientDataKeyNames="EMPLOYEE_CODE" DataKeyNames="EMPLOYEE_CODE">
                <Columns>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: Mã nhân viên %>' DataField="EMPLOYEE_CODE">
                        <ItemStyle Width="50" />
                        <HeaderStyle Width="50" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: Tên nhân viên %>' DataField="EMPLOYEE_NAME">
                        <ItemStyle Width="100" />
                        <HeaderStyle Width="100" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày đăng ký %>' DataField="REGDATE"
                        DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle HorizontalAlign="Center" Width="70" />
                        <HeaderStyle Width="70" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: Từ ngày %>' DataField="FROM_DATE"
                        DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle HorizontalAlign="Center" Width="70" />
                        <HeaderStyle Width="70" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: Đến ngày %>' DataField="TO_DATE"
                        DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle HorizontalAlign="Center" Width="70" />
                        <HeaderStyle Width="70" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: Loại đăng ký %>' DataField="SIGN_NAME">
                        <ItemStyle Width="160" />
                        <HeaderStyle Width="160" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: Ghi chú %>' DataField="NOTE">
                        <ItemStyle Width="100" />
                        <HeaderStyle Width="100" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>