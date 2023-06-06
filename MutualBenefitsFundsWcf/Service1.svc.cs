using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MutualBenefitsFundsWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        SqlCommand command;
        SqlDataReader reader;
       // SqlConnection Connection = new SqlConnection(@"Data Source = SQL5052.site4now.net; Initial Catalog = DB_A57091_MutualBenefitFunds; User Id = DB_A57091_MutualBenefitFunds_admin; Password=maria@082");
       // SqlConnection Connection = new SqlConnection(@"Data Source=154.0.170.55; Initial Catalog = opgteehd_mutualfundsbenefits;User ID=opgteehd_warington;Encrypt=False;Packet Size=4096; Password=Hlawulo@082");
         SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MutualBenefitFundsData.mdf;Integrated Security=True");
        string isLogedin = "false";
        private static Random random = new Random();

        public string AddCLient(string code,string name, string surname, string tittle, string idnumber, int phonenumber, string postAddress, string ResAddress, int cmdaidpremium, int numdependants,string email,string password)
        {

            string qryStrr = "SELECT * FROM [Codes]";
            string CD_UID= null;
            string CODE = null;
            int incVal = 0;

            this.command = new SqlCommand(qryStrr);
            this.command.CommandType = CommandType.Text;
            this.command.Connection = Connection;
            this.command.Connection.Open();
            this.command.ExecuteNonQuery();
            this.reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader["CD_CODE"].ToString().Equals(code))
                    {
                        CD_UID = reader["CD_CREGID"].ToString();
                        CODE = reader["CD_CODE"].ToString();
                        incVal = Int32.Parse(reader["CD_INCVAl"].ToString());
                    }
                }

            }
            // close connection
            Connection.Close();
            //creating a sql command line
            if(CODE != null)
            { 
                //creating a sql command line
                string qryStr = "INSERT INTO [Clients] VALUES (@cid,@name,@surname,@tittle,@idnumber,@phonenumber,@postAddress,@ResAddress,@cmdaidpremium,@numdependants,@code,@email,@password)";

                // implement a connection
                this.command = new SqlCommand(qryStr);
                this.command.Connection = Connection;
                this.command.CommandType = CommandType.Text;
                this.command.Connection.Open();

                this.command.Parameters.AddWithValue("@cid", CD_UID);
                this.command.Parameters.AddWithValue("@name", name);
                this.command.Parameters.AddWithValue("@surname", surname);
                this.command.Parameters.AddWithValue("@tittle", tittle);
                this.command.Parameters.AddWithValue("@idnumber", idnumber);
                this.command.Parameters.AddWithValue("@phonenumber", phonenumber);
                this.command.Parameters.AddWithValue("@postAddress", postAddress);
                this.command.Parameters.AddWithValue("@ResAddress", ResAddress);
                this.command.Parameters.AddWithValue("@cmdaidpremium", cmdaidpremium);
                this.command.Parameters.AddWithValue("@numdependants", numdependants);
                this.command.Parameters.AddWithValue("@code", RandomString());
                this.command.Parameters.AddWithValue("@email", email);
                this.command.Parameters.AddWithValue("@password", Secrecy.HashPassword(password));

                // execute query 
                this.command.ExecuteNonQuery();

                // close connection
                Connection.Close();

                GenCode(email, password, CODE, CD_UID, incVal);
                updateCodeSatus(CODE, "Used");
                return "Client Added";

            }else
            {
                return "Not Found";
            }

        }

        private void GenCode(string email, string password, string code, string C_id, int incVal)
        {
            string qryStr = "SELECT * FROM [Clients]";
            string C_ID = "";
          //  string tempcode = "";

            this.command = new SqlCommand(qryStr);
            this.command.CommandType = CommandType.Text;
            this.command.Connection = Connection;
            this.command.Connection.Open();
            this.command.ExecuteNonQuery();
            this.reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if ((reader["C_EMAIL"].ToString() == email) && (reader["C_PASSWORD"].ToString() == Secrecy.HashPassword(password)))
                    {
                        C_ID = reader["C_ID"].ToString();
                       // tempcode = reader["C_CODE"].ToString();
                    }
                }

            }         
            // close connection
            Connection.Close();
            //creating a sql command line */


            int a = incVal + 1;
            int val;
            if (getLastColumn() == null) {              
                val = 0;
            }
            else
            {
               val = Int32.Parse(getLastColumn());
            }
            int vals = val + 11;
            for (int i = 0; i < 10;i++)
            {
               
                
                string status = "Ready";
                string qryStrs = "INSERT INTO [Codes] VALUES (@cid,@ccode,@cdcode,@cdstatus,@reg,@inc)";

                // implement a connection
                this.command = new SqlCommand(qryStrs);
                this.command.Connection = Connection;
                this.command.CommandType = CommandType.Text;
                this.command.Connection.Open();

                this.command.Parameters.AddWithValue("@cid", C_ID);
                this.command.Parameters.AddWithValue("@ccode", code);
                this.command.Parameters.AddWithValue("@cdcode", RandomString());
                this.command.Parameters.AddWithValue("@cdstatus", status);
                this.command.Parameters.AddWithValue("@reg", getID(a));
                this.command.Parameters.AddWithValue("@inc", vals);

                // execute query 
                this.command.ExecuteNonQuery();

                // close connection
                Connection.Close();
                vals = vals + 11;
                a++;
            }
            
        }

        private static string RandomString()
        {
            int length = 10;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string getID(int val)
        {
            string a = "000000";
            string b = "00000";
            string c = "0000";
            string d = "000";
            string e = "00";
            string f = "0";

            int count = 0;
            string id = "";
            int val2 = val;

            while (val2 != 0)
            {

                val2 /= 10;
                ++count;
            }

            if (count == 1)
            {
                id += a + val;
            }
            else if (count == 2)
            {
                id += b + val;
            }
            else if (count == 3)
            {
                id = c + val;
            }
            else if (count == 4)
            {
                id += d + val;
            }
            else if (count == 5)
            {
                id += e + val;
            }
            else if (count == 6)
            {
                id += f + val;
            }
            else if (count > 6)
            {
                id += val;
            }

            return id;
        }

        public string getLastColumn()
        {
            string qryStr = "SELECT TOP 1 * FROM [Codes] ORDER BY CD_INCVAl DESC";
            string Last = "";

            this.command = new SqlCommand(qryStr);
            this.command.CommandType = CommandType.Text;
            this.command.Connection = Connection;
            this.command.Connection.Open();
            this.command.ExecuteNonQuery();
            this.reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Last = reader["CD_INCVAl"].ToString();
                }

            }
            // close connection
            Connection.Close();
            //creating a sql command line

            return Last;
        }

        public void updateCodeSatus(string code,string status)
        {
            //creating a sql command line
            string qryStr = "UPDATE Codes SET CD_STATUS=@status WHERE CD_CODE=@code";

            // implement a connection
            this.command = new SqlCommand(qryStr);
            this.command.Connection = Connection;
            this.command.CommandType = CommandType.Text;
            this.command.Connection.Open();

            this.command.Parameters.AddWithValue("@code", code);
            this.command.Parameters.AddWithValue("@status", status);
           
            // execute query 
            this.command.ExecuteNonQuery();

            // close connection
            Connection.Close();
        }

        public string CheckCode(string code)
        {
            string qryStrr = "SELECT * FROM [Codes]";
            string status = "false";

            this.command = new SqlCommand(qryStrr);
            this.command.CommandType = CommandType.Text;
            this.command.Connection = Connection;
            this.command.Connection.Open();
            this.command.ExecuteNonQuery();
            this.reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader["CD_CODE"].ToString().Equals(code))
                    {
                        if(reader["CD_STATUS"].ToString().Equals("Ready"))
                        {
                            status = "true";
                        }
                        else if (reader["CD_STATUS"].ToString().Equals("Used"))
                        {
                            status = "Code Used";
                        }
                    }
                }

            }
            // close connection
            Connection.Close();

            return status;
        }

        public string signin(string email, string password)
        {
            string qryStr = "SELECT * FROM [Clients]";

            this.command = new SqlCommand(qryStr);
            this.command.CommandType = CommandType.Text;
            this.command.Connection = Connection;
            this.command.Connection.Open();
            this.command.ExecuteNonQuery();
            this.reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if ((reader["C_EMAIL"].ToString() == email) && (reader["C_PASSWORD"].ToString() == Secrecy.HashPassword(password)))
                    {
                        isLogedin = "true";
                    }
                }

            }
            return isLogedin;
        }

        public string getID(string email, string password)
        {
            string qryStr = "SELECT * FROM [Clients]";
            string ID = "";

            this.command = new SqlCommand(qryStr);
            this.command.CommandType = CommandType.Text;
            this.command.Connection = Connection;
            this.command.Connection.Open();
            this.command.ExecuteNonQuery();
            this.reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if ((reader["C_EMAIL"].ToString() == email) && (reader["C_PASSWORD"].ToString() == Secrecy.HashPassword(password)))
                    {
                        ID = reader["C_ID"].ToString();
                    }
                }

            }
            return ID;
        }

        public string SetPassword(string id, string password)
        {
            //creating a sql command line
            string qryStr = "UPDATE Clients SET C_PASSWORD=@password WHERE C_ID=@cid";

            // implement a connection
            this.command = new SqlCommand(qryStr);
            this.command.Connection = Connection;
            this.command.CommandType = CommandType.Text;
            this.command.Connection.Open();


            this.command.Parameters.AddWithValue("@cid", id);
            this.command.Parameters.AddWithValue("@password", Secrecy.HashPassword(password));

            // execute query 
            this.command.ExecuteNonQuery();

            // close connection
            Connection.Close();

            return "Success";
        }


        public Client GetIdname(string email)
        {
            string qryStr = "SELECT * FROM [Clients] WHERE C_EMAIL=@email";

            Client client = new Client();

            this.command = new SqlCommand(qryStr);
            this.command.CommandType = CommandType.Text;
            this.command.Connection = Connection;
            this.command.Connection.Open();

            this.command.Parameters.AddWithValue("@email", email);

            this.command.ExecuteNonQuery();
            this.reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    client.Id = reader["C_ID"].ToString();
                    client.Name = reader["C_NAME"].ToString();
                    client.Surname = reader["C_SURNAME"].ToString();
                    client.Tittle = reader["C_TITLE"].ToString();
                    client.Idnumber = reader["C_IDNUMBRT"].ToString();
                    client.Phonenumber = Int32.Parse(reader["C_PHONENUMBER"].ToString());
                    client.Paddress = reader["C_PADRESS"].ToString();
                    client.Raddress = reader["C_RESADDRESS"].ToString();
                    client.Cmpremium = Int32.Parse(reader["C_CMDAIDPREMIUM"].ToString());
                    client.Numdepandts = Int32.Parse(reader["C_NUMDEPENDANTS"].ToString());
                    client.Code = reader["C_CODE"].ToString();
                    client.Email = reader["C_EMAIL"].ToString();
                }

            }
            return client;
        }

        public Client GetClient(string cid)
        {
            string qryStr = "SELECT * FROM [Clients] WHERE C_ID=@cid";

            Client client = new Client();

            this.command = new SqlCommand(qryStr);
            this.command.CommandType = CommandType.Text;
            this.command.Connection = Connection;
            this.command.Connection.Open();

            this.command.Parameters.AddWithValue("@cid", cid);

            this.command.ExecuteNonQuery();
            this.reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    client.Id = reader["C_ID"].ToString();
                    client.Name = reader["C_NAME"].ToString();
                    client.Surname = reader["C_SURNAME"].ToString();
                    client.Tittle = reader["C_TITLE"].ToString();
                    client.Idnumber = reader["C_IDNUMBRT"].ToString();
                    client.Phonenumber = Int32.Parse(reader["C_PHONENUMBER"].ToString());
                    client.Paddress = reader["C_PADRESS"].ToString();
                    client.Raddress = reader["C_RESADDRESS"].ToString();
                    client.Cmpremium = Int32.Parse(reader["C_CMDAIDPREMIUM"].ToString());
                    client.Numdepandts = Int32.Parse(reader["C_NUMDEPENDANTS"].ToString());
                    client.Code = reader["C_CODE"].ToString();
                    client.Email = reader["C_EMAIL"].ToString();
                }

            }
            return client;
        }

        public List<Code> GetClientCode(string cid)
        {
            string qryStr = "SELECT * FROM [Codes] WHERE C_ID=@cid";

            List<Code> codes = new List<Code>();

            this.command = new SqlCommand(qryStr);
            this.command.CommandType = CommandType.Text;
            this.command.Connection = Connection;
            this.command.Connection.Open();

            this.command.Parameters.AddWithValue("@cid", cid);

            this.command.ExecuteNonQuery();
            this.reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Code code = new Code();

                    code.Cd_id = Int32.Parse(reader["CD_ID"].ToString());
                    code.C_id = reader["C_ID"].ToString();
                    code.C_code = reader["C_CODE"].ToString();
                    code.Cd_code = reader["CD_CODE"].ToString();
                    code.Cd_status = reader["CD_STATUS"].ToString();
                    code.Cd_regid = reader["CD_CREGID"].ToString();
                    code.Cd_incval = reader["CD_INCVAl"].ToString();
                    codes.Add(code);
                }

            }
            return codes;
        }

        public string UpdateProfile(string c_id, string Name, string Surname, string Tittle, string idnumber, int phonenumber, string paddress, string raddress, int cmnpremium, int numdepn, string email)
        {
            //creating a sql command line
            string qryStr = "UPDATE Clients SET C_NAME=@name,C_SURNAME=@surname,C_TITLE=@tittle,C_IDNUMBRT=@idnumber,C_PHONENUMBER=@phone,C_PADRESS=@paddress,C_RESADDRESS=@raddres,C_CMDAIDPREMIUM=@cmpremm,C_NUMDEPENDANTS=@numdepan,C_EMAIL=@gmail WHERE C_ID=@cid";

            // implement a connection
            this.command = new SqlCommand(qryStr);
            this.command.Connection = Connection;
            this.command.CommandType = CommandType.Text;
            this.command.Connection.Open();


            this.command.Parameters.AddWithValue("@cid", c_id);
            this.command.Parameters.AddWithValue("@name", Name);
            this.command.Parameters.AddWithValue("@surname", Surname);
            this.command.Parameters.AddWithValue("@tittle", Tittle);
            this.command.Parameters.AddWithValue("@idnumber", idnumber);
            this.command.Parameters.AddWithValue("@phone", phonenumber);
            this.command.Parameters.AddWithValue("@paddress", paddress);
            this.command.Parameters.AddWithValue("@raddres", raddress);
            this.command.Parameters.AddWithValue("@cmpremm", cmnpremium);
            this.command.Parameters.AddWithValue("@numdepan", numdepn);
            this.command.Parameters.AddWithValue("@gmail", email);

            // execute query 
            this.command.ExecuteNonQuery();

            // close connection
            Connection.Close();

            return "Success";
        }
    }
}
