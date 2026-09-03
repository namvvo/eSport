namespace Media.API.Models
{
    public class MediaAsset
    {
        public int Id { get; set; }
        /// <summary>
        /// R2 object key, e.g. "player/42/original.webp".
        /// </summary>
        public string StorageKey { get; set; } = null!;
        public string OwnerType { get; set; } = null!;

        public int OwnerId { get; set; }

        public string AssetName { get; set; } = null!;

        public string OriginalFileName { get; set; } = null!;

        public string ContentType { get; set; } = null!;

        public long Length { get; set; }

        public string? ETag { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? UploadedOnUtc { get; set; }

        //public bool IsAvailable { get; set; }
        public MediaUploadStatus Status { get; set; }
    }
}
