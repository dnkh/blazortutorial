using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System;

namespace AzureFunctionsMarkdownFiles
{
    public static class DownloadMarkdownFile
    {
        [FunctionName("DownloadMarkdownFile")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "DownloadMarkdownFile/{blobName}")] HttpRequest req,
            string blobName)
        {
            string storageName = Environment.GetEnvironmentVariable("StorageName");
            string storageSecret = Environment.GetEnvironmentVariable("StorageSecret");
            string containerName = Environment.GetEnvironmentVariable("ContainerName");

            StorageCredentials storageCredentials = new (storageName, storageSecret);
            CloudStorageAccount storageAccount = new (storageCredentials, true);
            CloudBlobContainer container = storageAccount.CreateCloudBlobClient().GetContainerReference(containerName);
            CloudBlockBlob block = container.GetBlockBlobReference(blobName);
            Stream blobStream = await block.OpenReadAsync();

            HttpResponseMessage message = new (HttpStatusCode.OK);
            message.Content = new StreamContent(blobStream);
            message.Content.Headers.ContentLength = block.Properties.Length;
            message.StatusCode = HttpStatusCode.OK;
            message.Content.Headers.ContentType = new MediaTypeHeaderValue(block.Properties.ContentType);
            message.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = $"CopyOf_{block.Name}",
                Size = block.Properties.Length
            };

            return message;
        }
    }
}

