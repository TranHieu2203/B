Function: SendMail
Datetime: 06/30/2022 1:39:17 PM
Error message: The connection is not open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.Secure.vb:line 4216
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1016
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 1:39:31 PM
Error message: The underlying provider failed on Open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.Secure.vb:line 4216
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1016
Inner error message: Connection request timed out
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, Object src, OracleLogicalTransaction m_oracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleConnectionOCP.Open(OracleConnection con)
   at Oracle.DataAccess.Client.OracleConnection.Open()
   at System.Data.EntityClient.EntityConnection.OpenStoreConnectionIf(Boolean openCondition, DbConnection storeConnectionToOpen, DbConnection originalConnection, String exceptionCode, String attemptedOperation, Boolean& closeStoreConnectionOnFailure)
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 2:09:53 PM
Error message: The SMTP server requires a secure connection or the client was not authenticated. The server response was: 5.7.0 Authentication Required. Learn more at
Stack trace:    at System.Net.Mail.MailCommand.CheckResponse(SmtpStatusCode statusCode, String response)
   at System.Net.Mail.MailCommand.Send(SmtpConnection conn, Byte[] command, MailAddress from, Boolean allowUnicode)
   at System.Net.Mail.SmtpTransport.SendMail(MailAddress sender, MailAddressCollection recipients, String deliveryNotify, Boolean allowUnicode, SmtpFailedRecipientException& exception)
   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1110
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 2:29:12 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.Secure.vb:line 4216
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1016
Inner error message: ORA-03135: connection lost contact
Process ID: 5284
Session ID: 403 Serial number: 21376
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:25:15 PM
Error message: This property cannot be set to a null value.
Stack trace:    at System.Data.EntityUtil.ThrowPropertyIsNotNullable(String propertyName)
   at System.Data.Objects.DataClasses.StructuralObject.SetValidValue(String value, Boolean isNullable)
   at CommonDAL.SE_MAIL.set_SUBJECT(String Value) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonContext1.Designer.vb:line 21772
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Shaper.HandleEntityAppendOnly[TEntity](Func`2 constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:26:10 PM
Error message: This property cannot be set to a null value.
Stack trace:    at System.Data.EntityUtil.ThrowPropertyIsNotNullable(String propertyName)
   at System.Data.Objects.DataClasses.StructuralObject.SetValidValue(String value, Boolean isNullable)
   at CommonDAL.SE_MAIL.set_SUBJECT(String Value) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonContext1.Designer.vb:line 21772
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Shaper.HandleEntityAppendOnly[TEntity](Func`2 constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:27:10 PM
Error message: This property cannot be set to a null value.
Stack trace:    at System.Data.EntityUtil.ThrowPropertyIsNotNullable(String propertyName)
   at System.Data.Objects.DataClasses.StructuralObject.SetValidValue(String value, Boolean isNullable)
   at CommonDAL.SE_MAIL.set_SUBJECT(String Value) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonContext1.Designer.vb:line 21772
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Shaper.HandleEntityAppendOnly[TEntity](Func`2 constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:28:10 PM
Error message: This property cannot be set to a null value.
Stack trace:    at System.Data.EntityUtil.ThrowPropertyIsNotNullable(String propertyName)
   at System.Data.Objects.DataClasses.StructuralObject.SetValidValue(String value, Boolean isNullable)
   at CommonDAL.SE_MAIL.set_SUBJECT(String Value) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonContext1.Designer.vb:line 21772
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Shaper.HandleEntityAppendOnly[TEntity](Func`2 constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:29:10 PM
Error message: This property cannot be set to a null value.
Stack trace:    at System.Data.EntityUtil.ThrowPropertyIsNotNullable(String propertyName)
   at System.Data.Objects.DataClasses.StructuralObject.SetValidValue(String value, Boolean isNullable)
   at CommonDAL.SE_MAIL.set_SUBJECT(String Value) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonContext1.Designer.vb:line 21772
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Shaper.HandleEntityAppendOnly[TEntity](Func`2 constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:30:10 PM
Error message: This property cannot be set to a null value.
Stack trace:    at System.Data.EntityUtil.ThrowPropertyIsNotNullable(String propertyName)
   at System.Data.Objects.DataClasses.StructuralObject.SetValidValue(String value, Boolean isNullable)
   at CommonDAL.SE_MAIL.set_SUBJECT(String Value) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonContext1.Designer.vb:line 21772
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Shaper.HandleEntityAppendOnly[TEntity](Func`2 constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:31:10 PM
Error message: This property cannot be set to a null value.
Stack trace:    at System.Data.EntityUtil.ThrowPropertyIsNotNullable(String propertyName)
   at System.Data.Objects.DataClasses.StructuralObject.SetValidValue(String value, Boolean isNullable)
   at CommonDAL.SE_MAIL.set_SUBJECT(String Value) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonContext1.Designer.vb:line 21772
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Shaper.HandleEntityAppendOnly[TEntity](Func`2 constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:32:10 PM
Error message: This property cannot be set to a null value.
Stack trace:    at System.Data.EntityUtil.ThrowPropertyIsNotNullable(String propertyName)
   at System.Data.Objects.DataClasses.StructuralObject.SetValidValue(String value, Boolean isNullable)
   at CommonDAL.SE_MAIL.set_SUBJECT(String Value) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonContext1.Designer.vb:line 21772
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Shaper.HandleEntityAppendOnly[TEntity](Func`2 constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:33:10 PM
Error message: This property cannot be set to a null value.
Stack trace:    at System.Data.EntityUtil.ThrowPropertyIsNotNullable(String propertyName)
   at System.Data.Objects.DataClasses.StructuralObject.SetValidValue(String value, Boolean isNullable)
   at CommonDAL.SE_MAIL.set_SUBJECT(String Value) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonContext1.Designer.vb:line 21772
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Shaper.HandleEntityAppendOnly[TEntity](Func`2 constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:34:10 PM
Error message: This property cannot be set to a null value.
Stack trace:    at System.Data.EntityUtil.ThrowPropertyIsNotNullable(String propertyName)
   at System.Data.Objects.DataClasses.StructuralObject.SetValidValue(String value, Boolean isNullable)
   at CommonDAL.SE_MAIL.set_SUBJECT(String Value) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonContext1.Designer.vb:line 21772
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Shaper.HandleEntityAppendOnly[TEntity](Func`2 constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:35:10 PM
Error message: This property cannot be set to a null value.
Stack trace:    at System.Data.EntityUtil.ThrowPropertyIsNotNullable(String propertyName)
   at System.Data.Objects.DataClasses.StructuralObject.SetValidValue(String value, Boolean isNullable)
   at CommonDAL.SE_MAIL.set_SUBJECT(String Value) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonContext1.Designer.vb:line 21772
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Shaper.HandleEntityAppendOnly[TEntity](Func`2 constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
   at lambda_method(Closure , Shaper )
   at System.Data.Common.Internal.Materialization.Coordinator`1.ReadNextElement(Shaper shaper)
   at System.Data.Common.Internal.Materialization.Shaper`1.SimpleEnumerator.MoveNext()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

Function: SendMail
Datetime: 06/30/2022 3:41:18 PM
Error message: An error occurred while executing the command definition. See the inner exception for details.
Stack trace:    at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Execute[TResultType](ObjectContext context, ObjectParameterCollection parameterValues)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Inner error message: ORA-03135: connection lost contact
Process ID: 6076
Session ID: 407 Serial number: 12390
Inner stack trace:    at Oracle.DataAccess.Client.OracleException.HandleErrorHelper(Int32 errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, String procedure, Boolean bCheck, Int32 isRecoverable, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleException.HandleError(Int32 errCode, OracleConnection conn, String procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, Object src, Boolean bCheck, OracleLogicalTransaction m_OracleLogicalTransaction)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteReader(Boolean requery, Boolean fillRequest, CommandBehavior behavior)
   at Oracle.DataAccess.Client.OracleCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.EntityClient.EntityCommandDefinition.ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
Other Text: iCommon

