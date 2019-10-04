using System;

namespace NeuralNetwork.Attempt3
{
    [Serializable]
    class Network
    {
        private InputLayer inputLayer;
        private HiddenLayer[] hiddenLayers;
        private OutputLayer outputLayer;

        public Network()
        {
            inputLayer = new InputLayer(784);
            hiddenLayers = new HiddenLayer[2];
            hiddenLayers[0] = new HiddenLayer(inputLayer, 500);
            hiddenLayers[1] = new HiddenLayer(inputLayer, 100);
            outputLayer = new OutputLayer(hiddenLayers[hiddenLayers.Length-1], 10);
        }

        public void FeedForward(double[] inputs)
        {
            inputLayer.SetInput(inputs);
            //inputLayer.FeedForward();

            for (int i = 0; i < hiddenLayers.Length; i++)
            {
                hiddenLayers[i].FeedForward();
            }

            outputLayer.FeedForward();
        }

        public void PropagateBack(double[] targets)
        {
            outputLayer.SetTargetOutput(targets);
            outputLayer.BackPropagate();

            for (int i = hiddenLayers.Length-1; i == 0; i--)
            {
                hiddenLayers[i].BackPropagate();
            }
        }

        /// <summary>
        /// Gets the answer. ASSURE TOI DE CALL FEED AVANT DE GET
        /// </summary>
        /// <returns></returns>
        public int GetAnswer()
        {
            return outputLayer.GetHighest();
        }
    }
}