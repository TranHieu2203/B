using HistaffWebApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServiceImport
{
    class Program
    {
        static void Main(string[] args)
        {
            var job = new JobSheduleImportAT();
            job.Execute();
        }
    }
}
