Imports System.IO
Imports Telerik.Web.UI
Public Class InsCommon

    Public Class OT_DECISION_TYPE
        Public Shared Name As String = "DECISION_TYPE"
        Public Shared DISCIPLINE As String = "DISCIPLINE"
        Public Shared DISCIPLINE_ID As Decimal = 448
        Public Shared COMMEND As String = "COMMEND"
        Public Shared COMMEND_ID As Decimal = 449
        Public Shared TERMINATE As String = "TERMINATE"
        Public Shared TERMINATE_ID As Decimal = 450
    End Class

    Public Class ReadExel
        Public Function ReadExcelToDT(strFilePath As String, intBeginRow As Integer, intBeginCol As Integer, ByRef strErr As String, iRowEndVal As Integer, iColEndVal As Integer) As DataTable
            'default 
            'int iRowEndVal = 0
            'int iColEndVal = 0

            If Not File.Exists(strFilePath) Then
                strErr = "FileNotFound"
            End If
            Dim wb As New Aspose.Cells.Workbook()
            wb.Open(strFilePath)
            Dim ws As Aspose.Cells.Worksheet = wb.Worksheets(0)

            Dim dt As New DataTable()
            '------init row,col-------------------------
            Dim iRowEnd As Integer = iRowEndVal
            Dim iColEnd As Integer = iColEndVal
            'If iRowEndVal > 0 Then
            iRowEnd = GetEndRow(intBeginRow, intBeginCol, ws)
            'End If
            ' If iColEndVal > 0 Then
            iColEnd = GetEndCol(intBeginRow, intBeginCol, ws)
            'End If
            '-------------------------------------------
            Try
                'get Header
                For j As Integer = intBeginCol To iColEnd - 1
                    Dim strDataColumn As String = ws.Cells(intBeginRow, j).Value.ToString().Trim()
                    dt.Columns.Add(New DataColumn(strDataColumn, GetType(String)))
                Next
                'get Data
                Dim intStep As Integer = 0
                For i As Integer = intBeginRow + 1 To iRowEnd - 1
                    intStep = 0
                    Dim dr As DataRow = dt.NewRow()
                    For j As Integer = intBeginCol To iColEnd - 1
                        Dim strValue As String = If(ws.Cells(i, j).Value Is Nothing, "", ws.Cells(i, j).Value.ToString())
                        dr(intStep) = strValue
                        intStep += 1
                    Next
                    dt.Rows.Add(dr)
                Next
                Return dt
            Catch exp As Exception
                Return Nothing
            End Try
        End Function


        Public Function ReadExcelToDTMultiSheetNew(strFilePath As String, listObject As List(Of Object), ByRef strErr As String) As DataSet
            If Not File.Exists(strFilePath) Then
                strErr = "FileNotFound"
            End If

            Dim wb As New Aspose.Cells.Workbook()
            wb.Open(strFilePath)

            ' tao dataset de tra ve
            Dim dataSet As New DataSet()
            Try

                For i As Integer = 0 To listObject.Count - 1
                    ' danh sach sheet
                    ' lay ra sheet can doc

                    Dim ws As Aspose.Cells.Worksheet = wb.Worksheets(i)
                    ' tao dataTable
                    Dim dt As New DataTable()

                    ' doc lay ra thong tin sheet
                    Dim arrSheet As Integer() = DirectCast(listObject(i), Integer())
                    Dim beginRow As Integer = arrSheet(0)
                    Dim beginCol As Integer = arrSheet(1)

                    ' lay ra so luong row can doc
                    Dim iRowEnd As Integer = GetEndRow(beginRow, beginCol, ws)
                    ' lay ra so luong col can doc
                    Dim iColEnd As Integer = GetEndCol(beginRow, beginCol, ws)

                    'get Header
                    For j As Integer = beginCol To iColEnd - 1
                        Dim strDataColumn As String = ws.Cells(beginRow, j).Value.ToString().Trim()
                        dt.Columns.Add(New DataColumn(strDataColumn, GetType(String)))
                    Next
                    'get Data
                    Dim intStep As Integer = 0
                    For x As Integer = beginRow + 1 To iRowEnd - 1
                        intStep = 0
                        Dim dr As DataRow = dt.NewRow()
                        For j As Integer = beginCol To iColEnd - 1
                            Dim strValue As String = If(ws.Cells(x, j).Value Is Nothing, "", ws.Cells(x, j).Value.ToString())
                            dr(intStep) = strValue
                            intStep += 1
                        Next
                        dt.Rows.Add(dr)
                    Next
                    '''/ doc voi iNumberRow dong` va doc den cot thu iColEnd
                    'dt = ws.Cells.ExportDataTable(beginRow, beginCol, iRowEnd - beginRow, iColEnd, true);
                    ' xu ly convert gia tri trong table thanh chuoi string
                    ' dat ten cho table
                    dt.TableName = "Sheet" + (i + 1).ToString()
                    ' nhan biet vi tri sheet
                    '''/ dua vao dataset
                    dataSet.Tables.Add(dt)
                Next
            Catch exp As Exception
                Return Nothing
            End Try
            ' tra ve ket qua
            Return dataSet
        End Function

        Public Function GetEndCol(intBeginRow As Integer, intBeginCol As Integer, ws As Aspose.Cells.Worksheet) As Integer
            Try
                Dim intCol As Integer = intBeginCol
                While True
                    If ws.Cells(intBeginRow, intCol).Value Is Nothing OrElse String.IsNullOrEmpty(ws.Cells(intBeginRow, intCol).Value.ToString()) Then
                        ' TODO: might not be correct. Was : Exit While
                        Exit While
                    Else
                        intCol += 1
                    End If
                End While
                Return intCol
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Function GetEndRow(intBeginRow As Integer, intBeginCol As Integer, ws As Aspose.Cells.Worksheet) As Integer
            Try
                Dim intRow As Integer = intBeginRow + 1
                ' +1 --> bo Header
                While True
                    If ws.Cells(intRow, intBeginCol).Value Is Nothing OrElse String.IsNullOrEmpty(ws.Cells(intRow, intBeginCol).Value.ToString()) Then
                        ' TODO: might not be correct. Was : Exit While
                        Exit While
                    Else
                        intRow += 1
                    End If
                End While
                Return intRow
            Catch ex As Exception
                Return 0
            End Try
        End Function
    End Class


    Public Shared Function getNumber(ByVal obj As Object) As Double?
        Try
            If obj.ToString = "" Then
                Return CDbl(0)
            Else
                Return CDbl(obj)
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function getNumber2(ByVal obj As Object) As Double
        Try
            Return CDbl(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Shared Function getNumberBoolean(ByVal obj As Object) As Double
        Try
            Return IIf(obj = True, 1, 0)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Shared Function getDate(ByVal obj As Object) As DateTime
        Dim d As Date = New Date(1900, 1, 1)
        Try
            If obj Is Nothing Then
                ' Return New Nullable(Of Date)
                Return Nothing
            Else
                Return obj
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function getString(ByVal obj As Object) As String
        Try
            If obj Is Nothing Or obj.ToString() = "" Then
                Return ""
            Else
                Return obj
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function




    Public Shared Sub SetString(rtb As RadTextBox, ByVal obj As Object)
        Try
            If obj Is System.DBNull.Value Then
                rtb.Text = ""
            Else
                rtb.Text = obj
            End If
        Catch ex As Exception
            rtb.Text = ""
        End Try
    End Sub
    Public Shared Sub SetDate(rtb As RadDatePicker, obj As Object)
        Try
            If obj Is System.DBNull.Value Then
                rtb.SelectedDate = Nothing
            Else
                rtb.SelectedDate = obj
            End If
        Catch ex As Exception
            rtb.SelectedDate = Nothing
        End Try
    End Sub
    Public Shared Sub SetDate(rtb As RadMonthYearPicker, obj As Object)
        Try
            If obj Is System.DBNull.Value Then
                rtb.SelectedDate = Nothing
            Else
                rtb.SelectedDate = obj
            End If
        Catch ex As Exception
            rtb.SelectedDate = Nothing
        End Try
    End Sub
    Public Shared Sub SetNumber(rtb As RadNumericTextBox, ByVal obj As Object)
        Try
            If obj Is System.DBNull.Value Then
                rtb.Value = Nothing
            Else
                rtb.Text = obj
            End If
        Catch ex As Exception
            rtb.Value = Nothing
        End Try
    End Sub
    Public Shared Sub SetNumber(rtb As RadComboBox, ByVal obj As Object)
        Try
            If obj Is System.DBNull.Value OrElse obj Is Nothing OrElse obj = 0 Then
                rtb.Text = ""
                rtb.SelectedValue = Nothing
            Else
                rtb.SelectedValue = obj
            End If
        Catch ex As Exception
            rtb.SelectedValue = Nothing
        End Try
    End Sub
    Public Shared Sub SetNumber(rtb As RadButton, ByVal obj As Object)
        Try
            If obj Is System.DBNull.Value Then
                rtb.Checked = False
            Else
                rtb.Checked = obj
            End If
        Catch ex As Exception
            rtb.Checked = False
        End Try
    End Sub

    Public Shared Function CheckData_Delete(ByVal sTable As String, ByVal id As String) As Boolean
        Try
            Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetCheckExist(sTable, id)
            If Not (lstSource Is Nothing) AndAlso lstSource.Rows.Count > 0 AndAlso lstSource.Rows(0)("EXIST") <> "0" Then
                Return False 'ko dc xoa
            Else
                Return True
            End If
        Catch ex As Exception
            Return True
        End Try
    End Function

    '---------Hàm lấy ID từ DM-------------------
    Public Shared Function getIdFromList(ByVal strVal As String, ByVal dt As DataTable, ByVal strID As String, ByVal strName As String) As String
        Try
            For Each dr As DataRow In dt.Rows
                If dr(strName).ToString.Trim.ToLower = strVal.Trim.ToLower Then
                    Return dr(strID)
                End If
            Next
            Return "0"
        Catch ex As Exception
            Return "0"
        End Try
    End Function

    '---------Hàm check ID từ DM-------------------
    Public Shared Function checkIDFromList(ByVal strVal As String, ByVal dt As DataTable, ByVal strID As String) As String
        Try
            For Each dr As DataRow In dt.Rows
                If dr(strID).ToString.Trim.ToLower = strVal.Trim.ToLower Then
                    Return dr(strID)
                End If
            Next
            Return "0"
        Catch ex As Exception
            Return "0"
        End Try
    End Function

End Class
