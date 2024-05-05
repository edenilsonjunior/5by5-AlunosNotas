using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlunosNotas
{
    internal class PilhaAluno
    {
        Aluno? topo;
        int tamanho;

        public PilhaAluno()
        {
            topo = null;
            tamanho = 0;
        }

        public void Push(Aluno aux)
        {
            if (IsEmpty())
            {
                topo = aux;
            }
            else
            {
                aux.SetAnterior(topo);
                topo = aux;
            }

            tamanho++;
        }

        public Aluno? Pop()
        {
            Aluno? removido = null;

            if (!IsEmpty())
            {
                removido = topo;
                topo = topo.GetAnterior();

                tamanho--;
            }
            removido.SetAnterior(null);
            return removido;
        }

        public int GetTamanho()
        {
            return tamanho;
        }

        public bool IsEmpty()
        {
            return topo == null;
        }

        public bool ExisteAluno(string nomeAluno)
        {
            if (!IsEmpty())
            {
                Aluno aux = topo;

                while (aux != null)
                {
                    if(aux.GetNome().Equals(nomeAluno))
                    {
                        return true;
                    }
                    aux = aux.GetAnterior();
                }
            }

            return false;
        }

        public bool ExisteAluno(int numeroAluno)
        {
            if (!IsEmpty())
            {
                Aluno aux = topo;

                while (aux != null)
                {
                    if (aux.GetNumero() == numeroAluno)
                    {
                        return true;
                    }
                    aux = aux.GetAnterior();
                }
            }

            return false;
        }


        public void Print()
        {
            Console.WriteLine("Alunos cadastrados:");

            if (IsEmpty())
            {
                Console.WriteLine("-->Nao existem alunos cadastrados!");
            }
            else
            {
                Aluno aux = topo;

                while (aux != null)
                {
                    Console.WriteLine($"--> {aux}");
                    aux = aux.GetAnterior();
                }
            }
        }
    }
}