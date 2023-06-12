Public Class APPOINTMENT_DTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal?
    Public Property ORG_ID As Decimal?
    Public Property TITLE_ID As Decimal?
    Public Property EMPLOYEECODE As String
    Public Property EMPLOYEENAME As String

    Public Property SIGN_ID As Decimal?
    Public Property SIGN_CODE As String
    Public Property SIGN_NAME As String
    Public Property GSIGN_CODE As String

    Public Property WORKINGDAY As Date?
    Public Property NVALUE As Decimal?
    Public Property TYPE_LEAVE As String
    Public Property NVALUE_NAME As String

    Public Property SUBJECT As String
    Public Property NOTE As String

    Public Property RecurrenceRule As String
    Public Property RecurrenceParentID As String

    Public Property IS_INSERT As Boolean
    Public Property IS_DELETE As Boolean
    Public Property IS_SEND_MAIL As Boolean
    Public Property IS_NGT As Boolean
    Public Property IS_EDITSHIFT As Boolean
    Public Property IS_Portal As Boolean
    Public Property IS_OT As Integer
    Public Property JOIN_DATE As Date?
    '1: Không check ca đêm, giờ bắt đầu < giờ kết thúc
    '2: Không check ca đêm, giờ bắt đầu > giờ kết thúc
    '3: check ca đêm
    Public Property TYPE_OT As Integer
    Public Property ID_GROUPREG As Decimal?
    Public Property STATUS As Short
    Public Property IS_CNB As Boolean?

    Public Property FROM_HOUR As Date?
    Public Property TO_HOUR As Date?
End Class
