using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace TestStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestBlobService();
            TestTableService();
        }

        private static void TestTableService()
        {
            TableStorageHelper tableHelper = new TableStorageHelper();

            tableHelper.GetCloudTableClient();

            tableHelper.CreateTable("sampleTable");

            TableServiceContext tableContext = tableHelper.GetTableContext();

            AddData(tableContext, "sampleTable");

            CloudTableQuery<Object> tableQuery = tableHelper.GetEntities(tableContext, "sampleTable");
        }

        private static void AddData(TableServiceContext tableContext, string p)
        {
            CustomerEntity customerEntity1 = new CustomerEntity("Jusel", "Jaroslaw");
            customerEntity1.Email = "jjusel@gmail.com";
            customerEntity1.PhoneNumber = "+370xxxxx1";

            tableContext.AddObject("sampleTable", customerEntity1);
            
            CustomerEntity customerEntity2 = new CustomerEntity("Ungurys", "Andrius");
            customerEntity2.Email = "aungurys@1clickfactory.com";
            customerEntity2.PhoneNumber = "+370xxxxx2";

            tableContext.AddObject("sampleTable", customerEntity2);

            CustomerEntity customerEntity3 = new CustomerEntity("Čiukšys", "Vytautas");
            customerEntity3.Email = "vciuksys@1clickfactory.com";
            customerEntity3.PhoneNumber = "+370xxxxx3";

            tableContext.AddObject("sampleTable", customerEntity3);

            CustomerEntity customerEntity4 = new CustomerEntity("Norvaišaitė", "Modesta");
            customerEntity4.Email = "mnorvaisaite@1clickfactory.com";
            customerEntity4.PhoneNumber = "+370xxxxx4";

            tableContext.AddObject("sampleTable", customerEntity4);
            
            CustomerEntity customerEntity5 = new CustomerEntity("Vaicekauskas", "Vilius");
            customerEntity5.Email = "vvaicekauskas@1clickfactory.com";
            customerEntity5.PhoneNumber = "+370xxxxx5";

            tableContext.AddObject("sampleTable", customerEntity5); 
            

            CustomerEntity customerEntity6 = new CustomerEntity("Stašys", "Laurynas");
            customerEntity6.Email = "lstasys@1clickfactory.com";
            customerEntity6.PhoneNumber = "+370xxxxx6";

            tableContext.AddObject("sampleTable", customerEntity6);

            //Finally, changes are saved 
            tableContext.SaveChangesWithRetries();
        }

        private static void TestBlobService()
        {
            BlobStorageHelper storageHelper = new BlobStorageHelper();

            storageHelper.GetCloudBlobClient();

            storageHelper.WriteContainersToConsole();

            storageHelper.WriteBlobToConsole();

            storageHelper.PutBlob("nitakoj", "pizdec", "nivadzinoj");

            storageHelper.WriteBlobToConsole();
        }        
    }
}