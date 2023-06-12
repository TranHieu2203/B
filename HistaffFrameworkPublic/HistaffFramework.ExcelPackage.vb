Option Strict On
Option Explicit On

Imports System.IO
Imports Excel

Public Class ExcelPackage
    Implements IDisposable

    Public Shared Function ReadExcelToDataSet(ByVal filePath As String, Optional ByVal isFirstRowAsColumnNames As Boolean = False) As DataSet
        Dim ds As New DataSet
        If Not String.IsNullOrEmpty(filePath) Then
            Dim fi As New FileInfo(filePath)
            If fi.Exists Then
                Dim stream As FileStream
                Try
                    stream = File.Open(filePath, FileMode.Open, FileAccess.Read)
                Catch ex As Exception
                    Throw ex
                End Try

                Dim excelReader As IExcelDataReader
                excelReader = Nothing
                '1. Reading from a binary Excel file ('97-2003 format; *.xls)
                If fi.Extension.ToUpper().Equals(".XLS") Then
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream)
                End If

                '2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                If fi.Extension.ToUpper().Equals(".XLSX") Then
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                End If

                If excelReader IsNot Nothing Then
                    '4. DataSet - Create column names from first row
                    excelReader.IsFirstRowAsColumnNames = isFirstRowAsColumnNames
                    ds = excelReader.AsDataSet()
                End If
            End If
        End If
        Return ds
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
