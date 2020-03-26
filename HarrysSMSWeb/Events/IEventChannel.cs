namespace HarrysSMSWeb.Events
{
    public interface IEventChannel
    {
        void Publish(object o);
    }
    public class  NewCustomerCreated
    {
        public string PersonNummer { get; set; }
    }
}