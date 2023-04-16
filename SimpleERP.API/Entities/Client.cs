namespace SimpleERP.API.Entities
{
    public class Client
    {
        public Client()
        {
            IsActive = true;
        }

        public Guid Id { get; set; }
        public string CpfCnpj { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public void Update(string name)
        {
            Name = name;
        }

        public void Delete()
        {
            IsActive = false;
        }
    }
}
