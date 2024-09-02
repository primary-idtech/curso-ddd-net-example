namespace CleanArchitecture.Domain.Models
{
    public partial class Portfolio
    {
        public long Id { get; private set; }
        public PortfolioName Name { get; private set; }
        public bool Enabled { get; private set; }

        private Portfolio() { }

        public Portfolio(string name)
        {
            this.Name = PortfolioName.Create(name);
            this.Enabled = true;
        }

        public Portfolio Update(string name, bool enabled)
        {
            this.Name = PortfolioName.Create(name);
            this.Enabled = enabled;
            return this;
        }
    }
}
