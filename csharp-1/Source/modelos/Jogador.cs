using System;

namespace Modelos
{
    class Jogador
    {
        public long id;
        public long teamId;
        public string name;
        public DateTime birthDate;
        public int skillLevel;
        public decimal salary;

        public Jogador(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            this.id = id;
            this.teamId = teamId;
            this.name = name;
            this.birthDate = birthDate;
            this.skillLevel = skillLevel;
            this.salary = salary;
        }
    }
}
