using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlunosNotas
{
    internal class Nota
    {
        int numeroAluno;
        float nota;
        Nota? proximo;

        public Nota(int numeroAluno, float nota)
        {
            this.numeroAluno = numeroAluno;
            this.nota = nota;
            proximo = null;
        }

        public void SetProximo(Nota proximo)
        {
            this.proximo = proximo;
        }

        public Nota? GetProximo()
        {
            return proximo;
        }

        public int GetNumeroAluno()
        {
            return numeroAluno;
        }

        public float GetNota()
        {
            return nota;
        }

        public override string? ToString()
        {
            return $"Numero aluno: {numeroAluno}| Nota: {nota}";
        }
    }
}
