using System;

namespace Codenation.Challenge
{
    public class Country
    {        
        public State[] Top10StatesByArea()
        {
            State[] resp = new State[10];
            resp[0] = new State("Amazonas", "AM");
            resp[1] = new State("Pará", "PA");
            resp[2] = new State("Mato Grosso", "MT");
            resp[3] = new State("Minas Gerais", "MG");
            resp[4] = new State("Bahia", "BA");
            resp[5] = new State("Mato Grosso do Sul", "MS");
            resp[6] = new State("Goiás", "GO");
            resp[7] = new State("Maranhão", "MA");
            resp[8] = new State("Rio Grande do Sul", "RS");
            resp[9] = new State("Tocantins", "TO");

            return resp;
        }
    }
}
