
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Lab5_Serialization
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var triangle = new Triangle(new Dot(1, 1.25), new Dot(2, 2.5), new Dot(2, 7.5) );
            var serializer = new XmlSerializer(typeof(Triangle));
            using (var stream = new FileStream("/Users/sergei/RiderProjects/sem3_OOPLabs/Lab5_Serialization/Triangles.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(stream, triangle);
            }
            
            using (var stream = new FileStream("/Users/sergei/RiderProjects/sem3_OOPLabs/Lab5_Serialization/Triangles.xml", FileMode.OpenOrCreate))
            {
                var deserializedTriangle = (Triangle)serializer.Deserialize(stream);
                Console.Out.WriteLine(deserializedTriangle);
            }

            
            var dot = new Dot(1, 4.5);
            var binaryFormatter = new BinaryFormatter();
            using (var stream = new FileStream("bindots", FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(stream, dot);
                binaryFormatter.Serialize(stream, dot);
            }
            
            using (var stream = new FileStream("bindots", FileMode.OpenOrCreate))
            {
                var deserializedDot = (Dot)binaryFormatter.Deserialize(stream);
                Console.Out.WriteLine(deserializedDot.ToString());
            }
            
            triangle.ToSql(DBUtils.GetSqlConnection());
            var fromSqlTriangle = Triangle.FromSql(DBUtils.GetSqlConnection(), 0);
            Console.Out.WriteLine(fromSqlTriangle.ToString());
        }
    }
}