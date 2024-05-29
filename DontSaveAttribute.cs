namespace Reflection
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class DontSaveAttribute : Attribute
    {
        public DontSaveAttribute() { }
    }
}
