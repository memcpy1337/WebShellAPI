using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Model
{
    public class Values
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CmdId { get; set; }
        public string CmdText { get; set; }
        public string CmdInput { get; set; }
        public string CmdUser { get; set; }
    }
}
