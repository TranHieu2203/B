<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindPlanPopup.ascx.vb"
    Inherits="Training.ctrlFindPlanPopup" %>
<style type="text/css">
    #ctl00_MainContent_ctrlTR_ProgramNewEdit_ctrlFindPlanPopup_rwMessage_C_RadSplitter1,
    #ctl00_MainContent_ctrlTR_ProgramNewEdit_ctrlFindPlanPopup_rwMessage_C_RadSplitter3,
    #ctl00_MainContent_ctrlTR_ProgramNewEdit_ctrlFindPlanPopup_rwMessage_C_RadSplitter2,
    {
        height: 420px !important;
    }
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlTR_ProgramNewEdit_ctrlFindPlanPopup_rwMessage_C_RadPane7
    {
        height: 400px !important;
    }
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlTR_ProgramNewEdit_ctrlFindPlanPopup_rwMessage_C_RadPane6
    {
        height: 450px !important;
    }
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlTR_ProgramNewEdit_ctrlFindPlanPopup_rwMessage_C_RadPane5
    {
        height: 380px !important;
    }
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlTR_ProgramNewEdit_ctrlFindPlanPopup_rwMessage_C_RadPane3
    {
        height: 430px !important;
    }
    #ctl00_MainContent_ctrlTR_ProgramNewEdit_ctrlFindPlanPopup_rwMessage_C_RadSplitter1
    {
        height: 420px !important;
    }
    #ctl00_MainContent_ctrlTR_ProgramNewEdit_ctrlFindPlanPopup_rwMessage_C_RadSplitter2
    {
        height: 420px !important;
    }
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlTR_ProgramNewEdit_ctrlFindPlanPopup_rwMessage_C_RadPane8
    {
        width: 500px !important;
    }
</style>
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="Close" VisibleStatusbar="false"
    Height="500px" Width="1000px" Modal="true" Title="<%$ Translate: Kế hoạch đào tạo năm %>"
   OnClientClose="popupclose" VisibleOnPageLoad ="true"  Scrolling="None"
    ViewStateMode="Enabled">
    <ContentTemplate>
    <asp:HiddenField ID="hidEmployeeID" runat="server" />
    <asp:TextBox ID="txtEmployeeID" runat="server" style="display:none;"></asp:TextBox>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Scrolling="None" Height="400px" >
            <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Scrolling="None" Orientation="Horizontal">
                <%--<tlk:RadPane ID="RadPane3" runat="server" MinWidth="280" Width="280px" Scrolling="None">
                    <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
                        <tlk:RadPane ID="RadPane4" runat="server" Height="50px" Scrolling="None">
                            <table class="table-form">
                                <tr>
                                    <td>
                                        <%# Translate("Năm")%>
                                    </td>
                                    <td>
                                        <tlk:RadComboBox ID="cboYear" runat="server">
                                        </tlk:RadComboBox>
                                    </td>
                                    <td colspan="2">
                                        <tlk:RadButton ID="btnSearch" runat="server" Width="60px" Text="<%$ Translate: Tìm %>"
                                            Font-Bold="true" CausesValidation="false">
                                        </tlk:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </tlk:RadPane>
                        <tlk:RadPane ID="RadPane5" runat="server" MinWidth="200" Width="250px" Height="300px" Scrolling="None">
                            <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
                        </tlk:RadPane>
                    </tlk:RadSplitter>
                </tlk:RadPane>--%>
             <%--   <tlk:RadPane ID="RadPane6" runat="server" Scrolling="None">
                    <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">--%>
                        <tlk:RadPane ID="RadPane7" runat="server">
                            <tlk:RadGrid  ID="rgPlan" runat="server" AutoGenerateColumns="False" AllowPaging="False" Height="400px">
                                <MasterTableView DataKeyNames="ID,TR_PLAN_CODE,ORG_NAME,ORG_ID,TR_COURSE_NAME,TR_COURSE_ID,VENUE,EXPECT_TR_FROM,EXPECT_TR_TO,STUDENT_NUMBER,COST_TOTAL_USD"
                                                ClientDataKeyNames="ID,TR_PLAN_CODE,ORG_NAME,ORG_ID,TR_COURSE_NAME,TR_COURSE_ID,VENUE,EXPECT_TR_FROM,EXPECT_TR_TO,STUDENT_NUMBER,COST_TOTAL_USD">
                                    <Columns>
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: ID %>" DataField="ID"
                                                                SortExpression="ID" UniqueName="ID"  Visible="false"/>
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã kế hoạch đào tạo %>" DataField="TR_PLAN_CODE"
                                            SortExpression="TR_PLAN_CODE" UniqueName="TR_PLAN_CODE" />
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                            SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo %>" DataField="TR_COURSE_NAME" 
                                            SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME"/>
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi đào tạo %>" DataField="VENUE" 
                                            SortExpression="VENUE" UniqueName="VENUE"/>
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian dự kiến từ %>" DataField="EXPECT_TR_FROM"
                                            ItemStyle-HorizontalAlign="Center" SortExpression="EXPECT_TR_FROM" UniqueName="EXPECT_TR_FROM"
                                            DataFormatString="{0:dd/MM/yyyy}" />
                                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian dự kiến đến %>" DataField="EXPECT_TR_TO"
                                            ItemStyle-HorizontalAlign="Center" SortExpression="EXPECT_TR_TO" UniqueName="EXPECT_TR_TO"
                                            DataFormatString="{0:dd/MM/yyyy}" />
                                        <tlk:GridNumericColumn HeaderText="<%$ Translate: Số học viên %>" DataField="STUDENT_NUMBER" 
                                            SortExpression="STUDENT_NUMBER" UniqueName="STUDENT_NUMBER"/>
                                        <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí dự kiến %>" DataField="COST_TOTAL_USD" 
                                            SortExpression="COST_TOTAL_USD" UniqueName="COST_TOTAL_USD"/>
                                    </Columns>
                                    <HeaderStyle Width="50px" />
                                </MasterTableView>
                            </tlk:RadGrid>
                        </tlk:RadPane>
                        <tlk:RadPane ID="RadPane8" runat="server" MinHeight="50" Height="50px" Scrolling="None">
                            <div style="margin: 20px 10px 10px 10px; text-align: right; vertical-align: middle">
                                <asp:HiddenField ID="hidSelected" runat="server" />
                                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                                    Font-Bold="true" CausesValidation="false">
                                </tlk:RadButton>
                                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                                    Font-Bold="true" CausesValidation="false">
                                </tlk:RadButton>
                            </div>
                        </tlk:RadPane>
                    <%--</tlk:RadSplitter>
                </tlk:RadPane>--%>
            </tlk:RadSplitter>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">
        function popupclose(sender, args) {
            $get("<%= btnNO.ClientId %>").click();
        }
    </script>
</tlk:RadScriptBlock>
