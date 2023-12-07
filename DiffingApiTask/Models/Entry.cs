namespace DiffingApiTask.Models
{
    //[Keyless]
    public class Entry
    {
        //[Key]
        public int Id { get; set; }

        public string? Left { get; set; }
        public string? Right { get; set; }
    }
}
