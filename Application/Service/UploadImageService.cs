//public class UploadImageService
//{
//    private readonly IImageRepository _imageRepository;

//    public UploadImageService(IImageRepository imageRepository)
//    {
//        _imageRepository = imageRepository;
//    }

//    public void UploadImage(int referenceId, string type, byte[] imageData, string description)
//    {
//        // Create a new image record related to an exam or surgery
//        var image = new Image
//        {
//            ReferenceId = referenceId,
//            Type = type,
//            ImageData = imageData,
//            Description = description,
//            CreatedAt = DateTime.Now,
//            UpdatedAt = DateTime.Now
//        };

//        _imageRepository.AddAsync(image);
//    }
//}