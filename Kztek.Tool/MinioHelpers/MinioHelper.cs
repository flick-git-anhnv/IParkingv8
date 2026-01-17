using Minio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Kztek.Tool
{
    public class MinioConfig
    {
        public string EndPoint = "192.168.20.10:9000";
        public string AccessKey = "CzNXj6PlpqklUS1n7l2U";
        public string SecretKey = "zGTpk9EANk71ayWWOyK9nkvt2XeY1fH47a7o0fPl";
        public bool secure = false;
        public string bucketName = "parking-images";
    }

    /// <summary>
    /// Built with MINIO - 5.0.0
    /// </summary>
    public static class MinioHelper
    {
        public static string EndPoint = string.Empty;
        public static string AccessKey = string.Empty;
        public static string SecretKey = string.Empty;
        public static bool secure = false;
        public static string bucketName = "parking-images";

        public static async Task<string> GetImage(string key)
        {
            try
            {
                MinioClient minio = new MinioClient()
                            .WithEndpoint(EndPoint)
                            .WithCredentials(AccessKey, SecretKey)
                            .WithSSL(secure)
                            .Build()
                            .WithRegion("us-west-rack");
                var getListBucketsTask = await minio.ListBucketsAsync().ConfigureAwait(false);
                PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                                        .WithBucket(bucketName)
                                        .WithObject(key)
                                        .WithExpiry(60 * 60 * 24);
                string url = await minio.PresignedGetObjectAsync(args);
                return url;
            }
            catch (Exception)
            {
                return key;
            }
        }
        public static Stream ToStream(this Image image, ImageFormat format)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
        public static async Task<string> UploadPicture(Image? image, string imageKey)
        {
            try
            {
                if (image == null)
                {
                    return string.Empty;
                }
                MinioClient minio = new MinioClient()
                            .WithEndpoint(EndPoint)
                            .WithCredentials(AccessKey, SecretKey)
                            .WithSSL(secure)
                            .Build()
                            .WithRegion("us-west-rack");

                BucketExistsArgs bucketExistsArgs = new BucketExistsArgs().WithBucket(bucketName);
                bool bucketExists = await minio.BucketExistsAsync(bucketExistsArgs);
                if (!bucketExists)
                {
                    MakeBucketArgs makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);
                    await minio.MakeBucketAsync(makeBucketArgs);
                }

                Stream data = image.ToStream(ImageFormat.Jpeg);
                PutObjectArgs putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithStreamData(data)
                    .WithObjectSize(data.Length)
                    .WithObject(imageKey)
                    //.WithContentType("application/octet-stream");
                    .WithContentType("image/jpeg");

                var response = await minio.PutObjectAsync(putObjectArgs);
                return imageKey;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static async Task<bool> UploadFile(string? fileName, string filePath, string machineName, DateTime logTime)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return false;
                }
                MinioClient minio = new MinioClient()
                            .WithEndpoint(EndPoint)
                            .WithCredentials(AccessKey, SecretKey)
                            .WithSSL(secure)
                            .Build()
                            .WithRegion("us-west-rack");

                BucketExistsArgs bucketExistsArgs = new BucketExistsArgs().WithBucket(bucketName);
                bool bucketExists = await minio.BucketExistsAsync(bucketExistsArgs);
                if (!bucketExists)
                {
                    MakeBucketArgs makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);
                    await minio.MakeBucketAsync(makeBucketArgs);
                }
                PutObjectArgs putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithFileName(filePath)
                .WithObject("support/" + machineName + $@"/{logTime.Year}_{logTime.Month}_{logTime.Day}/" + fileName)
                    .WithContentType("text/plain");
                var response = await minio.PutObjectAsync(putObjectArgs);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
