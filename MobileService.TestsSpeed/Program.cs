using MobileService.TestsSpeed.GetFlashcardsListWithProgressTests;
using System;
using System.Threading.Tasks;

namespace MobileService.TestsSpeed
{
    public class Program
    {
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            await SyncVsParallel.Test();
        }
    }
}
