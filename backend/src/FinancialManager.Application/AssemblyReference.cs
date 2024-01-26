using System.Reflection;

namespace FinancialManager.Application
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly; 
    }
}
