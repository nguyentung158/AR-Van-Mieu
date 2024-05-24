namespace ModelData{ 

    public class Datum
    {
        public Position position { get; set; }
        public Scale scale { get; set; }
        public Rotation rotation { get; set; }
        public string _id { get; set; }
        public string modelId { get; set; }
        public string textContent { get; set; }
        public string imageUrl { get; set; }
        public string audioUrl { get; set; }
        public string videoUrl { get; set; }
        public string viewUrl { get; set; }
        public string downloadUrl { get; set; }
        public string deleteUrl { get; set; }
        public int __v { get; set; }
    }

}