<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAtSeniority.ascx.vb"
    Inherits="Attendance.ctrlAtSeniority" %>
<%@ Import Namespace="Common" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="330px" Scrolling="Y" >
        <asp:HiddenField ID="hidID" runat="server" />
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
       <table class="table-form">
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label33" Text="Ngày hiệu lực"></asp:Label><span class="lbReq">*</span>
                </td>
               <td>
                   <tlk:RadDatePicker runat="server" ID="rdEffectDate"></tlk:RadDatePicker>
               </td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdEffectDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày hiệu lực %>"> </asp:RequiredFieldValidator>
                <td class="lb">
                    <asp:Label runat="server" ID="Label34" Text="Ghi chú"></asp:Label>
                </td>
               <td colspan="9">
                   <tlk:RadTextBox runat="server" ID="txtNote" Width="100%"></tlk:RadTextBox>
               </td>
           </tr>
            <tr>
                <td colspan="12">
                    <b>
                        <asp:Label ForeColor="Blue" runat="server" ID="Label35" Text="Thiết lập tham niên"></asp:Label>
                    </b>
                    <hr />
                </td>
            </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label4" Text="1 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear1" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label36" Text="11 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear11" SkinID="decimal" Width="120px" ></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label37" Text="21 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear21" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label38" Text="31 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear31" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label39" Text="41 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear41" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label40" Text="51 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear51" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
           </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="2 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear2" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label2" Text="12 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear12" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label3" Text="22 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear22" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label5" Text="32 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear32" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="42 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear42" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label7" Text="52 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear52" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
           </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label8" Text="3 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear3" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label9" Text="13 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear13" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label10" Text="23 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear23" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label11" Text="33 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear33" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label12" Text="43 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear43" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label13" Text="53 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear53" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
           </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label14" Text="4 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear4" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label15" Text="14 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear14" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label16" Text="24 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear24" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label17" Text="34 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear34" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label18" Text="44 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear44" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label19" Text="54 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear54" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
           </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label20" Text="5 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear5" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label21" Text="15 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear15" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label22" Text="25 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear25" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label23" Text="35 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear35" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label24" Text="45 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear45" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label25" Text="55 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear55" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
           </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label26" Text="6 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear6" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label27" Text="16 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear16" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label28" Text="26 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear26" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label29" Text="36 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear36" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label30" Text="46 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear46" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label31" Text="56 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear56" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
           </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label32" Text="7 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear7" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label41" Text="17 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear17" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label42" Text="27 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear27" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label43" Text="37 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear37" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label44" Text="47 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear47" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label45" Text="57 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear57" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
           </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label46" Text="8 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear8" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label47" Text="18 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear18" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label48" Text="28 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear28" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label49" Text="38 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear38" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label50" Text="48 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear48" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label51" Text="58 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear58" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
           </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label52" Text="9 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear9" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label53" Text="19 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear19" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label54" Text="29 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear29" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label55" Text="39 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear39" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label56" Text="49 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear49" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label57" Text="59 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear59" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
           </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label58" Text="10 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear10" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label59" Text="20 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear20" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label60" Text="30 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear30" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label61" Text="40 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear40" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label62" Text="50 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear50" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label63" Text="60 năm"></asp:Label>
                </td>
                <td>
                  <tlk:RadNumericTextBox runat="server" ID="ntxtYear60" SkinID="decimal" Width="120px"></tlk:RadNumericTextBox>
                </td>
           </tr>       
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" 
        style="margin-top: 11px">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,EFFECTDATE,NOTE,YEAR1,YEAR2,YEAR3,YEAR4,YEAR5,YEAR6,YEAR7,YEAR8,YEAR9,YEAR10,YEAR11,YEAR12,YEAR13,YEAR14,YEAR15,YEAR16,YEAR17,YEAR18,YEAR19,YEAR20,
YEAR21,YEAR22,YEAR23,YEAR24,YEAR25,YEAR26,YEAR27,YEAR28,YEAR29,YEAR30,YEAR31,YEAR32,YEAR33,YEAR34,YEAR35,YEAR36,YEAR37,YEAR38,YEAR39,YEAR40,YEAR41,YEAR42,
YEAR43,YEAR44,YEAR45,YEAR46,YEAR47,YEAR48,YEAR49,YEAR50,YEAR51,YEAR52,YEAR53,YEAR54,YEAR55,YEAR56,YEAR57,YEAR58,YEAR59,YEAR60">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTDATE"
                        DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime" UniqueName="EFFECTDATE"
                        SortExpression="EFFECTDATE" CurrentFilterFunction="EqualTo">
                        <HeaderStyle Width="80px" />
                     </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE">
                    </tlk:GridBoundColumn>
                </Columns>
                <HeaderStyle Width="120px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlAtSeniority_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAtSeniority_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAtSeniority_RadPane2';
        var validateID = 'MainContent_ctrlAtSeniority_valSum';
        var oldSize = $('#' + pane1ID).height();
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
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                var grid = $find("<%=rgDanhMuc.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
