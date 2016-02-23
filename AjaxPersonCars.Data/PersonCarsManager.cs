using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjaxPersonCars.Data
{
    public class PersonCarsManager
    {
        private readonly string _connectionString;

        public PersonCarsManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddPerson(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO People VALUES (@firstName, @lastName); SELECT @@Identity";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                connection.Open();
                person.Id = (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public IEnumerable<Person> GetAllPersons()
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                var list = new List<Person>();
                cmd.CommandText = "SELECT * FROM People";
                connection.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var person = new Person
                    {
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["lastName"],
                        Id = (int)reader["Id"]
                    };
                    list.Add(person);
                }

                return list;
            }
        }

        public void AddCar(Car car)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText =
                    "INSERT INTO Cars (Make, Model, Year, PersonId) VALUES (@make, @model, @year, @personId)";
                cmd.Parameters.AddWithValue("@make", car.Make);
                cmd.Parameters.AddWithValue("@model", car.Model);
                cmd.Parameters.AddWithValue("@year", car.Year);
                cmd.Parameters.AddWithValue("@personId", car.PersonId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Car> GetCarsForPerson(int personId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                var list = new List<Car>();
                cmd.CommandText = "SELECT * FROM Cars where PersonId = @personId";
                cmd.Parameters.AddWithValue("@personId", personId);
                connection.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var car = new Car
                    {
                        Make = (string)reader["Make"],
                        Model = (string)reader["Model"],
                        Year = (int)reader["Year"],
                        Id = (int)reader["Id"]
                    };
                    list.Add(car);
                }

                return list;
            }
        }

        public void DeletePersonAndCars(int personId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Cars WHERE PersonId = @personId; DELETE FROM People WHERE Id = @personId";
                cmd.Parameters.AddWithValue("@personId", personId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
