using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureFunctionsMarkdownFiles
{
    public static class UploadMarkdownFile
    {
        [FunctionName("UploadMarkdownFile")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            string sasToken = Environment.GetEnvironmentVariable("SASToken");
            string accountName = Environment.GetEnvironmentVariable("AccountName");
            string containerName = Environment.GetEnvironmentVariable("ContainerName");

            StorageCredentials credentials = new(sasToken);
            CloudStorageAccount storageacc = new(credentials, accountName, endpointSuffix: null, useHttps: true);
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            var file = req.Form.Files["File"];
            Stream myBlob = file.OpenReadStream();
            var blobRef = container.GetBlockBlobReference(file.FileName);
            await blobRef.UploadFromStreamAsync(myBlob);

            return new OkObjectResult("File uploaded successfully.");
        }
    }
}