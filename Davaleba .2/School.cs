using System;
using System.Data;
using System.Data.SqlClient;

namespace Davaleba_._2
{
    public class School
    {
        // მონაცემთა ბაზის მისამართი
        SqlConnection connectionString = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Davaleba .3;Integrated Security=True");
        public School()
        {
            connectionString.Open();
        }
        // მეთოდი ამატებს ახალ მასწავლებელს Teacher-ში
        public void AddTeacher(string teacherName)
        {
            String querystring = "INSERT INTO Teacher VALUES (@name)";
            SqlCommand sqlCommand = new SqlCommand(querystring, connectionString);
            sqlCommand.Parameters.AddWithValue("@name", teacherName);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = sqlCommand;
            adapter.InsertCommand.ExecuteNonQuery();
            adapter.Dispose();
        }
        // მეთოდი ამატებს საგანს და დამატებამდე ამოწმებს თუ მოცემული სახელის
        // მასწავლებელი არ არსებობს, მეთოდი არაფერს აკეთებს
        public void AddSubject(string subjectName, string teacherName)
        {
            // ვამოწმებთ თუ მსგავსი სახელის მასწავლებელი არ მოიძებნება, მაშინ პროგრამა არაფერს აკეთებს
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            SqlDataReader sqlDataReader = null;
            string querystring = "SELECT COUNT(name) FROM Teacher WHERE name = @name";
            SqlCommand sqlCommand = new SqlCommand(querystring, connectionString);
            sqlCommand.Parameters.AddWithValue("@name", teacherName);
            sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();

            if (sqlDataReader.GetInt32(0) == 0)
            {
                return;
            }

            // ეს ნაწილი ამატებს მასწავლებეს და შესაბამის საგანს
            sqlDataReader.Close();
            dataAdapter = new SqlDataAdapter();
            querystring = "INSERT INTO TeacherSubjectRelation VALUES (@teacher, @subject)";
            sqlCommand = new SqlCommand(querystring, connectionString);
            sqlCommand.Parameters.AddWithValue("@teacher", teacherName);
            sqlCommand.Parameters.AddWithValue("@subject", subjectName);
            dataAdapter = new SqlDataAdapter(querystring, connectionString);
            dataAdapter.InsertCommand = sqlCommand;
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.Dispose();
            sqlDataReader.Close();
        }
        // მეთოდი ამატებს მოსწავლეს შესაბამის საგანზე
        public void AddPupil(string pupilName, string subjectName)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            string queryString = "INSERT INTO PupilSubjectRelation VALUES (@pupilName, @subjectName)";
            SqlCommand sqlCommand = new SqlCommand(queryString, connectionString);
            sqlCommand.Parameters.AddWithValue("@pupilName", pupilName);
            sqlCommand.Parameters.AddWithValue("@subjectName", subjectName);
            dataAdapter = new SqlDataAdapter(queryString, connectionString);
            dataAdapter.InsertCommand = sqlCommand;
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.Dispose();
        }
        // მეთოდი აბრუნებს იმ მასწავლებლებს, რომლებიც ასწავლიან იმ საგნებს, რომლებსაც
        // სწავლობს გადაცემული მოსწავლე. თუ მსგავსი მოსწავლე ვერ მოიძებნა,
        // მეთოდი არაფერს არ აბრუნებს
        public IEnumerable<string> GetTeachers(string pupil)
        {
            SqlDataReader sqlDataReader = null;
            string querystring = "SELECT COUNT(pupilName) FROM PupilSubjectRelation WHERE pupilName = @pupil";
            SqlCommand sqlCommand = new SqlCommand(querystring, connectionString);
            sqlCommand.Parameters.AddWithValue("@pupil", pupil);
            sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            if (sqlDataReader.GetInt32(0) == 0)
            {
                sqlDataReader.Close();
                return null;
            }

            sqlDataReader.Close();
            List<string> teachersList = new List<string>();
            sqlDataReader = null;
            querystring = "SELECT DISTINCT teacherName FROM TeacherSubjectRelation WHERE subjectName IN " +
                "(SELECT DISTINCT subjectName FROM PupilSubjectRelation WHERE pupilName = @pupil)";
            sqlCommand = new SqlCommand(querystring, connectionString);
            sqlCommand.Parameters.AddWithValue("@pupil", pupil);
            sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                teachersList.Add(sqlDataReader.GetString(0));
            }

            sqlDataReader.Close();

            return teachersList;
        }
        // მეთოდი აბრუნებს იმ მოსწავლეებს, რომლებიც სწავლობენ იმ მასწავლებლის
        // საგნებს, რომლებსაც გადავცემთ ფუნქციას არგუმენტად
        public IEnumerable<string> GetPupils(string teacher)
        {
            SqlDataReader sqlDataReader = null;
            string querystring = "SELECT COUNT(name) FROM Teacher WHERE name = @teacher";
            SqlCommand sqlCommand = new SqlCommand(querystring, connectionString);
            sqlCommand.Parameters.AddWithValue("@teacher", teacher);
            sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            if (sqlDataReader.GetInt32(0) == 0)
            {
                sqlDataReader.Close();
                return null;
            }

            sqlDataReader.Close();
            List<string> teachersList = new List<string>();
            sqlDataReader = null;
            querystring = "SELECT DISTINCT pupilName FROM PupilSubjectRelation WHERE subjectName IN " +
                "(SELECT DISTINCT subjectName FROM TeacherSubjectRelation WHERE teacherName = @teacher)";
            sqlCommand = new SqlCommand(querystring, connectionString);
            sqlCommand.Parameters.AddWithValue("@teacher", teacher);
            sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                teachersList.Add(sqlDataReader.GetString(0));
            }

            sqlDataReader.Close();

            return teachersList;
        }
        // მეთოდი შლის მასწავლებელს, რომლის სახელსაც გადავცემთ
        public void RemoveTeacher(string teacher)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            string queryString = "DELETE * FROM Teacher WHERE name = @teacher";
            SqlCommand sqlCommand = new SqlCommand(queryString, connectionString);
            sqlCommand.Parameters.AddWithValue("@teacher", teacher);
            dataAdapter = new SqlDataAdapter(queryString, connectionString);
            dataAdapter.InsertCommand = sqlCommand;
            dataAdapter.InsertCommand.ExecuteNonQuery();

            dataAdapter.Dispose();
        }
    }
}