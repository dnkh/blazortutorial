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
    public static class DeleteMarkdownFile
    {
        [OpenApiOperation(operationId: "deleteMarkdownFile", tags: new[] { "deleteMarkdownFile" }, Summary = "Deletes the markdown file", Description = "Deletes the markdown file.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "blobName?", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "Name of the blob", Description = "Name of the blob.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "response", Description = "This returns the response.")]
        [FunctionName("DeleteMarkdownFile")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteMarkdownFile/{blobName?}")] HttpRequest req, string blobName)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                return new BadRequestObjectResult("Parameter blobName missing or empty.");
            }
            string sasToken = Environment.GetEnvironmentVariable("SASToken");
            string accountName = Environment.GetEnvironmentVariable("AccountName");
            string containerName = Environment.GetEnvironmentVariable("ContainerName");

            StorageCredentials credentials = new (sasToken);
            CloudStorageAccount storageacc = new (credentials, accountName, endpointSuffix: null, useHttps: true);
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            var blobRef = container.GetBlobReference(blobName);

            try
            {
                await blobRef.DeleteIfExistsAsync();

                return new OkObjectResult($"Deleted file {blobName}.");
            }
            catch (Exception)
            {
                return new NotFoundObjectResult($"Could not delete file {blobName}.");
            }
        }
    }
}

