using DevQuestions.Application.FilesStorage;

namespace Infrastructure.S3;

public class S3Provider : IFilesProvider
{
    public Task<string> UploadAsync(Stream stream, string key, string bucket) => throw new NotImplementedException();
    public Task<string> GetUrlByIdAsync(Guid fileId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Dictionary<Guid, string>> GetUrlsByIdAsync(IEnumerable<Guid> fileIds, CancellationToken cancellationToken) => throw new NotImplementedException();
}