using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class MatchJuryRelation
    {
        public int Id { get; set; }
        public int JudgeId { get; set; }
        public int MatchId { get; set; }
        public bool Enable { get; set; }

    }
}
