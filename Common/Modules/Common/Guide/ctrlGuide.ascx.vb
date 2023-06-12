Imports System.IO
Imports Aspose.Words
Imports Aspose.Words.Fields
Imports Aspose.Words.Saving
Imports Framework.UI
Imports Telerik.Web.UI

Public Class ctrlGuide
    Inherits CommonView

    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"

    Private Property isLoad As Boolean
        Get
            Return ViewState(Me.ID & "_isLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isLoad") = value
        End Set
    End Property


    Private Property dtTableMain As DataTable
        Get
            Return ViewState(Me.ID & "_dtTableMain")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtTableMain") = value
        End Set
    End Property

    Private Property doc As Document
        Get
            Return ViewState(Me.ID & "_doc")
        End Get
        Set(ByVal value As Document)
            ViewState(Me.ID & "_doc") = value
        End Set
    End Property

#End Region

#Region "Page"


    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        If Not isLoad Then
            isLoad = True
            ViewImage(CreateReport(dtTableMain), 1)
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

    End Sub

#End Region

#Region "Event"

    Protected Sub treeHeading_NodeClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles treeHeading.NodeClick

        Try
            If treeHeading.SelectedNode Is Nothing Then
                Exit Sub
            End If
            If treeHeading.SelectedValue <> "" Then
                Dim selectOrg = Integer.Parse(treeHeading.SelectedValue)
                ViewImage(CreateReport(dtTableMain), selectOrg, True)
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

#Region "Custom"

    Private Function CreateReport(ByVal dtTable As DataTable) As Document
        Dim sr As Document
        Try
            If doc Is Nothing Then
                'Render the sheet with respect to specified image/print options
                sr = New Document(Server.MapPath("~/Guide/iPortal.docx"))
                doc = sr
            Else
                sr = doc
            End If

            If dtTable Is Nothing OrElse (dtTable.Rows.Count = 0) Then

                dtTable = New DataTable
                dtTable.Columns.Add("Node")
                dtTable.Columns.Add("CurrentPage")
                dtTable.Columns.Add("Node_Parent")
                dtTable.Columns.Add("StyleIdentifier")

                Dim paragraphs = ParagraphsByDoc(sr)
                Dim section = sr.GetChildNodes(NodeType.Paragraph, True, True)
                Dim dtLevel = GetLevelByParagragh(paragraphs)
                Dim i As Integer = 0
                For Each paragraph As Paragraph In paragraphs
                    Dim org As DataRow = dtTable.NewRow
                    org("Node") = If(paragraph.ParagraphFormat.StyleIdentifier <> StyleIdentifier.Title,
                                     dtLevel(i)("SUM").ToString & " ", "") & paragraph.Range.Text
                    org("CurrentPage") = section.IndexOf(paragraph)
                    org("NODE_PARENT") = dtLevel(i)("PARENT").ToString
                    org("StyleIdentifier") = paragraph.ParagraphFormat.StyleIdentifier.ToString
                    dtTable.Rows.Add(org)
                    i += 1
                Next paragraph

                dtTableMain = dtTable
            End If

            BuildTreeNode(treeHeading, dtTableMain)

            Return sr
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetLevelByParagragh(ByVal paragraphs As ArrayList) As DataTable
        Dim dtLevel = New DataTable
        dtLevel.Columns.Add("H1", Type.GetType("System.Double"))
        dtLevel.Columns.Add("H2", Type.GetType("System.Double"))
        dtLevel.Columns.Add("H3", Type.GetType("System.Double"))
        dtLevel.Columns.Add("H4", Type.GetType("System.Double"))
        dtLevel.Columns.Add("PARENT", Type.GetType("System.String"))
        dtLevel.Columns.Add("SUM", Type.GetType("System.String"))
        Try
            Dim ih1 As Double = 1
            Dim ih2 As Double = 1
            Dim ih3 As Double = 1
            Dim ih4 As Double = 1
            Dim sh1 As String = ""
            Dim sh2 As String = ""
            Dim sh3 As String = ""
            Dim sh4 As String = ""
            For Each paragraph As Paragraph In paragraphs
                Dim org As DataRow = dtLevel.NewRow
                Select Case paragraph.ParagraphFormat.StyleIdentifier
                    Case StyleIdentifier.Title
                    Case StyleIdentifier.Heading1
                        org("H1") = ih1
                        sh1 = ih1.ToString & " " & paragraph.Range.Text
                        org("SUM") = ih1.ToString
                        ih1 += 1
                        ih2 = 1
                        ih3 = 1
                        ih4 = 1
                        sh2 = ""
                        sh3 = ""
                        sh4 = ""
                    Case StyleIdentifier.Heading2
                        org("H1") = ih1
                        org("H2") = ih2
                        sh2 = (ih1 - 1).ToString & "." & ih2.ToString & " " & paragraph.Range.Text
                        org("SUM") = (ih1 - 1).ToString & "." & ih2.ToString
                        org("PARENT") = sh1
                        ih2 += 1
                        ih3 = 1
                        ih4 = 1
                        sh3 = ""
                        sh4 = ""
                    Case StyleIdentifier.Heading3
                        org("H1") = ih1
                        org("H2") = ih2
                        org("H3") = ih3
                        sh3 = (ih1 - 1).ToString & "." & (ih2 - 1).ToString & "." & ih3.ToString & " " & paragraph.Range.Text
                        org("SUM") = (ih1 - 1).ToString & "." & (ih2 - 1).ToString & "." & ih3.ToString
                        org("PARENT") = sh2
                        ih3 += 1
                        ih4 = 1
                        sh4 = ""
                    Case StyleIdentifier.Heading4
                        org("H1") = ih1
                        org("H2") = ih2
                        org("H3") = ih3
                        org("H4") = ih4
                        sh4 = (ih1 - 1).ToString & "." & (ih2 - 1).ToString & "." & (ih3 - 1).ToString & "." & ih4.ToString & " " & paragraph.Range.Text
                        org("SUM") = (ih1 - 1).ToString & "." & (ih2 - 1).ToString & "." & (ih3 - 1).ToString & "." & ih4.ToString
                        org("PARENT") = sh3
                        ih4 += 1
                End Select
                dtLevel.Rows.Add(org)
            Next
            Return dtLevel
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Private Sub ViewImage(ByVal sr As Document, ByVal pageindex As Integer,
                          Optional ByVal isTreeview As Boolean = False)
        Try
            If isTreeview Then
                Dim section = sr.GetChildNodes(NodeType.Paragraph, True, True)
                Dim builder As New DocumentBuilder(sr)
                builder.MoveTo(section.Item(pageindex))
                Dim p As Field = builder.InsertField("PAGE")
                builder.Document.UpdatePageLayout()
                p.Update()
                pageindex = Integer.Parse(p.Result)
                p.Result = ""
                p.Remove()

            End If
            'Define ImageOrPrintOptions
            Dim imgOptions As New ImageSaveOptions(SaveFormat.Png)

            lbPage.Text = "/ " & sr.PageCount.ToString
            If pageindex = sr.PageCount Or pageindex > sr.PageCount Then
                btnNext.Enabled = False
                pageindex = sr.PageCount
                rntxtPageIndex.Value = sr.PageCount
            Else
                btnNext.Enabled = True
            End If
            If pageindex = 1 Then
                btnBack.Enabled = False
            Else
                btnBack.Enabled = True
            End If

            rntxtPageIndex.Value = pageindex
            imgOptions.PageIndex = pageindex - 1
            Dim ms As New MemoryStream
            sr.Save(ms, imgOptions)

            imgPreview.DataValue = ms.ToArray()
            imgPreview.Visible = True
            RadPane1.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function ParagraphsByDoc(ByVal doc As Document) As ArrayList
        ' Create an array to collect paragraphs of the specified style.
        Dim paragraphsWithStyle As New ArrayList()
        ' Get all paragraphs from the document.
        Dim paragraphs As NodeCollection
        paragraphs = doc.GetChildNodes(NodeType.Paragraph, True)

        ' Look through all paragraphs to find those with the specified style.
        For Each paragraph As Paragraph In paragraphs
            If paragraph.ParagraphFormat.Style.StyleIdentifier = StyleIdentifier.Heading1 Or _
               paragraph.ParagraphFormat.Style.StyleIdentifier = StyleIdentifier.Heading2 Or _
                paragraph.ParagraphFormat.Style.StyleIdentifier = StyleIdentifier.Heading3 Or _
                paragraph.ParagraphFormat.Style.StyleIdentifier = StyleIdentifier.Heading4 Or _
                paragraph.ParagraphFormat.Style.StyleIdentifier = StyleIdentifier.Title Then
                paragraphsWithStyle.Add(paragraph)

            End If
        Next paragraph



        Return paragraphsWithStyle
    End Function

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Try
            If rntxtPageIndex.Value Is Nothing Then
                rntxtPageIndex.Value = 1
            End If
            If rntxtPageIndex.Value < 1 Then
                rntxtPageIndex.Value = 1
            Else
                If rntxtPageIndex.Value > 1 Then
                    rntxtPageIndex.Value = rntxtPageIndex.Value - 1
                End If
            End If
            If rntxtPageIndex.Value = 1 Then
                ViewImage(CreateReport(dtTableMain), 1)
            Else
                ViewImage(CreateReport(dtTableMain), rntxtPageIndex.Value)
            End If

        Catch ex As Exception
            DisplayException(ViewName, ID, ex)
        End Try
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
        Try
            If rntxtPageIndex.Value Is Nothing Then
                rntxtPageIndex.Value = 1
            End If
            If rntxtPageIndex.Value < 1 Then
                rntxtPageIndex.Value = 1
            Else
                rntxtPageIndex.Value = rntxtPageIndex.Value + 1
            End If
            If rntxtPageIndex.Value = 1 Then
                ViewImage(CreateReport(dtTableMain), 1)
            Else
                ViewImage(CreateReport(dtTableMain), rntxtPageIndex.Value)
            End If

        Catch ex As Exception
            DisplayException(ViewName, ID, ex)
        End Try
    End Sub

    Protected Sub rntxtPageIndex_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rntxtPageIndex.TextChanged
        Try
            If rntxtPageIndex.Value Is Nothing Then
                rntxtPageIndex.Value = 1
            End If
            If rntxtPageIndex.Value < 1 Then
                rntxtPageIndex.Value = 1
            End If
            ViewImage(CreateReport(dtTableMain), rntxtPageIndex.Value)
        Catch ex As Exception
            DisplayException(ViewName, ID, ex)
        End Try

    End Sub

    Protected Sub BuildTreeNode(ByVal tree As RadTreeView,
                                ByVal list As DataTable)

        Dim bSelected As Boolean = False
        Try

            tree.Nodes.Clear()
            If list.Rows.Count = 0 Then
                Exit Sub
            End If
            Dim listTemp = (From t In list
                            Where t("NODE_PARENT").ToString = "" Select t).ToList
            For index As Integer = 0 To listTemp.Count - 1
                Dim node As New RadTreeNode
                node.Text = listTemp(index)("NODE")
                node.ToolTip = listTemp(index)("NODE")
                node.Value = listTemp(index)("CurrentPage").ToString

                tree.Nodes.Add(node)
                BuildTreeChildNode(node, list)
            Next


            tree.ExpandAllNodes()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub BuildTreeChildNode(ByVal nodeParent As RadTreeNode,
                                     ByVal list As DataTable)
        Try

            Dim listTemp = (From t In list
                            Where t("NODE_PARENT").ToString = nodeParent.ToolTip).ToList

            If listTemp.Count = 0 Then
                Exit Sub
            End If
            For index As Integer = 0 To listTemp.Count - 1
                Dim node As New RadTreeNode
                node.Text = listTemp(index)("NODE")
                node.ToolTip = listTemp(index)("NODE")
                node.Value = listTemp(index)("CurrentPage").ToString
                node.Width = New Unit(100)
                nodeParent.Nodes.Add(node)
                BuildTreeChildNode(node, list)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
End Class