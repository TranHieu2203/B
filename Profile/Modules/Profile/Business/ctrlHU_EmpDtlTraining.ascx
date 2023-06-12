<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlTraining.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlTraining" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="ToolbarPane" runat="server" Height="35px" Scrolling="None" Visible="false">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="RadPane4" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgEmployeeTrain" runat="server" AllowMultiRowSelection="true"
            Height="100%" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Têm khóa đào tạo %>" DataField="TR_COURSE_NAME"
                        SortExpression="TR_COURSE_NAME" UniqueName="TR_COURSE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chương trình đào tạo %>" DataField="TR_PROGRAM_NAME"
                        SortExpression="TR_PROGRAM_NAME" UniqueName="TR_PROGRAM_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chương trình %>" DataField="TR_PROGRAM_GROUP_NAME"
                        SortExpression="TR_PROGRAM_GROUP_NAME" UniqueName="TR_PROGRAM_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="TR_TRAIN_FIELD_NAME"
                        SortExpression="TR_TRAIN_FIELD_NAME" UniqueName="TR_TRAIN_FIELD_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức đào tạo %>" DataField="TR_TRAIN_FORM_NAME"
                        SortExpression="TR_TRAIN_FORM_NAME" UniqueName="TR_TRAIN_FORM_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời lượng %>" DataField="DURATION"
                        SortExpression="DURATION" UniqueName="DURATION" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="START_DATE"
                        SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="END_DATE"
                        SortExpression="END_DATE" UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trung tâm đào tạo %>" DataField="TR_UNIT_NAME"
                        SortExpression="TR_UNIT_NAME" UniqueName="TR_UNIT_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nội dung đào tạo %>" DataField="CONTENT"
                        SortExpression="CONTENT" UniqueName="CONTENT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mục đích %>" DataField="TARGET_TRAIN"
                        SortExpression="TARGET_TRAIN" UniqueName="TARGET_TRAIN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa điểm tổ chức %>" DataField="VENUE"
                        SortExpression="VENUE" UniqueName="VENUE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điểm đào tạo %>" DataField="TOEIC_FINAL_SCORE"
                        SortExpression="TOEIC_FINAL_SCORE" UniqueName="TOEIC_FINAL_SCORE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kết quả %>" DataField="IS_REACH"
                        SortExpression="IS_REACH" UniqueName="IS_REACH" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp loại %>" DataField="RANK_NAME"
                        SortExpression="RANK_NAME" UniqueName="RANK_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Văn bằng/Chứng chỉ %>" DataField="CERTIFICATE_NO"
                        SortExpression="CERTIFICATE_NO" UniqueName="CERTIFICATE_NO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời hạn chứng chỉ %>" DataField="CERTIFICATE_DATE"
                        SortExpression="CERTIFICATE_DATE" UniqueName="CERTIFICATE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày cấp chứng chỉ %>" DataField="CER_RECEIVE_DATE"
                        SortExpression="CER_RECEIVE_DATE" UniqueName="CER_RECEIVE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hạn chứng chỉ %>" DataField="CER_EXPIRED_DATE"
                        SortExpression="CER_EXPIRED_DATE" UniqueName="CER_EXPIRED_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số cam kết %>" DataField="COMITMENT_TRAIN_NO"
                        SortExpression="COMITMENT_TRAIN_NO" UniqueName="COMITMENT_TRAIN_NO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian cam kết  %>" DataField="COMMIT_WORK"
                        SortExpression="COMMIT_WORK" UniqueName="COMMIT_WORK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bắt đầu cam kết %>" DataField="COMITMENT_START_DATE"
                        SortExpression="COMITMENT_START_DATE" UniqueName="COMITMENT_START_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc cam kết %>" DataField="COMITMENT_END_DATE"
                        SortExpression="COMITMENT_END_DATE" UniqueName="COMITMENT_END_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                        UniqueName="REMARK" />
                </Columns>
                <HeaderStyle Width="150px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane4.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
