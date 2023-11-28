using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace API_G2.Models
{
    public class ReservaExperiencia{
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? id {get;set;}
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? experiencia_id {get;set;}
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? cliente_id {get;set;}
        public DateTime? fecha {get;set;}
        public int? participantes {get;set;}
        public int? precio_total {get;set;}
        public string? estado {get;set;}
    }
}

/**
 * @swagger
 * components:
 *   schemas:
 *     ReservaExperiencia:
 *       type: object
 *       properties:
 *         experiencia_id:
 *           type: string
 *           description: Número de identificación de experiencia turística reservada.
 *         cliente_id:
 *           type: string
 *           description: Número de identificación del cliente que ha realizado la reserva.
 *         fecha:
 *           type: DateTime
 *           description: Fecha de realización de la reserva.
 *         participantes:
 *           type: int
 *           description: Número de personas que participarán de la experiencia turística reservada.
 *         precio_total:
 *           type: int
 *           description: Valor monetario de la Reserva.
 *         estado:
 *           type: string
 *           description: Estado actual de la transacción Reserva.
 */
