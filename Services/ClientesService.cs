using API_G2.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace API_G2.Services
{
    public class ClientesService
    {
        private readonly IMongoCollection<ReservaExperiencia> reservasCollection;
        public ClientesService(IOptions<MongoDBSettings> mongoDBSettings){
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            reservasCollection = database.GetCollection<ReservaExperiencia>("ReservaExperiencia");
        }

        public async Task<List<ReservaExperiencia>> GetReservasPorClienteAsync(string id){
            var filter = Builders<ReservaExperiencia>.Filter.Eq("cliente_id", id);
            return await reservasCollection.Find(filter).ToListAsync();
        }
    }
}

/**
 * @swagger
 * tags:
 *   name: Clientes
 *   description: Acciones relacionadas con clientes
 */




/**
 * @swagger
 *   /clientes/{id_cliente}/reservas-experiencias
 *   get:
 *     summary: Obtener todas las experiencias turísticas reservadoas por un cliente
 *     tags: [Clientes]
 *     description: Obtiene una lista de todos los experiencias turísticas reservadas por un cliente.
 *     parameters:
 *       - in: path
 *         name: id_cliente
 *         required: true
 *         description: ID del cliente.
 *         schema:
 *           type: number
 *           minimum: 0
 *           example: 123
 *     responses:
 *       '200':
 *         description: Lista de experiencias turísticas reservadas obtenidas exitosamente.
 *         content:
 *           application/json:
 *             schema:
 *               type: array
 *               items: Experiencias turísticas
 *       '400':
 *         description: Error en la solicitud debido a datos incorrectos.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 msg:
 *                   type: string
 *                   description: Hubo un error al validar los campos.
 *                 errors:
 *                   type: array
 *                   description: Listado de errores de validación de datos de entrada.
 *                   items:
 *                     type: object
 *                     properties:
 *                       type:
 *                         type: string
 *                         description: Tipo de error.
 *                       value:
 *                         type: string
 *                         description: Valor que causó el error.
 *                       msg:
 *                         type: string
 *                         description: Mensaje de error específico.
 *                       path:
 *                         type: string
 *                         description: Ruta del campo que causó el error.
 *                       location:
 *                         type: string
 *                         description: Ubicación del error (por ejemplo, "body").
 *       '500':
 *         description: Error de servidor interno.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 error:
 *                   type: string
 *                   description: Error de servidor.
 */