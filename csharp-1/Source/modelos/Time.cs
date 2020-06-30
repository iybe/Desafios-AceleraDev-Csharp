using System;

namespace Modelos
{
    class Time
    {
        public long id;
        public string name;
        public DateTime dataCriacao;
        public string corUniformePrincipal;
        public string corUniformeSecundario;

        public Time(long id, string name, DateTime dataCriacao, string corUniformePrincipal, string corUniformeSecundario)
        {
            this.id = id;
            this.name = name;
            this.dataCriacao = dataCriacao;
            this.corUniformePrincipal = corUniformePrincipal;
            this.corUniformeSecundario = corUniformeSecundario;
    }

    }
}
