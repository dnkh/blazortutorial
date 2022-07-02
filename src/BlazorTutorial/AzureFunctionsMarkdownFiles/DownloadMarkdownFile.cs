using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using Microsoft.AspNetCore.Mvc;

namespace AzureFunctionsMarkdownFiles
{
    public static class DownloadMarkdownFile
    {
        [FunctionName("DownloadMarkdownFile")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "DownloadMarkdownFile/{blobName}")] HttpRequest req,
            string blobName)
        {
            string sasToken = Environment.GetEnvironmentVariable("SASToken");
            string accountName = Environment.GetEnvironmentVariable("AccountName");
            string containerName = Environment.GetEnvironmentVariable("ContainerName");

            StorageCredentials credentials = new (sasToken);
            CloudStorageAccount storageacc = new (credentials, accountName, endpointSuffix: null, useHttps: true);
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            var blobRef = container.GetBlobReference(blobName);
            var fileStream = await blobRef.OpenReadAsync();
            StreamReader sr = new(fileStream);
            var filecontent = await sr.ReadToEndAsync();

            return new OkObjectResult(filecontent);
        }
    }
}

