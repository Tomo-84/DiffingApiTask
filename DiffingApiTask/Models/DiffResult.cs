namespace DiffingApiTask.Models
{
    public class DiffResult
    {
        public string DiffResultType { get; set; }
        public List<Diff> Diffs { get; set; }
    }

    public class Diff
    {
        public int Offset { get; set; }
        public int Length { get; set; }
    }
}
