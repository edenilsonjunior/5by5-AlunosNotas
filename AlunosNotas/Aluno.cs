using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlunosNotas
{
    internal class Aluno
    {
        // atributo estatico, ou seja, pertence à classe e nao ao objeto
        static int contador = 0;

        string nome;
        int numero;
        Aluno? anterior;

        public Aluno(string nome)
        {
            this.nome = nome;
            this.numero = ++contador;
            anterior = null;
        }

        public string GetNome()
        {
            return nome;
        }

        public int GetNumero()
        {
            return numero;
        }

        public Aluno? GetAnterior()
        {
            return anterior;
        }

        public void SetAnterior(Aluno anterior)
        {
            this.anterior = anterior;
        }

        public override string? ToString()
        {
            return $"Nome: {nome}| Numero: {numero}";
        }
    }
}
