namespace TaskControl
{
    public class User
    {
        public string Name { get; set; }
        /// <summary>
        /// User name.
        /// </summary>
        /// <param name="name"></param>
        public User(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}