Function: SendMail
Datetime: 07/08/2022 12:47:08 AM
Error message: The SMTP server requires a secure connection or the client was not authenticated. The server response was: 5.7.0 Authentication Required. Learn more at
Stack trace:    at System.Net.Mail.MailCommand.CheckResponse(SmtpStatusCode statusCode, String response)
   at System.Net.Mail.MailCommand.Send(SmtpConnection conn, Byte[] command, MailAddress from, Boolean allowUnicode)
   at System.Net.Mail.SmtpTransport.SendMail(MailAddress sender, MailAddressCollection recipients, String deliveryNotify, Boolean allowUnicode, SmtpFailedRecipientException& exception)
   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1110
Other Text: iCommon

Function: SendMail
Datetime: 07/08/2022 12:48:08 AM
Error message: The SMTP server requires a secure connection or the client was not authenticated. The server response was: 5.7.0 Authentication Required. Learn more at
Stack trace:    at System.Net.Mail.MailCommand.CheckResponse(SmtpStatusCode statusCode, String response)
   at System.Net.Mail.MailCommand.Send(SmtpConnection conn, Byte[] command, MailAddress from, Boolean allowUnicode)
   at System.Net.Mail.SmtpTransport.SendMail(MailAddress sender, MailAddressCollection recipients, String deliveryNotify, Boolean allowUnicode, SmtpFailedRecipientException& exception)
   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1110
Other Text: iCommon

Function: SendMail
Datetime: 07/08/2022 8:27:01 AM
Error message: The connection is not open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.Secure.vb:line 4303
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1016
Other Text: iCommon

Function: SendMail
Datetime: 07/08/2022 8:28:01 AM
Error message: The connection is not open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.Secure.vb:line 4303
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1016
Other Text: iCommon

Function: SendMail
Datetime: 07/08/2022 8:30:01 AM
Error message: The connection is not open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.Secure.vb:line 4303
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1016
Other Text: iCommon

Function: SendMail
Datetime: 07/08/2022 8:31:01 AM
Error message: The connection is not open.
Stack trace:    at CommonDAL.CommonRepository.GetConfig(ModuleID eModule) in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.Secure.vb:line 4303
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1016
Other Text: iCommon

Function: SendMail
Datetime: 07/08/2022 8:43:04 AM
Error message: The SMTP server requires a secure connection or the client was not authenticated. The server response was: 5.7.0 Authentication Required. Learn more at
Stack trace:    at System.Net.Mail.MailCommand.CheckResponse(SmtpStatusCode statusCode, String response)
   at System.Net.Mail.MailCommand.Send(SmtpConnection conn, Byte[] command, MailAddress from, Boolean allowUnicode)
   at System.Net.Mail.SmtpTransport.SendMail(MailAddress sender, MailAddressCollection recipients, String deliveryNotify, Boolean allowUnicode, SmtpFailedRecipientException& exception)
   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1110
Other Text: iCommon

Function: SendMail
Datetime: 07/08/2022 11:12:52 AM
Error message: The SMTP server requires a secure connection or the client was not authenticated. The server response was: 5.7.0 Authentication Required. Learn more at
Stack trace:    at System.Net.Mail.MailCommand.CheckResponse(SmtpStatusCode statusCode, String response)
   at System.Net.Mail.MailCommand.Send(SmtpConnection conn, Byte[] command, MailAddress from, Boolean allowUnicode)
   at System.Net.Mail.SmtpTransport.SendMail(MailAddress sender, MailAddressCollection recipients, String deliveryNotify, Boolean allowUnicode, SmtpFailedRecipientException& exception)
   at System.Net.Mail.SmtpClient.Send(MailMessage message)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1110
Other Text: iCommon

Function: SendMail
Datetime: 07/08/2022 11:33:18 AM
Error message: Object reference not set to an instance of an object.
Stack trace:    at Oracle.DataAccess.Client.SqlGen.SqlSelectStatement.WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
   at Oracle.DataAccess.Client.SqlGen.SqlGenerator.WriteSql(ISqlFragment sqlStatement)
   at Oracle.DataAccess.Client.EFOracleProviderServices.CreateCommand(EFOracleProviderManifest providerManifest, DbCommandTree commandTree)
   at Oracle.DataAccess.Client.EFOracleProviderServices.CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityCommandDefinition..ctor(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.EntityClient.EntityProviderServices.CreateCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree)
   at System.Data.Objects.Internal.ObjectQueryExecutionPlan.Prepare(ObjectContext context, DbQueryCommandTree tree, Type elementType, MergeOption mergeOption, Span span, ReadOnlyCollection`1 compiledQueryParameters, AliasGenerator aliasGenerator)
   at System.Data.Objects.ELinq.ELinqQueryState.GetExecutionPlan(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1029
Other Text: iCommon

