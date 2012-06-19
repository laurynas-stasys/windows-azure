using System;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Data.Services.Client;

namespace TestStorage
{
    class TableStorageHelper
    {
        #region Accessories

        private CloudStorageAccount storageAccount;
        private CloudTableClient tableClient;
        
        #endregion
        
        #region TableStorageHelpers

        /// <summary>
        /// Default constructor
        /// </summary>
        public TableStorageHelper()
        {
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        }

        /// <summary>
        /// Returns cloud table storage client
        /// </summary>
        /// <returns></returns>
        public void GetCloudTableClient()
        {
            if (tableClient == null)
                new TableStorageHelper();

            tableClient = storageAccount.CreateCloudTableClient();
        }

        /// <summary>
        /// Returns table service context
        /// </summary>
        /// <returns></returns>
        public TableServiceContext GetTableContext()
        {
            if (tableClient == null)
                GetCloudTableClient();

            return tableClient.GetDataServiceContext();
        }

        #endregion
              
        #region TableStorageMethods

        /// <summary>
        /// Creates table in cloud table storage
        /// </summary>
        /// <param name="tableName"></param>
        public void CreateTable(string tableName)
        {
            if (tableClient == null)
                GetCloudTableClient();

            tableClient.CreateTableIfNotExist(tableName);
        }

        /// <summary>
        /// Inserts single entity to database
        /// </summary>
        public void InsertEntity(string tableName, Object entity)
        {
            TableServiceContext tableContext = GetTableContext();

            tableContext.AddObject("sampleTable", entity);

            tableContext.SaveChangesWithRetries();            
        }

        /// <summary>
        /// Returns all entities from given table
        /// </summary>
        /// <param name="serviceContext"></param>
        /// <param name="entitySetName"></param>
        /// <returns></returns>
        public CloudTableQuery<Object> GetEntities(TableServiceContext serviceContext, string entitySetName)
        {
            CloudTableQuery<Object> partitionQuery =
            (from e in serviceContext.CreateQuery<Object>(entitySetName)
             select e).AsTableServiceQuery<Object>();

            return partitionQuery;
        }

        #endregion
    }
}
