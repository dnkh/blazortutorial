namespace BlazorTutorial.Components.Classes
{
    public class MenuItem
    {
        public MenuItem()
        {
            Children = new List<MenuItem>();
        }

        public string Link { get; set; }

        public string Text { get; set; }

        public string IconName { get; set; }

        public string MarkDownFileName { get; set; }

        public List<MenuItem> Children { get; set; }

        public string Identifier { get; set; }

        public bool IsSecured { get; set; }
    }
}
