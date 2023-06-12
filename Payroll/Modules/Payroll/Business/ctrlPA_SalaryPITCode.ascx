<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryPITCode.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryPITCode" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrganization" runat="server" checkboxes="All" checkchildnodes="true" autopostback="false" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="70px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbYear" Text="Năm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cbYear" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true" AllowMultiRowEdit="true">
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID" ClientDataKeyNames="ID,EMPLOYEE_ID" EditMode="InPlace">
                        <ColumnGroups>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 1" Name="M1">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 2" Name="M2">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 3" Name="M3">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 4" Name="M4">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 5" Name="M5">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 6" Name="M6">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 7" Name="M7">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 8" Name="M8">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 9" Name="M9">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 10" Name="M10">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 11" Name="M11">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 12" Name="M12">
                            </tlk:GridColumnGroup>
                            <tlk:GridColumnGroup HeaderText="Thông tin lương tháng 12 năm trước" Name="M12_O">
                            </tlk:GridColumnGroup>
                        </ColumnGroups>
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridCheckBoxColumn HeaderText="Đã khóa" AllowFiltering="false" DataField="IS_LOCK" SortExpression="IS_LOCK"
                                UniqueName="IS_LOCK" ReadOnly="true"  HeaderStyle-Width="65px"/>
                            <tlk:GridBoundColumn HeaderText="Mã số thuế" DataField="PIT_CODE" SortExpression="PIT_CODE"
                                UniqueName="PIT_CODE" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Năm" DataField="YEAR" SortExpression="YEAR" DataFormatString="{0:####}"
                                UniqueName="YEAR" ReadOnly="true"  HeaderStyle-Width="65px"/>
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" SortExpression="TITLE_NAME"
                                UniqueName="TITLE_NAME" ReadOnly="true" />
                            
                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="M12_THUNHAP_CHIUTHUE"
                                SortExpression="M12_THUNHAP_CHIUTHUE" UniqueName="M12_THUNHAP_CHIUTHUE" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M12_O" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="M12_GTGC_NV"
                                SortExpression="M12_GTGC_NV" UniqueName="M12_GTGC_NV" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M12_O" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="M12_SUM_BHXH"
                                SortExpression="M12_SUM_BHXH" UniqueName="M12_SUM_BHXH" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M12_O" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="M12_THUNHAP_TINHTHUE"
                                SortExpression="M12_THUNHAP_TINHTHUE" UniqueName="M12_THUNHAP_TINHTHUE" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M12_O" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="M12_THUETNCN"
                                SortExpression="M12_THUETNCN" UniqueName="M12_THUETNCN" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M12_O" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE1"
                                SortExpression="TT_THUNHAP_CHIUTHUE1" UniqueName="TT_THUNHAP_CHIUTHUE1" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M1" DataFormatString="{0:N0}"/>
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV1"
                                SortExpression="TT_GTGC_NV1" UniqueName="TT_GTGC_NV1" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M1" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH1"
                                SortExpression="TT_SUM_BHXH1" UniqueName="TT_SUM_BHXH1" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M1" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE1"
                                SortExpression="TT_THUNHAP_TINHTHUE1" UniqueName="TT_THUNHAP_TINHTHUE1" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M1" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN1"
                                SortExpression="TT_THUETNCN1" UniqueName="TT_THUETNCN1" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M1" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE2"
                                SortExpression="TT_THUNHAP_CHIUTHUE2" UniqueName="TT_THUNHAP_CHIUTHUE2" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M2" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV2"
                                SortExpression="TT_GTGC_NV2" UniqueName="TT_GTGC_NV2" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M2" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH2"
                                SortExpression="TT_SUM_BHXH2" UniqueName="TT_SUM_BHXH2" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M2" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE2"
                                SortExpression="TT_THUNHAP_TINHTHUE2" UniqueName="TT_THUNHAP_TINHTHUE2" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M2" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN2"
                                SortExpression="TT_THUETNCN2" UniqueName="TT_THUETNCN2" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M2" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE3"
                                SortExpression="TT_THUNHAP_CHIUTHUE3" UniqueName="TT_THUNHAP_CHIUTHUE3" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M3" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV3"
                                SortExpression="TT_GTGC_NV3" UniqueName="TT_GTGC_NV3" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M3" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH3"
                                SortExpression="TT_SUM_BHXH3" UniqueName="TT_SUM_BHXH3" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M3" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE3"
                                SortExpression="TT_THUNHAP_TINHTHUE3" UniqueName="TT_THUNHAP_TINHTHUE3" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M3" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN3"
                                SortExpression="TT_THUETNCN3" UniqueName="TT_THUETNCN3" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M3" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE4"
                                SortExpression="TT_THUNHAP_CHIUTHUE4" UniqueName="TT_THUNHAP_CHIUTHUE4" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M4" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV4"
                                SortExpression="TT_GTGC_NV4" UniqueName="TT_GTGC_NV4" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M4" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH4"
                                SortExpression="TT_SUM_BHXH4" UniqueName="TT_SUM_BHXH4" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M4" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE4"
                                SortExpression="TT_THUNHAP_TINHTHUE4" UniqueName="TT_THUNHAP_TINHTHUE4" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M4" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN4"
                                SortExpression="TT_THUETNCN4" UniqueName="TT_THUETNCN4" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M4" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE5"
                                SortExpression="TT_THUNHAP_CHIUTHUE5" UniqueName="TT_THUNHAP_CHIUTHUE5" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M5" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV5"
                                SortExpression="TT_GTGC_NV5" UniqueName="TT_GTGC_NV5" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M5" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH5"
                                SortExpression="TT_SUM_BHXH5" UniqueName="TT_SUM_BHXH5" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M5" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE5"
                                SortExpression="TT_THUNHAP_TINHTHUE5" UniqueName="TT_THUNHAP_TINHTHUE5" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M5" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN5"
                                SortExpression="TT_THUETNCN5" UniqueName="TT_THUETNCN5" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M5" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE6"
                                SortExpression="TT_THUNHAP_CHIUTHUE6" UniqueName="TT_THUNHAP_CHIUTHUE6" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M6" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV6"
                                SortExpression="TT_GTGC_NV6" UniqueName="TT_GTGC_NV6" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M6" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH6"
                                SortExpression="TT_SUM_BHXH6" UniqueName="TT_SUM_BHXH6" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M6" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE6"
                                SortExpression="TT_THUNHAP_TINHTHUE6" UniqueName="TT_THUNHAP_TINHTHUE6" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M6" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN6"
                                SortExpression="TT_THUETNCN6" UniqueName="TT_THUETNCN6" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M6" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE7"
                                SortExpression="TT_THUNHAP_CHIUTHUE7" UniqueName="TT_THUNHAP_CHIUTHUE7" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M7" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV7"
                                SortExpression="TT_GTGC_NV7" UniqueName="TT_GTGC_NV7" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M7" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH7"
                                SortExpression="TT_SUM_BHXH7" UniqueName="TT_SUM_BHXH7" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M7" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE7"
                                SortExpression="TT_THUNHAP_TINHTHUE7" UniqueName="TT_THUNHAP_TINHTHUE7" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M7" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN7"
                                SortExpression="TT_THUETNCN7" UniqueName="TT_THUETNCN7" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M7" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE8"
                                SortExpression="TT_THUNHAP_CHIUTHUE8" UniqueName="TT_THUNHAP_CHIUTHUE8" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M8" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV8"
                                SortExpression="TT_GTGC_NV8" UniqueName="TT_GTGC_NV8" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M8" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH8"
                                SortExpression="TT_SUM_BHXH8" UniqueName="TT_SUM_BHXH8" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M8" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE8"
                                SortExpression="TT_THUNHAP_TINHTHUE8" UniqueName="TT_THUNHAP_TINHTHUE8" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M8" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN8"
                                SortExpression="TT_THUETNCN8" UniqueName="TT_THUETNCN8" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M8" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE9"
                                SortExpression="TT_THUNHAP_CHIUTHUE9" UniqueName="TT_THUNHAP_CHIUTHUE9" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M9" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV9"
                                SortExpression="TT_GTGC_NV9" UniqueName="TT_GTGC_NV9" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M9" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH9"
                                SortExpression="TT_SUM_BHXH9" UniqueName="TT_SUM_BHXH9" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M9" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE9"
                                SortExpression="TT_THUNHAP_TINHTHUE9" UniqueName="TT_THUNHAP_TINHTHUE9" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M9" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN9"
                                SortExpression="TT_THUETNCN9" UniqueName="TT_THUETNCN9" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M9" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE10"
                                SortExpression="TT_THUNHAP_CHIUTHUE10" UniqueName="TT_THUNHAP_CHIUTHUE10" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M10" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV10"
                                SortExpression="TT_GTGC_NV10" UniqueName="TT_GTGC_NV10" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M10" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH10"
                                SortExpression="TT_SUM_BHXH10" UniqueName="TT_SUM_BHXH10" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M10" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE10"
                                SortExpression="TT_THUNHAP_TINHTHUE10" UniqueName="TT_THUNHAP_TINHTHUE10" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M10" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN10"
                                SortExpression="TT_THUETNCN10" UniqueName="TT_THUETNCN10" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M10" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE11"
                                SortExpression="TT_THUNHAP_CHIUTHUE11" UniqueName="TT_THUNHAP_CHIUTHUE11" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M11" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV11"
                                SortExpression="TT_GTGC_NV11" UniqueName="TT_GTGC_NV11" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M11" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH11"
                                SortExpression="TT_SUM_BHXH11" UniqueName="TT_SUM_BHXH11" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M11" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE11"
                                SortExpression="TT_THUNHAP_TINHTHUE11" UniqueName="TT_THUNHAP_TINHTHUE11" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M11" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN11"
                                SortExpression="TT_THUETNCN11" UniqueName="TT_THUETNCN11" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M11" DataFormatString="{0:N0}" />
                            
                            <tlk:GridNumericColumn HeaderText="Lương tạm ứng" DataField="TT_TAMUNG_LUONG12"
                                SortExpression="TT_TAMUNG_LUONG12" UniqueName="TT_TAMUNG_LUONG12" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M12" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế" DataField="TT_THUNHAP_CHIUTHUE12"
                                SortExpression="TT_THUNHAP_CHIUTHUE12" UniqueName="TT_THUNHAP_CHIUTHUE12" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M12" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng giảm trừ gia cảnh" DataField="TT_GTGC_NV12"
                                SortExpression="TT_GTGC_NV12" UniqueName="TT_GTGC_NV12" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M12" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH" DataField="TT_SUM_BHXH12"
                                SortExpression="TT_SUM_BHXH12" UniqueName="TT_SUM_BHXH12" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M12" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="TT_THUNHAP_TINHTHUE12"
                                SortExpression="TT_THUNHAP_TINHTHUE12" UniqueName="TT_THUNHAP_TINHTHUE12" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M12" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế" DataField="TT_THUETNCN12"
                                SortExpression="TT_THUETNCN12" UniqueName="TT_THUETNCN12" ReadOnly="true" HeaderStyle-Width="150px"
                                ColumnGroupName="M12" DataFormatString="{0:N0}" />

                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế năm" DataField="TT_CHIUTHUE_TOTAL" DataFormatString="{0:N0}" 
                                SortExpression="TT_CHIUTHUE_TOTAL" UniqueName="TT_CHIUTHUE_TOTAL" ReadOnly="true" HeaderStyle-Width="150px" Visible="true" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền BHXH năm" DataField="TT_BHXH_TOTAL" DataFormatString="{0:N0}" 
                                SortExpression="TT_BHXH_TOTAL" UniqueName="TT_BHXH_TOTAL" ReadOnly="true" HeaderStyle-Width="150px" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế năm" DataField="TT_THUETNCN_TOTAL" DataFormatString="{0:N0}" 
                                SortExpression="TT_THUETNCN_TOTAL" UniqueName="TT_THUETNCN_TOTAL" ReadOnly="true" HeaderStyle-Width="150px" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <Scrolling AllowScroll="true"/>
                    </ClientSettings>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_SalaryPITCode_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SalaryPITCode_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SalaryPITCode_RadPane2';
        var validateID = 'MainContent_ctrlPA_SalaryPITCode_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgMain.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize

            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;

            } else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>

<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
