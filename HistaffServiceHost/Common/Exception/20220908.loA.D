Function: SendMail
Datetime: 09/08/2022 10:45:06 AM
Error message: Execution of the command requires an open and available connection. The connection's current state is broken.
Stack trace:    at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at CommonDAL.CommonRepository.SendMail() in D:\GIT\BCG_DEV_NEW\CommonDAL\CommonRepository.vb:line 1037
Other Text: iCommon

