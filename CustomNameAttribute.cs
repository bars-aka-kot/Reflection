namespace Reflection
{
    internal class CustomNameAttribute(string CustomName) : Attribute
    {
        public string CustomName { get; private set; } = CustomName;
    }
}
