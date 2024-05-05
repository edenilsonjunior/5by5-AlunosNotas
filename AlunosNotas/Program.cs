using System.Security;

namespace AlunosNotas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FilaNota filaNota = new FilaNota();
            PilhaAluno pilhaAluno = new PilhaAluno();

            while (true)
            {
                int opcao = Menu();

                switch (opcao)
                {
                    case 1:
                        CriarAluno(pilhaAluno);
                        break;
                    case 2:
                        CriarNota(pilhaAluno, filaNota);
                        break;
                    case 3:
                        CalcularMedia(pilhaAluno, filaNota);
                        break;
                    case 4:
                        ListarAlunosSemNota(pilhaAluno, filaNota);
                        break;
                    case 5:
                        ExcluirAluno(pilhaAluno, filaNota);
                        break;
                    case 6:
                        ExcluirNotas(pilhaAluno, filaNota);
                        break;
                    case 0:
                        Console.WriteLine("Saindo...");
                        Environment.Exit(0);
                        break;
                    case -1:
                        Console.WriteLine();
                        filaNota.Print();
                        Console.WriteLine();
                        pilhaAluno.Print();
                        Console.WriteLine();
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opcao inválida!");
                        Console.WriteLine("Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static int Menu()
        {
            Console.Clear();
            Console.WriteLine("======Alunos e notas======");

            Console.WriteLine("Escolha uma opcao");
            Console.WriteLine("1- Cadastrar aluno");
            Console.WriteLine("2- Cadastrar nota");
            Console.WriteLine("3- Calcular a media do aluno");
            Console.WriteLine("4- Listar o nome dos alunos sem nota");
            Console.WriteLine("5- Excluir aluno");
            Console.WriteLine("6- Excluir nota");
            Console.WriteLine("0- Sair");
            Console.Write("R: ");

            bool conversao = int.TryParse(Console.ReadLine(), out int option);

            if (!conversao)
            {
                ExibirMensagem("Voce deve digitar um numero!");
                return Menu();
            }

            return option;
        }

        static void CriarAluno(PilhaAluno pilhaAluno)
        {
            Console.WriteLine("======Criar Aluno======");

            pilhaAluno.Print();
            Console.WriteLine("==========================");

            Console.WriteLine("Digite o nome do aluno:");
            Console.Write("R: ");
            string nomeAluno = Console.ReadLine();

            if (pilhaAluno.ExisteAluno(nomeAluno))
            {
                ExibirMensagem("Ja existe um aluno com esse nome!");
            }
            else
            {
                pilhaAluno.Push(new(nomeAluno));
                ExibirMensagem("Aluno adicionado com sucesso!");
            }
        }

        static void CriarNota(PilhaAluno pilhaAluno, FilaNota filaNota)
        {

            Console.WriteLine("======Adicionar Nota======");

            pilhaAluno.Print();
            Console.WriteLine("==========================");

            Console.WriteLine("Digite o numero do aluno:");
            Console.Write("R: ");

            // Verifica se o usuario digitou um numero
            if (!int.TryParse(Console.ReadLine(), out int numeroAluno))
            {
                ExibirMensagem("Voce deve digitar um numero!");
                return;
            }

            // Verifica se existe aluno cadastrado com esse numero
            if (!pilhaAluno.ExisteAluno(numeroAluno))
            {
                ExibirMensagem("Nao existe um aluno com esse numero no sistema!");
                return;
            }

            if (filaNota.GetQntNotasPorAluno(numeroAluno) == 2)
            {
                ExibirMensagem("O aluno ja possui duas notas cadastradas!");
            }
            else
            {
                Console.WriteLine("Digite a nota do aluno:");
                Console.Write("R: ");

                if (!float.TryParse(Console.ReadLine(), out float notaAluno) || notaAluno < 0)
                {
                    ExibirMensagem("Voce deve digitar um numero e maior que zero!");
                }
                else
                {
                    Nota aux = new Nota(numeroAluno, notaAluno);
                    filaNota.Enqueue(aux);
                    ExibirMensagem("Nota cadastrada com sucesso!");
                }
            }

        }


        static void CalcularMedia(PilhaAluno pilhaAluno, FilaNota filaNota)
        {
            Console.WriteLine("======Calcular Media======");

            pilhaAluno.Print();
            Console.WriteLine("==========================");

            Console.WriteLine("Digite o numero do aluno:");
            Console.Write("R: ");

            if (!int.TryParse(Console.ReadLine(), out int numeroAluno))
            {
                ExibirMensagem("Voce deve digitar um numero!");
                return;
            }

            if (!pilhaAluno.ExisteAluno(numeroAluno))
            {
                ExibirMensagem("Nao existe aluno com esse numero!");
                return;
            }

            if (filaNota.GetQntNotasPorAluno(numeroAluno) == 2)
            {
                double media = 0;
                FilaNota filaAux = new();

                while (!filaNota.IsEmpty())
                {
                    Nota aux = filaNota.Dequeue();
                    filaAux.Enqueue(aux);

                    media += aux.GetNota();
                }

                while (!filaAux.IsEmpty())
                {
                    filaNota.Enqueue(filaAux.Dequeue());
                }

                media = media / 2;
                ExibirMensagem($"Media do aluno--> {media}");
            }
            else
            {
                ExibirMensagem("O aluno possui menos que duas notas cadastradas!");
            }
        }

        static void ListarAlunosSemNota(PilhaAluno pilhaAluno, FilaNota filaNota)
        {
            PilhaAluno pilhaAux = new PilhaAluno();
            int qntAlunosSemNota = 0;

            Console.WriteLine("Alunos sem nota:");

            if (!pilhaAluno.IsEmpty())
            {
                string alunos = "";
                // Percorrendo todos os alunos
                while (!pilhaAluno.IsEmpty())
                {

                    Aluno alunoAux = pilhaAluno.Pop();
                    int numeroAluno = alunoAux.GetNumero();

                    if (filaNota.GetQntNotasPorAluno(numeroAluno) == 0)
                    {
                        alunos += $"--> {alunoAux}\n";
                        qntAlunosSemNota++;
                    }

                    pilhaAux.Push(alunoAux);
                }

                // re-populando a pilha de alunos original de volta
                while (!pilhaAux.IsEmpty())
                {
                    pilhaAluno.Push(pilhaAux.Pop());
                }

                ExibirMensagem(alunos);
            }

            if (qntAlunosSemNota == 0)
            {
                ExibirMensagem("Nao existe alunos sem notas!");
            }
        }

        static void ExcluirAluno(PilhaAluno pilhaAluno, FilaNota filaNota)
        {
            Console.WriteLine("======Excluir Aluno======");

            pilhaAluno.Print();
            Console.WriteLine("==========================");

            Console.WriteLine("Digite o numero do aluno:");
            Console.Write("R: ");

            if (!int.TryParse(Console.ReadLine(), out int numeroAluno))
            {
                ExibirMensagem("Voce deve digitar um numero!");
                return;
            }

            if (filaNota.GetQntNotasPorAluno(numeroAluno) != 0)
            {
                ExibirMensagem("Nao é possivel excluir esse aluno pois ainda existem notas vinculadas a ele!");
            }
            else
            {
                bool achouAluno = false;
                string alunoRemovido = "";
                PilhaAluno pilhaAux = new PilhaAluno();
                
                while (!pilhaAluno.IsEmpty())
                {
                    Aluno aux = pilhaAluno.Pop();

                    if (aux.GetNumero() == numeroAluno)
                    {
                        alunoRemovido = $"Aluno removido:\n{aux}";
                        achouAluno = true;
                    }
                    else
                    {
                        pilhaAux.Push(aux);
                    }
                }
                if (!achouAluno)
                {
                    ExibirMensagem("Nao existe aluno com esse numero!");
                }
                else
                {
                    ExibirMensagem(alunoRemovido);
                }

                while (!pilhaAux.IsEmpty())
                {
                    pilhaAluno.Push(pilhaAux.Pop());
                }
            }
        }

        static void ExcluirNotas(PilhaAluno pilhaAluno, FilaNota filaNota)
        {
            Console.WriteLine("======Excluir Notas======");

            pilhaAluno.Print();
            Console.WriteLine("==========================");

            Console.WriteLine("Digite o numero do aluno:");
            Console.Write("R: ");

            if (!int.TryParse(Console.ReadLine(), out int numeroAluno))
            {
                ExibirMensagem("Voce deve digitar um numero!");
                return;
            }

            if (filaNota.GetQntNotasPorAluno(numeroAluno) == 0)
            {
                ExibirMensagem("Esse aluno nao possui notas!");
            }
            else
            {
                FilaNota filaAux = new();
                bool removeuNotas = false;
                string notas = "";
                while (!filaNota.IsEmpty())
                {
                    Nota aux = filaNota.Dequeue();

                    if (aux.GetNumeroAluno() == numeroAluno)
                    {
                        notas += $"Nota removida--> {aux}\n";
                        removeuNotas = true;
                    }
                    else
                    {
                        filaAux.Enqueue(aux);
                    }
                }

                if (removeuNotas)
                {
                    ExibirMensagem(notas);
                }

                // re-populando a fila de notas original
                while (!filaAux.IsEmpty())
                {
                    filaNota.Enqueue(filaAux.Dequeue());
                }
            }
        }

        static void ExibirMensagem(string msg)
        {
            Console.WriteLine(msg);
            Console.Write("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }
    }
}
