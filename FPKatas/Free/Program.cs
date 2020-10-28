namespace FPKatas.Free
{
    class Program
    {
        public static void Test()
        {
            var m1 = new Member1<string>("test", (int x) => x.ToString());
            var m2 = new Member2<double>("test2", (int x) => x);

            var res = (Member2<string>)m2.MapI(x => x.ToString());

            var face = new FaceBuilder();

        }
    }
}
