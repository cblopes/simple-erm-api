namespace SimpleERP.API.Entities
{
    public class Client
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CpfCnpj { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public void Update(string name)
        {
            Name = name;
        }

        public void Active()
        {
            IsActive = true;
        }

        public void Delete()
        {
            IsActive = false;
        }
    }
}
