Option Strict On
Option Explicit On

Namespace HistaffFramework.ServiceContracts

    ''' <summary>
    ''' Framework này cung cấp các hàm kết nối database Oracle
    ''' </summary>
    ''' <remarks></remarks>
    <ServiceContract()>
    Public Interface IHistaffFramework

#Region "Oracle Data Access"

#Region "Histaff Framework Version 2.0"

        ''' <summary>
        ''' Dll: Oracle.DataAccess.Client
        ''' Get danh sách tham số của 1 StoreProcedure
        ''' </summary>
        ''' <param name="storeName">Tên store Procedure</param>
        ''' <param name="isInParameterOnly">True: Chỉ lấy danh sách tham số In, False: lấy tất cả tham số</param>
        ''' <returns>Datatable danh sách tham số</returns>
        ''' <remarks>Histaff Framework 2.0</remarks>
        <OperationContract()>
        Function GetSpParameterSet(ByVal storeName As String, Optional isInParameterOnly As Boolean = False) As DataTable

        ''' <summary>
        ''' Dll: Oracle.DataAccess.Client
        ''' Thực thi Store Procedure trả về 1 DataSet (1 hoặc nhiều datatable) 
        ''' Thứ tự value trong parameterValue phải đúng với thứ tự tham số Input (không quan tâm vị trí tham số Output) được khái báo trong Store Procedure
        ''' Không truyền biến Out Cursor
        ''' Không Output biến Scalar
        ''' Không cần khai báo ParameterName 
        ''' Có sử dụng Cache
        ''' </summary>
        ''' <param name="storeName">Tên Store Procedure Oracle</param>
        ''' <param name="parameterValue">Danh sách value truyền vào để thực thi Store Procedure</param>
        ''' <returns>Dataset (không return biến Output)</returns>
        ''' <remarks>Histaff Framework 2.0</remarks>
        <OperationContract()>
        Function ExecuteToDataSet(ByVal storeName As String, Optional ByVal parameterValue As List(Of Object) = Nothing) As DataSet

        ''' <summary>
        ''' Dll: Oracle.DataAccess.Client
        ''' Thực thi Store Procedure Oracle trả về mảng cái tham số Output là Scalar (không phải Cursor)
        ''' Có sử dụng Cache
        ''' </summary>
        ''' <param name="storeName">Tên store procedure</param>
        ''' <param name="parameters">List value các tham số Input để thực thi Store</param>
        ''' <returns>Mảng các giá trị output</returns>
        ''' <remarks>Histaff Framework 2.0</remarks>
        <OperationContract()>
        Function ExecuteStoreScalar(ByVal storeName As String, ByVal parameters As List(Of Object)) As Object()

        ''' <summary>
        ''' Update/Insert 1 DataTable lên database Mở kết nối => duyệt từng rows cho đến khi hết row thì => đóng kết nối
        ''' Có transaction
        ''' </summary>
        ''' <param name="storeName">Tên Store Produre</param>
        ''' <param name="dtb">Datatable chứa dữ liệu update/insert, Tên Cột là tên tham số, giá trị từng Cell trên 1 row là value tương ứng</param>
        ''' <returns>Số dùng transaction thành công</returns>
        ''' <remarks>Histaff Framework 2.0</remarks>
        <OperationContract()>
        Function ExecuteBatchCommand(ByVal storeName As String, ByVal dtb As DataTable) As Integer
#End Region

#Region "Histaff Framework Version 1.5"
        ''' <summary>
        ''' Execute Store Procedure chỉ truyền vào List Value của Parameters
        ''' Không dùng cache
        ''' </summary>
        ''' <param name="storeName">Tên store procedure</param>
        ''' <param name="parameters">List Value tham số In hoặc Out truyền vào</param>
        ''' <returns></returns>
        ''' <remarks>Histaff Framework 1.5</remarks>
        <OperationContract()>
        Function ExecuteStore1_5(ByVal storeName As String, ByVal parameters As List(Of Object)) As DataSet

        ''' <summary>
        ''' Execute Store Procedure return DataTable or DataSet. Fix bug Out parameters in Function ExecuteStore 
        ''' </summary>
        ''' <param name="parameters">List Parameters (Parameter Name in procedure, Value)</param>
        ''' <param name="storeName">StoreProcedure </param>
        ''' <param name="isHasOutParameter">Có tham số OUTPUT trả về hay không, Trừ OUT_CUROR </param>
        ''' <returns>DataTable or DataSet and parameters</returns>
        ''' <remarks>Histaff Framework 1.5</remarks>
        <OperationContract()>
        Function ExecuteStoreOutParameters(ByVal storeName As String, ByVal parameters As List(Of ArrayList), Optional ByVal isHasOutParameter As Boolean = False) As DataSet
#End Region

#Region "Histaff Framework Version 1.0"
        ''' <summary>
        ''' Execute Store Procedure return DataTable or DataSet. Out param will return in parameters
        ''' </summary>
        ''' <param name="parameters">List Parameters (Parameter Name in procedure, Value)</param>
        ''' <param name="storeName">StoreProcedure </param>
        ''' <returns>DataTable or DataSet and parameters</returns>
        ''' <remarks>Histaff Framework 1.0</remarks>
        <OperationContract()>
        Function ExecuteStore(ByVal storeName As String, ByVal parameters As List(Of ArrayList)) As DataSet

        ''' <summary>
        ''' Execute Store Procedure Insert/Update/Delete
        ''' </summary>
        ''' <param name="storeName">StoreName</param>
        ''' <param name="parameters">List Parameters</param>
        ''' <returns>Row(s) commited</returns>
        ''' <remarks>Histaff Framework 1.0</remarks>
        <OperationContract()>
        Function ExecuteStoreNonQuery(ByVal storeName As String, ByVal parameters As List(Of ArrayList)) As Integer
#End Region

#End Region

    End Interface

End Namespace
