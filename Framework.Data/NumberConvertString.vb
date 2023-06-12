Public Class NumberConvertString
    Implements IDisposable
    Private strSo() As String = {"không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín"}
    Private strDonViNho() As String = {"linh", "lăm", "mười", "mươi", "mốt", "trăm"}
    Private strDonViLon() As String = {"", "nghìn", "triệu", "tỷ"}
    Private strMainGroup() As String
    Private strSubGroup() As String
    Private Function Len1(ByVal strA As String) As String
        Try
            Return strSo(Integer.Parse(strA))
        Catch
            Return "0"
        End Try
    End Function
    Private Function Len2(ByVal strA As String) As String
        If strA.Substring(0, 1) = "0" Then
            Return strDonViNho(0) & " " & Len1(strA.Substring(1, 1))
        ElseIf strA.Substring(0, 1) = "1" Then
            If strA.Substring(1, 1) = "5" Then
                Return strDonViNho(2) & " " & strDonViNho(1)
            ElseIf strA.Substring(1, 1) = "0" Then
                Return strDonViNho(2)
            Else
                Return strDonViNho(2) & " " & Len1(strA.Substring(1, 1))
            End If
        Else
            If strA.Substring(1, 1) = "5" Then
                Return Len1(strA.Substring(0, 1)) & " " & strDonViNho(3) & " " & strDonViNho(1)
            ElseIf strA.Substring(1, 1) = "0" Then
                Return Len1(strA.Substring(0, 1)) & " " & strDonViNho(3)
            ElseIf strA.Substring(1, 1) = "1" Then
                Return Len1(strA.Substring(0, 1)) & " " & strDonViNho(3) & " " & strDonViNho(4)
            Else
                Return Len1(strA.Substring(0, 1)) & " " & strDonViNho(3) & " " & Len1(strA.Substring(1, 1))
            End If
        End If
    End Function
    Private Function Len3(ByVal strA As String) As String
        If (strA.Substring(0, 3) = "000") Then
            Return Nothing
        ElseIf (strA.Substring(1, 2) = "00") Then
            Return Len1(strA.Substring(0, 1)) & " " & strDonViNho(5)
        Else
            Return Len1(strA.Substring(0, 1)) & " " & strDonViNho(5) & " " & Len2(strA.Substring(1, strA.Length - 1))
        End If
    End Function
    '///////////////////
    Private Function FullLen(ByVal strSend As String) As String
        Dim boKTNull As Boolean = False
        Dim strKQ As String = ""
        Dim strA As String = strSend.Trim()
        Dim iIndex As Integer = strA.Length - 9
        Dim iPreIndex As Integer = 0

        If strSend.Trim() = "" Then
            Return Len1("0")
        End If
        'tra ve khong neu la khong
        For i As Integer = 0 To strA.Length - 1
            If strA.Substring(i, 1) <> "0" Then
                Exit For
            ElseIf i = strA.Length - 1 Then
                Return strSo(0)
            End If
        Next i
        Dim k As Integer = 0
        Dim tempVar As Boolean = strSend.Trim().Substring(k, 1) = "0"
        k += 1
        Do While tempVar
            strA = strA.Remove(0, 1)
            tempVar = strSend.Trim().Substring(k, 1) = "0"
            k += 1
        Loop
        '
        If strA.Length < 9 Then
            iPreIndex = strA.Length
        End If
        '
        If (strA.Length Mod 9) <> 0 Then
            strMainGroup = New String(strA.Length \ 9) {}
        Else
            strMainGroup = New String(strA.Length \ 9 - 1) {}
        End If
        'nguoc
        For i As Integer = strMainGroup.Length - 1 To 0 Step -1
            If iIndex >= 0 Then
                iPreIndex = iIndex
                strMainGroup(i) = strA.Substring(iIndex, 9)
                iIndex -= 9
            Else
                strMainGroup(i) = strA.Substring(0, iPreIndex)
            End If

        Next i
        '///////////////////////////////
        'tach moi maingroup thanh nhieu subgroup
        'xuoi
        For j As Integer = 0 To strMainGroup.Length - 1
            'gan lai gia tri
            iIndex = strMainGroup(j).Length - 3
            If strMainGroup(j).Length < 3 Then
                iPreIndex = strMainGroup(j).Length
            End If
            '''
            If (strMainGroup(j).Length Mod 3) <> 0 Then
                strSubGroup = New String(strMainGroup(j).Length \ 3) {}
            Else
                strSubGroup = New String(strMainGroup(j).Length \ 3 - 1) {}
            End If
            For i As Integer = strSubGroup.Length - 1 To 0 Step -1
                If iIndex >= 0 Then
                    iPreIndex = iIndex
                    strSubGroup(i) = strMainGroup(j).Substring(iIndex, 3)
                    iIndex -= 3
                Else
                    strSubGroup(i) = strMainGroup(j).Substring(0, iPreIndex)
                End If
            Next i
            'duyet subgroup de lay string
            For i As Integer = 0 To strSubGroup.Length - 1
                boKTNull = False 'phai de o day
                If (j = strMainGroup.Length - 1) AndAlso (i = strSubGroup.Length - 1) Then
                    If strSubGroup(i).Length < 3 Then
                        If strSubGroup(i).Length = 1 Then
                            strKQ &= Len1(strSubGroup(i))
                        Else
                            strKQ &= Len2(strSubGroup(i))
                        End If
                    Else
                        strKQ &= Len3(strSubGroup(i))
                    End If
                Else
                    If strSubGroup(i).Length < 3 Then
                        If strSubGroup(i).Length = 1 Then
                            strKQ &= Len1(strSubGroup(i)) & " "
                        Else
                            strKQ &= Len2(strSubGroup(i)) & " "
                        End If
                    Else
                        If Len3(strSubGroup(i)) Is Nothing Then
                            boKTNull = True
                        Else
                            strKQ &= Len3(strSubGroup(i)) & " "
                        End If
                    End If
                End If
                'dung
                If Not boKTNull Then
                    If strSubGroup.Length - 1 - i <> 0 Then
                        strKQ &= strDonViLon(strSubGroup.Length - 1 - i) & ", "
                    Else
                        strKQ &= strDonViLon(strSubGroup.Length - 1 - i) & " "
                    End If

                End If
            Next i
            'dung
            If j <> strMainGroup.Length - 1 Then
                If Not boKTNull Then
                    strKQ = strKQ.Substring(0, strKQ.Length - 1) & strDonViLon(3) & ", "
                Else
                    strKQ = strKQ.Substring(0, strKQ.Length - 1) & " " & strDonViLon(3) & ", "
                End If
            End If
        Next j
        'xoa ky tu trang
        strKQ = strKQ.Trim()
        'xoa dau , neu co
        If strKQ.Substring(strKQ.Length - 1, 1) = "." Then
            strKQ = strKQ.Remove(strKQ.Length - 1, 1)
        End If
        Return strKQ

        '//////////////////////////////////


    End Function
    Public Function ConvertNumber(ByVal strSend As String,
                                  Optional ByVal charInSeparator As Char = ".",
                                  Optional ByVal strOutSeparator As String = "phẩy",
                                  Optional ByVal isSeparator As Boolean = False) As String
        If strOutSeparator = "" Then
            Return "Lỗi dấu phân cách đầu ra rỗng"
        End If
        If strSend = "" Then
            Return Len1("0")
        End If

        Dim strTmp(1) As String
        Try
            Dim strTmpRight As String
            If isSeparator Then
                strTmp = strSend.Split(charInSeparator)
                strTmpRight = strTmp(1)
            Else
                strTmpRight = ""
                strTmp(0) = strSend
            End If

            For i As Integer = strTmpRight.Length - 1 To 0 Step -1
                If strTmpRight.Substring(i, 1) = "0" Then
                    strTmpRight = strTmpRight.Remove(i, 1)
                Else
                    Exit For
                End If
            Next i
            If strTmpRight <> "" Then
                Dim strRight As String = ""
                For i As Integer = 0 To strTmpRight.Length - 1
                    strRight &= Len1(strTmpRight.Substring(i, 1)) & " "
                Next i


                Return FullLen(strTmp(0)) & " " & strOutSeparator & " " & strRight.TrimEnd()
            Else
                Return FullLen(strTmp(0))
            End If
        Catch
            Return FullLen(strTmp(0))
        End Try

    End Function

    ' To detect redundant calls
    Private disposed As Boolean = False

    ' IDisposable
    Protected Overridable Sub Dispose( _
       ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                ' Free other state (managed objects).
            End If
            ' Free your own state (unmanaged objects).
            ' Set large fields to null.
        End If
        Me.disposed = True
    End Sub

    ' This code added by Visual Basic to 
    ' correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. 
        ' Put cleanup code in
        ' Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code. 
        ' Put cleanup code in
        ' Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

End Class
