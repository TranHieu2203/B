<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalTrainingOutCompany.ascx.vb"
    Inherits="Profile.ctrlPortalTrainingOutCompany" %>
<tlk:RadGrid PageSize="50" ID="rgTrainingOutCompany" runat="server" Height="350px" AllowFilteringByColumn="true" Scrolling="both">
    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,FROM_DATE,TO_DATE,YEAR_GRA,NAME_SHOOLS,FORM_TRAIN_ID,FORM_TRAIN_NAME,TYPE_TRAIN_ID,TYPE_TRAIN_NAME,SPECIALIZED_TRAIN,RESULT_TRAIN,CERTIFICATE,RECEIVE_DEGREE_DATE,RECEIVE_DEGREE_DATE,EFFECTIVE_DATE_TO">
        <Columns>
            <%--<tlk:GridDateTimeColumn HeaderText="Từ tháng/năm" DataField="FROM_DATE"
                UniqueName="FROM_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="FROM_DATE"
                ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Tới tháng/năm" DataField="TO_DATE"
                UniqueName="TO_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="TO_DATE"
                ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridBoundColumn HeaderText="Năm tốt nghiệp" DataField="YEAR_GRA"
                UniqueName="YEAR_GRA" SortExpression="YEAR_GRA" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Tên trường" DataField="NAME_SHOOLS"
                UniqueName="NAME_SHOOLS" SortExpression="NAME_SHOOLS" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Hình thức đào tạo" DataField="FORM_TRAIN_NAME"
                UniqueName="FORM_TRAIN_NAME" SortExpression="FORM_TRAIN_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Loại hình đào tạo" DataField="TYPE_TRAIN_NAME"
                UniqueName="TYPE_TRAIN_NAME" SortExpression="TYPE_TRAIN_NAME" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Chuyên ngành" DataField="SPECIALIZED_TRAIN"
                UniqueName="SPECIALIZED_TRAIN" SortExpression="SPECIALIZED_TRAIN" ShowFilterIcon="false"
                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle Width="20%" HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Kết quả đào tạo" DataField="RESULT_TRAIN"
                UniqueName="RESULT_TRAIN" SortExpression="RESULT_TRAIN"
                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Bằng cấp/chứng chỉ" DataField="CERTIFICATE"
                UniqueName="CERTIFICATE" SortExpression="CERTIFICATE"
                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày nhận bằng" DataField="RECEIVE_DEGREE_DATE"
                UniqueName="RECEIVE_DEGREE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="RECEIVE_DEGREE_DATE"
                ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECTIVE_DATE_FROM"
                UniqueName="EFFECTIVE_DATE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>
            <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EFFECTIVE_DATE_TO"
                UniqueName="EFFECTIVE_DATE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                ShowFilterIcon="true">
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </tlk:GridDateTimeColumn>--%>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>