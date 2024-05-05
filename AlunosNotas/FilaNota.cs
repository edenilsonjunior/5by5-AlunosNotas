using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlunosNotas
{
    internal class FilaNota
    {
        Nota? head;
        Nota? tail;
        int tamanho;

        public FilaNota()
        {
            this.head = null;
            this.tail = null;
        }

        public void Enqueue(Nota aux)
        {
            if (IsEmpty())
            {
                head = aux;
                tail = aux;
            }
            else
            {
                tail.SetProximo(aux);
                tail = aux;
            }
            tamanho++;
        }

        public Nota? Dequeue()
        {
            if (!IsEmpty())
            {
                Nota aux = head;

                if (head == tail)
                    head = tail = null;
                else
                    head = head.GetProximo();

                tamanho--;

                return aux;
            }
            return null;
        }

        public int GetTamanho()
        {
            return tamanho;
        }

        public void Print()
        {
            Console.WriteLine("Notas cadastradas:");
            if (IsEmpty())
            {
                Console.WriteLine("-->Nao existem notas cadastradas!");
            }
            else
            {
                Nota aux = head;

                while (aux != null)
                {
                    Console.WriteLine($"--> {aux}");
                    aux = aux.GetProximo();
                }
            }
        }

        public int GetQntNotasPorAluno(int numeroAluno)
        {
            int qnt = 0;

            if (!IsEmpty())
            {
                Nota aux = head;

                while (aux != null)
                {
                    if(aux.GetNumeroAluno() == numeroAluno)
                    {
                        qnt++;
                    }

                    aux = aux.GetProximo();
                }
            }
            return qnt;
        }

        public bool IsEmpty()
        {
            return head == null && tail == null;
        }
    }
}
