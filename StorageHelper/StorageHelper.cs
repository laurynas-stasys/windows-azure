using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace StorageHelper
{
    public class StorageHelper
   {
        public static CloudStorageAccount GetStorageAccount()
        {
            return CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        }
    }
}
