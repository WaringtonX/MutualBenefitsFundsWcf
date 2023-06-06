using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MutualBenefitsFundsWcf
{
    public class Client
    {
        private string id;
        private string name;
        private string surname;
        private string tittle;
        private string idnumber;
        private int phonenumber;
        private string paddress;
        private string raddress;
        private int cmpremium;
        private int numdepandts;
        private string code;
        private string email;

        public Client()
        {

        }

        public Client(string id, string name, string surname, string tittle, string idnumber, int phonenumber, string paddress, string raddress, int cmpremium, int numdepandts, string code, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Tittle = tittle;
            this.Idnumber = idnumber;
            this.Phonenumber = phonenumber;
            this.Paddress = paddress;
            this.Raddress = raddress;
            this.Cmpremium = cmpremium;
            this.Numdepandts = numdepandts;
            this.Code = code;
            this.Email = email;
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Tittle { get => tittle; set => tittle = value; }
        public string Idnumber { get => idnumber; set => idnumber = value; }
        public int Phonenumber { get => phonenumber; set => phonenumber = value; }
        public string Paddress { get => paddress; set => paddress = value; }
        public string Raddress { get => raddress; set => raddress = value; }
        public int Cmpremium { get => cmpremium; set => cmpremium = value; }
        public int Numdepandts { get => numdepandts; set => numdepandts = value; }
        public string Code { get => code; set => code = value; }
        public string Email { get => email; set => email = value; }
    }
}