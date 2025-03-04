namespace ex03
{
    public struct CustomerInfo
    {
        public string CustomerName { get; }
        public string CustomerPhoneNumber { get; }

        public CustomerInfo(string i_CustomerName, string i_CustomerPhoneNumnber)
        {
            CustomerName = i_CustomerName;  
            CustomerPhoneNumber = i_CustomerPhoneNumnber;
        }
    }
}
