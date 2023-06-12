Imports System.Text.RegularExpressions


Public Class ImportValidate

    Public Shared Sub AlphaNumeric(ByVal colName As String, _
                                           ByRef row As DataRow, _
                                           ByRef rowError As DataRow, _
                                           ByRef isError As Boolean, _
                                           ByVal sError As String)

        Dim value As String
        Try
            value = row(colName).ToString.Trim
            row(colName) = value
            If Not Validator.IsAlphaNumeric(value) Then
                rowError(colName) += sError
                isError = True
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub IsValidEmail(ByVal colName As String, _
                                           ByRef row As DataRow, _
                                           ByRef rowError As DataRow, _
                                           ByRef isError As Boolean, _
                                           ByVal sError As String)

        Dim value As String
        Try
            value = row(colName).ToString.Trim
            row(colName) = value
            If Not Validator.IsValidEmail(value) Then
                rowError(colName) += sError
                isError = True
            End If

        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Public Shared Sub IsValidLenght(ByVal colName As String,
                                    ByRef row As DataRow,
                                    ByRef rowError As DataRow,
                                    ByRef isError As Boolean,
                                    ByVal sError As String)

        Dim value As Integer
        Try
            value = row(colName).ToString.Length
            If value > 2000 Then
                rowError(colName) = "Độ dài vượt quá giới hạn cho phép"
                isError = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub IsValidNumber(ByVal colName As String, _
                                           ByRef row As DataRow, _
                                           ByRef rowError As DataRow, _
                                           ByRef isError As Boolean, _
                                           ByVal sError As String)

        Dim value As String
        Try

            value = row(colName).ToString.Trim.Replace(",", "")
            row(colName) = value
            If Not IsNumeric(value) Then
                rowError(colName) += sError
                isError = True
            Else
                If value.Length > 30 Then
                    rowError(colName) += "Độ dài số không được vượt quá 30 ký tự"
                    isError = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Kiểm tra không nhập + sai định dạng số
    ''' </summary>
    ''' <param name="colText">tên cột cần kiểm tra</param>
    ''' <param name="colNumber">tên cột cần kiểm tra</param>
    ''' <param name="row">bản ghi cần kiểm tra</param>
    ''' <param name="rowError">bản ghi ghi nhận lỗi</param>
    ''' <param name="isError">biến xác định có lỗi hay không?</param>
    ''' <param name="sError">Mô tả cột. VD: Số tiền</param>
    ''' <remarks></remarks>
    Public Shared Sub IsValidList(ByVal colText As String, _
                                  ByVal colNumber As String, _
                                  ByRef row As DataRow, _
                                  ByRef rowError As DataRow, _
                                  ByRef isError As Boolean, _
                                  ByVal sError As String)

        Dim value As String
        Try
            EmptyValue(colText, row, rowError, isError, sError)
            If rowError(colText).ToString <> "" Then
                rowError(colText) += " chưa nhập"
                Exit Sub
            End If

            value = row(colNumber).ToString.Trim.Replace(",", "")
            row(colNumber) = value
            If Not IsNumeric(value) Then
                rowError(colText) += " sai định dạng"
                isError = True
            Else
                If value.Length > 30 Then
                    rowError(colNumber) += " độ dài số không được vượt quá 30 ký tự"
                    isError = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub CheckNumber(ByVal colName As String, _
                                           ByRef row As DataRow, _
                                           Optional ByVal defaultValue As Object = Nothing)

        Dim value As String
        Try
            value = row(colName).ToString.Trim
            row(colName) = value
            If Not IsNumeric(value) Then
                If defaultValue Is Nothing Then
                    row(colName) = DBNull.Value
                Else
                    row(colName) = defaultValue
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Public Shared Sub IsValidDate(ByVal colName As String, _
                                           ByRef row As DataRow, _
                                           ByRef rowError As DataRow, _
                                           ByRef isError As Boolean, _
                                           ByVal sError As String)

        Dim value As String
        Try
            value = row(colName).ToString.Trim
            row(colName) = value
            If Validator.EmptyValue(value) Then
                rowError(colName) += sError
                isError = True
                Exit Sub
            End If

            If Not ValidateDate(value, sError) Then
                rowError(colName) += sError
                isError = True
                Exit Sub
            End If

            'row(colName) = FormatDate(value)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Shared Function ValidateMDate(ByVal str As String, Optional ByRef _error As String = "") As Boolean
        If str.Split("/").Length <> 2 Then
            _error = "Sai định dạng MM/yyyy"
            Return False
        End If
        Dim month = str.Split("/")(0)
        Dim year = str.Split("/")(1)
        If Not IsNumeric(month) Or Not IsNumeric(year) Then
            _error = "Sai định dạng MM/yyyy"
            Return False
        End If
        If year < 1900 Or year > 2999 Then
            _error = "Năm không được nhỏ hơn 1900 và lớn hơn 2999"
            Return False
        End If
        If month < 1 Or month > 12 Then
            _error = "Tháng không được nhỏ hơn 1 và lớn hơn 12"
            Return False
        End If
        Return True
    End Function

    Public Shared Function ValidateDate(ByVal str As String, Optional ByRef _error As String = "") As Boolean
        If str.Split("/").Length <> 3 Then
            _error = "Sai định dạng dd/MM/yyyy"
            Return False
        End If
        Dim day = str.Split("/")(0)
        Dim month = str.Split("/")(1)
        Dim year = str.Split("/")(2)
        If Not IsNumeric(day) Or Not IsNumeric(month) Or Not IsNumeric(year) Then
            _error = "Sai định dạng dd/MM/yyyy"
            Return False
        End If
        If year < 1900 Or year > 2999 Then
            _error = "Năm không được nhỏ hơn 1900 và lớn hơn 2999"
            Return False
        End If
        If month < 1 Or month > 12 Then
            _error = "Tháng không được nhỏ hơn 1 và lớn hơn 12"
            Return False
        End If
        If day < 1 Or day > Date.DaysInMonth(year, month) Then
            _error = "Ngày không được nhỏ hơn 1 và lớn hơn số ngày trong tháng"
            Return False
        End If
        Return True
    End Function

    Public Shared Function FormatDate(ByVal str As String) As String
        Dim day = str.Split("/")(0)
        Dim month = str.Split("/")(1)
        Dim year = str.Split("/")(2)
        Return Format(New Date(year, month, day), "dd/MMM/yyyy")
    End Function

    Public Shared Sub KiemtraURL(ByVal colName As String, _
                                          ByRef row As DataRow, _
                                          ByRef rowError As DataRow, _
                                          ByRef isError As Boolean, _
                                          ByVal sError As String)
        Dim value As String
        Try
            value = row(colName).ToString.Trim
            row(colName) = value
            If Not mf_KiemtraURL(value) Then
                rowError(colName) += sError
                isError = True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub IsValidLength(ByVal colName As String, _
                                          ByRef row As DataRow, _
                                          ByRef rowError As DataRow, _
                                          ByRef isError As Boolean, _
                                          ByVal sError As String, _
                                          ByVal ileng As Integer)
        Dim value As String
        Try
            value = row(colName).ToString.Trim
            row(colName) = value
            If value.Length > ileng Then
                rowError(colName) += String.Format(sError, ileng)
                isError = True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub EmptyValue(ByVal colName As String, _
                                          ByRef row As DataRow, _
                                          ByRef rowError As DataRow, _
                                          ByRef isError As Boolean, _
                                          ByVal sError As String)
        Dim value As String
        Try
            value = row(colName).ToString.Trim
            row(colName) = value
            If Validator.EmptyValue(value) Then
                rowError(colName) += sError
                isError = True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Shared Sub IsValidTime(ByVal colName As String, _
                                          ByRef row As DataRow, _
                                          ByRef rowError As DataRow, _
                                          ByRef isError As Boolean, _
                                          ByVal sError As String)
        Dim value As String
        Try
            value = row(colName).ToString.Trim
            row(colName) = value
            If Validator.EmptyValue(value) Then
                rowError(colName) += sError
                isError = True
                Exit Sub
            End If
            Dim hour = value.Split(":")(0)
            Dim minute = value.Split(":")(1)
            If Validator.EmptyValue(hour) Then
                rowError(colName) += sError
                isError = True
                Exit Sub
            End If
            If Validator.EmptyValue(minute) Then
                rowError(colName) += sError
                isError = True
                Exit Sub
            End If
            If hour > 23 Or hour < 0 Then
                rowError(colName) += sError
                isError = True
                Exit Sub
            End If
            If minute > 60 Or minute < 0 Then
                rowError(colName) += sError
                isError = True
                Exit Sub
            End If
            If IsNumeric2(hour) Then
                rowError(colName) += sError
                isError = True
            End If
            If IsNumeric2(minute) Then
                rowError(colName) += sError
                isError = True
            End If
        Catch ex As Exception
            rowError(colName) += sError
            isError = True
        End Try
    End Sub
    Public Shared Function TrimRow(ByRef row As DataRow) As Boolean
        Dim value As String
        Dim colName As String
        Dim isRow As Boolean = False
        Try
            Dim dtData As DataTable = row.Table
            For Each col As DataColumn In dtData.Columns
                colName = col.ColumnName
                value = row(colName).ToString.Trim
                row(colName) = value
                If value <> "#N/A" And value <> "" Then
                    isRow = True
                End If
            Next
            Return isRow
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub AddError(ByVal colName As String, _
                               ByRef row As DataRow, _
                               ByRef rowError As DataRow, _
                               ByRef isError As Boolean, _
                               ByVal sError As String)

        Try
            rowError(colName) += sError
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class

Module Validator
    Public Function EmptyValue(ByVal s As String) As Boolean
        'return true if s is null string
        Return (s.Trim() Is Nothing OrElse s.Trim().Length = 0)
    End Function

    Public Function IsValidEmail(ByVal strIn As String) As Boolean
        ' Return true if strIn is in valid e-mail format.
        If Not EmptyValue(strIn) Then
            Return Regex.IsMatch(strIn, "^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
        Else
            Return True
        End If
    End Function

    Public Function IsAlphaNumeric(ByVal s As String) As Boolean
        If Not EmptyValue(s) Then
            Dim chars As Char() = s.ToCharArray()
            For Each c As Char In chars
                If Regex.IsMatch(c.ToString(), "^[-_,A-Za-z0-9]$") Then
                    Return True
                End If
            Next
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IsNumeric2(ByVal s As String) As Boolean
        If Not EmptyValue(s) Then
            Dim chars As Char() = s.ToCharArray()
            For Each c As Char In chars
                If Regex.IsMatch(c.ToString(), "^[0-9]*$") Then
                    Return False
                End If
            Next
            Return True
        Else
            Return False
        End If

    End Function

    Public Function mf_KiemtraURL(ByVal sv_Email As String) As Boolean
        Try
            If mf_blength(sv_Email) = False Then
                Return True
            End If
            If mf_Kiemtrakytudacbiet(sv_Email, "-,@,.,_") Then
                Return False
            End If

            Dim sl As Integer = 0

            sl = 0
            For i As Integer = 0 To sv_Email.Length - 1
                If sv_Email.Substring(i, 1) = "." Then
                    sl += 1
                End If
            Next

            If sl > 2 Then
                Return False
            End If

            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Function mf_Kiemtrakytudacbiet(ByVal _sKytu As String, Optional ByVal sv_ExceptionCharacter As String = "") As Boolean
        Try
            If _sKytu.IndexOf("@", 0) >= 0 Or _sKytu.IndexOf("#", 0) >= 0 Or _sKytu.IndexOf("$", 0) >= 0 Or _sKytu.IndexOf("%", 0) >= 0 Or _sKytu.IndexOf("!", 0) >= 0 Or _sKytu.IndexOf("^", 0) >= 0 Or _sKytu.IndexOf("&", 0) >= 0 _
                        Or _sKytu.IndexOf("*", 0) >= 0 Or _sKytu.IndexOf("(", 0) >= 0 Or _sKytu.IndexOf(")", 0) >= 0 Or _sKytu.IndexOf("+", 0) >= 0 Or _sKytu.IndexOf("=", 0) >= 0 Or _sKytu.IndexOf("<", 0) >= 0 Or _sKytu.IndexOf(">", 0) >= 0 Then
                If mf_ObjectIsNull(sv_ExceptionCharacter) = False Then
                    Dim sv_listKytudacbiet As String = "!,@,#,$,%,^,&,*,(,),=,+,{,},[,],|,\,',/,?,>,<"
                    Dim _sv_sValues As String() = sv_listKytudacbiet.Split(",")
                    sv_listKytudacbiet = ""
                    For kk As Integer = 0 To _sv_sValues.Length - 1
                        If sv_ExceptionCharacter.IndexOf(mf_ObjectToString(_sv_sValues(kk)), 0) < 0 Then
                            sv_listKytudacbiet += mf_ObjectToString(_sv_sValues(kk)) & ","
                        End If
                    Next
                    sv_listKytudacbiet = sv_listKytudacbiet.Trim(",")
                    _sv_sValues = sv_listKytudacbiet.Split(",")
                    For ii As Integer = 0 To _sv_sValues.Length - 1
                        If _sKytu.IndexOf(mf_ObjectToString(_sv_sValues(ii)), 0) >= 0 Then
                            Return True
                            Exit For
                        End If
                    Next
                Else
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function mf_blength(ByVal obj As Object) As Boolean
        Try
            Dim flag As Boolean = False
            If obj Is Nothing Then Return False
            If TypeOf obj Is DataTable Then
                If CType(obj, DataTable).Rows.Count > 0 Then
                    flag = True
                End If
            ElseIf TypeOf obj Is DataRow() Then
                If CType(obj, DataRow()).Length > 0 Then
                    flag = True
                End If
            ElseIf TypeOf obj Is DataSet Then
                If CType(obj, DataSet) IsNot Nothing Then
                    If CType(obj, DataSet).Tables(0).Rows.Count > 0 Then
                        flag = True
                    End If
                End If
            ElseIf TypeOf obj Is DataRow Then
                If CType(obj, DataRow) IsNot Nothing Then
                    flag = True
                End If
            Else
                If (mf_ObjectToString(obj).Length > 0) Then
                    flag = True
                End If
            End If
            Return flag
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function mf_ObjectToString(ByVal obj As Object) As String
        Try
            If obj Is Nothing Then Return ""
            Return obj.ToString().Trim()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function mf_ObjectIsNull(ByVal obj As Object) As Boolean
        Try
            If obj Is Nothing Then
                Return True
            ElseIf mf_blength(obj) = False Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Module