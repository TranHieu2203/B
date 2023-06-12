<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCanDtl.ascx.vb"
    Inherits="Recruitment.ctrlCanDtl" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%">
    <tlk:RadPane ID="RadPaneLeft" runat="server" MinWidth="190" MaxWidth="190" Width="190px">
        <div style="width: 90px; text-align: center; margin: auto; margin-bottom: 5px;">
            <asp:PlaceHolder ID="ViewPlaceHoderImage" runat="server"></asp:PlaceHolder>
        </div>
        <tlk:RadPanelBar ID="rpbRecruitment" runat="server" Width="190px">
            <Items>
                <tlk:RadPanelItem Expanded="True">
                    <Items>
                        <tlk:RadPanelItem Value="ctrlCanDtlProfile" Text="<%$ Translate:Hồ sơ ứng viên%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlCanDtl&group=Business&Can={0}&plan={1}&Place=ctrlCanDtlProfile&state=Normal", CandidateCode,PlanningID) %>' />
                        <tlk:RadPanelItem Value="ctrlCanDtlFamily" Text="<%$ Translate:Quan hệ thân nhân%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlCanDtl&group=Business&Can={0}&plan={1}&Place=ctrlCanDtlFamily&state=Normal", CandidateCode,PlanningID) %>' />
                        <tlk:RadPanelItem Value="ctrlCanDtlTraining" Text="<%$ Translate:Quá trình đào tạo%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlCanDtl&group=Business&Can={0}&plan={1}&Place=ctrlCanDtlTraining&state=Normal", CandidateCode,PlanningID) %>' />
                        <tlk:RadPanelItem Value="ctrlCanDtlWorkingBefore" Text="<%$ Translate:Quá trình công tác trước đây%>"
                            NavigateUrl='<%# String.Format("/Default.aspx?mid=Recruitment&fid=ctrlCanDtl&group=Business&Can={0}&plan={1}&Place=ctrlCanDtlWorkingBefore&state=Normal", CandidateCode,PlanningID) %>' />
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
