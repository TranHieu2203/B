Public Class EmployeeTrainDTO
    Public Property ID As Decimal
    Public Property EMPLOYEE_ID As Decimal
    Public Property FROM_DATE As Date?

    Public Property FMONTH As Decimal
    Public Property FYEAR As Decimal
    Public Property TMONTH As Decimal
    Public Property TYEAR As Decimal

    Public Property TO_DATE As Date?
    Public Property SCHOOL_NAME As String
    Public Property TRAINING_FORM As Decimal?

    Public Property TRAINING_FORM_NAME As String

    Public Property HIGHEST_LEVEL As Boolean
    Public Property LEARNING_LEVEL As Decimal?

    Public Property LEARNING_LEVEL_NAME As String

    Public Property MAJOR As Decimal?

    Public Property MAJOR_NAME As String

    Public Property GRADUATE_YEAR As Decimal
    Public Property MARK As Decimal?

    Public Property MARK_NAME As String

    Public Property TRAINING_CONTENT As String
    Public Property CREATED_DATE As Date
    Public Property CREATED_BY As String
    Public Property CREATED_LOG As String
    Public Property MODIFIED_DATE As Date
    Public Property MODIFIED_BY As String
    Public Property MODIFIED_LOG As String
End Class