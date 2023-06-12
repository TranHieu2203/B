<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CanDtl.ascx.vb"
    Inherits="Recruitment.ctrlRC_CanDtl" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%">
    <tlk:RadPane ID="RadPaneLeft" runat="server" MinWidth="190" MaxWidth="190" Width="190px">
        <div style="width: 90px; text-align: center; margin: auto; margin-bottom: 5px;">
            <asp:PlaceHolder ID="ViewPlaceHoderImage" runat="server"></asp:PlaceHolder>
        </div>
        <tlk:RadPanelBar ID="rpbRecruitment" runat="server" Width="190px">
            <Items>
                <tlk:RadPanelItem Text="<%$ Translate:Hồ sơ ứng viên %>" Expanded="True">
                    <Items>
                        <tlk:RadPanelItem Value="ctrlRC_CanDtlProfile" Text="<%$ Translate:Hồ sơ ứng viên%>"
                            NavigateUrl='<%# String.Format("/Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can={0}&PROGRAM_ID={1}&Place=ctrlRC_CanDtlProfile&state=Normal", CandidateCode, ProgramID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CanDtlBeforeWT" Text="<%$ Translate:Quá trình công tác%>"
                            NavigateUrl='<%# String.Format("/Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can={0}&PROGRAM_ID={1}&Place=ctrlRC_CanDtlBeforeWT&state=Normal", CandidateCode, ProgramID) %>' />
                        <%--<tlk:RadPanelItem Value="ctrlRC_CanDtlTraining" Text="<%$ Translate:Quá trình đào tạo%>"
                            NavigateUrl='<%# String.Format("/Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can={0}&PROGRAM_ID={1}&Place=ctrlRC_CanDtlTraining&state=Normal", CandidateCode, ProgramID) %>' />--%>
                        <tlk:RadPanelItem Value="ctrlRC_CadidateReference" Text="<%$ Translate:Người tham chiếu%>"
                            NavigateUrl='<%# String.Format("/Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can={0}&PROGRAM_ID={1}&Place=ctrlRC_CadidateReference&state=Normal", CandidateCode, ProgramID) %>' />
                    </Items>
                </tlk:RadPanelItem>
            </Items>
        </tlk:RadPanelBar>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <asp:PlaceHolder ID="phRecruitment" runat="server"></asp:PlaceHolder>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:HiddenField ID="hidCandidateCode" runat="server" />
