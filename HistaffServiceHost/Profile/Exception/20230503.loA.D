Function: UpdateTrangThaiNghiViec
Datetime: 5/3/2023 9:54:00 PM
Error message: Schema specified is not valid. Errors: 
ProfileContext.msl(3,4) : error 2062: No mapping specified for instances of the EntitySet and AssociationSet in the EntityContainer ProfileContext.
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
   at System.Data.Objects.ObjectContext.ExecuteStoreCommand(String commandText, Object[] parameters)
   at ProfileDAL.ProfileRepository.UpdateTrangThaiNghiViec() in E:\Source\BCG_DEV\ProfileDAL\ProfileRepository.BackgroundProcess.vb:line 182
Other Text: iProfile

Function: SendMailStockRemind
Datetime: 5/3/2023 9:54:00 PM
Error message: Schema specified is not valid. Errors: 
ProfileContext.msl(3,4) : error 2062: No mapping specified for instances of the EntitySet and AssociationSet in the EntityContainer ProfileContext.
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
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.SendMailStockRemind() in E:\Source\BCG_DEV\ProfileDAL\ProfileRepository.BackgroundProcess.vb:line 244
Other Text: iProfile

Function: UpdateWorking
Datetime: 5/3/2023 9:54:00 PM
Error message: Schema specified is not valid. Errors: 
ProfileContext.msl(3,4) : error 2062: No mapping specified for instances of the EntitySet and AssociationSet in the EntityContainer ProfileContext.
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
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.UpdateWorking() in E:\Source\BCG_DEV\ProfileDAL\ProfileRepository.BackgroundProcess.vb:line 25
Other Text: iProfile

Function: UpdateTerminate
Datetime: 5/3/2023 9:54:00 PM
Error message: Schema specified is not valid. Errors: 
ProfileContext.msl(3,4) : error 2062: No mapping specified for instances of the EntitySet and AssociationSet in the EntityContainer ProfileContext.
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
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.UpdateTerminate() in E:\Source\BCG_DEV\ProfileDAL\ProfileRepository.BackgroundProcess.vb:line 51
Other Text: iProfile

Function: UpdateContract
Datetime: 5/3/2023 9:54:00 PM
Error message: Schema specified is not valid. Errors: 
ProfileContext.msl(3,4) : error 2062: No mapping specified for instances of the EntitySet and AssociationSet in the EntityContainer ProfileContext.
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
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.UpdateContract() in E:\Source\BCG_DEV\ProfileDAL\ProfileRepository.BackgroundProcess.vb:line 124
Other Text: iProfile

Function: UpdateKiemNhiem
Datetime: 5/3/2023 9:54:00 PM
Error message: Schema specified is not valid. Errors: 
ProfileContext.msl(3,4) : error 2062: No mapping specified for instances of the EntitySet and AssociationSet in the EntityContainer ProfileContext.
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
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.UpdateKiemNhiem() in E:\Source\BCG_DEV\ProfileDAL\ProfileRepository.BackgroundProcess.vb:line 216
Other Text: iProfile

Function: UpdateUser
Datetime: 5/3/2023 9:54:00 PM
Error message: Schema specified is not valid. Errors: 
ProfileContext.msl(3,4) : error 2062: No mapping specified for instances of the EntitySet and AssociationSet in the EntityContainer ProfileContext.
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
   at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
   at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
   at ProfileDAL.ProfileRepository.UpdateUser() in E:\Source\BCG_DEV\ProfileDAL\ProfileRepository.BackgroundProcess.vb:line 100
Other Text: iProfile

