using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Linq;

namespace AzureFunctionsMarkdownFiles
{
    public static class GetAllMarkdownFileNames
    {
        [FunctionName("GetAllMarkdownFileNames")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            string sasToken = Environment.GetEnvironmentVariable("SASToken");
            string accountName = Environment.GetEnvironmentVariable("AccountName");
            string containerName = Environment.GetEnvironmentVariable("ContainerName");

            StorageCredentials credentials = new(sasToken);
            CloudStorageAccount storageacc = new(credentials, accountName, endpointSuffix: null, useHttps: true);
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            BlobContinuationToken continuationToken = null;
            var listOfFileNames = new List<string>();
            do
            {
                var blobs = await container.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = blobs.ContinuationToken;
                var files = blobs.Results.ToList();

                foreach (var file in files)
                {
                    if (file.GetType() == typeof(CloudBlockBlob))
                    {
                        var blob = (CloudBlockBlob)file;
                        listOfFileNames.Add(blob.Uri.Segments.Last());
                    }
                }
            } while (continuationToken != null);

            return new OkObjectResult(listOfFileNames);
        }
    }
}