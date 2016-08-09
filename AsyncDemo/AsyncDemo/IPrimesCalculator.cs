using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncDemo
{
    interface IPrimesCalculator
    {
        IEnumerable<int> CalcPrimes(int from, int to);
    }
}