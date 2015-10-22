using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace AlertSense.Lumberjack.Contracts.Entities
{
    [Alias("Log")]
    public class AdoNetLog
    {
        [Alias("Id")]
        [AutoIncrement]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Thread { get; set; }

        [Required]
        public string Level { get; set; }

        [Required]
        public string Logger { get; set; }

        [Required]
        public string Message { get; set; }

        public string Exception { get; set; }
    }
}
