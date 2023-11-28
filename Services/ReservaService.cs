using API_G2.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace API_G2.Services
{
    public class ReservaService
    {
        private readonly IMongoCollection<ReservaExperiencia> reservasCollection;
        public ReservaService(IOptions<MongoDBSettings> mongoDBSettings){
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            reservasCollection = database.GetCollection<ReservaExperiencia>("ReservaExperiencia");
        }

        public async Task CreateAsync(ReservaExperiencia reservaExperiencia){
            await reservasCollection.InsertOneAsync(reservaExperiencia);
            return;
        }

        public async Task<List<ReservaExperiencia>> GetAsync(){
            return await reservasCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task DeleteAsync(string id){
            FilterDefinition<ReservaExperiencia> filter = Builders<ReservaExperiencia>.Filter.Eq("id", id);
            await reservasCollection.DeleteOneAsync(filter);
            return;
        }
    }
}

/**
 * @swagger
 * tags:
 *   name: Reservas de Experiencias Turística
 *   description: Acciones relacionadas Reservas de Experiencias Turística.
 */






/**
 * @swagger
 * /reservas:
 *   get:
 *     summary: Obtener todas las reservas de experiencias turísticas
 *     tags: [Reservas de Experiencias Turística]
 *     description: Obtiene una lista de todas las Reservas de Experiencias Turística.
 *     responses:
 *       '200':
 *         description: Lista de Reservas de Experiencias Turística obtenida exitosamente.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 experiencias:
 *                   type: array
 *                   items:
 *                     type: object
 *                     properties:
 *                 experiencia_id:
 *                  type: string
 *                  description: Número de identificación de experiencia turística reservada.
 *                cliente_id:
 *                  type: string
 *                  description: Número de identificación del cliente que ha realizado la reserva.
 *                fecha:
 *                  type: DateTime
 *                  description: Fecha de realización de la reserva.
 *                participantes:
 *                  type: int
 *                  description: Número de personas que participarán de la experiencia turística reservada.
 *                precio_total:
 *                  type: int
 *                  description: Valor monetario de la Reserva.
 *                estado:
 *                  type: string
 *                  description: Estado actual de la transacción Reserva.
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
 *     security:
 *       - bearerAuth: []
 */




/**
 * @swagger
 * /reservas:
 *   post:
 *     summary: Crear una reservas de experiencias turísticas
 *     tags: [Reservas de Experiencias Turística]
 *     description: Crea una nueva Reservas de Experiencias Turística.
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             properties:
 *                 experienciasTuristicas:
 *                   type: array
 *                   items:
 *                     type: object
 *                     properties:
 *                 experiencia_id:
 *                  type: string
 *                  description: Número de identificación de experiencia turística reservada.
 *                cliente_id:
 *                  type: string
 *                  description: Número de identificación del cliente que ha realizado la reserva.
 *                fecha:
 *                  type: DateTime
 *                  description: Fecha de realización de la reserva.
 *                participantes:
 *                  type: int
 *                  description: Número de personas que participarán de la experiencia turística reservada.
 *                precio_total:
 *                  type: int
 *                  description: Valor monetario de la Reserva.
 *                estado:
 *                  type: string
 *                  description: Estado actual de la transacción Reserva.
 *     responses:
 *       '201':
 *         description: Reserva de Experiencias Turística creada exitosamente.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 msg:
 *                   type: string
 *                   description: Reserva de Experiencias Turística creado exitosamente.
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
 *     security:
 *       - bearerAuth: []
 */