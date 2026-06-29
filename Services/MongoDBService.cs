using MongoDB.Driver;
using controlcbtis.Models;
using Microsoft.Extensions.Configuration;

namespace controlcbtis.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Alumno> _alumnosCollection;
        private readonly IMongoCollection<Articulo> _articulosCollection;
        private readonly IMongoCollection<Prestamo> _prestamosCollection;
        private readonly IMongoCollection<Usuario> _usuariosCollection;
        private readonly IMongoCollection<Docente> _docentesCollection;
        private readonly IMongoCollection<PaseSalida> _pasesCollection;

        public MongoDBService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);

            _alumnosCollection = database.GetCollection<Alumno>("Alumnos");
            _articulosCollection = database.GetCollection<Articulo>("Articulos");
            _prestamosCollection = database.GetCollection<Prestamo>("Prestamos");
            _usuariosCollection = database.GetCollection<Usuario>("Usuarios");
            _docentesCollection = database.GetCollection<Docente>("Docentes");
            _pasesCollection = database.GetCollection<PaseSalida>("PasesSalida");
        }

        public async Task<List<Alumno>> GetAlumnosAsync()
        {
            return await _alumnosCollection.Find(_ => true).ToListAsync();
        }

        public async Task CreateAlumnoAsync(Alumno alumno)
        {
            await _alumnosCollection.InsertOneAsync(alumno);
        }

        public async Task DeleteAlumnoAsync(string id)
        {
            await _alumnosCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task UpdateAlumnoAsync(Alumno alumno)
        {
            await _alumnosCollection.ReplaceOneAsync(x => x.Id == alumno.Id, alumno);
        }

        public async Task<List<Articulo>> GetArticulosAsync()
        {
            return await _articulosCollection.Find(_ => true).ToListAsync();
        }

        public async Task CreateArticuloAsync(Articulo articulo)
        {
            await _articulosCollection.InsertOneAsync(articulo);
        }

        public async Task<List<Prestamo>> GetPrestamosAsync()
        {
            return await _prestamosCollection.Find(_ => true).ToListAsync();
        }

        public async Task CreatePrestamoAsync(Prestamo prestamo)
        {
            await _prestamosCollection.InsertOneAsync(prestamo);
        }
        public async Task RestarArticuloAsync(string nombreArticulo, int cantidad)
        {
            var articulo = await _articulosCollection
                .Find(a => a.Nombre == nombreArticulo)
                .FirstOrDefaultAsync();

            if (articulo != null)
            {
                articulo.Cantidad -= cantidad;

                await _articulosCollection.ReplaceOneAsync(
                    a => a.Id == articulo.Id,
                    articulo);
            }
        }
        public async Task CreateUsuarioAsync(Usuario usuario)
        {
            await _usuariosCollection.InsertOneAsync(usuario);
        }

        public async Task<Usuario> GetUsuarioAsync(string correo, string password)
        {
            return await _usuariosCollection
                .Find(u => u.Correo == correo && u.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Docente>> GetDocentesAsync()
        {
            return await _docentesCollection.Find(_ => true).ToListAsync();
        }

        public async Task CreateDocenteAsync(Docente docente)
        {
            await _docentesCollection.InsertOneAsync(docente);
        }

        public async Task DeleteDocenteAsync(string id)
        {
            await _docentesCollection.DeleteOneAsync(d => d.Id == id);
        }

        public async Task<List<PaseSalida>> GetPasesSalidaAsync()
        {
            return await _pasesCollection.Find(_ => true).ToListAsync();
        }

        public async Task CreatePaseSalidaAsync(PaseSalida pase)
        {
            await _pasesCollection.InsertOneAsync(pase);
        }

        public async Task DeletePaseSalidaAsync(string id)
        {
            await _pasesCollection.DeleteOneAsync(p => p.Id == id);
        }
    }
}