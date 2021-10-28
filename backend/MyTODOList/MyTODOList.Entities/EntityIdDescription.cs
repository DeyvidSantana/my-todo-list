using System.ComponentModel.DataAnnotations.Schema;

namespace MyTODOList.Entities
{
    public class EntityIdDescription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}
