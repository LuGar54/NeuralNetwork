using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Attempt3
{
    class BiasNeuron : InputNeuron
    {
        public BiasNeuron()
        {
            base.SetValue(1);
        }

        public override void SetValue(double value)
        {
            throw new Exception("bias peut pas changer");
        }
    }
}