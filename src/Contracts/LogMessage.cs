using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class LogMessage
    {
        public Guid Id { get; set; }

        public int Type { get; set; }
        public string Message { get; set; }
    }
}
