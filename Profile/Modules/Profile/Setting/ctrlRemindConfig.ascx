<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRemindConfig.ascx.vb"
    Inherits="Profile.ctrlRemindConfig" %>

<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:Panel ID="radProbation" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkProbation" runat="server" Text="<%$ Translate: Nhân viên hết hạn hợp đồng thử việc%>"
                        onclick="CheckChangeProbation(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtProbation" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvalProbation" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn hợp đồng thử việc. %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn hợp đồng thử việc. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radCONTRACT" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkCONTRACT" runat="server" Text="<%$ Translate: Nhân viên hết hạn hợp đồng %>"
                        onclick="CheckChangeContract(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtCONTRACT" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                                    <asp:CustomValidator ID="cvalCONTRACT" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn hợp đồng. %>"
                                        ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn hợp đồng. %>">
                                    </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radBIRTHDAY" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkBIRTHDAY" runat="server" Text="<%$ Translate: Nhân viên sắp đến sinh nhật %>"
                        onclick="CheckChangeBirthday(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtBIRTHDAY" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                                    <asp:CustomValidator ID="cvalBIRTHDAY" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên sắp đến sinh nhật. %>"
                                        ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên sắp đến sinh nhật. %>">
                                    </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radVISA" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkVisa" runat="server" Text="<%$ Translate: Nhân viên hết hạn Visa%>"
                        onclick="CheckChangeVisa(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtVISA" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="valnmVISA" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn visa, hộ chiếu. %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn visa, hộ chiếu. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radWORKING" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkWorking" runat="server" Text="<%$ Translate: Nhân viên hết hạn tờ trình %>"
                        onclick="CheckChangeWorking(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtWORKING" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radLABOR" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkLabor" runat="server" Text="<%$ Translate: Nhân viên hết hạn giấy phép lao động  %>"
                        onclick="CheckChangeLabor(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtLABOR" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radCERTIFICATE" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkCertificate" runat="server" Text="<%$ Translate: Nhân viên hết hạn chứng chỉ lao động  %>"
                        onclick="CheckChangeCertificate(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtCERTIFICATE" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radTERMINATE" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkTerminate" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc trong tháng %>"
                        onclick="CheckChangeTerminate(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtTERMINATE" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radTERMINATEDEBT" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkTerminateDebt" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc chưa bàn giao hoặc còn thiếu công nợ %>"
                        onclick="CheckChangeTerminateDebt(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtTERMINATEDEBT" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radNOPAPER" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkNoPaper" runat="server" Text="<%$ Translate: Nhân viên chưa nộp đủ giấy tờ khi tiếp nhận %>" 
                        onclick="CheckChangeNoPaper(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo sau")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtNOPAPER" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radApprove" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkApprove" runat="server" Text="<%$ Translate: Nhân viên đến hạn bổ nhiệm lại chức vụ%>"
                        onclick="CheckChangeApprove(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtApprove" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvalApprove" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên đến hạn bổ nhiệm lại chức vụ . %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên đến hạn bổ nhiệm lại chức vụ. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radApproveHDLD" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkApproveHDLD" runat="server" Text="<%$ Translate: Nhân viên đến hạn ký lại HĐLĐ%>"
                        onclick="CheckChangeApproveHDLD(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtApproveHDLD" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvalApproveHDLD" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên đến hạn ký lại HĐLĐ . %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên đến hạn ký lại HĐLĐ. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radApproveTHHD" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkApproveTHHD" runat="server" Text="<%$ Translate: Nhân viên hết hạn tạm hoãn HĐ. %>"
                        onclick="CheckChangeApproveTHHD(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtApproveTHHD" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvalApproveTHHD" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn tạm hoãn HĐ. %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên hết hạn tạm hoãn HĐ. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radMaterniti" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkMaterniti" runat="server" Text="<%$ Translate: Nhân viên nghỉ thai sản đi làm lại. %>"
                        onclick="CheckChangeMaterniti(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtMaterniti" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvalMaterniti" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên nghỉ thai sản đi làm lại. %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên nghỉ thai sản đi làm lại. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radRetirement" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkRetirement" runat="server" Text="<%$ Translate: Nhân viên đến tuổi nghỉ hưu. %>"
                        onclick="CheckChangeRetirement(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtRetirement" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvalRetirement" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên đến tuổi nghỉ hưu. %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên đến tuổi nghỉ hưu. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radNoneSalary" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkNoneSalary" runat="server" Text="<%$ Translate: Nhân viên nghỉ không lương đi làm lại. %>"
                        onclick="CheckChangeNoneSalary(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtNoneSalary" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvalNoneSalary" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên nghỉ không lương đi làm lại. %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên nghỉ không lương đi làm lại. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>

        

        <%-------------------------%>
        <asp:Panel ID="radConcurrently" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkConcurrently" runat="server" Text="<%$ Translate: Nhân viên sắp hết hiệu lực kiêm nhiệm %>"
                        onclick="CheckConcurrently(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtConcurrently" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                           <asp:CustomValidator ID="cvalConcurrently" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên sắp hết hiệu lực kiêm nhiệm. %>"
                               ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên sắp hết hiệu lực kiêm nhiệm. %>">
                           </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%-------------------------------%>

        <%-------------------------%>
        <asp:Panel ID="radEmpDtlFamily" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkEmpDtlFamily" runat="server" Text="<%$ Translate: Nhân viên có người thân sắp hết hạn giảm trừ gia cảnh %>"
                        onclick="CheckEmpDtlFamily(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtEmpDtlFamily" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                           <asp:CustomValidator ID="cvalEmpDtlFamily" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên có người thân sắp hết hạn giảm trừ gia cảnh. %>"
                               ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên có người thân sắp hết hạn giảm trừ gia cảnh. %>">
                           </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%-------------------------------%>

       <%-------------------------%>
        <asp:Panel ID="Panel4" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkInsArising" runat="server" Text="<%$ Translate: Danh sách biến động bảo hiểm chưa được xử lý khai báo %>"
                        />
                </legend>
                <table class="table-form">
                    <tr>
                        <%--<td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnInsArising" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                           <asp:CustomValidator ID="cvalInsArising" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước Danh sách biến động bảo hiểm chưa được xử lý khai báo. %>"
                               ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước Danh sách biến động bảo hiểm chưa được xử lý khai báo. %>">
                           </asp:CustomValidator>
                        </td>--%>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%-------------------------------%>
        <%-------------------------%>
        <asp:Panel ID="Panel3" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkExpDiscipline" runat="server" Text="<%$ Translate: Nhân viên hết thời hạn kỷ luật %>"
                        onclick="CheckExpDiscipline(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnExpDiscipline" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                           <asp:CustomValidator ID="cvalExpDiscipline" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước Nhân viên hết thời hạn kỷ luật. %>"
                               ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước Nhân viên hết thời hạn kỷ luật. %>">
                           </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%-------------------------------%>

        
         <%-------------------------%>
        <asp:Panel ID="Panel1" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkOverRegDate" runat="server" Text="<%$ Translate: Nhân viên đăng ký nghỉ không lương >= 14 ngày %>" />
                </legend>
                <table class="table-form">
                    <tr>
                        <%-- <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="RadNumericTextBox1" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            (<%# Translate("ngày")%>)
                           <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên có người thân sắp hết hạn giảm trừ gia cảnh. %>"
                                        ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên có người thân sắp hết hạn giảm trừ gia cảnh. %>">
                            </asp:CustomValidator>
                        </td>--%>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%-------------------------------%>
        <%-------------------------%>
        <asp:Panel ID="Panel5" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkSign" runat="server" Text="<%$ Translate:  Danh sách nhân viên bán hàng chưa ghi nhận ký quỹ %>" />
                </legend>
                <table class="table-form">
                </table>
            </fieldset>
        </asp:Panel>
        <%-------------------------------%>

        <%-------------------------%>
        <asp:Panel ID="Panel6" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkBHYT" runat="server" Text="<%$ Translate: Danh sách nhân viên chưa nhận thẻ BHYT %>" />
                </legend>
                <table class="table-form">
                </table>
            </fieldset>
        </asp:Panel>
        <%-------------------------------%>
        <%-------------------------------%>
        <asp:Panel ID="Panel7" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkAge18" runat="server" Text="<%$ Translate: Danh sách người thân sắp 18 tuổi %>"
                        onclick="CheckAge18(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnAge18" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                           <asp:CustomValidator ID="cvalAge18" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước Danh sách người thân sắp 18 tuổi. %>"
                               ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước Danh sách người thân sắp 18 tuổi. %>">
                           </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%-------------------------------%>
         <%-------------------------%>
        <asp:Panel ID="radBIRTHDAY_LD" runat="server">
            <fieldset style="display:none">
                <legend>
                    <asp:CheckBox ID="chkBIRTHDAY_LD" runat="server" Text="<%$ Translate: Sắp đến sinh nhật lãnh đạo %>"
                        onclick="CheckChangeBirthday_LD(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtBIRTHDAY_LD" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                           <asp:CustomValidator ID="cvalBIRTHDAY_LD" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước sắp đến sinh nhật lãnh đạo. %>"
                               ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước sắp đến sinh nhật lãnh đạo. %>">
                           </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%-------------------------------%>
        <%--------------------------------------%>
        <asp:Panel ID="radExpiredCertificate" runat="server">
            <fieldset style="display:none">
                <legend>
                    <asp:CheckBox ID="chkExpiredCertificate" runat="server" Text="<%$ Translate: Nhân viên sắp hết hạn chứng chỉ. %>"
                        onclick="CheckChangExpiredCertificate(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtExpiredCertificate" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                            <asp:CustomValidator ID="cvaExpiredCertificate" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên sắp hết hạn chứng chỉ. %>"
                                ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước nhân viên sắp hết hạn chứng chỉ. %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%-------------------------%>
        <asp:Panel ID="Panel2" runat="server">
            <fieldset style="display:none">
                <legend>
                    <asp:CheckBox ID="chkExpContrat" runat="server" Text="<%$ Translate: Nhân viên hết hạn hợp đồng %>"
                        onclick="CheckExpContrat(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnExpContrat" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                           <asp:CustomValidator ID="cvalExpContrat" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước Nhân viên hết hạn hợp đồng. %>"
                               ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước Nhân viên hết hạn hợp đồng. %>">
                           </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%-------------------------%>
        <%-----------------------------------%>
        <asp:Panel ID="radExpAuthority" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkExpAuthority" runat="server" Text="<%$ Translate: Nhân viên Ủy quyền sắp hết hạn %>"
                        onclick="CheckExpAuthority(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnExpAuthority" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                           <asp:CustomValidator ID="cvalExpAuthority" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước Nhân viên Ủy quyền sắp hết hạn. %>"
                               ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước Nhân viên Ủy quyền sắp hết hạn. %>">
                           </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>  
        <%-------------------------%>
        <%-----------------------------------%>
        <asp:Panel ID="radNoticePersonalIncomeTax" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkNoticePersonalIncomeTax" runat="server" Text="<%$ Translate: Thông báo quyết toán thuế thu nhập cá nhân %>"
                        onclick="CheckNoticePersonalIncomeTax(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnNoticePersonalIncomeTax" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                           <asp:CustomValidator ID="cvalNoticePersonalIncomeTax" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước Thông báo quyết toán thuế thu nhập cá nhân. %>"
                               ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước Thông báo quyết toán thuế thu nhập cá nhân. %>">
                           </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="radNoticeStock" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkNoticeStock" runat="server" Text="<%$ Translate: Nhân viên đến hạn thanh toán cổ phiếu %>"
                        onclick="CheckNoticeStock(this)" />
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb lbRemind">
                            <%# Translate("Thời gian báo trước")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnNoticeStock" runat="server" SkinID="Number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>(<%# Translate("ngày")%>)
                           <asp:CustomValidator ID="cvalNoticeStock" runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập thời gian báo trước Thông báo đến hạn thanh toán cổ phiếu. %>"
                               ToolTip="<%$ Translate: Bạn chưa nhập thời gian báo trước Thông báo đến hạn thanh toán cổ phiếu. %>">
                           </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel> 
        <asp:Panel ID="radApproveHSNV" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkApproveHSNV" runat="server" Text="<%$ Translate: Phê duyệt HSNV %>" />
                </legend>
                <table class="table-form">
                </table>
            </fieldset>
        </asp:Panel>         
        <asp:Panel ID="radApproveWorkBefore" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkApproveWorkBefore" runat="server" Text="<%$ Translate: Phê duyệt Kinh nghiệm làm việc %>" />
                </legend>
                <table class="table-form">
                </table>
            </fieldset>
        </asp:Panel>  
        <asp:Panel ID="radApproveFamily" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkApproveFamily" runat="server" Text="<%$ Translate: Phê duyệt thông tin nhân thân %>" />
                </legend>
                <table class="table-form">
                </table>
            </fieldset>
        </asp:Panel>  
        <asp:Panel ID="radApproveCertificate" runat="server" CssClass="Pane">
            <fieldset>
                <legend>
                    <asp:CheckBox ID="chkApproveCertificate" runat="server" Text="<%$ Translate: Phê duyệt bằng cấp chứng chỉ %>" />
                </legend>
                <table class="table-form">
                </table>
            </fieldset>
        </asp:Panel>    
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function CheckChangeContract(chk) {
            if (chk.checked) {
                $find("<%=rntxtCONTRACT.ClientID %>").enable();
                $find("<%=rntxtCONTRACT.ClientID %>").focus();
            } else {
                $find("<%=rntxtCONTRACT.ClientID %>").clear();
                $find("<%=rntxtCONTRACT.ClientID %>").disable();
            }
        }
        function CheckChangeBirthday(chk) {
            if (chk.checked) {
                $find("<%=rntxtBIRTHDAY.ClientID %>").enable();
                $find("<%=rntxtBIRTHDAY.ClientID %>").focus();
            } else {
                $find("<%=rntxtBIRTHDAY.ClientID %>").clear();
                $find("<%=rntxtBIRTHDAY.ClientID %>").disable();
            }
        }


        function CheckChangeVisa(chk) {
            if (chk.checked) {
                $find("<%=rntxtVISA.ClientID %>").enable();
                $find("<%=rntxtVISA.ClientID %>").focus();
            } else {
                $find("<%=rntxtVISA.ClientID %>").clear();
                $find("<%=rntxtVISA.ClientID %>").disable();
            }
        }
        function CheckChangeWorking(chk) {
            if (chk.checked) {
                $find("<%=rntxtWORKING.ClientID %>").enable();
                $find("<%=rntxtWORKING.ClientID %>").focus();
            } else {
                $find("<%=rntxtWORKING.ClientID %>").clear();
                $find("<%=rntxtWORKING.ClientID %>").disable();
            }
        }
        function CheckChangeTerminate(chk) {
            if (chk.checked) {
                $find("<%=rntxtTERMINATE.ClientID %>").enable();
                $find("<%=rntxtTERMINATE.ClientID %>").focus();
            } else {
                $find("<%=rntxtTERMINATE.ClientID %>").clear();
                $find("<%=rntxtTERMINATE.ClientID %>").disable();
            }
        }
        function CheckChangeTerminateDebt(chk) {
            if (chk.checked) {
                $find("<%=rntxtTERMINATEDEBT.ClientID %>").enable();
                $find("<%=rntxtTERMINATEDEBT.ClientID %>").focus();
            } else {
                $find("<%=rntxtTERMINATEDEBT.ClientID %>").clear();
                $find("<%=rntxtTERMINATEDEBT.ClientID %>").disable();
            }
        }

        function CheckChangeNoPaper(chk) {
            if (chk.checked) {
                $find("<%=rntxtNOPAPER.ClientID %>").enable();
                $find("<%=rntxtNOPAPER.ClientID %>").focus();
            } else {
                $find("<%=rntxtNOPAPER.ClientID %>").clear();
                $find("<%=rntxtNOPAPER.ClientID %>").disable();
            }
        }

        function CheckChangeProbation(chk) {
            if (chk.checked) {
                $find("<%=rntxtProbation.ClientID %>").enable();
                $find("<%=rntxtProbation.ClientID %>").focus();
            } else {
                $find("<%=rntxtProbation.ClientID %>").clear();
                $find("<%=rntxtProbation.ClientID %>").disable();
            }
        }

        function CheckChangeCertificate(chk) {
            if (chk.checked) {
                $find("<%=rntxtCERTIFICATE.ClientID %>").enable();
                $find("<%=rntxtCERTIFICATE.ClientID %>").focus();
            } else {
                $find("<%=rntxtCERTIFICATE.ClientID %>").clear();
                $find("<%=rntxtCERTIFICATE.ClientID %>").disable();
            }
        }

        function CheckChangeLabor(chk) {
            if (chk.checked) {
                $find("<%=rntxtLABOR.ClientID %>").enable();
                $find("<%=rntxtLABOR.ClientID %>").focus();
            } else {
                $find("<%=rntxtLABOR.ClientID %>").clear();
                $find("<%=rntxtLABOR.ClientID %>").disable();
            }
        }

        function CheckChangeApprove(chk) {
            if (chk.checked) {
                $find("<%=rntxtApprove.ClientID %>").enable();
                $find("<%=rntxtApprove.ClientID %>").focus();
            } else {
                $find("<%=rntxtApprove.ClientID %>").clear();
                $find("<%=rntxtApprove.ClientID %>").disable();
            }
        }
        function CheckChangeApproveHDLD(chk) {
            if (chk.checked) {
                $find("<%=rntxtApproveHDLD.ClientID %>").enable();
                $find("<%=rntxtApproveHDLD.ClientID %>").focus();
            } else {
                $find("<%=rntxtApproveHDLD.ClientID %>").clear();
                $find("<%=rntxtApproveHDLD.ClientID %>").disable();
            }
        }

        function CheckChangeApproveTHHD(chk) {
            if (chk.checked) {
                $find("<%=rntxtApproveTHHD.ClientID %>").enable();
                $find("<%=rntxtApproveTHHD.ClientID %>").focus();
            } else {
                $find("<%=rntxtApproveTHHD.ClientID %>").clear();
                $find("<%=rntxtApproveTHHD.ClientID %>").disable();
            }
        }
        function CheckChangeMaterniti(chk) {
            if (chk.checked) {
                $find("<%=rntxtMaterniti.ClientID %>").enable();
                $find("<%=rntxtMaterniti.ClientID %>").focus();
            } else {
                $find("<%=rntxtMaterniti.ClientID %>").clear();
                $find("<%=rntxtMaterniti.ClientID %>").disable();
            }
        }
        function CheckChangeRetirement(chk) {
            if (chk.checked) {
                $find("<%=rntxtRetirement.ClientID %>").enable();
                $find("<%=rntxtRetirement.ClientID %>").focus();
            } else {
                $find("<%=rntxtRetirement.ClientID %>").clear();
                $find("<%=rntxtRetirement.ClientID %>").disable();
            }
        }
        function CheckChangeNoneSalary(chk) {
            if (chk.checked) {
                $find("<%=rntxtNoneSalary.ClientID %>").enable();
                $find("<%=rntxtNoneSalary.ClientID %>").focus();
            } else {
                $find("<%=rntxtNoneSalary.ClientID %>").clear();
                $find("<%=rntxtNoneSalary.ClientID %>").disable();
            }
        }
        function CheckChangExpiredCertificate(chk) {
            if (chk.checked) {
                $find("<%=rntxtExpiredCertificate.ClientID %>").enable();
                $find("<%=rntxtExpiredCertificate.ClientID %>").focus();
            } else {
                $find("<%=rntxtExpiredCertificate.ClientID %>").clear();
                $find("<%=rntxtExpiredCertificate.ClientID %>").disable();
            }
        }

        function CheckConcurrently(chk) {
            if (chk.checked) {
                $find("<%=rntxtConcurrently.ClientID %>").enable();
                $find("<%=rntxtConcurrently.ClientID %>").focus();
            } else {
                $find("<%=rntxtConcurrently.ClientID %>").clear();
                $find("<%=rntxtConcurrently.ClientID %>").disable();
            }
        }
        function CheckEmpDtlFamily(chk) {
            if (chk.checked) {
                $find("<%=rntxtEmpDtlFamily.ClientID %>").enable();
                $find("<%=rntxtEmpDtlFamily.ClientID %>").focus();
            } else {
                $find("<%=rntxtEmpDtlFamily.ClientID %>").clear();
                $find("<%=rntxtEmpDtlFamily.ClientID %>").disable();
            }
        }

        function CheckExpContrat(chk) {
            if (chk.checked) {
                $find("<%=rnExpContrat.ClientID%>").enable();
                $find("<%=rnExpContrat.ClientID%>").focus();
            } else {
                $find("<%=rnExpContrat.ClientID%>").clear();
                $find("<%=rnExpContrat.ClientID%>").disable();
            }
        }

        function CheckExpDiscipline(chk) {
            if (chk.checked) {
                $find("<%=rnExpDiscipline.ClientID%>").enable();
                $find("<%=rnExpDiscipline.ClientID%>").focus();
            } else {
                $find("<%=rnExpDiscipline.ClientID%>").clear();
                $find("<%=rnExpDiscipline.ClientID%>").disable();
            }
        }

        function CheckAge18(chk) {
            if (chk.checked) {
                $find("<%=rnAge18.ClientID %>").enable();
                $find("<%=rnAge18.ClientID %>").focus();
            } else {
                $find("<%=rnAge18.ClientID %>").clear();
                $find("<%=rnAge18.ClientID %>").disable();
            }
        }

        function CheckExpAuthority(chk) {
            if (chk.checked) {
                $find("<%=rnExpAuthority.ClientID %>").enable();
                $find("<%=rnExpAuthority.ClientID %>").focus();
            } else {
                $find("<%=rnExpAuthority.ClientID %>").clear();
                $find("<%=rnExpAuthority.ClientID %>").disable();
            }
        }

        function CheckNoticePersonalIncomeTax(chk) {
            if (chk.checked) {
                $find("<%=rnNoticePersonalIncomeTax.ClientID %>").enable();
                $find("<%=rnNoticePersonalIncomeTax.ClientID %>").focus();
            } else {
                $find("<%=rnNoticePersonalIncomeTax.ClientID %>").clear();
                $find("<%=rnNoticePersonalIncomeTax.ClientID %>").disable();
            }
        }

        function CheckNoticeStock(chk) {
            if (chk.checked) {
                $find("<%=rnNoticeStock.ClientID %>").enable();
                $find("<%=rnNoticeStock.ClientID %>").focus();
            } else {
                $find("<%=rnNoticeStock.ClientID %>").clear();
                $find("<%=rnNoticeStock.ClientID %>").disable();
            }
        }

    </script>
    <style>
        .Pane
        {
            width: 33%;
            float: left;
            height:80px;
        }
    </style>
</tlk:RadCodeBlock>
