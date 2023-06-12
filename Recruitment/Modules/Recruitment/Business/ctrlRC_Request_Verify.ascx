<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_Request_Verify.ascx.vb"
    Inherits="Recruitment.ctrlRC_Request_Verify" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal" Scrolling="None">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="80px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Lý do không duyệt")%>
                        </td>
                        <td colspan="4">
                            <tlk:RadTextBox ID="txtRemarkReject" runat="server" Width="100%" SkinID="Textbox1023" TextMode="MultiLine"></tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdToDate"
                                ControlToCompare="rdFromDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"
                                ToolTip="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"></asp:CompareValidator>
                        </td>
                        <td>
                            <tlk:RadButton Width="100px" ID="btnFind" runat="server" Text="<%$ Translate: Tìm kiếm %>"
                                CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowMultiRowSelection="True">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE_RC,NAME_RC,REQUIRER_NAME,STATUS_ID,RC_RECRUIT_PROPERTY,RC_RECRUIT_PROPERTY_NAME,IS_OVER_LIMIT,FOREIGN_ABILITY,COMPUTER_APP_LEVEL,GENDER_PRIORITY,GENDER_PRIORITY_NAME,RECRUIT_NUMBER,UPLOAD_FILE,REMARK_REJECT,EMPLOYEE_APPROVED,REQUIRER">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã YCTD %>" DataField="CODE_RC"
                                SortExpression="CODE_RC" UniqueName="CODE_RC" HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên YCTD %>" DataField="NAME_RC"
                                SortExpression="NAME_RC" UniqueName="NAME_RC" HeaderStyle-Width="200px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người gửi yêu cầu %>" DataField="REQUIRER_NAME"
                                SortExpression="REQUIRER_NAME" UniqueName="REQUIRER_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày gửi yêu cầu %>" DataField="SEND_DATE"
                                SortExpression="SEND_DATE" UniqueName="SEND_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày đáp ứng %>" DataField="EXPECTED_JOIN_DATE"
                                SortExpression="EXPECTED_JOIN_DATE" UniqueName="EXPECTED_JOIN_DATE" HeaderStyle-Width="120px" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí tuyển dụng %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không duyệt %>" DataField="REMARK_REJECT"
                                SortExpression="REMARK_REJECT" UniqueName="REMARK_REJECT" />
                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Scrolling AllowScroll="true" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:HiddenField ID="hddLinkPopup" runat="server" Value="Dialog.aspx?mid=Recruitment&fid=ctrlRC_Request_VerifyReject&group=Business&noscroll=1" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<common:ctrlupload id="ctrlUpload1" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
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

        function gridRowDblClick(sender, eventArgs) {
            OpenEdit();
        }

        function OpenEdit() {
            var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            //window.open('/Default.aspx?mid=Recruitment&fid=ctrlRC_RequestNewEdit&group=Business&noscroll=1&ID=' + id, "_self");
            var oWindow = radopen('Dialog.aspx?mid=Recruitment&fid=ctrlRC_RequestNewEdit&group=Business&noscroll=1&ID=' + id, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            //oWindow.moveTo(pos.left, pos.middle);
            //oWindow.setSize(1200, 500);
            return 2;
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;

            if (args.get_item().get_commandName() == 'APROVE' || args.get_item().get_commandName() == 'UNAPPROVE') {
                enableAjax = false;
            }
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            }
        }

    </script>
</tlk:RadCodeBlock>
