using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;


public interface ICloudinaryService
{
    string UploadImage(IFormFile imageFile);
}

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration configuration)
    {
        var cloudinaryConfig = new Account(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]);

        _cloudinary = new Cloudinary(cloudinaryConfig);
    }

    public string UploadImage(IFormFile imageFile)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill")
        };

        var uploadResult = _cloudinary.Upload(uploadParams);

        return uploadResult.Url.ToString();
    }

}
