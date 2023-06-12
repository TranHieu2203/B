Imports System.IO
Imports Framework.Data
Imports System.Data.Objects
Imports Framework.Data.System.Linq.Dynamic
Imports System.Reflection

Partial Class ProfileRepository
    Public Function GetPA_SALARY_TYPE(ByVal query As PA_SALARY_TYPEQuery) As PA_SALARY_TYPEResult
        Dim result As New PA_SALARY_TYPEResult
        Dim queryBuilder = From s In Context.PA_SALARY_TYPE
                           Select New PA_SALARY_TYPEDTO With {.ID = s.ID,
                                               .CODE = s.CODE,
                                               .Name = s.NAME,
                                               .ACTFLG = s.ACTFLG}
        If query.ID.HasValue Then
            queryBuilder = queryBuilder.Where(Function(f) f.ID = query.ID)
        End If
        If query.Ids IsNot Nothing AndAlso query.Ids.Any Then
            queryBuilder = queryBuilder.Where(Function(f) query.Ids.Contains(f.ID))
        End If
        If query.Status IsNot Nothing AndAlso query.Status.Any Then
            queryBuilder = queryBuilder.Where(Function(f) query.Status.Contains(f.ACTFLG))
        End If
        result.Result = queryBuilder.ToList
        result.TotalRecords = result.Result.Count
        Return result
    End Function
End Class

