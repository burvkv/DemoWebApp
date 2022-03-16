namespace WebApp.Models
{
    public class MenuItem : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }

    }
}
