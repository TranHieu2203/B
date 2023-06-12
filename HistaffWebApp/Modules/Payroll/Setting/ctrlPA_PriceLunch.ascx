<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_PriceLunch.ascx.vb"
    Inherits="Payroll.ctrlPA_PriceLunch" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" CheckBoxes="All" CheckChildNodes="true" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Height="150px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày bắt đầu")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="dpEffectDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="dpEffectDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày bắt đầu %>" ToolTip="<%$ Translate: Bạn phải nhập ngày bắt đầu %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày kết thúc")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="dpExpireDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="dpExpireDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày kết thúc %>" ToolTip="<%$ Translate: Bạn phải nhập ngày kết thúc %>">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày bắt đầu phải nhỏ hơn ngày kết thúc %>"
                                ErrorMessage="<%$ Translate: Ngày bắt đầu phải nhỏ hơn ngày kết thúc %>" Type="Date"
                                Operator="GreaterThan" ControlToCompare="dpEffectDate" ControlToValidate="dpExpireDate"></asp:CompareValidator>
                            <asp:CustomValidator ID="cvalEffedate" runat="server" ErrorMessage="<%$ Translate: Khoảng thời gian của đơn giá tiền ăn đã tồn tại. %>"
                                ToolTip="<%$ Translate: Khoảng thời gian của đơn giá tiền ăn đã tồn tại. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Đơn giá tiền ăn")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="nmrPrice" runat="server" MinValue="0"
                               >
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="nmrPrice"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập đơn giá tiền ăn %>" ToolTip="<%$ Translate: Bạn phải nhập công chuẩn %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi Chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" SkinID="GridSingleSelect" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,PRICE,EFFECT_DATE,EXPIRE_DATE,REMARK">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="CHK" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="EFFECT_DATE"
                                SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc%>" DataField="EXPIRE_DATE"
                                SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Đơn giá tiền ăn %>" DataField="PRICE"
                                SortExpression="PRICE" UniqueName="PRICE" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                SortExpression="REMARK" UniqueName="REMARK">
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function onRequestStart(sender, eventArgs)
        {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args)
        {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT")
            {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE")
            {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else
            {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter()
        {
            setTimeout(function ()
            {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault()
        {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
            if (oldSize == 0)
            {
                oldSize = pane.getContentElement().scrollHeight;
            } else
            {
                var pane2 = splitter.getPaneById('<%= RadPane1.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
