/*
    If a MonoBehaviour depends on gameSwtter,
    It ought to implement this interface.
    The detail of this idea is in gameSetter.cs
*/
namespace CS576.Janitor.Process
{
    public interface IRequireGameSetterInitialize 
    {
        void Initialize(GameSetter gameSetter);
    }
}
