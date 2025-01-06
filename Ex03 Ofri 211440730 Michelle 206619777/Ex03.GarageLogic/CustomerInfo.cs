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
