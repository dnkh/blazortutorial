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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace AzureFunctionsMarkdownFiles
{
    public static class UploadMarkdownFile
    {
        [OpenApiOperation(operationId: "uploadMarkdownFile", tags: new[] { "uploadMarkdownFile" }, Summary = "Uploads a markdown file", Description = "Uploads a markdown file.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(MultiPartFormDataModel), Required = true, Description = "File")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "response", Description = "This returns the response.")]
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

    public class MultiPartFormDataModel
    {
        public byte[] File { get; set; }
    }
}