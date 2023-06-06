using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MutualBenefitsFundsWcf
{
    public class Code
    {
        private int cd_id;
        private string c_id;
        private string c_code;
        private string cd_code;
        private string cd_status;
        private string cd_regid;
        private string cd_incval;

        public Code()
        {

        }

        public Code(int cd_id, string c_id, string c_code, string cd_code, string cd_status, string cd_regid, string cd_incval)
        {
            this.Cd_id = cd_id;
            this.C_id = c_id;
            this.C_code = c_code;
            this.Cd_code = cd_code;
            this.Cd_status = cd_status;
            this.Cd_regid = cd_regid;
            this.Cd_incval = cd_incval;
        }

        public int Cd_id { get => cd_id; set => cd_id = value; }
        public string C_id { get => c_id; set => c_id = value; }
        public string C_code { get => c_code; set => c_code = value; }
        public string Cd_code { get => cd_code; set => cd_code = value; }
        public string Cd_status { get => cd_status; set => cd_status = value; }
        public string Cd_regid { get => cd_regid; set => cd_regid = value; }
        public string Cd_incval { get => cd_incval; set => cd_incval = value; }
    }
}