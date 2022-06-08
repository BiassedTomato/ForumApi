namespace Shmelev_Backend_Task3
{
    public class BoardViewDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class BoardCreateEditDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class ThreadViewDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class ThreadCreateEditDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class PostViewDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

    }

    public class PostCreateEditDTO
    {
        public string Text { get; set; }

    }
}
