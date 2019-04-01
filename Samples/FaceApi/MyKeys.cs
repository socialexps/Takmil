namespace FaceApi
{
    class MyKeys
    {   
        string subscriptionkey = "";
        string storageConnectionString = "";
        string faceEndpoint = "";
        public MyKeys()
        {
            this.Subscriptionkey = "1c0e6d5149394ceaa9c24abbcbad9eab";
            this.StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=yadavsrorageaccount01;AccountKey=PBotd4hx3Z1o3VOYDvohsAhKpypGKp8c5GwdOgge0gQZGEtlFvkbdTIVoPjhw0Dm7QUD/Gc/PwlI4DE1P9yfhg==;EndpointSuffix=core.windows.net";
            this.FaceEndpoint = "https://eastus.api.cognitive.microsoft.com";

        }
        public string Subscriptionkey{
            get
            {
                return this.subscriptionkey;
            }
            private set
            {
                this.subscriptionkey = value;
            }
        }

        public string FaceEndpoint{
            get
            {
                return this.faceEndpoint;
            }
            private set
            {
                this.faceEndpoint = value;
            }
        }
        public string StorageConnectionString{
            get
            {
                return this.storageConnectionString;
            }
            private set
            {
                this.storageConnectionString = value;
            }
        }

    }
}