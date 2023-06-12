<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_AssessHistory.ascx.vb"
    Inherits="Performance.ctrlPE_AssessHistory" %>
<tlk:RadSplitter runat="server" ID="splitAll" Orientation="Horizontal" Width="100%"
    Height="100%">
    <tlk:RadPane runat="server" ID="RadPane1" Height="320px">
        <tlk:RadGrid PageSize="50" runat="server" ID="rgData" Width="99%" Height="250px">
            <MasterTableView ClientDataKeyNames="EMPLOYEE_CODE" DataKeyNames="EMPLOYEE_CODE">
                <Columns>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: STT %>' DataField="STT">
                        <ItemStyle Width="25" />
                        <HeaderStyle Width="25" />
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText='<%$ Translate: Mã nhân viên %>' DataField="EMPLOYEE_CODE">
                        <ItemStyle Width="50" />
                        <HeaderStyle Width="50" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: Người duyệt %>' DataField="FULLNAME_VN">
                        <ItemStyle Width="100" />
                        <HeaderStyle Width="100" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: Điểm đánh giá %>' DataField="RESULT_DIRECT">
                        <ItemStyle HorizontalAlign="Center" Width="70" />
                        <HeaderStyle Width="70" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: Ngày đánh giá %>' DataField="ASS_DATE"
                        DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle HorizontalAlign="Center" Width="70" />
                        <HeaderStyle Width="70" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText='<%$ Translate: Ghi chú %>' DataField="REMARK_DIRECT">
                        <ItemStyle Width="100" />
                        <HeaderStyle Width="100" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>