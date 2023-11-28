using MongoDB.Bson.Serialization.Attributes;

namespace API_G2.Models
{

    public class ExperienciaTuristica{
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? id {get;set;}
        public string? titulo {get;set;}
        public string? descripcion {get;set;}
        public string? ubicacion {get;set;}
        public decimal? duracion {get;set;}
        public int? precio {get;set;}
        public string? inclusiones {get;set;}
        public string? restricciones {get;set;}
        public int? proveedor_id {get;set;}
    }
}

/**
 * @swagger
 * components:
 *   schemas:
 *     ExperienciaTurística:
 *       type: object
 *       properties:
 *         titulo:
 *           type: string
 *           description: El título de la Experiencia Turística.
 *         descripcion:
 *           type: string
 *           description: La descripción de la Experiencia Turística.
 *         ubicacion:
 *           type: string
 *           description: Lugar donde se realizará la Experienecia Turística.
 *         duracion:
 *           type: decimal
 *           description: Tiempo de duración de la Experiencia Turística.
 *         precio:
 *           type: int
 *           description: Valor monetario de la Experiencia Turística.
 *         inclusiones:
 *           type: string
 *           description: Servicios complementarios brindados por el pago de la Experiencia Turística.
 *         restricciones:
 *           type: string
 *           description: Reglas o prohibiciones a cumplir para realizar la Experiencia Turística.
 *         proveedor_id:
 *           type: int
 *           description: Número de identificación del proveedor.
 */