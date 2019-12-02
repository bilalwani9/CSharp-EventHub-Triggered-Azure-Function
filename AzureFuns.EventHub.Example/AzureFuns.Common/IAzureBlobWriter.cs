namespace AzureFuns.Common
{
    using System; 
    using System.Threading.Tasks;

    public interface IAzureBlobWriter
    {
        Task<Uri> WriteAsync(string containerName, string blobName, string data);
    }
}
