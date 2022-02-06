namespace App.Scripts.Pooled_Object
{
    public interface IPoolObject <T>
    {
        T Group { get; }
        void Create();
        void OnPush();
        void FailedPush();

    }
}