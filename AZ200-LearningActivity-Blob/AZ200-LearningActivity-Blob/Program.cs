namespace AZ200_LearningActivity_Blob
{
    using System;
    using System.IO;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Newtonsoft.Json;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            string _sasConnectionString = "";
            CloudStorageAccount account = CloudStorageAccount.Parse(_sasConnectionString);

            CloudBlobClient client = account.CreateCloudBlobClient();

            CloudBlobContainer container = client.GetContainerReference("test");
            container.CreateIfNotExistsAsync().Wait();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(DateTime.Now.Ticks.ToString());
            blockBlob.Properties.ContentType = "application/json";
            var obj = new
            {
                By = "AZ-200 Learning Activity - Blob",
                Message = "This is a test Message"
            };
            var json = JsonConvert.SerializeObject(obj);

           blockBlob.UploadTextAsync(json).Wait();

            Console.WriteLine("Press any key to exit the sample application.");
            Console.ReadLine();
        }
    }
}
