<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ListOfferCandidate.ascx.vb"
    Inherits="Recruitment.ctrlRC_ListOfferCandidate" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal" Scrolling="None">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="130px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Yêu cầu từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdSendFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdSendTo" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>

                    <tr>
                        <td class="lb">
                            <%# Translate("Đáp ứng từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdExpectedFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdExpectedTo" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>

                    <tr>
                        <td class="lb">
                            <%# Translate("Vị trí tuyển dụng")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTitle" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái tuyển dụng")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboStatus" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="chkIsOfferletter" runat="server" Text="<%$ Translate: Đã Offerletter %>" />
                        </td>
                        <td></td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm kiếm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <MasterTableView DataKeyNames="ID,CANDIDATE_ID" ClientDataKeyNames="ID,CANDIDATE_ID,OFFER_NAME,RC_PROGRAM_ID,STATUS_ID_STR" >
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã UV %>" DataField="CANDIDATE_CODE"
                                SortExpression="CANDIDATE_CODE" UniqueName="CANDIDATE_CODE" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên UV %>" DataField="CANDIDATE_NAME"
                                SortExpression="CANDIDATE_NAME" UniqueName="CANDIDATE_NAME" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái Offerletter %>" DataField="OFFER_NAME"
                                SortExpression="OFFER_NAME" UniqueName="OFFER_NAME" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái tuyển dụng %>" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã YCTD %>" DataField="CODE_RC"
                                SortExpression="CODE_RC" UniqueName="CODE_RC" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người yêu cầu %>" DataField="REQUIRER_NAME"
                                SortExpression="REQUIRER_NAME" UniqueName="REQUIRER_NAME" />
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày gửi yêu cầu %>" DataField="SEND_DATE"
                                SortExpression="SEND_DATE" UniqueName="SEND_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                                
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày đáp ứng %>" DataField="EXPECTED_JOIN_DATE"
                                SortExpression="EXPECTED_JOIN_DATE" UniqueName="EXPECTED_JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />  
                               
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban yêu cầu %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />

                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />
                                
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí tuyển dụng %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                                
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do tuyển dụng %>" DataField="RECRUIT_REASON_NAME"
                                SortExpression="RECRUIT_REASON_NAME" UniqueName="RECRUIT_REASON_NAME" />
                                
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phụ trách %>" DataField="PERSON_PT_RC_NAME"
                                SortExpression="PERSON_PT_RC_NAME" UniqueName="PERSON_PT_RC_NAME" />                         
                        </Columns>
                        <HeaderStyle Width="150px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True"/>
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            OnClientBeforeClose="OnClientBeforeClose" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
        }
        function OnClientClose(oWnd, args) {
            $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            enableAjax = false;
        }

        function OnClientBeforeClose(sender, eventArgs) {
            var arg = eventArgs.get_argument();
            if (!arg) {
                if (!confirm("Bạn có muốn đóng màn hình không?")) {
                    //if cancel is clicked prevent the window from closing
                    args.set_cancel(true);
                }
            }
        }
    </script>
</tlk:RadCodeBlock>
