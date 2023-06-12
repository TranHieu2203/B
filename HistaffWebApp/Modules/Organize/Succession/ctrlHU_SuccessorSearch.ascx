<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_SuccessorSearch.ascx.vb"
    Inherits="Profile.ctrlHU_SuccessorSearch" %>
<style type="text/css">
    .width_100_percent
    {
        width: 100% !important;
    }
    .height_95_percent
    {
        height: 95% !important;
    }
    .float_right
    {
        float: right;
    }
    .margin_top_-2px
    {
        margin-top: -2px;
    }
    .custom_txt
    {
        height: 24px;
        box-sizing: border-box;
    }
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_SuccessorSearch_RadPane4
    {
        min-height: 45px;
        max-height: 530px;
    }
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_SuccessorSearch_RadPane5
    {
        max-height: 475px;
    }
    .hide
    {
        visibility: hidden;
    }
    .show
    {
        visibility: visible;
    }    
</style>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server" MinWidth="100" Width="280px" Height="100%">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="RadPane2" runat="server" Height="100%">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" CssClass="height_95_percent"
            Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px">
                <tlk:RadToolBar ID="tbarOrgFunctions" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="7%" MinHeight="45">
                <asp:HiddenField runat="server" ID="hdnIsPostback" />
                <div data-value="1" style="padding-top: 10px">
                    <div id="div1" runat="server" style="display: inline-block; width: 70%; vertical-align: middle;">
                        <asp:CheckBox data-value="1" runat="server" ID="chkExperience" Checked="true" Style="display: inline-block;
                            vertical-align: middle; width: 4%; text-align: right" />
                        <label id="lblExperience" style="display: inline-block; width: 20%; vertical-align: middle;">
                            <%# Translate("Kinh nghiệm vị trí:")%></label>
                        <tlk:RadComboBox runat="server" ID="cboPosition" Width="45%">
                        </tlk:RadComboBox>
                        <tlk:RadComboBox runat="server" ID="cboComparisonOperators1" Width="45px">
                            <Items>
                                <tlk:RadComboBoxItem runat="server" Selected="true" Text="<" Value="<" />
                                <tlk:RadComboBoxItem runat="server" Text="&#8804;" Value="<=" />
                                <tlk:RadComboBoxItem runat="server" Text="=" Value="=" />
                                <tlk:RadComboBoxItem runat="server" Text=">" Value=">" />
                                <tlk:RadComboBoxItem runat="server" Text="&#8805;" Value=">="  />
                            </Items>
                        </tlk:RadComboBox>
                        <tlk:RadNumericTextBox CssClass="custom_txt" runat="server" ID="txtEperience" Width="59px"
                            MaxLength="6" />
                    </div>
                    <div style="display: inline-block; width: 20%; vertical-align: middle;">
                        <tlk:RadComboBox runat="server" ID="cboFilterList" CausesValidation="false" OnClientSelectedIndexChanged="cboFilterChanged" EmptyMessage = "Lựa chọn tiêu chí">
                            <Items>
                                <tlk:RadComboBoxItem runat="server" Text="" Value="0" />
                                <tlk:RadComboBoxItem runat="server" Text="Kinh nghiệm vị trí" Value="1" />
                                <tlk:RadComboBoxItem runat="server" Text="Đã từng công tác tại vị trí" Value="2" />
                                <tlk:RadComboBoxItem runat="server" Text="Tuổi" Value="4" />
                                <tlk:RadComboBoxItem runat="server" Text="Giới tính" Value="5" />
                                <tlk:RadComboBoxItem runat="server" Text="Thâm niên" Value="6" />
                                <tlk:RadComboBoxItem runat="server" Text="Cấp nhân sự" Value="7" />
                                <tlk:RadComboBoxItem runat="server" Text="Trình độ học vấn" Value="8" />
                                <tlk:RadComboBoxItem runat="server" Text="Các loại bằng cấp, chứng chỉ" Value="9" />
                                <tlk:RadComboBoxItem runat="server" Text="Khóa đào tạo" Value="10" />
                                <tlk:RadComboBoxItem runat="server" Text="Kết quả đánh giá" Value="11" />
                                <tlk:RadComboBoxItem runat="server" Text="Năng lực" Value="12" />
                            </Items>
                        </tlk:RadComboBox>
                    </div>
                </div>
                <div id="div2" data-value="2" runat="server" style="padding-top: 10px">
                    <div style="display: inline-block; width: 70%; vertical-align: middle;">
                        <asp:CheckBox data-value="2" runat="server" ID="chkPositions" Style="display: inline-block;
                            vertical-align: middle; width: 4%; text-align: right" />
                        <label id="lblPosition" style="display: inline-block; width: 20%; vertical-align: middle;">
                            <%# Translate("Đã từng công tác tại vị trí:")%></label>
                        <tlk:RadComboBox runat="server" ID="cboPositions" Width="59.7%" CheckBoxes="true"
                            EnableCheckAllItemsCheckBox="true">
                        </tlk:RadComboBox>
                    </div>
                </div>
                <div id="div4" data-value="4" runat="server" style="padding-top: 10px">
                    <div style="display: inline-block; width: 70%; vertical-align: middle;">
                        <asp:CheckBox data-value="4" runat="server" ID="chkAge" Style="display: inline-block;
                            vertical-align: middle; width: 4%; text-align: right" />
                        <label id="Label2" style="display: inline-block; width: 20%; vertical-align: middle;">
                            <%# Translate("Tuổi:")%></label>
                        <tlk:RadComboBox runat="server" ID="cboComparisonOperators2" Width="45px">
                            <Items>
                                <tlk:RadComboBoxItem runat="server" Selected="true" Text="<" Value="<" />
                                <tlk:RadComboBoxItem runat="server" Text="&#8804;" Value="<=" />
                                <tlk:RadComboBoxItem runat="server" Text="=" Value="=" />
                                <tlk:RadComboBoxItem runat="server" Text=">" Value=">" />
                                <tlk:RadComboBoxItem runat="server" Text="&#8805;" Value=">="  />
                            </Items>
                        </tlk:RadComboBox>
                        <tlk:RadNumericTextBox CssClass="custom_txt" runat="server" ID="txtAge" Width="60px"
                            MaxLength="6" />
                    </div>
                </div>
                <div id="div5" data-value="5" runat="server" style="padding-top: 10px">
                    <div style="display: inline-block; width: 70%; vertical-align: middle;">
                        <asp:CheckBox data-value="5" runat="server" ID="chkGender" Style="display: inline-block;
                            vertical-align: middle; width: 4%; text-align: right" />
                        <label id="Label3" style="display: inline-block; width: 20%; vertical-align: middle;">
                            <%# Translate("Giới tính:")%></label>
                        <tlk:RadComboBox runat="server" ID="cboGender" Width="59.7%" CheckBoxes="true" EnableCheckAllItemsCheckBox="true">
                        </tlk:RadComboBox>
                    </div>
                </div>
                <div id="div6" data-value="6" runat="server" style="padding-top: 10px">
                    <div style="display: inline-block; width: 70%; vertical-align: middle;">
                        <asp:CheckBox data-value="6" runat="server" ID="chkSeniority" Style="display: inline-block;
                            vertical-align: middle; width: 4%; text-align: right" />
                        <label id="Label4" style="display: inline-block; width: 20%; vertical-align: middle;">
                            <%# Translate("Thâm niên:")%></label>
                        <tlk:RadComboBox runat="server" ID="cboComparisonOperators3" Width="45px">
                            <Items>
                                <tlk:RadComboBoxItem runat="server" Selected="true" Text="<" Value="<" />
                                <tlk:RadComboBoxItem runat="server" Text="&#8804;" Value="<=" />
                                <tlk:RadComboBoxItem runat="server" Text="=" Value="=" />
                                <tlk:RadComboBoxItem runat="server" Text=">" Value=">" />
                                <tlk:RadComboBoxItem runat="server" Text="&#8805;" Value=">="  />
                            </Items>
                        </tlk:RadComboBox>
                        <tlk:RadNumericTextBox CssClass="custom_txt" runat="server" ID="txtSeniority" Width="60px"
                            MaxLength="6" />
                    </div>
                </div>
                <div id="div7" data-value="7" runat="server" style="padding-top: 10px">
                    <div style="display: inline-block; width: 70%; vertical-align: middle;">
                        <asp:CheckBox data-value="7" runat="server" ID="chkStaffRank" Style="display: inline-block;
                            vertical-align: middle; width: 4%; text-align: right" />
                        <label id="Label5" style="display: inline-block; width: 20%; vertical-align: middle;">
                            <%# Translate("Cấp nhân sự:")%></label>
                        <tlk:RadComboBox runat="server" ID="cboStaffRank" Width="59.7%" CheckBoxes="true"
                            EnableCheckAllItemsCheckBox="true">
                        </tlk:RadComboBox>
                    </div>
                </div>
                <div id="div8" data-value="8" runat="server" style="padding-top: 10px">
                    <div style="display: inline-block; width: 70%; vertical-align: middle;">
                        <asp:CheckBox data-value="8" runat="server" ID="chkAcademicLevel"
                            Style="display: inline-block; vertical-align: middle; width: 4%; text-align: right" />
                        <label id="Label6" style="display: inline-block; width: 20%; vertical-align: middle;">
                            <%# Translate("Trình độ học vấn:")%></label>
                        <tlk:RadComboBox runat="server" ID="cboComparisonOperators4" Width="45px">
                            <Items>
                                <tlk:RadComboBoxItem runat="server" Selected="true" Text="<" Value="<" />
                                <tlk:RadComboBoxItem runat="server" Text="&#8804;" Value="<=" />
                                <tlk:RadComboBoxItem runat="server" Text="=" Value="=" />
                                <tlk:RadComboBoxItem runat="server" Text=">" Value=">" />
                                <tlk:RadComboBoxItem runat="server" Text="&#8805;" Value=">="  />
                            </Items>
                        </tlk:RadComboBox>
                        <tlk:RadComboBox runat="server" ID="cboAcademicLevel" Width="53.5%" CheckBoxes="true"
                            EnableCheckAllItemsCheckBox="true">
                        </tlk:RadComboBox>
                    </div>
                </div>
                <div id="div9" data-value="9" runat="server" style="padding-top: 10px">
                    <div style="display: inline-block; width: 70%; vertical-align: middle;">
                        <asp:CheckBox data-value="9" runat="server" ID="chkCertificate" Style="display: inline-block;
                            vertical-align: middle; width: 4%; text-align: right" />
                        <label id="Label7" style="display: inline-block; width: 20%; vertical-align: middle;">
                            <%# Translate("Bằng cấp, chứng chỉ:")%></label>
                        <tlk:RadComboBox runat="server" ID="cboCertificate" Width="59.7%" CheckBoxes="true"
                            EnableCheckAllItemsCheckBox="true">
                        </tlk:RadComboBox>
                    </div>
                </div>
                <div id="div10" runat="server" data-value="10" style="padding-top: 10px">
                    <div style="display: inline-block; width: 70%; vertical-align: middle;">
                        <asp:CheckBox data-value="10" runat="server" ID="chkCourses" Style="display: inline-block;
                            vertical-align: middle; width: 4%; text-align: right" />
                        <label id="Label8" style="display: inline-block; width: 20%; vertical-align: middle;">
                            <%# Translate("Khóa đào tạo:")%></label>
                        <tlk:RadComboBox runat="server" ID="cboCourses" Width="59.7%" CheckBoxes="true" EnableCheckAllItemsCheckBox="true">
                        </tlk:RadComboBox>
                    </div>
                </div>
                <div id="div11" runat="server" data-value="11" style="padding-top: 10px">
                    <div style="display: inline-block; width: 70%; vertical-align: middle;">
                        <asp:CheckBox data-value="11" runat="server" ID="chkEvaluation" Style="display: inline-block;
                            vertical-align: middle; width: 4%; text-align: right" />
                        <label id="Label9" style="display: inline-block; width: 20%; vertical-align: middle;">
                            <%# Translate("Kết quả đánh giá:")%></label>
                        <tlk:RadComboBox runat="server" ID="cboYear" Width="10%" CausesValidation="false"
                            AutoPostBack="true">
                        </tlk:RadComboBox>
                        <tlk:RadComboBox runat="server" ID="cboPeriod" Width="25%">
                        </tlk:RadComboBox>
                        <tlk:RadComboBox runat="server" ID="cboComparisonOperators5" Width="45px">
                            <Items>
                                <tlk:RadComboBoxItem runat="server" Selected="true" Text="<" Value="<" />
                                <tlk:RadComboBoxItem runat="server" Text="&#8804;" Value="<=" />
                                <tlk:RadComboBoxItem runat="server" Text="=" Value="=" />
                                <tlk:RadComboBoxItem runat="server" Text=">" Value=">" />
                                <tlk:RadComboBoxItem runat="server" Text="&#8805;" Value=">="  />
                            </Items>
                        </tlk:RadComboBox>
                        <tlk:RadComboBox runat="server" ID="cboGrade" Width="17.7%">
                        </tlk:RadComboBox>
                    </div>
                </div>
                <div id="div12" runat="server" data-value="12" style="padding-top: 10px">
                    <div style="display: inline-block; width: 70%; vertical-align: middle;">
                        <asp:CheckBox data-value="12" runat="server" ID="chkTalents" Style="display: inline-block;
                            vertical-align: middle; width: 4%; text-align: right" />
                        <label id="Label10" style="display: inline-block; width: 20%; vertical-align: middle;">
                            <%# Translate("Năng lực:")%></label>
                        <tlk:RadComboBox runat="server" ID="cboTalents" Width="59.8%" CausesValidation="false" OnClientSelectedIndexChanged="cboTalentsChanged">
                        </tlk:RadComboBox>
                    </div>
                </div>
                <div id="div13" runat="server" data-value="13" style="padding-top: 10px">
                    <div id="divTalent1" style="display: inline-block;width: 49.2%;text-align:right;vertical-align:top">
                        <asp:CheckBox data-value="13" runat="server" ID="chkTalent1" Style="vertical-align: -webkit-baseline-middle; width: 4%" />
                        <tlk:RadComboBox runat="server" ID="cboTalent1" Width="64.5%" />
                    </div>
                    <tlk:RadComboBox ID="cboComparisonOperatorsTalent1" runat="server" Width="45px">
                        <Items>
                            <tlk:RadComboBoxItem Selected="true" Text="<" Value="<" />
                            <tlk:RadComboBoxItem Text="&#8804;" Value="<=" />
                            <tlk:RadComboBoxItem Text="=" Value="=" />
                            <tlk:RadComboBoxItem Text=">" Value=">" />
                            <tlk:RadComboBoxItem Text="&#8805;" Value=">="  />
                        </Items>
                     </tlk:RadComboBox>
                     <tlk:RadNumericTextBox CssClass="custom_txt" ID="txtTalent1" Width="57px" MaxLength="6" runat="server" />
                </div>
                <div id="div14" runat="server" data-value="14" style="padding-top: 10px">
                    <div id="divTalent1" style="display: inline-block;width: 49.2%;text-align:right;vertical-align:top">
                        <asp:CheckBox data-value="14" runat="server" ID="chkTalent2" Style="vertical-align: -webkit-baseline-middle; width: 4%" />
                        <tlk:RadComboBox ID="cboRelativeOperatorsTalent2" runat="server" Width="13%">
                            <Items>
                                <tlk:RadComboBoxItem Selected="true" Text="And" Value="And" />
                                <tlk:RadComboBoxItem Text="Or" Value="Or" />
                            </Items>
                        </tlk:RadComboBox>
                        <tlk:RadComboBox runat="server" ID="cboTalent2" Width="50.9%" />
                    </div>
                    <tlk:RadComboBox ID="cboComparisonOperatorsTalent2" runat="server" Width="45px">
                        <Items>
                            <tlk:RadComboBoxItem Selected="true" Text="<" Value="<" />
                            <tlk:RadComboBoxItem Text="&#8804;" Value="<=" />
                            <tlk:RadComboBoxItem Text="=" Value="=" />
                            <tlk:RadComboBoxItem Text=">" Value=">" />
                            <tlk:RadComboBoxItem Text="&#8805;" Value=">="  />
                        </Items>
                     </tlk:RadComboBox>
                     <tlk:RadNumericTextBox CssClass="custom_txt" ID="txtTalent2" Width="57px" MaxLength="6" runat="server" />
                </div>
                <div id="div15" runat="server" data-value="15" style="padding-top: 10px">
                    <div id="divTalent3" style="display: inline-block;width: 49.2%;text-align:right;vertical-align:top">
                        <asp:CheckBox data-value="15" runat="server" ID="chkTalent3" Style="vertical-align: -webkit-baseline-middle; width: 4%" />
                        <tlk:RadComboBox ID="cboRelativeOperatorsTalent3" runat="server" Width="13%">
                            <Items>
                                <tlk:RadComboBoxItem Selected="true" Text="And" Value="And" />
                                <tlk:RadComboBoxItem Text="Or" Value="Or" />
                            </Items>
                        </tlk:RadComboBox>
                        <tlk:RadComboBox runat="server" ID="cboTalent3" Width="50.9%" />
                    </div>
                    <tlk:RadComboBox ID="cboComparisonOperatorsTalent3" runat="server" Width="45px">
                        <Items>
                            <tlk:RadComboBoxItem Selected="true" Text="<" Value="<" />
                            <tlk:RadComboBoxItem Text="&#8804;" Value="<=" />
                            <tlk:RadComboBoxItem Text="=" Value="=" />
                            <tlk:RadComboBoxItem Text=">" Value=">" />
                            <tlk:RadComboBoxItem Text="&#8805;" Value=">="  />
                        </Items>
                     </tlk:RadComboBox>
                     <tlk:RadNumericTextBox CssClass="custom_txt" ID="txtTalent3" Width="57px" MaxLength="6" runat="server" />
                </div>
                <div id="div16" runat="server" data-value="16" style="padding-top: 10px">
                    <div id="divTalent4" style="display: inline-block;width: 49.2%;text-align:right;vertical-align:top">
                        <asp:CheckBox data-value="16" runat="server" ID="chkTalent4" Style="vertical-align: -webkit-baseline-middle; width: 4%" />
                        <tlk:RadComboBox ID="cboRelativeOperatorsTalent4" runat="server" Width="13%">
                            <Items>
                                <tlk:RadComboBoxItem Selected="true" Text="And" Value="And" />
                                <tlk:RadComboBoxItem Text="Or" Value="Or" />
                            </Items>
                        </tlk:RadComboBox>
                        <tlk:RadComboBox runat="server" ID="cboTalent4" Width="50.9%" />
                    </div>
                    <tlk:RadComboBox ID="cboComparisonOperatorsTalent4" runat="server" Width="45px">
                        <Items>
                            <tlk:RadComboBoxItem Selected="true" Text="<" Value="<" />
                            <tlk:RadComboBoxItem Text="&#8804;" Value="<=" />
                            <tlk:RadComboBoxItem Text="=" Value="=" />
                            <tlk:RadComboBoxItem Text=">" Value=">" />
                            <tlk:RadComboBoxItem Text="&#8805;" Value=">="  />
                        </Items>
                     </tlk:RadComboBox>
                     <tlk:RadNumericTextBox CssClass="custom_txt" ID="txtTalent4" Width="57px" MaxLength="6" runat="server" />
                </div>
                <div id="div17" runat="server" data-value="17" style="padding-top: 10px">
                    <div id="divTalent5" style="display: inline-block;width: 49.2%;text-align:right;vertical-align:top">
                        <asp:CheckBox data-value="17" runat="server" ID="chkTalent5" Style="vertical-align: -webkit-baseline-middle; width: 4%" />
                        <tlk:RadComboBox ID="cboRelativeOperatorsTalent5" runat="server" Width="13%">
                            <Items>
                                <tlk:RadComboBoxItem Selected="true" Text="And" Value="And" />
                                <tlk:RadComboBoxItem Text="Or" Value="Or" />
                            </Items>
                        </tlk:RadComboBox>
                        <tlk:RadComboBox runat="server" ID="cboTalent5" Width="50.9%" />
                    </div>
                    <tlk:RadComboBox ID="cboComparisonOperatorsTalent5" runat="server" Width="45px">
                        <Items>
                            <tlk:RadComboBoxItem Selected="true" Text="<" Value="<" />
                            <tlk:RadComboBoxItem Text="&#8804;" Value="<=" />
                            <tlk:RadComboBoxItem Text="=" Value="=" />
                            <tlk:RadComboBoxItem Text=">" Value=">" />
                            <tlk:RadComboBoxItem Text="&#8805;" Value=">="  />
                        </Items>
                     </tlk:RadComboBox>
                     <tlk:RadNumericTextBox CssClass="custom_txt" ID="txtTalent5" Width="57px" MaxLength="6" runat="server" />
                </div>
                <div id="div18" runat="server" data-value="18" style="padding-top: 10px">
                    <div id="divTalent6" style="display: inline-block;width: 49.2%;text-align:right;vertical-align:top">
                        <asp:CheckBox data-value="18" runat="server" ID="chkTalent6" Style="vertical-align: -webkit-baseline-middle; width: 4%" />
                        <tlk:RadComboBox ID="cboRelativeOperatorsTalent6" runat="server" Width="13%">
                            <Items>
                                <tlk:RadComboBoxItem Selected="true" Text="And" Value="And" />
                                <tlk:RadComboBoxItem Text="Or" Value="Or" />
                            </Items>
                        </tlk:RadComboBox>
                        <tlk:RadComboBox runat="server" ID="cboTalent6" Width="50.9%" />
                    </div>
                    <tlk:RadComboBox ID="cboComparisonOperatorsTalent6" runat="server" Width="45px">
                        <Items>
                            <tlk:RadComboBoxItem Selected="true" Text="<" Value="<" />
                            <tlk:RadComboBoxItem Text="&#8804;" Value="<=" />
                            <tlk:RadComboBoxItem Text="=" Value="=" />
                            <tlk:RadComboBoxItem Text=">" Value=">" />
                            <tlk:RadComboBoxItem Text="&#8805;" Value=">="  />
                        </Items>
                     </tlk:RadComboBox>
                     <tlk:RadNumericTextBox CssClass="custom_txt" ID="txtTalent6" Width="57px" MaxLength="6" runat="server" />
                </div>
                <div id="div19" runat="server" data-value="19" style="padding-top: 10px">
                    <div id="divTalent7" style="display: inline-block;width: 49.2%;text-align:right;vertical-align:top">
                        <asp:CheckBox data-value="19" runat="server" ID="chkTalent7" Style="vertical-align: -webkit-baseline-middle; width: 4%" />
                        <tlk:RadComboBox ID="cboRelativeOperatorsTalent7" runat="server" Width="13%">
                            <Items>
                                <tlk:RadComboBoxItem Selected="true" Text="And" Value="And" />
                                <tlk:RadComboBoxItem Text="Or" Value="Or" />
                            </Items>
                        </tlk:RadComboBox>
                        <tlk:RadComboBox runat="server" ID="cboTalent7" Width="50.9%" />
                    </div>
                    <tlk:RadComboBox ID="cboComparisonOperatorsTalent7" runat="server" Width="45px">
                        <Items>
                            <tlk:RadComboBoxItem Selected="true" Text="<" Value="<" />
                            <tlk:RadComboBoxItem Text="&#8804;" Value="<=" />
                            <tlk:RadComboBoxItem Text="=" Value="=" />
                            <tlk:RadComboBoxItem Text=">" Value=">" />
                            <tlk:RadComboBoxItem Text="&#8805;" Value=">="  />
                        </Items>
                     </tlk:RadComboBox>
                     <tlk:RadNumericTextBox CssClass="custom_txt" ID="txtTalent7" Width="57px" MaxLength="6" runat="server" />
                </div>
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Backward">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="RadPane5" runat="server" CssClass="" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="CODE"
                                UniqueName="CODE" SortExpression="CODE" HeaderStyle-Width="80px"/>
                           <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULL_NAME"
                                UniqueName="FULL_NAME" SortExpression="FULL_NAME" HeaderStyle-Width="150px"/>
                           <tlk:GridNumericColumn HeaderText="<%$ Translate: Tuổi %>" DataField="AGE"                                
                                SortExpression="AGE" UniqueName="AGE" HeaderStyle-Width="50px">
                             </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                                UniqueName="GENDER_NAME" SortExpression="GENDER_NAME" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trình độ học vấn %>" DataField="TD_HV_NAME"
                                UniqueName="TD_HV_NAME" SortExpression="TD_HV_NAME" HeaderStyle-Width="120px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thâm niên %>" DataField="SENIOR_KN"
                                UniqueName="SENIOR_KN" SortExpression="SENIOR_KN" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí %>" DataField="POSITION_NAME"
                                UniqueName="POSITION_NAME" SortExpression="POSITION_NAME" HeaderStyle-Width="200px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_NAME"
                                UniqueName="STAFF_NAME" SortExpression="STAFF_NAME" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kinh nghiệm theo vị trí %>" DataField="KN_VT"
                                UniqueName="KN_VT" SortExpression="KN_VT" HeaderStyle-Width="200px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đã từng công tác %>" DataField="DT_CT_NAME"
                                UniqueName="DT_CT_NAME" SortExpression="DT_CT_NAME" HeaderStyle-Width="200px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả đánh giá %>" DataField="KQ_DG"
                                UniqueName="KQ_DG" SortExpression="KQ_DG" HeaderStyle-Width="350px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Khóa đào tạo %>" DataField="KHOA_DT"
                                UniqueName="KHOA_DT" SortExpression="KHOA_DT" HeaderStyle-Width="250px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Bằng cấp chứng chỉ %>" DataField="BC_CC"
                                UniqueName="BC_CC" SortExpression="BC_CC" HeaderStyle-Width="250px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đánh giá năng lực %>" DataField="NL_CM"
                                UniqueName="NL_CM" SortExpression="NL_CM" HeaderStyle-Width="350px"/>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="rscriptblock" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_SuccessorSearch_RadSplitter2';
        var enableAjax = true;
        var count = 13;
        var oldsize2 = 475;
        var postback;

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        $(document).ready(function () {
            postback = <%= postBack %>;
            if(postback == 0){
                $("span[data-value='" + 1 + "']").children().prop('checked', true);
                for (i = 2; i <= 19; i++) {
                    $("div[data-value='" + i + "']").hide();
                    $("span[data-value='" + i + "']").children().prop('checked', false)
                }
            } else {
                for (i = 1; i <= 19; i++) {
                    var ischecked = $("span[data-value='" + i + "']").children().is(':checked');
                    if(!ischecked) {
                        if( i == 1){
                            $($("div[data-value='" + i + "']").children()[0]).removeClass('show');
                            $($("div[data-value='" + i + "']").children()[0]).addClass('hide');
                        } else {
                            $("div[data-value='" + i + "']").hide();
                        }
                    }
                }
            }
        });

        function OnClientButtonClicking(sender, args) {
            var pane = $find("<%= RadPane4.ClientID %>");
            var pane2 = $find("<%= RadPane5.ClientID %>");
            pane.set_height(45);
            pane2.set_height(50);
        }

        function cboFilterChanged(sender, eventArgs) {
            var item = eventArgs.get_item();
            var value = item.get_value();
            var radControl = '';
            if (value != 0) {
                item.disable();
                if (value == 1) {
                    sender.findItemByValue('0').select();
                    $($("div[data-value='" + value + "']").children()[0]).removeClass('hide');
                    $($("div[data-value='" + value + "']").children()[0]).addClass('show');
                } else {
                    $("div[data-value='" + value + "']").show();
                    addSize($("div[data-value='" + value + "']").height() + 10);
                }
                sender.findItemByValue('0').select();
                $("span[data-value='" + value + "']").children().prop('checked', true);
                $("table[summary='combobox']").addClass("width_100_percent");
            }
        }

        $("input:checkbox").change(function () {
            var ischecked = $(this).is(':checked');
            var checkboxParent = $(this).parent();
            var dataValue = $(checkboxParent).attr('data-value');
            if (!ischecked) {
                if (dataValue != undefined) {
                    if (dataValue != 1) {
                        if (dataValue == 12) {
                            for (i = 13; i <= count; i++) {
                                var oldSize = $("div[data-value='" + i + "']").height() + 10;
                                $("div[data-value='" + i + "']").hide();
                                minusSize(oldSize)
                            }
                        }
                        var oldSize = $("div[data-value='" + dataValue + "']").height() + 10;
                        $("div[data-value='" + dataValue + "']").hide();
                        minusSize(oldSize);
                    } else {
                        $($("div[data-value='" + dataValue + "']").children()[0]).removeClass('show');
                        $($("div[data-value='" + dataValue + "']").children()[0]).addClass('hide');
                    }
                    var combo = $find("<%= cboFilterList.ClientID %>");
                    var comboItem = combo.get_items().getItem(dataValue);
                    comboItem.enable();
                    combo.trackChanges();
                }
            }
        });

        function cboTalentsChanged(sender, eventArgs) {
            var item = eventArgs.get_item();
            var value = item.get_value();
            if (value != '') {
                if (count < 20) {
                    item.disable();
                    $("div[data-value='" + count + "']").show();
                    addSize($("div[data-value='" + count + "']").height() + 10);
                    sender.findItemByText('').select();
                    $("span[data-value='" + count + "']").children().prop('checked', true);
                    $("table[summary='combobox']").addClass("width_100_percent");
                    var radControl = $telerik.$("[id$='cboTalent" + (count - 12) + "']").attr("id");
                    var combo = $find(radControl);
                    var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                    comboItem.set_text(item.get_text());
                    comboItem.set_value(value);
                    combo.trackChanges();
                    combo.get_items().add(comboItem);
                    comboItem.select();
                    combo.commitChanges();        
                    count++;
                } else {
                    var m = '<%= Translate("Bạn không thể chọn nhiều hơn 7 năng lực") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            }
        }

        function addSize(divHeight) {
            var pane = $find("<%= RadPane4.ClientID %>");
            var pane2 = $find("<%= RadPane5.ClientID %>");
            var currentHeight = pane.get_height();
            pane.set_height(currentHeight + divHeight);
            pane2.set_height(oldsize2 - divHeight);
            oldsize2 = pane2.get_height();
        }

        function minusSize(divHeight) {
            var pane = $find("<%= RadPane4.ClientID %>");
            var pane2 = $find("<%= RadPane5.ClientID %>")
            var currentHeight = pane.get_height();
            pane.set_height(currentHeight - divHeight);
            pane2.set_height(oldsize2 + divHeight);
            oldsize2 = pane2.get_height();
        }

        function saveFilter() {
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadScriptBlock>
