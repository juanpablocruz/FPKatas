using System;

namespace FPKatas.EffectExample
{

    using static EffectExtension;

    public class EffectExample
    {
        public void Example()
        {
            Func<int> fZero = () =>
            {
                Console.WriteLine("Launching missiles");
                return 0;
            };

            
            Func<int,int> duplicar = x => x * 2;
            Func<int, int> cube = x => x * x * x;
            Func<int,int> increment = x => x + 1;

            var eight = effect(fZero)
                    .map(increment)
                    .map(x => x)
                    .map(duplicar)
                    .map(cube);

            eight.runEffects();
        }
    }
}
