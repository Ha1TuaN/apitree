namespace apitree.Database
{
    public class Tree
    {
        public int Id { get; set; }
        public string title { get; set; }
        public bool isDisplay { get; set; }
        public List<Tree>? Children { get; set; }
    }
}
