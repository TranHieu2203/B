Public Module HistaffFrameworkEnum
    ''' <summary>
    ''' Trạng thái bảng công
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enStatusParox
        Working = 0
        Close = 1
        Reactive = 2
        FinalClose = 3
    End Enum

    ''' <summary>
    ''' Trạng thái bảng lương
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enStatusColex
        Working = 0
        Close = 1
        Reactive = 2
        FinalClose = 3
    End Enum

    Public Enum enGroupSalaryType
        ImportOther = 1
        ImportDeduct = 2
    End Enum
    Public Enum PortalStatus
        Saved = 3
        WaitingForApproval = 0
        ApprovedByLM = 1
        UnApprovedByLM = 2
        Cancel = 4
        UnVerifiedByHr = 6864
        ApprovedByGM = 21
        UnApprovedByGM = 22
        Discussing = 23
        ApprovedBySDC = 24
        SupportedByManager = 25
        VerifiedByHR = 26
        ApprovedByNCSP = 27
        UnapprovedByNCSP = 28
        ApprovedByTCT = 29
        UnapprovedByTCT = 30

        unsent = 6860
        waitsend = 6861
        aprrove = 6862
        unaprrove = 6863
        unCBNS = 6864
    End Enum
End Module
