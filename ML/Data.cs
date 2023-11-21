using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Data
    {
        public string PROFIT { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string JobOperator { get; set; }
        public DateTime JobRevRecog { get; set; }
        public decimal Revenue { get; set; }
        public int WIP { get; set; }
        public decimal Cost { get; set; }
        public int Accrual { get; set; }
        public decimal JobProfit { get; set; }
        public string Stat { get; set; }
        public string HoldReason { get; set; }
        public string Reason { get; set; }
        public long LocalClientCode { get; set; }
        public string LocalClientName { get; set; }
        public string Rep { get; set; }
    }
}
