using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex03
{
    public struct CustomerInfo
    {
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }

        public CustomerInfo(string i_CustomerName, string i_CustomerPhoneNumnber)
        {
            CustomerName = i_CustomerName;  
            CustomerPhoneNumber = i_CustomerPhoneNumnber;
        }
    }
}
