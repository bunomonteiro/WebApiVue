namespace AspCoreVuejs.Api.Model.Entities
{
    public class Foo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public Foo() { }
        public Foo(int id, string name, bool isActive = true)
        {
            this.Id = id;
            this.Name = name;
            this.IsActive = isActive;
        }
    }
}
