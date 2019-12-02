namespace AzureFuns.Common
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.Threading.Tasks;

    public class AzureBlobWriter : IAzureBlobWriter
    {
        private readonly CloudBlobClient _blobClient;

        public AzureBlobWriter(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            _blobClient = storageAccount.CreateCloudBlobClient();
        }


        public async Task<Uri> WriteAsync(string containerName, string blobName, string data)
        {
            var container = _blobClient.GetContainerReference(containerName);

            var blob = container.GetBlockBlobReference(blobName);
            blob.Properties.ContentType = "application/json";

            await blob.UploadTextAsync(data);

            return blob.Uri;
        }
    }
}
