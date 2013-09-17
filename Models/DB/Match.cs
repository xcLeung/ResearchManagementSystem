using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DB
{
    public class Match
    {
        public int ID {get; set;}
        public String JudgeID {get; set;}
        public String MatchName {get; set;}
        public DateTime DeadLine {get; set;}
        public String Status {get; set;}
        public DateTime DeclarantDeadLine {get; set;}
        public int MatchModel {get; set;}
        public String ProjectID {get; set;}
        public String College {get; set;}
        public String Rule {get; set;}
        public String ScoreIntro {get; set;}

    }
}
