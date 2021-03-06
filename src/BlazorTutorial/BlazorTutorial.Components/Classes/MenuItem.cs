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

        public List<MenuItem> Children { get; set; }
    }
}
