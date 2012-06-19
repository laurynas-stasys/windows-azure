using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace TestStorage
{
    public class BlobStorageHelper
    {
        #region Accessories
		
        private CloudStorageAccount storageAccount { get; set; }
        private CloudBlobClient blobManager { get; set; }
        
	    #endregion

        #region StorageHelperManager

        /// <summary>
        /// Default constructor
        /// </summary>
        public BlobStorageHelper()
        {
            GetStorageAccount();
        }

        /// <summary>
        /// Gets cloud storage acount
        /// </summary>
        /// <returns></returns>
        private void GetStorageAccount()
        {
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        }

        /// <summary>
        /// Gets blob client
        /// </summary>
        /// <param name="cloudStorageAccount"></param>
        /// <returns></returns>
        public void GetCloudBlobClient()
        {
            blobManager = storageAccount.CreateCloudBlobClient();
        }
        
        #endregion

        #region ContainerHelpers

        /// <summary>
        /// Creates container with given name
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public bool CreateContainer(string containerName)
        {
            CloudBlobContainer container = blobManager.GetContainerReference(containerName);

            try
            {
                container.CreateIfNotExist();

                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// Creates container with given name and permissions
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="containerPermissions"></param>
        /// <returns></returns>
        public bool CreateContainer(string containerName, BlobContainerPermissions containerPermissions)
        {
            CloudBlobContainer container = blobManager.GetContainerReference(containerName);

            try
            {
                container.CreateIfNotExist();
                container.SetPermissions(containerPermissions);

                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// Gets all the containers in given blob account
        /// </summary>
        /// <param name="blobClient"></param>
        /// <returns></returns>
        public List<CloudBlobContainer> GetContainers()
        {
            return blobManager.ListContainersSegmented().Results.ToList();
        }

        /// <summary>
        /// Prints containers to console
        /// </summary>
        public void WriteContainersToConsole()
        {
            List<CloudBlobContainer> containers = GetContainers();

            containers.ForEach(x => Console.WriteLine(x.Name));
        }

        /// <summary>
        /// Sets container permissions as public
        /// </summary>
        /// <param name="containerName"></param>
        public void SetPublicAccess(string containerName)
        {
            CloudBlobContainer conteiner = blobManager.GetContainerReference(containerName);

            BlobContainerPermissions permissions = new BlobContainerPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Blob;

            conteiner.SetPermissions(permissions);
        }

        #endregion

        #region BlobHelpers
        /// <summary>
        /// Gets all the blobs in given blob container
        /// </summary>
        /// <param name="blobContainer"></param>
        /// <returns></returns>
        public List<IListBlobItem> GetBlobs(string containerName)
        {
            CloudBlobContainer container = blobManager.GetContainerReference(containerName);
            
            return container.ListBlobs().ToList();
        }
                
        /// <summary>
        /// Gets particular blob in the container
        /// </summary>
        /// <param name="blobClient"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        public CloudBlob GetBlob(string blobName)
        {
            return blobManager.GetBlobReference(blobName);
        }

        public bool PutBlob(string containerName, string fileName, string content)
        {
            try
            {
                CloudBlobContainer blobContainer = blobManager.GetContainerReference(containerName);

                blobContainer.CreateIfNotExist();
                SetPublicAccess(blobContainer.Name);

                CloudBlob blob = blobContainer.GetBlobReference(fileName);
                
                blob.UploadText(content);

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        /// <summary>
        /// Prints blobs in blob storage
        /// </summary>
        /// <param name="storageHelper"></param>
        public void WriteBlobToConsole()
        {
            List<CloudBlobContainer> containers = GetContainers();

            foreach (CloudBlobContainer conteiner in containers)
            {
                List<IListBlobItem> blobs = GetBlobs(conteiner.Name);

                Console.WriteLine(String.Format("\n{0}", conteiner.Name));

                foreach (IListBlobItem blob in blobs)
                {
                    Console.WriteLine(blob.Uri);
                }
            }
        }
        #endregion
    }
}
