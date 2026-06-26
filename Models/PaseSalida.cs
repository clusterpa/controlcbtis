using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace controlcbtis.Models
{
    public class PaseSalida
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string NombreDocente { get; set; }

        public DateTime Fecha { get; set; }

        public string HoraSalida { get; set; }

        public string HoraRegreso { get; set; }

        public string Asunto { get; set; }
    }
}
