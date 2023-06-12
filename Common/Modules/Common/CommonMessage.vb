﻿Public Class CommonMessage
    Public Shared TOOLBARITEM_SEARCH As String = "SEARCH"
    Public Shared TOOLBARITEM_CREATE As String = "CREATE"
    Public Shared TOOLBARITEM_EDIT As String = "EDIT"
    Public Shared TOOLBARITEM_DELETE As String = "DELETE"
    Public Shared TOOLBARITEM_SAVE As String = "SAVE"
    Public Shared TOOLBARITEM_CANCEL As String = "CANCEL"
    Public Shared TOOLBARITEM_PRINT As String = "PRINT"
    Public Shared TOOLBARITEM_IMPORT As String = "IMPORT"
    Public Shared TOOLBARITEM_EXPORT As String = "EXPORT"
    Public Shared TOOLBARTIEM_CALCULATE As String = "CALCULATE"
    Public Shared TOOLBARITEM_APPROVE As String = "APPROVE"
    Public Shared TOOLBARITEM_UNAPPROVE As String = "UNAPPROVE"
    Public Shared TOOLBARITEM_REJECT As String = "REJECT"
    Public Shared TOOLBARITEM_ACTIVE As String = "ACTIVE"
    Public Shared TOOLBARITEM_DEACTIVE As String = "DEACTIVE"
    Public Shared TOOLBARITEM_NEXT As String = "NEXT"
    Public Shared TOOLBARITEM_PREVIOUS As String = "PREVIOUS"
    Public Shared TOOLBARITEM_ATTACH As String = "ATTACH"
    Public Shared TOOLBARITEM_SUBMIT As String = "SUBMIT"
    Public Shared TOOLBARITEM_DECLARE As String = "DECLARE"
    Public Shared TOOLBARITEM_EXTEND As String = "EXTEND"
    Public Shared TOOLBARITEM_DETAIL As String = "DETAIL"
    Public Shared TOOLBARITEM_CHECK As String = "CHECK"
    Public Shared TOOLBARITEM_REFRESH As String = "REFRESH"
    Public Shared TOOLBARITEM_SYNC As String = "SYNC"
    Public Shared TOOLBARITEM_UNLOCK As String = "UNLOCK"
    Public Shared TOOLBARITEM_LOCK As String = "LOCK"
    Public Shared TOOLBARITEM_RESET As String = "RESET_PASSWORD"
    Public Shared TOOLBARITEM_SENDMAIL As String = "SENDMAIL"
    Public Shared TOOLBARITEM_HIEXT As String = "HIEXT"
    Public Shared TOOLBARITEM_HIUPDATEINFO As String = "HIUPDATEINFO"
    Public Shared TOOLBARITEM_CREATE_BATCH As String = "CREATE_BATCH"
    Public Shared TOOLBARITEM_APPROVE_OPEN As String = "APPROVE_OPEN" '  Mở phê duyệt
    Public Shared TOOLBARITEM_EXPORT_TEMPLATE As String = "EXPORT_TEMPLATE" 'ThanhNT added 04072016
    Public Shared TOOLBARITEM_APPROVE_BATCH As String = "APPROVE_BATCH" 'ThanhNT added 04072016
    Public Shared TOOLBARITEM_VIEW As String = "VIEW" 'ThanhNT added 04072016
    Public Shared TOOLBARITEM_KILL As String = "KILL"

    Public Shared AUTHORIZE_CREATE As String = "Create"
    Public Shared AUTHORIZE_MODIFY As String = "Modify"
    Public Shared AUTHORIZE_DELETE As String = "Delete"
    Public Shared AUTHORIZE_KILL As String = "Kill"
    Public Shared AUTHORIZE_PRINT As String = "Print"
    Public Shared AUTHORIZE_IMPORT As String = "Import"
    Public Shared AUTHORIZE_EXPORT As String = "Export"
    Public Shared AUTHORIZE_SPECIAL1 As String = "Special1"
    Public Shared AUTHORIZE_SPECIAL2 As String = "Special2"
    Public Shared AUTHORIZE_SPECIAL3 As String = "Special3"
    Public Shared AUTHORIZE_SPECIAL4 As String = "Special4"
    Public Shared AUTHORIZE_SPECIAL5 As String = "Special5"
    Public Shared AUTHORIZE_RESET As String = "Reset"

    Public Shared STATE_NEW As String = "New"
    Public Shared STATE_EDIT As String = "Edit"
    Public Shared STATE_DELETE As String = "Delete"
    Public Shared STATE_APPROVE As String = "Approve"
    Public Shared STATE_REJECT As String = "Reject"
    Public Shared STATE_ACTIVE As String = "Active"
    Public Shared STATE_DEACTIVE As String = "DeActive"
    Public Shared STATE_NORMAL As String = "Normal"
    Public Shared STATE_CANCEL As String = "Cancel"
    Public Shared STATE_DECLARE As String = "Declare"
    Public Shared STATE_EXTEND As String = "Extend"
    Public Shared STATE_DETAIL As String = "Detail"
    Public Shared STATE_CHECK As String = "Check"
    Public Shared STATE_KILL As String = "Kill"

    Public Shared ACTION_SAVED As String = "SAVED"
    Public Shared ACTION_INSERTED As String = "INSERTED"
    Public Shared ACTION_UPDATED As String = "UPDATED"
    Public Shared ACTION_UPDATING As String = "UPDATING"
    Public Shared ACTION_DELETED As String = "DELETED"
    Public Shared ACTION_OK As String = "OK"
    Public Shared ACTION_CANCEL As String = "CANCEL"
    Public Shared ACTION_ERROR As String = "ERROR"
    Public Shared ACTION_SUCCESS As String = "SUCCESS"
    Public Shared ACTION_UNSUCCESS As String = "UNSUCCESS"
    Public Shared ACTION_LOCKED As String = "LOCKED"
    Public Shared ACTION_UNLOCKED As String = "UNLOCKED"
    Public Shared ACTION_ACTIVE As String = "ACTIVE"
    Public Shared ACTION_APPROVE As String = "APPROVE"
    Public Shared ACTION_DEACTIVE As String = "DEACTIVE"
    Public Shared ACTION_SYNC As String = "SYNC"
    Public Shared ACTION_RESET As String = "RESET"
    Public Shared ACTION_ID_NO As String = "ID_NO"
    Public Shared ACTION_KILL As String = "KILLED"
    Public Shared ACCESS_DENIED = "ACCESS_DENIED"

    Public Shared MESSAGE_NUMBER_EXPIRE = "MESSAGE_NUMBER_EXPIRE"
    Public Shared MESSAGE_NOT_SELECT_ROW = "MESSAGE_NOT_SELECT_ROW"
    Public Shared MESSAGE_NOT_EXISTS_EXAMS = "Vui lòng tạo các vòng phỏng vấn trước khi chọn ứng viên"
    Public Shared MESSAGE_NOT_ROW = "MESSAGE_NOT_ROW"
    Public Shared MESSAGE_DATA_EMPTY = "MESSAGE_DATA_EMPTY"
    Public Shared MESSAGE_NOT_SELECT_MULTI_ROW = "Không thể chỉnh sửa nhiều dòng cùng lúc"
    Public Shared MESSAGE_CONFIRM_LOCK = "MESSAGE_CONFIRM_LOCK"
    Public Shared MESSAGE_CONFIRM_UNLOCK = "MESSAGE_CONFIRM_UNLOCK"
    Public Shared MESSAGE_CONFIRM_SYNC = "MESSAGE_CONFIRM_SYNC"
    Public Shared MESSAGE_CONFIRM_DELETE = "MESSAGE_CONFIRM_DELETE"
    Public Shared MESSAGE_CONFIRM_APPROVE = "Bạn có muốn gửi phê duyệt"
    Public Shared MESSAGE_APPROVE = "Bạn có muốn phê duyệt"
    Public Shared MESSAGE_CONFIRM_UNAPPROVE = "MESSAGE_CONFIRM_UNAPPROVE"
    Public Shared MESSAGE_CONFIRM_REJECT = "MESSAGE_CONFIRM_REJECT"
    Public Shared MESSAGE_CONFIRM_ACCEPT = "MESSAGE_CONFIRM_ACCEPT"
    Public Shared MESSAGE_CONFIRM_ACTIVE = "MESSAGE_CONFIRM_ACTIVE"
    Public Shared MESSAGE_CONFIRM_DEACTIVE = "MESSAGE_CONFIRM_DEACTIVE"
    Public Shared MESSAGE_CONFIRM_DELETE_TITLE = "MESSAGE_CONFIRM_DELETE_TITLE"
    Public Shared MESSAGE_CONFIRM_ACTIVE_TITLE = "MESSAGE_CONFIRM_ACTIVE_TITLE"
    Public Shared MESSAGE_CONFIRM_DEACTIVE_TITLE = "MESSAGE_CONFIRM_DEACTIVE_TITLE"
    Public Shared MESSAGE_NOT_SELECT_ORG = "MESSAGE_NOT_SELECT_ORG"
    Public Shared MESSAGE_TRANSACTION_SUCCESS As String = "MESSAGE_TRANSACTION_SUCCESS"
    Public Shared MESSAGE_TRANSACTION_FAIL As String = "MESSAGE_TRANSACTION_FAIL"
    Public Shared MESSAGE_EXIST_CODE As String = "MESSAGE_EXIST_CODE"
    Public Shared MESSAGE_IS_EFFECT_NOT_EDIT As String = "MESSAGE_IS_EFFECT_NOT_EDIT"
    Public Shared MESSAGE_IS_CONFIRM_EDIT_SAVE As String = "MESSAGE_IS_CONFIRM_EDIT_SAVE"
    Public Shared MESSAGE_IS_EFFECT_NOT_DELETE As String = "MESSAGE_IS_EFFECT_NOT_DELETE"
    Public Shared MESSAGE_IS_USING As String = "MESSAGE_IS_USING"
    Public Shared MESSAGE_APPROVED_WANRNING = "MESSAGE_APPROVED_WANRNING"
    Public Shared MESSAGE_CONFIRM_ID_NO = "MESSAGE_CONFIRM_ID_NO"
    Public Shared MESSAGE_EXIST_INFOR = "MESSAGE_EXIST_INFOR"
    Public Shared MESSAGE_RESET_USER = "MESSAGE_RESET_USER"
    Public Shared EXCEPTION As String = "EXCEPTION"

    Public Shared VIEW_TITLE_NEW As String = "VIEW_TITLE_NEW"
    Public Shared VIEW_TITLE_EDIT As String = "VIEW_TITLE_EDIT"


    Public Shared MESSAGE_NOTSELECT_PERIOD = "MESSAGE_NOTSELECT_PERIOD"
    Public Shared MESSAGE_EMAIL_ISEMPTY = "MESSAGE_EMAIL_ISEMPTY"
    Public Shared MESSAGE_SENDMAIL_ERROR = "MESSAGE_SENDMAIL_ERROR"
    Public Shared MESSAGE_SENDMAIL_COMPLETED = "MESSAGE_SENDMAIL_COMPLETED"
    Public Shared MESSAGE_WARNING_EXIST_DATABASE As String = "MESSAGE_WARNING_EXIST_DATABASE"
    Public Shared MESSAGE_WARNING_EXPORT_EMPTY As String = "MESSAGE_WARNING_EXPORT_EMPTY"
    Public Shared MESSAGE_ERROR_EXIST_FOLDER As String = "MESSAGE_ERROR_EXIST_FOLDER"
    Public Shared MESSAGE_TEXT_IS_HTML As String = "MESSAGE_TEXT_IS_HTML"
    Public Shared MESSAGE_NOT_EXSIXT_FILE As String = "MESSAGE_NOT_EXSIXT_FILE"
    Public Shared MESSAGE_NOT_CHOOSE_MACHINE_TYPE As String = "MESSAGE_NOT_CHOOSE_MACHINE_TYPE"
    Public Shared FILE_ISNOT_FORMAT As String = "FILE_ISNOT_FORMAT"

    Public Shared CM_CTRLPROGRAMS_IS_EXSIST_FILE_NOT_DELETE As String = "CM_CTRLPROGRAMS_IS_EXSIST_FILE_NOT_DELETE"
    Public Shared CM_CTRLPROGRAMS_IS_NOT_SELECTED_MODULE As String = "CM_CTRLPROGRAMS_IS_NOT_SELECTED_MODULE"
    Public Shared CM_CTRLPROGRAMS_IS_NOT_EXSIST_TEMPLATE_FILE As String = "CM_CTRLPROGRAMS_IS_NOT_EXSIST_TEMPLATE_FILE"
    Public Shared CM_CTRLPROGRAMS_VALIDATE_NUM_FILE_UPLOAD As String = "CM_CTRLPROGRAMS_VALIDATE_NUM_FILE_UPLOAD"
    Public Shared CM_CTRLPROGRAMS_VALIDATE_EXTENTION_FILE As String = "CM_CTRLPROGRAMS_VALIDATE_EXTENTION_FILE"
    Public Shared CM_CTRLPROGRAMS_NOT_CHOOSE_FILE As String = "CM_CTRLPROGRAMS_NOT_CHOOSE_FILE"
    Public Shared CM_CTRLPROGRAMS_NOT_EXTENTION_IMPORT_FILE As String = "CM_CTRLPROGRAMS_NOT_EXTENTION_IMPORT_FILE"
    Public Shared CM_CTRLPROGRAMS_UPDATE_TEMPLATE_REPORT_SUCCESS As String = "CM_CTRLPROGRAMS_UPDATE_TEMPLATE_REPORT_SUCCESS"
    Public Shared CM_CTRLPROGRAMS_UPDATE_TEMPLATE_REPORT_FAILED As String = "CM_CTRLPROGRAMS_UPDATE_TEMPLATE_REPORT_FAILED"
    Public Shared CM_CTRLPROGRAMS_IS_EXSIST_PROGRAM_CODE As String = "CM_CTRLPROGRAMS_IS_EXSIST_PROGRAM_CODE"
    Public Shared CM_CTRLPROGRAMS_IS_EXSIST_PROGRAM_NAME As String = "CM_CTRLPROGRAMS_IS_EXSIST_PROGRAM_NAME"
    Public Shared CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_NAME As String = "CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_NAME"
    Public Shared CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_SEQUENCE As String = "CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_SEQUENCE"
    Public Shared CM_CTRLPROGRAM_MESSAGE_BOX_DELETE_THIS_TEMPLATE As String = "CM_CTRLPROGRAM_MESSAGE_BOX_DELETE_THIS_TEMPLATE"

    Public Shared CM_CTRLPA_FORMULA_IS_SELECTED_CONTENT_COPY_FORMULAR As String = "CM_CTRLPA_FORMULA_IS_SELECTED_CONTENT_COPY_FORMULAR"
    Public Shared CM_CTRLPA_FORMULA_MESSAGE_BOX_CONFIRM As String = "CM_CTRLPA_FORMULA_MESSAGE_BOX_CONFIRM"


    Public Shared Function DATE_FORMAT() As String
        Return "DD/MM/YYYY"
    End Function

    Public Shared Function DATETIME_FORMAT() As String
        Return "dd/MM/yyyy hh:mm:ss"
    End Function
    '
    'Public Shared CONFIG_SESSION_TIMEOUT As String = "SessionTimeout"
    'Public Shared CONFIG_MAIL_SERVER As String = "MailServer"
    'Public Shared CONFIG_MAIL_SERVER_PORT As String = "MailServerPort"
    'Public Shared CONFIG_NUMBER_RETRY_LOCK_ACCOUNT As String = "NUMBER_RETRY_LOCK_ACCOUNT"
    'Public Shared CONFIG_PASSWORD_EXPIRE As String = "PASSWORD_EXPIRE"
    'Public Shared CONFIG_PASSWORD_LENGTH As String = "PASSWORD_LENGTH"
    'Public Shared CONFIG_PASSWORD_UPPER_CHAR As String = "PASSWORD_UPPER_CHAR"
    'Public Shared CONFIG_PASSWORD_LOWER_CHAR As String = "PASSWORD_LOWER_CHAR"
    'Public Shared CONFIG_PASSWORD_NUMBER_CHAR As String = "PASSWORD_NUMBER_CHAR"
    'Public Shared CONFIG_PASSWORD_SPECIAL_CHAR As String = "PASSWORD_SPECIAL_CHAR"
    'Public Shared CONFIG_PASSWORD_NOTIFY As String = "PASSWORD_NOTIFY"
    'Public Shared CONFIG_PASSWORD_NOTIFY_COUNT As String = "PASSWORD_NOTIFY_COUNT"

    'Public Shared CONFIG_REMINDER_CONTRACT As String = "REMINDER_CONTRACT"
    'Public Shared CONFIG_REMINDER_BIRTHDAY As String = "REMINDER_BIRTHDAY"
    'Public Shared CONFIG_REMINDER_PROBATION As String = "REMINDER_PROBATION"
    'Public Shared CONFIG_REMINDER_RETIRE As String = "REMINDER_RETIRE"

    'Public Shared CONFIG_USER_MODIFIED_LOG As String = "CONFIG_USER_MODIFIED_LOG"
End Class
