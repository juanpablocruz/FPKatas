namespace FPKatas.Immutability
{
    public class PostCodeBuilder : IPostCodeBuilder
    {
        private readonly string postCode;

        public PostCodeBuilder(string postCode = "")
        {
            this.postCode = postCode;
        }

        public PostCode Build()
            => new PostCode(postCode);

    }
}
