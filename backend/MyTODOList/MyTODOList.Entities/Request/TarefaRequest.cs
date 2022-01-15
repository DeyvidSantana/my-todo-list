namespace MyTODOList.Entities.Request
{
    public class TarefaRequest
    {
        public int? Id { get; set; }
        public string Descricao { get; set; }
        public bool Finalizada { get; set; }
    }
}
