using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


namespace FaceApi
{   
    class Program
    {   

        // You must use the same region as you used to get your subscription
        // keys. For example, if you got your subscription keys from westus,
        // replace "westcentralus" with "westus".
        //
        // Free trial subscription keys are generated in the westcentralus
        // region. If you use a free trial subscription key, you shouldn't
        // need to change the region.
        // Specify the Azure region

        
        
        // localImagePath = @"C:\Documents\LocalImage.jpg"
        private const string localImagePath = @"<LocalImage>";
       
        //"https://takmil.org/wp-content/uploads/2018/03/img-20180203-wa0004.jpg";
        //"https://upload.wikimedia.org/wikipedia/commons/3/37/Dagestani_man_and_woman.jpg";

        private static readonly FaceAttributeType[] faceAttributes = { FaceAttributeType.Age, FaceAttributeType.Gender };

        private static int totalFaceDetected = 0;
        private static int femaleDetected = 0;
        private static int maleDetected = 0;    
        
        static void Main(string[] args) 
        {   
            var remoteImageUrl = storagefunc();
            Console.WriteLine("Welcome to Face API at work ....!");
            MyKeys mykey = new MyKeys();
            string subscriptionkey = mykey.Subscriptionkey;
            string faceEndpoint = mykey.FaceEndpoint;
            FaceClient faceClient = new FaceClient (
                new ApiKeyServiceClientCredentials(subscriptionkey),
                new System.Net.Http.DelegatingHandler[] { });
            faceClient.Endpoint = faceEndpoint;
            Console.WriteLine("Face being detected . . .");
            var t1 = DetectedRemoteAsync(faceClient , remoteImageUrl);
            
            Task.WhenAll(t1).Wait(5000);
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();    
        }
        // It uses the Face service client to detect faces in a remote image, referenced by a URL. 
        // Note that it uses the faceAttributes field—the DetectedFace objects added to faceList will have the specified attributes (in this case, age and gender)
        private static async Task DetectedRemoteAsync(FaceClient faceClient, string ImageUrl)
        {
            if(!Uri.IsWellFormedUriString(ImageUrl, UriKind.Absolute))
            {
                Console.WriteLine("\n Invalid remoteImageurl : \n {0} \n",ImageUrl);
                return;
            }

            try
            {
                IList<DetectedFace> faceList = 
                    await faceClient.Face.DetectWithUrlAsync(
                        ImageUrl, true , false , faceAttributes);
                DisplayAttributes(GetFaceAttributes(faceList,ImageUrl), ImageUrl);
                Console.WriteLine("Total Face detected :: {0}",totalFaceDetected);
                Console.WriteLine("Total Female detected :: {0}",femaleDetected);
                Console.WriteLine("Total Male detected :: {0}",maleDetected);
            }
            catch (APIErrorException e)
            {
                Console.WriteLine(ImageUrl + ":" + e.Message);
            }
            
        }
        // the GetFaceAttributes method. It returns a string with the relevant attribute information.
        private static string GetFaceAttributes (
            IList<DetectedFace> faceList, string imagePath)
            {
                string attributes = string.Empty;

                foreach (DetectedFace face in faceList)
                {
                    double? age = face.FaceAttributes.Age;
                    string gender = face.FaceAttributes.Gender.ToString();
                    attributes += gender + " " + age + "   " + "\n";
                    if(gender == "Female")
                        femaleDetected ++;
                    else
                        maleDetected ++;
                    totalFaceDetected ++;
                }
                return attributes;
            }
        private static void DisplayAttributes(string attributes, string imageUri)
        {
            Console.WriteLine(imageUri);
            Console.WriteLine(attributes + "\n");
        }

        private static string storagefunc()
        {
            // Retrieve the connection string for use with the application. The storage connection string is stored
            // in an environment variable on the machine running the application called storageconnectionstring.
            // If the environment variable is created after the application is launched in a console or with Visual
            // Studio, the shell or application needs to be closed and reloaded to take the environment variable into account.
            
            string storageConnectionString = "";
            MyKeys mykey = new MyKeys();
            storageConnectionString = mykey.StorageConnectionString;
            string primaryUri = "";
            // Check whether the connection string can be parsed.
            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                // If the connection string is valid, proceed with operations against Blob storage here.
                // List the blobs in the container.
              
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("takmil");
                CloudBlob blob = container.GetBlobReference("takmildemo.jpg");
                primaryUri = blob.StorageUri.PrimaryUri.ToString();
                Console.WriteLine(primaryUri);
                return primaryUri;
            }
            else
            {
                // Otherwise, let the user know that they need to define the environment variable.
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add an environment variable named 'storageconnectionstring' with your storage " +
                    "connection string as a value.");
                Console.WriteLine("Press any key to exit the sample application.");
                Console.ReadLine();
                return null;
            }
        }

}
}
