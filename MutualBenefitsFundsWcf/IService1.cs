using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MutualBenefitsFundsWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string AddCLient(string code,string name,string surname,string tittle,string idnumber,int phonenumber,string postAddress,string ResAddress,int cmdaidpremium,int numdependants,string email, string password);

        [OperationContract]
        string signin(string email, string password);

        [OperationContract]
        string CheckCode(string code);

        [OperationContract]
        string getID(string email, string password);

        [OperationContract]
        Client GetClient(string cid);

        [OperationContract]
        List<Code> GetClientCode(string cid);

        [OperationContract]
        Client GetIdname(string email);

        [OperationContract]
        string SetPassword(string id, string password);

        [OperationContract]
        string UpdateProfile(string c_id,string Name, string Surname,string Tittle,string idnumber,int phonenumber,string paddress,string raddress,int cmnpremium,int numdepn,string email);
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
