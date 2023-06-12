<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_FindCandidate.ascx.vb"
    Inherits="Recruitment.ctrlRC_FindCandidate" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="150px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
            <table class="table-form padding-10">
                <tr>
                    <td colspan="7">
                        <b>
                            <%# Translate("Thông tin tìm kiếm")%></b>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Mã/Tên ứng viên")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtCandidateCode" runat="server">
                        </tlk:RadTextBox>
                    </td>
                    <td>
                        <tlk:RadButton Width="100px" runat="server" ID="btnSearch" SkinID="ButtonFind" Text="<%$ Translate: Tìm kiếm %>">
                        </tlk:RadButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgCandidateList" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID,Candidate_CODE,STATUS_ID" ClientDataKeyNames="ID,Candidate_CODE,STATUS_ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ứng viên %>" DataField="Candidate_CODE"
                        UniqueName="Candidate_CODE" SortExpression="Candidate_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULLNAME_VN"
                        UniqueName="FULLNAME_VN" SortExpression="FULLNAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME_VN"
                        UniqueName="TITLE_NAME_VN" SortExpression="TITLE_NAME_VN" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                        UniqueName="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="BIRTH_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CMND %>" DataField="ID_NO" UniqueName="ID_NO"
                        SortExpression="ID_NO" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="ID_DATE"
                        UniqueName="ID_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="ID_DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái ứng viên %>" DataField="STATUS_NAME"
                        UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" Width="100px" />
            <ItemStyle Width="100px" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
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


        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'SAVE') {
                var bCheck = $find('<%# rgCandidateList.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                if (bCheck > 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                var id = $find('<%# rgCandidateList.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                getRadWindow().close('PONTENTIAL_TRANSFER;' + id);
                args.set_cancel(true);
            }

            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

    </script>
</tlk:RadScriptBlock>
