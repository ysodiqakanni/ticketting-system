using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TickettingSystem.Core
{
    public class ClientNote: Entity
    {
        public string Notes { get; set; }
    }
}
