using MongoDB.Bson;
namespace Domain.Entities
{
    public class Rota
    {
        public ObjectId _id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public double Valor { get; set; }
        public List<string>? Paradas { get; set; } = new List<string>();
    }
}
