Function: CheckAndSendPayslip
Datetime: 5/1/2023 4:24:12 AM
Error message: Schema specified is not valid. Errors: 
PayrollContext.msl(3,4) : error 2062: No mapping specified for instances of the EntitySet and AssociationSet in the EntityContainer PayrollContext.
Stack trace:    at System.Data.Mapping.StorageMappingItemCollection.Init(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable`1 xmlReaders, List`1 filePaths, Boolean throwOnError)
   at System.Data.Mapping.StorageMappingItemCollection..ctor(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable`1 xmlReaders, List`1 filePaths)
   at System.Data.Metadata.Edm.MetadataCache.StoreMetadataEntry.LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
   at System.Data.Metadata.Edm.MetadataCache.LoadItemCollection[T](IItemCollectionLoader`1 itemCollectionLoader, T entry)
   at System.Data.Metadata.Edm.MetadataCache.GetOrCreateStoreAndMappingItemCollections(String cacheKey, MetadataArtifactLoader loader, EdmItemCollection edmItemCollection, Object& entryToken)
   at System.Data.EntityClient.EntityConnection.LoadStoreItemCollections(MetadataWorkspace workspace, DbConnection storeConnection, DbProviderFactory factory, DbConnectionOptions connectionOptions, EdmItemCollection edmItemCollection, MetadataArtifactLoader artifactLoader)
   at System.Data.EntityClient.EntityConnection.GetMetadataWorkspace(Boolean initializeAllCollections)
   at System.Data.EntityClient.EntityConnection.InitializeMetadata(DbConnection newConnection, DbConnection originalConnection, Boolean closeOriginalConnectionOnFailure)
   at System.Data.EntityClient.EntityConnection.Open()
   at System.Data.Objects.ObjectContext.EnsureConnection()
   at System.Data.Objects.ObjectQuery`1.GetResults(Nullable`1 forMergeOption)
   at System.Data.Objects.ObjectQuery`1.System.Collections.Generic.IEnumerable<T>.GetEnumerator()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at PayrollDAL.PayrollRepository.CheckAndSendPayslip() in E:\Source\BCG_DEV\PayrollDAL\PayrollRepository.vb:line 387
Other Text: iPayroll

