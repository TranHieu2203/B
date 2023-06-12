'----------------------------------------------------------------------------------------------
'Purpose : Thiet lap cac Function, Sub dung chung cho toan he thong UI
'Create By : TienDT
'Create Date : 01/11/2006
'----------------------------------------------------------------------------------------------

Module mdlUICommonFunction
    Public gv_SDK As zkemkeeper.CZKEM ' biến SDK kết nối máy chấm công
    Public gv_Initialize As Boolean = False ' biến kiểm tra trạng thái khởi tạo của biến SDK

    Public Sub gs_InitializeSDK()
        Try
            If gv_Initialize = False Then
                gv_SDK = New zkemkeeper.CZKEM
                gv_Initialize = True
            Else
                gv_SDK.Disconnect()
                gv_SDK = New zkemkeeper.CZKEM
            End If
        Catch ex As Exception
            WriteEventsLog(String.Format("[{0}] - {1}", ex.TargetSite.Name, ex.Message))
        End Try
    End Sub
    Public Function CanConnect_MCC(mv_IP As String) As Boolean

        Try
            gs_InitializeSDK()
            If mv_IP <> "" Then
                If gv_SDK.Connect_Net(mv_IP, 4370) Then
                    Return True
                Else
                    Return False
                End If
                gv_SDK.Disconnect()
                gv_Initialize = False
            End If
        Catch ex As Exception
            gv_SDK.Disconnect()
            gv_Initialize = False
            Return False
        Finally

        End Try
    End Function
End Module

