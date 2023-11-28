using API_G2.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace API_G2.Services
{
    public class ExperienciaService
    {
        private readonly IMongoCollection<ExperienciaTuristica> experienciasCollection;
        public ExperienciaService(IOptions<MongoDBSettings> mongoDBSettings){
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            experienciasCollection = database.GetCollection<ExperienciaTuristica>("ExperienciaTuristica");
        }

        public async Task CreateAsync(ExperienciaTuristica experienciaTuristica){
            await experienciasCollection.InsertOneAsync(experienciaTuristica);
            return;
        }

        public async Task<List<ExperienciaTuristica>> GetAsync(){
            return await experienciasCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddToExperienciaAsync(string id, ExperienciaTuristica experienciaTuristica){
            if (experienciaTuristica == null)
            {
                throw new ArgumentNullException(nameof(experienciaTuristica), "El objeto DTO no puede ser nulo.");
            }

            FilterDefinition<ExperienciaTuristica> filter = Builders<ExperienciaTuristica>.Filter.Eq("id", id);
            UpdateDefinition<ExperienciaTuristica> update = Builders<ExperienciaTuristica>.Update
                .Set("titulo", experienciaTuristica.titulo ?? "")
                .Set("descripcion", experienciaTuristica.descripcion ?? "")
                .Set("ubicacion", experienciaTuristica.ubicacion ?? "")
                .Set("duracion", experienciaTuristica.duracion ?? 0)
                .Set("precio", experienciaTuristica.precio ?? 0)
                .Set("inclusiones", experienciaTuristica.inclusiones ?? "")
                .Set("restricciones", experienciaTuristica.restricciones ?? "")
                .Set("proveedor_id", experienciaTuristica.proveedor_id ?? 0);

            await experienciasCollection.UpdateOneAsync(filter, update);
            return;
        }    

        public async Task DeleteAsync(string id){
            FilterDefinition<ExperienciaTuristica> filter = Builders<ExperienciaTuristica>.Filter.Eq("id", id);
            await experienciasCollection.DeleteOneAsync(filter);
            return;
        }
    }
}

/**
 * @swagger
 * tags:
 *   name: Experiencia Turística
 *   description: Acciones relacionadas con alta, baja, modificación y consulta (CRUD) de Experiencias Turísticas
 */


 /**
 * @swagger
 * /experiencias:
 *   get:
 *     summary: Obtener todas las Experiencias Turísticas
 *     tags: [Experiencias Turísticas]
 *     description: Obtiene una lista de todas las Experiencias Turísticas disponibles.
 *     responses:
 *       '200':
 *         description: Lista de Experiencias Turísticas obtenida con éxito.
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
 *                       titulo:
 *                       type: string
 *                       description: El título de la Experiencia Turística.
 *                     descripcion:
 *                       type: string
 *                       description: La descripción de la Experiencia Turística.
 *                     ubicacion:
 *                       type: string
 *                       description: Lugar donde se realizará la Experienecia Turística.
 *                     duracion:
 *                       type: decimal
 *                       description: Tiempo de duración de la Experiencia Turística.
 *                     precio:
 *                       type: int
 *                       description: Valor monetario de la Experiencia Turística.
 *                     inclusiones:
 *                       type: string
 *                       description: Servicios complementarios brindados por el pago de la Experiencia Turística.
 *                     restricciones:
 *                       type: string
 *                       description: Reglas o prohibiciones a cumplir para realizar la Experiencia Turística.
 *                     proveedor_id:
 *                       type: int
 *                       description: Número de identificación del proveedor.
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
 * /experiencias:
 *   post:
 *     summary: Crear una Experiencia Turística.
 *     tags: [Experiencia Turística]
 *     description: Crea una nueva Experiencia Turística.
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 experiencias:
 *                   type: array
 *                   items:
 *                     type: object
 *                     properties:
 *                       titulo:
 *                       type: string
 *                       description: El título de la Experiencia Turística.
 *                     descripcion:
 *                       type: string
 *                       description: La descripción de la Experiencia Turística.
 *                     ubicacion:
 *                       type: string
 *                       description: Lugar donde se realizará la Experienecia Turística.
 *                     duracion:
 *                       type: decimal
 *                       description: Tiempo de duración de la Experiencia Turística.
 *                     precio:
 *                       type: int
 *                       description: Valor monetario de la Experiencia Turística.
 *                     inclusiones:
 *                       type: string
 *                       description: Servicios complementarios brindados por el pago de la Experiencia Turística.
 *                     restricciones:
 *                       type: string
 *                       description: Reglas o prohibiciones a cumplir para realizar la Experiencia Turística.
 *                     proveedor_id:
 *                       type: int
 *                       description: Número de identificación del proveedor.
 *     responses:
 *       '201':
 *         description: Experiencia Turistica creada exitosamente.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 msg:
 *                   type: string
 *                   description: Experiencia Turística creada exitosamente
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


/**
 * @swagger
 * /experiencias:
 *   put:
 *     summary: Actualizar datos de una Experiencia Turística por ID
 *     tags: [Experiencia Turística]
 *     description: Actualiza una Experiencia Turística existente mediante su ID.
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         description: ID de la experiencia turística a actualizar.
 *         schema:
 *           type: string
 *           format: mongoId
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 experiencias:
 *                   type: array
 *                   items:
 *                     type: object
 *                     properties:
 *                       titulo:
 *                       type: string
 *                       description: El título de la Experiencia Turística.
 *                     descripcion:
 *                       type: string
 *                       description: La descripción de la Experiencia Turística.
 *                     ubicacion:
 *                       type: string
 *                       description: Lugar donde se realizará la Experienecia Turística.
 *                     duracion:
 *                       type: decimal
 *                       description: Tiempo de duración de la Experiencia Turística.
 *                     precio:
 *                       type: int
 *                       description: Valor monetario de la Experiencia Turística.
 *                     inclusiones:
 *                       type: string
 *                       description: Servicios complementarios brindados por el pago de la Experiencia Turística.
 *                     restricciones:
 *                       type: string
 *                       description: Reglas o prohibiciones a cumplir para realizar la Experiencia Turística.
 *                     proveedor_id:
 *                       type: int
 *                       description: Número de identificación del proveedor.
 *     responses:
 *       '200':
 *         description: Experiencia Turística actualizada exitosamente.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 msg:
 *                   type: string
 *                   description: La Experiencia Turística fue modificada exitosamente
 *       '404':
 *         description: No se encontró una Experiencia Turística con el ID proporcionado.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 error:
 *                   type: string
 *                   description: No existe un Experiencia Turística con ID {id}.
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
 *                   description: Mensaje de error.
 *     security:
 *       - bearerAuth: []
 */




/**
 * @swagger
 * /experiencias:
 *   delete:
 *     summary: Eliminar una Experiencia Turística por ID
 *     tags: [Experiencia Turística]
 *     description: Elimina una Experiencia Turística existente mediante su ID.
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         description: ID de la Experiencia Turística a eliminar.
 *         schema:
 *           type: string
 *           format: mongoId
 *     responses:
 *       '200':
 *         description: Experiencia Turística eliminada exitosamente.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 msg:
 *                   type: string
 *                   description: La Experiencia Turística con ID {id} fue eliminado exitosamente.
 *       '404':
 *         description: No se encontró una Experiencia Turística con el ID proporcionado.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 error:
 *                   type: string
 *                   description: No existe una Experiencia Turística con ID {id}.
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
 *                         description: Ubicación del error (por ejemplo, "params").
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