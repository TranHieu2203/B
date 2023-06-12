<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalWorkingBefore.ascx.vb"
    Inherits="Profile.ctrlPortalWorkingBefore" %>
<tlk:RadGrid PageSize="50" ID="rgWorkingBefore" runat="server" Height="350px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,COMPANY_NAME,COMPANY_ADDRESS,JOIN_DATE,END_DATE,TITLE_NAME,LEVEL_NAME">
        <Columns>
            <tlk:GridBoundColumn HeaderText="Tên công ty" DataField="COMPANY_NAME" UniqueName="COMPANY_NAME"
                SortExpression="COMPANY_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Địa chỉ công ty" DataField="COMPANY_ADDRESS"
                UniqueName="COMPANY_ADDRESS" SortExpression="COMPANY_ADDRESS" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Từ ngày" DataField="JOIN_DATE" UniqueName="JOIN_DATE"
                DataFormatString="{0:MM/yyyy}" SortExpression="JOIN_DATE" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Đến ngày" DataField="END_DATE" UniqueName="END_DATE"
                DataFormatString="{0:MM/yyyy}" SortExpression="END_DATE" ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                SortExpression="TITLE_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Công việc chính" DataField="WORK" UniqueName="WORK">
             <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Thâm niên" DataField="THAM_NIEN" UniqueName="THAM_NIEN">
             <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Lí do nghỉ việc" DataField="TER_REASON" UniqueName="TER_REASON">
             <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:gridcheckboxcolumn HeaderText="HSV/Hoàn vũ" DataField="IS_HSV" UniqueName="IS_HSV">
             <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:gridcheckboxcolumn>

            <%-- <tlk:GridBoundColumn HeaderText="Cấp bậc" DataField="LEVEL_NAME"
                UniqueName="LEVEL_NAME" SortExpression="LEVEL_NAME"
                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>--%>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>