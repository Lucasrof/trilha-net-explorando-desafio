using System.Text;
using System.Globalization;
using DesafioProjetoHospedagem.Models;
using System;

Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
Console.OutputEncoding = Encoding.UTF8;

// Inserindo a lista de hospedes, suite e reserva;
List<Pessoa> hospedes = new List<Pessoa>();
List<Suite> suites = new List<Suite>();
List<Reserva> reservas = new List<Reserva>();


bool sair = false;
while (!sair)
{
    ExibirMenu();
    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CadastrarHospede();
            break;
        case "2":
            ListarHospedes();
            break;
        case "3":
            CadastrarSuite();
            break;
        // case "4":
        //     ListarSuites();
        //     break;
        // case "5":
        //     FazerReserva();
        //     break;
        // case "6":
        //     ListarReservas();
        //     break;
        // case "7":
        //     CalcularValorDiaria();
        //     break;
        // case "0":
        //     sair = true;
        //     Console.WriteLine("Saindo do sistema...");
        //     break;
        default:
            Console.WriteLine("Opção inválida!");
            Console.ReadKey();
            break;
    }
}



void ExibirMenu()
{
    Console.Clear();
    Console.WriteLine("***************************");
    Console.WriteLine("*****WEX HOSPEDAGENS*****");
    Console.WriteLine("***************************");
    Console.WriteLine("1 - Cadastrar Hóspede");
    Console.WriteLine("2 - Listar Hóspedes");
    Console.WriteLine("3 - Cadastrar Suíte");
    Console.WriteLine("4 - Listar Suítes");
    Console.WriteLine("5 - Fazer Reserva");
    Console.WriteLine("6 - Listar Reservas");
    Console.WriteLine("7 - Calcular Valor da Diária de uma Reserva");
    Console.WriteLine("0 - Sair");
    Console.Write("\nEscolha uma opção: ");
}

void CadastrarHospede()
{
    Console.Clear();
    Console.WriteLine("*** CADASTRO DE HOSPEDES ***");
    Console.Write("Insira o nome do hóspede: ");
    string nome = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(nome))
    {
        Console.WriteLine("Nome inválido!");
        Console.ReadKey();
        return;
    }
    hospedes.Add(new Pessoa(nome));
    Console.WriteLine($"Hóspede {nome} cadastrado com sucesso!");
    Console.ReadKey();
} 


void ListarHospedes()
{
    
    if (hospedes.Count == 0)
    {
        Console.WriteLine("Nenhum hóspede cadastrado no sistema.");
    }
    else
    {
        for (int i = 0; i < hospedes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {hospedes[i].NomeCompleto}");
        }
    }
    Console.ReadKey();
}

void CadastrarSuite()
{
    Console.Clear();
    Console.WriteLine("*** CADASTRAR SUITE(S) ***");

    Console.WriteLine("Qual tipo da suíte?: ");
    string tipo = Console.ReadLine();

    Console.WriteLine("Capacidade de hospedes para essa suite?: ");
    // Transformo o string recebido em int e insiro na variável capacidade.
    // Verificação se o valor inserido é positivo, caso não seja, há a emissão da mensagem de alerta;
    if (!int.TryParse(Console.ReadLine(), out int capacidade) || capacidade <= 0)
    {
        Console.WriteLine("Capacidade de hospedes para suíte é inválida! Por favor insira o valor correto.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("Qual valor da diária para essa suite?:");
    // Transformo o string recebido em decimal e insiro na variável valorDiaria.
    // Verificação se o valor inserido é positivo, caso não seja, há a emissão da mensagem de alerta;
    if (!decimal.TryParse(Console.ReadLine(), out decimal valorDiaria) || valorDiaria <= 0)
    {
        Console.WriteLine("Valor da diária é inválida! Insira um valor correto.");
        Console.ReadKey();
        return;
    }

    suites.Add(new Suite(tipo, capacidade, valorDiaria));
    Console.WriteLine("Suite cadastrada com sucesso!");
    Console.ReadKey();
}


































// Pessoa p1 = new Pessoa(nome: "Lucas Rosa");
// Pessoa p2 = new Pessoa(nome: "Talita Fernandes");

// hospedes.Add(p1);
// hospedes.Add(p2);

// // Cria a suíte
// Suite suite = new Suite(tipoSuite: "Premium", capacidade: 2, valorDiaria: 30);

// // Cria uma nova reserva, passando a suíte e os hóspedes
// Reserva reserva = new Reserva(diasReservados: 10);
// reserva.CadastrarSuite(suite);
// reserva.CadastrarHospedes(hospedes);

// // Exibe a quantidade de hóspedes e o valor da diária
// Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
// Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria()}");