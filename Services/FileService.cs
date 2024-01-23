using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage;
using FileUploader.Model;

namespace proeduedge.Services
{
    public class FileService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public FileService(string storageAccountName, string key)
        {
            var credential = new StorageSharedKeyCredential(storageAccountName, key);
            var blobUri = $"https://{storageAccountName}.blob.core.windows.net";
            _blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
        }

        public async Task<List<Blob>> ListAsync(string containerName)
        {
            var _filesContainer = _blobServiceClient.GetBlobContainerClient(containerName);
            List<Blob> files = new List<Blob>();
            await foreach (var file in _filesContainer.GetBlobsAsync())
            {
                var name = file.Name;
                var fullUri = _filesContainer.GetBlobClient(name).Uri.ToString();

                files.Add(new Blob
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType,
                });
            }
            return files;
        }

        public async Task<BlobResponse> UploadAsync(string containerName, IFormFile blob)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobResponse response = new BlobResponse();
            BlobClient client = container.GetBlobClient(blob.FileName);

            await using (Stream data = blob.OpenReadStream())
            {
                await client.UploadAsync(data, true);
            }

            response.Status = $"File {blob.FileName} uploaded successfully ;)";
            response.Error = false;
            response.Blob = new Blob
            {
                Uri = client.Uri.AbsoluteUri,
                Name = blob.FileName,
                ContentType = blob.ContentType
            };

            return response;
        }

        public async Task<IEnumerable<BlobResponse>> UploadMultipleAsync(string containerName, IEnumerable<IFormFile> blobs)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var responses = new List<BlobResponse>();

            foreach (var blob in blobs)
            {
                BlobResponse response = new BlobResponse();
                BlobClient client = container.GetBlobClient(blob.FileName);

                await using (Stream data = blob.OpenReadStream())
                {
                    await client.UploadAsync(data, true);
                }

                response.Status = $"File {blob.FileName} uploaded successfully ;)";
                response.Error = false;
                response.Blob = new Blob
                {
                    Uri = client.Uri.AbsoluteUri,
                    Name = blob.FileName,
                    ContentType = blob.ContentType
                };

                responses.Add(response);
            }

            return responses;
        }
        public async Task<Blob> DownloadAsync(string containerName, string blobFilename)
        {
            var _filesContainer = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient file = _filesContainer.GetBlobClient(blobFilename);
            if (await file.ExistsAsync())
            {
                BlobDownloadInfo download = await file.DownloadAsync();

                return new Blob
                {
                    Content = download.Content,
                    Name = blobFilename,
                    ContentType = download.ContentType,
                    Uri = file.Uri.ToString()
                };
            }
            return null;
        }

        public async Task<BlobResponse> DeleteAsync(string containerName, string blobFilename)
        {
            var _filesContainer = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient file = _filesContainer.GetBlobClient(blobFilename);
            await file.DeleteAsync();

            return new BlobResponse
            {
                Error = false,
                Status = $"File {blobFilename} has been successfully deleted :)"
            };
        }
    }
}
