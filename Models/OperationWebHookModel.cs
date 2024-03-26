using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationWebHook.Models
{
    public class OperationWebHookModel
    {
        public int Id { get; set; }
        public DateTime DataMovto { get; set; }
        public DateTime DataFinalizacao { get; set; }
        public int IdStatus { get; set; }
        public string DescricaoStatus { get; set; }
        public int IdSubStatus { get; set; }
        public string DescricaoSubStatus { get; set; }
        public string UrlWebHook { get; set; }
    }
}
