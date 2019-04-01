namespace FaceApi
{
    class MyKeys
    {   
        string subscriptionkey = "";
        string storageConnectionString = "";
        string faceEndpoint = "";
        public MyKeys()
        {
            this.Subscriptionkey = "";
            this.StorageConnectionString = "";
            this.FaceEndpoint = "";

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