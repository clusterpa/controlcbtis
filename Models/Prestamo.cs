using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace controlcbtis.Models
{
    public class Prestamo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string NombreUsuario { get; set; }
        public string TipoUsuario { get; set; } 
        public string Articulo { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
}
