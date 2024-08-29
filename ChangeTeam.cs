using CounterStrikeSharp.API.Core;

namespace ChangeTeams
{
    public class ChangeTeams : BasePlugin
    {
        public override string ModuleAuthor => "Marius";
        public override string ModuleName => "ChangeTeams";
        public override string ModuleVersion => "0.0.5";
        public static ChangeTeams Instance { get; set; } = new ChangeTeams();

        public Random Random = new Random();

        public override void Load(bool hotReload)
        {
            Console.WriteLine("░█████╗░██╗░░██╗░█████╗░███╗░░██╗░██████╗░███████╗████████╗███████╗░█████╗░███╗░░░███╗");
            Console.WriteLine("██╔══██╗██║░░██║██╔══██╗████╗░██║██╔════╝░██╔════╝╚══██╔══╝██╔════╝██╔══██╗████╗░████║");
            Console.WriteLine("██║░░╚═╝███████║███████║██╔██╗██║██║░░██╗░█████╗░░░░░██║░░░█████╗░░███████║██╔████╔██║");
            Console.WriteLine("██║░░██╗██╔══██║██╔══██║██║╚████║██║░░╚██╗██╔══╝░░░░░██║░░░██╔══╝░░██╔══██║██║╚██╔╝██║");
            Console.WriteLine("╚█████╔╝██║░░██║██║░░██║██║░╚███║╚██████╔╝███████╗░░░██║░░░███████╗██║░░██║██║░╚═╝░██║");
            Console.WriteLine("╚════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝░╚═════╝░╚══════╝░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═╝░░░░░╚═╝ ");

            Instance = this;
            Config_Config.Load();
            Commands.Load();
        }
    }
}
