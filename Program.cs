using System.Text;
using System.Globalization;
using DesafioProjetoHospedagem.Models;
using System;

Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
Console.OutputEncoding = Encoding.UTF8;

List<Pessoa> hospedes = new List<Pessoa>();
List<Suite> suites = new List<Suite>();
List<Reserva> reservas = new List<Reserva>();
// Menu principal
bool sair = false;
while(!sair)
{
    ExibirMenu();
    string opcao = Console.ReadLine();
    
    switch(opcao)
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
        case "4":
            ListarSuites();
            break;
        case "5":
            FazerReserva();
            break;
        case "6":
            ListarReservas();
            break;
        case "7":
            AvaliarHospedagem();
            break;   
        case "0":
            sair = true;
            Console.WriteLine("Saindo do sistema...");
            break;
        default:
            Console.WriteLine("Opção inválida!");
            Console.ReadKey();
            break;
    }
}

void ExibirMenu()
{
    Console.Clear();
    Console.WriteLine("****************************************************************");
    Console.WriteLine("*** Bem vindo(a) ao sistema de hospedagem da Wex Hotelaria!! ***");
    Console.WriteLine("****************************************************************");
    Console.WriteLine("1 - Cadastrar Hóspede");
    Console.WriteLine("2 - Listar Hóspedes");
    Console.WriteLine("3 - Cadastrar Suíte");
    Console.WriteLine("4 - Listar Suítes");
    Console.WriteLine("5 - Fazer Reserva");
    Console.WriteLine("6 - Listar Reservas");
    Console.WriteLine("0 - Sair");
    Console.Write("\nEscolha uma opção: ");
}

void CadastrarHospede()
{
    Console.Clear();
    Console.WriteLine("*** CADASTRAR HÓSPEDE ***");
    Console.Write("Nome do hóspede: ");
    string nome = Console.ReadLine();
    
    if(string.IsNullOrWhiteSpace(nome))
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
    Console.Clear();
    Console.WriteLine("*** LISTAGEM DE HÓSPEDES ***");
    
    if(hospedes.Count == 0)
    {
        Console.WriteLine("Nenhum hóspede cadastrado.");
    }
    else
    {
        for(int i = 0; i < hospedes.Count; i++)
        {
            Console.WriteLine($"{i+1}. {hospedes[i].Nome}");
        }
    }
    
    Console.ReadKey();
}

void CadastrarSuite()
{
    Console.Clear();
    Console.WriteLine("*** CADASTRAR SUÍTE ***");
    
    Console.Write("Tipo da suíte: ");
    string tipo = Console.ReadLine();
    
    Console.Write("Capacidade: ");
    if(!int.TryParse(Console.ReadLine(), out int capacidade) || capacidade <= 0)
    {
        Console.WriteLine("Capacidade inválida! Deve ser um número positivo.");
        Console.ReadKey();
        return;
    }
    
    Console.Write("Valor da diária: ");
    if(!decimal.TryParse(Console.ReadLine(), out decimal valorDiaria) || valorDiaria <= 0)
    {
        Console.WriteLine("Valor inválido! Deve ser um número positivo.");
        Console.ReadKey();
        return;
    }
    
    suites.Add(new Suite(tipo, capacidade, valorDiaria));
    Console.WriteLine("Suíte cadastrada com sucesso!");
    Console.ReadKey();
}

void ListarSuites()
{
    Console.Clear();
    Console.WriteLine("*** LISTAGEM  DE SUÍTES ***");
    
    if(suites.Count == 0)
    {
        Console.WriteLine("Nenhuma suíte cadastrada.");
    }
    else
    {
        for(int i = 0; i < suites.Count; i++)
        {
            Console.WriteLine($"{i+1}. Tipo: {suites[i].TipoSuite} | Capacidade: {suites[i].Capacidade} | Valor Diária: {suites[i].ValorDiaria:C}");
        }
    }
    
    Console.ReadKey();
}

void FazerReserva()
{
    Console.Clear();
    Console.WriteLine("*** FAZER RESERVA ***");
    
    if(suites.Count == 0)
    {
        Console.WriteLine("Nenhuma suíte cadastrada. Cadastre uma suíte primeiro.");
        Console.ReadKey();
        return;
    }
    
    if(hospedes.Count == 0)
    {
        Console.WriteLine("Nenhum hóspede cadastrado. Cadastre hóspedes primeiro.");
        Console.ReadKey();
        return;
    }
    
    // Selecionar suíte
    Console.WriteLine("Selecione a suíte:");
    ListarSuites();
    Console.Write("\nNúmero da suíte: ");
    if(!int.TryParse(Console.ReadLine(), out int suiteIndex) || suiteIndex < 1 || suiteIndex > suites.Count)
    {
        Console.WriteLine("Índice inválido!");
        Console.ReadKey();
        return;
    }
    Suite suiteSelecionada = suites[suiteIndex - 1];
    
    // Selecionar hóspedes
    List<Pessoa> hospedesReserva = new List<Pessoa>();
    bool continuarAdicionando = true;
    
    while(continuarAdicionando)
    {
        Console.Clear();
        Console.WriteLine("Hóspedes na reserva:");
        foreach(var h in hospedesReserva)
        {
            Console.WriteLine($"- {h.Nome}");
        }
        
        Console.WriteLine("\nHóspedes disponíveis:");
        for(int i = 0; i < hospedes.Count; i++)
        {
            if (!hospedesReserva.Contains(hospedes[i]))
            {
                Console.WriteLine($"{i + 1}. {hospedes[i].Nome}");
            }
        }
        
        Console.Write("\nSelecione o número do hóspede para adicionar (0 para finalizar): ");
        if(!int.TryParse(Console.ReadLine(), out int hospedeIndex))
        {
            Console.WriteLine("Opção inválida!");
            Console.ReadKey();
            continue;
        }
        
        if(hospedeIndex == 0)
        {
            continuarAdicionando = false;
        }
        else if(hospedeIndex < 1 || hospedeIndex > hospedes.Count)
        {
            Console.WriteLine("Índice inválido!");
            Console.ReadKey();
        }
        else if(hospedesReserva.Contains(hospedes[hospedeIndex - 1]))
        {
            Console.WriteLine("Hóspede já adicionado!");
            Console.ReadKey();
        }
        else
        {
            hospedesReserva.Add(hospedes[hospedeIndex - 1]);
            
            if(hospedesReserva.Count >= suiteSelecionada.Capacidade)
            {
                Console.WriteLine("Capacidade máxima da suíte atingida. Finalizando seleção de hóspedes.");
                continuarAdicionando = false;
                Console.ReadKey();
            }
        }
    }
    
    if(hospedesReserva.Count == 0)
    {
        Console.WriteLine("Nenhum hóspede selecionado. Reserva cancelada.");
        Console.ReadKey();
        return;
    }
    
    // Dias reservados
    Console.Write("\nQuantidade de dias que deseja reservar: ");
    if(!int.TryParse(Console.ReadLine(), out int diasReservados) || diasReservados <= 0)
    {
        Console.WriteLine("Valor inválido! Deve ser um número positivo.");
        Console.ReadKey();
        return;
    }
    
    // Criar reserva
    Reserva novaReserva = new Reserva(diasReservados);
    novaReserva.CadastrarSuite(suiteSelecionada);
    novaReserva.CadastrarHospedes(hospedesReserva);
    
    reservas.Add(novaReserva);
    Console.WriteLine("\nReserva realizada com sucesso!");
    
    // Mostrar detalhes
    Console.WriteLine($"\nDetalhes da reserva:");
    Console.WriteLine($"- Suíte: {suiteSelecionada.TipoSuite}");
    Console.WriteLine($"- Hóspedes: {novaReserva.ObterQuantidadeHospedes()}");
    Console.WriteLine($"- Dias reservados: {diasReservados}");
    Console.WriteLine($"- Valor total: {novaReserva.CalcularValorDiaria():C}");
    
    Console.ReadKey();
}

void ListarReservas()
{
    Console.Clear();
    Console.WriteLine("*** LISTAGEM DE RESERVAS ***");

    if (reservas.Count == 0)
    {
        Console.WriteLine("Nenhuma reserva cadastrada.");
    }
    else
    {
        for (int i = 0; i < reservas.Count; i++)
        {
            Console.WriteLine($"Reserva {i + 1}:");
            Console.WriteLine($"- Suíte: {reservas[i].Suite.TipoSuite}");
            Console.WriteLine($"- Hóspedes: {reservas[i].ObterQuantidadeHospedes()}");
            Console.WriteLine($"- Dias reservados: {reservas[i].DiasReservados}");
            Console.WriteLine($"- Valor total: {reservas[i].CalcularValorDiaria():C}\n");
        }
    }

    Console.ReadKey();
}

