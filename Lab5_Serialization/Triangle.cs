using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Lab5_Serialization
{
    [Serializable]
    public class Triangle
    {
        
        public Dot A { get; set; }
        public Dot B { get; set; }
        public Dot C { get; set; }
        
        public Triangle(Dot a, Dot b, Dot c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        public Triangle() {}

        public void ToSql(SqlConnection connection)
        {
            var cmd = new SqlCommand("INSERT INTO [Triangles]" +
                                     "(Ax, Ay, Bx, [By], Cx, Cy)" +
                                     "VALUES(@Ax, @Ay, @Bx, @By, @Cx, @Cy)", connection);
            cmd.Parameters.AddWithValue("@Ax", this.A.X);
            cmd.Parameters.AddWithValue("@Ay", this.A.Y);
            cmd.Parameters.AddWithValue("@Bx", this.B.X);
            cmd.Parameters.AddWithValue("@By", this.B.Y);
            cmd.Parameters.AddWithValue("@Cx", this.C.X);
            cmd.Parameters.AddWithValue("@Cy", this.C.Y);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public static Triangle FromSql(SqlConnection connection, int id)
        {
            var cmd = new SqlCommand("SELECT Ax, Ay, Bx, [By], Cx, Cy FROM [Triangles]" +
                                     "WHERE ID = @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            Triangle result = null;
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                { 
                    result = new Triangle(new Dot((double)reader[0], (double)reader[1]), 
                        new Dot((double)reader[2], (double)reader[3]),
                        new Dot((double)reader[4], (double)reader[5]));
                }
            }

            connection.Close();
            return result;
        }

        public override string ToString()
        {
            return A.ToString() + " " + B.ToString() + " " + C.ToString();
        }
    }
}