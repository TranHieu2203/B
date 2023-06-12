<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CVPoolDtl.ascx.vb"
    Inherits="Recruitment.ctrlRC_CVPoolDtl" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%">
    <tlk:RadPane ID="RadPaneLeft" runat="server" MinWidth="190" MaxWidth="190" Width="190px">
        <div style="width: 90px; text-align: center; margin: auto; margin-bottom: 5px;">
            <asp:PlaceHolder ID="ViewPlaceHoderImage" runat="server"></asp:PlaceHolder>
        </div>
        <tlk:RadPanelBar ID="rpbRecruitment" runat="server" Width="190px">
            <Items>
                <tlk:RadPanelItem Text="<%$ Translate:Hồ sơ ứng viên %>" Expanded="True">
                    <Items>
                        <tlk:RadPanelItem Value="ctrlRC_CVPoolDtlProfile" Text="<%$ Translate:Hồ sơ ứng viên%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CVPoolDtl&group=Business&Can={0}&Place=ctrlRC_CVPoolDtlProfile&noscroll=1&state=Normal", CandidateCode) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CVPoolDtlBeforeWT" Text="<%$ Translate:Quá trình công tác%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CVPoolDtl&group=Business&Can={0}&PROGRAM_ID={1}&Place=ctrlRC_CVPoolDtlBeforeWT&noscroll=1&state=Normal", CandidateCode, ProgramID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CVPoolDtlFamily" Text="<%$ Translate:Quan hệ thân nhân%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CVPoolDtl&group=Business&Can={0}&PROGRAM_ID={1}&Place=ctrlRC_CVPoolDtlFamily&noscroll=1&state=Normal", CandidateCode, ProgramID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CVPoolDtlTraining" Text="<%$ Translate:Quá trình đào tạo%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CVPoolDtl&group=Business&Can={0}&PROGRAM_ID={1}&Place=ctrlRC_CVPoolDtlTraining&noscroll=1&state=Normal", CandidateCode, ProgramID) %>' />
                        <tlk:RadPanelItem Value="ctrlRC_CadidateReference" Text="<%$ Translate:Người tham chiếu%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlRC_CVPoolDtl&group=Business&Can={0}&PROGRAM_ID={1}&Place=ctrlRC_CVPoolReference&noscroll=1&state=Normal", CandidateCode, ProgramID) %>' />
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
