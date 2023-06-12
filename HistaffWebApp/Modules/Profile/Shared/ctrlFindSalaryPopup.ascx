<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindSalaryPopup.ascx.vb"
    Inherits="Profile.ctrlFindSalaryPopup" %>
    <link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
   
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="Close" VisibleStatusbar="false"
    Height="400px" Width="600px" Modal="true" Title="<%$ Translate: Quá trình lương %>"
   OnClientClose="popupclose" VisibleOnPageLoad ="true" 
    ViewStateMode="Enabled">
    <ContentTemplate>
    <asp:HiddenField ID="hidEmployeeID" runat="server" />
    <asp:TextBox ID="txtEmployeeID" runat="server" style="display:none;"></asp:TextBox>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <p>
                
                <tlk:RadGrid  ID="rgSalary" runat="server" AutoGenerateColumns="False" AllowPaging="False" Height="300px">
                    <MasterTableView DataKeyNames="ID,DECISION_NO,EFFECT_DATE,EXPIRE_DATE,SAL_GROUP_NAME,SAL_LEVEL_NAME,SAL_RANK_NAME,SAL_BASIC,PERCENT_SALARY">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID %>" DataField="ID"
                                                 SortExpression="ID" UniqueName="ID"  Visible="false"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                                SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />

                            <tlk:GridBoundColumn HeaderText="" DataField="SAL_TYPE_NAME" SortExpression="SAL_TYPE_NAME" UniqueName="SAL_TYPE_NAME" />
                            <tlk:GridNumericColumn HeaderText="" DataField="TAX_TABLE_Name" SortExpression="TAX_TABLE_Name" UniqueName="TAX_TABLE_Name"/>
                            <tlk:GridNumericColumn HeaderText="Lương cơ bản" DataField="SALARY_BHXH" SortExpression="SALARY_BHXH" UniqueName="SALARY_BHXH" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="Lương thỏa thuận" DataField="SAL_BASIC" SortExpression="SAL_BASIC" UniqueName="SAL_BASIC" DataFormatString="{0:n0}" />

                            <tlk:GridNumericColumn HeaderText="" DataField="GAS_SAL" SortExpression="GAS_SAL" UniqueName="GAS_SAL" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="PHONE_SAL" SortExpression="PHONE_SAL" UniqueName="PHONE_SAL" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="ADDITIONAL_SAL" SortExpression="ADDITIONAL_SAL" UniqueName="ADDITIONAL_SAL" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="OTHERSALARY1" SortExpression="OTHERSALARY1" UniqueName="OTHERSALARY1" DataFormatString="{0:n0}" />
                        
                            <tlk:GridNumericColumn HeaderText="" DataField="PC1" SortExpression="PC1" UniqueName="PC1" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="PC2" SortExpression="PC2" UniqueName="PC2" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="PC3" SortExpression="PC3" UniqueName="PC3" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="PC4" SortExpression="PC4" UniqueName="PC4" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="PC5" SortExpression="PC5" UniqueName="PC5" DataFormatString="{0:n0}" />

                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </p>
            <div style="margin: 0px 10px 10px 10px; text-align: right;">
                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
            </div>
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
