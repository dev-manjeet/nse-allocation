namespace FormUpload
{
    internal class FileParameter
    {
        private object data;
        private string v1;
        private string v2;

        public FileParameter(object data, string v1, string v2)
        {
            this.data = data;
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}