namespace Domain.Data
{
    public record Brand
    {
        private Brand(string name)
        {
            Name = name;
        }

        public string Name { get; init; }

        public static Brand? Create(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length > 255)
            {
                return null;
            }

            return new Brand(name);
        }
    }
}
