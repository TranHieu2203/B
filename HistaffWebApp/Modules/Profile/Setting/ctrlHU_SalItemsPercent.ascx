<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_SalItemsPercent.ascx.vb"
    Inherits="Profile.ctrlHU_SalItemsPercent" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .borderRight {
        /*border-right: 1px solid #C1C1C1;*/
    }
</style>
<asp:HiddenField ID="hiOrgdID" runat="server" />
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%">
    <tlk:radpane id="LeftPane" runat="server" minwidth="200" width="250px" scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
    </tlk:radpane>
    <tlk:radsplitbar id="RadSplitBar1" runat="server" collapsemode="Forward">
    </tlk:radsplitbar>
    <tlk:radpane id="MainPane" runat="server" scrolling="None">
        <tlk:radsplitter id="RadSplitter3" runat="server" width="100%" height="100%" orientation="Horizontal">
            <tlk:radpane id="RadPane3" runat="server" height="38px" scrolling="None">
                <tlk:radtoolbar id="tbarMain" runat="server" onclientbuttonclicking="clientButtonClicking" />
            </tlk:radpane>
            <tlk:radpane id="RadPane1" runat="server" height="180px">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td colspan="10" style="font-style: italic; color: red; padding-left: 450px;">
                            <asp:Label ID="Label13" runat="server" Text="Lưu ý: Chọn LEFT khi khoản lương đó là % còn lại, để trống những khoản lương không dùng"></asp:Label>
                            <asp:Label ID="Label14" runat="server" Visible="false" Text="Lưu ý: Điền 0 vào những khoản có sử dụng, những khoản không sử dụng, vui lòng để trống"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbOrg" runat="server" Text="Đơn vị" Width="120px"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:radtextbox id="txtOrgName" runat="server" width="150px" readonly="true" skinid="Readonly">
                            </tlk:radtextbox>
                        </td>
                        <td class="borderRight"></td>
                        <td class="lb">
                            <asp:Label ID="Label1" runat="server" Text="Lương cơ bản (BHXH)" Width="150px"></asp:Label>
                        </td>
                        <td>
                            <tlk:radnumerictextbox id="rnLuongCB" runat="server" skinid="Decimal" Width="50px">
                            </tlk:radnumerictextbox>
                        </td>
                        <td style="display: none">
                            <asp:RadioButton ID="chkLuongCB" runat="server" Text="LEFT" AutoPostBack="true" CausesValidation="false" GroupName="checkLeft" />
                        </td>
                        <td></td>
                        <td class="borderRight"></td>
                        <td class="lb">
                            <asp:Label ID="Label4" runat="server" Text="PC cơm" Width="150px"></asp:Label>
                        </td>
                        <td>
                            <tlk:radnumerictextbox id="rnOther1" runat="server" skinid="Decimal" Width="50px">
                            </tlk:radnumerictextbox>
                        </td>
                        <td>
                            <asp:RadioButton ID="chkOther1" runat="server" Text="LEFT" AutoPostBack="true" CausesValidation="false" GroupName="checkLeft" />
                        </td>
                        <td style ="padding-left:50px">
                            <asp:CheckBox ID="chkViolate" runat="server" Text="Áp dụng quy chế xử phạt"  />
                        </td>
                    </tr>
                    <tr>
                        <%--<td class="lb">
                            <asp:Label ID="lbUnuseRatio" runat="server" Text="Không sử dụng tỉ lệ"></asp:Label>
                        </td>--%>
                        <td class="lb">
                            <asp:Label ID="lbToDate" runat="server" Text="Ngày hiệu lực"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:raddatepicker id="rdEffectDate" runat="server" width="150px">
                            </tlk:raddatepicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdEffectDate" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải chọn ngày hiệu lực. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="borderRight"></td>
                        <td class="lb">
                            <asp:Label ID="Label3" runat="server" Text="Xăng xe"></asp:Label>
                        </td>
                        <td>
                            <tlk:radnumerictextbox id="rnXangXe" runat="server" skinid="Decimal" Width="50px">
                            </tlk:radnumerictextbox>
                        </td>
                        <td>
                            <asp:RadioButton ID="chkXangXe" runat="server" Text="LEFT" AutoPostBack="true" CausesValidation="false" GroupName="checkLeft" />
                        </td>
                        <td class="borderRight"></td>
                        <td class="lb">
                            <asp:Label ID="Label5" runat="server" Text="PC hỗ trợ nhà ở"></asp:Label>
                        </td>
                        <td>
                            <tlk:radnumerictextbox id="rnOther2" runat="server" skinid="Decimal" Width="50px">
                            </tlk:radnumerictextbox>
                        </td>
                        <td>
                            <asp:RadioButton ID="chkOther2" runat="server" Text="LEFT" AutoPostBack="true" CausesValidation="false" GroupName="checkLeft" />
                        </td>
                        <td class="lb" style ="padding-left:50px; text-align: left">
                            <asp:Label ID="Label15" runat="server" Text="Phí công đoàn:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="Label12" runat="server" Text="Trạng thái"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:radcombobox id="cboActflg" runat="server" width="150px">
                                <items>
                                    <tlk:radcomboboxitem runat="server" text='<%$ Translate: Áp dụng %>' value="1" />
                                    <tlk:radcomboboxitem runat="server" text='<%$ Translate: Ngừng áp dụng %>' value="2" />
                                </items>
                            </tlk:radcombobox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboActflg" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn trạng thái. %>" ToolTip="<%$ Translate: Bạn phải chọn trạng thái. %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="borderRight"></td>
                        <td class="lb">
                            <asp:Label ID="Label6" runat="server" Text="Điện thoại"></asp:Label>
                        </td>
                        <td>
                            <tlk:radnumerictextbox id="rnDienThoai" runat="server" skinid="Decimal" Width="50px">
                            </tlk:radnumerictextbox>
                        </td>
                        <td>
                            <asp:RadioButton ID="chkDienThoai" runat="server" Text="LEFT" AutoPostBack="true" CausesValidation="false" GroupName="checkLeft" />
                        </td>
                        <td class="borderRight"></td>
                        <td class="lb">
                            <asp:Label ID="Label7" runat="server" Text="PC đồng phục"></asp:Label>
                        </td>
                        <td>
                            <tlk:radnumerictextbox id="rnOther3" runat="server" skinid="Decimal" Width="50px">
                            </tlk:radnumerictextbox>
                        </td>
                        <td>
                            <asp:RadioButton ID="chkOther3" runat="server" Text="LEFT" AutoPostBack="true" CausesValidation="false" GroupName="checkLeft" />
                        </td>
                        <td style ="padding-left:50px">
                            <asp:RadioButton ID="chkUnionPercent" runat="server" Text="Theo tỷ lệ" AutoPostBack="true" CausesValidation="false" GroupName="checkUnion" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="Label2" runat="server" Text="Ghi chú"></asp:Label>
                        </td>
                        <td>
                            <tlk:radtextbox id="txtNote" runat="server" width="150px">
                            </tlk:radtextbox>
                        </td>
                        <td class="borderRight"></td>
                        <td class="lb">
                            <asp:Label ID="Label8" runat="server" Text="Khoản bổ sung"></asp:Label>
                        </td>
                        <td>
                            <tlk:radnumerictextbox id="rnLuongBS" runat="server" skinid="Decimal" Width="50px">
                            </tlk:radnumerictextbox>
                        </td>
                        <td>
                            <asp:RadioButton ID="chkLuongBS" runat="server" Text="LEFT" AutoPostBack="true" CausesValidation="false" GroupName="checkLeft" />
                        </td>
                        <td class="borderRight"></td>
                        <td class="lb">
                            <asp:Label ID="Label9" runat="server" Text="PC đi lại"></asp:Label>
                        </td>
                        <td>
                            <tlk:radnumerictextbox id="rnOther4" runat="server" skinid="Decimal" Width="50px">
                            </tlk:radnumerictextbox>
                        </td>
                        <td>
                            <asp:RadioButton ID="chkOther4" runat="server" Text="LEFT" AutoPostBack="true" CausesValidation="false" GroupName="checkLeft" />
                        </td>
                        <td style ="padding-left:50px">
                            <asp:RadioButton ID="chkUnionPermanent" runat="server" Text="Cố định" AutoPostBack="true" CausesValidation="false" GroupName="checkUnion" />
                            <tlk:radnumerictextbox id="rnUnionMoney" runat="server" skinid="Money" Width="70px">
                            </tlk:radnumerictextbox>
                            <asp:Label ID="Label16" runat="server" Text="(VNĐ)"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="chkUnuseRatio" runat="server" Text="Không sử dụng tỉ lệ" AutoPostBack="true" />
                        </td>
                        <td class="borderRight"></td>
                        <td class="lb">
                            <asp:Label ID="Label10" runat="server" Text="Thưởng HQCLCV"></asp:Label>
                        </td>
                        <td>
                            <tlk:radnumerictextbox id="rnYTCLCV" runat="server" skinid="Decimal" Width="50px">
                            </tlk:radnumerictextbox>
                        </td>
                        <td>
                            <asp:RadioButton ID="chkYTCLCV" runat="server" Text="LEFT" AutoPostBack="true" CausesValidation="false" GroupName="checkLeft" />
                        </td>
                        <td class="borderRight"></td>
                        <td class="lb">
                            <asp:Label ID="Label11" runat="server" Text="Thưởng chuyên cần"></asp:Label>
                        </td>
                        <td>
                            <tlk:radnumerictextbox id="rnOther5" runat="server" skinid="Decimal" Width="50px">
                            </tlk:radnumerictextbox>
                        </td>
                        <td>
                            <asp:RadioButton ID="chkOther5" runat="server" Text="LEFT" AutoPostBack="true" CausesValidation="false" GroupName="checkLeft" />
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:radpane>
            <tlk:radpane id="RadPane2" runat="server" scrolling="None">
                <tlk:radgrid pagesize="50" id="rgData" runat="server" height="100%" allowpaging="True" allowfilteringbycolumn="true"
                    allowsorting="True" allowmultirowselection="true">
                    <clientsettings enablerowhoverstyle="true" enablepostbackonrowclick="true">
                        <selecting allowrowselect="true" />
                    </clientsettings>
                    <mastertableview datakeynames="ID,ORG_ID,ORG_NAME,EFFECT_DATE,LUONGCB,XANGXE,DIENTHOAI,LUONGBS,THUONGYTCLCV,OTHER1,OTHER2,OTHER3,OTHER4,OTHER5,NOTE,ACTFLG,UNUSE_RATIO,VIOLATE,UNION_PERCENT,UNION_PERMANENT,UNION_MONEY"
                        clientdatakeynames="ID,ORG_ID,ORG_NAME,EFFECT_DATE,LUONGCB,XANGXE,DIENTHOAI,LUONGBS,THUONGYTCLCV,OTHER1,OTHER2,OTHER3,OTHER4,OTHER5,NOTE,ACTFLG,UNUSE_RATIO,VIOLATE,UNION_PERCENT,UNION_PERMANENT,UNION_MONEY">
                        <columns>
                            <%--  <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="130px" ShowFilterIcon="true" ShowSortIcon="false" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Không sử dụng ệệ %>" DataField="UNUSE_RATIO" HeaderStyle-HorizontalAlign="Center"
                                UniqueName="UNUSE_RATIO" SortExpression="UNUSE_RATIO" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="ACTFLG_NAME"
                                SortExpression="ACTFLG_NAME" UniqueName="ACTFLG_NAME" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Lương cơ bản" DataField="LUONGCB"
                                SortExpression="LUONGCB" UniqueName="LUONGCB" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="Xăng xe" DataField="XANGXE"
                                SortExpression="XANGXE" UniqueName="XANGXE" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="Điện thoại" DataField="DIENTHOAI"
                                SortExpression="DIENTHOAI" UniqueName="DIENTHOAI" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="Khoản bổ sung" DataField="LUONGBS"
                                SortExpression="LUONGBS" UniqueName="LUONGBS" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="Thưởng HQCLCV" DataField="THUONGYTCLCV"
                                SortExpression="THUONGYTCLCV" UniqueName="THUONGYTCLCV" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="PC cơm" DataField="OTHER1"
                                SortExpression="OTHER1" UniqueName="OTHER1" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="PC hỗ trợ nhà ở" DataField="OTHER2"
                                SortExpression="OTHER2" UniqueName="OTHER2" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="PC đồng phục" DataField="OTHER3"
                                SortExpression="OTHER3" UniqueName="OTHER3" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="PC đi lại" DataField="OTHER4"
                                SortExpression="OTHER4" UniqueName="OTHER4" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="Thưởng chuyên cần" DataField="OTHER5"
                                SortExpression="OTHER5" UniqueName="OTHER5" HeaderStyle-Width="80px" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE"
                                SortExpression="NOTE" UniqueName="NOTE" HeaderStyle-Width="150px" />--%>
                        </columns>
                    </mastertableview>
                </tlk:radgrid>
            </tlk:radpane>
        </tlk:radsplitter>
    </tlk:radpane>
</tlk:radsplitter>
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }
    </script>
</tlk:radcodeblock>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
