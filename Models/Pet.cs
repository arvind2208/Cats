namespace Models
{
    public enum PetType { Cat, Dog, Fish };

    public class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
