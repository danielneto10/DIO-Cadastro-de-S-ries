using System;
using System.Collections;
using DIO.Series.Classes;
using DIO.Series.Enums;

namespace DIO.Series
{
    class Program
    {
        static SerieRepository repositorio = new SerieRepository();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }

        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("--- DIO Séries ---");
            Console.WriteLine("Digite a opção desejada");

            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static void ObterOpcoesSerie(out int genero, out string titulo, out int ano, out string descricao)
        {
            Console.WriteLine("Gêneros");
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i}-{Enum.GetName(typeof(Genero), i)}");
            }

            Console.Write("Digite o gênero da série: ");
            while (!int.TryParse(Console.ReadLine(), out genero) || !Enum.IsDefined(typeof(Genero), genero))
            {
                Console.WriteLine("Opção inválida, tente novamente");
                Console.Write("Digite o gênero da série: ");
            }

            Console.Write("Digite o titulo da série: ");
            titulo = Console.ReadLine();

            Console.Write("Digite o ano da série: ");
            while (!int.TryParse(Console.ReadLine(), out ano))
            {
                Console.WriteLine("Opção inválida, tente novamente");
                Console.Write("Digite o gênero da série: ");
            }

            Console.Write("Digite a descrição da série: ");
            descricao = Console.ReadLine();
        }
        private static void ListarSeries()
        {
            Console.WriteLine("--- LISTA SÉRIES ---");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série encontrada");
                return;
            }

            foreach (var serie in lista)
            {
                Console.WriteLine($"#ID {serie.RetornaId()}: - {serie.RetornarTitulo()}");
            }
        }
        private static void InserirSerie()
        {
            Console.WriteLine("--- INSERIR SÉRIE ---");

            ObterOpcoesSerie(out int genero, out string titulo, out int ano, out string descricao);
            Serie serie = new Serie(repositorio.ProximoId(), (Genero)genero, titulo, descricao, ano);

            repositorio.Inserir(serie);
        }
        private static void AtualizarSerie()
        {
            Console.WriteLine("--- ATUALIZAR SÉRIE ---");

            Console.Write("ID da série que ira atualizar: ");
            int idSerie = int.Parse(Console.ReadLine());

            ObterOpcoesSerie(out int genero, out string titulo, out int ano, out string descricao);
            Serie serie = new Serie(idSerie, (Genero)genero, titulo, descricao, ano);

            repositorio.Atualizar(idSerie, serie);
        }
        private static void ExcluirSerie()
        {
            Console.WriteLine("--- EXCLUIR SÉRIE ---");
            int idSerie;
            Console.Write("ID da série que será excluida: ");
            while (!int.TryParse(Console.ReadLine(), out idSerie))
            {
                Console.WriteLine("Digite um número válido!");
                Console.Write("ID da série que será excluida: ");
            }

            try
            {
                repositorio.Exclui(idSerie);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void VisualizarSerie()
        {
            Console.WriteLine("--- VISUALIZAR SÉRIE ---");
            Console.Write("ID da série que deseja visualizar: ");
            int idSerie;
            while (!int.TryParse(Console.ReadLine(), out idSerie))
            {
                Console.WriteLine("Digite um número válido!");
                Console.Write("ID da série que deseja visualizar: ");
            }

            try
            {
                Serie serie = repositorio.RetornaPorId(idSerie);
                Console.WriteLine(serie);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private static void LimparTela()
        {
            Console.Clear();
        }
    }
}
