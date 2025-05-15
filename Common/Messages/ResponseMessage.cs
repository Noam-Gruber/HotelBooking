namespace Common.Messages
{
    public class ResponseMessage<T>
    {
        public int Status { get; set; }
        public T Data { get; set; }
    }
}
