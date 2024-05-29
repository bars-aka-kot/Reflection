using System.Reflection;
using System.Text;

namespace Reflection
{
    class Program
    {
        static object StringToObject(string s)
        {
            Object obj = new Object();
            if (s == null) return false;
            var o = s.Split("|");
            obj = Activator.CreateInstance(null, o[0])?.Unwrap() ?? 0;
            Console.WriteLine($"\n{obj?.ToString()}");
            Type type = obj.GetType();
            var properties = type.GetProperties();
            for (int i = 1; i < o.Length - 2; i += 3)
            {
                Console.WriteLine("--------");
                Console.Write($"{o[i]}\n");
                Console.Write($"{o[i + 1]}\n");
                Console.Write($"{o[i + 2]}\n");
                var pr = type.GetProperty(o[i + 1]);
                if (pr?.PropertyType == typeof(int))
                {
                    pr.SetValue(obj, int.Parse(o[i + 2]));
                }
                if (pr?.PropertyType == typeof(string))
                {
                    pr.SetValue(obj, o[i + 2]);
                }
                if (pr?.PropertyType == typeof(char[]))
                {
                    pr.SetValue(obj, (o[i + 2]).ToCharArray());
                }
                if (pr?.PropertyType == typeof(decimal))
                {
                    pr.SetValue(obj, decimal.Parse(o[i + 2]));
                }
            }
            return obj;
        }
        static string ObjectToString(object o)
        {
            StringBuilder sb = new();
            sb.Append(o.GetType().ToString() + "|");
            Type type = o.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<DontSaveAttribute>() != null)
                {
                    continue;
                }
                sb.Append(property.PropertyType + "|");
                sb.Append(property.Name + "|");
                if (property.PropertyType == typeof(char[]))
                {
                    if (property.GetValue(o, null) != null)
                    {
                        foreach (var value in property.GetValue(o, null) as char[])
                        {
                            sb.Append(value + "|");
                        }
                    }
                }
                else { sb.Append(property.GetValue(o) + "|"); }
            }
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttribute<CustomNameAttribute>();
                if (attribute != null)
                {
                    sb.Append(attribute.CustomName + "=");
                    var fieldVal = field.GetValue(o);
                    sb.Append(fieldVal + "\n");
                }
            }
            return sb.ToString();
        }
        static void Main()
        {
            TestClass obt = new (2, ['a'] , "asdasd", 3.05m, "Hello!");
            Console.WriteLine(ObjectToString(obt));

            string str = ObjectToString(obt);
            var obj = StringToObject(str);

            Console.WriteLine($"\n{ObjectToString(obj)}");
        }
    }
}
