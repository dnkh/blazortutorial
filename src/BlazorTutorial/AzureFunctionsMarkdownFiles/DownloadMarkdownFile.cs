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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Net;

namespace AzureFunctionsMarkdownFiles
{
    public static class DownloadMarkdownFile
    {
        [OpenApiOperation(operationId: "downloadMarkdownFile", tags: new[] { "downloadMarkdownFile" }, Summary = "Gets the markdown file", Description = "Gets the markdown file.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "blobName", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "Name of the blob", Description = "Name of the blob.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "file", Description = "This returns the file.")]
        [FunctionName("DownloadMarkdownFile")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "DownloadMarkdownFile")] HttpRequest req)
        {
            string blobName = req.Query["blobName"];
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

