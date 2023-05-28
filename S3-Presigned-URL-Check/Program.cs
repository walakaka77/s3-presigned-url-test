// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Diagnostics;

namespace Amazon.DocSamples.S3
{
    class GenPresignedURLTest
    {
        private const string bucketName = "walakaka-private-bucket";
        private const string objectKey = "TestFile.txt";
        // Specify how long the presigned URL lasts, in hours
        private const double timeoutDuration = 1;
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSoutheast1;
        private static IAmazonS3 s3Client;

        public static void Main()
        {
            //s3Client = new AmazonS3Client(bucketRegion);
            s3Client = new AmazonS3Client("Access key ID", "Secret access key");
            string urlString = GeneratePreSignedURL(timeoutDuration);
        }
        static string GeneratePreSignedURL(double duration)
        {
            string urlString = "";
            try
            {
                //Console.WriteLine("I have reached GetPresignedURLRequest");
                GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = objectKey,
                    Expires = DateTime.UtcNow.AddHours(duration)
                };
                urlString = s3Client.GetPreSignedURL(request1);
                //Console.WriteLine("I have passed the try");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            Console.WriteLine("I am returning {0}", urlString);
            return urlString;
        }
    }
}
