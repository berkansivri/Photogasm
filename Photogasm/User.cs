namespace Photogasm
{
    public class User
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public string FName { get; set; }
        public string SName { get; set; }
        public string Email { get; set; }
        public string Desc { get; set; }
        public int Online { get; set; } = 0;
        public string ImgPath { get; set; }
        public int totalLike { get; set; }
        public int totalProject { get; set; }
        public int totalPhoto { get; set; }
        public string URL { get; set; }
    }
}