using System;
using System.Collections.Generic;

namespace Codenation.Challenge
{
    public class Math
    {
        public List<int> Fibonacci()
        {
            List<int> resp = new List<int>();
            resp.Add(0);
            resp.Add(1);
            while (resp[resp.Count - 1] + resp[resp.Count - 2] <= 350)
            {
                resp.Add(resp[resp.Count - 1] + resp[resp.Count - 2]);
            }
            return resp;
        }

        public bool IsFibonacci(int numberToTest)
        {
            List<int> seq = new List<int>();
            seq.Add(0);
            seq.Add(1);
            while (seq[seq.Count - 1] <= numberToTest)
            {
                if (seq[seq.Count - 1] == numberToTest)
                {
                    return true;
                }
                seq.Add(seq[seq.Count - 1] + seq[seq.Count - 2]);
            }
            return false;
        }
    }
}
