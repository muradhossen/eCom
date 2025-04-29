
namespace Application.DTOs
{
    public class Hierarchy<T>
    {
        public T Key { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public IEnumerable<Hierarchy<T>> Child { get; set; }




    }
}
