using Assets.Src.Domain.Model.Abstract;

namespace Assets.Src.Mock
{
    public class ViewValueMock : IViewValue
    {
        public ViewValueMock(int value)
        {
            this.value = value;
        }
        public int value { get; }
        public static ViewValueMock Generate(int value = 0) => new ViewValueMock(value);
    }
}
