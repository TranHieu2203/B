<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLeaveApprove.ascx.vb"
    Inherits="Attendance.ctrlLeaveApprove" %>
<tlk:RadSplitter runat="server" ID="splitAll" Orientation="Horizontal" Width="100%"
    Height="100%">
    <tlk:RadPane runat="server" ID="paneTop" Scrolling="None" Height="70px">
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
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Loại đăng ký") %>
                    </td>
                    <td>
                        <tlk:RadComboBox runat="server" ID="cboLeaveTypeSearch">
                            <Items>
                                <tlk:RadComboBoxItem Text='<%$ Translate: Tất cả %>' Value="0" />
                            </Items>
                        </tlk:RadComboBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Nhân viên") %>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtEmployee">
                        </tlk:RadTextBox>
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
        <tlk:RadTabStrip runat="server" ID="tabTypeApprove" SelectedIndex="0" MultiPageID="pageTypeApprove"
            ScrollChildren="true">
            <Tabs>
                <tlk:RadTab Text='<%$ Translate: Đăng ký chưa duyệt %>' PageViewID="RadPageView1"
                    Selected="true">
                </tlk:RadTab>
                <tlk:RadTab Text='<%$ Translate: Đăng ký đã duyệt %>' PageViewID="RadPageView2">
                </tlk:RadTab>
                <tlk:RadTab Text='<%$ Translate: Đăng ký không duyệt %>' PageViewID="RadPageView3">
                </tlk:RadTab>
            </Tabs>
        </tlk:RadTabStrip>
        <tlk:RadMultiPage runat="server" ID="pageTypeApprove">
            <tlk:RadPageView ID="RadPageView1" runat="server" Selected="true">
                <div style="display: block; padding: 10px">
                    <tlk:RadTextBox runat="server" ID="txtNote" EmptyMessage='<%$ Translate: Ý kiến %>'
                        Width="300px">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnApprove" Text='<%$ Translate: Duyệt %>' ForeColor="Blue"
                        ValidationGroup="Approve">
                    </tlk:RadButton>
                    <tlk:RadButton runat="server" ID="btnDeny" Text='<%$ Translate: Không duyệt %>' ForeColor="Red"
                        ValidationGroup="Approve">
                    </tlk:RadButton>
                </div>
                <tlk:RadGrid PageSize="50" runat="server" ID="rgWaiting" Width="99%" Height="250px" AllowPaging="True">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView ClientDataKeyNames="ID,ID_EMPLOYEE" DataKeyNames="ID,ID_REGGROUP,LINK_POPUP,ID_EMPLOYEE">
                        <Columns>
                             <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: id %>' DataField="ID_EMPLOYEE" Visible="false">
                            </tlk:GridBoundColumn>
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
                            <tlk:GridTemplateColumn UniqueName="LINK_POPUP" AllowFiltering="false" HeaderText="<%$ Translate: Lịch sử nghỉ %>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnLink" runat="server" Text="View">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="90px" />
                            </tlk:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPageView>
            <tlk:RadPageView ID="RadPageView2" runat="server">
                <tlk:RadGrid PageSize="50" runat="server" ID="grvApproved" Width="99%" Height="280px">
                    <MasterTableView ClientDataKeyNames="ID" DataKeyNames="ID,ID_REGGROUP,LINK_POPUP">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Mã nhân viên %>' DataField="EMPLOYEE_CODE">
                                <ItemStyle Width="75" />
                                <HeaderStyle Width="75" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Tên nhân viên %>' DataField="EMPLOYEE_NAME">
                                <ItemStyle Width="150" />
                                <HeaderStyle Width="150" />
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
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ghi chú %>' DataField="NOTE">
                                <ItemStyle Width="200" />
                                <HeaderStyle Width="200" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày duyệt %>' DataField="APP_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridTemplateColumn UniqueName="LINK_POPUP" AllowFiltering="false" HeaderText="<%$ Translate: Lịch sử nghỉ %>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnLink" runat="server" Text="View">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="90px" />
                            </tlk:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPageView>
            <tlk:RadPageView ID="RadPageView3" runat="server">
                <tlk:RadGrid PageSize="50" runat="server" ID="grvDenied" Width="99%" Height="280px">
                    <MasterTableView ClientDataKeyNames="ID" DataKeyNames="ID,ID_REGGROUP,LINK_POPUP">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Mã nhân viên %>' DataField="EMPLOYEE_CODE">
                                <ItemStyle Width="75" />
                                <HeaderStyle Width="75" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Tên nhân viên %>' DataField="EMPLOYEE_NAME">
                                <ItemStyle Width="150" />
                                <HeaderStyle Width="150" />
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
                                <ItemStyle Width="120" />
                                <HeaderStyle Width="120" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ghi chú %>' DataField="NOTE">
                                <ItemStyle Width="200" />
                                <HeaderStyle Width="200" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày duyệt %>' DataField="APP_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center" Width="70" />
                                <HeaderStyle Width="70" />
                            </tlk:GridBoundColumn>
                            <tlk:GridTemplateColumn UniqueName="LINK_POPUP" AllowFiltering="false" HeaderText="<%$ Translate: Lịch sử nghỉ %>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnLink" runat="server" Text="View">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="90px" />
                            </tlk:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPageView>
        </tlk:RadMultiPage>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function POPUP(value) {
            window.open(value, "_blank");
            return false;
        }
    </script>
</tlk:RadCodeBlock>
