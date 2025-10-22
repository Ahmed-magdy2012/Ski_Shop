namespace SKINET.Server.Entities.Specifictions
{
    public class ProductParams
    {
        private const int Maxpagesize = 50;
        public int Pageindex { get; set; } = 1;
        private int _Pagesize=6;
        public int Pagesize
        {
            get => _Pagesize; 
            set => _Pagesize = value>Maxpagesize?Maxpagesize:value; 
        }


        private List<string>? _brand;
        public List<string> Brand
        {
            get => _brand ?? [];
            set => _brand = value.SelectMany(x=>x.Split(",",StringSplitOptions.RemoveEmptyEntries)).ToList();
        }

        private List<string> _types = [];
        public List<string> Types
        {
            get => _types;
            set => _types = value.SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
        public string? Sort { get;set; }
        private string? _search;

        public string Search
        {
            get =>  _search??""; 
            set { _search = value.ToLower(); }
        }

    }
}
