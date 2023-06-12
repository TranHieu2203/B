<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtPortalInsurance.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtPortalInsurance" %>
<%@ Import Namespace="Common" %>

<div style="border: 1px solid silver;">
    <div style="margin-top: 10px">
        <span style="color: Teal; font-size: small; font-weight: bold; padding-left: 5px">
            ::::: <%# Translate("Thông tin bảo hiểm")%>
        </span>
        <hr />
        <tlk:RadGrid ID="rgGrid" runat="server" AllowFilteringByColumn="true" Height="170px" Visible="false">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Số sổ BHXH")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSOCIAL_NUMBER" runat="server" Enabled="false" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Lương đóng BHXH")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox MinValue="0" Value="0" ReadOnly="true" ID="txtSALARY" Enabled="false" runat="server">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Tham gia BHXH")%>
                </td>
                <td>
                    <tlk:RadMonthYearPicker runat="server" ID="txtSI_FROM_MONTH" ReadOnly="true" DateInput-DisplayDateFormat="MM/yyyy" TabIndex="6" Enabled="false" Culture="en-US">
                            </tlk:RadMonthYearPicker>
                </td>
            </tr>
             <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Số thẻ BHYT")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtHEALTH_NUMBER" runat="server" ReadOnly="true" Enabled="false" TabIndex="19">
                            </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Nơi đóng bảo hiểm")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtRegisterPlace" runat="server" Enabled="false" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Tham gia BHYT")%>
                </td>
                <td>
                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtHI_FROM_MONTH" ReadOnly="true" Enabled="false" Culture="en-US"
                                TabIndex="17">
                            </tlk:RadMonthYearPicker>
                </td>
            </tr>
              <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Nơi KCB")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="ddlHEALTH_AREA_INS_ID" runat="server" ReadOnly="true" Enabled="false" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Tham gia BHTN")%>
                </td>
                <td >
                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtBHTNLD_BNN_FROM_MONTH" ReadOnly="true" Enabled="false" Culture="en-US"
                                TabIndex="27">
                            </tlk:RadMonthYearPicker>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 20px">
         <span style="color: Teal; font-size: small; font-weight: bold; padding-left: 5px">
            ::::: <%# Translate("Biến động bảo hiểm")%>
        </span>
        <hr />
        <tlk:RadGrid ID="rgGrid2" runat="server" AllowFilteringByColumn="true" Height="300px">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </div>
</div>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT" || item.get_commandName() == "IMPORT") {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
