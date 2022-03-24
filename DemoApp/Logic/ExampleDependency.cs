using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Logic
{
    public class ExampleDependency : IExampleDependency
    {
        public double Multiply(double value1, double value2)
            => value1 * value2;
    }
}
